using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Bobs.DataHolders;

namespace Bobs
{
	public class SecPay
	{
		private const string CURRENT_ACCOUNT_USER_NAME = "develo02";
        private const string CURRENT_ACCOUNT_PASSWORD = "Blue-1234";
        //private const string CLIENT_ACCOUNT_USER_NAME = "dontst02";
        //private const string CLIENT_ACCOUNT_PASSWORD = "Green-4321";

		private const string SPACE_REPLACER = "+";
		private const string MONEY_FORMAT = "0.00";
		private const string DATE_FORMAT = "MMyy";

		Spotted.com.secpay.www.SECVPNService secVpnService = new Spotted.com.secpay.www.SECVPNService();

		private string cardNumber = "";
		private string cardAddressCity = "";
		private string cardAddressCountry = "UK";
        private string userName = CURRENT_ACCOUNT_USER_NAME;
        private string password = CURRENT_ACCOUNT_PASSWORD;
        private string ipAddress = Visit.HasCurrent ? Visit.Current.IpAddress : "";
		private decimal amount = 0;
		private string response = "";
		private List<InvoiceDataHolder> invoiceDataHolders = new List<InvoiceDataHolder>();
		private List<Invoice> invoices = new List<Invoice>();
		private Usr usr = new Usr();
		private Transfer transfer = new Transfer();
		private Transfer.FraudCheckEnum fraudCheckEnum = Transfer.FraudCheckEnum.Relaxed;
		private bool PerformSecurityCheck { get { return this.fraudCheckEnum == Transfer.FraudCheckEnum.Strict; } }
		
		// Minimum details
		public SecPay()
		{
		}

		#region Properties
		public List<Invoice> Invoices
		{
			get { return this.invoices; }
		}

		public Transfer Transfer
		{
			get { return this.transfer; }
		}
		#endregion

		#region Public Methods
		public void MakePayment(List<InvoiceDataHolder> invoices, decimal amount, Usr usr, int promoterK, int actionUsrK, string cardFullName, string cardAddressStreet, string cardAddressArea, string cardAddressTown, string cardAddressCounty, int cardAddressCountryK, string cardAddressPostCode, string cardAddressCountry, string cardNumber, DateTime cardExpiryDate, string cardCV2, Transfer.FraudCheckEnum fraudCheckEnum, bool saveCard, Guid duplicateGuid)
		{
			MakePayment(invoices, amount, usr, promoterK, actionUsrK, cardFullName, cardAddressStreet, cardAddressArea, cardAddressTown, cardAddressCounty, cardAddressCountryK, cardAddressPostCode, cardAddressCountry, cardNumber, cardExpiryDate, cardCV2, fraudCheckEnum, saveCard, duplicateGuid, DateTime.MinValue, "");
		}

