using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Pages
{
	public partial class MyComments : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Bobs.Usr.Current != null)
				Response.Redirect(Bobs.Usr.Current.UrlMyComments());
			else
				Response.Redirect("/chat");
		}
	}
}
