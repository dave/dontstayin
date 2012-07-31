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
	public partial class Home : DsiUserControl
	{
		public Panel PanelGroup, PanelPrivate;

		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentGroup != null)
				CurrentGroup.AddRelevant(ContainerPage);

			if (CurrentGroup.K == 29419)
				Response.Redirect("/pages/shadownap");

		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);

			if (!Page.IsPostBack)
				ChangePanel(PanelGroup);

			if (!CurrentGroup.CanViewHomePage(Usr.Current, CurrentGroupUsr))
			{
				ChangePanel(PanelPrivate);
			}


			if (Vars.IsCompetitionGroupActive(CurrentGroup.K))
			{
				bindCaptionCompetitionPhotos();
				bindCompetitionPhotos();

				
				uiCompetitionPanel1.Visible = Common.Settings.CompetitionHtml1Html.Length > 0;
				uiCompetitionPanel1.InnerHtml = Common.Settings.CompetitionHtml1Html;

				uiCompetitionPanel2.Visible = Common.Settings.CompetitionHtml2Html.Length > 0;
				uiCompetitionPanel2.InnerHtml = Common.Settings.CompetitionHtml2Html;

				uiCompetitionPanel3.Visible = Common.Settings.CompetitionHtml3Html.Length > 0;
				uiCompetitionPanel3.InnerHtml = Common.Settings.CompetitionHtml3Html;
				

				HtmlPanel.Visible = false;
				uiTopPhotosHeader.InnerHtml = Common.Settings.CompetitionTopPhotosHeader;
				uiCompetitionPhotosHeader.InnerHtml = Common.Settings.CompetitionAllPhotosHeader;
			}
		}

		public void Page_PreRender(object o, System.EventArgs e)
		{
			if (CurrentGroup.K == 33961)
			{
				MiscInfoPanel.Visible = false;
				//PlainHtmlPlaceHolder.Visible = false;
				HtmlPanel.Visible = false;
				uiCompetitionPanel1.Visible = false;
				CaptionCompetitionPanel.Visible = false;
				uiCompetitionPanel2.Visible = false;
				uiCompetitionPanel3.Visible = false;
				GroupPhotoPanel.Visible = false;
				CaptionHtmlPlaceHolder.Visible = false;
				GroupPhotoModPanelPanel.Visible = false;
				CompetitionPhotoPanel.Visible = false;
				Latest.EventBox.Visible = false;
				Latest.ChatHolderOuter.Visible = false;
				Latest.LatestContentUc.Visible = false;
			}
		}

		private void bindCompetitionPhotos()
		{
			CompetitionPhotoPanel.Visible = true;
			Photo[] ps = SpottedLibrary.Pages.Groups.Photos.GroupPhotosController.GetGroupPhotosPagedDataService(CurrentGroup).Page(1, 8);
			if (ps.Length == 0)
			{
				CompetitionPhotoPanel.Visible = false;
			}
			else
			{
				CompetitionPhotosDataGrid.ItemTemplate = this.LoadTemplate("/Templates/GroupPhotos/Icon.ascx");
				CompetitionPhotosDataGrid.DataSource = ps;
				CompetitionPhotosDataGrid.DataBind();
			}
		}

		private void bindCaptionCompetitionPhotos()
		{
			CaptionCompetitionPanel.Visible = true;
			Query q = new Query();
			q.Columns = Templates.GroupPhotos.CompetitionPhotoIcon.Columns;
			q.TableElement = new Bobs.Join(
				new TableElement(TablesEnum.Photo),
				new TableElement(TablesEnum.GroupPhoto),
				QueryJoinType.Inner,
				new And(
					new Q(Photo.Columns.K, GroupPhoto.Columns.PhotoK, true),
					new Q(GroupPhoto.Columns.GroupK, CurrentGroup.K),
					new Q(GroupPhoto.Columns.ShowOnFrontPage, true),
					new Q(Photo.Columns.IsInCaptionCompetition, true)
					));
			q.OrderBy = new OrderBy(GroupPhoto.Columns.DateTime, OrderBy.OrderDirection.Descending);
			q.TopRecords = 4;

			PhotoSet ps = new PhotoSet(q);
			if (ps.Count == 0)
			{
				CaptionCompetitionPanel.Visible = false;
			}
			else
			{
				CaptionCompetitionPhotoDataList.ItemTemplate = this.LoadTemplate("/Templates/GroupPhotos/CompetitionPhotoIcon.ascx");
				CaptionCompetitionPhotoDataList.DataSource = ps;
				CaptionCompetitionPhotoDataList.DataBind();
			}
		}

		#region PageMode
		PageModes PageMode
		{
			get
			{
				if (ContainerPage.Url["Manage"] == 1)
					return PageModes.Manage;
				else
					return PageModes.Group;
			}
		}
		public enum PageModes
		{
			Group,
			Manage
		}
		#endregion

		#region MiscInfo
		private void MiscInfo_Load(object sender, System.EventArgs e)
		{
			if (PageMode.Equals(PageModes.Group) && CurrentGroup.CanViewHomePage(Usr.Current, CurrentGroupUsr))
			{
				if (CurrentGroup.Brand != null)
				{
					ContainerPage.SetPageTitle(CurrentGroup.Brand.Name + " group");
					GroupName.Text = CurrentGroup.Brand.Name;
					GroupName1.Text = CurrentGroup.Brand.Name;
				}
				else
				{
					ContainerPage.SetPageTitle(CurrentGroup.FriendlyName + " group");
					GroupName.Text = CurrentGroup.FriendlyName + " group";
					GroupName1.Text = CurrentGroup.FriendlyName;
				}

				if (CurrentGroup.Brand != null && CurrentGroup.Brand.HasPic)
					GroupPicImg.Src = CurrentGroup.Brand.PicPath;
				else if (CurrentGroup.HasPic)
					GroupPicImg.Src = CurrentGroup.PicPath;
				else
					GroupPicCell.Visible = false;

				if (CurrentGroup.LongDescriptionHtml.Length > 0)
				{
					HtmlRenderer r = new HtmlRenderer();
					r.Formatting = !CurrentGroup.LongDescriptionPlain;
					r.Container = !CurrentGroup.LongDescriptionPlain;
					r.LoadHtml(CurrentGroup.LongDescriptionHtml);

					if (r.Container)
					{
						HtmlPlaceHolder.Controls.Clear();
						HtmlPlaceHolder.Controls.Add(new LiteralControl(r.Render(HtmlPlaceHolder)));
					}
					else
					{
						HtmlPanel.Visible = false;
						if (Vars.IsCompetitionGroupActive(CurrentGroup.K))
						{
							CaptionHtmlPlaceHolder.Controls.Clear();
							CaptionHtmlPlaceHolder.Controls.Add(new LiteralControl("<div style=\"width:634px; overflow:hidden;\">" + r.Render(CaptionHtmlPlaceHolder) + "</div>"));
						}
						else
						{
							PlainHtmlPlaceHolder.Controls.Clear();
							PlainHtmlPlaceHolder.Controls.Add(new LiteralControl("<div style=\"width:634px; overflow:hidden;\">" + r.Render(PlainHtmlPlaceHolder) + "</div>"));
						}
					}
				}
				else
					HtmlPanel.Visible = false;

				if (CurrentGroup.Brand != null)
				{
					PublicChatLink.HRef = CurrentGroup.Brand.UrlDiscussion();
					PublicChatLinkLabel.Text = "Public chat - " + CurrentGroup.Brand.TotalComments.ToString("#,##0") + " comment" + (CurrentGroup.Brand.TotalComments == 1 ? "" : "s");
					GroupChatLink.HRef = CurrentGroup.UrlDiscussion();
					GroupChatLinkLabel.Text = "Regulars chat - " + CurrentGroup.TotalComments.ToString("#,##0") + " comment" + (CurrentGroup.TotalComments == 1 ? "" : "s");
				}
				else
				{
					PublicChatP.Visible = false;
					GroupChatLink.HRef = CurrentGroup.UrlDiscussion();
					GroupChatLinkLabel.Text = CurrentGroup.FriendlyName + " chat - " + CurrentGroup.TotalComments.ToString("#,##0") + " comment" + (CurrentGroup.TotalComments == 1 ? "" : "s");
				}

				GroupChatP.Visible = CurrentGroup.IsRead(Usr.Current, CurrentGroupUsr);

				if (CurrentGroup.Brand != null)
				{
					CalendarLink.HRef = CurrentGroup.Brand.UrlCalendar();
					CalendarLinkLabel.Text = CurrentGroup.Brand.Name;
					//TicketsLink.HRef = CurrentGroup.Brand.UrlCalendar(true, false);
					//TicketsLinkLabel.Text = CurrentGroup.Brand.Name;
					//FreeGuestlistLink.HRef = CurrentGroup.Brand.UrlCalendar(false, true);
					//FreeGuestlistLinkLabel.Text = CurrentGroup.Brand.Name;
					//HotTicketsLink.HRef = CurrentGroup.Brand.UrlApp("hottickets");
					//HotTicketsLinkLabel.Text = CurrentGroup.Brand.Name;
				}
				else if (CurrentGroup.HasEvents)
				{
					CalendarLink.HRef = CurrentGroup.UrlCalendar();
					CalendarLinkLabel.Text = CurrentGroup.FriendlyName;
					//TicketsLink.HRef = CurrentGroup.UrlCalendar(true, false);
					//TicketsLinkLabel.Text = CurrentGroup.FriendlyName;
					//FreeGuestlistLink.HRef = CurrentGroup.UrlCalendar(false, true);
					//FreeGuestlistLinkLabel.Text = CurrentGroup.FriendlyName;
					//HotTicketsLink.HRef = CurrentGroup.UrlApp("hottickets");
					//HotTicketsLinkLabel.Text = CurrentGroup.FriendlyName;
				}
				else
				{
					CalendarP.Visible = false;
					//TicketsP.Visible = false;
					//HotTicketsP.Visible = false;
				}

				if (CurrentGroup.HasEvents && CurrentGroup.NextEventSet.Count > 0)
				{
					NextEventCell.Visible = true;
					NextEventDataList.DataSource = CurrentGroup.NextEventSet;
					NextEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/NextEventBoxLarge.ascx");
					NextEventDataList.DataBind();
				}
				else if (CurrentGroup.Brand != null && CurrentGroup.Brand.NextEventSet.Count > 0)
				{
					NextEventCell.Visible = true;
					NextEventDataList.DataSource = CurrentGroup.Brand.NextEventSet;
					NextEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/NextEventBoxLarge.ascx");
					NextEventDataList.DataBind();
				}
				else
					NextEventCell.Visible = false;

			}
		}

		#endregion

		#region GroupInfo
		protected Label InfoNameLabel;
		protected HtmlAnchor InfoMembersLink;
		protected HtmlGenericControl PrivacySpan;
		protected PlaceHolder InfoModsPh, InfoMembersLinkPh;
		protected HtmlButton InfoLeaveButton, InfoInviteRejectButton;
		protected HtmlGenericControl InfoJoinLoggedOutP, InfoJoinP, InfoLeaveP, InfoMemberStatusP, InfoInviteP;
		protected Panel InfoFavouriteGroupButtonPanel, CommentAlertButtonPanel;
		public void GroupInfo_Load(object o, System.EventArgs e)
		{
			if (PageMode.Equals(PageModes.Group) && CurrentGroup.CanViewHomePage(Usr.Current, CurrentGroupUsr))
			{
				InfoNameLabel.Text = CurrentGroup.FriendlyName;
				if (CurrentGroup.TotalModerators == 0)
				{
					InfoModsPh.Visible = false;
				}
				else if (CurrentGroup.TotalModerators <= 10)
				{
					//draw the moderators usernames in InfoModsPh
					InfoModsPh.Controls.Clear();
					InfoModsPh.Controls.Add(new LiteralControl("<p>"));
					InfoModsPh.Controls.Add(new LiteralControl(CurrentGroup.TotalModerators == 1 ? "The moderator is " : "The moderators are "));
					Query q = new Query();
					q.NoLock = true;
					q.Columns = Usr.LinkColumns;
					q.TableElement = Group.UsrMemberJoin;
					q.QueryCondition = CurrentGroup.ModeratorQ;
					q.OrderBy = new OrderBy(new OrderBy(GroupUsr.Columns.Owner, OrderBy.OrderDirection.Descending), new OrderBy(GroupUsr.Columns.MemberAdmin, OrderBy.OrderDirection.Descending), new OrderBy(GroupUsr.Columns.NewsAdmin, OrderBy.OrderDirection.Descending), new OrderBy(Usr.Columns.NickName));
					UsrSet us = new UsrSet(q);
					us.WriteUsrLinks(InfoModsPh);
					InfoModsPh.Controls.Add(new LiteralControl(".</p>"));
				}
				else
				{
					InfoModsPh.Controls.Clear();
					InfoModsPh.Controls.Add(new LiteralControl("<p>This group has <a href=\"" + CurrentGroup.UrlApp("members", "moderators", "") + "\">" + CurrentGroup.TotalModerators.ToString() + " moderator" + (CurrentGroup.TotalModerators == 1 ? "" : "s") + "</a>.</p>"));
				}
				if (CurrentGroup.Restriction.Equals(Group.RestrictionEnum.None))
					PrivacySpan.InnerText = "It's an open group (anyone may join)";
				else
					PrivacySpan.InnerText = "It's a restricted group (new members must be accepted)";

				if (CurrentGroup.PrivateChat)
					PrivacySpan.InnerText += ", and the group chat is members-only.";
				else
					PrivacySpan.InnerText += ".";

				InfoMembersLinkPh.Controls.Clear();
				if (CurrentGroup.TotalMembers > 0 && CurrentGroup.PrivateMemberList && !CurrentGroup.CanMember(Usr.Current, CurrentGroupUsr))
					InfoMembersLinkPh.Controls.Add(new LiteralControl(CurrentGroup.TotalMembers + " member" + (CurrentGroup.TotalMembers == 1 ? "" : "s")));
				else if (CurrentGroup.TotalMembers > 0)
					InfoMembersLinkPh.Controls.Add(new LiteralControl("<a href=\"" + CurrentGroup.UrlApp("members", "new", "") + "\">" + CurrentGroup.TotalMembers + " member" + (CurrentGroup.TotalMembers == 1 ? "" : "s") + "</a>"));
				else
					InfoMembersLinkPh.Controls.Add(new LiteralControl("no members"));

				InfoLeaveButton.Attributes["onclick"] = "if (confirm('Are you sure you want to exit this group?')){__doPostBack('" + InfoLeaveButton.UniqueID + "','');return false;}else{return false;};";

				InfoJoinLoggedOutP.Visible = false;// Usr.Current == null;
				InfoJoinP.Visible = Usr.Current == null || Group.AllowJoinRequest(Usr.Current, CurrentGroup, CurrentGroupUsr);
				InfoLeaveP.Visible = Group.AllowExit(Usr.Current, CurrentGroup, CurrentGroupUsr);
				InfoInviteRejectButton.Visible = Group.AllowInviteReject(Usr.Current, CurrentGroupUsr);

				InfoFavouriteGroupButtonPanel.Visible = CurrentGroupUsr != null && CurrentGroupUsr.IsMember;
				CommentAlertButtonPanel.Visible = CurrentGroup.IsRead(Usr.Current, CurrentGroupUsr);

				if (CurrentGroupUsr == null)
					InfoMemberStatusP.InnerHtml = "You're not a member of this group.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Barred))
					InfoMemberStatusP.InnerHtml = "You've been barred from this group. Please contact a <a href=\"" + CurrentGroup.UrlApp("members", "moderators", "") + "\">group moderator</a> for more information.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.RequestRejected))
					InfoMemberStatusP.InnerHtml = "Your request to join this group has been denied. Please contact a <a href=\"" + CurrentGroup.UrlApp("members", "moderators", "") + "\">group moderator</a> for more information.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Exited))
					InfoMemberStatusP.InnerHtml = "You've exited this group.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Invite))
					InfoMemberStatusP.InnerHtml = "You've been invited to join this group.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Member))
					InfoMemberStatusP.InnerHtml = "You're a member of this group. <a href=\"" + CurrentGroup.UrlApp("admin") + "\">Group options</a>.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Request))
					InfoMemberStatusP.InnerHtml = "You've requested to join this group. Please wait until a moderator reviews your application. If you still see this message after a few days, contact a <a href=\"" + CurrentGroup.UrlApp("members", "moderators", "") + "\">group moderator</a> for more information.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Recommend))
					InfoMemberStatusP.InnerHtml = "You're not a member of this group.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.RecommendRejected))
					InfoMemberStatusP.InnerHtml = "You're not a member of this group.";
				else if (CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.InviteRejected))
					InfoMemberStatusP.InnerHtml = "You've previously been invited to this group, but you turned down the invitation.";

				if (CurrentGroupUsr != null && CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Member))
				{
					if (Usr.Current.HasBuddiesFull)
						InfoInviteP.InnerHtml = "<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "buddies") + "\">Invite your buddies</a> or ";

					InfoInviteP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "email") + "\">" + (Usr.Current.HasBuddiesFull ? "i" : "I") + "nvite people by email</a>.";
				}
				else
					InfoInviteP.Visible = false;

				if (CurrentGroupUsr != null && CurrentGroupUsr.Status.Equals(GroupUsr.StatusEnum.Member) && CurrentGroupUsr.Owner && CurrentGroup.LongDescriptionHtml.Length > 0)
				{
					InfoMemberStatusP.InnerHtml += " <a href=\"" + CurrentGroup.UrlApp("edit", "mode", "details") + "\">Edit</a>.";
				}


			}
		}
		protected string CommentAlertButtonDesc
		{
			get
			{
				if (CurrentGroup.BrandK == 0)
					return "this group";
				else
					return "the regulars group";
			}
		}
		public void InfoLeaveClick(object o, System.EventArgs e)
		{
			if (Group.AllowExit(Usr.Current, CurrentGroup, CurrentGroupUsr))
			{
				CurrentGroup.Exit(Usr.Current);
				CurrentGroupUsr = null;
				Response.Redirect(CurrentGroup.Url());
			}
		}
		public void InfoJoinClick(object o, System.EventArgs e)
		{
			if (Group.AllowJoinRequest(Usr.Current, CurrentGroup, CurrentGroupUsr))
			{
				Response.Redirect(CurrentGroup.UrlApp("join"));
			}
		}
		public void InfoInviteRejectClick(object o, System.EventArgs e)
		{
			if (Group.AllowInviteReject(Usr.Current, CurrentGroupUsr))
			{
				CurrentGroup.InviteReject(Usr.Current, CurrentGroupUsr);
				CurrentGroupUsr = null;
				Response.Redirect(CurrentGroup.Url());
			}
		}
		protected string CommentAlertButtonState
		{
			get
			{
				return (Usr.Current != null && CommentAlert.IsEnabled(Usr.Current.K, CurrentGroup.K, Model.Entities.ObjectType.Group)) ? "1" : "0";
			}
		}
		protected string InfoFavouriteGroupButtonState
		{
			get
			{
				return (CurrentGroupUsr != null && CurrentGroupUsr.IsMember && CurrentGroupUsr.Favourite) ? "1" : "0";
			}
		}
		#endregion

		#region GroupPhoto
		private void GroupPhoto_Load(object sender, System.EventArgs e)
		{
			bool competitionGroup = Vars.IsCompetitionGroupActive(CurrentGroup.K);

			Query q = new Query();
			q.Columns = Templates.GroupPhotos.Icon.Columns;
			q.TableElement = new Bobs.Join(
				new TableElement(TablesEnum.Photo),
				new TableElement(TablesEnum.GroupPhoto),
				QueryJoinType.Inner,
				new And(
					new Q(Photo.Columns.K, GroupPhoto.Columns.PhotoK, true),
					new Q(GroupPhoto.Columns.GroupK, CurrentGroup.K),
					new Q(GroupPhoto.Columns.ShowOnFrontPage, true),
					competitionGroup ? new Q(Photo.Columns.IsInCaptionCompetition, false) : new Q(true)));
			q.OrderBy = new OrderBy(GroupPhoto.Columns.DateTime, OrderBy.OrderDirection.Descending);

			q.TopRecords = competitionGroup ? 4 : 8;

			PhotoSet ps = new PhotoSet(q);
			if (ps.Count == 0)
			{
				GroupPhotoPanel.Visible = false;
				if (CurrentGroupUsr != null && CurrentGroupUsr.Moderator)
					GroupPhotoModPanelPanel.Visible = true;
			}
			else
			{
				GroupPhotoDataList.ItemTemplate = this.LoadTemplate("/Templates/GroupPhotos/Icon.ascx");
				GroupPhotoDataList.DataSource = ps;
				GroupPhotoDataList.DataBind();
				GroupPhotoArchiveLinkP.Visible = ps.Count == q.TopRecords;
			}
		}
		#endregion

		#region Latest
		protected Controls.Latest Latest;
		public void Latest_Load(object o, System.EventArgs e)
		{
			Latest.Parent = CurrentGroup;
		}

		#endregion

		#region CurrentGroup
		public Group CurrentGroup
		{
			get
			{
				if (ContainerPage.Url.HasBrandObjectFilter)
					return ContainerPage.Url.ObjectFilterBrand.Group;
				else
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

		void ChangePanel(Panel p)
		{
			PanelGroup.Visible = p.Equals(PanelGroup);
			PanelPrivate.Visible = p.Equals(PanelPrivate);
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
			this.Load += new System.EventHandler(this.MiscInfo_Load);
			this.Load += new System.EventHandler(this.GroupInfo_Load);
			this.Load += new System.EventHandler(this.Latest_Load);
			this.Load += new System.EventHandler(this.GroupPhoto_Load);
		}
		#endregion
	}
}