		public void MakePayment(List<InvoiceDataHolder> invoices, decimal amount, Usr usr, int promoterK, int actionUsrK, string cardFullName, string cardAddressStreet, string cardAddressArea, string cardAddressTown, string cardAddressCounty, int cardAddressCountryK, string cardAddressPostCode, string cardAddressCountry, string cardNumber, DateTime cardExpiryDate, string cardCV2, Transfer.FraudCheckEnum fraudCheckEnum, bool saveCard, Guid duplicateGuid, DateTime cardStartDate, string cardIssueNumber)
		{
			this.amount = Math.Round(amount, 2);
			if (this.amount <= 0)
			{
				throw new Exception("Cannot make a payment for " + this.amount.ToString("c") + ". It must be a positive amount.");
			}
			else
			{
				this.cardNumber = cardNumber.Trim().Replace(" ", "");
				this.cardAddressCountry = cardAddressCountry.Trim();
				this.fraudCheckEnum = fraudCheckEnum;
				this.invoiceDataHolders = invoices;
				this.transfer.Guid = Guid.NewGuid();
				this.transfer.DuplicateGuid = duplicateGuid;
				this.transfer.Amount = this.amount;
				this.transfer.Type = Transfer.TransferTypes.Payment;
				this.transfer.Method = Transfer.Methods.Card;
				this.transfer.Company = Model.Entities.Transfer.CompanyEnum.DH;
				this.transfer.CardAddress1 = cardAddressStreet.Trim();
				this.transfer.CardAddressArea = cardAddressArea;
				this.transfer.CardAddressCounty = cardAddressCounty;
				this.transfer.CardAddressTown = cardAddressTown;
				this.transfer.CardAddressCountryK = cardAddressCountryK;
				this.transfer.CardPostcode = cardAddressPostCode.Trim();
				this.transfer.CardName = cardFullName.Trim();
				this.transfer.CardCV2 = cardCV2.Trim();
				this.transfer.CardExpires = cardExpiryDate;
				this.transfer.CardStart = cardStartDate;
				if (cardIssueNumber.Length > 0)
					this.transfer.CardIssue = Convert.ToInt32(cardIssueNumber);
				this.transfer.SetUsrAndActionUsr(usr);
				this.transfer.DateTimeCreated = DateTime.Now;
				this.transfer.PromoterK = promoterK;
				this.usr = usr;

				this.transfer.StoreCardEndAndHashAndCardType(this.cardNumber);

				ValidateMinimumDetails();

				try
				{
					response = secVpnService.validateCardFull(
									userName,
									password,
									transfer.Guid.ToString(),				// DSI created SecPay Transaction Id "tran0001"
                                    ipAddress,								// Credit Card user IP address "127.0.0.1"
									cardFullName,							// User Name "Mr Cardholder"
									cardNumber,								// User card number "4444333322221111"
									transfer.Amount.ToString(MONEY_FORMAT),	// because of deferred=true in Options, will only do shadow payment of just £1 while checking details validate
									DateToString(cardExpiryDate),			// expiry date "1208"
									cardIssueNumber,						// Card Issue Number
									DateToString(cardStartDate),			// start date "0102"
									OrderToString(),
									ShippingToString(),
									BillingToString(),
									OptionsToString());  					// options from Options() "name=Fred+Bloggs,company=Online+Shop+Ltd,addr_1=Dotcom+House,addr_2=London+Road,city=Townville,state=Countyshire,post_code=AB1+C23,tel=01234+567+890,fax=09876+543+210,email=somebody%40secpay.com,url=http%3A%2F%2Fwww.somedomain.com,test_status=true,dups=false,card_type=Visa,mand_cv2avs=[false|true,deferred=true]"

					// Log SecPay Response to transfer and save transfer details
					ResponseParser(this.PerformSecurityCheck);

					if (this.transfer.Status.Equals(Transfer.StatusEnum.Success))
					{
						this.transfer.CardSaved = saveCard;

						// Make sure there is only one saved transfer for each card
						if (saveCard == true)
						{
							TransferSet savedTransferSet = new TransferSet(new Query(new And(new Q(Transfer.Columns.CardNumberHash, this.transfer.CardNumberHash),
																							 new Q(Transfer.Columns.CardSaved, true))));
							foreach (Transfer savedTransfer in savedTransferSet)
							{
								savedTransfer.CardSaved = false;
								savedTransfer.Update();
							}
						}

						if (this.PerformSecurityCheck)
						{
							response = secVpnService.releaseCardFull(
								userName,
								password,
								transfer.Guid.ToString(),
								transfer.Amount.ToString(MONEY_FORMAT),
								password,
								transfer.Guid.ToString());

							// run this through again with the new response
							ResponseParser();
						}
					}

					ProcessPaymentResults();
				}
				catch (Exception ex)
				{
					EmailSecPayException(ex);

					throw new Exception("SecPay Payment failed. See transfer #" + this.transfer.K.ToString() + " for details");
				}
			}
		}

		public void MakeRefund(Transfer transferToRefund, Guid duplicateGuid, int actionUsrK)
		{
			MakeRefund(transferToRefund, duplicateGuid, actionUsrK, transferToRefund.Amount);
		}

