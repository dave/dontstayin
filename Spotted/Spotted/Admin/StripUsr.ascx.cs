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

namespace Spotted.Admin
{
	public partial class StripUsr : AdminUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			DeleteButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			if (!Page.IsPostBack)
			{
				if (Request.QueryString["UsrK"] != null)
					ObjectKTextBox.Text = Request.QueryString["UsrK"];
			}

		}
		public void DeleteNow(object o, System.EventArgs e)
		{
			Usr c = new Usr(int.Parse(ObjectKTextBox.Text));
			if (!c.Banned)
				throw new DsiUserFriendlyException("User should be banned first!");

			c.StripAll();

			DoneLabel.Visible = true;
		}
	}
}
