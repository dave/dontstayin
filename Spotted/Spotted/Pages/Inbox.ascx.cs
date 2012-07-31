using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using System.Text;

namespace Spotted.Pages
{
	public partial class Inbox : DsiUserControl
	{


		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page");
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Inbox))
					ChangePanel(PanelInbox);

				AddThread.AddThreadAdvancedCheckBox.Checked = true;
				AddThread.AddThreadPublicRadioButton.Checked = false;
				AddThread.AddThreadPrivateRadioButton.Checked = true;

				InboxEmailsNo.Checked = Usr.Current.NoInboxEmails;
				InboxEmailsYes.Checked = !Usr.Current.NoInboxEmails;

				//SetupBuddyDropDownList();
				//SetupGroupDropDownList();
			}
			InboxControl.CaptureUrlParameters();

			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}

		#region PanelInbox

		protected void Go(object o, EventArgs ev)
		{
			if (Usr.Current.CheckPassword(Password.Text))
			{

				Usr.Current.ClearMyInbox();

				Response.Redirect("/pages/inbox");

			}
			else
			{
				Error.Visible = true;
				ClearMyInbox.Style["display"] = "";
				ContainerPage.AnchorSkip("ClearMyInbox");
			}
			
		}

		public void UpdateInboxEmails(object o, System.EventArgs e)
		{
			Usr.Current.NoInboxEmails = InboxEmailsNo.Checked;
			Usr.Current.Update();

			InboxEmailsNo.Checked = Usr.Current.NoInboxEmails;
			InboxEmailsYes.Checked = !Usr.Current.NoInboxEmails;
		}
		
		private void PanelInbox_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Inbox))
			{
				InboxControl.BindThreads();
			}
            if (!this.IsPostBack)
            {
                //ClearInboxButton.Attributes["onclick"] = "if(confirm('Are you sure you want to clear your inbox? You can still find these messages by clicking Watching.') && confirm('Are you really sure you want to clear your inbox?')){__doPostBack('" + ClearInboxButton.UniqueID + "','');return false;}else{return false;};";
            }
		}
		#endregion


		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("xxx"))
					return Modes.XXX;
				else
					return Modes.Inbox;
			}
		}
		public enum Modes
		{
			Inbox,
			XXX
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelInbox.Visible = p.Equals(PanelInbox);
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.PanelInbox_Load);
		}
		#endregion
	}
}
