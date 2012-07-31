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
	public partial class SettingsRefresh : BlankUserControl
	{
		public static string Success = "refresh successful";

		protected void Page_Load(object sender, EventArgs e)
		{

			Common.Settings.RefreshAll();
			Response.Clear();
			Response.Write(SettingsRefresh.Success);
			Response.Flush();
			//Response.End();
		}
	}
}
