using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Local;
using System.Text.RegularExpressions;
using Bobs.DataHolders;
using Common.Clocks;
using Common;

namespace Spotted.Controls
{
	public partial class Payment : System.Web.UI.UserControl
	{

		private void CheckSslStatus()
		{
			if (!Common.Properties.IsDevelopmentEnvironment && !Request.IsSecureConnection)
			{
				throw new Exception("Payment control used in non-secure request!");
			}
		}

		#region Public properties

		#region PaymentTogglePayOptionsScript
		protected string PaymentTogglePayOptionsScript { get; set; }
		private void RegisterPaymentOptionControlInToggleScript(Panel panel, RadioButton radioButton)
		{
			if (panel.Visible)
			{
				PaymentTogglePayOptionsScript += "document.getElementById(\"" + panel.ClientID + "\").style.display = document.getElementById(\"" + 
					radioButton.ClientID + "\").checked?'':'none';\n";
			}
		}
		#endregion

		#region Card processing properties
		#region FraudCheck
		public Transfer.FraudCheckEnum FraudCheck
		{
			get
			{
				if (this.ViewState["FraudCheck"] == null)
					return Transfer.FraudCheckEnum.Relaxed;

				return (Transfer.FraudCheckEnum)this.ViewState["FraudCheck"];
			}
			set
			{
				this.ViewState["FraudCheck"] = value;
				ShowHideCountryDropDownList();
				//fraudCheck = value;
			}
		}
		//Transfer.FraudCheckEnum fraudCheck = Transfer.FraudCheckEnum.Relaxed;
		#endregion

	 	#endregion

		int? LockedCountryK
		{
			get
			{
				return ViewState["LockedCountryK"] as int?;
			}
		}
		public void LockCountryK(int? countryK)
		{
			if (countryK > 0)
			{
				ViewState["LockedCountryK"] = countryK;
			}
		}
		public bool AllowSavedCards
		{
			get
			{
				return ViewState["AllowSavedCards"] as bool? ?? true;
			}
			set
			{
				ViewState["AllowSavedCards"] = value;
			}
		}

		#region ShowItemsIncVat
		public bool ShowItemsIncVat
		{
			get
			{
				if (this.ViewState["ShowItemsIncVat"] != null)
					return (bool)this.ViewState["ShowItemsIncVat"];
				else
					return false;
			}
			set
			{
				this.ViewState["ShowItemsIncVat"] = value;
			}
		}
		#endregion
		#region AllowPayWithBalance
		public bool AllowPayWithBalance
		{
			get { return this.ViewState["AllowPayWithBalance"] as bool? ?? true; }
			set { this.ViewState["AllowPayWithBalance"] = value; }
		}
		#endregion
		#region
		public bool GetFullAddress
		{
			get { return this.ViewState["GetFullAddress"] as bool? ?? false; }
			set 
			{ 
				this.ViewState["GetFullAddress"] = value;
				uiFullAddressRow1.Visible = value;
				uiFullAddressRow2.Visible = value;
				AllowSavedCards = false;
			}
		}
		#endregion
		#region AllowPayWithCredit
		public bool AllowPayWithCredit
		{
			get
			{
				if (this.ViewState["AllowPayWithCredit"] != null)
					return (bool)this.ViewState["AllowPayWithCredit"];
				else
					return true;
			}
			set
			{
				this.ViewState["AllowPayWithCredit"] = value;
			}
		}
		#endregion
		#region HideValidationSummary
		public bool HideValidationSummary
		{
			get
			{
				if (this.ViewState["HideValidationSummary"] != null)
					return (bool)this.ViewState["HideValidationSummary"];
				else
					return false;
			}
			set
			{
				this.ViewState["HideValidationSummary"] = value;
			}
		}
		#endregion

		#region PromoterK
		public int PromoterK
		{
			get
			{
				if (this.ViewState["PromoterK"] != null)
					return (int)this.ViewState["PromoterK"];
				else
					return 0;
			}
			set
			{
				ResetCurrentBuyer();
				this.ViewState["PromoterK"] = value;
			}
		}
		#endregion

		#region UsrK
		public int UsrK
		{
			get
			{
				if (this.ViewState["UsrK"] != null)
					return (int)this.ViewState["UsrK"];
				else
					return 0;
			}
			set
			{
				ResetCurrentBuyer();
				this.ViewState["UsrK"] = value;
			}
		}
		#endregion

		#region AllowPayWithCampaignCredits
		public bool AllowPayWithCampaignCredits
		{
			get
			{
				if (this.ViewState["AllowPayWithCampaignCredits"] == null)
				{
					this.ViewState["AllowPayWithCampaignCredits"] = true;
				}
				return (bool)this.ViewState["AllowPayWithCampaignCredits"];
			}
			set
			{
				this.ViewState["AllowPayWithCampaignCredits"] = value;
			}
		}

		#endregion

		#region Invoices
		public List<InvoiceDataHolder> Invoices
		{
			get
			{
				if (invoices == null)
					invoices = new List<InvoiceDataHolder>();
				return this.invoices;
			}
			set
			{
				this.invoices = value;
			}
		}
		private List<InvoiceDataHolder> invoices;
		#endregion

		#region CampaignCredits
		private List<CampaignCredit> campaignCredits;
		public List<CampaignCredit> CampaignCredits
		{
			get
			{
				if (campaignCredits == null)
					campaignCredits = new List<CampaignCredit>();
				return this.campaignCredits;
			}
			set
			{
				this.campaignCredits = value;
			}
		}
		#endregion

		#region SecPay
		public SecPay SecPay
		{
			get
			{
				return secPay;
			}
		}
		private SecPay secPay;
		#endregion
		#endregion

		#region Global helpers

		#region CurrentPromoter
		public Promoter CurrentPromoter
		{
			get
			{
				if (currentPromoter == null && PromoterK > 0)
					currentPromoter = new Promoter(PromoterK);
				return currentPromoter;
			}
			set
			{
				currentPromoter = value;
			}
		}
		private Promoter currentPromoter;
		#endregion

		#region CurrentUsr
		public Usr CurrentUsr
		{
			get
			{
				if (currentUsr == null && UsrK > 0)
					currentUsr = new Usr(UsrK);
				if (currentUsr == null)
					currentUsr = Usr.Current;
				return currentUsr;
			}
			set
			{
				currentUsr = value;
			}
		}
		private Usr currentUsr;
		#endregion

		#region CurrentBuyer
		public IBuyer CurrentBuyer
		{
			get
			{
				if (CurrentPromoter != null)
					return (IBuyer)CurrentPromoter;
				else
					return (IBuyer)CurrentUsr;
			}
		}

		#endregion

		#region ResetCurrentBuyer()
		void ResetCurrentBuyer()
		{
			currentBuyerBalance = 0.0m;
			currentBuyerBalanceDone = false;
			currentPromoter = null;
			currentUsr = null;
		}

		#endregion

		#region CurrentBuyerBalance
		/// <summary>
		/// This is the balance of the buyer account +ve where DSI owes the buyer, -ve where the buyer owes DSI
		/// </summary>
		public decimal CurrentBuyerBalance
		{
			get
			{
				if (CurrentBuyer == null || CurrentBuyer.K == 0)
					return 0.0m;

				if (!currentBuyerBalanceDone && AllowPayWithBalance)
				{
					currentBuyerBalanceDone = true;
					if (CurrentBuyer != null)
						currentBuyerBalance = CurrentBuyer.GetBalance();
				}
				return currentBuyerBalance;
			}
		}
		private decimal currentBuyerBalance = 0.0m;
		private bool currentBuyerBalanceDone = false;
		#endregion

		#region CurrentBuyerPositiveBalance
		/// <summary>
		/// This is the positive balance of the promoter account, this is zero if the promoter owes DSI money
		/// </summary>
		public decimal CurrentBuyerPositiveBalance
		{
			get
			{
				if (CurrentBuyerBalance > 0.0m)
					return CurrentBuyerBalance;
				else
					return 0.0m;
			}
		}
		#endregion

		#region CurrentBuyerAvailableCredit
		/// <summary>
		/// This is the credit available on the IBuyer account. This doesn't include any positive balance - it's only the credit.
		/// </summary>
		public decimal CurrentBuyerAvailableCredit
		{
			get
			{
				if (CurrentBuyer == null || CurrentBuyer.K == 0)
					return 0.0m;

				if (CurrentBuyerBalance < 0)
				{
					if (CurrentBuyer.CreditLimit + CurrentBuyerBalance <= 0)
						return 0;
					else
						return CurrentBuyer.CreditLimit + CurrentBuyerBalance;
				}
				else
					return CurrentBuyer.CreditLimit;
			}
		}
		#endregion

		#region TotalDueExceedsAvailableCredit
		/// <summary>
		/// Does the total due after balance (InvoiceTotalAfterBalance) exceeds the available credit?
		/// </summary>
		bool TotalDueExceedsAvailableCredit
		{
			get
			{
				return InvoiceTotalAfterBalance > CurrentBuyerAvailableCredit;
			}
		}
		#endregion

		#region InvoiceDueDays
		int InvoiceDueDays
		{
			get
			{
				if (CurrentBuyer.K == 0)
					return 0;
				else
					return CurrentBuyer.InvoiceDueDaysEffective;
			}
		}
		#endregion

		#region Constants + Readonly
		private const string RETRY_INVOICE_ERROR_MSG = "Error occurred in processing your purchase.<br>Please try again.<br>If error occurs again, please contact an administrator for assistance.";
		private const string CALL_ADMIN_INVOICE_ERROR_MSG = "Error occurred during processing.<br>Please contact an administrator for assistance.";
		private const string INVOICE_DONE_WITH_ERRORS = "Your purchase has been processed, but an error occurred in processing one of the items.<br>One of our administrators will contact you shortly.";
		private const string ITEMS_NOT_READY_FOR_PROCESSING = "Some of the items you've selected are not ready for processing.<br>Please try again.<br>If this continues to occur, please contact an administrator for assistance.";
		private readonly string INVOICE_ITEMS_LOCKED = "Some of the items you are trying to update are locked. Please try again in {0} seconds.";
		private const string ITEMS_ALREADY_PROCESSED = "Some of the items you've selected have already been processed.<br>Please confirm which items have already been processed before continuing with payment.";
		#endregion
			
		#endregion

		#region Money helpers

		// InvoiceTotal - Raw total from invoices (taking into account partial transfers and credits)
		// InvoiceTotalAfterBalance - Raw invoices total minus any positive balance on the CurrentBuyer account
		// InvoiceTotalAfterBalanceAndCredit - Raw invoices total minus any positive balance on the CurrentBuyer account minus any credit applied

		// BalanceApplied - total positive balance applied to this invoice
		// CreditApplied - total credit applied to this invoice

		#region ResetMoneyHelpers()
		void ResetMoneyHelpers()
		{
			invoiceTotal = 0.0m;
			invoiceTotalDone = false;
		}
		#endregion

