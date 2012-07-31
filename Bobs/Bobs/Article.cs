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

	#region Article
	[Serializable]
	public partial class Article : IPic, IPage, ICanView, IHasPrimaryThread, IDiscussable, IName, IReadableReference, IBobType, IObjectPage, IRelevanceContributor, IArchive, IDeleteAll, IConnectedTo, ILinkable, IHasParent
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Article.Columns.K] as int? ?? 0; }
			set { this[Article.Columns.K] = value; }
		}
		/// <summary>
		/// The title of the article. Less that 100 characters.
		/// </summary>
		public override string Title
		{
			get { return (string)this[Article.Columns.Title]; }
			set { this[Article.Columns.Title] = value; }
		}
		/// <summary>
		/// Summary - couple of sentances about the article. Less than 30 words.
		/// </summary>
		public override string Summary
		{
			get { return (string)this[Article.Columns.Summary]; }
			set { this[Article.Columns.Summary] = value; }
		}
		/// <summary>
		/// Cropped image 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Article.Columns.Pic]); }
			set { this[Article.Columns.Pic] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Owner
		/// </summary>
		public override int OwnerUsrK
		{
			get { return (int)this[Article.Columns.OwnerUsrK]; }
			set { owner = null; this[Article.Columns.OwnerUsrK] = value; }
		}
		/// <summary>
		/// When the artical was originally added
		/// </summary>
		public override DateTime AddedDateTime
		{
			get { return (DateTime)this[Article.Columns.AddedDateTime]; }
			set { this[Article.Columns.AddedDateTime] = value; }
		}
		/// <summary>
		/// Status - New, Edit, Enabled, Disabled
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[Article.Columns.Status]; }
			set { this[Article.Columns.Status] = value; }
		}
		/// <summary>
		/// When the article was marked as enabled
		/// </summary>
		public override DateTime EnabledDateTime
		{
			get { return (DateTime)this[Article.Columns.EnabledDateTime]; }
			set { this[Article.Columns.EnabledDateTime] = value; }
		}
		/// <summary>
		/// Who marked the article as enabled?
		/// </summary>
		public override int EnabledUsrK
		{
			get { return (int)this[Article.Columns.EnabledUsrK]; }
			set { enabledUsr = null; this[Article.Columns.EnabledUsrK] = value; }
		}
		/// <summary>
		/// What's the geographic relevance? 0=All, 1=Country, 2=Region, 3=Place, 4=Venue, 5=Event
		/// </summary>
		public override RelevanceEnum Relevance
		{
			get { return (RelevanceEnum)this[Article.Columns.Relevance]; }
			set { this[Article.Columns.Relevance] = value; }
		}
		/// <summary>
		/// What's the type of the parent object?
		/// </summary>
		public override Model.Entities.ObjectType ParentObjectType
		{
			get { return (Model.Entities.ObjectType)this[Article.Columns.ParentObjectType]; }
			set { parentObject = null; this[Article.Columns.ParentObjectType] = value; }
		}
		/// <summary>
		/// What's the key of the parent object?
		/// </summary>
		public override int ParentObjectK
		{
			get { return (int)this[Article.Columns.ParentObjectK]; }
			set { 
				parentObject = null; 
				this[Article.Columns.ParentObjectK] = value;
			}
		}
		/// <summary>
		/// Event that this article is relevant to
		/// </summary>
		public override int EventK
		{
			get { return (int)this[Article.Columns.EventK]; }
			set
			{
				this.Event = null;
				this[Article.Columns.EventK] = value;
			}
		}
		/// <summary>
		/// Venue that this article is relevant to
		/// </summary>
		public override int VenueK
		{
			get { return (int)this[Article.Columns.VenueK]; }
			set
			{
				this.venue = null;
				this[Article.Columns.VenueK] = value;
			}
		}
		/// <summary>
		/// Place that this article is relevant to
		/// </summary>
		public override int PlaceK
		{
			get { return (int)this[Article.Columns.PlaceK]; }
			set
			{
				this.place = null;
				this[Article.Columns.PlaceK] = value;
			}
		}
		/// <summary>
		/// Place that this article is relevant to
		/// </summary>
		public override int CountryK
		{
			get { return (int)this[Article.Columns.CountryK]; }
			set { this[Article.Columns.CountryK] = value; }
		}
		/// <summary>
		/// Does the article have a single discussion thread, or does each paragraph have it's own thread?
		/// </summary>
		public override bool HasSingleThread
		{
			get { return (bool)this[Article.Columns.HasSingleThread]; }
			set { this[Article.Columns.HasSingleThread] = value; }
		}
		/// <summary>
		/// Admin note, to be edited by admins at Edit stage, and visible to the author
		/// </summary>
		public override string AdminNote
		{
			get { return (string)this[Article.Columns.AdminNote]; }
			set { this[Article.Columns.AdminNote] = value; }
		}
		/// <summary>
		/// Total number of views the article has had
		/// </summary>
		public override int Views
		{
			get { return (int)this[Article.Columns.Views]; }
			set { this[Article.Columns.Views] = value; }
		}
		/// <summary>
		/// Does the artical have worldwide relevance?
		/// </summary>
		public override bool IsWorldwide
		{
			get { return (bool)this[Article.Columns.IsWorldwide]; }
			set { this[Article.Columns.IsWorldwide] = value; }
		}
		/// <summary>
		/// Total number of comments in all threads in this article
		/// </summary>
		public override int TotalComments
		{
			get { return (int)this[Article.Columns.TotalComments]; }
			set { this[Article.Columns.TotalComments] = value; }
		}
		/// <summary>
		/// DateTime of thge last comments posted in this article
		/// </summary>
		public override DateTime LastPost
		{
			get { return (DateTime)this[Article.Columns.LastPost]; }
			set { this[Article.Columns.LastPost] = value; }
		}
		/// <summary>
		/// The average date/time of all the comments posted ion this article
		/// </summary>
		public override DateTime AverageCommentDateTime
		{
			get { return (DateTime)this[Article.Columns.AverageCommentDateTime]; }
			set { this[Article.Columns.AverageCommentDateTime] = value; }
		}
		/// <summary>
		/// Filename of the user control that will be displayed instead of the paragraphs.
		/// </summary>
		public override string OverrideContents
		{
			get { return (string)this[Article.Columns.OverrideContents]; }
			set { this[Article.Columns.OverrideContents] = value; }
		}
		/// <summary>
		/// Shall we hide the owner image?
		/// </summary>
		public override bool HideOwner
		{
			get { return (bool)this[Article.Columns.HideOwner]; }
			set { this[Article.Columns.HideOwner] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Article.Columns.PicState]; }
			set { this[Article.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Article.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Article.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Article.Columns.PicMiscK]; }
			set { picMisc = null; this[Article.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// The url fragment - so that the url can be generated without accessing parent database records
		/// </summary>
		public override string UrlFragment
		{
			get { return (string)this[Article.Columns.UrlFragment]; }
			set { this[Article.Columns.UrlFragment] = value; }
		}
		/// <summary>
		/// Main public override thread (first one started)
		/// </summary>
		public override int? ThreadK
		{
			get { return (int?) this[Article.Columns.ThreadK]; }
			set { this[Article.Columns.ThreadK] = value; }
		}
		/// <summary>
		/// Headline of the article for sending to Twitter
		/// </summary>
		public override string TwitterHeadline
		{
			get { return (string)this[Article.Columns.TwitterHeadline]; }
			set { this[Article.Columns.TwitterHeadline] = value; }
		}
		/// <summary>
		/// Is this article displayed in the Mixmag news section?
		/// </summary>
		public override bool IsMixmagNews
		{
			get { return (bool)this[Article.Columns.IsMixmagNews]; }
			set { this[Article.Columns.IsMixmagNews] = value; }
		}
		/// <summary>
		/// Is this article displayed in the Mixmag news section?
		/// </summary>
		public override bool IsExtendedDisplay
		{
			get { return (bool)this[Article.Columns.IsExtendedDisplay]; }
			set { this[Article.Columns.IsExtendedDisplay] = value; }
		}
		/// <summary>
		/// Which Mixmag sections is this article in?
		/// </summary>
		public override int MixmagSections
		{
			get { return (int)this[Article.Columns.MixmagSections]; }
			set { this[Article.Columns.MixmagSections] = value; }
		}
		/// <summary>
		/// Do we show this article above the fold on the front page?
		/// </summary>
		public override bool ShowAboveFoldOnFrontPage
		{
			get { return (bool)this[Article.Columns.ShowAboveFoldOnFrontPage]; }
			set { this[Article.Columns.ShowAboveFoldOnFrontPage] = value; }
		}
		#endregion

		public void EnableArticle(Usr enablingUsr, bool update, bool setEnabledDate)
		{
			this.Status = Model.Entities.Article.StatusEnum.Enabled;
			this.EnabledUsrK = enablingUsr.K;

			if (setEnabledDate)
				this.EnabledDateTime = DateTime.Now;

			if (update)
				this.Update();


			if (this.Owner.FacebookConnected && this.Owner.FacebookStoryPublishArticle)
			{
				try
				{
					FacebookPost.CreateArticlePublish(this.Owner, this);
				}
				catch { }
			}


		}

		public void MixmagSectionAdd(int index)
		{
			int mask = (int)Math.Pow(2.0, (double)index);
			int config = MixmagSections;

			int result = config & mask;

			if (result == 0)
			{
				MixmagSections += mask;
			}
			//this.Update();
		}

		public void MixmagSectionRemove(int index)
		{
			int mask = (int)Math.Pow(2.0, (double)index);
			int config = MixmagSections;

			int result = config & mask;

			if (result > 0)
			{
				MixmagSections -= mask;
			}
			//this.Update();
		}

		public bool MixmagIsSection(int index)
		{
			int mask = (int)Math.Pow(2.0, (double)index);
			int config = MixmagSections;

			int result = config & mask;

			return result > 0;
		}


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

		#region Thread
		public Thread Thread
		{
			get
			{
				if (thread == null && ThreadK > 0)
					thread = new Thread(ThreadK.Value);
				return thread;
			}
		}
		Thread thread;
		#endregion
		
		public object ThreadTableColumnToBeSet { get { return Thread.Columns.ArticleK; } }
		
		#region CanView(Usr u)
		public bool CanView(Usr u)
		{
			if (u == null)
			{
				return this.Status.Equals(Article.StatusEnum.Enabled);
			}
			else
			{
				if (this.Status.Equals(Article.StatusEnum.Enabled)
					|| u.IsAdmin
					|| u.IsSuper
					|| u.K == this.OwnerUsrK
					|| (this.Event != null && u.IsEnabledPromoterOfEventConfirmed(this.Event))
					|| (this.Venue != null && u.IsEnabledPromoterOfConfirmedVenue(this.Venue)))
					return true;
				else
					return false;
			}
		}
		#endregion

		public Para FirstPara
		{
			get
			{
				if (firstPara == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(Para.Columns.ArticleK, this.K);
					q.OrderBy = new OrderBy(Para.Columns.Order);
					q.TopRecords = 1;
					ParaSet ps = new ParaSet(q);
					firstPara = ps[0];
				}
				return firstPara;
			}
		}
		private Para firstPara;

		public void AddRelevant(IRelevanceHolder ContainerPage)
		{
			if (this.ParentEvent != null)
			{
				this.ParentEvent.AddRelevant(ContainerPage);
			}
			else if (this.ParentVenue != null)
			{
				this.ParentVenue.AddRelevant(ContainerPage);
			}
			else if (this.ParentPlace != null)
			{
				ContainerPage.RelevantPlacesAdd(this.PlaceK);
			}
		}


		#region IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			if (objectType.Equals(Model.Entities.ObjectType.Article) && this.K == objectK)
				return true;

			if (this.ParentObject is IConnectedTo)
			{
				IConnectedTo parent = (IConnectedTo)this.ParentObject;

				if (objectType.Equals(this.ParentObjectType) && this.ParentObjectK == objectK)
					return true;

				if (parent.CanBeConnectedTo(objectType) && parent.IsConnectedTo(objectType, objectK))
					return true;
			}

			return false;
		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			if (o.Equals(Model.Entities.ObjectType.Event))
				return true;

			if (Event.CanBeConnectedToStatic(o))
				return true;

			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Article.CanBeConnectedToStatic(o);
		}
		#endregion

		#region Rollover

		string mouseOverText
		{
			get
			{
				return "stma('"+Pic.ToString()+"');";
			}
		}

		public string Rollover
		{
			get
			{
				if (HasPic)
					return "onmouseover=\""+mouseOverText+"\" onmouseout=\"htm();\"";
				else
					return "";
			}
		}

		public void MakeRollover(HtmlControl c)
		{
			if (HasPic)
			{
				c.Attributes["onmouseover"]=mouseOverText;
				c.Attributes["onmouseout"]="htm();";
			}
		}
		public void MakeRollover(WebControl c)
		{
			if (HasPic)
			{
				c.Attributes["onmouseover"]=mouseOverText;
				c.Attributes["onmouseout"]="htm();";
			}
		}
		#endregion

		public static Q EnabledQueryCondition
		{
			get
			{
				return new Q(Article.Columns.Status, Article.StatusEnum.Enabled);
			}
		}

		public void ReOrder(int page)
		{
			Query q = new Query();
			q.QueryCondition=new And(
				new Q(Para.Columns.ArticleK,this.K),
				new Q(Para.Columns.Page,page)
			);
			q.OrderBy=new OrderBy(Para.Columns.Order);
			ParaSet ps = new ParaSet(q);
			double order = 1.0;
			foreach (Para p in ps)
			{
				p.Order=order;
				order = order + 1.0;
				p.Update();
			}
		}

		

		#region Links to BobSets
		public ParaSet GetParaInPage(int pageIndex)
		{
			Query q = new Query();
			q.QueryCondition=new And(new Q(Para.Columns.ArticleK,this.K),new Q(Para.Columns.Page,pageIndex));
			q.OrderBy=new OrderBy(Para.Columns.Order);
			q.TableElement=new Join(Para.Columns.PhotoK,Photo.Columns.K,QueryJoinType.Left);
			return new ParaSet(q);
		}

		#region Photos
		public PhotoSet PhotosByDate
		{
			get
			{
				if (photosByDate == null)
				{
					Query q = new Query();
					q.QueryCondition=new And(
							Photo.EnabledQueryCondition,
							new Q(Photo.Columns.ArticleK, K)
						);
					q.OrderBy=Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
					photosByDate = new PhotoSet(q);
				}
				return photosByDate;
			}
			set{photosByDate=value;}
		}
		PhotoSet photosByDate;
		#endregion

		#region Threads
		public ThreadSet Threads
		{
			get
			{
				if (threads==null)
				{
					Query q = new Query();
					q.QueryCondition=ThreadsQ(this.K);
					threads = new ThreadSet(q);
				}
				return threads;
			}
			set
			{
				threads = value;
			}
		}
		private ThreadSet threads;
		#endregion

		public static Q ThreadsQ(int ArticleK)
		{
			return new Q(Thread.Columns.ArticleK,ArticleK);
		}

		#endregion

		#region Event
		public Event Event
		{ 
			get 
			{ 
				if (_event==null && EventK>0)
					_event = new Event(EventK, this, Article.Columns.EventK);
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
				if (venue==null && VenueK>0)
					venue = new Venue(VenueK, this, Article.Columns.VenueK);
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
				if (place==null && PlaceK>0)
					place = new Place(PlaceK, this, Article.Columns.PlaceK);
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
				if (country==null && CountryK>0)
					country = new Country(CountryK, this, Article.Columns.CountryK);
				return country;
			}
			set
			{
				country = value;
			}
		}
		private Country country;
		#endregion

		#region Pic
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

		#region Galleries
		public GallerySet Galleries
		{
			get
			{
				if (galleries==null)
				{
					Query q = new Query();
					q.QueryCondition=new And(new Q(Gallery.Columns.ArticleK,K),new Q(Gallery.Columns.LivePhotos,QueryOperator.GreaterThan,0));
					q.TableElement=Gallery.OwnerJoin;
					q.OrderBy = new OrderBy(new OrderBy(Usr.Columns.IsProSpotter,OrderBy.OrderDirection.Descending),new OrderBy(Usr.Columns.IsSpotter,OrderBy.OrderDirection.Descending),new OrderBy(Usr.Columns.SpottingsTotal,OrderBy.OrderDirection.Descending));
					//q.OrderBy = new OrderBy(" (CASE WHEN [Usr].[SpotterK]>0 THEN 1 ELSE 0 END) DESC, [Usr].[LoginCount] DESC ");
					galleries = new GallerySet(q);
				}
				return galleries;
			}
			set
			{
				galleries = value;
			}
		}
		private GallerySet galleries;
		#endregion

		public void UpdateAncestorLinks()
		{
			parentObject = null;
			if (this.ParentObjectType.Equals(Model.Entities.ObjectType.Event))
			{
				this.EventK = ParentEvent.K;

				if (this.Relevance<=RelevanceEnum.Venue)
					this.VenueK = ParentEvent.VenueK;
				else
					this.VenueK = 0;

				if (this.Relevance<=RelevanceEnum.Place)
					this.PlaceK = ParentEvent.Venue.PlaceK;
				else 
					this.PlaceK = 0;

				if (this.Relevance<=RelevanceEnum.Country)
					this.CountryK = ParentEvent.Venue.Place.CountryK;
				else
					this.CountryK = 0;
			}
			else if (this.ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
			{
				this.EventK = 0;
				this.VenueK = ParentVenue.K;

				if (this.Relevance<=RelevanceEnum.Place)
					this.PlaceK = ParentVenue.PlaceK;
				else
					this.PlaceK = 0;

				if (this.Relevance<=RelevanceEnum.Country)
					this.CountryK = ParentVenue.Place.CountryK;
				else 
					this.CountryK = 0;

			}
			else if (this.ParentObjectType.Equals(Model.Entities.ObjectType.Place))
			{
				this.EventK = 0;
				this.VenueK = 0;
				this.PlaceK = ParentPlace.K;
				if (this.Relevance<=RelevanceEnum.Country)
					this.CountryK = ParentPlace.CountryK;
				else
					this.CountryK = 0;

			}
			else if (this.ParentObjectType.Equals(Model.Entities.ObjectType.Country))
			{
				this.EventK = 0;
				this.VenueK = 0;
				this.PlaceK = 0;
				this.CountryK = ParentCountry.K;
			}
			else if (this.ParentObjectType.Equals(Model.Entities.ObjectType.None))
			{
				this.EventK = 0;
				this.VenueK = 0;
				this.PlaceK = 0;
				this.CountryK = 0;
			}
			this.IsWorldwide = this.Relevance <= RelevanceEnum.Worldwide;
			this.Update();
			
			foreach (Thread t in this.Threads)
				t.UpdateAncestorLinks(null);

			this.UpdateUrlFragment(true);
			
		}

		#region LastPage
		public int LastPage
		{
			get
			{
				if (lastPage == 0)
				{
					Query q = new Query();
					q.QueryCondition=new Q(Para.Columns.ArticleK,this.K);
					q.OrderBy=new OrderBy(Para.Columns.Page,OrderBy.OrderDirection.Descending);
					q.TopRecords=1;
					ParaSet ps = new ParaSet(q);
					if (ps.Count==0)
						lastPage = 1;
					else 
						lastPage = ps[0].Page;
				}
				return lastPage;
			}
			set
			{
				lastPage = 0;
			}
		}
		private int lastPage=0;
		#endregion

		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;
			
			if (this.ThreadK > 0)
				this.Thread.DeleteAll(transaction);


			foreach (Thread t in this.Threads)
				t.DeleteAll(transaction);

			foreach (Para p in this.AllPara)
				p.DeleteAll(transaction);


			GallerySet gs = new GallerySet(new Query(new Q(Gallery.Columns.ArticleK,this.K)));
			foreach (Gallery g in gs)
				g.DeleteAll(transaction);

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

		#region Links to Bobs

		#region AllPara
		public ParaSet AllPara
		{
			get
			{
				if (allPara==null)
					allPara = new ParaSet(new Query(new Q(Para.Columns.ArticleK,this.K)));
				return allPara;
			}
			set
			{
				allPara = value;
			}
		}
		private ParaSet allPara;
		#endregion

		#region Owner
		public Usr Owner
		{
			get
			{
				if (owner==null && OwnerUsrK>0)
					owner = new Usr(OwnerUsrK, this, Article.Columns.OwnerUsrK);
				return owner;
			}
			set
			{
				owner = value;
			}
		}
		private Usr owner;
		#endregion

		#region EnabledUsr
		public Usr EnabledUsr
		{
			get
			{
				if (enabledUsr==null && EnabledUsrK>0)
					enabledUsr = new Usr(EnabledUsrK);
				return enabledUsr;
			}
			set
			{
				enabledUsr = value;
			}
		}
		private Usr enabledUsr;
		#endregion

		#region ParentObject
		public IBob ParentObject
		{
			get
			{
				if (parentObject==null)
					switch(ParentObjectType)
					{
						case Model.Entities.ObjectType.Event:
							parentObject = new Event(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Venue:
							parentObject = new Venue(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Place:
							parentObject = new Place(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Country:
							parentObject = new Country(ParentObjectK);
							break;
						default:
							break;
					}
				return parentObject;
			}
		}
		IBob parentObject;
		#endregion

		#region Typed Parent Accessors
		public Event ParentEvent 
		{ 
			get 
			{ 
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Event))
					return null;
				else
					return (Event)ParentObject;
			} 
		}
		public Venue ParentVenue 
		{ 
			get 
			{ 
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
					return null;
				else
					return (Venue)ParentObject; 
			} 
		}
		public Place ParentPlace 
		{ 
			get 
			{ 
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Place))
					return null;
				else
					return (Place)ParentObject; 
			} 
		}
		public Country ParentCountry
		{ 
			get 
			{ 
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Country))
					return null;
				else
					return (Country)ParentObject; 
			} 
		}
		public Article ParentArticle
		{ 
			get 
			{ 
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Article))
					return null;
				else
					return (Article)ParentObject;
			} 
		}
		#endregion

		#endregion

		#region StatusEnum
		#endregion

		#region IPage Members

		public void UpdateChildUrlFragments(bool Cascade)
		{
			Update uPhotos = new Update();
			uPhotos.Table=TablesEnum.Photo;
			uPhotos.Changes.Add(new Assign(Photo.Columns.UrlFragment,UrlFilterPart));
			uPhotos.Where=new Q(Photo.Columns.ArticleK,this.K);
			uPhotos.Run();

			Update uThreads = new Update();
			uThreads.Table=TablesEnum.Thread;
			uThreads.Changes.Add(new Assign(Thread.Columns.UrlFragment,UrlFilterPart));
			uThreads.Where=new Q(Thread.Columns.ArticleK,this.K);
			uThreads.Run();

			Update uGalleries = new Update();
			uGalleries.Table=TablesEnum.Gallery;
			uGalleries.Changes.Add(new Assign(Gallery.Columns.UrlFragment,UrlFilterPart));
			uGalleries.Where=new Q(Gallery.Columns.ArticleK,this.K);
			uGalleries.Run();
		}

		/// <summary>
		/// returns true if the urlFragment has changed
		/// </summary>
		/// <returns></returns>
		public bool InitUrlFragment()
		{
			string oldUrlFragment = this.UrlFragment;
			string newUrlFragment = GetUrlFragment();

			this.UrlFragment=newUrlFragment;
			return !oldUrlFragment.Equals(newUrlFragment);
		}

		public void UpdateUrlFragment(bool UpdateChildUrlFragments)
		{
			bool changed = InitUrlFragment();

			if (changed)
			{
				this.Update();
				if (UpdateChildUrlFragments)
				{
					Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Article, this.K, true);
					job.ExecuteAsynchronously();
				}
			}
		}

		public string GetUrlFragment()
		{
			if (ParentObject != null && ParentObject is IObjectPage)
			{
				if (ParentObject is Event)
					return ((Event)ParentObject).UrlFilterPartVenueDate;
				else
					return ((IObjectPage)ParentObject).UrlFilterPart;
			}
			else
				return "";
		}

		public string UrlFilterPart
		{
			get
			{
				string urlFragment = "";
				if (Common.Settings.DynamicUrlFragments)
					urlFragment = GetUrlFragment();
				else
					urlFragment = UrlFragment;

				if (urlFragment.Length > 0)
					return urlFragment + "/article-" + this.K.ToString();
				else
					return "article-" + this.K.ToString();
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

		public string UrlEdit(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] {"mode", "edit", "k", this.K.ToString() }, par);
			return UrlInfo.PageUrl("myarticles", fullParams);
		}

		#endregion

		#region IDiscuss Members

		public string UrlDiscussion(params string[] par)
		{
		//	if (ThreadK > 0)
		//		return Thread.UrlDiscussion(par);
		//	else
			return Url(par);

			//return UrlInfo.MakeUrl(UrlFilterPart,"chat",par);
		}

		public void UpdateTotalComments(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition = new Q(Thread.Columns.ArticleK,this.K);
			q.ExtraSelectElements = ForumStats.ExtraSelectElements;
			q.Columns = new ColumnSet();
			ForumStats cs = new ForumStats(q);
			this.TotalComments=cs.TotalComments;
			this.AverageCommentDateTime=cs.AverageCommentDateTime;
			this.LastPost=cs.LastPost;

			Update(transaction);
			if (ParentObjectK>0 && ParentObject!=null && ParentObject is IDiscussable)
				((IDiscussable)ParentObject).UpdateTotalComments(transaction);

		}

		#endregion

		#region IName Members

		public string Name
		{
			get
			{
				return Title;
			}
			set
			{
				Title = value;
			}
		}

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
				return "Article";
			}
		}

		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Article;
			}
		}

		#endregion

		#region IArchive Members

		public DateTime ArchiveDateTime
		{
			get
			{
				return this.EnabledDateTime;
			}
		}

		public string ArchiveHtml(bool showCountry, bool showPlace, bool showVenue, bool showEvent, int size)
		{

			string rolloverHtml = "<div style=\"width:250px;\"><b>"+this.Name+"</b><br>";
			if (this.Summary.Trim().Length>0)
				rolloverHtml += this.Summary+"<br>";
			rolloverHtml += "<b>By "+this.Owner.NickName+" - "+
				this.TotalComments+" comment"+(this.TotalComments==1?"":"s")+ " - " +
				this.Views+" view"+(this.Views==1?"":"s");
			rolloverHtml += "</b></div>";
			rolloverHtml = HttpUtility.UrlEncodeUnicode(rolloverHtml).Replace("'","\\'");
			string attribs = " onmouseover=\"stt('"+rolloverHtml+"');\" onmouseout=\"htm();\"";

			return "<a href=\""+this.Url()+"\"><img "+attribs+" src=\""+this.PicPath+"\" border=\"0\" class=\"ArchiveItem BorderBlack All\" width=\""+size.ToString()+"\" height=\""+size.ToString()+"\"></a>";
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
		Q IDiscussable.QueryConditionForGettingThreads
		{
			get
			{
				if (ThreadK != null && ThreadK.Value > 0)
				{
					return new And(
						Article.ThreadsQ(K),
						new Q(Thread.Columns.K, QueryOperator.NotEqualTo, ThreadK.Value));
				}
				else
				{
					return Article.ThreadsQ(K);
				}
			}
		}
		bool IDiscussable.ShowPrivateThreads { get { return true; } }
		IDiscussable IDiscussable.UsedDiscussable { get { return this; } }
		bool IDiscussable.OnlyShowThreads { get { return true; } }
		

		
	}
	#endregion

}
