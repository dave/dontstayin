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
	public partial class TicketsLatest : System.Web.UI.UserControl
	{
		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Event.LinkColumns,
					Event.Columns.SpotterRequest,
					Event.Columns.IsTicketsAvailable,
					Event.Columns.DateTime,
					Event.Columns.VenueK,
					Event.Columns.StartTime,
					Event.Columns.LivePhotos,
					Event.Columns.HasHilight,
					Event.Columns.Pic,
					Event.Columns.IsDescriptionText,
					Event.Columns.IsDescriptionCleanHtml,
					Event.Columns.ShortDetailsHtml,
					Event.Columns.MusicTypesString,
					Venue.Columns.Pic,
					Place.Columns.Pic
				);
			}
		}




		private void Page_Load(object sender, System.EventArgs e)
		{

			FreeGuestlistIconAnchor.Visible = CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value;
		}

		public void Page_Init(object o, System.EventArgs e)
		{
			//Strange - CurrentThread is always null if we don't access it in the Init!
			int i = CurrentEvent.K;
		}

		protected string Details
		{
			get
			{
				string html = CurrentEvent.ShortDetailsHtml;
				bool addLink = false;

				if (html.Length > 200)
				{
					addLink = true;
					html = html.Substring(0, 190);
				}

				html = html.Replace("\n", string.Empty);

				html = Cambro.Web.Helpers.MakeHtml(html, false);

				if (addLink)
					return html + "... (<a href=\"" + CurrentEvent.Url() + "\">more</a>)<br />";
				else if (html.Length > 0)
					return html + "<br />";
				else
					return "";

			}
		}
		protected string MusicTypeText
		{
			get
			{
				if (CurrentEvent.MusicTypesString.Length > 0)
					return "Music : " + CurrentEvent.MusicTypesString;
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

		protected string Links
		{
			get
			{
				object parent = null;
				if (NamingContainer.NamingContainer.NamingContainer.NamingContainer is Controls.Latest)
					parent = ((Controls.Latest)NamingContainer.NamingContainer.NamingContainer.NamingContainer).Parent;

				bool showVenue = true;
				bool showPlace = true;
				bool showCountry = true;
				if (parent != null && parent is Venue)
				{
					showVenue = false;
					showPlace = false;
					showCountry = false;
				}
				else if (parent != null && parent is Place)
				{
					showPlace = false;
					showCountry = false;
				}
				else if (parent != null && parent is Country)
				{
					showCountry = false;
				}
				return CurrentEvent.EventListFriendlyHtml(showVenue, showPlace, showCountry, false);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			
		}
		#endregion
	}
}
