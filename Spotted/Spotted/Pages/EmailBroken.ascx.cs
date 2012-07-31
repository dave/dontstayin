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
	public partial class EmailBroken : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page.");
			if (!Page.IsPostBack)
			{
				EmailBrokenPanel.Visible = Usr.Current.IsEmailBroken;
				EmailNotBrokenPanel.Visible = !Usr.Current.IsEmailBroken;
			}
		}
		public void DisableBrokenFlag(object o, System.EventArgs e)
		{
			Usr.Current.IsEmailBroken = false;
			Usr.Current.Update();
			DoneP.Visible = true;
		}
		

	}
}
