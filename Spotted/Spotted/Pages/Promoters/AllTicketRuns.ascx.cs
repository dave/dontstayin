using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Cambro;

namespace Spotted.Pages.Promoters
{
	public partial class AllTicketRuns : PromoterUserControl
	{
		#region Variables
		int RecordsPerPage = 20;
		int PageNumber = 1;
		Utilities.DateRange SelectedDateRange = Utilities.DateRange.Current;
		DateTime StartDateRange;
		DateTime EndDateRange;
		#endregion

		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				LoadPromoterTicketRuns();
                //GetTicketSalesSummary();
			}
			AdminLinksPanel.Visible = Usr.Current.IsAdmin;

            ((Spotted.Master.DsiPage)this.Page).SetPageTitle("All ticket runs for " + CurrentPromoter.Name);            
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
		#region LoadPromoterTicketRuns
		private void LoadPromoterTicketRuns()
		{
			SetDateRange();
			Query promoterTicketRunsQuery = new Query(new And(new Q(Bobs.TicketRun.Columns.PromoterK, CurrentPromoter.K),
															  new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, StartDateRange),
															  new Q(Event.Columns.DateTime, QueryOperator.LessThan, EndDateRange)));
			promoterTicketRunsQuery.TableElement = new Join(Bobs.TicketRun.Columns.EventK, Event.Columns.K);
			promoterTicketRunsQuery.OrderBy = new OrderBy(new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending), new OrderBy(Event.Columns.K), new OrderBy(Bobs.TicketRun.Columns.ListOrder),
														  new OrderBy(Bobs.TicketRun.Columns.Price), new OrderBy(Bobs.TicketRun.Columns.StartDateTime));
			promoterTicketRunsQuery.Paging.RecordsPerPage = this.RecordsPerPage;
			promoterTicketRunsQuery.Paging.RequestedPage = this.PageNumber;
			promoterTicketRunsQuery.TopRecords = (this.PageNumber * this.RecordsPerPage) + 1;


			TicketRunSet promoterTicketRuns = new TicketRunSet(promoterTicketRunsQuery);

			TicketRunsGridView.DataSource = promoterTicketRuns;
			TicketRunsGridView.DataBind();

			this.NextPageLinkButton.Enabled = promoterTicketRuns.Paging.ShowNextPageLink;
			this.PrevPageLinkButton.Enabled = promoterTicketRuns.Paging.ShowPrevPageLink;			
		}
		#endregion

		#region SetDateRange
		private void SetDateRange()
		{
			this.H1Title.InnerHtml = this.SelectedDateRange.ToString() + " ticket runs";

			// Date range for all
            StartDateRange = Vars.TICKETS_NEW_SYSTEM_START_DATE;
			EndDateRange = DateTime.MaxValue;

			if (SelectedDateRange.Equals(Utilities.DateRange.Current))
				StartDateRange = DateTime.Today.AddMonths(-1);
			else if (SelectedDateRange.Equals(Utilities.DateRange.Old))
				EndDateRange = DateTime.Today.AddMonths(-1);

			this.SelectAllDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.All);
			this.SelectCurrentDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.Current);
			this.SelectPastDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.Old);
		}
		#endregion

        private void GetTicketSalesSummary()
        {
            this.TotalTicketRunsLabel.Text = CurrentPromoter.TicketRuns.Count.ToString();
            this.TotalTicketsSoldLabel.Text = CurrentPromoter.TicketsSoldTotal.ToString() + (CurrentPromoter.TicketsCancelledTotal > 0 ? " (" + CurrentPromoter.TicketsCancelledTotal.ToString() + ")" : "");
            this.TicketFundsReleasedLabel.Text = CurrentPromoter.TicketFundsReleased.ToString("c");
            this.TicketFundsInWaitingLabel.Text = CurrentPromoter.GetTicketFundsAwaitingRelease().ToString("c");
        }
		#endregion

		#region Page Event Handlers
		protected void NextPageLinkButton_Click(object sender, EventArgs e)
		{
			this.PageNumber++;
			LoadPromoterTicketRuns();
		}

		protected void PrevPageLinkButton_Click(object sender, EventArgs e)
		{
			this.PageNumber--;
			LoadPromoterTicketRuns();
		}

		protected void TicketRunDateRangeAllSelect(object sender, EventArgs e)
		{
			this.SelectedDateRange = Utilities.DateRange.All;
			this.PageNumber = 1;
			LoadPromoterTicketRuns();
		}

		protected void TicketRunDateRangeCurrentSelect(object sender, EventArgs e)
		{
			this.SelectedDateRange = Utilities.DateRange.Current;
			this.PageNumber = 1;
			LoadPromoterTicketRuns();
		}

		protected void TicketRunDateRangePastSelect(object sender, EventArgs e)
		{
			this.SelectedDateRange = Utilities.DateRange.Old;
			this.PageNumber = 1;
			LoadPromoterTicketRuns();
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

				LoadPromoterTicketRuns();
			}
		}
		#endregion
	}
}
