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

namespace Spotted.Templates.ThreadUsrs
{
	public partial class Large : System.Web.UI.UserControl
	{
		protected PlaceHolder InvitedPh, OwnerPh;
		protected PlaceHolder NamePh, EmailPh, SkeletonPh;
		protected HtmlAnchor InvitingUsrAnchor;

		private void Page_Load(object sender, System.EventArgs e)
		{
			OwnerPh.Visible = (CurrentThreadUsr.Thread.UsrK == CurrentUsr.K);
			InvitedPh.Visible = (CurrentThreadUsr.Thread.UsrK != CurrentUsr.K && InvitingUsr != null);
			NamePh.Visible = (CurrentUsr.NickName.Length > 0);
			SkeletonPh.Visible = (CurrentUsr.NickName.Length == 0 && Usr.Current.K != CurrentUsr.AddedByUsrK);
			EmailPh.Visible = (CurrentUsr.NickName.Length == 0 && Usr.Current.K == CurrentUsr.AddedByUsrK);

			if (InvitingUsr != null)
			{
				InvitingUsrAnchor.HRef = InvitingUsr.Url();
				InvitingUsrAnchor.InnerText = InvitingUsr.NickName;
				InvitingUsr.MakeRollover(InvitingUsrAnchor);
			}

		}
		protected string BuddyStart
		{
			get
			{
				if (CurrentThreadUsr.JoinedBuddy.FullBuddy)
					return "</small><b>";
				else
					return "";
			}
		}
		protected string BuddyEnd
		{
			get
			{
				if (CurrentThreadUsr.JoinedBuddy.FullBuddy)
					return "</b><small>";
				else
					return "";
			}
		}
		protected string FriendlyInviteDateTime
		{
			get
			{
				return " " + Cambro.Misc.Utility.FriendlyTime(CurrentThreadUsr.DateTime, false);
			}
		}
		protected ThreadUsr CurrentThreadUsr
		{
			get
			{
				if (currentThreadUsr == null)
					currentThreadUsr = ((ThreadUsr)((DataListItem)NamingContainer).DataItem);
				return currentThreadUsr;
			}
		}
		ThreadUsr currentThreadUsr;

		protected Usr CurrentUsr
		{
			get
			{
				return CurrentThreadUsr.Usr;
			}
		}
		protected Usr InvitingUsr
		{
			get
			{
				return CurrentThreadUsr.InvitingUsr;
			}
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
