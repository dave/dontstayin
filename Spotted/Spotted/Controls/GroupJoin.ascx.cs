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

namespace Spotted.Controls
{
	public partial class GroupJoin : UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{

			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Join))
					ChangePanel(PanelJoin);
           	}
            AssignValidationGroup();

			SonyPanel.Visible = false;// CurrentGroup.K == Vars.SonyGroupK;

		}

		#region SonyMegaPixels_Val
		protected void SonyMegaPixels_Val(object sender, ServerValidateEventArgs eventArgs)
		{
			eventArgs.IsValid = SonyMegaPixels32.Checked;
		}
		#endregion
		#region SonySpecialFeature_Val
		protected void SonySpecialFeature_Val(object sender, ServerValidateEventArgs eventArgs)
		{
			eventArgs.IsValid = SonySpecialFeatureFlash.Checked;
		}
		#endregion

		#region PanelJoin
		private void PanelJoin_Load(object sender, System.EventArgs e)
		{
			LoadGroupJoin();
		}

        private void AssignValidationGroup()
        {
            this.JoinAgreeCustomValidator.ValidationGroup = ValidationGroup;
            this.JoinWatchCustomValidator.ValidationGroup = ValidationGroup;
            this.SonyCustomValidator1.ValidationGroup = ValidationGroup;
            this.SonyCustomValidator2.ValidationGroup = ValidationGroup;
            this.JoinButton.ValidationGroup = ValidationGroup;
        }

		private void LoadGroupJoin()
		{
			GroupHomePageLink.NavigateUrl = CurrentGroup.Url();
			this.CancelButton.Visible = !IsInList;

			if (Mode.Equals(Modes.Join))
			{
				if (CurrentGroup.PrivateGroupPage)
				{
					if (!CurrentGroup.CanViewHomePage(Usr.Current, CurrentGroupUsr))
					{
						//throw new DSIUserFriendlyException("This group is private - you must be invited before joining.");
						CanJoinPanel.Visible = false;
						CanNotJoinPanel.Visible = true;
						CanNotJoinP.InnerHtml = "This group is private - you must be invited before joining.";
						return;
					}
				}

				//ContainerPage.SetPageTitle("Join the " + CurrentGroup.FriendlyName + " group");
				JoinTitle.InnerText = "Join the " + CurrentGroup.FriendlyName + " group";

				if (CurrentGroup.Brand != null && CurrentGroup.Brand.HasPic)
					JoinPicImg.Src = CurrentGroup.Brand.PicPath;
				else if (CurrentGroup.HasPic)
					JoinPicImg.Src = CurrentGroup.PicPath;
				else
					JoinPicCell.Visible = false;
				JoinPicAnchor.HRef = CurrentGroup.Url();

				JoinNameLabel.Text = CurrentGroup.FriendlyName;
				if (CurrentGroup.TotalModerators == 0)
				{
					JoinModsPh.Visible = false;
				}
				else if (CurrentGroup.TotalModerators <= 10)
				{
					//draw the moderators usernames in InfoModsPh
					JoinModsPh.Controls.Clear();
					JoinModsPh.Controls.Add(new LiteralControl(CurrentGroup.TotalModerators == 1 ? "The moderator is " : "The moderators are "));
					Query q = new Query();
					q.NoLock = true;
					q.Columns = Usr.LinkColumns;
					q.TableElement = Group.UsrMemberJoin;
					q.QueryCondition = CurrentGroup.ModeratorQ;
					q.OrderBy = new OrderBy(new OrderBy(GroupUsr.Columns.Owner, OrderBy.OrderDirection.Descending), new OrderBy(GroupUsr.Columns.MemberAdmin, OrderBy.OrderDirection.Descending), new OrderBy(GroupUsr.Columns.NewsAdmin, OrderBy.OrderDirection.Descending), new OrderBy(Usr.Columns.NickName));
					UsrSet us = new UsrSet(q);
					us.WriteUsrLinks(JoinModsPh);
					JoinModsPh.Controls.Add(new LiteralControl("."));
				}
				else
				{
					JoinModsPh.Controls.Clear();
					JoinModsPh.Controls.Add(new LiteralControl("This group has <a href=\"" + CurrentGroup.UrlApp("members", "moderators", "") + "\">" + CurrentGroup.TotalModerators.ToString() + " moderator" + (CurrentGroup.TotalModerators == 1 ? "" : "s") + "</a>."));
				}
				if (CurrentGroup.Restriction.Equals(Group.RestrictionEnum.None))
					PrivacySpan.InnerText = "It's an open group - anyone may join.";
				else
					PrivacySpan.InnerText = "It's a restricted group - new members must be accepted.";

				JoinMembersLinkPh.Controls.Clear();
				if (CurrentGroup.TotalMembers > 0 && CurrentGroup.PrivateMemberList && !CurrentGroup.CanMember(Usr.Current, CurrentGroupUsr))
					JoinMembersLinkPh.Controls.Add(new LiteralControl(CurrentGroup.TotalMembers + " member" + (CurrentGroup.TotalMembers == 1 ? "" : "s")));
				else if (CurrentGroup.TotalMembers > 0)
					JoinMembersLinkPh.Controls.Add(new LiteralControl("<a href=\"" + CurrentGroup.UrlApp("members") + "\">" + CurrentGroup.TotalMembers + " member" + (CurrentGroup.TotalMembers == 1 ? "" : "s") + "</a>"));
				else
					JoinMembersLinkPh.Controls.Add(new LiteralControl("no members"));

				JoinDescP.InnerText = CurrentGroup.Description;

				JoinRulesPanel.Visible = CurrentGroup.PostingRules.Trim().Length > 0;
				if (CurrentGroup.PostingRules.Trim().Length > 0)
					JoinRulesP.InnerHtml = CurrentGroup.PostingRules.Trim().Replace("\n", "<br>");

				JoinWatchRadio.Attributes["onclick"] = "document.getElementById('" + GroupJoinEyeImage.ClientID + "').src='/gfx/icon-eye-up.png';";
				JoinIgnoreRadio.Attributes["onclick"] = "document.getElementById('" + GroupJoinEyeImage.ClientID + "').src='/gfx/icon-eye-dn.png';";
				GroupJoinEyeImage.Attributes["onclick"] = "if (document.getElementById('" + JoinWatchRadio.ClientID + "').checked){document.getElementById('" + JoinWatchRadio.ClientID + "').checked=false;document.getElementById('" + JoinIgnoreRadio.ClientID + "').checked=true;document.getElementById('" + GroupJoinEyeImage.ClientID + "').src='/gfx/icon-eye-dn.png';}else{document.getElementById('" + JoinWatchRadio.ClientID + "').checked=true;document.getElementById('" + JoinIgnoreRadio.ClientID + "').checked=false;document.getElementById('" + GroupJoinEyeImage.ClientID + "').src='/gfx/icon-eye-up.png';};";
				if (JoinIgnoreRadio.Checked)
					GroupJoinEyeImage.Src = "/gfx/icon-eye-dn.png";
				else if (JoinWatchRadio.Checked)
					GroupJoinEyeImage.Src = "/gfx/icon-eye-up.png";

				if (Usr.Current == null || Group.AllowJoinRequest(Usr.Current, CurrentGroup, CurrentGroupUsr))
				{
					CanJoinPanel.Visible = true;
					CanNotJoinPanel.Visible = false;
				}
				else
				{
					CanJoinPanel.Visible = false;
					CanNotJoinPanel.Visible = true;

					if (CurrentGroupUsr == null)
						CanNotJoinP.InnerHtml = "You're not a member of this group.";
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Barred))
						CanNotJoinP.InnerHtml = "You've been barred from this group. Please contact a <a href=\"" + CurrentGroup.UrlApp("members", "moderators", "") + "\">group moderator</a> for more information.";
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.RequestRejected))
						CanNotJoinP.InnerHtml = "Your request to join this group has been denied. Please contact a <a href=\"" + CurrentGroup.UrlApp("members", "moderators", "") + "\">group moderator</a> for more information.";
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Exited))
						CanNotJoinP.InnerHtml = "You've exited this group.";
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Invite))
						CanNotJoinP.InnerHtml = "You've been invited to join this group.";
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Member))
					{
						CanNotJoinP.InnerHtml = "You're a member of this group. <a href=\"" + CurrentGroup.UrlApp("admin") + "\">Group options</a>.";
						JoinTitle.InnerText = "You're a member of the " + CurrentGroup.FriendlyName + " group";
						GroupHomePageP.Visible = true;
					}
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Request))
						CanNotJoinP.InnerHtml = "You've requested to join this group. Please wait until a moderator reviews your application. If you still see this message after a few days, contact a <a href=\"" + CurrentGroup.UrlApp("members", "moderators", "") + "\">group moderator</a> for more information.";
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Recommend))
						CanNotJoinP.InnerHtml = "You're not a member of this group.";
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.RecommendRejected))
						CanNotJoinP.InnerHtml = "You're not a member of this group.";
					else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.InviteRejected))
						CanNotJoinP.InnerHtml = "You've previously been invited to this group, but you turned down the invitation.";
				}
			}
		}
		public void JoinCancelClick(object o, System.EventArgs e)
		{
			Response.Redirect(CurrentGroup.Url());
		}
		public void JoinAgreeCheckBox_Val(object o, ServerValidateEventArgs e)
		{
			e.IsValid = JoinAgreeCheckBox.Checked;
		}
		public void JoinWatchRadio_Val(object o, ServerValidateEventArgs e)
		{
			e.IsValid = JoinWatchRadio.Checked || JoinIgnoreRadio.Checked;
		}
		public void JoinJoinClick(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to do this");

            Page.Validate(this.ValidationGroup);
			if (Page.IsValid)
			{
				if (Group.AllowJoinRequest(Usr.Current, CurrentGroup, CurrentGroupUsr))
				{
					CurrentGroup.Join(Usr.Current, CurrentGroupUsr);
                    doneCurrentGroupUsr = false;
					LoadGroupJoin();
					((Spotted.Master.DsiPage)Page).AnchorSkip(CurrentGroup.K.ToString());
				}

				if (JoinWatchRadio.Checked)
				{
					GroupUsr gu = CurrentGroup.GetGroupUsr(Usr.Current);
					if (Usr.Current.CanGroupRead(CurrentGroup, gu))
					{
						CommentAlert.Enable(Usr.Current, CurrentGroup.K, Model.Entities.ObjectType.Group);
					}
				}
				else
				{
					CommentAlert.Disable(Usr.Current, CurrentGroup.K, Model.Entities.ObjectType.Group);
				}

				if (!IsInList)
				{
					if (ContainerPage.Url["type"].Exists && ContainerPage.Url["k"].Exists)
					{
						Model.Entities.ObjectType type = (Model.Entities.ObjectType)int.Parse(ContainerPage.Url["type"]);
						IBob b = Bob.Get(type, (int)ContainerPage.Url["k"]);
						if (b is IPage)
							Response.Redirect(((IPage)b).Url());
						else
							Response.Redirect(CurrentGroup.Url());
					}
					else
						Response.Redirect(CurrentGroup.Url());
				}
			}
		}
		#endregion

		#region CurrentGroup
		public Group CurrentGroup
		{
			get
			{
				if(currentGroup == null)
					currentGroup = ContainerPage.Url.ObjectFilterGroup;
				return currentGroup;
			}
			set
			{
				currentGroup = value;
			}
		}
		private Group currentGroup;
		#endregion

		#region ContainerPage
		public Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
			}
		}
		#endregion

		#region IsInList
		public bool IsInList
		{
			get
			{
				return isInList;
			}
			set
			{
				isInList = value;
			}
		}
		private bool isInList = false;
		#endregion

		#region ValidationGroup
		public string ValidationGroup
		{
            get
            {
                return "GroupJoinValidation" + CurrentGroup.K.ToString();
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
			//	if (ContainerPage.Url[0].Equals("join"))
			//		return Modes.Join;
			//	else
					return Modes.Join;
			}
		}
		public enum Modes
		{
			None,
			Join
		}
		#endregion

		

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelJoin.Visible = p.Equals(PanelJoin);
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
			this.Load += new System.EventHandler(this.PanelJoin_Load);

		}
		#endregion
	}
}
