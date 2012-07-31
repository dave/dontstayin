using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Spotted.Pages
{
	public partial class MyAccount : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Bobs.Usr.KickUserIfNotLoggedIn();
		}
	}
}
