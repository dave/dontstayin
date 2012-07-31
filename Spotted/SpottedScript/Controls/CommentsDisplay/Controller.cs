using System;
using System.DHTML;
using SpottedScript.Controls.ThreadControl;
using Sys;
using Sys.Net;
using Sys.UI;
using Spotted.WebServices.Controls.CommentsDisplay;
using Utils;

namespace SpottedScript.Controls.CommentsDisplay
{
	public class Controller
	{
		private View view;
		internal ThreadCommentsProvider threadCommentsProvider;
		private string uniqueID { get { return view.uiClientID.Value + "_controls_"; } }
		private DivElement uiCommentsCount;
		private DivElement uiPaging1Div;
		private DivElement uiCommentsDiv;
		private DivElement uiPaging2Div;
		private int commentsPerPage { get { return int.ParseInvariant(view.uiCommentsPerPage.Value); } }

		public Controller(View view)
		{
			this.view = view;

			threadCommentsProvider = new ThreadCommentsProvider(commentsPerPage);
			threadCommentsProvider.CommentsAnchorName = (string)view.uiCommentsAnchor.GetAttribute("name");
			threadCommentsProvider.OnLoaded = new EventHandler(display);

			hideCommentsPanelServerSide();

			uiCommentsCount = (DivElement)Document.CreateElement("center");
			view.uiCommentsPanelClientSide.AppendChild(uiCommentsCount);

			uiPaging1Div = (DivElement)Document.CreateElement("div");
			uiPaging1Div.Style.TextAlign = "right";
			view.uiCommentsPanelClientSide.AppendChild(uiPaging1Div);
			uiCommentsDiv = (DivElement)Document.CreateElement("div");
			view.uiCommentsPanelClientSide.AppendChild(uiCommentsDiv);
			uiPaging2Div = (DivElement)Document.CreateElement("div");
			uiPaging2Div.Style.TextAlign = "right";
			view.uiCommentsPanelClientSide.AppendChild(uiPaging2Div);
		}

		private void hideCommentsPanelServerSide()
		{
			if (view.uiInitialCommentDataList != null)
			{
				view.uiInitialCommentDataList.Style.Display = "none";
			}
			view.uiCommentsPanelServerSide.Style.Display = "none";
			view.uiCommentsPanelClientSide.Style.Display = "";
		}

		public void SetCommentsCount(int commentsCount)
		{
			if (commentsCount == 0)
			{
				uiCommentsCount.InnerHTML = "<p>No comments</p>";
				view.uiInitialCommentPanel.Style.Display = "none";
				view.uiCommentsPanel.Style.Display = "none";
			}
			else
			{
				uiCommentsCount.InnerHTML = (commentsCount == 1) ? "<p>1 comment loading...</p>" : "<p>" + commentsCount + " comments loading...</p>";
				view.uiCommentsPanelClientSide.Style.Display = "";
				view.uiCommentsPanel.Style.Display = "";
			}
			setCommentsAreaVisible(false);
			uiCommentsCount.Style.Display = "";
		}

		private void setCommentsAreaVisible(bool vis)
		{
			string display = vis ? "" : "none";
			string displayPaging = vis && threadCommentsProvider.LastPage > 1 ? "" : "none";
			uiPaging1Div.Style.Display = displayPaging;
			uiCommentsDiv.Style.Display = display;
			uiPaging2Div.Style.Display = displayPaging;
			if (vis)
			{
				view.uiCommentsPanel.Style.Display = "";
			}
		}

		private void hideCommentsCount()
		{
			uiCommentsCount.Style.Display = "none";
		}

		public void ShowComments(int threadK, int pageNumber)
		{
			threadCommentsProvider.ThreadK = threadK;
			threadCommentsProvider.PageNumber = pageNumber;
			threadCommentsProvider.LoadThreadComments();
		}

