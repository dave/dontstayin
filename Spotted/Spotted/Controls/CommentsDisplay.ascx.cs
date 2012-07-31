using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;
using System.Text;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class CommentsDisplay : EnhancedUserControl, Spotted.Templates.Comments.ICommentsPage, IIncludesJs
	{
		public CommentsDisplay()
		{
		}
		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		public Thread CurrentThread { get; set; }
		public IDiscussable ParentObject { get; set; }
		#region CurrentThreadUsr
		public ThreadUsr CurrentThreadUsr
		{
			get
			{
				if (!doneThreadUsr && currentThreadUsr == null && CurrentThread != null && Usr.Current != null)
				{
					if (CurrentThread.CheckPermissionRead(Usr.Current))
					{
						try
						{
							doneThreadUsr = true;
							currentThreadUsr = new ThreadUsr(CurrentThread.K, Usr.Current.K);
						}
						catch
						{
						}
					}
				}
				return currentThreadUsr;
			}
			set
			{
				currentThreadUsr = value;
				doneThreadUsr = false;
			}
		}
		private ThreadUsr currentThreadUsr;
		private bool doneThreadUsr = false;
		#endregion
		#region CurrentThreadGroupUsr
		public GroupUsr CurrentThreadGroupUsr
		{
			get
			{
				if (!currentThreadGroupUsrDone && CurrentThread != null && CurrentThread.GroupK > 0)
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

		public Spotted.Master.DsiPage ContainerPage
		{
			get { return (Spotted.Master.DsiPage)Page; }
		}

		bool HasParentObject { get { return ParentObject != null; } }

		public int CurrentPage
		{
			get
			{
				int pageNumber;
				if (int.TryParse(uiPageNumber.Value, out pageNumber)) return pageNumber;
				else return 1;
			}
			set
			{
				uiPageNumber.Value = value.ToString();
			}
		}

		public string CommentsSubjectH1InnerText
		{
			set { this.CommentsSubjectH1.InnerText = value; }
		}
		#region ThreadComments


		string GetCommentPageUrl(object page)
		{
			if (ParentObject.ObjectType == Model.Entities.ObjectType.Photo)
				return ContainerPage.Url.CurrentUrl("c", page, "photo", ParentObject.K) + "#Comments";
			else
				return ContainerPage.Url.CurrentUrl("c", page) + "#Comments";
		}

		#region FirstUnreadPage
		int FirstUnreadPage
		{
			get
			{
				if (!HasParentObject)
					return 0;

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
				if (!HasParentObject)
					return 0;

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
					Builder.Append(GetCommentPageUrl(Page));
				else
					Builder.Append(GetCommentPageUrl(null));
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

		public override void DataBind()
		{
			if (CurrentThread == null || CurrentThread.TotalComments == 0)
			{
				this.uiCommentsPanel.Style["display"] = "none";
			}
			else
			{
				if (CurrentPage > CurrentThread.LastPage)
					CurrentPage = CurrentThread.LastPage;

				CommentSet Comments = CurrentThread.GetPagedCommentSet(CurrentThread.CommentsQ, CurrentPage);

				this.uiCommentsPanel.Style["display"] = "";
				this.CommentsSubjectH1InnerText = "Comments";

				if (CurrentThread.IsReview)
				{
					InitialCommentH1.InnerText = "Review";
					this.CommentsSubjectH1InnerText = "Review";
				}
				else if (CurrentThread.IsNews)
				{
					InitialCommentH1.InnerText = "News";
					this.CommentsSubjectH1InnerText = "News";
				}

				if (CurrentPage > 1)
				{
					uiInitialCommentPanel.Style["display"] = "";
					CommentSet csInitial = CurrentThread.GetPagedCommentSet(new Q(Comment.Columns.K, CurrentThread.FirstComment.K), 1);
					uiInitialCommentDataList.DataSource = csInitial;
					uiInitialCommentDataList.ItemTemplate = this.LoadTemplate("/Templates/Comments/Default.ascx");
					uiInitialCommentDataList.DataBind();
					this.CommentsSubjectH1InnerText = "Replies";
				}
				else
					uiInitialCommentPanel.Style["display"] = "none";


				if (CurrentPage == 1 && CurrentThread.LastPage == 1)
				{
					CommentsPageP2.Visible = false;
					CommentsPageP1.Visible = false;
				}
				else
				{
					CommentsPageP2.Visible = true;
					CommentsPageP1.Visible = true;

					object prevPage = null;
					if (CurrentPage > 2)
						prevPage = CurrentPage - 1;


					CommentsNextPageLink1.Enabled = CurrentPage < CurrentThread.LastPage;
					CommentsNextPageLink1.NavigateUrl = GetCommentPageUrl(CurrentPage + 1);
					CommentsPrevPageLink1.Enabled = CurrentPage > 1;
					CommentsPrevPageLink1.NavigateUrl = GetCommentPageUrl(prevPage);

					CommentsNextPageLink.Enabled = CurrentPage < CurrentThread.LastPage;
					CommentsNextPageLink.NavigateUrl = GetCommentPageUrl(CurrentPage + 1);
					CommentsPrevPageLink.Enabled = CurrentPage > 1;
					CommentsPrevPageLink.NavigateUrl = GetCommentPageUrl(prevPage);

					CommentsNextPageLink1.CssClass = CommentsNextPageLink1.Enabled ? "" : "DisabledAnchor";
					CommentsNextPageLink.CssClass = CommentsNextPageLink.Enabled ? "" : "DisabledAnchor";
					CommentsPrevPageLink1.CssClass = CommentsPrevPageLink1.Enabled ? "" : "DisabledAnchor";
					CommentsPrevPageLink.CssClass = CommentsPrevPageLink.Enabled ? "" : "DisabledAnchor";

					CurrentThreadUsr = null;
					doneFirstUnreadPage = false;
					doneViewComments = false;
					int endLinks = 3;
					int midLinks = 4;
					int midLinksUread = 2;
					PageLinkWriter p = new PageLinkWriter();
					p.LastPage = CurrentThread.LastPage;
					p.CurrentPageForLinks = CurrentPage;
					p.Zones.Add(new PageLinkWriter.Zone(1, endLinks));
					p.Zones.Add(new PageLinkWriter.Zone(CurrentThread.LastPage - endLinks + 1, CurrentThread.LastPage));
					if (FirstUnreadPage > 0)
					{
						p.Zones.Add(new PageLinkWriter.Zone(FirstUnreadPage - midLinksUread, FirstUnreadPage + midLinksUread - 1));
					}
					p.Zones.Add(new PageLinkWriter.Zone(CurrentPage - midLinks, CurrentPage + midLinks));
					StringBuilder sb = new StringBuilder();
					sb.Append("Pages: ");
					p.Build(new PageLinkWriter.LinkWriter(PageLinkWriter), new PageLinkWriter.SeperatorWriter(PageSeperatorWriter), sb);
					CommentsPagesP1.Controls.Clear();
					CommentsPagesP2.Controls.Clear();
					CommentsPagesP1.Controls.Add(new LiteralControl(sb.ToString()));
					CommentsPagesP2.Controls.Add(new LiteralControl(sb.ToString()));
				}

				if (CurrentPage*Vars.CommentsPerPage > CurrentThread.TotalComments)
					CurrentThread.SetThreadUsr(CurrentThread.TotalComments);
				else
					CurrentThread.SetThreadUsr(CurrentPage*Vars.CommentsPerPage);

				CommentsDataList.DataSource = Comments;
				CommentsDataList.ItemTemplate = this.LoadTemplate("/Templates/Comments/Default.ascx");
				CommentsDataList.DataBind();
			}
		}

		internal void HideInitialCommentPanel()
		{
			this.uiInitialCommentPanel.Style["display"] = "none";
		}

		public void Page_PreRender(object sender, EventArgs e)
		{
			this.uiCommentsPerPage.Value = Vars.CommentsPerPage.ToString();
			this.uiClientID.Value = this.ClientID;
			this.uiUsrIsLoggedIn.Value = (Usr.Current != null).ToString();
		}

		internal void HideCommentsPanel()
		{
			this.uiCommentsPanel.Style["display"] = "none";
		}
	}
}
