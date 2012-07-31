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

namespace Spotted.Controls.EventDisplay
{
	public partial class EventDisplay3 : EventDisplay
	{

		Control baseNamingContainer
		{
			get
			{
				return NamingContainer.NamingContainer.NamingContainer.NamingContainer;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			bool displayHilight = true;
			if (baseNamingContainer is Spotted.Controls.Calendar)
				displayHilight = !((Spotted.Controls.Calendar)baseNamingContainer).Tickets;


			if (CurrentEvent.HasHilight && displayHilight)
			{
				//MainDiv.Style["border"] = "1px solid #A58319";
				MainDiv.Style["padding"] = "1px 3px 3px 3px";
				//MainDiv.Style["background-color"] = "#FED551";
				MainDiv.Attributes["class"] = "BorderKeyline All BackgroundVeryLight";
			}

			TicketsIcon.Visible = CurrentEvent.IsTicketsAvailable;
			FreeGuestlistIcon.Visible = CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value;

		}
		public void Page_PreRender(object o, System.EventArgs e)
		{
			bool showDate = false;
			if (baseNamingContainer is Spotted.Controls.EventList)
				showDate = !((Spotted.Controls.EventList)baseNamingContainer).ShowDateTitles;

			if (showDate)
				DatePh.Controls.Add(new LiteralControl(" - " + CurrentEvent.FriendlyDate(false)));
		}

		protected string Links
		{
			get
			{
				Model.Entities.ObjectType type;
				if (baseNamingContainer is Spotted.Controls.EventList)
					type = ((Spotted.Controls.EventList)baseNamingContainer).ParentObjectType;
				else
					type = ((Master.DsiPage)Page).Url.ObjectFilterType;

				bool showVenue = true;
				bool showPlace = true;
				bool showCountry = true;
				if (type.Equals(Model.Entities.ObjectType.Venue))
				{
					showVenue = false;
					showPlace = false;
					showCountry = false;
				}
				else if (type.Equals(Model.Entities.ObjectType.Place))
				{
					showPlace = false;
					showCountry = false;
				}
				else if (type.Equals(Model.Entities.ObjectType.Country))
				{
					showCountry = false;
				}
				return CurrentEvent.EventListFriendlyHtml(showVenue, showPlace, showCountry, false);
			}
		}

	}
}
