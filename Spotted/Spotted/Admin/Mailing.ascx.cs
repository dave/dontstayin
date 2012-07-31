using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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
	public partial class Mailing : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Vars.DevEnv)
			{
				Query q = new Query();
				q.QueryCondition = new Or(
					new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.New),
					new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack),
					new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.NeedCards),
					new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingRefill)
				);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				TitleLabel.Text = us.Count.ToString("#,##0") + " item" + (us.Count == 1 ? "" : "s");
			}
		}
		protected void MarkSent(object o, EventArgs e)
		{
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new Or(
				new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack),
				new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingRefill));
			UsrSet us = new UsrSet(q);
			foreach (Usr u in us)
			{
				if (u.CardStatus.Equals(Usr.CardStatusEnum.PrintingWelcomePack))
				{
					u.CardStatus = Usr.CardStatusEnum.WelcomePackInPost;
				}
				else
				{
					u.CardStatus = Usr.CardStatusEnum.CardsInPost;
				}
				u.TotalCardsSent += 360;
				u.Update();
			}
			DoneLabel.Visible = true;
		}
	}
}
