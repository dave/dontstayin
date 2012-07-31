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

namespace Spotted.Templates.Usrs
{
	public partial class EventTicketsRepeater : System.Web.UI.UserControl
	{
		protected bool TicketRunHasDescription
		{
			get
			{
				return CurrentTicket.ExtraSelectElements["TicketRunDescription"] != DBNull.Value && ((string)CurrentTicket.ExtraSelectElements["TicketRunDescription"]).Length > 0;	
			}
		}

		protected bool TicketRunHasContactEmail
		{
			get
			{
				return CurrentTicket.ExtraSelectElements["ContactEmail"] != DBNull.Value && ((string)CurrentTicket.ExtraSelectElements["ContactEmail"]).Length > 0;
			}
		}
		protected TicketRun.DeliveryMethodType CurrentTicketRunDeliveryType
		{
			get
			{
				return (TicketRun.DeliveryMethodType)(CurrentTicket.ExtraSelectElements["DeliveryMethod"] as int? ?? 0);
			}
		}
		protected DateTime? CurrentDeliveryDate
		{
			get
			{
				return CurrentTicket.ExtraSelectElements["DeliveryDate"] as DateTime?;
			}
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected Ticket CurrentTicket
		{
			get
			{
				if (currentTicket == null)
					currentTicket = (Ticket)((RepeaterItem)NamingContainer).DataItem;
				return currentTicket;
			}
		}
		Ticket currentTicket;
	}
}
