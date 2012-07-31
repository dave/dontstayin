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
	public partial class Login : DsiUserControl
	{
		protected Button LogoutButton;
		protected Panel LoginPanel, LoggedInPanel;
		protected Label loginCountLabel;
		protected HtmlAnchor PublicProfileLink;
		protected string ErrorText
		{
			get
			{
				if (Page.Request.QueryString["er"] != null)
					return Page.Request.QueryString["er"].Replace("\\n", "<br>");
				else
					return "Log on page";
			}
		}
		

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.ContainerPage.SetPageTitle("Log on");
			
			
			if (Usr.Current == null)
			{
				//ContainerPage.ContentDiv.Visible = false;
				//ContainerPage.NavLogin.ShowHideSpan.Style["display"] = "";
				//ContainerPage.NavLogin.LogOnLink.Style["display"] = "none";
				ChangePanel(LoginPanel);
				
			}
			else
			{
				ChangePanel(LoggedInPanel);
			}
			
		}
		#endregion

		protected string thisUrl
		{
			get
			{
				return Request.RawUrl;
			}
		}

		#region ChangePanel
		protected void ChangePanel(Panel panel)
		{

			if (panel == LoginPanel)
			{
				LoginPanel.Visible = true;
			}
			else
			{
				LoginPanel.Visible = false;
			}

			if (panel == LoggedInPanel)
			{
				LoggedInPanel.Visible = true;
			}
			else
			{
				LoggedInPanel.Visible = false;
			}
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
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
