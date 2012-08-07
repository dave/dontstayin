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
