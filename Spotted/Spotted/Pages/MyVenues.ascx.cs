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

namespace Spotted.Pages
{
	public partial class MyVenues : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			ContainerPage.SetPageTitle("My venues");
			BindVenues();
		}


		#region VenuesPanel
		protected Panel VenuesPanel;
		protected DataGrid VenuesDataGrid;
		void BindVenues()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Venue.Columns.OwnerUsrK, Usr.Current.K);
			q.TableElement = new Join(Venue.Columns.PlaceK, Place.Columns.K);
			q.OrderBy = new OrderBy(new OrderBy(Place.Columns.Name), new OrderBy(Venue.Columns.Name));
			VenueSet vs = new VenueSet(q);
			if (vs.Count > 0)
			{
				VenuesDataGrid.AllowPaging = (vs.Count > VenuesDataGrid.PageSize);
				VenuesDataGrid.DataSource = vs;
				VenuesDataGrid.DataBind();
			}
			else
				VenuesPanel.Visible = false;

		}
		public void VenuesDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			VenuesDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindVenues();
		}
		protected string EditHtml(Venue v)
		{
			if (Usr.Current.CanEdit(v))
			{
				string part = "<a href=\"" + v.UrlApp("edit") + "\">Edit&nbsp;details</a>";
				if (v.HasPic)
					return part;
				else
					return part + "&nbsp;or&nbsp;<a href=\"" + v.UrlApp("edit","page","pic") + "\">add&nbsp;a&nbsp;picture</a>";
			}
			else
				return "<small>n/a</small>";
		}
		#endregion

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
