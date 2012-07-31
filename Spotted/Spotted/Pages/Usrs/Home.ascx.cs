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

namespace Spotted.Pages.Usrs
{
	public partial class Home : UsrUserControl
	{
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
			if (ThisUsr != null)
				ThisUsr.AddRelevant(ContainerPage);
		}

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);

			if (Usr.Current != null && (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin))
			{
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/usr?ID=" + ThisUsr.K.ToString() + "\">Edit this usr (admin)</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"" + ThisUsr.UrlApp("edit") + "\">Edit this usr (my details)</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"/admin/changepassword?usrk=" + ThisUsr.K + "\">Admin change password</a></p>"));
				if (ThisUsr.IsPromoter)
				{
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p>Promoters:<br>"));
					foreach (Promoter p in ThisUsr.Promoters(new ColumnSet(Promoter.Columns.Name, Promoter.Columns.Name, Promoter.Columns.UrlName, Promoter.Columns.PrimaryUsrK)))
						ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<a href=\"" + p.Url() + "\">" + p.Name + "</a>" + (p.PrimaryUsrK == ThisUsr.K ? "&nbsp;(PRIMARY)" : "") + "<br>"));
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("</p>"));
				}
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p>Name: " + ThisUsr.FirstName + " " + ThisUsr.LastName + "</p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p>Email: <a href=\"mailto:" + ThisUsr.Email + "\">" + ThisUsr.Email + "</a></p>"));
				if (!ThisUsr.IsSuperAdmin || Usr.Current.IsSuperAdmin)
				{
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"" + ThisUsr.LoginAndTransfer(ThisUsr.Url()) + "\">Log in as this user</a></p>"));
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://test.dontstayin.com" + ThisUsr.LoginAndTransferShort(ThisUsr.Url()) + "\">Log in to TEST as this user</a></p>"));
				}
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('This will delete ALL attached objects.\\nARE YOU SURE?');\" href=\"/admin/multidelete?ObjectType=Usr&ObjectK=" + ThisUsr.K + "\">Delete this usr</a><br>Be careful - deletes ALL comments, threads, galleries, photos etc. that this user has added! Events/venues that this usr added are are not deleted, but transfered to DaveB</p>"));
				if (!ThisUsr.IsSuperAdmin || Usr.Current.IsSuperAdmin)
				{
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('ARE YOU SURE?');\" href=\"/admin/deletepic/usrk-" + ThisUsr.K + "\">Delete profile pic</a></p>"));
				}
			}

			//PanelBanned.Visible = (ThisUsr != null && ThisUsr.Banned);

			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{
				ChangePanel(PanelDetails);
				SetPageTitle(ThisUsr.NickName + " - user profile");
			}
			else
				ChangePanel(PanelNoDetails);

		}
		#endregion

		#region PanelBan
		public void PanelBan_Load(object o, System.EventArgs e)
		{
			PanelBan.Visible = Usr.Current != null && Usr.Current.IsJunior && !ThisUsr.Banned && !ThisUsr.IsJunior;
			BanButton.Attributes["onclick"] = "return (confirm('Are you sure?') && confirm('Are you double sure?')  && confirm('Last chance - are you sure you\\'re double sure?'));";
		}
		public void Ban_Click(object o, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsJunior)
			{
				ThisUsr.Banned = true;
				ThisUsr.BannedByUsrK = Usr.Current.K;
				ThisUsr.BannedDateTime = DateTime.Now;
				ThisUsr.BannedReason = BanReasonTextBox.Text;
				ThisUsr.Update();

				Mailer sm = new Mailer();
				sm.Body = "<p>Banned user: <a href=\"[LOGIN(" + ThisUsr.Url() + ")]\">" + ThisUsr.NickName + "</a> (" + ThisUsr.K + " - " + ThisUsr.Email + ")</p>";
				sm.Body += "<p>They were banned by: <a href=\"[LOGIN(" + Usr.Current.Url() + ")]\">" + Usr.Current.NickName + "</a> (" + Usr.Current.K + " - " + Usr.Current.Email + ")</p>";
				sm.Body += "<p>DateTime: " + DateTime.Now.ToString() + "</p>";
				sm.Body += "<p>Reason: " + ThisUsr.BannedReason + "</p>";
				sm.TemplateType = Mailer.TemplateTypes.AdminNote;
				sm.Subject = "New banned user - " + ThisUsr.NickName + " was banned by " + Usr.Current.NickName;
				sm.To = "abuse@dontstayin.com";
				sm.Send();

				//PanelBanned.Visible = true;
				PanelBan.Visible = false;

				this.RunThisUsrFilter();
			}
		}
		#endregion

		#region PanelChat
		protected string UsrCurrentK
		{
			get
			{
				if (Usr.Current != null)
					return Usr.Current.K.ToString();
				else
					return "0";
			}
		}
		public void PanelChat_Load(object o, System.EventArgs e)
		{
			if (Usr.Current == null || (Usr.Current != null && ThisUsr.K == Usr.Current.K) || !ThisUsr.LoggedInNow)
			{
				ChatCell.Visible = false;
			}
			else
			{
				ChatCell.Visible = true;
			}
		}

		#endregion

		#region SendMessagePanel
		public void SendMessage_Load(object o, System.EventArgs e)
		{

			AddThreadSubjectTextBox.Style["width"] = Vars.IE ? "576px" : "578px";

			if ((Usr.Current != null && ThisUsr.K == Usr.Current.K) || ThisUsr.Banned || ThisUsr.K == 8)
				SendMessagePanel.Visible = false;
			else
				SendMessagePanel.Visible = true;

			if (Usr.Current != null)
			{
				AddThreadAddBuddyP.Visible = !Usr.Current.HasBuddy(ThisUsr.K);
				AddThreadAddBuddy.Text = "Also add " + ThisUsr.NickName + " to your buddy list when you send the message.";
			}
			else
				AddThreadAddBuddyP.Visible = false;

			#region Init AddComment/AddThread post boxes for users not logged in etc.
			//Cambro.Web.Helpers.TieButton(AddThreadSubjectTextBox, AddThreadPostButton);
			//if (Usr.Current == null)
			//{
			//    AddThreadLoginPanel.Visible = true;
			//    AddThreadEmailVerifyPanel.Visible = false;
			//    AddThreadCommentHtml.Enabled = false;
			//    AddThreadCommentHtml.Text = "You can't post until you are logged in!";
			//}
			//else if (!Usr.Current.IsEmailVerified)
			//{
			//    AddThreadLoginPanel.Visible = false;
			//    AddThreadEmailVerifyPanel.Visible = true;
			//    AddThreadCommentHtml.Enabled = false;
			//    AddThreadCommentHtml.Text = "You can't post until your email address has been verified!";
			//}
			//else
			//{
				AddThreadLoginPanel.Visible = false;
				AddThreadEmailVerifyPanel.Visible = false;
				AddThreadCommentHtml.Enabled = true;
			//}
			#endregion

		}
		public void AddThreadPostClick(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (Usr.Current == null)
				{
					throw new DsiUserFriendlyException("You can't post until you are logged in!");
				}
				if (ThisUsr.IsSkeleton)
					throw new Exception("Can't send message.");

				if (AddThreadAddBuddy.Checked)
					Usr.Current.AddBuddy(ThisUsr, Usr.AddBuddySource.UsrPageSendPrivateMessage, Buddy.BuddyFindingMethod.Nickname, null);

				Thread.Maker m = new Thread.Maker();
				m.Subject = AddThreadSubjectTextBox.Text;
				m.Body = AddThreadCommentHtml.GetHtml();
				m.ParentType = Model.Entities.ObjectType.None;
				m.DuplicateGuid = ContainerPage.ViewStatePublic["CommentDuplicateGuid"];
				m.Private = true;
				m.InviteKs.Add(ThisUsr.K);
				m.PostingUsr = Usr.Current;
				Thread.MakerReturn r = m.Post();

				if (r.Success || r.Duplicate)
					Response.Redirect(r.Thread.Url());
				else
					throw new Exception(r.MessageHtml);

			}
		}
		public void SetCommentDuplicateGuid(object o, System.EventArgs e)
		{
			ContainerPage.ViewStatePublic["CommentDuplicateGuid"] = Guid.NewGuid();
		}
		#endregion

		#region PersonalStatementPanel
		public void PersonalStatement_PreRender(object o, System.EventArgs e)
		{
			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{
				BindPersonalStatement();
				BindFavouriteGroups();

				if (Usr.Current != null && Usr.Current.K == ThisUsr.K)
				{
					EditPersonalStatementPanel.Visible = true;
					if (!Page.IsPostBack)
						PersonalStatementHtml.LoadHtml(ThisUsr.PersonalStatement);
				}
				else
					EditPersonalStatementPanel.Visible = false;
			}
		}
		void BindFavouriteGroups()
		{
			Query q = new Query();
			q.TableElement = new Join(
				new TableElement(TablesEnum.Group),
				new TableElement(TablesEnum.GroupUsr),
				QueryJoinType.Inner,
				new And(
					new Q(Group.Columns.K, GroupUsr.Columns.GroupK, true),
					new Q(GroupUsr.Columns.Favourite, true),
					new Q(GroupUsr.Columns.UsrK, ThisUsr.K),
					new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)
				)
			);
			q.QueryCondition = new And(
				new Q(Group.Columns.PrivateGroupPage, false),
				new Q(Group.Columns.PrivateMemberList, false));
			q.Columns = Templates.Groups.Latest.Columns;
			q.OrderBy = new OrderBy(new OrderBy(GroupUsr.Columns.Moderator, OrderBy.OrderDirection.Descending), new OrderBy(OrderBy.OrderDirection.Random));
			q.TopRecords = 6;
			GroupSet gs = new GroupSet(q);
			if (gs.Count > 0)
			{
				FavouriteGroupsDataList.DataSource = gs;
				FavouriteGroupsDataList.ItemTemplate = this.LoadTemplate("/Templates/Groups/Latest.ascx");
				FavouriteGroupsDataList.DataBind();
			}
			else
				FavouriteGroupsPanel.Visible = false;
		}
		bool doneBindPersonalStatement = false;
		void BindPersonalStatement()
		{
			if (!doneBindPersonalStatement)
			{
				doneBindPersonalStatement = true;
				if (ThisUsr.PersonalStatement.Length > 0)
				{
					PersonalStatementPanel.Visible = true;
					HtmlRenderer r = new HtmlRenderer();
					r.LoadHtml(ThisUsr.PersonalStatement);

					if (r.Container)
					{
						PersonalStatementPlainPh.Visible = false;
						PersonalStatementPh.Visible = true;
						PersonalStatementPh.Controls.Clear();
						PersonalStatementPh.Controls.Add(new LiteralControl(r.Render(PersonalStatementPh)));
					}
					else
					{
						PersonalStatementPh.Visible = false;
						PersonalStatementPlainPh.Visible = true;
						PersonalStatementPlainPh.Controls.Clear();
						PersonalStatementPlainPh.Controls.Add(new LiteralControl("<div style=\"width:634px; overflow:hidden;\">" + r.Render(PersonalStatementPlainPh) + "</div>"));
					}
				}
				else
					PersonalStatementPh.Visible = false;
			}
		}
		public void PersonalStatementSave(object o, System.EventArgs e)
		{
			if (Usr.Current.K == ThisUsr.K)
			{
				ThisUsr.PersonalStatement = PersonalStatementHtml.GetHtml();
				ThisUsr.Update();
				BindPersonalStatement();
			}
			else
				throw new Exception("Error khfd807h43kjnfdskjhg");
		}
		#endregion

		#region EventsAttendFuturePanel
		public void EventsAttendFuture_Load(object o, System.EventArgs e)
		{
			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{
				Query q = new Query();
				q.NoLock = true;
				q.TableElement = Templates.Events.UsrPageAttendedList.PerformJoins(Event.UsrAttendedJoin);
				q.Columns = Templates.Events.UsrPageAttendedList.Columns;
				q.QueryCondition = new And(
					new Q(Usr.Columns.K, UsrK),
					Event.FutureEventsQueryCondition);
                q.OrderBy = new OrderBy(new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Ascending), new OrderBy(Event.Columns.StartTime));
				q.TopRecords = 10;
				EventSet esf = new EventSet(q);

				if (esf.Count == 0)
					EventsAttendFuturePanel.Visible = false;
				else
				{
					EventsAttendFuturePanel.Visible = true;
					EventsAttendFutureDataList.DataSource = esf;
					EventsAttendFutureDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/UsrPageAttendedList.ascx");
					EventsAttendFutureDataList.DataBind();
				}
			}
		}
		#endregion

		#region EventsAttendedPanel
		public void EventsAttended_Load(object o, System.EventArgs e)
		{
			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{
				Query q = new Query();
				q.NoLock = true;
				q.TableElement = Templates.Events.UsrPageAttendedList.PerformJoins(Event.UsrAttendedJoin);
				q.Columns = Templates.Events.UsrPageAttendedList.Columns;
				q.QueryCondition = new And(
					new Q(Usr.Columns.K, UsrK),
					Event.PreviousEventsQueryCondition);
				q.OrderBy = new OrderBy(Bobs.Event.Columns.DateTime, OrderBy.OrderDirection.Descending);
				q.TopRecords = 10;
				EventSet es = new EventSet(q);
				if (es.Count == 0)
				{
					EventsAttendedPanel.Visible = false;
				}
				else
				{
					EventsAttendedPanel.Visible = true;
					EventsAttendedDataList.DataSource = es;
					EventsAttendedDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/UsrPageAttendedList.ascx");
					EventsAttendedDataList.DataBind();
				}
			}
		}
		#endregion

		#region GalleriesPanel
		public void GalleriesPanel_Load(object o, System.EventArgs e)
		{
			GalleriesPanel.Visible = uiRecentGalleries.Visible;
		}
		#endregion

		#region UsrsSpottedPanel
		public void UsrsSpotted_Load(object o, System.EventArgs e)
		{
			//if (ThisUsr != null && !ThisUsr.IsSkeleton && ThisUsr.SpottingsTotal > 0)
			//{
			//    UsrsSpottedStatsP.InnerText =
			//        "I've spotted " +
			//        ThisUsr.SpottingsTotal.ToString("#,##0") + " " +
			//        (ThisUsr.SpottingsTotal == 1 ? "person" : "people");

			//    if (ThisUsr.SpottingsMonth > 0)
			//    {
			//        UsrsSpottedStatsP.InnerHtml +=
			//            "<br>(" + ThisUsr.SpottingsMonth.ToString("#,##0") + " in the last month";
					
			//        if (ThisUsr.SpottingsMonthRank > 0)
			//            UsrsSpottedStatsP.InnerHtml +=
			//                " - this ranks me #" + ThisUsr.SpottingsMonthRank + " in the top spotters list";

			//        UsrsSpottedStatsP.InnerHtml += "*)";

			//        UsrsSpottedFootnoteP.InnerHtml = "<small>* The monthly stats and rankings are only updated overnight</small>";
			//    }
			//    else
			//        UsrsSpottedFootnoteP.Visible = false;


			//    Query q = new Query();
			//    q.Columns = Usr.LinkColumns;
			//    q.OrderBy = new OrderBy(Usr.Columns.DateTimeSignUp, OrderBy.OrderDirection.Descending);
			//    q.TableElement = new Join(
			//        Usr.Columns.K,
			//        UsrPhotoMe.Columns.UsrK);
			//    q.TableElement = new Join(
			//        q.TableElement,
			//        new TableElement(TablesEnum.Photo),
			//        QueryJoinType.Inner,
			//        new And(new Q(UsrPhotoMe.Columns.PhotoK, Photo.Columns.K, true), new Q(Photo.Columns.UsrK, ThisUsr.K))
			//    );
			//    q.QueryCondition = new And(
			//        new Q(Usr.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty),
			//        new Q(Usr.Columns.IsEmailVerified, true),
			//        new Q(Usr.Columns.IsSkeleton, false)
			//    );
			//    q.Distinct = true;
			//    q.DistinctColumn = Usr.Columns.K;
			//    q.TopRecords = 18;
			//    UsrSet us = new UsrSet(q);
			//    UsrsSpottedShowAllLinkPanel.Visible = us.Count != ThisUsr.SpottingsTotal;

			//    if (us.Count > 0)
			//    {
			//        UsrsSpottedPanel.Visible = true;
			//        UsrsSpottedDataList.DataSource = us;
			//        UsrsSpottedDataList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/Spottings.ascx");
			//        UsrsSpottedDataList.DataBind();
			//    }
			//    else
			//        UsrsSpottedPanel.Visible = false;


			//    uiRecentGalleries.SpotterUsr = ThisUsr;
			//    uiRecentGalleries.DataBind();
			//    uiRecentGalleries.Visible = true;

			//}
			//else
			//{
			//    UsrsSpottedPanel.Visible = false;
			//    uiRecentGalleries.Visible = false;
			//}


		//	if (ThisUsr != null && !ThisUsr.IsSkeleton && ThisUsr.gall > 0)
		//	{
			try
			{
				uiRecentGalleries.SpotterUsr = ThisUsr;
				uiRecentGalleries.DataBind();
				uiRecentGalleries.Visible = uiRecentGalleries.HasContent;
			}
			catch { }

		//	}
		//	else
		//	{
		//		uiRecentGalleries.Visible = false;
		//	}
		}
		#endregion

		#region FavouritePhotoPanel
		public void FavouritePhoto_Load(object o, System.EventArgs e)
		{
			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{
				Query q = new Query();
				q.TableElement = Templates.Photos.Default.PerformJoins(Photo.UsrFavouritesJoin);
				q.Columns = Templates.Photos.Default.Columns;
				q.NoLock = true;
				q.QueryCondition = new And(
					Photo.EnabledQueryCondition,
					new Q(UsrPhotoFavourite.Columns.UsrK, ThisUsr.K)
				);
				q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Descending);
				q.TopRecords = 6;
				PhotoSet ps = new PhotoSet(q);
				FavouritePhotosShowAllLinkPanel.Visible = ps.Count == 6;

				if (ps.Count > 0)
				{

					FavouritePhotoPanel.Visible = true;
					FavouritePhotoDataList.DataSource = ps;
					FavouritePhotoDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/Default.ascx");
					FavouritePhotoDataList.DataBind();
				}
				else
					FavouritePhotoPanel.Visible = false;

			}
		}
		#endregion

		#region PhotosMePanel
		public void PhotosMe_Load(object o, System.EventArgs e)
		{
			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{

				Query q = new Query();
				q.TableElement = Templates.Photos.Default.PerformJoins(Photo.UsrMeJoin);
				q.Columns = Templates.Photos.Default.Columns;
				q.NoLock = true;
				q.QueryCondition = new And(
					Photo.EnabledQueryCondition,
					new Q(UsrPhotoMe.Columns.UsrK, UsrK)
					);
				q.TopRecords = 6;
				q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Descending);
				PhotoSet ps = new PhotoSet(q);
				PhotosMeShowAllLinkPanel.Visible = ps.Count == 6;
				if (ps.Count > 0)
				{

					PhotosMePanel.Visible = true;
					PhotosMeDataList.DataSource = ps;
					PhotosMeDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/Default.ascx");
					PhotosMeDataList.DataBind();
				}
				else
					PhotosMePanel.Visible = false;
			}
		}

		#endregion

		#region Main panel
		public void MainPanelMisc_Load(object o, System.EventArgs e)
		{
			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{
				if (ThisUsr.CommentCount > 0)
					UsrCommentsLabel.Text = "<a href=\"" + ThisUsr.UrlMyComments() + "\">" + ThisUsr.CommentCount.ToString("#,##0") + " comment" + (ThisUsr.CommentCount == 1 ? "" : "s") + "</a>";
				else
					UsrCommentsLabel.Text = "no comments";

				GroupInviteAnchor.HRef = ThisUsr.UrlApp("invite");
				GroupInviteAnchor.InnerHtml = GroupInviteAnchor.InnerHtml.Replace("xxx", ThisUsr.NickName);

				if (ThisUsr.CommentCount > 0)
				{
					UsrChatArchiveAnchor.HRef = ThisUsr.UrlApp("chat");
					UsrChatArchiveAnchor.InnerHtml = UsrChatArchiveAnchor.InnerHtml.Replace("xxx", "Read comments posted by " + ThisUsr.NickName);
				}
				else
					UsrChatArchiveAnchorP.Visible = false;

				if (ThisUsr.ChatMessageCount == 0)
					UsrChatLabel.Text = "I've not posted in the live-chat yet";
				else if (ThisUsr.ChatMessageCount == 1)
					UsrChatLabel.Text = "one live chat message";
				else
					UsrChatLabel.Text = ThisUsr.ChatMessageCount.ToString("#,##0") + " live chat messages";

				if (ThisUsr.PhotosMeCount == 0)
					UsrPhotoLabel.Text = ", and I've not been spotted yet.";
				else if (ThisUsr.PhotosMeCount == 1)
					UsrPhotoLabel.Text = ", and I've been <a href=\"" + ThisUsr.Url() + "#PhotosMe\">spotted once</a>.";
				else if (ThisUsr.PhotosMeCount <= 18)
					UsrPhotoLabel.Text = ", and I've been <a href=\"" + ThisUsr.Url() + "#PhotosMe\">spotted " + ThisUsr.PhotosMeCount.ToString("#,##0") + " times</a>.";
				else
					UsrPhotoLabel.Text = ", and I've been <a href=\"" + ThisUsr.UrlMyPhotos() + "\">spotted " + ThisUsr.PhotosMeCount.ToString("#,##0") + " times</a>.";

				AdminIconPanel.Visible = ThisUsr.IsAdmin;
				ModeratorIconPanel.Visible = !ThisUsr.IsAdmin && ThisUsr.IsSenior;
				EventModeratorIconPanel.Visible = !ThisUsr.IsAdmin && ThisUsr.IsSuper;
				SuperIconPanel.Visible = ThisUsr.IsSuperUser;
				DsiRegularIconPanel.Visible = ThisUsr.IsDsiRegular & !ThisUsr.IsSuperUser;
				//Donate1Panel.Visible = ThisUsr.Donate1IconShown;
				//Donate2Panel.Visible = ThisUsr.Donate2IconShown;
				DjIconPanel.Visible = ThisUsr.IsDj.HasValue && ThisUsr.IsDj.Value;

				var ds = DonationIcon.GetIconsForUsr(ThisUsr.K);
				if (ds != null && ds.Count > 0)
				{
					int i = 0;
					foreach (var d in ds)
					{
						uiDonationIconsHtml.Text +=
							string.Format(
								"<a href='/pages/icons/k-{0}' class='CleanLinks'><img src='{1}' border='0' align='absmiddle' style='margin-right:3px;' width='26' height='21'></a>",
								d.K, d.IconPath);
					}
					uiDonationIconsPanel.Visible = true;

					try
					{
						DonationIcon d = new DonationIcon(ThisUsr.RolloverDonationIconK.Value);
						DonationIconTopP.InnerHtml = string.Format(@"<a href=""/pages/icons/k-{0}""><img src=""{1}"" border=""0"" align=""absmiddle"" style=""margin-right:3px;"" width=""26"" height=""21"" />{2}</a>",
							d.K.ToString(),
							d.IconPath,
							d.IconText);
					}
					catch {
						DonationIconTopP.Visible = false;
					}

					if (Usr.Current != null && ThisUsr.K == Usr.Current.K)
					{
						DonationRolloverP.Visible = true;

						{

							if (!Page.IsPostBack)
							{
								DonationRolloverDrop.DataSource = DonationIcon.GetIconsForUsr(ThisUsr.K);
								DonationRolloverDrop.DataTextField = "IconText";
								DonationRolloverDrop.DataValueField = "K";
								DonationRolloverDrop.DataBind();
							}

							try
							{
								DonationIcon d = new DonationIcon(Usr.Current.RolloverDonationIconK.Value);

								

								if (!Page.IsPostBack)
									DonationRolloverDrop.SelectedValue = d.K.ToString();
								DonationRolloverImage.Src = d.IconPath;
							}
							catch {
								
							}
						}
					}
					else
						DonationRolloverP.Visible = false;
				}
				else
				{
					uiDonationIconsPanel.Visible = false;
					DonationRolloverP.Visible = false;
				}


				DonationIconLegendP.Visible = ThisUsr.IsDonationIconLegend;

				ExtraIconPanel.Visible = ThisUsr.ExtraIconInUse > 0;
				TicketIconPanel.Visible = ThisUsr.HasTicket && ThisUsr.LastTicketEventDateTime > DateTime.Now.AddDays(-1);

				SpotterIconPanel.Visible = ThisUsr.IsSpotter;
				if (SpotterIconPanel.Visible)
				{
					SpotterIcon.Src = ThisUsr.SpotterIconPath;
					SpotterLink.HRef = ThisUsr.IsProSpotter ? "/pages/prospotters" : "/pages/spotters";
					SpotterLabel.Text = ThisUsr.SpotterStatus(true, true, true);

					SpotterSpottingsLabel.Text = (ThisUsr.SpottingsTotal > 0 ? "<a href=\"" + ThisUsr.UrlSpottings() + "\">" : "") + ThisUsr.SpottingsTotal.ToString("#,##0") + " " + (ThisUsr.SpottingsTotal == 1 ? "person" : "people") + (ThisUsr.SpottingsTotal > 0 ? "</a>" : "");
				}

				if (ThisUsr.PicPhotoK > 0)
				{
					try
					{
						PicAnchor.HRef = ThisUsr.PicPhoto.Url();
					}
					catch { }
				}

				ChatterboxIconPanel.Visible = ThisUsr.IsChatterBox;
				NewUserIconPanel.Visible = ThisUsr.IsNewUser;
				DiscussionModeratorIconPanel.Visible = !ThisUsr.IsAdmin && ThisUsr.IsJunior;

				if (ThisUsr.MusicTypesFavouriteCount > 0)
				{
					UsrMusicTypesPanel.Visible = true;
					UsrMusicTypesLabel.Text = "";
					for (int i = 0; i < ThisUsr.MusicTypesFavourite.Count; i++)
					{
						UsrMusicTypesLabel.Text += (i > 0 ? ", " : "") + ThisUsr.MusicTypesFavourite[i].GenericName;
					}
				}
				else
					UsrMusicTypesPanel.Visible = false;

				if (ThisUsr.PlacesVisitCount > 0)
				{
					UsrPlaceVisitPanel.Visible = true;
					UsrPlaceVisitLabel.Text = "";
					PlaceSet ps = ThisUsr.PlacesVisit(Place.LinkColumns, 0);
					for (int i = 0; i < ps.Count; i++)
					{
						UsrPlaceVisitLabel.Text += (i > 0 ? ", " : "") + "<a href=\"" + ps[i].Url() + "\">" + ps[i].Name + "</a>";
					}
				}
				else
					UsrPlaceVisitPanel.Visible = false;
			}
		}
		#region DonationRolloverDrop_Change
		protected void DonationRolloverDrop_Change(object sender, EventArgs eventArgs)
		{
			var dis = DonationIcon.GetIconsForUsr(Usr.Current.K);
			int currentlySelectedK = int.Parse(DonationRolloverDrop.SelectedValue);
			
			foreach (DonationIcon di in dis)
			{
				if (di.K == currentlySelectedK)
				{
					Usr.Current.RolloverDonationIconK = di.K;
					Usr.Current.Update();
					Response.Redirect(Usr.Current.Url());
					break;
				}
			}
		}
		#endregion
		public string BuddyButtonState
		{
			get
			{
				if (Usr.Current == null)
					return "0";

				return Usr.Current.HasBuddy(ThisUsr.K) ? "1" : "0";
			}
		}
		public string BuddyInviteButtonState
		{
			get
			{
				if (Usr.Current == null)
					return "0";

				return Usr.Current.CanBuddyInvite(ThisUsr.K) ? "1" : "0";
			}
		}
		#region BuddyCheck
		public void BuddyCheck_Load(object o, System.EventArgs e)
		{
			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{
				//	if (!Page.IsPostBack)
				//	{
				//		BuddyCheck.Checked = (Usr.Current!=null && Usr.Current.HasBuddy(ThisUsr.K));
				//	}
				//	BuddyCheck.Text = BuddyCheck.Text.Replace("?",HttpUtility.HtmlEncode(ThisUsr.NickName));
				if ((Usr.Current != null && ThisUsr.K == Usr.Current.K) || ThisUsr.Banned || ThisUsr.K == 8)
				{
					OtherUsrPanel.Visible = false;
				}
				else
				{
					OtherUsrPanel.Visible = true;
				}
			}
		}
		#endregion
		#region Pic
		public void Pic_Load(object o, System.EventArgs e)
		{
			if (ThisUsr != null && !ThisUsr.IsSkeleton)
			{
				if (ThisUsr.HasPic)
				{
					PicImg.Visible = true;
					PicImg.Src = ThisUsr.PicPath;
					PicCell.Visible = true;
				}
				else
					PicCell.Visible = false;
			}
		}
		#endregion
		#endregion

		#region UsrK, ThisUsr
		int UsrK
		{
			get
			{
				if (ContainerPage.Url.HasUsrObjectFilter)
					return ContainerPage.Url.ObjectFilterUsr.K;
				else if (ContainerPage.Url["K"].IsNull)
				{
					Usr.KickUserIfNotLoggedIn("You must be logged in to view your profile.");
					return Usr.Current.K;
				}
				else
					return ContainerPage.Url["K"];
			}
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelNoDetails.Visible = p.Equals(PanelNoDetails);
			PanelDetails.Visible = p.Equals(PanelDetails);
			//PanelNoVerifyEmail.Visible = p.Equals(PanelNoVerifyEmail);
			//PanelUnsubscribedUser.Visible = p.Equals(PanelUnsubscribedUser);
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
			this.Load += new System.EventHandler(this.PhotosMe_Load);
			this.Load += new System.EventHandler(this.SendMessage_Load);
			this.Load += new System.EventHandler(this.BuddyCheck_Load);
			this.Load += new System.EventHandler(this.Pic_Load);
			this.Load += new System.EventHandler(this.MainPanelMisc_Load);
			this.Load += new System.EventHandler(this.EventsAttended_Load);
			this.Load += new System.EventHandler(this.EventsAttendFuture_Load);
			this.Load += new System.EventHandler(this.FavouritePhoto_Load);
			this.Load += new System.EventHandler(this.PanelChat_Load);
			this.Load += new System.EventHandler(this.PanelBan_Load);
			this.Load += new System.EventHandler(this.UsrsSpotted_Load);
			this.Load += new System.EventHandler(this.GalleriesPanel_Load);

			this.PreRender += new System.EventHandler(SetCommentDuplicateGuid);
			this.PreRender += new System.EventHandler(this.PersonalStatement_PreRender);
		}
		#endregion
	}
}
