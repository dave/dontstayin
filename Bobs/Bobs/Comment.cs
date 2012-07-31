using System;
using System.Data;
using System.Data.SqlClient;

using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Xml;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
//using Quartz;
using Common.Collections;
using System.Runtime.Serialization;
using Bobs.JobProcessor;
using Bobs.Jobs;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Sgml;
using SpottedScript.Controls.ChatClient.Shared;


namespace Bobs
{

	#region Comment
	/// <summary>
	/// Comment - discussion comment either under a photo or a thread
	/// </summary>
	[Serializable]
	public partial class Comment : IPage, IDeleteAll
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Comment.Columns.K] as int? ?? 0; }
			set { this[Comment.Columns.K] = value; }
		}
		/// <summary>
		/// Text of comment
		/// </summary>
		public override string Text
		{
			get { return (string)this[Comment.Columns.Text]; }
			set { this[Comment.Columns.Text] = value; }
		}
		/// <summary>
		/// Date / time posted
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[Comment.Columns.DateTime]; }
			set { this[Comment.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Links to one Thread
		/// </summary>
		public override int ThreadK
		{
			get { return (int)this[Comment.Columns.ThreadK]; }
			set { this[Comment.Columns.ThreadK] = value; }
		}
		/// <summary>
		/// Links to one User
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Comment.Columns.UsrK]; }
			set { this[Comment.Columns.UsrK] = value; }
		}
		/// <summary>
		/// If this is set to false, the comment will not be displayed (for disabling offensive posts)
		/// </summary>
		public override bool Enabled
		{
			get { return (bool)this[Comment.Columns.Enabled]; }
			set { this[Comment.Columns.Enabled] = value; }
		}
		/// <summary>
		/// Guid used to ensure duplicate messages don't get posted if the user refreshes the page after posting a message.
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Comment.Columns.DuplicateGuid]); }
			set { this[Comment.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// If the comment has been edited by it's author, this is true
		/// </summary>
		public override bool IsEdited
		{
			get { return (bool)this[Comment.Columns.IsEdited]; }
			set { this[Comment.Columns.IsEdited] = value; }
		}
		/// <summary>
		/// If the comment has been edited by it's author, this will show the date/time that the edit took place
		/// </summary>
		public override DateTime EditDateTime
		{
			get { return (DateTime)this[Comment.Columns.EditDateTime]; }
			set { this[Comment.Columns.EditDateTime] = value; }
		}
		/// <summary>
		/// Number of users that Lol'ed at this comment
		/// </summary>
		public override int LolCount
		{
			get { return (int)this[Comment.Columns.LolCount]; }
			set { this[Comment.Columns.LolCount] = value; }
		}
		/// <summary>
		/// Index of thie comment in the thread
		/// </summary>
		public override int IndexInThread
		{
			get { return (int)this[Comment.Columns.IndexInThread]; }
			set { this[Comment.Columns.IndexInThread] = value; }
		}
		/// <summary>
		/// Ip of the user when posted
		/// </summary>
		public override string Ip
		{
			get { return (string)this[Comment.Columns.Ip]; }
			set { this[Comment.Columns.Ip] = value; }
		}
		/// <summary>
		/// The guid of the main chat item (for the archive)
		/// </summary>
		public override Guid? ChatItemGuid
		{
			get { return (Guid?)this[Comment.Columns.ChatItemGuid]; }
			set { this[Comment.Columns.ChatItemGuid] = value; }
		}
		#endregion

		#region RegisterDelete
		public void RegisterDelete(Usr deletingUsr)
		{
			if (deletingUsr == null)
				return;

			if (deletingUsr.K == this.UsrK)
				return;

			bool isGroupMod = false;

			if (this.Thread.GroupK > 0)
			{
				GroupUsr groupUsr = this.Thread.Group.GetGroupUsr(deletingUsr);
				if (groupUsr != null && groupUsr.Moderator)
					isGroupMod = true;
			}

			string s = "";
			s += isGroupMod ? "<b>GROUP moderator delete</b><br /><br />" : "<b>SITE moderator delete</b><br /><br />";
			s += String.Format(@"Posted by: <dsi:object type=""usr"" ref=""{0}"" />, <small>{1}</small><br /><br />", this.UsrK.ToString(), this.DateTime.ToString());
			s += String.Format(@"Deleted by: <dsi:object type=""usr"" ref=""{0}"" />, <small>{1}</small><br /><br />", deletingUsr.K.ToString(), DateTime.Now.ToString());
			s += String.Format(@"Thread <a href=""/chat/k-{0}"">{1}</a>: <small>{2}</small><br /><br />", this.ThreadK.ToString(), this.ThreadK.ToString(), this.Thread.Subject);
			if (this.Thread.GroupK > 0)
				s += String.Format(@"Group: <a href=""{0}"">{1}</a><br /><br />", this.Thread.Group.Url(), this.Thread.Group.Name);

			Bobs.HtmlRenderer r = new Bobs.HtmlRenderer();
			r.LoadHtml(this.Text);
			s += r.GetHtmlForEditorControl();

			string sOut = ParseCommentHtml(s, r.Formatting, true);

			Thread commentDeleteTrackerThread = new Thread(Vars.DevEnv ? 2735620 : 2782072);

			Comment.Maker m = commentDeleteTrackerThread.GetCommentMaker();
			m.DuplicateGuid = Guid.NewGuid();
			m.Body = sOut;
			m.PostingUsr = new Usr(8);
			m.CurrentThreadUsr = thread.GetThreadUsr(m.PostingUsr);
			Comment.MakerReturn r1 = m.Post(null);
		}
		#endregion

		#region Parse CommentHtml
		public static string ParseCommentHtml(string rawCommentHtml, bool formatting, bool container)
		{
			string dsiHtmlTagStart = getDsiHtmlTag(formatting, container);
			string dsiHtmlTagEnd = "</dsi:html>";

			return dsiHtmlTagStart + Cambro.Web.Helpers.CleanHtml(rawCommentHtml) + dsiHtmlTagEnd;
		}
		static string getDsiHtmlTag(bool formatting, bool container)
		{
			return "<dsi:html formatting=\"" + formatting.ToString().ToLower() + "\" container=\"" + container.ToString().ToLower() + "\">";
		}
		#endregion

		#region WatchingHtml
		public string WatchingHtml(string onReturn, Control controlForScript)
		{
			string stateWatch = "0";
			if (this.Thread.JoinedThreadUsr.UsrK>0 && this.Thread.JoinedThreadUsr.IsWatching)
				stateWatch = "1";

			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "w" + this.K.ToString(), String.Format("DbButtonFull(i1,i2,a1,a2,\"\",\"\",\"\",s1,l1,26,21,f1,{0},{1},\"w{2}\",\"" + onReturn + "\",\"\",\"\",\"w{2}\");", this.Thread.K.ToString(), stateWatch, this.K.ToString()), true);
			return String.Format("<div style=\"width:26px;height:21px;\" id=\"w{0}\"><img src=\"{1}\" align=\"left\" width=\"26\" height=\"21\"></div>", this.K.ToString(), stateWatch == "1" ? "/gfx/icon-eye-up.png" : "/gfx/icon-eye-dn.png");
		}
		#endregion
		#region FavouriteHtml
		public string FavouriteHtml(Control controlForScript)
		{
			string stateFavourite = "0";
			if (this.Thread.JoinedThreadUsr.UsrK>0 && this.Thread.JoinedThreadUsr.Favourite)
				stateFavourite = "1";

			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "f" + this.K.ToString(), String.Format("DbButtonFull(i3,i4,a3,a4,\"\",\"\",\"\",s2,l2,22,21,f2,{0},{1},\"f{2}\",\"\",\"\",\"\",\"f{2}\");", this.Thread.K.ToString(), stateFavourite, this.K.ToString()), true);
			return String.Format("<div style=\"width:26px;height:21px;\" id=\"f{0}\"><img src=\"{1}\" align=\"left\" width=\"22\" height=\"21\"></div>", this.K.ToString(), stateFavourite == "1" ? "/gfx/icon-star-22-up.png" : "/gfx/icon-star-22-dn.png");
		}
		#endregion

		#region Page
		public int Page
		{
			get
			{
				return (IndexInThread / Vars.CommentsPerPage)+1;
			}
		}
		#endregion

		#region TextSnip(int length)
		public string TextSnip(int length)
		{
			string noHtml = Cambro.Web.Helpers.StripHtml(this.Text).Replace("\n","");
			if (noHtml.Length>length)
				return noHtml.Substring(0,length-3)+"...";
			else if (noHtml.Length==0)
				return "[no text]";
			else
				return noHtml;
		}
		#endregion

		#region Maker
		public class Maker
		{
			public string Body { get; set; }
			public Thread ParentThread { get; set; }
			public object DuplicateGuid { get; set; }
			public Usr PostingUsr { get; set; }
			#region InviteKs
			/// <summary>
			/// List of Usr.K's to invite
			/// </summary>
			public List<int> InviteKs
			{
				get
				{
					if (inviteKs==null)
						inviteKs = new List<int>();
					return inviteKs;
				}
				set
				{
					inviteKs = value;
				}
			}
			private List<int> inviteKs;
			#endregion
			public List<int> AlertedUsrs { private get; set; }
			public bool NewThread { get; set; }
			public Comment NewComment { private get; set; }
			public ThreadUsr CurrentThreadUsr { get; set; }
			public GroupUsr CurrentGroupUsr { get; set; }
			#region StartNewThread
			public bool RunAsync
			{
				get
				{
					return runAysnc;
				}
				set
				{
					runAysnc = value;
				}
			}
			private bool runAysnc=true;
			#endregion
			public bool DisableLiveChatMessage { get; set; }
			public Guid? ChatItemGuid { get; set; }

			#region Post()
			public MakerReturn Post(Transaction transaction)
			{
				if (HttpContext.Current!=null)
					HttpContext.Current.Items["VisitComments"]=1;

				MakerReturn r = new MakerReturn();

				#region Look for duplicates
				Query qDuplicate = new Query();
				qDuplicate.QueryCondition=new Q(Comment.Columns.DuplicateGuid,(Guid)DuplicateGuid);
				qDuplicate.Columns=new ColumnSet(Comment.Columns.K, Comment.Columns.IndexInThread, Comment.Columns.ThreadK);
				CommentSet csDuplicate = new CommentSet(qDuplicate);
				if (csDuplicate.Count>0)
				{
					r.Success = false;
					r.Duplicate = true;
					r.Comment = csDuplicate[0];
					return r;
				}
				#endregion
				#region Check permission
				if (!NewThread && !ParentThread.CheckPermissionPost(PostingUsr, CurrentThreadUsr, CurrentGroupUsr))
				{
					r.Success=false;
					r.MessageHtml="You can't post in this topic.";
					return r;
				}
				#endregion
				#region Check for closed thread
				if (!NewThread && ParentThread.Closed)
				{
					r.Success=false;
					r.MessageHtml="This topic is closed - posting is disabled.";
					return r;
				}
				#endregion
			
				#region Create comment
				NewComment = new Comment();
				NewComment.DateTime = DateTime.Now;
				if (HttpContext.Current!=null)
					NewComment.Ip = Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);

				if (ChatItemGuid == null)
					NewComment.ChatItemGuid = Guid.NewGuid();
				else
					NewComment.ChatItemGuid = ChatItemGuid;

				NewComment.Enabled = true;
				NewComment.Text = Body;
				NewComment.ThreadK = ParentThread.K;
				NewComment.UsrK = PostingUsr.K;
				if (!ParentThread.Private)
				{
					PostingUsr.CommentCount++;
					PostingUsr.Update(transaction);
				}
				NewComment.DuplicateGuid = (Guid)DuplicateGuid;
				NewComment.IndexInThread = ParentThread.TotalComments;
				NewComment.Update(transaction);
				#endregion

				#region Update thread
				ParentThread.LastPost=NewComment.DateTime;
				ParentThread.LastPostUsrK=NewComment.UsrK;
				ParentThread.Update(transaction);
				ParentThread.UpdateTotalComments(transaction);
				#endregion
				#region Update / create ThreadUsr
				CurrentThreadUsr.ChangeStatus(ThreadUsr.StatusEnum.Archived, NewComment.DateTime);
				CurrentThreadUsr.ViewDateTime = NewComment.DateTime;
				CurrentThreadUsr.ViewDateTimeLatest = NewComment.DateTime;
				CurrentThreadUsr.ViewComments = ParentThread.TotalComments;
				CurrentThreadUsr.ViewCommentsLatest = ParentThread.TotalComments;
				CurrentThreadUsr.Update(transaction);
				#endregion

				string chatMessageBody = "";
				bool alreadySentChatMessage = false;
				bool commentIsPrivate = ParentThread.Private || ParentThread.GroupPrivate || ParentThread.PrivateGroup;
				if (!DisableLiveChatMessage)
				{
					chatMessageBody = Chat.GetMessageFromCommentBody(Body);

					//If the thread is subject to group privacy and we are inviting people, we should wait until after all the invites have been processed before sending the 
					//message, or we might sent the chat message to people who don't have permission to see the thread
					bool commentIsSubjectToGroupPrivacy = ParentThread.GroupPrivate || ParentThread.PrivateGroup;
					bool canPostImmediatly = !commentIsSubjectToGroupPrivacy || InviteKs.Count == 0;
					if (!NewThread && canPostImmediatly)
					{
						//If we're NOT posting a new thread, we should post the chat message immediatly...
						//if we are posting a new thread, we should wait until after all the invites have been done, so we have a better list of participants.
						sendChatMessageToParticipantsNow(InviteKs, NewComment, ParentThread, PostingUsr, chatMessageBody);
						alreadySentChatMessage = true;
					}

					
					if (!commentIsPrivate)
					{
						//Since we don't invite anyone to this room, we can post immediatly to the random chat room.
						//CommentMessageStub ms = getChatMessage(
						//    //new Chat.RoomSpec(RoomType.RandomChat).Guid,
						//    new Chat.RoomSpec(RoomType.PublicStream).Guid,
						//    ParentThread.GetRoomSpec().Guid,
						//    NewComment,
						//    ParentThread,
						//    PostingUsr,
						//    chatMessageBody);
						//Chat.SendJsonChatItem(ms);

						CommentMessageStub ms1 = getChatMessage(
							new Chat.RoomSpec(RoomType.PublicStream).Guid,
							ParentThread.GetRoomSpec().Guid,
							NewComment,
							ParentThread,
							PostingUsr,
							chatMessageBody);
						Chat.SendJsonChatItem(ms1);
					}

				}

				try
				{
					if (NewThread && !commentIsPrivate && PostingUsr.FacebookConnected)
					{
						bool sentToFacebook = false;

						if (NewComment.Thread.IsReview && PostingUsr.FacebookStoryEventReview)
						{
							FacebookPost.CreateEventReview(PostingUsr, NewComment.Thread, NewComment);
							sentToFacebook = true;
						}

						if (!sentToFacebook && NewComment.Thread.IsNews && PostingUsr.FacebookStoryPostNews)
						{
							FacebookPost.CreateEventReview(PostingUsr, NewComment.Thread, NewComment);
							sentToFacebook = true;
						}

						if (!sentToFacebook && PostingUsr.FacebookStoryNewTopic)
						{
							FacebookPost.CreateNewTopic(PostingUsr, NewComment.Thread, NewComment);
						}
					}
				}
				catch { }

				SendCommentAlertsJob job = new SendCommentAlertsJob(
					this.ParentThread, 
					this.PostingUsr, 
					this.NewComment, 
					this.NewThread, 
					this.AlertedUsrs, 
					this.InviteKs,
					DisableLiveChatMessage,
					alreadySentChatMessage,
					chatMessageBody);

				if (Vars.DevEnv)
				{
					job.ExecuteSynchronously();
				}
				else
				{
					if (this.RunAsync)
						job.ExecuteAsynchronously();
					else
						job.ExecuteSynchronously();
				}

				if (Usr.Current != null && PostingUsr.K != Usr.DsiUsrK)
				{
					Usr.IncrementSpamBotDefeaterCounter(Usr.SpamBotDefeaterCounter.Comments, Usr.Current.K);
				}

				r.Success = true;
				r.Comment = NewComment;
				return r;

			}
			#endregion

		}
		#endregion
		#region MakerReturn
		public class MakerReturn : Return
		{
			#region Duplicate
			public bool Duplicate
			{
				get
				{
					return duplicate;
				}
				set
				{
					duplicate = value;
				}
			}
			private bool duplicate;
			#endregion
			#region Comment
			public Comment Comment
			{
				get
				{
					return comment;
				}
				set
				{
					comment = value;
				}
			}
			private Comment comment;
			#endregion
		}
		#endregion

		public void SendLiveChatMessagesForTesting()
		{
			string chatMessageBody = Chat.GetMessageFromCommentBody(this.Text);

			sendChatMessageToParticipantsNow(null, this, this.Thread, this.Usr, chatMessageBody);
		}

		public CommentMessageStub GetCommentMessageStub()
		{
			return getChatMessage(
				this.Thread.GetRoomSpec().Guid,
				Guid.Empty,
				this,
				this.Thread,
				this.Usr,
				Chat.GetMessageFromCommentBody(this.Text));
		}

		#region sendChatMessageToParticipantsNow, getChatMessage
		internal static void sendChatMessageToParticipantsNow(List<int> inviteKs, Comment newComment, Thread parentThread, Usr postingUsr, string chatMessageBody)
		{
			//Lets send a chat alert...
			CommentMessageStub ms = getChatMessage(
				parentThread.GetRoomSpec().Guid,
				Guid.Empty,
				newComment,
				parentThread,
				postingUsr,
				chatMessageBody);

			List<int> sendTo = new List<int>();
			#region add logged inparticipants
			UsrSet usParticipants = Thread.GetAllLoggedInParticipants(parentThread);
			foreach (Usr u in usParticipants)
			{
				try
				{
					sendTo.Add(u.K);
				}
				catch { }
			}
			#endregion
			#region add all people that have been invited
			if (inviteKs != null)
			{
				bool checkGroupMembership = parentThread.GroupK > 0 && (parentThread.GroupPrivate || parentThread.PrivateGroup);
				foreach (int k in inviteKs)
				{
					try
					{
						if (checkGroupMembership)
						{
							GroupUsr gu = parentThread.Group.GetGroupUsr(k);
							if (parentThread.Group.IsMember(gu))
								sendTo.Add(k);
						}
						else
							sendTo.Add(k);
					}
					catch { }
				}
			}
			#endregion
			#region add posting user
			try
			{
				sendTo.Add(postingUsr.K);
			}
			catch { }
			#endregion
			Chat.SendJsonChatItem(ms, sendTo.ToArray());
		}
		static CommentMessageStub getChatMessage(Guid roomGuid, Guid pinRoomGuid, Comment newComment, Thread parentThread, Usr postingUsr, string chatMessageBody)
		{
			return new CommentMessageStub(
				newComment.ChatItemGuid.Value.Pack(),
				ItemType.CommentChatMessage,
				newComment.DateTime.Ticks.ToString(),
				roomGuid.Pack(),
				postingUsr.NickName,
				postingUsr.StmuParams,
				postingUsr.K,
				postingUsr.HasPic ? postingUsr.Pic.ToString() : "0",
				postingUsr.HasChatPic ? postingUsr.ChatPic.Value.ToString() : "0",
				chatMessageBody,
				pinRoomGuid.Pack(),
				newComment.UrlRefresher(),
				parentThread.Subject.TruncateWithDots(30)
			);
		}
		#endregion

		#region Url
		public string Url(params string[] par)
		{
			return this.Url(this.Thread,par);
		}
		public string Url(Thread parentThread, params string[] par)
		{
			if (Page>1)
				par = Cambro.Misc.Utility.JoinStringArrays(par,new string[]{"c",Page.ToString()});
			return parentThread.Url(par)+"#CommentK-"+this.K;
		}
		public string UrlRefresher(params string[] par)
		{
			return this.UrlRefresher(this.Thread,par);
		}
		public string UrlRefresher(Thread parentThread, params string[] par)
		{
			if (Page>1)
				par = Cambro.Misc.Utility.JoinStringArrays(par,new string[]{"c",Page.ToString()});
			par = Cambro.Misc.Utility.JoinStringArrays(par,new string[]{"m",this.K.ToString()});
			return parentThread.Url(par)+"#CommentK-"+this.K;
		}
		/// <summary>
		/// This is used by the chat service to get a url to a comment before it's posted!
		/// </summary>
		/// <param name="par"></param>
		/// <returns></returns>
		public string UrlRefresherAnchorAfter(params string[] par)
		{
			return this.UrlRefresherAnchorAfter(this.Thread, par);
		}
		public string UrlRefresherAnchorAfter(Thread parentThread, params string[] par)
		{
			if (Page > 1)
				par = Cambro.Misc.Utility.JoinStringArrays(par, new string[] { "c", Page.ToString() });
			par = Cambro.Misc.Utility.JoinStringArrays(par, new string[] { "m", this.K.ToString() });
			return parentThread.Url(par) + "#AfterCommentK-" + this.K;
		}
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			Thread t = this.Thread;
			this.Delete(transaction);

			if (t.TotalComments==1)
			{
				t.DeleteAll(transaction);
			}
			else
			{
				t.UpdateTotalComments(transaction);
				if (t.TotalComments == 0)
					t.DeleteAll(transaction);
			}
		}
		#endregion

		#region NewHtml
		public bool IsNew
		{
			get
			{
				if (Usr.Current==null)
					return false;
				var dataRow = this.Bob.DataRow;
				if (dataRow!=null)
				{
					if (dataRow.Table.Columns.Contains("ThreadUsr_ViewDateTime"))
					{
						if (dataRow["ThreadUsr_ThreadK"].Equals(DBNull.Value))
							return false;
						else
						{
							DateTime dateTime = DateTime.MinValue;
							if (!dataRow["ThreadUsr_ViewDateTime"].Equals(DBNull.Value))
								dateTime = (DateTime)dataRow["ThreadUsr_ViewDateTime"];

							int comments = 0;
							if (!dataRow["ThreadUsr_ViewComments"].Equals(DBNull.Value))
								comments = (int)dataRow["ThreadUsr_ViewComments"];

							DateTime dateTimeLatest = DateTime.MinValue;
							if (!dataRow["ThreadUsr_ViewDateTimeLatest"].Equals(DBNull.Value))
								dateTimeLatest = (DateTime)dataRow["ThreadUsr_ViewDateTimeLatest"];

							int commentsLatest = 0;
							if (!dataRow["ThreadUsr_ViewCommentsLatest"].Equals(DBNull.Value))
								commentsLatest = (int)dataRow["ThreadUsr_ViewCommentsLatest"];
							
							int commentsInUse = 0;
							//if (dateTimeLatest.AddMinutes(5)<DateTime.Now && !dateTime.Equals(DateTime.MinValue))
							if (dateTimeLatest.AddMinutes(5)<DateTime.Now) //removed above to solve tims problem
								commentsInUse = commentsLatest;
							else if (!dateTime.Equals(DateTime.MinValue))
								commentsInUse = comments;
							else
								commentsInUse = 0;

							return (this.IndexInThread+1)>commentsInUse && commentsInUse>0;
						}
					}
				}
				return false;
				//throw new Exception("Can't find ThreadUsr records!");
				//return (this.Thread.UsrView!=null && this.DateTime>this.Thread.UsrView.DateTimeInUse);
			}
		}
