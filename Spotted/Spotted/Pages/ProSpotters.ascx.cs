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
	public partial class ProSpotters : DsiUserControl
	{
		protected DataList ProSpottersDataList;
		private void Page_Load(object sender, System.EventArgs e)
		{
			ContainerPage.SetPageTitle("Pro spotters");
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new Q(Usr.Columns.IsProSpotter, true);
			q.OrderBy = new OrderBy(Usr.Columns.SpottingsTotal, OrderBy.OrderDirection.Descending);
			UsrSet us = new UsrSet(q);
			ProSpottersDataList.DataSource = us;
			ProSpottersDataList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/ProSpotter.ascx");
			ProSpottersDataList.DataBind();
		}
	}
}
