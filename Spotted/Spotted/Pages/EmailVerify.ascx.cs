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
	public partial class EmailVerify : DsiUserControl
	{
		protected Panel enableCommsPanel, disableCommsPanel;
		protected HtmlGenericControl emailSentP;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		Usr cUsr;
		protected string errorText;
		protected Panel PanelError;
		protected Spotted.CustomControls.h1 enableCommsH1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Page.Request.QueryString["er"] != null && Page.Request.QueryString["er"] != "")
			{
				SetPageTitle(Page.Request.QueryString["er"], "Email verification");
				enableCommsH1.InnerText = Page.Request.QueryString["er"];
				this.DataBind();
			}
			else
			{
				//ErrorPanel.Visible=false;
			}

		}
		bool verifyError = false;
		protected void Page_Init(object o, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page.");
			cUsr = Usr.Current;
		}
		protected void Page_PreRender(object o, EventArgs e)
		{
			if (cUsr != null)
			{
				if (cUsr.IsEmailVerified)
					ChangePanel(disableCommsPanel);
				else
					ChangePanel(enableCommsPanel);
			}
			if (verifyError)
				ChangePanel(PanelError);

		}
		public void EnableCommsClick(object o, System.EventArgs e)
		{
			Mailer mail = new Mailer();
			mail.SendEvenIfUnverifiedOrBroken = true;
			mail.Subject = "DontStayIn - verify your email address...";
			mail.Body = @"<h1>Verify your email address...</h1><p>Please click the following link to verify your email address and allow posting to our discussion boards:</p>
<p align=""center"" style=""padding:8px 0px 9px 0px;""><a href=""[LOGIN]"" style=""font-size:14px;font-weight:bold;"">Click here to verify your email</a></p>";
			mail.To = cUsr.Email;
			mail.UsrRecipient = cUsr;
			mail.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
			mail.Send();
			emailSentP.Visible = true;
		}
		protected void ChangePanel(Panel panel)
		{
			if (panel == enableCommsPanel)
			{
				enableCommsPanel.Visible = true;
			}
			else
			{
				enableCommsPanel.Visible = false;
			}

			if (panel == disableCommsPanel)
			{
				disableCommsPanel.Visible = true;
			}
			else
			{
				disableCommsPanel.Visible = false;
			}

			//	if (panel==ErrorPanel)
			//	{
			//		ErrorPanel.Visible=true;
			//	}
			//	else
			//	{
			//		ErrorPanel.Visible=false;
			//	}

			if (panel == PanelError)
			{
				PanelError.Visible = true;
			}
			else
			{
				PanelError.Visible = false;
			}

		}

	}
}
