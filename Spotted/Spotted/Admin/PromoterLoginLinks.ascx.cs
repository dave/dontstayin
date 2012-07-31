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
	public partial class PromoterLoginLinks : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Query q = new Query();
			q.TopRecords = 100;
			q.TableElement = new Join.Series(Usr.Columns.K, PromoterUsr.Columns.UsrK, PromoterUsr.Columns.PromoterK, Promoter.Columns.K);
			q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);

			UsrSet us = new UsrSet(q);
			foreach (Usr u in us)
			{
				Ph.Controls.Add(new LiteralControl(u.JoinedPromoterUsr.Promoter.GetUpcomingEvents(false).Count.ToString() + " <a href=\"" + u.LoginAndTransferShort(u.JoinedPromoterUsr.Promoter.Url()) + "\" target=\"_blank\">" + u.JoinedPromoterUsr.Promoter.UrlName + " - " + u.NickName + "</a><br>"));
			}
		}
	}
}
