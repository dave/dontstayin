using System;
using System.Web;
using Bobs.Jobs;

namespace Bobs
{
	/// <summary>
	/// Links a user to many photos (photos of me)
	/// </summary>
	[Serializable]
	public partial class UsrDate 
	{

		#region simple members
		/// <summary>
		/// Link to Usr table - the usr that initiated the date request
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[UsrDate.Columns.UsrK]; }
			set { this.usr = null; this[UsrDate.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to Usr table - the usr that they want to be introduced to
		/// </summary>
		public override int DateUsrK
		{
			get { return (int)this[UsrDate.Columns.DateUsrK]; }
			set { this.dateUsr = null; this[UsrDate.Columns.DateUsrK] = value; }
		}
		/// <summary>
		/// Status
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[UsrDate.Columns.Status]; }
			set { this[UsrDate.Columns.Status] = value; }
		}
		/// <summary>
		/// DateTime that the request was done
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[UsrDate.Columns.DateTime]; }
			set { this[UsrDate.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Is this Yes UsrDate record matched to a similar 'reverse' Yes record?
		/// </summary>
		public override bool PreMatch
		{
			get { return (bool)this[UsrDate.Columns.PreMatch]; }
			set { this[UsrDate.Columns.PreMatch] = value; }
		}
		/// <summary>
		/// Are these users fully matched? 0 = no match, 1 = matched, 3 = was matched, but change of prefs means not any more
		/// </summary>
		public override MatchEnum Match
		{
			get { return (MatchEnum)this[UsrDate.Columns.Match]; }
			set { this[UsrDate.Columns.Match] = value; }
		}
		/// <summary>
		/// The datetime when the users were first fully matched.
		/// </summary>
		public override DateTime MatchDateTime
		{
			get { return (DateTime)this[UsrDate.Columns.MatchDateTime]; }
			set { this[UsrDate.Columns.MatchDateTime] = value; }
		}
		/// <summary>
		/// This it the ThreadK of the private message thread that we start when they are matched.
		/// </summary>
		public override int MatchThreadK
		{
			get { return (int)this[UsrDate.Columns.MatchThreadK]; }
			set { this.matchThread = null; this[UsrDate.Columns.MatchThreadK] = value; }
		}
		#endregion


		public static Join DateUsrJoin
		{
			get
			{
				return new Join(UsrDate.Columns.DateUsrK, Usr.Columns.K);
			}
		}

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (this.usr == null)
					this.usr = new Usr(this.UsrK);
				return this.usr;
			}
		}
		Usr usr;
		#endregion
		#region DateUsr
		public Usr DateUsr
		{
			get
			{
				if (this.dateUsr == null)
					this.dateUsr = new Usr(this.DateUsrK);
				return this.dateUsr;
			}
		}
		Usr dateUsr;
		#endregion
		#endregion

		partial void BeforeUpdate(Transaction t)
		{
			this.DateTime = DateTime.Now;
		}
		

		#region Bobs
		#region MatchThread
		public Thread MatchThread
		{
			get
			{
				if (this.matchThread == null)
					this.matchThread = new Thread(this.MatchThreadK);
				return this.matchThread;
			}
			set
			{
				this.matchThread = value;
			}
		}
		private Thread matchThread;
		#endregion
		#endregion

		#region UpdatePreMatch()
		public void UpdatePreMatch()
		{
			try
			{
				UsrDate ud = new UsrDate(this.DateUsrK, this.UsrK);
				if (ud.Status.Equals(StatusEnum.Yes) && this.Status.Equals(StatusEnum.Yes))
				{
					this.PreMatch = true;
					this.Update();
					ud.PreMatch = true;
					ud.Update();
				}
				else
				{
					this.PreMatch = false;
					this.Update();
					ud.PreMatch = false;
					ud.Update();
				}
			}
			catch
			{
				this.PreMatch = false;
				this.Update();
			}
		}
		#endregion
		public void SendMatchNotification()
		{
			#region Init thread
			Thread t = new Thread();
			t.DateTime = DateTime.Now;
			t.Enabled = true;
			t.ParentObjectK = 0;
			t.ParentObjectType = Model.Entities.ObjectType.None;
			t.Subject = "DSI Date introduction - " + this.Usr.NickName + " and " + this.DateUsr.NickName;
			t.UsrK = this.UsrK;
			t.Private = true;
			t.IsNews = false;
			t.IsSticky = false;
			t.Update();
			t.UpdateAncestorLinks(null);

			ThreadUsr tu = new ThreadUsr();
			tu.DateTime = DateTime.Now;
			tu.InvitingUsrK = this.UsrK;
			tu.UsrK = this.DateUsrK;
			tu.ThreadK = t.K;
			tu.ChangeStatus(ThreadUsr.StatusEnum.NewInvite);
			tu.StatusChangeObjectK = this.UsrK;
			tu.StatusChangeObjectType = Model.Entities.ObjectType.Usr;
			tu.PrivateChatType = ThreadUsr.PrivateChatTypes.Popup;
			tu.Update();

			ThreadUsr tu1 = new ThreadUsr();
			tu1.DateTime = DateTime.Now;
			tu1.InvitingUsrK = this.DateUsrK;
			tu1.UsrK = this.UsrK;
			tu1.ThreadK = t.K;
			tu1.ChangeStatus(ThreadUsr.StatusEnum.NewInvite);
			tu1.PrivateChatType = ThreadUsr.PrivateChatTypes.Popup;
			tu1.StatusChangeObjectK = this.UsrK;
			tu1.StatusChangeObjectType = Model.Entities.ObjectType.Usr;
			tu1.Update();

			Comment c = new Comment();
			c.DateTime = DateTime.Now;
			if (HttpContext.Current != null)
				c.Ip = Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
			c.Enabled = true;
			string rel = "";
			if (this.Usr.Relationship1 && this.DateUsr.Relationship1)
				rel += " <b>just friends</b>";
			if (this.Usr.Relationship2 && this.DateUsr.Relationship2)
				rel += (rel.Length > 0 ? " or " : "") + " <b>a bit of a fling</b>";
			if (this.Usr.Relationship3 && this.DateUsr.Relationship3)
				rel += (rel.Length > 0 ? " or " : "") + " <b>love</b>";
			c.Text = "<b>You've both been matched by DSI Date.</b>\n\nYou've selected Yes to each others profiles on the DSI Date page. You're both looking for: " + rel + ".\n\nGet to know each other better in this private conversation.";
			c.ThreadK = t.K;
			c.UsrK = 3526;
			c.DuplicateGuid = Guid.NewGuid();
			c.Update();
			c = new Comment(c.K);

			CommentAlert.Enable(this.Usr, t.K, Model.Entities.ObjectType.Thread);
			CommentAlert.Enable(this.DateUsr, t.K, Model.Entities.ObjectType.Thread);

			t.LastPost = c.DateTime;
			t.LastPostUsrK = c.UsrK;
			t.Update();
			t.UpdateTotalComments(null);
			//t.UpdateTotalParticipants();
			UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob();
			job.ExecuteSynchronously();

			#endregion

			this.MatchThreadK = t.K;
			this.Update();
			UsrDate udRev = new UsrDate(this.DateUsrK, this.UsrK);
			udRev.MatchThreadK = t.K;
			udRev.Update();

			//Email
			Mailer sm = new Mailer();
			sm.RedirectUrl = t.Url();
			sm.Subject = "DSI Date has matched you to " + this.DateUsr.NickName;
			sm.Body = "<p>DSI Date has matched you to " + this.DateUsr.NickNameSafe + ". We've invited you both to a private conversation, where you can get to know each other better.</p>";
			sm.Body += "<p><a href=\"[LOGIN(" + t.Url() + ")]\">Click here to send " + this.DateUsr.NickNameSafe + " a message</a></p>";
			sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
			sm.UsrRecipient = this.Usr;
			sm.To = this.Usr.Email;
			sm.Send();

			Mailer sm1 = new Mailer();
			sm1.RedirectUrl = t.Url();
			sm1.Subject = "DSI Date has matched you to " + this.Usr.NickName;
			sm1.Body = "<p>DSI Date has matched you to " + this.Usr.NickNameSafe + ". We've invited you both to a private conversation, where you can get to know each other better.</p>";
			sm1.Body += "<p><a href=\"[LOGIN(" + t.Url() + ")]\">Click here to send " + this.Usr.NickNameSafe + " a message</a></p>";
			sm1.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
			sm1.UsrRecipient = this.DateUsr;
			sm1.To = this.DateUsr.Email;
			sm1.Send();

		}

	}
}
