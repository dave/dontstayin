using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Local;
using Spotted;
using Spotted.Controls;
using Spotted.Master;
using Bobs.DataHolders;

namespace Spotted.Admin
{
    public partial class CreditScreen : AdminUserControl
	{
		#region Variables
		int CreditK = 0;
        int InvoiceK = 0;
        Invoice CurrentCredit = new Invoice();
		Invoice currentInvoice;
        InvoiceItemSet CurrentCreditItems;
        InvoiceTransferSet CurrentCreditTransfers;
        InvoiceCreditSet CurrentCreditInvoices;

        List<InvoiceItemDataHolder> creditItemDataHolderList = new List<InvoiceItemDataHolder>();
        List<InvoiceItemDataHolder> creditItemDataHolderDeleteList = new List<InvoiceItemDataHolder>();
        List<InvoiceTransferDataHolder> creditTransferDataHolderList = new List<InvoiceTransferDataHolder>();
        List<InvoiceTransferDataHolder> creditTransferDataHolderDeleteList = new List<InvoiceTransferDataHolder>();
        List<InvoiceCreditDataHolder> creditInvoiceDataHolderList = new List<InvoiceCreditDataHolder>();
        List<InvoiceCreditDataHolder> creditInvoiceDataHolderDeleteList = new List<InvoiceCreditDataHolder>();
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                if (ViewState["CreditK"] != null)
                    CreditK = (int)ViewState["CreditK"];
                else
                {
					CreditK = Convert.ToInt32(ContainerPage.Url["K"].Value);
                    ViewState["CreditK"] = CreditK;
					
                }
				if(CreditK > 0)
					CurrentCredit = new Invoice(CreditK);
            }
            catch
            {
                // if no Credit K, then assume we are creating a new Credit
				CreditK = 0;
            }

			if (!this.IsPostBack)
            {
				PriceTextBox.Style["text-align"] = "right";
				VATTextBox.Style["text-align"] = "right";
				TotalTextBox.Style["text-align"] = "right";
				CreatedDateTextBox.Style["text-align"] = "right";
				PaidDateTextBox.Style["text-align"] = "right";
				TaxDateValueLabel.Style["text-align"] = "right";
				OverrideTaxDateCheckBox.Style["text-align"] = "right";

				NotesAddOnlyTextBox.ReadOnlyTextBox.CssClass = "readOnlyNotesTextBox";
				NotesAddOnlyTextBox.AddTextBox.CssClass = "addNotesTextBox";
				NotesAddOnlyTextBox.TimeStampFormat = "dd/MM/yy HH:mm";
				NotesAddOnlyTextBox.AuthorName = Usr.Current.NickName;
				NotesAddOnlyTextBox.InsertOption = AddOnlyTextBox.InsertOptions.AddAtBeginning;

				this.SearchForTransferHyperLink.NavigateUrl = UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "adminmainaccounting", new string[] { });

				ViewState["DuplicateGuid"] = Guid.NewGuid();

				if (ContainerPage.Url["invoiceK"].IsInt)
                {
					InvoiceK = Convert.ToInt32(ContainerPage.Url["invoiceK"].Value);
					InvoiceDataHolder creditDataHolder = CurrentInvoice.CreateCredit();
					CurrentCredit = creditDataHolder.Invoice;
					CreditItemDataHolderList = creditDataHolder.InvoiceItemDataHolderList;
                }

