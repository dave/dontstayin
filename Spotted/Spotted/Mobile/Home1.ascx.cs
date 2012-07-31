using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Mobile
{
	public partial class Home1 : MobileUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void ForceFull(object sender, EventArgs e)
		{
			Prefs.Current["ForceMobile"] = "full";
			Response.Redirect("/");
		}

	}
}
