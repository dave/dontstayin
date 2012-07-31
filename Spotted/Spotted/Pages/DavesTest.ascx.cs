using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Pages
{
	public partial class DavesTest : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		#region ServerClick
		protected void ServerClick(object sender, EventArgs eventArgs)
		{
			ServerP.Style["display"] = "";
		}
		#endregion
	}
}
