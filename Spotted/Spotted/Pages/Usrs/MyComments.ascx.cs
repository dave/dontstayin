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

namespace Spotted.Pages.Usrs
{
	public partial class MyComments : UsrUserControl
	{
        private string WatchAll = "";
        private string FavouriteAll = "";

		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);

			
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);

			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Inbox))
					ChangePanel(PanelMyComments);
			}
		}

		#region InfoPanel
		protected Panel MyChatLinksPanel;
		protected Spotted.CustomControls.UsrIntro UsrIntro;
		public void InfoPanel_Load(object o, System.EventArgs e)
		{
			ContainerPage.SetPageTitle(ThisUsr.NickName + "'s comments");
			MyChatLinksPanel.Visible = Usr.Current != null && ThisUsr.K == Usr.Current.K;
			UsrIntro.Visible = Usr.Current == null || ThisUsr.K != Usr.Current.K;
			UsrIntro.Header = ThisUsr.NickName + "'s comments";

		}

		#endregion

		#region Cal
		protected Controls.Cal Cal;
		public void Cal_Load(object o, System.EventArgs e)
		{

			int usrK = 0;
			if (Usr.Current != null)
				usrK = Usr.Current.K;

			TableElement te = new TableElement(TablesEnum.Comment);

			te = new Join(
				te,
				Thread.Columns.K,
				Comment.Columns.ThreadK);

			te = new Join(
				te,
				new TableElement(TablesEnum.ThreadUsr),
				QueryJoinType.Left,
				new And(
				new Q(Thread.Columns.K, ThreadUsr.Columns.ThreadK, true),
				new Q(ThreadUsr.Columns.UsrK, usrK),
				new Q(ThreadUsr.Columns.Status, QueryOperator.NotEqualTo, ThreadUsr.StatusEnum.Deleted)));

			te = new Join(
				te,
				new TableElement(TablesEnum.GroupUsr),
				QueryJoinType.Left,
				new And(
				new Q(Thread.Columns.GroupK, GroupUsr.Columns.GroupK, true),
				new Q(GroupUsr.Columns.UsrK, usrK),
				new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));

			Q q = new And(
				new Q(Comment.Columns.UsrK, ThisUsr.K),
				new Or(
					new Q(Thread.Columns.Private, false),
					new Q(ThreadUsr.Columns.UsrK, usrK)
				),
				new Or(
					new Q(Thread.Columns.GroupPrivate, false),
					new Q(GroupUsr.Columns.UsrK, usrK)
				),
				new Or(
					new Q(Thread.Columns.PrivateGroup, false),
					new Q(GroupUsr.Columns.UsrK, usrK)
				)
			);

			Cal.MonthUrlGetter = new Controls.Cal.MonthUrlDelegate(GetMonthUrl);
			Cal.DayUrlGetter = new Controls.Cal.DayUrlDelegate(GetDayUrl);
			Cal.DateTimeColumn = new Column(Comment.Columns.DateTime);
			Cal.TableElement = te;
			Cal.QueryCondition = q;
		}
		public string GetMonthUrl(DateTime d, params object[] par)
		{
			return ThisUsr.UrlMyCommentsMonth(d);
		}
		public string GetDayUrl(DateTime d, params object[] par)
		{
			return ThisUsr.UrlMyCommentsDate(d);
		}
		#endregion

		#region PanelMyComments
		protected Panel PanelMyComments;
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
			if (Mode.Equals(Modes.Inbox))
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
			if (ThreadPage != Comments.Paging.ReturnedPage)
				ThreadPage = Comments.Paging.ReturnedPage;


			if (Comments.Count == 0)
			{
				ThreadsPanel.Visible = false;
				NoThreadsPanel.Visible = true;
			}
			else
			{
				ThreadsPanel.Visible = true;
				NoThreadsPanel.Visible = false;

				if (Comments.Paging.ShowNoLinks)
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

					ThreadsNextPageLink1.Enabled = Comments.Paging.ShowNextPageLink;
					ThreadsNextPageLink1.NavigateUrl = urlNextPage;
					ThreadsPrevPageLink1.Enabled = Comments.Paging.ShowPrevPageLink;
					ThreadsPrevPageLink1.NavigateUrl = urlPrevPage;

					ThreadsNextPageLink.Enabled = Comments.Paging.ShowNextPageLink;
					ThreadsNextPageLink.NavigateUrl = urlNextPage;
					ThreadsPrevPageLink.Enabled = Comments.Paging.ShowPrevPageLink;
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
				//Comments.Reset();
				//List<Comment> comments = new List<Comment>();
				//for (int i = 0; i < Comments.Count && i < Bobs.Vars.ThreadsPerPage; i++)
				//{
				//    comments.Add(Comments[i]);
				//}
				//WatchAll = Comment.WatchingAllHtml(comments, "");
				//FavouriteAll = Comment.FavouriteAllHtml(comments, "");

                Comments.Reset();
				ThreadsDataGrid.DataSource = Comments;
				ThreadsDataGrid.DataBind();

			}

		}
		#endregion
		#region Comments
		CommentSet Comments
		{
			get
			{
				if (comments == null)
				{
					Query q = new Query();
					q.Paging.RecordsPerPage = Vars.ThreadsPerPage;
					q.Paging.RequestedPage = ThreadPage;
					q.TopRecords = (ThreadPage * Bobs.Vars.ThreadsPerPage) + 1;

					q.Columns = new ColumnSet(
						Comment.Columns.K,
						Comment.Columns.ThreadK,
						Comment.Columns.Text,
						Comment.Columns.DateTime,
						Comment.Columns.IndexInThread,
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
						Thread.Columns.FirstParticipantUsrK
					);

					int usrK = 0;
					if (Usr.Current != null)
						usrK = Usr.Current.K;

					q.TableElement = new TableElement(TablesEnum.Comment);

					q.TableElement = new Join(
						q.TableElement,
						Thread.Columns.K,
						Comment.Columns.ThreadK);

					q.TableElement = new Join(
						q.TableElement,
						new TableElement(TablesEnum.ThreadUsr),
						QueryJoinType.Left,
						new And(
							new Q(Thread.Columns.K, ThreadUsr.Columns.ThreadK, true),
							new Q(ThreadUsr.Columns.UsrK, usrK),
							new Q(ThreadUsr.Columns.Status, QueryOperator.NotEqualTo, ThreadUsr.StatusEnum.Deleted)
						)
					);

					q.TableElement = new Join(
						q.TableElement,
						new TableElement(TablesEnum.GroupUsr),
						QueryJoinType.Left,
						new And(
						new Q(Thread.Columns.GroupK, GroupUsr.Columns.GroupK, true),
						new Q(GroupUsr.Columns.UsrK, usrK),
						new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));


					if (ContainerPage.Url.HasDayFilter ||
						ContainerPage.Url.HasMonthFilter ||
						ContainerPage.Url.HasYearFilter)
						q.OrderBy = new OrderBy(Comment.Columns.DateTime);
					else
						q.OrderBy = new OrderBy(Comment.Columns.DateTime, OrderBy.OrderDirection.Descending);


					Q dateQ = new Q(true);
					if (ContainerPage.Url.HasDayFilter)
						dateQ = new And(
							new Q(Comment.Columns.DateTime, QueryOperator.LessThan, ContainerPage.Url.DateFilter.AddDays(1)),
							new Q(Comment.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, ContainerPage.Url.DateFilter));
					else if (ContainerPage.Url.HasMonthFilter)
						dateQ = new And(
							new Q(Comment.Columns.DateTime, QueryOperator.LessThan, ContainerPage.Url.DateFilter.AddMonths(1)),
							new Q(Comment.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, ContainerPage.Url.DateFilter));
					else if (ContainerPage.Url.HasYearFilter)
						dateQ = new And(
							new Q(Comment.Columns.DateTime, QueryOperator.LessThan, ContainerPage.Url.DateFilter.AddYears(1)),
							new Q(Comment.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, ContainerPage.Url.DateFilter));

					q.QueryCondition = new And(
						dateQ,
						new Q(Comment.Columns.UsrK, ThisUsr.K),
						new Or(
							new Q(Thread.Columns.Private, false),
							new Q(ThreadUsr.Columns.UsrK, usrK)
						),
						new Or(
							new Q(Thread.Columns.GroupPrivate, false),
							new Q(GroupUsr.Columns.UsrK, usrK)
						),
						new Or(
							new Q(Thread.Columns.PrivateGroup, false),
							new Q(GroupUsr.Columns.UsrK, usrK)
						)
					);

					comments = new CommentSet(q);

				}
				return comments;
			}
			set
			{
				comments = value;
			}
		}
		CommentSet comments;
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

		protected string DateString(Comment c)
		{
			if (ContainerPage.Url.HasDayFilter)
				return c.DateTime.ToString("HH:mm:ss");
			else
				return c.DateTime.ToString("HH:mm:ss (dd MMM yy)");
		}
		protected string CommentUrl(Comment c)
		{
			object page = null;
			if (c.Page > 1)
				page = c.Page;
			return Bobs.UrlInfo.MakeUrl(
				"",
				"chat",
				"u", ThisUsr.NickName.ToLower(),
				"d", CurrentDate,
				"y", ThreadPage,
				"k", c.ThreadK.ToString(),
				"c", page) + "#CommentK-" + c.K;
		}

		protected object CurrentDate
		{
			get
			{
				if (ContainerPage.Url.HasDayFilter)
					return ContainerPage.Url.DateFilter.ToString("yyyyMMdd");
				else if (ContainerPage.Url.HasMonthFilter)
					return ContainerPage.Url.DateFilter.ToString("yyyyMM");
				else if (ContainerPage.Url.HasYearFilter)
					return ContainerPage.Url.DateFilter.ToString("yyyy");
				else
					return null;
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
					return Modes.Inbox;
			}
		}
		public enum Modes
		{
			Inbox,
			XXX
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelMyComments.Visible = p.Equals(PanelMyComments);
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
			this.Load += new System.EventHandler(InfoPanel_Load);
			this.Load += new System.EventHandler(this.Cal_Load);
		}
		#endregion
	}
}
