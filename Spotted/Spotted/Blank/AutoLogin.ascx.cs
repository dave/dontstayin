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

namespace Spotted.Blank
{
	public partial class AutoLogin : BlankUserControl
	{
		protected TextBox PasswordTextBox;
		protected HtmlGenericControl ErrorP;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Page.Response.Expires = 0;
			// Put user code to initialize the page here
			this.DataBind();
			if (Page.IsPostBack)
			{
				if (AttemptUsr.CheckPassword(PasswordTextBox.Text))
				{
					AttemptUsr.LogInAsThisUser(false);
					Response.Redirect(ContainerPage.Url.LoggedInPlainUrl);
				}
				else
				{
					ErrorP.Visible = true;
				}
			}
		}
		public void LogOn_Click(object o, System.EventArgs e)
		{

		}
		public void Cancel_Click(object o, System.EventArgs e)
		{
			Response.Redirect(ContainerPage.Url.LoggedInPlainUrl);
		}
		#region AttemptUsr
		public Usr AttemptUsr
		{
			get
			{
				if (attemptUsr == null)
				{
					attemptUsr = new Usr(ContainerPage.Url.LoginPasswordPageUsrK);
				}
				return attemptUsr;
			}
			set
			{
				attemptUsr = value;
			}
		}
		private Usr attemptUsr;
		#endregion
	}
}
