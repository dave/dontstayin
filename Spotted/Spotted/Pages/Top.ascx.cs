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

namespace Spotted.Pages
{
	public partial class Top : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (ContainerPage.Url["photos"].Exists)
				ContainerPage.SetPageTitle("Top photos");
			else if (ContainerPage.Url["videos"].Exists)
				ContainerPage.SetPageTitle("Top videos");
			else
				ContainerPage.SetPageTitle("Top photos / videos");
		}
	}
}
