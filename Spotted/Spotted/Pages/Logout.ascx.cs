using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class Logout : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current != null)
			{
				Usr.Current.SendLogoutChatAlert();
				Usr.Current.IsLoggedOn = false;
				Usr.Current.Update();

				Cambro.Web.Helpers.DeleteCookie("SpottedAuthFix");

				Usr.Current = null;
			}
			
		}
	}
}
