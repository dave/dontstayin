using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Blank
{
	public partial class Mixmag : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["go"] != null)
			{
				if (Usr.Current == null)
					Response.Redirect("http://www.mixmag-online.com/");
				else
				{
					Prefs.Current["ActivityMixmagDone"] = 1;
					Response.Redirect("http://www.mixmag-online.com/?k=" + Usr.Current.K + "&s=" + Usr.Current.LoginString.ToLower());
				}
			}



		}
		protected void Skip(object o, EventArgs e)
		{
			if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
				Response.Redirect(Request.QueryString["Url"]);
			else
				Response.Redirect("/popup/invitefriends");
		}
	}
}
