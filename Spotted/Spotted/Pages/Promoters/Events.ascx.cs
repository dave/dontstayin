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

namespace Spotted.Pages.Promoters
{
	public partial class Events : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		#region PanelEvents
		protected Panel PanelEventsList;
		public void Page_Load(object o, System.EventArgs e)
		{
			ContainerPage.SetPageTitle("Promoter administration");

			BindEvents();
			
		}
		protected string PhotosHtml(Event e)
		{
			if (e.DateTime <= DateTime.Today)
				return "<a href=\"/pages/galleries/add/eventk-" + e.K.ToString() + "\">Add&nbsp;photos</a>";
			else
				return "<small>n/a</small>";
		}
		protected string EditHtml(Event e)
		{
			if (Usr.Current.CanEdit(e))
				return "<a href=\"" + e.UrlApp("edit") + "\">Edit</a>&nbsp;or&nbsp;<a href=\"" + e.UrlApp("edit","page","pic") + "\">" + (e.HasPic ? "edit" : "add") + "&nbsp;pic</a>";
			else
				return "<small>n/a</small>";
		}
		void BindEvents()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Promoter.Columns.K, CurrentPromoter.K);
			q.NoLock = true;
			q.Distinct = true;
			q.DistinctColumn = Event.Columns.K;
			q.TableElement = Event.PromoterJoinWithVenue;
			q.OrderBy = Event.PastEventOrder;
			EventSet es = new EventSet(q);
			EventsGridView.AllowPaging = (es.Count > EventsGridView.PageSize);
			EventsGridView.DataSource = es;
			EventsGridView.DataBind();
		}
		public void EventsGridViewChangePage(object o, GridViewPageEventArgs e)
		{
			EventsGridView.PageIndex = e.NewPageIndex;
			BindEvents();
		}
		#endregion
	}
}
