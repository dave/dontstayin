using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
using Bobs.DataHolders;

namespace Spotted.Admin
{
	public partial class TransferScreen : AdminUserControl
	{
		#region Variables
		private int transferK = 0;
		private int invoiceK = 0;
		private int transferRefundK = 0;
		private bool loadRefundTransfer = false;
		private bool redoFailedTransfer = false;
		private InvoiceTransferSet CurrentInvoiceTransfers;
		//private TransferSet CurrentRefundTransfers;
		private List<InvoiceTransferDataHolder> invoiceTransferDataHolderList = new List<InvoiceTransferDataHolder>();
		private List<TransferDataHolder> refundTransferDataHolderList = new List<TransferDataHolder>();

		private Transfer currentTransfer = new Transfer();
		private Transfer transferToRefund;
		private Promoter currentPromoter;
		#endregion

		public TransferScreen()
		{
			this.Init += new EventHandler(TransferScreen_Init);
			
		}

		void TransferScreen_Init(object sender, EventArgs e)
		{
			this.uiPromoterAutoComplete.ValueChanged += new EventHandler(uiPromoterAutoComplete_ValueChanged);
		}

		void uiPromoterAutoComplete_ValueChanged(object sender, EventArgs e)
		{

			// Promoter selection should only occur for new transfers before it is saved
			if (TransferK == 0 && (ViewState["PromoterK"] == null || ((int)ViewState["PromoterK"]) != Convert.ToInt32(this.uiPromoterAutoComplete.Value)))
			{
				if (this.uiPromoterAutoComplete.Value.Length > 0 && CurrentPromoter.AdminUsrs.Count > 0)
				{
					this.uiUsersAutoComplete.Visible = false;
					this.UserDropDownList.Visible = true;
				}
				else
				{
					this.uiUsersAutoComplete.Visible = true;
					this.UserDropDownList.Visible = false;
				}
				SetupUserDropDownList();
			}
		}
		
		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SslPage = true;

			try
			{
				if (ViewState["TransferK"] != null && (int)ViewState["TransferK"] > 0)
					TransferK = (int)ViewState["TransferK"];
				else
				{
					if (ContainerPage.Url["K"].IsInt)
					{
						TransferK = Convert.ToInt32(ContainerPage.Url["K"].Value);
						ViewState["TransferK"] = TransferK;
					}
					else if (ContainerPage.Url["InvoiceK"].IsInt)
					{
						Invoice invoice = new Invoice(Convert.ToInt32(ContainerPage.Url["InvoiceK"].Value));
						this.InvoiceK = invoice.K;
						if (!this.IsPostBack)
							LoadScreenFromInvoice(invoice);
					}
					else if (ContainerPage.Url["TransferRefundK"].IsInt)
					{
						Transfer transferToRefund = new Transfer(Convert.ToInt32(ContainerPage.Url["TransferRefundK"].Value));

						decimal refundAmount = 0;
						if (!this.IsPostBack && !ContainerPage.Url["RefundAmount"].IsNull)
						{
							try { refundAmount = Convert.ToDecimal(ContainerPage.Url["RefundAmount"].Value) / 100m; }
							catch (Exception) { }
						}
						else if (this.AmountTextBox.Text.Length > 0 && Utilities.ConvertMoneyStringToDecimal(this.AmountTextBox.Text) != 0)
						{
							refundAmount = Utilities.ConvertMoneyStringToDecimal(this.AmountTextBox.Text);
						}
						currentTransfer = transferToRefund.RefundThisTransfer(refundAmount);
						// Dont set DateTimeCreated until it is saved
						currentTransfer.DateTimeCreated = DateTime.MinValue;
						loadRefundTransfer = true;
					}

					if (ContainerPage.Url["FailedTransferK"].IsInt)
					{
						Transfer failedTransfer = new Transfer(Convert.ToInt32(ContainerPage.Url["FailedTransferK"].Value));
						currentTransfer = failedTransfer.CopyThisTransfer();
						// Since we cant store card numbers and the SecPay transaction failed, then user must re-enter card number
						currentTransfer.Notes = failedTransfer.Notes;
						currentTransfer.CardNumberEnd = "";
						currentTransfer.Status = Transfer.StatusEnum.Pending;
						redoFailedTransfer = true;
					}
				}
				if (TransferK > 0)
				{
					try
					{
						currentTransfer = new Transfer(TransferK);
					}
					catch (Exception)
					{
						// TODO: add popup error message, then redirect
						Response.Redirect(Transfer.UrlAdminNewTransfer());
					}
				}
			}
			catch
			{
				// if no Transfer K, then assume we are creating a new Transfer
				TransferK = 0;
			}

			
			this.DownloadButton.Enabled = TransferK > 0;
			var currentTransferAmountRemaining = currentTransfer.AmountRemaining();
			this.CreateCampaignCreditsButton.Visible = currentTransfer.Method == Transfer.Methods.TicketSales && currentTransferAmountRemaining > 0;
			if (this.CreateCampaignCreditsButton.Visible)
			{
				int numberOfCredits = CampaignCredit.CalculateTotalCreditsForMoney(currentTransferAmountRemaining / (decimal)(1 + Invoice.VATRate(Invoice.VATCodes.T1, DateTime.Now)), 0.5, currentTransfer.Promoter);
				CreateCampaignCreditsButton.InnerHtml = "Buy " + numberOfCredits.ToString("N0") + " campaign credits";
			}
			if (!this.IsPostBack)
			{
				ViewState["DuplicateGuid"] = Guid.NewGuid();

				this.RefundAmountTextBox.Style["text-align"] = "right";

				NotesAddOnlyTextBox.ReadOnlyTextBox.CssClass = "readOnlyNotesTextBox";
				NotesAddOnlyTextBox.AddTextBox.CssClass = "addNotesTextBox";
				NotesAddOnlyTextBox.TimeStampFormat = "dd/MM/yy HH:mm";
				NotesAddOnlyTextBox.AuthorName = Usr.Current.NickName;
				NotesAddOnlyTextBox.InsertOption = AddOnlyTextBox.InsertOptions.AddAtBeginning;
				CreateCampaignCreditsButton.Attributes["onclick"] = "if(confirm('Are you sure you want to use ticket funds to buy campaign credits?')){__doPostBack('" + CreateCampaignCreditsButton.UniqueID + "','');return false;}else{return false;};";
                SetupTypeDropDownList();

                if (currentTransfer != null)
                    this.TypeDropDownList.SelectedValue = Convert.ToInt32(currentTransfer.Type).ToString();
                
                SetupMethodDropDownList();
				SetupCardTypeDropDownList();				

				if (currentTransfer.K > 0 || loadRefundTransfer == true || redoFailedTransfer == true)
				{
					// Setup screen rules for updating transfer only
					LoadScreenFromTransfer();

					//if(loadRefundTransfer == true)
					//    SetupStatusDropDownList();
				}
				else
				{
					SetupStatusDropDownList();
					this.TransferKValueLabel.Visible = false;
					this.TransferKLabel.Visible = false;
					// Setup screen for new transfer data input
					this.CreatedDateTextBox.Text = DateTime.Now.ToShortDateString();
					if (Usr.Current != null)
					{
						this.uiActionUserAutoComplete.Value = Usr.Current.K.ToString();
						this.uiActionUserAutoComplete.Text = Usr.Current.Name;
						this.ActionUserValueLabel.Text = Usr.Current.Link();
					}
				}

				ShowHidePanels();
			}

			SetBankDetailValidators();
		}
		#endregion

