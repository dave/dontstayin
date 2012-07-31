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

namespace Spotted.Admin
{
	public partial class FlyerStats : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Query q = new Query();
				q.OrderBy = new OrderBy(Flyer.Columns.SendDateTime, OrderBy.OrderDirection.Descending);
				if (!ContainerPage.Url["all"].Exists)
					q.TopRecords = 20;
				uiGridView.DataSource = new FlyerSet(q);
				uiGridView.DataBind();
			}
		}
	}
}
