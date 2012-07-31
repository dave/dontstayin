using System;
using System.Collections.Generic;

namespace Bobs
{
	/// <summary>
	/// Links a private thread to many users
	/// </summary>
	[Serializable] 
	public partial class ThreadUsr 
	{

		#region simple members
		/// <summary>
		/// The thread
		/// </summary>
		public override int ThreadK
		{
			get { return (int)this[ThreadUsr.Columns.ThreadK]; }
			set { this.thread = null; this[ThreadUsr.Columns.ThreadK] = value; }
		}
		/// <summary>
		/// The user that has been invited
		/// </summary>
		public override int UsrK
		{
			get { return (int)(this[ThreadUsr.Columns.UsrK] ?? 0); }
			set { this.usr = null; this[ThreadUsr.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The user that made the invitation
		/// </summary>
		public override int InvitingUsrK
		{
			get { return (int)this[ThreadUsr.Columns.InvitingUsrK]; }
			set { this.invitingUsr = null; this[ThreadUsr.Columns.InvitingUsrK] = value; }
		}
		/// <summary>
		/// 0=New, 1=Accepted
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[ThreadUsr.Columns.Status]; }
			set { this[ThreadUsr.Columns.Status] = value; }
		}
		/// <summary>
		/// Datetime invitation made
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[ThreadUsr.Columns.DateTime]; }
			set { this[ThreadUsr.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Type of private chat alert
		/// </summary>
		public override PrivateChatTypes PrivateChatType
		{
			get { return (PrivateChatTypes)this[ThreadUsr.Columns.PrivateChatType]; }
			set { this[ThreadUsr.Columns.PrivateChatType] = value; }
		}
		/// <summary>
		/// Favourite topic?
		/// </summary>
		public override bool Favourite
		{
			get { return (bool)this[ThreadUsr.Columns.Favourite]; }
			set { this[ThreadUsr.Columns.Favourite] = value; }
		}
		/// <summary>
		/// Deleted invitation?
		/// </summary>
		public override bool Deleted
		{
			get { return (bool)this[ThreadUsr.Columns.Deleted]; }
			set { this[ThreadUsr.Columns.Deleted] = value; }
		}
		/// <summary>
		/// The datetime that the thread was last viewed
		/// </summary>
		public override DateTime ViewDateTime
		{
			get { return (DateTime)this[ThreadUsr.Columns.ViewDateTime]; }
			set { this[ThreadUsr.Columns.ViewDateTime] = value; }
		}
		/// <summary>
		/// The new datetime (when this is set, it's value is copied to the DateTime if it is more than 5 mins ago) 
		/// </summary>
		public override DateTime ViewDateTimeLatest
		{
			get { return (DateTime)this[ThreadUsr.Columns.ViewDateTimeLatest]; }
			set { this[ThreadUsr.Columns.ViewDateTimeLatest] = value; }
		}
		/// <summary>
		/// The number of comments that have been viewed at the time of the ViewDateTime
		/// </summary>
		public override int ViewComments
		{
			get { return (int)this[ThreadUsr.Columns.ViewComments]; }
			set { this[ThreadUsr.Columns.ViewComments] = value; }
		}
		/// <summary>
		/// The number of comments that have been viewed at the time of the ViewDateTimeLatest
		/// </summary>
		public override int ViewCommentsLatest
		{
			get { return (int)this[ThreadUsr.Columns.ViewCommentsLatest]; }
			set { this[ThreadUsr.Columns.ViewCommentsLatest] = value; }
		}
		/// <summary>
		/// Datetime of last status change
		/// </summary>
		public override DateTime StatusChangeDateTime
		{
			get { return (DateTime)this[ThreadUsr.Columns.StatusChangeDateTime]; }
			set { this[ThreadUsr.Columns.StatusChangeDateTime] = value; }
		}
		/// <summary>
		/// Type of object that caused the previous status change
		/// </summary>
		public override Model.Entities.ObjectType StatusChangeObjectType
		{
			get { return (Model.Entities.ObjectType)this[ThreadUsr.Columns.StatusChangeObjectType]; }
			set { this[ThreadUsr.Columns.StatusChangeObjectType] = value; }
		}
		/// <summary>
		/// Key of the object that caused the previous status change
		/// </summary>
		public override int StatusChangeObjectK
		{
			get { return (int)this[ThreadUsr.Columns.StatusChangeObjectK]; }
			set { this[ThreadUsr.Columns.StatusChangeObjectK] = value; }
		}
		#endregion

		#region IsNew
		/// <summary>
		/// This is set to true by GetThreadUsr() when we've created a new 
		/// ThreadUsr. It's not persisted in the database.
		/// </summary>
		public bool IsNew
		{
			get
			{
				return this.isNew;
			}
			set
			{
				this.isNew = value;
			}
		}
		private bool isNew = false;
		#endregion

		#region JoinedBuddy
		public Buddy JoinedBuddy
		{
			get
			{
				if (this.joinedBuddy==null)
				{
					this.joinedBuddy = new Buddy(this, ThreadUsr.Columns.UsrK);
				}
				return this.joinedBuddy;
			}
			set
			{
				this.joinedBuddy = value;
			}
		}
		private Buddy joinedBuddy;
		#endregion

		#region ChangeStatus
		public void ChangeStatus(StatusEnum NewStatus, DateTime ChangeDateTime, bool Update, bool joinOrExitChatRoom)
		{
			if (!this.Status.Equals(NewStatus))
			{
				bool isWatchingBefore = this.IsWatching;

				this.Status = NewStatus;
				this.StatusChangeDateTime = ChangeDateTime;

				if (Update)
					this.Update();

				if (joinOrExitChatRoom && this.IsWatching != isWatchingBefore)
				{
					Guid room = this.Thread.GetRoomSpec().Guid;
					if (this.IsWatching)
						Chat.JoinRoom(room, this.UsrK);
					else
						Chat.ExitRoom(room, this.UsrK);
				}

			}
		}
		public void ChangeStatus(StatusEnum NewStatus, DateTime ChangeDateTime)
		{
			this.ChangeStatus(NewStatus, ChangeDateTime, false, true);
		}
		public void ChangeStatus(StatusEnum NewStatus, bool Update)
		{
			this.ChangeStatus(NewStatus, DateTime.Now, Update, true);
		}
		public void ChangeStatus(StatusEnum NewStatus)
		{
			this.ChangeStatus(NewStatus, DateTime.Now, false, true);
		}
		#endregion

		#region StatusChangeObject
		public IBob StatusChangeObject
		{
			get
			{
				if (this.statusChangeObject==null && this.StatusChangeObjectK>0)
				{
					this.statusChangeObject = Bob.Get(this.StatusChangeObjectType,this.StatusChangeObjectK);
				}
				return this.statusChangeObject;
			}
		}
		private IBob statusChangeObject;
		#endregion

		#region PrivateCanSee
		public bool PrivateCanSee
		{
			get
			{
				return (this.Status.Equals(ThreadUsr.StatusEnum.Ignore) ||
				        this.Status.Equals(ThreadUsr.StatusEnum.Archived) ||
				        this.Status.Equals(ThreadUsr.StatusEnum.NewComment) ||
				        this.Status.Equals(ThreadUsr.StatusEnum.UnArchived) ||
				        this.Status.Equals(ThreadUsr.StatusEnum.NewGroupNewsAlert) ||
				        this.Status.Equals(ThreadUsr.StatusEnum.NewInvite) ||
				        this.Status.Equals(ThreadUsr.StatusEnum.NewWatchedForumAlert));
			}
		}
		public static Q PrivateCanSeeQ
		{
			get
			{
				return new Or(
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.Ignore),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.Archived),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewComment),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.UnArchived),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewGroupNewsAlert),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewInvite),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewWatchedForumAlert));
			}
		}
		#endregion
		#region Watching
		public static Q WatchingQ
		{
			get
			{
				return new Or(
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.Archived), 
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewComment), 
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.UnArchived), 
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewGroupNewsAlert), 
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewInvite), 
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewWatchedForumAlert)); 
			}
		}
		public bool IsWatching
		{
			get
			{
				return WatchingStatuses.Contains(this.Status);
				//(
				//Status.Equals(ThreadUsr.StatusEnum.Archived) ||
				//Status.Equals(ThreadUsr.StatusEnum.NewComment) ||
				//Status.Equals(ThreadUsr.StatusEnum.UnArchived) ||
				//Status.Equals(ThreadUsr.StatusEnum.NewGroupNewsAlert) ||
				//Status.Equals(ThreadUsr.StatusEnum.NewInvite) ||
				//Status.Equals(ThreadUsr.StatusEnum.NewWatchedForumAlert));
			}
		}
		public static List<StatusEnum> WatchingStatuses
		{
			get
			{
				return new List<StatusEnum>()
				{
					ThreadUsr.StatusEnum.Archived, 
					ThreadUsr.StatusEnum.NewComment, 
					ThreadUsr.StatusEnum.UnArchived, 
					ThreadUsr.StatusEnum.NewGroupNewsAlert, 
					ThreadUsr.StatusEnum.NewInvite, 
					ThreadUsr.StatusEnum.NewWatchedForumAlert
				};
			}
		}
		#endregion
		#region Inbox
		public static Q InboxQ
		{
			get
			{
				return new Or(
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewComment),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.UnArchived),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewGroupNewsAlert),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewInvite),
					new Q(ThreadUsr.Columns.Status,ThreadUsr.StatusEnum.NewWatchedForumAlert));
			}
		}
		public bool IsInbox
		{
			get
			{
				return InboxStatuses.Contains(this.Status);
				//Status.Equals(ThreadUsr.StatusEnum.NewComment) ||
				//Status.Equals(ThreadUsr.StatusEnum.UnArchived) ||
				//Status.Equals(ThreadUsr.StatusEnum.NewGroupNewsAlert) ||
				//Status.Equals(ThreadUsr.StatusEnum.NewInvite) ||
				//Status.Equals(ThreadUsr.StatusEnum.NewWatchedForumAlert));
			}
		}
		public static List<StatusEnum> InboxStatuses
		{
			get
			{
				return new List<StatusEnum>()
				{
					ThreadUsr.StatusEnum.NewComment, 
					ThreadUsr.StatusEnum.UnArchived, 
					ThreadUsr.StatusEnum.NewGroupNewsAlert, 
					ThreadUsr.StatusEnum.NewInvite, 
					ThreadUsr.StatusEnum.NewWatchedForumAlert
				};
			}
		}
		#endregion

		#region Set
		/// <summary>
		/// Use this to set the ViewDateTime(s).
		/// </summary>
		public void Set(int comments)
		{
			//	if (ViewDateTimeLatest == DateTime.MinValue)
			//	{
			//		ViewDateTime = DateTime.Now;
			//		ViewComments = comments;
			//	}
			//	else 
			if (this.ViewDateTimeLatest.AddMinutes(5)<DateTime.Now)
			{
				this.ViewDateTime = this.ViewDateTimeLatest;
				this.ViewComments = this.ViewCommentsLatest;
			}
			this.ViewDateTimeLatest = DateTime.Now;
			if (comments>this.ViewCommentsLatest)
				this.ViewCommentsLatest = comments;
			this.Update();
		}
		#endregion
		#region ViewDateTimeInUse
		/// <summary>
		/// This is the DateTime to use when marking a message as read or not.
		/// </summary>
		public DateTime ViewDateTimeInUse
		{
			get
			{
				//if (ViewDateTimeLatest.AddMinutes(5)<DateTime.Now || !IsInitialised)
				if (this.ViewDateTimeLatest.AddMinutes(5)<DateTime.Now && this.IsInitialised)
					return this.ViewDateTimeLatest;
				else
					return this.ViewDateTime;
			}
		}
		#endregion
		#region ViewCommentsInUse
		/// <summary>
		/// This is the DateTime to use when marking a message as read or not.
		/// </summary>
		public int ViewCommentsInUse
		{
			get
			{
				//if (ViewDateTimeLatest.AddMinutes(5)<DateTime.Now && IsInitialised) // this doesn't work when we view half a thread then wait 5 mins
				if (this.ViewDateTimeLatest.AddMinutes(5)<DateTime.Now)
					return this.ViewCommentsLatest;
				else if (this.IsInitialised)
					return this.ViewComments;
				else
					return 0;
			}
		}
		#endregion
		#region IsInitialised
		public bool IsInitialised
		{
			get
			{
				return !this.ViewDateTime.Equals(DateTime.MinValue);
			}
		}
		#endregion


		#region Links to Bobs
		#region Thread
		/// <summary>
		/// The thread
		/// </summary>
		public Thread Thread
		{
			get
			{
				if (this.thread==null)
					this.thread = new Thread(this.ThreadK, this, ThreadUsr.Columns.ThreadK);
				return this.thread;
			}
		}
		Thread thread;
		#endregion

		#region Usr
		/// <summary>
		/// The user that has been invited
		/// </summary>
		public Usr Usr
		{
			get
			{
				if (this.usr==null && this.UsrK>0)
					this.usr = new Usr(this.UsrK, this, ThreadUsr.Columns.UsrK);
				return this.usr;
			}
		}
		Usr usr;
		#endregion

		#region InvitingUsr
		/// <summary>
		/// The user that made the invitation
		/// </summary>
		public Usr InvitingUsr
		{
			get
			{
				if (this.invitingUsr==null && this.InvitingUsrK>0)
					this.invitingUsr = new Usr(this.InvitingUsrK, this, ThreadUsr.Columns.InvitingUsrK);
				return this.invitingUsr;
			}
		}
		Usr invitingUsr;
		#endregion

		#endregion
		
	}
}