		#region Properties
		public int TransferK
		{
			get { return this.transferK; }
			set { this.transferK = value; }
		}
		public int InvoiceK
		{
			get { return this.invoiceK; }
			set { this.invoiceK = value; }
		}
		public int TransferRefundK
		{
			get { return this.transferRefundK; }
			set { this.transferRefundK = value; }
		}
		public Transfer TransferToRefund
		{
			get
			{
				if (transferToRefund == null && currentTransfer != null && currentTransfer.TransferRefundedK > 0)
					transferToRefund = new Transfer(currentTransfer.TransferRefundedK);
				return transferToRefund;
			}
			set
			{
				transferToRefund = value;
			}
		}
		protected Promoter CurrentPromoter
		{
			get
			{
				if (currentPromoter == null && this.uiPromoterAutoComplete.Value.Length > 0)
					currentPromoter = new Promoter(Convert.ToInt32(this.uiPromoterAutoComplete.Value));
				return currentPromoter;
			}
			set
			{
				currentPromoter = value;
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
					Query InvoiceTransferQuery = new Query(new Q(InvoiceTransfer.Columns.TransferK, TransferK));
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

		public List<TransferDataHolder> RefundTransferDataHolderList
		{
			get
			{
				if (ViewState["RefundTransferDataHolderList"] != null)
				{
					refundTransferDataHolderList = (List<TransferDataHolder>)ViewState["RefundTransferDataHolderList"];
				}
				else
				{
					refundTransferDataHolderList.Clear();
					
					foreach (Transfer transfer in currentTransfer.RefundTransfers)
					{
						refundTransferDataHolderList.Add(new TransferDataHolder(transfer));
					}

					ViewState["RefundTransferDataHolderList"] = refundTransferDataHolderList;
				}
				return refundTransferDataHolderList;
			}
			set
			{
				refundTransferDataHolderList = value;
				ViewState["RefundTransferDataHolderList"] = refundTransferDataHolderList;
			}
		}

		#endregion

		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["TransferK"] = TransferK;
			this.ViewState["InvoiceK"] = InvoiceK;
			this.ViewState["TransferRefundK"] = TransferK;

			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["TransferK"] != null) TransferK = (int)this.ViewState["TransferK"];
			if (this.ViewState["InvoiceK"] != null) InvoiceK = (int)this.ViewState["InvoiceK"];
			if (this.ViewState["TransferRefundK"] != null) TransferRefundK = (int)this.ViewState["TransferRefundK"];
		}
		#endregion

		#region Setup DropDownLists
		private void SetupTypeDropDownList()
		{
			this.TypeDropDownList.Items.Clear();
			this.TypeDropDownList.Items.AddRange(Transfer.TypesAsListItemArray());

			TypeDropDownList.SelectedIndex = 0;
		}

		private void SetupCardTypeDropDownList()
		{
			this.CardTypeDropDownList.Items.Clear();
			this.CardTypeDropDownList.Items.AddRange(BinRange.TypesAsListItemArray());

			// Default to Master Card
			this.CardTypeDropDownList.SelectedValue = "5";
		}

