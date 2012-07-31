using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class Watching : DsiUserControl
	{
        private string WatchAll = "";
        private string FavouriteAll = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page");
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Watching))
					ChangePanel(PanelWatching);
				
				CaptureUrlParameters();
			}
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}

		#region PanelArchive
		protected Panel PanelWatching;
		protected Panel ThreadsPanel, NoThreadsPanel;
		protected HtmlGenericControl ThreadsPageP, ThreadsPageP1;
		protected HyperLink ThreadsNextPageLink, ThreadsNextPageLink1,
			ThreadsPrevPageLink, ThreadsPrevPageLink1;
		protected DataGrid ThreadsDataGrid;

		public string GetThreadUrl(Thread t, object[] par)
		{
			return UrlInfo.MakeUrl("", "chat", par);
		}
		private void PanelInbox_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Watching))
			{
				BindThreads();
			}
		}
		public void ArchiveClick(object o, System.EventArgs e)
		{

		}
		public void IgnoreClick(object o, System.EventArgs e)
		{

		}
		#region BindThreads()
		void BindThreads()
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

				//Threads.Reset();
				//List<Thread> threads = new List<Thread>();
				//for (int i = 0; i < Threads.Count && i < Bobs.Vars.ThreadsPerPage; i++)
				//{
				//    threads.Add(Threads[i]);
				//}
				//WatchAll = Thread.WatchingAllHtml(threads, "");
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
						Thread.Columns.UsrK,
						Thread.Columns.LastPost,
						Thread.Columns.TotalComments,
						Thread.Columns.TotalParticipants,
						Thread.Columns.TotalWatching,
						Thread.Columns.IsNews,
						Thread.Columns.IsReview,
						Thread.Columns.ParentObjectType,
						Thread.Columns.ParentObjectK,
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

					q.OrderBy = Thread.Order;

					q.TableElement = new TableElement(TablesEnum.Thread);

					q.TableElement = new Join(
						q.TableElement,
						new TableElement(TablesEnum.ThreadUsr),
						QueryJoinType.Inner,
						new And(
							new Q(Thread.Columns.K, ThreadUsr.Columns.ThreadK, true),
							new Q(ThreadUsr.Columns.UsrK, Usr.Current.K),
							ThreadUsr.WatchingQ
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


        public void ThreadsDataGrid_ItemDataBound(object o, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (e.Item.FindControl("WatchingAllLabel") != null)
                {
                    ((Label)e.Item.FindControl("WatchingAllLabel")).Text = WatchAll;
                }
                if (e.Item.FindControl("FavouriteAllLabel") != null)
                {
                    ((Label)e.Item.FindControl("FavouriteAllLabel")).Text = FavouriteAll;
                }
            }
        }
		#endregion

		#region Filters
		public int FilterInvitingUsrK { get; set; }

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

		public int FilterStatusChangeObjectK { get; set; }

		public bool FilterGroupNews { get; set; }

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

		private void CaptureUrlParameters()
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
				}
			}
			catch
			{ }
		}
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("xxx"))
					return Modes.XXX;
				else
					return Modes.Watching;
			}
		}
		public enum Modes
		{
			Watching,
			XXX
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelWatching.Visible = p.Equals(PanelWatching);
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
			this.Load += new System.EventHandler(this.PanelInbox_Load);
		}
		#endregion
	}
}
