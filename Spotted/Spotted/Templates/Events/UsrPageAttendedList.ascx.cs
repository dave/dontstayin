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

namespace Spotted.Templates.Events
{
	public partial class UsrPageAttendedList : System.Web.UI.UserControl
	{
		protected HtmlImage Pic, PicAlt, BigPic;
		protected HtmlAnchor PicAnchor;

		public static ColumnSet Columns
		{
			get
			{
				return Usr.UsrPageAttendedListColumns;
			}
		}
		public static TableElement PerformJoins(TableElement tIn)
		{
			return Usr.UsrPageAttendedListPerformJoins(tIn);
		}

		protected string SpotterImg
		{
			get
			{
				if (CurrentEvent.JoinedUsrEventAttend.Spotter)
				{
					string file = "";
					if (((Pages.Usrs.Home)this.NamingContainer.NamingContainer.NamingContainer).ThisUsr.IsProSpotter)
						file = "/gfx/icon-prospotter.png";
					else
						file = "/gfx/icon-spotter.png";

					return "<img src=\"" + file + "\" onmouseout=\"htm();\" onmouseover=\"stt('I" + (CurrentEvent.IsFuture ? "\\'m" : " was") + " a spotter at this event');\" align=\"left\" style=\"margin-top:1px;margin-right:2px;\">";

				}
				else
					return "";
			}
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			Pic.Visible = CurrentEvent.HasAnyPic;

			FreeGuestlistIconAnchor.Visible = CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value;

			//	if (CurrentEvent.HasAnyPic && !Vars.Netscape)
			//	{
			//		string bigPic = BigPic.ClientID;
			//		PicAnchor.Attributes["onmouseover"]="document.getElementById('"+bigPic+"').style.display='';";
			//		PicAnchor.Attributes["onmouseout"]="document.getElementById('"+bigPic+"').style.display='none';";
			//	}
			//	else
			BigPic.Visible = false;
			TicketsIconAnchor.Visible = CurrentEvent.IsTicketsAvailable;


		}
		protected string EventText
		{
			get
			{
				return CurrentEvent.ShortDetailsHtmlRender;
			}
		}
		protected string MusicTypeText
		{
			get
			{
				if (CurrentEvent.MusicTypesString.Length > 0)
					return "<b>Music</b> : " + CurrentEvent.MusicTypesString;
				else
					return "";
			}
		}


		protected Event CurrentEvent
		{
			get
			{
				if (currentEvent == null)
					currentEvent = ((Event)((DataListItem)NamingContainer).DataItem);
				return currentEvent;
			}
		}
		Event currentEvent;


	}
}