		private void SetupStatusDropDownList()
		{
			List<ListItem> ListItemsList = new List<ListItem>();

			if (this.MethodDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.Methods.Cheque).ToString()))
			{
				this.StatusDropDownList.Visible = true;
				this.StatusTextBox.Visible = false;
				ListItemsList.Add(new ListItem(Transfer.StatusEnum.Pending.ToString(), Convert.ToInt32(Transfer.StatusEnum.Pending).ToString()));
				ListItemsList.Add(new ListItem(Transfer.StatusEnum.Success.ToString(), Convert.ToInt32(Transfer.StatusEnum.Success).ToString()));
				ListItemsList.Add(new ListItem(Transfer.StatusEnum.Cancelled.ToString(), Convert.ToInt32(Transfer.StatusEnum.Cancelled).ToString()));
			}
            else if (this.MethodDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.Methods.BankTransfer).ToString()))
            {
                this.StatusDropDownList.Visible = (TypeDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.TransferTypes.Payment).ToString()) || currentTransfer.K > 0) && StatusDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.StatusEnum.Pending).ToString());
                this.StatusTextBox.Visible = !StatusDropDownList.Visible;
                ListItemsList.Add(new ListItem(Transfer.StatusEnum.Pending.ToString(), Convert.ToInt32(Transfer.StatusEnum.Pending).ToString()));
                ListItemsList.Add(new ListItem(Transfer.StatusEnum.Success.ToString(), Convert.ToInt32(Transfer.StatusEnum.Success).ToString()));
                ListItemsList.Add(new ListItem(Transfer.StatusEnum.Cancelled.ToString(), Convert.ToInt32(Transfer.StatusEnum.Cancelled).ToString()));
                ListItemsList.Add(new ListItem(Transfer.StatusEnum.Failed.ToString(), Convert.ToInt32(Transfer.StatusEnum.Failed).ToString()));
            }
            else if (this.MethodDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.Methods.Card).ToString()))
            {
                if (currentTransfer.K > 0)
                {
                    ListItemsList.Add(new ListItem(Transfer.StatusEnum.Success.ToString(), Convert.ToInt32(Transfer.StatusEnum.Success).ToString()));
                    ListItemsList.Add(new ListItem(Transfer.StatusEnum.Cancelled.ToString(), Convert.ToInt32(Transfer.StatusEnum.Cancelled).ToString()));
                    ListItemsList.Add(new ListItem(Transfer.StatusEnum.Failed.ToString(), Convert.ToInt32(Transfer.StatusEnum.Failed).ToString()));
                }
                else
                {
                    ListItemsList.Add(new ListItem(Transfer.StatusEnum.Pending.ToString(), Convert.ToInt32(Transfer.StatusEnum.Pending).ToString()));
                }
                this.StatusTextBox.Visible = true;
                this.StatusDropDownList.Visible = false;
            }
            else //if (this.MethodDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.Methods.Cash).ToString()))
            {
                ListItemsList.Add(new ListItem(Transfer.StatusEnum.Success.ToString(), Convert.ToInt32(Transfer.StatusEnum.Success).ToString()));
                this.StatusTextBox.Visible = true;
                this.StatusDropDownList.Visible = false;
            }

			this.StatusDropDownList.DataSource = ListItemsList.ToArray();

			StatusDropDownList.DataTextField = "Text";
			StatusDropDownList.DataValueField = "Value";

			this.StatusDropDownList.DataBind();

			this.StatusTextBox.Text = this.StatusDropDownList.SelectedItem.ToString();
		}
        private void SetupMethodDropDownList()
        {
            // Setup drop down lists
            // Can only refund Ticket Funds with Bank Transfer, Cheque, or Cash
            if (TypeDropDownList.SelectedValue == Convert.ToInt32(Transfer.TransferTypes.Refund).ToString())
            {
                if (currentTransfer.TransferRefunded != null && currentTransfer.TransferRefunded.Method == Transfer.Methods.Card)
                    SetupAllowCardMethodDropDownList();
                else
                    SetupRefundMethodDropDownList();
            }
            else
                SetupAllMethodDropDownList();
        }

		private void SetupAllMethodDropDownList()
		{	
            this.MethodDropDownList.DataSource = Transfer.MethodsAsListItemArray();

			MethodDropDownList.DataTextField = "Text";
			MethodDropDownList.DataValueField = "Value";

			this.MethodDropDownList.DataBind();

			MethodDropDownList.SelectedIndex = 1;
		}

		private void SetupAllowCardMethodDropDownList()
		{
			List<ListItem> listItemList = new List<ListItem>();

			ListItem[] listItemArray = Transfer.MethodsAsListItemArray();
			foreach (ListItem li in listItemArray)
			{
                if (!li.Value.Equals(Convert.ToInt32(Transfer.Methods.TicketSales).ToString()))
					listItemList.Add(li);
			}

			MethodDropDownList.DataTextField = "Text";
			MethodDropDownList.DataValueField = "Value";

			this.MethodDropDownList.DataSource = listItemList.ToArray();
			this.MethodDropDownList.DataBind();
		}

        private void SetupRefundMethodDropDownList()
        {
            List<ListItem> listItemList = new List<ListItem>();

            ListItem[] listItemArray = Transfer.MethodsAsListItemArray();
            foreach (ListItem li in listItemArray)
            {
                if (!li.Value.Equals(Convert.ToInt32(Transfer.Methods.Card).ToString()) && !li.Value.Equals(Convert.ToInt32(Transfer.Methods.TicketSales).ToString()))
                    listItemList.Add(li);
            }

            MethodDropDownList.DataTextField = "Text";
            MethodDropDownList.DataValueField = "Value";

            this.MethodDropDownList.DataSource = listItemList.ToArray();
            this.MethodDropDownList.DataBind();
        }

		private void SetupUserDropDownList()
		{
			this.UserDropDownList.Items.Clear();

			if (this.uiPromoterAutoComplete.Value.Length > 0)
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
				if (currentTransfer.Usr != null)
					this.UserDropDownList.Items.Add(new ListItem(currentTransfer.Usr.Name, currentTransfer.Usr.K.ToString()));
			}
		}

		#endregion

		#region Page Event Handlers
		protected void MethodDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowHidePanels();
			SetupStatusDropDownList();
		}

		protected void CancelButton_Click(object sender, EventArgs e)
		{
			if (TransferK > 0)
			{
				Response.Redirect(currentTransfer.UrlAdmin());
			}
			else
			{
				Response.Redirect(Transfer.UrlAdminNewTransfer());
			}
		}

		protected void CreateCampaignCreditsButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (Usr.Current != null && Usr.Current.IsAdmin && currentTransfer != null && currentTransfer.Method == Transfer.Methods.TicketSales)
				{
					currentTransfer.PurchaseCampaignCreditsWithRemainingFunds(Usr.Current, 0.5);
					string response = "<script type=\"text/javascript\">alert('Transfer #" + currentTransfer.K.ToString() + " successfully purchased campaign credits'); open('" + currentTransfer.UrlAdmin() + "?" + Cambro.Misc.Utility.GenRandomText(5) + "', '_self');</script>";
					Response.Write(response);
					Response.End();
				}
			}
			catch (Exception ex)
			{
				this.ProcessingVal.IsValid = false;
				this.ProcessingVal.ErrorMessage = ex.Message;
			}
		}

		protected void DownloadButton_Click(object sender, EventArgs e)
		{
			if (currentTransfer.K > 0)
			{
				Utilities.DownloadAsWord(currentTransfer, "DontStayIn " + (currentTransfer.Type.Equals(Transfer.TransferTypes.Payment) ? "Receipt" : "Remittance Advice") + " #" + currentTransfer.K.ToString() + ".doc", Response);
			}
			else
			{
				this.ProcessingVal.IsValid = false;
				this.ProcessingVal.ErrorMessage = "Can only download a saved invoice.";
			}
		}

		protected void SaveButton_Click(object sender, EventArgs e)
		{
			Page.Validate("");
			if (Page.IsValid)
			{
				bool succeeded = false;
				Transfer.StatusEnum previousStatus = currentTransfer.Status;

				SecPay secPay = new SecPay();
				try
				{
					LoadTransferFromScreen();

                    //if (CurrentTransfer.K == 0)
                    //{
                    //    CurrentTransfer.SetDSIBankAccount();
                    //}

					if (currentTransfer.Type.Equals(Transfer.TransferTypes.Refund) || currentTransfer.Amount < 0)
					{
						// Verify its Dave, Tim, Gee, Neil, or John
						if (!Usr.Current.IsSuperAdmin)
							throw new Exception("You do not have permission to refund. Please ask a super admin for assistance.");

                        if (currentTransfer.TransferRefundedK > 0 && currentTransfer.K == 0)
                        {
                            Transfer transferToRefund = new Transfer(currentTransfer.TransferRefundedK);
                            var transferToRefundAmountRemaining = TransferToRefund.AmountRemaining();
                            if (transferToRefundAmountRemaining < Math.Abs(currentTransfer.Amount))
                                throw new Exception("Cannot refund " + currentTransfer.Amount.ToString("c") + " because transfer to refund only has " + transferToRefundAmountRemaining.ToString("c") + " remaining.");
                        }
					}

					if (!Transfer.DoesDuplicateGuidExistInDb((Guid)this.ViewState["DuplicateGuid"]) || currentTransfer.K > 0)
					{
						if (currentTransfer.Type.Equals(Transfer.TransferTypes.Payment) && currentTransfer.Amount <= 0)
							throw new Exception("Payment must have positive amount > 0");

						if (currentTransfer.Type.Equals(Transfer.TransferTypes.Refund) && currentTransfer.Amount >= 0)
							throw new Exception("Refund must have negative amount < 0");

						if (currentTransfer.Type.Equals(Transfer.TransferTypes.Refund) && currentTransfer.Method.Equals(Transfer.Methods.Card) && currentTransfer.TransferRefundedK == 0)
							throw new Exception("Cannot process card refund without an original succesful card transfer.");

						// Set DateTimeCreated for new Transfers
						if (currentTransfer.K == 0 || currentTransfer.DateTimeCreated.Equals(DateTime.MinValue))
						{
							currentTransfer.DateTimeCreated = DateTime.Now;
						}

						// If Transfer is now set to completed and DateTimeComplete is not set, then set it to NOW
						if (!currentTransfer.Status.Equals(Transfer.StatusEnum.Pending) && currentTransfer.DateTimeComplete.Equals(DateTime.MinValue))
						{
							currentTransfer.DateTimeComplete = DateTime.Now;
						}
						// If saving new refund, first verify that original transfer amount >= new refund + original transfer amount refunded already
                        if (currentTransfer.K == 0 && currentTransfer.Status.Equals(Transfer.StatusEnum.Pending) && currentTransfer.Type.Equals(Transfer.TransferTypes.Refund) && currentTransfer.TransferRefundedK > 0)
						{
							var amountRefunded = TransferToRefund.AmountRefunded();
							if (Math.Round(amountRefunded + Math.Abs(currentTransfer.Amount),2) > Math.Round(TransferToRefund.Amount,2))
							{
								throw new Exception("Refund cannot exceed original transfer amount. " + amountRefunded.ToString("c") + " already refunded "
													+ currentTransfer.Amount.ToString("c") + " is greater than the original transfer amount " + transferToRefund.Amount.ToString("c"));
							}
							if (Math.Round(Math.Abs(currentTransfer.Amount), 2) == Math.Round(transferToRefund.Amount, 2))
								currentTransfer.RefundStatus = Transfer.RefundStatuses.FullRefund;
							else
								currentTransfer.RefundStatus = Transfer.RefundStatuses.PartialRefund;
						}
						// for new card transactions that are marked as Pending, then process via SecPay
						if (currentTransfer.K == 0 && currentTransfer.Method.Equals(Transfer.Methods.Card) && currentTransfer.Status.Equals(Transfer.StatusEnum.Pending))
						{

							if (currentTransfer.Type.Equals(Transfer.TransferTypes.Payment))
							{
								// Process card via SecPay
								secPay.MakePayment(new List<InvoiceDataHolder>(), currentTransfer.Amount, new Usr(currentTransfer.UsrK), currentTransfer.PromoterK, Usr.Current.K, currentTransfer.CardName,
													currentTransfer.CardAddress1, currentTransfer.CardAddressArea, currentTransfer.CardAddressTown, currentTransfer.CardAddressCounty, currentTransfer.CardAddressCountryK, currentTransfer.CardPostcode, "UK", this.CardNumberTextBox.Text.Trim().Replace(" ", ""),
													currentTransfer.CardExpires, currentTransfer.CardCV2, Transfer.FraudCheckEnum.Relaxed, false, currentTransfer.DuplicateGuid, currentTransfer.CardStart, 
													currentTransfer.CardIssue > 0 ? currentTransfer.CardIssue.ToString() : "");
								
								// Since SecPay doesnt store CardAddress2, we need to save it here.

								secPay.Transfer.CardAddressArea = currentTransfer.CardAddressArea;
								secPay.Transfer.CardAddressTown = currentTransfer.CardAddressTown;
								secPay.Transfer.CardAddressCounty = currentTransfer.CardAddressCounty;
								secPay.Transfer.CardAddressCountryK = currentTransfer.CardAddressCountryK;

									secPay.Transfer.Update();

								currentTransfer = secPay.Transfer;
							}
							else if (currentTransfer.Type.Equals(Transfer.TransferTypes.Refund) && currentTransfer.TransferRefundedK > 0)
							{
								secPay.MakeRefund(TransferToRefund, (Guid)ViewState["DuplicateGuid"], Usr.Current.K, currentTransfer.Amount);
								currentTransfer = secPay.Transfer;

								if (this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text.Length > 0)
								{
									// Clear old notes, as they will be in the Notes textbox
									if (TransferToRefund.Notes.Length > 0)
										currentTransfer.Notes = currentTransfer.Notes.Replace(TransferToRefund.Notes, "") + "\n";
									else
										currentTransfer.Notes = "";

									currentTransfer.Notes += this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text;
								}
								TransferToRefund.AddNote("This transfer has been refunded " + currentTransfer.Amount.ToString("c") + " on refund transfer #" + currentTransfer.K.ToString(), "System");
								TransferToRefund.UpdateAndResolveOverapplied();
								Utilities.EmailTransfer(TransferToRefund, false, false);
							}
							if (!currentTransfer.Status.Equals(Transfer.StatusEnum.Success))
							{
								throw new Exception("SecPay transaction was not successful.");
								//TransferK = CurrentTransfer.K;
								//LoadScreenFromTransfer();

								//this.ProcessingCustomValidator.IsValid = false;

								//this.ProcessingCustomValidator.ErrorMessage = "Transfer #" + secPay.SecPayTransfer.K.ToString() + " failed. Please contact administrator for further details";
								//return;
							}
						}
						else
						{
                            if (currentTransfer.K == 0 && currentTransfer.Method == Transfer.Methods.BankTransfer && currentTransfer.Type == Transfer.TransferTypes.Refund)
                                currentTransfer = Transfer.RefundViaBACS(currentTransfer);
                            else
    							currentTransfer.Update();
							if ((currentTransfer.Status.Equals(Transfer.StatusEnum.Pending) || currentTransfer.Status.Equals(Transfer.StatusEnum.Success)) && currentTransfer.Type.Equals(Transfer.TransferTypes.Refund) && currentTransfer.TransferRefundedK > 0)
							{
								TransferToRefund.AddNote("This transfer has been refunded " + currentTransfer.Amount.ToString("c") + " on refund transfer #" + currentTransfer.K.ToString(), "System");
								TransferToRefund.UpdateAndResolveOverapplied();
								Utilities.EmailTransfer(TransferToRefund, false, false);
							}
                            else if (currentTransfer.Status.Equals(Transfer.StatusEnum.Cancelled) && currentTransfer.Type.Equals(Transfer.TransferTypes.Refund) && currentTransfer.TransferRefundedK > 0)
                            {
                                // This scenario only occurs in very rare situations and handled only by SuperAdmins.
                                TransferToRefund.UpdateAndResolveOverapplied();
                            }
						}
						if (this.InvoiceK > 0 && currentTransfer.K > 0)
						{
							InvoiceTransfer invoiceTransfer;
							try
							{
								invoiceTransfer = new InvoiceTransfer(this.InvoiceK, currentTransfer.K);
							}
							catch (Exception)
							{
								invoiceTransfer = new InvoiceTransfer();
								invoiceTransfer.InvoiceK = this.InvoiceK;
								invoiceTransfer.TransferK = currentTransfer.K;
							}
							invoiceTransfer.Amount = new Invoice(this.InvoiceK).Total;

							if (invoiceTransfer.Amount > currentTransfer.Amount)
								invoiceTransfer.Amount = currentTransfer.Amount;

							invoiceTransfer.Update();
						}
						// Update Invoices that are affected by this transfer
						currentTransfer.UpdateAffectedInvoices();

						succeeded = true;
					}
					// Do not process if duplicate exists. User probably tried refreshing the page.
					else
					{
						throw new Exception("Duplicate transfer already exists in the database.");
					}
				}

				catch (Exception ex)
				{
					// If processing SecPay card transfer and error occurred, but transfer saved
					if (secPay.Transfer != null && secPay.Transfer.K > 0 && this.TransferK == 0)
					{
						Mailer sm = new Mailer();
						if (Vars.DevEnv)
						{
							sm.Subject = "Test - ";
							sm.To = "neil@dontstayin.com";
						}
						else
							sm.To = "d.brophy@dontstayin.com, t.aylott@dontstayin.com, neil@dontstayin.com";

						sm.Body = "<p>Exception occurred using SecPay! - Usr = " + Usr.Current.NickName + " (" + Usr.Current.K + "), PromoterK=" + secPay.Transfer.PromoterK.ToString() + "</p>";
						sm.Body += "<p>Message: " + ex.Message + "</p>";
						sm.Body += "<p>Transfer K: " + secPay.Transfer.K.ToString() + "</p>";
						sm.Body += secPay.Transfer.AsHTML();
						sm.TemplateType = Mailer.TemplateTypes.AdminNote;
						sm.Subject = "Exception occurred using SecPay! Transfer K:" + secPay.Transfer.K.ToString();

						sm.Send();

						string redirectUrl = "";
						if (this.InvoiceK > 0)
							redirectUrl = secPay.Transfer.UrlAdminRetryFailedTransfer(this.InvoiceK);
						else
							redirectUrl = secPay.Transfer.UrlAdminRetryFailedTransfer();

						string response = "<script type=\"text/javascript\">alert('Transfer #" + secPay.Transfer.K.ToString() + " " + secPay.Transfer.Status.ToString();

						if (secPay.Transfer.Status.Equals(Transfer.StatusEnum.Success))
						{
							try
							{
								secPay.Transfer.UpdateAndResolveOverapplied();
							}
							catch (Exception)
							{ }
							response += ". There was an exception during saving: " + ex.Message + "'); open('" + secPay.Transfer.UrlAdmin() + "?" + Cambro.Misc.Utility.GenRandomText(5) + "', '_self');</script>";
						}
						else
							response += ". Please try again.'); open('" + redirectUrl + "', '_self');history.go(1);</script>";
						ViewState["DuplicateGuid"] = Guid.NewGuid();
						Response.Write(response);

						//Response.End();
					}
					else
					{
						// Display error message
						this.ProcessingVal.ErrorMessage = ex.Message;
						this.ProcessingVal.IsValid = false;
					}
				}

				// Having Server.Transfer or Response.Redirect in Try{} caused an error during debugging.
				if (succeeded == true)
				{
					// Send email to promoter and to DSI accounts
					bool madeSuccessful = false;
					if (!previousStatus.Equals(Transfer.StatusEnum.Success) && currentTransfer.Status.Equals(Transfer.StatusEnum.Success))
						madeSuccessful = true;

					// New successful transfers via SecPay will be auto emailed.
					// Only new non-SecPay transfers or non-SecPay transfers made successful shall be emailed.
					if (secPay.Transfer.K == 0 && (TransferK == 0 || madeSuccessful))
						Utilities.EmailTransfer(currentTransfer, TransferK == 0, madeSuccessful);

					string response = "<script type=\"text/javascript\">alert('Transfer #" + currentTransfer.K.ToString() + " saved successfully'); open('" + currentTransfer.UrlAdmin() + "?" + Cambro.Misc.Utility.GenRandomText(5) + "', '_self');</script>";
					Response.Write(response);
					Response.End();
				}
			}
		}

		protected void RefundButton_Click(object sender, EventArgs e)
		{
			if (currentTransfer != null && currentTransfer.K > 0 && currentTransfer.Status.Equals(Transfer.StatusEnum.Success) && currentTransfer.Type.Equals(Transfer.TransferTypes.Payment))
			{
				var refundAmount = Utilities.ConvertMoneyStringToDecimal(this.RefundAmountTextBox.Text);

				if (refundAmount > 0 && refundAmount <= Math.Round(currentTransfer.Amount - currentTransfer.AmountRefunded(), 2))
					Response.Redirect(currentTransfer.UrlAdminRefundMe(refundAmount));
				else
				{
					this.ProcessingVal.ErrorMessage = "Invalid refund amount.";
					this.ProcessingVal.IsValid = false;
				}
			}
		}

	 
		public void SetBankDetailValidators()
		{
			bool enable = this.TransferK == 0;
			if (!this.MethodDropDownList.SelectedValue.Equals(Transfer.Methods.BankTransfer.ToString()) || TypeDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.TransferTypes.Payment).ToString()))
				enable = false;
			
			this.BankAccountNameRequiredFieldValidator.Enabled = enable;
			this.BankAccountNumberRequiredFieldValidator.Enabled = enable;
			this.BankNameRequiredFieldValidator.Enabled = enable;
			this.BankSortCodeRequiredFieldValidator.Enabled = enable;
			this.BankTransferRequiredFieldValidator.Enabled = enable;
		}

		protected void TypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
            string selectedMethod = this.MethodDropDownList.SelectedValue;

            SetupMethodDropDownList();
            
            try
            {
                this.MethodDropDownList.SelectedValue = selectedMethod;
            }
            catch { }

            SetupStatusDropDownList();            
		}
		#endregion

		#region Methods
		private void ShowHidePanels()
		{
			this.CardDetailsPanel.Visible = false;
			this.BankDetailsPanel.Visible = false;
			this.CardAdminDetailsPanel.Visible = false;
			this.ChequeDetailsPanel.Visible = false;

			this.CardAddressRequiredFieldValidator.Enabled = false;
			this.CardCV2RegularExpressionValidator.Enabled = false;
			this.CardCV2RequiredFieldValidator.Enabled = false;
			this.CardExpiryDateCustomValidator.Enabled = false;
			this.CardNameRequiredFieldValidator.Enabled = false;
			this.CardNumberCustomValidator.Enabled = false;
			this.CardPostCodeRequiredFieldValidator.Enabled = false;
			this.CardStartDateCustomValidator.Enabled = false;
			this.BankAccountNameRequiredFieldValidator.Enabled = false;
			this.BankAccountNumberRequiredFieldValidator.Enabled = false;
			this.BankNameRequiredFieldValidator.Enabled = false;
			this.BankSortCodeRequiredFieldValidator.Enabled = false;
			this.BankTransferRequiredFieldValidator.Enabled = false;

			if (this.MethodDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.Methods.Card).ToString()))
			{
				this.CardDetailsPanel.Visible = true;
				this.CardAdminDetailsPanel.Visible = true;

				if (TransferK == 0 && loadRefundTransfer == false)
				{
					this.CardAddressRequiredFieldValidator.Enabled = true;
					this.CardCV2RegularExpressionValidator.Enabled = true;
					this.CardCV2RequiredFieldValidator.Enabled = true;
					this.CardExpiryDateCustomValidator.Enabled = true;
					this.CardNameRequiredFieldValidator.Enabled = true;
					this.CardNumberCustomValidator.Enabled = true;
					this.CardPostCodeRequiredFieldValidator.Enabled = true;
					this.CardStartDateCustomValidator.Enabled = true;
				}
				if (TransferK == 0)
					this.CardAdminDetailsPanel.Visible = false;
			}
			else if (this.MethodDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.Methods.BankTransfer).ToString()))
			{
				this.BankDetailsPanel.Visible = true;

				if (TransferK == 0)
				{
					this.BankAccountNameRequiredFieldValidator.Enabled = true;
					this.BankAccountNumberRequiredFieldValidator.Enabled = true;
					this.BankNameRequiredFieldValidator.Enabled = true;
					this.BankSortCodeRequiredFieldValidator.Enabled = true;
					this.BankTransferRequiredFieldValidator.Enabled = true;
				}
			}
			else if (this.MethodDropDownList.SelectedValue.Equals(Convert.ToInt32(Transfer.Methods.Cheque).ToString()))
			{
				this.ChequeDetailsPanel.Visible = true;
			}
		}

		/// <summary>
		/// Retrieves data from the screen and loads into the CurrentTransfer BOB
		/// </summary>
		private void LoadTransferFromScreen()
		{
			currentTransfer.DuplicateGuid = (Guid)ViewState["DuplicateGuid"];

			// Transfer Details
			if (this.uiPromoterAutoComplete.Value.Length > 0)
			{
				currentTransfer.PromoterK = Convert.ToInt32(this.uiPromoterAutoComplete.Value);
				if (this.UserDropDownList.SelectedValue.Length > 0)
					currentTransfer.UsrK = Convert.ToInt32(this.UserDropDownList.SelectedValue);
			}
			else if (this.uiUsersAutoComplete.Value.Length > 0)
			{
				currentTransfer.UsrK = Convert.ToInt32(this.uiUsersAutoComplete.Value);
			}
			if (this.uiActionUserAutoComplete.Value.Length > 0)
				currentTransfer.ActionUsrK = Convert.ToInt32(this.uiActionUserAutoComplete.Value);
			currentTransfer.Type = (Transfer.TransferTypes)Convert.ToInt32(this.TypeDropDownList.SelectedValue);
			currentTransfer.Method = (Transfer.Methods)Convert.ToInt32(this.MethodDropDownList.SelectedValue);
			currentTransfer.Company = (Transfer.CompanyEnum)Convert.ToInt32(this.CompanyDropDownList.SelectedValue);
			
			currentTransfer.Amount = Math.Abs(Utilities.ConvertMoneyStringToDecimal(this.AmountTextBox.Text));

			if (currentTransfer.Type.Equals(Transfer.TransferTypes.Refund))
				currentTransfer.Amount *= -1;

			//CurrentTransfer.DateTimeCreated = Convert.ToDateTime(this.CreatedDateTextBox.Text);
			//if(this.CompletionDateTextBox.Text.Length >= 5)
			//    CurrentTransfer.DateTimeComplete = Convert.ToDateTime(this.CompletionDateTextBox.Text);
			//CurrentTransfer.DateTimeComplete = this.CompletionDateCal.Date;

			currentTransfer.Notes = this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text;
			if (this.NotesAddOnlyTextBox.AddTextBox.Text.Trim().Length > 0)
			{
				currentTransfer.AddNote(this.NotesAddOnlyTextBox.AddTextBox.Text.Trim(), Usr.Current.NickName);
			}

			// Only editable for new transfers
			if (currentTransfer.K == 0)
			{
				currentTransfer.CardName = this.CardNameTextBox.Text.Trim();
				currentTransfer.CardAddress1 = this.CardAddress1TextBox.Text.Trim();
				currentTransfer.CardPostcode = this.CardPostCodeTextBox.Text.Trim();

				// Card Details
				if (this.CardDetailsPanel.Visible == true && currentTransfer.K == 0)
				{
					currentTransfer.StoreCardEndAndHashAndCardType(this.CardNumberTextBox.Text);
					currentTransfer.CardCV2 = this.CardCV2TextBox.Text.Replace(" ", "");
					if (CardStartDateYearTextBox.Text.Length > 0 && CardStartDateMonthTextBox.Text.Length > 0)
					{
						string CardStartDateYear = (Convert.ToInt32(CardStartDateYearTextBox.Text) <= 80 ? "20" + Convert.ToInt32(CardStartDateYearTextBox.Text).ToString("00") : "19" + Convert.ToInt32(CardStartDateYearTextBox.Text).ToString("00"));
						currentTransfer.CardStart = new DateTime(Convert.ToInt32(CardStartDateYear), Convert.ToInt32(CardStartDateMonthTextBox.Text), 1);
					}
					if (CardExpiryDateYearTextBox.Text.Length > 0 && CardExpiryDateMonthTextBox.Text.Length > 0)
					{
						string CardExpiryDateYear = (Convert.ToInt32(CardExpiryDateYearTextBox.Text) < 80 ? "20" + Convert.ToInt32(CardExpiryDateYearTextBox.Text).ToString("00") : "19" + Convert.ToInt32(CardExpiryDateYearTextBox.Text).ToString("00"));
						currentTransfer.CardExpires = new DateTime(Convert.ToInt32(CardExpiryDateYear), Convert.ToInt32(CardExpiryDateMonthTextBox.Text), 1);
					}
					if (this.CardIssueNumberTextBox.Text.Length > 0)
						currentTransfer.CardIssue = Convert.ToInt32(this.CardIssueNumberTextBox.Text);
					currentTransfer.CardType = (BinRange.Types)Convert.ToInt32(this.CardTypeDropDownList.SelectedValue);

					// Card Admin Details
					currentTransfer.CardResponseAuthCode = this.CardAuthorizationCodeTextBox.Text;
					currentTransfer.CardResponseCv2Avs = this.CardResponseCV2AVSTextBox.Text;
					currentTransfer.CardResponseMessage = this.CardResponseMessageTextBox.Text;
					currentTransfer.CardResponseRespCode = this.CardResponseRespCodeTextBox.Text;
				}

				// Bank Details
				if (this.BankDetailsPanel.Visible == true && currentTransfer.K == 0)
				{
					currentTransfer.BankName = this.BankNameTextBox.Text.Trim();
					currentTransfer.BankAccountName = this.BankAccountNameTextBox.Text.Trim();
					currentTransfer.BankSortCode = this.BankSortCodeTextBox.Text.Trim();
					currentTransfer.BankAccountNumber = this.BankAccountNumberTextBox.Text.Trim();
					currentTransfer.BankTransferReference = this.BankTransferNumberTextBox.Text.Trim();
				}

				// Cheque Details
				if (this.ChequeDetailsPanel.Visible == true && currentTransfer.K == 0)
				{
					currentTransfer.ChequeReferenceNumber = this.ChequeReferenceNumberTextBox.Text.Trim();
				}
			}
			else // Put in tracking notes
			{
				if(currentTransfer.Status != (Transfer.StatusEnum)Convert.ToInt32(this.StatusDropDownList.SelectedValue))
					currentTransfer.AddNote("User: " + Usr.Current.NickName + " changed status from " + currentTransfer.Status.ToString() + " to " + ((Transfer.StatusEnum)Convert.ToInt32(this.StatusDropDownList.SelectedValue)).ToString(), "System");
			}

			currentTransfer.Status = (Transfer.StatusEnum)Convert.ToInt32(this.StatusDropDownList.SelectedValue);
		}
		
		/// <summary>
		/// Disables / replaces specific data entry controls that are not modifiable after a transfer has been saved
		/// </summary>
		private void DisableForPreviouslySaved()
		{
			if (loadRefundTransfer == false)
			{
				this.CreatedDateLabel.Visible = true;
				this.CreatedDateTextBox.Visible = true;
				this.MethodDropDownList.Visible = false;
				this.MethodTextBox.Visible = true;

				Utilities.EnableDisableControls(this.MainPanel, false);
			}

			Utilities.EnableDisableControls(this.CardDetailsPanel, false);
			Utilities.EnableDisableControls(this.CardAdminDetailsPanel, false);
			if (currentTransfer.K > 0)
			{
				Utilities.EnableDisableControls(this.BankDetailsPanel, false);
				this.BankAccountNameTextBox.Visible = false;
				this.BankAccountNumberTextBox.Visible = false;
				this.BankNameTextBox.Visible = false;
				this.BankSortCodeTextBox.Visible = false;
				this.BankTransferNumberTextBox.Visible = false;
				this.ChequeReferenceNumberTextBox.Visible = false;

				this.BankAccountNameValueLabel.Visible = true;
				this.BankAccountNumberValueLabel.Visible = true;
				this.BankNameValueLabel.Visible = true;
				this.BankSortCodeValueLabel.Visible = true;
				this.BankTransferNumberValueLabel.Visible = true;
				this.ChequeReferenceNumberValueLabel.Visible = true;
			}

			this.uiPromoterAutoComplete.Visible = false;
			this.uiUsersAutoComplete.Visible = false;
			this.UserDropDownList.Visible = false;
			this.uiActionUserAutoComplete.Visible = false;
			this.TypeDropDownList.Visible = false;
			this.CardTypeDropDownList.Visible = false;

			this.RefundHyperLink.Enabled = true;
			this.PromoterValueLabel.Visible = true;
			this.UserValueLabel.Visible = true;
			this.ActionUserValueLabel.Visible = true;
			this.TypeTextBox.Visible = true;
			this.CardTypeTextBox.Visible = true;

			this.StatusLabel.Visible = true;

			// Replace TextBoxes with Labels for text that may exceed TextBox width
			this.CardAddress1TextBox.Visible = false;
			this.CardAuthorizationCodeTextBox.Visible = false;
			this.CardNameTextBox.Visible = false;
			this.CardResponseCV2AVSTextBox.Visible = false;
			this.CardResponseMessageTextBox.Visible = false;
			this.CardResponseRespCodeTextBox.Visible = false;			

			this.CardAddress1ValueLabel.Visible = true;
			this.CardAuthorizationCodeValueLabel.Visible = true;
			this.CardNameValueLabel.Visible = true;
			this.CardResponseCV2AVSValueLabel.Visible = true;
			this.CardResponseMessageValueLabel.Visible = true;
			this.CardResponseRespCodeValueLabel.Visible = true;
			
			if (currentTransfer.CardStart.Equals(DateTime.MinValue))
			{
				this.CardStartDateLabel.Visible = false;
				this.CardStartDateDividerLabel.Visible = false;
			}

			if (currentTransfer.CardIssue == 0)
			{
				this.CardIssueNumberLabel.Visible = false;
			}

			if (currentTransfer.K == 0 || currentTransfer.Status.Equals(Transfer.StatusEnum.Pending))
			{
				StatusDropDownList.Visible = true;
				StatusTextBox.Visible = false;
			}
			// Lock status if its not Pending
			else
			{
				StatusDropDownList.Visible = false;
				StatusTextBox.Visible = true;
			}

			if (currentTransfer.DateTimeComplete > DateTime.MinValue)
			{
				this.CompletionDateTextBox.Visible = true;
				this.CompletionDateLabel.Visible = true;
			}

			// Do not lock the following controls
			//this.NotesTextBox.ReadOnly = false;
			//this.NotesTextBox.CssClass = "";

			//            this.CompletionDateTextBox.ReadOnly = false;
			if (currentTransfer.Status.Equals(Transfer.StatusEnum.Pending))
			{
				this.StatusDropDownList.Enabled = true;
			}
			// Allow refund for successful transfers
			if (currentTransfer.Status.Equals(Transfer.StatusEnum.Success) && currentTransfer.Type.Equals(Transfer.TransferTypes.Payment))
			{
				var amountAvailableForRefund = currentTransfer.AmountRemaining();
				this.RefundAmountTextBox.Text = amountAvailableForRefund.ToString("c");
				this.RefundAmountTextBox.Visible = true;
				this.RefundButton.Visible = true;
				this.RefundAmountLabel.Visible = true;
				if (amountAvailableForRefund <= 0)
				{
					this.RefundAmountTextBox.Enabled = false;
					this.RefundButton.Enabled = false;
				}
				//this.RefundButton.Attributes.Clear();
				//string refundConfirmString = "Are you sure you want to refund this Transfer? Are you sure you have entered the correct refund amount in the text box beside this button?";
				//if (this.CurrentTransfer.Method.Equals(Transfer.Methods.Card))
				//{
				//    refundConfirmString += " NOTE: This will automatically refund the amount via SecPay to the User's card.";
				//}

				//this.RefundButton.Attributes.Add("onclick", "javascript:return confirm('" + refundConfirmString + "')");
			}

			this.DownloadButton.Visible = currentTransfer.K > 0;

			this.uiPromoterAutoComplete.AutoPostBack = false;
			this.uiPromoterAutoComplete.ValueChanged -= new EventHandler(uiPromoterAutoComplete_ValueChanged);
			
			DisableValidatorsForNonEditableControls();
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
			this.AmountRequiredFieldValidator.Enabled = false;

			// Disable card validators
			this.CardAddressRequiredFieldValidator.Enabled = false;
			this.CardCV2RegularExpressionValidator.Enabled = false;
			this.CardCV2RequiredFieldValidator.Enabled = false;
			this.CardExpiryDateCustomValidator.Enabled = false;
			this.CardNameRequiredFieldValidator.Enabled = false;
			this.CardNumberCustomValidator.Enabled = false;
			this.CardPostCodeRequiredFieldValidator.Enabled = false;
			this.CardStartDateCustomValidator.Enabled = false;

			// Disable bank validators
			this.BankAccountNameRequiredFieldValidator.Enabled = false;
			this.BankAccountNumberRequiredFieldValidator.Enabled = false;
			this.BankNameRequiredFieldValidator.Enabled = false;
			this.BankSortCodeRequiredFieldValidator.Enabled = false;
			this.BankTransferRequiredFieldValidator.Enabled = false;
		}

		/// <summary>
		/// Loads screen controls with data from a saved transfer 
		/// </summary>
		private void LoadScreenFromTransfer()
		{
			if (currentTransfer.K > 0 || loadRefundTransfer == true || redoFailedTransfer == true)
			{
				// Transaction Details
				if (currentTransfer.K > 0)
				{
					this.TransferKValueLabel.Visible = true;
					this.TransferKLabel.Visible = true;
				}
				else
				{
					this.TransferKValueLabel.Visible = false;
					this.TransferKLabel.Visible = false;
				}
				this.TransferKValueLabel.Text = Utilities.LinkNewWindow(currentTransfer.UrlReport(), currentTransfer.K.ToString());
				if (currentTransfer.Promoter != null)
				{
					this.uiPromoterAutoComplete.Value = currentTransfer.Promoter.K.ToString();
					this.uiPromoterAutoComplete.Text = currentTransfer.Promoter.Name;
					this.PromoterValueLabel.Text = currentTransfer.Promoter.Link();
					ViewState["PromoterK"] = currentTransfer.Promoter.K;
					SetupUserDropDownList();
				}
				if (currentTransfer.Usr != null)
				{
					this.uiUsersAutoComplete.Text = currentTransfer.Usr.NickName;
					this.uiUsersAutoComplete.Value = currentTransfer.Usr.K.ToString();
					this.UserDropDownList.SelectedValue = currentTransfer.Usr.K.ToString();
					this.UserValueLabel.Text = currentTransfer.Usr.Link();
				}
				if (currentTransfer.ActionUsr != null)
				{
					this.uiActionUserAutoComplete.Value = currentTransfer.ActionUsr.K.ToString();
					this.uiActionUserAutoComplete.Text = currentTransfer.ActionUsr.Name;
					this.ActionUserValueLabel.Text = currentTransfer.ActionUsr.Link();
				}
				if (loadRefundTransfer == true)
				{
					this.uiActionUserAutoComplete.Value = Usr.Current.K.ToString();
					this.uiActionUserAutoComplete.Text = Usr.Current.Name;
					this.ActionUserValueLabel.Text = Usr.Current.Link();
				}
				this.TypeDropDownList.SelectedValue = Convert.ToInt32(currentTransfer.Type).ToString();
				this.TypeTextBox.Text = currentTransfer.Type.ToString();
				this.MethodDropDownList.SelectedValue = Convert.ToInt32(currentTransfer.Method).ToString();
				this.CompanyDropDownList.SelectedValue = Convert.ToInt32(currentTransfer.Company).ToString();
				this.MethodTextBox.Text = Utilities.CamelCaseToString(currentTransfer.Method.ToString());

				this.SetupStatusDropDownList();

				this.StatusDropDownList.SelectedValue = Convert.ToInt32(currentTransfer.Status).ToString();
				this.StatusTextBox.Text = currentTransfer.Status.ToString();

				this.AmountTextBox.Text = currentTransfer.Amount.ToString("c");
				if (currentTransfer.DateTimeCreated > DateTime.MinValue)
					this.CreatedDateTextBox.Text = currentTransfer.DateTimeCreated.ToString("HH:mm dd/MM/yy");

				if (currentTransfer.DateTimeComplete > DateTime.MinValue)
				{
					this.CompletionDateTextBox.Text = currentTransfer.DateTimeComplete.ToString("HH:mm dd/MM/yy");
					this.CompletionDateLabel.Visible = true;
					this.CompletionDateTextBox.Visible = true;
				}
				this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text = currentTransfer.Notes;

				// Card Details
				this.CardAddress1TextBox.Text = currentTransfer.CardAddress1;
				this.CardAddress1ValueLabel.Text = currentTransfer.CardAddress1;
				this.CardNameTextBox.Text = currentTransfer.CardName;
				this.CardNameValueLabel.Text = currentTransfer.CardName;
				this.CardPostCodeTextBox.Text = currentTransfer.CardPostcode;
				if (currentTransfer.CardNumberEnd.Length > 0)
					this.CardNumberTextBox.Text = currentTransfer.CardNumber;
				this.CardCV2TextBox.Text = currentTransfer.CardCV2;
				if (currentTransfer.CardStart > DateTime.MinValue)
				{
					this.CardStartDateMonthTextBox.Text = currentTransfer.CardStart.Month.ToString("00");
					this.CardStartDateYearTextBox.Text = (currentTransfer.CardStart.Year % 100).ToString("00");
				}
				if (currentTransfer.CardExpires > DateTime.MinValue)
				{
					this.CardExpiryDateDividerLabel.Visible = true;
					this.CardExpiryDateMonthTextBox.Text = currentTransfer.CardExpires.Month.ToString("00");
					this.CardExpiryDateYearTextBox.Text = (currentTransfer.CardExpires.Year % 100).ToString("00");
				}
				else
				{
					this.CardExpiryDateDividerLabel.Visible = false;
				}
				if (currentTransfer.CardIssue > 0)
					this.CardIssueNumberTextBox.Text = currentTransfer.CardIssue.ToString();
				this.CardTypeDropDownList.SelectedValue = Convert.ToInt32(currentTransfer.CardType).ToString();
				this.CardTypeTextBox.Text = Utilities.CamelCaseToString(currentTransfer.CardType.ToString());

				// Card Admin Details
				this.CardAuthorizationCodeTextBox.Text = currentTransfer.CardResponseAuthCode;
				this.CardAuthorizationCodeValueLabel.Text = currentTransfer.CardResponseAuthCode;
				this.CardResponseCV2AVSTextBox.Text = currentTransfer.CardResponseCv2Avs;
				this.CardResponseCV2AVSValueLabel.Text = currentTransfer.CardResponseCv2Avs;
				this.CardResponseMessageTextBox.Text = currentTransfer.CardResponseMessage;
				this.CardResponseMessageValueLabel.Text = currentTransfer.CardResponseMessage;
				this.CardResponseRespCodeTextBox.Text = currentTransfer.CardResponseRespCode;
				this.CardResponseRespCodeValueLabel.Text = currentTransfer.CardResponseRespCode;

				// Bank Details
				this.BankAccountNameTextBox.Text = currentTransfer.BankAccountName;
				this.BankAccountNameValueLabel.Text = currentTransfer.BankAccountName;
				this.BankAccountNumberTextBox.Text = currentTransfer.BankAccountNumber;
				this.BankAccountNumberValueLabel.Text = currentTransfer.BankAccountNumber;
				this.BankNameTextBox.Text = currentTransfer.BankName;
				this.BankNameValueLabel.Text = currentTransfer.BankName;
				this.BankSortCodeTextBox.Text = currentTransfer.BankSortCode;
				this.BankSortCodeValueLabel.Text = currentTransfer.BankSortCode;
				this.BankTransferNumberTextBox.Text = currentTransfer.BankTransferReference;
				this.BankTransferNumberValueLabel.Text = currentTransfer.BankTransferReference;

				// Cheque Details
				this.ChequeReferenceNumberTextBox.Text = currentTransfer.ChequeReferenceNumber;
				this.ChequeReferenceNumberValueLabel.Text = currentTransfer.ChequeReferenceNumber;

				if (currentTransfer.TransferRefundedK > 0)
				{
					RefundHyperLink.Visible = true;
					RefundHyperLink.NavigateUrl = currentTransfer.UrlAdminTransferRefunded();
					RefundHyperLink.Text = "View Transfer #" + currentTransfer.TransferRefundedK.ToString();
					RefundLabel.Visible = true;
				}

				LoadInvoiceTransfers();
				LoadRefundTransfers();

				if ((currentTransfer.K > 0 || loadRefundTransfer == true) && redoFailedTransfer == false)
					DisableForPreviouslySaved();
			}
		}

		/// <summary>
		/// Setups the screen screen controls for a new transfer for a saved invoice 
		/// </summary>
		private void LoadScreenFromInvoice(Invoice invoice)
		{
			//this.uiPromoterAutoComplete.AutoPostBack = false;
			this.uiPromoterAutoComplete.ValueChanged -= new EventHandler(uiPromoterAutoComplete_ValueChanged);

			// Transaction Details
			this.TransferKValueLabel.Text = Utilities.LinkNewWindow(currentTransfer.UrlReport(), currentTransfer.K.ToString());
			if (invoice.Promoter != null)
			{
				this.uiPromoterAutoComplete.Value = invoice.Promoter.K.ToString();
				this.uiPromoterAutoComplete.Text = invoice.Promoter.Name;
				this.PromoterValueLabel.Text = invoice.Promoter.Link();

				ViewState["PromoterK"] = invoice.Promoter.K;
				// Cant change the promoter for a transfer that will be tied to a specific invoice
				this.uiPromoterAutoComplete.Visible = false;
				this.PromoterValueLabel.Visible = true;

				SetupUserDropDownList();
                if (this.UserDropDownList.Items.Count > 0)
                {
                    this.uiUsersAutoComplete.Visible = false;
					this.uiUsersAutoComplete.Visible = true;
                }
			}
			if (invoice.Usr != null)
			{
				this.UserDropDownList.SelectedValue = invoice.Usr.K.ToString();
				this.UserValueLabel.Text = invoice.Usr.Link();
			}
			
			if (invoice.Type.Equals(Invoice.Types.Invoice))
			{
				this.TypeDropDownList.SelectedValue = Convert.ToInt32(Transfer.TransferTypes.Payment).ToString();
				this.TypeTextBox.Text = Transfer.TransferTypes.Payment.ToString();
				this.BankTransferNumberTextBox.Text = "IN" + invoice.K.ToString();
			}
			else
			{
				this.TypeDropDownList.SelectedValue = Convert.ToInt32(Transfer.TransferTypes.Refund).ToString();
				this.TypeTextBox.Text = Transfer.TransferTypes.Refund.ToString();
				this.BankTransferNumberTextBox.Text = "CR" + invoice.K.ToString();
			}

			this.AmountTextBox.Text = invoice.AmountDue.ToString("c");
	
			//this.uiPromoterAutoComplete.AutoPostBack = true;
			this.uiPromoterAutoComplete.ValueChanged += new EventHandler(uiPromoterAutoComplete_ValueChanged);
		}

		private void LoadInvoiceTransfers()
		{
			if (currentTransfer.K > 0)
			{
				if (InvoiceTransferDataHolderList.Count > 0)
				{
					this.InvoiceTransferPanel.Visible = true;

					if (currentTransfer.Type == Transfer.TransferTypes.Refund)
					{
						this.InvoiceCreditLabel.Text = "Credits";
						this.InvoiceTransferGridView.Columns[0].HeaderText = "Credit K";
						this.InvoiceTransferGridView.Columns[2].HeaderText = "View&nbsp;Credit";
						((HyperLinkField)this.InvoiceTransferGridView.Columns[2]).DataNavigateUrlFormatString = "/admin/creditscreen/K-{0}";
						((HyperLinkField)this.InvoiceTransferGridView.Columns[2]).DataTextFormatString = "Credit&nbsp;#{0}";
					}

					this.InvoiceTransferGridView.DataSource = InvoiceTransferDataHolderList;
					this.InvoiceTransferGridView.DataBind();
				}
			}
		}

		private void LoadRefundTransfers()
		{
			if (currentTransfer.K > 0 && RefundTransferDataHolderList.Count > 0 && currentTransfer.Type == Transfer.TransferTypes.Payment)
			{
				this.RefundTransferPanel.Visible = true;

				this.RefundTransferGridView.DataSource = RefundTransferDataHolderList;
				this.RefundTransferGridView.DataBind();
			}
		}
		#endregion

		#region Custom Validations
		#region CardNumVal
		public void CardNumVal(object o, ServerValidateEventArgs e)
		{
			Regex r = new Regex("[^0-9]*");
			this.CardNumberTextBox.Text = r.Replace(CardNumberTextBox.Text, "");
			e.IsValid = CardNumberTextBox.Text.Length == 16 || CardNumberTextBox.Text.Length == 18 || CardNumberTextBox.Text.Length == 19 || CardNumberTextBox.Text.Length == 20;
		}
		#endregion
		#region StartDateVal
		public void StartDateVal(object o, ServerValidateEventArgs e)
		{
			if (this.CardStartDateYearTextBox.Text.Length == 0 && CardStartDateMonthTextBox.Text.Length == 0)
			{
				// Start Date is not required
				e.IsValid = true;
				return;
			}
			try
			{
				int year = int.Parse(CardStartDateYearTextBox.Text.Trim());
				if (year > 80)
					year += 1900;
				else
					year += 2000;
				DateTime startDate = new DateTime(year, int.Parse(CardStartDateMonthTextBox.Text.Trim()), 1);
				e.IsValid = startDate < DateTime.Now;
			}
			catch
			{
				e.IsValid = false;
			}
		}
		#endregion
		#region ExpiryDateVal
		public void ExpiryDateVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				int year = int.Parse(this.CardExpiryDateYearTextBox.Text.Trim());
				if (year > 80)
					year += 1900;
				else
					year += 2000;
				DateTime endDate = new DateTime(year, int.Parse(CardExpiryDateMonthTextBox.Text.Trim()), 1);
				endDate = endDate.AddMonths(1);
				e.IsValid = endDate > DateTime.Now;
			}
			catch
			{
				e.IsValid = false;
			}
		}
		#endregion
		#region PromoterAndUserVal
		public void PromoterAndUserVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = this.uiPromoterAutoComplete.Value.Length > 0 || this.uiUsersAutoComplete.Value.Length > 0;
		}
		#endregion
		#endregion
	}
}
