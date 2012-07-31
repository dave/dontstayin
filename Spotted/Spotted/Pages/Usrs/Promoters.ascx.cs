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

namespace Spotted.Pages.Usrs
{
	public partial class Promoters : UsrUserControl
	{
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotAdmin("");
			ThisUsr.PromotersClear();
			PromoterSet ps = ThisUsr.Promoters(new ColumnSet(Promoter.Columns.UrlName, Promoter.Columns.Name));
			if (ps.Count == 0)
			{
				ChangePanel(PanelNoAccount);
			}
			else
			{
				ChangePanel(PanelPromoterList);
				PromoterRepeater.DataSource = ps;
				PromoterRepeater.DataBind();
			}
		}

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelNoAccount.Visible = p.Equals(PanelNoAccount);
			PanelPromoterList.Visible = p.Equals(PanelPromoterList);
		}
		#endregion
	}
}