		internal EventHandler OnCommentsDisplayed;
		private void display(object o, EventArgs e)
		{
			if (threadCommentsProvider.TotalComments > 0)
			{
				displayAll();
			}
			else
			{
				view.uiCommentsPanel.Style.Display = "none";
			}

			if (OnCommentsDisplayed != null)
				OnCommentsDisplayed(this, e);
		}

		private void displayAll()
		{
			displayInitialComment();
			displayPaging();
			view.uiCommentsPanelClientSide.Style.Display = "";
			displayComments();
			hideCommentsCount();
		}

		private void displayComments()
		{
			removeAllChildren(uiCommentsDiv);
			setCommentsAreaVisible(true);

			CommentStub[] comments = threadCommentsProvider.Comments;
			for (int commentIndex = 0; commentIndex < comments.Length; commentIndex++)
			{
				addComment(comments[commentIndex]);
			}
			for (int commentIndex = 0; commentIndex < comments.Length; commentIndex++)
			{
				try
				{
					Script.Eval(comments[commentIndex].script);
				}
				catch
				{
				}
			}
		}

		
		private void addComment(CommentStub comment)
		{
			uiCommentsDiv.AppendChild(createAnchor(comment));
			uiCommentsDiv.AppendChild(createComment(comment));
		}

		private void displayPaging()
		{
			if (threadCommentsProvider.LastPage == 1)
			{
				uiPaging1Div.Style.Display = "none";
				uiPaging2Div.Style.Display = "none";
				return;
			}

			uiPaging1Div.Style.Display = "";
			removeAllChildren(uiPaging1Div);
			uiPaging1Div.AppendChild(threadCommentsProvider.CreatePrevNextPaging());
			uiPaging1Div.AppendChild(threadCommentsProvider.CreateNumberedPaging());
			uiPaging1Div.Style.Display = "";
			removeAllChildren(uiPaging2Div);
			uiPaging2Div.AppendChild(threadCommentsProvider.CreateNumberedPaging());
			uiPaging2Div.AppendChild(threadCommentsProvider.CreatePrevNextPaging());
		}

		private static void removeAllChildren(DOMElement domElement)
		{
			for (int childNodeIndex = 0; childNodeIndex < domElement.ChildNodes.Length; childNodeIndex++)
			{
				domElement.RemoveChild(domElement.ChildNodes[childNodeIndex]);
			}
			domElement.InnerHTML = ""; // i really don't like this being necessary but it seems to be...
		}

		private void displayInitialComment()
		{
			if (threadCommentsProvider.InitialComment != null && threadCommentsProvider.PageNumber > 1)
			{
				view.uiInitialCommentPanel.Style.Display = "";
				view.uiInitialComment.Style.Display = "";
				for (int childNodeIndex = 0; childNodeIndex < view.uiInitialComment.ChildNodes.Length; childNodeIndex++)
				{
					view.uiInitialComment.RemoveChild(view.uiInitialComment.ChildNodes[childNodeIndex]);
				}
				view.uiInitialComment.AppendChild(createComment(threadCommentsProvider.InitialComment));
				setCommentsSubject("Replies");
			}
			else
			{
				view.uiInitialCommentPanel.Style.Display = "none";
				setCommentsSubject("Comments");
			}
		}

		private void setCommentsSubject(string subject)
		{
			view.CommentsSubjectH1.ChildNodes[0].InnerHTML = subject;
		}

		private void updateLols(int commentK, string newLolHtml)
		{
			Document.GetElementById(getLolAnchorControlID(commentK)).Style.Display = "none";
			((DivElement)Document.GetElementById(getLolSpanControlID(commentK))).InnerHTML = newLolHtml;
		}

		#region Create stuff
		#region Create Comment Elements
		//private string getAnchorControlID(int commentK)
		//{
		//    return uniqueID + "A" + commentK;
		//}
		private string getCommentControlID(int commentK)
		{
			return uniqueID + "C" + commentK;
		}
		private string getLolAnchorControlID(int commentK)
		{
			return uniqueID + "L" + commentK;
		}
		private string getLolSpanControlID(int commentK)
		{
			return uniqueID + "H" + commentK;
		}


