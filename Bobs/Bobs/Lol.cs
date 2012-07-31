using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
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
using System.Xml;
using SpottedScript.Controls.ChatClient.Shared;

namespace Bobs
{

	#region Lol
	/// <summary>
	/// e.g. Promoter / Event Lol
	/// </summary>
	[Serializable] 
	public partial class Lol
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Lol.Columns.K] as int? ?? 0; }
			set { this[Lol.Columns.K] = value; }
		}
		/// <summary>
		/// The user that laughed
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Lol.Columns.UsrK]; }
			set { usr = null; this[Lol.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The comment that they laughed at
		/// </summary>
		public override int CommentK
		{
			get { return (int)this[Lol.Columns.CommentK]; }
			set { comment = null; this[Lol.Columns.CommentK] = value; }
		}
		/// <summary>
		/// The Usr that posted the comment
		/// </summary>
		public override int CommentUsrK
		{
			get { return (int)this[Lol.Columns.CommentUsrK]; }
			set { commentUsr = null; this[Lol.Columns.CommentUsrK] = value; }
		}
		/// <summary>
		/// The datetime that they laughed
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[Lol.Columns.DateTime]; }
			set { this[Lol.Columns.DateTime] = value; }
		}
		#endregion

		public static void CreateLol(Comment comment)
		{
			LolSet ls = new LolSet(
				new Query(
					new And(
						new Q(Lol.Columns.CommentK,comment.K),
						new Q(Lol.Columns.UsrK,Usr.Current.K)
					)
				)
			);
			if (ls.Count==0)
			{
				Usr usr = new Usr(comment.UsrK);
				LolSet lolUniqueSet = new LolSet(new Query(new And(
					new Q(Lol.Columns.CommentUsrK,usr.K),
					new Q(Lol.Columns.UsrK,Usr.Current.K))));
				if (lolUniqueSet.Count==0)
					usr.UniqueMadeLol++;
				usr.TotalMadeLol++;
				usr.Update();

				comment.LolCount++;
				comment.Update();

				Lol l = new Lol();
				l.DateTime=DateTime.Now;
				l.CommentK=comment.K;
				l.UsrK=Usr.Current.K;
				l.CommentUsrK=comment.UsrK;
				l.Update();
				l = new Lol(l.K);
				
				//DateTime lastLolDateTime = Usr.Current.LastLol;
						
				Usr.Current.TotalLol++;
				Usr.Current.LastLol = l.DateTime;
				Usr.Current.Update();

				Comment fullComment = new Comment(comment.K);

				if (!fullComment.Thread.Private && !fullComment.Thread.GroupPrivate && !fullComment.Thread.PrivateGroup)
				{

					if (Usr.Current.FacebookConnected && Usr.Current.FacebookStoryLaugh)
					{
						FacebookPost.CreateLaugh(Usr.Current, comment);
					}

					////LaughStub randomChatLaughStub = getLaughStub(ItemType.LaughAlert, new Chat.RoomSpec(RoomType.Laughs).Guid, fullComment);
					//LaughStub randomChatLaughStub = getLaughStub(ItemType.LaughAlert, new Chat.RoomSpec(RoomType.PublicStream).Guid, fullComment);
					//Chat.SendJsonChatItem(randomChatLaughStub);

					LaughStub randomChatLaughStub1 = getLaughStub(ItemType.LaughAlert, new Chat.RoomSpec(RoomType.PublicStream).Guid, fullComment);
					Chat.SendJsonChatItem(randomChatLaughStub1);
				}
				else
				{
					//LaughStub laughStub = getLaughStub(ItemType.LaughAlert, new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Thread, fullComment.ThreadK).Guid, fullComment);
					//UsrSet us = Thread.GetAllLoggedInParticipants(fullComment.Thread);
					//Chat.SendJsonChatItem(laughStub, us);
				}

			}
		}
		static LaughStub getLaughStub(ItemType itemType, Guid roomGuid, Comment fullComment)
		{
			return new LaughStub(
						Guid.NewGuid().Pack(),
						itemType,
						DateTime.Now.Ticks.ToString(),
						roomGuid.Pack(),
						Usr.Current.NickName,
						Usr.Current.StmuParams,
						Usr.Current.K,
						Usr.Current.HasPic ? Usr.Current.Pic.ToString() : "0",
						Usr.Current.HasChatPic ? Usr.Current.ChatPic.Value.ToString() : "0",
						Chat.GetMessageFromCommentBody(fullComment.Text),
						fullComment.Thread.GetRoomSpec().Guid.Pack(),
						fullComment.UrlRefresher(),
						fullComment.Thread.Subject.TruncateWithDots(50));
		}

		
		#region Links to Bob
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr==null && UsrK>0)
					usr = new Usr(UsrK);
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		private Usr usr;
		#endregion
		#region Comment
		public Comment Comment
		{
			get
			{
				if (comment==null && CommentK>0)
					comment = new Comment(CommentK);
				return comment;
			}
			set
			{
				comment = value;
			}
		}
		private Comment comment;
		#endregion
		#region CommentUsr
		public Usr CommentUsr
		{
			get
			{
				if (commentUsr==null && CommentUsrK>0)
					commentUsr = new Usr(CommentUsrK);
				return commentUsr;
			}
			set
			{
				commentUsr = value;
			}
		}
		private Usr commentUsr;
		#endregion
		#endregion

	}
	#endregion

}
