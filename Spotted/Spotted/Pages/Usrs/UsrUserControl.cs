using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;

namespace Spotted.Pages.Usrs
{
	public class UsrUserControl : DsiUserControl
	{
		Panel BannedUserPanel = new Panel();
		Panel UnsubscribedUserPanel = new Panel();
		//Panel UnverifiedEmailUserPanel = new Panel();

		public Usr ThisUsr
		{
			get
			{
				if (thisUsr == null)
				{
					if (ContainerPage.Url.HasUsrObjectFilter)
						thisUsr = ContainerPage.Url.ObjectFilterUsr;
				}
				return thisUsr;
			}
			set
			{
				thisUsr = value;
			}
		}
		Usr thisUsr;

		protected virtual void Page_Init(object sender, System.EventArgs e)
		{
			RunThisUsrFilter();
		}

		public void RunThisUsrFilter()
		{
			if (ThisUsr == null)
			{
				throw new DsiUserFriendlyException("Invalid user selected.");
			}
			else if (ThisUsr.Banned)
			{
				SetupBannedUserPanel();
				if (Usr.Current != null && (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin))
					ContainerPage.MainContentPlaceHolder.Controls.AddAt(0, BannedUserPanel);
				else
				{
					ContainerPage.MainContentPlaceHolder.Controls.Clear();
					ContainerPage.MainContentPlaceHolder.Controls.Add(BannedUserPanel);
				}
			}
			// EmailHold = User unsubscribed
			else if (ThisUsr.EmailHold)
			{
				SetupUnsubscribedUserPanel();
				if (Usr.Current != null && (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin))
					ContainerPage.MainContentPlaceHolder.Controls.AddAt(0, UnsubscribedUserPanel);
				else
				{
					ContainerPage.MainContentPlaceHolder.Controls.Clear();
					ContainerPage.MainContentPlaceHolder.Controls.Add(UnsubscribedUserPanel);
				}
			}
			//else if (!ThisUsr.IsEmailVerified)
			//{
			//    // Do not show Unverified Email panel if ThisUsr == Usr.Current
			//    if (Usr.Current != null && (Usr.Current.K != ThisUsr.K))
			//    {
			//        SetupUnverifiedEmailUserPanel();
			//        if (Usr.Current != null && (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin))
			//            ContainerPage.MainContentPlaceHolder.Controls.AddAt(0, UnverifiedEmailUserPanel);
			//        else
			//        {
			//            ContainerPage.MainContentPlaceHolder.Controls.Clear();
			//            ContainerPage.MainContentPlaceHolder.Controls.Add(UnverifiedEmailUserPanel);
			//        }
			//    }
			//}
		}

		#region Setup Panels
		private void SetupBannedUserPanel()
		{
			Spotted.CustomControls.h1 BannedUserH1 = new Spotted.CustomControls.h1();
			LiteralControl BannedLiteral = new LiteralControl();

			BannedUserH1.InnerHtml = "Banned";
			BannedLiteral.Text = "<div class=\"ContentBorder\"><p class=\"BigCenter\">This user has been banned from DontStayIn.</p></div>";
			BannedUserPanel.Controls.Add(BannedUserH1);
			BannedUserPanel.Controls.Add(BannedLiteral);
		}

		private void SetupUnsubscribedUserPanel()
		{
			Spotted.CustomControls.h1 UnsubscribedUserH1 = new Spotted.CustomControls.h1();
			LiteralControl UnsubscribedLiteral = new LiteralControl();

			UnsubscribedUserH1.InnerHtml = "User unsubscribed from DontStayIn";
			UnsubscribedLiteral.Text = "<div class=\"ContentBorder\"><p>This user has unsubscribed from DontStayIn.</p></div>";
			UnsubscribedUserPanel.Controls.Add(UnsubscribedUserH1);
			UnsubscribedUserPanel.Controls.Add(UnsubscribedLiteral);
		}

		//private void SetupUnverifiedEmailUserPanel()
		//{
		//    Spotted.CustomControls.h1 UnverifiedEmailUserH1 = new Spotted.CustomControls.h1();
		//    LiteralControl UnverifiedEmailLiteral = new LiteralControl();

		//    UnverifiedEmailUserH1.InnerHtml = "User email not verified";
		//    UnverifiedEmailLiteral.Text = "<div class=\"ContentBorder\"><p>This user hasn't yet verified their email address, or has recently changed their email address. You'll be able to see their profile as soon as they verify their email address.</p></div>";
		//    UnverifiedEmailUserPanel.Controls.Add(UnverifiedEmailUserH1);
		//    UnverifiedEmailUserPanel.Controls.Add(UnverifiedEmailLiteral);
		//}
		#endregion
	}
}
