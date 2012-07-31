using Sys;
using Sys.UI;
using Sys.Net;
using System;
using System.DHTML;
using SpottedScript.Controls;
using Spotted.WebServices.Controls.CommentsDisplay;
using Utils;
using BannersGeneratorController = SpottedScript.Controls.Banners.Generator.Controller;

namespace SpottedScript.Controls.CommentsDisplay
{
	class ThreadCommentsProvider
	{
		int commentsPerPage;
		int threadK;
		internal int ThreadK
		{
			get { return threadK; }
			set { threadK = value; }
		}

		int pageNumber;
		internal int PageNumber
		{
			get { return pageNumber; }
			set { pageNumber = value; }
		}

		Array initialComments;
		internal CommentStub InitialComment
		{
			get { return (CommentStub)initialComments[threadK]; }
			private set { initialComments[threadK] = value; }
		}
		Array comments;
		internal CommentStub[] Comments
		{
			get { return getCommentsByThreadAndPage(threadK, pageNumber); }
			private set
			{
				if (comments[threadK] == null)
				{
					comments[threadK] = new Array();
				}
				((Array)comments[threadK])[pageNumber] = value;
			}
		}
		CommentStub[] getCommentsByThreadAndPage(int threadK, int pageNumber)
		{
			if (comments[threadK] == null) return null;
			if (((Array)comments[threadK])[pageNumber] == null) return null;
			return (CommentStub[])((Array)comments[threadK])[pageNumber];
		}

		Array lastPage;
		internal int LastPage
		{
			get { return lastPage[threadK] != null ? (int)lastPage[threadK] : 0; }
			private set { lastPage[threadK] = value; }
		}
		Array lastKnownCommentK;
		internal int LastKnownCommentK
		{
			get { return lastKnownCommentK[threadK] != null ? (int)lastKnownCommentK[threadK] : 0; }
			private set { lastKnownCommentK[threadK] = value; }
		}
		Array firstUnreadPage;
		internal int FirstUnreadPage
		{
			get { return firstUnreadPage[threadK] != null ? (int)firstUnreadPage[threadK] : 0; }
			private set { firstUnreadPage[threadK] = value; }
		}
		Array viewComments;
		internal int ViewComments
		{
			get { return viewComments[threadK] != null ? (int)viewComments[threadK] : 0; }
			private set { viewComments[threadK] = value; }
		}
		Array totalComments;
		internal int TotalComments
		{
			get { return totalComments[threadK] != null ? (int)totalComments[threadK] : 0; }
			private set { totalComments[threadK] = value; }
		}

		public EventHandler OnLoaded;
		public EventHandler OnThreadCreated;

		public ThreadCommentsProvider(int commentsPerPage)
		{
			this.commentsPerPage = commentsPerPage;
			this.comments = new Array();
			this.initialComments = new Array();
			this.lastPage = new Array();
			this.firstUnreadPage = new Array();
			this.viewComments = new Array();
			this.totalComments = new Array();
			this.lastKnownCommentK = new Array();
		}


		#region append comments
		internal void appendComment(CommentStub newComment)
		{
			if (this.PageNumber != this.LastPage)
			{
				this.PageNumber = this.LastPage;
			}

			if (this.threadK == 0)
			{
				this.threadK = newComment.threadK;
			}

			if (this.Comments == null)
			{
				this.Comments = new CommentStub[1] { newComment };
			}
			else if (this.Comments.Length == this.commentsPerPage)
			{
				this.pageNumber++;
				this.LastPage++;
				this.Comments = new CommentStub[1] { newComment };
			}
			else
			{
				// this is fine cos it's a js array!
				this.Comments[this.Comments.Length] = newComment;
			}
		}
		internal void appendComments(CommentStub[] newComments)
		{
			for (int i = 0; i < newComments.Length; i++)
			{
				appendComment(newComments[i]);
			}
		}
		#endregion

		internal void LoadThreadComments()
		{
			if (this.threadK > 0)
			{
				if (Comments == null)
				{
					loadThreadCommentsViaWebRequest();
				}
				else
				{
					loaded();
				}
			}
		}

