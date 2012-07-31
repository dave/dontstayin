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
	public partial class EventDisplay1 : EventDisplay
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
			//TicketsIconAnchor.Visible = CurrentEvent.IsTicketsAvailable;
		}
		public void Page_PreRender(object o, System.EventArgs e)
		{
			bool showDate = false;
			if (baseNamingContainer is Spotted.Controls.EventList)
				showDate = !((Spotted.Controls.EventList)baseNamingContainer).ShowDateTitles;

			if (showDate)
				DatePh.Controls.Add(new LiteralControl(" - " + CurrentEvent.FriendlyDate(false)));
		}
		protected string TicketsIconHtml
		{
			get
			{
				if (CurrentEvent.IsTicketsAvailable)
					return "<div style=\"float:left;\"><a href=\"" + CurrentEvent.Url() + "\"><img src=\"/gfx/icon-tickets-small.png\" width=\"20\" height=\"16\" align=\"left\" border=\"0\" style=\"margin-top:2px; margin-right:0px;\" onmouseover=\"stt('Tickets available');\" onmouseout=\"htm();\" /></a></div>";
				else
					return "";
			}
		}
		protected string FreeGuestlistIconHtml
		{
			get
			{
				if (CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value)
					return "<div style=\"float:left;\"><a href=\"" + CurrentEvent.Url() + "\"><img src=\"/gfx/icon-freeguestlist-small.png\" width=\"20\" height=\"16\" align=\"left\" border=\"0\" style=\"margin-top:2px; margin-right:0px;\" onmouseover=\"stt('Free Guestlist available');\" onmouseout=\"htm();\" /></a></div>";
				else
					return "";
			}
		}
		protected string EventText
		{
			get
			{
				return CurrentEvent.ShortDetailsHtmlGeneric(false, 200, false);
			}
		}
		protected string MusicTypeText
		{
			get
			{
				if (CurrentEvent.MusicTypesString.Length > 0)
					return "<div style=\"margin-top:4px;\"><small><b>Music</b> : " + CurrentEvent.MusicTypesString + "</small></div>";
				else
					return "";
			}
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

				//ObjectType type = ((Spotted.Controls.EventList)NamingContainer.NamingContainer.NamingContainer).ParentObjectType;
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