		private DOMElement createAnchor(CommentStub comment)
		{
			AnchorElement anchor = (AnchorElement)Document.CreateElement("a");
			anchor.ID = "Anchor-CommentK-" + comment.k;
			return anchor;
		}


		/*
<a name="CommentK-<%#CurrentComment.K%>"></a> -??????????????
<div class="CommentOuter ClearAfter">
	/CommentLeft/
	/CommentBody/
</div>
<a name="AfterCommentK-<%#CurrentComment.K%>"></a> -??????????????
		*/

		private DivElement createComment(CommentStub comment)
		{
			DivElement div = (DivElement)Document.CreateElement("div");
			div.ClassName = "CommentOuter ClearAfter";
			div.AppendChild(createCommentLeft(comment));
			div.AppendChild(createCommentBody(comment));
			return div;

			//TableElement table = (TableElement)Document.CreateElement("table");
			//table.ID = getCommentControlID(comment.k);
			//table.ClassName = "CommentTable";

			//table.Style.Border = "0px";
			//table.Style.Width = "582px";

			//TableRowElement tr = (TableRowElement)Document.CreateElement("tr");
			//tr.AppendChild(createCommentLeft(comment));
			//tr.AppendChild(createCommentRight(comment));

			//TableSectionElement tbody = (TableSectionElement)Document.CreateElement("tbody");
			//tbody.AppendChild(tr);
			//table.AppendChild(tbody);

			//return table;
		}

		/*
<div class="CommentLeft">
	<a href="<%#CurrentComment.Usr.Url()%>" <%#CurrentComment.Usr.RolloverNoPic%>><img src="" runat="server" id="PicImg" border="0" width="100" height="100" style="margin-bottom:2px;margin-top:0px;" class="BorderBlack All Block"></a>
	<a href="<%#CurrentComment.Usr.Url()%>"><%#CurrentComment.Usr.NickName%></a>
</div>
		*/
		private DivElement createCommentLeft(CommentStub comment)
		{
			DivElement div = (DivElement)Document.CreateElement("div");
			div.ClassName = "CommentLeft";

			AnchorElement aimg = (AnchorElement)Document.CreateElement("a");
			aimg.Href = comment.usrUrl;
			createMouseOverAndOut(aimg, comment.usrRollover, "htm();");

			ImageElement img = (ImageElement)Document.CreateElement("img");
			img.Src = comment.usrPicSrc;
			img.Style.Width = "100px";
			img.Style.Height = "100px";
			img.Style.MarginBottom = "2px";
			img.Style.MarginTop = "0px";
			img.ClassName = "BorderBlack All Block";
			
			aimg.AppendChild(img);
			div.AppendChild(aimg);
			
			AnchorElement aname = (AnchorElement)Document.CreateElement("a");
			aname.Href = comment.usrUrl;
			aname.InnerHTML = comment.usrName;
			div.AppendChild(aname);

			return div;
		}

