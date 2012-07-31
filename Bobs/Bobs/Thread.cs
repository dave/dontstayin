using System;
using System.Collections;
using System.Data;
using System.Text;

using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Cambro.Misc;
using Cambro.Web;
using System.Collections.Generic;
//using Quartz;
using Common.Collections;
using Bobs.JobProcessor;
using Bobs.Jobs;
using System.Web.UI;
using SpottedScript.Controls.ChatClient.Shared;

namespace Bobs
{

	#region Thread
	/// <summary>
	/// Links to one Photo OR Event OR Venue OR Place and contains many Comments
	/// </summary>
	[Serializable]
	public partial class Thread : IPage, IName, IReadableReference, IBobType, IRelevanceContributor, IArchive, IDeleteAll, IConnectedTo, IHasParentDiscussable, IHasParent
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Thread.Columns.K] as int? ?? 0; }
			set { this[Thread.Columns.K] = value; }
		}
		/// <summary>
		/// Subject of the thread
		/// </summary>
		public override string Subject
		{
			get { return (string)this[Thread.Columns.Subject]; }
			set { this[Thread.Columns.Subject] = value; }
		}
		/// <summary>
		/// Which type of object is the parent of this thread? Photo=1, Event=2, Venue=3, Place=4, None=5, Country=7
		/// </summary>
		public override Model.Entities.ObjectType ParentObjectType
		{
			get { return (Model.Entities.ObjectType)this[Thread.Columns.ParentObjectType]; }
			set { this[Thread.Columns.ParentObjectType] = value; }
		}
		/// <summary>
		/// Key of record in parent table
		/// </summary>
		public override int ParentObjectK
		{
			get
			{
				//return (int)this[Thread.Columns.ParentObjectK] == 0 ? (int?)null : (int)this[Thread.Columns.ParentObjectK];
				return (int)this[Thread.Columns.ParentObjectK];
			}
			//set { parentObject = null; this[Thread.Columns.ParentObjectK] = value ?? 0; }
			set { parentDiscussable = null; this[Thread.Columns.ParentObjectK] = value; }
		}
		/// <summary>
		/// Links to one User
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Thread.Columns.UsrK]; }
			set { usr = null; this[Thread.Columns.UsrK] = value; }
		}
		/// <summary>
		/// If this is set to false, the thread will not be displayed (for disabling offensive posts)
		/// </summary>
		public override bool Enabled
		{
			get { return (bool)this[Thread.Columns.Enabled]; }
			set { this[Thread.Columns.Enabled] = value; }
		}
		/// <summary>
		/// Date/Time of last post - threads are ordered by this.
		/// </summary>
		public override DateTime LastPost
		{
			get { return (DateTime)this[Thread.Columns.LastPost]; }
			set { this[Thread.Columns.LastPost] = value; }
		}
		/// <summary>
		/// Usr that posted the last comment
		/// </summary>
		public override int LastPostUsrK
		{
			get { return (int)this[Thread.Columns.LastPostUsrK]; }
			set { lastPostUsr = null; this[Thread.Columns.LastPostUsrK] = value; }
		}
		/// <summary>
		/// Tracks the total number of comments in the thread
		/// </summary>
		public override int TotalComments
		{
			get { return (int)this[Thread.Columns.TotalComments]; }
			set { this[Thread.Columns.TotalComments] = value; }
		}
		/// <summary>
		/// The average date.time of all comments posted in this thread
		/// </summary>
		public override DateTime AverageCommentDateTime
		{
			get { return (DateTime)this[Thread.Columns.AverageCommentDateTime]; }
			set { this[Thread.Columns.AverageCommentDateTime] = value; }
		}
		/// <summary>
		/// Private threads can only be viewed by linked users, and the thread poster
		/// </summary>
		public override bool Private
		{
			get { return (bool)this[Thread.Columns.Private]; }
			set { this[Thread.Columns.Private] = value; }
		}
		/// <summary>
		/// In a group, private threads can only be viewed by group members
		/// </summary>
		public override bool GroupPrivate
		{
			get { return (bool)this[Thread.Columns.GroupPrivate]; }
			set { this[Thread.Columns.GroupPrivate] = value; }
		}
		/// <summary>
		/// All threads in a private chat group have this flag set
		/// </summary>
		public override bool PrivateGroup
		{
			get { return (bool)this[Thread.Columns.PrivateGroup]; }
			set { this[Thread.Columns.PrivateGroup] = value; }
		}
		/// <summary>
		/// The theme of the article - mostly for groups
		/// </summary>
		public override int ThemeK
		{
			get { return (int)this[Thread.Columns.ThemeK]; }
			set { theme = null; this[Thread.Columns.ThemeK] = value; }
		}
		/// <summary>
		/// The article that this thread is associated with
		/// </summary>
		public override int ArticleK
		{
			get { return (int)this[Thread.Columns.ArticleK]; }
			set {
				Article = null;
				this[Thread.Columns.ArticleK] = value;
			}
		}
		/// <summary>
		/// If this thread is linked to a photo, this is the key
		/// </summary>
		public override int PhotoK
		{
			get { return (int)this[Thread.Columns.PhotoK]; }
			set {
				Photo = null;
				this[Thread.Columns.PhotoK] = value;
			}
		}
		/// <summary>
		/// If this thread is linked to a event, this is the key
		/// </summary>
		public override int EventK
		{
			get { return (int)this[Thread.Columns.EventK]; }
			set {
				Event = null;
				this[Thread.Columns.EventK] = value;
			}
		}
		/// <summary>
		/// If this thread is linked to a venue, this is the key
		/// </summary>
		public override int VenueK
		{
			get { return (int)this[Thread.Columns.VenueK]; }
			set { 
				this[Thread.Columns.VenueK] = value;
			}
		}
		/// <summary>
		/// If this thread is linked to a place, this is the key
		/// </summary>
		public override int PlaceK
		{
			get { return (int)this[Thread.Columns.PlaceK]; }
			set { 
				this[Thread.Columns.PlaceK] = value;
			}
		}
		/// <summary>
		/// If this thread is linked to a country, this is the key
		/// </summary>
		public override int CountryK
		{
			get { return (int)this[Thread.Columns.CountryK]; }
			set { this[Thread.Columns.CountryK] = value; }
		}
		/// <summary>
		/// If this thread is directly linked to a brand (not in an event), this is the K
		/// </summary>
		public override int BrandK
		{
			get { return (int)this[Thread.Columns.BrandK]; }
			set { this[Thread.Columns.BrandK] = value; }
		}
		/// <summary>
		/// If this thread is in a group, this is the K
		/// </summary>
		public override int GroupK
		{
			get { return (int)this[Thread.Columns.GroupK]; }
			set { this[Thread.Columns.GroupK] = value; }
		}
		/// <summary>
		/// If this thread is in a music specific group, this is the K
		/// </summary>
		public override int MusicTypeK
		{
			get { return (int)this[Thread.Columns.MusicTypeK]; }
			set { this[Thread.Columns.MusicTypeK] = value; }
		}
		/// <summary>
		/// These are news threads posted by place admins... they show in bold in the discussions page, and the last 5 are displayed on the place home page.
		/// </summary>
		public override bool IsNews
		{
			get { return (bool)this[Thread.Columns.IsNews]; }
			set { this[Thread.Columns.IsNews] = value; }
		}
		/// <summary>
		/// Date/time that the comment was posted
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[Thread.Columns.DateTime]; }
			set { this[Thread.Columns.DateTime] = value; }
		}
		/// <summary>
		/// These are news threads posted by country admins...
		/// </summary>
		public override bool IsNationwideNews
		{
			get { return (bool)this[Thread.Columns.IsNationwideNews]; }
			set { this[Thread.Columns.IsNationwideNews] = value; }
		}
		/// <summary>
		/// Is this an event review?
		/// </summary>
		public override bool IsReview
		{
			get { return (bool)this[Thread.Columns.IsReview]; }
			set { this[Thread.Columns.IsReview] = value; }
		}
		/// <summary>
		/// Is this a sticky thread?
		/// </summary>
		public override bool IsSticky
		{
			get { return (bool)this[Thread.Columns.IsSticky]; }
			set { this[Thread.Columns.IsSticky] = value; }
		}
		/// <summary>
		/// Is this a sticky thread?
		/// </summary>
		public override bool IsWorldwideNews
		{
			get { return (bool)this[Thread.Columns.IsWorldwideNews]; }
			set { this[Thread.Columns.IsWorldwideNews] = value; }
		}
		/// <summary>
		/// Total number of participants
		/// </summary>
		public override int TotalParticipants
		{
			get { return (int)this[Thread.Columns.TotalParticipants]; }
			set { this[Thread.Columns.TotalParticipants] = value; }
		}
		/// <summary>
		/// UsrK of the other participant (for private messages between 2 users)
		/// </summary>
		public override int FirstParticipantUsrK
		{
			get { return (int)this[Thread.Columns.FirstParticipantUsrK]; }
			set { firstParticipantUsr = null; this[Thread.Columns.FirstParticipantUsrK] = value; }
		}
		/// <summary>
		/// Do we hide this thread from the Hot Topics lists?
		/// </summary>
		public override bool HideFromHighlights
		{
			get { return (bool)this[Thread.Columns.HideFromHighlights]; }
			set { this[Thread.Columns.HideFromHighlights] = value; }
		}
		/// <summary>
		/// DateTime used to order threads in the Latest "Hot topics" box: AverageDateTime.AddHours(TotalComments*2)
		/// </summary>
		public override DateTime HotTopicsOrder
		{
			get { return (DateTime)this[Thread.Columns.HotTopicsOrder]; }
			set { this[Thread.Columns.HotTopicsOrder] = value; }
		}
		/// <summary>
		/// The url fragment - so that the url can be generated without accessing parent database records
		/// </summary>
		public override string UrlFragment
		{
			get { return (string)this[Thread.Columns.UrlFragment]; }
			set { this[Thread.Columns.UrlFragment] = value; }
		}
		/// <summary>
		/// Only the owner may invite people to this thread (for private threads only)
		/// </summary>
		public override bool Sealed
		{
			get { return (bool)this[Thread.Columns.Sealed]; }
			set { this[Thread.Columns.Sealed] = value; }
		}
		/// <summary>
		/// Posting disabled
		/// </summary>
		public override bool Closed
		{
			get { return (bool)this[Thread.Columns.Closed]; }
			set { this[Thread.Columns.Closed] = value; }
		}
		/// <summary>
		/// News recommendation status - None=0, Recommended=1, Done=2
		/// </summary>
		public override NewsStatusEnum NewsStatus
		{
			get { return (NewsStatusEnum)this[Thread.Columns.NewsStatus]; }
			set { this[Thread.Columns.NewsStatus] = value; }
		}
		/// <summary>
		/// News importance level
		/// </summary>
		public override int NewsLevel
		{
			get { return (int)this[Thread.Columns.NewsLevel]; }
			set { this[Thread.Columns.NewsLevel] = value; }
		}
		/// <summary>
		/// News recommendation usr
		/// </summary>
		public override int NewsUsrK
		{
			get { return (int)this[Thread.Columns.NewsUsrK]; }
			set { this[Thread.Columns.NewsUsrK] = value; }
		}
		/// <summary>
		/// Total number of users that are watching this thread
		/// </summary>
		public override int TotalWatching
		{
			get { return (int)this[Thread.Columns.TotalWatching]; }
			set { this[Thread.Columns.TotalWatching] = value; }
		}
		/// <summary>
		/// News moderator assigned to moderate this news
		/// </summary>
		public override int NewsModeratorUsrK
		{
			get { return (int)this[Thread.Columns.NewsModeratorUsrK]; }
			set { newsModeratorUsr = null; this[Thread.Columns.NewsModeratorUsrK] = value; }
		}
		/// <summary>
		/// Who actually moderated the news
		/// </summary>
		public override int NewsModeratedByUsrK
		{
			get { return (int)this[Thread.Columns.NewsModeratedByUsrK]; }
			set { newsModeratedByUsr = null; this[Thread.Columns.NewsModeratedByUsrK] = value; }
		}
		/// <summary>
		/// Date/Time when the news was moderated
		/// </summary>
		public override DateTime NewsModerationDateTime
		{
			get { return (DateTime)this[Thread.Columns.NewsModerationDateTime]; }
			set { this[Thread.Columns.NewsModerationDateTime] = value; }
		}
		/// <summary>
		/// Is this thread in a caption competition?
		/// </summary>
		public override bool IsInCaptionCompetition
		{
			get { return (bool)this[Thread.Columns.IsInCaptionCompetition]; }
			set { this[Thread.Columns.IsInCaptionCompetition] = value; }
		}
	 
		#endregion
		 
		public Chat.RoomSpec GetRoomSpec()
		{
			Chat.RoomSpec roomSpec = new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Thread, this.K);
			try
			{
				//Lets work out the room... remember for photo / article primary threads, we post to the parent room, not the thread room.
				if (this.PhotoK > 0 && this.Photo != null && this.Photo.ThreadK == this.K)
				{
					roomSpec = new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Photo, this.PhotoK);
				}
				else if (this.ArticleK > 0 && this.Article != null && this.Article.ThreadK == this.K)
				{
					roomSpec = new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Article, this.ArticleK);
				}
			}
			catch { }

			return roomSpec;
		}

		#region GetAllLoggedInParticipants
		public static UsrSet GetAllLoggedInParticipants(Thread thread)
		{
			Query qLoggedInParticipants = new Query();
			qLoggedInParticipants.Columns = new ColumnSet(Usr.Columns.K);
			qLoggedInParticipants.TableElement = new Join
			(
				Usr.Columns.K,
				ThreadUsr.Columns.UsrK,
				new And
				(
					new Q(ThreadUsr.Columns.ThreadK, thread.K),
					ThreadUsr.WatchingQ
				)
			);
			if (!Vars.DevEnv)
				qLoggedInParticipants.QueryCondition = Usr.LoggedInChatQ;

			if (thread.GroupK > 0 && (thread.GroupPrivate || thread.PrivateGroup))
			{
				//lets only select those who are members of the group!
				qLoggedInParticipants.TableElement = new Join(
					qLoggedInParticipants.TableElement,
					new TableElement(TablesEnum.GroupUsr),
					QueryJoinType.Inner,
					new And(
						new Q(Usr.Columns.K, GroupUsr.Columns.UsrK, true),
						new Q(GroupUsr.Columns.GroupK, thread.GroupK),
						new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));
					
			}

			UsrSet loggedInParticipants = new UsrSet(qLoggedInParticipants);
			return loggedInParticipants;
		}
		#endregion

		#region EnableNews(int Level)
		public void EnableNews(int Level)
		{
			if (this.GroupK == 0)
			{
				this.IsNews = true;
				this.NewsLevel = Level;
				this.NewsModeratedByUsrK = Usr.Current.K;
				this.NewsModerationDateTime = DateTime.Now;
				this.NewsStatus = Thread.NewsStatusEnum.Done;
				this.Update();

				this.NewsUsr.NewsPermissionLevel++;
				this.NewsUsr.Update();

				Mailer m = new Mailer();
				m.Subject = "Your recent news posting";
				m.Body = "<p>Your news posting has been accepted.</p>";
				m.Body += "<p>Your news level rises by 1.</p>";
				m.Body += "<p>Each time you post news that's accepted, your news level goes up by 1. Posting news that is rejected by our moderators reduces your news level by 1.</p>";
				m.Body += "<p>When you reach a news level 10, your news will go live straight away (without waiting for our moderators to check it).</p>";
				m.Body += "<p><b>Your current news level is " + this.NewsUsr.NewsPermissionLevel + ".</b></p>";

				m.RedirectUrl = this.Url();
				m.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
				m.UsrRecipient = this.NewsUsr;
				m.Send();


			}
			else
			{
				bool sendAlerts = false;
				if (this.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended))
					sendAlerts = true;
				this.IsNews = true;
				this.NewsLevel = 10;
				this.NewsModeratedByUsrK = Usr.Current.K;
				this.NewsModerationDateTime = DateTime.Now;
				this.NewsStatus = Thread.NewsStatusEnum.Done;
				this.Update();

				if (sendAlerts)
				{
					Mailer m = new Mailer();
					m.Subject = "Your recent group news posting";
					m.Body = "<p>Your news group posting has been accepted.</p>";
					m.RedirectUrl = this.Url();
					m.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
					m.UsrRecipient = this.NewsUsr;
					m.Send();

					SendNewsAlertsJob sendNewsAlertsJob = new SendNewsAlertsJob(this);
					sendNewsAlertsJob.AddJobToRunOnceFinished(new UpdateTotalParticipantsJob(this));
					sendNewsAlertsJob.ExecuteAsynchronously();


				}
			}

		}
		#endregion
		#region DisableNews()
		public void DisableNews()
		{
			if (this.GroupK == 0)
			{
				this.IsNews = false;
				this.NewsLevel = 0;
				this.NewsModeratedByUsrK = Usr.Current.K;
				this.NewsModerationDateTime = DateTime.Now;
				this.NewsStatus = Thread.NewsStatusEnum.Done;
				this.Update();

				this.NewsUsr.NewsPermissionLevel--;
				this.NewsUsr.Update();

				Mailer m = new Mailer();
				m.Subject = "Your recent news posting";
				m.Body = "<p>Your news posting has been rejected.</p>";
				m.Body += "<p>Your news level drops by 1.</p>";
				m.Body += "<p>Each time you post news that's accepted, your news level goes up by 1. Posting news that is rejected by our moderators reduces your news level by 1.</p>";
				m.Body += "<p>When you reach a news level 10, your news will go live straight away (without waiting for our moderators to check it).</p>";
				m.Body += "<p><b>Your current news level is " + this.NewsUsr.NewsPermissionLevel + ".</b></p>";

				m.RedirectUrl = this.Url();
				m.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
				m.UsrRecipient = this.NewsUsr;
				m.Send();
			}
			else
			{
				bool sendAlerts = false;
				if (this.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended))
					sendAlerts = true;
				this.IsNews = false;
				this.NewsLevel = 0;
				this.NewsModeratedByUsrK = Usr.Current.K;
				this.NewsModerationDateTime = DateTime.Now;
				this.NewsStatus = Thread.NewsStatusEnum.Done;
				this.Update();

				if (sendAlerts)
				{
					Mailer m = new Mailer();
					m.Subject = "Your recent news posting";
					m.Body = "<p>Your news posting has been rejected.</p>";
					m.RedirectUrl = this.Url();
					m.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
					m.UsrRecipient = this.NewsUsr;
					m.Send();
				}
			}
		}
		#endregion

		#region SubjectSnip
		public string SubjectSnip(int length)
		{
			if (this.Subject.Length > length)
				return this.Subject.Substring(0, length - 3) + "...";
			else if (this.Subject.Replace("\n", "").Trim().Length == 0)
				return "[no subject]";
			else
				return this.Subject;
		}
		#endregion

		#region SubjectSafe
		public string SubjectSafe
		{
			get
			{
				if (this.Subject.Length > 0)
					return this.Subject;
				else
					return "[no subject]";
			}
		}
		#endregion

		#region GetCommentMaker()
		public Comment.Maker GetCommentMaker()
		{
			Comment.Maker m = new Comment.Maker();
			m.AlertedUsrs = new List<int>();
			m.NewThread = false;
			m.ParentThread = this;
			return m;
		}
		#endregion
		#region Maker
		public class Maker
		{
			public string Subject { get; set; }
			public string Body { get; set; }
			public Model.Entities.ObjectType ParentType { get; set; }

			public int ParentK { get; set; }
			#region ParentBob
			public IBob ParentBob
			{
				get
				{
					if (parentBob == null && ParentK > 0 && !ParentType.Equals(Model.Entities.ObjectType.None))
						parentBob = Bob.Get(ParentType, ParentK);
					return parentBob;
				}
				set
				{
					parentBob = value;
				}
			}
			private IBob parentBob;
			#endregion

			public int GroupK { get; set; }
			#region ParentGroup
			public Group ParentGroup
			{
				get
				{
					if (parentGroup == null && GroupK > 0)
						parentGroup = new Group(GroupK);
					return parentGroup;
				}
				set
				{
					parentGroup = value;
				}
			}
			private Group parentGroup;
			#endregion

			public object DuplicateGuid { get; set; }

			public bool News { get; set; }
			public bool Closed { get; set; }

			public bool Sealed { get; set; }
			public bool Private { get; set; }
			public Usr PostingUsr { get; set; }
			public bool Review { get; set; }
			#region InviteKs
			/// <summary>
			/// List of Usr.K's to invite
			/// </summary>
			public List<int> InviteKs
			{
				get
				{
					if (inviteKs == null)
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
			#region StartNewThread
			public bool RunAsync
			{
				get
				{
					return runAsync;
				}
				set
				{
					runAsync = value;
				}
			}
			private bool runAsync = true;
			#endregion

			public ThreadUsr CurrentThreadUsr { get; set; }
			public GroupUsr CurrentGroupUsr { get; set; }
			public Thread NewThread { get; set; }
			public bool DisableLiveChatMessage { get; set; }

			#region AlertedUsrs
			public List<int> AlertedUsrs
			{
				get
				{
					if (alertedUsrs == null)
						alertedUsrs = new List<int>();
					return alertedUsrs;
				}
				set
				{
					alertedUsrs = value;
				}
			}
			private List<int> alertedUsrs;
			#endregion

			#region Post()
			public MakerReturn Post()
			{
				MakerReturn r = new MakerReturn();
				#region look for duplicate
				Query qDup = new Query();
				qDup.TopRecords = 1;
				qDup.Columns = new ColumnSet(Bobs.Comment.Columns.K, Bobs.Comment.Columns.ThreadK);
				qDup.QueryCondition = new Q(Bobs.Comment.Columns.DuplicateGuid, (Guid)DuplicateGuid);
				CommentSet csDup = new CommentSet(qDup);
				if (csDup.Count > 0)
				{
					r.Success = false;
					r.MessageHtml = "Duplicate thread detected";
					r.Thread = csDup[0].Thread;
					r.Duplicate = true;
					return r;
				}
				#endregion
				#region ensure two parents are different types...
				if (ParentType.Equals(Model.Entities.ObjectType.Group) && GroupK != ParentK)
				{
					r.Success = false;
					r.MessageHtml = "Posting to group, but GroupK!=ParentK";
					return r;
				}
				#endregion
				#region Make sure Group and ParentType are syncronised
				if (GroupK > 0 && ParentType.Equals(Model.Entities.ObjectType.None))
				{
					ParentType = Model.Entities.ObjectType.Group;
					ParentK = GroupK;
				}
				if (GroupK == 0 && ParentType.Equals(Model.Entities.ObjectType.Group))
				{
					GroupK = ParentK;
				}
				#endregion
				#region if we're posting in a group, make sure we are a member
				if (GroupK > 0)
				{
					GroupUsr gu = ParentGroup.GetGroupUsr(PostingUsr);
					if (!PostingUsr.CanGroupMember(gu))
					{
						r.Success = false;
						r.MessageHtml = "You have tried to post a message in the " + ParentGroup.Link() + " group, but you're not currently a member of this group. Check out the group homepage for more information.";
						return r;
					}
				}
				#endregion
				#region ensure both parents map to real objects
				if (!ParentType.Equals(Model.Entities.ObjectType.None))
				{
					if (ParentBob == null)
					{
						r.Success = false;
						r.MessageHtml = "You've tried to post a message in a non-existant chat. Maybe it's been deleted.";
						return r;
					}
				}
				if (GroupK > 0)
				{
					if (ParentGroup == null)
					{
						r.Success = false;
						r.MessageHtml = "You've tried to post a message in a non-existant group. Maybe it's been deleted.";
						return r;
					}
				}
				#endregion
				#region Private
				bool tPrivate = false;
				bool gPrivate = false;
				if (Private)
				{
					if (GroupK == 0)
						tPrivate = true;
					else
						gPrivate = true;
				}
				#endregion
				#region News
				tNews = false;
				tNewsStatus = NewsStatusEnum.None;
				int tNewsLevel = 0;
				int tNewsUsrK = 0;
				int tNewsModeratorUsrK = 0;
				if (News)
				{
					if (tPrivate)
					{
						r.Success = false;
						r.MessageHtml = "You've tried to post private news! How did this happen?";
						return r;
					}
					else if (GroupK == 0)
					{
						r.Success = false;
						r.MessageHtml = "You've tried to post public non-group news. How did this happen?";
						return r;

						//if (PostingUsr.NewsPermissionLevel >= 10 || PostingUsr.IsSuper)
						//{
						//    tNews = true;
						//    tNewsStatus = NewsStatusEnum.Recommended;
						//    tNewsModeratorUsrK = Usr.GetNewsModeratorUsrK();
						//    tNewsLevel = 10;
						//    tNewsUsrK = PostingUsr.K;
						//}
						//else
						//{
						//    tNews = false;
						//    tNewsStatus = NewsStatusEnum.Recommended;
						//    tNewsModeratorUsrK = Usr.GetNewsModeratorUsrK();
						//    tNewsLevel = 0;
						//    tNewsUsrK = PostingUsr.K;
						//}
					}
					else
					{
						GroupUsr gu = ParentGroup.GetGroupUsr(PostingUsr);

						if (PostingUsr.CanGroupNewsAdmin(gu))
						{
							tNews = true;
							tNewsStatus = NewsStatusEnum.Done;
							tNewsLevel = 10;
							tNewsUsrK = PostingUsr.K;
						}
						else
						{
							tNews = false;
							tNewsStatus = NewsStatusEnum.Recommended;
							tNewsLevel = 10;
							tNewsUsrK = PostingUsr.K;
						}
					}
				}
				#endregion
				#region Sealed
				bool tSealed = false;
				if (Sealed)
				{
					if (tPrivate)
						tSealed = true;
				}
				#endregion
				#region Closed
				bool tClosed = false;
				if (Closed)
				{
					tClosed = true;
				}
				#endregion
				#region Review
				bool tReview = false;
				if (Review)
				{
					if (ParentType.Equals(Model.Entities.ObjectType.Event) && GroupK == 0)
					{
						Query qReviews = new Query();
						qReviews.QueryCondition = new And(
							new Q(Columns.UsrK, PostingUsr.K),
							new Q(Columns.ParentObjectType, Model.Entities.ObjectType.Event),
							new Q(Columns.ParentObjectK, ParentK),
							new Q(Columns.IsReview, true)
							);
						qReviews.TopRecords = 1;
						qReviews.Columns = new ColumnSet(Thread.Columns.K);
						ThreadSet tsReviews = new ThreadSet(qReviews);
						if (tsReviews.Count > 0)
						{
							r.Success = false;
							r.MessageHtml = "You've already posted a review. You can only post one review per event.";
							r.Duplicate = true;
							r.Thread = tsReviews[0];
							return r;
						}
						else
							tReview = true;
					}
					else
					{
						r.Success = false;
						r.MessageHtml = "You must post a review in an event chat board.";
						return r;
					}
				}
				#endregion

				if (GroupK > 0)
					CurrentGroupUsr = ParentGroup.GetGroupUsr(PostingUsr);

				Transaction trans = null;//new Transaction();

				NewThread = new Thread();

				try
				{
					#region Create thread
					NewThread.DateTime = DateTime.Now;
					NewThread.Enabled = true;
					NewThread.UsrK = PostingUsr.K;
					NewThread.LastPostUsrK = PostingUsr.K;
					NewThread.Subject = Helpers.Strip(Subject, true, true, true, true);

					NewThread.ParentObjectType = ParentType;
					NewThread.ParentObjectK = ParentK;
					if (GroupK > 0)
						NewThread.GroupK = ParentGroup.K;
					else
						NewThread.GroupK = 0;

					NewThread.Private = tPrivate;
					NewThread.GroupPrivate = gPrivate;
					if (GroupK > 0)
						NewThread.PrivateGroup = ParentGroup.PrivateChat;
					else
						NewThread.PrivateGroup = false;

					NewThread.IsNews = tNews;
					NewThread.NewsStatus = tNewsStatus;
					NewThread.NewsLevel = tNewsLevel;
					NewThread.NewsModeratorUsrK = tNewsModeratorUsrK;
					NewThread.NewsUsrK = tNewsUsrK;

					NewThread.Sealed = tSealed;
					NewThread.Closed = tClosed;
					NewThread.IsReview = tReview;

					NewThread.UpdateAncestorLinksNoUpdate();
					NewThread.Update(trans);

					#endregion

					#region Add PostingUsr ThreadUsr
					CurrentThreadUsr = new ThreadUsr();
					CurrentThreadUsr.DateTime = DateTime.Now;
					CurrentThreadUsr.InvitingUsrK = PostingUsr.K;
					CurrentThreadUsr.UsrK = PostingUsr.K;
					CurrentThreadUsr.ThreadK = NewThread.K;
					CurrentThreadUsr.ChangeStatus(ThreadUsr.StatusEnum.Archived, NewThread.DateTime);
					CurrentThreadUsr.StatusChangeObjectK = PostingUsr.K;
					CurrentThreadUsr.StatusChangeObjectType = Model.Entities.ObjectType.Usr;
					CurrentThreadUsr.Update(trans);
					#endregion

					#region Make the comment
					Comment.Maker cMaker = new Comment.Maker();
					cMaker.Body = Body;
					cMaker.ParentThread = NewThread;
					cMaker.DuplicateGuid = DuplicateGuid;
					cMaker.PostingUsr = PostingUsr;
					cMaker.InviteKs = InviteKs;
					cMaker.AlertedUsrs = AlertedUsrs;
					cMaker.NewThread = true;
					cMaker.CurrentThreadUsr = CurrentThreadUsr;
					cMaker.CurrentGroupUsr = CurrentGroupUsr;
					cMaker.RunAsync = RunAsync;
					cMaker.DisableLiveChatMessage = DisableLiveChatMessage;
					Comment.MakerReturn cReturn = null;
					try
					{
						cReturn = cMaker.Post(trans);
					}
					catch (Exception ex)
					{
						NewThread.DeleteAll(null);
						throw ex;
					}
					#endregion

					

					//SendThreadAlertsJob sendThreadsAlert = new SendThreadAlertsJob(NewThread.K, AlertedUsrs, PostingUsr.K, Subject, InviteKs);
					//if (this.RunAsync)
					//{
					//    sendThreadsAlert.ExecuteAsynchronously();
					//}
					//else
					//{
					//    sendThreadsAlert.ExecuteSynchronously();
					//}

					r.Success = true;
					r.Thread = NewThread;
					r.Comment = cReturn.Comment;
					return r;
				}
				catch (Exception ex)
				{
					//trans.Rollback();
					NewThread.DeleteAll(null);
					throw ex;
				}
				finally
				{
					//trans.Close();
				}
			}
			#endregion

			#region DoAlerts()
			NewsStatusEnum tNewsStatus;
			bool tNews;

			#endregion
		}
		#endregion

		#region MakerReturn
		public class MakerReturn : Return
		{
			public bool Duplicate { get; set; }
			public Thread Thread { get; set; }
			public Comment Comment { get; set; }
		}
		#endregion
		#region Invite
		public int Invite(
			List<int> inviteKs,
			Usr invitingUsr,
			DateTime inviteDateTime,
			List<int> alertedUsrs,
			bool isNewThread,
			Comment postedComment,
			bool joinChatRoom)
		{
			IDiscussable Parent = this.ParentForumObject;
			Query qInvites = new Query();

			List<Q> qList = new List<Q>();
			qList.Add(new Q(false));
			if (inviteKs != null && inviteKs.Count > 0)
				qList.Add(new InListQ(Usr.Columns.K, inviteKs));
			
			qInvites.QueryCondition = new Or(qList.ToArray());
			qInvites.Columns = new ColumnSet(
				Usr.EmailColumns,
				Usr.LinkColumns,
				Usr.Columns.IsLoggedOn,
				Usr.Columns.DateTimeLastPageRequest,
				Usr.Columns.AddedByGroupK);
			UsrSet usInvites = new UsrSet(qInvites);

			int count = 0;

			GroupUsr guInvitingUsr = null;
			if (this.GroupK > 0)
				guInvitingUsr = this.Group.GetGroupUsr(invitingUsr);

			List<int> addedThreadUsrsUsrKs = new List<int>();

			foreach (Usr u in usInvites)
			{
				if (!alertedUsrs.Contains(u.K))
				{
					try
					{
						ThreadUsr tuInvite = new ThreadUsr(this.K, u.K);
					}
					catch (Bobs.BobNotFound)
					{
						bool sendInviteAlerts = true;
						bool addThreadUsr = true;

						if (this.GroupK > 0)
						{
							GroupUsr gu = this.Group.GetGroupUsr(u);

							if (this.GroupPrivate || this.PrivateGroup)
							{
								//if this thread is private to the group or in a private group, there's a danger that we're 
								//inviting someone to a thread that they can't see. Also - if the group is made public, many 
								//threads may appear in peoples inboxes. This bit stops us sending these dodgy invites out...
								if (gu == null)
								{
									//if they don't have a GroupUsr record, lets send a group invite, but hold off on the thread alert
									sendInviteAlerts = false;
									this.Group.Invite(u, gu, invitingUsr, guInvitingUsr, "", false);
								}
								else if (gu.IsMember)
								{
									//if they are a member, we can send the thread invite
								}
								else if (gu.Status.Equals(GroupUsr.StatusEnum.Recommend) ||
									gu.Status.Equals(GroupUsr.StatusEnum.RecommendRejected) ||
									gu.Status.Equals(GroupUsr.StatusEnum.Request) ||
									gu.Status.Equals(GroupUsr.StatusEnum.RequestRejected))
								{
									//if we might be able to invite or join them to the group, we give it a try (the outcome will depend on some rather complex code)
									this.Group.Invite(u, gu, invitingUsr, guInvitingUsr, "", false);

									if (!gu.IsMember)
									{
										//if we didn't join them straight up, they still won't be able to see the thread, so we hold off on the thread alert email
										sendInviteAlerts = false;

										if (!gu.Status.Equals(GroupUsr.StatusEnum.Invite))
										{
											//if we didn't join them straight up AND we didn't invite them, lets not bother inviting them to the thread
											addThreadUsr = false;
										}
									}
								}
								else
								{
									//ok so they have a GroupUsr, but they're not a member, and we can't invite or join them, so we're not going to bother inviting them to the thread
									addThreadUsr = false;
								}
							}
						}

						//We still add this user to the alertedusers list (even if !addThreadUsr) - this just makes sure we don't attempt to send them an invite a second time.
						alertedUsrs.Add(u.K);

						if (addThreadUsr)
						{
							ThreadUsr tu = this.GetThreadUsr(u);
							tu.ChangeStatus(ThreadUsr.StatusEnum.NewInvite, DateTime.Now, false, false);
							try
							{
								addedThreadUsrsUsrKs.Add(u.K);
							}
							catch { }
							tu.InvitingUsrK = invitingUsr.K;
							tu.StatusChangeObjectK = invitingUsr.K;
							tu.StatusChangeObjectType = Model.Entities.ObjectType.Usr;
							tu.Update();

							count++;

							#region sendInviteAlerts
							if (sendInviteAlerts)
							{
								try
								{
									Mailer usrMail = new Mailer();
									if (isNewThread && this.Private && this.ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
									{
										usrMail.Subject = invitingUsr.NickName + " sent you a photo";
										usrMail.Body += "<h1>" + invitingUsr.NickName + " sent you a photo</h1>";
										usrMail.Body += "<p align=\"center\"><a href=\"[LOGIN]\"><img border=\"0\" src=\"" + this.ParentPhoto.WebPath + "\" width=\"" + this.ParentPhoto.WebWidth + "\" height=\"" + this.ParentPhoto.WebHeight + "\" class=\"BorderBlack All\" /></a></p>";
									}
									else if (isNewThread)
									{
										usrMail.Subject = invitingUsr.NickName + " posts: \"" + this.SubjectSnip(40) + "\"";
										usrMail.Body += "<h1>" + invitingUsr.NickName + " has posted a new topic</h1>";
									}
									else
									{
										usrMail.Subject = invitingUsr.NickName + " invites you to: \"" + this.SubjectSnip(40) + "\"";
										usrMail.Body += "<h1>" + invitingUsr.NickName + " invites you to a topic</h1>";
									}

									usrMail.Body += "<p>The subject is: \"" + this.Subject + "\"</p>";
									usrMail.Body += "<p>To read " + invitingUsr.HisString(false) + " message, check out the <a href=\"[LOGIN]\">topic page</a>.</p>";
									usrMail.Body += "<p>If you want to stop " + invitingUsr.HimString(false) + " inviting you to topics, click the <i>Stop " + invitingUsr.NickName + " inviting me to chat topics</i> button on <a href=\"[LOGIN(" + invitingUsr.Url() + ")]\">" + invitingUsr.HisString(false) + " profile page</a>.</p>";
									usrMail.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
									usrMail.RedirectUrl = this.UrlDiscussion();
									usrMail.UsrRecipient = u;
									//usrMail.Bulk=usInvites.Count>5;
									usrMail.Bulk = false; // This is interesting... if people are invited by email, they should get the invite really...
									usrMail.Inbox = true;
									usrMail.Send();
								}
								catch (Exception ex) { Global.Log("1d3726bf-0715-4404-9059-4e53ca9e3dc5", ex); }

								//try
								//{
								//    if (u.IsLoggedOn && u.DateTimeLastPageRequest > DateTime.Now.AddMinutes(-5))
								//    {
								//        XmlDocument xmlDoc = new XmlDocument();
								//        XmlNode n = xmlDoc.CreateElement("privateMessageAlert");
								//        n.AddAttribute("nickName", invitingUsr.NickNameSafe);
								//        n.AddAttribute("stmu", invitingUsr.StmuParams);
								//        n.AddAttribute("usrK", invitingUsr.K.ToString());
								//        if (invitingUsr.HasPic)
								//            n.AddAttribute("pic", invitingUsr.Pic.ToString());
								//        else
								//            n.AddAttribute("pic", "0");
								//        n.AddAttribute("k", this.K.ToString());
								//        if (postedComment == null)
								//        {
								//            n.InnerText = this.Url();
								//        }
								//        else
								//        {
								//            n.InnerText = postedComment.Url(this);
								//        }
								//        Chat.SendChatItem(ItemType.Invite, n, DateTime.Now.Ticks, u.K, Guid.NewGuid());

								//    }
								//}
								//catch (Exception ex) { Global.Log("2902fd82-e8a4-49c2-9e33-ccfca01ca1f9", ex); }
							}
							#endregion
						}
					}
				}
			}

			if (joinChatRoom)
			{
				Guid room = this.GetRoomSpec().Guid;
				Chat.JoinRoom(room, addedThreadUsrsUsrKs.ToArray());
			}

			return count;
		}
		#endregion

		#region AddThreadUsrWithoutInvite
		public void AddThreadUsrWithoutInvite(int usrK)
		{
			try
			{
				ThreadUsr newThreadUsr = new ThreadUsr(this.K, usrK);
			}
			catch
			{
				ThreadUsr newThreadUsr = new ThreadUsr();
				newThreadUsr.ThreadK = this.K;
				newThreadUsr.UsrK = usrK;
				newThreadUsr.InvitingUsrK = 1;
				newThreadUsr.Status = ThreadUsr.StatusEnum.Archived;
				newThreadUsr.DateTime = DateTime.Now;
				newThreadUsr.PrivateChatType = ThreadUsr.PrivateChatTypes.None;
				newThreadUsr.Favourite = false;
				newThreadUsr.Deleted = false;
				newThreadUsr.ViewDateTime = DateTime.Now;
				newThreadUsr.ViewDateTimeLatest = DateTime.Now;
				newThreadUsr.ViewComments = this.TotalComments;
				newThreadUsr.ViewCommentsLatest = this.TotalComments;
				newThreadUsr.StatusChangeDateTime = DateTime.Now;
				newThreadUsr.StatusChangeObjectType = Model.Entities.ObjectType.Usr;
				newThreadUsr.StatusChangeObjectK = usrK;
				newThreadUsr.Update();
			}
		}
		#endregion


		#region SendNewsAlerts

		#endregion
		#region SendGroupNewsModNewNewsAlerts()
		public void SendGroupNewsModNewNewsAlerts()
		{
			Query qNewsMod = new Query();
			qNewsMod.QueryCondition = this.Group.NewsAdminQ;
			qNewsMod.TableElement = Group.UsrMemberJoin;
			qNewsMod.Columns = Usr.EmailColumns;
			UsrSet usNewsMods = new UsrSet(qNewsMod);
			foreach (Usr newsMod in usNewsMods)
			{
				Mailer mAdmin = new Mailer();
				mAdmin.UsrRecipient = newsMod;
				mAdmin.Subject = "New group news in " + this.Group.FriendlyName;
				if (this.UsrK != this.NewsUsrK)
				{
					mAdmin.Body += "<p>The news was recommended by " + this.NewsUsr.LinkEmail() + "</p>";
				}
				mAdmin.Body += "<p>" + this.Usr.LinkEmail() + " has posted new news in the " +
					this.Group.FriendlyName + " group. It's not enabled yet - you can enable or reject it on the <a href=\"[LOGIN]\">news admin</a> page</p>";
				mAdmin.RedirectUrl = this.Group.UrlApp("admin", "mode", "news");
				mAdmin.Send();
			}
		}
		#endregion


		#region NewsStatusEnum
		#endregion

		#region MyCommentsToHtml
		public string MyCommentsToHtml(int CurrentUsrK)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<nobr>");
			if (Private)
			{
				if (this.TotalParticipants < 2)
				{

				}
				else if (this.TotalParticipants == 2)
				{
					if (this.FirstParticipantUsrK > 0 && this.FirstParticipantUsrK != CurrentUsrK)
					{
						sb.Append("<nobr>To ");
						sb.Append(this.FirstParticipantUsr.Link());
						sb.Append("</nobr>");
					}
					else if (this.UsrK != CurrentUsrK)
					{
						sb.Append("<nobr>To ");
						sb.Append(this.Usr.Link());
						sb.Append("</nobr>");
					}
					else
						return "n/a";
				}
				else
				{
					sb.Append("<nobr>To ");
					sb.Append((this.TotalParticipants - 1).ToString("#,##0"));
					sb.Append(" people</nobr>");
				}
			}
			else
			{
				if (this.TotalWatching > 1)
				{
					sb.Append("<nobr>");
					sb.Append((this.TotalWatching - 1).ToString("#,##0"));
					if (this.TotalWatching != 2)
						sb.Append(" people");
					else
						sb.Append(" person");
					sb.Append(" watching</nobr>");
				}
			}
			if (sb.Length == 0)
				return "&nbsp;";
			else
				return sb.ToString();
		}
		#endregion
		#region AuthorHtml
		public string AuthorHtml
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("<nobr>");
				sb.Append(this.Usr.Link());
				if (Private)
				{
					if (this.TotalParticipants < 2)
					{

					}
					else if (this.TotalParticipants == 2)
					{
						if (this.FirstParticipantUsrK > 0)
						{
							sb.Append("</nobr><br><nobr>To ");
							sb.Append(this.FirstParticipantUsr.Link());
							sb.Append("</nobr>");
						}
						else
							return "n/a";
					}
					else
					{
						sb.Append("</nobr><br><nobr>To ");
						sb.Append((this.TotalParticipants - 1).ToString("#,##0"));
						sb.Append(" people</nobr>");
					}
				}
				else
				{
					if (this.TotalWatching > 1)
					{
						sb.Append("</nobr><br><nobr>");
						sb.Append((this.TotalWatching - 1).ToString("#,##0"));
						//	if (this.TotalWatching!=2)
						//		sb.Append(" people");
						//	else
						//		sb.Append(" person");
						sb.Append(" watching</nobr>");
					}
				}
				if (sb.Length == 0)
					return "&nbsp;";
				else
					return sb.ToString();
			}
		}
		#endregion
		#region RepliesHtml
		public string RepliesHtml
		{
			get
			{
				if (TotalComments == 1)
				{
					return String.Format("<nobr>0 / {0}</nobr>", this.LastPostFriendlyTime(true));
				}
				else if (TotalComments > 1)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("<nobr>");
					sb.Append((TotalComments - 1).ToString("#,##0"));

					try
					{
						sb.Append(" / ");
						sb.Append(this.LastPostFriendlyTime(true));
					}
					catch { }

					sb.Append("</nobr><br><nobr>");

					try
					{
						if (this.LastPostUsrK > 0)
						{
							sb.Append(" by ");
							sb.Append(this.LastPostUsr.Link());
						}
					}
					catch { }

					sb.Append("</nobr>");
					return sb.ToString();
				}
				else
				{
					return "&nbsp;";
				}
			}
		}
		#endregion
		#region InboxButtonHtml
		public string InboxButtonHtml(Control controlForScript)
		{
			string stateInbox = "0";
			if (this.JoinedThreadUsr.UsrK > 0 && this.JoinedThreadUsr.IsInbox)
				stateInbox = "1";

			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "i" + this.K.ToString(), String.Format("DbButtonFull(i5,i6,a5,a6,\"\",\"\",\"\",s3,l3,26,21,f3,{0},{1},\"i{0}\",\"iT\",\"\",\"\",\"i{0}\");", this.K.ToString(), stateInbox), true);
			return String.Format("<div style=\"width:26px;height:21px;\" id=\"i{0}\"><img src=\"{1}\" align=\"left\" width=\"26\" height=\"21\"></div>", this.K.ToString(), stateInbox == "1" ? "/gfx/icon-inbox-up.png" : "/gfx/icon-inbox-dn.png");
		}
		#endregion

		#region WatchingHtml
		public string WatchingScript(string controlPrefix, string onReturn)
		{
			string stateWatch = "0";
			if (this.JoinedThreadUsr.UsrK > 0 && this.JoinedThreadUsr.IsWatching)
				stateWatch = "1";

			return String.Format("DbButtonFull(i1,i2,a1,a2,\"\",\"\",\"\",s1,l1,26,21,f1,{0},{1},\"{2}{0}\",\"{3}\",\"\",\"\",\"{2}{0}\");", this.K.ToString(), stateWatch, controlPrefix, onReturn);
		}
		public string WatchingHtml(string controlPrefix)
		{
			string stateWatch = "0";
			if (this.JoinedThreadUsr.UsrK > 0 && this.JoinedThreadUsr.IsWatching)
				stateWatch = "1";

			return String.Format("<div style=\"width:26px;height:21px;\" id=\"{1}{0}\"><img src=\"{2}\" align=\"left\" width=\"26\" height=\"21\"></div>", this.K.ToString(), controlPrefix, stateWatch == "1" ? "/gfx/icon-eye-up.png" : "/gfx/icon-eye-dn.png");
		}
		public string WatchingHtml(string onReturn, Control controlForScript)
		{
			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "w" + this.K.ToString(), this.WatchingScript("w", onReturn), true);
			return this.WatchingHtml("w");
		}
		public string WatchingHtml(string onReturn, Control controlForScript, string prefix)
		{
			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "w" + prefix + this.K.ToString(), this.WatchingScript("w" + prefix, onReturn), true);
			return this.WatchingHtml("w" + prefix);
		}
		#endregion
		#region FavouriteHtml
		public string FavouriteScript(string controlPrefix)
		{
			string stateFavourite = "0";
			if (this.JoinedThreadUsr.UsrK > 0 && this.JoinedThreadUsr.Favourite)
				stateFavourite = "1";

			return String.Format("DbButtonFull(i3,i4,a3,a4,\"\",\"\",\"\",s2,l2,22,21,f2,{0},{1},\"{2}{0}\",\"\",\"\",\"\",\"{2}{0}\");", this.K.ToString(), stateFavourite, controlPrefix);
		}
		public string FavouriteHtml(string controlPrefix)
		{
			string stateFavourite = "0";
			if (this.JoinedThreadUsr.UsrK > 0 && this.JoinedThreadUsr.Favourite)
				stateFavourite = "1";

			return String.Format("<div style=\"width:26px;height:21px;\" id=\"{1}{0}\"><img src=\"{2}\" align=\"left\" width=\"22\" height=\"21\"></div>", this.K.ToString(), controlPrefix, stateFavourite == "1" ? "/gfx/icon-star-22-up.png" : "/gfx/icon-star-22-dn.png");
		}
		public string FavouriteHtml(Control controlForScript)
		{
			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "f" + this.K.ToString(), this.FavouriteScript("f"), true);
			return this.FavouriteHtml("f");
		}
		public string FavouriteHtml(Control controlForScript, string prefix)
		{
			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "f" + prefix + this.K.ToString(), this.FavouriteScript("f" + prefix), true);
			return this.FavouriteHtml("f" + prefix);
		}
		#endregion

		#region ToHtml
		protected string ToHtml(Thread t)
		{
			if (t.TotalParticipants < 2)
			{
				return "n/a";
			}
			else if (t.TotalParticipants == 2)
			{
				if (t.FirstParticipantUsrK > 0)
					return "<a href=\"" + t.FirstParticipantUsr.Url() + "\" " + t.FirstParticipantUsr.Rollover + ">" + t.FirstParticipantUsr.NickNameSafe + "</a>";
				else
					return "n/a";
			}
			else
			{
				return "<nobr>" + ((int)(t.TotalParticipants - 1)).ToString() + " users</nobr>";
			}
		}
		#endregion
		#region PagesHtml
		#region Discussable - TODO: merge with Url below
		public delegate string GetUrlByDiscussableDelegate(IDiscussable discussable, params object[] par);
		public string PagesHtml(IDiscussable discussable, GetUrlByDiscussableDelegate GetThreadUrl, params object[] par)
		{
			int firstUnreadPage = 0;
			if (this.JoinedThreadUsr.ViewCommentsInUse > 0)
			{
				firstUnreadPage = (this.JoinedThreadUsr.ViewCommentsInUse / Vars.CommentsPerPage) + 1;
			}

			StringBuilder sb = new StringBuilder();

			if (firstUnreadPage > 0 && LastPage >= firstUnreadPage && this.JoinedThreadUsr.ViewCommentsInUse < this.TotalComments)
			{
				int numLinks = 3;
				int midLinks = 2;
				sb.Append(" <a class=\"Unread\" href=\"");
				if (firstUnreadPage > 1)
					sb.Append(GetThreadUrl(discussable, Cambro.Misc.Utility.Join(par, "k", this.K.ToString(), "c", firstUnreadPage.ToString())) + "#Unread");
				else
					sb.Append(GetThreadUrl(discussable, Cambro.Misc.Utility.Join(par, "k", this.K.ToString())) + "#Unread");
				sb.Append("\" onmouseover=\"stt(UnreadString(");
				sb.Append(firstUnreadPage.ToString());
				sb.Append("));\" onmouseout=\"htm();\">NEW</a>");
				if (LastPage > 1)
				{
					sb.Append("<br><small>Page: </small>");
					int gap1Start = numLinks;
					int gap1End = firstUnreadPage - midLinks;
					int gap2Start = firstUnreadPage + midLinks - 1;
					int gap2End = LastPage - numLinks + 1;
					bool hasGap1 = gap1Start < gap1End - 1;
					bool hasGap2 = gap2Start < gap2End - 1;
					int seq1End = hasGap1 ? gap1Start : (hasGap2 ? gap2Start : LastPage);
					int seq2End = hasGap2 ? gap2Start : LastPage;
					for (int i = 1; i <= seq1End; i++)
					{
						WritePageLink(i, sb, discussable, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
					}
					if (hasGap1)
					{
						sb.Append("... ");
						for (int i = gap1End; i <= seq2End; i++)
						{
							WritePageLink(i, sb, discussable, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
					}
					if (hasGap2)
					{
						sb.Append("... ");
						for (int i = gap2End; i <= LastPage; i++)
						{
							WritePageLink(i, sb, discussable, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
					}
				}
			}
			else
			{
				if (firstUnreadPage == 0 && this.JoinedThreadUsr.ThreadK == this.K && this.JoinedThreadUsr.IsWatching && !this.JoinedThreadUsr.Status.Equals(ThreadUsr.StatusEnum.Archived) && !this.JoinedThreadUsr.Status.Equals(ThreadUsr.StatusEnum.UnArchived))
				{
					sb.Append(" <a class=\"UnreadNew\" href=\"");
					sb.Append(GetThreadUrl(discussable, Cambro.Misc.Utility.Join(par, "k", this.K.ToString())) + "#Unread");
					sb.Append("\" onmouseover=\"stt(UnreadNewString(");
					sb.Append(((int)this.JoinedThreadUsr.Status).ToString());
					sb.Append(",");
					sb.Append(((int)this.JoinedThreadUsr.StatusChangeObjectType).ToString());
					sb.Append("));\" onmouseout=\"htm();\">NEW</a>");
				}
				int numLinks = 3;
				if (LastPage > 1)
				{
					sb.Append("<br><small>Page: </small>");
					if (LastPage <= numLinks * 2)
					{
						for (int i = 1; i <= LastPage; i++)
						{
							WritePageLink(i, sb, discussable, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
					}
					else
					{
						for (int i = 1; i <= (numLinks); i++)
						{
							WritePageLink(i, sb, discussable, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
						sb.Append("... ");
						for (int i = LastPage - (numLinks) + 1; i <= LastPage; i++)
						{
							WritePageLink(i, sb, discussable, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
					}
				}
			}
			return sb.ToString();
		}
		private void WritePageLink(int page, StringBuilder sb, IDiscussable discussable, int firstUnreadPage, int viewComments, int totalComments, GetUrlByDiscussableDelegate GetThreadUrl, params object[] par)
		{
			bool hilight = firstUnreadPage > 0 && firstUnreadPage <= page && viewComments < totalComments;
			if (!hilight)
				sb.Append("<small>");
			sb.Append("<a ");
			if (hilight)
				sb.Append("class=\"Unread\" ");
			sb.Append("href=\"");
			if (page > 1)
				sb.Append(GetThreadUrl(discussable, Cambro.Misc.Utility.Join(par, "k", this.K.ToString(), "c", page.ToString())));
			else
				sb.Append(GetThreadUrl(discussable, Cambro.Misc.Utility.Join(par, "k", this.K.ToString())));
			sb.Append("\">");
			sb.Append(page.ToString());
			sb.Append("</a>");
			if (!hilight)
				sb.Append("</small>");
			sb.Append(" ");
		}
		#endregion
		#region Url - TODO: merge with Discussable above
		public delegate string GetUrlDelegate(Thread t, object[] par);
		public string PagesHtml(UrlInfo Url, GetUrlDelegate GetThreadUrl, params object[] par)
		{
			int firstUnreadPage = 0;
			if (this.JoinedThreadUsr.ViewCommentsInUse > 0)
			{
				firstUnreadPage = (this.JoinedThreadUsr.ViewCommentsInUse / Vars.CommentsPerPage) + 1;
			}

			StringBuilder sb = new StringBuilder();

			if (firstUnreadPage > 0 && LastPage >= firstUnreadPage && this.JoinedThreadUsr.ViewCommentsInUse < this.TotalComments)
			{
				int numLinks = 3;
				int midLinks = 2;
				sb.Append(" <a class=\"Unread\" href=\"");
				if (firstUnreadPage > 1)
					sb.Append(GetThreadUrl(this, Cambro.Misc.Utility.Join(par, "k", this.K.ToString(), "c", firstUnreadPage.ToString())) + "#Unread");
				else
					sb.Append(GetThreadUrl(this, Cambro.Misc.Utility.Join(par, "k", this.K.ToString())) + "#Unread");
				sb.Append("\" onmouseover=\"stt(UnreadString(");
				sb.Append(firstUnreadPage.ToString());
				sb.Append("));\" onmouseout=\"htm();\">NEW</a>");
				if (LastPage > 1)
				{
					sb.Append("<br><small>Page: </small>");
					int gap1Start = numLinks;
					int gap1End = firstUnreadPage - midLinks;
					int gap2Start = firstUnreadPage + midLinks - 1;
					int gap2End = LastPage - numLinks + 1;
					bool hasGap1 = gap1Start < gap1End - 1;
					bool hasGap2 = gap2Start < gap2End - 1;
					int seq1End = hasGap1 ? gap1Start : (hasGap2 ? gap2Start : LastPage);
					int seq2End = hasGap2 ? gap2Start : LastPage;
					for (int i = 1; i <= seq1End; i++)
					{
						WritePageLink(i, sb, Url, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
					}
					if (hasGap1)
					{
						sb.Append("... ");
						for (int i = gap1End; i <= seq2End; i++)
						{
							WritePageLink(i, sb, Url, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
					}
					if (hasGap2)
					{
						sb.Append("... ");
						for (int i = gap2End; i <= LastPage; i++)
						{
							WritePageLink(i, sb, Url, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
					}
				}
			}
			else
			{
				if (firstUnreadPage == 0 && this.JoinedThreadUsr.ThreadK == this.K && this.JoinedThreadUsr.IsWatching && !this.JoinedThreadUsr.Status.Equals(ThreadUsr.StatusEnum.Archived) && !this.JoinedThreadUsr.Status.Equals(ThreadUsr.StatusEnum.UnArchived))
				{
					sb.Append(" <a class=\"UnreadNew\" href=\"");
					sb.Append(GetThreadUrl(this, Cambro.Misc.Utility.Join(par, "k", this.K.ToString())) + "#Unread");
					sb.Append("\" onmouseover=\"stt(UnreadNewString(");
					sb.Append(((int)this.JoinedThreadUsr.Status).ToString());
					sb.Append(",");
					sb.Append(((int)this.JoinedThreadUsr.StatusChangeObjectType).ToString());
					sb.Append("));\" onmouseout=\"htm();\">NEW</a>");
				}
				int numLinks = 3;
				if (LastPage > 1)
				{
					sb.Append("<br><small>Page: </small>");
					if (LastPage <= numLinks * 2)
					{
						for (int i = 1; i <= LastPage; i++)
						{
							WritePageLink(i, sb, Url, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
					}
					else
					{
						for (int i = 1; i <= (numLinks); i++)
						{
							WritePageLink(i, sb, Url, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
						sb.Append("... ");
						for (int i = LastPage - (numLinks) + 1; i <= LastPage; i++)
						{
							WritePageLink(i, sb, Url, firstUnreadPage, this.JoinedThreadUsr.ViewCommentsInUse, this.TotalComments, GetThreadUrl, par);
						}
					}
				}
			}
			return sb.ToString();
		}
		private void WritePageLink(int page, StringBuilder sb, UrlInfo Url, int firstUnreadPage, int viewComments, int totalComments, GetUrlDelegate GetThreadUrl, params object[] par)
		{
			bool hilight = firstUnreadPage > 0 && firstUnreadPage <= page && viewComments < totalComments;
			if (!hilight)
				sb.Append("<small>");
			sb.Append("<a ");
			if (hilight)
				sb.Append("class=\"Unread\" ");
			sb.Append("href=\"");
			if (page > 1)
				sb.Append(GetThreadUrl(this, Cambro.Misc.Utility.Join(par, "k", this.K.ToString(), "c", page.ToString())));
			else
				sb.Append(GetThreadUrl(this, Cambro.Misc.Utility.Join(par, "k", this.K.ToString())));
			sb.Append("\">");
			sb.Append(page.ToString());
			sb.Append("</a>");
			if (!hilight)
				sb.Append("</small>");
			sb.Append(" ");
		}
		#endregion
		public int LastPage
		{
			get
			{
				return (int)Math.Ceiling((double)this.TotalComments / (double)Vars.CommentsPerPage);
			}
		}
		#endregion
		#region PrivateHtml
		public string PrivateHtml
		{
			get
			{
				if (this.Private)
				{
					return "<img src=\"/gfx/icon-key.png\" width=\"26\" height=\"21\" style=\"margin-right:3px;\" border=\"0\" align=\"right\" onmouseover=\"stt('Private')\" onmouseout=\"htm();\">";
				}
				else
					return "";
			}
		}
		#endregion
		#region NewHtml
		public string NewHtml
		{
			get
			{
				if (IsNew)
					return "<font style=\"color:red;\"><b>NEW</b></font> ";
				else
					return "";
			}
		}
		#endregion
		#region IsNew
		public bool IsNew
		{
			get
			{
				if (Usr.Current == null)
					return false;
				if (this.JoinedThreadUsr != null)
				{
					if (this.JoinedThreadUsr.UsrK == 0)
						return false;
					else
						return this.LastPost > this.JoinedThreadUsr.ViewDateTimeInUse;
				}
				//				if (this.DataRow!=null)
				//				{
				//					if (this.DataRow.Table.Columns.Contains("ThreadUsr_ViewDateTime"))
				//					{
				//						if (this.DataRow["ThreadUsr_ThreadK"].Equals(DBNull.Value))
				//							return false;
				//						else
				//						{
				//							DateTime dateTime = DateTime.MinValue;
				//							if (!this.DataRow["ThreadUsr_ViewDateTime"].Equals(DBNull.Value))
				//								dateTime = (DateTime)this.DataRow["ThreadUsr_ViewDateTime"];
				//
				//							DateTime dateTimeLatest = DateTime.MinValue;
				//							if (!this.DataRow["ThreadUsr_ViewDateTimeLatest"].Equals(DBNull.Value))
				//								dateTimeLatest = (DateTime)this.DataRow["ThreadUsr_ViewDateTimeLatest"];
				//
				//							DateTime dateTimeInUse = DateTime.MinValue;
				//							if (dateTimeLatest.AddMinutes(5)<DateTime.Now || dateTime==DateTime.MinValue)
				//								dateTimeInUse = dateTimeLatest;
				//							else
				//								dateTimeInUse = dateTime;
				//
				//							return this.LastPost>dateTimeInUse;
				//						}
				//					}
				//				}
				throw new Exception("can't find JoinedThreadUsr records!");
			}
		}
		#endregion
		#region IconsHtml
		public string IconsHtml(UrlInfo urlInfo)
		{
			return IconsHtml(urlInfo.HasGroupObjectFilter);
		}
		public string IconsHtml(bool hasGroupObjectFilter)
		{
			string html = "";

			if (this.IsNews)
				html += @"<img src=""/gfx/icon-news.png"" onmouseover=""stt('News');"" onmouseout=""htm();"" border=""0"" align=""right"" height=""21"" width=""26"" style=""margin-right:3px;"">";

			if (this.IsReview)
				html += @"<img src=""/gfx/icon-chatter.png"" onmouseover=""stt('Review');"" onmouseout=""htm();"" border=""0"" align=""right"" height=""21"" width=""26"" style=""margin-right:3px;"">";

			if (this.Private)
				html += @"<img src=""/gfx/icon-key.png"" width=""26"" height=""21"" style=""margin-right:3px;"" border=""0"" align=""right"" onmouseover=""stt('Private')"" onmouseout=""htm();"">";
			else if (this.GroupPrivate)
				html += @"<img src=""/gfx/icon-key.png"" width=""26"" height=""21"" style=""margin-right:3px;"" border=""0"" align=""right"" onmouseover=""stt('Group private')"" onmouseout=""htm();"">";
			else if (this.PrivateGroup && !hasGroupObjectFilter)
				html += @"<img src=""/gfx/icon-key.png"" width=""26"" height=""21"" style=""margin-right:3px;"" border=""0"" align=""right"" onmouseover=""stt('Private group')"" onmouseout=""htm();"">";

			//	if (this.GroupK>0 && !Url.HasGroupObjectFilter)
			//		html += @"<img src=""/gfx/icon-group.png"" onmouseover=""stt('Group topic');"" onmouseout=""htm();"" border=""0"" align=""right"" height=""21"" width=""26"" style=""margin-right:3px;"">";

			return html;
		}
		#endregion
		#region CommentHtml
		public string CommentHtmlStart
		{
			get
			{
				if (this.IsNews || this.IsReview)
					return "<b>";
				else
					return "";
			}
		}
		public string CommentHtmlEnd
		{
			get
			{
				if (this.IsNews || this.IsReview)
					return "</b>";
				else
					return "";
			}
		}
		#endregion
		#region SetThreadUsr
		public void SetThreadUsr(int comments)
		{
			if (Usr.Current != null)
			{
				ThreadUsr threadUsr = GetThreadUsr(Usr.Current);
				threadUsr.Set(comments);
			}
		}
		#endregion
		#region GetThreadUsr
		/// <summary>
		/// Returns the current (or creates a new) ThreadUsr for the current thread and the logged in usr.
		/// </summary>
		public ThreadUsr GetThreadUsr(Usr u)
		{
			ThreadUsr threadUsr = null;
			if (u != null)
			{
				try
				{
					threadUsr = new ThreadUsr(this.K, u.K);
				}
				catch (BobNotFound)
				{
					try
					{
						threadUsr = new ThreadUsr();
						threadUsr.DateTime = DateTime.Now;
						threadUsr.IsNew = true;
						threadUsr.ThreadK = this.K;
						threadUsr.UsrK = u.K;
						threadUsr.ChangeStatus(ThreadUsr.StatusEnum.None);
						threadUsr.Update();
					}
					catch
					{
						try
						{
							threadUsr = new ThreadUsr(this.K, u.K);
						}
						catch
						{

						}
					}
				}
				if (threadUsr == null)
					throw new Exception("Still can't get ThreadUsr after silly complex mess!");
			}
			return threadUsr;
		}
		#endregion
		#region UsrView (removed)
		//		/// <summary>
		//		/// Returns the current ThreadUsr for the current thread and Usr (returns null if there is no ThreadUsr)
		//		/// </summary>
		//		public ThreadUsr ThreadUsr
		//		{
		//			get
		//			{
		//				if (threadUsr==null && Usr.Current!=null)
		//				{
		//					try
		//					{
		//						threadUsr = new ThreadUsr(this.K,Usr.Current.K);
		//					}
		//					catch
		//					{
		//					}
		//				}
		//				return threadUsr;
		//			}
		//		}
		//		ThreadUsr threadUsr;
		#endregion

		#region OrderBy's - Order, NewsOrder, HotTopicsOrderBy
		public static OrderBy Order
		{
			get
			{
				//return new OrderBy(new OrderBy(Columns.IsSticky,OrderBy.OrderDirection.Descending),new OrderBy(Columns.LastPost,OrderBy.OrderDirection.Descending));
				return new OrderBy(Columns.LastPost, OrderBy.OrderDirection.Descending);
			}
		}
		public static OrderBy NewsOrder
		{
			get
			{
				//return new OrderBy(new OrderBy(Columns.IsSticky,OrderBy.OrderDirection.Descending),new OrderBy(Columns.DateTime,OrderBy.OrderDirection.Descending));
				return new OrderBy(Columns.DateTime, OrderBy.OrderDirection.Descending);
			}
		}
		public static OrderBy HotTopicsOrderBy
		{
			get
			{
				//return new OrderBy("(Thread.TotalComments - (CASE SIGN(DATEDIFF(hour, Thread.AverageCommentDateTime, GetDate())/3.0) WHEN 1 THEN DATEDIFF(hour, Thread.AverageCommentDateTime, GetDate())/3.0 WHEN 0 THEN 0 ELSE 0 END)) DESC");
				return new OrderBy(Columns.HotTopicsOrder, OrderBy.OrderDirection.Descending);
			}
		}
		#endregion

		#region AddRelevant
		public void AddRelevant(IRelevanceHolder ContainerPage)
		{
			if (this.ParentArticle != null)
				this.ParentArticle.AddRelevant(ContainerPage);
			else if (this.ParentPhoto != null)
				this.ParentPhoto.AddRelevant(ContainerPage);
			else if (this.ParentEvent != null)
				this.ParentEvent.AddRelevant(ContainerPage);
			else if (this.ParentVenue != null)
				this.ParentVenue.AddRelevant(ContainerPage);
			else if (this.ParentPlace != null)
			{
				ContainerPage.RelevantPlacesAdd(this.PlaceK);
			}
			else if (this.ParentGroup != null)
				this.ParentGroup.AddRelevant(ContainerPage);
		}
		#endregion

		#region IsConnectedTo(ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			if (objectType.Equals(Model.Entities.ObjectType.Thread) && this.K == objectK)
				return true;

			if (this.ParentDiscussable is IConnectedTo)
			{
				IConnectedTo parent = (IConnectedTo)this.ParentDiscussable;

				if (objectType.Equals(this.ParentObjectType) && this.ParentObjectK == objectK)
					return true;

				if (parent.CanBeConnectedTo(objectType) && parent.IsConnectedTo(objectType, objectK))
					return true;
			}

			return false;
		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			//not needed - never called.
			return true;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Thread.CanBeConnectedToStatic(o);
		}
		#endregion

		#region IBobType Members
		public string TypeName
		{
			get
			{
				return "Topic";
			}
		}

		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Thread;
			}
		}
		#endregion

		#region Rollover
		public string Rollover
		{
			get
			{
				try
				{
					if (ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
						return "onmouseover=\"stma('" + this.ParentPhoto.Icon + "');\" onmouseout=\"htm();\"";
					else
						return "";
				}
				catch (BobNotFound)
				{
					return "";
				}
			}
		}
		#endregion

		#region MakeRollover
		public void MakeRollover(HtmlControl c)
		{
			if (ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
			{
				c.Attributes["onmouseover"] = "stma('" + this.ParentPhoto.Icon + "');";
				c.Attributes["onmouseout"] = "htm();";
			}
		}
		#endregion
		#region MakeRollover
		public void MakeRollover(WebControl c)
		{
			if (ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
			{
				c.Attributes["onmouseover"] = "stma('" + this.ParentPhoto.Icon + "');";
				c.Attributes["onmouseout"] = "htm();";
			}
		}
		#endregion



		#region Joins

		public static Join EventGroupLeftJoin
		{
			get
			{
				return new Join(
					new TableElement(TablesEnum.Thread),
					Event.GroupJoin,
					QueryJoinType.Left,
					Thread.Columns.EventK,
					Event.Columns.K);
			}
		}

		public static Join EventBrandLeftJoin
		{
			get
			{
				return new Join(
					new TableElement(TablesEnum.Thread),
					new TableElement(TablesEnum.EventBrand),
					QueryJoinType.Left,
					Thread.Columns.EventK,
					EventBrand.Columns.EventK
				);
			}
		}

		public static Join EventGroupJoin
		{
			get
			{
				return new Join(
					new TableElement(TablesEnum.Thread),
					new TableElement(TablesEnum.GroupEvent),
					QueryJoinType.Inner,
					Thread.Columns.EventK,
					GroupEvent.Columns.EventK);
			}
		}

		public static Join EventBrandJoin
		{
			get
			{
				return new Join(
					new TableElement(TablesEnum.Thread),
					new TableElement(TablesEnum.EventBrand),
					QueryJoinType.Inner,
					Thread.Columns.EventK,
					EventBrand.Columns.EventK
				);
			}
		}


		public static Join BrandLeftJoin
		{
			get
			{
				return new Join(
					new TableElement(TablesEnum.Thread),
					Event.BrandJoin,
					QueryJoinType.Left,
					Columns.EventK,
					Event.Columns.K
				);
			}
		}
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			//delete Comments
			Delete CommentDelete = new Delete(
				TablesEnum.Comment,
				new Q(Comment.Columns.ThreadK, this.K)
				);
			CommentDelete.Run(transaction);

			//delete ThreadUsrs
			Delete ThreadUsrDelete = new Delete(
				TablesEnum.ThreadUsr,
				new Q(ThreadUsr.Columns.ThreadK, this.K)
				);
			ThreadUsrDelete.Run(transaction);

			//delete CommentAlerts
			Delete CommentAlertDelete = new Delete(
				TablesEnum.CommentAlert,
				new And(
				new Q(CommentAlert.Columns.ParentObjectK, this.K),
				new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Thread)
				)
				);
			CommentAlertDelete.Run(transaction);

			this.Delete(transaction);

			IHasPrimaryThread parentHasPrimaryThread = this.Parent as IHasPrimaryThread;
			if (parentHasPrimaryThread != null) parentHasPrimaryThread.UpdateSingleThread();


			if (this.Parent != null)
				this.Parent.UpdateTotalComments(transaction);
		}
		#endregion

		#region Url
		public void UpdateUrlFragmentNoUpdate()
		{
			if (ParentObjectType.Equals(Model.Entities.ObjectType.None))
			{
				UrlFragment = "";
			}
			else
			{
				UrlFragment = ((IObjectPage)ParentForumObject).UrlFilterPart;
			}
		}
		public string Url(params string[] par)
		{
			return UrlDiscussion(par);
		}
		public string UrlDiscussion(params string[] par)
		{
			string[] fullParams = Utility.JoinStringArrays(new string[] { "k", this.K.ToString() }, par);
			return UrlInfo.MakeUrl(this.UrlFragment, "chat", fullParams);
		}
		public string UrlRefresher(params string[] par)
		{
			string[] fullParams = Utility.JoinStringArrays(new string[] { "k", this.K.ToString(), "m", Common.ThreadSafeRandom.Next(1,10000).ToString() }, par);
			return UrlInfo.MakeUrl(this.UrlFragment, "chat", fullParams);
		}
		#endregion

		#region Parent
		public IDiscussable Parent
		{
			get
			{
				return ParentDiscussable as IDiscussable;
			}
		}
		#endregion

		#region UpdateTotalComments()
		public void UpdateTotalComments(Transaction transaction)
		{
			#region Update AverageCommentDateTime, TotalComments, HotTopicsOrder
			Query qStat = new Query();
			qStat.QueryCondition = new Q(Comment.Columns.ThreadK, this.K);
			qStat.TableElement = new JoinLeft(
				Comment.Columns.ThreadK,
				Thread.Columns.K);
			qStat.ExtraSelectElements = ThreadStats.ExtraSelectElements;
			qStat.Columns = new ColumnSet();
			ThreadStats tStat = new ThreadStats(qStat);
			this.AverageCommentDateTime = tStat.AverageCommentDateTime;
			this.TotalComments = tStat.CommentCount;
			this.HotTopicsOrder = this.AverageCommentDateTime.AddHours(this.TotalComments * 2);
			#endregion

			#region Update IndexInThread for all the comments (changed 13/03/2006 to order this by K rather than DateTime to sort problem with incorrect datetimes being set by different servers)
			Db.Qu(@"WITH CommentIndex_Tab AS
			(
			  SELECT *,
				ROW_NUMBER() OVER(ORDER BY [Comment].[K]) AS Row_Number
			  FROM [Comment] WHERE [Comment].[ThreadK] = " + this.K.ToString() + @"
			)
			UPDATE [CommentIndex_Tab]
			SET [CommentIndex_Tab].[IndexInThread] = Row_Number - 1
			WHERE [CommentIndex_Tab].[ThreadK] = " + this.K.ToString() + @" 
			AND [CommentIndex_Tab].[IndexInThread] != Row_Number - 1;");
			#endregion

			#region Update LastPost and LastPostUsrK
			Query qLastComment = new Query();
			qLastComment.TopRecords = 1;
			qLastComment.QueryCondition = new Q(Comment.Columns.ThreadK, this.K);
			qLastComment.OrderBy = new OrderBy(Comment.Columns.DateTime, OrderBy.OrderDirection.Descending);
			CommentSet cs = new CommentSet(qLastComment);
			if (cs.Count == 1)
			{
				this.LastPost = cs[0].DateTime;
				this.LastPostUsrK = cs[0].UsrK;
			}
			else
			{
				this.LastPost = this.DateTime;
				this.LastPostUsrK = this.UsrK;
			}
			#endregion

			this.Update(transaction);

			if (this.GroupK > 0 && this.Group != null)
				this.Group.UpdateTotalComments(transaction);

			if (!this.ParentObjectType.Equals(Model.Entities.ObjectType.Group) && this.Parent != null)
				this.Parent.UpdateTotalComments(transaction);

		}
		#endregion

		#region UpdateAncestorLinks(Transaction transaction)
		public void UpdateAncestorLinks(Transaction transaction)
		{
			this.UpdateAncestorLinksNoUpdate();
			this.Update(transaction);
		}
		#endregion
		#region UpdateAncestorLinksNoUpdate()
		public void UpdateAncestorLinksNoUpdate()
		{
			#region Clear ancestors
			if (this.MusicTypeK != 0) this.MusicTypeK = 0;
			if (this.PhotoK != 0) this.PhotoK = 0;
			if (this.ArticleK != 0) this.ArticleK = 0;
			if (this.PhotoK != 0) this.PhotoK = 0;
			if (this.EventK != 0) this.EventK = 0;
			if (this.VenueK != 0) this.VenueK = 0;
			if (this.PlaceK != 0) this.PlaceK = 0;
			if (this.CountryK != 0) this.CountryK = 0;
			#endregion

			if (GroupK == 0)
				this.ThemeK = 1;

			if (GroupK > 0 && !ParentObjectType.Equals(Model.Entities.ObjectType.Group))
				UpdateGroupAncestors(Group);

			if (!ParentObjectType.Equals(Model.Entities.ObjectType.None))
				UpdateAncestors(ParentDiscussable);

			UpdateUrlFragmentNoUpdate();
		}
		#endregion
		#region void UpdateGroupAncestors(Group g)
		void UpdateGroupAncestors(Group g)
		{
			this.GroupK = g.K;
			this.ThemeK = g.ThemeK;

			if (g.MusicTypeK > 0)
				UpdateAncestors(g.MusicType);

			if (g.BrandK > 0)
				UpdateAncestors(g.Brand);

		}
		#endregion

		#region void UpdateAncestors(Bob b)
		void UpdateAncestors(IDiscussable b)
		{
			if (b is Group)
				UpdateAncestors((Group)b);
			else if (b is MusicType)
				UpdateAncestors((MusicType)b);
			else if (b is Photo)
				UpdateAncestors((Photo)b);
			else if (b is Article)
				UpdateAncestors((Article)b);
			else if (b is Event)
				UpdateAncestors((Event)b);
			else if (b is Venue)
				UpdateAncestors((Venue)b);
			else if (b is Place)
				UpdateAncestors((Place)b);
			else if (b is Country)
				UpdateAncestors((Country)b);

		}
		#endregion
		#region void UpdateAncestors(Group g)
		void UpdateAncestors(Group g)
		{
			this.GroupK = g.K;
			this.ThemeK = g.ThemeK;

			if (g.MusicTypeK > 0)
				UpdateAncestors(g.MusicType);

			if (g.PlaceK > 0)
				UpdateAncestors(g.Place);
			else if (g.CountryK > 0)
				UpdateAncestors(g.Country);

		}
		#endregion
		#region void UpdateAncestors(MusicType mt)
		void UpdateAncestors(MusicType mt)
		{
			this.MusicTypeK = mt.K;
		}
		#endregion
		#region void UpdateAncestors(Photo p)
		void UpdateAncestors(Photo p)
		{
			this.PhotoK = p.K;
			if (p.Article != null)
				UpdateAncestors(p.Article);
			else if (p.Event != null)
				UpdateAncestors(p.Event);
		}
		#endregion
		#region void UpdateAncestors(Article a)
		void UpdateAncestors(Article a)
		{
			this.ArticleK = a.K;
			if (a.ParentObjectType.Equals(Model.Entities.ObjectType.Event))
				UpdateAncestors(a.ParentEvent);
			else if (a.ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
				UpdateAncestors(a.ParentVenue);
			else if (a.ParentObjectType.Equals(Model.Entities.ObjectType.Place))
				UpdateAncestors(a.ParentPlace);
			else if (a.ParentObjectType.Equals(Model.Entities.ObjectType.Country))
				UpdateAncestors(a.ParentCountry);
		}
		#endregion
		#region void UpdateAncestors(Event e)
		void UpdateAncestors(Event e)
		{
			this.EventK = e.K;
			UpdateAncestors(e.Venue);
		}
		#endregion
		#region void UpdateAncestors(Venue v)
		void UpdateAncestors(Venue v)
		{
			this.VenueK = v.K;
			UpdateAncestors(v.Place);
		}
		#endregion
		#region void UpdateAncestors(Place p)
		void UpdateAncestors(Place p)
		{
			this.PlaceK = p.K;
			UpdateAncestors(p.Country);
		}
		#endregion
		#region void UpdateAncestors(Country c)
		void UpdateAncestors(Country c)
		{
			this.CountryK = c.K;
		}
		#endregion

		#region IconPath
		public string IconPath
		{
			get
			{
				if (ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
				{
					return Photo.IconPath;
				}
				else if (ParentObjectType.Equals(Model.Entities.ObjectType.Article))
				{
					if (Article.HasPic)
						return Article.PicPath;
					else
						return AltIcon;
				}
				else if (ParentObjectType.Equals(Model.Entities.ObjectType.Event))
				{
					if (Event.HasPic)
						return Event.PicPath;
					else if (Venue.HasPic)
						return Venue.PicPath;
					else if (Place.HasPic)
						return Place.PicPath;
					else
						return AltIcon;
				}
				else if (ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
				{
					if (Venue.HasPic)
						return Venue.PicPath;
					else if (Place.HasPic)
						return Place.PicPath;
					else
						return AltIcon;
				}
				else if (ParentObjectType.Equals(Model.Entities.ObjectType.Place))
				{
					if (Place.HasPic)
						return Place.PicPath;
					else
						return AltIcon;
				}
				else if (GroupK > 0 && Group != null && Group.HasPic)
				{
					return Group.PicPath;
				}
				else
					return AltIcon;
			}
		}
		#endregion
		#region SimpleIconPath
		public string SimpleIconPath
		{
			get
			{
				if (ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
					return this.ParentPhoto.IconPath;
				else
					return this.Usr.AnyPicPath;
			}
		}
		#endregion
		#region AltIcon
		string AltIcon
		{
			get
			{
				if (GroupK > 0 && Group != null && Group.HasPic)
					return Group.PicPath;
				else
					return this.Usr.AnyPicPath;
			}
		}
		#endregion

		#region LastPostFriendlyTime
		public string LastPostFriendlyTime(bool Capital)
		{
			return Utility.FriendlyTime(LastPost, Capital);
		}
		#endregion

		#region FriendlyTime
		public string FriendlyTime(bool Capital)
		{
			return Utility.FriendlyTime(FirstComment.DateTime, Capital);
		}
		#endregion

		#region Links to Bobs

		#region FirstParticipantUsr
		public Usr FirstParticipantUsr
		{
			get
			{
				if (firstParticipantUsr == null)
					firstParticipantUsr = new Usr(FirstParticipantUsrK, this, Columns.FirstParticipantUsrK);
				return firstParticipantUsr;
			}
			set
			{
				firstParticipantUsr = value;
			}
		}
		private Usr firstParticipantUsr;
		#endregion

		#region LastPostUsr
		public Usr LastPostUsr
		{
			get
			{
				if (lastPostUsr == null)
					lastPostUsr = new Usr(LastPostUsrK, this, Columns.LastPostUsrK);
				return lastPostUsr;
			}
			set
			{
				lastPostUsr = value;
			}
		}
		private Usr lastPostUsr;
		#endregion

		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
					usr = new Usr(UsrK, this, Columns.UsrK);
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		Usr usr;
		#endregion

		#region JoinedThreadUsr
		public ThreadUsr JoinedThreadUsr
		{
			get
			{
				if (joinedThreadUsr == null)
				{
					joinedThreadUsr = new ThreadUsr(this, Thread.Columns.K);
				}
				return joinedThreadUsr;
			}
			set
			{
				joinedThreadUsr = value;
			}
		}
		private ThreadUsr joinedThreadUsr;
		#endregion

		#region NewsUsr
		public Usr NewsUsr
		{
			get
			{
				if (newsUsr == null)
					newsUsr = new Usr(NewsUsrK, this, Columns.NewsUsrK);
				return newsUsr;
			}
			set
			{
				newsUsr = value;
			}
		}
		Usr newsUsr;
		#endregion
		#region NewsModeratorUsr
		public Usr NewsModeratorUsr
		{
			get
			{
				if (newsModeratorUsr == null)
					newsModeratorUsr = new Usr(NewsModeratorUsrK, this, Columns.NewsModeratorUsrK);
				return newsModeratorUsr;
			}
			set
			{
				newsModeratorUsr = value;
			}
		}
		Usr newsModeratorUsr;
		#endregion
		#region NewsModeratedByUsr
		public Usr NewsModeratedByUsr
		{
			get
			{
				if (newsModeratedByUsr == null)
					newsModeratedByUsr = new Usr(NewsModeratedByUsrK, this, Columns.NewsModeratedByUsrK);
				return newsModeratedByUsr;
			}
			set
			{
				newsModeratedByUsr = value;
			}
		}
		Usr newsModeratedByUsr;
		#endregion

		#region ParentObject
		public IBob ParentObject
		{
			get
			{
				if (parentObject == null && ParentObjectK > 0)
				{
					parentObject = Bob.Get(ParentObjectType, ParentObjectK);
				}
				return parentObject;
			}
			set
			{
				parentObject = value;
			}
		}
		IBob parentObject;
		public IDiscussable ParentDiscussable
		{
			get
			{
				if (parentDiscussable == null && ParentObjectK > 0)
				{
					parentDiscussable = (IDiscussable)Bob.Get(ParentObjectType, ParentObjectK);
					//switch(ParentObjectType)
					//{
					//    case ObjectType.Photo:
					//        parentObject = new Photo(ParentObjectK.Value, this, Columns.ParentObjectK);
					//        break;
					//    case ObjectType.Event:
					//        parentObject = new Event(ParentObjectK);
					//        break;
					//    case ObjectType.Venue:
					//        parentObject = new Venue(ParentObjectK);
					//        break;
					//    case ObjectType.Place:
					//        parentObject = new Place(ParentObjectK);
					//        break;
					//    case ObjectType.Country:
					//        parentObject = new Country(ParentObjectK);
					//        break;
					//    case ObjectType.Article:
					//        parentObject = new Article(ParentObjectK);
					//        break;
					//    case ObjectType.Brand:
					//        parentObject = new Brand(ParentObjectK);
					//        break;
					//    case ObjectType.Group:
					//        parentObject = new Group(ParentObjectK);
					//        break;
					//    default:
					//        break;
					//
				}
				return parentDiscussable;
			}
			set
			{
				parentDiscussable = value;
			}
		}
		IDiscussable parentDiscussable;
		#endregion

		#region Typed Parent Accessors
		public Photo ParentPhoto
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
					return null;
				else
					return (Photo)ParentDiscussable;
			}
		}
		public Event ParentEvent
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Event))
					return null;
				else
					return (Event)ParentDiscussable;
			}
		}
		public Venue ParentVenue
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
					return null;
				else
					return (Venue)ParentDiscussable;
			}
		}
		public Place ParentPlace
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Place))
					return null;
				else
					return (Place)ParentDiscussable;
			}
		}
		public Country ParentCountry
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Country))
					return null;
				else
					return (Country)ParentDiscussable;
			}
		}
		public Article ParentArticle
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Article))
					return null;
				else
					return (Article)ParentDiscussable;
			}
		}
		public Brand ParentBrand
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Brand))
					return null;
				else
					return (Brand)ParentDiscussable;
			}
		}
		public Group ParentGroup
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Group))
					return null;
				else
					return (Group)ParentDiscussable;
			}
		}
		#endregion

		#region Photo
		public Photo Photo
		{
			get
			{
				if (photo == null && PhotoK > 0)
					photo = new Photo(PhotoK, this, Thread.Columns.PhotoK);
				return photo;
			}
			set
			{
				photo = value;
			}
		}
		private Photo photo;
		#endregion
		#region Article
		public Article Article
		{
			get
			{
				if (article == null && ArticleK > 0)
					article = new Article(ArticleK, this, Thread.Columns.ArticleK);
				return article;
			}
			set
			{
				article = value;
			}
		}
		private Article article;
		#endregion
		#region Group
		public Group Group
		{
			get
			{
				if (group == null && GroupK > 0)
					group = new Group(GroupK, this, Thread.Columns.GroupK);
				return group;
			}
			set
			{
				group = value;
			}
		}
		private Group group;
		#endregion
		#region Event
		public Event Event
		{
			get
			{
				if (_event == null && EventK > 0)
					_event = new Event(EventK, this, Thread.Columns.EventK);
				return _event;
			}
			set
			{
				_event = value;
			}
		}
		private Event _event;
		#endregion
		#region Venue
		public Venue Venue
		{
			get
			{
				if (venue == null && VenueK > 0)
					venue = new Venue(VenueK, this, Thread.Columns.VenueK);
				return venue;
			}
			set
			{
				venue = value;
			}
		}
		private Venue venue;
		#endregion
		#region Place
		public Place Place
		{
			get
			{
				if (place == null && PlaceK > 0)
					place = new Place(PlaceK, this, Thread.Columns.PlaceK);
				return place;
			}
			set
			{
				place = value;
			}
		}
		private Place place;
		#endregion
		#region Country
		public Country Country
		{
			get
			{
				if (country == null && CountryK > 0)
					country = new Country(CountryK, this, Thread.Columns.CountryK);
				return country;
			}
			set
			{
				country = value;
			}
		}
		private Country country;
		#endregion
		#region Theme
		public Theme Theme
		{
			get
			{
				if (theme == null && ThemeK > 0)
					theme = new Theme(ThemeK, this, Thread.Columns.ThemeK);
				return theme;
			}
			set
			{
				theme = value;
			}
		}
		private Theme theme;
		#endregion

		#region FirstComment
		public Comment FirstComment
		{
			get
			{
				if (TotalComments == 0)
					return null;
				if (firstComment == null)
				{
					CommentSet tmpComments = new CommentSet(new Query(
						new And(
						new Q(Comment.Columns.ThreadK, K),
						new Q(Comment.Columns.Enabled, true)
						),
						new OrderBy(Comment.Columns.K, OrderBy.OrderDirection.Ascending), 1));
					try
					{
						firstComment = tmpComments[0];
					}
					catch
					{
						return null;
					}
				}
				return firstComment;
			}
			set
			{
				firstComment = value;
			}
		}
		Comment firstComment;
		#endregion

		#region LastComment
		public Comment LastComment
		{
			get
			{
				if (TotalComments == 0)
					return null;
				if (lastComment == null)
				{
					CommentSet tmpComments = new CommentSet(
						new Query(
							new Q(Comment.Columns.ThreadK, K),
							new OrderBy(Comment.Columns.K, OrderBy.OrderDirection.Descending),
							1)
						);

					if (tmpComments.Count == 1)
						lastComment = tmpComments[0];
					else
						return null;
				}
				return lastComment;
			}
		}
		Comment lastComment;
		#endregion

		#endregion

		#region Links to BobSets

		#region Comments
		public CommentSet GetPagedCommentSet(Q queryCondition, int requestedPage)
		{
			Query q = new Query();

			q.Columns = new ColumnSet(
				Comment.Columns.K,
				Comment.Columns.UsrK,
				Comment.Columns.Text,
				Comment.Columns.DateTime,
				Comment.Columns.IsEdited,
				Comment.Columns.EditDateTime,
				Comment.Columns.LolCount,
				Comment.Columns.ThreadK,
				Comment.Columns.IndexInThread,
				Usr.LinkColumns,
				Usr.Columns.Banned,
				//				Thread.Columns.K,
				//				Thread.Columns.Private,
				//				Thread.Columns.UsrK,
				ThreadUsr.Columns.ThreadK,
				ThreadUsr.Columns.ViewDateTime,
				ThreadUsr.Columns.ViewDateTimeLatest,
				ThreadUsr.Columns.ViewComments,
				ThreadUsr.Columns.ViewCommentsLatest
				);
			//	q.TableElement=new Join(
			//		new Join(Comment.Columns.UsrK,Usr.Columns.K),
			//		Thread.Columns.K,
			//		Comment.Columns.ThreadK
			//	);

			int usrK = 0;
			if (Usr.Current != null)
				usrK = Usr.Current.K;

			q.TableElement = new Join(
				new Join(Comment.Columns.UsrK, Usr.Columns.K),
				new TableElement(TablesEnum.ThreadUsr),
				QueryJoinType.Left,
				new And(
					new Q(Comment.Columns.ThreadK, ThreadUsr.Columns.ThreadK, true),
					new Q(ThreadUsr.Columns.UsrK, usrK)
				)
			);

			//	q.TableElement=new Join(
			//		q.TableElement,
			//		new TableElement(TablesEnum.ThreadUsr),
			//		QueryJoinType.Left,
			//		new And(
			//			new Q(Thread.Columns.K,ThreadUsr.Columns.ThreadK,true),
			//			new Q(ThreadUsr.Columns.UsrK,usrK)
			//		)
			//	);

			q.OrderBy = new OrderBy(Comment.Columns.IndexInThread);
			q.QueryCondition = new And(
				queryCondition,
				new BetweenQ(Comment.Columns.IndexInThread, ((requestedPage - 1) * Vars.CommentsPerPage), ((requestedPage * Vars.CommentsPerPage) - 1)));

			return new CommentSet(q);

		}
		#endregion
		#region CommentsQ
		public Q CommentsQ
		{
			get
			{
				return new Q(Comment.Columns.ThreadK, K);
			}
		}
		#endregion
		#region Participants
		public UsrSet Participants
		{
			get
			{
				if (particiapnts == null)
				{
					Query q = new Query();
					q.NoLock = true;
					q.TableElement = Usr.PrivateMessageThreadJoin;
					q.QueryCondition = new Q(Columns.K, K);
					particiapnts = new UsrSet(q);
				}
				return particiapnts;
			}
		}
		UsrSet particiapnts;
		#endregion

		#endregion

		#region CheckPermissionRead(Usr u)
		public bool CheckPermissionRead(Usr u)
		{
			if (this.Private)
			{
				if (u == null)
					return false;

				try
				{
					ThreadUsr tu = new ThreadUsr(this.K, u.K);
					return !tu.Deleted;
				}
				catch
				{
					return false;
				}
			}
			else
			{
				if (this.GroupK > 0)
				{
					GroupUsr gu = this.Group.GetGroupUsr(u);
					if (this.PrivateGroup || this.GroupPrivate)
						return gu != null && gu.IsMember;
					else
						return this.Group.CanRead(u, gu);
				}
				return true;
			}
		}
		#endregion

		#region CheckPermissionPost(Usr u)
		public bool CheckPermissionPost(Usr u)
		{
			if (this.Private)
			{
				try
				{
					ThreadUsr tu = new ThreadUsr(this.K, u.K);
					return !tu.Deleted;
				}
				catch
				{
					return false;
				}
			}
			else
			{
				if (this.GroupK > 0)
				{
					GroupUsr gu = this.Group.GetGroupUsr(u);
					return this.Group.CanMember(u, gu);
				}
				return u != null;
			}
		}
		#endregion
		#region CheckPermissionPost(Usr u, ThreadUsr threadUsr, GroupUsr groupUsr)
		public bool CheckPermissionPost(Usr u, ThreadUsr threadUsr, GroupUsr groupUsr)
		{
			if (this.Private)
			{
				try
				{
					return !threadUsr.Deleted;
				}
				catch
				{
					return false;
				}
			}
			else
			{
				if (this.GroupK > 0)
				{
					return u.CanGroupMember(groupUsr);
				}
				return true;
			}
		}
		#endregion

		#region IName Members

		public string Name
		{
			get
			{
				return this.Subject;
			}
			set
			{
				// TODO:  Add Thread.Name setter implementation
			}
		}

		public string FriendlyName
		{
			get
			{
				return this.Subject;
			}
		}

		#endregion

		#region ParentForumObject
		public IDiscussable ParentForumObject
		{
			get
			{
				IDiscussable current = this.ParentDiscussable;
				while (current is IHasParentDiscussable)
				{

					if (((IHasParentDiscussable)current).ParentDiscussable == null)
					{
						break;
					}

					//if (

					//    ((IHasParentDiscussable)current).ParentObject != null &&
					//    ((IHasParentDiscussable)current).ParentObject is IName &&
					//    ((IHasParentDiscussable)current).ParentObject is IDiscussable &&
					//    ((IHasParentDiscussable)current).ParentObject is IObjectPage
					//    )
					//{
					//    return current;
					//}
					current = ((IHasParentDiscussable)current).ParentDiscussable;
				}
				return current;
			}
		}
		#endregion

		#region IArchive Members

		public DateTime ArchiveDateTime
		{
			get
			{
				return this.DateTime;
			}
		}

		public string ArchiveHtml(bool showCountry, bool showPlace, bool showVenue, bool showEvent, int size)
		{

			string rolloverHtml = "<div style=\"width:250px;\"><b>" + this.Subject + "</b>";
			if (this.IsReview)
			{
				if (showEvent)
					rolloverHtml += " - " + this.ParentEvent.Name;
				if (showVenue)
					rolloverHtml += " @ " + this.ParentEvent.Venue.Name;
				if (showPlace)
					rolloverHtml += " in " + this.ParentEvent.Venue.Place.NamePlainRegion;
				if (showCountry && Country.FilterK != this.ParentEvent.Venue.Place.CountryK)
					rolloverHtml += " (" + this.ParentEvent.Venue.Place.Country.FriendlyName + ")";

				rolloverHtml += ", " + this.ParentEvent.FriendlyDate(false);

				rolloverHtml += (showEvent ? "" : " -") + " by " + this.Usr.NickName;
				rolloverHtml += " - " + this.TotalComments + " comment" + (this.TotalComments == 1 ? "" : "s");
			}
			else if (this.IsNews)
			{
				rolloverHtml += "<br>By " + this.Usr.NickName;
				rolloverHtml += " - " + this.FriendlyTime(false);
				rolloverHtml += " - " + this.TotalComments + " comment" + (this.TotalComments == 1 ? "" : "s");
			}
			rolloverHtml += "</div>";
			rolloverHtml = HttpUtility.UrlEncodeUnicode(rolloverHtml).Replace("'", "\\'");
			string attribs = " onmouseover=\"stt('" + rolloverHtml + "');\" onmouseout=\"htm();\"";

			return "<a href=\"" + this.Url() + "\"><img " + attribs + " src=\"" + this.IconPath + "\" border=\"0\" class=\"ArchiveItem BorderBlack All\" style=\"margin:2px;\" width=\"" + size.ToString() + "\" height=\"" + size.ToString() + "\"></a>";
		}


		#endregion

		//#region IPic Members

		//public Guid Pic
		//{
		//    get
		//    {
		//        // TODO:  Add Thread.Pic getter implementation
		//        return new Guid ();
		//    }
		//    set
		//    {
		//        // TODO:  Add Thread.Pic setter implementation
		//    }
		//}

		//public bool HasPic
		//{
		//    get
		//    {
		//        // TODO:  Add Thread.HasPic getter implementation
		//        return false;
		//    }
		//}

		//public string PicPath
		//{
		//    get
		//    {
		//        // TODO:  Add Thread.PicPath getter implementation
		//        return null;
		//    }
		//}

		//public int PicMiscK
		//{
		//    get
		//    {
		//        // TODO:  Add Thread.PicMiscK getter implementation
		//        return 0;
		//    }
		//    set
		//    {
		//        // TODO:  Add Thread.PicMiscK setter implementation
		//    }
		//}

		//public int PicPhotoK
		//{
		//    get
		//    {
		//        // TODO:  Add Thread.PicPhotoK getter implementation
		//        return 0;
		//    }
		//    set
		//    {
		//        // TODO:  Add Thread.PicPhotoK setter implementation
		//    }
		//}

		//public string PicState
		//{
		//    get
		//    {
		//        // TODO:  Add Thread.PicState getter implementation
		//        return null;
		//    }
		//    set
		//    {
		//        // TODO:  Add Thread.PicState setter implementation
		//    }
		//}

		//public Misc PicMisc
		//{
		//    get
		//    {
		//        // TODO:  Add Thread.PicMisc getter implementation
		//        return null;
		//    }
		//    set
		//    {
		//        // TODO:  Add Thread.PicMisc setter implementation
		//    }
		//}

		//public Photo PicPhoto
		//{
		//    get
		//    {
		//        // TODO:  Add Thread.PicPhoto getter implementation
		//        return null;
		//    }
		//    set
		//    {
		//        // TODO:  Add Thread.PicPhoto setter implementation
		//    }
		//}

		//#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion


		#region GetThreadsByIDiscussable
		public static ThreadSet GetThreadsByIDiscussable(IDiscussable discussable, int threadsCount, bool hotTopicsOnly)
		{


			int usrK = 0;
			if (Usr.Current != null)
				usrK = Usr.Current.K;

			Query q = new Query();
			q.TopRecords = threadsCount;

			#region Columns
			q.Columns = new ColumnSet(
				Thread.Columns.K,
				Thread.Columns.Private,
				Thread.Columns.GroupPrivate,
				Thread.Columns.PrivateGroup,
				Thread.Columns.Subject,
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
				Thread.Columns.UrlFragment,

				ThreadUsr.Columns.ThreadK,
				ThreadUsr.Columns.UsrK,
				ThreadUsr.Columns.Status,
				ThreadUsr.Columns.StatusChangeObjectType,
				ThreadUsr.Columns.Favourite,
				ThreadUsr.Columns.ViewComments,
				ThreadUsr.Columns.ViewCommentsLatest,
				ThreadUsr.Columns.ViewDateTime,
				ThreadUsr.Columns.ViewDateTimeLatest,

				Thread.Columns.UsrK,
				//new JoinedColumnSet(Thread.Columns.UsrK, Usr.LinkColumns),

				Thread.Columns.LastPostUsrK,
				//new JoinedColumnSet(Thread.Columns.LastPostUsrK, Usr.LinkColumns),

				Thread.Columns.FirstParticipantUsrK
				//new JoinedColumnSet(Thread.Columns.FirstParticipantUsrK, Usr.LinkColumns),

				//Photo.Columns.K,
				//Photo.Columns.Icon,
				//Photo.Columns.ContentDisabled
			);
			#endregion

			if (hotTopicsOnly)
				q.OrderBy = Thread.HotTopicsOrderBy;
			else
				q.OrderBy = Thread.Order;


			q.TableElement = new TableElement(TablesEnum.Thread);

			#region Main querycondition
			if (discussable == null)
				q.QueryCondition = new Q(true);
			else if (discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Event)
				q.QueryCondition = new Q(Thread.Columns.EventK, discussable.UsedDiscussable.K);
			else if (discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Venue)
				q.QueryCondition = new Q(Thread.Columns.VenueK, discussable.UsedDiscussable.K);
			else if (discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Place)
				q.QueryCondition = new Q(Thread.Columns.PlaceK, discussable.UsedDiscussable.K);
			else if (discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Country)
				q.QueryCondition = Country.ThreadsQ(discussable.UsedDiscussable.K);
			else if (discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Article)
			{
				Article a = (Article)discussable.UsedDiscussable;
				if (a.ThreadK > 0)
					q.QueryCondition = new And(
						Article.ThreadsQ(discussable.UsedDiscussable.K),
						new Q(Thread.Columns.K, QueryOperator.NotEqualTo, a.ThreadK));
				else
					q.QueryCondition = Article.ThreadsQ(discussable.UsedDiscussable.K);
			}
			else if (discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Brand)
			{
				if (((Brand)discussable.UsedDiscussable).BrandPageShowEventChat)
				{
					q.TableElement = Thread.EventBrandLeftJoin;
					q.QueryCondition = Brand.ThreadsQEvents((Brand)discussable.UsedDiscussable);
				}
				else
					q.QueryCondition = Group.ThreadsQ(((Brand)discussable.UsedDiscussable).GroupK);
			}
			else if (discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Group)
				q.QueryCondition = Group.ThreadsQ(discussable.UsedDiscussable.K);
			else if (discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Photo)
			{
				Photo p = (Photo)discussable.UsedDiscussable;
				if (p.ThreadK > 0)
					q.QueryCondition = new And(
						new Q(Thread.Columns.PhotoK, discussable.UsedDiscussable.K),
						new Q(Thread.Columns.K, QueryOperator.NotEqualTo, p.ThreadK));
				else
					q.QueryCondition = new Q(Thread.Columns.PhotoK, discussable.UsedDiscussable.K);
			}
			#endregion

			if (hotTopicsOnly)
			{
				q.QueryCondition = new And(
					q.QueryCondition,
					new Q(Thread.Columns.TotalComments, QueryOperator.GreaterThan, 20),
					new Q(Thread.Columns.AverageCommentDateTime, QueryOperator.GreaterThan, DateTime.Now.AddDays(-14))
				);
			}

			if (Usr.Current != null &&
				discussable != null &&
				discussable.UsedDiscussable.ShowPrivateThreads
			)
			{
				#region ThreadUsr join
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
				#endregion

				#region GroupUsr join
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.GroupUsr),
					QueryJoinType.Left,
					new And(
						new Q(Thread.Columns.GroupK, GroupUsr.Columns.GroupK, true),
						new Q(GroupUsr.Columns.UsrK, usrK),
						new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)
					)
				);
				#endregion

				#region Privacy querycondition
				q.QueryCondition = new And(
					q.QueryCondition,
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
				#endregion
			}
			else
			{
				#region ThreadUsr join
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.ThreadUsr),
					QueryJoinType.Left,
					new And(
						new Q(Thread.Columns.K, ThreadUsr.Columns.ThreadK, true),
						new Q(ThreadUsr.Columns.UsrK, usrK)
					)
				);
				#endregion

				#region Privacy querycondition
				q.QueryCondition = new And(
					q.QueryCondition,
					new Q(Thread.Columns.Private, false),
					new Q(Thread.Columns.GroupPrivate, false),
					new Q(Thread.Columns.PrivateGroup, false)
				);
				#endregion
			}

			try
			{
				return new ThreadSet(q);
			}
			catch
			{
				q.QueryCondition = new Q(Thread.Columns.K, Vars.DevEnv ? 3252221 : 3252503);
				return new ThreadSet(q);
			}
		}
		#endregion

		#region GetThreadUrl(Thread t, object[] par)
		public string GetThreadUrl(IDiscussable discussable, object[] par)
		{
			string[] sA = new string[par.Length];
			for (int i = 0; i < par.Length; i++) { sA[i] = par[i].ToString(); }
			if (discussable == null)
			{
				return UrlInfo.MakeUrl("", "chat", par);
			}
			else if (discussable.UsedDiscussable is Photo || discussable.UsedDiscussable is Article)
			{
				return this.Url(sA);
			}
			else if (discussable.UsedDiscussable is IDiscussable)
			{
				return ((IDiscussable)discussable.UsedDiscussable).UrlDiscussion(sA);
			}

			else
				return "";
		}
		public string GetThreadUrlSimple(IDiscussable discussable)
		{
			if (discussable != null && (discussable.UsedDiscussable is Photo || discussable.UsedDiscussable is Article))
				return this.Url();
			else if (discussable != null && discussable.UsedDiscussable is IDiscussable)
				return ((IDiscussable)discussable.UsedDiscussable).UrlDiscussion("k", this.K.ToString());
			else
				return UrlInfo.MakeUrl("", "chat", "k", this.K.ToString());
		}
		#endregion


		#region PostComment
		public static Thread.MakerReturn CreateNewThreadInGroup(int groupK, IDiscussable discussable, Guid duplicateGuid, string commentHtml, bool isNews, List<int> invitedUsrKs, bool isPrivate)
		{
			Group g = new Group(groupK);
			GroupUsr gu = g.GetGroupUsr(Usr.Current);
			if (!g.CanMember(Usr.Current, gu))
				throw new DsiUserFriendlyException("You can't post to this group!");

			Thread.Maker m = new Thread.Maker();
			m.PostingUsr = Usr.Current;
			m.Body = commentHtml;

			string stripped = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(m.Body);
			if (stripped.Trim().Length == 0)
				stripped = "[no subject]";

			m.Subject = ((stripped.Length > 50) ? (stripped.Substring(0, 47) + "...") : stripped);

			m.ParentType = discussable.ObjectType;
			m.ParentK = discussable.K;
			m.DuplicateGuid = duplicateGuid;
			m.Private = isPrivate;

			if (isNews)
				m.News = true;

			m.InviteKs = invitedUsrKs;
			m.GroupK = g.K;

			Thread.MakerReturn r = m.Post();
			return r;
		}

		public static Thread.MakerReturn CreatePrivateThread(IDiscussable discussable, Guid duplicateGuid, string commentHtml, List<int> invitedUsrKs, bool isSealed)
		{
			Thread.Maker m = new Thread.Maker();
			m.PostingUsr = Usr.Current;
			m.Body = commentHtml;

			string stripped = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(m.Body);
			if (stripped.Trim().Length == 0)
				stripped = "[no subject]";

			m.Subject = ((stripped.Length > 50) ? stripped.TruncateWithDots(50) : stripped);

			m.ParentType = discussable.ObjectType;
			m.ParentK = discussable.K;
			m.DuplicateGuid = duplicateGuid;
			m.Private = true;
			m.InviteKs = invitedUsrKs;
			m.Sealed = isSealed;

			Thread.MakerReturn r = m.Post();
			return r;
		}

		public static Comment.MakerReturn CreateReply(Thread thread, Guid duplicateGuid, string commentHtml, List<int> invitedUsrKs)
		{
			Comment.Maker m = thread.GetCommentMaker();
			m.DuplicateGuid = duplicateGuid;
			m.Body = commentHtml;

			m.InviteKs = invitedUsrKs;

			m.PostingUsr = Usr.Current;
			m.CurrentThreadUsr = thread.GetThreadUsr(Usr.Current);
			if (thread.GroupK > 0)
				m.CurrentGroupUsr = thread.Group.GetGroupUsr(Usr.Current);

			Comment.MakerReturn r = m.Post(null);
			return r;
		}

		public static Thread.MakerReturn CreateNewPublicThread(IDiscussable discussable, Guid duplicateGuid, string commentHtml, bool isNews, List<int> invitedUsrKs)
		{
			Thread.Maker m = new Thread.Maker();
			m.PostingUsr = Usr.Current;
			m.Body = commentHtml;
			m.Subject = Cambro.Misc.Utility.Snip(Cambro.Web.Helpers.Strip(m.Body), 50);
			m.ParentType = discussable.ObjectType;
			m.ParentK = discussable.K;
			m.DuplicateGuid = duplicateGuid;

			if (isNews)
				m.News = true;

			m.InviteKs = invitedUsrKs;

			Thread.MakerReturn r = m.Post();

			if (r.Success)
			{
				CheckToPutThreadInCaptionCompetition(discussable, r.Thread);
			}
			return r;
		}

		private static void CheckToPutThreadInCaptionCompetition(IDiscussable discussable, Thread thread)
		{
			if (Vars.IsCompetitionActive && discussable.ObjectType == Model.Entities.ObjectType.Photo)
			{
				Photo photo = (Photo)discussable;
				if (photo.IsInCaptionCompetition && (!photo.ThreadK.HasValue || !photo.Thread.IsInCaptionCompetition))
				{
					thread.GroupK = Vars.CompetitionGroupK;
					thread.IsInCaptionCompetition = true;
					thread.Update();

					photo.ThreadK = thread.K;
					photo.Update();
				}
			}
		}

		public static Thread.MakerReturn CreatePublicThread(IDiscussable parentObject, Guid duplicateGuid, string commentHtml, bool isNews, List<int> invitedUsrKs, bool disableLiveChatMessage)
		{
			Thread.Maker m = new Thread.Maker();
			m.PostingUsr = Usr.Current;
			string subject = "";
			if (parentObject is Article)
			{
				subject = Cambro.Misc.Utility.Snip(((Article)parentObject).Title, 50);
			}
			else
			{
				subject = Cambro.Misc.Utility.Snip(Cambro.Web.Helpers.Strip(commentHtml), 50);
			}
			m.Subject = subject;
			m.Body = commentHtml;
			m.ParentType = parentObject.ObjectType;
			m.ParentK = parentObject.K;
			m.DuplicateGuid = duplicateGuid;

			if (isNews)
				m.News = true;

			m.InviteKs = invitedUsrKs;
			m.DisableLiveChatMessage = disableLiveChatMessage;

			Thread.MakerReturn r = m.Post();

			if (r.Success)
			{
				if (parentObject is IHasPrimaryThread && !r.Thread.Private && r.Thread.GroupK == 0)
				{
					((IHasPrimaryThread)parentObject).UpdateSingleThread();
				}
				//if (parentObject is Article && !r.Thread.Private && r.Thread.GroupK == 0)
				//{
				//    Article article = (Article)parentObject;
				//    article.ThreadK = r.Thread.K;
				//    article.Update();
				//    article.UpdateTotalComments(null);
				//}
				//else
				//{
					CheckToPutThreadInCaptionCompetition(parentObject, r.Thread);
				//}
			}
			return r;
		}

		#endregion

	}
	#endregion

	#region ThreadUsr

	public class UpdateThreadUsrJob : Job
	{
		JobDataMapItemProperty<int> UsrK  { get { return new JobDataMapItemProperty<int>("UsrK", JobDataMap); } }
		JobDataMapItemProperty<ThreadUsr.StatusEnum> ChangeStatus { get { return new JobDataMapItemProperty<ThreadUsr.StatusEnum>("ChangeStatus", JobDataMap); } }
		JobDataMapItemProperty<List<ThreadUsr.StatusEnum>> ThreadStatusesToChange { get { return new JobDataMapItemProperty<List<ThreadUsr.StatusEnum>>("ThreadStatusesToChange", JobDataMap); } }
		JobDataMapItemProperty<Model.Entities.ObjectType?> StatusChangeObjectType { get { return new JobDataMapItemProperty<Model.Entities.ObjectType?>("StatusChangeObjectType", JobDataMap); } }
		JobDataMapItemProperty<int?> StatusChangeObjectK { get { return new JobDataMapItemProperty<int?>("StatusChangeObjectK", JobDataMap); } }
		JobDataMapItemProperty<string> MemcachedKey { get { return new JobDataMapItemProperty<string>("MemcachedKey", JobDataMap); } }
		JobDataMapItemProperty<bool> IsAlreadyRunning { get { return new JobDataMapItemProperty<bool>("IsAlreadyRunning", JobDataMap); } }

		public UpdateThreadUsrJob()
		{}
		public UpdateThreadUsrJob(int usrK, ThreadUsr.StatusEnum changeStatus, List<ThreadUsr.StatusEnum> threadStatusesToChange, Model.Entities.ObjectType? statusChangeObjectType, int? statusChangeObjectK, string memcachedKey)
		{
			UsrK.Value = usrK;
			ChangeStatus.Value = changeStatus;
			ThreadStatusesToChange.Value = threadStatusesToChange;
			StatusChangeObjectType.Value = statusChangeObjectType;
			StatusChangeObjectK.Value = statusChangeObjectK;
			MemcachedKey.Value = memcachedKey;

			object cachedJobStatus = Caching.Instances.Main.Get(memcachedKey);
			if (cachedJobStatus != null && (Bobs.JobProcessor.Job.JobStatus.Queued == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus || Bobs.JobProcessor.Job.JobStatus.Running == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus))
			{
				IsAlreadyRunning.Value = true;
			}
			else
			{
				Caching.Instances.Main.Store(memcachedKey, Job.JobStatus.Queued, new TimeSpan(0, 15, 0));
				IsAlreadyRunning.Value = false;
			}
		}

		protected override void Execute()
		{
			if (!IsAlreadyRunning.Value)
			{
				Caching.Instances.Main.Store(MemcachedKey.Value, Job.JobStatus.Running, new TimeSpan(0, 15, 0));

				Update uThreadUsr = new Update();
				try
				{
					if (StatusChangeObjectType.Value == null && StatusChangeObjectK.Value != null)
						throw new Exception("Usr.UpdateThreadUsrJob(): Invalid StatusChangeObject.");
			
					uThreadUsr.Table = TablesEnum.ThreadUsr;
					uThreadUsr.Changes.Add(new Assign(ThreadUsr.Columns.Status, ChangeStatus.Value));
					uThreadUsr.Changes.Add(new Assign(ThreadUsr.Columns.StatusChangeDateTime, Common.Time.Now));
					uThreadUsr.Where = new Q(ThreadUsr.Columns.UsrK, UsrK.Value);

					if (ThreadStatusesToChange.Value != null && ThreadStatusesToChange.Value.Count > 0)
					{
						Or statusOr = new Or();
						foreach (ThreadUsr.StatusEnum statusEnum in ThreadStatusesToChange.Value)
						{
							statusOr = new Or(statusOr,
											new Q(ThreadUsr.Columns.Status, statusEnum));
						}
						uThreadUsr.Where = new And(uThreadUsr.Where,
												   statusOr);
					}
					else
						throw new Exception("Usr.UpdateThreadUsrs(): Invalid list of ThreadUsr.StatusEnum to change.");

					if (StatusChangeObjectType.Value != null)
					{
						if (StatusChangeObjectType.Value == Model.Entities.ObjectType.Usr)
						{
							// do nothing here
						}
						else
						{
							uThreadUsr.Where = new And(uThreadUsr.Where,
													   new Q(ThreadUsr.Columns.StatusChangeObjectType, StatusChangeObjectType.Value));
						}

						if (StatusChangeObjectK.Value != null)
						{
							if (StatusChangeObjectType.Value == Model.Entities.ObjectType.Usr)
							{
								uThreadUsr.Where = new And(uThreadUsr.Where,
														   new Q(ThreadUsr.Columns.InvitingUsrK, StatusChangeObjectK.Value));
							}
							else
							{
								uThreadUsr.Where = new And(uThreadUsr.Where,
														   new Q(ThreadUsr.Columns.StatusChangeObjectK, StatusChangeObjectK.Value));
							}
						}
					}

					uThreadUsr.CommandTimeout = 300;
					uThreadUsr.Run();

					// Update memcached
					Caching.Instances.Main.Store(MemcachedKey.Value, Job.JobStatus.Completed, new TimeSpan(0, 15, 0));
				}
				catch (Exception ex)
				{
					Caching.Instances.Main.Store(MemcachedKey.Value, Job.JobStatus.Failed, new TimeSpan(0, 15, 0));
					throw ex;
				}
			}
		}
	}

	public class SmartDeleteThreadUsrJob : Job
	{
		public const int SMART_DELETE_CUT_OFF_NUMBER_OF_PEOPLE_WATCHING = 50;

		JobDataMapItemProperty<int> UsrK { get { return new JobDataMapItemProperty<int>("UsrK", JobDataMap); } }
		JobDataMapItemProperty<List<ThreadUsr.StatusEnum>> ThreadStatusesToChange { get { return new JobDataMapItemProperty<List<ThreadUsr.StatusEnum>>("ThreadStatusesToChange", JobDataMap); } }
		JobDataMapItemProperty<Model.Entities.ObjectType?> StatusChangeObjectType { get { return new JobDataMapItemProperty<Model.Entities.ObjectType?>("StatusChangeObjectType", JobDataMap); } }
		JobDataMapItemProperty<int?> StatusChangeObjectK { get { return new JobDataMapItemProperty<int?>("StatusChangeObjectK", JobDataMap); } }
		JobDataMapItemProperty<string> MemcachedKey { get { return new JobDataMapItemProperty<string>("MemcachedKey", JobDataMap); } }
		JobDataMapItemProperty<bool> IsAlreadyRunning { get { return new JobDataMapItemProperty<bool>("IsAlreadyRunning", JobDataMap); } }

		public SmartDeleteThreadUsrJob()
		{ }
		public SmartDeleteThreadUsrJob(int usrK, List<ThreadUsr.StatusEnum> threadStatusesToChange, Model.Entities.ObjectType? statusChangeObjectType, int? statusChangeObjectK, string memcachedKey)
		{
			UsrK.Value = usrK;
			ThreadStatusesToChange.Value = threadStatusesToChange;
			StatusChangeObjectType.Value = statusChangeObjectType;
			StatusChangeObjectK.Value = statusChangeObjectK;
			MemcachedKey.Value = memcachedKey;

			object cachedJobStatus = Caching.Instances.Main.Get(memcachedKey);
			if (cachedJobStatus != null && (Bobs.JobProcessor.Job.JobStatus.Queued == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus || Bobs.JobProcessor.Job.JobStatus.Running == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus))
			{
				IsAlreadyRunning.Value = true;
			}
			else
			{
				Caching.Instances.Main.Store(memcachedKey, Job.JobStatus.Queued, new TimeSpan(0, 15, 0));
				IsAlreadyRunning.Value = false;
			}
		}

		private void RunUpdate(bool isArchive)
		{
			if (StatusChangeObjectType.Value == null && StatusChangeObjectK.Value != null)
				throw new Exception("Usr.SmartDeleteThreadUsrJob(): Invalid StatusChangeObject.");

			if(ThreadStatusesToChange.Value.Count == 0)
				throw new Exception("Usr.SmartDeleteThreadUsrJob(): Invalid ThreadStatusesToChange.");

			Update uThreadUsr = new Update();

			uThreadUsr.Table = TablesEnum.ThreadUsr;
			uThreadUsr.From = new Join(ThreadUsr.Columns.ThreadK, Thread.Columns.K);
			uThreadUsr.Changes.Add(new Assign(ThreadUsr.Columns.StatusChangeDateTime, Common.Time.Now));
			uThreadUsr.Where = new Q(ThreadUsr.Columns.UsrK, UsrK.Value);

			if (isArchive)
			{
				uThreadUsr.Changes.Add(new Assign(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.Archived));
				uThreadUsr.Where = new And(uThreadUsr.Where,
										   new Q(Thread.Columns.TotalWatching, QueryOperator.LessThanOrEqualTo, SMART_DELETE_CUT_OFF_NUMBER_OF_PEOPLE_WATCHING));
			}
			else
			{
				uThreadUsr.Changes.Add(new Assign(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.Ignore));
				uThreadUsr.Where = new And(uThreadUsr.Where,
										   new Q(Thread.Columns.TotalWatching, QueryOperator.GreaterThan, SMART_DELETE_CUT_OFF_NUMBER_OF_PEOPLE_WATCHING));
			}
			if (ThreadStatusesToChange.Value != null && ThreadStatusesToChange.Value.Count > 0)
			{
				Or statusOr = new Or();
				foreach (ThreadUsr.StatusEnum statusEnum in ThreadStatusesToChange.Value)
				{
					statusOr = new Or(statusOr,
									new Q(ThreadUsr.Columns.Status, statusEnum));
				}
				uThreadUsr.Where = new And(uThreadUsr.Where,
										   statusOr);
			}
			else
				throw new Exception("Usr.SmartDeleteThreadUsrJob(): Invalid list of ThreadUsr.StatusEnum to change.");


			if (StatusChangeObjectType.Value != null)
			{
				if (StatusChangeObjectType.Value == Model.Entities.ObjectType.Usr)
				{
					// do nothing here
				}
				else
				{
					uThreadUsr.Where = new And(uThreadUsr.Where,
											   new Q(ThreadUsr.Columns.StatusChangeObjectType, StatusChangeObjectType.Value));
				}

				if (StatusChangeObjectK.Value != null)
				{
					if (StatusChangeObjectType.Value == Model.Entities.ObjectType.Usr)
					{
						uThreadUsr.Where = new And(uThreadUsr.Where,
												   new Q(ThreadUsr.Columns.InvitingUsrK, StatusChangeObjectK.Value));
					}
					else
					{
						uThreadUsr.Where = new And(uThreadUsr.Where,
												   new Q(ThreadUsr.Columns.StatusChangeObjectK, StatusChangeObjectK.Value));
					}
				}

			}
			uThreadUsr.CommandTimeout = 300;
			uThreadUsr.Run();

		}


		protected override void Execute()
		{
			if (!IsAlreadyRunning.Value)
			{
				try
				{
					Caching.Instances.Main.Store(MemcachedKey.Value, Job.JobStatus.Running, new TimeSpan(0, 15, 0));

					RunUpdate(true);
					RunUpdate(false);

					// Update memcached
					Caching.Instances.Main.Store(MemcachedKey.Value, Job.JobStatus.Completed, new TimeSpan(0, 15, 0));
				}
				catch (Exception ex)
				{
					Caching.Instances.Main.Store(MemcachedKey.Value, Job.JobStatus.Failed, new TimeSpan(0, 15, 0));
					throw ex;
				}
			}
		}
	}
	#endregion

}