		public void MakeRefund(Transfer transferToRefund, Guid duplicateGuid, int actionUsrK, decimal refundAmount)
		{
			// SecPay takes refund amount as positive
			refundAmount = Math.Abs(Math.Round(refundAmount, 2));
			// Copy all details from transferToRefund to the new transfer
			this.transfer = transferToRefund.RefundThisTransfer(refundAmount);
            
			this.transfer.ActionUsrK = actionUsrK;

			this.transfer.Guid = Guid.NewGuid();
			this.transfer.DuplicateGuid = duplicateGuid;

			this.transfer.AddNote("Refund for transfer #" + transferToRefund.K.ToString(), "SecPay");
			this.transfer.DateTimeCreated = DateTime.Now;

			try
			{
				if (!Vars.DevEnv)
				{
					response = secVpnService.refundCardFull(
											userName,
											password,
											transferToRefund.Guid.ToString(),			// DSI created SecPay Transaction Id as GUID
											refundAmount.ToString(MONEY_FORMAT),
											password,
											this.transfer.Guid.ToString());				// DSI created SecPay Transaction Id as GUID

					// Parse thru to see if it was successful
					ResponseParser();
				}
				else
				{
					this.transfer.Status = Transfer.StatusEnum.Success;
				}
				this.transfer.DateTimeComplete = this.transfer.DateTimeCreated;

				this.transfer.Update();

				if (this.transfer.Status.Equals(Transfer.StatusEnum.Success))
				{
					try
					{
						Utilities.EmailTransfer(transfer, true, true);
					}
					catch (Exception ex)
					{
						EmailSecPayException(ex);
					}
				}
			}
			catch (Exception ex)
			{
				EmailSecPayException(ex);

				throw new DsiUserFriendlyException("SecPay Refund failed. See transfer #" + this.transfer.K + " for details");
			}
		}

		public void MakePaymentUsingSavedTransferDetails(List<InvoiceDataHolder> invoices, decimal amount, Usr usr, Transfer previouslySavedTransfer, Transfer.FraudCheckEnum fraudCheckEnum, Guid duplicateGuid)
		{
			if (previouslySavedTransfer.UsrK != usr.K)
				throw new Exception("User doesn't match!");

			if (!previouslySavedTransfer.CardSaved)
				throw new Exception("Card not saved!");

			this.amount = Math.Round(amount, 2);
			if (this.amount <= 0)
			{
				throw new Exception("Cannot make a payment for " + this.amount.ToString("c") + ". It must be a positive amount.");
			}
			else
			{
				this.invoiceDataHolders = invoices;
				this.fraudCheckEnum = fraudCheckEnum;
                
                //this.SelectDSIBankAccount(invoices);
				
                this.transfer = previouslySavedTransfer.CopyThisTransfer();
				// Previously saved transfer could be using different promoter account
				if (invoices.Count > 0)
					this.transfer.PromoterK = invoices[0].PromoterK;
				this.transfer.DuplicateGuid = duplicateGuid;

				this.transfer.SetUsrAndActionUsr(usr);
				this.transfer.CardSavedTransferK = previouslySavedTransfer.K;

				if (this.transfer.PromoterK == 0 && invoices.Count > 0)
					this.transfer.PromoterK = invoices[0].PromoterK;

				this.transfer.Amount = this.amount;

				this.transfer.Type = Transfer.TransferTypes.Payment;
				this.transfer.DateTimeCreated = DateTime.Now;
				try
				{
					response = secVpnService.repeatCardFullAddr(
									userName,
									password,
									previouslySavedTransfer.Guid.ToString(),// DSI created SecPay Transaction Id "tran0001"
									transfer.Amount.ToString(MONEY_FORMAT),	// amount "50.00"
                                    password,
									transfer.Guid.ToString(),				// DSI created SecPay Transaction Id "tran0001"
									DateToString(transfer.CardExpires),		// User card number "4444333322221111"
									OrderToString(),
									BillingToString(),
									ShippingToString(),
									OptionsToString());  					// options from Options() "name=Fred+Bloggs,company=Online+Shop+Ltd,addr_1=Dotcom+House,addr_2=London+Road,city=Townville,state=Countyshire,post_code=AB1+C23,tel=01234+567+890,fax=09876+543+210,email=somebody%40secpay.com,url=http%3A%2F%2Fwww.somedomain.com,test_status=true,dups=false,card_type=Visa,mand_cv2avs=[false|true,deferred=true]"

					// Log SecPay Response to transfer and save transfer details
					ResponseParser(this.PerformSecurityCheck);

					if (this.transfer.Status == Transfer.StatusEnum.Success && this.PerformSecurityCheck)
					{
						response = secVpnService.releaseCardFull(
							userName,
							password,
							transfer.Guid.ToString(),
							transfer.Amount.ToString(MONEY_FORMAT),
							password,
							transfer.Guid.ToString());

						// run this through again with the new response
						ResponseParser();
					}

					ProcessPaymentResults();
				}
				catch (Exception ex)
				{
					EmailSecPayException(ex);

					throw new Exception("SecPay Refund failed. See transfer #" + this.transfer.K + " for details");
				}
			}
		}