				// Must have at least one
				if (CreditK == 0 && InvoiceK == 0)
				{
					string response = "<script type=\"text/javascript\">alert('Credit must be created from an invoice'); open('" + UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "adminmainaccounting") + ", '_self');</script>";
					Response.Write(response);
					Response.End();
				}

				if (Usr.Current != null && CurrentCredit.K == 0)
				{
					CurrentCredit.ActionUsr = Usr.Current;
				}

                if (CreditK > 0)
                {
                    CreditAndSubItemsBindData();                                      
                }
                
                LoadScreenFromCredit();

                if(CreditK == 0)
                {
                    // Setup screen for new data input
                    this.CreatedDateTextBox.Text = DateTime.Now.ToShortDateString();
					this.PaidCheckBox.Checked = false;
					this.SetPaidImage(false);
                }

                CalculateCreditItemVatAndTotals();		
            }
            SetupAvailableTransfersDropDownList();

            // Setup screen rules for updating only
			if (CreditK > 0)
			{
				if (CurrentCredit.Type.Equals(Invoice.Types.Invoice))
					Response.Redirect(CurrentCredit.UrlAdmin());

				DisableForPreviouslySaved();
			}
			
			ShowHideControls(CreditK > 0);
			EnableSalesUsrAndAmount();
			if (CreditK > 0 && Math.Round(CurrentCredit.AmountPaid, 2) == Math.Round(CurrentCredit.Total, 2))
			{
				CreditTransferGridView.FooterRow.Visible = false;
				this.CreateTransferHyperLink.Visible = false;
				this.SearchForTransferHyperLink.Visible = false;
			}
		}
		#endregion

		#region Properties
		public Invoice CurrentInvoice
		{
			get
			{
				if (currentInvoice == null && InvoiceK > 0)
					currentInvoice = new Invoice(InvoiceK);
				return currentInvoice;
			}
		}

		public List<InvoiceItemDataHolder> CreditItemDataHolderList
        {
            get
            {
                if (ViewState["CreditItemDataHolderList"] != null)
                {
                    creditItemDataHolderList = (List<InvoiceItemDataHolder>)ViewState["CreditItemDataHolderList"];
                }
                else
                {
                    Query CreditItemQuery = new Query();
                    CreditItemQuery.QueryCondition = new Q(InvoiceItem.Columns.InvoiceK, CreditK);
                    creditItemDataHolderList.Clear();
                    CurrentCreditItems = new InvoiceItemSet(CreditItemQuery);

                    foreach (InvoiceItem creditItem in CurrentCreditItems)
                    {
                        creditItemDataHolderList.Add(new InvoiceItemDataHolder(creditItem));
                    }

                    ViewState["CreditItemDataHolderList"] = creditItemDataHolderList;
                }
                return creditItemDataHolderList;
            }
            set
            {
                creditItemDataHolderList = value;
                ViewState["CreditItemDataHolderList"] = creditItemDataHolderList;
            }
        }

        public List<InvoiceItemDataHolder> CreditItemDataHolderDeleteList
        {
            get
            {
                if (ViewState["CreditItemDataHolderDeleteList"] != null)
                {
                    creditItemDataHolderDeleteList = (List<InvoiceItemDataHolder>)ViewState["CreditItemDataHolderDeleteList"];
                }
                else
                {
                    creditItemDataHolderDeleteList = new List<InvoiceItemDataHolder>();
                    ViewState["CreditItemDataHolderDeleteList"] = creditItemDataHolderDeleteList;
                }
                return creditItemDataHolderDeleteList;
            }
            set
            {
                creditItemDataHolderDeleteList = value;
                ViewState["CreditItemDataHolderDeleteList"] = creditItemDataHolderDeleteList;
            }
        }

        public List<InvoiceTransferDataHolder> CreditTransferDataHolderList
        {
            get
            {
                if (ViewState["CreditTransferDataHolderList"] != null)
                {
                    creditTransferDataHolderList = (List<InvoiceTransferDataHolder>)ViewState["CreditTransferDataHolderList"];
                }
                else
                {
                    Query CreditTransferQuery = new Query();
                    CreditTransferQuery.QueryCondition = new Q(InvoiceTransfer.Columns.InvoiceK, CreditK);
                    creditTransferDataHolderList.Clear();
                    CurrentCreditTransfers = new InvoiceTransferSet(CreditTransferQuery);

                    foreach (InvoiceTransfer creditTransfer in CurrentCreditTransfers)
                    {
                        creditTransferDataHolderList.Add(new InvoiceTransferDataHolder(creditTransfer));
                    }

                    ViewState["CreditTransferDataHolderList"] = creditTransferDataHolderList;
                }
                return creditTransferDataHolderList;
            }
            set
            {
                creditTransferDataHolderList = value;
                ViewState["CreditTransferDataHolderList"] = creditTransferDataHolderList;
            }
        }

        public List<InvoiceTransferDataHolder> CreditTransferDataHolderDeleteList
        {
            get
            {
                if (ViewState["CreditTransferDataHolderDeleteList"] != null)
                {
                    creditTransferDataHolderDeleteList = (List<InvoiceTransferDataHolder>)ViewState["CreditTransferDataHolderDeleteList"];
                }
                else
                {
                    creditTransferDataHolderDeleteList = new List<InvoiceTransferDataHolder>();
                    ViewState["CreditTransferDataHolderDeleteList"] = creditTransferDataHolderList;
                }
                return creditTransferDataHolderDeleteList;
            }
            set
            {
                creditTransferDataHolderDeleteList = value;
                ViewState["CreditTransferDataHolderDeleteList"] = creditTransferDataHolderDeleteList;
            }
        }

        public List<InvoiceCreditDataHolder> CreditInvoiceDataHolderList
        {
            get
            {
                if (ViewState["CreditInvoiceDataHolderList"] != null)
                {
                    creditInvoiceDataHolderList = (List<InvoiceCreditDataHolder>)ViewState["CreditInvoiceDataHolderList"];
                }
                else
                {
                    Query CreditInvoiceQuery = new Query();
                    CreditInvoiceQuery.QueryCondition = new Q(InvoiceCredit.Columns.CreditInvoiceK, CreditK);
                    creditInvoiceDataHolderList.Clear();
                    CurrentCreditInvoices = new InvoiceCreditSet(CreditInvoiceQuery);

                    foreach (InvoiceCredit creditInvoice in CurrentCreditInvoices)
                    {
                        creditInvoiceDataHolderList.Add(new InvoiceCreditDataHolder(creditInvoice));
                    }

                    ViewState["CreditInvoiceDataHolderList"] = creditInvoiceDataHolderList;
                }
                return creditInvoiceDataHolderList;
            }
            set
            {
                creditInvoiceDataHolderList = value;
                ViewState["CreditInvoiceDataHolderList"] = creditItemDataHolderList;
            }
        }

        public List<InvoiceCreditDataHolder> CreditInvoiceDataHolderDeleteList
        {
            get
            {
                if (ViewState["CreditInvoiceDataHolderDeleteList"] != null)
                {
                    creditInvoiceDataHolderDeleteList = (List<InvoiceCreditDataHolder>)ViewState["CreditInvoiceDataHolderDeleteList"];
                }
                else
                {
                    creditInvoiceDataHolderDeleteList = new List<InvoiceCreditDataHolder>();

                    ViewState["CreditInvoiceDataHolderDeleteList"] = creditInvoiceDataHolderDeleteList;
                }
                return creditInvoiceDataHolderDeleteList;
            }
            set
            {
                creditInvoiceDataHolderDeleteList = value;
                ViewState["CreditInvoiceDataHolderDeleteList"] = creditItemDataHolderDeleteList;
            }
        }
       
        #endregion

		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["CreditK"] = CreditK;
			this.ViewState["InvoiceK"] = InvoiceK;

			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["InvoiceK"] != null) InvoiceK = (int)this.ViewState["InvoiceK"];
			if (this.ViewState["CreditK"] != null) CreditK = (int)this.ViewState["CreditK"];
		}
		#endregion

		#region Methods
        private void DisableForPreviouslySaved()
        {
            Utilities.EnableDisableControls(MainPanel, false);
            //Utilities.EnableDisableControls(CreditItemsPanel, false);
            
			//this.NotesTextBox.ReadOnly = false;
			//this.NotesTextBox.CssClass = "";
            this.CreditItemsGridView.Enabled = true;

			//this.OverrideTaxDateCheckBox.Visible = false;

			this.OverrideTaxDateCheckBox.Style.Add("display", "none");
			this.OverrideTaxDateCheckBox.Checked = Usr.Current.IsSuperAdmin;
			this.TaxDateValueLabel.Visible = !Usr.Current.IsSuperAdmin;

			if (!Usr.Current.IsSuperAdmin)
			{
				this.TaxDateCal.Visible = false;
			}
			else
			{
				this.TaxDateCal.Disabled = false;
			}
            
            // hide the update / delete column
            this.CreditItemsGridView.Columns[CreditItemsGridView.Columns.Count-1].Visible = false;
            
            // Hide the add new footer row
            this.CreditItemsGridView.FooterRow.Visible = false;

            this.ViewInvoiceHyperLink.Enabled = true;
        }

		private void EnableSalesUsrAndAmount()
		{
			// only allow adjustment of SalesAmount and SalesUsr when payment was received in the current month. We do not want to change SalesAmount or SalesUsr after end of month sales commissions have been delegated.
			DateTime startOfThisMonth = Utilities.GetStartOfMonth(DateTime.Now);

			Utilities.EnableDisableControls(this.SalesAmountTextBox, Usr.Current.IsSuperAdmin && (CurrentCredit.CreatedDateTime.Equals(DateTime.MinValue) || CurrentCredit.CreatedDateTime >= startOfThisMonth));

			this.SalesUsrLabel.Visible = this.SalesUsrValueLabel.Text.Length > 0;
			this.SalesUsrValueLabel.Visible = this.SalesUsrLabel.Visible;
			this.SalesAmountLabel.Visible = this.SalesUsrLabel.Visible && ((CurrentCredit.K > 0 && CurrentCredit.SalesUsrAmount < 0)
											|| (Usr.Current.IsSuperAdmin && (CurrentCredit.CreatedDateTime.Equals(DateTime.MinValue) || CurrentCredit.CreatedDateTime >= startOfThisMonth)));
			this.SalesAmountTextBox.Visible = this.SalesAmountLabel.Visible;

		}

		private void SetPaidImage(bool isPaid)
		{
			this.PaidImage.Visible = isPaid;
			this.NotPaidImage.Visible = !isPaid;
		}

        private void LoadScreenFromCredit()
        {
            if (CurrentCredit.K != 0)
            {
                this.CreditKTextBox.Text = CurrentCredit.K.ToString();
				this.CreditKValueLabel.Text = Utilities.LinkNewWindow(CurrentCredit.UrlReport(), CurrentCredit.K.ToString());
                Query InvoiceCreditQuery = new Query();
                InvoiceCreditQuery.QueryCondition = new Q(InvoiceCredit.Columns.CreditInvoiceK, CurrentCredit.K);
                InvoiceCreditSet invoiceCreditSet = new InvoiceCreditSet(InvoiceCreditQuery);
                if (invoiceCreditSet.Count > 0)
                    InvoiceK = invoiceCreditSet[0].InvoiceK;

				this.TaxDateCal.Date = CurrentCredit.TaxDateTime;
				if (!CurrentCredit.TaxDateTime.Equals(DateTime.MinValue))
					this.TaxDateValueLabel.Text = CurrentCredit.TaxDateTime.ToString("dd/MM/yy");
				else
					this.TaxDateValueLabel.Text = "";

				this.SalesUserKHiddenTextBox.Text = CurrentCredit.SalesUsrK.ToString();
				if (CurrentCredit.SalesUsr != null)
					this.SalesUsrValueLabel.Text = CurrentCredit.SalesUsr.Link();
				else
					this.SalesUsrValueLabel.Text = "";
				this.SalesAmountTextBox.Text = CurrentCredit.SalesUsrAmount.ToString("c");
            }

			if (CurrentInvoice != null)
            {
				this.InvoiceKValueLabel.Text = Utilities.LinkNewWindow(CurrentInvoice.UrlReport(), CurrentInvoice.K.ToString());
                this.ViewInvoiceHyperLink.Visible = true;
				this.ViewInvoiceHyperLink.Text = "<nobr>View invoice #" + CurrentInvoice.K.ToString() + "</nobr>";
				this.ViewInvoiceHyperLink.NavigateUrl = CurrentInvoice.UrlAdmin();

				this.SalesUserKHiddenTextBox.Text = CurrentInvoice.SalesUsrK.ToString();
				if(CurrentInvoice.SalesUsr != null)
					this.SalesUsrValueLabel.Text = CurrentInvoice.SalesUsr.Link();
				else
					this.SalesUsrValueLabel.Text = "";
            }
            if (CurrentCredit.Promoter != null)
            {
                this.PromoterValueLabel.Text = CurrentCredit.Promoter.Link();
                this.PromoterKHiddenTextBox.Text = CurrentCredit.Promoter.K.ToString();
            }
            if (CurrentCredit.Usr != null)
            {
				this.UserValueLabel.Text = CurrentCredit.Usr.Link();
                this.UserKHiddenTextBox.Text = CurrentCredit.Usr.K.ToString();
            }
            if (CurrentCredit.ActionUsr != null)
            {
                this.ActionUserValueLabel.Text = CurrentCredit.ActionUsr.Link();
                this.ActionUserKHiddenTextBox.Text = CurrentCredit.ActionUsr.K.ToString();
            }
            else
            {
                this.ActionUserValueLabel.Text = "";
                this.ActionUserKHiddenTextBox.Text = "0";
            }
			this.PaidCheckBox.Checked = CurrentCredit.Paid;
			this.SetPaidImage(CurrentCredit.Paid);

            this.VATCodeTextBox.Text = CurrentCredit.VatCode.ToString();
            this.VATCodeNumberHiddenTextBox.Text = Convert.ToInt32(CurrentCredit.VatCode).ToString();
			
			this.CreatedDateTextBox.Text = CurrentCredit.CreatedDateTime.ToString("HH:mm dd/MM/yy");
                            
            // DateTime.MinValue is the equivalent of NULL for PaidDateTime
			if (CurrentCredit.PaidDateTime == DateTime.MinValue)
			{
				this.PaidDateTextBox.Text = "";
				this.PaidDateLabel.Visible = false;
			}
			else
			{
				this.PaidDateTextBox.Text = CurrentCredit.PaidDateTime.ToString("HH:mm dd/MM/yy");
				this.PaidDateLabel.Visible = true;
			}

            this.PriceTextBox.Text = CurrentCredit.Price.ToString("c");
            this.VATTextBox.Text = CurrentCredit.Vat.ToString("c");
            this.TotalTextBox.Text = CurrentCredit.Total.ToString("c");
            this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text = CurrentCredit.Notes;

			this.CreateTransferHyperLink.NavigateUrl = CurrentCredit.UrlAdminCreateTransfer();
        }

        private void LoadCreditFromScreen()
        {
			CurrentCredit.Notes = this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text;
			if (this.NotesAddOnlyTextBox.AddTextBox.Text.Trim().Length > 0)
			{
				CurrentCredit.AddNote(this.NotesAddOnlyTextBox.AddTextBox.Text.Trim(), Usr.Current.NickName);
			}

			if (CurrentCredit.K == 0 || CreditK == 0)
			{
				CurrentCredit.CreatedDateTime = DateTime.Now;

                try
                {
                    CurrentCredit.PromoterK = Convert.ToInt32(this.PromoterKHiddenTextBox.Text);
                }
                catch { }
                try
                {
                    CurrentCredit.UsrK = Convert.ToInt32(this.UserKHiddenTextBox.Text);
                }
                catch { }
				CurrentCredit.ActionUsrK = Convert.ToInt32(this.ActionUserKHiddenTextBox.Text);
				CurrentCredit.Type = Invoice.Types.Credit;
				CurrentCredit.Price = Utilities.ConvertMoneyStringToDecimal(this.PriceTextBox.Text);
				CurrentCredit.Vat = Utilities.ConvertMoneyStringToDecimal(this.VATTextBox.Text);
				CurrentCredit.Total = Utilities.ConvertMoneyStringToDecimal(this.TotalTextBox.Text);
				CurrentCredit.DueDateTime = CurrentCredit.CreatedDateTime;
				CurrentCredit.VatCode = (Invoice.VATCodes)Convert.ToInt32(this.VATCodeNumberHiddenTextBox.Text);

				if (this.TaxDateCal.Visible == true && this.TaxDateCal.Date > DateTime.MinValue)
				{
					if (DateTime.Today != Utilities.GetStartOfDay(TaxDateCal.Date))
					{
						CurrentCredit.AddNote("User " + Usr.Current.NickName + " set tax date to " + this.TaxDateCal.Date.ToString("dd/MM/yy"), "System");
					}
					CurrentCredit.TaxDateTime = this.TaxDateCal.Date;
				}
				else
					CurrentCredit.TaxDateTime = CurrentCredit.CreatedDateTime;

				if (this.SalesAmountTextBox.Visible)
				{
					decimal standardSalesUsrAmount = 0;
					foreach (InvoiceItemDataHolder iidh in this.CreditItemDataHolderList)
					{
						if (iidh.DoesApplyToSalesUsrAmount)
							standardSalesUsrAmount += iidh.Price;
					}
					if (Math.Round(Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text), 2) != standardSalesUsrAmount)
					{
						CurrentCredit.AddNote("User " + Usr.Current.NickName + " set sales user amount to " + Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text).ToString("c"), "System");
					}
					CurrentCredit.SalesUsrAmount = Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text);
				}
				else
					CurrentCredit.SalesUsrAmount = CurrentCredit.Price;
			}
			else
			{
				if (Utilities.GetStartOfDay(CurrentCredit.TaxDateTime) != Utilities.GetStartOfDay(TaxDateCal.Date))
				{
					CurrentCredit.AddNote("User " + Usr.Current.NickName + " changed tax date from " + CurrentCredit.TaxDateTime.ToString("dd/MM/yy") + " to " + this.TaxDateCal.Date.ToString("dd/MM/yy"), "System");
					CurrentCredit.TaxDateTime = this.TaxDateCal.Date;
				}
				if (Math.Round(Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text), 2) != Math.Round(CurrentCredit.SalesUsrAmount, 2))
				{
					CurrentCredit.AddNote("User " + Usr.Current.NickName + " changed sales user amount from " + CurrentCredit.SalesUsrAmount.ToString("c") + " to " + Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text).ToString("c"), "System");
					CurrentCredit.SalesUsrAmount = Math.Round(Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text), 2);
				}
				CurrentCredit.SalesUsrAmount = Utilities.ConvertMoneyStringToDecimal(this.SalesAmountTextBox.Text);
			}		

			CurrentCredit.SetPaidAndPaidDateTime(this.PaidCheckBox.Checked);

			CurrentCredit.SalesUsrK = Convert.ToInt32(this.SalesUserKHiddenTextBox.Text);

			CurrentCredit.DuplicateGuid = (Guid)ViewState["DuplicateGuid"];
        }

        private void CreditAndSubItemsBindData()
        {          
            // Credit Item GridView loading
            if (CreditItemDataHolderList.Count == 0)
                CreditItemDataHolderList.Add(null);
            CreditItemsGridView.DataSource = CreditItemDataHolderList;
            CreditItemsGridView.DataBind();

            DropDownList newVatCodeDropDownList = (DropDownList)CreditItemsGridView.FooterRow.FindControl("NewVatCodeDropDownList");
			newVatCodeDropDownList.Items.Clear();
            Utilities.AddEnumValuesToDropDownList(newVatCodeDropDownList, typeof(InvoiceItem.VATCodes));
			newVatCodeDropDownList.SelectedIndex = 1;

            DropDownList newTypeDropDownList = (DropDownList)CreditItemsGridView.FooterRow.FindControl("NewTypeDropDownList");
            newTypeDropDownList.DataSource = InvoiceItem.TypesAsListItemArray();
            newTypeDropDownList.DataTextField = "Text";
            newTypeDropDownList.DataValueField = "Value";
            newTypeDropDownList.DataBind();

            newVatCodeDropDownList.SelectedIndex = 1;

            // Credit Transfer GridView loading
            if (CreditTransferDataHolderList.Count == 0)
                CreditTransferDataHolderList.Add(null);
            CreditTransferGridView.DataSource = CreditTransferDataHolderList;
            CreditTransferGridView.DataBind();

			if (CreditK > 0)
			{
				// hide the update / delete column
				this.CreditItemsGridView.Columns[CreditItemsGridView.Columns.Count - 1].Visible = false;

				// Hide the add new footer row
				this.CreditItemsGridView.FooterRow.Visible = false;
			}

            //SetupAvailableTransfersDropDownList();
		}

		private void ShowHideControls(bool isSavedCredit)
		{
			this.CreditKTextBox.Visible = false;
			this.CreditKValueLabel.Visible = isSavedCredit;
			this.CreditKLabel.Visible = isSavedCredit;
			this.CreatedDateLabel.Visible = isSavedCredit;
			this.CreatedDateTextBox.Visible = isSavedCredit;
			this.DownloadButton.Visible = isSavedCredit;
		}

		// Recalculate all VAT and Totals
		public void CalculateCreditItemVatAndTotals()
		{
			//double creditVATRate = Invoice.VATRate((Invoice.VATCodes)Convert.ToInt32(this.VATCodeNumberHiddenTextBox.Text));
			decimal creditPrice = 0;
			decimal creditVAT = 0;
			decimal creditTotal = 0;
			decimal salesUsrAmount = 0;

			//double creditItemVATRate = 0;
			foreach (InvoiceItemDataHolder iidh in CreditItemDataHolderList)
			{
				var total = iidh.Total;
				//creditItemVATRate = creditVATRate <= InvoiceItem.VATRate(iidh.VatCode) ? creditVATRate : InvoiceItem.VATRate(iidh.VatCode);
				iidh.InvoiceVatCode = (Invoice.VATCodes)Convert.ToInt32(this.VATCodeNumberHiddenTextBox.Text);
				iidh.SetTotal(total);

				//iidh.Price = Math.Round(iidh.Price, 2);
				//if (iidh.K > 0)
				//    iidh.Vat = Math.Round(iidh.Vat, 2);
				//else
				//    iidh.Vat = Math.Round(iidh.Price * creditItemVATRate, 2);
				//iidh.Total = Math.Round(iidh.Price + iidh.Vat, 2);

				creditPrice += iidh.Price;
				creditVAT += iidh.Vat;
				creditTotal += iidh.Total;

				if (iidh.DoesApplyToSalesUsrAmount)
					salesUsrAmount += iidh.Price;
			}
			this.PriceTextBox.Text = creditPrice.ToString("c");
			this.VATTextBox.Text = creditVAT.ToString("c");
			this.TotalTextBox.Text = creditTotal.ToString("c");

			if (CreditK == 0)
			{
				this.SalesAmountTextBox.Text = salesUsrAmount.ToString("c");
			}

			decimal amountPaid = 0;
			DateTime lastTransferDate = DateTime.MinValue;

			foreach (InvoiceTransferDataHolder itdh in CreditTransferDataHolderList)
			{
				amountPaid += itdh.Amount;
				DateTime creditTransferCreatedDate = new Transfer(itdh.TransferK).DateTimeCreated;
				if (lastTransferDate <= creditTransferCreatedDate)
					lastTransferDate = creditTransferCreatedDate;
			}

			if (Math.Round(creditTotal,2) > Math.Round(amountPaid,2) || lastTransferDate.Equals(DateTime.MinValue))
			{
				this.PaidCheckBox.Checked = false;
				this.SetPaidImage(false);
			}
			else if (Math.Round(creditTotal, 2).Equals(Math.Round(amountPaid, 2)))
			{
				this.PaidCheckBox.Checked = true;
				this.SetPaidImage(true);
				this.PaidDateTextBox.Text = lastTransferDate.ToShortDateString();
			}

			CreditAndSubItemsBindData();
		}

		protected string CreditItemTypeToString(object o)
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
		private void SetupAvailableTransfersDropDownList()
        {
			if (this.CreditK == 0 || CurrentCredit.Paid == false)
			{
				LinkButton newTransferLinkButton = (LinkButton)CreditTransferGridView.FooterRow.FindControl("AddLinkButton");
				TextBox newAmountTextBox = (TextBox)CreditTransferGridView.FooterRow.FindControl("NewAmountTextBox");
				newTransferLinkButton.Visible = true;

				Q transfersForPromotersUsersQueryConditions = null;
				//if(this.PromoterKHiddenTextBox.Text != "" && this.UserKHiddenTextBox.Text != "")
				//    transfersForPromotersUsersQueryConditions = new Or(new Q(Transfer.Columns.PromoterK, Convert.ToInt32(PromoterKHiddenTextBox.Text)), new Q(Transfer.Columns.UsrK, Convert.ToInt32(UserKHiddenTextBox.Text)));
				//else if (PromoterKHiddenTextBox.Text != "")
				//    transfersForPromotersUsersQueryConditions = new Q(Transfer.Columns.PromoterK, Convert.ToInt32(PromoterKHiddenTextBox.Text));
				//else if (UserKHiddenTextBox.Text != "")
				//    transfersForPromotersUsersQueryConditions = new Q(Transfer.Columns.UsrK, Convert.ToInt32(UserKHiddenTextBox.Text));
				if (PromoterKHiddenTextBox.Text != "")
					transfersForPromotersUsersQueryConditions = new Q(Transfer.Columns.PromoterK, Convert.ToInt32(PromoterKHiddenTextBox.Text));

				Query availableTransfersQuery = new Query();

				// Credit Transfers must be repayments, thus negative amounts
				string queryForAvailableTransfers = @"-1 * Round([Transfer].[Amount],2) < -1 * (CASE WHEN ((SELECT Sum([InvoiceTransfer].[Amount]) FROM [InvoiceTransfer] WHERE [InvoiceTransfer].[TransferK] = [Transfer].[K]) IS NULL) THEN 0 ELSE Round((SELECT Sum([InvoiceTransfer].[Amount]) FROM [InvoiceTransfer] WHERE [InvoiceTransfer].[TransferK] = [Transfer].[K]),2) END)";

				ListItem[] availableTransferListItems = new ListItem[0];

				if (transfersForPromotersUsersQueryConditions != null)
				{
					availableTransfersQuery.QueryCondition = new And(
						new StringQueryCondition(queryForAvailableTransfers),
						transfersForPromotersUsersQueryConditions,
						new Q(Transfer.Columns.Status, QueryOperator.NotEqualTo, Transfer.StatusEnum.Cancelled),
						new Q(Transfer.Columns.Status, QueryOperator.NotEqualTo, Transfer.StatusEnum.Failed),
						new Q(Transfer.Columns.Type, Transfer.TransferTypes.Refund));

					availableTransfersQuery.Columns = new ColumnSet(Transfer.Columns.K, Transfer.Columns.Amount, Transfer.Columns.Method, Transfer.Columns.DateTimeCreated);

					TransferSet availableTransfers = new TransferSet(availableTransfersQuery);

					availableTransferListItems = new ListItem[availableTransfers.Count];

					for (int i = 0; i < availableTransfers.Count; i++)
					{
						availableTransferListItems[i] = new ListItem("K=" + availableTransfers[i].K.ToString() + " | "
																	+ availableTransfers[i].Amount.ToString("c") + " | "
																	+ Utilities.CamelCaseToString(availableTransfers[i].Method.ToString()) + " | "
																	+ availableTransfers[i].DateTimeCreated.ToShortDateString(),
																	availableTransfers[i].K.ToString());
					}
					if (availableTransfers.Count == 0)
					{
						availableTransferListItems = new ListItem[] { new ListItem("<NO AVAILABLE TRANSFERS>") };
						newTransferLinkButton.Visible = false;
						newAmountTextBox.Visible = false;
					}
				}
				else
					availableTransferListItems = new ListItem[] { new ListItem("<SELECT PROMOTER FIRST>") };

				DropDownList newTransferKDropDownList = (DropDownList)CreditTransferGridView.FooterRow.FindControl("NewTransferKDropDownList");
				newTransferKDropDownList.DataSource = availableTransferListItems;
				newTransferKDropDownList.DataTextField = "Text";
				newTransferKDropDownList.DataValueField = "Value";
				newTransferKDropDownList.DataBind();
			}
        }
        #endregion

        #region Page Event Handlers
        protected void VatCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateCreditItemVatAndTotals();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
			Page.Validate("");
			if (Page.IsValid)
			{
				if (this.OverrideTaxDateCheckBox.Checked == true && !this.TaxDateCal.ValidateNow())
				{
					this.TaxDateCustomValidator.IsValid = false;
					return;
				}

				bool succeeded = false;
				bool previouslyPaid = CurrentCredit.Paid;
				
				LoadCreditFromScreen();

				try
				{
					if (!Invoice.DoesDuplicateGuidExistInDb((Guid)this.ViewState["DuplicateGuid"]) || CurrentCredit.K > 0)
					{
						if (this.CreditItemDataHolderList.Count == 0)
							throw new Exception("Must have at least one credit item!");

						bool newCredit = CurrentCredit.K == 0;
						//Invoice invoiceToCredit = new Invoice(Convert.ToInt32(InvoiceKValueLabel.Text));

						if (CurrentCredit.K == 0 && CurrentInvoice.AmountAllowedToCredit < Math.Abs(CurrentCredit.Total))
							throw new Exception("Cannot credit more than " + CurrentInvoice.AmountAllowedToCredit.ToString("c") + " for Invoice #" + CurrentInvoice.K.ToString());

						CurrentCredit.Update();

						foreach (InvoiceItemDataHolder creditItemDataHolder in this.CreditItemDataHolderList)
						{
							creditItemDataHolder.InvoiceK = CurrentCredit.K;
							creditItemDataHolder.UpdateInsertDelete();
						}
						foreach (InvoiceTransferDataHolder creditTransferDataHolder in this.CreditTransferDataHolderList)
						{
							creditTransferDataHolder.InvoiceK = CurrentCredit.K;
							creditTransferDataHolder.UpdateInsertDelete();
						}
						foreach (InvoiceTransferDataHolder creditTransferDataHolderDelete in this.CreditTransferDataHolderDeleteList)
						{
							bool toBeDeleted = true;
							foreach (InvoiceTransferDataHolder creditTransferDataHolder in this.CreditTransferDataHolderList)
							{
								// Do not delete ones that are confirmed to be saved.  This resolves issues when saved items are marked for deletion, then added again.
								if (creditTransferDataHolder.TransferK == creditTransferDataHolderDelete.TransferK)
								{
									toBeDeleted = false;
									break;
								}
							}

							if (toBeDeleted == true)
							{
								creditTransferDataHolderDelete.State = DataHolderState.Deleted;
								creditTransferDataHolderDelete.UpdateInsertDelete();
							}
						}

						CurrentInvoice.ApplyCreditToThisInvoice(CurrentCredit);

						if (newCredit)
						{
							// Refresh CurrentCredit
							CurrentCredit = new Invoice(CurrentCredit.K);
                            CurrentCredit.AssignBuyerType();
							CurrentCredit.UpdatePromoterStatusAndSalesStatus();
                            CurrentCredit.Update();
						}

						succeeded = true;
					}
					// Do not process if duplicate exists. User probably tried refreshing the page.
					else
					{
						throw new Exception("Duplicate credit already exists in the database.");
					}
				}
			
				catch (Exception ex)
				{
					// Display error message
					this.ProcessingVal.ErrorMessage = ex.Message;
					this.ProcessingVal.IsValid = false;
				}

				// Having Server.Transfer or Response.Redirect caused an error during debugging.
				if (succeeded == true)
				{
					// Send email to promoter and to DSI accounts
					bool creditCreated = false;
					if (CreditK == 0)
						creditCreated = true;

					// Only send out emails when invoice is created or when Paid status changes
					if (creditCreated || previouslyPaid != CurrentCredit.Paid)
						Utilities.EmailInvoice(CurrentCredit, creditCreated);

					string response = "<script type=\"text/javascript\">alert('Credit #" + CurrentCredit.K.ToString() + " saved successfully'); open('" + CurrentCredit.UrlAdmin() + "?" + Cambro.Misc.Utility.GenRandomText(5) + "', '_self');</script>";
					ViewState["DuplicateGuid"] = Guid.NewGuid();
					Response.Write(response);
					Response.End();
				}
			}
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
			if (this.CreditK > 0)
			{
				Response.Redirect(CurrentCredit.UrlAdmin());
			}
			else
			{
				CurrentCredit.Type = Invoice.Types.Credit;
				Response.Redirect(CurrentCredit.UrlAdmin(new string[] { "InvoiceK", this.InvoiceK.ToString() }));
			}
        }

		protected void DownloadButton_Click(object sender, EventArgs e)
		{
			if (CurrentCredit.K > 0)
			{
				Utilities.DownloadAsWord(CurrentCredit, "DontStayIn " + CurrentCredit.TypeToString + " #" + CurrentCredit.K.ToString() + ".doc", Response);
			}
			else
			{
				this.ProcessingVal.IsValid = false;
				this.ProcessingVal.ErrorMessage = "Can only download a saved invoice.";
			}
		}
        #endregion

		#region Custom Validators
		#region DateVal
		public void DateVal(object o, ServerValidateEventArgs e)
		{
			DateTime calendarDate = Convert.ToDateTime(e.Value);

			e.IsValid = (calendarDate > new DateTime(2000, 1, 1) && calendarDate < new DateTime(2050, 1, 1));
		}
		#endregion

		#region Revenue Dates Val
		public void EditRevenueEndDateVal(object o, ServerValidateEventArgs e)
		{
			DateTime revenueEndDate = Convert.ToDateTime(e.Value);
			DateTime revenueStartDate = ((Spotted.CustomControls.Cal)this.CreditItemsGridView.Rows[this.CreditItemsGridView.EditIndex].FindControl("EditRevenueStartDateCal")).Date;

			e.IsValid = revenueEndDate >= revenueStartDate;
		}
		public void NewRevenueEndDateVal(object o, ServerValidateEventArgs e)
		{
			DateTime revenueEndDate = Convert.ToDateTime(e.Value);
			DateTime revenueStartDate = ((Spotted.CustomControls.Cal)this.CreditItemsGridView.FooterRow.FindControl("NewRevenueStartDateCal")).Date;

			e.IsValid = revenueEndDate >= revenueStartDate;
		}
		#endregion

		#region PriceTotalVal
		public void EditPriceTotalVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				var total = Utilities.ConvertMoneyStringToDecimal(e.Value);
				var price = Utilities.ConvertMoneyStringToDecimal(((TextBox)this.CreditItemsGridView.Rows[this.CreditItemsGridView.EditIndex].FindControl("EditPriceTextBox")).Text);

				e.IsValid = total < 0 || price < 0;
			}
			catch
			{
				e.IsValid = false;
			}
		}
		public void NewPriceTotalVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				var total = Utilities.ConvertMoneyStringToDecimal(e.Value);
				var price = Utilities.ConvertMoneyStringToDecimal(((TextBox)this.CreditItemsGridView.FooterRow.FindControl("NewPriceTextBox")).Text);

				e.IsValid = total < 0 || price < 0;
			}
			catch
			{
				e.IsValid = false;
			}
		}

		#endregion

		#region MoneyTextBoxVal
		public void MoneyTextBoxVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = Utilities.IsNegativeMoneyText(e.Value, true);
		}
		#endregion
		#endregion

		#region CreditItemsGridView Event Handlers
		protected void CreditItemsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.CreditItemsGridView.ShowFooter = false;
            CreditItemsGridView.EditIndex = e.NewEditIndex;

            CalculateCreditItemVatAndTotals();

            DropDownList editVatCodeDropDownList = (DropDownList)CreditItemsGridView.Rows[e.NewEditIndex].FindControl("EditVatCodeDropDownList");
            editVatCodeDropDownList.Items.Clear();
            Utilities.AddEnumValuesToDropDownList(editVatCodeDropDownList, typeof(InvoiceItem.VATCodes));
            editVatCodeDropDownList.SelectedIndex = 1;

            DropDownList editTypeDropDownList = (DropDownList)CreditItemsGridView.Rows[e.NewEditIndex].FindControl("EditTypeDropDownList");
            editTypeDropDownList.Items.Clear();
			editTypeDropDownList.Items.AddRange(InvoiceItem.TypesAsListItemArray());

            // Note: this works only when paging is turned off
            editVatCodeDropDownList.SelectedValue = Convert.ToInt32(((List<InvoiceItemDataHolder>)CreditItemsGridView.DataSource)[e.NewEditIndex].VatCode).ToString();
            editTypeDropDownList.SelectedValue = Convert.ToInt32(((List<InvoiceItemDataHolder>)CreditItemsGridView.DataSource)[e.NewEditIndex].Type).ToString();
        }

        protected void CreditItemsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.CreditItemsGridView.ShowFooter = true;
            CreditItemsGridView.EditIndex = -1;

            CalculateCreditItemVatAndTotals(); 
        }

        protected void CreditItemsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CreditItemsGridView.PageIndex = e.NewPageIndex;

            CreditAndSubItemsBindData();
        }

        protected void CreditItemsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
			Page.Validate("CreditItemUpdate");
			if (Page.IsValid)
			{
				try
				{
					GridViewRow row = CreditItemsGridView.Rows[e.RowIndex];
					// Note: this works only when paging is turned off
					InvoiceItemDataHolder editCreditItemDataHolder = CreditItemDataHolderList[e.RowIndex];

					editCreditItemDataHolder.Description = ((TextBox)row.FindControl("EditDescriptionTextBox")).Text.Trim();
					editCreditItemDataHolder.RevenueStartDate = ((Spotted.CustomControls.Cal)row.FindControl("EditRevenueStartDateCal")).Date;
					editCreditItemDataHolder.RevenueEndDate = ((Spotted.CustomControls.Cal)row.FindControl("EditRevenueEndDateCal")).Date;
					if (((TextBox)row.FindControl("EditTotalTextBox")).Text.Length > 0)
						editCreditItemDataHolder.SetTotal( -1 * Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditTotalTextBox")).Text)));
					else
						editCreditItemDataHolder.PriceBeforeDiscount = -1 * Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditPriceTextBox")).Text));

					//editCreditItemDataHolder.Price = -1 * Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditPriceTextBox")).Text));
					//editCreditItemDataHolder.Vat = -1 * Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditVatTextBox")).Text));
					//editCreditItemDataHolder.Total = -1 * Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditTotalTextBox")).Text));
					editCreditItemDataHolder.VatCode = (InvoiceItem.VATCodes)Convert.ToInt32(((DropDownList)row.FindControl("EditVatCodeDropDownList")).SelectedValue);
					editCreditItemDataHolder.Type = (InvoiceItem.Types)Convert.ToInt32(((DropDownList)row.FindControl("EditTypeDropDownList")).SelectedValue);

					CreditItemDataHolderList[e.RowIndex] = editCreditItemDataHolder;

					ViewState["CreditItemDataHolderList"] = CreditItemDataHolderList;

					this.CreditItemsGridView.EditIndex = -1;
					this.CreditItemsGridView.ShowFooter = true;
				}
				catch (Exception)
				{ }
				finally
				{
					CalculateCreditItemVatAndTotals();
				}
			}
        }

        protected void CreditItemsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.CreditItemsGridView.EditIndex = -1;
            this.CreditItemsGridView.ShowFooter = true;

            if (CreditItemDataHolderList.Count >= e.RowIndex)
            {
                InvoiceItemDataHolder deleteCreditItemDataHolder = CreditItemDataHolderList[e.RowIndex];
                // Delete row from List
                CreditItemDataHolderList.Remove(deleteCreditItemDataHolder);
                CreditItemDataHolderDeleteList.Add(deleteCreditItemDataHolder);
            }
            CalculateCreditItemVatAndTotals();
        }

        protected void CreditItemsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex != CreditItemsGridView.EditIndex)
                {
                    LinkButton l = (LinkButton)e.Row.FindControl("DeleteLinkButton");
                    // Only add onclick if the Credit has not already been saved.  Cannot delete Credit Items if Credit is already saved
                    if (CreditK <= 0)
                    {
                        l.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this Credit Item?')");
                    }
                    else
                    {
                        l.Attributes.Clear();
                    }
                }
				else
				{
					TextBox editPriceTextBox = (TextBox)e.Row.FindControl("EditPriceTextBox");
					TextBox editTotalTextBox = (TextBox)e.Row.FindControl("EditTotalTextBox");
					editPriceTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + editTotalTextBox.ClientID + "').value = '';");
					editTotalTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + editPriceTextBox.ClientID + "').value = '';");
				}
            }
			else if (e.Row.RowType == DataControlRowType.Footer)
			{
				TextBox newPriceTextBox = (TextBox)e.Row.FindControl("NewPriceTextBox");
				TextBox newTotalTextBox = (TextBox)e.Row.FindControl("NewTotalTextBox");
				newPriceTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + newTotalTextBox.ClientID + "').value = '';");
				newTotalTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + newPriceTextBox.ClientID + "').value = '';");
			}
        }

        protected void CreditItemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToUpper().Equals("ADD"))
            {
				Page.Validate("CreditItemNew");
				if (Page.IsValid)
				{
					try
					{
						// add new row from footer to db
						InvoiceItem newCreditItem = new InvoiceItem();

						GridViewRow row = CreditItemsGridView.FooterRow;

						newCreditItem.Description = ((TextBox)row.FindControl("NewDescriptionTextBox")).Text.Trim();
						newCreditItem.RevenueStartDate = ((Spotted.CustomControls.Cal)row.FindControl("NewRevenueStartDateCal")).Date;
						newCreditItem.RevenueEndDate = ((Spotted.CustomControls.Cal)row.FindControl("NewRevenueEndDateCal")).Date;
						if (((TextBox)row.FindControl("NewTotalTextBox")).Text.Length > 0)
							newCreditItem.SetTotal(Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("NewTotalTextBox")).Text)));
						else
							newCreditItem.PriceBeforeDiscount = Math.Abs(Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("NewPriceTextBox")).Text));

						newCreditItem.VatCode = (InvoiceItem.VATCodes)Convert.ToInt32(((DropDownList)row.FindControl("NewVatCodeDropDownList")).SelectedValue);
						newCreditItem.Type = (InvoiceItem.Types)Convert.ToInt32(((DropDownList)row.FindControl("NewTypeDropDownList")).SelectedValue);

						CreditItemDataHolderList.Add(new InvoiceItemDataHolder(newCreditItem));

						ViewState["CreditItemDataHolderList"] = CreditItemDataHolderList;
					}
					catch (Exception)
					{ }
					finally
					{
						CalculateCreditItemVatAndTotals();
					}
				}
            }
        }

        protected void CreditItemsGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // For Empty DataSource, we've created one null record so that the header and footers are displayed.  We then hide this row
                if(CreditItemDataHolderList.Count <= e.Row.RowIndex)
                    e.Row.Visible = false;

                if (CreditItemDataHolderList.Count > e.Row.RowIndex && CreditItemDataHolderList[e.Row.RowIndex] == null)
                {
                    e.Row.Visible = false;
                    CreditItemDataHolderList.RemoveAt(e.Row.RowIndex);
                }
            }
        }             

        #endregion     

        #region CreditTransfersGridView Event Handlers
        protected void CreditTransferGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // For Empty DataSource, we've created one null record so that the header and footers are displayed.  We then hide this row
                if (CreditTransferDataHolderList.Count <= e.Row.RowIndex)
                    e.Row.Visible = false;

                if (CreditTransferDataHolderList.Count > e.Row.RowIndex && CreditTransferDataHolderList[e.Row.RowIndex] == null)
                {
                    e.Row.Visible = false;
                    CreditTransferDataHolderList.RemoveAt(e.Row.RowIndex);
                }
				if (CreditTransferDataHolderList.Count > e.Row.RowIndex && CreditTransferDataHolderList[e.Row.RowIndex] != null)
				{
					if (CreditTransferDataHolderList[e.Row.RowIndex].State.Equals(DataHolderState.Unchanged))
					{
						e.Row.FindControl("DeleteLinkButton").Visible = false;
					}
				}
            }
        }

        protected void CreditTransferGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex != CreditTransferGridView.EditIndex)
                {
                    LinkButton l = (LinkButton)e.Row.FindControl("DeleteLinkButton");
                    l.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this Credit Transfer?')");
                }
            }
        }

        protected void CreditTransferGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
			if (e.CommandName.ToUpper().Equals("ADD"))
			{
				// add new row from footer to db
				InvoiceTransfer newCreditTransfer = new InvoiceTransfer();

				GridViewRow row = CreditTransferGridView.FooterRow;

				newCreditTransfer.Amount = Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("NewAmountTextBox")).Text);
				newCreditTransfer.TransferK = Convert.ToInt32(((DropDownList)row.FindControl("NewTransferKDropDownList")).SelectedValue);
				newCreditTransfer.InvoiceK = CreditK;

				decimal totalRefundAmount = 0;

				bool doNotAdd = Math.Round(newCreditTransfer.Amount, 2) == 0;

				foreach (InvoiceTransferDataHolder itdh in this.CreditTransferDataHolderList)
				{
					if (itdh.TransferK == newCreditTransfer.TransferK)
					{
						this.ProcessingVal.ErrorMessage = "Cannot add money from transfer #" + itdh.TransferK + " more than once";
						this.ProcessingVal.IsValid = false;
						doNotAdd = true;
					}
					totalRefundAmount += itdh.Amount;
				}
				if (doNotAdd == false)
				{
					if (new Transfer(newCreditTransfer.TransferK).AmountRemaining() > newCreditTransfer.Amount)
					{
						this.ProcessingVal.ErrorMessage = "Insufficient funds available on refund #" + newCreditTransfer.TransferK;
						this.ProcessingVal.IsValid = false;
						doNotAdd = true;
					}
				}
				if (doNotAdd == false)
				{
					foreach (InvoiceCreditDataHolder icdh in this.CreditInvoiceDataHolderList)
					{
						totalRefundAmount -= icdh.Amount;
					}

					if (totalRefundAmount + newCreditTransfer.Amount < Convert.ToDecimal(TotalTextBox.Text.Replace("£", "")))
					{
						this.ProcessingVal.ErrorMessage = "Adding " + newCreditTransfer.Amount.ToString("c") + " from transfer #" + newCreditTransfer.TransferK + " would exceed the total amount of this credit";
						this.ProcessingVal.IsValid = false;
						doNotAdd = true;
					}
				}
				if (doNotAdd == false)
				{
					InvoiceTransferDataHolder itdh = new InvoiceTransferDataHolder(newCreditTransfer);
					itdh.State = DataHolderState.Added;
					CreditTransferDataHolderList.Add(itdh);

					ViewState["CreditTransferDataHolderList"] = CreditTransferDataHolderList;

					CalculateCreditItemVatAndTotals();
				}
				else
				{
					CreditAndSubItemsBindData();
				}
			}
        }

        protected void CreditTransferGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.CreditTransferGridView.EditIndex = -1;
            this.CreditTransferGridView.ShowFooter = true;

            if (CreditTransferDataHolderList.Count >= e.RowIndex)
            {
                InvoiceTransferDataHolder deleteCreditTransferDataHolder = CreditTransferDataHolderList[e.RowIndex];
                // Delete row from List
                CreditTransferDataHolderList.Remove(deleteCreditTransferDataHolder);
				if (!CreditTransferDataHolderDeleteList.Contains(deleteCreditTransferDataHolder))
				{
					bool contains = false;
					foreach (InvoiceTransferDataHolder itdh in CreditTransferDataHolderDeleteList)
					{
						if (itdh.TransferK == deleteCreditTransferDataHolder.TransferK)
						{
							contains = true;
							break;
						}
					}
					if (contains == false)
						CreditTransferDataHolderDeleteList.Add(deleteCreditTransferDataHolder);
				}
            }
            CalculateCreditItemVatAndTotals();
        }   

        #endregion		
    }
}
