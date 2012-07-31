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

namespace Spotted.Blank
{
	public partial class FlyerClick : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int flyerK = ContainerPage.Url["k"].ValueInt;
			if (flyerK > 0)
			{
				Bobs.Flyer f = new Bobs.Flyer(flyerK);
				f.LogClick();
				Response.Redirect(f.LinkTargetUrl);
			}

		}
	}
}
