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
using System.Text;
using System.IO;
using System.Collections.Generic;
using Bobs.Jobs;
using Spotted.Templates.Comments;
using System.Linq;
namespace Spotted.Pages
{
	public partial class Chat : DsiUserControl, ICommentsPage
	{
		#region Page_Init
		public void Page_Init(object o, System.EventArgs eArgs)
		{
			if (ViewThread)
			{
				CurrentThread.AddRelevant(ContainerPage);
			}
			
		}
		#endregion
		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}
		#endregion

		#region PanelForum
		
		#region PanelForum_Load
		public void PanelForum_Load(object o, System.EventArgs e)
		{
			if (!ViewForum)
				return;

			if (!CurrentForumCheckPermissionRead)
			{
				ChangePanel(PanelForumPrivate);
				return;
			}

			ChangePanel(PanelForum);
		}
		#endregion

		#region CurrentForumCheck
		bool CurrentForumCheck
		{
			get
			{
				return ViewForum && CurrentForumCheckPermissionRead;
			}
		}
		#endregion
		#region CurrentForumCheckPermissionRead
		bool CurrentForumCheckPermissionRead
		{
			get
			{
				if (!currentForumCheckPermissionReadSet)
				{
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
						currentForumCheckPermissionRead = CurrentGroup.CanRead(Usr.Current, CurrentGroupUsr);
					else
						currentForumCheckPermissionRead = true;

					currentForumCheckPermissionReadSet = true;
				}
				return currentForumCheckPermissionRead;
			}
		}
		bool currentForumCheckPermissionRead;
		bool currentForumCheckPermissionReadSet = false;
		#endregion
		#region CurrentForumCheckPermissionPost
		bool CurrentForumCheckPermissionPost
		{
			get
			{
				if (!currentForumCheckPermissionPostSet)
				{
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
						currentForumCheckPermissionPost = CurrentGroup.IsMember(CurrentGroupUsr);
					else
						currentForumCheckPermissionPost = Usr.Current != null;

					currentForumCheckPermissionPostSet = true;
				}
				return currentForumCheckPermissionPost;
			}
		}
		bool currentForumCheckPermissionPost;
		bool currentForumCheckPermissionPostSet = false;
		#endregion

		#region ForumInfo
		#region ForumInfo_Load
		public void ForumInfo_Load(object o, System.EventArgs e)
		{
			if (!CurrentForumCheck)
				return;

			PanelThreadDescTypeNone.Visible = ThreadParentType.Equals(Model.Entities.ObjectType.None);
			PanelThreadDescTypeEvent.Visible = ThreadParentType.Equals(Model.Entities.ObjectType.Event);
			PanelThreadDescTypeVenue.Visible = ThreadParentType.Equals(Model.Entities.ObjectType.Venue);
			PanelThreadDescTypePlace.Visible = ThreadParentType.Equals(Model.Entities.ObjectType.Place);
			PanelThreadDescTypeCountry.Visible = ThreadParentType.Equals(Model.Entities.ObjectType.Country);
			PanelThreadDescTypeArticle.Visible = ThreadParentType.Equals(Model.Entities.ObjectType.Article);
			PanelThreadDescTypeBrand.Visible = ThreadParentType.Equals(Model.Entities.ObjectType.Brand);
			PanelThreadDescTypeGroup.Visible = ThreadParentType.Equals(Model.Entities.ObjectType.Group);
			PanelThreadDescRelatedPanel.Visible = false;
			PanelThreadDescGroupBrandPanel.Visible = false;
			PanelThreadDescBrandPanel.Visible = false;
			FavouriteGroupPanel.Visible = (ThreadParentType.Equals(Model.Entities.ObjectType.Group) && CurrentGroupUsr != null && CurrentGroupUsr.IsMember);

			SetPageTitle("General discussions");

			if (ThreadParentType.Equals(Model.Entities.ObjectType.None))
			{
				ThreadDescWorldwideHomeCountryLink.InnerText = ThreadDescWorldwideHomeCountryLink.InnerText.Replace("???", Country.Current.FriendlyName);
				ThreadDescWorldwideHomeCountryLink.HRef = Country.Current.UrlDiscussion();
			}
			if (ThreadParentType.Equals(Model.Entities.ObjectType.Event))
			{
				Event ev = new Event(ObjectK);
				ThreadDescEventEventLink.InnerText = ev.Name;
				ThreadDescEventEventLink.HRef = ev.Url();

				ThreadDescEventVenueLink.InnerText = ev.Venue.Name;
				ThreadDescEventVenueLink.HRef = ev.Venue.Url();

				ThreadDescEventPlaceLink.InnerText = ev.Venue.Place.Name;
				ThreadDescEventPlaceLink.HRef = ev.Venue.Place.Url();

				ThreadDescEventDateLabel.Text = ev.FriendlyDate(false);

				PanelThreadDescRelatedPanel.Visible = ev.Brands.Count > 0;
				string brandsHtml = "";
				for (int i = 0; i < ev.Brands.Count; i++)
				{
					brandsHtml += (i == 0 ? "" : (i == (ev.Brands.Count - 1) ? " or " : ", ")) + "the <b><a href=\"" + ev.Brands[i].UrlDiscussion() + "\">" + ev.Brands[i].Name + " forum</a></b>";
				}
				PanelThreadDescRelatedPh.Controls.Add(new LiteralControl(brandsHtml));

				SetPageTitle(ev.Name + " discussions");


			}
			if (ThreadParentType.Equals(Model.Entities.ObjectType.Venue))
			{
				Venue v = new Venue(ObjectK);

				ThreadDescVenueVenueLink.InnerText = v.Name;
				ThreadDescVenueVenueLink.HRef = v.Url();

				ThreadDescVenuePlaceLink.InnerText = v.Place.Name;
				ThreadDescVenuePlaceLink.HRef = v.Place.Url();
				SetPageTitle(v.Name + " discussions");
			}
			if (ThreadParentType.Equals(Model.Entities.ObjectType.Place))
			{
				Place t = new Place(ObjectK);

				ThreadDescPlacePlaceLink.InnerText = t.Name;
				ThreadDescPlacePlaceLink.HRef = t.Url();
				SetPageTitle(t.Name + " discussions");
			}
			if (ThreadParentType.Equals(Model.Entities.ObjectType.Country))
			{
				Country c = new Country(ObjectK);

				ThreadDescCountryLabel.Text = c.FriendlyName;
				ThreadDescCountryLink.HRef = c.Url();
				SetPageTitle(c.FriendlyName + " discussions");
			}
			if (ThreadParentType.Equals(Model.Entities.ObjectType.Article))
			{
				Article a = new Article(ObjectK);
				ThreadDescArticleArticleLink.InnerText = a.Title;
				ThreadDescArticleArticleLink.HRef = a.Url();
				SetPageTitle(a.Title + " discussions");
			}
			if (ThreadParentType.Equals(Model.Entities.ObjectType.Brand))
			{
				Brand b = new Brand(ObjectK);
				ThreadDescBrandBrandLink.InnerText = b.Name;
				ThreadDescBrandBrandLink.HRef = b.Url();
				SetPageTitle(b.Name + " discussions");
				if (b.Group.TotalComments > 0)
				{
					PanelThreadDescBrandPanel.Visible = true;
					PanelThreadDescBrandGroupChatAnchor.InnerText = b.Group.FriendlyName + " group chat";
					PanelThreadDescBrandGroupChatAnchor.HRef = b.Group.UrlDiscussion();
					PanelThreadDescBrandGroupChatCommentsLabel.Text = b.Group.TotalComments.ToString("#,##0") + " comment" + (b.Group.TotalComments == 1 ? "" : "s");
				}
			}
			if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
			{
				ThreadDescGroupGroupLink.InnerText = CurrentGroup.FriendlyName + " group";
				ThreadDescGroupGroupLink.HRef = CurrentGroup.Url();
				SetPageTitle(CurrentGroup.FriendlyName + " discussions");
				if (CurrentGroup.BrandK > 0)
				{
					PanelThreadDescGroupBrandPanel.Visible = true;
					PanelThreadDescGroupBrandAnchor.HRef = CurrentGroup.Brand.UrlDiscussion();
					PanelThreadDescGroupBrandAnchor.InnerText = CurrentGroup.Brand.Name + " public chat";
					PanelThreadDescGroupBrandCommentsLabel.Text = CurrentGroup.Brand.TotalComments.ToString("#,##0") + " comment" + (CurrentGroup.Brand.TotalComments == 1 ? "" : "s");
				}
			}
		}
		#endregion
		#region CommentAlertButton
		protected string CommentAlertButtonArgs
		{
			get
			{
				return ObjectK.ToString() + "," + ((int)ThreadParentType).ToString();
			}
		}
		protected string CommentAlertButtonState
		{
			get
			{
				return (Usr.Current != null && CommentAlert.IsEnabled(Usr.Current.K, ObjectK, ThreadParentType)) ? "1" : "0";
			}
		}
		protected string CommentAlertButtonGroupForumString
		{
			get
			{
				return ThreadParentType.Equals(Model.Entities.ObjectType.Group) ? "group" : "forum";
			}
		}
		#endregion
		#region FavouriteGroupButton
		protected string FavouriteGroupButtonGroupK
		{
			get
			{
				if (CurrentGroup != null)
					return CurrentGroup.K.ToString();
				else
					return "";
			}
		}
		protected string FavouriteGroupButtonState
		{
			get
			{
				return (CurrentGroupUsr != null && CurrentGroupUsr.IsMember && CurrentGroupUsr.Favourite) ? "1" : "0";
			}
		}
		#endregion
		#endregion