		#endregion

		#region Private Methods
		private void ProcessPaymentResults()
		{
			this.transfer.DateTimeComplete = this.transfer.DateTimeCreated;

			this.transfer.Update();

			if (this.transfer.Status.Equals(Transfer.StatusEnum.Success))
			{
				try
				{
					Invoice invoice = new Invoice();
					// Apply to invoices
					foreach (InvoiceDataHolder idh in invoiceDataHolders)
					{
						bool creatingInvoice = false;

						// Get Invoice.K for unsaved invoices
						if (idh.K == 0)
						{
							creatingInvoice = true;
							idh.Type = Invoice.Types.Invoice;
							idh.VatCode = Invoice.VATCodes.T1;
							invoice = idh.UpdateInsertDelete();
						}
						else
						{
							invoice = new Invoice(idh.K);
						}
                        
						InvoiceTransfer invoiceTransfer = new InvoiceTransfer();
						invoiceTransfer.InvoiceK = invoice.K;
						invoiceTransfer.TransferK = this.transfer.K;
						if (amount >= invoice.Total)
							invoiceTransfer.Amount = invoice.Total;
						else
							invoiceTransfer.Amount = amount;

						amount -= invoiceTransfer.Amount;

						invoiceTransfer.Update();

						if (invoiceTransfer.Amount == invoice.Total && creatingInvoice)
							invoice.IsImmediateCreditCardPayment = true;
                        invoice.AssignBuyerType();
						invoice.UpdateAndAutoApplySuccessfulTransfersWithAvailableMoney();//.UpdateAndSetPaidStatus();
						if (creatingInvoice)
						{
							if (invoice.Paid && invoice.DueDateTime > invoice.PaidDateTime)
							{
								invoice.DueDateTime = invoice.PaidDateTime;
								invoice.Update();
							}
							invoice.Process();
						}
						this.invoices.Add(invoice);
					}

					if (Math.Round(amount, 2) == 0)
					{
						this.transfer.IsFullyApplied = true;
						this.transfer.Update();
					}
					Utilities.EmailTransfer(transfer, true, true);
				}
				catch (Exception ex)
				{
					EmailSecPayException(ex);
				}
			}
		}

		private void EmailSecPayException(Exception ex)
		{
			string[] recipientAddresses = new string[] { };
			string details = "";
			if (Vars.DevEnv)
			{
				details = "Test - ";
				recipientAddresses = new string[] { Vars.EMAIL_ADDRESS_TIMI };
			}
			else
				recipientAddresses = new string[] { Vars.EMAIL_ADDRESS_DAVE, Vars.EMAIL_ADDRESS_TIM, Vars.EMAIL_ADDRESS_TIMI };

			if (response.Equals(""))
				details += "SecPay exception occurred while sending " + this.transfer.Type.ToString().ToLower() + " to SecPay.";
			else
				details += "SecPay exception occurred after " + this.transfer.Type.ToString().ToLower() + " processed. Transfer K= " + this.transfer.K.ToString() + ", Status= " + this.transfer.Status.ToString();

			List<IBobAsHTML> bobsAsHTML = new List<IBobAsHTML>();
			bobsAsHTML.Add(this.Transfer);
			if (Usr.Current != null)
				bobsAsHTML.Add(Usr.Current);

			Utilities.AdminEmailAlert(details, details, ex, bobsAsHTML, recipientAddresses);
		}

