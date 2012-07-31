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

namespace Spotted.Styled
{
	public partial class Home : StyledUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				LoadUpcomingEventRepeater();
			}
	
		}

		private void LoadUpcomingEventRepeater()
		{
			// List ALL upcoming events
			EventSet events = null;
			
			if (this.StyledObject is Brand)
			{
				events = Event.GetUpcomingEvents((Brand)this.StyledObject);
			}
			else if (this.StyledObject is Venue)
			{
				events = Event.GetUpcomingEvents((Venue)this.StyledObject);
			}
			NoEventsLabel.Visible = events == null || events.Count == 0;
			this.EventLinkRepeater.DataSource = events;
			this.EventLinkRepeater.DataBind();
		}
	}
}
