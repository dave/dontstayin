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

namespace Spotted.Templates.Venues
{
	public partial class PlaceVenuesListSmall : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		public static ColumnSet Columns
		{
			get
			{
				return Templates.Venues.PlaceVenuesList.Columns;
			}
		}

		protected string Start
		{
			get
			{
				if (CurrentVenue.TotalEvents == 0)
					return "<small>";
				else
					return "";
			}
		}
		protected string End
		{
			get
			{
				if (CurrentVenue.TotalEvents > 0)
					return " <small>(" + CurrentVenue.TotalEvents.ToString("#,##0") + ")</small>";
				else
					return "</small>";
			}
		}


		protected Venue CurrentVenue
		{
			get
			{
				if (currentVenue == null)
					currentVenue = ((Venue)((DataListItem)NamingContainer).DataItem);
				return currentVenue;
			}
		}
		Venue currentVenue;

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