		internal void ReloadThreadComments(int threadK, int pageNumber)
		{
			this.threadK = threadK;
			this.pageNumber = pageNumber;
			loadThreadCommentsViaWebRequest();
		}

		void loadThreadCommentsViaWebRequest()
		{
			Service.GetThreadComments(threadK, pageNumber, //commentsPerPage,
				// doesn't like (currentInitialComment != null) - becomes undefined bool!
				((InitialComment != null) ? true : false) && (LastPage > 0),
				getThreadCommentsSuccess, getThreadCommentsFailure, null, -1);
		}

		void getThreadCommentsSuccess(CommentResult result, object userContext, string methodName)
		{
			pageNumber = result.currentPage;
			Comments = result.comments;
			if (InitialComment == null)
			{
				InitialComment = (result.initialComment != null) ? result.initialComment : result.comments[0];
			}
			if (LastPage == 0)
			{
				LastPage = result.lastPage;
			}
			if (FirstUnreadPage == 0)
			{
				FirstUnreadPage = result.firstUnreadPage;
			}
			if (TotalComments == 0)
			{
				TotalComments = result.totalComments;
			}
			if (ViewComments == 0)
			{
				ViewComments = result.viewComments;
			}

			updateLastKnownCommentK(result.comments[result.comments.Length - 1].k);

			loaded();
			Service.SetThreadUsr(this.threadK, this.pageNumber, null, null, null, -1);
		}
		void getThreadCommentsFailure(WebServiceError error, object userContext, string methodName)
		{
			// probably there weren't any comments to retrieve - just set everything to defaults
			pageNumber = 0;
			Comments = null;
			FirstUnreadPage = 0;
			InitialComment = null;
			LastKnownCommentK = 0;
			LastPage = 0;
			TotalComments = 0;
			ViewComments = 0;

			loaded();
		}

		void loaded()
		{
			if (OnLoaded != null)
				OnLoaded(this, EventArgs.Empty);
		}

		internal string CommentsAnchorName;
		void prevPageClick(DomEvent e)
		{
			e.PreventDefault();
			this.RefreshBanners();
			this.pageNumber--;
			this.LoadThreadComments();
			Misc.RedirectToAnchor(CommentsAnchorName);
		}

		private void RefreshBanners()
		{
			BannersGeneratorController.RefreshAllBanners();
		}

		void nextPageClick(DomEvent e)
		{
			e.PreventDefault();
			this.RefreshBanners();
			this.pageNumber++;
			this.LoadThreadComments();
			Misc.RedirectToAnchor(CommentsAnchorName);
		}
		void pageClick(DomEvent e)
		{
			e.PreventDefault();
			this.RefreshBanners();
			this.pageNumber = (int)e.Target.GetAttribute("pagenumber");
			this.LoadThreadComments();
			Misc.RedirectToAnchor(CommentsAnchorName);
		}

		#region Create Paging
		internal DivElement CreatePrevNextPaging()
		{
			DivElement pagingDiv = (DivElement)Document.CreateElement("p");
			pagingDiv.Style.TextAlign = "right";

			AnchorElement prevPage = (AnchorElement)Document.CreateElement("a");
			ImageElement bckImg = (ImageElement)Document.CreateElement("img");
			bckImg.Src = "/gfx/icon-back-12.png";
			bckImg.Style.Border = "0";
			bckImg.Style.VerticalAlign = "middle";
			prevPage.AppendChild(bckImg);
			DivElement prevText = (DivElement)Document.CreateElement("span");
			prevText.InnerHTML = "prev page";
			prevPage.AppendChild(prevText);

			if (this.pageNumber > 1 && this.LastPage > 1)
			{
				prevPage.Href = "#";
				DomEvent.AddHandler(prevPage, "click", new DomEventHandler(prevPageClick));
			}
			else
			{
				prevPage.Disabled = true;
				prevPage.ClassName = "DisabledAnchor";
			}
			pagingDiv.AppendChild(prevPage);

			DivElement elipses = (DivElement)Document.CreateElement("span");
			elipses.InnerHTML = "&nbsp;...&nbsp;";
			pagingDiv.AppendChild(elipses);

			AnchorElement nextPage = (AnchorElement)Document.CreateElement("a");
			DivElement nextText = (DivElement)Document.CreateElement("span");
			nextText.InnerHTML = "next page";
			nextPage.AppendChild(nextText);
			ImageElement fwdImg = (ImageElement)Document.CreateElement("img");
			fwdImg.Src = "/gfx/icon-forward-12.png";
			fwdImg.Style.Border = "0";
			fwdImg.Style.VerticalAlign = "middle";
			nextPage.AppendChild(fwdImg);

			if (this.pageNumber < this.LastPage)
			{
				nextPage.Href = "#";
				DomEvent.AddHandler(nextPage, "click", new DomEventHandler(nextPageClick));
			}
			else
			{
				nextPage.Disabled = true;
				nextPage.ClassName = "DisabledAnchor";
			}
			pagingDiv.AppendChild(nextPage);

			return pagingDiv;
		}

