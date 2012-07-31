using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;


namespace Spotted.Pages.Events
{
	[ClientScript]
	public class EventUserControl : DsiUserControl, IIncludesJs
	{
		public EventUserControl()
		{
		}

		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		#region Properties
		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null)
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						currentEvent = ContainerPage.Url.ObjectFilterEvent;
					else if (EventK > 0)
						currentEvent = new Event(EventK);
				}
				return currentEvent;
			}
			set
			{
				currentEvent = value;
				ContainerPage.Url.ResetObjectFilterObject();
			}
		}
		Event currentEvent;
		#endregion

		#region EventK
		public int EventK
		{
			get
			{
				if (ContainerPage.Url.HasEventObjectFilter)
					return ContainerPage.Url.ObjectFilterEvent.K;
				else
					return ContainerPage.Url["K"];
			}
		}
		#endregion

		#region CurrentUsrTickets
		public TicketSet CurrentUsrTickets
		{
			get
			{
				if (currentUsrTickets == null && Usr.Current != null)
					currentUsrTickets = Usr.Current.Tickets(CurrentEvent.K);
				return currentUsrTickets;
			}
			set
			{
				currentUsrTickets = null;
			}
		}
		private TicketSet currentUsrTickets;

		#endregion
		#endregion
	}
}