		#region Total
		private void ProcessInvoiceTotals()
		{
			invoiceTotalDone = true;

			invoiceTotal = 0.0m;
			invoiceTotalAsCampaignCredits = 0;
			decimal totalMoneyForFixedDiscountCampaignCredits = 0;
			int totalNumberOfFixedDiscountCampaignCredits = 0;

			if (CampaignCredits.Count > 0)
			{
				string invoiceItemDescription = "";

				paymentTypeIsCampaignCredits = true;
				bool fixedDiscountUsed = false;
				foreach (CampaignCredit cc in CampaignCredits)
				{
					if (cc.BuyableObject != null && cc.BuyableObject is IBuyableCredits)
					{
						// We need to regenerate campaign credits from saved buyable object to verify any price changes.
						List<CampaignCredit> generatedCampaignCredits = ((IBuyableCredits)cc.BuyableObject).ToCampaignCredits(Usr.Current, CurrentPromoter.K, false);
						foreach (CampaignCredit genCC in generatedCampaignCredits)
						{
							if (genCC.InvoiceItemType == cc.InvoiceItemType)
							{
								cc.Credits = genCC.Credits;
								cc.IsPriceFixed = genCC.IsPriceFixed;
								cc.FixedDiscount = genCC.FixedDiscount;
								cc.Description = genCC.Description;
								cc.Update();
								break;
							}
						}

						invoiceItemDescription += cc.Description + " | ";
					}
					// campaign credits come in negative
					invoiceTotalAsCampaignCredits += (-cc.Credits);
					if (cc.IsPriceFixed)
					{
						fixedDiscountUsed = true;
						totalMoneyForFixedDiscountCampaignCredits += CampaignCredit.CalculateTotalCostForCredits(-cc.Credits, cc.FixedDiscount, cc.Promoter);
						totalNumberOfFixedDiscountCampaignCredits += -cc.Credits;
					}
				}

				if (invoiceItemDescription.Length > 2)
					invoiceItemDescription = invoiceItemDescription.Substring(0, invoiceItemDescription.Length - 3);

				AllowPayWithCampaignCredits = !fixedDiscountUsed;
				this.PayOptionRadioButtonPayWithCampaignCredit.Enabled = AllowPayWithCampaignCredits;

				if (!fixedDiscountUsed)
				{
					invoiceTotalAsCampaignCredits -= CampaignCreditsApplied;
				}
				double nonFixedCampaignCreditDiscount = CampaignCredit.GetDiscountForCredits(invoiceTotalAsCampaignCredits, CurrentPromoter);
				totalCampaignCreditsAsMoney = Math.Round(CampaignCredit.CalculateTotalCostForCredits(invoiceTotalAsCampaignCredits - totalNumberOfFixedDiscountCampaignCredits, nonFixedCampaignCreditDiscount, CurrentPromoter) + totalMoneyForFixedDiscountCampaignCredits, 2);


				// then we actually want to make the equivalent money cost to be the cost of the total credits
				// including bulk discount
				
				totalVatOnCampaignCreditsAsMoney = Math.Round(((decimal)Invoice.VATRate(Invoice.VATCodes.T1, DateTime.Now)) * totalCampaignCreditsAsMoney, 2);
				invoiceTotal = Math.Round(totalCampaignCreditsAsMoney + totalVatOnCampaignCreditsAsMoney, 2);
                //ViewState["InvoiceTotal"] = invoiceTotal;

				// avoid adding this invoice item multiple times
                if (Invoices.Count == 0)
                {
					SetInvoiceTotalAsCampaignCreditsDiscount(fixedDiscountUsed);
                    // Generate Positive Campaign Credit to balance account and an invoice to pay for it
                    CampaignCredit campaignCredit = new CampaignCredit()
                    {
                        Credits = invoiceTotalAsCampaignCredits,
                        ActionDateTime = DateTime.Now,
                        BuyableLockDateTime = DateTime.Now,
                        BuyableObjectType = Model.Entities.ObjectType.Invoice,
                        Description = invoiceTotalAsCampaignCredits.ToString() + " credits",
                        Enabled = false,
                        PromoterK = CurrentPromoter.K,
                        InvoiceItemType = InvoiceItem.Types.CampaignCredits,
                        DisplayOrder = 0
                    };
					if(invoiceItemDescription.Length > 0)
						campaignCredit.Description += " [" + invoiceItemDescription + "]";

					if (fixedDiscountUsed)
						campaignCredit.FixedDiscount = invoiceTotalAsCampaignCreditsDiscount;
					campaignCredit.SetUsrAndActionUsr(Usr.Current, false);
                    campaignCredit.Update();
                    InvoiceDataHolder idh = new InvoiceDataHolder()
                    {
                        ActionUsrK = Usr.Current.K,
                        CreatedDateTime = DateTime.Now,
                        DueDateTime = DateTime.Now,
                        Paid = false,
                        PromoterK = CurrentPromoter.K,
                        Type = Invoice.Types.Invoice,
                        VatCode = Invoice.VATCodes.T1
                    };
                    InvoiceItemDataHolder iidh = new InvoiceItemDataHolder()
                    {
                        BuyableObjectK = campaignCredit.K,
                        BuyableObjectType = Model.Entities.ObjectType.CampaignCredit,
                        Description = campaignCredit.Credits.ToString() + " credits",
                        PriceBeforeDiscount = campaignCredit.Credits * CurrentPromoter.CostPerCampaignCredit,
                        RevenueStartDate = DateTime.Today,
                        RevenueEndDate = DateTime.Today,
                        ShortDescription = campaignCredit.Credits.ToString() + " credits",
                        State = DataHolderState.Added,
                        Type = InvoiceItem.Types.CampaignCredits,
                        VatCode = InvoiceItem.VATCodes.T1
                    };
					if (invoiceItemDescription.Length > 0)
						iidh.Description += " [" + invoiceItemDescription + "]";
					
                    iidh.Discount = invoiceTotalAsCampaignCreditsDiscount;

                    idh.InvoiceItemDataHolderList.Add(iidh);
                    this.Invoices.Add(idh);
                }

                else if(Invoices.Count == 1 && Invoices[0].InvoiceItemDataHolderList.Count > 0)
                {
                    if (Invoices[0].InvoiceItemDataHolderList[0].BuyableObjectType == Model.Entities.ObjectType.CampaignCredit && Invoices[0].InvoiceItemDataHolderList[0].BuyableObjectK > 0)
                    {
						SetInvoiceTotalAsCampaignCreditsDiscount(fixedDiscountUsed);

                        CampaignCredit campaignCredit = new CampaignCredit(Invoices[0].InvoiceItemDataHolderList[0].BuyableObjectK);
                        campaignCredit.Credits = invoiceTotalAsCampaignCredits;
                        campaignCredit.ActionDateTime = DateTime.Now;
                        campaignCredit.BuyableLockDateTime = DateTime.Now;
                        campaignCredit.Description = invoiceTotalAsCampaignCredits.ToString() + " credits";
						campaignCredit.FixedDiscount = invoiceTotalAsCampaignCreditsDiscount;
						campaignCredit.IsPriceFixed = true;
						Invoices[0].InvoiceItemDataHolderList[0].Description = campaignCredit.Description;
                        Invoices[0].InvoiceItemDataHolderList[0].ShortDescription = campaignCredit.Description;
						Invoices[0].InvoiceItemDataHolderList[0].PriceBeforeDiscount = campaignCredit.Credits * CurrentPromoter.CostPerCampaignCredit;
                        Invoices[0].InvoiceItemDataHolderList[0].Discount = invoiceTotalAsCampaignCreditsDiscount;

						if (invoiceItemDescription.Length > 0)
						{
							campaignCredit.Description += " [" + invoiceItemDescription + "]";
							Invoices[0].InvoiceItemDataHolderList[0].Description += " [" + invoiceItemDescription + "]";
						}

						campaignCredit.SetUsrAndActionUsr(Usr.Current, false);
                        campaignCredit.Update();
                    }
                }
			}
			else
			{
				paymentTypeIsCampaignCredits = false;

				foreach (InvoiceDataHolder idh in Invoices)
				{
					invoiceTotal += idh.AmountDue;
				}
			}
		}

		private void SetInvoiceTotalAsCampaignCreditsDiscount(bool fixedDiscountUsed)
		{
			if (fixedDiscountUsed)
			{
				if (invoiceTotalAsCampaignCredits > 0)
					invoiceTotalAsCampaignCreditsDiscount = Math.Round(1 - (((double)totalCampaignCreditsAsMoney) / invoiceTotalAsCampaignCredits), 4);
				else
					invoiceTotalAsCampaignCreditsDiscount = 0;
			}
			else
				invoiceTotalAsCampaignCreditsDiscount = CampaignCredit.GetDiscountForCredits(invoiceTotalAsCampaignCredits, CurrentPromoter);
		}
		private bool invoiceTotalDone = false;
		private decimal invoiceTotal = 0.0m;
		private int invoiceTotalAsCampaignCredits = 0;
		private double invoiceTotalAsCampaignCreditsDiscount = 0.0;
		private decimal totalCampaignCreditsAsMoney = 0.0m;
		private decimal totalVatOnCampaignCreditsAsMoney = 0.0m;
		private bool paymentTypeIsCampaignCredits = true;
        private DateTime processingDateTime = Time.Now;
		/// <summary>
		/// Raw total from invoices (taking into account partial transfers and credits)
		/// </summary>
		decimal InvoiceTotal
		{
			get
			{
				if (!invoiceTotalDone)
				{
					ProcessInvoiceTotals();
				}
				return invoiceTotal;
			}
		}
		int InvoiceTotalAsCampaignCredits
		{
			get
			{
				if (!invoiceTotalDone)
				{
					ProcessInvoiceTotals();
				}
                return invoiceTotalAsCampaignCredits;
			}
		}
		double InvoiceTotalAsCampaignCreditsDiscount
		{
			get
			{
				if (!invoiceTotalDone)
				{
					ProcessInvoiceTotals();
				}
				return invoiceTotalAsCampaignCreditsDiscount;
			}
		}
		bool PaymentTypeIsCampaignCredits
		{
			get
			{
				if (!invoiceTotalDone)
				{
					ProcessInvoiceTotals();
				}
				return paymentTypeIsCampaignCredits;
			}
		}
		decimal TotalCampaignCreditsAsMoney
		{
			get
			{
				if (!invoiceTotalDone)
				{
					ProcessInvoiceTotals();
				}
                return totalCampaignCreditsAsMoney;
			}
		}
		decimal TotalVatOnCampaignCreditsAsMoney
		{
			get
			{
				if (!invoiceTotalDone)
				{
					ProcessInvoiceTotals();
				}
				return totalVatOnCampaignCreditsAsMoney;
			}
		}
		#endregion

		#region InvoiceTotalAfterBalance
		/// <summary>
		/// Raw invoices total minus any positive balance on the CurrentBuyer account
		/// </summary>
		decimal InvoiceTotalAfterBalance
		{
			get
			{
				if (CurrentBuyerPositiveBalance >= InvoiceTotal)
					return 0.0m;
				else
					return InvoiceTotal - CurrentBuyerPositiveBalance;
			}
		}
		#endregion

		#region InvoiceTotalAfterBalanceAndCredit
		/// <summary>
		/// Raw invoices total minus any positive balance on the CurrentBuyer account minus any credit applied
		/// </summary>
		public decimal InvoiceTotalAfterBalanceAndCredit
		{
			get
			{
				return InvoiceTotalAfterBalance - CreditApplied;
			}
		}
		#endregion

		#region BalanceApplied
		/// <summary>
		/// Total positive balance applied to this invoice
		/// </summary>
		decimal BalanceApplied
		{
			get
			{
				if (CurrentBuyerPositiveBalance >= InvoiceTotal)
					return InvoiceTotal;
				else
					return CurrentBuyerPositiveBalance;
			}
		}
		#endregion

        #region CreditApplied
        /// <summary>
		/// Total credit applied to this invoice
		/// </summary>
		public decimal CreditApplied
		{
			get
			{
				if (TotalDueExceedsAvailableCredit)
				{
					if (PayUsingAvailableCredit)
						return CurrentBuyerAvailableCredit;
					else
						return 0;
				}
				else
				{
					if (PayUsingAvailableCredit)
						return InvoiceTotalAfterBalance;
					else
						return 0;
				}
			}
		}
		#endregion

        #region CampaignCreditsApplied
        /// <summary>
        /// Total credit applied to this invoice
        /// </summary>
        public int CampaignCreditsApplied
        {
            get
            {
                if (PayUsingAvailableCampaignCredits)
                    return CurrentBuyer.CampaignCredits > 0 ? CurrentBuyer.CampaignCredits : 0;
                else
                    return 0;
            }
        }
        #endregion

        #region ProcessingDateTime
        public DateTime ProcessingDateTime
        {
            get
            {
                return processingDateTime;
            }
            set
            {
                processingDateTime = value;
            }
        }
        #endregion
		#endregion

		#region Page_Init

		#endregion

		#region Page_Load

		#endregion

		#region Page_PreRender
		public void Page_PreRender(object o, System.EventArgs e)
		{
			BindBuyerItems();
			BindPaymentOptions();
			BindPayLater();
			BindPayWithCampaignCredits();

			if (PayOptionsPanel.Visible)
			{
				if (PayOptionRadioButtonPayNow.Checked)
				{
					PayNowPanel.Style.Remove("display");
					PayLaterPanel.Style["display"] = "none";
					PayWithBalancePanel.Style["display"] = "none";
					PayWithCampaignCreditPanel.Style["display"] = "none";
				}
				else if (PayOptionRadioButtonPayLater.Checked)
				{
					PayNowPanel.Style["display"] = "none";
					PayLaterPanel.Style.Remove("display");
					PayWithBalancePanel.Style["display"] = "none";
					PayWithCampaignCreditPanel.Style["display"] = "none";
				}
				else if (PayOptionRadioButtonPayWithBalance.Checked)
				{
					PayLaterPanel.Style["display"] = "none";
					PayNowPanel.Style["display"] = "none";
					PayWithBalancePanel.Style.Remove("display");
					PayWithCampaignCreditPanel.Style["display"] = "none";
				}
				else if (PayOptionRadioButtonPayWithCampaignCredit.Checked)
				{
					PayNowPanel.Style["display"] = "none";
					PayLaterPanel.Style["display"] = "none";
					PayWithBalancePanel.Style["display"] = "none";
					PayWithCampaignCreditPanel.Style.Remove("display");
				}
				else
				{
					PayLaterPanel.Style["display"] = "none";
					PayNowPanel.Style["display"] = "none";
					PayWithBalancePanel.Style["display"] = "none";
					PayWithCampaignCreditPanel.Style["display"] = "none";
				}
			}
			else
			{
				PayLaterPanel.Style.Remove("display");
				PayNowPanel.Style.Remove("display");
				PayWithBalancePanel.Style.Remove("display");
				PayWithCampaignCreditPanel.Style.Remove("display");
			}

			if (SavedCardPanel.Visible)
			{
				if (SavedCardOptionsSavedCard.Checked)
				{
					SavedCardInnerPanel.Style.Remove("display");
					NewCardPanel.Style["display"] = "none";
				}
				else
				{
					SavedCardInnerPanel.Style["display"] = "none";
					NewCardPanel.Style.Remove("display");
				}
			}
			else
			{
				SavedCardInnerPanel.Style.Remove("display");
				NewCardPanel.Style.Remove("display");
			}

			if (CurrentPromoter != null)
				CreditApplicationLink.HRef = CurrentPromoter.UrlApp("plus");

			PayOptionRadioButtonPayWithCampaignCredit.Enabled = AllowPayWithCampaignCredits;
		}
		#endregion

		#region BuyerItemListPanel

		#region BindBuyerItems
		private void BindBuyerItems()
		{
			if (CurrentBuyer is Usr)
				BindUserItems();
			else if (CurrentBuyer is Promoter)
				BindPromoterItems();
		}

		#endregion

