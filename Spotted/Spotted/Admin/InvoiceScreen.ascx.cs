using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Local;
using Spotted.Controls;
using Spotted;
using Bobs.DataHolders;

namespace Spotted.Admin
{
    public partial class InvoiceScreen : AdminUserControl
    {
		public InvoiceScreen()
		{
			this.Init += new EventHandler(InvoiceScreen_Init);
			
		}

		void InvoiceScreen_Init(object sender, EventArgs e)
		{
			this.uiPromotersAutoComplete.ValueChanged += new EventHandler(uiPromotersAutoComplete_ValueChanged); 
		}

        int InvoiceK = 0;
        Invoice CurrentInvoice = new Invoice();
        InvoiceItemSet CurrentInvoiceItems;
        InvoiceTransferSet CurrentInvoiceTransfers;
        InvoiceCreditSet CurrentInvoiceCredits;

        List<InvoiceItemDataHolder> invoiceItemDataHolderList = new List<InvoiceItemDataHolder>();
        //List<InvoiceItemDataHolder> invoiceItemDataHolderDeleteList = new List<InvoiceItemDataHolder>();
        List<InvoiceTransferDataHolder> invoiceTransferDataHolderList = new List<InvoiceTransferDataHolder>();
        List<InvoiceTransferDataHolder> invoiceTransferDataHolderDeleteList = new List<InvoiceTransferDataHolder>();
        List<InvoiceCreditDataHolder> invoiceCreditDataHolderList = new List<InvoiceCreditDataHolder>();
        //List<InvoiceCreditDataHolder> invoiceCreditDataHolderDeleteList = new List<InvoiceCreditDataHolder>();
		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.uiAgencyDiscountTextBox.Visible = CurrentPromoter != null && CurrentPromoter.IsAgency;
			this.uiAgencyDiscountLabel.Visible = CurrentPromoter != null && CurrentPromoter.IsAgency;
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["InvoiceK"] != null)
                    InvoiceK = (int)ViewState["InvoiceK"];
                else
                {
					if (ContainerPage.Url["K"].IsInt)
					{
						InvoiceK = Convert.ToInt32(ContainerPage.Url["K"].Value);
						ViewState["InvoiceK"] = InvoiceK;
					}
                }
				if(InvoiceK > 0)
					CurrentInvoice = new Invoice(InvoiceK);
            }
            catch
            {
                // if no Invoice K, then assume we are creating a new Invoice
				Response.Redirect(Invoice.UrlAdminNewInvoice());

            }


			InvoiceItemsMessageLabel.Text = "";
			InvoiceItemsMessageLabel.Visible = false;

			if (!this.IsPostBack)
			{
				PriceTextBox.Style["text-align"] = "right";
				VATTextBox.Style["text-align"] = "right";
				TotalTextBox.Style["text-align"] = "right";
				DueDateValueLabel.Style["text-align"] = "right";
				TaxDateValueLabel.Style["text-align"] = "right";
				CreatedDateTextBox.Style["text-align"] = "right";
				PaidDateTextBox.Style["text-align"] = "right";
				OverrideDueDateCheckBox.Style["text-align"] = "right";
				OverrideTaxDateCheckBox.Style["text-align"] = "right";
				AmountDueTextBox.Style["text-align"] = "right";
				uiAgencyDiscountTextBox.Style["text-align"] = "right";


				NotesAddOnlyTextBox.ReadOnlyTextBox.CssClass = "readOnlyNotesTextBox";
				NotesAddOnlyTextBox.AddTextBox.CssClass = "addNotesTextBox";
				NotesAddOnlyTextBox.TimeStampFormat = "dd/MM/yy HH:mm";
				NotesAddOnlyTextBox.AuthorName = Usr.Current.NickName;
				NotesAddOnlyTextBox.InsertOption = AddOnlyTextBox.InsertOptions.AddAtBeginning;

				this.SearchForTransferHyperLink.NavigateUrl = UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "adminmainaccounting", new string[] { });

				ViewState["DuplicateGuid"] = Guid.NewGuid();

				ViewState["InvoiceTransferDataHolderList"] = null;
				ViewState["InvoiceTransferDataHolderDeleteList"] = null;
				ViewState["InvoiceItemDataHolderList"] = null;
				ViewState["InvoiceItemDataHolderDeleteList"] = null;

				SetupVatCodeDropDownLists();
				SetupSalesUsrDropDownList();

				if (InvoiceK > 0)
				{
					CalculateInvoiceItemVatAndTotals();
					LoadScreenFromInvoice();
					if (CurrentInvoice.PromoterK > 0)
						EmailButton.Visible = true;
				}
				else
				{
					if (Usr.Current != null)
					{
						this.uiActionUserAutoComplete.Value = Usr.Current.K.ToString();
						this.uiActionUserAutoComplete.Text = Usr.Current.Name;
						this.ActionUserValueLabel.Text = Usr.Current.Link();
						this.ActionUserValueLabel.Visible = false;
					}

					// Setup screen for new data input
					this.SetPaidImage(false);
				}
				CalculateInvoiceItemVatAndTotals();
				SetupAvailableTransfersDropDownList();
			}


            // Setup screen rules for updating only
			if (InvoiceK > 0)
			{
				DisableForPreviouslySaved();
				
				// Only allow invoice refund for invoices that were paid immediately by credit card and have not been credited
				if (this.InvoiceTransferDataHolderList.Count == 1 && CurrentInvoice.IsImmediateCreditCardPayment == true && InvoiceCreditDataHolderList.Count == 0
					&& InvoiceTransferDataHolderList[0].Method.Equals(Transfer.Methods.Card) && InvoiceTransferDataHolderList[0].Status.Equals(Transfer.StatusEnum.Success)
					&& Math.Round(InvoiceTransferDataHolderList[0].Amount, 2) == Math.Round(CurrentInvoice.Total, 2))
				{
					this.RefundInvoiceButton.Enabled = true;
					this.RefundInvoiceButton.Attributes.Clear();
					this.RefundInvoiceButton.Attributes.Add("onclick", "javascript:return confirm('This will create a credit and refund the transfer via SecPay. Are you sure you want to refund this Invoice?')");
				}
			}
			ShowHideControls(InvoiceK > 0);
			EnableSalesUsrAndAmount();
			SetAddTransfersOptions();
			SetPromoterAvailableFunds();
        }

        #region Properties
		protected Promoter CurrentPromoter
		{
			get
			{
				if (currentPromoter == null && this.uiPromotersAutoComplete.Value.Length > 0)
					currentPromoter = new Promoter(Convert.ToInt32(this.uiPromotersAutoComplete.Value));
				return currentPromoter;
			}
			set
			{
				currentPromoter = value;
				this.uiPromotersAutoComplete.Value = currentPromoter.K.ToString();
			}
		}
		private Promoter currentPromoter;

        public List<InvoiceItemDataHolder> InvoiceItemDataHolderList
        {
            get
            {
                if (ViewState["InvoiceItemDataHolderList"] != null)
                {
                    invoiceItemDataHolderList = (List<InvoiceItemDataHolder>)ViewState["InvoiceItemDataHolderList"];
                }
                else
                {
                    Query InvoiceItemQuery = new Query();
                    InvoiceItemQuery.QueryCondition = new Q(InvoiceItem.Columns.InvoiceK, InvoiceK);
                    invoiceItemDataHolderList.Clear();
                    CurrentInvoiceItems = new InvoiceItemSet(InvoiceItemQuery);

                    foreach (InvoiceItem invoiceItem in CurrentInvoiceItems)
                    {
                        invoiceItemDataHolderList.Add(new InvoiceItemDataHolder(invoiceItem));
                    }

                    ViewState["InvoiceItemDataHolderList"] = invoiceItemDataHolderList;
                }
                return invoiceItemDataHolderList;
            }
            set
            {
                invoiceItemDataHolderList = value;
                ViewState["InvoiceItemDataHolderList"] = invoiceItemDataHolderList;
            }
        }

        public List<InvoiceTransferDataHolder> InvoiceTransferDataHolderList
        {
            get
            {
                if (ViewState["InvoiceTransferDataHolderList"] != null)
                {
                    invoiceTransferDataHolderList = (List<InvoiceTransferDataHolder>)ViewState["InvoiceTransferDataHolderList"];
                }
                else
                {
                    Query InvoiceTransferQuery = new Query();
                    InvoiceTransferQuery.QueryCondition = new Q(InvoiceTransfer.Columns.InvoiceK, InvoiceK);
                    invoiceTransferDataHolderList.Clear();
                    CurrentInvoiceTransfers = new InvoiceTransferSet(InvoiceTransferQuery);

                    foreach (InvoiceTransfer invoiceTransfer in CurrentInvoiceTransfers)
                    {
                        invoiceTransferDataHolderList.Add(new InvoiceTransferDataHolder(invoiceTransfer));
                    }

                    ViewState["InvoiceTransferDataHolderList"] = invoiceTransferDataHolderList;
                }
                return invoiceTransferDataHolderList;
            }
            set
            {
                invoiceTransferDataHolderList = value;
                ViewState["InvoiceTransferDataHolderList"] = invoiceTransferDataHolderList;
            }
        }

        public List<InvoiceTransferDataHolder> InvoiceTransferDataHolderDeleteList
        {
            get
            {
                if (ViewState["InvoiceTransferDataHolderDeleteList"] != null)
                {
                    invoiceTransferDataHolderDeleteList = (List<InvoiceTransferDataHolder>)ViewState["InvoiceTransferDataHolderDeleteList"];
                }
                else
                {
                    invoiceTransferDataHolderDeleteList = new List<InvoiceTransferDataHolder>();
                    ViewState["InvoiceTransferDataHolderDeleteList"] = invoiceTransferDataHolderDeleteList;
                }
                return invoiceTransferDataHolderDeleteList;
            }
            set
            {
                invoiceTransferDataHolderDeleteList = value;
                ViewState["InvoiceTransferDataHolderDeleteList"] = invoiceTransferDataHolderDeleteList;
            }
        }

        public List<InvoiceCreditDataHolder> InvoiceCreditDataHolderList
        {
            get
            {
                if (ViewState["InvoiceCreditDataHolderList"] != null)
                {
                    invoiceCreditDataHolderList = (List<InvoiceCreditDataHolder>)ViewState["InvoiceCreditDataHolderList"];
                }
                else
                {
                    Query InvoiceCreditQuery = new Query();
                    InvoiceCreditQuery.QueryCondition = new Q(InvoiceCredit.Columns.InvoiceK, InvoiceK);
                    invoiceCreditDataHolderList.Clear();
                    CurrentInvoiceCredits = new InvoiceCreditSet(InvoiceCreditQuery);

                    foreach (InvoiceCredit invoiceCredit in CurrentInvoiceCredits)
                    {
                        invoiceCreditDataHolderList.Add(new InvoiceCreditDataHolder(invoiceCredit));
                    }

                    ViewState["InvoiceCreditDataHolderList"] = invoiceCreditDataHolderList;
                }
                return invoiceCreditDataHolderList;
            }
            set
            {
                invoiceCreditDataHolderList = value;
                ViewState["InvoiceCreditDataHolderList"] = invoiceItemDataHolderList;
            }
        }

        #endregion

		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["InvoiceK"] = InvoiceK;

			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["InvoiceK"] != null) InvoiceK = (int)this.ViewState["InvoiceK"];
		}
		#endregion

		#region Methods
		/// <summary>
		/// Disables / replaces specific data entry controls that are not modifiable after an invoice has been saved
		/// </summary>
		private void DisableForPreviouslySaved()
        {
			AddOnlyTextBox tempNotesAddOnlyTextBox = NotesAddOnlyTextBox;
            Utilities.EnableDisableControls(MainPanel, false);

			this.NotesAddOnlyTextBox = tempNotesAddOnlyTextBox;
			Utilities.EnableDisableControls(this.PurchaseOrderNumberTextBox, true);
			this.uiPromotersAutoComplete.Visible = false;
			this.uiUsersAutoComplete.Visible = false;
			this.UserDropDownList.Visible = false;
			this.uiActionUserAutoComplete.Visible = false;
			this.VatCodeDropDownList.Visible = false;
			this.DueDateCal.Visible = false;
			
			this.OverrideDueDatePanel.Visible = false;
			this.OverrideDueDateCheckBox.Visible = false;
			//this.OverrideTaxDateCheckBox.Visible = false;
			this.OverrideTaxDateCheckBox.Style.Add("display", "none");
			this.OverrideTaxDateCheckBox.Checked = Usr.Current.IsSuperAdmin;
			this.TaxDateValueLabel.Visible = !Usr.Current.IsSuperAdmin;
			this.uiAgencyDiscountTextBox.Visible = false;
			if (!Usr.Current.IsSuperAdmin)
			{
				this.TaxDateCal.Visible = false;
			}
			else
			{
				this.TaxDateCal.Disabled = false;
			}

			this.PromoterValueLabel.Visible = true;
			this.UserValueLabel.Visible = true;
			this.ActionUserValueLabel.Visible = true;
			
			this.VatCodeTextBox.Visible = true;
			this.DueDateValueLabel.Visible = true;
			

            // hide the update / delete column
            this.InvoiceItemsGridView.Columns[InvoiceItemsGridView.Columns.Count - 1].Visible = false;

            // Hide the add new footer row
            this.InvoiceItemsGridView.FooterRow.Visible = false;

			this.uiPromotersAutoComplete.AutoPostBack = false;
			this.uiPromotersAutoComplete.ValueChanged -= new EventHandler(uiPromotersAutoComplete_ValueChanged);

			DisableValidatorsForNonEditableControls();
        }

		private void SetAddTransfersOptions()
		{
			InvoiceTransferGridView.FooterRow.Visible = !(InvoiceK > 0 && CurrentInvoice.Paid == true);
			this.CreateTransferHyperLink.Visible = !(InvoiceK > 0 && CurrentInvoice.Paid == true);
			this.SearchForTransferHyperLink.Visible = !(InvoiceK > 0 && CurrentInvoice.Paid == true);
		}

		private void EnableSalesUsrAndAmount()
		{
			// only allow adjustment of SalesAmount and SalesUsr when payment was received in the current month. We do not want to change SalesAmount or SalesUsr after end of month sales commissions have been delegated.
			DateTime startOfThisMonth = Utilities.GetStartOfMonth(DateTime.Now);
	
			Utilities.EnableDisableControls(this.SalesAmountTextBox, Usr.Current.IsSuperAdmin && (CurrentInvoice.PaidDateTime.Equals(DateTime.MinValue) || CurrentInvoice.PaidDateTime >= startOfThisMonth));
			Utilities.EnableDisableControls(this.SalesUsrDropDownList, ((Usr.Current.IsSuperAdmin || (CurrentInvoice.K == 0 && CurrentPromoter != null && CurrentPromoter.SalesUsrK == Usr.Current.K) ||(CurrentInvoice.K > 0 && CurrentInvoice.SalesUsrK == Usr.Current.K)) 
																		&& (CurrentInvoice.PaidDateTime.Equals(DateTime.MinValue) || CurrentInvoice.PaidDateTime >= startOfThisMonth)));

			this.SalesUsrDropDownList.Visible = this.SalesUsrDropDownList.Enabled;
												//(Usr.Current.IsSuperAdmin || (CurrentPromoter != null && CurrentPromoter.SalesUsrK == Usr.Current.K)) 
												//&& (CurrentInvoice.PaidDateTime.Equals(DateTime.MinValue) || CurrentInvoice.PaidDateTime >= startOfThisMonth);

			this.SalesUsrValueLabel.Visible = !this.SalesUsrDropDownList.Visible;
												//!((Usr.Current.IsSuperAdmin || CurrentInvoice.SalesUsrK == Usr.Current.K) 
												//&& (CurrentInvoice.PaidDateTime.Equals(DateTime.MinValue) || CurrentInvoice.PaidDateTime >= startOfThisMonth));
			this.SalesAmountLabel.Visible = (CurrentInvoice.K > 0 && CurrentInvoice.SalesUsrAmount > 0) 
											|| (Usr.Current.IsSuperAdmin && (CurrentInvoice.PaidDateTime.Equals(DateTime.MinValue) || CurrentInvoice.PaidDateTime >= startOfThisMonth));
			this.SalesAmountTextBox.Visible = this.SalesAmountLabel.Visible;

			this.SalesUsrLabel.Visible = this.SalesUsrDropDownList.Visible || (this.SalesUsrValueLabel.Visible && this.SalesUsrValueLabel.Text.Length > 0);			
		}

		/// <summary>
		/// Disables validators for non editable controls that have been disabled for saved invoices
		/// </summary>
		private void DisableValidatorsForNonEditableControls()
		{
			//this.PromoterRequiredFieldValidator.Enabled = false;
			//this.UserRequiredFieldValidator.Enabled = false;
			this.PromoterAndUserCustomValidator.Enabled = false;
			this.ActionUserRequiredFieldValidator.Enabled = false;
			this.DueDateCustomValidator.Enabled = false;
		}

		/// <summary>
		/// Hides empty non-editable fields for unsaved invoices.  Shows these fields once they are populated by saving the invoice.
		/// </summary>
		/// <param name="isSavedInvoice"></param>
		private void ShowHideControls(bool isSavedInvoice)
		{
			this.InvoiceKValueLabel.Visible = isSavedInvoice;
			this.InvoiceKLabel.Visible = isSavedInvoice;
			// Refund Button not visible 
			//this.RefundInvoiceButton.Visible = isSavedInvoice;
			this.CreatedDateLabel.Visible = isSavedInvoice;
			this.CreatedDateTextBox.Visible = isSavedInvoice;
			this.CreditsPanel.Visible = isSavedInvoice;

			this.DownloadButton.Visible = isSavedInvoice;			
		}

		/// <summary>
		/// Sets the Paid image for the invoice being displayed
		/// </summary>
		/// <param name="isPaid"></param>
		private void SetPaidImage(bool isPaid)
		{
			this.PaidImage.Visible = isPaid;
			this.NotPaidImage.Visible = !isPaid;
		}

		/// <summary>
		/// Loads screen controls with data from a saved invoice 
		/// </summary>
        private void LoadScreenFromInvoice()
        {
            if (CurrentInvoice.K != 0)
            {
				this.InvoiceKValueLabel.Text = Utilities.LinkNewWindow(CurrentInvoice.UrlReport(), CurrentInvoice.K.ToString());
				this.InvoiceKLabel.Visible = true;

				if (CurrentInvoice.Promoter != null)
				{
					this.uiPromotersAutoComplete.Text = CurrentInvoice.Promoter.Name;
					this.uiPromotersAutoComplete.Value = CurrentInvoice.Promoter.K.ToString();
					this.PromoterValueLabel.Text = CurrentInvoice.Promoter.Link();
					//this.PromoterAvailableFundsLabel.Text = "<br><nobr>Available funds: " + CurrentInvoice.Promoter.GetAvailableFunds().ToString("c") + "</nobr>";
					SetupUserDropDownList();

				}
				//else
				//    this.PromoterAvailableFundsLabel.Text = "";
                if (CurrentInvoice.Usr != null)
                {
					this.uiUsersAutoComplete.Text = CurrentInvoice.Usr.NickName;
					this.uiUsersAutoComplete.Value = CurrentInvoice.Usr.K.ToString();
					this.UserDropDownList.SelectedValue = CurrentInvoice.Usr.K.ToString();
					this.UserValueLabel.Text = CurrentInvoice.Usr.Link();
                }
				if (CurrentInvoice.ActionUsr != null)
				{
					this.uiActionUserAutoComplete.Text = CurrentInvoice.ActionUsr.Name;
					this.uiActionUserAutoComplete.Value = CurrentInvoice.ActionUsr.K.ToString();
					this.ActionUserValueLabel.Text = CurrentInvoice.ActionUsr.Link();
				}
				else if (CurrentInvoice.Usr != null)
				{
					this.uiActionUserAutoComplete.Text = CurrentInvoice.Usr.Name;
					this.uiActionUserAutoComplete.Value = CurrentInvoice.Usr.K.ToString();
					this.ActionUserValueLabel.Text = CurrentInvoice.Usr.Link();
				}
				this.PaidCheckBox.Checked = CurrentInvoice.Paid;
				this.SetPaidImage(CurrentInvoice.Paid);
                this.VatCodeDropDownList.SelectedValue = Convert.ToInt32(CurrentInvoice.VatCode).ToString();
				this.VatCodeTextBox.Text = CurrentInvoice.VatCode.ToString();
				this.SalesUsrDropDownList.SelectedValue = CurrentInvoice.SalesUsrK.ToString();
				this.SalesAmountTextBox.Text = CurrentInvoice.SalesUsrAmount.ToString("c");
				this.PurchaseOrderNumberTextBox.Text = CurrentInvoice.PurchaseOrderNumber;
				if (CurrentInvoice.SalesUsr != null)
					this.SalesUsrValueLabel.Text = CurrentInvoice.SalesUsr.Link();
				else
					this.SalesUsrValueLabel.Text = "";

				this.CreatedDateTextBox.Text = CurrentInvoice.CreatedDateTime.ToString("HH:mm dd/MM/yy");

				this.DueDateCal.Date = CurrentInvoice.DueDateTime;
				if (!CurrentInvoice.DueDateTime.Equals(DateTime.MinValue))
					this.DueDateValueLabel.Text = CurrentInvoice.DueDateTime.ToString("dd/MM/yy");
				else
					this.DueDateValueLabel.Text = "";

				this.TaxDateCal.Date = CurrentInvoice.TaxDateTime;
				if (!CurrentInvoice.TaxDateTime.Equals(DateTime.MinValue))
					this.TaxDateValueLabel.Text = CurrentInvoice.TaxDateTime.ToString("dd/MM/yy");
				else
					this.TaxDateValueLabel.Text = "";
                
                // DateTime.MinValue is the equivalent of NULL for PaidDateTime
				if (CurrentInvoice.PaidDateTime == DateTime.MinValue)
				{
					this.PaidDateTextBox.Text = "";
					this.PaidDateLabel.Visible = false;
				}
				else
				{
					this.PaidDateTextBox.Text = CurrentInvoice.PaidDateTime.ToString("HH:mm dd/MM/yy");
					this.PaidDateLabel.Visible = true;
				}
                this.PriceTextBox.Text = CurrentInvoice.Price.ToString("c");
                this.VATTextBox.Text = CurrentInvoice.Vat.ToString("c");
                this.TotalTextBox.Text = CurrentInvoice.Total.ToString("c");
				this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text = CurrentInvoice.Notes;

				this.CreateTransferHyperLink.NavigateUrl = CurrentInvoice.UrlAdminCreateTransfer();
            }
        }

		/// <summary>
		/// Retrieves data from the screen and loads into the CurrentInvoice BOB
		/// </summary>
        private void LoadInvoiceFromScreen()
        {
			CurrentInvoice.Notes = this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text;
			if (this.NotesAddOnlyTextBox.AddTextBox.Text.Trim().Length > 0)
			{
				CurrentInvoice.AddNote(this.NotesAddOnlyTextBox.AddTextBox.Text.Trim(), Usr.Current.NickName);
			}

			// Set only for new invoices
			if (CurrentInvoice.K == 0 || InvoiceK == 0)
			{
				if (this.uiPromotersAutoComplete.Value.Length > 0)
				{
					CurrentInvoice.PromoterK = Convert.ToInt32(this.uiPromotersAutoComplete.Value);
					if (this.UserDropDownList.SelectedValue.Length > 0)
						CurrentInvoice.UsrK = Convert.ToInt32(this.UserDropDownList.SelectedValue);
				}
				else if (this.uiUsersAutoComplete.Value.Length > 0)
				{
					CurrentInvoice.UsrK = Convert.ToInt32(this.uiUsersAutoComplete.Value);
				}
				if (this.uiActionUserAutoComplete.Value.Length > 0)
					CurrentInvoice.ActionUsrK = Convert.ToInt32(this.uiActionUserAutoComplete.Value);
				CurrentInvoice.Type = Invoice.Types.Invoice;

				CurrentInvoice.VatCode = (Invoice.VATCodes)Convert.ToInt32(this.VatCodeDropDownList.SelectedValue);

				CurrentInvoice.Price = Utilities.ConvertMoneyStringToDecimal(this.PriceTextBox.Text);
				CurrentInvoice.Vat = Utilities.ConvertMoneyStringToDecimal(this.VATTextBox.Text);
				CurrentInvoice.Total = Utilities.ConvertMoneyStringToDecimal(this.TotalTextBox.Text);

				CurrentInvoice.CreatedDateTime = DateTime.Now;
				if (this.TaxDateCal.Visible == true && this.TaxDateCal.Date > DateTime.MinValue)
				{
					if (DateTime.Today != Utilities.GetStartOfDay(TaxDateCal.Date))
					{
						CurrentInvoice.AddNote("User " + Usr.Current.NickName + " set tax date to " + TaxDateCal.Date.ToString("dd/MM/yy"), "System");
					}
					CurrentInvoice.TaxDateTime = this.TaxDateCal.Date;
				}
				else
					CurrentInvoice.TaxDateTime = CurrentInvoice.CreatedDateTime;

				if (this.OverrideDueDateCheckBox.Checked == true && this.DueDateCal.Date > DateTime.MinValue)
				{
					CurrentInvoice.AddNote("User " + Usr.Current.NickName + " set due date to " + DueDateCal.Date.ToString("dd/MM/yy"), "System");
					CurrentInvoice.DueDateTime = this.DueDateCal.Date;
				}
				else if (CurrentInvoice.Promoter != null)
					CurrentInvoice.DueDateTime = CurrentInvoice.TaxDateTime.AddDays(CurrentInvoice.Promoter.InvoiceDueDaysEffective);
				else
					CurrentInvoice.DueDateTime = CurrentInvoice.TaxDateTime;

				if (this.SalesAmountTextBox.Visible)
				{
					decimal standardSalesUsrAmount = 0;
					foreach (InvoiceItemDataHolder iidh in this.InvoiceItemDataHolderList)
					{
						if (iidh.DoesApplyToSalesUsrAmount)
							standardSalesUsrAmount += iidh.Price;
					}
					if (Math.Round(Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text), 2) != standardSalesUsrAmount)
					{
						CurrentInvoice.AddNote("User " + Usr.Current.NickName + " set sales user amount to " + Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text).ToString("c"), "System");
					}
					CurrentInvoice.SalesUsrAmount = Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text);
				}
				else
					CurrentInvoice.SalesUsrAmount = CurrentInvoice.Price;

				if (CurrentPromoter != null && CurrentPromoter.SalesUsrK != Convert.ToInt32(this.SalesUsrDropDownList.SelectedValue))
					CurrentInvoice.AddNote("User " + Usr.Current.NickName + " set sales user to " + this.SalesUsrDropDownList.SelectedItem.Text, "System");
			}
			else
			{
				if (Utilities.GetStartOfDay(CurrentInvoice.TaxDateTime) != Utilities.GetStartOfDay(TaxDateCal.Date))
				{
					CurrentInvoice.AddNote("User " + Usr.Current.NickName + " changed tax date from " + CurrentInvoice.TaxDateTime.ToString("dd/MM/yy") + " to " + this.TaxDateCal.Date.ToString("dd/MM/yy"), "System");
					CurrentInvoice.TaxDateTime = this.TaxDateCal.Date;
				}

				if (Math.Round(Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text), 2) != Math.Round(CurrentInvoice.SalesUsrAmount, 2))
				{
					CurrentInvoice.AddNote("User " + Usr.Current.NickName + " changed sales user amount from " + CurrentInvoice.SalesUsrAmount.ToString("c") + " to " + Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text).ToString("c"), "System");
					CurrentInvoice.SalesUsrAmount = Math.Round(Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text), 2);
				}
				CurrentInvoice.SalesUsrAmount = Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text);
				if (CurrentInvoice.SalesUsrK != Convert.ToInt32(this.SalesUsrDropDownList.SelectedValue))
					CurrentInvoice.AddNote("User " + Usr.Current.NickName + " changed sales user to " + this.SalesUsrDropDownList.SelectedItem.Text, "System");
			}

			if(CurrentInvoice.PurchaseOrderNumber.Length > 0 && CurrentInvoice.PurchaseOrderNumber != PurchaseOrderNumberTextBox.Text)
				CurrentInvoice.AddNote("User " + Usr.Current.NickName + " changed purchase order # from " + CurrentInvoice.PurchaseOrderNumber + " to " + PurchaseOrderNumberTextBox.Text, "System");
			CurrentInvoice.PurchaseOrderNumber = PurchaseOrderNumberTextBox.Text;

			// If Invoice was not paid until now, set PaidDateTime to DateTime.Now.  If it was already set to Paid, then do not update PaidDateTime
			CurrentInvoice.SetPaidAndPaidDateTime(this.PaidCheckBox.Checked);
			  
			CurrentInvoice.SalesUsrK = Convert.ToInt32(this.SalesUsrDropDownList.SelectedValue);			

			CurrentInvoice.DuplicateGuid = (Guid)ViewState["DuplicateGuid"];
        }

		/// <summary>
		/// Loads InvoiceItem grid, InvoiceTransfer grid, and InvoiceCredit grid
		/// </summary>
        private void InvoiceAndSubItemsBindData()
        {
            if(!this.IsPostBack)
            {
				if (InvoiceK > 0)
				{
					CurrentInvoice = new Invoice(InvoiceK);
					if(CurrentInvoice.Type.Equals(Invoice.Types.Credit))
						Response.Redirect(CurrentInvoice.UrlAdmin());
				}
				else
					CurrentInvoice = new Invoice();
            }

			BindInvoiceItemGridView();

            // Invoice Transfer GridView loading
            if (InvoiceTransferDataHolderList.Count == 0)
            {
                this.TransfersPanel.Visible = !CurrentInvoice.Paid;
                InvoiceTransferDataHolderList.Add(null);
            }
            InvoiceTransferGridView.DataSource = InvoiceTransferDataHolderList;
            InvoiceTransferGridView.DataBind();


			if (InvoiceCreditDataHolderList.Count == 0)
				this.CreditsPanel.Visible = false;
			else
			{
				this.CreditsPanel.Visible = true;
				InvoiceCreditGridView.DataSource = InvoiceCreditDataHolderList;
				InvoiceCreditGridView.DataBind();
			}

			if (InvoiceK > 0)
			{
				//this.InvoiceTransferGridView.Visible = this.InvoiceTransferDataHolderList.Count > 0;

				// hide the update / delete column
				this.InvoiceItemsGridView.Columns[InvoiceItemsGridView.Columns.Count - 1].Visible = false;

				// Hide the add new footer row
				this.InvoiceItemsGridView.FooterRow.Visible = false;
			}

            SetupAvailableTransfersDropDownList();
		}

		private void BindInvoiceItemGridView()
		{
			// Invoice Item GridView loading
			if (InvoiceItemDataHolderList.Count == 0)
				InvoiceItemDataHolderList.Add(null);
			InvoiceItemsGridView.DataSource = InvoiceItemDataHolderList;
			InvoiceItemsGridView.DataBind();

			DropDownList newVatCodeDropDownList = (DropDownList)InvoiceItemsGridView.FooterRow.FindControl("NewVatCodeDropDownList");
			newVatCodeDropDownList.Items.Clear();
			Utilities.AddEnumValuesToDropDownList(newVatCodeDropDownList, typeof(InvoiceItem.VATCodes));
			newVatCodeDropDownList.SelectedIndex = 1;

			DropDownList newTypeDropDownList = (DropDownList)InvoiceItemsGridView.FooterRow.FindControl("NewTypeDropDownList");
			newTypeDropDownList.Items.Clear();
			newTypeDropDownList.Items.AddRange(InvoiceItem.TypesAsListItemArray());

			newVatCodeDropDownList.SelectedIndex = 1;
		}

		/// <summary>
		/// Recalculate all VAT and Totals
		/// </summary> 
        public void CalculateInvoiceItemVatAndTotals()
        {
            //double InvoiceVATRate = Invoice.VATRate((Invoice.VATCodes)Convert.ToInt32(this.VatCodeDropDownList.SelectedValue));
			decimal invoicePrice = 0;
			decimal invoiceVAT = 0;
			decimal invoiceTotal = 0;
			decimal salesUsrAmount = 0;

            //double InvoiceItemVATRate = 0;
            foreach (InvoiceItemDataHolder iidh in InvoiceItemDataHolderList)
            {
				decimal total = iidh.Total;
				//InvoiceItemVATRate = InvoiceVATRate <= InvoiceItem.VATRate(iidh.VatCode) ? InvoiceVATRate : InvoiceItem.VATRate(iidh.VatCode);
				iidh.InvoiceVatCode = (Invoice.VATCodes)Convert.ToInt32(this.VatCodeDropDownList.SelectedValue);
				iidh.SetTotal(total);
				//iidh.Price = Math.Round(iidh.Price, 2);
				//if (iidh.K > 0)
				//    iidh.Vat = Math.Round(iidh.Vat, 2);
				//else
				//    iidh.Vat = Math.Round(iidh.Price * InvoiceItemVATRate, 2);
				//iidh.Total = Math.Round(iidh.Price + iidh.Vat, 2);

                invoicePrice += iidh.Price;
                invoiceVAT += iidh.Vat;
                invoiceTotal += iidh.Total;

				if(iidh.DoesApplyToSalesUsrAmount)
					salesUsrAmount += iidh.Price;
            }
            this.PriceTextBox.Text = invoicePrice.ToString("c");
            this.VATTextBox.Text = invoiceVAT.ToString("c");
            this.TotalTextBox.Text = invoiceTotal.ToString("c");

			if (InvoiceK == 0)
			{
				this.SalesAmountTextBox.Text = salesUsrAmount.ToString("c");
			}

            //this.InvoiceItemsGridView.DataSource = this.InvoiceItemDataHolderList;
            //this.InvoiceItemsGridView.DataBind();

            decimal amountPaid = 0;
            DateTime lastTransferDate = DateTime.MinValue;

			foreach (InvoiceTransferDataHolder itdh in InvoiceTransferDataHolderList)
			{
				if (new Transfer(itdh.TransferK).Status.Equals(Transfer.StatusEnum.Success))
				{
					amountPaid += itdh.Amount;
					DateTime invoiceTransferCompletedDate = new Transfer(itdh.TransferK).DateTimeComplete;
					if (lastTransferDate <= invoiceTransferCompletedDate)
						lastTransferDate = invoiceTransferCompletedDate;
				}
			}

			foreach (InvoiceCreditDataHolder icdh in InvoiceCreditDataHolderList)
			{
				// Credit amounts will be negative
				amountPaid -= icdh.Amount;
				if (icdh.CreditK != 0)
				{
					amountPaid += new Invoice(icdh.CreditK).AmountPaid;
				}
				if (lastTransferDate <= icdh.CreatedDateTime)
					lastTransferDate = icdh.CreatedDateTime;
			}

			if (!CurrentInvoice.PaidDateTime.Equals(DateTime.MinValue))
				lastTransferDate = CurrentInvoice.PaidDateTime;

			if (Math.Round(invoiceTotal, 2) > Math.Round(amountPaid, 2) || lastTransferDate.Equals(DateTime.MinValue))
            {
                this.PaidCheckBox.Checked = false;
				this.SetPaidImage(false);
				this.PaidDateLabel.Visible = false;
            }
			else if (Math.Round(invoiceTotal, 2) == Math.Round(amountPaid, 2))
            {
				this.PaidCheckBox.Checked = true;
				this.SetPaidImage(true);
				this.PaidDateTextBox.Text = lastTransferDate.ToString("HH:mm dd/MM/yy");
				this.PaidDateLabel.Visible = true;
				ProcessingVal.IsValid = true;
            }
            else
            {
				this.PaidCheckBox.Checked = true;
				this.SetPaidImage(true);
				this.PaidDateTextBox.Text = lastTransferDate.ToString("HH:mm dd/MM/yy");
				this.PaidDateLabel.Visible = true;
				ProcessingVal.ErrorMessage = "Transfer totals (" + amountPaid.ToString("c") + ") are greater than the total invoice amount (" + invoiceTotal.ToString("c") + ").";
				ProcessingVal.IsValid = false;
            }

			this.AmountDueTextBox.Text = ((double)(invoiceTotal - amountPaid)).ToString("c");

            InvoiceAndSubItemsBindData();
		}

		private void SetPromoterAvailableFunds()
		{
			this.PromoterAvailableFundsLabel.Text = "";
			try
			{
				if (this.CurrentPromoter != null)
					this.PromoterAvailableFundsLabel.Text = "<br><nobr>Available funds: " + CurrentPromoter.GetAvailableFunds().ToString("c") + "</nobr>";
			}
			catch (Exception)
			{ }
		}

		protected string InvoiceItemTypeToString(object o)
		{
			string result = "";

			if (o is InvoiceItemDataHolder)
			{
				result = Utilities.CamelCaseToString(((InvoiceItemDataHolder)o).Type.ToString());
			}

			return result;
		}
		#endregion

		#region Setup DropDownLists
		private void SetupVatCodeDropDownLists()
        {
			this.VatCodeDropDownList.Items.Clear();
            Utilities.AddEnumValuesToDropDownList(this.VatCodeDropDownList, typeof(Invoice.VATCodes));

			VatCodeDropDownList.SelectedIndex = 0;
        }

		private void SetupUserDropDownList()
		{
			this.UserDropDownList.Items.Clear();
			if (this.uiPromotersAutoComplete.Value.Length > 0)
			{
				if (CurrentPromoter != null)
				{
					foreach (Usr promoterUser in CurrentPromoter.AdminUsrs)
					{
						this.UserDropDownList.Items.Add(new ListItem(promoterUser.Name, promoterUser.K.ToString()));
					}

					if(CurrentPromoter.PrimaryUsrK != 0)
						this.UserDropDownList.SelectedValue = CurrentPromoter.PrimaryUsrK.ToString();
				}
			}
			else
			{
				if(CurrentInvoice.Usr != null)
					this.UserDropDownList.Items.Add(new ListItem(CurrentInvoice.Usr.Name, CurrentInvoice.Usr.K.ToString()));
			}
		}

		private void SetupSalesUsrDropDownList()
		{
			UsrSet salesUsrs;
			if(InvoiceK == 0)
				salesUsrs = Usr.GetCurrentSalesUsrsNameAndK();
			else  // Get previous sales usrs so you dont automatically remove people who are no longer sales usrs from previously saved invoices.
				salesUsrs = Usr.GetCurrentAndPreviousSalesUsrsNameAndK();

			this.SalesUsrDropDownList.Items.Clear();
			this.SalesUsrDropDownList.Items.Add(new ListItem("NONE", "0"));
			foreach (Usr salesUsr in salesUsrs)
			{
				this.SalesUsrDropDownList.Items.Add(new ListItem(salesUsr.NickName, salesUsr.K.ToString()));
			}
		}

        private void SetupAvailableTransfersDropDownList()
        {
			if (this.InvoiceK == 0 || CurrentInvoice.Paid == false)
			{
				LinkButton newTransferLinkButton = (LinkButton)InvoiceTransferGridView.FooterRow.FindControl("AddLinkButton");
				TextBox newAmountTextBox = (TextBox)InvoiceTransferGridView.FooterRow.FindControl("NewAmountTextBox");
				newTransferLinkButton.Visible = true;

				Q transfersForPromotersUsersQueryConditions = null;
				//if(PromoterDbCombo.Value != "" && UserDbCombo.Value != "")
				//    transfersForPromotersUsersQueryConditions = new Or(new Q(Transfer.Columns.PromoterK, Convert.ToInt32(PromoterDbCombo.Value)), new Q(Transfer.Columns.UsrK, Convert.ToInt32(UserDbCombo.Value)));
				//else if(PromoterDbCombo.Value != "")
				//    transfersForPromotersUsersQueryConditions = new Q(Transfer.Columns.PromoterK, Convert.ToInt32(PromoterDbCombo.Value));
				//else if(UserDbCombo.Value != "")
				//    transfersForPromotersUsersQueryConditions = new Q(Transfer.Columns.UsrK, Convert.ToInt32(UserDbCombo.Value));

				if (this.uiPromotersAutoComplete.Value != "")
					transfersForPromotersUsersQueryConditions = new Q(Transfer.Columns.PromoterK, Convert.ToInt32(this.uiPromotersAutoComplete.Value));


				Query availableTransfersQuery = new Query();

				string queryForAvailableTransfers = @"Round([Transfer].[Amount],2) > (CASE WHEN ((SELECT Sum([InvoiceTransfer].[Amount]) FROM [InvoiceTransfer] WHERE [InvoiceTransfer].[TransferK] = [Transfer].[K]) IS NULL) THEN 0 ELSE Round((SELECT Sum([InvoiceTransfer].[Amount]) FROM [InvoiceTransfer] WHERE [InvoiceTransfer].[TransferK] = [Transfer].[K]),2) END)";

				List<ListItem> availableTransferListItems = new List<ListItem>();

				decimal promoterAvailableCredit = 0;

				if (this.uiPromotersAutoComplete.Value.Length > 0)
				{
					promoterAvailableCredit = CurrentPromoter.GetAvailableMoney() + CurrentPromoter.CreditLimit;
				}
				if (transfersForPromotersUsersQueryConditions != null)
				{
					availableTransfersQuery.QueryCondition = new And(
						new StringQueryCondition(queryForAvailableTransfers),
						transfersForPromotersUsersQueryConditions,
						new Q(Transfer.Columns.Status, QueryOperator.NotEqualTo, Transfer.StatusEnum.Cancelled),
						new Q(Transfer.Columns.Status, QueryOperator.NotEqualTo, Transfer.StatusEnum.Failed),
						new Q(Transfer.Columns.Type, Transfer.TransferTypes.Payment),
						new Q(Transfer.Columns.IsFullyApplied, false));

					availableTransfersQuery.Columns = new ColumnSet(Transfer.Columns.K, Transfer.Columns.Amount, Transfer.Columns.Method, Transfer.Columns.DateTimeCreated, Transfer.Columns.Status);

					//TransferSet availableTransfers = new TransferSet();

					//if (promoterAvailableMoney > 0)
					//{
					TransferSet availableTransfers = new TransferSet(availableTransfersQuery);
					//}

					decimal pendingTransferMoney = 0;

					for (int i = 0; i < availableTransfers.Count; i++)
					{
						decimal amountRemaining = availableTransfers[i].AmountRemaining();
						string pending = availableTransfers[i].Status.Equals(Transfer.StatusEnum.Pending) ? " (P)" : "";
						
						if (amountRemaining > 0)
						{
							availableTransferListItems.Add(new ListItem("K=" + availableTransfers[i].K.ToString() + " | "
																		+ amountRemaining.ToString("c") + " | "
																		+ Utilities.CamelCaseToString(availableTransfers[i].Method.ToString()) + " | "
																		+ availableTransfers[i].DateTimeCreated.ToString("dd/MM/yy") + pending,
																		availableTransfers[i].K.ToString()));
							if (availableTransfers[i].Status.Equals(Transfer.StatusEnum.Pending))
								pendingTransferMoney += amountRemaining;
						}
					}
					newTransferLinkButton.Visible = true;
					newAmountTextBox.Visible = true;

					if (availableTransferListItems.Count == 0)
					{
						availableTransferListItems.Clear();
						availableTransferListItems.Add(new ListItem("<NO AVAILABLE TRANSFERS>"));
						newTransferLinkButton.Visible = false;
						newAmountTextBox.Visible = false;
					}
				}
				else
				{
					availableTransferListItems.Add(new ListItem("<SELECT PROMOTER FIRST>"));
					newTransferLinkButton.Visible = false;
					newAmountTextBox.Visible = false;
				}

				DropDownList newTransferKDropDownList = (DropDownList)InvoiceTransferGridView.FooterRow.FindControl("NewTransferKDropDownList");
				newTransferKDropDownList.DataSource = availableTransferListItems.ToArray();
				newTransferKDropDownList.DataTextField = "Text";
				newTransferKDropDownList.DataValueField = "Value";
				newTransferKDropDownList.DataBind();
			}
        }

		private void SetupUnprocessedBanners()
		{
			this.AddBannerInvoiceItemRow.Visible = false;

			this.AddBannerInvoiceItemDropDownList.Items.Clear();

			if (CurrentPromoter != null && CurrentPromoter.K > 0)
			{
				Query unprocessedBannerQuery = new Query(new And(new Q(Banner.Columns.PromoterK, CurrentPromoter.K),
																 new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, DateTime.Today),
																 new Q(Banner.Columns.StatusBooked, false)));

				foreach (InvoiceItemDataHolder iidh in this.InvoiceItemDataHolderList)
				{
					if (iidh.BuyableObjectK > 0 && iidh.BuyableObjectType == Model.Entities.ObjectType.Banner)
						unprocessedBannerQuery.QueryCondition = new And(unprocessedBannerQuery.QueryCondition,
																		new Q(Banner.Columns.K, QueryOperator.NotEqualTo, iidh.BuyableObjectK));
				}

				unprocessedBannerQuery.Columns = new ColumnSet(Banner.Columns.K, Banner.Columns.Name, Banner.Columns.Position);

				BannerSet unprocessedBanners = new BannerSet(unprocessedBannerQuery);

				if (unprocessedBanners.Count > 0)
				{
					this.AddBannerInvoiceItemRow.Visible = true;

					foreach (Banner banner in unprocessedBanners)
					{
						this.AddBannerInvoiceItemDropDownList.Items.Add(
							new ListItem("K=" + banner.K.ToString() + " | " + Utilities.CamelCaseToString(banner.Position.ToString()) + " | " + banner.Name, banner.K.ToString()));
					}
				}
			}
		}

		private void SetupInvoiceItemDropDownLists()
		{
			if (InvoiceItemsGridView.EditIndex >= 0)
			{
				if (InvoiceItemsGridView.Rows[InvoiceItemsGridView.EditIndex].FindControl("EditVatCodeDropDownList") != null)
				{
					DropDownList editVatCodeDropDownList = (DropDownList)InvoiceItemsGridView.Rows[InvoiceItemsGridView.EditIndex].FindControl("EditVatCodeDropDownList");
					editVatCodeDropDownList.Items.Clear();
                    Utilities.AddEnumValuesToDropDownList(editVatCodeDropDownList, typeof(InvoiceItem.VATCodes));
                    editVatCodeDropDownList.SelectedIndex = 1;
					// Note: this works only when paging is turned off
					editVatCodeDropDownList.SelectedValue = Convert.ToInt32(((List<InvoiceItemDataHolder>)InvoiceItemsGridView.DataSource)[InvoiceItemsGridView.EditIndex].VatCode).ToString();
				}
				if (InvoiceItemsGridView.Rows[InvoiceItemsGridView.EditIndex].FindControl("EditTypeDropDownList") != null)
				{
					DropDownList editTypeDropDownList = (DropDownList)InvoiceItemsGridView.Rows[InvoiceItemsGridView.EditIndex].FindControl("EditTypeDropDownList");
					editTypeDropDownList.Items.Clear();
					editTypeDropDownList.Items.AddRange(InvoiceItem.TypesAsListItemArray());
					// Note: this works only when paging is turned off
					editTypeDropDownList.SelectedValue = Convert.ToInt32(((List<InvoiceItemDataHolder>)InvoiceItemsGridView.DataSource)[InvoiceItemsGridView.EditIndex].Type).ToString();
				}
			}
		}
        #endregion

        #region Page Event Handlers
		protected void uiPromotersAutoComplete_ValueChanged(object sender, EventArgs e)
        {
            // Promoter selection should only occur for new invoices before it is saved
			if (InvoiceK == 0)
			{
				CalculateInvoiceItemVatAndTotals();
				SetupInvoiceItemDropDownLists();
				SetupAvailableTransfersDropDownList();
				SetupUnprocessedBanners();
				if (this.uiPromotersAutoComplete.Value.Length > 0)
				{
					this.uiUsersAutoComplete.Visible = false;
					this.UserDropDownList.Visible = true;
				}
				else
				{
					this.uiUsersAutoComplete.Visible = true;
					this.UserDropDownList.Visible = false;
				}
			}
			else
			{
				if (this.DueDateCal.Date.Equals(DateTime.MinValue))
				{
					// If Due Date has not yet been set, then set it to default number of days from Promoter account
					if (this.uiPromotersAutoComplete.Value.Length > 0)
					{
						int promoterInvoiceDueDays = CurrentPromoter.InvoiceDueDays;
						if (promoterInvoiceDueDays == 0)
							promoterInvoiceDueDays = Vars.InvoiceDueDaysDefault;
						DueDateCal.Date = DateTime.Now.AddDays(promoterInvoiceDueDays);
					}
				}
			}

			if (this.uiPromotersAutoComplete.Value.Length > 0 && Convert.ToInt32(this.uiPromotersAutoComplete.Value) > 0)
			{
				if (CurrentPromoter.IsAgency)
				{
					uiAgencyDiscountTextBox.Text = "15%";
				}
				else
				{
					uiAgencyDiscountTextBox.Text = "0%";
				}
				
				if (CurrentPromoter != null && CurrentPromoter.SalesUsrK > 0)
				{
					try
					{
						this.SalesUsrDropDownList.SelectedValue = CurrentPromoter.SalesUsrK.ToString();
						this.SalesUsrValueLabel.Text = CurrentPromoter.SalesUsr.Link();
					}
					catch (Exception)
					{
						this.SalesUsrDropDownList.SelectedIndex = 0;
						this.SalesUsrValueLabel.Text = "";
					}
					EnableSalesUsrAndAmount();
				}
				else
				{
					this.SalesUsrDropDownList.SelectedIndex = 0;
					this.SalesUsrValueLabel.Text = "";
				}
			}
			else
			{
				//this.PromoterAvailableFundsLabel.Text = "";
				CurrentPromoter = null;
				this.SalesUsrDropDownList.SelectedIndex = 0;
				this.SalesUsrValueLabel.Text = "";
			}
			SetAgencyDiscountForInvoiceItems();
			SetupUserDropDownList();
			SetPromoterAvailableFunds();
        }

		protected void CreditInvoiceButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(CurrentInvoice.UrlAdminCreditMe());
		}

        protected void VatCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateInvoiceItemVatAndTotals();
			SetupInvoiceItemDropDownLists();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
			Page.Validate("");
			if (Page.IsValid)
			{
				// Validation
				if (this.OverrideDueDateCheckBox.Checked == true && !this.DueDateCal.ValidateNow())
				{
					this.DueDateCustomValidator.IsValid = false;
					return;
				}
				if (this.OverrideTaxDateCheckBox.Checked == true && !this.TaxDateCal.ValidateNow())
				{
					this.TaxDateCustomValidator.IsValid = false;
					return;
				}
				if (this.InvoiceItemsGridView.EditIndex >= 0)
				{
					this.NoEditCustomVal.ErrorMessage = "Invoice item must be saved.";
					this.NoEditCustomVal.IsValid = false;
					return;
				}
				else if (this.InvoiceTransferGridView.EditIndex >= 0)
				{
					this.NoEditCustomVal.ErrorMessage = "Transfer must be saved.";
					this.NoEditCustomVal.IsValid = false;
					return;
				}
				else if (this.InvoiceCreditGridView.EditIndex >= 0)
				{
					this.NoEditCustomVal.ErrorMessage = "Credit must be saved.";
					this.NoEditCustomVal.IsValid = false;
					return;
				}
				
				bool succeeded = false;
				bool previouslyPaid = CurrentInvoice.Paid;

				//DateTime prevTaxDateTime = CurrentInvoice.TaxDateTime;

				LoadInvoiceFromScreen();

				//if (CurrentInvoice.K > 0 && Utilities.GetStartOfDay(prevTaxDateTime) != Utilities.GetStartOfDay(CurrentInvoice.TaxDateTime))
				//{
				//    this.TaxDateCustomValidator.IsValid = false;
				//    return;
				//}

				try
				{
					if (!Invoice.DoesDuplicateGuidExistInDb((Guid)this.ViewState["DuplicateGuid"]) || CurrentInvoice.K > 0)
					{
						if (this.InvoiceItemDataHolderList.Count == 0)
							throw new Exception("Must have at least one invoice item!");
                        bool newInvoice = CurrentInvoice.K == 0;
						// if this is a new Invoice, then save first to get Invoice.K from DB
						if (this.InvoiceK == 0 || CurrentInvoice.K == 0)
						{
                            if (CurrentInvoice.Total < 0)
                                throw new Exception("Cannot have invoice of negative amount: " + CurrentInvoice.Total.ToString("c"));

							if (CurrentInvoice.Promoter != null)
							{
								decimal promoterCreditBalance = CurrentInvoice.Promoter.CreditLimit + CurrentInvoice.Promoter.GetBalance();

								if (promoterCreditBalance >= CurrentInvoice.Total)
								{
									CurrentInvoice.UpdatePromoterStatusAndSalesStatus();
									CurrentInvoice.Update();
								}
								else
								{
									throw new Exception("This promoter has insufficient credit balance.\nCurrent credit balance = " + promoterCreditBalance.ToString("c") + ".\nClick Create Transfer to add money to this promoter account");
								}
							}
							else if(CurrentInvoice.Usr != null)
							{
								decimal userCreditBalance = CurrentInvoice.Usr.GetBalance();

								if (userCreditBalance >= CurrentInvoice.Total)
								{
									CurrentInvoice.Update();
								}
								else
								{
									throw new Exception("This user has insufficient balance.\nCurrent balance = " + userCreditBalance.ToString("c") + ".\nClick Create Transfer to add money to this user account");
								}
							}
							else
							{
								throw new Exception("No account selected.");
							}
						}
						foreach (InvoiceItemDataHolder invoiceItemDataHolder in this.InvoiceItemDataHolderList)
						{
							if (CurrentInvoice.K > 0)
							{
								invoiceItemDataHolder.InvoiceK = CurrentInvoice.K;
								invoiceItemDataHolder.UpdateInsertDelete();
							}
						}
						foreach (InvoiceTransferDataHolder invoiceTransferDataHolder in this.InvoiceTransferDataHolderList)
						{
							if (CurrentInvoice.K > 0)
							{
								invoiceTransferDataHolder.InvoiceK = CurrentInvoice.K;
								invoiceTransferDataHolder.UpdateInsertDelete();
                                
                                if (newInvoice)
                                {
                                    Transfer transferApplied = new Transfer(invoiceTransferDataHolder.TransferK);
                                    //if (transferApplied.Method == Transfer.Methods.TicketSales)
                                    //{
                                    //    Bobs.BankExport.GenerateBankExportForTicketFundsUsed(transferApplied, invoiceTransferDataHolder.Amount, CurrentInvoice);
                                    //}
                                }
							}
						}
						foreach (InvoiceTransferDataHolder invoiceTransferDataHolderDelete in this.InvoiceTransferDataHolderDeleteList)
						{
							bool toBeDeleted = true;
							foreach (InvoiceTransferDataHolder invoiceTransferDataHolder in this.InvoiceTransferDataHolderList)
							{
								// Do not delete ones that are confirmed to be saved.  This resolves issues when saved items are marked for deletion, then added again.
								if (invoiceTransferDataHolder.TransferK == invoiceTransferDataHolderDelete.TransferK)
								{
									toBeDeleted = false;
									break;
								}
							}

							if (toBeDeleted == true)
							{
								invoiceTransferDataHolderDelete.State = DataHolderState.Deleted;
								invoiceTransferDataHolderDelete.UpdateInsertDelete();
							}
						}
                        CurrentInvoice.AssignBuyerType();
						CurrentInvoice.UpdateAndAutoApplySuccessfulTransfersWithAvailableMoney();						
						
						succeeded = true;
					}
					// Do not process if duplicate exists. User probably tried refreshing the page.
					else
					{
						throw new Exception("Duplicate invoice already exists in the database.");
					}					
				}

				catch (Exception ex)
				{
					// Display error message
					ProcessingVal.ErrorMessage = ex.Message;
					ProcessingVal.IsValid = false;
				}

				// Having Server.Transfer or Response.Redirect in Try{} throws an abort thread exception.
				if (succeeded == true)
				{
					// Send email to promoter and to DSI accounts
					bool invoiceCreated = false;
					if (InvoiceK == 0)
						invoiceCreated = true;

					// Only send out emails when invoice is created or when Paid status changes
					if(invoiceCreated || previouslyPaid != CurrentInvoice.Paid)
						Utilities.EmailInvoice(CurrentInvoice, invoiceCreated);

					string response = "<script type=\"text/javascript\">alert('Invoice #" + CurrentInvoice.K.ToString() + " saved successfully'); open('" + CurrentInvoice.UrlAdmin() + "?" + Cambro.Misc.Utility.GenRandomText(5) + "', '_self' );</script>";
					ViewState["DuplicateGuid"] = Guid.NewGuid();
					Response.Write(response);
					Response.End();
				}
			}
        }

		protected void uiAgencyDiscountLabel_TextChanged(object sender, EventArgs e)
		{
			SetAgencyDiscountForInvoiceItems();
			CalculateInvoiceItemVatAndTotals();
			
		}

		private void SetAgencyDiscountForInvoiceItems()
		{
			foreach (InvoiceItemDataHolder iidh in InvoiceItemDataHolderList)
			{
				iidh.AgencyDiscount = Utilities.ConvertPercentageStringToDouble(this.uiAgencyDiscountTextBox.Text);
			}
			BindInvoiceItemGridView();
		}

        protected void CancelButton_Click(object sender, EventArgs e)
        {
			if (CurrentInvoice.K > 0)
			{
				Response.Redirect(CurrentInvoice.UrlAdmin());
			}
			else
			{
				Response.Redirect(Invoice.UrlAdminNewInvoice());
			}
        }

		protected void DownloadButton_Click(object sender, EventArgs e)
		{
			if (CurrentInvoice.K > 0)
			{
				Utilities.DownloadAsWord(CurrentInvoice, "DontStayIn " + CurrentInvoice.TypeToString + " #" + CurrentInvoice.K.ToString() + ".doc", Response);
			}
			else
			{
				this.ProcessingVal.IsValid = false;
				this.ProcessingVal.ErrorMessage = "Can only download a saved invoice.";
			}
		}

		protected void RefundInvoiceButton_Click(object sender, EventArgs e)
		{
			//bool success = false;
			//Invoice credit = new Invoice();

			//try
			//{
			//    if ((CurrentInvoice == null || CurrentInvoice.K == 0) && this.InvoiceK > 0)
			//        CurrentInvoice = new Invoice(this.InvoiceK);

			//    if (!DoesDuplicateTransferGuidExistInDb())
			//    {
			//        Query refundTransferSetQuery = new Query(new Q(InvoiceTransfer.Columns.InvoiceK, this.InvoiceK));
			//        refundTransferSetQuery.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K, QueryJoinType.Inner);
			//        TransferSet refundTransferSet = new TransferSet(refundTransferSetQuery);
			//        InvoiceTransferSet refundInvoiceTransferSet = new InvoiceTransferSet(new Query(new Q(InvoiceTransfer.Columns.InvoiceK, this.InvoiceK)));
			//        InvoiceCreditSet invoiceCreditSet = new InvoiceCreditSet(new Query(new Q(InvoiceCredit.Columns.InvoiceK, this.InvoiceK)));
			//        if (invoiceCreditSet.Count == 0 && refundTransferSet.Count == 1 && refundTransferSet[0].Type.Equals(Transfer.TransferTypes.Payment)
			//            && refundTransferSet[0].Status.Equals(Transfer.Statuses.Success) && refundTransferSet[0].Method.Equals(Transfer.Methods.Card)
			//            && refundTransferSet[0].Amount >= CurrentInvoice.Total && refundInvoiceTransferSet[0].Amount == CurrentInvoice.Total)
			//        {
			//            InvoiceDataHolder creditDataHolder = CurrentInvoice.CreateCredit();
			//            credit = creditDataHolder.UpdateInsertDelete();
			//            InvoiceCredit invoiceCredit = new InvoiceCredit();
			//            invoiceCredit.InvoiceK = this.InvoiceK;
			//            invoiceCredit.CreditInvoiceK = credit.K;
			//            invoiceCredit.Amount = credit.Total;
			//            invoiceCredit.Update();

			//            SecPay secPay = new SecPay();
			//            secPay.MakeRefund(refundTransferSet[0], (Guid)ViewState["DuplicateGuid"], Usr.Current.K, CurrentInvoice.Total);
			//            InvoiceTransfer creditTransfer = new InvoiceTransfer();
			//            creditTransfer.InvoiceK = credit.K;
			//            creditTransfer.TransferK = secPay.Transfer.K;
			//            creditTransfer.Amount = secPay.Transfer.Amount;
			//            creditTransfer.Update();
			//            secPay.Transfer.IsFullyApplied = true;
			//            secPay.Transfer.Update();
			//            credit.UpdateAndSetPaidStatus();

			//            success = true;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    success = false;
			//    ProcessingVal.ErrorMessage = ex.Message;
			//    ProcessingVal.IsValid = false;
			//}
			//if (success == true)
			//{
			//    //Response.Redirect("/admin/creditscreen/K-" + credit.K.ToString());
			//}
		}

		protected void EmailButton_Click(object sender, EventArgs e)
		{
			//Page.Validate("EmailValidation");
			//if (Page.IsValid)
			//{
			    bool result = false;
			//    if (this.OverrideEmailRecipientsCheckBox.Checked)
					result = Utilities.EmailInvoice(CurrentInvoice, false);
				//else
				//    result = Utilities.EmailInvoice(CurrentInvoice, this.OverrideEmailRecipientTextBox.Text.Trim());
				EmailSentLabel.Visible = result;
				EmailFailedLabel.Visible = !result;
			//}
		}

		protected void AddBannerInvoiceItemButton_Click(object sender, EventArgs e)
		{
			try
			{
				//TODO
				throw new Exception("Removed by DaveB 19/08 - Hmmmmmmmmmmmmmmmmmmmm");

				//int bannerK = Convert.ToInt32(this.AddBannerInvoiceItemDropDownList.SelectedValue);
				//if (bannerK > 0)
				//{
				//    Banner banner = new Banner(bannerK);
				//    bool found = false;
				//    foreach (InvoiceItemDataHolder iidh in this.InvoiceItemDataHolderList)
				//    {
				//        if (iidh.BuyableObjectK == banner.K && iidh.BuyableObjectType == Model.Entities.ObjectType.Banner)
				//        {
				//            found = true;
				//            break;
				//        }
				//    }
				//    if (!found)
				//    {
				//        InvoiceItemDataHolder newBannerItem = new InvoiceItemDataHolder();
				//        newBannerItem.BuyableObjectK = banner.K;
				//        newBannerItem.BuyableObjectType = Model.Entities.ObjectType.Banner;
				//        newBannerItem.Description = banner.InvoiceItemDescription;
				//        newBannerItem.ItemProcessed = false;
				//        newBannerItem.KeyData = banner.K;
				//        newBannerItem.PriceBeforeDiscount = (Convert.ToDouble(banner.Price));
				//        newBannerItem.Type = banner.InvoiceItemType;
				//        newBannerItem.RevenueStartDate = banner.FirstDay;
				//        newBannerItem.RevenueEndDate = banner.LastDay;

				//        this.InvoiceItemDataHolderList.Add(newBannerItem);

				//        ViewState["InvoiceItemDataHolderList"] = InvoiceItemDataHolderList;

				//        SetupUnprocessedBanners();
				//    }
				//    else
				//    {
				//        this.InvoiceItemsMessageLabel.Visible = true;
				//        this.InvoiceItemsMessageLabel.Text = "* Banner " + bannerK.ToString() + " has already been added.";
				//    }
				//}
			}
			catch
			{ }
			finally
			{
				this.CalculateInvoiceItemVatAndTotals();
			}
		}
        #endregion

        #region InvoiceItemsGridView Event Handlers
        protected void InvoiceItemsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.InvoiceItemsGridView.ShowFooter = false;
            InvoiceItemsGridView.EditIndex = e.NewEditIndex;

            CalculateInvoiceItemVatAndTotals();

			SetupInvoiceItemDropDownLists();
        }

        protected void InvoiceItemsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.InvoiceItemsGridView.ShowFooter = true;
            InvoiceItemsGridView.EditIndex = -1;

            CalculateInvoiceItemVatAndTotals(); 
        }

        protected void InvoiceItemsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            InvoiceItemsGridView.PageIndex = e.NewPageIndex;

            InvoiceAndSubItemsBindData();
        }

        protected void InvoiceItemsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
			Page.Validate("InvoiceItemUpdate");
			if (Page.IsValid)
			{
				try
				{
					GridViewRow row = InvoiceItemsGridView.Rows[e.RowIndex];
					// Note: this works only when paging is turned off
					InvoiceItemDataHolder editInvoiceItemDataHolder = InvoiceItemDataHolderList[e.RowIndex];

					editInvoiceItemDataHolder.Description = ((TextBox)row.FindControl("EditDescriptionTextBox")).Text.Trim();
					editInvoiceItemDataHolder.RevenueStartDate = ((Spotted.CustomControls.Cal)row.FindControl("EditRevenueStartDateCal")).Date;
					editInvoiceItemDataHolder.RevenueEndDate = ((Spotted.CustomControls.Cal)row.FindControl("EditRevenueEndDateCal")).Date;
					editInvoiceItemDataHolder.VatCode = (InvoiceItem.VATCodes)Convert.ToInt32(((DropDownList)row.FindControl("EditVatCodeDropDownList")).SelectedValue);
					editInvoiceItemDataHolder.Discount = Utilities.ConvertPercentageStringToDouble(((TextBox)row.FindControl("uiEditDiscountTextBox")).Text);
					if (((TextBox)row.FindControl("EditTotalTextBox")).Text.Length > 0)
						editInvoiceItemDataHolder.SetTotal(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditTotalTextBox")).Text));
					else
						editInvoiceItemDataHolder.PriceBeforeDiscount =Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditPriceBeforeDiscountTextBox")).Text);
					//editInvoiceItemDataHolder.Price = Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditPriceBeforeDiscountTextBox")).Text));
					//editInvoiceItemDataHolder.Vat = Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditVatTextBox")).Text));
					//editInvoiceItemDataHolder.Total = Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditTotalTextBox")).Text));					
					editInvoiceItemDataHolder.Type = (InvoiceItem.Types)Convert.ToInt32(((DropDownList)row.FindControl("EditTypeDropDownList")).SelectedValue);

					InvoiceItemDataHolderList[e.RowIndex] = editInvoiceItemDataHolder;

					ViewState["InvoiceItemDataHolderList"] = InvoiceItemDataHolderList;

					this.InvoiceItemsGridView.EditIndex = -1;
					this.InvoiceItemsGridView.ShowFooter = true;
				}
				catch (Exception ex)
				{ 

				}
				finally
				{
					CalculateInvoiceItemVatAndTotals();
				}
			}
        }

        protected void InvoiceItemsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.InvoiceItemsGridView.EditIndex = -1;
            this.InvoiceItemsGridView.ShowFooter = true;

            if (InvoiceItemDataHolderList.Count >= e.RowIndex)
            {
                InvoiceItemDataHolder deleteInvoiceItemDataHolder = InvoiceItemDataHolderList[e.RowIndex];
                // Delete row from List
                InvoiceItemDataHolderList.Remove(deleteInvoiceItemDataHolder);
				//InvoiceItemDataHolderDeleteList.Add(deleteInvoiceItemDataHolder);
            }
			SetupUnprocessedBanners();
            CalculateInvoiceItemVatAndTotals();
        }

        protected void InvoiceItemsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
				if (e.Row.RowIndex != InvoiceItemsGridView.EditIndex)
				{
					if (e.Row.FindControl("DeleteLinkButton") != null)
					{
						LinkButton deleteInvoiceItemLinkButton = (LinkButton)e.Row.FindControl("DeleteLinkButton");
						// Only add onclick if the Invoice has not already been saved.  Cannot delete Invoice Items if Invoice is already saved
						if (InvoiceK <= 0)
						{
							deleteInvoiceItemLinkButton.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to remove this invoice item?')");
						}
						else
						{
							deleteInvoiceItemLinkButton.Attributes.Remove("onclick");
						}
					}
					if (e.Row.FindControl("RevenueStartDateLabel") != null)
					{
						Label revenueStartDateLabel = (Label)e.Row.FindControl("RevenueStartDateLabel");
						revenueStartDateLabel.Text = revenueStartDateLabel.Text.Equals("01/01/01") ? "" : revenueStartDateLabel.Text;
					}
					if (e.Row.FindControl("RevenueEndDateLabel") != null)
					{
						Label revenueEndDateLabel = (Label)e.Row.FindControl("RevenueEndDateLabel");
						revenueEndDateLabel.Text = revenueEndDateLabel.Text.Equals("01/01/01") ? "" : revenueEndDateLabel.Text;
					}
				}
				else
				{
					if (e.Row.FindControl("EditPriceBeforeDiscountTextBox") != null && e.Row.FindControl("EditTotalTextBox") != null)
					{
						TextBox EditPriceBeforeDiscountTextBox = (TextBox)e.Row.FindControl("EditPriceBeforeDiscountTextBox");
						TextBox editTotalTextBox = (TextBox)e.Row.FindControl("EditTotalTextBox");
						EditPriceBeforeDiscountTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + editTotalTextBox.ClientID + "').value = '';");
						editTotalTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + EditPriceBeforeDiscountTextBox.ClientID + "').value = '';");
					}
				}
            }
			else if (e.Row.RowType == DataControlRowType.Footer)
			{
				if (e.Row.FindControl("NewPriceBeforeDiscountTextBox") != null && e.Row.FindControl("NewTotalTextBox") != null)
				{
					TextBox newPriceBeforeDiscountTextBox = (TextBox)e.Row.FindControl("NewPriceBeforeDiscountTextBox");
					TextBox newTotalTextBox = (TextBox)e.Row.FindControl("NewTotalTextBox");
					newPriceBeforeDiscountTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + newTotalTextBox.ClientID + "').value = '';");
					newTotalTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + newPriceBeforeDiscountTextBox.ClientID + "').value = '';");
				}
			}
		}

        protected void InvoiceItemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToUpper().Equals("ADD"))
            {
				GridViewRow row = InvoiceItemsGridView.FooterRow;

				CustomValidator revenueStartDateCustomValidator = ((CustomValidator)row.FindControl("NewRevenueStartDateCalCustomValidator"));
				CustomValidator revenueEndDateCustomValidator1 = ((CustomValidator)row.FindControl("NewRevenueEndDateCalCustomValidator1"));
				CustomValidator revenueEndDateCustomValidator2 = ((CustomValidator)row.FindControl("NewRevenueEndDateCalCustomValidator2"));
				
				revenueStartDateCustomValidator.Enabled = InvoiceItem.DoesTypeHaveRevenueDateRange((InvoiceItem.Types)Convert.ToInt32(((DropDownList)row.FindControl("NewTypeDropDownList")).SelectedValue));
				revenueEndDateCustomValidator1.Enabled = revenueStartDateCustomValidator.Enabled;
				revenueEndDateCustomValidator2.Enabled = revenueStartDateCustomValidator.Enabled;

				Page.Validate("InvoiceItemNew");
				if (Page.IsValid)
				{
					try
					{
						// add new row from footer to db
						InvoiceItem newInvoiceItem = new InvoiceItem();
						
						newInvoiceItem.Description = ((TextBox)row.FindControl("NewDescriptionTextBox")).Text.Trim();
						newInvoiceItem.RevenueStartDate = ((Spotted.CustomControls.Cal)row.FindControl("NewRevenueStartDateCal")).Date;
						newInvoiceItem.RevenueEndDate = ((Spotted.CustomControls.Cal)row.FindControl("NewRevenueEndDateCal")).Date;
						newInvoiceItem.VatCode = (InvoiceItem.VATCodes)Convert.ToInt32(((DropDownList)row.FindControl("NewVatCodeDropDownList")).SelectedValue);
						newInvoiceItem.Discount = Utilities.ConvertPercentageStringToDouble(((TextBox)row.FindControl("uiNewDiscountTextBox")).Text);
						newInvoiceItem.AgencyDiscount = Utilities.ConvertPercentageStringToDouble(uiAgencyDiscountTextBox.Text);
						if (((TextBox)row.FindControl("NewTotalTextBox")).Text.Length > 0)
						{	newInvoiceItem.SetTotal(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("NewTotalTextBox")).Text)); }
						else
						{ newInvoiceItem.PriceBeforeDiscount = Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("NewPriceBeforeDiscountTextBox")).Text); }
						//newInvoiceItem.Price = Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("NewPriceBeforeDiscountTextBox")).Text));
						//newInvoiceItem.Vat = Convert.ToDouble(((TextBox)row.FindControl("NewVatTextBox")).Text);
						//newInvoiceItem.Total = Convert.ToDouble(((TextBox)row.FindControl("NewTotalTextBox")).Text);
						newInvoiceItem.Type = (InvoiceItem.Types)Convert.ToInt32(((DropDownList)row.FindControl("NewTypeDropDownList")).SelectedValue);

						string alertMessage = "";
						if (revenueStartDateCustomValidator.Enabled == false && Utilities.GetStartOfDay(newInvoiceItem.RevenueStartDate) != DateTime.Today)
						{
							newInvoiceItem.RevenueStartDate = DateTime.Now;
							alertMessage += Utilities.CamelCaseToString(newInvoiceItem.Type.ToString()) + " revenue start date autoset to " + Utilities.DateToString(DateTime.Now) + ". ";
						}
						if (revenueEndDateCustomValidator1.Enabled == false && Utilities.GetStartOfDay(newInvoiceItem.RevenueEndDate) != DateTime.Today)
						{
							newInvoiceItem.RevenueEndDate = DateTime.Now;
							alertMessage += Utilities.CamelCaseToString(newInvoiceItem.Type.ToString()) + " revenue end date autoset to " + Utilities.DateToString(DateTime.Now) + ".";
						}

						if (alertMessage.Length > 0)
						{
							InvoiceItemsMessageLabel.Text = "* " + alertMessage;
							InvoiceItemsMessageLabel.Visible = true;
						}
						InvoiceItemDataHolderList.Add(new InvoiceItemDataHolder(newInvoiceItem));

						ViewState["InvoiceItemDataHolderList"] = InvoiceItemDataHolderList;
						SetupUnprocessedBanners();
					}
					catch (Exception)
					{ }
					finally
					{
						CalculateInvoiceItemVatAndTotals();
					}
				}
            }
        }

        protected void InvoiceItemsGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // For Empty DataSource, we've created one null record so that the header and footers are displayed.  We then hide this row
                if(InvoiceItemDataHolderList.Count <= e.Row.RowIndex)
                    e.Row.Visible = false;

                if (InvoiceItemDataHolderList.Count > e.Row.RowIndex && InvoiceItemDataHolderList[e.Row.RowIndex] == null)
                {
                    e.Row.Visible = false;
                    InvoiceItemDataHolderList.RemoveAt(e.Row.RowIndex);
                }
            }
        }             

        #endregion     

        #region InvoiceTransfersGridView Event Handlers
        protected void InvoiceTransferGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // For Empty DataSource, we've created one null record so that the header and footers are displayed.  We then hide this row
                if (InvoiceTransferDataHolderList.Count <= e.Row.RowIndex)
                    e.Row.Visible = false;

                if (InvoiceTransferDataHolderList.Count > e.Row.RowIndex && InvoiceTransferDataHolderList[e.Row.RowIndex] == null)
                {
                    e.Row.Visible = false;
                    InvoiceTransferDataHolderList.RemoveAt(e.Row.RowIndex);
                }

				if (InvoiceTransferDataHolderList.Count > e.Row.RowIndex && InvoiceTransferDataHolderList[e.Row.RowIndex] != null)
				{
					if (InvoiceTransferDataHolderList[e.Row.RowIndex].State.Equals(DataHolderState.Unchanged))
					{
						e.Row.FindControl("DeleteLinkButton").Visible = Usr.Current.IsSuperAdmin;
					}
				}
            }
        }

        protected void InvoiceTransferGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex != InvoiceTransferGridView.EditIndex)
                {
                    LinkButton l = (LinkButton)e.Row.FindControl("DeleteLinkButton");
                    l.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to remove this transfer?')");
                }
            }
        }

        protected void InvoiceTransferGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToUpper().Equals("ADD"))
            {
                // add new row from footer to db
                InvoiceTransfer newInvoiceTransfer = new InvoiceTransfer();

                GridViewRow row = InvoiceTransferGridView.FooterRow;

                newInvoiceTransfer.Amount = Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("NewAmountTextBox")).Text);
                newInvoiceTransfer.TransferK = Convert.ToInt32(((DropDownList)row.FindControl("NewTransferKDropDownList")).SelectedValue);
                newInvoiceTransfer.InvoiceK = InvoiceK;

				decimal totalTransferAmount = 0;

				bool doNotAdd = Math.Round(newInvoiceTransfer.Amount, 2) == 0;

				foreach (InvoiceTransferDataHolder itdh in this.InvoiceTransferDataHolderList)
				{
					if (itdh.TransferK == newInvoiceTransfer.TransferK)
					{
						ProcessingVal.ErrorMessage = "Cannot add money from transfer #" + itdh.TransferK + " more than once";
						ProcessingVal.IsValid = false;
						doNotAdd = true;
					}

					totalTransferAmount += itdh.Amount;
				}

				if (doNotAdd == false)
				{
					if (new Transfer(newInvoiceTransfer.TransferK).AmountRemaining() < newInvoiceTransfer.Amount)
					{
						ProcessingVal.ErrorMessage = "Insufficient funds available on transfer #" + newInvoiceTransfer.TransferK;
						ProcessingVal.IsValid = false;
						doNotAdd = true;
					}
				}
				if (doNotAdd == false)
				{
					foreach (InvoiceCreditDataHolder icdh in this.InvoiceCreditDataHolderList)
					{
						totalTransferAmount -= icdh.Amount;
					}

					if (totalTransferAmount + newInvoiceTransfer.Amount > Convert.ToDecimal(TotalTextBox.Text.Replace("£", "")))
					{
						ProcessingVal.ErrorMessage = "Adding " + newInvoiceTransfer.Amount.ToString("c") + " from transfer #" + newInvoiceTransfer.TransferK + " would exceed the total amount of this invoice";
						ProcessingVal.IsValid = false;
						doNotAdd = true;
					}
				}
				if (doNotAdd == false)
				{
					InvoiceTransferDataHolder itdh = new InvoiceTransferDataHolder(newInvoiceTransfer);
					itdh.State = DataHolderState.Added;
					InvoiceTransferDataHolderList.Add(itdh);

					ViewState["InvoiceTransferDataHolderList"] = InvoiceTransferDataHolderList;

					CalculateInvoiceItemVatAndTotals();
				}
				else
				{
					InvoiceAndSubItemsBindData();
				}
            }
        }

        protected void InvoiceTransferGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.InvoiceTransferGridView.EditIndex = -1;
            this.InvoiceTransferGridView.ShowFooter = true;

            if (InvoiceTransferDataHolderList.Count >= e.RowIndex)
            {
                InvoiceTransferDataHolder deleteInvoiceTransferDataHolder = InvoiceTransferDataHolderList[e.RowIndex];
                // Delete row from List
                InvoiceTransferDataHolderList.Remove(deleteInvoiceTransferDataHolder);
				if (!InvoiceTransferDataHolderDeleteList.Contains(deleteInvoiceTransferDataHolder))
				{
					bool contains = false;
					foreach (InvoiceTransferDataHolder itdh in InvoiceTransferDataHolderDeleteList)
					{
						if (itdh.TransferK == deleteInvoiceTransferDataHolder.TransferK)
						{
							contains = true;
							break;
						}
					}
					if (contains == false)
					{
						InvoiceTransferDataHolderDeleteList.Add(deleteInvoiceTransferDataHolder);
						if (CurrentInvoice.K > 0)
						{
							deleteInvoiceTransferDataHolder.State = DataHolderState.Deleted;
							deleteInvoiceTransferDataHolder.UpdateInsertDelete();
							if (Math.Round(deleteInvoiceTransferDataHolder.Amount, 2) != 0)
							{
								Transfer t = new Transfer(deleteInvoiceTransferDataHolder.TransferK);
								t.IsFullyApplied = false;
								t.Update();
								//if (t.Method == Transfer.Methods.TicketSales)
								//{
								//	Bobs.BankExport.GenerateBankExportForTicketFundsUsed(t, -1 * deleteInvoiceTransferDataHolder.Amount, CurrentInvoice);
								//}

								CurrentInvoice.AddNote("Transfer #" + t.K.ToString() + " was unapplied from " + (CurrentInvoice.Paid ? "paid" : "saved") + " invoice", Usr.Current.NickName);
								CurrentInvoice.UpdateAndSetPaidStatus();
								LoadScreenFromInvoice();
							}
							SetAddTransfersOptions();
						}
					}
				}
            }
            CalculateInvoiceItemVatAndTotals();
			ShowHideControls(CurrentInvoice.K > 0);
        }   

        #endregion

		#region Custom Validators
		#region DateVal
		public void DateVal(object o, ServerValidateEventArgs e)
		{
			DateTime calendarDate = Convert.ToDateTime(e.Value);

			e.IsValid = (calendarDate > new DateTime(2000, 1, 1) && calendarDate < new DateTime(2040, 1, 1));	
		}
		#endregion

		#region Revenue Dates Val
		public void EditRevenueEndDateVal(object o, ServerValidateEventArgs e)
		{
			DateTime revenueEndDate = Convert.ToDateTime(e.Value);
			DateTime revenueStartDate = ((Spotted.CustomControls.Cal)this.InvoiceItemsGridView.Rows[this.InvoiceItemsGridView.EditIndex].FindControl("EditRevenueStartDateCal")).Date;

			if (revenueEndDate > new DateTime(2000, 1, 1) && revenueEndDate < new DateTime(2040, 1, 1))
				e.IsValid = revenueEndDate >= revenueStartDate;
		}
		public void NewRevenueEndDateVal(object o, ServerValidateEventArgs e)
		{
			DateTime revenueEndDate = Convert.ToDateTime(e.Value);
			DateTime revenueStartDate = ((Spotted.CustomControls.Cal)this.InvoiceItemsGridView.FooterRow.FindControl("NewRevenueStartDateCal")).Date;

			if (revenueEndDate > new DateTime(2000, 1, 1) && revenueEndDate < new DateTime(2040, 1, 1))
				e.IsValid = revenueEndDate >= revenueStartDate;
		}
		#endregion

		#region PriceTotalVal
		public void EditPriceBeforeDiscountTotalVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				decimal total = Utilities.ConvertMoneyStringToDecimal(e.Value);
				decimal price = Utilities.ConvertMoneyStringToDecimal(((TextBox)this.InvoiceItemsGridView.Rows[this.InvoiceItemsGridView.EditIndex].FindControl("EditPriceBeforeDiscountTextBox")).Text);

				e.IsValid = total > 0 || price > 0;
			}
			catch (Exception)
			{
				e.IsValid = false;
			}
		}
		public void NewPriceBeforeDiscountTotalVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				decimal total = Utilities.ConvertMoneyStringToDecimal(e.Value);
				decimal priceBeforeDiscount = Utilities.ConvertMoneyStringToDecimal(((TextBox)this.InvoiceItemsGridView.FooterRow.FindControl("NewPriceBeforeDiscountTextBox")).Text);

				e.IsValid = total > 0 || priceBeforeDiscount > 0;
			}
			catch (Exception)
			{
				e.IsValid = false;
			}
		}

		#endregion

		#region NoEditVal
		public void NoEditVal(object o, ServerValidateEventArgs e)
		{
			if (this.InvoiceItemsGridView.EditIndex >= 0)
			{
				this.NoEditCustomVal.ErrorMessage = "Invoice item must be saved.";
				this.NoEditCustomVal.IsValid = false;
				this.ProcessingVal.IsValid = false;
			}
			else if (this.InvoiceTransferGridView.EditIndex >= 0)
			{
				this.NoEditCustomVal.ErrorMessage = "Transfer must be saved.";
				this.NoEditCustomVal.IsValid = false;
			}
			else if (this.InvoiceCreditGridView.EditIndex >= 0)
			{
				this.NoEditCustomVal.ErrorMessage = "Credit must be saved.";
				this.NoEditCustomVal.IsValid = false;
			}
		}
		#endregion

		#region MoneyTextBoxVal
		public void MoneyTextBoxVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = Utilities.IsPositiveMoneyText(e.Value, true);
		}
		#endregion

		#region OverrideEmailAddressVal
		public void OverrideEmailAddressVal(object o, ServerValidateEventArgs e)
		{
			if (!this.OverrideEmailRecipientsCheckBox.Checked)
			{
				Regex EmailRegex = new Regex(Cambro.Misc.RegEx.Email);
				e.IsValid = EmailRegex.IsMatch(e.Value.Trim());
			}
			else
				e.IsValid = true;

		}
		#endregion
		#region PromoterAndUserVal
		public void PromoterAndUserVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = this.uiPromotersAutoComplete.Value.Length > 0 || this.uiUsersAutoComplete.Value.Length > 0;
		}
		#endregion
		#endregion
	}
}