		private void ValidateMinimumDetails()
		{
			if (this.transfer.CardName.Length == 0)
                throw new DsiUserFriendlyException("Invalid user name");

			if (cardNumber.Length != 16 && cardNumber.Length != 18 && cardNumber.Length != 19 && cardNumber.Length != 20)
                throw new DsiUserFriendlyException("Invalid card number");

			if (this.transfer.CardCV2.Length < 3 || this.transfer.CardCV2.Length > 4)
                throw new DsiUserFriendlyException("Invalid card CV2");

			if (this.transfer.CardStart != DateTime.MinValue && this.transfer.CardStart > DateTime.Now)
                throw new DsiUserFriendlyException("Invalid start date");

			if (this.transfer.CardExpires < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
                throw new DsiUserFriendlyException("Invalid expiry date");

            if (this.userName.Length == 0 || this.password.Length == 0)
                throw new DsiUserFriendlyException("DSI cardnet account details were not set. Please contact an administrator.");
		}

		private string DateToString(DateTime dt)
		{
			if (dt.Equals(DateTime.MinValue))
				return "";
			else
				return dt.ToString(DATE_FORMAT);
		}

		private string OrderToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < invoices.Count; i++ )
			{
				sb.Append("prod=Invoice #");
				sb.Append(invoices[i].K.ToString());
				sb.Append(",item_amount=");
				sb.Append(invoices[i].Total.ToString(MONEY_FORMAT));
				if(i+1 < invoices.Count)
					sb.Append(",");
			}
			return sb.ToString();
		}

		/// <summary>
		/// Currently just a method place holder to allow easier future development
		/// </summary>
		/// <returns></returns>
		private string ShippingToString()
		{
			return "";
		}

		private string BillingToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("name=");
			sb.Append(this.transfer.CardName.Replace(",", ""));
			if (this.transfer.CardAddress1.Length > 0)
			{
				sb.Append(",addr_1=");
				sb.Append(this.transfer.CardAddress1.Replace(",", ""));
			}
			if (this.cardAddressCity.Length > 0)
			{
				sb.Append(",city=");
				sb.Append(this.cardAddressCity.Replace(",", ""));
			}
			if (this.cardAddressCountry.Length > 0)
			{
				sb.Append(",country=");
				sb.Append(this.cardAddressCountry.Replace(",", ""));
			}
			if (this.transfer.CardPostcode.Length > 0)
			{
				sb.Append(",post_code=");
				sb.Append(this.transfer.CardPostcode.Replace(",", ""));
			}

