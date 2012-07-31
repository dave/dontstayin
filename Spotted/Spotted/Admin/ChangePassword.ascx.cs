using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Admin
{
	public partial class ChangePassword : Spotted.AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack && Page.Request["usrk"] != null)
			{
				UsrK.Text = Page.Request["usrk"];
			}

		}
		protected void Change(object sender, EventArgs e)
		{
			Usr u = new Usr(int.Parse(UsrK.Text));
			if (!u.IsAdmin)
			{
				u.SetPassword(UsrPassword.Text, true);
				OutLabel.Text = "Done.";
			}
			else
				OutLabel.Text = "Can't change an admin password on this page.";
		}
	}
}
