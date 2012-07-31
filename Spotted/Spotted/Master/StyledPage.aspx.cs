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
using Common;

namespace Spotted.Master
{
	public partial class StyledPage : GenericPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsSkeleton && !Usr.Current.IsTicketsRegistered)
			{
				// log off skeleton users
				LogOffUsr();
			}
			SetupWelcomeDiv();

			if(StyledObject.StyledCss.Length > 0)
				StyleTag.Href = StyledObject.UrlCss();
			else
			    StyleTag.Href = "/misc/styled/default.css";
		}

		#region StyledObject
		public Bobs.IStyledEventHolder StyledObject
		{
			get
			{
				return (Bobs.IStyledEventHolder)Url.ObjectFilterBob;
			}
		}
		#endregion

		protected void Page_PreRender(object sender, EventArgs e)
		{
			PageTitleTag.Text = Title;
		}
		#region AnchorSkip
		public Panel AnchorSkipJs;
		public PlaceHolder AnchorSkipName;
		public void AnchorSkip(string AnchorName)
		{
			AnchorSkipName.Controls.Clear();
			AnchorSkipName.Controls.Add(new LiteralControl(AnchorName));
			AnchorSkipJs.Visible = true;
		}
		#endregion
		#region SetPageTitle
		public void SetPageTitle(string title)
		{
			Title = title;
		}
		#endregion

		private void SetupWelcomeDiv()
		{
			if (Usr.Current != null)
			{
				this.UserName.Text = Usr.Current.FirstName;
				this.LogOnOffLinkButton.Text = "log off";
			}
			else
			{
				this.UserName.Text = "Guest";
				this.LogOnOffLinkButton.Text = "log on";
			}
		}

		protected void LogOnOffLinkButton_Click(object sender, EventArgs e)
		{
			if (Usr.Current == null)
			{
				// Redirect to login screen
				Response.Redirect(this.StyledObject.UrlStyledApp("login") + "?url=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
			}
			else
			{
				// Logoff user and redirect to main screen
				LogOffUsr();
				Response.Redirect(StyledObject.UrlStyled());
			}
		}

		private void LogOffUsr()
		{
			if (Usr.Current != null)
			{
				Log.Increment(Log.Items.WelcomeLogOff);
				Usr.SignOut();
			}
		}
	}
}