		#region ForumThreads
		#region ForumThreads_PreRender
		public void ForumThreads_PreRender(object o, System.EventArgs e)
		{
			if (!CurrentForumCheck)
				return;

			BindThreads();
		}
		#endregion
		#region BindThreads()
		void BindThreads()
		{

			if (ThreadParentType.Equals(Model.Entities.ObjectType.None) ||
				ThreadParentType.Equals(Model.Entities.ObjectType.Country) ||
				ThreadParentType.Equals(Model.Entities.ObjectType.Place))
			{
				CommentAlertPanel.Visible = false;
			}
			else
			{
				CommentAlertPanel.Visible = true;
			}

			if (Threads.Count == 0)
			{
				ThreadsPanel.Visible = false;
				NoThreadsPanel.Visible = true;
			}
			else
			{
				ThreadsPanel.Visible = true;
				NoThreadsPanel.Visible = false;

				if (!Threads.Paging.ShowNextPageLink && !Threads.Paging.ShowPrevPageLink)
				{
					ThreadsPageP.Visible = false;
					ThreadsPageP1.Visible = false;
				}
				else
				{
					ThreadsPageP.Visible = true;
					ThreadsPageP1.Visible = true;

					string urlNextPage = ContainerPage.Url.CurrentUrl("p", ((int)(ThreadPage + 1)).ToString());
					string urlPrevPage = "";
					if (ThreadPage == 2)
						urlPrevPage = ContainerPage.Url.CurrentUrl("p", null);
					else
						urlPrevPage = ContainerPage.Url.CurrentUrl("p", ((int)(ThreadPage - 1)).ToString());

					ThreadsNextPageLink1.Enabled = Threads.Paging.ShowNextPageLink;
					ThreadsNextPageLink1.NavigateUrl = urlNextPage;
					ThreadsPrevPageLink1.Enabled = Threads.Paging.ShowPrevPageLink;
					ThreadsPrevPageLink1.NavigateUrl = urlPrevPage;

					ThreadsNextPageLink.Enabled = Threads.Paging.ShowNextPageLink;
					ThreadsNextPageLink.NavigateUrl = urlNextPage;
					ThreadsPrevPageLink.Enabled = Threads.Paging.ShowPrevPageLink;
					ThreadsPrevPageLink.NavigateUrl = urlPrevPage;

					if (!ThreadsNextPageLink1.Enabled)
						ThreadsNextPageLink1.CssClass = "DisabledAnchor";
					if (!ThreadsNextPageLink.Enabled)
						ThreadsNextPageLink.CssClass = "DisabledAnchor";
					if (!ThreadsPrevPageLink1.Enabled)
						ThreadsPrevPageLink1.CssClass = "DisabledAnchor";
					if (!ThreadsPrevPageLink.Enabled)
						ThreadsPrevPageLink.CssClass = "DisabledAnchor";
				}

				ThreadsDataGrid.DataSource = Threads;
				ThreadsDataGrid.DataBind();

			}

		}
		#endregion

		bool EnableThreadPrivacy
		{
			get
			{
				return Usr.Current != null && !(ThreadParentType.Equals(Model.Entities.ObjectType.None) || ThreadParentType.Equals(Model.Entities.ObjectType.Country) || ThreadParentType.Equals(Model.Entities.ObjectType.Place));
			}
		}

