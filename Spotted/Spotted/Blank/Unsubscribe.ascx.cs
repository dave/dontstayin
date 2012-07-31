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
	public partial class Unsubscribe : BlankUserControl
	{
		protected Panel SubscribedPanel, UnsubscribedPanel, CancelPanel;
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
			this.DataBind();
		}
		private void Page_PreRender(object sender, System.EventArgs e)
		{
			SubscribedPanel.Visible = !CurrentUsr.EmailHold;
			UnsubscribedPanel.Visible = CurrentUsr.EmailHold;
			CancelPanel.Visible = !CurrentUsr.EmailHold;

		}
		public void UnsubscribeClick(object o, System.EventArgs e)
		{
			CurrentUsr.Unsubscribe();
		}

		public void SubscribeClick(object o, System.EventArgs e)
		{
			CurrentUsr.EmailHold = false;
			CurrentUsr.Update();
			Response.Redirect("/");

		}

		protected void LogOff(object sender, System.EventArgs e)
		{
			Log.Increment(Log.Items.WelcomeLogOff);
			Usr.SignOut();
		}

		public void Cancel(object o, System.EventArgs e)
		{
			Response.Redirect("/");
		}
	}
}