		internal DivElement CreateNumberedPaging()
		{
			DivElement uiPaging = (DivElement)Document.CreateElement("p");
			DivElement pagesSpan = (DivElement)Document.CreateElement("span");
			pagesSpan.InnerHTML = "Pages: ";
			uiPaging.AppendChild(pagesSpan);

			bool[] renderPageNumber = getPageNumbersToRender();

			bool renderedEllipsis = false;
			for (int i = 1; i <= LastPage; i++)
			{
				if (renderPageNumber[i])
				{
					bool unread = FirstUnreadPage > 0 && FirstUnreadPage <= i && ViewComments < TotalComments;
					DOMElement uiPageNumber;
					if (i == pageNumber)
					{
						uiPageNumber = (DivElement)Document.CreateElement("span");
						uiPageNumber.ClassName = unread ? "CurrentPageUnread" : "CurrentPage";
					}
					else
					{
						uiPageNumber = (AnchorElement)Document.CreateElement("a");
						uiPageNumber.SetAttribute("pagenumber", i);
						((AnchorElement)uiPageNumber).Href = "#Comments";
						DomEvent.AddHandler(uiPageNumber, "click", new DomEventHandler(pageClick));

						if (unread)
						{
							uiPageNumber.ClassName = "Unread";
						}
					}
					uiPageNumber.InnerHTML = i.ToString();
					uiPaging.AppendChild(uiPageNumber);

					DivElement space = (DivElement)Document.CreateElement("span");
					space.InnerHTML = "&nbsp;";
					uiPaging.AppendChild(space);

					renderedEllipsis = false;
				}
				else
				{
					if (!renderedEllipsis)
					{
						DivElement ellipsis = (DivElement)Document.CreateElement("span");
						ellipsis.InnerHTML = "...&nbsp;";
						if (FirstUnreadPage > 0 && i > FirstUnreadPage)
						{
							ellipsis.ClassName = "Unread";
						}
						uiPaging.AppendChild(ellipsis);

						renderedEllipsis = true;
					}
				}
			}

			return uiPaging;
		}

		bool[] getPageNumbersToRender()
		{
			int endLinks = 3;
			int midLinks = 4;
			int midLinksUnread = 2;
			bool[] renderPageNumber = new bool[LastPage + 1];

			setRenderPageNumbers(renderPageNumber, 1, endLinks);
			setRenderPageNumbers(renderPageNumber, LastPage - endLinks + 1, LastPage);
			if (FirstUnreadPage > 0)
			{
				setRenderPageNumbers(renderPageNumber, FirstUnreadPage - midLinksUnread, FirstUnreadPage + midLinksUnread - 1);
			}
			setRenderPageNumbers(renderPageNumber, pageNumber - midLinks, pageNumber + midLinks);

			for (int i = 1; i < renderPageNumber.Length - 1; i++)
			{
				// if numbers on either side are displayed, then display this one
				if (renderPageNumber[i - 1] == true && renderPageNumber[i] == false && renderPageNumber[i + 1] == true)
				{
					renderPageNumber[i] = true;
				}
			}
			return renderPageNumber;
		}

