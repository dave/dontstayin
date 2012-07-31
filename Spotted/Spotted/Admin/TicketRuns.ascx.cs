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

namespace Spotted.Admin
{
	public partial class TicketRuns : AdminUserControl
	{
		#region Variables
		//int RecordsPerPage = 20;
		int PageNumber = 1;
		Utilities.DateRange SelectedDateRange = Utilities.DateRange.Current;
		//DateTime StartDateRange;
		//DateTime EndDateRange;
		TicketRun.TicketRunStatus ticketRunStatus;
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				SetupStatusDropDownList();
				LoadTicketRuns();
			}
			this.TicketRunsJavascriptLabel.Visible = false;
		}
		#endregion

		#region ViewState
		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["PageNumber"] = PageNumber;
			this.ViewState["SelectedDateRange"] = SelectedDateRange;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["PageNumber"] != null) PageNumber = (int)this.ViewState["PageNumber"];
			if (this.ViewState["SelectedDateRange"] != null) SelectedDateRange = (Utilities.DateRange)this.ViewState["SelectedDateRange"];
		}
		#endregion
		#endregion

		#region Methods
		#region LoadTicketRuns
		private void LoadTicketRuns()
		{
			//SetDateRange();

			Query ticketRunsQuery = new Query();
			List<Q> QueryConditionList = new List<Q>();

			if (this.TicketRunKTextBox.Text.Trim().Length > 0)
				QueryConditionList.Add(new Q(TicketRun.Columns.K, Convert.ToInt32(TicketRunKTextBox.Text)));
			else
			{
				if (this.uiEventAutoComplete.Value != null && !uiEventAutoComplete.Value.Equals(""))
					QueryConditionList.Add(new Q(TicketRun.Columns.EventK, Convert.ToInt32(uiEventAutoComplete.Value)));
				if (this.uiPromotersAutoComplete.Value != null && !uiPromotersAutoComplete.Value.Equals(""))
					QueryConditionList.Add(new Q(TicketRun.Columns.PromoterK, Convert.ToInt32(uiPromotersAutoComplete.Value)));
                if(this.OnlyShowTicketRunsWithSoldTicketsCheckBox.Checked)
                    QueryConditionList.Add(new Q(TicketRun.Columns.SoldTickets, QueryOperator.GreaterThan, 0));
				if (this.StatusDropDownList.SelectedValue != "")
				{
					ticketRunStatus = (TicketRun.TicketRunStatus)Convert.ToInt32(this.StatusDropDownList.SelectedValue);

					if (ticketRunStatus == TicketRun.TicketRunStatus.Ended)
					{
						QueryConditionList.Add(new Q(TicketRun.Columns.EndDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now));
					}
					else if (ticketRunStatus == TicketRun.TicketRunStatus.Running)
					{
						QueryConditionList.Add(new Q(TicketRun.Columns.EndDateTime, QueryOperator.GreaterThan, DateTime.Now));
						QueryConditionList.Add(new Q(TicketRun.Columns.StartDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now));
						QueryConditionList.Add(new Or(new Q(TicketRun.Columns.Paused, false),
													  new Q(TicketRun.Columns.Paused, QueryOperator.IsNull, null)));
						QueryConditionList.Add(new Q(TicketRun.Columns.SoldTickets, QueryOperator.LessThan, TicketRun.Columns.MaxTickets, true));
					}
					else if (ticketRunStatus == TicketRun.TicketRunStatus.SoldOut)
					{
						QueryConditionList.Add(new Q(TicketRun.Columns.EndDateTime, QueryOperator.GreaterThan, DateTime.Now));
						QueryConditionList.Add(new Q(TicketRun.Columns.StartDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now));
						QueryConditionList.Add(new Q(TicketRun.Columns.SoldTickets, TicketRun.Columns.MaxTickets, true));
					}
					else if (ticketRunStatus == TicketRun.TicketRunStatus.WaitingStartDate)
					{
						QueryConditionList.Add(new Q(TicketRun.Columns.StartDateTime, QueryOperator.GreaterThan, DateTime.Now));
					}
					else if (ticketRunStatus == TicketRun.TicketRunStatus.WaitingToFollowOtherTicketRun)
					{
						QueryConditionList.Add(new Q(TicketRun.Columns.EndDateTime, QueryOperator.GreaterThan, DateTime.Now));
						QueryConditionList.Add(new Q(TicketRun.Columns.StartDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now));
						QueryConditionList.Add(new Or(new Q(TicketRun.Columns.FollowsTicketRunK, QueryOperator.GreaterThan, 0)));
					}
					else if (ticketRunStatus == TicketRun.TicketRunStatus.Paused)
					{
						QueryConditionList.Add(new Q(TicketRun.Columns.EndDateTime, QueryOperator.GreaterThan, DateTime.Now));
						QueryConditionList.Add(new Q(TicketRun.Columns.StartDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now));
						QueryConditionList.Add(new Q(TicketRun.Columns.Paused, true));
						QueryConditionList.Add(new Q(TicketRun.Columns.SoldTickets, QueryOperator.LessThan, TicketRun.Columns.MaxTickets, true));
					}
					else if (ticketRunStatus == TicketRun.TicketRunStatus.Refunded)
					{
						QueryConditionList.Add(new Q(TicketRun.Columns.EndDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now));
						QueryConditionList.Add(new Q(TicketRun.Columns.SoldTickets, QueryOperator.GreaterThan, 0));
					}
				}
			}
			if (QueryConditionList.Count > 0)
				ticketRunsQuery.QueryCondition = new And(QueryConditionList.ToArray());

			ticketRunsQuery.TableElement = new Join(Bobs.TicketRun.Columns.EventK, Event.Columns.K);
			ticketRunsQuery.OrderBy = new OrderBy(new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending), new OrderBy(Event.Columns.Name), new OrderBy(Bobs.TicketRun.Columns.ListOrder),
												  new OrderBy(Bobs.TicketRun.Columns.Price), new OrderBy(Bobs.TicketRun.Columns.StartDateTime));
			//ticketRunsQuery.Paging.RecordsPerPage = this.RecordsPerPage;
			//ticketRunsQuery.Paging.RequestedPage = this.PageNumber;
			//ticketRunsQuery.TopRecords = (this.PageNumber * this.RecordsPerPage) + 1;


		//	TicketRunSet ticketRuns = new TicketRunSet(ticketRunsQuery);


			List<TicketRun> ticketRunSearchResults = VerifyTicketRunsMatchSearchCriteria(new TicketRunSet(ticketRunsQuery));

			TicketRunsGridView.DataSource = ticketRunSearchResults;
			TicketRunsGridView.DataBind();

			if (ticketRunSearchResults == null || ticketRunSearchResults.Count == 0)
			{
				SearchResultsMessageLabel.Text = "* Zero results for your search.  Please verify your search criteria.";
				SearchResultsMessageLabel.Visible = true;
			}

			//this.NextPageLinkButton.Enabled = ticketRuns.Paging.ShowNextPageLink;
			//this.PrevPageLinkButton.Enabled = ticketRuns.Paging.ShowPrevPageLink;
		}
		#endregion

		#region VerifyTicketRunsMatchSearchCriteria
		// Due to the complicated nature of the searching based on status, it is much easier to verify ticketrun status of the returned results
		public List<TicketRun> VerifyTicketRunsMatchSearchCriteria(TicketRunSet ticketRuns)
		{
			List<TicketRun> verifiedTicketRuns = new List<TicketRun>();
			
			ticketRuns.Reset();
			foreach (TicketRun tr in ticketRuns)
			{
				if (ticketRunStatus == 0 || ticketRunStatus == tr.Status)
				{
					verifiedTicketRuns.Add(tr);
				}
			}

			return verifiedTicketRuns;
		}

		#endregion

		//#region SetDateRange
		//private void SetDateRange()
		//{
		//    this.H1Title.InnerHtml = this.SelectedDateRange.ToString() + " ticket runs";

		//    // Date range for all
		//    StartDateRange = new DateTime(2006, 7, 1);
		//    EndDateRange = DateTime.MaxValue;

		//    if (SelectedDateRange.Equals(Utilities.DateRange.Current))
		//        StartDateRange = DateTime.Today.AddMonths(-1);
		//    else if (SelectedDateRange.Equals(Utilities.DateRange.Past))
		//        EndDateRange = DateTime.Today.AddMonths(-1);

		//    //this.SelectAllDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.All);
		//    //this.SelectCurrentDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.Current);
		//    //this.SelectPastDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.Past);
		//}
		//#endregion

		#region SetupStatusDropDownList
		public void SetupStatusDropDownList()
		{
			this.StatusDropDownList.Items.Clear();
			this.StatusDropDownList.Items.Add(new ListItem("", ""));
			this.StatusDropDownList.Items.AddRange(TicketRun.StatusesAsListItemArray());
		}
		#endregion
		#endregion

		#region Page Event Handlers
		//protected void NextPageLinkButton_Click(object sender, EventArgs e)
		//{
		//    this.PageNumber++;
		//    LoadTicketRuns();
		//}

		//protected void PrevPageLinkButton_Click(object sender, EventArgs e)
		//{
		//    this.PageNumber--;
		//    LoadTicketRuns();
		//}

		//protected void TicketRunDateRangeAllSelect(object sender, EventArgs e)
		//{
		//    this.SelectedDateRange = Utilities.DateRange.All;
		//    this.PageNumber = 1;
		//    LoadTicketRuns();
		//}

		//protected void TicketRunDateRangeCurrentSelect(object sender, EventArgs e)
		//{
		//    this.SelectedDateRange = Utilities.DateRange.Current;
		//    this.PageNumber = 1;
		//    LoadTicketRuns();
		//}

		//protected void TicketRunDateRangePastSelect(object sender, EventArgs e)
		//{
		//    this.SelectedDateRange = Utilities.DateRange.Past;
		//    this.PageNumber = 1;
		//    LoadTicketRuns();
		//}
		protected void SearchButton_Click(object sender, EventArgs e)
		{
			TicketRunsGridView.PageIndex = 0;
			// Clear the error message.
			SearchResultsMessageLabel.Text = "";
			SearchResultsMessageLabel.Visible = false;

			LoadTicketRuns();
		}
		protected void ClearButton_Click(object sender, EventArgs e)
		{
			this.uiEventAutoComplete.Value = "";
			this.uiEventAutoComplete.Text = "";
			this.uiPromotersAutoComplete.Value = "";
			this.uiPromotersAutoComplete.Text = "";
			this.TicketRunKTextBox.Text = "";
			this.StatusDropDownList.SelectedIndex = 0;
			SearchResultsMessageLabel.Visible = false;
		}

		#endregion

		#region TicketRunsGridView Event Handlers
		protected void TicketRunsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "StopTicketRun" || e.CommandName == "PauseResumeTicketRun")
			{
				GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;

				Bobs.TicketRun ticketRun = new Bobs.TicketRun(Convert.ToInt32(((TextBox)row.FindControl("TicketRunKTextBox")).Text));
				if (e.CommandName == "StopTicketRun")
				{
					ticketRun.EndTicketRun();
				}
				else if (e.CommandName == "PauseResumeTicketRun")
				{
					ticketRun.Paused = !ticketRun.Paused;
					ticketRun.Update();
				}
				else
					return;
				
				LoadTicketRuns();
			}
		}

		protected void TicketRunsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			// Cancel the paging operation if the user attempts to navigate
			// to another page while the GridView control is in edit mode. 
			if (TicketRunsGridView.EditIndex != -1)
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
				TicketRunsGridView.PageIndex = e.NewPageIndex;
				LoadTicketRuns();
				if (TicketRunsGridView.PageIndex > TicketRunsGridView.PageCount)
					TicketRunsGridView.PageIndex = 1;

				// Clear the error message.
				SearchResultsMessageLabel.Text = "";
				SearchResultsMessageLabel.Visible = false;
			}
		}

		protected void TicketRunsGridView_DataBound(object sender, EventArgs e)
		{
			TicketRunsGridView.Columns[TicketRunsGridView.Columns.Count - 1].Visible = Usr.Current.IsSuperAdmin;
		}
		       
		//protected void TicketRunsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		//{
		//    if (e.Row.RowType == DataControlRowType.DataRow && Usr.Current.IsSuperAdmin)
		//    {				
		//        try
		//        {
		//            Button refundButton = (Button)e.Row.FindControl("RefundTicketRunButton");
		//            //if (!refundButton.Enabled)
		//            //{
		//                refundButton.Attributes.Add("onmouseover", "stt('You must stop the ticket run before refunding it.');");
		//            //}
		//        }
		//        catch
		//        { }				
		//    }
		//}
		protected void RefundTicketRunButton_Click(object sender, EventArgs e)
		{
			GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent;// ((Control)e.CommandSource).Parent.Parent;

			Bobs.TicketRun ticketRun = new Bobs.TicketRun(Convert.ToInt32(((TextBox)row.FindControl("TicketRunKTextBox")).Text));
			ticketRun.CalculateSoldTicketsAndUpdate();
			if (ticketRun.SoldTickets > 0)
			{
				Ticket.Refund(Usr.Current, ticketRun.Tickets);
				ticketRun.CalculateSoldTicketsAndUpdate();
				if (ticketRun.Status == TicketRun.TicketRunStatus.Refunded)
				{
					this.TicketRunsJavascriptLabel.Text = "<script type=\"text/javascript\">alert('TicketRun #" + ticketRun.K.ToString() + " refunded.');</script>";
					this.TicketRunsJavascriptLabel.Visible = true;
				}
			}
			LoadTicketRuns();
		}
		#endregion
	}
}
