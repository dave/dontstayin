using System;
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
using System.Collections.Generic;
using Bobs.CachedDataAccess;

namespace Bobs
{

	#region Group
	/// <summary>
	/// e.g. Promoter / Event Group
	/// </summary>
	[Serializable]
	public partial class Group : IPage, IPic, IName, IReadableReference, IBobType, IDiscussable, IObjectPage, ICalendar, IDeleteAll, IHasArchive, IConnectedTo, ILinkable, IHasIcon, IPicObjectPage, IPicHasIconObjectPage
	{
		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Group.Columns.K] as int? ?? 0; }
			set { this[Group.Columns.K] = value; }
		}
		/// <summary>
		/// Name of the group
		/// </summary>
		public override string Name
		{
			get { return (string)this[Group.Columns.Name]; }
			set { this[Group.Columns.Name] = value; }
		}
		/// <summary>
		/// Text describing the group topic or scope
		/// </summary>
		public override string Description
		{
			get { return (string)this[Group.Columns.Description]; }
			set { this[Group.Columns.Description] = value; }
		}
		/// <summary>
		/// Html shown on the group homepage
		/// </summary>
		public override string LongDescriptionHtml
		{
			get { return (string)this[Group.Columns.LongDescriptionHtml]; }
			set { this[Group.Columns.LongDescriptionHtml] = value; }
		}
		/// <summary>
		/// Is the long description surrounded by a div and checked for proper html?
		/// </summary>
		public override bool LongDescriptionPlain
		{
			get { return (bool)this[Group.Columns.LongDescriptionPlain]; }
			set { this[Group.Columns.LongDescriptionPlain] = value; }
		}
		/// <summary>
		/// Posting rules that people have to agree to before joining
		/// </summary>
		public override string PostingRules
		{
			get { return (string)this[Group.Columns.PostingRules]; }
			set { this[Group.Columns.PostingRules] = value; }
		}
		/// <summary>
		/// DateTime the group was added
		/// </summary>
		public override DateTime DateTimeCreated
		{
			get { return (DateTime)this[Group.Columns.DateTimeCreated]; }
			set { this[Group.Columns.DateTimeCreated] = value; }
		}
		/// <summary>
		/// Total number of members in this group
		/// </summary>
		public override int TotalMembers
		{
			get { return (int)this[Group.Columns.TotalMembers]; }
			set { this[Group.Columns.TotalMembers] = value; }
		}
		/// <summary>
		/// Total number of moderators in this group
		/// </summary>
		public override int TotalModerators
		{
			get { return (int)this[Group.Columns.TotalModerators]; }
			set { this[Group.Columns.TotalModerators] = value; }
		}
		/// <summary>
		/// Total number of owners of this group
		/// </summary>
		public override int TotalOwners
		{
			get { return (int)this[Group.Columns.TotalOwners]; }
			set { this[Group.Columns.TotalOwners] = value; }
		}
		/// <summary>
		/// Total number of comments
		/// </summary>
		public override int TotalComments
		{
			get { return (int)this[Group.Columns.TotalComments]; }
			set { this[Group.Columns.TotalComments] = value; }
		}
		/// <summary>
		/// DateTime of the last post
		/// </summary>
		public override DateTime? LastPost
		{
			get { return this[Group.Columns.LastPost] as DateTime?; }
			set { this[Group.Columns.LastPost] = value; }
		}
		/// <summary>
		/// Average DateTime of all the comments
		/// </summary>
		public override DateTime? AverageCommentDateTime
		{
			get { return this[Group.Columns.AverageCommentDateTime] as DateTime?; }
			set { this[Group.Columns.AverageCommentDateTime] = value; }
		}
		/// <summary>
		/// Private group page?
		/// </summary>
		public override bool PrivateGroupPage
		{
			get { return (bool)this[Group.Columns.PrivateGroupPage]; }
			set { this[Group.Columns.PrivateGroupPage] = value; }
		}
		/// <summary>
		/// Private chat forum?
		/// </summary>
		public override bool PrivateChat
		{
			get { return (bool)this[Group.Columns.PrivateChat]; }
			set { this[Group.Columns.PrivateChat] = value; }
		}
		/// <summary>
		/// Private members list?
		/// </summary>
		public override bool PrivateMemberList
		{
			get { return (bool)this[Group.Columns.PrivateMemberList]; }
			set { this[Group.Columns.PrivateMemberList] = value; }
		}
		/// <summary>
		/// Membership restriction (Automatic=1, OwnerMustApprove=2, Custom=3)
		/// </summary>
		public override RestrictionEnum Restriction
		{
			get { return (RestrictionEnum)this[Group.Columns.Restriction]; }
			set { this[Group.Columns.Restriction] = value; }
		}
		/// <summary>
		/// Enum specifying a membership object - e.g. admins, photo moderators, promoters etc.
		/// </summary>
		public override CustomRestrictionTypes CustomRestrictionType
		{
			get { return (CustomRestrictionTypes)this[Group.Columns.CustomRestrictionType]; }
			set { this[Group.Columns.CustomRestrictionType] = value; }
		}
		/// <summary>
		/// Group theme
		/// </summary>
		public override int ThemeK
		{
			get { return (int)this[Group.Columns.ThemeK]; }
			set { theme = null; this[Group.Columns.ThemeK] = value; }
		}
		/// <summary>
		/// Is the group country specific?
		/// </summary>
		public override int CountryK
		{
			get { return (int)this[Group.Columns.CountryK]; }
			set { country = null; this[Group.Columns.CountryK] = value; }
		}
		/// <summary>
		/// Is the group place specific?
		/// </summary>
		public override int PlaceK
		{
			get { return (int)this[Group.Columns.PlaceK]; }
			set { place = null; this[Group.Columns.PlaceK] = value; }
		}
		/// <summary>
		/// Is the group music specific?
		/// </summary>
		public override int MusicTypeK
		{
			get { return (int)this[Group.Columns.MusicTypeK]; }
			set { musicType = null; this[Group.Columns.MusicTypeK] = value; }
		}
		/// <summary>
		/// Is the group brand specific?
		/// </summary>
		public override int BrandK
		{
			get { return (int)this[Group.Columns.BrandK]; }
			set { brand = null; this[Group.Columns.BrandK] = value; }
		}
		/// <summary>
		/// Unique url-compliant name
		/// </summary>
		public override string UrlName
		{
			get { return (string)this[Group.Columns.UrlName]; }
			set { this[Group.Columns.UrlName] = value; }
		}
		/// <summary>
		/// Cropped image 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Group.Columns.Pic]); }
			set { this[Group.Columns.Pic] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Group.Columns.PicState]; }
			set { this[Group.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Group.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Group.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Group.Columns.PicMiscK]; }
			set { picMisc = null; this[Group.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// Guid used to ensure duplicate groups don't get posted if the user refreshes the page after saving.
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Group.Columns.DuplicateGuid]); }
			set { this[Group.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Force members to receive an email each time a new thread is posted to this group
		/// </summary>
		public override bool EmailOnAllThreads
		{
			get { return (bool)this[Group.Columns.EmailOnAllThreads]; }
			set { this[Group.Columns.EmailOnAllThreads] = value; }
		}
		/// <summary>
		/// The total number of members that have this group on their favourites list
		/// </summary>
		public override int FavouriteCount
		{
			get { return (int)this[Group.Columns.FavouriteCount]; }
			set { this[Group.Columns.FavouriteCount] = value; }
		}
		/// <summary>
		/// The total number of members that are watching this group for new messages
		/// </summary>
		public override int WatchCount
		{
			get { return (int)this[Group.Columns.WatchCount]; }
			set { this[Group.Columns.WatchCount] = value; }
		}
		#endregion

		#region ILinkable Members

		public string Link(params string[] par)
		{
			return ILinkableExtentions.Link(this, par);
		}
		public string LinkNewWindow(params string[] par)
		{
			return ILinkableExtentions.LinkNewWindow(this, par);
		}

		#endregion

		#region LinkedTables
		public CachedSqlSelect<Photo> ChildPhotos()
		{
			return this.ChildPhotos(null, null);
		}
		public CachedSqlSelect<Photo> ChildPhotos(Q where)
		{
			return this.ChildPhotos(where, null);
		}
		public CachedSqlSelect<Photo> ChildPhotos(params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			return this.ChildPhotos(null, orderBy);
		}
		public CachedSqlSelect<Photo> ChildPhotos(Q where, params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			PhotoTableDef def = new PhotoTableDef();
			GroupPhotoTableDef def2 = new GroupPhotoTableDef();
			return new CachedSqlSelect<Photo>(
				new LinkedChildren<Photo>
				(
					TablesEnum.Group,
					this.K,
					TablesEnum.Photo,
					def.TableCacheKey,
					dr =>
					{
						Photo newPhoto = new Photo();
						newPhoto.Initialise(dr);
						return newPhoto;
					},
					where,
					orderBy,
					TablesEnum.GroupPhoto,
					def2.TableCacheKey
				)
			);
		}
		#endregion


		#region IHasArchive
		public string UrlArchiveDate(int Year, int Month, int Day, Model.Entities.ArchiveObjectType Type, params string[] par)
		{
			return Vars.GetArchiveUrl(Year,Month,Day,Type,par,UrlFilterPart);
		}
		public string UrlArchive(Model.Entities.ArchiveObjectType type, params string[] par)
		{
			return UrlArchiveDate(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, type, par);
		}
		#endregion

		#region UrlGroupPhotos
		public string UrlGroupPhotos(params string[] par)
		{
			return UrlInfo.MakeUrl(this.UrlFilterPart,"topphotos",par);
		}
		#endregion
		#region UrlGroupPhotosDate
		public string UrlGroupPhotosDate(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}/{3}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower(), d.Day.ToString("00")),"topphotos",par);
		}
		#endregion
		#region UrlGroupPhotosMonth
		public string UrlGroupPhotosMonth(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower()),"topphotos",par);
		}
		#endregion

		#region MergeAndDelete
		public void MergeAndDelete(Group merge)
		{
			if (this.K==merge.K)
				throw new DsiUserFriendlyException("Can't merge group into itself!");

			Cambro.Web.Helpers.WriteAlertHeader();

			Cambro.Web.Helpers.WriteAlert("Starting group merge...", true);

			Cambro.Web.Helpers.WriteAlert("Merging group members...", true);
			//members
			Query qGu = new Query();
			qGu.QueryCondition=new Q(GroupUsr.Columns.GroupK,merge.K);
			GroupUsrSet gus = new GroupUsrSet(qGu);
			int count = 0;
			foreach (GroupUsr guMerge in gus)
			{
				if (count%10==0)
					Cambro.Web.Helpers.WriteAlert("Merging usr " + guMerge.UsrK + " ("+count+" / "+gus.Count+")...");

				try
				{

					count++;
					GroupUsr guMaster = this.GetGroupUsr(guMerge.Usr);
					if (guMaster == null)
					{
						guMaster = new GroupUsr();
						guMaster.UsrK = guMerge.UsrK;
						guMaster.GroupK = this.K;
						guMaster.Status = guMerge.Status;
						guMaster.StatusChangeDateTime = guMerge.StatusChangeDateTime;
						guMaster.StatusChangeUsrK = guMerge.StatusChangeUsrK;
						guMaster.Owner = guMerge.Owner;
						guMaster.Moderator = guMerge.Moderator;
						guMaster.NewsAdmin = guMerge.NewsAdmin;
						guMaster.MemberAdmin = guMerge.MemberAdmin;
						guMaster.MemberAdminNewUserEmails = guMerge.MemberAdminNewUserEmails;
						guMaster.Favourite = guMerge.Favourite;
						guMaster.InviteMessage = guMerge.InviteMessage;
						guMaster.InviteUsrK = guMerge.InviteUsrK;
						guMaster.Update();

						guMaster.Usr.UpdateIsGroupModerator();

					}
					else
					{
						if (guMaster.StatusPermissionLevel < guMerge.StatusPermissionLevel)
						{
							guMaster.Status = guMerge.Status;
							guMaster.StatusChangeDateTime = guMerge.StatusChangeDateTime;
							guMaster.StatusChangeUsrK = guMerge.StatusChangeUsrK;
							guMaster.InviteMessage = guMerge.InviteMessage;
							guMaster.InviteUsrK = guMerge.InviteUsrK;
						}
						if (this.BrandK == 0 || merge.BrandK > 0)
						{
							if (guMerge.Owner)
								guMaster.Owner = true;
							if (guMerge.Moderator)
								guMaster.Moderator = true;
							if (guMerge.NewsAdmin)
								guMaster.NewsAdmin = true;
							if (guMerge.MemberAdmin)
							{
								guMaster.MemberAdmin = true;
								guMaster.MemberAdminNewUserEmails = guMerge.MemberAdminNewUserEmails;
							}
						}
						if (guMerge.Favourite)
							guMaster.Favourite = true;

						guMaster.Update();

						guMaster.Usr.UpdateIsGroupModerator();
					}

					if (guMaster.IsMember)
					{
						Mailer m = new Mailer();
						m.UsrRecipient = guMerge.Usr;
						m.Subject = "A group you were in has been merged";
						m.Body = "<p><i>" + merge.FriendlyName + "</i> has been merged with <i>" + this.FriendlyName + "</i>. " +
							"Your membership details have been moved across. If you ever want to exit the group, click the button " +
							"on the <a href=\"[LOGIN(" + this.Url() + ")]\">group homepage</a>.</p>";
						m.RedirectUrl = this.Url();
						m.Bulk = true;
						m.Send();
					}

					if (guMaster.IsMember && CommentAlert.IsEnabled(guMerge.UsrK, guMerge.GroupK, Model.Entities.ObjectType.Group))
						CommentAlert.Enable(guMaster.Usr, guMaster.GroupK, Model.Entities.ObjectType.Group);
				}
				catch 
				{
					Cambro.Web.Helpers.WriteAlert("Exception! ... deleting membership for usr " + guMerge.UsrK + " (" + count + " / " + gus.Count + ")...");
					guMerge.Delete();
				}
			}
			Cambro.Web.Helpers.WriteAlert("Done merging usrs...");

			//picture
			if (merge.HasPic && !this.HasPic)
			{
				Cambro.Web.Helpers.WriteAlert("Copying picture...", true);
				try
				{
					Utilities.CopyPic(merge, this);
				}
				catch
				{
					Cambro.Web.Helpers.WriteAlert("Exception while copying picture...", true);
				}
				Cambro.Web.Helpers.WriteAlert("Done copying picture...", true);
			}
			
			//recommended events
			if (this.BrandK == 0)
			{
				Cambro.Web.Helpers.WriteAlert("Merging recommended events...", true);
				Query qEv = new Query();
				qEv.QueryCondition = new Q(GroupEvent.Columns.GroupK, merge.K);
				GroupEventSet ges = new GroupEventSet(qEv);
				foreach (GroupEvent geMerge in ges)
				{
					try
					{
						GroupEvent geMaster = new GroupEvent(this.K, geMerge.EventK);
					}
					catch (BobNotFound)
					{
						GroupEvent geMaster = new GroupEvent();
						geMaster.GroupK = this.K;
						geMaster.EventK = geMerge.EventK;
						geMaster.Update();
					}
				}
				Cambro.Web.Helpers.WriteAlert("Done merging recommended events...");
			}

			Cambro.Web.Helpers.WriteAlert("Merging top photos...", true);
			//top photos
			Query qPh = new Query();
			qPh.QueryCondition=new Q(GroupPhoto.Columns.GroupK,merge.K);
			GroupPhotoSet gps = new GroupPhotoSet(qPh);
			foreach (GroupPhoto gpMerge in gps)
			{
				try
				{
					GroupPhoto gpMaster = new GroupPhoto(this.K,gpMerge.PhotoK);
					if (!gpMaster.ShowOnFrontPage && gpMerge.ShowOnFrontPage)
					{
						gpMaster.Caption=gpMerge.Caption;
						gpMaster.DateTime=gpMerge.DateTime;
						gpMaster.AddedByUsrK=gpMerge.AddedByUsrK;
						gpMaster.ShowOnFrontPage=gpMerge.ShowOnFrontPage;
						gpMaster.Update();
					}
				}
				catch (BobNotFound)
				{
					GroupPhoto gpMaster = new GroupPhoto();
					gpMaster.GroupK=this.K;
					gpMaster.PhotoK=gpMerge.PhotoK;
					gpMaster.Caption=gpMerge.Caption;
					gpMaster.DateTime=gpMerge.DateTime;
					gpMaster.AddedByUsrK=gpMerge.AddedByUsrK;
					gpMaster.ShowOnFrontPage=gpMerge.ShowOnFrontPage;
					gpMaster.Update();
				}
			}
			Cambro.Web.Helpers.WriteAlert("Done merging top photos...");

			//addedbyusrk's
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Updating invited users...", true);
				Update update = new Update();
				update.Changes.Add(new Assign(Usr.Columns.AddedByGroupK, this.K));
				update.Table = TablesEnum.Usr;
				update.Where = new Q(Usr.Columns.AddedByGroupK, merge.K);
				update.Run();
				Cambro.Web.Helpers.WriteAlert("Done updating invited users...");
			}

			Cambro.Web.Helpers.WriteAlert("Merging topics (1/4)...", true);
			//chats
			if (merge.PrivateChat != this.PrivateChat ||
				merge.ThemeK != this.ThemeK ||
				merge.MusicTypeK != this.MusicTypeK)
			{
				Update update = new Update();
				update.Table=TablesEnum.Thread;
				update.Changes.Add(new Assign(Thread.Columns.PrivateGroup,this.PrivateChat));
				update.Changes.Add(new Assign(Thread.Columns.ThemeK, this.ThemeK));
				update.Changes.Add(new Assign(Thread.Columns.MusicTypeK, this.MusicTypeK));
				update.Where=new Q(Thread.Columns.GroupK,merge.K);
				update.Run();
			}
			Cambro.Web.Helpers.WriteAlert("Done merging topics (1/4)...");

			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging topics (2/4)...", true);
				Update update = new Update();
				update.Table=TablesEnum.Thread;
				update.Changes.Add(new Assign(Thread.Columns.UrlFragment,"groups/"+this.UrlName));
				update.Changes.Add(new Assign(Thread.Columns.CountryK, this.CountryK));
				update.Changes.Add(new Assign(Thread.Columns.PlaceK,this.PlaceK));
				update.Where=new And(new Q(Thread.Columns.ParentObjectType,Model.Entities.ObjectType.Group),new Q(Thread.Columns.ParentObjectK,merge.K));
				update.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging topics (2/4)...");
			}

			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging topics (3/4)...", true);
				Update update = new Update();
				update.Table=TablesEnum.Thread;
				update.Changes.Add(new Assign(Thread.Columns.GroupK,this.K));
				update.Where=new Q(Thread.Columns.GroupK,merge.K);
				update.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging topics (3/4)...");
			}
			
			if (true)
			{
				Cambro.Web.Helpers.WriteAlert("Merging topics (4/4)...", true);
				Update update = new Update();
				update.Table=TablesEnum.Thread;
				update.Changes.Add(new Assign(Thread.Columns.ParentObjectK,this.K));
				update.Where=new And(new Q(Thread.Columns.ParentObjectType,Model.Entities.ObjectType.Group),new Q(Thread.Columns.ParentObjectK,merge.K));
				update.Run();
				Cambro.Web.Helpers.WriteAlert("Done merging topics (4/4)...");
			}

			Cambro.Web.Helpers.WriteAlert("Deleting old group...", true);
			merge.DeleteAll(null);
			Cambro.Web.Helpers.WriteAlert("Done deleting old group...");

			Cambro.Web.Helpers.WriteAlert("Updating stats...", true);
			this.UpdateTotalComments(null);
			this.UpdateTotalMembers();
			Cambro.Web.Helpers.WriteAlert("Done updating stats...");

			Cambro.Web.Helpers.WriteAlert("Finished merging groups.", true);


		}
		#endregion

		#region AllowJoinRequest
		public static bool AllowJoinRequest(Usr u, Group g, GroupUsr gu)
		{
			if (u==null)
				return false;

			if (g.PrivateGroupPage)
			{
				if (gu!=null && 
					(gu.Status.Equals(GroupUsr.StatusEnum.Exited) || 
					gu.Status.Equals(GroupUsr.StatusEnum.Invite) || 
					gu.Status.Equals(GroupUsr.StatusEnum.InviteRejected)))
					return true;
			}
			else
			{
				if (gu==null || 
					gu.Status.Equals(GroupUsr.StatusEnum.Exited) || 
					gu.Status.Equals(GroupUsr.StatusEnum.Invite) || 
					gu.Status.Equals(GroupUsr.StatusEnum.Recommend) || 
					gu.Status.Equals(GroupUsr.StatusEnum.RecommendRejected) || 
					gu.Status.Equals(GroupUsr.StatusEnum.InviteRejected))
					return true;
			}
			return false;
		}
		#endregion
		#region AllowInviteReject
		public static bool AllowInviteReject(Usr u, GroupUsr gu)
		{
			if (u==null)
				return false;

			if (gu==null)
				return false;

			return gu.Status.Equals(GroupUsr.StatusEnum.Invite);
		}
		#endregion

		#region CanRead
		public bool CanRead(Usr u, GroupUsr gu)
		{
			return (!this.PrivateChat || (u!=null && u.IsAdmin) || (gu!=null && gu.IsMember));
		}
		public bool IsRead(Usr u, GroupUsr gu)
		{
			return (!this.PrivateChat || (gu!=null && gu.IsMember));
		}
		#endregion
		#region CanMember
		public bool CanMember(Usr u, GroupUsr gu)
		{
			return (u!=null && u.IsAdmin) || (gu!=null && gu.IsMember);
		}
		#endregion
		#region IsMember
		public bool IsMember(Usr usr)
		{
			try
			{
				GroupUsr gu = new GroupUsr(usr.K, this.K);
				return gu != null && gu.IsMember;
			}
			catch
			{
				return false;
			}
		}

		public bool IsMember(GroupUsr gu)
		{
			return gu!=null && gu.IsMember;
		}
		#endregion
		#region CanViewHomePage
		public bool CanViewHomePage(Usr u, GroupUsr gu)
		{
			return (!this.PrivateGroupPage || (u!=null && u.IsAdmin) || (gu!=null && (gu.IsMember || gu.Status.Equals(GroupUsr.StatusEnum.Exited) || gu.Status.Equals(GroupUsr.StatusEnum.Invite) || gu.Status.Equals(GroupUsr.StatusEnum.InviteRejected))));
		}
		#endregion
		
		#region AllowExit
		public static bool AllowExit(Usr u, Group g, GroupUsr gu)
		{
			if (u==null)
				return false;

			if (gu!=null && (gu.Moderator || gu.MemberAdmin || gu.NewsAdmin || gu.Owner))
				return false;

		//	if (gu!=null && (gu.Status.Equals(GroupUsr.StatusEnum.Member) || gu.Status.Equals(GroupUsr.StatusEnum.Request)))
		//		return true;
			//removed this Request bit to remove confusing "exit" button when the group membership has been requested.

			if (gu!=null && gu.Status.Equals(GroupUsr.StatusEnum.Member))
				return true;

			return false;
		}
		#endregion
		#region Exit
		public void Exit(Usr u)
		{	
			GroupUsr gu = this.GetGroupUsr(u);
			if (Group.AllowExit(u, this, gu))
			{
				gu.Status = GroupUsr.StatusEnum.Exited;
				gu.StatusChangeDateTime = DateTime.Now;
				gu.StatusChangeUsrK = u.K;
				gu.Update();

				this.UpdateTotalMembers();

				CommentAlert.Disable(u, this.K, Model.Entities.ObjectType.Group);

				Mailer m = new Mailer();
				m.UsrRecipient = u;
				m.Subject = "Group exited - " + this.FriendlyName;
				m.Body = "<p>You've exited the " + this.FriendlyName + " group. You will no longer receive news from this group.</p>";
				m.RedirectUrl = this.Url();
				m.Send();
			}
		}
		#endregion
		#region Join
		public void Join(Usr u, GroupUsr gu)
		{
			bool exists = true;
			if (gu==null)
			{
				exists = false;
				gu = new GroupUsr();
				gu.UsrK=u.K;
				gu.GroupK=this.K;
				gu.Owner=false;
				gu.Moderator=false;
				gu.NewsAdmin=false;
				gu.MemberAdmin=false;
			}
			else
			{
				if (gu.Status.Equals(GroupUsr.StatusEnum.Barred) || gu.Status.Equals(GroupUsr.StatusEnum.RequestRejected))
					throw new DsiUserFriendlyException("You can't join this group. You have been barred or denied.");
			}
			
			
			bool joined = false;
			bool requested = false;

			if (this.PrivateGroupPage)
			{
				if (exists && (gu.Status.Equals(GroupUsr.StatusEnum.Invite) || gu.Status.Equals(GroupUsr.StatusEnum.InviteRejected) || gu.Status.Equals(GroupUsr.StatusEnum.Exited)))
				{
					gu.Status=GroupUsr.StatusEnum.Member;
					gu.StatusChangeDateTime=DateTime.Now;
					gu.StatusChangeUsrK=u.K;
					gu.Update();
					joined = true;
				}
				else
					throw new DsiUserFriendlyException("You can't request membership of a private group. You must wait to be invited.");
			}
			else if (this.Restriction.Equals(Group.RestrictionEnum.None))
			{
				gu.Status=GroupUsr.StatusEnum.Member;
				gu.StatusChangeDateTime=DateTime.Now;
				gu.StatusChangeUsrK=u.K;
				gu.Update();
				joined = true;
			}
			else if (this.Restriction.Equals(Group.RestrictionEnum.Member) || this.Restriction.Equals(Group.RestrictionEnum.Moderator))
			{
				if (exists && 
					(gu.Status.Equals(GroupUsr.StatusEnum.Invite) || gu.Status.Equals(GroupUsr.StatusEnum.InviteRejected) || gu.Status.Equals(GroupUsr.StatusEnum.Exited)))
				{
					gu.Status=GroupUsr.StatusEnum.Member;
					gu.StatusChangeDateTime=DateTime.Now;
					gu.StatusChangeUsrK=u.K;
					gu.Update();
					joined = true;
				}
				else if (
					!exists || 
					exists && (gu.Status.Equals(GroupUsr.StatusEnum.Recommend) || gu.Status.Equals(GroupUsr.StatusEnum.RecommendRejected)))
				{
					gu.Status=GroupUsr.StatusEnum.Request;
					gu.StatusChangeDateTime=DateTime.Now;
					gu.StatusChangeUsrK=u.K;
					gu.Update();
					requested = true;
				}
				else
					throw new DsiUserFriendlyException("You can't join this group");
			}
			else if (this.Restriction.Equals(Group.RestrictionEnum.Custom))
				throw new DsiUserFriendlyException("You can't join this group - it's membership restriction is set to Custom.");

			if (joined)
			{
				this.UpdateTotalMembers();

				SendJoinedEmail(u);

				if (this.K == Vars.CompetitionGroupK)
				{
					Log.Increment(Log.Items.CaptionGroupJoin);
				}

				Query qAdmin = new Query();
				qAdmin.QueryCondition=new And(this.MemberAdminQ, new Q(GroupUsr.Columns.MemberAdminNewUserEmails,true));
				qAdmin.TableElement=Group.UsrMemberJoin;
				qAdmin.Columns=Usr.EmailColumns;
				UsrSet usAdmins = new UsrSet(qAdmin);
				foreach (Usr admin in usAdmins)
				{
					Mailer mAdmin = new Mailer();
					mAdmin.UsrRecipient=admin;
					mAdmin.Subject="New group member - "+u.NickName+" has joined "+this.FriendlyName;
					mAdmin.Body="<p>" + u.LinkEmail() + " has joined the "+
						this.FriendlyName+" group.</p>";
					mAdmin.Body += "<p><b>Did you know you can turn these emails off?</b> Check out your <a href=\"[LOGIN]\">group membership moderator options</a>.</p>";
					mAdmin.RedirectUrl=this.UrlApp("admin","mode","membership");
					mAdmin.Send();
				}

				if (!this.PrivateGroupPage && u.FacebookConnected && u.FacebookStoryJoinGroup)
				{
					try
					{
						FacebookPost.CreateJoinGroup(u, this);
					}
					catch { }
				}

			}
			else if (requested)
			{
				this.UpdateTotalMembers();

				string alertScope="when news is posted";
				if (this.EmailOnAllThreads)
					alertScope="each time anyone posts a new topic";

				Mailer m = new Mailer();
				m.UsrRecipient=u;
				m.Subject="Group membership requested - "+this.FriendlyName;
				m.Body="<p>You've requested to join the "+this.FriendlyName+" group. The group moderator "+
					"has been sent an email and will deal with your application shortly.</p>"+
					"<p>If you're accepted as a member, you will receive email alerts "+
					alertScope+" in this group. If you ever want to exit the group, click the button "+
					"on the <a href=\"[LOGIN("+this.Url()+")]\">group homepage</a>.</p>";
				m.RedirectUrl=this.Url();
				m.Send();

				Query qAdmin = new Query();
				qAdmin.QueryCondition=this.MemberAdminQ;
				qAdmin.TableElement=Group.UsrMemberJoin;
				qAdmin.Columns=Usr.EmailColumns;
				UsrSet usAdmins = new UsrSet(qAdmin);
				foreach (Usr admin in usAdmins)
				{
					Mailer mAdmin = new Mailer();
					mAdmin.UsrRecipient=admin;
					mAdmin.Subject="Group membership requested - "+u.NickName+" would like to join "+this.FriendlyName;
					mAdmin.Body="<p>" + u.LinkEmail() + " would like to join the "+
						this.FriendlyName+" group. Please deal with this request as soon as possible. You can accept or deny the "+
						"request on the <a href=\"[LOGIN]\">group options page</a>.</p>";
					mAdmin.RedirectUrl=this.UrlApp("admin","mode","membership");
					mAdmin.Send();
				}
			}
		}
		#endregion
		#region SendJoinedEmail
		void SendJoinedEmail(Usr u)
		{
			string alertScope="when news is posted";
			if (this.EmailOnAllThreads)
				alertScope="each time anyone posts a new topic";

			Mailer m = new Mailer();
			m.UsrRecipient=u;
			m.Subject="Group joined - "+this.FriendlyName;
			m.Body="<p>You've joined the "+this.FriendlyName+" group. You will receive email alerts "+
				alertScope+" in this group. If you ever want to exit the group, click the button "+
				"on the <a href=\"[LOGIN("+this.Url()+")]\">group homepage</a>.</p>";
			m.RedirectUrl=this.Url();
			m.Send();
		}
		#endregion
		#region InvitePrivate
		private void InvitePrivate(Usr InvitedUsr, GroupUsr InvitedGroupUsr, Usr InvitingUsr, GroupUsr InvitingGroupUsr, 
			string InviteMessage)
		{
			string inviteMessageStripped = Cambro.Web.Helpers.Strip(InviteMessage,true,true,false,true);
			if (InvitedGroupUsr==null)
			{
				InvitedGroupUsr = new GroupUsr();
				InvitedGroupUsr.UsrK = InvitedUsr.K;
				InvitedGroupUsr.GroupK = this.K;
			}
			InvitedGroupUsr.Status = GroupUsr.StatusEnum.Invite;
			InvitedGroupUsr.StatusChangeDateTime = DateTime.Now;
			InvitedGroupUsr.StatusChangeUsrK = InvitingUsr.K;
			if (InvitedGroupUsr.InviteUsrK==0)
			{
				InvitedGroupUsr.InviteUsrK = InvitingUsr.K;
				InvitedGroupUsr.InviteMessage = inviteMessageStripped;
			}
			else if (InvitedGroupUsr.InviteUsrK != InvitingUsr.K)
			{
				//already had an inviting usr
				InvitingUsr = InvitedGroupUsr.InviteUsr;
				InvitingGroupUsr = GetGroupUsr(InvitingUsr);
				InviteMessage = InvitedGroupUsr.InviteMessage;
				inviteMessageStripped = Cambro.Web.Helpers.Strip(InviteMessage, true, true, false, true);
			}
			
			

			if (InvitedUsr.AddedByGroupK!=this.K)
			{
				string messageString = "";
				if (InviteMessage.Length>0)
					messageString = "</p><p>"+InvitedGroupUsr.InviteUsr.LinkEmail()+" left you this messsage:</p><p><b>"+inviteMessageStripped+"</b></p><p>";

				Mailer sm = new Mailer();
				sm.UsrRecipient = InvitedUsr;

				sm.RedirectUrl = this.Url();

				sm.Subject = InvitingUsr.NickName + @" has invited you to " + (InvitingGroupUsr!=null && InvitingGroupUsr.Moderator ? InvitingUsr.HisString(false) : "a") + @" group: " + this.FriendlyName;
				string pic = "<p>";
				string picEnd = "</p>";
				if (InvitingUsr.HasPic)
				{
					pic = @"<table cellspacing=""0"" cellpadding=""0"" border=""0"" style=""margin:10px 5px 5px 1px;""><tr><td valign=""top"" style=""padding:0px 10px 0px 0px;"">";
					pic += "<a href=\"[LOGIN(" + InvitingUsr.Url() + ")]\"><img src=\"" + InvitingUsr.PicPath + "\" class=\"BorderBlack All\" width=\"100\" height=\"100\" vspace=\"3\" border=\"0\"></a></td><td valign=\"top\">";
					picEnd = "</td></tr></table>";
				}
				string members = "";
				if (this.TotalMembers>5)
				{
					Query q = new Query();
					q.TableElement = Usr.GroupJoin;
					q.QueryCondition = new And(new Q(Group.Columns.K, this.K), new Q(Usr.Columns.Pic,QueryOperator.NotEqualTo,Guid.Empty));
					q.TopRecords=5;
					q.OrderBy=new OrderBy(OrderBy.OrderDirection.Random);
					q.Columns=Usr.LinkColumns;
					UsrSet us = new UsrSet(q);
					if (us.Count==5)
					{
						members = @"<p><b>"+this.FriendlyName+@"</b> has "+this.TotalMembers.ToString("#,##0")+@" members. Here's a few of them:</p>";
						members += @"<table cellspacing=""4"" cellpadding=""4"" border=""0"" width=""100%""><tr>";
						foreach (Usr uPic in us)
						{
							members += "<td width=\"20%\" valign=\"top\"><center><a href=\"[LOGIN(" + uPic.Url() + ")]\"><img src=\"" + uPic.PicPath + "\" width=\"75\" height=\"75\" style=\"margin:0px 0px 5px 0px;\" class=\"BorderBlack All\"><br>" + Cambro.Misc.Utility.Snip(uPic.NickName, 12) + "</a></center></td>";
						}
						members += @"</tr></table>";
					}
				}
				string inviteMessage = "";
				if (Cambro.Web.Helpers.Strip(InviteMessage,true,true,true,true).Length==0)
					inviteMessage = "Hi!";
				else
					inviteMessage = Cambro.Web.Helpers.Strip(InviteMessage,true,true,false,true).Replace("\n","<br>");


				sm.Body=@"
"+pic+@"
<i style=""font-size:18px;""><b>""</b>"+inviteMessage+@"<b>""</b></i>
"+picEnd+@"
<p>" + InvitingUsr.LinkEmail() + @" has invited you to " + (InvitingGroupUsr != null && InvitingGroupUsr.Moderator ? InvitingUsr.HisString(false) : "a") + @" group!
You can use this to keep in contact with your friends. Here's a quick 
description of <b>" +this.FriendlyName+@"</b>:</p>
<p>
<i>"+this.Description+@"</i>
</p>
"+members+ @"
<p align=""center"" style=""margin:10px 0px 8px 0px;"">
<a href=""[LOGIN("+this.UrlApp("join")+@")]"" style=""font-size:18px;font-weight:bold;"">Join the group</a> | <a href=""[LOGIN]"" style=""font-size:18px;font-weight:bold;"">decline the invite</a>
</p>
";
				sm.Send();


			}
			InvitedGroupUsr.Update();
		}
		#endregion
		#region RecommendPrivate
		private void RecommendPrivate(Usr InvitedUsr, GroupUsr InvitedGroupUsr, Usr InvitingUsr, GroupUsr InvitingGroupUsr, 
			string InviteMessage)
		{
			if (InvitedGroupUsr==null)
			{
				InvitedGroupUsr = new GroupUsr();
				InvitedGroupUsr.UsrK = InvitedUsr.K;
				InvitedGroupUsr.GroupK = this.K;
			}
			InvitedGroupUsr.Status = GroupUsr.StatusEnum.Recommend;
			InvitedGroupUsr.StatusChangeDateTime = DateTime.Now;
			InvitedGroupUsr.StatusChangeUsrK = InvitingUsr.K;
			if (InvitedGroupUsr.InviteUsrK==0)
			{
				InvitedGroupUsr.InviteUsrK = InvitingUsr.K;
				InvitedGroupUsr.InviteMessage = InviteMessage;
			}
			Query qAdmin = new Query();
			qAdmin.QueryCondition=this.MemberAdminQ;
			qAdmin.TableElement=Group.UsrMemberJoin;
			qAdmin.Columns=Usr.EmailColumns;
			UsrSet usAdmins = new UsrSet(qAdmin);
			foreach (Usr admin in usAdmins)
			{
				Mailer mAdmin = new Mailer();
				mAdmin.UsrRecipient=admin;
				mAdmin.Subject="Group membership recommended - "+InvitedUsr.NickName+" has been recommended "+
					"by "+InvitingUsr.NickName+" as a member of "+this.FriendlyName;
				mAdmin.Body="<p>"+InvitedUsr.LinkEmail()+" has been recommended "+
					"by "+InvitingUsr.LinkEmail()+" as a member of the "+this.FriendlyName+
					" group. Please deal with this request as soon as possible. You can accept or deny the request "+
					"on the <a href=\"[LOGIN]\">group options page</a>.</p>";
				mAdmin.RedirectUrl=this.UrlApp("admin","mode","membership");
				mAdmin.Send();
			}
			InvitedGroupUsr.Update();
		}
		#endregion
		#region JoinPrivate
		private void JoinPrivate(Usr InvitedUsr, GroupUsr InvitedGroupUsr, Usr InvitingUsr, GroupUsr InvitingGroupUsr)
		{
			InvitedGroupUsr.Status = GroupUsr.StatusEnum.Member;
			InvitedGroupUsr.StatusChangeDateTime = DateTime.Now;
			InvitedGroupUsr.StatusChangeUsrK=InvitingUsr.K;
			InvitedGroupUsr.Update();
			this.UpdateTotalMembers();
			SendJoinedEmail(InvitedUsr);
		}
		#endregion
		#region Invite
		public Return Invite(Usr TargetUsr, GroupUsr TargetGroupUsr, Usr PerformingUsr, GroupUsr PerformingGroupUsr, 
			string InviteMessage, bool InviteByEmail)
		{
			Return r = new Return();

			string targetUsr = "";
			if (InviteByEmail && TargetUsr.NickName.Length==0)
				targetUsr = TargetUsr.Email;
			else
				targetUsr = TargetUsr.Link();

			if (this.Restriction.Equals(Group.RestrictionEnum.Custom))
			{
				r.Success=false;
				r.MessageHtml="The "+this.FriendlyName+" group is a special group - the membership "+
					"is automatically controlled. You can't invite people to this group.";
				return r;
			}

			if (!PerformingUsr.IsAdmin && (PerformingGroupUsr==null || !PerformingGroupUsr.IsMember))
			{
				r.Success=false;
				r.MessageHtml="You're not a member of the "+this.FriendlyName+" group!";
				return r;
			}

			if (TargetGroupUsr==null)
			{
				if (this.Restriction.Equals(Group.RestrictionEnum.Moderator))
				{
					//nothing now
					//if the current user is a moderator, we add an invite request. 
					//If the current user isn't a mod, we add a recommend.
					if (PerformingUsr.CanGroupMemberAdmin(PerformingGroupUsr))
					{
						InvitePrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr, InviteMessage);
						r.Success=true;
						r.MessageHtml="You have invited "+targetUsr+" to the "+
							this.FriendlyName+" group. They will be sent a message shortly.";
					}
					else
					{
						RecommendPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr, InviteMessage);
						r.Success=true;
						r.MessageHtml="Since "+this.FriendlyName+" is a restricted group, only "+
							"moderators may send invites. "+targetUsr+" has been recommended "+
							"as a possible member. A moderator will decide whether to send the invite.";
					}
				}
				else
				{
					InvitePrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr, InviteMessage);
					r.Success=true;
					r.MessageHtml="You have invited "+targetUsr+" to the "+this.FriendlyName+" group. "+
						"They will be sent a message shortly.";
				}
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Member))
			{
				// This person is a member of the group
				r.Success=false;
				r.MessageHtml=targetUsr+" is already a member of the "+this.FriendlyName+" group.";
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Request))
			{
				// This person has requested membership
				// if the current user is a moderator, we must enable the request. 
				// If the current user isn't a mod, we must show an error.
				if (this.Restriction.Equals(Group.RestrictionEnum.Moderator))
				{
					if (PerformingUsr.CanGroupMemberAdmin(PerformingGroupUsr))
					{
						JoinPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
						r.Success=true;
						r.MessageHtml=targetUsr+" had already requested to join the "+
							this.FriendlyName+" group, so we've instantly made them a member.";
					}
					else
					{
						r.Success=false;
						r.MessageHtml=targetUsr+" has already requested to be a member "+
							"of this group. The group moderators will deal with the request and either accept or "+
							"deny the request.";
					}
				}
				else
				{
					JoinPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
					r.Success=true;
					r.MessageHtml=targetUsr+" had already requested to join the "+
						this.FriendlyName+" group, so we've instantly made them a member.";
				}
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.RequestRejected))
			{
				// A previous membership request was denied
				// if the current user is a moderator, we must enable them straight away. 
				// If the current user isn't a mod, we must show an error.
				if (PerformingUsr.CanGroupMemberAdmin(PerformingGroupUsr))
				{
					JoinPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
					r.Success=true;
					r.MessageHtml=targetUsr+" had already requested to join the "+
						this.FriendlyName+", so we've instantly made them a member.";
				}
				else
				{
					r.Success=false;
					r.MessageHtml=targetUsr+" has already requested to be a member, "+
						"but their request was denied. You can't invite this person to this group. Contact "+
						"a group moderator if you think this person should be a member.";
				}
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Invite))
			{
				// The person has been officially invited to the group - if they accept they will become a 
				// member instantly
				r.Success=false;
				r.MessageHtml=targetUsr+" has already been invited to the "+
					this.FriendlyName+" group. You can't send another invite.";
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Exited))
			{
				// The person exited the group themselves
				r.Success=false;
				r.MessageHtml=targetUsr+" was previously a member of the "+
					this.FriendlyName+" group, but they exited it. You can't invite them back.";
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Barred))
			{
				// The person has been barred by a moderator
				if (PerformingUsr.CanGroupMemberAdmin(PerformingGroupUsr))
				{
					r.Success = false;
					r.MessageHtml = targetUsr+" has been barred from the "+
						this.FriendlyName+" group. You must first un-bar them on the "+
						"<a href=\""+this.UrlApp("admin","mode","membership")+"\">group options page</a>.";
				}
				else
				{
					r.Success = false;
					r.MessageHtml = targetUsr+" has been barred from the "+
						this.FriendlyName+" group by a moderator. You can't send an invite.";
				}
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Recommend))
			{
				// A member has recommended this person as a group member. The membership admins will either 
				// change the status to Invited, or change it to RecommendRejected. This status level is 
				// only used in groups with restricted membership
				if (this.Restriction.Equals(Group.RestrictionEnum.Moderator))
				{
					if (PerformingUsr.CanGroupMemberAdmin(PerformingGroupUsr))
					{
						InvitePrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr, InviteMessage);
						r.Success = true;
						r.MessageHtml = "You have invited "+targetUsr+" to the "+
							this.FriendlyName+" group. They will be sent a message shortly.";
					}
					else
					{
						r.Success = false;
						r.MessageHtml = "Since "+this.FriendlyName+" is a restricted group, only "+
							"moderators may send invites. "+targetUsr+" has been recommended as a "+
							"possible member. A moderator will decide whether to send the invite.";
					}
				}
				else
				{
					InvitePrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr, InviteMessage);
					r.Success = true;
					r.MessageHtml = "You have invited "+targetUsr+" to the "+
						this.FriendlyName+" group. They will be sent a message shortly.";
				}
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.RecommendRejected))
			{
				// A member has recommended this person as a member of the group, but they were rejected by a 
				// membership moderator. This status level is only used in groups with restricted membership
				if (PerformingUsr.CanGroupMemberAdmin(PerformingGroupUsr))
				{
					InvitePrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr, InviteMessage);
					r.Success = true;
					r.MessageHtml = "You have invited "+targetUsr+" to the "+
						this.FriendlyName+" group. They will be sent a message shortly.";
				}
				else
				{
					r.Success = false;
					r.MessageHtml = targetUsr+" has previously been rejected by a "+
						"moderator, so you can't send an invite. Contact a group moderator if you think "+
						"this person should be a member.";
				}
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.InviteRejected))
			{
				// This person was invited to the group, but decided they didn't want to be a member
				r.Success = false;
				r.MessageHtml = targetUsr+" has already been invited to the "+
					this.FriendlyName+" group, but they rejected the invitation. You can't send another invite.";
			}
			return r;
			
		}
		#endregion
		#region Accept
		public void Accept(Usr TargetUsr, GroupUsr TargetGroupUsr, Usr PerformingUsr, GroupUsr PerformingGroupUsr)
		{
			if (this.Restriction.Equals(Group.RestrictionEnum.Custom))
				throw new DsiUserFriendlyException("Can't accept into a custom group!");

			if (!PerformingUsr.IsAdmin && (PerformingGroupUsr==null || !PerformingGroupUsr.IsMember))
				throw new DsiUserFriendlyException("You must be a member of this group to accept!");


			if (TargetGroupUsr==null)
			{
				// Nothing
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Request))
			{
				// This person has requested membership
				// if the current user is a moderator, we must enable the request.
				if (this.Restriction.Equals(Group.RestrictionEnum.Moderator))
				{
					if (PerformingUsr.IsAdmin || (PerformingGroupUsr!=null && PerformingGroupUsr.MemberAdmin))
					{
						JoinPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
					}
				}
				else
				{
					JoinPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
				}
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.RequestRejected))
			{
				// A previous membership request was denied
				// if the current user is a moderator, we must enable them straight away. 
				if (PerformingUsr.IsAdmin || (PerformingGroupUsr!=null && PerformingGroupUsr.MemberAdmin))
				{
					JoinPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
				}
			}
		}
		#endregion
		#region RejectPrivate
		private void RejectPrivate(Usr RejectedUsr, GroupUsr RejectedGroupUsr, Usr PerformingUsr, GroupUsr PerformingGroupUsr)
		{
			if (RejectedUsr==null)
			{
				throw new Exception("InvitedGroupUsr is null!");
			}

			if (RejectedGroupUsr.Status.Equals(GroupUsr.StatusEnum.Request))
				RejectedGroupUsr.Status = GroupUsr.StatusEnum.RequestRejected;
			else if (RejectedGroupUsr.Status.Equals(GroupUsr.StatusEnum.Recommend))
				RejectedGroupUsr.Status = GroupUsr.StatusEnum.RecommendRejected;
			else
				throw new Exception("Incorrect status to reject!");
			
			RejectedGroupUsr.StatusChangeDateTime = DateTime.Now;
			RejectedGroupUsr.StatusChangeUsrK=PerformingUsr.K;

			if (RejectedGroupUsr.Status.Equals(GroupUsr.StatusEnum.RequestRejected))
			{
				Mailer m = new Mailer();
				m.UsrRecipient = RejectedUsr;
				m.Subject = "Your request to join the "+this.FriendlyName+" group has been rejected.";
				m.Body = "<p>Your request to join the "+this.FriendlyName+" group has been rejected. You can contact a group moderator for more information.</p>";
				m.RedirectUrl = this.Url();
				m.Send();
			}
			else if (RejectedGroupUsr.Status.Equals(GroupUsr.StatusEnum.RecommendRejected))
			{
				Mailer m = new Mailer();
				m.UsrRecipient = RejectedGroupUsr.InviteUsr;
				m.Subject = "Your recommendation for "+RejectedUsr.NickName+" to join the "+this.FriendlyName+" group has been rejected.";
				m.Body = "<p>Your recommendation for "+RejectedUsr.LinkEmail()+" to join the "+this.FriendlyName+" group has been rejected. You can contact a group moderator for more information.</p>";
				m.RedirectUrl = this.Url();
				m.Send();
			}
			RejectedGroupUsr.Update();
		}
		#endregion
		#region Reject
		public Return Reject(Usr TargetUsr, GroupUsr TargetGroupUsr, Usr PerformingUsr, GroupUsr PerformingGroupUsr)
		{
			Return r = new Return();

			if (this.Restriction.Equals(Group.RestrictionEnum.Custom))
			{
				r.Success=false;
				r.MessageHtml="Can't accept into a custom group!";
				return r;
			}

			if (!PerformingUsr.IsAdmin && (PerformingGroupUsr==null || !PerformingGroupUsr.IsMember))
			{
				r.Success=false;
				r.MessageHtml="You must be a member of this group to reject someone!";
				return r;
			}

			if (TargetGroupUsr==null)
			{
				r.Success=false;
				r.MessageHtml="Can't reject - user hasn't requested membership!";
				return r;
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Request))
			{
				// This person has requested membership
				// if the current user is a moderator, we must enable the request.
				if (this.Restriction.Equals(Group.RestrictionEnum.Moderator))
				{
					if (PerformingUsr.CanGroupMemberAdmin(PerformingGroupUsr))
					{
						RejectPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
						r.Success=true;
						return r;
					}
					else
					{
						r.Success=false;
						r.MessageHtml="You don't have permission to reject.";
						return r;
					}
				}
				else
				{
					RejectPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
					r.Success=true;
					return r;
				}
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Recommend))
			{
				// A previous membership request was denied
				// if the current user is a moderator, we must enable them straight away. 
				if (PerformingUsr.CanGroupMemberAdmin(PerformingGroupUsr))
				{
					RejectPrivate(TargetUsr, TargetGroupUsr, PerformingUsr, PerformingGroupUsr);
					r.Success=true;
					return r;
				}
				else
				{
					r.Success=false;
					r.MessageHtml="You don't have permission to reject.";
					return r;
				}
			}
			else
			{
				r.Success=false;
				r.MessageHtml="Can't reject - wrong status.";
				return r;
			}
		}
		#endregion
		#region Bar
		public void Bar(Usr TargetUsr, GroupUsr TargetGroupUsr, Usr PerformingUsr, GroupUsr PerformingGroupUsr)
		{
			if (this.Restriction.Equals(Group.RestrictionEnum.Custom))
				throw new DsiUserFriendlyException("Can't bar from a custom group!");

			if (!PerformingUsr.IsAdmin && (PerformingGroupUsr==null || !PerformingGroupUsr.MemberAdmin))
				throw new DsiUserFriendlyException("You must be a membership admin of this group to bar someone!");

			if (TargetGroupUsr!=null && TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Barred))
			{
			}
			else
			{
				if (TargetGroupUsr==null)
				{
					TargetGroupUsr = new GroupUsr();
					TargetGroupUsr.UsrK = TargetUsr.K;
					TargetGroupUsr.GroupK = this.K;
				}
				else if (TargetGroupUsr.Moderator)
					throw new DsiUserFriendlyException("You can't bar this person - they are a moderator. First remove their moderator status before banning them.");

				TargetGroupUsr.Status = GroupUsr.StatusEnum.Barred;
				TargetGroupUsr.StatusChangeDateTime = DateTime.Now;
				TargetGroupUsr.StatusChangeUsrK = PerformingUsr.K;

				Query qAdmin = new Query();
				qAdmin.QueryCondition=this.MemberAdminQ;
				qAdmin.TableElement=Group.UsrMemberJoin;
				qAdmin.Columns=Usr.EmailColumns;
				UsrSet usAdmins = new UsrSet(qAdmin);
				foreach (Usr admin in usAdmins)
				{
					Mailer mAdmin = new Mailer();
					mAdmin.UsrRecipient=admin;
					mAdmin.Subject="Member barred - "+TargetUsr.NickName+" has been barred from the "+
						this.FriendlyName+ " group by "+PerformingUsr.NickName;
					mAdmin.Body="<p>"+TargetUsr.LinkEmail()+" has been barred from the "+
						this.FriendlyName+ " group by "+PerformingUsr.LinkEmail()+"</p>";
					mAdmin.RedirectUrl=this.UrlApp("admin","mode","membership");
					mAdmin.Send();
				}

				Mailer m = new Mailer();
				m.UsrRecipient = TargetUsr;
				m.Subject = "You have been barred from the the "+this.FriendlyName+" group.";
				m.Body = "<p>You have been barred from the the "+this.FriendlyName+" group. You can contact a group moderator for more information.</p>";
				m.RedirectUrl = this.Url();
				m.Send();

				TargetGroupUsr.Update();

				CommentAlert.Disable(TargetUsr, this.K, Model.Entities.ObjectType.Group);

			}

		}
		#endregion
		#region UnBar
		public void UnBar(Usr TargetUsr, GroupUsr TargetGroupUsr, Usr PerformingUsr, GroupUsr PerformingGroupUsr)
		{
			if (this.Restriction.Equals(Group.RestrictionEnum.Custom))
				throw new DsiUserFriendlyException("Can't unbar from a custom group!");

			if (!PerformingUsr.IsAdmin && (PerformingGroupUsr==null || !PerformingGroupUsr.MemberAdmin))
				throw new DsiUserFriendlyException("You must be a membership admin of this group to unbar someone!");

			if (TargetGroupUsr!=null && TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Barred))
			{
				TargetGroupUsr.Status = GroupUsr.StatusEnum.Invite;
				TargetGroupUsr.StatusChangeDateTime = DateTime.Now;
				TargetGroupUsr.StatusChangeUsrK = PerformingUsr.K;

				Query qAdmin = new Query();
				qAdmin.QueryCondition=this.MemberAdminQ;
				qAdmin.TableElement=Group.UsrMemberJoin;
				qAdmin.Columns=Usr.EmailColumns;
				UsrSet usAdmins = new UsrSet(qAdmin);
				foreach (Usr admin in usAdmins)
				{
					Mailer mAdmin = new Mailer();
					mAdmin.UsrRecipient=admin;
					mAdmin.Subject="Member un-barred - "+TargetUsr.NickName+" has been un-barred from the "+
						this.FriendlyName+ " group by "+PerformingUsr.NickName;
					mAdmin.Body="<p>"+TargetUsr.LinkEmail()+" has been un-barred from the "+
						this.FriendlyName+ " group by "+PerformingUsr.LinkEmail()+"</p>";
					mAdmin.RedirectUrl=this.UrlApp("admin","mode","membership");
					mAdmin.Send();
				}

				Mailer m = new Mailer();
				m.UsrRecipient = TargetUsr;
				m.Subject = "You have been un-barred from the the "+this.FriendlyName+" group.";
				m.Body = "<p>You have been un-barred from the the "+this.FriendlyName+" group. You may join the group by clicking the button on the group homepage. You can contact a group moderator for more information.</p>";
				m.RedirectUrl = this.Url();
				m.Send();

				TargetGroupUsr.Update();

			}

		}
		#endregion
		#region InviteReject
		public Return InviteReject(Usr TargetUsr, GroupUsr TargetGroupUsr)
		{
			Return r = new Return();

			if (this.Restriction.Equals(Group.RestrictionEnum.Custom))
			{
				r.Success=false;
				r.MessageHtml="The "+this.FriendlyName+" group is a special group - the membership "+
					"is automatically controlled. You can't decline an invite to this group.";
				return r;
			}

			if (TargetGroupUsr==null)
			{
				r.Success=false;
				r.MessageHtml="You haven't been invited to the "+this.FriendlyName+" group!";
				return r;
			}
			else if (TargetGroupUsr.Status.Equals(GroupUsr.StatusEnum.Invite))
			{
				TargetGroupUsr.Status = GroupUsr.StatusEnum.InviteRejected;
				TargetGroupUsr.StatusChangeDateTime = DateTime.Now;
				TargetGroupUsr.StatusChangeUsrK = TargetUsr.K;

				GroupUsr gu = this.GetGroupUsr(TargetGroupUsr.InviteUsr);
				if (gu.MemberAdminNewUserEmails)
				{
					Mailer m = new Mailer();
					m.UsrRecipient = TargetGroupUsr.InviteUsr;
					m.Subject = "Your invitation for " + TargetUsr.NickName + " to join the " + this.FriendlyName + " group has been rejected.";
					m.Body = "<p>Your invitation for " + TargetUsr.LinkEmail() + " to join the " + this.FriendlyName + " group has been rejected. " + TargetUsr.LinkEmail() + " did not want to join the group.</p>";
					m.RedirectUrl = TargetUsr.Url();
					m.Send();
				}

				TargetGroupUsr.Update();

				r.Success=true;
				return r;
			}
			else
			{
				r.Success=false;
				r.MessageHtml="You haven't been invited to the "+this.FriendlyName+" group!";
				return r;
			}			
		}
		#endregion

		#region GetGroupUsr
		public GroupUsr GetGroupUsr(Usr u)
		{
			if (u==null)
				return null;
			else
			{
				try
				{
					return new GroupUsr(u.K, this.K);
				}
				catch
				{
					return null;
				}
			}
		}
		public GroupUsr GetGroupUsr(int usrK)
		{
			if (usrK == 0)
				return null;
			else
			{
				try
				{
					return new GroupUsr(usrK, this.K);
				}
				catch
				{
					return null;
				}
			}
		}
		#endregion

		#region ChangeUsr
		public GroupUsr ChangeUsr(bool Remove, int UsrK, bool Moderator, bool NewsAdmin, bool MemberAdmin, bool Owner, GroupUsr.StatusEnum Status, DateTime DateTimeAdded, bool UpdateTotalMembers)
		{
			bool found = false;
			GroupUsr gu = new GroupUsr();
			try
			{
				gu = new GroupUsr(UsrK, this.K);
				found=true;
			}
			catch{}

			if (found)
			{
				if (Remove)
				{
					gu.Delete();
					if (UpdateTotalMembers)
						this.UpdateTotalMembers();
				}
				else
				{
					gu.Moderator=Moderator || NewsAdmin || MemberAdmin || Owner;
					gu.NewsAdmin=NewsAdmin || Owner;
					if (gu.MemberAdmin != (MemberAdmin || Owner))
					{
						gu.MemberAdmin = MemberAdmin || Owner;
						gu.MemberAdminNewUserEmails = MemberAdmin || Owner;
					}
					gu.Owner=Owner;
					if (!gu.Status.Equals(Status))
					{
						gu.Status=Status;
						gu.StatusChangeDateTime=DateTimeAdded;
						gu.StatusChangeUsrK=UsrK;
					}

					gu.Update();
					gu.Usr.UpdateIsGroupModerator();
					if (UpdateTotalMembers)
						this.UpdateTotalMembers();
				}
			}
			else
			{
				if (Remove)
				{
					gu = null;
				}
				else
				{
					gu.UsrK=UsrK;
					gu.GroupK=this.K;
					if (!gu.Status.Equals(Status))
					{
						gu.Status=Status;
						gu.StatusChangeDateTime=DateTimeAdded;
						gu.StatusChangeUsrK=UsrK;
					}
					gu.Moderator=Moderator || NewsAdmin || MemberAdmin || Owner;
					gu.NewsAdmin=NewsAdmin || Owner;
					if (gu.MemberAdmin != (MemberAdmin || Owner))
					{
						gu.MemberAdmin = MemberAdmin || Owner;
						gu.MemberAdminNewUserEmails = MemberAdmin || Owner;
					}
					gu.Owner=Owner;

					gu.Update();
					gu.Usr.UpdateIsGroupModerator();
					if (UpdateTotalMembers)
						this.UpdateTotalMembers();
				}
			}
			return gu;
		}
		#endregion

		#region UpdateTotalMembers
		public void UpdateTotalMembers()
		{
			bool update=false;
			if (true)
			{
				Query q = new Query();
				q.QueryCondition=this.MemberQ;
				q.ReturnCountOnly=true;
				GroupUsrSet gus = new GroupUsrSet(q);
				if (this.TotalMembers!=gus.Count)
				{
					this.TotalMembers=gus.Count;
					update=true;
				}
			}
			if (true)
			{
				Query q = new Query();
				q.QueryCondition=this.ModeratorQ;
				q.ReturnCountOnly=true;
				GroupUsrSet gus = new GroupUsrSet(q);
				if (this.TotalModerators!=gus.Count)
				{
					this.TotalModerators=gus.Count;
					update=true;
				}
			}
			if (true)
			{
				Query q = new Query();
				q.QueryCondition=this.OwnerQ;
				q.ReturnCountOnly=true;
				GroupUsrSet gus = new GroupUsrSet(q);
				if (this.TotalOwners!=gus.Count)
				{
					this.TotalOwners=gus.Count;
					update=true;
				}
			}
			if (true)
			{
				Query q = new Query();
				q.QueryCondition=new And(this.MemberQ,new Q(GroupUsr.Columns.Favourite,true));
				q.ReturnCountOnly=true;
				GroupUsrSet gus = new GroupUsrSet(q);
				if (this.FavouriteCount!=gus.Count)
				{
					this.FavouriteCount=gus.Count;
					update=true;
				}
			}
			if (update)
				this.Update();
		}
		#endregion

		#region AddRelevant
		public void AddRelevant(IRelevanceHolder ContainerPage)
		{
			if (this.PlaceK>0)
			{
				ContainerPage.RelevantPlacesAdd(this.PlaceK);
			}
			if (this.MusicTypeK>0)
			{
				ContainerPage.RelevantMusicAdd(this.MusicTypeK);
			}
		}
		#endregion

		#region IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			if (objectType.Equals(Model.Entities.ObjectType.Group) && this.K == objectK)
				return true;

			if (objectType.Equals(Model.Entities.ObjectType.Brand) && this.BrandK == objectK) //This is to stop a circular reference
				return true;

			if (this.PlaceK > 0)
			{
				if (objectType.Equals(Model.Entities.ObjectType.Place) && this.PlaceK == objectK)
					return true;

				if (Place.CanBeConnectedToStatic(objectType) && this.Place.IsConnectedTo(objectType, objectK))
					return true;
			}
			else if (this.CountryK > 0)
			{
				if (objectType.Equals(Model.Entities.ObjectType.Country) && this.CountryK == objectK)
					return true;
			}
			
			return false;
		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			if (o.Equals(Model.Entities.ObjectType.Place))
				return true;

			if (Place.CanBeConnectedToStatic(o))
				return true;

			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Group.CanBeConnectedToStatic(o);
		}
		#endregion

		#region LongDescriptionHtmlRender
		public string LongDescriptionHtmlRender
		{
			get
			{
				if (this.LongDescriptionPlain)
					return "<div style=\"margin-bottom:13px;\">"+LongDescriptionHtml+"</div>";
				else
					return "<p>"+LongDescriptionHtml.Replace("\n","<br>")+"</p>";
			}
		}
		#endregion

		#region Events
		public bool HasEvents
		{
			get
			{
				if (!hasEventsDone)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition=new Q(GroupEvent.Columns.GroupK,this.K);
					q.TopRecords=1;
					GroupEventSet ges = new GroupEventSet(q);
					hasEvents = ges.Count>0;
					hasEventsDone = true;
				}
				return hasEvents;
			}
		}
		bool hasEvents=false;
		bool hasEventsDone=false;

		public EventSet NextEventSet
		{
			get
			{
				Query q = new Query();
				q.NoLock = true;
				q.TableElement=Event.GroupJoin;
				q.QueryCondition=new And(new Q(Group.Columns.K,this.K),Event.FutureEventsQueryCondition);
				q.OrderBy=new OrderBy(Event.Columns.DateTime,OrderBy.OrderDirection.Ascending);
				q.TopRecords=1;
				return new EventSet(q);
			}
		}
		public Event NextEvent
		{
			get
			{
				EventSet es = this.NextEventSet;
				if (es.Count==1)
					return es[0];
				else
					return null;
			}
		}
		public int EventsCount()
		{
			Query q = new Query();
			q.NoLock = true;
			q.TableElement=Event.GroupJoin;
			q.QueryCondition=new Q(Group.Columns.K,this.K);
			q.ReturnCountOnly=true;
			EventSet es = new EventSet(q);
			int i = es.Count;
			return i;
		}
		#endregion

		#region StatusEnum
		#endregion
		#region RestrictionEnum
		#endregion
		#region CustomRestrictionTypes
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			
			if (!this.Bob.DbRecordExists)
				return;

			//GroupUsrs
			Delete GroupUsrDelete = new Delete(
				TablesEnum.GroupUsr,
				new Q(GroupUsr.Columns.GroupK, this.K)
				);
			GroupUsrDelete.Run(transaction);

			//GroupUsrs
			Delete GroupPhotoDelete = new Delete(
				TablesEnum.GroupPhoto,
				new Q(GroupPhoto.Columns.GroupK, this.K)
				);
			GroupPhotoDelete.Run(transaction);

			//GroupEvents
			Delete GroupEventDelete = new Delete(
				TablesEnum.GroupEvent,
				new Q(GroupEvent.Columns.GroupK, this.K)
			);
			GroupEventDelete.Run(transaction);

			ThreadSet ts = new ThreadSet(new Query(new Q(Thread.Columns.GroupK,this.K)));
			foreach (Thread t in ts)
				t.DeleteAll(transaction);

			Guid oldPic = this.HasPic ? this.Pic : Guid.Empty;
			int oldPicMiscK = this.PicMisc != null ? this.PicMiscK : 0;

			this.Delete(transaction);

			if (oldPic != Guid.Empty)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

			if (oldPicMiscK > 0)
			{
				Misc m = new Misc(oldPicMiscK);
				m.DeleteAll(transaction);
			}
		}
		#endregion

		#region ThreadsQ
		public static Q ThreadsQ(int GroupK)
		{
			return new Q(Thread.Columns.GroupK,GroupK);
		}
		#endregion
		#region ThreadsQWithLinkedEvents
		public static Q ThreadsQWithLinkedEvents(Group g)
		{
			if (g.BrandK > 0)
				return new Q(Thread.Columns.GroupK, g.K);
			else
			{
				return new Or(
					new And(
						new Q(GroupEvent.Columns.GroupK,g.K),
						new Q(Thread.Columns.GroupK,g.K)),
					new And(
						new Or(new Q(GroupEvent.Columns.GroupK,0),new Q(GroupEvent.Columns.GroupK,QueryOperator.IsNull,null)),
						new Q(Thread.Columns.GroupK,g.K)),
					new And(
						new Q(GroupEvent.Columns.GroupK,g.K),
						new Or(new Q(Thread.Columns.GroupK,0),new Q(Thread.Columns.GroupK,QueryOperator.IsNull,null)))
				);
			}
		}
		#endregion
		#region MemberQ
		public Q MemberQ
		{
			get
			{
				return new And(
					new Q(GroupUsr.Columns.GroupK,this.K),
					new Q(GroupUsr.Columns.Status,GroupUsr.StatusEnum.Member)
				);
			}
		}
		#endregion
		#region ModeratorQ
		public Q ModeratorQ
		{
			get
			{
				return new And(
					new Q(GroupUsr.Columns.GroupK,this.K),
					new Q(GroupUsr.Columns.Status,GroupUsr.StatusEnum.Member),
					new Q(GroupUsr.Columns.Moderator,true)
				);
			}
		}
		#endregion
		#region NewsAdminQ
		public Q NewsAdminQ
		{
			get
			{
				return new And(
					new Q(GroupUsr.Columns.GroupK,this.K),
					new Q(GroupUsr.Columns.Status,GroupUsr.StatusEnum.Member),
					new Q(GroupUsr.Columns.NewsAdmin,true)
				);
			}
		}
		#endregion
		#region MemberAdminQ
		public Q MemberAdminQ
		{
			get
			{
				return new And(
					new Q(GroupUsr.Columns.GroupK,this.K),
					new Q(GroupUsr.Columns.Status,GroupUsr.StatusEnum.Member),
					new Q(GroupUsr.Columns.MemberAdmin,true)
				);
			}
		}
		#endregion
		#region OwnerQ
		public Q OwnerQ
		{
			get
			{
				return new And(
					new Q(GroupUsr.Columns.GroupK,this.K),
					new Q(GroupUsr.Columns.Status,GroupUsr.StatusEnum.Member),
					new Q(GroupUsr.Columns.Owner,true)
				);
			}
		}
		#endregion
		#region UsrMemberJoin
		public static Join UsrMemberJoin
		{
			get
			{
				return new JoinDouble(Group.Columns.K,GroupUsr.Columns.GroupK,GroupUsr.Columns.UsrK,Usr.Columns.K);
			}
		}
		#endregion

		#region Theme
		public Theme Theme
		{
			get
			{
				if (theme==null && ThemeK>0)
					theme = new Theme(ThemeK);
				return theme;
			}
			set
			{
				theme = value;
			}
		}
		private Theme theme;
		#endregion
		#region Country
		public Country Country
		{
			get
			{
				if (country==null && CountryK>0)
					country = new Country(CountryK);
				return country;
			}
			set
			{
				country = value;
			}
		}
		private Country country;
		#endregion
		#region Place
		public Place Place
		{
			get
			{
				if (place==null && PlaceK>0)
					place = new Place(PlaceK);
				return place;
			}
			set
			{
				place = value;
			}
		}
		private Place place;
		#endregion
		#region MusicType
		public MusicType MusicType
		{
			get
			{
				if (musicType==null && MusicTypeK>0)
					musicType = new MusicType(MusicTypeK);
				return musicType;
			}
			set
			{
				musicType = value;
			}
		}
		private MusicType musicType;
		#endregion
		#region Brand
		public Brand Brand
		{
			get
			{
				if (brand==null && BrandK>0)
					brand = new Brand(BrandK);
				return brand;
			}
			set
			{
				brand = value;
			}
		}
		private Brand brand;
		#endregion

		#region Url
		public void UpdateChildUrlFragments(bool Cascade)
		{
			Update uThreads = new Update();
			uThreads.Table=TablesEnum.Thread;
			uThreads.Changes.Add(new Assign(Thread.Columns.UrlFragment,UrlFilterPart));
			uThreads.Where=new And(
				new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Group),
				new Q(Thread.Columns.ParentObjectK,this.K));
			uThreads.Run();
		}
		public string UrlFragment
		{
			get
			{
				return "groups";
			}
		}
		public string UrlFilterPart
		{
			get
			{
				return UrlFragment+"/"+UrlName;
			}
		}
		public string Url(params string[] par)
		{
			if (UrlName.IndexOf('/')>-1)
				return UrlInfo.MakeUrl(UrlName, null, par);
			else
				return UrlInfo.MakeUrl(UrlFilterPart,null,par);
		}
		public string UrlApp(string Application, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,Application,par);
		}
		public string UrlJoin()
		{
			return Url("join");
		}
		#endregion

		#region Link
		public string LinkJoin
		{
			get
			{
				return Utilities.Link(UrlJoin(), FriendlyName);
			}
		}
		#endregion
		#region LinkEmail
		public string LinkEmail
		{
			get
			{
				return "<a href=\"[LOGIN("+Url()+")]\">"+FriendlyName+"</a>";
			}
		}
		#endregion

		#region CreateUniqueUrlName(bool update)
		public void CreateUniqueUrlName(bool update)
		{
			string urlName = UrlInfo.GetUrlName(this.Name);
			if (urlName.Length==0)
				urlName = "group-"+this.K.ToString();
			if (UrlInfo.IsReservedString(urlName))
				urlName = "group-"+urlName;

			GroupSet gs = null;
			int namePost = 0;
			string newName = urlName;
			while (gs==null || gs.Count>0)
			{
				if (namePost>0)
					newName = urlName+"-"+namePost.ToString();
				Query q = new Query();
				q.NoLock=true;
				q.ReturnCountOnly=true;
				q.QueryCondition=new And(
					new Q(Group.Columns.UrlName,newName),
					new Q(Group.Columns.K,QueryOperator.NotEqualTo,this.K)
				);
				gs = new GroupSet(q);
				namePost++;
			}
			
			if (!this.UrlName.Equals(newName))
			{
				this.UrlName = newName;
				if (update)
				{
					this.Update();
					Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Group, this.K, true);
					job.ExecuteAsynchronously();
				}
			}
		}
		#endregion

		#region IPic Members

		public bool HasPic
		{
			get
			{
				return !Pic.Equals(Guid.Empty);
			}
		}
		
		public string PicPath
		{
			get
			{
				if (HasPic)
					return Storage.Path(Pic);
				else
					return "/gfx/dsi-sign-100.png";
			}
		}

		public bool HasAnyPic
		{
			get
			{
				if (this.HasPic)
					return true;
				else if (this.BrandK>0 && this.Brand!=null)
					return this.Brand.HasPic;
				else
					return false;
			}
		}
				

		public string AnyPicPath
		{
			get
			{
				if (HasPic)
					return Storage.Path(Pic);
				else if (this.BrandK>0 && this.Brand!=null && this.Brand.HasPic)
					return this.Brand.PicPath;
				else
					return "/gfx/dsi-sign-100.png";
			}
		}

		#endregion

		#region PicMisc and PicPhoto
		#region PicMisc
		public Misc PicMisc
		{
			get
			{
				if (picMisc==null && PicMiscK>0)
					picMisc = new Misc(PicMiscK);
				return picMisc;
			}
			set
			{
				picMisc = value;
			}
		}
		private Misc picMisc;
		#endregion
		#region PicPhoto
		public Photo PicPhoto
		{
			get
			{
				if (picPhoto==null && PicPhotoK>0)
					picPhoto = new Photo(PicPhotoK);
				return picPhoto;
			}
			set
			{
				picPhoto = value;
			}
		}
		private Photo picPhoto;
		#endregion
		#endregion

		#region IName Members

		public string FriendlyName
		{
			get
			{
				if (this.BrandK>0)
					return Name+" regulars";
				else
					return Name;
			}
		}

		#endregion

		#region IBobType Members

		public string TypeName
		{
			get
			{
				return "Group";
			}
		}
		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Group;
			}
		}
		#endregion

		#region IDiscuss Members

		public string UrlDiscussion(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,"chat",par);
		}

