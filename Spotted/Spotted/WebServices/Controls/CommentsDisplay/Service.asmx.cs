using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using Bobs;
using SpottedScript.Controls.CommentsDisplay;

namespace Spotted.WebServices.Controls.CommentsDisplay
{
	/// <summary>
	/// Summary description for ThreadControlService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[ScriptService]
	public class Service : System.Web.Services.WebService
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="threadK"></param>
		/// <param name="pageNumber">if less than 1, will get first unread page if applicable</param>
		/// <param name="pageSize"></param>
		/// <param name="getCommentsOnly"></param>
		/// <returns></returns>
		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public CommentResult GetThreadComments(int threadK, int pageNumber, bool getCommentsOnly)
		{
			Thread thread = new Thread(threadK);
			GroupUsr groupUsr = null;
			if (Usr.Current != null && thread.Group != null)
			{
				groupUsr = thread.Group.GetGroupUsr(Usr.Current);
			}
			ThreadUsr threadUsr = null;
			if (Usr.Current != null && thread.CheckPermissionRead(Usr.Current))
			{
				try
				{
					threadUsr = new ThreadUsr(thread.K, Usr.Current.K);
				}
				catch (BobNotFound)
				{
				}
			}


			CommentResult result = new CommentResult();

			if (pageNumber <= 0)
			{
				pageNumber = (result.firstUnreadPage > 0) ? result.firstUnreadPage : 1;
			}

			result.comments = thread.ChildComments().Page(pageNumber, Vars.CommentsPerPage).ConvertAll(c => CreateCommentStub(c, threadUsr, groupUsr)).ToArray();
			if (result.comments.Length == 0)
			{
				throw new Exception("No comments to retrieve.");
			}

			result.firstUnreadPage = !getCommentsOnly && threadUsr != null && threadUsr.ViewCommentsInUse > 0 ?
					(threadUsr.ViewCommentsInUse / Vars.CommentsPerPage) + 1 : 0;
			result.lastPage = !getCommentsOnly ? thread.LastPage : 0;
			result.currentPage = pageNumber;
			result.initialComment = (!getCommentsOnly && pageNumber > 1) ? CreateCommentStub(thread.ChildComments()[0], threadUsr, groupUsr) : null;
			result.viewComments = !getCommentsOnly && threadUsr != null ? threadUsr.ViewCommentsInUse : 0;
			result.totalComments = !getCommentsOnly ? thread.TotalComments : 0;

			return result;
		}

