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

namespace Spotted.Pages
{
	public partial class Cameras : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Log.Increment(Log.Items.CamerasPage);
		}
	}
}
