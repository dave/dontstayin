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
	public partial class EventHomeToday : System.Web.UI.UserControl
	{
		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Event.Columns.K,
					Event.Columns.SpotterRequest,
					Event.Columns.IsTicketsAvailable,
					Event.Columns.UrlFragment,
					Event.Columns.VenueK,
					Event.Columns.DateTime,
					Event.Columns.HasHilight,
					Event.Columns.StartTime,
					Event.Columns.LivePhotos,
					Event.Columns.Name,
					Event.Columns.MusicTypesString,
					Event.Columns.VenueK,
					Venue.Columns.K,
					Venue.LinkColumns
				);
			}
		}

		protected HtmlGenericControl MainP;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (CurrentEvent.HasHilight)
			{
				MainP.Attributes["class"] = "EventHomeToday Hilight";
				//MainP.Style["border"] = "1px solid #A58319";
				//MainP.Style["background-color"] = "#FED551";
				//MainP.Style["padding"] = "3px";
				//MainP.Style["padding-top"] = "2px";
			}

			TicketsIconAnchor.Visible = CurrentEvent.IsTicketsAvailable;
			FreeGuestlistIconAnchor.Visible = CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value;
		}
		public void Page_DataBinding(object o, System.EventArgs e)
		{
			//Page.Trace.Warn("f");
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
