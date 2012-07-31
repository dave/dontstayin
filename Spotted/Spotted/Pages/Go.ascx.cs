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
	public partial class Go : DsiUserControl
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			string tag = ContainerPage.Url[0].Raw;
			Visit.Current.IsFromExternal = true;
			Visit.Current.ExternalTag = tag;
			Visit.Current.Update();

		//	try
		//	{
				Query q = new Query();
				q.QueryCondition = new And(new Q(Photo.Columns.PhotoOfWeek, true), new Q(Photo.Columns.PhotoOfWeekDateTime, QueryOperator.GreaterThan, DateTime.Now.AddDays(-30)));
				//q.QueryCondition = new Q(Photo.Columns.PhotoOfWeek, true);
				q.TopRecords = 1;
				q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
				PhotoSet ps = new PhotoSet(q);
				Response.Redirect(ps[0].Url());
		//	}
		//	catch
		//	{
		//		Response.Redirect("/");
		//	}
		}
	}
}
