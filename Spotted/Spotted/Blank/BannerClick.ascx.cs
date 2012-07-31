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

namespace Spotted.Blank
{
	public partial class BannerClick : BlankUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (ContainerPage.Url["BannerK"].IsInt)
			{
				Banner b = new Banner(ContainerPage.Url["BannerK"]);

				b.RegisterClick();
				Response.Redirect(b.LinkTargetUrl);
			}
		}
	}
}
