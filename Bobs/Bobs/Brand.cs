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

namespace Bobs
{

	#region Brand
	/// <summary>
	/// e.g. Promoter / Event Brand
	/// </summary>
	[Serializable]
	public partial class Brand : IPage, IPic, IName, IReadableReference, IBobType, IDiscussable, ICalendar, IObjectPage, IHasArchive, IDeleteAll, IConnectedTo, ILinkable, IStyledEventHolder
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Brand.Columns.K] as int? ?? 0; }
			set { this[Brand.Columns.K] = value; }
		}
		/// <summary>
		/// Name of the brand
		/// </summary>
		public override string Name
		{
			get { return (string)this[Brand.Columns.Name]; }
			set { this[Brand.Columns.Name] = value; }
		}
		/// <summary>
		/// Link to the promoter - doesn't always have a promoter, so sometimes is 0.
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[Brand.Columns.PromoterK]; }
			set { promoter = null; this[Brand.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// Cropped image 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Brand.Columns.Pic]); }
			set { this[Brand.Columns.Pic] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Which user added this brand
		/// </summary>
		public override int OwnerUsrK
		{
			get { return (int)this[Brand.Columns.OwnerUsrK]; }
			set { owner = null; this[Brand.Columns.OwnerUsrK] = value; }
		}
		/// <summary>
		/// Has this brand just been added to the site?
		/// </summary>
		public override bool IsNew
		{
			get { return (bool)this[Brand.Columns.IsNew]; }
			set { this[Brand.Columns.IsNew] = value; }
		}
		/// <summary>
		/// Has this brand recently been edited?
		/// </summary>
		public override bool IsEdited
		{
			get { return (bool)this[Brand.Columns.IsEdited]; }
			set { this[Brand.Columns.IsEdited] = value; }
		}
		/// <summary>
		/// Guid used to ensure duplicate brands don't get posted if the user refreshes the page after saving.
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Brand.Columns.DuplicateGuid]); }
			set { this[Brand.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// How is this brand controlled by this promoter?
		/// </summary>
		public override PromoterStatusEnum PromoterStatus
		{
			get { return (PromoterStatusEnum)this[Brand.Columns.PromoterStatus]; }
			set { this[Brand.Columns.PromoterStatus] = value; }
		}
		/// <summary>
		/// Unique url-compliant name
		/// </summary>
		public override string UrlName
		{
			get { return (string)this[Brand.Columns.UrlName]; }
			set { this[Brand.Columns.UrlName] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Brand.Columns.PicState]; }
			set { this[Brand.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Brand.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Brand.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Brand.Columns.PicMiscK]; }
			set { picMisc = null; this[Brand.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// The party group
		/// </summary>
		public override int GroupK
		{
			get { return (int)this[Brand.Columns.GroupK]; }
			set { group = null; this[Brand.Columns.GroupK] = value; }
		}
		/// <summary>
		/// Total number of comments
		/// </summary>
		public override int TotalComments
		{
			get { return (int)this[Brand.Columns.TotalComments]; }
			set { this[Brand.Columns.TotalComments] = value; }
		}
		/// <summary>
		/// DateTime of the last post
		/// </summary>
		public override DateTime LastPost
		{
			get { return (DateTime)this[Brand.Columns.LastPost]; }
			set { this[Brand.Columns.LastPost] = value; }
		}
		/// <summary>
		/// Average DateTime of all the comments
		/// </summary>
		public override DateTime AverageCommentDateTime
		{
			get { return (DateTime)this[Brand.Columns.AverageCommentDateTime]; }
			set { this[Brand.Columns.AverageCommentDateTime] = value; }
		}
		/// <summary>
		/// When the brand was added to the system
		/// </summary>
		public override DateTime DateTimeCreated
		{
			get { return (DateTime)this[Brand.Columns.DateTimeCreated]; }
			set { this[Brand.Columns.DateTimeCreated] = value; }
		}
		/// <summary>
		/// Does this brand ban photos? e.g. Love Puppy / Manumission
		/// </summary>
		public override bool NoPhotos
		{
			get { return (bool)this[Brand.Columns.NoPhotos]; }
			set { this[Brand.Columns.NoPhotos] = value; }
		}
		/// <summary>
		/// Has daves util function added the regulars to the group?
		/// </summary>
		public override bool AddedRegulars
		{
			get { return (bool)this[Brand.Columns.AddedRegulars]; }
			set { this[Brand.Columns.AddedRegulars] = value; }
		}
		/// <summary>
		/// Css to emit for the styled pages
		/// </summary>
		public override string StyledCss
		{
			get { return (string)this[Brand.Columns.StyledCss]; }
			set { this[Brand.Columns.StyledCss] = value; }
		}
		/// <summary>
		/// Xml to emit for the styled pages
		/// </summary>
		public override string StyledXml
		{
			get { return (string)this[Brand.Columns.StyledXml]; }
			set { this[Brand.Columns.StyledXml] = value; }
		}
		#endregion

		#region IStyledEventHolder Members

		public bool IsEvent(Event evnt)
		{
			return evnt.IsBrand(this.K);
		}

		public string UrlStyledSetup(params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledSetup(this);
		}

		public string UrlStyledCalendar(int year, int month, params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledCalendar(this, year, month, par);
		}

		public string UrlStyled(params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyled(this, par);
		}

		public string UrlStyledApp(string application, params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledApp(this, application, par);
		}

		public string UrlStyledEvent(int eventK, params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledEvent(this, eventK, par);
		}

		public string UrlStyledEventLink(Event evnt, params string[] par)
		{
			return IStyledEventHolderExtentions.UrlStyledEventLink(this, evnt, par);
		}

		public string UrlCss()
		{
			return IStyledEventHolderExtentions.UrlCss(this);
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

		#region IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			if (objectType.Equals(Model.Entities.ObjectType.Brand) && this.K == objectK)
				return true;

			if (objectType.Equals(Model.Entities.ObjectType.Group) && this.GroupK == objectK)
				return true;

			if (Group.CanBeConnectedToStatic(objectType) && this.Group.IsConnectedTo(objectType, objectK))
				return true;

			return false;
		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			if (o.Equals(Model.Entities.ObjectType.Group))
				return true;

			if (Group.CanBeConnectedToStatic(o))
				return true;

			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Brand.CanBeConnectedToStatic(o);
		}
		#endregion

		#region MergeAndDelete
		public void MergeAndDelete(Brand merge)
		{

			if (this.K == merge.K)
				throw new DsiUserFriendlyException("Can't merge brand into itself!");

			Cambro.Web.Helpers.WriteAlertHeader();

			Cambro.Web.Helpers.WriteAlert("Starting brand merge...", true);

			Cambro.Web.Helpers.WriteAlert("Merging promoter...", true);
			if (merge.PromoterK > 0 && merge.PromoterStatus.Equals(PromoterStatusEnum.Confirmed) && merge.Promoter.IsEnabled)
			{
				this.PromoterK = merge.PromoterK;
				this.PromoterStatus = PromoterStatusEnum.Confirmed;
			}
			else if (this.PromoterK == 0 && merge.PromoterK > 0)
			{
				this.PromoterK = merge.PromoterK;
				this.PromoterStatus = merge.PromoterStatus;
			}
			Cambro.Web.Helpers.WriteAlert("Done merging promoter...");

			Cambro.Web.Helpers.WriteAlert("Merging events...", true);
			EventBrandSet ebs = new EventBrandSet(new Query(new Q(EventBrand.Columns.BrandK, merge.K)));
			foreach (EventBrand eb in ebs)
			{
				Cambro.Web.Helpers.WriteAlert("Merging event " + eb.EventK + "...");
				eb.Event.AssignBrand(this.K, true, null);
			}
			Cambro.Web.Helpers.WriteAlert("Done merging events...");

			Cambro.Web.Helpers.WriteAlert("Merging competitions...", true);
			Update uComp = new Update();
			uComp.Table = TablesEnum.Comp;
			uComp.Where = new Q(Comp.Columns.BrandK, merge.K);
			uComp.Changes.Add(new Assign(Comp.Columns.BrandK, this.K));
			uComp.Run();
			Cambro.Web.Helpers.WriteAlert("Done merging competitions...");

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

			this.Update(null);

			Cambro.Web.Helpers.WriteAlert("*** Merging group...", true);
			try
			{
				this.Group.MergeAndDelete(merge.Group);
				Cambro.Web.Helpers.WriteAlert("*** Done group merge...", true);
			}
			catch 
			{
				Cambro.Web.Helpers.WriteAlert("*** Exception merging group...", true);
			}

			Cambro.Web.Helpers.WriteAlert("Deleting old brand...", true);
			merge.DeleteAll(null);
			Cambro.Web.Helpers.WriteAlert("Done deleting old brand...");

			Cambro.Web.Helpers.WriteAlert("Updating stats...", true);
			this.CreateUniqueUrlName(true);
			this.UpdateTotalComments(null);
			Cambro.Web.Helpers.WriteAlert("Done updating stats...");

			Cambro.Web.Helpers.WriteAlert("Done merging brands!", true);

		}
		#endregion

		#region LinkColumns
		public static ColumnSet LinkColumns
		{
			get
			{
				return new ColumnSet(
					Columns.K, 
					Columns.Name,
					Columns.UrlName);
			}
		}
		#endregion

		#region PromoterStatusEnum
		#endregion

		#region Joins
		public static Join EventJoin
		{
			get
			{
				return new Join(new Join(Brand.Columns.K,EventBrand.Columns.BrandK),Event.Columns.K,EventBrand.Columns.EventK);
			}
		}
		#endregion

		#region PreHtml
		public string PreHtml
		{
			get
			{
				if (this.IsNew)
					return "<span style=\"font-weight:bold;color:#ff0000;\">NEW</span> ";
				else if (this.IsEdited)
					return "<span style=\"font-weight:bold;color:#ff0000;\">EDITED</span> ";
				else 
					return "";

			}
		}
		#endregion
		#region PostHtml
		public string PostHtml
		{
			get
			{
				if (this.IsNew || this.IsEdited)
					return " <b><a href=\""+this.Url("Reset","1")+"\" target=\"BrandResetWindow\">reset new/edited</a></b>";
				else 
					return "";

			}
		}
		#endregion
		#region PicRollover
		public string PicRollover
		{
			get
			{
				if (this.HasPic)
					return " <a href=\"\" style=\"font-weight:"+((IsNew || IsEdited)?"bold":"normal")+";\" onmouseover=\"stm('<img src="+this.PicPath+" width=100 height=100 class=Block>');\" onmouseout=\"htm();\">PIC</a>";
				else 
					return "";
			}
		}
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			//EventBrands
			Delete BrandDelete = new Delete(
				TablesEnum.EventBrand,
				new Q(EventBrand.Columns.BrandK, this.K)
				);
			BrandDelete.Run(transaction);

			CompSet cs = new CompSet(new Query(new Q(Comp.Columns.BrandK, this.K)));
			foreach (Comp c in cs)
			{
				c.BrandK = 0;
				c.LinkType = Comp.LinkTypes.None;
				c.Update();
			}

			try
			{
				this.Group.DeleteAll(transaction);
			}
			catch { }

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

		#region ThreadsQGroup
		public static Q ThreadsQGroup(Brand b)
		{
			return new Q(Thread.Columns.GroupK, b.GroupK);
		}
		#endregion

		#region ThreadsQEvents
		public static Q ThreadsQEvents(Brand b)
		{
			return new Q(EventBrand.Columns.BrandK, b.K);
		}
		#endregion

		#region CompQ
		public static Q CompQ(int BrandK)
		{
			return new Or(
				new And(new Q(EventBrand.Columns.BrandK,BrandK),new Q(Comp.Columns.BrandK,BrandK)),
				new And(new Or(new Q(EventBrand.Columns.BrandK,0),new Q(EventBrand.Columns.BrandK,QueryOperator.IsNull,null)),new Q(Comp.Columns.BrandK,BrandK)),
				new And(new Q(EventBrand.Columns.BrandK,BrandK),new Or(new Q(Comp.Columns.BrandK,0),new Q(Comp.Columns.BrandK,QueryOperator.IsNull,null)))
			);
		}
		#endregion

		#region Events
		public EventSet NextEventSet
		{
			get
			{
				Query q = new Query();
				q.NoLock = true;
				q.TableElement=Event.BrandJoin;
				q.QueryCondition=new And(new Q(Brand.Columns.K,this.K),Event.FutureEventsQueryCondition);
				q.OrderBy=Event.FutureEventOrder;
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
			q.TableElement=Event.BrandJoin;
			q.QueryCondition=new Q(Brand.Columns.K,this.K);
			q.ReturnCountOnly=true;
			EventSet es = new EventSet(q);
			int i = es.Count;
			return i;
		}
		#endregion

		#region Links to Bobs
		#region Promoter
			public Promoter Promoter
		{
			get
			{
				if (promoter==null && PromoterK>0)
					promoter = new Promoter(PromoterK);
				return promoter;
			}
			set
			{
				promoter = value;
			}
		}
		private Promoter promoter;
		#endregion
		#region Usr
		public Usr Owner
		{
			get
			{
				if (owner==null && OwnerUsrK>0)
					owner = new Usr(OwnerUsrK);
				return owner;
			}
			set
			{
				owner = value;
			}
		}
		private Usr owner;
		#endregion
		#region Group
		public Group Group
		{
			get
			{
				if (group==null && GroupK>0)
					group = new Group(GroupK);
				return group;
			}
			set
			{
				group = value;
			}
		}
		private Group group;
		#endregion
		#endregion

		#region Links to BobSets
		#region Events
		public EventSet Events
		{
			get
			{
				if (events==null)
				{
					Query q = new Query();
					q.NoLock=true;
					q.TableElement=Event.EventBrandJoin;
					q.QueryCondition=new Q(EventBrand.Columns.BrandK,this.K);
					events = new EventSet(q);
				}
				return events;
			}
			set{events=value;}
		}
		EventSet events;
		#endregion
		#endregion

		#region Url
		public void UpdateChildUrlFragments(bool Cascade)
		{
			Update uThreads = new Update();
			uThreads.Table=TablesEnum.Thread;
			uThreads.Changes.Add(new Assign(Thread.Columns.UrlFragment,UrlFilterPart));
			uThreads.Where=new And(
				new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Brand),
				new Q(Thread.Columns.ParentObjectK,this.K));
			uThreads.Run();
		}
		public string UrlFragment
		{
			get
			{
				return "parties";
			}
		}
		public string UrlFilterPart
		{
			get
			{
				return UrlFragment+"/"+this.UrlName;
			}
		}
		public string Url(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,null,par);
		}
		public string UrlApp(string Application, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,Application,par);
		}
		#endregion

		#region CreateUniqueUrlName()
		public void CreateUniqueUrlName(bool updateGroup)
		{
			string urlName = UrlInfo.GetUrlName(this.Name);
			if (urlName.Length==0)
				urlName = "brand-"+this.K.ToString();
			if (UrlInfo.IsReservedString(urlName))
				urlName = "brand-"+urlName;

			BrandSet bs = null;
			int namePost = 0;
			string newName = urlName;
			while (bs==null || bs.Count>0)
			{
				if (namePost>0)
					newName = urlName+"-"+namePost.ToString();
				Query q = new Query();
				q.NoLock=true;
				q.ReturnCountOnly=true;
				q.QueryCondition=new And(
					new Q(Brand.Columns.UrlName,newName),
					new Q(Brand.Columns.K,QueryOperator.NotEqualTo,this.K)
				);
				bs = new BrandSet(q);
				namePost++;
			}
			
			if (!this.UrlName.Equals(newName))
			{
				this.UrlName = newName;
				this.Update();
				Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Brand, this.K, true);
				job.ExecuteAsynchronously();

				if (updateGroup)
				{
					this.Group.UrlName="parties/"+newName;
					this.Group.Update();
					
					Utilities.UpdateChildUrlFragmentsJob job1 = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Group, this.GroupK, true);
					job1.ExecuteAsynchronously();

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
				return Name;
			}
		}

		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion

		#region IBobType Members

		public string TypeName
		{
			get
			{
				return "Brand";
			}
		}

		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Brand;
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
//				// TODO:  Add Brand.Threads getter implementation
//				return null;
//			}
//			set
//			{
//				// TODO:  Add Brand.Threads setter implementation
//			}
//		}

		#region UpdateTotalComments()
		public void UpdateTotalComments(Transaction transaction)
		{
			Query q = new Query();
			q.TableElement = new Join(
				new TableElement(TablesEnum.Thread),
				new TableElement(TablesEnum.EventBrand),
				QueryJoinType.Left,
				Thread.Columns.EventK,
				EventBrand.Columns.EventK);
			q.QueryCondition = Brand.ThreadsQEvents(this);
			q.ExtraSelectElements = ForumStats.ExtraSelectElements;
			q.Columns = new ColumnSet();
			ForumStats cs = new ForumStats(q);

			this.TotalComments=cs.TotalComments;
			this.AverageCommentDateTime=cs.AverageCommentDateTime;
			this.LastPost=cs.LastPost;

			Update(transaction);
		}
		#endregion

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
		public string UrlCalendarMonth(bool Tickets, bool FreeGuestlist, int Year, int Month, int SkipDay, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, Year, Month, 0, SkipDay, par);
		}
		public string UrlCalendarMonth(int Year, int Month, int SkipDay, params string[] par)
		{
			return UrlCalendarGeneric(null, Year, Month, 0, SkipDay, par);
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
			q.TableElement=Event.BrandJoin;
			q.QueryCondition=new And(new Q(Brand.Columns.K,this.K),DateTimeQ);
			EventSet es = new EventSet(q);
			if (es.Count==1)
				return es[0];
			else
				return null;
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
		Q IDiscussable.QueryConditionForGettingThreads
		{
			get
			{
				return Group.ThreadsQ(GroupK);
			}
		}
		bool IDiscussable.ShowPrivateThreads { get { return true; } }
		IDiscussable IDiscussable.UsedDiscussable { get { return this; } }
		bool IDiscussable.OnlyShowThreads { get { return false; } }


		#region BrandPageShowEventChat
		public bool BrandPageShowEventChat
		{
			get
			{
				return this.Group.TotalComments == 0 || (Prefs.Current["BrandChat"].Exists && Prefs.Current["BrandChat"].Equals("Brand") && this.TotalComments > 0);
			}
		}
		#endregion
	}
	#endregion
	#region BrandSet
	[Serializable] 
	public partial class BrandSet : BobSet
	{
		public void WriteLinks(StringBuilder sb)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (i > 0)
				{
					if (i == this.Count - 1)
						sb.Append(" and ");
					else
						sb.Append(", ");
				}
				sb.Append("<a href=\"");
				sb.Append(this[i].Url());
				sb.Append("\" >");
				sb.Append(this[i].FriendlyName);
				sb.Append("</a>");
			}
		}
	}
	#endregion

}
