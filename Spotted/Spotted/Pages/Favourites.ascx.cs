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
using System.Text;

namespace Spotted.Pages
{
	public partial class Favourites : DsiUserControl
	{
        private string WatchAll = "";
        private string FavouriteAll = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page");
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Favourites))
					ChangePanel(PanelFavourites);
			}
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}

		#region PanelFavourites
		protected Panel PanelFavourites;
		protected Panel ThreadsPanel, NoThreadsPanel;
		protected HtmlGenericControl ThreadsPageP, ThreadsPageP1;
		protected HyperLink ThreadsNextPageLink, ThreadsNextPageLink1,
			ThreadsPrevPageLink, ThreadsPrevPageLink1;
		protected DataGrid ThreadsDataGrid;
		protected HtmlGenericControl ThreadsPageLinksP, ThreadsPageLinksP1;

		public string GetThreadUrl(Thread t, object[] par)
		{
			return UrlInfo.MakeUrl("", "chat", par);
		}
		private void PanelFavourites_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Favourites))
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
                    //threads.Add(Threads[i]);
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
					q.Paging.RecordsPerPage = Bobs.Vars.ThreadsPerPage;
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
							new Q(ThreadUsr.Columns.Favourite, true)
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
			if (page == 1)
				return ContainerPage.Url.CurrentUrl("p", null);
			else
				return ContainerPage.Url.CurrentUrl("p", page.ToString());
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

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("xxx"))
					return Modes.XXX;
				else
					return Modes.Favourites;
			}
		}
		public enum Modes
		{
			Favourites,
			XXX
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelFavourites.Visible = p.Equals(PanelFavourites);
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
			this.Load += new System.EventHandler(this.PanelFavourites_Load);

		}
		#endregion
	}
}