		#region BindUserItems()
		void BindUserItems()
		{
			if (CurrentBuyer is Usr)
			{
				decimal total = 0.0m;
				decimal vat = 0.0m;

				TotalCreditsRow.Visible = false;
				TotalCreditsAsMoneyRow.Visible = false;

				UserItemListPanel.Visible = false;
				BuyerItemListPanel.Visible = true;
				this.BuyerItemListColumn1HeaderLabel.Text = "Items";
				this.BuyerItemListColumn2HeaderLabel.Text = "";
				this.BuyerItemListColumn3HeaderLabel.Text = "Total";
				InvoicesBody.Controls.Clear();

				foreach (InvoiceDataHolder idh in Invoices)
				{
					idh.Type = Invoice.Types.Invoice;
					total += idh.Total;
					vat += idh.Vat;

					foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
					{
						HtmlTableRow row = new HtmlTableRow();
						HtmlTableCell c1 = new HtmlTableCell();
						HtmlTableCell c1a = new HtmlTableCell();
						HtmlTableCell c2 = new HtmlTableCell();
						c1.Attributes["style"] = "background-color:transparent;padding-top:0px;padding-bottom:2px;";
						c1.Width = "100%";
						c1.VAlign = "top";
						c2.VAlign = "top";
						c2.Attributes["style"] = "background-color:transparent;padding-top:0px;padding-bottom:2px;horizontal-align:right;";
						c2.Align = "right";
						//	if (idh.InvoiceItemDataHolderList.Count > 0)
						
						//string percentageFormat = Math.Round(iidh.Discount * 100, 2) == Convert.ToInt32(iidh.Discount * 100) ? "P0" : "P2";
						//string discountHtml = " <small>(@ " + iidh.Discount.ToString(percentageFormat) + " discount)</small>";
						c1.InnerHtml = HttpUtility.HtmlEncode(iidh.ShortDescription);// +discountHtml;
						//else
						//    c1.InnerHtml = HttpUtility.HtmlEncode("Invoice #" + idh.K.ToString());
						if (ShowItemsIncVat)
							c2.InnerHtml = Utilities.MoneyToHTML(iidh.Total);
						else
							c2.InnerHtml = Utilities.MoneyToHTML(iidh.Price);
						row.Cells.Add(c1);
						row.Cells.Add(c1a);
						row.Cells.Add(c2);
						InvoicesBody.Controls.Add(row);
					}
				}

				if (vat > 0.005m && !ShowItemsIncVat)
				{
					VatLabel.Text = Utilities.MoneyToHTML(vat);
					VatRow.Visible = true;
					//HtmlTableRow row = new HtmlTableRow();
					//HtmlTableCell c1 = new HtmlTableCell();
					//HtmlTableCell c1a = new HtmlTableCell();
					//HtmlTableCell c2 = new HtmlTableCell();
					//c1.Attributes["style"] = "background-color:transparent;padding-top:0px;padding-bottom:2px;";
					//c1.Width = "100%";
					//c2.Attributes["style"] = "background-color:transparent;padding-top:0px;padding-bottom:2px;horizontal-align:right;";
					//c2.Align = "right";
					//c1.InnerHtml = "Vat";
					//c2.InnerHtml = "<nobr>" + HttpUtility.HtmlEncode(vat.ToString("c")) + "</nobr>";
					//row.Cells.Add(c1);
					//row.Cells.Add(c1a);
					//row.Cells.Add(c2);
					//ItemsBody.Controls.Add(row);
				}
				else
				{
					VatRow.Visible = false;
				}

				HtmlTableRow rowTotal = new HtmlTableRow();
				HtmlTableCell c1Total = new HtmlTableCell();
				HtmlTableCell c2Total = new HtmlTableCell();
				c1Total.Attributes["style"] = "background-color:transparent;padding-top:0px;padding-bottom:2px;";
				c1Total.Align = "right";
				c1Total.Width = "100%";
				c2Total.Attributes["style"] = "background-color:transparent;padding-top:0px;padding-bottom:2px;horizontal-align:right;";
				c2Total.Align = "right";
				c1Total.InnerHtml = "<b>Total</b>";
				c2Total.InnerHtml = "<b>" + Utilities.MoneyToHTML(total) + "</b>";
				rowTotal.Cells.Add(c1Total);
				rowTotal.Cells.Add(c2Total);
				ItemsBody.Controls.Add(rowTotal);
			}
		}
		#endregion