		#region Threads
		ThreadSet Threads
		{
			get
			{
				if (threads == null)
				{
					Query q = new Query();

					q.TopRecords = (ThreadPage * Vars.ThreadsPerPage) + 1;

					q.Paging.RecordsPerPage = Vars.ThreadsPerPage;
					q.Paging.RequestedPage = ThreadPage;

					#region Columns
					q.Columns = new ColumnSet(
						Thread.Columns.K,
						Thread.Columns.Private,
						Thread.Columns.GroupPrivate,
						Thread.Columns.PrivateGroup,
						Thread.Columns.Subject,
						Thread.Columns.LastPost,
						Thread.Columns.TotalComments,
						Thread.Columns.TotalParticipants,
						Thread.Columns.TotalWatching,
						Thread.Columns.IsNews,
						Thread.Columns.IsReview,
						Thread.Columns.ParentObjectType,
						Thread.Columns.ParentObjectK,
						Thread.Columns.GroupK,

						ThreadUsr.Columns.ThreadK,
						ThreadUsr.Columns.UsrK,
						ThreadUsr.Columns.Status,
						ThreadUsr.Columns.StatusChangeObjectType, 
						ThreadUsr.Columns.Favourite,
						ThreadUsr.Columns.ViewComments,
						ThreadUsr.Columns.ViewCommentsLatest,
						ThreadUsr.Columns.ViewDateTime,
						ThreadUsr.Columns.ViewDateTimeLatest,

						Thread.Columns.UsrK,
						//	new JoinedColumnSet(Thread.Columns.UsrK, Usr.LinkColumns),

						Thread.Columns.LastPostUsrK,
						//	new JoinedColumnSet(Thread.Columns.LastPostUsrK, Usr.LinkColumns),

						Thread.Columns.FirstParticipantUsrK
						//	new JoinedColumnSet(Thread.Columns.FirstParticipantUsrK, Usr.LinkColumns),

					//	Photo.Columns.K,
						//	Photo.Columns.Icon,
						//	Photo.Columns.ContentDisabled
					);
					#endregion

					q.OrderBy = Thread.Order;

					#region Main query condition
					if (ThreadParentType.Equals(Model.Entities.ObjectType.None))
						q.QueryCondition = new Q(true);
					else if (ThreadParentType.Equals(Model.Entities.ObjectType.Event))
						q.QueryCondition = new Q(Thread.Columns.EventK, ObjectK);
					else if (ThreadParentType.Equals(Model.Entities.ObjectType.Venue))
						q.QueryCondition = new Q(Thread.Columns.VenueK, ObjectK);
					else if (ThreadParentType.Equals(Model.Entities.ObjectType.Place))
						q.QueryCondition = new Q(Thread.Columns.PlaceK, ObjectK);
					else if (ThreadParentType.Equals(Model.Entities.ObjectType.Country))
						q.QueryCondition = Country.ThreadsQ(ObjectK);
					else if (ThreadParentType.Equals(Model.Entities.ObjectType.Article))
						q.QueryCondition = Article.ThreadsQ(ObjectK);
					else if (ThreadParentType.Equals(Model.Entities.ObjectType.Brand))
						q.QueryCondition = Brand.ThreadsQEvents((Brand)ThreadParent);
					else if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
						q.QueryCondition = Group.ThreadsQ(ObjectK);
					#endregion

					#region Main table element
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Brand))
						q.TableElement = Thread.EventBrandJoin;
					else
						q.TableElement = new TableElement(TablesEnum.Thread);
					#endregion

					if (EnableThreadPrivacy)
					{
						#region ThreadUsr join
						q.TableElement = new Join(
							q.TableElement,
							new TableElement(TablesEnum.ThreadUsr),
							QueryJoinType.Left,
							new And(
								new Q(Thread.Columns.K, ThreadUsr.Columns.ThreadK, true),
								new Q(ThreadUsr.Columns.UsrK, (Usr.Current == null ? 0 : Usr.Current.K)),
								new Q(ThreadUsr.Columns.Status, QueryOperator.NotEqualTo, ThreadUsr.StatusEnum.Deleted)
							)
						);
						#endregion

						#region GroupUsr join
						q.TableElement = new Join(
							q.TableElement,
							new TableElement(TablesEnum.GroupUsr),
							QueryJoinType.Left,
							new And(
								new Q(Thread.Columns.GroupK, GroupUsr.Columns.GroupK, true),
								new Q(GroupUsr.Columns.UsrK, (Usr.Current == null ? 0 : Usr.Current.K)),
								new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));
						#endregion

						#region Privacy query condition
						q.QueryCondition = new And(
							q.QueryCondition,
							new Or(
								new Q(Thread.Columns.Private, false),
								new Q(ThreadUsr.Columns.UsrK, (Usr.Current == null ? 0 : Usr.Current.K))
							),
							new Or(
								new Q(Thread.Columns.GroupPrivate, false),
								new Q(GroupUsr.Columns.UsrK, (Usr.Current == null ? 0 : Usr.Current.K))
							),
							new Or(
								new Q(Thread.Columns.PrivateGroup, false),
								new Q(GroupUsr.Columns.UsrK, (Usr.Current == null ? 0 : Usr.Current.K))
							)
						);
						#endregion
					}
					else
					{
						#region ThreadUsr join
						q.TableElement = new Join(
							q.TableElement,
							new TableElement(TablesEnum.ThreadUsr),
							QueryJoinType.Left,
							new And(
								new Q(Thread.Columns.K, ThreadUsr.Columns.ThreadK, true),
								new Q(ThreadUsr.Columns.UsrK, (Usr.Current == null ? 0 : Usr.Current.K))
							)
						);
						#endregion

						#region Privacy query condition
						q.QueryCondition = new And(
							q.QueryCondition,
							new Q(Thread.Columns.Private, false),
							new Q(Thread.Columns.GroupPrivate, false),
							new Q(Thread.Columns.PrivateGroup, false)
						);
						#endregion
					}

					threads = new ThreadSet(q);


					ThreadPage = threads.Paging.ReturnedPage;

				}
				return threads;
			}
			set
			{
				threads = value;
			}
		}
		ThreadSet threads;
		#endregion
		#region GetThreadUrl(Thread t, object[] par)
		public string GetThreadUrl(Thread t, object[] par)
		{
			return ContainerPage.Url.CurrentUrl(par);
		}
		#endregion
		#endregion

		#region ViewForum
		bool ViewForum
		{
			get
			{
				return (CurrentThreadK == 0);
			}
		}
		#endregion
		#region ThreadParentType
		Model.Entities.ObjectType ThreadParentType
		{
			get
			{
				if (IsUrlFilterObjectDiscuss)
					return ContainerPage.Url.ObjectFilterType;
				else
					return Model.Entities.ObjectType.None;
			}
		}
		#endregion
		#region ObjectK
		int ObjectK
		{
			get
			{
				if (IsUrlFilterObjectDiscuss)
					return ContainerPage.Url.ObjectFilterK;
				else
					return 0;
			}
		}
		#endregion
		#region ThreadParent
		object ThreadParent
		{
			get
			{
				if (threadParent == null)
					threadParent = Bob.Get(ThreadParentType, ObjectK);
				return threadParent;
			}
			set
			{
				threadParent = value;
			}
		}
		object threadParent;
		#endregion
		#region IsUrlFilterObjectDiscuss
		bool IsUrlFilterObjectDiscuss
		{
			get
			{
				return ContainerPage.Url.HasObjectFilter && ContainerPage.Url.ObjectFilterBob is IDiscussable;
			}
		}
		#endregion
		#region ThreadPage
		int ThreadPage
		{
			get
			{
				if (threadPage == -1)
				{
					if (ContainerPage.Url["P"].IsInt)
						return ContainerPage.Url["P"];
					else if (ContainerPage.Url["ThreadPage"].IsInt)
						return ContainerPage.Url["ThreadPage"];
					else
						return 1;
				}
				else
					return threadPage;
			}
			set
			{
				threadPage = value;
			}
		}
		int threadPage = -1;
		#endregion
		#region CurrentGroup
		Group CurrentGroup
		{
			get
			{
				if (!doneCurrentGroup)
				{
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
					{
						currentGroup = new Group(ObjectK);
					}
					doneCurrentGroup = true;
				}
				return currentGroup;
			}
		}
		bool doneCurrentGroup = false;
		Group currentGroup;
		#endregion
		#region CurrentGroupUsr
		GroupUsr CurrentGroupUsr
		{
			get
			{
				if (!doneCurrentGroupUsr)
				{
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
					{
						currentGroupUsr = CurrentGroup.GetGroupUsr(Usr.Current);
					}
					doneCurrentGroupUsr = true;
				}
				return currentGroupUsr;
			}
		}
		bool doneCurrentGroupUsr = false;
		GroupUsr currentGroupUsr;
		#endregion
		#endregion

		#region PanelThread

		#region PanelThread_PreRender
		public void PanelThread_PreRender(object o, System.EventArgs e)
		{
			if (!ViewThread)
				return;
			ContainerPage.ViewStatePublic["ReplyDuplicateGuid"] = Guid.NewGuid();
		}
		#endregion

		#region PanelThread_Load
		public void PanelThread_Load(object o, System.EventArgs e)
		{
			if (!ViewThread)
				return;


			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/thread?ID=" + CurrentThread.K + "\">Edit this thread</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('This will delete all comments.\\nARE YOU SURE?');\" href=\"/admin/multidelete?ObjectType=Thread&ObjectK=" + CurrentThread.K + "\">Delete this thread</a><br>Be careful - deletes all comments</p>"));
			}

			if (!CurrentThreadCheckPermissionRead)
			{
				if (CurrentThread.GroupK > 0)
				{
					GroupUsr gu = CurrentThread.Group.GetGroupUsr(Usr.Current);
					if (Group.AllowJoinRequest(Usr.Current, CurrentThread.Group, gu))
					{
						PanelGroupPrivateCanJoinGroupAnchor1.HRef = CurrentThread.Group.Url();
						PanelGroupPrivateCanJoinGroupAnchor2.HRef = CurrentThread.Group.Url();
						PanelGroupPrivateCanJoinGroupAnchor1.InnerText = CurrentThread.Group.FriendlyName;
						PanelGroupPrivateCanJoinGroupAnchor2.InnerText = CurrentThread.Group.FriendlyName;
						ChangePanel(PanelGroupPrivateCanJoin);
						return;
					}
					else if (gu != null && (gu.Status.Equals(GroupUsr.StatusEnum.Recommend) || gu.Status.Equals(GroupUsr.StatusEnum.RecommendRejected)))
					{
						ThreadUsr invitedThreadUsr = null;
						try
						{
							invitedThreadUsr = new ThreadUsr(CurrentThread.K, Usr.Current.K);
						}
						catch
						{
							ChangePanel(PanelPrivateThread);
							return;
						}
						if (invitedThreadUsr.Status.Equals(ThreadUsr.StatusEnum.NewInvite))
						{
							if (gu.Status.Equals(GroupUsr.StatusEnum.Recommend))
							{
								if (CurrentThread.Group.PrivateGroupPage)
									PanelGroupPrivateRecommendGroupSpan.InnerText = CurrentThread.Group.FriendlyName;
								else
									PanelGroupPrivateRecommendGroupSpan.InnerHtml = "<a href=\"" + CurrentThread.Group.Url() + "\">" + HttpUtility.HtmlEncode(CurrentThread.Group.FriendlyName) + "</a>";

								PanelGroupPrivateRecommendInvitingUsrSpan.InnerHtml = gu.InviteUsr.Link();
								ChangePanel(PanelGroupPrivateRecommend);
								return;
							}
							else
							{
								if (CurrentThread.Group.PrivateGroupPage)
									PanelGroupPrivateRecommendRejectedGroupSpan.InnerText = CurrentThread.Group.FriendlyName;
								else
									PanelGroupPrivateRecommendRejectedGroupSpan.InnerHtml = "<a href=\"" + CurrentThread.Group.Url() + "\">" + HttpUtility.HtmlEncode(CurrentThread.Group.FriendlyName) + "</a>";
								ChangePanel(PanelGroupPrivateRecommendRejected);
								return;
							}
						}
					}
					else if (gu != null && (gu.Status.Equals(GroupUsr.StatusEnum.Request)))
					{
						PanelGroupPrivateWaitingAnchor1.HRef = CurrentThread.Group.Url();
						PanelGroupPrivateWaitingAnchor2.HRef = CurrentThread.Group.Url();
						PanelGroupPrivateWaitingAnchor1.InnerText = CurrentThread.Group.FriendlyName;
						PanelGroupPrivateWaitingAnchor2.InnerText = CurrentThread.Group.FriendlyName;
						ChangePanel(PanelGroupPrivateWaiting);
						return;
					}
				}

				ChangePanel(PanelPrivateThread);

				return;
			}

			ChangePanel(PanelThread);
			BindSpamOptions();		   			
		}
		#endregion

		#region Spam Panels
		#region BindSpamOptions
		private void BindSpamOptions()
		{
			this.ThreadDetailsSpamPanel.Visible = Usr.Current != null && ReferringObject != null && ReferringObject is IBobType;
			this.ThreadSpamOptionsPanel.Visible = this.ThreadDetailsSpamPanel.Visible;

			if (Vars.DevEnv && ReferringObject != null && !(ReferringObject is IBobType))
				throw new DsiUserFriendlyException("Referring object is not IBobType");

			if (this.ThreadDetailsSpamPanel.Visible)
			{
				SpamSourceLabel.Text = " - SPAM! - From " + ((IBobType)ReferringObject).TypeName.ToLower() + (CurrentThread.IsNews && ReferringObject is Group ? " news" : "") + ": ";

				if (ReferringObject is ILinkable)
					SpamSourceLabel.Text += ((ILinkable)ReferringObject).Link();
				else if (ReferringObject is IReadableReference)
					SpamSourceLabel.Text += ((IReadableReference)ReferringObject).ReadableReference;
			//	else if (ReferringObject is Photo)
			//		SpamSourceLabel.Text += ((IName)ReferringObject).Name;

				
				this.ThreadSpamOption1RadioButton.Visible = false;
				this.ThreadSpamOption2RadioButton.Visible = false;
				this.ThreadSpamOption3RadioButton.Visible = false;

				try
				{
					if (ReferringObject is Usr)
					{
						if (Usr.Current.HasBuddy(((Usr)ReferringObject).K))
						{
							this.ThreadSpamOption1RadioButton.Visible = true;
							this.ThreadSpamOption1RadioButton.Text = "Un-buddy";
						}
						if (((Usr)ReferringObject).CanInvite(Usr.Current.K))
						{
							this.ThreadSpamOption2RadioButton.Visible = true;
							this.ThreadSpamOption2RadioButton.Text = "Stop bulk invites";
						}
					}
					else
					{
						this.ThreadSpamOption1RadioButton.Visible = true;
						this.ThreadSpamOption1RadioButton.Text = "Stop watching";

						if (ReferringObject is Group)
						{
							if (((Group)ReferringObject).IsMember(Usr.Current))
							{
								if (CurrentThread.IsNews)
								{
									this.ThreadSpamOption1RadioButton.Visible = false;
								}
								this.ThreadSpamOption2RadioButton.Visible = true;
								this.ThreadSpamOption2RadioButton.Text = "Exit group";
							}
						}
						else
						{
							this.ThreadSpamOption1RadioButton.Visible = false;
						}
					}

					this.ThreadSpamOption3RadioButton.Visible = true;
					this.ThreadSpamOption3RadioButton.Text = "Ignore this topic";
				}
				catch { }

			}
		}
		#endregion

		#region ReferringObject
		private IBob ReferringObject
		{
			get
			{
				if (referringObject == null)
				{
					if (ViewState["ThreadReferringObject"] != null)
						referringObject = (IBob)ViewState["ThreadReferringObject"];
					else if (CurrentThreadUsr != null)
					{
						if (CurrentThreadUsr.Status.Equals(ThreadUsr.StatusEnum.NewComment))
							referringObject = null;
						else if (CurrentThreadUsr.Status.Equals(ThreadUsr.StatusEnum.NewInvite))
							referringObject = CurrentThreadUsr.InvitingUsr;
						else
							referringObject = CurrentThreadUsr.StatusChangeObject;
						ViewState["ThreadReferringObject"] = referringObject;
					}
				}
				return referringObject;
			}
		}
		private IBob referringObject;
		#endregion

		#region ThreadSpamUpdateButton_Click
		public void ThreadSpamUpdateButton_Click(object o, System.EventArgs e)
		{
			Page.Validate("ThreadSpamValidationGroup");
			if(Page.IsValid)
			{
				try
				{
					if (ThreadSpamOption1RadioButton.Checked)
					{
						if (ReferringObject is Usr)
						{
							// Unbuddy user
							Usr.Current.RemoveBuddy(((Usr)ReferringObject).K);
						}
						else if (ReferringObject is IBobType)
						{
							CommentAlert.Disable(Usr.Current, ((IBobType)ReferringObject).K, ((IBobType)ReferringObject).ObjectType);
						}
						else if(Vars.DevEnv)
						{
							throw new DsiUserFriendlyException("ReferringObject is not IBobType");
						}
					}
					else if (ThreadSpamOption2RadioButton.Checked)
					{
						if (ReferringObject is Usr)
						{
							// Stop bulk invites
							Usr.Current.SetBuddyInvite((Usr)ReferringObject, false, Usr.AddBuddySource.SpamPage, null);
							
							//if (Usr.Current.HasBuddy(((Usr)ReferringObject).K))
							//{
							//    Buddy b = new Buddy(Usr.Current.K, ((Usr)ReferringObject).K);
							//    b.CanBuddyInvite = false;
							//    b.Update();
							//}
						}
						else if (ReferringObject is Group)
						{
							// Exit group
							((Group)ReferringObject).Exit(Usr.Current);
						}
					}
					else if (ThreadSpamOption3RadioButton.Checked)
					{
						CurrentThreadUsr.ChangeStatus(ThreadUsr.StatusEnum.Ignore);
					}

					Response.Redirect(UrlInfo.PageUrl("inbox"));
				}
				catch
				{ }
			}
			
		}
		#endregion
		#endregion

		#region ThreadInfo
		#region ThreadInfo_Load
		public void ThreadInfo_Load(object o, System.EventArgs e)
		{
			if (!CurrentThreadCheck())
				return;

			ThreadDetailAdvancedOptionsP.Visible = Usr.Current != null && (Usr.Current.K == CurrentThread.UsrK || (!CurrentThread.Private && Usr.Current.IsJunior) || (CurrentThread.GroupK > 0 && CurrentThreadGroupUsr != null && CurrentThreadGroupUsr.Moderator));
			ThreadDetailAdvancedOptionsAnchor.HRef = "/pages/chatadmin/k-" + CurrentThread.K;

			BindButtons();
			#region Comment descriptions

			StringBuilder sb = new StringBuilder();
			HtmlTextWriter w = new HtmlTextWriter(new StringWriter(sb));

			string topicType = "topic";
			if (CurrentThread.IsNews)
				topicType = "news";
			else if (CurrentThread.IsReview)
				topicType = "review";
			else if (CurrentThread.Private)
				topicType = "private topic";

			string icon = "/gfx/icon-discuss.png";
			if (CurrentThread.Private)
				icon = "/gfx/icon-key.png";

			if (CurrentThread.GroupK > 0)
			{
				w.Write("<img src=\"/gfx/icon-group.png\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
				w.Write("This ");
				w.Write(topicType);
				w.Write(" was posted in the ");
				w.RenderBeginTag("b");
				w.AddAttribute("href", CurrentThread.Group.UrlDiscussion());
				w.RenderBeginTag("a");
				w.Write("chat forum");
				w.RenderEndTag();//a
				w.RenderEndTag();//b
				w.Write(" of the ");
				w.RenderBeginTag("b");
				w.AddAttribute("href", CurrentThread.Group.Url());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.Group.FriendlyName));
				w.RenderEndTag();//a
				w.RenderEndTag();//b
				w.Write(" group");
				SetPageTitle(CurrentThread.Group.FriendlyName + " comments");
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
			{
				if (CurrentThread.ParentPhoto.Event != null)
				{
					w.Write("<img src=\"");
					w.Write(icon);
					w.Write("\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
					w.Write("This ");
					w.Write(topicType);
					w.Write(" was posted in the ");
					w.RenderBeginTag("b");
					w.AddAttribute("href", CurrentThread.ParentPhoto.Event.UrlDiscussion());
					w.RenderBeginTag("a");
					w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentPhoto.Event.Name));
					w.Write(" forum");
					w.RenderEndTag();//a
					w.RenderEndTag();//b
					SetPageTitle(CurrentThread.ParentPhoto.Event.Name + " comments");
				}
				else if (CurrentThread.ParentPhoto.Article != null)
				{
					w.Write("<img src=\"");
					w.Write(icon);
					w.Write("\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
					w.Write("This ");
					w.Write(topicType);
					w.Write(" was posted in the ");
					w.RenderBeginTag("b");
					w.AddAttribute("href", CurrentThread.ParentPhoto.Article.UrlDiscussion());
					w.RenderBeginTag("a");
					w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentPhoto.Article.Title));
					w.Write(" forum");
					w.RenderEndTag();//a
					w.RenderEndTag();//b
					SetPageTitle(CurrentThread.ParentPhoto.Article.Title + " comments");
				}
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Event))
			{
				w.Write("<img src=\"");
				w.Write(icon);
				w.Write("\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
				w.Write("This ");
				w.Write(topicType);
				w.Write(" was posted in the ");
				w.RenderBeginTag("b");
				w.AddAttribute("href", CurrentThread.ParentEvent.UrlDiscussion());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentEvent.Name));
				w.Write(" forum");
				w.RenderEndTag();//a
				w.RenderEndTag();//b
				SetPageTitle(CurrentThread.ParentEvent.Name + " comments");
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
			{
				w.Write("<img src=\"");
				w.Write(icon);
				w.Write("\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
				w.Write("This ");
				w.Write(topicType);
				w.Write(" was posted in the ");
				w.RenderBeginTag("b");
				w.AddAttribute("href", CurrentThread.ParentVenue.UrlDiscussion());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentVenue.Name));
				w.Write(" forum");
				w.RenderEndTag();//a
				w.RenderEndTag();//b
				SetPageTitle(CurrentThread.ParentVenue.Name + " comments");
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Place))
			{
				w.Write("<img src=\"");
				w.Write(icon);
				w.Write("\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
				w.Write("This ");
				w.Write(topicType);
				w.Write(" was posted in the ");
				w.RenderBeginTag("b");
				w.AddAttribute("href", CurrentThread.ParentPlace.UrlDiscussion());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentPlace.Name));
				w.Write(" forum");
				w.RenderEndTag();//a
				w.RenderEndTag();//b
				SetPageTitle(CurrentThread.ParentPlace.Name + " comments");
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Country))
			{
				w.Write("<img src=\"");
				w.Write(icon);
				w.Write("\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
				w.Write("This ");
				w.Write(topicType);
				w.Write(" was posted in the ");
				w.RenderBeginTag("b");
				w.AddAttribute("href", CurrentThread.ParentCountry.UrlDiscussion());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentCountry.FriendlyName));
				w.Write(" forum");
				w.RenderEndTag();//a
				w.RenderEndTag();//b
				SetPageTitle(CurrentThread.ParentCountry.FriendlyName + " comments");
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Article))
			{
				w.Write("<img src=\"");
				w.Write(icon);
				w.Write("\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
				w.Write("This ");
				w.Write(topicType);
				w.Write(" was posted in the ");
				w.RenderBeginTag("b");
				w.AddAttribute("href", CurrentThread.ParentArticle.UrlDiscussion());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentArticle.Title));
				w.Write(" forum");
				w.RenderEndTag();//a
				w.RenderEndTag();//b
				SetPageTitle(CurrentThread.ParentArticle.Title + " comments");
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.None))
			{
				w.Write("<img src=\"");
				w.Write(icon);
				w.Write("\" border=\"0\" align=\"absmiddle\" width=\"26\" height=\"21\" style=\"margin-right:3px;\">");
				w.Write("This ");
				w.Write(topicType);
				w.Write(" was posted in the ");
				w.RenderBeginTag("b");
				w.AddAttribute("href", "/chat");
				w.RenderBeginTag("a");
				w.Write("general forum");
				w.RenderEndTag();//a
				w.RenderEndTag();//b
				SetPageTitle("General comments");
			}

			PanelCommentsDescriptionP.InnerHtml = sb.ToString();

			#endregion
		}
		#endregion
		#region Buttons
		#region ThreadWatchingButtonState
		protected string ThreadWatchingButtonState
		{
			get
			{
				if (CurrentThreadUsr != null)
					return CurrentThreadUsr.IsWatching ? "1" : "0";
				else
					return "0";
			}
		}
		#endregion
		public string HasInbox
		{
			get
			{
				if (CurrentThreadUsr != null && CurrentThreadUsr.IsInbox)
					return "true";
				else
					return "false";
			}
		}
		#region ThreadFavouriteButtonState
		protected string ThreadFavouriteButtonState
		{
			get
			{
				if (CurrentThreadUsr != null)
					return CurrentThreadUsr.Favourite ? "1" : "0";
				else
					return "0";
			}
		}
		#endregion
		public string ThreadInboxButtonState
		{
			get
			{
				if (CurrentThreadUsr != null)
					return CurrentThreadUsr.IsInbox ? "1" : "0";
				else
					return "0";
			}
		}
		#region ThreadButtonK
		protected string ThreadButtonK
		{
			get
			{
				if (ViewThread)
					return CurrentThread.K.ToString();
				else
					return "";
			}
		}
		#endregion
		#endregion
		public void BindButtons()
		{
			#region Buttons
			ThreadDetailInboxPanel.Visible = (CurrentThreadUsr != null && CurrentThreadUsr.IsInbox);
			ThreadDetailInboxBottomSpan.Visible = (CurrentThreadUsr != null && CurrentThreadUsr.IsInbox);
			#endregion
			#region Inbox explanation
			ThreadDetailInboxPh.Controls.Clear();
			if (CurrentThreadUsr != null && CurrentThreadUsr.IsInbox)
			{
				try
				{
					if (CurrentThreadUsr.Status.Equals(ThreadUsr.StatusEnum.NewComment))
						ThreadDetailInboxPh.Controls.Add(new LiteralControl(" - it's here because you were watching it when a new comment was posted"));
					else if (CurrentThreadUsr.Status.Equals(ThreadUsr.StatusEnum.NewGroupNewsAlert))
						ThreadDetailInboxPh.Controls.Add(new LiteralControl(" - it's here because it's <a href=\"" + ((Group)CurrentThreadUsr.StatusChangeObject).Url() + "\">" + ((Group)CurrentThreadUsr.StatusChangeObject).Name + "</a> news"));
					else if (CurrentThreadUsr.Status.Equals(ThreadUsr.StatusEnum.NewInvite))
						ThreadDetailInboxPh.Controls.Add(new LiteralControl(" - it's here because you were invited by " + CurrentThreadUsr.InvitingUsr.Link()));
					else if (CurrentThreadUsr.Status.Equals(ThreadUsr.StatusEnum.NewWatchedForumAlert))
					{
						if (CurrentThreadUsr.StatusChangeObjectType.Equals(Model.Entities.ObjectType.Photo))
							ThreadDetailInboxPh.Controls.Add(new LiteralControl(" - it's here because you're watching for comments on <a href=\"" + ((Photo)CurrentThreadUsr.StatusChangeObject).Url() + "\">this photo</a>"));
						else
							ThreadDetailInboxPh.Controls.Add(new LiteralControl(" - it's here because you're watching the <a href=\"" + ((IDiscussable)CurrentThreadUsr.StatusChangeObject).UrlDiscussion() + "\">" + ((IName)CurrentThreadUsr.StatusChangeObject).Name + "</a> forum"));
					}
				}
				catch
				{

				}
			}
			#endregion
		}
		#endregion

		#region ThreadParticipants
		public void ThreadParticipants_Load(object o, System.EventArgs e)
		{
			if (!CurrentThreadCheck())
				return;

			PanelParticipants.Visible = CurrentThread.Private;

			BindParticipants();

		}
		#region ShowAllParticipants
		public bool ShowAllParticipants
		{
			get
			{
				if (this.ViewState["ShowAllParticipants"] == null)
					return false;
				else
					return (bool)this.ViewState["ShowAllParticipants"];
			}
			set
			{
				this.ViewState["ShowAllParticipants"] = value;
			}
		}
		#endregion
		public void ParticipantsShowAll_Click(object o, System.EventArgs e)
		{
			ShowAllParticipants = true;
			BindParticipants();
		}
		void BindParticipants()
		{
			if (!CurrentThread.Private)
				return;

			ParticipantsRefreshAnchor.HRef = ContainerPage.Url.CurrentUrl();

			if (CurrentThread.TotalParticipants > 24 && !ShowAllParticipants)
			{
				ParticipantsListPanel.Visible = false;
				ParticipantsHiddenPanel.Visible = true;
				ParticipantsLabel.Text = (CurrentThread.TotalParticipants == 1 ? "is " : "are ") + CurrentThread.TotalParticipants.ToString("#,##0") + (CurrentThread.TotalParticipants == 1 ? " participant" : " participants");
			}
			else
			{
				ParticipantsListPanel.Visible = true;
				ParticipantsHiddenPanel.Visible = false;
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(ThreadUsr.Columns.ThreadK, CurrentThreadK),
					new Q(ThreadUsr.Columns.Status, QueryOperator.NotEqualTo, ThreadUsr.StatusEnum.Deleted));
				q.OrderBy = new OrderBy(
					new OrderBy(Buddy.Columns.FullBuddy, OrderBy.OrderDirection.Descending),
					new OrderBy(new Column(ThreadUsr.Columns.UsrK, Usr.Columns.NickName)));
				q.Columns = new ColumnSet(
					ThreadUsr.Columns.ThreadK,
					ThreadUsr.Columns.Status,
					ThreadUsr.Columns.InvitingUsrK,
					ThreadUsr.Columns.DateTime,
					ThreadUsr.Columns.UsrK,
					new JoinedColumnSet(ThreadUsr.Columns.UsrK, Usr.LinkColumns, Usr.Columns.Email, Usr.Columns.FirstName, Usr.Columns.LastName, Usr.Columns.IsSkeleton, Usr.Columns.AddedByUsrK),
					ThreadUsr.Columns.InvitingUsrK,
					new JoinedColumnSet(ThreadUsr.Columns.InvitingUsrK, Usr.LinkColumns),
					Buddy.Columns.FullBuddy);
				q.TableElement = new Join(
					new TableElement(TablesEnum.ThreadUsr),
					new TableElement(new Column(ThreadUsr.Columns.UsrK, Usr.Columns.K)),
					QueryJoinType.Inner,
					ThreadUsr.Columns.UsrK,
					new Column(ThreadUsr.Columns.UsrK, Usr.Columns.K));
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(new Column(ThreadUsr.Columns.InvitingUsrK, Usr.Columns.K)),
					QueryJoinType.Left,
					ThreadUsr.Columns.InvitingUsrK,
					new Column(ThreadUsr.Columns.InvitingUsrK, Usr.Columns.K));
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.Buddy),
					QueryJoinType.Left,
					new And(
						new Q(ThreadUsr.Columns.UsrK, Buddy.Columns.UsrK, true),
						new Q(Buddy.Columns.BuddyUsrK, Usr.Current.K),
						new Q(Buddy.Columns.FullBuddy, true)));

				ThreadUsrSet tus = new ThreadUsrSet(q);

				if (tus.Count > 24 && !ShowAllParticipants)
				{
					ParticipantsListPanel.Visible = false;
					ParticipantsHiddenPanel.Visible = true;
					ParticipantsLabel.Text = (tus.Count == 1 ? "is " : "are ") + tus.Count.ToString("#,##0") + (tus.Count == 1 ? " participant" : " participants");
				}
				else
				{
					ParticipantsDataList.DataSource = tus;
					if (tus.Count > 12)
					{
						ParticipantsDataList.ItemTemplate = this.LoadTemplate("/Templates/ThreadUsrs/Small.ascx");
						ParticipantsDataList.RepeatColumns = 1;
					}
					else if (tus.Count > 4)
					{
						ParticipantsDataList.ItemTemplate = this.LoadTemplate("/Templates/ThreadUsrs/Medium.ascx");
					}
					else
					{
						ParticipantsDataList.ItemTemplate = this.LoadTemplate("/Templates/ThreadUsrs/Large.ascx");
					}
					ParticipantsDataList.DataBind();
				}
			}

		}
		#endregion

		#region ThreadSubject
		#region ThreadSubject_Load
		protected Pages.Articles.HomeContent PanelThreadSubjectArticleExtended;
		public void ThreadSubject_Init(object o, EventArgs e)
		{
			if (!CurrentThreadCheck())
				return;

			if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Article))
			{
				if (CurrentThread.ParentArticle.IsExtendedDisplay)
				{
					PanelThreadSubject.Visible = false;
					PanelThreadSubjectArticleExtended.CurrentArticle = CurrentThread.ParentArticle;
					PanelThreadSubjectArticleExtended.Visible = true;

				}
			}
		}
		public void ThreadSubject_Load(object o, System.EventArgs e)
		{
			if (!CurrentThreadCheck())
				return;

			StringBuilder sb = new StringBuilder();
			HtmlTextWriter w = new HtmlTextWriter(new StringWriter(sb));


			if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
			{
				if (CurrentThread.ParentPhoto.Event != null)
				{
					CurrentThread.ParentPhoto.Event.AppendFriendlyHtml(sb, true, true, true, false, false);
					PanelThreadSubjectSubHeadP.Visible = true;
					PanelThreadSubjectSubHeadP.InnerHtml = "<small>" + CurrentThread.ParentPhoto.Event.FriendlyDate(true) + "</small>";
				}
				else if (CurrentThread.ParentPhoto.Article != null)
				{
					w.AddAttribute("href", CurrentThread.ParentPhoto.Article.Url());
					w.RenderBeginTag("a");
					w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentPhoto.Article.Title));
					w.RenderEndTag();//a
				}

				PanelThreadSubjectPhotoP.Visible = true;
				ThreadSubjectPhotoImg.Src = CurrentThread.ParentPhoto.WebPath;
				ThreadSubjectPhotoImg.Width = CurrentThread.ParentPhoto.WebWidth;
				ThreadSubjectPhotoImg.Height = CurrentThread.ParentPhoto.WebHeight;
				ThreadSubjectPhotoAnchor.HRef = CurrentThread.ParentPhoto.Url();

				BuildUsrPhotoMeList(CurrentThread.ParentPhoto);
				CurrentThread.ParentPhoto.IncrementViews();
				CurrentThread.ParentPhoto.Update();
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Event))
			{
				CurrentThread.ParentEvent.AppendFriendlyHtml(sb, true, true, true, false, false);
				PanelThreadSubjectSubHeadP.Visible = true;
				PanelThreadSubjectSubHeadP.InnerHtml = "<small>" + CurrentThread.ParentEvent.FriendlyDate(true) + "</small>";
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
			{
				CurrentThread.ParentVenue.AppendFriendlyHtml(sb, true, false);
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Place))
			{
				w.AddAttribute("href", CurrentThread.ParentPlace.Url());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentPlace.FriendlyName));
				w.RenderEndTag();//a
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Country))
			{
				w.AddAttribute("href", CurrentThread.ParentCountry.Url());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentCountry.FriendlyName));
				w.RenderEndTag();//a
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Article))
			{
				if (CurrentThread.ParentArticle.IsExtendedDisplay)
				{
					//All this stuff is set in the Init function
				}
				else
				{
					PanelThreadSubjectArticleP.Visible = true;
					PanelThreadSubjectArticleP.InnerText = CurrentThread.ParentArticle.Summary;
					PanelThreadSubjectArticleMoreP.Visible = true;
					PanelThreadSubjectArticleMoreP.InnerHtml = "<a href=\"" + CurrentThread.ParentArticle.Url() + "\">Read the whole article</a>";

					w.AddAttribute("href", CurrentThread.ParentArticle.Url());
					w.RenderBeginTag("a");
					w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentArticle.Title));
					w.RenderEndTag();//a
				}
			}
			else if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Group))
			{
				w.AddAttribute("href", CurrentThread.ParentGroup.Url());
				w.RenderBeginTag("a");
				w.Write(HttpUtility.HtmlEncode(CurrentThread.ParentGroup.FriendlyName));
				w.RenderEndTag();//a
			}
			else
			{
				PanelThreadSubject.Visible = false;
			}
			PanelThreadSubjectHeadP.InnerHtml = sb.ToString();
		}
		#endregion
		#region BuildUsrPhotoMeList()
		void BuildUsrPhotoMeList(Photo currentPhoto)
		{
			if (currentPhoto.UsrCount == 0)
			{
				PanelThreadSubjectPhotoMePanel.Visible = false;
			}
			else
			{
				PanelThreadSubjectPhotoMePanel.Visible = true;
				PanelThreadSubjectPhotoMePh.Controls.Clear();
				PanelThreadSubjectPhotoMePh.Controls.Add(new LiteralControl(currentPhoto.UsrHtml));
			}
		}
		#endregion
		#endregion

		#region ThreadComments
		#region ThreadComments_Load
		public void ThreadComments_Load(object o, System.EventArgs e)
		{
			if (!CurrentThreadCheck())
				return;

			ThreadDetailBackLink1.HRef = CommentsBackUrl;
			ThreadDetailBackLink2.HRef = CommentsBackUrl;
			ThreadDetailBackLinkLabel1.Text = CommentsBackText;
			ThreadDetailBackLinkLabel2.Text = CommentsBackText;

			BindComments();

		}
		#endregion
		#region BindComments()
		void BindComments()
		{
			if (CommentPage > CurrentThread.LastPage)
				CommentPage = CurrentThread.LastPage;

			CommentSet Comments = CurrentThread.GetPagedCommentSet(CurrentThread.CommentsQ, CommentPage);
			
			ThreadSubject2.Text = CurrentThread.Subject;

			if (CurrentThread.TotalComments == 1)
				CommentsSubjectH1.InnerText = "Comment";
			else
				CommentsSubjectH1.InnerText = "Comments";

			if (CurrentThread.IsReview)
			{
				InitialCommentH1.InnerText = "Review";
				CommentsSubjectH1.InnerText = "Review";
			}
			else if (CurrentThread.IsNews)
			{
				InitialCommentH1.InnerText = "News";
				CommentsSubjectH1.InnerText = "News";
			}

			SubjectPanel1.Visible = true;
			SubjectPanel2.Visible = true;

			if (CommentPage > 1)
			{
				InitialCommentPanel.Visible = true;
				CommentSet csInitial = CurrentThread.GetPagedCommentSet(new Q(Comment.Columns.K, CurrentThread.FirstComment.K), 1);
				InitialCommentDataList.DataSource = csInitial;
				InitialCommentDataList.ItemTemplate = this.LoadTemplate("/Templates/Comments/Default.ascx");
				InitialCommentDataList.DataBind();
				ThreadSubject1.Text = CurrentThread.Subject;
				CommentsSubjectH1.InnerText = "Replies";
				SubjectPanel2.Visible = false;
			}
			else
				InitialCommentPanel.Visible = false;


			if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
			{
				SubjectPanel1.Visible = false;
				SubjectPanel2.Visible = false;
			}

			if (CommentPage==1 && CurrentThread.LastPage==1)
			{
				CommentsPageTopHolder.Visible = false;
				CommentsPageBottomHolder.Visible = false;
				CommentsPageP1.Visible = false;
				CommentsPageP2.Visible = false;
			}
			else
			{
				CommentsPageP1.Visible = true;
				CommentsPageP2.Visible = true;

				object prevPage = null;
				if (CommentPage > 2)
					prevPage = CommentPage - 1;

				CommentsNextPageLink1.Enabled = CommentPage < CurrentThread.LastPage;
				CommentsNextPageLink1.NavigateUrl = ContainerPage.Url.CurrentUrl("c", ((int)(CommentPage + 1)).ToString()) + "#Comments";
				CommentsPrevPageLink1.Enabled = CommentPage > 1;
				CommentsPrevPageLink1.NavigateUrl = ContainerPage.Url.CurrentUrl("c", prevPage) + "#Comments";

				CommentsNextPageLink2.Enabled = CommentPage < CurrentThread.LastPage;
				CommentsNextPageLink2.NavigateUrl = ContainerPage.Url.CurrentUrl("c", ((int)(CommentPage + 1)).ToString()) + "#Comments";
				CommentsPrevPageLink2.Enabled = CommentPage > 1;
				CommentsPrevPageLink2.NavigateUrl = ContainerPage.Url.CurrentUrl("c", prevPage) + "#Comments";

				if (!CommentsNextPageLink1.Enabled)
					CommentsNextPageLink1.CssClass = "DisabledAnchor";
				if (!CommentsNextPageLink2.Enabled)
					CommentsNextPageLink2.CssClass = "DisabledAnchor";
				if (!CommentsPrevPageLink1.Enabled)
					CommentsPrevPageLink1.CssClass = "DisabledAnchor";
				if (!CommentsPrevPageLink2.Enabled)
					CommentsPrevPageLink2.CssClass = "DisabledAnchor";

				CurrentThreadUsr = null;
				doneFirstUnreadPage = false;
				doneViewComments = false;
				int endLinks = 3;
				int midLinks = 4;
				int midLinksUnread = 2;
				PageLinkWriter p = new PageLinkWriter();
				p.LastPage = CurrentThread.LastPage;
				p.CurrentPageForLinks = CommentPage;
				p.Zones.Add(new PageLinkWriter.Zone(1, endLinks));
				p.Zones.Add(new PageLinkWriter.Zone(CurrentThread.LastPage - endLinks + 1, CurrentThread.LastPage));
				if (FirstUnreadPage > 0)
				{
					p.Zones.Add(new PageLinkWriter.Zone(FirstUnreadPage - midLinksUnread, FirstUnreadPage + midLinksUnread - 1));
				}
				p.Zones.Add(new PageLinkWriter.Zone(CommentPage - midLinks, CommentPage + midLinks));
				StringBuilder sb = new StringBuilder();
				sb.Append("Pages: ");
				p.Build(new PageLinkWriter.LinkWriter(PageLinkWriter), new PageLinkWriter.SeperatorWriter(PageSeperatorWriter), sb);
				CommentsPageP1.Controls.Clear();
				CommentsPageP2.Controls.Clear();
				CommentsPageP1.Controls.Add(new LiteralControl(sb.ToString()));
				CommentsPageP2.Controls.Add(new LiteralControl(sb.ToString()));
			}

			if (CommentPage * Vars.CommentsPerPage > CurrentThread.TotalComments)
				CurrentThread.SetThreadUsr(CurrentThread.TotalComments);
			else
				CurrentThread.SetThreadUsr(CommentPage * Vars.CommentsPerPage);

			CommentsDataList.DataSource = Comments;
			CommentsDataList.ItemTemplate = this.LoadTemplate("/Templates/Comments/Default.ascx");
			CommentsDataList.DataBind();

		}
		#endregion
		#region FirstUnreadPage
		int FirstUnreadPage
		{
			get
			{
				if (!doneFirstUnreadPage)
				{
					firstUnreadPage = 0;
					if (CurrentThreadUsr != null && CurrentThreadUsr.ViewCommentsInUse > 0)
					{
						firstUnreadPage = (CurrentThreadUsr.ViewCommentsInUse / Vars.CommentsPerPage) + 1;
					}
					doneFirstUnreadPage = true;
				}
				return firstUnreadPage;
			}
		}
		int firstUnreadPage;
		bool doneFirstUnreadPage = false;
		#endregion
		#region ViewComments
		int ViewComments
		{
			get
			{
				if (!doneViewComments)
				{
					viewComments = 0;
					if (CurrentThreadUsr != null && CurrentThreadUsr.ViewCommentsInUse > 0)
					{
						viewComments = CurrentThreadUsr.ViewCommentsInUse;
					}
					doneViewComments = true;
				}
				return viewComments;
			}
		}
		int viewComments;
		bool doneViewComments = false;
		#endregion
		#region PageSeperatorWriter
		public void PageSeperatorWriter(int PreviousPage, int NextPage, StringBuilder Builder)
		{
			bool hilight = FirstUnreadPage > 0 && FirstUnreadPage <= PreviousPage && ViewComments < CurrentThread.TotalComments;
			if (hilight)
				Builder.Append(" <span class=\"Unread\">...</span> ");
			else
				Builder.Append(" ... ");
		}
		#endregion
		#region PageLinkWriter
		public void PageLinkWriter(int Page, int CurrentPage, StringBuilder Builder)
		{
			bool hilight = FirstUnreadPage > 0 && FirstUnreadPage <= Page && ViewComments < CurrentThread.TotalComments;
			if (CurrentPage == Page)
			{
				if (hilight)
					Builder.Append("<span class=\"CurrentPageUnread\">");
				else
					Builder.Append("<span class=\"CurrentPage\">");
			}
			else
			{
				Builder.Append("<a ");
				if (hilight)
					Builder.Append("class=\"Unread\" ");
				Builder.Append("href=\"");
				if (Page > 1)
					Builder.Append(ContainerPage.Url.CurrentUrl("c", Page) + "#Comments");
				else
					Builder.Append(ContainerPage.Url.CurrentUrl("c", null) + "#Comments");
				Builder.Append("\">");
			}
			Builder.Append(Page.ToString());
			if (CurrentPage == Page)
			{
				Builder.Append("</span>");
			}
			else
			{
				Builder.Append("</a>");

			}
			Builder.Append(" ");
		}
		#endregion
		#endregion

		#region ThreadReply
		#region ThreadReply_Load
		public void ThreadReply_Load(object o, System.EventArgs e)
		{
			if (!CurrentThreadCheck())
				return;

			#region Init AddComment/AddThread post boxes for users not logged in etc.
			//if (Usr.Current == null)
			//{
			//    AddCommentLoginPanel.Visible = true;
			//    AddCommentEmailVerifyPanel.Visible = false;
			//    AddCommentGroupMemberPanel.Visible = false;

			//    AddCommentHtml.Enabled = false;
			//    AddCommentHtml.Text = "You can't post until you are logged in!";
			//    PanelAddCommentClosed.Visible = false;
			//    return;
			//}
			//else if (!Usr.Current.IsEmailVerified)
			//{
			//    AddCommentLoginPanel.Visible = false;
			//    AddCommentEmailVerifyPanel.Visible = true;
			//    AddCommentGroupMemberPanel.Visible = false;

			//    AddCommentHtml.Enabled = false;
			//    AddCommentHtml.Text = "You can't post until your email address has been verified!";
			//    PanelAddCommentClosed.Visible = false;
			//    return;
			//}
			//else 
			if (CurrentThread.GroupK > 0)
			{
				GroupUsr gu = Usr.Current == null ? null : CurrentThread.Group.GetGroupUsr(Usr.Current);
				if (!CurrentThread.Group.IsMember(gu))
				{
					AddCommentLoginPanel.Visible = false;
					AddCommentEmailVerifyPanel.Visible = false;
					AddCommentGroupMemberPanel.Visible = true;
					AddCommentGroupMemberAnchor.HRef = CurrentThread.Group.UrlApp("join", "type", "6", "k", CurrentThread.K.ToString());

					AddCommentHtml.Enabled = false;
					AddCommentHtml.Text = "You can't post until you're a member of this group!";
					PanelAddCommentClosed.Visible = false;
					return;

				}
			}
			#endregion

			AddCommentLoginPanel.Visible = false;
			AddCommentEmailVerifyPanel.Visible = false;
			AddCommentGroupMemberPanel.Visible = false;
			AddCommentHtml.Enabled = true;

			bool showThreadOpen = !CurrentThread.Closed;
			PanelAddComment.Visible = showThreadOpen;
			PanelAddCommentClosed.Visible = !showThreadOpen;
		}
		#endregion
		#region ThreadReply_Click
		public void ThreadReply_Click(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				Usr.KickUserIfNotLoggedIn();

				Comment.Maker m = CurrentThread.GetCommentMaker();
				m.DuplicateGuid = ContainerPage.ViewStatePublic["ReplyDuplicateGuid"];
				m.Body = AddCommentHtml.GetHtml();
				m.InviteKs = new List<int>(uiMultiBuddyChooser.SelectedUsrKs);
				m.PostingUsr = Usr.Current;
				m.CurrentThreadUsr = CurrentThreadUsr;
				if (CurrentThread.GroupK > 0)
					m.CurrentGroupUsr = CurrentThread.Group.GetGroupUsr(Usr.Current);

				Transaction t = null;//new Transaction();
				Comment.MakerReturn r = null;
				try
				{
					r = m.Post(t);

					//if (r.Success || r.Duplicate)
					//	Response.Redirect(r.Comment.Url());
					if (!r.Success && !r.Duplicate)
						throw new Exception(r.MessageHtml);

					if (r.Comment.Page != CommentPage)
					{
						Response.Redirect(ContainerPage.Url.CurrentUrl("c", r.Comment.Page.ToString()) + "#CommentK-" + r.Comment.K);
						return;
					}
					AddCommentHtml.Text = "";
					BindComments();
					BindButtons();
					Page.DataBind();
					ContainerPage.AnchorSkip("CommentK-" + r.Comment.K);

				}
				catch (Exception ex)
				{
					//t.Rollback();
					throw ex;
				}
				finally
				{
					//t.Close();
				}


			}
		}
		#endregion
		#endregion

		#region ThreadInvite
		#region ThreadInvite_Load
		public void ThreadInvite_Load(object o, System.EventArgs e)
		{
			if (!CurrentThreadCheck())
				return;

			bool showThreadInvite = !CurrentThread.Sealed || (Usr.Current != null && (Usr.Current.K == CurrentThread.UsrK || Usr.Current.IsAdmin));

			if (CurrentThread.CheckPermissionPost(Usr.Current) && showThreadInvite)
			{
				PanelInviteBuddies.Visible = true;
				PanelInviteBuddiesSealed.Visible = false;
			}
			else
			{
				PanelInviteBuddies.Visible = false;
				PanelInviteBuddiesSealed.Visible = !showThreadInvite;
			}


		}
		#endregion
		#region ThreadInvite_Click
		public void ThreadInvite_Click(object o, System.EventArgs e)
		{
			var usrKs = new List<int>(this.uiMultiBuddyChooser.SelectedUsrKs);
			if (usrKs.Any())
			{
			
				Usr currentUsr = Usr.Current;
				Utilities.GetSafeThread(() => DoInvites(usrKs, currentUsr)).Start();
				

				uiMultiBuddyChooser.Clear();
				BindParticipants();
				//ContainerPage.AnchorSkip("InviteBuddies");
				PanelInviteDoneP.Visible = true;
				PanelInviteDoneP.InnerHtml = "<script>alert('" + usrKs.Count.ToString("#,##0") + " " + (usrKs.Count == 1 ? "person" : "people") + " invited');</script>";
			}
			else
			{
				PanelInviteErrorP.Visible = true;
				ContainerPage.AnchorSkip("InviteBuddies");
			}
		}
		public void DoInvites(List<int> usrKs, Usr currentUsr)
		{
			try
			{
				CurrentThread.Invite(
					usrKs,
					currentUsr,
					DateTime.Now,
					new List<int>(),
					false,
					null,
					true);
			}
			catch (Exception ex) { Bobs.Global.Log("36bcfd0e-f9ec-4e91-85eb-df36fac2ffc4 Usr.Current.K=" + currentUsr.K + " ThreadK=" + CurrentThread.K, ex); }

			try
			{
				UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob(CurrentThread);
				job.ExecuteSynchronously();
			}
			catch (Exception ex) { Bobs.Global.Log("6165c2ba-d60f-4850-871f-002befecb0c6", ex); }
		}
		#endregion
		#endregion

		#region CommentsBackText
		string CommentsBackText
		{
			get
			{
				if (ContainerPage.Url["i"].IsInt)
					return "Back to my inbox";
				else if (ContainerPage.Url["f"].IsInt)
					return "Back to my favourites";
				else if (ContainerPage.Url["a"].IsInt)
					return "Back to watching topics";
				else if (ContainerPage.Url["m"].IsInt)
					return "Back to my comments";
				else
					return "Back to topics list";
			}
		}
		#endregion
		#region CommentsBackUrl
		public string CommentsBackUrl
		{
			get
			{
				if (ContainerPage.Url["i"].IsInt)
				{
					object backInboxPage = null;
					if (ContainerPage.Url["i"] > 1)
						backInboxPage = (int)ContainerPage.Url["i"];
					return UrlInfo.PageUrl("inbox", "p", backInboxPage);
				}
				else if (ContainerPage.Url["f"].IsInt)
				{
					object backFavouritesPage = null;
					if (ContainerPage.Url["f"] > 1)
						backFavouritesPage = (int)ContainerPage.Url["f"];
					return UrlInfo.PageUrl("favourites", "p", backFavouritesPage);
				}
				else if (ContainerPage.Url["a"].IsInt)
				{
					object backArchivePage = null;
					if (ContainerPage.Url["a"] > 1)
						backArchivePage = (int)ContainerPage.Url["a"];
					return UrlInfo.PageUrl("watching", "p", backArchivePage);
				}
				else if (ContainerPage.Url["y"].IsInt)
				{
					object backPage = null;
					if (ContainerPage.Url["y"] > 1)
						backPage = (int)ContainerPage.Url["y"];
					string dateFilter = "";
					string date = ContainerPage.Url["d"].ToString();
					if (date.Length == 8)
					{
						DateTime d = new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(6, 2)));
						dateFilter = "/" + d.Year + "/" + d.ToString("MMM").ToLower() + "/" + d.Day.ToString("00");
					}
					else if (date.Length == 6)
					{
						DateTime d = new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(4, 2)), 1);
						dateFilter = "/" + d.Year + "/" + d.ToString("MMM").ToLower();
					}
					else if (date.Length == 4)
					{
						dateFilter = "/" + date;
					}

					return UrlInfo.MakeUrl("members/" + ContainerPage.Url["u"] + dateFilter, "chat", "p", backPage);
				}
				else
				{
					object backThreadPage = null;
					if (ThreadPage > 1)
						backThreadPage = ThreadPage;
					return ContainerPage.Url.CurrentUrl("p", backThreadPage, "c", null, "k", null, "m", null);
				}
			}
		}
		#endregion
		#region CommentPage
		int CommentPage
		{
			get
			{
				if (commentPage == -1)
				{
					if (ContainerPage.Url["C"].IsInt)
						return ContainerPage.Url["C"];
					else
						return 1;
				}
				else
					return commentPage;
			}
			set
			{
				commentPage = value;
			}
		}
		int commentPage = -1;
		#endregion

		#region CurrentThreadK
		int CurrentThreadK
		{
			get
			{
				if (ContainerPage.Url["k"].IsInt)
					return ContainerPage.Url["k"];
				else
					return 0;
			}
		}
		#endregion
		#region CurrentThread
		public Thread CurrentThread
		{
			get
			{
				if (currentThread == null && CurrentThreadK > 0)
					currentThread = new Thread(CurrentThreadK);
				return currentThread;
			}
			set
			{
				currentThread = value;
			}
		}
		Thread currentThread;
		#endregion
		#region CurrentThreadGroupUsr
		public GroupUsr CurrentThreadGroupUsr
		{
			get
			{
				if (!currentThreadGroupUsrDone && CurrentThread.GroupK > 0)
				{
					currentThreadGroupUsr = CurrentThread.Group.GetGroupUsr(Usr.Current);
					currentThreadGroupUsrDone = true;
				}
				return currentThreadGroupUsr;
			}
			set
			{
				currentThreadGroupUsr = value;
			}
		}
		bool currentThreadGroupUsrDone = false;
		GroupUsr currentThreadGroupUsr;
		#endregion
		#region CurrentThreadUsr
		public ThreadUsr CurrentThreadUsr
		{
			get
			{
				if (!doneThreadUsr1 && currentThreadUsr == null && CurrentThread != null && Usr.Current != null)
				{
					if (CurrentThread.CheckPermissionRead(Usr.Current))
					{
						try
						{
							doneThreadUsr1 = true;
							currentThreadUsr = CurrentThread.GetThreadUsr(Usr.Current);
						}
						catch { }
					}
				}
				return currentThreadUsr;
			}
			set
			{
				currentThreadUsr = value;
				doneThreadUsr1 = false;
			}
		}
		private ThreadUsr currentThreadUsr;
		private bool doneThreadUsr1 = false;
		#endregion
		#region CurrentThreadCheck
		bool ViewThread
		{
			get
			{
				return (CurrentThreadK > 0);
			}
		}
		bool CurrentThreadCheck()
		{
			return ViewThread && CurrentThreadCheckPermissionRead;
		}
		bool CurrentThreadCheckPermissionRead
		{
			get
			{
				if (!currentThreadCheckPermissionReadSet)
				{
					currentThreadCheckPermissionRead = CurrentThread.CheckPermissionRead(Usr.Current);
					currentThreadCheckPermissionReadSet = true;
				}
				return currentThreadCheckPermissionRead;
			}
		}
		bool currentThreadCheckPermissionRead;
		bool currentThreadCheckPermissionReadSet = false;
		#endregion

		#region Caption competition

		#region Caption_Load
		protected void Caption_Load(object sender, EventArgs eventArgs)
		{
			if (!Page.IsPostBack)
				this.ViewState["CaptionDuplicateGuid"] = Guid.NewGuid();
		}
		#endregion

		#region Caption_PreRender
		protected void Caption_PreRender(object sender, EventArgs eventArgs)
		{
			if (!CurrentThreadCheck())
				return;

			if (CurrentThread.IsInCaptionCompetition)
			{
				PanelThreadSubjectHeadP.InnerHtml = CurrentThread.Group.Link();

				CaptionButtonP.Visible = Usr.Current != null;
				CaptionLoginPanel.Visible = Usr.Current == null;
				if (Usr.Current == null)
				{
					CaptionTextBox.Enabled = false;
					CaptionTextBox.Text = "Log in first!";
				}

				CaptionEntryPanel.Visible = true;

			}
			else
			{
				CaptionEntryPanel.Visible = false;
			}


		}
		#endregion

		#region Caption_Click
		protected void Caption_Click(object sender, EventArgs eventArgs)
		{
			#region Add a new thread in a group
			Bobs.Group g = new Bobs.Group(Vars.CompetitionGroupK);
			GroupUsr gu = g.GetGroupUsr(Usr.Current);

			if (gu == null || !g.IsMember(gu))
			{
				//join the group
				try
				{
					g.Join(Usr.Current, gu);
				}
				catch
				{
					CaptionErrorP.Visible = true;
					CaptionErrorP.InnerHtml = "Sorry, you can't post a caption here. Maybe you're not a member of the <a href=\"" + g.Url() + "\">Bacardi B Live</a> group?";
					return;
				}
			}

			string captionStart = "";// "<img src=\"http://www.dontstayin.com/gfx/caption-start.gif\" width=\"26\" height=\"20\" /><span style=\"font-size: 20px;\"> ";
			string captionEnd = "";// " </span><img src=\"http://www.dontstayin.com/gfx/caption-end.gif\" width=\"26\" height=\"20\" />";

			string caption = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(CaptionTextBox.Text.Trim());

			if (caption.Trim().Length == 0)
			{
				CaptionErrorP.Visible = true;
				CaptionErrorP.InnerHtml = "Please enter a caption. Remember you can't enter HTML tags in your caption.";
				return;
			}

			if (caption.StartsWith("\"") || caption.StartsWith("“"))
				caption = captionStart + caption.Substring(1);
			else
				caption = captionStart + caption;

			if (caption.EndsWith("\"") || caption.EndsWith("”"))
				caption = caption.Substring(0, caption.Length - 1) + captionEnd;
			else
				caption = caption + captionEnd;

			//add comment to current thread
			Comment.Maker m = CurrentThread.GetCommentMaker();
			m.PostingUsr = Usr.Current;
			m.Body = caption;
			m.DuplicateGuid = this.ViewState["CaptionDuplicateGuid"];
			if (CurrentThread.GroupK > 0)
				m.CurrentGroupUsr = CurrentThread.Group.GetGroupUsr(Usr.Current);
			m.CurrentThreadUsr = CurrentThreadUsr;
			Comment.MakerReturn r = m.Post(null);

			if (r.Success || r.Duplicate)
			{
				if (!r.Duplicate)
					Log.Increment(Log.Items.CaptionsAdded);

				Response.Redirect(r.Comment.Url());
			}
			else
			{
				CaptionErrorP.Visible = true;
				CaptionErrorP.InnerText = "Sorry, you can't post a caption here. Maybe you've been banned from the Bacardi B Live group?";
				return;
			}
			#endregion
		}
		#endregion

		#endregion

		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelForum.Visible = p.Equals(PanelForum);
			PanelForumPrivate.Visible = p.Equals(PanelForumPrivate);
			PanelThread.Visible = p.Equals(PanelThread);
			PanelThreadDiasbled.Visible = p.Equals(PanelThreadDiasbled);
			PanelPrivateThread.Visible = p.Equals(PanelPrivateThread);
			PanelGroupPrivateCanJoin.Visible = p.Equals(PanelGroupPrivateCanJoin);
			PanelGroupPrivateRecommend.Visible = p.Equals(PanelGroupPrivateRecommend);
			PanelGroupPrivateRecommendRejected.Visible = p.Equals(PanelGroupPrivateRecommendRejected);
			PanelGroupPrivateWaiting.Visible = p.Equals(PanelGroupPrivateWaiting);
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
			this.PreRender += new System.EventHandler(this.PanelThread_PreRender);
			this.Init += new System.EventHandler(this.ThreadSubject_Init);

			this.Load += new System.EventHandler(this.PanelForum_Load);
			this.Load += new System.EventHandler(this.ForumInfo_Load);
			this.Load += new System.EventHandler(this.ForumThreads_PreRender);

			this.Load += new System.EventHandler(this.PanelThread_Load);
			this.Load += new System.EventHandler(this.ThreadInfo_Load);
			this.Load += new System.EventHandler(this.ThreadParticipants_Load);
			this.Load += new System.EventHandler(this.ThreadSubject_Load);
			
			this.Load += new System.EventHandler(this.ThreadComments_Load);
			this.Load += new System.EventHandler(this.ThreadReply_Load);
			this.Load += new System.EventHandler(this.ThreadInvite_Load);

			this.Load += new EventHandler(this.Caption_Load);
			this.PreRender += new EventHandler(this.Caption_PreRender);
		}
		#endregion
	}
}