//		public bool IsOld
//		{
//			get
//			{
//				return (Usr.Current!=null && this.Thread.UsrView!=null && (this.Thread.UsrView.IsInitialised || this.Thread.IsNew) && this.DateTime<=this.Thread.UsrView.DateTimeInUse);
//			}
//		}
		public string NewHtml
		{
			get
			{
				if (IsNew)
					return "<a name=\"Unread\"></a><span class=\"Unread\">NEW</span> ";
				else
					return "";
			}
		}

		public bool GetIsNew(ThreadUsr threadUsr)
		{
			if (Usr.Current == null || threadUsr == null)
				return false;

			int commentsInUse = 0;
			if (threadUsr.ViewDateTimeLatest.AddMinutes(5) < DateTime.Now)
				commentsInUse = threadUsr.ViewCommentsLatest;
			else if (!threadUsr.ViewDateTime.Equals(DateTime.MinValue))
				commentsInUse = threadUsr.ViewComments;
			else
				commentsInUse = 0;

			return (this.IndexInThread + 1) > commentsInUse && commentsInUse > 0;
		}
		#endregion

		#region EditedHtml
		public string EditedHtml
		{
			get
			{
				if (this.IsEdited)
					return "<br>Edited "+Cambro.Misc.Utility.FriendlyTime(this.EditDateTime,false);
				else
					return "";
			}
		}
		#endregion

		#region LolUsrListHtml
		public string LolUsrListHtml(out bool me, int meK)
		{
			me = false;
			if (this.LolCount > 0)
			{			
				StringBuilder sb = new StringBuilder();

				Query q = new Query();
				q.QueryCondition = new Q(Lol.Columns.CommentK, this.K);
				q.TableElement = new Join(Lol.Columns.UsrK, Usr.Columns.K);
				q.Columns = Usr.LinkColumns;
				q.NoLock = true;
				q.OrderBy = new OrderBy(Lol.Columns.DateTime, OrderBy.OrderDirection.Descending);
				q.TopRecords = 100;
				UsrSet us = new UsrSet(q);
				for (int i = 0; i < us.Count; i++)
				{
					if (us[i].K == meK)
						me = true;

					sb.Append("<a href=\"");
					sb.Append(us[i].Url());
					sb.Append("\"");
					sb.Append(us[i].Rollover);
					sb.Append(">");
					sb.Append(us[i].NickNameSafe);
					sb.Append("</a>");
					//txt += "<a href=\"" + us[i].Url() + "\"" + us[i].Rollover + ">" + us[i].NickNameSafe + "</a>";

					if (i == us.Count - 2)
						sb.Append(" and ");
					//txt += " and ";
					else if (i != us.Count - 1)
						sb.Append(", ");
						//txt += ", ";
				}
				if (!me && this.LolCount > 100)
				{
					LolSet meLol = new LolSet(new Query(new And(new Q(Lol.Columns.CommentK, this.K), new Q(Lol.Columns.UsrK, meK))));
					me = meLol.Count > 0;
				}
				if (sb.Length > 0)
				{
					if (this.LolCount > 100)
					{
						sb.Insert(0, " people laughed at this - the latest are: ");
						sb.Insert(0, this.LolCount.ToString("#,##0"));
						sb.Append("<br>");
						//this.LolCount.ToString("#,##0") + " people laughed at this - the latest are: " + txt + "<br>";
					}
					else
					{
						sb.Insert(0, "Who laughed: ");
						sb.Append("<br>");
						//txt = "Who laughed: " + txt + "<br>";
					}
				}
				return sb.ToString();
				
			}
			else
				return string.Empty;
		}
		#endregion

		#region GetHtml
		HtmlRenderer r = new HtmlRenderer();
		public string Script
		{
			get { return r.Script; }
		}
		public string GetHtml(Control controlForScripts)
		{
			//return HttpUtility.HtmlEncode(Text.Trim()).Replace("\n","<br>");
			Usr u = null;
			try
			{
				u = this.Usr;
			}
			catch { }

			string b = "";
			string b1 = "";

			if (u != null && this.Usr.Banned)
			{
				string g = Guid.NewGuid().ToString("N");
				b = "The user that posted this comment has been banned from DontStayIn. To view the message, <a href=\"\" onclick=\"document.getElementById('BannedComment" + g + "').style.display = document.getElementById('BannedComment" + g + "').style.display == 'none' ? '' : 'none'; return false;\">click here at your own risk</a>.<br /><br /><div id=\"BannedComment" + g + "\" style=\"display: none;\">";
				b1 = "</div>";
			}

			string captionStart = "" , captionEnd = "";
			if (this.Thread.IsInCaptionCompetition)
			{
				captionStart = "<img src=\"/gfx/caption-start.gif\" width=\"26\" height=\"20\" /><span style=\"font-size: 20px;\">&nbsp;";
				captionEnd = "&nbsp;</span><img src=\"/gfx/caption-end.gif\" width=\"26\" height=\"20\" />";
			}

			r.LoadHtml(Text);
			r.AddPTagsWhenRenderingFormattedHtmlInContainer = false;
			return b + captionStart + r.Render(controlForScripts) + captionEnd + b1;

		}
		#endregion

		#region FriendlyTime
		public string FriendlyTime
		{
			get
			{
				return Cambro.Misc.Utility.FriendlyTime(DateTime);
			}
		}
		public string FriendlyTimeNoCaps
		{
			get
			{
				return Cambro.Misc.Utility.FriendlyTime(DateTime,false);
			}
		}
		#endregion

		#region ThreadJoin
		public static Join ThreadJoin
		{
			get
			{
				return new Join(Comment.Columns.ThreadK,Thread.Columns.K);
			}
		}
		#endregion
		#region PrivateMesssageUsrJoin
		public static Join PrivateMesssageUsrJoin
		{
			get
			{
				return new Join(
					new Join(
					new Join(Comment.Columns.ThreadK,Thread.Columns.K),
					ThreadUsr.Columns.ThreadK,
					Thread.Columns.K),
					Usr.Columns.K,
					ThreadUsr.Columns.UsrK
				);
			}
		}
		#endregion

		#region Links to Bobs
		#region Thread
		public Thread Thread
		{
			get
			{
				if (thread==null)
					thread = new Thread(ThreadK, this, Comment.Columns.ThreadK);
				return thread;
			}
		}
		Thread thread;
		#endregion
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr==null)
					usr = new Usr(UsrK, this, Comment.Columns.UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#endregion

	}
	#endregion
	#region SendCommentsAlertsJob
	public class SendCommentAlertsJob : Job
	{

		JobDataMapItemProperty<int> ParentThreadK { get { return new JobDataMapItemProperty<int>("ParentThreadK", JobDataMap); } }
		JobDataMapItemProperty<int> PostingUsrK { get { return new JobDataMapItemProperty<int>("PostingUsrK", JobDataMap); } }
		JobDataMapItemProperty<int> NewCommentK { get { return new JobDataMapItemProperty<int>("NewCommentK", JobDataMap); } }
		JobDataMapItemProperty<bool> IsNewThread { get { return new JobDataMapItemProperty<bool>("NewThread", JobDataMap); } }
		JobDataMapItemProperty<List<int>> AlertedUsrKs { get { return new JobDataMapItemProperty<List<int>>("AlertedUsrKs", JobDataMap); } }
		JobDataMapItemProperty<List<int>> InviteKs { get { return new JobDataMapItemProperty<List<int>>("InviteKs", JobDataMap); } }
		JobDataMapItemProperty<bool> DisableChatMessage { get { return new JobDataMapItemProperty<bool>("DisableChatMessage", JobDataMap); } }
		JobDataMapItemProperty<bool> AlreadySentChatMessage { get { return new JobDataMapItemProperty<bool>("AlreadySentChatMessage", JobDataMap); } }
		JobDataMapItemProperty<string> ChatMessageBody { get { return new JobDataMapItemProperty<string>("ChatMessageBody", JobDataMap); } }
		public SendCommentAlertsJob() // this is required by Quatz.net
		{
		}
		internal SendCommentAlertsJob(Thread parentThread, Usr postingUsr, Comment newComment, bool isNewThread, List<int> alertedUsrKs, List<int> inviteKs, bool disableChatMessage, bool alreadySentChatMessage, string chatMessageBody) : this()
		{
			ParentThreadK.Value = parentThread.K;
			PostingUsrK.Value = postingUsr.K;
			NewCommentK.Value = newComment.K;
			IsNewThread.Value = isNewThread;
			AlertedUsrKs.Value = alertedUsrKs;
			InviteKs.Value = inviteKs;
			DisableChatMessage.Value = disableChatMessage;
			AlreadySentChatMessage.Value = alreadySentChatMessage;
			ChatMessageBody.Value = chatMessageBody;
		}
		protected override void Execute()
		{
			#region Comment newComment
			Comment newComment = null;
			try
			{
				newComment = new Comment(this.NewCommentK.Value);
			}
			catch (BobNotFound ex)
			{
				return; //sometimes the user deletes the comment before this is executed
			}
			#endregion
			#region Thread parentThread
			Thread parentThread = null;
			try
			{
				parentThread = new Thread(this.ParentThreadK.Value);
			}
			catch (BobNotFound ex)
			{
				return; //sometimes the user deletes the thread before this is executed
			}
			#endregion
			Usr postingUsr = new Usr(this.PostingUsrK.Value);
			List<int> alertedUsrKs = this.AlertedUsrKs.Value;
			bool isNewThread = this.IsNewThread.Value;
			List<int> inviteKs = this.InviteKs.Value;

			DateTime dt = DateTime.Today;
			Log.Increment(Log.Items.DoAlertsStart, 1, dt);

			SendAlertsToUsersWatchingThisThread(parentThread, postingUsr, newComment, isNewThread, alertedUsrKs);
			SendInvites(parentThread, postingUsr, newComment, isNewThread, alertedUsrKs, inviteKs);

			if (IsNewThread)
			{
				ThreadSendMessagesToCommentAlerts(parentThread, postingUsr, alertedUsrKs);
				//ThreadSendInvites(parentThread, alertedUsrKs, postingUsr, inviteKs);
				ThreadSendGroupNewsAdminsAReminderIfIsRecommendedNews(parentThread);
				ThreadSendNewsAlerts(parentThread);
			}

			SendChatMessage(DisableChatMessage, AlreadySentChatMessage, newComment, parentThread, postingUsr, ChatMessageBody);
			UpdateTotalParticipants(parentThread);

			Log.Increment(Log.Items.DoAlertsEnd, 1, dt);
		}
		#region SendAlertsToUsersWatchingThisThread
		private static void SendAlertsToUsersWatchingThisThread(Thread parentThread, Usr postingUsr, Comment newComment, bool isNewThread, List<int> alertedUsrKs)
		{
			#region Move to inbox and send inbox emails
			try
			{
				if (!isNewThread)
				{
					ThreadUsrSet tus = GetAllArchivedThreadUsrs(parentThread, postingUsr);
					MoveToInboxWhereArchived(parentThread, postingUsr, newComment);
					foreach (ThreadUsr tuAlert in tus)
					{
						if (!alertedUsrKs.Contains(tuAlert.UsrK))
						{
							SendTopicNotificationEmail(parentThread, postingUsr, newComment, tuAlert);
							alertedUsrKs.Add(tuAlert.UsrK);
						}
					}
				}
			}
			catch (Exception ex) { Global.Log("80811792-ac79-4130-ac2c-9902e79101f0", ex); }
			#endregion
		}
		#region GetPeopleWhoHaveArchivedThisThread
		private static ThreadUsrSet GetAllArchivedThreadUsrs(Thread parentThread, Usr postingUsr)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(ThreadUsr.Columns.UsrK, QueryOperator.NotEqualTo, postingUsr.K),
				new Q(ThreadUsr.Columns.ThreadK, parentThread.K),
				new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.Archived));
			ThreadUsrSet tus = new ThreadUsrSet(q);
			return tus;
		}
		#endregion
		#region MoveToInboxWhereArchived
		private static void MoveToInboxWhereArchived(Thread parentThread, Usr postingUsr, Comment newComment)
		{
			Update updateCommand = new Update();
			updateCommand.Table = TablesEnum.ThreadUsr;
			updateCommand.Changes.Add(new Assign(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewComment));
			updateCommand.Changes.Add(new Assign(ThreadUsr.Columns.StatusChangeDateTime, newComment.DateTime));
			updateCommand.Where = new And(
				new Q(ThreadUsr.Columns.UsrK, QueryOperator.NotEqualTo, postingUsr.K),
				new Q(ThreadUsr.Columns.ThreadK, parentThread.K),
				new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.Archived));
			updateCommand.Run();
		}
		#endregion
		#region SendTopicNotificationEmail
		private static void SendTopicNotificationEmail(Thread parentThread, Usr postingUsr, Comment newComment, ThreadUsr tuAlert)
		{
			Mailer usrMail = new Mailer();
			Usr u = tuAlert.Usr;

			//Insert something in here to send chat items?...

			usrMail.Subject = postingUsr.NickName + " comments in: \"" + parentThread.SubjectSnip(40) + "\"";
			usrMail.Body += "<h1>" + postingUsr.NickName + " has posted a comment</h1>";
			usrMail.Body += "<p>The subject of the topic is: \"" + parentThread.Subject + "\"</p>";
			usrMail.Body += "<p>To read and reply, check out the <a href=\"[LOGIN]\">topic page</a>.</p>";
			usrMail.Body += "<p>We won't send you any more alerts about this topic until you move it out of your inbox. To stop receiving these emails, click the <i>Ignore this topic</i> button on the topic page.</p>";
			usrMail.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
			usrMail.RedirectUrl = newComment.UrlRefresher(parentThread);
			usrMail.UsrRecipient = u;
			usrMail.To = u.Email;
			usrMail.Bulk = parentThread.TotalParticipants > 5;
			usrMail.Inbox = true;
			usrMail.Send();
		}
		#endregion
		#endregion
		#region SendInvites
		private static void SendInvites(Thread parentThread, Usr postingUsr, Comment newComment, bool isNewThread, List<int> alertedUsrKs, List<int> inviteKs)
		{
			#region Send invites
			try
			{
				if (inviteKs.Count > 0)
				{
					parentThread.Invite(
						inviteKs,
						postingUsr,
						newComment.DateTime,
						alertedUsrKs,
						isNewThread,
						newComment,
						false);
				}
			}
			catch (Exception ex) { Global.Log("8a0b951f-f1f2-4a08-9598-2093d7ccf938", ex); }
			#endregion
		}
		#endregion

		#region ThreadSendMessagesToCommentAlerts
		private static void ThreadSendMessagesToCommentAlerts(Thread parentThread, Usr postingUsr, List<int> alertedUsrKs)
		{
			#region Send messages to CommentAlerts
			try
			{
				CommentAlertSet cas = null;
				IDiscussable parent = null;

				try
				{
					parent = parentThread.ParentForumObject;
				}
				catch (Exception ex) { Bobs.Global.Log("316e602c-f6ce-4fa5-a628-b05335596a57", ex); }

				if (parentThread.GroupK == 0)
				{
					if (!parentThread.Private &&
						!parentThread.ParentObjectType.Equals(Model.Entities.ObjectType.None) &&
						!parentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Country))
					{
						Query q = new Query();
						List<Q> al = new List<Q>();

						try
						{
							#region Build query
							if (parentThread.ArticleK > 0)
								al.Add(new And(new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Article), new Q(CommentAlert.Columns.ParentObjectK, parentThread.ArticleK)));
							if (parentThread.PhotoK > 0)
								al.Add(new And(new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Photo), new Q(CommentAlert.Columns.ParentObjectK, parentThread.PhotoK)));
							if (parentThread.EventK > 0)
							{
								al.Add(new And(new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Event), new Q(CommentAlert.Columns.ParentObjectK, parentThread.EventK)));
								Event ev = new Event(parentThread.EventK);
								foreach (Brand b in ev.Brands)
									al.Add(new And(new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Brand), new Q(CommentAlert.Columns.ParentObjectK, b.K)));
							}
							if (parentThread.VenueK > 0)
								al.Add(new And(new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Venue), new Q(CommentAlert.Columns.ParentObjectK, parentThread.VenueK)));
							if (parentThread.PlaceK > 0)
								al.Add(new And(new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Place), new Q(CommentAlert.Columns.ParentObjectK, parentThread.PlaceK)));
							#endregion
						}
						catch (Exception ex) { Bobs.Global.Log("2120d49c-25e6-4988-9517-f6253583f6e6", ex); }

						if (al.Count > 0)
						{
							try
							{
								#region Get commentAlerts
								if (al.Count == 1)
								{
									q.QueryCondition = (Q)al[0];
								}
								else
								{
									Q[] qarr = al.ToArray();
									q.QueryCondition = new Or(qarr);
								}
								q.OrderBy = new OrderBy(CommentAlert.Columns.ParentObjectType);
								q.TableElement = new Join(CommentAlert.Columns.UsrK, new Column(CommentAlert.Columns.UsrK, Usr.Columns.K));
								q.Columns = new ColumnSet(
									CommentAlert.Columns.ParentObjectK,
									CommentAlert.Columns.ParentObjectType,
									CommentAlert.Columns.UsrK,
									new JoinedColumnSet(CommentAlert.Columns.UsrK, Usr.EmailColumns)
									);
								cas = new CommentAlertSet(q);
								#endregion
							}
							catch (Exception ex) { Bobs.Global.Log("345a3a46-4c6e-406b-909b-ee79997f21ee", ex); }
						}
					}
				}
				else
				{
					try
					{
						#region Get group commentAlerts
						Query q = new Query();
						q.TableElement = new Join(CommentAlert.Columns.UsrK, new Column(CommentAlert.Columns.UsrK, Usr.Columns.K));
						if (parentThread.GroupPrivate || parentThread.PrivateGroup)
						{
							q.QueryCondition = new And(
								new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Group),
								new Q(CommentAlert.Columns.ParentObjectK, parentThread.GroupK),
								new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member));

							q.TableElement = new Join(
								q.TableElement,
								new TableElement(TablesEnum.GroupUsr),
								QueryJoinType.Inner,
								new And(
									new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Group),
									new Q(CommentAlert.Columns.ParentObjectK, GroupUsr.Columns.GroupK, true),
									new Q(CommentAlert.Columns.UsrK, GroupUsr.Columns.UsrK, true),
									new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));
						}
						else
						{
							q.QueryCondition = new And(
								new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Group), 
								new Q(CommentAlert.Columns.ParentObjectK, parentThread.GroupK));
						}
						q.OrderBy = new OrderBy(CommentAlert.Columns.ParentObjectType);
						q.Columns = new ColumnSet(
							CommentAlert.Columns.ParentObjectK,
							CommentAlert.Columns.ParentObjectType,
							CommentAlert.Columns.UsrK,
							new JoinedColumnSet(CommentAlert.Columns.UsrK, Usr.EmailColumns)
						);
						cas = new CommentAlertSet(q);
						#endregion
					}
					catch (Exception ex) { Bobs.Global.Log("45e3f62f-cdfc-4d34-a506-b7a0b1fe9b94", ex); }
				}

				ThreadSendAlerts(parentThread, alertedUsrKs, postingUsr, cas, parent);
			}
			catch (Exception ex) { Bobs.Global.Log("1dacb724-64b1-4c53-9d95-f043221b1d39", ex); }
			#endregion
		}
		private static void ThreadSendAlerts(Thread parentThread, List<int> alertedUsrs, Usr postingUsr, CommentAlertSet cas, IDiscussable parent)
		{
			#region Send alerts
			if (cas != null)
			{
				foreach (CommentAlert ca in cas)
				{
					try
					{
						if (!alertedUsrs.Contains(ca.UsrK) && postingUsr.K != ca.UsrK)
						{

							ThreadUsr tu = parentThread.GetThreadUsr(ca.Usr);
							tu.ChangeStatus(ThreadUsr.StatusEnum.NewWatchedForumAlert, parentThread.DateTime, false, false);
							tu.StatusChangeObjectK = ca.ParentObjectK;
							tu.StatusChangeObjectType = ca.ParentObjectType;
							tu.Update(null);

							Mailer usrMail = new Mailer();
							usrMail.Subject = postingUsr.NickName + " posts: \"" + parentThread.SubjectSnip(40) + "\"";
							usrMail.Body += "<h1>" + postingUsr.NickName + " has posted a new topic</h1>";
							usrMail.Body += "<p>The subject is: \"" + parentThread.Subject + "\"</p>";
							usrMail.Body += "<p>To read and reply, check out the <a href=\"[LOGIN]\">topic page</a>.</p>";
							string s = postingUsr.LinkEmail();
							usrMail.Body += "<p>" + postingUsr.LinkEmail() + " posted it in the <a href=\"[LOGIN(" + ((IDiscussable)parent).UrlDiscussion() + ")]\">" + ((IName)parent).Name + " forum</a>.</p>";
							if (ca.ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
								usrMail.Body += "<p>You're watching for comments posted on <a href=\"[LOGIN(" + ca.ParentPhoto.Url() + ")]\">this photo</a>.</p>";
							else
								usrMail.Body += "<p>You're watching all new topics in the <a href=\"[LOGIN(" + ((IDiscussable)ca.ParentObject).UrlDiscussion() + ")]\">" + ((IName)ca.ParentObject).Name + " forum</a>. To stop receiving these emails, click the <i>Ignore new topics in this forum</i> button on the forum page.</p>";
							usrMail.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
							usrMail.RedirectUrl = parentThread.UrlDiscussion();
							usrMail.UsrRecipient = ca.Usr;
							usrMail.Bulk = true;
							usrMail.Inbox = true;
							usrMail.Send();

							alertedUsrs.Add(ca.UsrK);
						}
					}
					catch (Exception ex) { Bobs.Global.Log("9a9f2ee1-41ea-4e2b-8187-caa75ddd1b8e - UsrK=" + postingUsr.K.ToString() + " ThreadK=" + parentThread.K.ToString(), ex); }
				}
			}
			#endregion
		}
		#endregion
		#region ThreadSendInvites - removed because we're going to do it in the earlier invite section!
		//private static void ThreadSendInvites(Thread parentThread, List<int> alertedUsrKs, Usr postingUsr, List<int> inviteKs)
		//{
		//    #region Send invites
		//    try
		//    {
		//        if (inviteKs.Count > 0)
		//        {
		//            parentThread.Invite(
		//                inviteKs,
		//                postingUsr,
		//                parentThread.DateTime,
		//                alertedUsrKs,
		//                true,
		//                null);
		//        }
		//    }
		//    catch (Exception ex) { Bobs.Global.Log("8add7788-a9de-4d35-99ca-120ee94919dc", ex); }
		//    #endregion
		//}
		#endregion
		#region ThreadSendGroupNewsAdminsAReminderIfIsRecommendedNews
		private static void ThreadSendGroupNewsAdminsAReminderIfIsRecommendedNews(Thread parentThread)
		{
			#region If this is recommended news, send a reminder to all group news admins
			try
			{
				if (parentThread.GroupK > 0 && parentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended))
				{
					parentThread.SendGroupNewsModNewNewsAlerts();
				}
			}
			catch (Exception ex) { Bobs.Global.Log("71dee799-2ddf-4f49-9b82-dcfc49f6a32e", ex); }
			#endregion
		}
		#endregion
		#region ThreadSendNewsAlerts
		private static void ThreadSendNewsAlerts(Thread parentThread)
		{
			#region SendNewsAlerts()
			try
			{
				if (parentThread.IsNews && parentThread.GroupK > 0)
				{
					SendNewsAlertsJob job = new SendNewsAlertsJob(parentThread);
					job.ExecuteSynchronously();
				}
			}
			catch (Exception ex) { Bobs.Global.Log("64f76097-9bc6-4b63-a7a4-b52eb649534c", ex); }
			#endregion
		}
		#endregion

		#region SendChatMessage
		public void SendChatMessage(bool disableChatMessage, bool alreadySentChatMessage, Comment newComment, Thread parentThread, Usr postingUsr, string chatMessageBody)
		{
			if (!disableChatMessage && !alreadySentChatMessage)
			{
				Comment.sendChatMessageToParticipantsNow(null, newComment, parentThread, postingUsr, chatMessageBody);
			}
		}
		#endregion
		#region UpdateTotalParticipants
		private static void UpdateTotalParticipants(Thread newThread)
		{
			try
			{
				UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob(newThread);
				job.ExecuteSynchronously();
			}
			catch (Exception ex) { Bobs.Global.Log("572d2bb0-bcd8-4e88-a3d3-ab25d00db417", ex); }
			
		}
		#endregion

	}
	#endregion
	#region ForumStats
	public class ForumStats : Bobs.ThreadSet
	{
		public ForumStats(Query query) : base(query){}

		public static Hashtable ExtraSelectElements
		{
			get
			{
				Hashtable h = new Hashtable();
				h["TotalComments"]="SUM([Thread].[TotalComments])";
				h["AverageCommentDateTime"]="CAST(SUM(CAST([Thread].[DateTime] AS DECIMAL(18,12)) * [Thread].[TotalComments]) / SUM([Thread].[TotalComments]) AS DATETIME)";
				h["LastPost"]="MAX([Thread].[LastPost])";
				return h;
			}
		}
		
		public int TotalComments
		{
			get
			{
				if (this[0].ExtraSelectElements["TotalComments"].Equals(DBNull.Value))
					return 0;
				else
					return (int)this[0].ExtraSelectElements["TotalComments"];
			}
		}

		public DateTime AverageCommentDateTime
		{
			get
			{
				if (this[0].ExtraSelectElements["AverageCommentDateTime"].Equals(DBNull.Value))
					return DateTime.MinValue;
				else
					return (DateTime)this[0].ExtraSelectElements["AverageCommentDateTime"];
			}
		}

		public DateTime LastPost
		{
			get
			{
				if (this[0].ExtraSelectElements["LastPost"].Equals(DBNull.Value))
					return DateTime.MinValue;
				else
					return (DateTime)this[0].ExtraSelectElements["LastPost"];
			}
		}
	}
	#endregion
	#region ThreadStats
	public class ThreadStats : Bobs.CommentSet
	{
		public ThreadStats(Query query) : base(query){}

		public static Hashtable ExtraSelectElements
		{
			get
			{
				Hashtable h = new Hashtable();
				h["AverageCommentDateTime"]="CAST(AVG(CAST([Comment].[DateTime] AS DECIMAL(18,12))) AS DATETIME)";
				h["CommentCount"] = "COUNT(*)";
				return h;
			}
		}

		public DateTime AverageCommentDateTime
		{
			get
			{
				return (DateTime)this[0].ExtraSelectElements["AverageCommentDateTime"];
			}
		}

		public int CommentCount
		{
			get
			{
				return (int)this[0].ExtraSelectElements["CommentCount"];
			}
		}
	}
	#endregion

	//[TestFixture]
	//public class CommentTests
	//{
	//    [Test]
	//    public void TestVideoRegex()
	//    {
	//        Comment c = new Comment();
	//        c.Text = "foo <dsi:video type=\"youtube\" ref=\"MkZNakj3uko\" /> bar";

	//        Console.WriteLine(c.Html);
	//        Assert.IsTrue(c.Html.Contains("MATCH"));
	//    }
	//}

}

