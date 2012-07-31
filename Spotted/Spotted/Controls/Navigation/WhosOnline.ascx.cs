using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Controls.Navigation
{
	
	public partial class WhosOnline : System.Web.UI.UserControl
	{
		protected void Page_PreRender(object sender, EventArgs e)
		{
			BindOnlineBox();
		}
		#region OnlinePanel
		#region ShowOnlineLinkButtonClick
		public void ShowOnlineLinkButtonClick(object o, System.EventArgs e)
		{
			Prefs.Current["ShowOnline"] = 1;
			BindOnlineBox();
		}
		#endregion
		#region HideOnlineLinkButtonClick
		public void HideOnlineLinkButtonClick(object o, System.EventArgs e)
		{
			Prefs.Current["ShowOnline"] = 0;
			BindOnlineBox();
		}
		#endregion
		#region BindOnlineBox()
		protected void BindOnlineBox()
		{
			if (Prefs.Current["ShowOnline"].Exists && Prefs.Current["ShowOnline"] == 1)
			{
				//Show box
				OnlineHiddenPanel.Visible = false;
				OnlinePanel.Visible = true;
			}
			else
			{
				//show link
				Query q = new Query();
				q.CacheDuration = TimeSpan.FromMinutes(10);
				q.QueryCondition = Usr.LoggedInChatQ;
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				if (us.Count <= 1)
				{
					this.Visible = false;
				}
				else
				{
					Bobs.Global.SetMaxUsers(us.Count);
					OnlineHiddenLabel.Text = us.Count.ToString() + " user" + (us.Count == 1 ? "" : "s") + " online";
					OnlineHiddenPanel.Visible = true;
					OnlinePanel.Visible = false;
				}
			}
			

		}
		#endregion
		#endregion

	}
}
