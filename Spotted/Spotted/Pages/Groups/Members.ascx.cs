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

namespace Spotted.Pages.Groups
{
	public partial class Members : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentGroup.PrivateMemberList && Mode.Equals(Modes.MembersList) && !CurrentGroup.CanMember(Usr.Current, CurrentGroupUsr))
				throw new DsiUserFriendlyException("The membership list is private for this group.");

			SetupUsrBrowser();
		}

		#region CurrentGroup
		public Group CurrentGroup
		{
			get
			{
				return ContainerPage.Url.ObjectFilterGroup;
			}
		}
		#endregion

		#region CurrentGroupUsr
		GroupUsr CurrentGroupUsr
		{
			get
			{
				if (!doneCurrentGroupUsr)
				{
					currentGroupUsr = CurrentGroup.GetGroupUsr(Usr.Current);
					doneCurrentGroupUsr = true;
				}
				return currentGroupUsr;
			}
			set
			{
				doneCurrentGroupUsr = false;
				currentGroupUsr = value;
			}
		}
		bool doneCurrentGroupUsr = false;
		GroupUsr currentGroupUsr;
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("moderators"))
					return Modes.ModeratorsList;
				else
					return Modes.MembersList;
			}
		}
		public enum Modes
		{
			None,
			MembersList,
			ModeratorsList
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
		}
		#endregion



		void SetupUsrBrowser()
		{
			usrBrowser.DescriptionText = "Listed below are the ";
			if (Mode.Equals(Modes.MembersList))
			{
				usrBrowser.DescriptionText += "current members";
			}
			else
			{
				usrBrowser.DescriptionText += "moderators";
			}
			usrBrowser.DescriptionText += " of the <a href=\"" + CurrentGroup.Url() + "\">" + CurrentGroup.FriendlyName + "</a> group.";

			usrBrowser.HeaderText = "Members list";

			usrBrowser.BaseQ = CurrentGroup.MemberQ;
			if (Mode.Equals(Modes.ModeratorsList))
			{
				usrBrowser.BaseQ = CurrentGroup.ModeratorQ;
			}

			usrBrowser.OrderByNewest = new OrderBy(GroupUsr.Columns.StatusChangeDateTime, OrderBy.OrderDirection.Descending);
			usrBrowser.JoinToUsrTable = Usr.GroupJoin;
			usrBrowser.BrowsingObject = CurrentGroup;
			usrBrowser.TotalSetCount = CurrentGroup.TotalMembers;
			usrBrowser.NoRecordsDisplayedText = "No users here. You can view a list of all members ordered by the date they joined - click <a href=\"" + usrBrowser.Url(null, 0, true) + "\">newest members</a>.";
		}
	}
}
