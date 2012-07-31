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
	public partial class Password : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.None))
				{
					ChangePanel(PanelPassword);
				}
				else if (Mode.Equals(Modes.Reset))
				{
					if (CurrentUsr == null)
					{
						ChangePanel(PanelResetError);
					}
					else if (!CurrentUsr.PasswordResetEmailSecret.ToLower().Equals(ContainerPage.Url["reset"].ToString().ToLower()) ||
						CurrentUsr.PasswordResetEmailSecret.Length == 0)
					{
						ChangePanel(PanelResetError);
					}
					else
					{
						ChangePanel(PanelReset);
					}
				}
				else if (Mode.Equals(Modes.ResetCancel))
				{
					if (CurrentUsr == null)
					{
						ChangePanel(PanelResetError);
					}
					else if (CurrentUsr.PasswordResetEmailSecret.ToLower().Equals(ContainerPage.Url["reset"].ToString().ToLower()))
					{
						CurrentUsr.PasswordResetEmailSecret = "";
						CurrentUsr.Update();
					}
					Response.Redirect("/pages/password/cancelled");
				}
				else if (Mode.Equals(Modes.Done))
				{
					ChangePanel(PanelResetDone);
				}
				else if (Mode.Equals(Modes.Cancelled))
				{
					ChangePanel(PanelResetCancelled);
				}
			}
		}

		#region CurrentUsr
		public Usr CurrentUsr
		{
			get
			{
				if (currentUsr == null && ContainerPage.Url["k"].IsInt)
					currentUsr = new Usr(ContainerPage.Url["k"]);
				return currentUsr;
			}
			set
			{
				currentUsr = value;
			}
		}
		Usr currentUsr;
		#endregion

		#region PanelPassword
		private void PanelPassword_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None))
			{
			}
		}
		public void SendReminder(object o, System.EventArgs e)
		{
			Query q = new Query();
			q.QueryCondition = new Or(new Q(Usr.Columns.NickName, EmailTextBox.Text.Trim()), new Q(Usr.Columns.Email, EmailTextBox.Text.Trim()));
			UsrSet us = new UsrSet(q);
			if (us.Count != 1)
			{
				ErrorPanel.Visible = true;
			}
			else
			{
				us[0].SendPasswordResetLink();
				DonePanel.Visible = true;
			}
		}

		#endregion

		#region PanelReset
		protected void PasswordResetChange_Click(object sender, System.EventArgs e)
		{
			if (CurrentUsr != null &&
				Mode.Equals(Modes.Reset) &&
				CurrentUsr.PasswordResetEmailSecret.ToLower().Equals(ContainerPage.Url["reset"].ToString().ToLower()) &&
				CurrentUsr.PasswordResetEmailSecret.Length == 10 &&
				Page.IsValid)
			{

				CurrentUsr.DeleteAllSavedCards();
				CurrentUsr.SetPassword(Password1.Text.Trim(), false);
				CurrentUsr.LoginString = Cambro.Misc.Utility.GenRandomText(6);
				CurrentUsr.PasswordResetEmailSecret = String.Empty;

				CurrentUsr.Update();

				Response.Redirect("/pages/password/done");
			}
			else
				ChangePanel(PanelResetError);
		}
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["reset"].Exists)
				{
					if (ContainerPage.Url["cancel"].Exists)
						return Modes.ResetCancel;
					else
						return Modes.Reset;
				}
				else if (ContainerPage.Url["done"].Exists)
					return Modes.Done;
				else if (ContainerPage.Url["cancelled"].Exists)
					return Modes.Cancelled;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			Reset,
			ResetCancel,
			Done,
			Cancelled
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelPassword.Visible = p.Equals(PanelPassword);
			PanelReset.Visible = p.Equals(PanelReset);
			PanelResetCancelled.Visible = p.Equals(PanelResetCancelled);
			PanelResetDone.Visible = p.Equals(PanelResetDone);
			PanelResetError.Visible = p.Equals(PanelResetError);
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
			this.Load += new System.EventHandler(this.PanelPassword_Load);
		}
		#endregion
	}
}