//		public ThreadSet Threads
//		{
//			get
//			{
//				// TODO:  Add Group.Threads getter implementation
//				return null;
//			}
//			set
//			{
//				// TODO:  Add Group.Threads setter implementation
//			}
//		}

		public void UpdateTotalComments(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition = new Q(Thread.Columns.GroupK,this.K);
			q.ExtraSelectElements = ForumStats.ExtraSelectElements;
			q.Columns = new ColumnSet();
			ForumStats cs = new ForumStats(q);
			this.TotalComments=cs.TotalComments;
			this.AverageCommentDateTime=cs.AverageCommentDateTime;
			this.LastPost=cs.LastPost;

			this.Update(transaction);
			
			if (this.BrandK>0)
				this.Brand.UpdateTotalComments(transaction);

			if (this.PlaceK>0)
				this.Place.UpdateTotalComments(transaction);
			else if (this.CountryK>0)
				this.Country.UpdateTotalComments(transaction);

		}

		#endregion

		#region ICalendar
		public string UrlCalendarGeneric(string Application, int Year, int Month, int Day, int SkipDay, params string[] par)
		{
			DateTime month = new DateTime(Year,Month,1);
			string dayString = Day == 0 ? "" : ("/" + Day.ToString("00"));
			string url = UrlInfo.MakeUrl(UrlFilterPart + "/" + Year + "/" + month.ToString("MMM").ToLower() + dayString, Application, par);
			string skip = "";
			if (SkipDay>0)
				skip = "#Day"+new DateTime(Year, Month, SkipDay).ToString("yyyyMMdd");
			return url+skip;
		}
		public string UrlCalendarDay(bool Tickets, bool FreeGuestlist, int Year, int Month, int Day, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, Year, Month, Day, 0, par);
		}
		public string UrlCalendarDay(int Year, int Month, int Day, params string[] par)
		{
			return UrlCalendarGeneric(null, Year, Month, Day, 0, par);
		}

		public string UrlCalendarMonth(bool Tickets, bool FreeGuestlist, int Year, int Month, int DaySkip, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, Year, Month, 0, DaySkip, par);
		}
		public string UrlCalendarMonth(int Year, int Month, int DaySkip, params string[] par)
		{
			return UrlCalendarGeneric(null, Year, Month, 0, DaySkip, par);
		}

		public string UrlCalendar(bool Tickets, bool FreeGuestlist, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, DateTime.Today.Year, DateTime.Today.Month, 0, DateTime.Today.Day, par);
		}
		public string UrlCalendar(params string[] par)
		{
			return UrlCalendarGeneric(null, DateTime.Today.Year, DateTime.Today.Month, 0, DateTime.Today.Day, par);
		}
		public Event HasSingleEvent(int Year, int Month, int Day)
		{
			Query q = new Query();
			q.NoLock=true;
			q.Columns=new ColumnSet(Event.Columns.K,Event.Columns.VenueK,Event.Columns.DateTime);
			Q DateTimeQ = null;
			if (Year>0 && Month>0 && Day>0)
				DateTimeQ = new Q(Event.Columns.DateTime,new DateTime(Year,Month,Day));
			else if (Year>0 && Month>0)
				DateTimeQ = new And(
					new Q(Event.Columns.DateTime,QueryOperator.GreaterThanOrEqualTo,new DateTime(Year,Month,1)),
					new Q(Event.Columns.DateTime,QueryOperator.LessThan,new DateTime(Year,Month,1).AddMonths(1)));
			else if (Year>0)
				DateTimeQ = new And(
					new Q(Event.Columns.DateTime,QueryOperator.GreaterThanOrEqualTo,new DateTime(Year,1,1)),
					new Q(Event.Columns.DateTime,QueryOperator.LessThan,new DateTime(Year,1,1).AddYears(1)));
			q.TopRecords=2;
			q.TableElement=Event.GroupJoin;
			q.QueryCondition=new And(new Q(Group.Columns.K,this.K),DateTimeQ);
			EventSet es = new EventSet(q);
			if (es.Count==1)
				return es[0];
			else
				return null;
		}
		#endregion



		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion
		#region IHasIcon Members
		public string IconSrc
		{
			get { return "/gfx/icon-group.png"; }
		}
		#endregion
		
		Q IDiscussable.QueryConditionForGettingThreads
		{
			get
			{
				return Group.ThreadsQ(K);
			}
		}
		bool IDiscussable.ShowPrivateThreads { get { return true; } }
		IDiscussable IDiscussable.UsedDiscussable { get { return Brand as IDiscussable ?? this; } }
		bool IDiscussable.OnlyShowThreads { get { return false; } }
	}
	#endregion

	#region GroupUsr
	/// <summary>
	/// Links a private group to many users
	/// </summary>
	[Serializable] 
	public partial class GroupUsr 
	{
		#region simple members
		/// <summary>
		/// The user that has been invited
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[GroupUsr.Columns.UsrK]; }
			set { usr = null; this[GroupUsr.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The group
		/// </summary>
		public override int GroupK
		{
			get { return (int)this[GroupUsr.Columns.GroupK]; }
			set { group = null; this[GroupUsr.Columns.GroupK] = value; }
		}
		/// <summary>
		/// Membershp status: Member=1, Requested=2, Denied=3, Invited=4, Exited=5, Barred=6
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[GroupUsr.Columns.Status]; }
			set { this[GroupUsr.Columns.Status] = value; }
		}
		/// <summary>
		/// DateTime that the status last changed
		/// </summary>
		public override DateTime StatusChangeDateTime
		{
			get { return (DateTime)this[GroupUsr.Columns.StatusChangeDateTime]; }
			set { this[GroupUsr.Columns.StatusChangeDateTime] = value; }
		}
		/// <summary>
		/// The UsrK of the user that performed the last status change
		/// </summary>
		public override int StatusChangeUsrK
		{
			get { return (int)this[GroupUsr.Columns.StatusChangeUsrK]; }
			set { statusChangeUsr = null; this[GroupUsr.Columns.StatusChangeUsrK] = value; }
		}
		/// <summary>
		/// Is the user an owner of this group?
		/// </summary>
		public override bool Owner
		{
			get { return (bool)this[GroupUsr.Columns.Owner]; }
			set { this[GroupUsr.Columns.Owner] = value; }
		}
		/// <summary>
		/// Is the user a moderator of this group?
		/// </summary>
		public override bool Moderator
		{
			get { return (bool)this[GroupUsr.Columns.Moderator]; }
			set { this[GroupUsr.Columns.Moderator] = value; }
		}
		/// <summary>
		/// Moderators with this flag set can post news.
		/// </summary>
		public override bool NewsAdmin
		{
			get { return (bool)this[GroupUsr.Columns.NewsAdmin]; }
			set { this[GroupUsr.Columns.NewsAdmin] = value; }
		}
		/// <summary>
		/// Moderators with this flag set can invite/confirm/ban members.
		/// </summary>
		public override bool MemberAdmin
		{
			get { return (bool)this[GroupUsr.Columns.MemberAdmin]; }
			set { this[GroupUsr.Columns.MemberAdmin] = value; }
		}
		/// <summary>
		/// Is this group a favourite?
		/// </summary>
		public override bool Favourite
		{
			get { return (bool)this[GroupUsr.Columns.Favourite]; }
			set { this[GroupUsr.Columns.Favourite] = value; }
		}
		/// <summary>
		/// Message sent to the person by the person that invited them
		/// </summary>
		public override string InviteMessage
		{
			get { return (string)this[GroupUsr.Columns.InviteMessage]; }
			set { this[GroupUsr.Columns.InviteMessage] = value; }
		}
		/// <summary>
		/// This is the user that invited this person
		/// </summary>
		public override int InviteUsrK
		{
			get { return (int)this[GroupUsr.Columns.InviteUsrK]; }
			set { inviteUsr = null; this[GroupUsr.Columns.InviteUsrK] = value; }
		}
		/// <summary>
		/// Should this membership admin receive new user emails?
		/// </summary>
		public override bool MemberAdminNewUserEmails
		{
			get { return (bool)this[GroupUsr.Columns.MemberAdminNewUserEmails]; }
			set { this[GroupUsr.Columns.MemberAdminNewUserEmails] = value; }
		}
		#endregion

		#region JoinedCommentAlert
		public CommentAlert JoinedCommentAlert
		{
			get
			{
				if (joinedCommentAlert==null)
				{
					joinedCommentAlert = new CommentAlert(this, GroupUsr.Columns.GroupK);
				}
				return joinedCommentAlert;
			}
			set
			{
				joinedCommentAlert = value;
			}
		}
		private CommentAlert joinedCommentAlert;
		#endregion

		#region InviteHtml
		public string InviteHtml()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.InviteUsr.Link());
			sb.Append(" invited you to the ");
			sb.Append("<a href=\"");
			sb.Append(this.Group.Url());
			sb.Append("\">");
			sb.Append(this.Group.FriendlyName);
			sb.Append("</a> group ");
			sb.Append(Cambro.Misc.Utility.FriendlyTime(this.StatusChangeDateTime,false));
			//sb.Append(". <small>Need <a href=\"");
			//sb.Append(this.Group.Url());
			//sb.Append("\">more info</a>?</small>");
			if (this.InviteMessage.Length>0)
			{
				sb.Append("<div style=\"margin-top:3px;\"><i><small>\"");
				sb.Append(this.InviteMessage);
				sb.Append("\"</small></i></div>");
			}

			return sb.ToString();
		}
		#endregion
		
		#region MembershipAdminStatusHtml
		public string MembershipAdminStatusHtml
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(Status.ToString());
				sb.Append(" <small>");
				if (this.StatusChangeDateTime>DateTime.MinValue)
					sb.Append(Cambro.Misc.Utility.FriendlyTime(this.StatusChangeDateTime,false));
				if (StatusChangeUsrK > 0 && StatusChangeUsrK != UsrK)
				{
					sb.Append(" by <a href=\"");
					sb.Append(StatusChangeUsr.Url());
					sb.Append("\" ");
					sb.Append(StatusChangeUsr.Rollover);
					sb.Append(">");
					sb.Append(StatusChangeUsr.NickName);
					sb.Append("</a>");
				}
				if (InviteUsrK > 0 && InviteUsrK != UsrK && InviteUsrK != StatusChangeUsrK)
				{
					sb.Append(" (invited by <a href=\"");
					sb.Append(InviteUsr.Url());
					sb.Append("\" ");
					sb.Append(InviteUsr.Rollover);
					sb.Append(">");
					sb.Append(InviteUsr.NickName);
					sb.Append(")</a>");
				}
				sb.Append("</small>");
				return sb.ToString();
			}
		}
		#endregion
		#region MemberAdminPermission
		public MemberAdminPermissionClass MemberAdminPermission
		{
			get
			{
				return new MemberAdminPermissionClass(this);
			}
		}
		public class MemberAdminPermissionClass
		{
			public bool Bar = false;
			public bool Accept = false;
			public bool Reject = false;
			public bool UnBar = false;
			public bool Invite = false;
			
			public MemberAdminPermissionClass(GroupUsr gu)
			{
				if (gu.Status.Equals(StatusEnum.Member))
				{
					Bar = true;
				}
				else if (gu.Status.Equals(StatusEnum.Request))
				{
					Accept = true;
					Reject = true;
					Bar = true;
				}
				else if (gu.Status.Equals(StatusEnum.RequestRejected))
				{
					Accept = true;
					Bar = true;
				}
				else if (gu.Status.Equals(StatusEnum.Invite))
				{
					Bar = true;
				}
				else if (gu.Status.Equals(StatusEnum.InviteRejected))
				{
					Bar = true;
				}
				else if (gu.Status.Equals(StatusEnum.Exited))
				{
					Bar = true;
				}
				else if (gu.Status.Equals(StatusEnum.Barred))
				{
					UnBar = true;
				}
				else if (gu.Status.Equals(StatusEnum.Recommend))
				{
					Invite = true; 
					Reject = true;
					Bar = true;
				}
				else if (gu.Status.Equals(StatusEnum.RecommendRejected))
				{
					Invite = true; 
					Bar = true;
				}
			}
		}
		#endregion
		#region AddMemberAdminOptions
		public void AddMemberAdminOptions(TableCell cell)
		{
			if (MemberAdminPermission.Accept)
				AddLink("accept","accept",cell,false);
			if (MemberAdminPermission.Invite)
				AddLink("invite","invite",cell,false);
			if (MemberAdminPermission.Reject)
				AddLink("reject","reject",cell,true);
			if (MemberAdminPermission.Bar)
				AddLink("bar","bar",cell,true);
			if (MemberAdminPermission.UnBar)
				AddLink("un-bar","unbar",cell,true);
		}
		#endregion
		#region AddLink
		void AddLink(string text, string command, TableCell cell, bool confirm)
		{
			if (cell.Controls.Count>0)
				cell.Controls.Add(new LiteralControl(" | "));
			LinkButton link = new LinkButton();
			link.ID = command;
			link.Text = text;
			link.CommandName = command;
			link.CommandArgument=this.UsrK.ToString();
			if (confirm)
				link.Attributes["onclick"]="return confirm('Are you sure?');";
			cell.Controls.Add(link);
		}
		#endregion
		#region StatusEnum
		#endregion

		#region StatusPermissionLevel
		public int StatusPermissionLevel
		{
			get
			{
				if (Status.Equals(StatusEnum.Member))
					return 2;
				else if (Status.Equals(StatusEnum.Invite) ||
					Status.Equals(StatusEnum.InviteRejected) ||
					Status.Equals(StatusEnum.Exited))
					return 1;
				else if (Status.Equals(StatusEnum.Request) ||
					Status.Equals(StatusEnum.Recommend))
					return 0;
				else if (Status.Equals(StatusEnum.RequestRejected) ||
					Status.Equals(StatusEnum.Barred) ||
					Status.Equals(StatusEnum.RecommendRejected))
					return -1;
				else
					return 0;
			}
		}
		#endregion

		#region ChangeModeratorPermission
		public void ChangeModeratorPermission(bool Moderator, bool NewsAdmin, bool MemberAdmin, bool Owner)
		{
			if (Owner != this.Owner ||
				MemberAdmin != this.MemberAdmin ||
				NewsAdmin != this.NewsAdmin ||
				Moderator != this.Moderator)
			{
				this.Owner = Owner;
				if (this.MemberAdmin != MemberAdmin)
				{
					this.MemberAdmin = MemberAdmin;
					this.MemberAdminNewUserEmails = MemberAdmin;
				}
				this.NewsAdmin = NewsAdmin;
				this.Moderator = Moderator;
				this.Update();
				this.Usr.UpdateIsGroupModerator();

				Mailer m = new Mailer();
				m.UsrRecipient=this.Usr;
				m.Subject="Group moderator permissions changed - "+this.Group.FriendlyName;
				m.Body="<p>"+Usr.Current.LinkEmail()+" has changed your moderator permissions for the "+this.Group.FriendlyName+" group. Your new moderator permissions are:</p>";
				if (this.Owner)
					m.Body+="<p><b>Owner</b><br>You have full permissions on this group, including the ability to add and delete moderators, and change their permissions.</p>";
				if (this.MemberAdmin)
					m.Body+="<p><b>Membership admin</b><br>You have the ability to bar people from the group, or invite new members. If the group has restricted membership, you can confirm / deny membership requests.</p>";
				if (this.NewsAdmin)
					m.Body+="<p><b>News admin</b><br>You have the ability to add news to the group. Each news article gets sent as an email to all members.</p>";
				if (this.Moderator)
					m.Body+="<p><b>Moderator</b><br>You have the ability to delete threads from the group chat.</p>";
				else
					m.Body+="<p><b>No moderator status</b><br>Your moderator status has been removed.</p>";
				m.Body+="<p>Click the login link below to see the new options available to you.</p>";
				m.RedirectUrl=this.Group.UrlApp("admin");
				m.Send();
			}
		}
		#endregion

		#region IsMember
		public bool IsMember
		{
			get
			{
				return Status.Equals(StatusEnum.Member);
			}
		}
		#endregion

		#region ModeratorHtml
		public string ModeratorHtml
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				if (this.Moderator)
				{
					sb.Append("<a href=\"");
					sb.Append(this.Group.UrlApp("admin"));
					sb.Append("\"><img src=\"/gfx/icon-admin.png\" width=\"26\" height=\"21\" border=\"0\" align=\"absmiddle\" style=\"margin-right:0px;\" onmouseover=\"stt('Moderator options');\" onmouseout=\"htm();\"></a>");
				}
				return sb.ToString();
			}
		}
		#endregion
		
		#region WatchingHtml
		public string WatchingHtml(Control controlForScript)
		{
			string stateWatch = "0";
			if (JoinedCommentAlert.UsrK>0)
				stateWatch = "1";

			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "w" + this.GroupK.ToString(), String.Format("DbButtonFull(i1,i2,a1,a2,\"\",\"\",\"\",s1,l1,26,21,f1,\"{0},15\",{1},\"w{0}\",\"\",\"\",\"\",\"w{0}\");", this.GroupK.ToString(), stateWatch), true);
			return String.Format("<div style=\"width:26px;height:21px;\" id=\"w{0}\"><img src=\"{1}\" align=\"left\" width=\"26\" height=\"21\"></div>", this.GroupK.ToString(), stateWatch == "1" ? "/gfx/icon-eye-up.png" : "/gfx/icon-eye-dn.png");
		}
		#endregion
		#region FavouriteHtml
		public string FavouriteHtml(Control controlForScript)
		{
			string stateFavourite = "0";
			if (this.Favourite)
				stateFavourite = "1";

			ScriptManager.RegisterStartupScript(controlForScript, typeof(Page), "f" + this.GroupK.ToString(), String.Format("DbButtonFull(i3,i4,a3,a4,\"\",\"\",\"\",s2,l2,22,21,f2,{0},{1},\"f{0}\",\"\",\"\",\"\",\"f{0}\");", this.GroupK.ToString(), stateFavourite), true);
			return String.Format("<div style=\"width:26px;height:21px;\" id=\"f{0}\"><img src=\"{1}\" align=\"left\" width=\"22\" height=\"21\"></div>", this.GroupK.ToString(), stateFavourite == "1" ? "/gfx/icon-star-22-up.png" : "/gfx/icon-star-22-dn.png");
			
		}
		#endregion
		#region GroupJoin
		/// <summary>
		/// Joins the GroupUsr table to the Group table using a column join
		/// </summary>
		public static Join GroupJoin
		{
			get
			{
				return new Join(GroupUsr.Columns.GroupK,new Column(GroupUsr.Columns.GroupK,Group.Columns.K));
			}
		}
		#endregion
		#region UsrJoin
		/// <summary>
		/// Joins the GroupUsr table to the Usr table using a column join
		/// </summary>
		public static Join UsrJoin
		{
			get
			{
				return new Join(GroupUsr.Columns.UsrK,new Column(GroupUsr.Columns.UsrK,Usr.Columns.K));
			}
		}
		#endregion
		#region UsrAndStatusChangeUsrJoin
		/// <summary>
		/// Joins the GroupUsr table to the Usr table using a column join, also to the Usr table via the StatusChangeUsrK (left join)
		/// </summary>
		public static Join UsrAndStatusChangeUsrJoin
		{
			get
			{
				return new Join(
					new Join(
						GroupUsr.UsrJoin,
						new TableElement(new Column(GroupUsr.Columns.StatusChangeUsrK,Usr.Columns.K)),
						QueryJoinType.Left,
						GroupUsr.Columns.StatusChangeUsrK,
						new Column(GroupUsr.Columns.StatusChangeUsrK,Usr.Columns.K)
					),
					new TableElement(new Column(GroupUsr.Columns.InviteUsrK, Usr.Columns.K)),
					QueryJoinType.Left,
					GroupUsr.Columns.InviteUsrK,
					new Column(GroupUsr.Columns.InviteUsrK, Usr.Columns.K)
				);
			}
		}
		#endregion
		#region GroupAndInvitingUsrJoin
		/// <summary>
		/// Joins the GroupUsr table to the Group table and to the Usr table via the InvitingUsrK column
		/// </summary>
		public static Join GroupAndInvitingUsrJoin
		{
			get
			{
				return new Join(
					GroupUsr.GroupJoin,
					new TableElement(new Column(GroupUsr.Columns.InviteUsrK,Usr.Columns.K)),
					QueryJoinType.Inner,
					GroupUsr.Columns.InviteUsrK,
					new Column(GroupUsr.Columns.InviteUsrK,Usr.Columns.K)
				);
			}
		}
		#endregion

		#region Links to Bobs
		#region Group
		/// <summary>
		/// The group
		/// </summary>
		public Group Group
		{
			get
			{
				if (group==null)
					group = new Group(GroupK,this,Columns.GroupK);
				return group;
			}
		}
		Group group;
		#endregion
		#region Usr
		/// <summary>
		/// The user
		/// </summary>
		public Usr Usr
		{
			get
			{
				if (usr==null)
					usr = new Usr(UsrK,this,Columns.UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region InviteUsr
		/// <summary>
		/// The user that invited this person
		/// </summary>
		public Usr InviteUsr
		{
			get
			{
				if (inviteUsr==null)
					inviteUsr = new Usr(InviteUsrK,this,Columns.InviteUsrK);
				return inviteUsr;
			}
		}
		Usr inviteUsr;
		#endregion
		#region StatusChangeUsr
		/// <summary>
		/// The user that performed the last status change
		/// </summary>
		public Usr StatusChangeUsr
		{
			get
			{
				if (statusChangeUsr==null)
					statusChangeUsr = new Usr(StatusChangeUsrK,this,Columns.StatusChangeUsrK);
				return statusChangeUsr;
			}
		}
		Usr statusChangeUsr;
		#endregion
		#endregion

	}
	#endregion

	#region GroupEvent
	/// <summary>
	/// Links a user to many events (my favorite events)
	/// </summary>
	[Serializable] 
	public partial class GroupEvent 
	{

		#region simple members
		/// <summary>
		/// Link to Group table
		/// </summary>
		public override int GroupK
		{
			get { return (int)this[GroupEvent.Columns.GroupK]; }
			set { group = null; this[GroupEvent.Columns.GroupK] = value; }
		}
		/// <summary>
		/// Link to the Event table
		/// </summary>
		public override int EventK
		{
			get { return (int)this[GroupEvent.Columns.EventK]; }
			set { _event = null; this[GroupEvent.Columns.EventK] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Group
		public Group Group
		{
			get
			{
				if (group==null)
					group = new Group(GroupK);
				return group;
			}
		}
		Group group;
		#endregion
		#region Event
		public Event Event
		{
			get
			{
				if (_event==null)
					_event = new Event(EventK);
				return _event;
			}
		}
		Event _event;
		#endregion
		#endregion
		
	}
	#endregion

	#region GroupPhoto

	#endregion

}










