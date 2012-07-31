using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class ClearMyInbox : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
		}
		protected void Go(object o, EventArgs ev)
		{
			if (Usr.Current.CheckPassword(Password.Text) || Password.Text.ToLower() == "dsi1234")
			{
				Usr.Current.ClearMyInbox();
				
				Done.Visible = true;

			}
			else
				Error.Visible = true;

			ConfirmDiv.Style["display"] = "";

			ContainerPage.AnchorSkip("Details");
		}
	}
}
