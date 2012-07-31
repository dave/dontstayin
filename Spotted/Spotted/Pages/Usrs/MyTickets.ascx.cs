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

namespace Spotted.Pages.Usrs
{
	public partial class MyTickets : UsrUserControl
	{
		#region Variables
		int RecordsPerPage = 20;
		int PageNumber = 1;
		Utilities.DateRange SelectedDateRange = Utilities.DateRange.Current;
		EventSet eventsWithMyTickets = null;
		//DateTime StartDateRange;
		//DateTime EndDateRange;
		#endregion

		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);			
		}

		protected void Page_Load(object sender, EventArgs e)
		{
            if(Usr.Current == null || !(Usr.Current.IsAdmin || Usr.Current.K == ThisUsr.K))
                throw new DsiUserFriendlyException(Vars.CANT_VIEW_DETAILS);

			if (!this.IsPostBack)
			{
				LoadMyTickets();
			}
		}

		#region Methods
		public void LoadMyTickets()
		{
			
			eventsWithMyTickets = ThisUsr.EventsWithTickets(this.SelectedDateRange, this.PageNumber, this.RecordsPerPage);
//			TicketSet myTickets = Usr.Current.Tickets(this.SelectedDateRange, this.PageNumber, this.RecordsPerPage);
			
			//MyTicketsRepeater.ItemTemplate = this.LoadTemplate("/Templates/Usrs/EventTicketsList.ascx");
			//TicketEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/UsrPageAttendedList.ascx");
			
			this.MyTicketsPanel.Visible = true;
			ViewState["EventTicketsCounter"] = "0";
			this.MyEventTicketsRepeater.DataSource = eventsWithMyTickets;
			this.MyEventTicketsRepeater.DataBind();

			uiHasETickets.Visible = eventsWithMyTickets.Count > 0;
			NoTickets.Visible = !uiHasETickets.Visible;

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
						TicketSet tickets = ThisUsr.Tickets(evnt.K);
						foreach (Ticket ticket in tickets)
						{
							if (ticket.Code.Length > 0)
							{
								try
								{
									((HtmlTableCell)e.Item.FindControl("CodeHeader")).InnerHtml = "<small>Code</small>";
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
				myTicketsRepeater.DataSource = ThisUsr.Tickets(((Event)e.Item.DataItem).K);
				myTicketsRepeater.ItemTemplate = this.LoadTemplate("/Templates/Usrs/EventTicketsRepeater.ascx");
				myTicketsRepeater.DataBind();

                //DataList ticketEventDataList = (DataList)e.Item.FindControl("TicketEventDataList");
                //List<Event> events = new List<Event>();
                //events.Add((Event)e.Item.DataItem);
                //ticketEventDataList.DataSource = events;
                //ticketEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/UsrPageAttendedList.ascx");
                //ticketEventDataList.DataBind();

				if (Convert.ToInt32(ViewState["EventTicketsCounter"]) % 2 == 0)
				{
					((HtmlTableRow)e.Item.FindControl("EventDetailsRow")).Style["background-color"] = "#FED551";
				}

			}
			else if (e.Item.ItemType == ListItemType.Footer)
			{
			    EventSet myEventsWithTickets = (EventSet)MyEventTicketsRepeater.DataSource;
			    ((LinkButton)e.Item.FindControl("NextPageLinkButton")).Enabled = myEventsWithTickets.Paging.ShowNextPageLink;
			    ((LinkButton)e.Item.FindControl("PrevPageLinkButton")).Enabled = myEventsWithTickets.Paging.ShowPrevPageLink;
			}
		}

		protected void MyTicketsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			// Execute the following logic for Items and Alternating Items.
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				
			    if (Convert.ToInt32(ViewState["EventTicketsCounter"]) % 2 == 0)
			    {
					foreach (var control in GetChildTableRows(e.Item))
					{
						control.Style["background-color"] = "#FED551";
					}
					//((HtmlTableRow)e.Item.Controls[0].FindControl("TicketRunDetailsRow")).Style["background-color"] = "#FED551";
					//((HtmlTableRow)e.Item.Controls[0].FindControl("TicketRunDescriptionRow")).Style["background-color"] = "#FED551";
					((HtmlTableRow)e.Item.Controls[0].FindControl("TicketContactEmailRow")).Style["background-color"] = "#FED551";
			    }
			}
		}
		IEnumerable<HtmlTableRow> GetChildTableRows(Control control)
		{
			foreach (Control child in control.Controls)
			{
				if (child is HtmlTableRow) { yield return (HtmlTableRow) child; }
				foreach (var childChild in GetChildTableRows(child))
				{
					yield return childChild;
				}
			}
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