		/*
<div class="CommentBody">
	<%#CurrentComment.NewHtml%><%#CurrentComment.GetHtml(this)%>
	<div class="CommentAdmin">
		<small>
			<span class="CleanLinks"><asp:PlaceHolder Runat="server" id="LolHtmlPh"></asp:PlaceHolder></span>
			<span runat="server" id="LolDownLevelSpan"><asp:LinkButton ID="LinkButton1" Runat="server" OnClick="LolClick" CausesValidation="False">This made me laugh!</asp:LinkButton><br /></span>
			<a href="#PostComment">Reply</a>
			<a href="" onmousedown="QuoteNow(<%#CurrentComment.Usr.K.ToString()%>);return false;" onclick="FocusNow();return false;">Quote</a>
			<a href="" runat="server" id="CommentEditAnchor">Edit</a>
			<asp:LinkButton Runat="server" ID="DeleteButton" CausesValidation="False" OnCommand="DeleteCommand" CommandName="Delete" CommandArgument="<%#CurrentComment.K%>">Delete</asp:LinkButton><br />
			<span onmouseover="stt('<%#CurrentComment.K.ToString("#,##0")%>');" onmouseout="htm();">Posted <%#CurrentComment.FriendlyTimeNoCaps%></span><%#CurrentComment.EditedHtml%>
		</small>
	</div>
</div>
		*/
		private DivElement createCommentBody(CommentStub comment)
		{
			DivElement div = (DivElement)Document.CreateElement("div");
			div.ClassName = "CommentBody";
			div.InnerHTML = (comment.isNew ? "<a name=\"Unread\"></a><span class=\"Unread\">NEW</span> " : "") + comment.html;

			DivElement admin = (DivElement)Document.CreateElement("div");
			admin.ClassName = "CommentAdmin";
			
			DivElement small = (DivElement)Document.CreateElement("small");

			DivElement lolSpan = (DivElement)Document.CreateElement("span");
			lolSpan.ClassName = "CleanLinks";
			lolSpan.InnerHTML = comment.lolHtml;
			lolSpan.ID = getLolSpanControlID(comment.k);
			small.AppendChild(lolSpan);

			if (comment.haveAlreadyLold == false && bool.Parse(view.uiUsrIsLoggedIn.Value))
			{
				small.AppendChild(Document.CreateTextNode(" "));
				DivElement lolAnchorSpan = (DivElement)Document.CreateElement("div");
				AnchorElement lolAnchor = (AnchorElement)Document.CreateElement("a");
				{
					lolAnchor.Href = "#";
					lolAnchor.InnerHTML = "This made me laugh!";
					lolAnchor.SetAttribute(Properties.CommentK, comment.k);
					DomEvent.AddHandler(lolAnchor, "click", new DomEventHandler(lolClick));
				}
				lolAnchorSpan.AppendChild(lolAnchor);
				lolAnchorSpan.ID = getLolAnchorControlID(comment.k);
				small.AppendChild(lolAnchorSpan);
			}


			small.AppendChild(Document.CreateTextNode(" "));

			AnchorElement reply = (AnchorElement)Document.CreateElement("a");
			reply.Href = "#PostComment";
			reply.InnerHTML = "Reply";
			small.AppendChild(reply);


			small.AppendChild(Document.CreateTextNode(" "));
			AnchorElement quote = (AnchorElement)Document.CreateElement("a");
			quote.InnerHTML = "Quote";
			quote.Href = "#";
			DomEvent.AddHandler(quote, "mousedown", new DomEventHandler(quoteMouseDown));
			DomEvent.AddHandler(quote, "click", new DomEventHandler(quoteClick));
			quote.SetAttribute(Properties.UsrK, comment.usrK);
			small.AppendChild(quote);
			
			

			if (comment.editLinkVisible)
			{
				small.AppendChild(Document.CreateTextNode(" "));
				AnchorElement edit = (AnchorElement)Document.CreateElement("a");
				edit.InnerHTML = "Edit";
				edit.Href = "/pages/commentedit/k-" + comment.k;
				small.AppendChild(edit);
				
			}

			if (comment.deleteLinkVisible)
			{
				small.AppendChild(Document.CreateTextNode(" "));
				AnchorElement deleteAnchor = (AnchorElement)Document.CreateElement("a");
				deleteAnchor.InnerHTML = "Delete";
				deleteAnchor.Href = "#";
				DomEvent.AddHandler(deleteAnchor, "click", new DomEventHandler(deleteClick));

				deleteAnchor.SetAttribute(Properties.DeleteConfirmText, comment.deleteLinkOnClickConfirmText);
				deleteAnchor.SetAttribute(Properties.CommentK, comment.k);

			//	deleteAnchor.Style.MarginLeft = "4px";
				small.AppendChild(deleteAnchor);
			}

			small.AppendChild((DivElement)Document.CreateElement("br"));

			DivElement postDetails = (DivElement)Document.CreateElement("span");
			createMouseOverAndOut(postDetails, "stt('" + comment.k + "');", "htm();");
			postDetails.InnerHTML = "Posted " + comment.friendlyTimeNoCaps;
			small.AppendChild(postDetails);

			if (comment.editedHtml != null && comment.editedHtml.Length > 0)
			{
				DivElement edited = (DivElement)Document.CreateElement("span");
				edited.InnerHTML = comment.editedHtml;
				small.AppendChild(edited);
			}

			
			admin.AppendChild(small);
			div.AppendChild(admin);
			return div;
		}
		#endregion
		#region CreateMouseOverAndOut
		private void createMouseOverAndOut(DOMElement domElement, string mouseover, string mouseout)
		{
			domElement.SetAttribute("mouseover", mouseover);
			DomEvent.AddHandler(domElement, "mouseover", new DomEventHandler(mouseOver));

			domElement.SetAttribute("mouseout", mouseout);
			DomEvent.AddHandler(domElement, "mouseout", new DomEventHandler(mouseOut));
		}
		private void mouseOver(DomEvent e)
		{
			Script.Eval((string)e.Target.GetAttribute("mouseover"));
		}
		private void mouseOut(DomEvent e)
		{
			Script.Eval("htm();");
		}
		#endregion
		#region Lol
		private void lolClick(DomEvent e)
		{
			e.PreventDefault();
			int commentK = (int)e.Target.GetAttribute(Properties.CommentK);
			Service.LolAtComment(commentK, lolAtCommentSuccess, Trace.WebServiceFailure, commentK, -1);
		}
		private void lolAtCommentSuccess(string newLolHtml, object commentK, string methodName)
		{
			updateLols((int)commentK, newLolHtml);
		}
		#endregion
		#region Quote
		private void quoteMouseDown(DomEvent e)
		{
			e.PreventDefault();
			int usrK = (int)e.Target.GetAttribute(Properties.UsrK);
			Script.Eval("QuoteNow(" + usrK.ToString() + ");");
		}
		private void quoteClick(DomEvent e)
		{
			e.PreventDefault();
			Script.Eval("FocusNow();");
		}
		#endregion
		#region Delete comment
		private void deleteClick(DomEvent e)
		{
			e.PreventDefault();
			Misc.ShowWaitingCursor();

			if (Script.Confirm((string)e.Target.GetAttribute(Properties.DeleteConfirmText)))
			{
				int commentK = (int)e.Target.GetAttribute(Properties.CommentK);
				Service.DeleteComment(commentK, deleteCommentSuccess, deleteCommentFailure, commentK, -1);
			}
		}

		public EventHandler OnThreadDeleted;

		private void deleteCommentSuccess(bool commentDeleted, object commentK, string methodName)
		{
			Misc.HideWaitingCursor();
			if (commentDeleted)
			{
				if (threadCommentsProvider.TotalComments == 1)
				{
					if (OnThreadDeleted != null)
						OnThreadDeleted(this, new IntEventArgs(threadCommentsProvider.ThreadK));
					threadCommentsProvider.ThreadK = 0;
					view.uiCommentsPanel.Style.Display = "none";
				}
				threadCommentsProvider.DecrementCurrentThreadTotalComments();
				threadCommentsProvider.ReloadComments();
			}
		}
		private void deleteCommentFailure(WebServiceError error, object context, string methodName)
		{
			Misc.HideWaitingCursor();
		}
		#endregion
		#endregion


	}


	static class Properties
	{
		internal readonly static string DeleteConfirmText = "ConfirmText";
		internal readonly static string CommentK = "CommentK";
		internal readonly static string UsrK = "UsrK";
	}
}
