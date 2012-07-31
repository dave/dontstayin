using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Pages
{
	public partial class TalkToFrank : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Ph.Controls.Clear();

			if (ContainerPage.Url["test"].Exists)
				Ph.Controls.Add(new LiteralControl(Common.Settings.TalkToFrankHtmlTest));
			else
				Ph.Controls.Add(new LiteralControl(Common.Settings.TalkToFrankHtml));
		}
	}
}
