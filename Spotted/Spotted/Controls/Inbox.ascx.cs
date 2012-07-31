using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Text;

namespace Spotted.Controls
{
	public partial class Inbox : EnhancedUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		private string WatchAll = "";
		private string InboxAll = "";
		private string FavouriteAll = "";

		public void ArchiveClick(object o, System.EventArgs e)
		{

		}
		public void IgnoreClick(object o, System.EventArgs e)
		{

		}

		protected Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;

			}
		}

		#region BindThreads()
		public void BindThreads()
		{
			if (ThreadPage != Threads.Paging.ReturnedPage)
				ThreadPage = Threads.Paging.ReturnedPage;

			if (Threads.Count == 0)
			{
				ThreadsPanel.Visible = false;
				NoThreadsPanel.Visible = true;
			}
			else
			{
				ThreadsPanel.Visible = true;
				NoThreadsPanel.Visible = false;

				if (Threads.Paging.ShowNoLinks)
				{
					ThreadsPageP.Visible = false;
					ThreadsPageP1.Visible = false;
					ThreadsPageLinksP.Visible = false;
					ThreadsPageLinksP1.Visible = false;
				}
				else
				{
					ThreadsPageP.Visible = true;
					ThreadsPageP1.Visible = true;

					string urlNextPage = GetPageUrl(ThreadPage + 1);
					string urlPrevPage = GetPageUrl(ThreadPage - 1);

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

					//					if (false)//show thread pages (must also disable TopRecords restriction)
					//					{
					//						int endLinks = 3;
					//						int midLinks = 4;
					//						PageLinkWriter p = new PageLinkWriter();
					//						p.LastPage = ThreadsPager.LastPage;
					//						p.CurrentPageForLinks = ThreadsPager.NewPage;
					//						p.Zones.Add(new PageLinkWriter.Zone(1,endLinks));
					//						p.Zones.Add(new PageLinkWriter.Zone(ThreadsPager.LastPage-endLinks+1,ThreadsPager.LastPage));
					//						p.Zones.Add(new PageLinkWriter.Zone(ThreadsPager.NewPage-midLinks,ThreadsPager.NewPage+midLinks));
					//						StringBuilder sb = new StringBuilder();
					//						sb.Append("Pages: ");
					//						p.Build(new PageLinkWriter.LinkWriter(PageLinkWriter), new PageLinkWriter.SeperatorWriter(PageSeperatorWriter), sb);
					//						ThreadsPageLinksP.Controls.Clear();
					//						ThreadsPageLinksP1.Controls.Clear();
					//						ThreadsPageLinksP.Controls.Add(new LiteralControl(sb.ToString()));
					//						ThreadsPageLinksP1.Controls.Add(new LiteralControl(sb.ToString()));
					//					}
					//					else
					//					{
					ThreadsPageLinksP.Visible = false;
					ThreadsPageLinksP1.Visible = false;
					//					}
				}

				//Threads.Reset();
				//List<Thread> threads = new List<Thread>();
				//for (int i = 0; i < Threads.Count && i < Bobs.Vars.ThreadsPerPage; i++)
				//{
				//    threads.Add(Threads[i]);
				//}
				//WatchAll = Thread.WatchingAllHtml(threads, "wR");
				//InboxAll = Thread.InboxAllHtml(threads, "");
				//FavouriteAll = Thread.FavouriteAllHtml(threads, "");

                Threads.Reset();
				ThreadsDataGrid.DataSource = Threads;
				ThreadsDataGrid.DataBind();

			}

		}
		#endregion
		#region Threads
		ThreadSet Threads
		{
			get
			{
				if (threads == null)
				{
					Query q = new Query();
					q.Paging.RecordsPerPage = Vars.ThreadsPerPage;
					q.Paging.RequestedPage = ThreadPage;
					q.TopRecords = (ThreadPage * Bobs.Vars.ThreadsPerPage) + 1;

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
						Thread.Columns.LastPostUsrK,
						Thread.Columns.GroupK,
						Thread.Columns.UsrK,
						ThreadUsr.Columns.ThreadK,
						ThreadUsr.Columns.UsrK,
						ThreadUsr.Columns.Status,
						ThreadUsr.Columns.StatusChangeObjectType, 
						ThreadUsr.Columns.Favourite,
						ThreadUsr.Columns.ViewComments,
						ThreadUsr.Columns.ViewCommentsLatest,
						ThreadUsr.Columns.ViewDateTime,
						ThreadUsr.Columns.ViewDateTimeLatest,
						Thread.Columns.LastPostUsrK,
						Thread.Columns.FirstParticipantUsrK
					);

					q.OrderBy = new OrderBy(ThreadUsr.Columns.StatusChangeDateTime, OrderBy.OrderDirection.Descending);
					//	q.OrderBy = new OrderBy(Thread.Columns.LastPost, OrderBy.OrderDirection.Descending);

					q.TableElement = new TableElement(TablesEnum.Thread);

					q.TableElement = new Join(
						q.TableElement,
						new TableElement(TablesEnum.ThreadUsr),
						QueryJoinType.Inner,
						new And(
							new Q(Thread.Columns.K, ThreadUsr.Columns.ThreadK, true),
							new Q(ThreadUsr.Columns.UsrK, Usr.Current.K),
							ThreadUsr.InboxQ
						)
					);

					q.TableElement = new Join(
						q.TableElement,
						new TableElement(TablesEnum.GroupUsr),
						QueryJoinType.Left,
						new And(
						new Q(Thread.Columns.GroupK, GroupUsr.Columns.GroupK, true),
						new Q(GroupUsr.Columns.UsrK, Usr.Current.K),
						new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));

					q.QueryCondition = new And(
						new Or(
							new Q(Thread.Columns.GroupPrivate, false),
							new Q(GroupUsr.Columns.UsrK, Usr.Current.K)),
						new Or(
							new Q(Thread.Columns.PrivateGroup, false),
							new Q(GroupUsr.Columns.UsrK, Usr.Current.K))
						);

					if (FilterInvitingUsrK != 0)
					{
						q.QueryCondition = new And(q.QueryCondition,
												   new Q(ThreadUsr.Columns.InvitingUsrK, FilterInvitingUsrK),
												   new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewInvite));
					}
					if (FilterStatusChangeObjectType != Model.Entities.ObjectType.None)
					{
						q.QueryCondition = new And(q.QueryCondition,
												   new Q(ThreadUsr.Columns.StatusChangeObjectType, Convert.ToInt32(FilterStatusChangeObjectType)));
						if (FilterStatusChangeObjectType == Model.Entities.ObjectType.Usr)
						{
							q.QueryCondition = new And(q.QueryCondition,
												   new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewInvite));
						}
						else
						{
							if (FilterGroupNews)
							{
								q.QueryCondition = new And(q.QueryCondition,
													   new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewGroupNewsAlert));
							}
							else
							{
								q.QueryCondition = new And(q.QueryCondition,
													   new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewWatchedForumAlert));
							}
						}
					}
					if (FilterStatusChangeObjectK != 0)
					{
						q.QueryCondition = new And(q.QueryCondition,
												   new Q(ThreadUsr.Columns.StatusChangeObjectK, FilterStatusChangeObjectK));
					}
					if (FilterStatusEnum != ThreadUsr.StatusEnum.None)
					{
						q.QueryCondition = new And(q.QueryCondition,
												   new Q(ThreadUsr.Columns.Status, FilterStatusEnum));
					}
					//if (FilterBuddyPosts && FilterBuddyUsrK > 0)
					//{
					//    q.TableElement = new Join(q.TableElement,
					//                              new TableElement(TablesEnum.Comment),
					//                              QueryJoinType.Inner,
					//                              new Q(Comment.Columns.UsrK, FilterBuddyUsrK));
					//}
					//if (FilterGroupPosts && FilterGroupK > 0)
					//{
					//    q.QueryCondition = new And(q.QueryCondition,
					//                               new Q(Thread.Columns.GroupK, FilterGroupK));
					//}

					threads = new ThreadSet(q);

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
		#region ThreadPage
		protected int ThreadPage
		{
			get
			{
				if (threadPage == -1)
				{
					if (ContainerPage.Url["P"].IsInt)
						threadPage = ContainerPage.Url["P"];
					else if (ContainerPage.Url["ThreadPage"].IsInt)
						threadPage = ContainerPage.Url["ThreadPage"];
					else
						threadPage = 1;
				}
				return threadPage;
			}
			set
			{
				threadPage = value;
			}
		}
		int threadPage = -1;
		#endregion
		string GetPageUrl(int page)
		{
			if (ContainerPage.Url.PageName.ToLower() == "inbox")
			{
				if (page == 1)
					return ContainerPage.Url.CurrentUrl("p", null) + "#Threads";
				else
					return ContainerPage.Url.CurrentUrl("p", page.ToString()) + "#Threads";
			}
			else
			{
				if (page == 1)
					return UrlInfo.PageUrl("inbox") + "#Threads";
				else
					return UrlInfo.PageUrl("inbox", "p", page.ToString()) + "#Threads";
			}
		}
		public string GetThreadUrl(Thread t, object[] par)
		{
			return UrlInfo.MakeUrl("", "chat", par);
		}

		public void PageSeperatorWriter(int PreviousPage, int NextPage, StringBuilder Builder)
		{
			Builder.Append(" ... ");
		}
		public void PageLinkWriter(int Page, int CurrentPage, StringBuilder Builder)
		{
			if (CurrentPage == Page)
			{
				Builder.Append("<span class=\"CurrentPage\">");
			}
			else
			{
				Builder.Append("<a href=\"");
				Builder.Append(GetPageUrl(Page));
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

        protected void ClearInboxButton_Click(object o, System.EventArgs e)
        {
            if (Usr.Current != null)
            {
				Usr.Current.UpdateInboxThreads(ThreadUsr.StatusEnum.Archived);
                ThreadsPanel.Visible = false;
                NoThreadsPanel.Visible = true;
            }
        }

		protected void RemoveSpamButton_Click(object o, System.EventArgs e)
        {
            if (Usr.Current != null)
            {
				Response.Redirect(UrlInfo.PageUrl("spam"));
            }
        }

        public void ThreadsDataGrid_ItemDataBound(object o, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (e.Item.FindControl("WatchingAllLabel") != null)
                {
                    ((Label)e.Item.FindControl("WatchingAllLabel")).Text = WatchAll;
                }
                if (e.Item.FindControl("InboxAllLabel") != null)
                {
                    ((Label)e.Item.FindControl("InboxAllLabel")).Text = InboxAll;
                }
                if (e.Item.FindControl("FavouriteAllLabel") != null)
                {
                    ((Label)e.Item.FindControl("FavouriteAllLabel")).Text = FavouriteAll;
                }
            }
        }

		protected void RefreshButton_Click(object o, System.EventArgs e)
		{
			BindThreads();
		}
		

		#region Filters
		public int FilterInvitingUsrK { get; set;}

		public int FilterBuddyUsrK { get; set; }

		public bool FilterBuddyPosts { get; set; }

		public int FilterGroupK { get; set; }

		public bool FilterGroupPosts { get; set; }

		public Model.Entities.ObjectType FilterStatusChangeObjectType
		{
			get
			{
				return filterStatusChangeObjectType;
			}
			set
			{
				filterStatusChangeObjectType = value;
			}
		}
		private Model.Entities.ObjectType filterStatusChangeObjectType = Model.Entities.ObjectType.None;

		public int FilterStatusChangeObjectK { get; set;}

		public bool FilterGroupNews { get; set;}

		public ThreadUsr.StatusEnum FilterStatusEnum
		{
			get
			{
				return filterStatusEnum;
			}
			set
			{
				filterStatusEnum = value;
			}
		}
		private ThreadUsr.StatusEnum filterStatusEnum = ThreadUsr.StatusEnum.None;
		
		public void CaptureUrlParameters()
		{
			try
			{
				if (ContainerPage.Url["InvitingUsrK"].IsInt)
					FilterInvitingUsrK = Convert.ToInt32(ContainerPage.Url["InvitingUsrK"].Value);
				else
				{
					if (ContainerPage.Url["StatusChangeObjectType"].IsInt)
						FilterStatusChangeObjectType = (Model.Entities.ObjectType)Convert.ToInt32(ContainerPage.Url["StatusChangeObjectType"].Value);
					if (ContainerPage.Url["StatusChangeObjectK"].IsInt)
						FilterStatusChangeObjectK = Convert.ToInt32(ContainerPage.Url["StatusChangeObjectK"].Value);
					if (ContainerPage.Url["GroupNews"].Exists)
						FilterGroupNews = Convert.ToBoolean(ContainerPage.Url["GroupNews"].Value);
					if (ContainerPage.Url["StatusEnum"].IsInt)
						FilterStatusEnum = (ThreadUsr.StatusEnum)Convert.ToInt32(ContainerPage.Url["StatusEnum"].Value);

					InboxFilterPanel.Visible = false;

					if (FilterStatusChangeObjectType != Model.Entities.ObjectType.None || FilterStatusChangeObjectK > 0 || ThreadUsr.InboxStatuses.Contains(FilterStatusEnum) || FilterGroupNews)
					{
						InboxFilterPanel.Visible = true;
						string filterText = "<b>We have filtered your inbox to just show you topics ";
						if (FilterStatusChangeObjectType != Model.Entities.ObjectType.None)
						{
							filterText += "from ";
							
							if (FilterStatusChangeObjectK > 0)
							{
								var filterBob = Bob.Get(FilterStatusChangeObjectType, FilterStatusChangeObjectK);
								if (filterBob is IBobType)
									filterText += ((IBobType)filterBob).TypeName.ToLower();
								else
									filterText += FilterStatusChangeObjectType.ToString().ToLower();

								if(FilterGroupNews)
									filterText += " news";
								filterText += ": ";

								if (filterBob is ILinkable)
									filterText += ((ILinkable)filterBob).Link();
								else
									filterText += "#" + FilterStatusChangeObjectK.ToString();
							}
							else
							{
								filterText += FilterStatusChangeObjectType.ToString().ToLower() + "s";
							}
						}
						else if (FilterStatusEnum == ThreadUsr.StatusEnum.UnArchived)
						{
							filterText += "that have been unarchived";
						}

						filterText += "</b><br><br>";
						filterText += Utilities.Link(UrlInfo.PageUrl("spam", null), "Click here to go back to spam page");

						InboxFilterP.InnerHtml = filterText;
					}
				}
			}
			catch
			{ }
		}

		//private void SetupBuddyDropDownList()
		//{
		//    this.BuddyDropDownList.Items.Clear();
		//    if(Usr.Current.BuddiesNamesAndKs != null)
		//    {
		//        this.BuddyDropDownList.Items.Add(new ListItem("<All buddies>", ""));
		//        this.BuddyDropDownList.Items.Add(new ListItem("---------------", ""));
		//        foreach (Usr buddy in Usr.Current.BuddiesNamesAndKs)
		//        {
		//            this.BuddyDropDownList.Items.Add(new ListItem(buddy.NickName, buddy.K.ToString()));
		//        }
		//    }
		//}
		//private void SetupGroupDropDownList()
		//{
		//    this.GroupDropDownList.Items.Clear();
		//    if (Usr.Current.GroupsNamesAndKs != null)
		//    {
		//        this.GroupDropDownList.Items.Add(new ListItem("<All groups>", ""));
		//        this.GroupDropDownList.Items.Add(new ListItem("--------------", ""));
		//        foreach (Group group in Usr.Current.GroupsNamesAndKs)
		//        {
		//            this.GroupDropDownList.Items.Add(new ListItem(group.Name, group.K.ToString()));
		//        }
		//    }
		//}

		#endregion
	}
}
