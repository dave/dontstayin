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
	public partial class Shadownap : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SetPageTitle("Shadow Napping");
			PlaceHolder1.Controls.Add(new LiteralControl(Common.Settings.ShadowNapHtml));
		}
	}
}
