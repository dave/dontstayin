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
using System.Text.RegularExpressions;

namespace Spotted.Pages
{
	public partial class MyDetails : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view your user preferences.");
			Response.Redirect(Usr.Current.UrlApp("edit"));
		}
	}
}
