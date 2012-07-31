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
	public partial class ChangePassword : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("");
			if (!Page.IsPostBack)
			{
				ChangePanel(PanelChange);
			}
			else
			{
				CurrentPassword.Attributes["value"] = CurrentPassword.Text;
				Password1.Attributes["value"] = Password1.Text;
				Password2.Attributes["value"] = Password2.Text;
			}
		}

		#region CurrentPassword_Val
		protected void CurrentPassword_Val(object sender, ServerValidateEventArgs eventArgs)
		{
			eventArgs.IsValid = Usr.Current.CheckPassword(CurrentPassword.Text.Trim());
		}
		#endregion

		#region Change_Click
		protected void Change_Click(object sender, EventArgs eventArgs)
		{
			if (Page.IsValid)
			{
				Usr.Current.DeleteAllSavedCards();
				Usr.Current.SetPassword(Password2.Text.Trim(), false);
				Usr.Current.LoginString = Cambro.Misc.Utility.GenRandomText(6);
				Usr.Current.Update();

				ChangePanel(PanelDone);
			}
		}
		#endregion

		void ChangePanel(Panel p)
		{
			PanelChange.Visible = p.Equals(PanelChange);
			PanelDone.Visible = p.Equals(PanelDone);
		}

	}
}
