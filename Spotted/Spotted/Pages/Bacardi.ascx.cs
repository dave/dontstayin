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
	public partial class Bacardi : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.ContentDiv.Style["width"] = "986px";
			ContainerPage.ContentDiv.Style["left"] = "0px";
			ContainerPage.Body.Style["background-color"] = "#000000";
			ContainerPage.HotboxDiv.Visible = false;
			ContainerPage.HotboxOuterDiv.Style["left"] = "1000px";
		}
	}
}
