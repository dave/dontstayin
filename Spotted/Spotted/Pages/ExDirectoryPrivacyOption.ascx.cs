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
using Bobs;

namespace Spotted.Pages
{
	public partial class ExDirectoryPrivacyOption : DsiUserControl
	{
		protected void Page_Init(object o, EventArgs e)
		{
			if (!IsPostBack && Prefs.Current["SetExDirectoryOption"].IsNull) Prefs.Current["SetExDirectoryOption"] = 1;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			((Spotted.Master.DsiPage)Page).SetPageTitle("Confirm privacy options");

			if (!IsPostBack)
			{
				uiExDirectory.Checked = Usr.Current.ExDirectory;
			}
		}

		protected void Save_Click(object sender, EventArgs e)
		{
			Usr.Current.ExDirectory = uiExDirectory.Checked;
			Usr.Current.Update();
			uiSuccess.Text = "Details updated";
		}
	}
}
