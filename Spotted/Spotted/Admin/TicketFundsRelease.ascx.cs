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
using Common;

namespace Spotted.Admin
{
	public partial class TicketFundsRelease : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//if (!Usr.Current.IsSuperAdmin)
			//    throw new Exception("You do not have permission to view this page.");

			if (!this.IsPostBack)
			{
                if (this.ContainerPage.Url["promoterk"].Exists && this.ContainerPage.Url["promoterk"].IsInt)
				{
					this.uiPromotersAutoComplete.Value = this.ContainerPage.Url["promoterk"].Value;
					this.uiPromotersAutoComplete.Text = new Promoter(Convert.ToInt32(this.ContainerPage.Url["promoterk"].Value)).Name;
				}

				ViewState["DuplicateGuid"] = Guid.NewGuid();
				SetupStatusDropDownList();
				SetupSalesUsrDropDownList();



				SalesPersonQuickButtonTable.Visible = Usr.Current.IsSalesPerson && !Usr.Current.IsSuperAdmin;
				SearchTable.Visible = Usr.Current.IsSuperAdmin;
				CreateCampaignCreditsButton.Attributes["onclick"] = "if(confirm('Are you sure you want to use ticket funds to buy campaign credits?')){__doPostBack('" + CreateCampaignCreditsButton.UniqueID + "','');return false;}else{return false;};";
				PayFundsToPromoterBankAccountButton.Attributes["onclick"] = "if(confirm('Are you sure you want to pay funds to the promoter bank account? This will add it to the queue awaiting batch processing.')){__doPostBack('" + PayFundsToPromoterBankAccountButton.UniqueID + "','');return false;}else{return false;};";
				DisableControlsBasedOnPermissions();

				try
				{
					if (Usr.Current.IsSalesPerson)
					{
						this.SalesUsrDropDownList.SelectedValue = Usr.Current.K.ToString();
						MyPromotersRadioButton.Checked = true;
					}
				}
				catch { }
				Search();				
			}
			//ReleaseFundsOptionsDropDownListSetup();
		}

		#region Enums
		public enum ReleaseFundsOptions
		{
			CreateReleaseTransfer = 1,
			AssignExistingTransferK = 2
		}

		public enum FundsSelectionEnum
		{
			AwaitingFundsRelease = 1,
			LockedFunds = 2,
			All = 3
		}
		#endregion

		#region Variables
		FundsSelectionEnum FundsSelection = FundsSelectionEnum.AwaitingFundsRelease;
		int PageNumber = 1;
		//int RecordsPerPage = 20;
		int EventK = 0;
		int PromoterK = 0;
		#endregion

		#region Properties
		#region CurrentTicketPromoterEvent
		public TicketPromoterEvent CurrentTicketPromoterEvent
		{
			get
			{
				if (currentTicketPromoterEvent == null && EventK > 0 && PromoterK > 0)
				{
					currentTicketPromoterEvent = new TicketPromoterEvent(PromoterK, EventK);
				}
				return currentTicketPromoterEvent;
			}
			set
			{
				currentTicketPromoterEvent = value;
				if (currentTicketPromoterEvent != null)
				{
					this.EventK = currentTicketPromoterEvent.EventK;
					this.PromoterK = currentTicketPromoterEvent.PromoterK;
				}
				else
				{
					this.EventK = 0;
					this.PromoterK = 0;
				}
			}
		}
		TicketPromoterEvent currentTicketPromoterEvent;
		#endregion
		#endregion

		#region ViewState
		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["PageNumber"] = PageNumber;
			this.ViewState["EventK"] = EventK;
			this.ViewState["PromoterK"] = PromoterK;
			this.ViewState["FundsSelection"] = FundsSelection;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["PageNumber"] != null) PageNumber = (int)this.ViewState["PageNumber"];
			if (this.ViewState["EventK"] != null) EventK = (int)this.ViewState["EventK"];
			if (this.ViewState["PromoterK"] != null) PromoterK = (int)this.ViewState["PromoterK"];
			if (this.ViewState["FundsSelection"] != null) FundsSelection = (FundsSelectionEnum)this.ViewState["FundsSelection"];
		}
		#endregion
		#endregion

		#region Methods
		private void DisableControlsBasedOnPermissions()
		{
			if (!Usr.Current.IsSuperAdmin)
			{
				this.OverrideLockButton.Enabled = false;
				this.ManualLockButton.Enabled = false;
				//this.PayFundsToPromoterBankAccountButton.Disabled = true;
				this.RerunFundsLocksChecksButton.Enabled = false;
				this.ManualLockNoteTextBox.Enabled = false;
				this.OverrideLockNoteTextBox.Enabled = false;
			}
		}
		//#region ReleaseFundsOptionsDropDownListSetup
		//public void ReleaseFundsOptionsDropDownListSetup()
		//{
		//    string releaseFundsOptionsJavascript = "this.options[this.selectedIndex].value == "
		//                                            + Convert.ToInt32(ReleaseFundsOptions.AssignExistingTransferK).ToString() + " ? document.getElementById('" + ReleaseFundsTransferKTextBox.UniqueID.Replace("$", "_") + "').style.display = '' : document.getElementById('" + ReleaseFundsTransferKTextBox.UniqueID.Replace("$", "_") + "').style.display = 'none';"
		//                                            + "this.options[this.selectedIndex].value == "
		//                                            + Convert.ToInt32(ReleaseFundsOptions.CreateReleaseTransfer).ToString() + " ? document.getElementById('" + TransferMethodsDropDownList.UniqueID.Replace("$", "_") + "').style.display = '' : document.getElementById('" + TransferMethodsDropDownList.UniqueID.Replace("$", "_") + "').style.display = 'none';";

		//    if (!this.IsPostBack)
		//    {
		//        ReleaseFundsOptionsDropDownList.Items.Clear();
		//        //ReleaseFundsOptionsDropDownList.Items.Add(new ListItem("<Release Funds Options>", "-1"));
		//        ReleaseFundsOptionsDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(ReleaseFundsOptions.AssignExistingTransferK.ToString()), Convert.ToInt32(ReleaseFundsOptions.AssignExistingTransferK).ToString()));
		//        ReleaseFundsOptionsDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(ReleaseFundsOptions.CreateReleaseTransfer.ToString()), Convert.ToInt32(ReleaseFundsOptions.CreateReleaseTransfer).ToString()));
		//        ReleaseFundsOptionsDropDownList.Attributes.Remove("onchange");
		//        ReleaseFundsOptionsDropDownList.Attributes.Add("onchange", "javascript:" + releaseFundsOptionsJavascript);
		//    }
		//    ReleaseFundsOptionsJavascriptLabel.Visible = true;
		//    ReleaseFundsOptionsJavascriptLabel.Text = "<script type=\"text/javascript\">" + releaseFundsOptionsJavascript.Replace("this.", "document.getElementById('" + ReleaseFundsOptionsDropDownList.UniqueID.Replace("$", "_") + "').") + "</script>";
		//}
		//#endregion

		//#region TransferTypesDropDownListSetup
		//public void TransferMethodsDropDownListSetup()
		//{
		//    this.TransferMethodsDropDownList.Items.Clear();
		//    this.TransferMethodsDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(Transfer.Methods.BankTransfer.ToString()), Convert.ToInt32(Transfer.Methods.BankTransfer).ToString()));
		//    this.TransferMethodsDropDownList.Items.Add(new ListItem(Transfer.Methods.Cheque.ToString(), Convert.ToInt32(Transfer.Methods.Cheque).ToString()));
		//}
		//#endregion

		#region GetTicketPromoterEvents
		public void GetTicketPromoterEvents()
		{
			Query ticketPromoterEventFundsQuery = new Query();

			List<Q> QueryConditionList = new List<Q>();

			ticketPromoterEventFundsQuery.TableElement = new Join(TicketPromoterEvent.Columns.EventK, Event.Columns.K);
			ticketPromoterEventFundsQuery.OrderBy = new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending);

			if (this.uiPromotersAutoComplete.Value != null && !uiPromotersAutoComplete.Value.Equals(""))
				QueryConditionList.Add(new Q(TicketPromoterEvent.Columns.PromoterK, Convert.ToInt32(uiPromotersAutoComplete.Value)));
			if (this.StatusDropDownList.SelectedValue != "")
			{
				if (this.StatusDropDownList.SelectedValue == "Locked")
					QueryConditionList.Add(TicketPromoterEvent.LockedFundsQ);
				else if (this.StatusDropDownList.SelectedValue == "Funds Released")
					QueryConditionList.Add(TicketPromoterEvent.FundsReleasedQ);
				else if (this.StatusDropDownList.SelectedValue == "Funds Not Released")
					QueryConditionList.Add(TicketPromoterEvent.AwaitingFundsReleaseQ);
				else if (this.StatusDropDownList.SelectedValue == "Funds Released With Money Remaining")
				{
					QueryConditionList.Add(TicketPromoterEvent.FundsReleasedQ);
					ticketPromoterEventFundsQuery.TableElement = new Join(ticketPromoterEventFundsQuery.TableElement, Transfer.Columns.K, TicketPromoterEvent.Columns.FundsTransferK);
					QueryConditionList.Add(new Q(Transfer.Columns.IsFullyApplied, false));
				}
				else if (this.StatusDropDownList.SelectedValue == "No Plus Account")
				{
					QueryConditionList.Add(new Or(new Q(Promoter.Columns.EnableTickets, QueryOperator.IsNull, null),
												  new Q(Promoter.Columns.EnableTickets, false)));					
				}
			}
			if (this.SalesUsrDropDownList.SelectedValue != "")
				QueryConditionList.Add(new Q(Promoter.Columns.SalesUsrK, Convert.ToInt32(SalesUsrDropDownList.SelectedValue)));
			if (this.SalesUsrDropDownList.SelectedValue != "" || this.StatusDropDownList.SelectedValue == "No Plus Account")
				ticketPromoterEventFundsQuery.TableElement = new Join(ticketPromoterEventFundsQuery.TableElement, Promoter.Columns.K, TicketPromoterEvent.Columns.PromoterK);
			if (this.FromDateCal.Date != DateTime.MinValue)
				QueryConditionList.Add(new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
			if (this.ToDateCal.Date != DateTime.MinValue)
				QueryConditionList.Add(new Q(Event.Columns.DateTime, QueryOperator.LessThanOrEqualTo, this.ToDateCal.Date));
            

			ticketPromoterEventFundsQuery.QueryCondition = new And(QueryConditionList.ToArray());      

            //ticketPromoterEventFundsQuery.Paging.RecordsPerPage = this.RecordsPerPage;
            //ticketPromoterEventFundsQuery.Paging.RequestedPage = this.PageNumber;
			//ticketPromoterEventFundsQuery.TopRecords = (this.PageNumber * this.RecordsPerPage) + 1;

			var ticketPromoterEventSet = new TicketPromoterEventSet(ticketPromoterEventFundsQuery);

			

			TicketPromoterEvent.CalculateTicketsAndRunFundLocksChecksAndReleaseFunds(ticketPromoterEventSet);
			
			ticketPromoterEventSet.Reset();
			List<TicketPromoterEvent> ticketPromoterEvents = ticketPromoterEventSet.ToList().FindAll(tpe => tpe.TotalFunds > 0 || this.ShowZeroMoneyTicketRunsCheckBox.Checked);

			if (this.StatusDropDownList.SelectedValue == "Funds Released With Money Remaining")
			{
				for (int i = ticketPromoterEvents.Count - 1; i >= 0; i--)
				{
					if (ticketPromoterEvents[i].FundsTransfer != null && ticketPromoterEvents[i].FundsTransfer.AmountRemaining() == 0)
						ticketPromoterEvents.RemoveAt(i);
				}
			}

			this.TicketPromoterEventFundsGridView.DataSource = ticketPromoterEvents; 
			this.TicketPromoterEventFundsGridView.DataBind();

			TotalsP.Visible = ticketPromoterEvents.Count > 0;
			if (ticketPromoterEvents.Count > 0)
			{
				decimal totalFunds = 0;
				decimal totalBookingFees = 0;
				int totalTickets = 0;
				int totalRefundTickets = 0;
				foreach (TicketPromoterEvent tpe in ticketPromoterEvents)
				{
					totalFunds += tpe.TotalFunds;
                    totalBookingFees += tpe.TotalBookingFees;
					totalTickets += tpe.SoldTickets;
					totalRefundTickets += tpe.CancelledTickets;
				}
				this.TotalFundsLabel.Text = totalFunds.ToString("c");
                this.TotalBookingFeesLabel.Text = totalBookingFees.ToString("c");
				this.TotalTicketsLabel.Text = totalTickets.ToString() + (totalRefundTickets > 0 ? " (" + totalRefundTickets.ToString() + ")" : "");
			}

			//this.SelectAllTicketFundsLinkButton.Enabled = !FundsSelection.Equals(FundsSelectionEnum.All);
			//this.SelectAwaitingFundsReleaseLinkButton.Enabled = !FundsSelection.Equals(FundsSelectionEnum.AwaitingFundsRelease);
			//this.SelectLockedFundsLinkButton.Enabled = !FundsSelection.Equals(FundsSelectionEnum.LockedFunds);

			//H1Title.InnerHtml = Utilities.CamelCaseToString(FundsSelection.ToString());

			//this.NextPageLinkButton.Enabled = ticketPromoterEventSet.Paging.ShowNextPageLink;
			//this.PrevPageLinkButton.Enabled = ticketPromoterEventSet.Paging.ShowPrevPageLink;
		}
		#endregion

		#region SetupStatusDropDownList
		public void SetupStatusDropDownList()
		{
			this.StatusDropDownList.Items.Clear();
			this.StatusDropDownList.Items.Add(new ListItem("", ""));
			this.StatusDropDownList.Items.Add(new ListItem("Funds Not Released", "Funds Not Released"));
			this.StatusDropDownList.Items.Add(new ListItem("Funds Released", "Funds Released"));
			this.StatusDropDownList.Items.Add(new ListItem("Funds Released With Money Remaining", "Funds Released With Money Remaining"));
			this.StatusDropDownList.Items.Add(new ListItem("Locked", "Locked"));
            this.StatusDropDownList.Items.Add(new ListItem("No Plus Account", "No Plus Account"));
		}
		#endregion

		#region SetupSalesUsrDropDownList
		public void SetupSalesUsrDropDownList()
		{
			this.SalesUsrDropDownList.Items.Clear();
			
			UsrSet salesUsrs = Usr.GetCurrentSalesUsrsNameAndK();
			this.SalesUsrDropDownList.Items.Add(new ListItem("", ""));
			foreach (Usr salesUsr in salesUsrs)
			{
				this.SalesUsrDropDownList.Items.Add(new ListItem(salesUsr.NickName, salesUsr.K.ToString()));
			}
		}
		#endregion

		#region LoadTicketPromoterEventToScreen
		public void LoadTicketPromoterEventToScreen()
		{
			TicketPromoterEventDetailsPanel.Visible = CurrentTicketPromoterEvent != null;

			if (CurrentTicketPromoterEvent != null)
			{
				CurrentTicketPromoterEvent.CalculateTotalFundsAndVat();
				CurrentTicketPromoterEvent.Update();

				if(CurrentTicketPromoterEvent.FundsReleased)
					H1EditTitle.InnerHtml = "View Funds Release";
				else
					H1EditTitle.InnerHtml = "Edit Funds Release";

				this.EventLabel.Text = CurrentTicketPromoterEvent.Event.LinkFriendlyNameNewWindow;
				this.PromoterLabel.Text = CurrentTicketPromoterEvent.Promoter.LinkNewWindow();
				this.TicketRunsLabel.Text = "";
				
				foreach (TicketRun ticketRun in CurrentTicketPromoterEvent.TicketRuns)
				{
					this.TicketRunsLabel.Text += "<nobr>" + ticketRun.SoldTickets.ToString() + (ticketRun.CancelledTicketQuantity > 0 ? " (" + ticketRun.CancelledTicketQuantity.ToString() + ")" : "") + " x " + ticketRun.LinkPriceBrandName + (ticketRun.SoldTickets > 0 ? " <small>" + Utilities.Link(ticketRun.UrlAdminTickets, "[Tickets]") + "</small>" : "") + "</nobr><br>";
				}
				this.TicketRunsLabel.Text = this.TicketRunsLabel.Text.Substring(0, this.TicketRunsLabel.Text.Length-4);
				this.AmountLabel.Text = 
					Utilities.MoneyToHTML(CurrentTicketPromoterEvent.GrossFunds) + " / " + 
					Utilities.MoneyToHTML(CurrentTicketPromoterEvent.TotalFundsReleased) + " / " + 
					Utilities.MoneyToHTML(CurrentTicketPromoterEvent.TotalFunds);
				this.BookingFeesLabel.Text = Utilities.MoneyToHTML(CurrentTicketPromoterEvent.GrossBookingFees) + " / " + Utilities.MoneyToHTML(CurrentTicketPromoterEvent.TotalBookingFeesReleased) + " / " + Utilities.MoneyToHTML(CurrentTicketPromoterEvent.TotalBookingFees);
				this.TotalVatLabel.Text = Utilities.MoneyToHTML(CurrentTicketPromoterEvent.GrossVat) + " / " + Utilities.MoneyToHTML(CurrentTicketPromoterEvent.TotalVatReleased) + " / " + Utilities.MoneyToHTML(CurrentTicketPromoterEvent.TotalVat);
				if (CurrentTicketPromoterEvent.FundsLockTotalFundsDontMatch)
					this.AmountLabel.Text += Utilities.IconHtml(Utilities.Icon.Cross);

                this.PromoterTicketsEnabledLabel.Text = Utilities.TickCrossHtml(CurrentTicketPromoterEvent.Promoter.EnableTickets, CurrentTicketPromoterEvent.FundsLockManual, CurrentTicketPromoterEvent.FundsLockOverride);
                this.ValidBrowserGuidsLabel.Text = Utilities.TickLockHtml(!CurrentTicketPromoterEvent.FundsLockFraudGuid, CurrentTicketPromoterEvent.FundsLockManual, CurrentTicketPromoterEvent.FundsLockOverride);
                this.ValidIPCountryLabel.Text = Utilities.TickLockHtml(CurrentTicketPromoterEvent.FundsLockFraudIpCountry == 0, CurrentTicketPromoterEvent.FundsLockManual, CurrentTicketPromoterEvent.FundsLockOverride);
                this.ValidIPDuplicateLabel.Text = Utilities.TickLockHtml(!CurrentTicketPromoterEvent.FundsLockFraudIpDuplicate, CurrentTicketPromoterEvent.FundsLockManual, CurrentTicketPromoterEvent.FundsLockOverride);
                this.ValidUserResponsesLabel.Text = Utilities.TickLockHtml(!CurrentTicketPromoterEvent.FundsLockUsrResponses, CurrentTicketPromoterEvent.FundsLockManual, CurrentTicketPromoterEvent.FundsLockOverride);
                this.EventEndedLabel.Text = Utilities.TickCrossHtml(CurrentTicketPromoterEvent.Event.DateTime < DateTime.Today, CurrentTicketPromoterEvent.FundsLockManual, CurrentTicketPromoterEvent.FundsLockOverride);
                
				this.FundsReleasedLabel.Text = Utilities.IconHtml(Utilities.Icon.Tick);
                this.TicketPaymentMadeLabel.Text = Utilities.IconHtml(Utilities.Icon.Tick);
                this.TicketPaymentRow.Visible = false;
                this.TicketFundsAppliedRow.Visible = false;
				this.PayFundsToPromoterBankAccountRow.Visible = false;

				if (CurrentTicketPromoterEvent.FundsTransfer != null)
                {
					this.ReleaseTransferLabel.Text = CurrentTicketPromoterEvent.FundsTransfer.AdminLinkNewWindow() + "&nbsp;&nbsp;" + CurrentTicketPromoterEvent.LinkNewWindow();
                    this.TicketPaymentRow.Visible = CurrentTicketPromoterEvent.FundsTransfer.RefundTransfers.Count > 0;
					decimal fundsTransferAmountRemaining = CurrentTicketPromoterEvent.FundsTransfer.AmountRemaining();
					this.PayFundsToPromoterBankAccountRow.Visible = CurrentTicketPromoterEvent.FundsTransfer.RefundTransfers.Count == 0 && fundsTransferAmountRemaining > 0;
					if (this.PayFundsToPromoterBankAccountRow.Visible)
					{
						int numberOfCredits = CampaignCredit.CalculateTotalCreditsForMoney(fundsTransferAmountRemaining / (decimal)(1 + Invoice.VATRate(Invoice.VATCodes.T1, DateTime.Now)), 0.5, CurrentTicketPromoterEvent.Promoter);
						CreateCampaignCreditsButton.InnerHtml = "Buy " + numberOfCredits.ToString("N0") + " campaign credits";
						PayFundsToPromoterBankAccountButton.InnerHtml = "Pay " + fundsTransferAmountRemaining.ToString("c") + " to promoter bank account";
					}
                    if(CurrentTicketPromoterEvent.FundsTransfer.RefundTransfers.Count > 0)
                    {
						this.TicketPaymentTransferLabel.Text = CurrentTicketPromoterEvent.FundsTransfer.RefundTransfers[0].AdminLinkNewWindow() + "&nbsp;(" + CurrentTicketPromoterEvent.FundsTransfer.RefundTransfers[0].Amount.ToString("c") + ")";
                    }
                    if (CurrentTicketPromoterEvent.FundsTransfer.InvoicesAppliedTo != null && CurrentTicketPromoterEvent.FundsTransfer.InvoicesAppliedTo.Count > 0)
                    {
                        this.TicketFundsAppliedRow.Visible = true;
                        InvoicesAppliedToLabel.Text = Utilities.IconHtml(Utilities.Icon.Tick) + "&nbsp;";
                        foreach (Invoice invoice in CurrentTicketPromoterEvent.FundsTransfer.InvoicesAppliedTo)
                        {
							InvoicesAppliedToLabel.Text += invoice.AdminLinkNewWindow() + "&nbsp;(" + invoice.Total.ToString("c") + ")&nbsp;&nbsp;";
                        }
                    }
                }
				this.LockTextRow.Visible = CurrentTicketPromoterEvent.FundsLockText.Trim().Length > 0;
				this.LockTextLabel.Text = CurrentTicketPromoterEvent.FundsLockText;

				this.OverrideLockNotSetDiv.Visible = !CurrentTicketPromoterEvent.FundsLockOverride;
				this.OverrideLockLabel.Text = (CurrentTicketPromoterEvent.FundsLockOverride ? Utilities.IconHtml(Utilities.Icon.Tick) : "");
				this.OverrideLockRow.Visible = CurrentTicketPromoterEvent.FundsLockOverride || (!CurrentTicketPromoterEvent.FundsReleased && CurrentTicketPromoterEvent.IsFundsLock);

				this.ManualLockNotSetDiv.Visible = !CurrentTicketPromoterEvent.FundsLockManual;
				this.ManualLockLabel.Text = (CurrentTicketPromoterEvent.FundsLockManual ? Utilities.IconHtml(Utilities.Icon.Lock) : "");
				this.ManualLockRow.Visible = !CurrentTicketPromoterEvent.FundsReleased || CurrentTicketPromoterEvent.FundsLockManual;

				this.FundsReleasedRow.Visible = CurrentTicketPromoterEvent.FundsReleased;
               
				//this.ReleaseFundsRow.Visible = !CurrentTicketPromoterEvent.FundsReleased && !CurrentTicketPromoterEvent.IsFundsLock;
				this.RerunFundsLocksChecksRow.Visible = !CurrentTicketPromoterEvent.FundsReleased;
			}
		}
		#endregion

		#region Search
		private void Search()
		{
			TicketPromoterEventFundsGridView.PageIndex = 0;
			// Clear the error message.
			SearchResultsMessageLabel.Text = "";
			SearchResultsMessageLabel.Visible = false;

			GetTicketPromoterEvents();
		}
		#endregion

		#region ClearSearchCriteria
		private void ClearSearchCriteria()
		{
			this.uiPromotersAutoComplete.Value = "";
			this.uiPromotersAutoComplete.Text = "";
			this.StatusDropDownList.SelectedIndex = 0;
			this.ToDateCal.Date = DateTime.MinValue;
			this.FromDateCal.Date = DateTime.MinValue;
			this.ShowZeroMoneyTicketRunsCheckBox.Checked = false;
			this.SalesUsrDropDownList.SelectedIndex = 0;

			SearchResultsMessageLabel.Visible = false;
		}
		#endregion
		#endregion

		#region Page Event Handlers
		protected void SearchButton_Click(object sender, EventArgs e)
		{
			Search();
		}
		protected void ClearButton_Click(object sender, EventArgs e)
		{
			ClearSearchCriteria();
		}

		protected void GetLatestEventsWithTicketsButton_Click(object sender, EventArgs e)
		{
			TicketPromoterEvent.CreateNewTicketPromoterEventsWhenNeeded();
		}

		protected void ManualLockButton_Click(object sender, EventArgs e)
		{
			Page.Validate("ManualLockValidationGroup");

			if (Usr.Current.IsSuperAdmin && Page.IsValid)
			{
				try
				{
					if (!CurrentTicketPromoterEvent.FundsLockManual && !CurrentTicketPromoterEvent.FundsReleased)
					{
						CurrentTicketPromoterEvent.FundsLockManual = true;
						CurrentTicketPromoterEvent.FundsLockManualNote = this.ManualLockNoteTextBox.Text.Trim();
						CurrentTicketPromoterEvent.FundsLockManualUsrK = Usr.Current.K;
						CurrentTicketPromoterEvent.FundsLockManualDateTime = DateTime.Now;
						CurrentTicketPromoterEvent.AddFundsLockText(DateTime.Now.ToString("dd/MM/yy HH:mm") + " Manual funds lock by " + Usr.Current.NickName + ": " + CurrentTicketPromoterEvent.FundsLockManualNote);
						CurrentTicketPromoterEvent.Update();

						this.ManualLockNoteTextBox.Text = "";
						LoadTicketPromoterEventToScreen();
						GetTicketPromoterEvents();
					}
				}
				catch (Exception ex)
				{
					this.ProcessingVal.IsValid = false;
					this.ProcessingVal.ErrorMessage = ex.Message;
				}
			}
		}

		protected void OverrideLockButton_Click(object sender, EventArgs e)
		{
			Page.Validate("ManualOverrideValidationGroup");

			if (Usr.Current.IsSuperAdmin && Page.IsValid)
			{
				try
				{
					if (CurrentTicketPromoterEvent.IsFundsLock && !CurrentTicketPromoterEvent.FundsReleased && CurrentTicketPromoterEvent.FundsTransferK == 0)
					{
						CurrentTicketPromoterEvent.FundsLockOverride = true;
						CurrentTicketPromoterEvent.FundsLockOverrideNote = this.OverrideLockNoteTextBox.Text.Trim();
						CurrentTicketPromoterEvent.FundsLockOverrideUsrK = Usr.Current.K;
						CurrentTicketPromoterEvent.FundsLockOverrideDateTime = DateTime.Now;
						CurrentTicketPromoterEvent.AddFundsLockText(DateTime.Now.ToString("dd/MM/yy HH:mm") + " Override funds lock by " + Usr.Current.NickName + ": " + CurrentTicketPromoterEvent.FundsLockOverrideNote);

						CurrentTicketPromoterEvent.CalculateTotalFundsAndVat();
                        CurrentTicketPromoterEvent.Update();

						CurrentTicketPromoterEvent.ReleaseFunds();						

						this.OverrideLockNoteTextBox.Text = "";
						LoadTicketPromoterEventToScreen();
						GetTicketPromoterEvents();
					}
				}
				catch (Exception ex)
				{
					this.ProcessingVal.IsValid = false;
					this.ProcessingVal.ErrorMessage = ex.Message;
				}
			}
		}

		protected void RerunFundsLocksChecksButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (!CurrentTicketPromoterEvent.FundsReleased)
				{
					CurrentTicketPromoterEvent.CalculateTicketsAndFunds();
					CurrentTicketPromoterEvent.RunFundsLockChecks();

					if (!CurrentTicketPromoterEvent.IsFundsLock)
					{
						CurrentTicketPromoterEvent.ReleaseFunds();
					}
					CurrentTicketPromoterEvent.Update();
				}
				LoadTicketPromoterEventToScreen();
				GetTicketPromoterEvents();
			}
			catch (Exception ex)
			{
				this.ProcessingVal.IsValid = false;
				this.ProcessingVal.ErrorMessage = ex.Message;
			}			
		}

        protected void PayFundsToPromoterBankAccountButton_Click(object sender, EventArgs e)
		{
			try
			{
                if (Usr.Current != null && Usr.Current.IsSuperAdmin && CurrentTicketPromoterEvent.FundsTransfer != null)
				{

                    decimal fundsRemaining = CurrentTicketPromoterEvent.FundsTransfer.AmountRemaining();
                    if (fundsRemaining > 0)
                    {
                        Transfer refund = CurrentTicketPromoterEvent.FundsTransfer.RefundViaBACS(fundsRemaining);
                        LoadTicketPromoterEventToScreen();
                    }
                    else
                    {
                        throw new DsiUserFriendlyException("Cannot pay funds to promoter. Ticket funds transfer #" + CurrentTicketPromoterEvent.FundsTransfer.K.ToString() + " has zero money remaining.");
                    }
				}
			}
			catch (Exception ex)
			{
				this.ProcessingVal.IsValid = false;
				this.ProcessingVal.ErrorMessage = ex.Message;
                Utilities.AdminEmailAlert("<p>Exception occured when paying ticket funds to promoter</p><p>PromoterK=" + CurrentTicketPromoterEvent.PromoterK + ", EventK=" + CurrentTicketPromoterEvent.EventK + "</p>", 
                                            "Exception occured when paying ticket funds to promoter", ex, CurrentTicketPromoterEvent);
			}
			
		}

		protected void CreateCampaignCreditsButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (Usr.Current != null && Usr.Current.IsAdmin && CurrentTicketPromoterEvent.FundsTransfer != null)
				{
					CurrentTicketPromoterEvent.ConvertRemainingFundsToCampaignCredits(Usr.Current);
					LoadTicketPromoterEventToScreen();
				}
			}
			catch (Exception ex)
			{
				this.ProcessingVal.IsValid = false;
				this.ProcessingVal.ErrorMessage = ex.Message;
				Utilities.AdminEmailAlert("<p>Exception occured when paying ticket funds to promoter</p><p>PromoterK=" + CurrentTicketPromoterEvent.PromoterK + ", EventK=" + CurrentTicketPromoterEvent.EventK + "</p>",
											"Exception occured when paying ticket funds to promoter", ex, CurrentTicketPromoterEvent);
			}
		}

		protected void MyPromotersRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			ClearSearchCriteria();
			this.SalesUsrDropDownList.SelectedValue = Usr.Current.K.ToString();
			Search();
		}

		protected void MyPromotersWithFundsNotSpentRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			ClearSearchCriteria();
			this.SalesUsrDropDownList.SelectedValue = Usr.Current.K.ToString();
			this.StatusDropDownList.SelectedValue = "Funds Released With Money Remaining";
			Search();
		}

		protected void MyPromotersWithFundsNotReleasedRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			ClearSearchCriteria();
			this.SalesUsrDropDownList.SelectedValue = Usr.Current.K.ToString();
			this.StatusDropDownList.SelectedValue = "Funds Not Released";
			Search();
		}

		protected void MyPromotersWithFundsSoonToBeReleasedRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			ClearSearchCriteria();
			this.SalesUsrDropDownList.SelectedValue = Usr.Current.K.ToString();
			this.FromDateCal.Date = Time.Today.AddDays(-7);
			this.ToDateCal.Date = Time.Today.AddDays(-1);
			Search();
		}
		#endregion

		#region TicketPromoterEventFundsGridView Event Handlers

        protected void TicketPromoterEventFundsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Cancel the paging operation if the user attempts to navigate
            // to another page while the GridView control is in edit mode. 
            if (TicketPromoterEventFundsGridView.EditIndex != -1)
            {
                // Use the Cancel property to cancel the paging operation.
                e.Cancel = true;

                // Display an error message.
                int newPageNumber = e.NewPageIndex + 1;
                SearchResultsMessageLabel.Text = "* Please update the record before moving to page " + newPageNumber.ToString() + ".";
                SearchResultsMessageLabel.Visible = true;
            }
            else
            {
                TicketPromoterEventFundsGridView.PageIndex = e.NewPageIndex;
                GetTicketPromoterEvents();
                if (TicketPromoterEventFundsGridView.PageIndex > TicketPromoterEventFundsGridView.PageCount)
                    TicketPromoterEventFundsGridView.PageIndex = 1;

                // Clear the error message.
                SearchResultsMessageLabel.Text = "";
                SearchResultsMessageLabel.Visible = false;
            }
        }

		protected void TicketPromoterEventFundsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "EditTicketPromoterEvent")
			{
				GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;

				TicketPromoterEvent ticketPromoterEvent = new TicketPromoterEvent(Convert.ToInt32(((TextBox)row.FindControl("PromoterKTextBox")).Text), Convert.ToInt32(((TextBox)row.FindControl("EventKTextBox")).Text));

				EventK = ticketPromoterEvent.EventK;
				PromoterK = ticketPromoterEvent.PromoterK;
				LoadTicketPromoterEventToScreen();
			}
			else
				return;
		}
		#endregion+
	}
}
