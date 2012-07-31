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
	public partial class InviteFriends : BlankUserControl
	{
		protected void Page_Init(object sender, System.EventArgs e)
		{
			this.SslPage = true;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			ScriptManager.RegisterStartupScript(Page, typeof(Page), "Tip", "mig_hand();", true);

			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page.");

			if (!IsPostBack)
			{
				uiBuddyImporterDiv.Style.Add("visibility", "hidden");
			}
			else
			{
				uiBuddyImporterDiv.Style.Remove("visibility");
			}

			uiBuddyImporter.OnDone += new EventHandler(uiBuddyImporter_OnDone);
		}

		void uiBuddyImporter_OnDone(object sender, EventArgs e)
		{
			this.uiSkipButton.Visible = false;
			this.uiGoToSiteButton.Visible = true;
			this.uiFinishedPanel.Visible = true;
		}

		protected void Skip(object o, EventArgs e)
		{
			Bobs.Log.Increment(Bobs.Log.Items.SkipInviteFriends);
			GoToSite(o, e);
		}
		protected void GoToSite(object sender, EventArgs e)
		{
			if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
				Response.Redirect(Request.QueryString["Url"]);
			else
				Response.Redirect("/");
		}

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
		}
		#endregion
	}
}