		#region BindPromoterItems()
		void BindPromoterItems()
		{
			if (CurrentBuyer is Promoter)
			{
				this.UserItemListPanel.Visible = false;
				this.BuyerItemListPanel.Visible = true;

				if (PaymentTypeIsCampaignCredits)
				{
					this.TotalCreditsRow.Visible = true;
					this.TotalCreditsAsMoneyRow.Visible = true;

					this.BuyerItemListColumn1HeaderLabel.Text = "Items";
					this.BuyerItemListColumn2HeaderLabel.Text = "";
					this.BuyerItemListColumn3HeaderLabel.Text = "Credits";
                    VatRow.Visible = true;
					VatLabel.Text = Utilities.MoneyToHTML(TotalVatOnCampaignCreditsAsMoney);

					invoiceTotalAsCampaignCredits = 0;
					bool showPerItemDiscount = false;
					double? fixedDiscount = null;
					foreach (CampaignCredit cc in CampaignCredits)
					{
						invoiceTotalAsCampaignCredits += -cc.Credits;
						if (fixedDiscount == null)
							fixedDiscount = cc.FixedDiscount;

						if (cc.FixedDiscount >= 0)
						{
							if (Math.Round(fixedDiscount.Value * 100, 2) != Math.Round(cc.FixedDiscount * 100, 2))
								showPerItemDiscount = true;
						}
					}
					double nonFixedCampaignCreditDiscount = CampaignCredit.GetDiscountForCredits(invoiceTotalAsCampaignCredits, CurrentPromoter);
					foreach (CampaignCredit cc in CampaignCredits)
					{
						HtmlTableRow invoiceTr = new HtmlTableRow();
						InvoicesBody.Controls.Add(invoiceTr);

						HtmlTableCell invoiceNameTd = new HtmlTableCell();
						invoiceNameTd.Style["background-color"] = "transparent";
						string discountHtml = "";
						if (showPerItemDiscount)
						{
							double discount = cc.FixedDiscount > 0 ? cc.FixedDiscount : nonFixedCampaignCreditDiscount;
							string percentageFormat = Math.Round(discount * 100, 2) == Convert.ToInt32(discount * 100) ? "P0" : "P2";
							discountHtml = " <small>(@ <nobr>" + discount.ToString(percentageFormat) + "</nobr> discount)</small>";
						}
						invoiceNameTd.InnerHtml = HttpUtility.HtmlEncode(cc.Description) + discountHtml;

						HtmlTableCell invoicePriceTd = new HtmlTableCell();
						invoicePriceTd.Align = "right";
						invoicePriceTd.Style["background-color"] = "transparent";

						HtmlTableCell emptyTd = new HtmlTableCell();
						emptyTd.Style["background-color"] = "transparent";
						emptyTd.InnerHtml = "&nbsp;";

						// credits are negative on the campaign credit list
						invoicePriceTd.InnerHtml = "<nobr>" + HttpUtility.HtmlEncode((-cc.Credits).ToString("N0")) + "</nobr>";

						invoiceTr.Cells.Add(invoiceNameTd);
						invoiceTr.Cells.Add(emptyTd);
						invoiceTr.Cells.Add(invoicePriceTd);
					}
				}
				else if (Invoices.Count == 1 && Invoices[0].K == 0)
				{
					this.TotalCreditsRow.Visible = false;
					this.TotalCreditsAsMoneyRow.Visible = false;

					this.BuyerItemListColumn1HeaderLabel.Text = "Invoice";
					this.BuyerItemListColumn2HeaderLabel.Text = "";
					this.BuyerItemListColumn3HeaderLabel.Text = "Price";
					VatLabel.Text = Utilities.MoneyToHTML(Invoices[0].Vat);
                    VatRow.Visible = true;

					foreach (InvoiceItemDataHolder iidh in Invoices[0].InvoiceItemDataHolderList)
					{
						HtmlTableRow invoiceTr = new HtmlTableRow();
						InvoicesBody.Controls.Add(invoiceTr);

						HtmlTableCell invoiceNameTd = new HtmlTableCell();
						invoiceNameTd.Style["background-color"] = "transparent";

						string percentageFormat = Math.Round(iidh.Discount * 100, 2) == Convert.ToInt32(iidh.Discount * 100) ? "P0" : "P2";
						string discountHtml = " <small>(@ <nobr>" + iidh.Discount.ToString(percentageFormat) + "</nobr> discount)</small>";
						invoiceNameTd.InnerHtml = HttpUtility.HtmlEncode(iidh.ShortDescription) + discountHtml;

						HtmlTableCell invoicePriceTd = new HtmlTableCell();
						invoicePriceTd.Align = "right";
						invoicePriceTd.Style["background-color"] = "transparent";

						HtmlTableCell emptyTd = new HtmlTableCell();
						emptyTd.Style["background-color"] = "transparent";
						emptyTd.InnerHtml = "&nbsp;";
						invoicePriceTd.InnerHtml = Utilities.MoneyToHTML(iidh.Price);

						invoiceTr.Cells.Add(invoiceNameTd);
						invoiceTr.Cells.Add(emptyTd);
						invoiceTr.Cells.Add(invoicePriceTd);
					}
				}
				else
				{
					this.TotalCreditsRow.Visible = false;
					this.TotalCreditsAsMoneyRow.Visible = false;

					foreach (InvoiceDataHolder idh in Invoices)
					{
						idh.Type = Invoice.Types.Invoice;

						HtmlTableRow itemsTr = new HtmlTableRow();
						HtmlTableRow invoiceTr = new HtmlTableRow();
						InvoicesBody.Controls.Add(invoiceTr);
						InvoicesBody.Controls.Add(itemsTr);

						itemsTr.ID = "PaymentItem" + idh.K;
						itemsTr.Style["display"] = "none";

						#region Name
						HtmlTableCell invoiceNameTd = new HtmlTableCell();
						invoiceNameTd.Style["background-color"] = "transparent";
						string invoiceHtml = "New invoice";
						if (idh.K > 0 && idh.PromoterK > 0)
							invoiceHtml = Utilities.LinkNewWindow(idh.UrlReport(), "Invoice #" + idh.K.ToString());
						invoiceNameTd.InnerHtml = "<a href=\"#\" onclick=\"var elem = document.getElementById('" + itemsTr.ClientID + "'); var img = document.getElementById('" + this.ClientID + "_PaymentPlusMinus" + idh.K + "'); img.src = elem.style.display == 'none' ? '/gfx/minus.gif' : '/gfx/plus.gif'; elem.style.display = elem.style.display == 'none' ? '' : 'none'; return false;\"><img id=\"" + this.ClientID + "_PaymentPlusMinus" + idh.K + "\" src=\"/gfx/plus.gif\" alt=\"Show items\" border=\"0\" align=\"absmiddle\" style=\"margin-right:4px;\" /></a>" + invoiceHtml;
						#endregion

						#region Total
						HtmlTableCell invoiceTotalTd = new HtmlTableCell();
						invoiceTotalTd.Align = "right";
						invoiceTotalTd.Style["background-color"] = "transparent";
						invoiceTotalTd.InnerHtml = Utilities.MoneyToHTML(idh.Total);
						#endregion

						#region Due
						HtmlTableCell invoiceDueTd = new HtmlTableCell();
						invoiceDueTd.Align = "right";
						invoiceDueTd.Style["background-color"] = "transparent";
						invoiceDueTd.InnerHtml = Utilities.MoneyToHTML(idh.AmountDue);
						#endregion

						invoiceTr.Cells.Add(invoiceNameTd);
						invoiceTr.Cells.Add(invoiceTotalTd);
						invoiceTr.Cells.Add(invoiceDueTd);

						HtmlTableCell itemsTd = new HtmlTableCell();
						itemsTd.Style["background-color"] = "transparent";
						itemsTd.Style["padding"] = "0px";
						itemsTd.Style["padding-left"] = "14px";
						itemsTd.Style["width"] = "279px";
						itemsTd.ColSpan = 3;
						itemsTd.Align = "right";

						#region Items table
						StringBuilder sb = new StringBuilder();
						sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
						foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
						{
							sb.Append("<tr><td align=\"left\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small>");
							sb.Append(iidh.Description);
							sb.Append("</small></td><td align=\"right\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small>");
							// Show IncVat for Promoters and other users. As per David Brophy on Oct 24, 2006
							// Show ExVat for all, then add VAT as its own line item after all invoice items. this is for OASIS v1.5, Dec 12, 2006
							sb.Append(Utilities.MoneyToHTML(iidh.Price));
							sb.Append("</small></td></tr>");
						}

						VatLabel.Text = Utilities.MoneyToHTML(idh.Vat);

						//sb.Append("<tr><td align=\"left\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small>VAT</small></td><td align=\"right\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>");
						//sb.Append();
						//sb.Append("</nobr></small></td></tr>");

						// Only if the some money has been paid or credited, then we go to DB to get successful transfer applied
						if (idh.AmountDue != idh.Total)
						{
							Query invoiceTransferQuery = new Query();
							invoiceTransferQuery.QueryCondition = new And(new Q(InvoiceTransfer.Columns.InvoiceK, idh.K),
																		  new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success));
							invoiceTransferQuery.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K, QueryJoinType.Inner);
							invoiceTransferQuery.Columns = new ColumnSet(InvoiceTransfer.Columns.Amount, InvoiceTransfer.Columns.TransferK, Transfer.Columns.Type);

							InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(invoiceTransferQuery);
							decimal invoiceTransferTotal = 0;
							foreach (InvoiceTransfer invoiceTransfer in invoiceTransferSet)
							{
								decimal amount = invoiceTransfer.Amount;
								if (((Transfer.TransferTypes)invoiceTransfer.ExtraSelectElements["Transfer_Type"]).Equals(Transfer.TransferTypes.Payment))
									amount = -1 * Math.Abs(amount);
								else
									amount = Math.Abs(amount);
								sb.Append("<tr><td align=\"left\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>Transfer #");
								sb.Append(invoiceTransfer.TransferK.ToString());
								sb.Append("</nobr></td><td align=\"right\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small>");
								sb.Append(Utilities.MoneyToHTML(amount));
								sb.Append("</small></td></tr>");

								invoiceTransferTotal += invoiceTransfer.Amount;
							}

							// Test if there is still money unaccounted for, then go to DB for credits applied
							if (invoiceTransferTotal + idh.AmountDue < idh.Total)
							{
								Query invoiceCreditQuery = new Query();
								invoiceCreditQuery.QueryCondition = new Q(InvoiceCredit.Columns.InvoiceK, idh.K);
								invoiceCreditQuery.Columns = new ColumnSet(InvoiceCredit.Columns.Amount, InvoiceCredit.Columns.CreditInvoiceK);

								InvoiceCreditSet invoiceCreditSet = new InvoiceCreditSet(invoiceCreditQuery);

								foreach (InvoiceCredit invoiceCredit in invoiceCreditSet)
								{
									sb.Append("<tr><td align=\"left\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>Credit #");
									sb.Append(invoiceCredit.CreditInvoiceK.ToString());
									sb.Append("</nobr></td><td align=\"right\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small>");
									sb.Append(Utilities.MoneyToHTML(invoiceCredit.Amount));
									sb.Append("</small></td></tr>");
								}
							}
						}

						sb.Append("</table>");
						#endregion

						//if (idh.K == 0)
						itemsTd.InnerHtml = sb.ToString();
						itemsTr.Cells.Add(itemsTd);
					}
				}
			}
		}
		#endregion

		#endregion

		#region PayOptionsPanel - Pay now / Pay later / Pay with Campaign Credit

		#region BindPaymentOptions
		public void BindPaymentOptions()
		{
			if (CurrentBuyer != null)
			{
				InvoiceTotalRow.Visible = BalanceApplied == 0 && CreditApplied == 0;
				InvoiceTotalLabel.Text = Utilities.MoneyToHTML(InvoiceTotal);
				InvoiceTotalAsCampaignCreditsLabel.Text = InvoiceTotalAsCampaignCredits.ToString("N0");

				if (invoiceTotalAsCampaignCreditsDiscount > 0)
				{
					string percentageFormat = Math.Round(invoiceTotalAsCampaignCreditsDiscount * 100, 2) == Convert.ToInt32(invoiceTotalAsCampaignCreditsDiscount * 100) ? "P0" : "P2";
					Discount.Text = "@ <nobr>" + invoiceTotalAsCampaignCreditsDiscount.ToString(percentageFormat) + "</nobr> discount";
				}
				else
				{
					Discount.Text = "";
				}

				CreditsToMoneyTotalLabel.Text = Utilities.MoneyToHTML(TotalCampaignCreditsAsMoney);

				FromBalanceRow.Visible = BalanceApplied > 0;
				FromBalanceAmountLabel.Text = Utilities.MoneyToHTML(-1 * BalanceApplied);

                FromCampaignCreditsRow.Visible = CampaignCreditsApplied > 0;
                FromCampaignCreditBalanceLabel.Text = "<nobr>-" + CampaignCreditsApplied.ToString("N0") + "</nobr>";

				FromCreditRow.Visible = CreditApplied > 0;
				FromCreditAmountLabel.Text = Utilities.MoneyToHTML(-1 * CreditApplied);

				InvoiceTotalAfterBalanceAndCreditRow.Visible = BalanceApplied != 0 || CreditApplied != 0;
				InvoiceTotalAfterBalanceAndCreditLabel.Text = Utilities.MoneyToHTML(InvoiceTotalAfterBalanceAndCredit);


				PayLaterPanel.Visible = ShowPayLater && InvoiceTotalAfterBalance > 0;
				PayOptionRadioButtonPayLater.Visible = PayLaterPanel.Visible;
				PayOptionRadioButtonPayLaterHolder.Visible = PayLaterPanel.Visible;

				PayNowPanel.Visible = InvoiceTotalAfterBalance > 0;
				PayOptionRadioButtonPayNow.Visible = PayNowPanel.Visible;
				PayOptionRadioButtonPayNowHolder.Visible = PayNowPanel.Visible;

				PayWithCampaignCreditPanel.Visible = PaymentTypeIsCampaignCredits;
				PayOptionRadioButtonPayWithCampaignCredit.Visible = PayWithCampaignCreditPanel.Visible;
				PayOptionRadioButtonPayWithCampaignCreditHolder.Visible = PayWithCampaignCreditPanel.Visible;

				PayWithBalancePanel.Visible = InvoiceTotalAfterBalance == 0;
				PayOptionRadioButtonPayWithBalance.Visible = PayWithBalancePanel.Visible;
				PayOptionRadioButtonPayWithBalanceHolder.Visible = PayWithBalancePanel.Visible;


				int paymentOptionsAvailable = 0;
				if (PayLaterPanel.Visible) paymentOptionsAvailable++;
				if (PayNowPanel.Visible) paymentOptionsAvailable++;
				if (PayWithBalancePanel.Visible) paymentOptionsAvailable++;
				if (PayWithCampaignCreditPanel.Visible) paymentOptionsAvailable++;
				PayOptionsPanel.Visible = paymentOptionsAvailable >= 2;


				RegisterPaymentOptionControlInToggleScript(PayNowPanel, PayOptionRadioButtonPayNow);
				RegisterPaymentOptionControlInToggleScript(PayLaterPanel, PayOptionRadioButtonPayLater);
				RegisterPaymentOptionControlInToggleScript(PayWithBalancePanel, PayOptionRadioButtonPayWithBalance);
				RegisterPaymentOptionControlInToggleScript(PayWithCampaignCreditPanel, PayOptionRadioButtonPayWithCampaignCredit);
			}
			else
			{
				PayOptionsPanel.Visible = false;
				PayLaterPanel.Visible = false;
				PayWithCampaignCreditPanel.Visible = false;
				PayWithBalanceButton.Visible = false;
				PayNowPanel.Visible = true;
			}
		}
		#endregion

		#region PayWithBalanceButton_Click
		public void PayWithBalanceButton_Click(object o, System.EventArgs e)
		{
			CheckSslStatus();

			try
			{
				if (IsValidTotal())
				{
					// allow 100% discounted payments 
					if (InvoiceTotal > 0 && CurrentBuyerBalance < InvoiceTotal)
					{
						// Balance has changed. Re evaluate.
						this.Initialize();
					}
					else if (this.ValidateAndLockInvoices())
					{
						List<Invoice> invoices = new List<Invoice>();
						try
						{
							foreach (InvoiceDataHolder idh in this.Invoices)
							{
								bool creatingInvoice = idh.K == 0;
								SetInvoiceDatesUsrsGuid(idh);
								Invoice invoice = idh.UpdateInsertDelete();
								invoice.AssignBuyerType();
								invoice.UpdateAndAutoApplySuccessfulTransfersWithAvailableMoney();
								if (creatingInvoice)
									invoice.Process();
								//this.UnlockInvoiceItems();
								invoices.Add(invoice);
								Utilities.EmailInvoice(invoice, creatingInvoice);
							}
						}
						catch (Exception ex)
						{
							ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG;
							//try
							//{
							//    this.UnlockInvoiceItems();
							//}
							//catch(Exception)
							//{}
							ProcessException(ex);
							return;
						}
						if (this.HandleAndVerifyInvoicesProcessed(invoices))
						{
							if (PaymentDone != null)
								PaymentDone(this, new PaymentDoneEventArgs(invoices, this.CampaignCredits, false, ""));
						}
					}
				}
				else
				{
					// Re evaluate.
					this.Initialize();
				}
			}
			catch (Exception ex)
			{
				ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG;

				ProcessException(ex);
				return;
			}
		}
		#endregion

		#endregion

		#region PayNowPanel

		#region BindSavedCards
		private void BindSavedCards()
		{
			SavedCardPanel.Visible = false;

			if (CurrentUsr.SavedCards.Count > 0 && AllowSavedCards)
			{
				SavedCardDropDownList.Items.Clear();
				SavedCardDropDownList.Items.AddRange(CurrentUsr.SavedCardsToListItemArray);
				SavedCardPanel.Visible = true;
			}
		}
		#endregion

		#region PayWithSavedCard_Click
		public void PayWithSavedCard_Click(object o, System.EventArgs e)
		{
			CheckSslStatus();

			try
			{
				if (IsValidTotal() && AllowSavedCards)
				{
					PaymentClickEventArgs paymentClickEventArgs = new PaymentClickEventArgs();
					if (PaymentClick != null)
						PaymentClick(this, paymentClickEventArgs);

					if (paymentClickEventArgs.Abort)
						return;

					if (Page.IsValid)
					{

						Transfer prevSavedTransfer = new Transfer(Convert.ToInt32(this.SavedCardDropDownList.SelectedValue));
						this.CardNumberHash = prevSavedTransfer.CardNumberHash;
						//check for duplicate invoices
						if (ValidateAndLockInvoices())
						{
							secPay = new SecPay();
							//bool creatingInvoices = false;
							//int savedTransferK = Convert.ToInt32(this.SavedCardDropDownList.SelectedValue);

							if (!CurrentUsr.CheckPassword(PasswordTextBox.Text))
							{
								ProcessingVal.IsValid = false;
								ProcessingVal.ErrorMessage = "Incorrect password";
								this.UnlockInvoiceItems();

								if (ContainerPage != null && !HideValidationSummary)
									ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");
							}
							else
							{
								try
								{
									foreach (InvoiceDataHolder idh in Invoices)
									{
										//creatingInvoices = idh.K == 0;
										SetInvoiceDatesUsrsGuid(idh);
									}

									secPay.MakePaymentUsingSavedTransferDetails(
										Invoices,
										InvoiceTotalAfterBalanceAndCredit,
										CurrentUsr,
										prevSavedTransfer,
										FraudCheck,
										(Guid)ViewState["DuplicateGuidTransfer"]);
								}
								catch (Exception ex)
								{
									this.ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG;
									//try
									//{
									//    this.UnlockInvoiceItems();
									//}
									//catch (Exception)
									//{ }
									this.ProcessException(ex, prevSavedTransfer.CardNumberEnd);
									return;
								}

								if (secPay.Transfer.Status.Equals(Transfer.StatusEnum.Success))
									this.ProcessSuccessfulSecPayTransfer(secPay);
								else
									this.HandleFailedSecPayTransfer(secPay);
							}
						}
					}
					else
					{
						if (ContainerPage != null && !HideValidationSummary)
							ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");
					}

				}
				else
				{
					// Re evaluate.
					this.Initialize();
					
				}
			}
			catch (Exception ex)
			{
				ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG;
				ProcessException(ex);
				return;
			}
			
		}
		#endregion

		#region PayWithNewCard_Click
		public void PayWithNewCard_Click(object o, System.EventArgs e)
		{
			CheckSslStatus();

			try
			{
				PaymentClickEventArgs paymentClickEventArgs = new PaymentClickEventArgs();
				if (PaymentClick != null)
					PaymentClick(this, paymentClickEventArgs);

				if (paymentClickEventArgs.Abort)
					return;

				if (Page.IsValid)
				{
					this.CardNumberHash = Cambro.Misc.Utility.Hash(CardNumber.Text.Replace(" ", ""));
					if (this.ValidateAndLockInvoices())
					{
						secPay = new SecPay();
						try
						{
							string cardFullName = "";
							
							
							
							string cardNumber = "";
							string cardCV2 = "";
							DateTime cardStartDate = DateTime.MinValue;
							DateTime cardExpiryDate = DateTime.MinValue;
							string cardIssueNumber = "";
							
							
							cardFullName = Name.Text;
							string cardAddressStreet = Address.Text;
							string cardAddressPostCode = Postcode.Text.ToUpper();
							string cardAddressArea = uiAddressArea.Text;
							string cardAddressTown = uiAddressTown.Text;
							string cardAddressCounty = uiAddressCounty.Text;


							int cardAddressCountryK = LockedCountryK ?? ((CountryTable.Visible && FraudCheck.Equals(Transfer.FraudCheckEnum.Strict)) ? int.Parse(CountryDropDownList.SelectedValue) : 224);
							string cardAddressCountry = (CountryTable.Visible && FraudCheck.Equals(Transfer.FraudCheckEnum.Strict)) ? CountryDropDownList.SelectedItem.Text : "UK";
							

							cardNumber = CardNumber.Text;
							cardCV2 = Cv2.Text;

							int cardEndYear = Convert.ToInt32(EndDateYear.Text.Trim());
							cardEndYear += ((cardEndYear < 80) ? 2000 : 1900);
							cardExpiryDate = new DateTime(cardEndYear, Convert.ToInt32(EndDateMonth.Text.Trim()), 1);
							if (StartDateMonth.Text.Length > 0)
							{
								int cardStartYear = Convert.ToInt32(StartDateYear.Text.Trim());
								cardStartYear += ((cardStartYear < 80) ? 2000 : 1900);
								cardStartDate = new DateTime(cardStartYear, Convert.ToInt32(StartDateMonth.Text.Trim()), 1);
							}
							if (Issue.Text.Length > 0)
								cardIssueNumber = Issue.Text;

							bool creatingInvoices = false;

							foreach (InvoiceDataHolder idh in Invoices)
							{
								creatingInvoices = idh.K == 0;

								SetInvoiceDatesUsrsGuid(idh);
							}

							int promoterK = 0;
							if (CurrentPromoter != null)
								promoterK = CurrentPromoter.K;

							if (Vars.DevEnv)
								System.Threading.Thread.Sleep(1000);

							secPay.MakePayment(
								Invoices,
								InvoiceTotalAfterBalanceAndCredit,
								CurrentUsr,
								promoterK,
								CurrentUsr.K,
								cardFullName,
								cardAddressStreet,
								cardAddressArea,
								cardAddressTown,
								cardAddressCounty,
								cardAddressCountryK,
								cardAddressPostCode,
								cardAddressCountry,
								cardNumber,
								cardExpiryDate,
								cardCV2,
								FraudCheck,
								this.SaveCardCheckBox.Checked,
								(Guid)ViewState["DuplicateGuidTransfer"],
								cardStartDate,
								cardIssueNumber);
							if (GetFullAddress)
							{
								Usr usr = Usr.Current;
								usr.AddressStreet = cardAddressStreet;
								usr.AddressTown = cardAddressTown;
								usr.AddressCounty = cardAddressCounty;
								usr.AddressCountryK = cardAddressCountryK;
								usr.AddressPostcode = cardAddressPostCode;
								usr.Update();

							}
						}
						catch (Exception ex)
						{
							ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG;
							//try
							//{
							//    this.UnlockInvoiceItems();
							//}
							//catch (Exception)
							//{ }
							ProcessException(ex, CardNumber.Text);
							return;
						}

						if (secPay.Transfer.Status.Equals(Transfer.StatusEnum.Success))
							this.ProcessSuccessfulSecPayTransfer(secPay);
						else
							this.HandleFailedSecPayTransfer(secPay);
					}
				}
				else
				{
					if (ContainerPage != null && !HideValidationSummary)
						ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");
				}
			}
			catch (Exception ex)
			{
				ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG;
				ProcessException(ex);
				return;
			}
		}
		#endregion

		#region SetInvoiceDatesUsrsGuid
		private void SetInvoiceDatesUsrsGuid(InvoiceDataHolder idh)
		{
			if (idh.CreatedDateTime == DateTime.MinValue)
				idh.CreatedDateTime = DateTime.Now;
			if (idh.TaxDateTime == DateTime.MinValue)
				idh.TaxDateTime = idh.CreatedDateTime;
			if (idh.DueDateTime == DateTime.MinValue)
				idh.DueDateTime = idh.TaxDateTime.AddDays(InvoiceDueDays);
			SetUsrAndActionUsr(idh);
			idh.DuplicateGuid = (Guid)ViewState["DuplicateGuidInvoice"];
		}
		#endregion

		#region DeleteCardLinkButton_Click
		protected void DeleteCardLinkButton_Click(object sender, EventArgs eventArgs)
		{
			Transfer t = new Transfer(int.Parse(SavedCardDropDownList.SelectedValue));

			if (t.UsrK != Usr.Current.K)
				throw new Exception("Wrong user!");

			if (t.CardSaved)
			{
				t.CardSaved = false;
				t.Update();
				BindSavedCards();
			}
			ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");
		}
		#endregion

		#endregion

		#region PayLaterPanel

		#region ShowPayLater
		/// <summary>
		/// Show the "pay later" section?
		/// </summary>
		public bool ShowPayLater
		{
			get
			{
				return PromoterK > 0 && InvoiceTotalAfterBalance > 0 && !PayUsingAvailableCredit && Invoices.Count == 1 && Invoices[0].K == 0;
			}
		}
		#endregion

        #region ShowPayCampaignCredits
        /// <summary>
		/// Show the "pay with campaign credits" section?
		/// </summary>
        public bool ShowPayCampaignCredits
		{
			get
			{
				return PromoterK > 0 && !PayUsingAvailableCampaignCredits && Invoices.Count == 1 && Invoices[0].K == 0;
			}
		}
		#endregion
        

		#region PayUsingAvailableCredit
		/// <summary>
		/// This value is set by the "Pay partially by card" button, and is persisted in the viewstate.
		/// This is only relevant when the total due after balance (InvoiceTotalAfterBalance) exceeds the available credit. Do we pay using the available credit (and partially by card), or to we pay fully by card (default).
		/// true - pay using available credit (the user has clicked the "Pay partially by card" button)
		/// false (default) - pay fully by card
		/// </summary>
		bool PayUsingAvailableCredit
		{
			get
			{
				if (this.ViewState["PayUsingAvailableCredit"] == null)
					return false;
				else
					return (bool)this.ViewState["PayUsingAvailableCredit"];
			}
			set
			{
				this.ViewState["PayUsingAvailableCredit"] = value;
			}
		}
		#endregion

        #region PayUsingAvailableCampaignCredits
        /// <summary>
        /// This value is set by the "Pay partially by campaign credits" button, and is persisted in the viewstate.
        /// This is only relevant when the total due after balance (InvoiceTotalAfterBalance) exceeds the available campaign credits. Do we pay using the available campaign credits (and partially by card), or to we pay fully by card (default).
        /// true - pay using available campaign credits (the user has clicked the "Pay partially by campaign credits" button)
        /// false (default) - pay fully by campaign credits
        /// </summary>
        bool PayUsingAvailableCampaignCredits
        {
            get
            {
                if (this.ViewState["PayUsingAvailableCampaignCredits"] == null)
                    return false;
                else
                    return (bool)this.ViewState["PayUsingAvailableCampaignCredits"];
            }
            set
            {
                this.ViewState["PayUsingAvailableCampaignCredits"] = value;
            }
        }
        #endregion

		#region BindPayLater
		/// <summary>
		/// "Pay later" section is only for promoters who have available credit
		/// </summary>
		private void BindPayLater()
		{
			PayLaterPanel.Visible = ShowPayLater;
			//PayOptionsPanel.Visible = ShowPayLater;

			if (ShowPayLater)
			{
				PayLaterIssueInvoicePanel.Visible = InvoiceTotalAfterBalance <= CurrentBuyerAvailableCredit;
				PayLaterNoCreditLimitPanel.Visible = InvoiceTotalAfterBalance > CurrentBuyerAvailableCredit && CurrentBuyer.CreditLimit == 0;
				PayLaterPartialPaymentPanel.Visible = InvoiceTotalAfterBalance > CurrentBuyerAvailableCredit && CurrentBuyer.CreditLimit > 0 && CurrentBuyerAvailableCredit > 0;
				PayLaterNoCreditAvailablePanel.Visible = InvoiceTotalAfterBalance > CurrentBuyerAvailableCredit && CurrentBuyer.CreditLimit > 0 && CurrentBuyerAvailableCredit == 0;

				PayLaterIssueInvoiceLabel.Text = "Click the button below to create an invoice for " + Utilities.MoneyToHTML(InvoiceTotalAfterBalance) +
					", payable within " + InvoiceDueDays + " day" + (InvoiceDueDays == 1 ? "" : "s") + ".";

				PayLaterPartialPaymentLabel.Text = "The total due (" + Utilities.MoneyToHTML(InvoiceTotalAfterBalance) + ") is more than " +
					"the credit available on your account (" + Utilities.MoneyToHTML(CurrentBuyerAvailableCredit) + "). " +
					"Click below to make a partial card payment now.";
			}
		}
		#endregion

        #region HandleFailedSecPayTransfer
        private void HandleFailedSecPayTransfer(SecPay secPay)
		{
			try
			{
				this.UnlockInvoiceItems();

				ProcessingVal.IsValid = false;
				ViewState["DuplicateGuidTransfer"] = Guid.NewGuid();
				if (FraudCheck.Equals(Transfer.FraudCheckEnum.Strict) && secPay.Transfer.CardResponseIsAddressMatch == false)
					ProcessingVal.ErrorMessage = "Please check your address and postcode. Make sure your address and postcode matches the address on your card statement.";
				else if (FraudCheck.Equals(Transfer.FraudCheckEnum.Strict) && secPay.Transfer.CardResponseIsPostCodeMatch == false)
					ProcessingVal.ErrorMessage = "Please check your address and postcode. Make sure your address and postcode matches the postcode on your card statement.";
				else if (FraudCheck.Equals(Transfer.FraudCheckEnum.Strict) && secPay.Transfer.CardResponseIsCv2Match == false)
					ProcessingVal.ErrorMessage = "Please check your CV2 code. The CV2 code is the last three digits on the signature strip on the reverse of your card.";
				else if (secPay.Transfer.CardResponseMessage.Contains("Luhn Check Failed"))
					ProcessingVal.ErrorMessage = "Please check your card number. Contact an administrator if this problem continues.";
				else if (secPay.Transfer.CardResponseMessage.Contains("DECLINED"))
					ProcessingVal.ErrorMessage = "Your card was declined. Please try another card. Contact your bank for card declined details.";
				else if (secPay.Transfer.CardResponseMessage.Contains("REFERRAL"))
				{
					ProcessingVal.ErrorMessage = "Your bank requires voice referral. Please try another card or contact your bank for card referral verification.";
					throw new DsiUserFriendlyException("SecPay payment failed. Card's bank requires voice referral.");
				}
				else
				{
					ProcessingVal.ErrorMessage = "Payment #" + secPay.Transfer.K.ToString() + " failed. Please contact an administrator for further details.";
                    throw new DsiUserFriendlyException("SecPay payment #" + secPay.Transfer.K.ToString() + " failed. See SecPay response for details.");
				}
			}
			catch (Exception ex)
			{
				ProcessException(ex, secPay);
			}
			if (ContainerPage != null && !HideValidationSummary)
				ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");
		}
		#endregion

		#region HandleDuplicates
		/// <summary>
		/// Handles the occurrance of duplicate invoices or transfer from the DuplicateState.  If there's no invoice created, re-initialize the page with the updated CurrentBuyer balance.
		/// Otherwise, fire PaymentDone event passing duplicate = true and any necessary error message.
		/// </summary>
		private void HandleDuplicates()
		{
			if (DuplicateState == DuplicateStateEnum.InvoiceFailAndTransferFail || DuplicateState == DuplicateStateEnum.InvoiceFailAndTransferSuccess)
			{
				this.Initialize();
				ProcessingVal.ErrorMessage = "Purchase was not processed, please try again.";
				ProcessingVal.IsValid = false;
			}
			else if (PaymentDone != null)
			{
				List<Invoice> invoices = new List<Invoice>();
				foreach (InvoiceDataHolder idh in this.Invoices)
				{
					invoices.Add(idh.Invoice);
				}
				PaymentDone(this, new PaymentDoneEventArgs(invoices, this.CampaignCredits, true, INVOICE_DONE_WITH_ERRORS));
			}
		}
		#endregion

		#region Handle and Verify Invoices Processed
		/// <summary>
		/// Checks that all IBuyable invoice items have been processed. If not, then displays an error message and fires an email to admins with error details.
		/// </summary>
		/// <param name="invoices"></param>
		/// <returns></returns>
		private bool HandleAndVerifyInvoicesProcessed(List<Invoice> invoices)
		{
			bool result = true;
            ProcessingDateTime = Time.Now;
			// Check if all items are processed
			foreach (Invoice invoice in invoices)
			{
				foreach (InvoiceItem ii in invoice.Items)
				{
					if (ii.BuyableObjectK > 0)
					{
						IBob buyableObject = Bob.Get(ii.BuyableObjectType, ii.BuyableObjectK);
                        
						if (buyableObject is IBuyable && !((IBuyable)buyableObject).IsProcessed(ii.Type))
						{                            
							((IBuyable)buyableObject).Process(ii.Type, ii.Price, ii.Total);
                            if (buyableObject is CampaignCredit)
                            {
                                ((CampaignCredit)buyableObject).ActionDateTime = ProcessingDateTime;
                                ((CampaignCredit)buyableObject).BuyableObjectK = invoice.K;
                                ((CampaignCredit)buyableObject).BuyableObjectType = Model.Entities.ObjectType.Invoice;
								((CampaignCredit)buyableObject).SetUsrAndActionUsr(Usr.Current, false);
                                ((CampaignCredit)buyableObject).UpdateWithRecalculateBalance();
                            }
							result = ((IBuyable)buyableObject).IsProcessed(ii.Type);
						}                        
					}
				}
			}
			if (!result)
			{
				ProcessingVal.ErrorMessage = INVOICE_DONE_WITH_ERRORS;
				string exceptionMessage = "Not all invoice items were processed.";

				ProcessException(new Exception(exceptionMessage));
			}

			// if this was simply buying the credits to afford a Campaign Credit payment, now fire that payment
			if (result && this.PaymentTypeIsCampaignCredits)
			{
				PayWithCampaignCredits();
			}

			return result;
		}

		private string InvoicesToHTML()
		{
			StringBuilder sb = new StringBuilder();

			if (this.Invoices != null && this.Invoices.Count > 0)
			{
				sb.Append("<ul>");

				for (int i = 0; i < this.Invoices.Count; i++)
				{
					sb.Append("<li>");
					if (this.Invoices[i].K > 0)
					{
						sb.Append("InvoiceK=");
						sb.Append(this.Invoices[i].K.ToString());
					}
					else
						sb.Append("New invoice");

					for (int j = 0; j < this.Invoices[i].InvoiceItemDataHolderList.Count; j++)
					{
						sb.Append("<ol><li>");
						if (this.Invoices[i].InvoiceItemDataHolderList[j].K > 0)
						{
							sb.Append("InvoiceItemK=");
							sb.Append(this.Invoices[i].InvoiceItemDataHolderList[j].K.ToString());
						}
						else
							sb.Append("New item");

						sb.Append("<ul type=\"square\"><li>Type: ");
						sb.Append(Utilities.CamelCaseToString(this.Invoices[i].InvoiceItemDataHolderList[j].Type.ToString()));
						sb.Append("</li><li>Desc: ");
						sb.Append(this.Invoices[i].InvoiceItemDataHolderList[j].Description);
						sb.Append("</li>");

						if (this.Invoices[i].InvoiceItemDataHolderList[j].BuyableObjectK > 0)
						{
							sb.Append("<li>BuyableObjectK: ");
							sb.Append(this.Invoices[i].InvoiceItemDataHolderList[j].BuyableObjectK.ToString());
							sb.Append("</li>");
						}

						sb.Append("<li>Price: ");
						sb.Append(this.Invoices[i].InvoiceItemDataHolderList[j].Price.ToString("c"));
						sb.Append("</li><li>VAT: &nbsp;");
						sb.Append(this.Invoices[i].InvoiceItemDataHolderList[j].Vat.ToString("c"));
						sb.Append("</li><li>Total: ");
						sb.Append(this.Invoices[i].InvoiceItemDataHolderList[j].Total.ToString("c"));
						sb.Append("</li><li>Processed: ");
						sb.Append(Utilities.BooleanToYesNo(this.Invoices[i].InvoiceItemDataHolderList[j].ItemProcessed));
						sb.Append("</li></ul></li></ol>");
					}
					sb.Append("</li>");
				}

				sb.Append("</ul>");
			}

			return sb.ToString();
		}

		private string CampaignCreditsToHTML()
		{
			StringBuilder sb = new StringBuilder();

			if (this.CampaignCredits != null && this.CampaignCredits.Count > 0)
			{
				sb.Append("<ul>");

				for (int i = 0; i < this.CampaignCredits.Count; i++)
				{
					sb.Append("<li>");
					if (this.CampaignCredits[i].K > 0)
					{
						sb.Append("CampaignCreditK= ");
						sb.Append(this.CampaignCredits[i].K.ToString());
					}
					else
						sb.Append("New campaign credit");

					sb.Append("<br>Credits= ");
					sb.Append(Math.Abs(this.CampaignCredits[i].Credits).ToString());

					if (this.CampaignCredits[i].BuyableObject != null)
					{
						sb.Append("<ul type=\"square\"><li>");
						sb.Append(((IBuyableCredits)this.CampaignCredits[i].BuyableObject).AsHTML());
						sb.Append("</li></ul>");
					}
					sb.Append("</li>");
				}

				sb.Append("</ul>");
			}

			return sb.ToString();
		}

		
		private bool ProcessSuccessfulSecPayTransfer(SecPay secPay)
		{
			bool result = true;
            
			if (this.Invoices.Count != secPay.Invoices.Count)
			{
				if (secPay.Invoices.Count == 0)
				{
					result = false;
					ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG;
				}
				else
					ProcessingVal.ErrorMessage = CALL_ADMIN_INVOICE_ERROR_MSG;

				string exceptionMessage = "Payment control invoices = " + this.Invoices.Count.ToString() + ". SecPay Invoices = " + secPay.Invoices.Count.ToString() + ". Invoices attempting process:<br>";

				ProcessException(new Exception(exceptionMessage));
			}
			else
				HandleAndVerifyInvoicesProcessed(secPay.Invoices);

			if (secPay.Transfer.IsFullyApplied == false)
			{
				Transfer secPayTransfer = new Transfer(secPay.Transfer.K);
				if (secPay.Transfer.IsFullyApplied == false)
				{
					string msg = "SecPay Transfer (K=" + secPayTransfer.K.ToString() + ") was not fully applied.";
					Utilities.AdminEmailAlert(msg , msg, new DsiUserFriendlyException(msg), secPayTransfer);
				}
			}
			if (result)
			{
				if (PaymentDone != null)
					PaymentDone(this, new PaymentDoneEventArgs(secPay.Invoices, this.CampaignCredits, false, ""));
			}
			if (GetFullAddress)
			{
				Usr usr = Usr.Current;

			}
			return result;
		}

		#endregion

		#region ProcessException
		private void ProcessException(Exception ex)
		{
			ProcessException(ex, null, "", true);
		}

		private void ProcessException(Exception ex, SecPay secPay)
		{
			ProcessException(ex, secPay, "", true);
		}

		private void ProcessException(Exception ex, string cardNumber)
		{
			ProcessException(ex, null, cardNumber, true);
		}

		private void ProcessException(Exception ex, SecPay secPay, string cardNumber, bool unlockInvoiceItems)
		{
			//if (!(ex is System.Threading.ThreadAbortException))
			//{
				ViewState["DuplicateGuidTransfer"] = Guid.NewGuid();

				ProcessingVal.IsValid = false;

				if (ContainerPage != null && !HideValidationSummary)
					ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");

				Mailer sm = new Mailer();
				sm.TemplateType = Mailer.TemplateTypes.AdminNote;
				sm.Subject = "Exception while sending payment!";
				sm.To = "d.brophy@dontstayin.com, t.aylott@dontstayin.com, neil@dontstayin.com";

				if (CurrentPromoter != null)
					sm.Body = "<p>Exception while sending payment! - Usr = " + CurrentUsr.NickName + " (" + CurrentUsr.K + "), Promoter = " + CurrentPromoter.Name + " (" + CurrentPromoter.K.ToString() + "), ActionUsr = " + Usr.Current.NickName + " (" + CurrentUsr.K + ")</p>";
				else
					sm.Body = "<p>Exception while sending payment! - Usr = " + CurrentUsr.NickName + " (" + CurrentUsr.K + "), PromoterK = 0, ActionUsr = " + Usr.Current.NickName + " (" + CurrentUsr.K + ")</p>";
				sm.Body += "<p>Exception details: " + ex.Message + "</p>";
				sm.Body += "<p>Exception stack trace: " + ex.StackTrace + "</p>";

				sm.Body += "<p>Invoice(s):<br>" + InvoicesToHTML() + "</p>";

				if (this.CampaignCredits.Count > 0)
				{
					sm.Body += "<p>Campaign credit(s):<br>" + CampaignCreditsToHTML() + "</p>";
				}

				if (secPay != null && secPay.Transfer != null)
				{
					sm.Body += "<p>Card number: " + secPay.Transfer.CardNumber + "</p>";
					if (secPay.Transfer.CardResponseMessage.Length > 0)
						sm.Body += "<p>SecPay response message: " + secPay.Transfer.CardResponseMessage + "</p>";
					if (secPay.Transfer.CardResponseAuthCode.Length > 0)
						sm.Body += "<p>SecPay response auth code: " + secPay.Transfer.CardResponseAuthCode + "</p>";
					if (secPay.Transfer.CardResponseCode.Length > 0)
						sm.Body += "<p>SecPay response code: " + secPay.Transfer.CardResponseCode + "</p>";
					if (secPay.Transfer.CardResponseCv2Avs.Length > 0)
						sm.Body += "<p>SecPay response CV2 AVS: " + secPay.Transfer.CardResponseCv2Avs + "</p>";
					if (secPay.Transfer.CardResponseRespCode.Length > 0)
						sm.Body += "<p>SecPay response resp code: " + secPay.Transfer.CardResponseRespCode + "</p>";
				}
				else if (cardNumber.Length > 0)
				{
					sm.Body += "<p>Card number: " + (cardNumber.Length > Utilities.DISPLAY_CARD_LAST_NUMBER_OF_DIGITS ? cardNumber.Substring(cardNumber.Length - Utilities.DISPLAY_CARD_LAST_NUMBER_OF_DIGITS) : cardNumber) + "</p>";
				}
				sm.Body += CurrentBuyer.AsHTML();
				sm.Send();
			//}

            // Unlock items so users may attempt to process again
            try
            {
                if(unlockInvoiceItems)
                    this.UnlockInvoiceItems();
            }
            catch (Exception)
            { }
			return;
		}
		#endregion

		private void SetUsrAndActionUsr(InvoiceDataHolder idh)
		{
			// for new invoices
			if (idh.K == 0)
			{
				idh.Promoter = CurrentPromoter;
				idh.SetUsrAndActionUsr(CurrentUsr, false);
			}
		}

		#region PayLaterIssueInvoice_Click
		public void PayLaterIssueInvoice_Click(object o, System.EventArgs e)
		{
			CheckSslStatus();

			try
			{
				if (IsValidTotal())
				{
					// Pay with credit / Balance
					if (InvoiceTotal <= CurrentBuyer.CreditLimit + CurrentBuyerBalance)
					{
						if (this.ValidateAndLockInvoices())
						{
							List<Invoice> invoices = new List<Invoice>();
							try
							{							
								// Create invoice(s)
								foreach (InvoiceDataHolder idh in Invoices)
								{
									bool creatingInvoice = idh.K == 0;
									if (CurrentBuyer.CreditLimit > 0)
										idh.DueDateTime = DateTime.Now.AddDays(InvoiceDueDays);
									SetInvoiceDatesUsrsGuid(idh);
									Invoice invoice = idh.UpdateInsertDelete();
									invoice.AssignBuyerType();
									invoice.UpdateAndAutoApplySuccessfulTransfersWithAvailableMoney();
									if (creatingInvoice)
										invoice.Process();
									invoices.Add(invoice);
									Utilities.EmailInvoice(invoice, true);
								}
							}
							catch (Exception ex)
							{
								ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG;
                                //try
                                //{
                                //    this.UnlockInvoiceItems();
                                //}
                                //catch (Exception)
                                //{ }
								ProcessException(ex);
								return;
							}
							if (this.HandleAndVerifyInvoicesProcessed(invoices))
							{
								if (PaymentDone != null)
									PaymentDone(this, new PaymentDoneEventArgs(invoices, this.CampaignCredits, false, ""));
							}
						}
					}
					else
					{
						// Re evaluate.
						this.Initialize();
					}
				}
				else
				{
					// Re evaluate.
					this.Initialize();
				}
			}
			catch (Exception ex)
			{
				ProcessingVal.ErrorMessage = RETRY_INVOICE_ERROR_MSG; 
				ProcessException(ex);
			}

			ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");
		}
		#endregion

		#region PayPartCardButton_Click
		protected void PayPartCardButton_Click(object sender, EventArgs eventArgs)
		{
			CheckSslStatus();

			PayUsingAvailableCredit = true;
			Initialize();
			ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");
			return;

			//try
			//{
			//    if (IsValidTotal() && TotalDueExceedsAvailableCredit)
			//    {
			//        //TODO: Why do we have to hide the validation summary here?
			//        PaymentValidationSummary.Visible = false;

			//        // Create invoice and switch to Pay Now
			//        this.PayNowPanel.Visible = true;
			//        this.BindPaymentOptions();
			//        // Allow Pay Later is only for promoters who have credit remaining
			//        this.BindPayLater();
			//    }
			//    else
			//    {
			//        // Re evaluate.
			//        this.Initialize();
			//    }
			//}
			//catch (Exception ex)
			//{
			//    ProcessException(ex);

			//    Mailer sm = new Mailer();
			//    sm.Body = "<p>Exception while sending payment! - Usr = " + Usr.Current.NickName + " (" + Usr.Current.K + "), PromoterK=" + PromoterK.ToString() + "</p>";
			//    sm.Body += "<p>Message: " + ex.Message + "</p>";
			//    sm.TemplateType = Mailer.TemplateTypes.AdminNote;
			//    sm.Subject = "Exception while sending payment!";
			//    sm.To = "d.brophy@dontstayin.com, j.brophy@dontstayin.com, t.aylott@dontstayin.com";
			//    sm.Send();
			//    return;
			//}			
		}
		#endregion

		#endregion

		#region InitialiseTicketsEntryCheckboxPanel()
		void InitialiseTicketsEntryCheckboxPanel()
		{
			uiTicketsEntryCheckboxPanel.Visible = false;
			foreach (InvoiceDataHolder idh in this.Invoices)
			{
				foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
				{
					if (iidh.BuyableObjectType == Model.Entities.ObjectType.Ticket && iidh.Type == InvoiceItem.Types.EventTickets)
					{
						uiTicketsEntryCheckboxPanel.Visible = true;
						Ticket ticket = new Ticket(iidh.BuyableObjectK);
						this.uiTicketConfirmationMessage.InnerText = ticket.TicketRun.GetPaymentControlConfirmationMessage();
						return;
					}
				}
			}
		}
		#endregion

		private bool totalChangedSinceLastPostBack = false;

		#region Reset()
		public void Reset()
		{
			this.Invoices.Clear();
			this.CampaignCredits.Clear();
			this.PayOptionRadioButtonPayNow.Checked = false;
			this.PayOptionRadioButtonPayLater.Checked = false;
            this.PayOptionRadioButtonPayWithBalance.Checked = false;
            this.PayOptionRadioButtonPayWithCampaignCredit.Checked = false;
			this.ViewState["DuplicateGuidInvoice"] = Guid.NewGuid();
			this.ViewState["DuplicateGuidTransfer"] = Guid.NewGuid();
			this.ViewState["InvoiceTotal"] = null;
            this.ViewState["AllowPayWithBalance"] = null;
            this.ViewState["AllowPayWithCredit"] = null;
            this.ViewState["HideValidationSummary"] = null;
            this.ViewState["PromoterK"] = null;
            this.ViewState["UsrK"] = null;
            this.ViewState["PayUsingAvailableCampaignCredits"] = null;
            this.ViewState["PayUsingAvailableCredit"] = null;
            this.ViewState["CampaignCreditItems"] = null;
            this.ViewState["Items"] = null;
            this.ViewState["CampaignCreditItems"] = null;
            
		}
		#endregion
		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["Items"] = Invoices;
			this.ViewState["CampaignCreditItems"] = CampaignCredits;
			this.ViewState["Balance"] = CurrentBuyerBalance;
            //this.ViewState["AmountPayByCredit"] = CreditApplied;
            //this.ViewState["NewTotal"] = InvoiceTotalAfterBalanceAndCredit;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["Items"] != null) Invoices = (List<InvoiceDataHolder>)this.ViewState["Items"];
			if (this.ViewState["CampaignCreditItems"] != null) CampaignCredits = (List<CampaignCredit>)this.ViewState["CampaignCreditItems"];
		}
		#endregion

		#region PaymentClick
		public event PaymentClickEventHandler PaymentClick;
		public delegate void PaymentClickEventHandler(object sender, PaymentClickEventArgs eventArgs);
		public class PaymentClickEventArgs : EventArgs
		{
			public PaymentClickEventArgs()
			{
			}

			#region Abort
			public bool Abort
			{
				get
				{
					return abort;
				}
				set
				{
					abort = value;
				}
			}
			bool abort;
			#endregion
		}
		#endregion

		#region PaymentDone
		public event PaymentDoneEventHandler PaymentDone;
		public delegate void PaymentDoneEventHandler(object sender, PaymentDoneEventArgs eventArgs);
		public class PaymentDoneEventArgs : EventArgs
		{
			#region Invoices
			public List<Invoice> Invoices
			{
				get
				{
					return invoices;
				}
				set
				{
					invoices = value;
				}
			}
			List<Invoice> invoices;
			#endregion
			#region CampaignCredits
			public List<CampaignCredit> CampaignCredits
			{
				get
				{
					return campaignCredits;
				}
				set
				{
					campaignCredits = value;
				}
			}
			List<CampaignCredit> campaignCredits;
			#endregion
			#region Duplicate
			public bool Duplicate
			{
				get
				{
					return duplicate;
				}
				set
				{
					duplicate = value;
				}
			}
			bool duplicate;
			#endregion
			#region ErrorMessage
			public string ErrorMessage
			{
				get
				{
					return errorMessage;
				}
				set
				{
					errorMessage = value;
				}
			}
			string errorMessage;
			#endregion

			public PaymentDoneEventArgs(List<Invoice> invoices, List<CampaignCredit> campaignCredits, bool duplicate, string errorMessage)
			{
				this.Invoices = invoices;
				this.CampaignCredits = campaignCredits;
				this.Duplicate = duplicate;
				this.ErrorMessage = errorMessage;
			}
		}
		#endregion

		#region ShowHideCountryDropDownList
		private void ShowHideCountryDropDownList()
		{
			CountryTable.Visible = this.FraudCheck.Equals(Transfer.FraudCheckEnum.Strict);
		}
		#endregion

		#region GetPriceToString
		public string GetPriceToString(object o)
		{
			string output = "";
			if (o is InvoiceDataHolder)
			{
				InvoiceDataHolder idh = (InvoiceDataHolder)o;
				// Show ExVat for Promoters and IncVat for other users
				if (this.PromoterK > 0)
					output = idh.Price.ToString("c");
				else
					output = idh.Total.ToString("c");
			}
			return output;
		}
		#endregion

		#region IsValidTotal
		/// <summary>
		/// Checks if the total amount to be paid on the payment control has been changed since the last postback
		/// </summary>
		/// <returns></returns>
		private bool IsValidTotal()
		{
			bool result = true;

			if (totalChangedSinceLastPostBack)
			{
				this.ProcessingVal.IsValid = false;
				this.ProcessingVal.ErrorMessage = "The items have been altered and the price has changed.  Please verify the new price and pay.";
				result = false;
			}

			return result;
		}

		#endregion

        #region Validate and Lock Invoices
        /// <summary>
		/// Checks for duplicates, then checks for any locked invoice items, and then checks that all invoice items are ready to be processed. If all checks pass, then it locks all IBuyable invoice items.
		/// </summary>
		/// <returns>True if all checks pass and all IBuyable invoice items are locked. Otherwise false</returns>
		private bool ValidateAndLockInvoices()
		{
			// Check for duplicate invoices
            if (DoesDuplicateGuidExistInDb())
            {
                this.HandleDuplicates();
                return false;
            }
            else if (this.Invoices.Count > 0 && this.Invoices[0].K > 0)
            {
                foreach (InvoiceDataHolder idh in this.Invoices)
                {
                    if (idh.K == 0 || idh.Paid)
                        return false;
                }
                return true;
            }
            // Ensure all invoice items arent locked
            else
            {
                double lockedSeconds = 0d;

                if (this.CampaignCredits.Count > 0)
                {
                    lockedSeconds = this.CampaignCreditsLockedSeconds();
                    if (lockedSeconds <= 0)
                    {
                        for (int i = 0; i < this.CampaignCredits.Count; i++)
                        {
                            this.CampaignCredits[i] = new CampaignCredit(CampaignCredits[i].K);
                            if (this.CampaignCredits[i].Enabled)
                                return false;
                        }
                    }
                }
                else
                {
                    lockedSeconds = this.InvoiceItemsLockedSeconds();
                }
                if (lockedSeconds > 0)
                {
                    // Add a few seconds, so users dont try again too soon and find the item locked again (as they have shown to do).
                    lockedSeconds += 5;

                    ProcessingVal.ErrorMessage = string.Format(INVOICE_ITEMS_LOCKED, lockedSeconds);
                    ProcessException(new Exception(ProcessingVal.ErrorMessage), null, "", false);
                    return false;
                }
                else
                {
                    // Now lock items
                    this.LockInvoiceItems();

                    foreach (InvoiceDataHolder idh in this.Invoices)
                    {
                        // Need to add CardNumberHash to EventTickets for validation processing
                        if (CardNumberHash != new Guid())
                        {
                            foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
                            {
                                if (iidh.BuyableObjectType == Model.Entities.ObjectType.Ticket && iidh.Type == InvoiceItem.Types.EventTickets)
                                {
                                    try
                                    {
                                        Ticket buyingTicket = new Ticket(iidh.BuyableObjectK);
                                        buyingTicket.CardNumberHash = CardNumberHash;
                                        buyingTicket.Update();
                                    }
                                    catch
                                    { }
                                }
                            }
                        }
                        try
                        {
                            // Checks that all items are ready to be processed. If any are not, then it will throw an exception
                            if (!idh.IsReadyToProcess())
                            {
								throw new DsiUserFriendlyException(ITEMS_NOT_READY_FOR_PROCESSING);
                                
                            }
							foreach (CampaignCredit cc in this.CampaignCredits)
							{
								if (cc.BuyableObject != null && cc.BuyableObject is IBuyableCredits && !((IBuyableCredits)cc.BuyableObject).IsReadyForProcessingCredits(cc.InvoiceItemType, Math.Abs(cc.Credits)))
								{
									throw new DsiUserFriendlyException(ITEMS_ALREADY_PROCESSED);
								}								
							}
                        }
                        catch (DsiUserFriendlyException ex)
                        {
                            ProcessingVal.ErrorMessage = ex.Message;
                            ProcessException(ex);
                            return false;
                        }
                        catch (Exception ex)
                        {
                            ProcessingVal.ErrorMessage = ITEMS_NOT_READY_FOR_PROCESSING;
                            ProcessException(ex);
                            return false;
                        }
                    }
                    return true;
                }
            }
		}
		#endregion

		#region DoesDuplicateGuidExistInDb
		private enum DuplicateStateEnum
		{
			None = 1,
			InvoiceProcessedAndTransferSuccess = 2,
			InvoiceProcessedAndTransferFail = 3,
			InvoiceFailAndTransferSuccess = 4,
			InvoiceFailAndTransferFail = 5
		}

		private DuplicateStateEnum DuplicateState = DuplicateStateEnum.None;

		/// <summary>
		/// Checks if there are any invoices or transfers with the DuplicateGUID from the current page ViewState. This prevents invoices and transfers being issued multiple times from user error.
		/// </summary>
		/// <returns></returns>
		private bool DoesDuplicateGuidExistInDb()
		{
			bool transferSuccess = false;

			bool duplicateInvoice = Invoice.DoesDuplicateGuidExistInDb((Guid)this.ViewState["DuplicateGuidInvoice"]);

			Query duplicateTransferQuery = new Query(new Q(Transfer.Columns.DuplicateGuid, (Guid)this.ViewState["DuplicateGuidTransfer"]));
			duplicateTransferQuery.Columns = new ColumnSet(Transfer.Columns.K, Transfer.Columns.Status);
			TransferSet tset = new TransferSet(duplicateTransferQuery);
			if (tset.Count > 0)
			{
				foreach (Transfer t in tset)
				{
					if (t.Status.Equals(Transfer.StatusEnum.Success))
					{
						transferSuccess = true;
						break;
					}
				}
					//throw new Exception("Payment already processed successfully.");
					//else if (tset[0].Status.Equals(Transfer.StatusEnum.Failed))
					//    throw new Exception("Payment process " + tset[0].Status.ToString().ToLower() + ". Please try again. If payment is not successful, please contact an administrator for assistance.");
					//else
					//    throw new Exception("Payment process was not successful. Please try again. If payment is not successful, please contact an administrator for assistance.");}
			}

			if (!duplicateInvoice && tset.Count == 0)
				DuplicateState = DuplicateStateEnum.None;
			else if (duplicateInvoice && transferSuccess)
				DuplicateState = DuplicateStateEnum.InvoiceProcessedAndTransferSuccess;
			else if (duplicateInvoice && !transferSuccess)
				DuplicateState = DuplicateStateEnum.InvoiceProcessedAndTransferFail;
			else if (!duplicateInvoice && transferSuccess)
				DuplicateState = DuplicateStateEnum.InvoiceFailAndTransferSuccess;
			else if (!duplicateInvoice && !transferSuccess)
				DuplicateState = DuplicateStateEnum.InvoiceFailAndTransferFail;

			return duplicateInvoice || tset.Count > 0;
		}
		#endregion

		#region InvoiceItemsLockedSeconds
		/// <summary>
		/// Checks all invoice items and gets the amount of time (in seconds) of at least how long the invoice will be locked. 0 seconds = no items locked
		/// </summary>
		/// <returns>The # of seconds of how long the first invoice item is locked. 0 seconds = no items locked.</returns>
		private double InvoiceItemsLockedSeconds()
		{
			foreach (InvoiceDataHolder idh in this.Invoices)
			{
				foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
				{
					if (iidh.BuyableObjectK > 0)
					{
						var buyableObject = Bob.Get(iidh.BuyableObjectType, iidh.BuyableObjectK);
						if (buyableObject is IBuyable && ((IBuyable)buyableObject).IsLocked)
						{
							return Math.Round(Vars.IBUYABLE_LOCK_SECONDS - ((TimeSpan)(DateTime.Now - ((IBuyable)buyableObject).BuyableLockDateTime)).TotalSeconds, 0);
						}
					}
				}
			}

			return 0;
		}
		#endregion

        #region CampaignCreditsLockedSeconds
        /// <summary>
        /// Checks all invoice items and gets the amount of time (in seconds) of at least how long the invoice will be locked. 0 seconds = no items locked
        /// </summary>
        /// <returns>The # of seconds of how long the first invoice item is locked. 0 seconds = no items locked.</returns>
        private double CampaignCreditsLockedSeconds()
        {
            foreach (CampaignCredit cc in this.CampaignCredits)
            {
                if (cc.BuyableObjectK > 0)
                {
                    var buyableObject = Bob.Get(cc.BuyableObjectType, cc.BuyableObjectK);
                    if (buyableObject is IBuyable && ((IBuyable)buyableObject).IsLocked)
                    {
                        return Math.Round(Vars.IBUYABLE_LOCK_SECONDS - ((TimeSpan)(DateTime.Now - ((IBuyable)buyableObject).BuyableLockDateTime)).TotalSeconds, 0);
                    }
                }
            }
            return 0;
        }
        #endregion

		#region Lock / Unlock Invoice Items
		/// <summary>
		/// Goes through all invoice items and for the IBuyable items it runs Lock()
		/// </summary>
		private void LockInvoiceItems()
		{
			foreach (InvoiceDataHolder idh in this.Invoices)
			{
				foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
				{
					if (iidh.BuyableObjectK > 0)
					{
						var buyableObject = Bob.Get(iidh.BuyableObjectType, iidh.BuyableObjectK);
						if (buyableObject is IBuyable)
						{
							((IBuyable)buyableObject).Lock();
						}
					}
				}
			}
		}

		/// <summary>
		/// Goes through all invoice items and for the IBuyable items it runs Unlock()
		/// </summary>
		private void UnlockInvoiceItems()
		{
			foreach (InvoiceDataHolder idh in this.Invoices)
			{
				foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
				{
					if (iidh.BuyableObjectK > 0)
					{
						var buyableObject = Bob.Get(iidh.BuyableObjectType, iidh.BuyableObjectK);
						if (buyableObject is IBuyable && !((IBuyable)buyableObject).IsProcessed(iidh.Type))
						{
							((IBuyable)buyableObject).Unlock();
						}
					}
				}
			}
		}
		#endregion

		#region CardNumberLast6Digits
		public string CardNumberLast6Digits
		{
			get
			{
				Regex numbesRegex = new Regex("[^0123456789]");
				string number = numbesRegex.Replace(CardNumber.Text, "");
				return number.Substring(number.Length - 6);
			}
		}
		#endregion

		#region CardNumberHash
		private Guid CardNumberHash = new Guid();
		#endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(ViewState["InvoiceTotal"] != null && (decimal)ViewState["InvoiceTotal"] != InvoiceTotal)
				totalChangedSinceLastPostBack = true;


			PaymentValidationSummary.Visible = !HideValidationSummary;
			PostcodeVal1.ValidationExpression = Cambro.Misc.RegEx.Postcode;
			
			
			if (Usr.Current != null)
			{
				if(this.ViewState["DuplicateGuidInvoice"] == null)
					this.ViewState["DuplicateGuidInvoice"] = Guid.NewGuid();
				if (this.ViewState["DuplicateGuidTransfer"] == null)
					this.ViewState["DuplicateGuidTransfer"] = Guid.NewGuid();

				if (!Page.IsPostBack)
				{
					this.ViewState["DuplicateGuidInvoice"] = Guid.NewGuid();
					this.ViewState["DuplicateGuidTransfer"] = Guid.NewGuid();

					LoadBuyerDetailsToScreen();
					SetupCountryDropDownList();
				}
			}
			// This allows us to set ViewState["InvoiceTotal"] later
			if(InvoiceTotal != 0)
				ViewState["InvoiceTotal"] = InvoiceTotal;
		}
		#endregion

		#region SetupCountryDropDownList
		private void SetupCountryDropDownList()
		{
			if (LockedCountryK != null)
			{
				CountryDropDownList.DataSource = Country.Countries(LockedCountryK.Value);
			}
			else
			{
				CountryDropDownList.DataSource = Country.Countries();
			}
			CountryDropDownList.DataTextField = "Name";
			CountryDropDownList.DataValueField = "K";
			CountryDropDownList.SelectedValue = null;
			CountryDropDownList.DataBind();

			CountryDropDownList.SelectedValue = (LockedCountryK ?? 224).ToString();

			if (Usr.Current != null && Usr.Current.AddressCountryK > 0)
			{
				try
				{
					CountryDropDownList.SelectedValue = Usr.Current.AddressCountryK.ToString();
				}
				catch {}
			}
		}
		
		#endregion

		#region Initialize
		/// <summary>
		/// Initialize needs to be run when this control becomes visible for the first time.
		/// </summary>
		public void Initialize()
		{
			ResetCurrentBuyer();
			ResetMoneyHelpers();

			BindSavedCards();

			// if ViewState["InvoiceTotal"] has not yet been set, then set it now.
			if(ViewState["InvoiceTotal"] == null && InvoiceTotal != 0)
				ViewState["InvoiceTotal"] = InvoiceTotal;

			ShowHideCountryDropDownList();

			InitialiseTicketsEntryCheckboxPanel();
		}
		#endregion

		#region LoadBuyerDetailsToScreen
		public void LoadBuyerDetailsToScreen()
		{
			Name.Text = CurrentUsr.FullName;

			Address.Text = CurrentBuyer.AddressStreet;
			Postcode.Text = CurrentBuyer.AddressPostcode;
			if (GetFullAddress)
			{
				uiAddressArea.Text = CurrentBuyer.AddressArea;
				uiAddressTown.Text = CurrentBuyer.AddressTown;
				uiAddressCounty.Text = CurrentBuyer.AddressCounty;
				CountryDropDownList.SelectedValue = CurrentBuyer.AddressCountryK.ToString();
			}
		}
		#endregion

		#region BackgroundColour
		//FED551 light
		//FECA26 dark
		public string BackgroundColour
		{
			get
			{
				if (this.ViewState["PaymentBackgroundColour"] == null)
				{
					this.ViewState["PaymentBackgroundColour"] = "#FED551";
				}
				return (string)this.ViewState["PaymentBackgroundColour"];
			}
			set
			{
				this.ViewState["PaymentBackgroundColour"] = value;
			}
		}
		#endregion

		#region BorderColour
		//FED551 light
		//FECA26 dark
		public string BorderColour
		{
			get
			{
				if (this.ViewState["PaymentBorderColour"] == null)
				{
					this.ViewState["PaymentBorderColour"] = "#CBA21E";
				}
				return (string)this.ViewState["PaymentBorderColour"];
			}
			set
			{
				this.ViewState["PaymentBorderColour"] = value;
			}
		}
		#endregion
		#region AltColour
		public string AltColour
		{
			get
			{
				if (AltColour.Length==0)
				{
					if (BackgroundColour.Equals("FED551"))
						return "FECA26";
					else
						return "FED551";
				}
				else
					return altColour;
			}
			set
			{
				altColour = value;
			}
		}
		private string altColour="";
		#endregion
		#region ContainerPage
		Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				if (Page is Spotted.Master.DsiPage)
					return (Spotted.Master.DsiPage)Page;
				else
					return null;
			}
		}
		#endregion

		#region CardVal1
		public void CardVal1(object sender, ServerValidateEventArgs e)
		{
			if (SavedCardPanel.Visible && SavedCardOptionsSavedCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = CardNumber.Text.Trim().Length > 0;
			}
		}
		#endregion
		#region CardNumVal
		public void CardNumVal(object o, ServerValidateEventArgs e)
		{
			if (SavedCardPanel.Visible && SavedCardOptionsSavedCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				Regex r = new Regex("[^0-9]*");
				CardNumber.Text = r.Replace(CardNumber.Text, "");
				e.IsValid = CardNumber.Text.Length == 16 || CardNumber.Text.Length == 18 || CardNumber.Text.Length == 19 || CardNumber.Text.Length == 20;
			}
		}
		#endregion
		#region CardCv2Val
		protected void CardCv2Val(object sender, ServerValidateEventArgs e)
		{
			if (SavedCardPanel.Visible && SavedCardOptionsSavedCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = Cv2.Text.Trim().Length > 0;
			}
		}
		#endregion
		#region CardCv2RegexVal
		protected void CardCv2RegexVal(object sender, ServerValidateEventArgs e)
		{
			if (SavedCardPanel.Visible && SavedCardOptionsSavedCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				Regex r = new Regex(@"^\d{3}$");
				e.IsValid = r.IsMatch(Cv2.Text.Trim()) && Cv2.Text.Trim().Length == 3;
			}
		}
		#endregion
		#region CardEndDateMonthVal
		protected void CardEndDateMonthVal(object sender, ServerValidateEventArgs e)
		{
			if (SavedCardPanel.Visible && SavedCardOptionsSavedCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = EndDateMonth.Text.Trim().Length > 0;
			}
		}
		#endregion
		#region CardEndDateYearVal
		protected void CardEndDateYearVal(object sender, ServerValidateEventArgs e)
		{
			if (SavedCardPanel.Visible && SavedCardOptionsSavedCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = EndDateYear.Text.Trim().Length > 0;
			}
		}
		#endregion
		#region CardStartDateVal
		public void CardStartDateVal(object o, ServerValidateEventArgs e)
		{
			if (SavedCardPanel.Visible && SavedCardOptionsSavedCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				if (StartDateYear.Text.Length == 0 && StartDateMonth.Text.Length == 0)
				{
					e.IsValid = true;
					return;
				}
				try
				{
					int year = int.Parse(StartDateYear.Text.Trim());
					if (year > 80)
						year += 1900;
					else
						year += 2000;
					DateTime startDate = new DateTime(year, int.Parse(StartDateMonth.Text.Trim()), 1);
					e.IsValid = startDate < DateTime.Now;
				}
				catch
				{
					e.IsValid = false;
				}
			}
		}
		#endregion
		#region CardEndDateVal
		public void CardEndDateVal(object o, ServerValidateEventArgs e)
		{
			if (SavedCardPanel.Visible && SavedCardOptionsSavedCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				try
				{
					int year = int.Parse(EndDateYear.Text.Trim());
					if (year > 80)
						year += 1900;
					else
						year += 2000;
					DateTime endDate = new DateTime(year, int.Parse(EndDateMonth.Text.Trim()), 1);
					endDate = endDate.AddMonths(1);
					e.IsValid = endDate > DateTime.Now;
				}
				catch
				{
					e.IsValid = false;
				}
			}
		}
		#endregion
		#region SavedCardPasswordVal
		protected void SavedCardPasswordVal(object sender, ServerValidateEventArgs e)
		{
			if (!SavedCardPanel.Visible || SavedCardOptionsNewCard.Checked)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = CurrentUsr.CheckPassword(PasswordTextBox.Text);
			}
		}
		#endregion
		#region TicketsEntryCheckboxVal
		protected void TicketsEntryCheckboxVal(object sender, ServerValidateEventArgs e)
		{
			e.IsValid = TicketsEntryCheckbox.Checked;
		}
		#endregion

		#region CampaignCredits
		void BindPayWithCampaignCredits()
		{
			if (!PayWithCampaignCreditPanel.Visible)
			{
				return;
			}

			int credits = CurrentBuyer.CampaignCredits;

			if (credits - CampaignCreditsApplied >= InvoiceTotalAsCampaignCredits)
			{
				NotEnoughCampaignCreditsPanel.Visible = false;
                ApplyCampaignCreditBalanceButton.Visible = false;
                PayWithCampaignCreditsButton.Visible = true;
				CampaignCreditsRemainingPanel.Visible = true;
				CurrentCampaignCreditsLabel.Text = credits.ToString("N0");
				RemainingCampaignCreditsLabel.Text = (credits - InvoiceTotalAsCampaignCredits).ToString("N0");
			}
			else
			{
				NotEnoughCampaignCreditsPanel.Visible = credits <= 0;
				NotEnoughCampaignCreditsLabel.Text = "The total due (" + InvoiceTotalAsCampaignCredits.ToString("N0") +
					") is more than you currently have in campaign credits (" + credits.ToString("N0") + ").";
                CampaignCreditsRemainingPanel.Visible = credits > 0;
                PayWithCampaignCreditsButton.Visible = false;
                ApplyCampaignCreditBalanceButton.Visible = credits > 0;
                CurrentCampaignCreditsLabel.Text = credits.ToString("N0");
                //RemainingCampaignCreditsLabel.Text = "0";
                RemainingCampaignCreditsRow.Visible = false;
			}

            if (!ShowPayCampaignCredits)
            {
                PayWithCampaignCreditPanel.Visible = false;
                PayOptionRadioButtonPayWithCampaignCredit.Visible = false;
            }
		}

		#region Pay with Campaign Credits
        public void ApplyCampaignCreditBalanceButton_Click(object o, System.EventArgs e)
		{
            PayUsingAvailableCampaignCredits = true;
            invoiceTotalDone = false;
            Initialize();
            BindPaymentOptions();
            PayOptionRadioButtonPayWithCampaignCredit.Visible = false;
            ContainerPage.AnchorSkip(this.ClientID + "_PaymentAnchor");
            ViewState["InvoiceTotal"] = InvoiceTotal;
            return;
        }

		public void PayWithCampaignCreditsButton_Click(object o, System.EventArgs e)
		{
			CheckSslStatus();


            ProcessingDateTime = Time.Now;
			PayWithCampaignCredits();

			// if we successfully paid all with Campaign Credits, we won't need that Invoice item
            //if (Invoices.Count == 1 && Invoices[0].InvoiceItemDataHolderList.Count == 1 && Invoices[0].InvoiceItemDataHolderList[0].BuyableObjectType == Model.Entities.ObjectType.CampaignCredit)
            //{
            //    CampaignCredit cc = new CampaignCredit(Invoices[0].InvoiceItemDataHolderList[0].BuyableObjectK);
            //    if (!cc.Enabled)
            //    {
            //        cc.Delete();
            //    }
            //}
			if (PaymentDone != null)
				PaymentDone(this, new PaymentDoneEventArgs(null, this.CampaignCredits, false, ""));
		}

		private void PayWithCampaignCredits()
		{
            int currentCampaignCreditBalance = CurrentPromoter.CampaignCredits;
            bool result = true;

            int counter = 1;
			foreach (CampaignCredit cc in this.CampaignCredits)
			{
                if (CurrentPromoter.CampaignCredits + cc.Credits >= 0)
                {
                    cc.ActionDateTime = ProcessingDateTime;
                    cc.Enabled = true;
                    cc.DisplayOrder = counter;
					cc.SetUsrAndActionUsr(Usr.Current, false);
                    cc.UpdateWithRecalculateBalance();

                    if (cc.BuyableObjectK > 0)
                    {
                        var buyableObject = Bob.Get(cc.BuyableObjectType, cc.BuyableObjectK);
                        if (buyableObject is IBuyableCredits && !((IBuyableCredits)buyableObject).IsProcessed(cc.InvoiceItemType))
                        {
                            ((IBuyableCredits)buyableObject).ProcessCredits(cc.InvoiceItemType, Convert.ToInt32(Math.Abs(cc.Credits)));

                            result = result && ((IBuyableCredits)buyableObject).IsProcessed(cc.InvoiceItemType);
                        }
                    }
                }
                else
                    result = false;

                counter++;
			}
            if (!result)
            {
                ProcessingVal.ErrorMessage = INVOICE_DONE_WITH_ERRORS;
                string exceptionMessage = "Not all campaign credit items were processed.";

                ProcessException(new Exception(exceptionMessage));
            }
		}
		#endregion
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

	}
}