		private static CommentStub CreateCommentStub(Comment comment, ThreadUsr threadUsr, GroupUsr groupUsr)
		{
			CommentStub c = new CommentStub()
			{
				k = comment.K,
				html = comment.GetHtml(null),
				script = comment.Script,
				usrName = comment.Usr.Name,
				usrPicSrc = comment.Usr.AnyPicPath,
				usrRollover = comment.Usr.RolloverMouseOverTextNoPic,
				usrUrl = comment.Usr.Url(),
				usrK = comment.Usr.K,
				isNew = comment.GetIsNew(threadUsr),
				friendlyTimeNoCaps = comment.FriendlyTimeNoCaps,
				editLinkVisible = Usr.Current != null && (comment.UsrK == Usr.Current.K || Usr.Current.IsAdmin),
				editedHtml = comment.EditedHtml,
				deleteLinkVisible = Usr.Current != null && Usr.Current.CanDelete(comment),
				deleteLinkOnClickConfirmText = Usr.Current == null ? "" :
					(groupUsr != null && groupUsr.Moderator) ? "You are using your group moderator power to delete this comment.\n\nAre you sure?" :
					(comment.UsrK != Usr.Current.K) ? "You are using your moderator power to delete this comment.\n\nAre you sure?" :
					"Are you sure?",

				threadK = comment.ThreadK
			};

			c.lolHtml = comment.LolUsrListHtml(out c.haveAlreadyLold, Usr.Current != null ? Usr.Current.K : -1);
			return c;
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public string LolAtComment(int commentK)
		{
			if (Usr.Current != null)
			{
				Comment c = new Comment(commentK);
				Lol.CreateLol(c);
				bool me;
				return c.LolUsrListHtml(out me, Usr.Current.K);
			}
			// if nothing, don't change
			return "";
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public bool DeleteComment(int commentK)
		{
			if (Usr.Current != null)
			{
				Comment comment = new Comment(commentK);
				if (Usr.Current.CanDelete(comment))
				{
					comment.RegisterDelete(Usr.Current);
					comment.DeleteAll(null);
					return true;
				}
			}
			return false;
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public void SetThreadUsr(int threadK, int page)
		{
			if (Usr.Current != null)
			{
				Thread t = new Thread(threadK);
				t.SetThreadUsr(Math.Min(t.TotalComments, page * Vars.CommentsPerPage));
			}
		}

		int[] GetUsrKsFromOptions(string[] options)
		{
			Spotted.WebServices.Controls.MultiBuddyChooser.Service service = new Spotted.WebServices.Controls.MultiBuddyChooser.Service();
			return service.ResolveUsrsFromMultiBuddyChooserValues(options).Values.Select(k => (int)k).ToArray();
		}

		[WebMethod]
		[ScriptMethod(UseHttpGet = false)]
		public string CreateNewThreadInGroup(int groupK, int discussableObjectType, int discussableObjectK, string duplicateGuid, string rawCommentHtml, bool formatting, bool isNews, string[] inviteUsrOptions, bool isPrivate)
		{
			Thread.MakerReturn makerReturn = Thread.CreateNewThreadInGroup(groupK, Bob.Get((Model.Entities.ObjectType)discussableObjectType, discussableObjectK) as IDiscussable, new Guid(duplicateGuid),
				Comment.ParseCommentHtml(rawCommentHtml, formatting, true), isNews, GetUsrKsFromOptions(inviteUsrOptions).ToList(), isPrivate);
			if (makerReturn.Success || makerReturn.Duplicate)
			{
				return makerReturn.Thread.Url();
			}
			else throw new Exception("Error");
		}

		[WebMethod]
		[ScriptMethod(UseHttpGet = false)]
		public string CreatePrivateThread(int discussableObjectType, int discussableObjectK, string duplicateGuid, string rawCommentHtml, bool formatting, string[] inviteUsrOptions, bool isSealed)
		{
			Thread.MakerReturn makerReturn = Thread.CreatePrivateThread(Bob.Get((Model.Entities.ObjectType)discussableObjectType, discussableObjectK) as IDiscussable, new Guid(duplicateGuid),
				Comment.ParseCommentHtml(rawCommentHtml, formatting, true), GetUsrKsFromOptions(inviteUsrOptions).ToList(), isSealed);
			if (makerReturn.Success || makerReturn.Duplicate)
			{
				return makerReturn.Thread.Url();
			}
			else throw new Exception("Error");
		}

		[WebMethod]
		[ScriptMethod(UseHttpGet = false)]
		public CommentStub[] CreateReply(int discussableObjectType, int discussableObjectK, int threadK, string duplicateGuid, string rawCommentHtml, bool formatting, int lastKnownCommentK, string[] inviteUsrOptions)
		{
			Thread thread = new Thread(threadK);
			Comment.MakerReturn makerReturn = Thread.CreateReply(thread, new Guid(duplicateGuid),
				Comment.ParseCommentHtml(rawCommentHtml, formatting, true), GetUsrKsFromOptions(inviteUsrOptions).ToList());

			if (makerReturn.Success)
			{
				IDiscussable discussable = Bob.Get((Model.Entities.ObjectType)discussableObjectType, discussableObjectK) as IDiscussable;
				if (discussable != null)
				{
					discussable.UpdateTotalComments(null);
				}

				return thread.ChildComments().Page(thread.LastPage, Vars.CommentsPerPage).ToList().Where(c => c.K > lastKnownCommentK).ToList()
					.ConvertAll(c => CreateCommentStub(c, thread.GetThreadUsr(Usr.Current), null)).ToArray();
			}
			else if (makerReturn.Duplicate)
				return null;
			else throw new Exception("Error");
		}

		[WebMethod]
		[ScriptMethod(UseHttpGet = false)]
		//public CommentStub CreateNewPublicThread(int discussableObjectType, int discussableObjectK, string duplicateGuid, string rawCommentHtml, bool formatting, bool isNews, string[] inviteUsrOptions)
		public string CreateNewPublicThread(int discussableObjectType, int discussableObjectK, string duplicateGuid, string rawCommentHtml, bool formatting, bool isNews, string[] inviteUsrOptions)
		{
			Thread.MakerReturn makerReturn = Thread.CreateNewPublicThread(Bob.Get((Model.Entities.ObjectType)discussableObjectType, discussableObjectK) as IDiscussable, new Guid(duplicateGuid),
				Comment.ParseCommentHtml(rawCommentHtml, formatting, true), isNews, GetUsrKsFromOptions(inviteUsrOptions).ToList());

			if (makerReturn.Success || makerReturn.Duplicate)
			{
				return makerReturn.Thread.Url();
				//return CreateCommentStub(makerReturn.Comment, makerReturn.Thread.GetThreadUsr(Usr.Current), null);
			}
			else throw new Exception("Error");
		}

		[WebMethod]
		[ScriptMethod(UseHttpGet = false)]
		public CommentStub CreatePublicThread(int discussableObjectType, int discussableObjectK, string duplicateGuid, string rawCommentHtml, bool formatting, bool isNews, string[] inviteUsrOptions)
		{
			var parentObject = Bob.Get((Model.Entities.ObjectType)discussableObjectType, discussableObjectK);
			Thread.MakerReturn makerReturn = Thread.CreatePublicThread(parentObject as IDiscussable, new Guid(duplicateGuid),
				Comment.ParseCommentHtml(rawCommentHtml, formatting, true), isNews, GetUsrKsFromOptions(inviteUsrOptions).ToList(), false);
			
			if (makerReturn.Success)
			{
				return CreateCommentStub(makerReturn.Comment, makerReturn.Thread.GetThreadUsr(Usr.Current), null);
			}
			else if (makerReturn.Duplicate)
				return null;
			else throw new Exception("Error");
		}



		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public string GetNewGuid()
		{
			return Guid.NewGuid().ToString();
		}


		[WebMethod]
		[ScriptMethod(UseHttpGet = false)]
		public string CleanHtml(string html)
		{
			return Cambro.Web.Helpers.CleanHtml(html);
		}

		[WebMethod]
		[ScriptMethod(UseHttpGet = false)]
		public string[] GetPreviewHtml(int previewType, string rawCommentHtml, bool formatting)
		{
			HtmlRenderer r = new HtmlRenderer();
			r.LoadHtml(Comment.ParseCommentHtml(rawCommentHtml, formatting, true));

			return new[] { getPreviewHtml(r, (Spotted.Controls.Html.PreviewTypes)previewType), r.Script };
		}

		private string getPreviewHtml(HtmlRenderer r, Spotted.Controls.Html.PreviewTypes previewType)
		{
			if (previewType == Spotted.Controls.Html.PreviewTypes.Competition)
			{
				return "<h1 style=\"width:295px;\"><span class=\"Inner\">Prize donated by...</span></h1><div class=\"ContentBorder\" style=\"width:295px; overflow:hidden; \">" + r.Html + "</div>";
				//<p class="ArticlePara" runat="server" id="ParaP">
			}
			else if (previewType == Spotted.Controls.Html.PreviewTypes.Article)
			{
				return "<div class=\"ContentBorder\" style=\"width:634px; overflow:hidden; margin-top:15px;\">" + r.Html + "</div>";
				//<p class="ArticlePara" runat="server" id="ParaP">
			}
			else if (previewType == Spotted.Controls.Html.PreviewTypes.Comment)
			{
				r.AddPTagsWhenRenderingFormattedHtmlInContainer = false;

				#region html
				string html = @"<div class=""CommentOuter ClearAfter"">
	<div class=""CommentLeft"">
		<a href=""" + Usr.Current.Url() + @""" " + Usr.Current.RolloverNoPic + @"><img src=""" + Usr.Current.AnyPicPath + @""" border=""0"" width=""100"" height=""100"" style=""margin-bottom:2px;margin-top:0px;"" class=""BorderBlack All Block""></a>
		<a href=""" + Usr.Current.Url() + @""">" + Usr.Current.NickName + @"</a>
	</div>
	<div class=""CommentBody"">
		" + r.Html + @"
	</div>
</div>";
				#endregion
				return "<h1><span class=\"Inner\">Header</span></h1><div class=\"ContentBorder\" style=\"width:634px; overflow:hidden;\">" + html + "</div>";
			}
			else if (r.Container)
				return "<h1><span class=\"Inner\">Header</span></h1><div class=\"ContentBorder\" style=\"width:634px; overflow:hidden;\">" + r.Html + "</div>";
			else
				return "<div style=\"width:634px; overflow:hidden;\">" + r.Html + "</div>";
		}
	}
}