			return sb.ToString().Replace(" ", SPACE_REPLACER);
		}

		private string OptionsToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("test_status=");
			if (Vars.DevEnv == true)
				sb.Append("true");
			else
				// Set to LIVE for live environment
				sb.Append("live");

			if (this.transfer.CardType.Equals(BinRange.Types.AmericanExpress))
			{
				sb.Append(",amex=true");	
			}
			sb.Append(",card_type=");
			sb.Append(BinRange.TypeToString(this.transfer.CardType));
			sb.Append(",cv2=");
			sb.Append(this.transfer.CardCV2);

			sb.Append(",mand_cv2avs=");
			if(this.PerformSecurityCheck)
				sb.Append("true,deferred=true");
			else
				sb.Append("false");

			return sb.ToString().Replace(" ", SPACE_REPLACER);
		}

		/// <summary>
		/// There are several response parts in the response string that are delimited by "&"
		/// </summary>
		/// <param name="response"></param>
		private void ResponseParser()
		{
			ResponseParser(false);
		}
		private void ResponseParser(bool isResponseFromSecurityCheck)
		{
			const string AMOUNT_KEY = "amount";
			const string AUTH_CODE_KEY = "auth_code";
			const string CODE_KEY = "code";
			const string CORRECT_KEY = "correct";
			const string CURRENCY_KEY = "currency";
			const string CV2AVS_KEY = "cv2avs";
			const string HASH_KEY = "hash";
			const string MESSAGE_KEY = "message";
			const string TEST_KEY = "test_status";
			const string TRANSACTION_ID_KEY = "trans_id";
			const string VALID_KEY = "valid";
			//const string RESPONSE_CODE_KEY = "resp_code";

			// SecPay response is inconsistent. Sometimes it begins with "?". We remove it so our ResponseParser has a more consistent string to work with
			if (response.IndexOf("?") == 0)
				response = response.Substring(1);
			string[] responseParts = response.Split('&');
			
			// Load them into a hashtable
			Hashtable hashtable = new Hashtable();
			foreach (string s in responseParts)
			{
				string key = s.Substring(0,s.IndexOf("="));
				string value = s.Substring(s.IndexOf("=") + 1);
				hashtable[key] = value;
			}

			// Retrieve from hashtable.  Since not all key value pairs are returned, we test for null on each.
			if (hashtable[AMOUNT_KEY] != null && !isResponseFromSecurityCheck)
			{
				decimal responseAmount = Convert.ToDecimal(hashtable[AMOUNT_KEY]);
				if (this.transfer.Amount != responseAmount)
					this.transfer.AddNote("SecPay response amount " + responseAmount.ToString(MONEY_FORMAT)
										+ " does not equal the submitted amount for the invoice " + this.transfer.Amount.ToString(MONEY_FORMAT), "SecPay");
				this.transfer.Amount = responseAmount;
			}
			if (hashtable[AUTH_CODE_KEY] != null)
			{
				this.transfer.CardResponseAuthCode = hashtable[AUTH_CODE_KEY].ToString();
			}
			if(hashtable[CODE_KEY] != null)
			{
				this.transfer.CardResponseCode = hashtable[CODE_KEY].ToString();
			}
			if(hashtable[CORRECT_KEY] != null)
			{
				// Only appears when valid is false
			}
			if(hashtable[CURRENCY_KEY] != null)
			{
				// Not currently doing transactions other than £GBP
			}
			if (hashtable[MESSAGE_KEY] != null)
			{
				this.transfer.CardResponseMessage = hashtable[MESSAGE_KEY].ToString();
			}
			// Since SecPay returns the validation message in Cv2Avs when its successful, but adds it to the CardResponseMessage when it fails
			// we therefore should check both to make sure we capture the validation information
			if (hashtable[CV2AVS_KEY] != null || this.transfer.CardResponseMessage.Length > 0)
			{
				if(hashtable[CV2AVS_KEY] != null)
					this.transfer.CardResponseCv2Avs = hashtable[CV2AVS_KEY].ToString();

				string cardRespCv2Avs = this.transfer.CardResponseCv2Avs.ToUpper();
				string cardRespMsg = this.transfer.CardResponseMessage.ToUpper();

				if(cardRespCv2Avs.Contains("ALL MATCH") || cardRespMsg.Contains("ALL MATCH"))
				{
					this.transfer.CardResponseIsAddressMatch = true;
					this.transfer.CardResponseIsCv2Match = true;
					this.transfer.CardResponseIsPostCodeMatch = true; 
					this.transfer.CardResponseIsDataChecked = true;
				}
				else if(cardRespCv2Avs.Contains("SECURITY CODE MATCH ONLY") || cardRespMsg.Contains("SECURITY CODE MATCH ONLY"))
				{
					this.transfer.CardResponseIsAddressMatch = false;
					this.transfer.CardResponseIsCv2Match = true;
					this.transfer.CardResponseIsPostCodeMatch = false;
					this.transfer.CardResponseIsDataChecked = true;
				}
				else if(cardRespCv2Avs.Contains("ADDRESS MATCH ONLY") || cardRespMsg.Contains("ADDRESS MATCH ONLY"))
				{
					this.transfer.CardResponseIsAddressMatch = true;
					this.transfer.CardResponseIsCv2Match = false;
					this.transfer.CardResponseIsPostCodeMatch = false;
					this.transfer.CardResponseIsDataChecked = true;
				}
				else if(cardRespCv2Avs.Contains("NO DATA MATCHES") || cardRespMsg.Contains("NO DATA MATCHES"))
				{
					this.transfer.CardResponseIsAddressMatch = false;
					this.transfer.CardResponseIsCv2Match = false;
					this.transfer.CardResponseIsPostCodeMatch = false;
					this.transfer.CardResponseIsDataChecked = true;
				}
				else if(cardRespCv2Avs.Contains("DATA NOT CHECKED") || cardRespMsg.Contains("DATA NOT CHECKED"))
				{
					this.transfer.CardResponseIsDataChecked = false;
				}
				else if(cardRespCv2Avs.Contains("PARTIAL ADDRESS MATCH / POSTCODE") || cardRespMsg.Contains("PARTIAL ADDRESS MATCH / POSTCODE"))
				{
					this.transfer.CardResponseIsAddressMatch = false;
					this.transfer.CardResponseIsCv2Match = false;
					this.transfer.CardResponseIsPostCodeMatch = true;
					this.transfer.CardResponseIsDataChecked = true;
				}
				else if(cardRespCv2Avs.Contains("PARTIAL ADDRESS MATCH / ADDRESS") || cardRespMsg.Contains("PARTIAL ADDRESS MATCH / ADDRESS"))
				{
					this.transfer.CardResponseIsAddressMatch = true;
					this.transfer.CardResponseIsCv2Match = false;
					this.transfer.CardResponseIsPostCodeMatch = false;
					this.transfer.CardResponseIsDataChecked = true;
				}
				else if(cardRespCv2Avs.Contains("SECURITY CODE MATCH / POSTCODE") || cardRespMsg.Contains("SECURITY CODE MATCH / POSTCODE"))
				{
					this.transfer.CardResponseIsAddressMatch = false;
					this.transfer.CardResponseIsCv2Match = true;
					this.transfer.CardResponseIsPostCodeMatch = true;
					this.transfer.CardResponseIsDataChecked = true;
				}
				else if(cardRespCv2Avs.Contains("SECURITY CODE MATCH / ADDRESS") || cardRespMsg.Contains("SECURITY CODE MATCH / ADDRESS"))
				{
					this.transfer.CardResponseIsAddressMatch = true;
					this.transfer.CardResponseIsCv2Match = true;
					this.transfer.CardResponseIsPostCodeMatch = false;
					this.transfer.CardResponseIsDataChecked = true;
				}
			}
			if(hashtable[HASH_KEY] != null)
			{
				// Not presently storing Hash information
			}
			if(hashtable[TEST_KEY] != null)
			{
				this.transfer.AddNote("Test status of this transfer = " + hashtable[TEST_KEY].ToString(), "SecPay");
			}
			if(hashtable[TRANSACTION_ID_KEY] != null)
			{
				if(!this.transfer.Guid.ToString().Equals(hashtable[TRANSACTION_ID_KEY].ToString()))
				{
					this.transfer.AddNote("Transaction Id did not match. Applying new transaction Id.", "SecPay");
					this.transfer.Guid = new Guid(hashtable[TRANSACTION_ID_KEY].ToString());
				}
			}
			if (hashtable[VALID_KEY] != null)
			{
				if (hashtable[VALID_KEY].ToString().ToUpper().Equals("TRUE"))
				{
					this.transfer.Status = Transfer.StatusEnum.Success;
				}
				else
					this.transfer.Status = Transfer.StatusEnum.Failed;
			}
		}
		#endregion
	}
}
