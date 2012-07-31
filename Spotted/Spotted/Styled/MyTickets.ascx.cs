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

namespace Spotted.Styled
{
	public partial class MyTickets : StyledUserControl
	{
		#region Variables
		int RecordsPerPage = 20;
		int PageNumber = 1;
		Utilities.DateRange SelectedDateRange = Utilities.DateRange.Current;
		EventSet eventsWithMyTickets = null;
		//DateTime StartDateRange;
		//DateTime EndDateRange;
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current == null)
				Response.Redirect(this.StyledObject.UrlStyledApp("login") + "?url=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));

			if (!this.IsPostBack)
			{
				LoadMyTickets();
			}
		}

		//protected bool TicketRunHasDescription
		//{
		//    get
		//    {
		//        return CurrentTicket.ExtraSelectElements["TicketRunDescription"] != DBNull.Value && ((string)CurrentTicket.ExtraSelectElements["TicketRunDescription"]).Length > 0;
		//    }
		//}

		#region Methods
		public void LoadMyTickets()
		{
			eventsWithMyTickets = Usr.Current.EventsWithTickets(this.SelectedDateRange, this.PageNumber, this.RecordsPerPage);


			ViewState["EventTicketsCounter"] = "0";
			this.MyEventTicketsRepeater.DataSource = eventsWithMyTickets;
			this.MyEventTicketsRepeater.DataBind();

			HasTickets.Visible = eventsWithMyTickets.Count > 0;
			NoTickets.Visible = !HasTickets.Visible;

			this.SelectAllDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.All);
			this.SelectCurrentDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.Current);
			this.SelectPastDateRangeLinkButton.Enabled = !SelectedDateRange.Equals(Utilities.DateRange.Old);
		}
		#endregion

		#region Event Handlers
		protected void MyEventTicketsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				eventsWithMyTickets.Reset();
				bool flag = true;
				foreach (Event evnt in eventsWithMyTickets)
				{
					if (flag)
					{
						TicketSet tickets = Usr.Current.Tickets(evnt.K);
						foreach (Ticket ticket in tickets)
						{
							if (ticket.Code.Length > 0)
							{
								try
								{
									((HtmlTableCell)e.Item.FindControl("CodeHeader")).InnerHtml = "Code";
									flag = false;
								}
								catch { }
								break;
							}
						}
					}
				}
			}
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				ViewState["EventTicketsCounter"] = ((int)(Convert.ToInt32(ViewState["EventTicketsCounter"]) + 1)).ToString();

				Repeater myTicketsRepeater = (Repeater)e.Item.FindControl("MyTicketsRepeater");
				myTicketsRepeater.DataSource = Usr.Current.Tickets(((Event)e.Item.DataItem).K);
				myTicketsRepeater.ItemTemplate = this.LoadTemplate("/Templates/Usrs/EventTicketsRepeater.ascx");
				myTicketsRepeater.DataBind();

				//if (Convert.ToInt32(ViewState["EventTicketsCounter"]) % 2 == 0)
				//{
				//    ((HtmlTableRow)e.Item.FindControl("EventDetailsRow")).Style["background-color"] = "#FED551";
				//}

			}
			else if (e.Item.ItemType == ListItemType.Footer)
			{
				EventSet myEventsWithTickets = (EventSet)MyEventTicketsRepeater.DataSource;
				if (!myEventsWithTickets.Paging.ShowNextPageLink && !myEventsWithTickets.Paging.ShowPrevPageLink)
					e.Item.Visible = false;
				else
				{
					e.Item.Visible = true;
					((LinkButton)e.Item.FindControl("NextPageLinkButton")).Enabled = myEventsWithTickets.Paging.ShowNextPageLink;
					((LinkButton)e.Item.FindControl("PrevPageLinkButton")).Enabled = myEventsWithTickets.Paging.ShowPrevPageLink;
				}
			}
		}

		protected void MyTicketsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			// Execute the following logic for Items and Alternating Items.
			//if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			//{
			//    if (Convert.ToInt32(ViewState["EventTicketsCounter"]) % 2 == 0)
			//    {
			//        ((HtmlTableRow)e.Item.Controls[0].FindControl("TicketRunDetailsRow")).Style["background-color"] = "#FED551";
			//        ((HtmlTableRow)e.Item.Controls[0].FindControl("TicketRunDescriptionRow")).Style["background-color"] = "#FED551";
			//    }
			//}
		}

		protected void NextPageLinkButton_Click(object sender, EventArgs e)
		{
			this.PageNumber++;
			LoadMyTickets();
		}

		protected void PrevPageLinkButton_Click(object sender, EventArgs e)
		{
			this.PageNumber--;
			LoadMyTickets();
		}

		protected void TicketRunDateRangeAllSelect(object sender, EventArgs e)
		{
			this.SelectedDateRange = Utilities.DateRange.All;
			this.PageNumber = 1;
			LoadMyTickets();
		}

		protected void TicketRunDateRangeCurrentSelect(object sender, EventArgs e)
		{
			this.SelectedDateRange = Utilities.DateRange.Current;
			this.PageNumber = 1;
			LoadMyTickets();
		}

		protected void TicketRunDateRangePastSelect(object sender, EventArgs e)
		{
			this.SelectedDateRange = Utilities.DateRange.Old;
			this.PageNumber = 1;
			LoadMyTickets();
		}
		#endregion
	}
}
