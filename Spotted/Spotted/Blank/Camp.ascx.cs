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

namespace Spotted.Blank
{
	public partial class Camp : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Query q = new Query();
			q.QueryCondition=new Q(Usr.Columns.CampTickets, QueryOperator.GreaterThan, 0);
			q.OrderBy = new OrderBy(Usr.Columns.NickName);
			UsrSet us = new UsrSet(q);
			foreach (Usr u in us)
			{
				Response.Write(u.NickName + " - " + u.CampTickets + "<br>");
			}
		}
	}
}
