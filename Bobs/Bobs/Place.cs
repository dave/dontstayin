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
using Bobs;
using Model.Entities.Properties;

namespace Bobs
{

	#region Place
	/// <summary>
	/// e.g. Southampton, London
	/// </summary>
	[Serializable]
	public partial class Place : IPic, IPage, IName, IReadableReference, IDiscussable, IBobType, ICalendar, IObjectPage, IHasArchive, IRelevanceContributor, IConnectedTo, ILinkable, IHasParent, IHasSinglePrimaryKey
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Place.Columns.K] as int? ?? 0; }
			set { this[Place.Columns.K] = value; }
		}
		/// <summary>
		/// Place name
		/// </summary>
		public override string Name
		{
			get
			{
				try
				{
					string abbr = "";
					if (this.RegionAbbreviation.Length > 0)
						abbr = " " + this.RegionAbbreviation;

					if (HttpContext.Current == null)
						return (string)this[Place.Columns.Name] + abbr + " (" + this.Country.FriendlyName + ")";
					else if (Bobs.Country.FilterK != this.CountryK)
						return (string)this[Place.Columns.Name] + abbr + " (" + this.Country.FriendlyName + ")";
					else
						return (string)this[Place.Columns.Name] + abbr;
				}
				catch
				{
					return (string)this[Place.Columns.Name];
				}
			}
			set { this[Place.Columns.Name] = value; }
		}

		public string NamePlainRegion
		{
			get
			{
				string abbr = "";
				if (this.RegionAbbreviation.Length > 0)
					abbr = " " + this.RegionAbbreviation;

				return (string)this[Place.Columns.Name] + abbr;
			}
		}
		public string NamePlain
		{
			get
			{
				return (string)this[Place.Columns.Name];
			}
		}
		/// <summary>
		/// Unique name i.e. Springfield TX
		/// </summary>
		public override string UniqueName
		{
			get { return (string)this[Place.Columns.UniqueName]; }
			set { this[Place.Columns.UniqueName] = value; }
		}
		/// <summary>
		/// Population of the place in 1000's
		/// </summary>
		public override double Population
		{
			get { return (double)this[Place.Columns.Population]; }
			set { this[Place.Columns.Population] = value; }
		}
		/// <summary>
		/// Latitude (degrees north)
		/// </summary>
		public override double LatitudeDegreesNorth
		{
			get { return (double)this[Place.Columns.LatitudeDegreesNorth]; }
			set { this[Place.Columns.LatitudeDegreesNorth] = value; }
		}
		/// <summary>
		/// Longitude (degrees west)
		/// </summary>
		public override double LongitudeDegreesWest
		{
			get { return (double)this[Place.Columns.LongitudeDegreesWest]; }
			set { this[Place.Columns.LongitudeDegreesWest] = value; }
		}
		/// <summary>
		/// Country (1=England, 2=Scotland, 3=Wales, 4=Northern Ireland)
		/// </summary>
		public override int SubCountry
		{
			get { return (int)this[Place.Columns.SubCountry]; }
			set { this[Place.Columns.SubCountry] = value; }
		}
		/// <summary>
		/// Link to the country table
		/// </summary>
		public override int CountryK
		{
			get { return (int)this[Place.Columns.CountryK]; }
			set { country = null; this[Place.Columns.CountryK] = value; }
		}
		/// <summary>
		/// Whether the place is displayed in the full place list.
		/// </summary>
		public override bool Enabled
		{
			get { return (bool)this[Place.Columns.Enabled]; }
			set { this[Place.Columns.Enabled] = value; }
		}
		/// <summary>
		/// Cropped image between 75*75 and 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Place.Columns.Pic]); }
			set { this[Place.Columns.Pic] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Details displayed on the place page
		/// </summary>
		public override string DetailsHtml
		{
			get { return (string)this[Place.Columns.DetailsHtml]; }
			set { this[Place.Columns.DetailsHtml] = value; }
		}
		/// <summary>
		/// The total number of events
		/// </summary>
		public override int TotalEvents
		{
			get { return (int)this[Place.Columns.TotalEvents]; }
			set { this[Place.Columns.TotalEvents] = value; }
		}
		/// <summary>
		/// The total number of comments
		/// </summary>
		public override int TotalComments
		{
			get { return (int)this[Place.Columns.TotalComments]; }
			set { this[Place.Columns.TotalComments] = value; }
		}
		/// <summary>
		/// The date/time of the last post that was posted in this board (including child objects)
		/// </summary>
		public override DateTime LastPost
		{
			get { return (DateTime)this[Place.Columns.LastPost]; }
			set { this[Place.Columns.LastPost] = value; }
		}
		/// <summary>
		/// The average date.time of all comments posted in this board (including child objects)
		/// </summary>
		public override DateTime AverageCommentDateTime
		{
			get { return (DateTime)this[Place.Columns.AverageCommentDateTime]; }
			set { this[Place.Columns.AverageCommentDateTime] = value; }
		}
		/// <summary>
		/// Appended to end of FriendlyName. Usually US State abbreviation.
		/// </summary>
		public override string RegionAbbreviation
		{
			get { return (string)this[Place.Columns.RegionAbbreviation]; }
			set { this[Place.Columns.RegionAbbreviation] = value; }
		}
		/// <summary>
		/// Link to Region table
		/// </summary>
		public override int RegionK
		{
			get { return (int)this[Place.Columns.RegionK]; }
			set { region = null; this[Place.Columns.RegionK] = value; }
		}
		/// <summary>
		/// Any regional place code (e.g. US FIPS code)
		/// </summary>
		public override string Code
		{
			get { return (string)this[Place.Columns.Code]; }
			set { this[Place.Columns.Code] = value; }
		}
		/// <summary>
		/// Place type - e.g. US CDP/Town/City
		/// </summary>
		public override string Type
		{
			get { return (string)this[Place.Columns.Type]; }
			set { this[Place.Columns.Type] = value; }
		}
		/// <summary>
		/// Is this the capital of the region?
		/// </summary>
		public override bool IsRegionCapital
		{
			get { return (bool)this[Place.Columns.IsRegionCapital]; }
			set { this[Place.Columns.IsRegionCapital] = value; }
		}
		/// <summary>
		/// Is this the capital of the country?
		/// </summary>
		public override bool IsCountryCapital
		{
			get { return (bool)this[Place.Columns.IsCountryCapital]; }
			set { this[Place.Columns.IsCountryCapital] = value; }
		}
		/// <summary>
		/// Leave this place off when drawing the map? (Usefull for outlying islands)
		/// </summary>
		public override bool ExcludeFromMap
		{
			get { return (bool)this[Place.Columns.ExcludeFromMap]; }
			set { this[Place.Columns.ExcludeFromMap] = value; }
		}
		/// <summary>
		/// Name used in the URL...
		/// </summary>
		public override string UrlName
		{
			get { return (string)this[Place.Columns.UrlName]; }
			set { this[Place.Columns.UrlName] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Place.Columns.PicState]; }
			set { this[Place.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Place.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Place.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Place.Columns.PicMiscK]; }
			set { picMisc = null; this[Place.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// The url fragment - so that the url can be generated without accessing parent database records
		/// </summary>
		public override string UrlFragment
		{
			get { return (string)this[Place.Columns.UrlFragment]; }
			set { this[Place.Columns.UrlFragment] = value; }
		}
		/// <summary>
		/// FeatureId in MeridianWorldData database
		/// </summary>
		public override int MeridianFeatureId
		{
			get { return (int)this[Columns.MeridianFeatureId]; }
			set { this[Columns.MeridianFeatureId] = value; }
		}
		/// <summary>
		/// Latitude
		/// </summary>
		public override double Lat
		{
			get { return (double) this[Columns.Lat] ; }
			set { this[Columns.Lat] = value; }
		}
		/// <summary>
		/// Longitude
		/// </summary>
		public override double Lon
		{
			get {  return (double)this[Columns.Lon] ; }
			set { this[Columns.Lon] = value; }
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

		public static ColumnSet NameColumns
		{
			get
			{
				return new ColumnSet(Place.Columns.Name, Place.Columns.RegionAbbreviation, Place.Columns.CountryK);
			}
		}

		#region Join
		public static Join BannerJoin
		{
			get
			{
				return new Join(new Join(Place.Columns.K,BannerPlace.Columns.PlaceK),Banner.Columns.K,BannerPlace.Columns.BannerK);
			}
		}
		public static Join UsrVisitJoin
		{
			get
			{
				return new Join(new Join(Place.Columns.K,UsrPlaceVisit.Columns.PlaceK),Usr.Columns.K,UsrPlaceVisit.Columns.UsrK);
			}
		}
		public static Join RegionCountryJoin
		{
			get
			{
				return 
					new Join(
						new Join(Place.Columns.RegionK,Region.Columns.K),
						Country.Columns.K,
						Place.Columns.CountryK
				);		
			}
		}
		#endregion

		public OrderBy NearestPlacesOrderBy
		{
			get
			{
				return new OrderBy("((("+this.LatitudeDegreesNorth.ToString()+" - [Place].["+Place.Columns.LatitudeDegreesNorth.ToString()+"]) * ("+this.LatitudeDegreesNorth.ToString()+" - [Place].["+Place.Columns.LatitudeDegreesNorth.ToString()+"])) + (("+this.LongitudeDegreesWest.ToString()+" - [Place].["+Place.Columns.LongitudeDegreesWest.ToString()+"]) * ("+this.LongitudeDegreesWest.ToString()+" - [Place].["+Place.Columns.LongitudeDegreesWest.ToString()+"])))");
			}
		}

		public string LastPostFriendlyTime(bool Capital)
		{
			return Cambro.Misc.Utility.FriendlyTime(LastPost,Capital);
		}

		#region FriendlyName
		public string FriendlyName
		{
			get
			{
				return Name;
			}
		}
		#endregion

		public static ColumnSet LinkColumns
		{
			get
			{
				return new ColumnSet(
					Place.Columns.K,
					Place.Columns.Name,
					Place.Columns.UrlName,
					Place.Columns.UrlFragment,
					Place.Columns.RegionAbbreviation);
			}
		}

		#region Url
		public void UpdateChildUrlFragments(bool Cascade)
		{
			Update uVenues = new Update();
			uVenues.Table=TablesEnum.Venue;
			uVenues.Changes.Add(new Assign(Venue.Columns.UrlFragment,UrlFilterPart));
			uVenues.Where=new Q(Venue.Columns.PlaceK,this.K);
			uVenues.Run();

			if (Cascade)
			{
				Query q = new Query();
				q.NoLock=true;
				q.QueryCondition=new Q(Venue.Columns.PlaceK,this.K);
				q.Columns=new ColumnSet(
					Venue.Columns.K, 
					Venue.Columns.UrlFragment, 
					Venue.Columns.PlaceK,
					Venue.Columns.UrlName);
				VenueSet vs = new VenueSet(q);
				foreach (Venue v in vs)
				{
					try
					{
						Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Venue, v.K, true);
						job.ExecuteAsynchronously();
					}
					catch(Exception ex)
					{
						if (Vars.DevEnv)
							throw ex;
					}
				}
			}

			Update uThreads = new Update();
			uThreads.Table=TablesEnum.Thread;
			uThreads.Changes.Add(new Assign(Thread.Columns.UrlFragment,UrlFilterPart));
			uThreads.Where=new And(
				new Q(Thread.Columns.ParentObjectType,Model.Entities.ObjectType.Place),
				new Q(Thread.Columns.ParentObjectK,this.K));
			uThreads.Run();

			Update uArticles = new Update();
			uArticles.Table=TablesEnum.Article;
			uArticles.Changes.Add(new Assign(Article.Columns.UrlFragment,UrlFilterPart));
			uArticles.Where=new And(
				new Q(Article.Columns.ParentObjectType,Model.Entities.ObjectType.Place),
				new Q(Article.Columns.ParentObjectK,this.K));
			uArticles.Run();

			if (Cascade)
			{
				Query q = new Query();
				q.NoLock=true;
				q.QueryCondition=new And(
					new Q(Article.Columns.ParentObjectType,Model.Entities.ObjectType.Place),
					new Q(Article.Columns.ParentObjectK,this.K));
				q.Columns=new ColumnSet(
					Article.Columns.K,
					Article.Columns.UrlFragment,
					Article.Columns.ParentObjectK,
					Article.Columns.ParentObjectType);
				ArticleSet aSet = new ArticleSet(q);
				foreach (Article a in aSet)
				{
					try
					{
						Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Article, a.K, true);
						job.ExecuteAsynchronously();
					}
					catch(Exception ex)
					{
						if (Vars.DevEnv)
							throw ex;
					}
				}
			}
		}

		public void UpdateUrlFragment()
		{
			if (!this.UrlFragment.Equals(this.Country.UrlName))
			{
				this.UrlFragment = this.Country.UrlName;
				this.Update();

				Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Place, this.K, true);
				job.ExecuteAsynchronously();
			}
		}

		public string UrlFilterPart
		{
			get
			{
				if (this.RegionAbbreviation.Length==0)
					return this.UrlFragment+"/"+this.UrlName;
				else
					return this.UrlFragment+"/"+this.RegionAbbreviation.ToLower()+"/"+this.UrlName;
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
		public string UrlAddVenue()
		{
			return "/pages/venues/edit/placek-" + this.K.ToString();
		}
		#endregion

		#region HasEvents
		public bool HasEvents
		{
			get
			{
				return this.TotalEvents>0;
			}
		}
		#endregion

		#region Pic
		public bool HasPic
		{
			get
			{
				return !Pic.Equals(Guid.Empty);
			}
		}
		public string PicPath{get{return Storage.Path(Pic);}}
		public string AnyPicPath
		{
			get
			{
				if (HasPic)
					return PicPath;
				else
					return "/gfx/dsi-sign-100.png";
			}
		}
		#endregion

		public string NameMapDropDown
		{
			get
			{
				if (this.TotalEvents>0)
					return this.NamePlainRegion+" ("+this.TotalEvents+" event"+(this.TotalEvents==1?"":"s")+")";
				else
					return this.NamePlainRegion;
			}
		}

		#region UrlCalendar
		public string UrlCalendarGeneric(string Application, int Year, int Month, int Day, int SkipDay, params string[] par)
		{
			DateTime month = new DateTime(Year, Month, 1);
			string dayString = Day == 0 ? "" : ("/" + Day.ToString("00"));
			string url = UrlInfo.MakeUrl(UrlFilterPart + "/" + Year + "/" + month.ToString("MMM").ToLower() + dayString, Application, par);
			string skip = "";
			if (SkipDay > 0)
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
			q.TableElement=Event.VenueJoin;
			q.QueryCondition=new And(new Q(Venue.Columns.PlaceK,this.K),DateTimeQ);
			EventSet es = new EventSet(q);
			if (es.Count==1)
				return es[0];
			else
				return null;
		}
		#endregion

		#region UrlDiscussion
		public string UrlDiscussion(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,"chat",par);
		}
		#endregion

		#region UpdateTotalComments()
		public void UpdateTotalComments(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition = new Q(Thread.Columns.PlaceK,this.K);
			q.ExtraSelectElements = ForumStats.ExtraSelectElements;
			q.Columns = new ColumnSet();
			ForumStats cs = new ForumStats(q);
			this.TotalComments=cs.TotalComments;
			this.AverageCommentDateTime=cs.AverageCommentDateTime;
			this.LastPost=cs.LastPost;

			Update(transaction);
		}
		#endregion

		#region UpdateTotalEvents(Transaction transaction)
		public void UpdateTotalEvents(Transaction transaction)
		{
			Query q = new Query();
			q.TableElement=Event.PlaceJoin;
			q.QueryCondition=new Q(Place.Columns.K,this.K);
			q.ReturnCountOnly=true;
			EventSet allEvents = new EventSet(q);
			this.TotalEvents = allEvents.Count;
			this.Update(transaction);

			this.Country.UpdateTotalEvents(transaction);
		}
		#endregion

		#region Left, Top, Size
		public double Left
		{
			get
			{
				return 360 - LongitudeDegreesWest*45.0 + 200.0 - Size/2.0;
			}
		}
		public double Top
		{
			get
			{
				return 450 - (LatitudeDegreesNorth-50)*60.0 + 200.0 - Size/2.0;
			}
		}
		public double Size
		{
			get
			{
				double a = Math.Ceiling(Population/50.0);
				if (a>30.0)
					return 30.0;
				else if (a<3)
					return 3.0;
				else
					return a;
			}
		}
		public double SizeExact
		{
			get
			{
				return Math.Ceiling(Population/100.0);
			}
		}
		#endregion

		#region Links

		public string LinkShort(int maxNameLength)
		{
            return Utilities.Link(Url(), Cambro.Misc.Utility.Snip(Name, maxNameLength), (Name.Length > maxNameLength ? " onmouseover=\"stt('" + HttpUtility.UrlEncodeUnicode(Name).Replace("'", "\\'") + "')\" onmouseout=\"htm();\"" : ""));
		}

		public string LinkShortNewWindow(int maxNameLength)
		{
            return Utilities.Link(Url(), Cambro.Misc.Utility.Snip(Name, maxNameLength), " target=\"_blank\"" + (Name.Length > maxNameLength ? " onmouseover=\"stt('" + HttpUtility.UrlEncodeUnicode(Name).Replace("'", "\\'") + "')\" onmouseout=\"htm();\"" : ""));
		}
		#endregion

		#region Links to BobSets
		#region AllEvents
		public EventSet AllEvents
		{
			get
			{
				if (allEvents==null)
					allEvents = new EventSet(
						new Query(
							Event.VenueJoin, 
							new Q(Bobs.Venue.Columns.PlaceK,K),
							new OrderBy(Bobs.Event.Columns.DateTime,OrderBy.OrderDirection.Descending),
							-1
						)
					);
				return allEvents;
			}
			set
			{
				allEvents=value;
			}
		}
		EventSet allEvents;
		#endregion
		#region Venues
		public VenueSet Venues
		{
			get
			{
				if (venues==null)
				{
					Query q = new Query();
					q.QueryCondition=new Q(Venue.Columns.PlaceK,K);
					q.OrderBy=new OrderBy(Venue.Columns.Name);
					venues = new VenueSet(q);
				}
				return venues;
			}
			set{venues=value;}
		}
		VenueSet venues;
		#endregion
		#region VenuesRandomOrder
		public VenueSet VenuesWithPicsRandomOrder
		{
			get
			{
				if (venuesRandomOrder==null)
					venuesRandomOrder = new VenueSet(
						new Query(
							new And(
								new Q(Venue.Columns.PlaceK,K),
								new Q(Venue.Columns.Pic,QueryOperator.NotEqualTo,Guid.Empty)
						)
						,
						new OrderBy(OrderBy.OrderDirection.Random)
					)
				);
				return venuesRandomOrder;
			}
			set{venuesRandomOrder=value;}
		}
		VenueSet venuesRandomOrder;
		#endregion
//		#region Threads
//		public ThreadSet Threads
//		{
//			get
//			{
//				if (threads==null)
//				{
//					#region removed
//					/*
//					#region placeQuery
//					string placeQuery=@"
//(
//  SELECT
//  Thread.*
//  FROM 
//  Thread
//    INNER JOIN Event ON Thread.ParentObjectK=Event.K AND Thread.ParentObjectType=2
//      INNER JOIN Venue ON Event.VenueK = Venue.K
//  WHERE 
//    Venue.PlaceK=$
//    AND
//    Thread.Enabled=1
//	AND
//	Thread.Private=0
//)
//UNION
//(
//  SELECT
//  Thread.*
//  FROM
//  Thread
//    INNER JOIN Photo ON Thread.ParentObjectK=Photo.K AND Thread.ParentObjectType=1
//      INNER JOIN Event ON Photo.EventK = Event.K
//        INNER JOIN Venue ON Event.VenueK = Venue.K
//  WHERE
//    Venue.PlaceK=$
//    AND
//    Thread.Enabled=1
//)
//UNION
//(
//  SELECT
//  Thread.*
//  FROM
//  Thread
//    INNER JOIN Venue ON Thread.ParentObjectK=Venue.K AND Thread.ParentObjectType=3
//  WHERE
//    Venue.PlaceK=$
//    AND
//    Thread.Enabled=1
//)
//UNION
//(
//  SELECT
//  Thread.*
//  FROM
//  Thread
//  WHERE
//    Thread.ParentObjectK=$
//    AND 
//    Thread.ParentObjectType=4
//    AND
//    Thread.Enabled=1
//)
//ORDER BY 
//Thread.LastPost 
//Desc
//";
//					placeQuery = placeQuery.Replace("$",this.K.ToString());
//					#endregion
//					threads = new ThreadSet ( new Query(true,placeQuery));
//					*/
//					#endregion
//					Query q = new Query();
//					q.QueryCondition = new Q(Thread.Columns.PlaceK,this.K);
//					q.OrderBy = Thread.Order;
//					threads = new ThreadSet(q);
//				}
//				return threads;
//			}
//			set
//			{
//				threads=value;
//			}
//		}
//		ThreadSet threads;
//		#endregion
		#region Admins
		public AdminSet Admins
		{
			get
			{
				if (admins==null)
					admins=new AdminSet(
						new Query(
							new And(
								new Q(Admin.Columns.ObjectType,Admin.AdminObjectType.Place),
								new Q(Admin.Columns.ObjectK,this.K)
							)
						)
					);
				return admins;
			}
		}
		AdminSet admins;
		#endregion
		#region News
		public ThreadSet NewsLast10
		{
			get
			{
				if (news==null)
				{
					Query q = new Query();
					q.QueryCondition=new And(
						new Q(Thread.Columns.IsNews,true),
						new Q(Thread.Columns.PlaceK,this.K),
						new Q(Thread.Columns.Private,false)
					);
					q.OrderBy=Thread.NewsOrder;
					q.TopRecords=10;
					newsLast10 = new ThreadSet(q);
				}
				return newsLast10;
			}
		}
		ThreadSet newsLast10;
		public ThreadSet News
		{
			get
			{
				if (news==null)
				{
					Query q = new Query();
					q.QueryCondition=new And(
						new Q(Thread.Columns.IsNews,true),
						new Q(Thread.Columns.PlaceK,this.K),
						new Q(Thread.Columns.Private,false)
						);
					q.OrderBy=Thread.NewsOrder;
					news = new ThreadSet(q);
				}
				return news;
			}
		}
		ThreadSet news;
		#endregion
		#endregion

		#region Links to Bobs
		public Bobs.Region Region
		{
			get
			{
				if (region==null && RegionK>0)
					region = new Region(RegionK);
				return region;
			}
			set
			{
				region = value;
			}
		}
		public Bobs.Region region;
		#endregion

		public bool IsCurrentUsrPlaceAdmin
		{
			get
			{
				if (Usr.Current==null)
					return false;
				else if (Usr.Current.IsAdmin)
					return true;
				else
				{
					AdminSet adminSet = new AdminSet(
						new Query(
							new And(
								new Q(Admin.Columns.UsrK,Usr.Current.K),
								new Q(Admin.Columns.ObjectType,Admin.AdminObjectType.Place),
								new Q(Admin.Columns.ObjectK,this.K)
							)
						)
					);
					return adminSet.Count!=0;
				}
			}
		}

		public Country Country
		{
			get
			{
				if (country==null)
					country=new Country(CountryK,this,Place.Columns.CountryK);
				return country;
			}
			set
			{
				country=value;
			}
		}
		Country country;
		
		#region IHasParent Members

		public Model.Entities.ObjectType ParentObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Country;
			}
			set
			{
				throw new Exception("Can't set this for Place type");
			}
		}

		public IBob ParentObject
		{
			get
			{
				return this.Country;
			}
		}

		public int ParentObjectK
		{
			get
			{
				return CountryK;
			}
			set
			{
				throw new Exception("Can't set this for Place type");
			}
		}

		#endregion

		#region IBobType Members

		public string TypeName
		{
			get
			{
				return "Place";
			}
		}
		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Place;
			}
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

		#region IRelevanceContributor Members

		public void AddRelevant(IRelevanceHolder ContainerPage)
		{
			ContainerPage.RelevantPlacesAdd(this.K);
		}

		#endregion

		#region IsConnectedTo(ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			if (objectType.Equals(Model.Entities.ObjectType.Place) && this.K == objectK)
				return true;

			if (objectType.Equals(Model.Entities.ObjectType.Country) && this.CountryK == objectK)
				return true;

			return false;
		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			if (o.Equals(Model.Entities.ObjectType.Country))
				return true;

			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Place.CanBeConnectedToStatic(o);
		}
		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion

		Q IDiscussable.QueryConditionForGettingThreads
		{
			get
			{
				return new Q(Thread.Columns.VenueK, K);
			}
		}
		bool IDiscussable.ShowPrivateThreads { get { return false; } }
		IDiscussable IDiscussable.UsedDiscussable { get { return this; } }
		bool IDiscussable.OnlyShowThreads { get { return false; } }

		public static PlaceSet GetTop(int countryK, int top)
		{
			Query qTop = new Query();
			qTop.Columns = new ColumnSet(Place.Columns.Name, Place.Columns.K);
			qTop.QueryCondition = new And(new Q(Place.Columns.CountryK, countryK), new Q(Place.Columns.Enabled, true));
			qTop.OrderBy = new OrderBy(Place.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
			qTop.TopRecords = top;
			return new PlaceSet(qTop);
		}

		public static PlaceSet GetAll(int countryK)
		{
			Query qAll = new Query();
			qAll.Columns = new ColumnSet(Place.Columns.Name, Place.Columns.K);
			qAll.OrderBy = new OrderBy(Place.Columns.Name);
			qAll.QueryCondition = new And(new Q(Place.Columns.CountryK, countryK), new Q(Place.Columns.Enabled, true));
			return new PlaceSet(qAll);
		}

	 
	}
	#endregion

}

