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
	public partial class UnsubscribeEflyers : BlankUserControl
	{

		protected Usr CurrentUsr;
		private void Page_Init(object sender, System.EventArgs e)
		{
			if (Usr.Current == null)
			{
				Usr u = new Usr(ContainerPage.Url["UsrK"]);
				if (u.LoginString.ToLower() != ContainerPage.Url["LoginString"].ToString().ToLower())
				{
					Usr.KickUserIfNotLoggedIn("You must be logged in to view your user preferences.");
				}
				else
				{
					CurrentUsr = u;
				}
			}
			else
				CurrentUsr = Usr.Current;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Usr.KickUserIfNotLoggedIn("You must be logged in to view your user preferences.");
			if (!IsPostBack)
			{
				uiSendEflyersOptions.SelectedValue = CurrentUsr.SendFlyers.ToString();
				uiOptionsPanel.Visible = true;
				uiSavedPanel.Visible = false;
			}
		}

		protected void Update(object sender, EventArgs e)
		{
			CurrentUsr.SendFlyers = bool.Parse(uiSendEflyersOptions.SelectedValue);
			CurrentUsr.Update();
			if (!CurrentUsr.SendFlyers) //unsubscribed
			{
				int flyerK = ContainerPage.Url["flyerk"].ValueInt;
				if (flyerK > 0) new Bobs.Flyer(flyerK).LogUnsubscribe();
			}
			uiOptionsPanel.Visible = false;
			uiSavedPanel.Visible = true;
			uiSavedSettingLabel.Text = uiSendEflyersOptions.SelectedItem.Text;
		}

	}
}
