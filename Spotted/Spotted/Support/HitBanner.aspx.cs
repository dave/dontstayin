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

namespace Spotted.Support
{
	public partial class HitBanner : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Banner b = new Banner(int.Parse(Request.QueryString["K"]));
			b.RegisterHit();
		}
	}
}
