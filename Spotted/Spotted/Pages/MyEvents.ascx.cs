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
	public partial class MyEvents : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			ContainerPage.SetPageTitle("My events");
			BindEvents();
		}

		#region EventsPanel
		protected Panel EventsPanel;
		protected DataGrid EventsDataGrid;
		protected string PhotosHtml(Event e)
		{
			if (e.DateTime <= DateTime.Today)
				return "<a href=\"/pages/galleries/add/eventk-" + e.K.ToString() + "\">Add&nbsp;photos</a>";
			else
				return "<small>n/a</small>";
		}
		protected string ReviewHtml(Event e)
		{
			if (e.DateTime <= DateTime.Today)
				return "<a href=\"" + e.UrlApp("review") + "\">Review</a>";
			else
				return "<small>n/a</small>";
		}
		protected string EditHtml(Event e)
		{
			if (Usr.Current.CanEdit(e))
			{
				string part = "<a href=\"/event-" + e.K.ToString() + "/edit\">Edit</a>";
				if (e.HasPic)
					return part;
				else
					return part + "&nbsp;or&nbsp;<a href=\"/event-" + e.K.ToString() + "/edit/page-pic\">add&nbsp;pic</a>";
			}
			else
				return "<small>n/a</small>";
		}
		void BindEvents()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Event.Columns.OwnerUsrK, Usr.Current.K);
			q.OrderBy = Event.PastEventOrder;
			EventSet es = new EventSet(q);
			if (es.Count > 0)
			{
				EventsDataGrid.AllowPaging = (es.Count > EventsDataGrid.PageSize);
				EventsDataGrid.DataSource = es;
				EventsDataGrid.DataBind();
			}
			else
				EventsPanel.Visible = false;
		}
		public void EventsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			EventsDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindEvents();
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
