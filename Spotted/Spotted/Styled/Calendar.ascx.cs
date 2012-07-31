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
using Common;

namespace Spotted.Styled
{
	public partial class Calendar : StyledUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				LoadUpcomingTicketEventRepeater();
				
			}

		}

		private void LoadUpcomingTicketEventRepeater()
		{
			EventSet events = null;
			if (this.StyledObject is Brand)
			{
				events = Event.GetEvents((Brand)this.StyledObject, Utilities.GetStartOfMonth(ContainerPage.Url.DateFilter), Utilities.GetStartOfMonth(ContainerPage.Url.DateFilter.AddMonths(1)));
			}
			else if (this.StyledObject is Venue)
			{
				events = Event.GetEvents((Venue)this.StyledObject, Utilities.GetStartOfMonth(ContainerPage.Url.DateFilter), Utilities.GetStartOfMonth(ContainerPage.Url.DateFilter.AddMonths(1)));
			}
			NoEventsLabel.Visible = events == null || events.Count == 0;
			this.EventLinkRepeater.DataSource = events;
			this.EventLinkRepeater.DataBind();
		}
	}
}