		void setRenderPageNumbers(bool[] renderPageNumber, int start, int end)
		{
			for (int i = start; i <= end; i++)
			{
				renderPageNumber[i] = true;
			}
		}
		#endregion





		#region Creating new comments
		internal EventHandler OnCommentPosted;

		private void appendNewComment(CommentStub comment)
		{
			appendNewComments(new CommentStub[] { comment });
		}
		private void appendNewComments(CommentStub[] comments)
		{
			appendComments(comments);
			this.TotalComments += comments.Length;
			this.updateLastKnownCommentK(comments[comments.Length - 1].k);
			loaded();
		}

		private void updateLastKnownCommentK(int lastCommentK)
		{
			if (lastCommentK > this.LastKnownCommentK)
			{
				this.LastKnownCommentK = lastCommentK;
			}
		}


		internal void CreatePublicThread(int currentParentObjectType, int currentParentObjectK, string duplicateGuid, string rawCommentHtml,
			bool formatting, bool isNews, string[] inviteUsrOptions)
		{
			Service.CreatePublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml,
							formatting, isNews, inviteUsrOptions,
							createPublicThreadSuccess, null, null, -1);
		}
		void createPublicThreadSuccess(CommentStub newComment, object userContext, string methodName)
		{
			Misc.HideWaitingCursor();
			if (newComment != null)
			{
				if (this.threadK == 0)
					this.threadK = newComment.threadK;

				// must allow these events to modify where appropriate ...
				if (OnCommentPosted != null)
				{
					OnCommentPosted(null, new IntEventArgs(this.threadK));
				}
				if (OnThreadCreated != null)
				{
					OnThreadCreated(null, new IntEventArgs(this.threadK));
				}

				// ... before reloading here
				appendNewComment(newComment);

				//Misc.RedirectToAnchor(CommentsAnchorName);
			}
		}


		internal void CreateNewPublicThread(int currentParentObjectType, int currentParentObjectK, string duplicateGuid, string rawCommentHtml,
			bool formatting, bool isNews, string[] inviteUsrOptions)
		{
			Service.CreateNewPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid,
								rawCommentHtml, formatting, isNews, inviteUsrOptions,
								createNewPublicThreadSuccess, null, null, -1);
		}
		//void createNewPublicThreadSuccess(CommentStub newComment, object userContext, string methodName)
		void createNewPublicThreadSuccess(string newThreadUrl, object userContext, string methodName)
		{
			Misc.HideWaitingCursor();
			Misc.Redirect(newThreadUrl);
			//if (newComment != null)
			//{
			//    if (OnCommentPosted != null)
			//    {
			//        OnCommentPosted(this, EventArgs.Empty);
			//    }
			//    if (OnThreadCreated != null)
			//    {
			//        OnThreadCreated(this, new IntEventArgs(this.threadK));
			//    }
			//    Misc.RedirectToAnchor("LatestBoxAnchor");
			//}
		}


		internal void CreateReply(int currentParentObjectType, int currentParentObjectK, int currentThreadK, string duplicateGuid, string rawCommentHtml,
			bool formatting, int currentThreadLastKnownCommentK, string[] inviteUsrOptions)
		{
			Service.CreateReply(currentParentObjectType, currentParentObjectK, currentThreadK, duplicateGuid,
								rawCommentHtml, formatting, currentThreadLastKnownCommentK,
								inviteUsrOptions, createReplySuccess, Trace.WebServiceFailure, null, -1);
		}
		void createReplySuccess(CommentStub[] newComments, object userContext, string methodName)
		{
			Misc.HideWaitingCursor();
			if (newComments != null)
			{
				appendNewComments(newComments);
				if (OnCommentPosted != null)
				{
					OnCommentPosted(this, EventArgs.Empty);
				}
				Misc.RedirectToAnchor("Anchor-CommentK-" + newComments[0].k);
			}
		}
		#endregion

		internal void ReloadComments()
		{
			this.initialComments = new Array();
			this.comments = new Array();
			this.lastPage = new Array();
			this.LoadThreadComments();
		}

		internal void DecrementCurrentThreadTotalComments()
		{
			this.TotalComments--;
		}
	}
}
