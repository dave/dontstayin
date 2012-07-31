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

namespace Spotted.Pages.Promoters
{
	public partial class Doorlist : PromoterUserControl
	{
		#region Variables
		Bobs.TicketRun CurrentTicketRun = new Bobs.TicketRun();
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
			if(!this.IsPostBack)
				LoadCurrentEventsWithTickets();
		}
		#endregion

		#region Methods
		private void LoadCurrentEventsWithTickets()
		{
			Query currentEventsWithTicketsQuery = new Query(new And(new Q(Bobs.TicketRun.Columns.PromoterK, CurrentPromoter.K),
																   new Q(Bobs.TicketRun.Columns.EndDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today.AddDays(-5)),
																   new Q(Bobs.TicketRun.Columns.SoldTickets, QueryOperator.GreaterThan, 0)));
			currentEventsWithTicketsQuery.TableElement = Bobs.TicketRun.EventJoin;
			currentEventsWithTicketsQuery.GroupBy = new GroupBy(new GroupBy(Event.Columns.K), new GroupBy(Event.Columns.DateTime), new GroupBy(Event.Columns.Name));
			currentEventsWithTicketsQuery.OrderBy = new OrderBy(Event.Columns.DateTime);
//			currentEventsWithTicketsQuery.ExtraSelectElements.Add("TicketsSold", "SUM([TicketRun].[SoldTickets])");

			currentEventsWithTicketsQuery.Columns = new ColumnSet(Event.Columns.K, Event.Columns.Name);
			EventSet currentEventsWithTickets = new EventSet(currentEventsWithTicketsQuery);

			this.NoTicketsP.Visible = false;
			if (currentEventsWithTickets.Count == 0)
			{
				this.NoTicketsP.Visible = true;
				this.HasTicketsP.Visible = false;
			}
			else if (currentEventsWithTickets.Count == 1)
			{
				// redirect to doorlist popup page
				Response.Redirect(currentEventsWithTickets[0].DoorlistUrl);
			}
			else
			{
				this.EventDropDownList.Items.Clear();
				foreach (Event ticketEvent in currentEventsWithTickets)
				{
					this.EventDropDownList.Items.Add(new ListItem(ticketEvent.Name, ticketEvent.K.ToString()));
				}
			}
		}

		protected void DoorlistButton_Click(object sender, EventArgs e)
		{
			if (this.EventDropDownList.SelectedValue != "")
			{
				Response.Redirect(new Event(Convert.ToInt32(this.EventDropDownList.SelectedValue)).DoorlistUrl);
			}
		}
		#endregion
	}
}
