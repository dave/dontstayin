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
using System.Collections.Generic;

namespace Bobs
{

	#region Country
	/// <summary>
	/// Country.
	/// </summary>
	[Serializable]
	public partial class Country : IDiscussable, IName, IReadableReference, IPage, ICalendar, IObjectPage, IHasArchive, IBobType, IConnectedTo, ILinkable, IHasSinglePrimaryKey
	{

		#region simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K 
		{ 
			get { return this[Country.Columns.K] as int? ?? 0; } 
			set { this[Country.Columns.K] = value; } 
		}
		/// <summary>
		/// Name of the country
		/// </summary>
		public override string Name 
		{ 
			get { return (string)this[Country.Columns.Name]; } 
			set { this[Country.Columns.Name] = value; } 
		}
		/// <summary>
		/// Region - BillingRegionEnum
		/// </summary>
		public override BillingRegionEnum Region 
		{ 
			get { return (BillingRegionEnum)this[Country.Columns.Region]; } 
			set { this[Country.Columns.Region] = value; } 
		}
		/// <summary>
		/// 2 letter country code transmitted to WorldPay during order
		/// </summary>
		public override string Code2Letter 
		{ 
			get { return (string)this[Country.Columns.Code2Letter]; } 
			set { this[Country.Columns.Code2Letter] = value; } 
		}
		/// <summary>
		/// 3 letter country code
		/// </summary>
		public override string Code3Letter 
		{
			get { return (string)this[Country.Columns.Code3Letter]; } 
			set { this[Country.Columns.Code3Letter] = value; } 
		}
		/// <summary>
		/// 3 number country code
		/// </summary>
		public override string Code3Number 
		{ 
			get { return (string)this[Country.Columns.Code3Number]; } 
			set { this[Country.Columns.Code3Number] = value; } 
		}
		/// <summary>
		/// Currency name (not currently used on site) - NULL if minor country
		/// </summary>
		public override string CurrencyName 
		{ 
			get { return (string)this[Country.Columns.CurrencyName]; } 
			set { this[Country.Columns.CurrencyName] = value; } 
		}
		/// <summary>
		/// Number of decimal places used in the currency (not currently used on site) - NULL if minor country
		/// </summary>
		public override int CurrencyDecimals 
		{ 
			get { return (int)this[Country.Columns.CurrencyDecimals]; } 
			set { this[Country.Columns.CurrencyDecimals] = value; } 
		}
		/// <summary>
		/// three letter currency code - NULL if minor country
		/// </summary>
		public override string CurrencyCode 
		{ 
			get { return (string)this[Country.Columns.CurrencyCode]; } 
			set { this[Country.Columns.CurrencyCode] = value; } 
		}
		/// <summary>
		/// The two letter prefix to the VAT codes
		/// </summary>
		public override string EuVatCodePrefix 
		{ 
			get { return (string)this[Country.Columns.EuVatCodePrefix]; } 
			set { this[Country.Columns.EuVatCodePrefix] = value; } 
		}
		/// <summary>
		/// Minimum place population to display in the top places list
		/// </summary>
		public override int PlacePopulationMinimum 
		{ 
			get { return (int)this[Country.Columns.PlacePopulationMinimum]; } 
			set { this[Country.Columns.PlacePopulationMinimum] = value; } 
		}
		/// <summary>
		/// Abbreviated name - e.g. United Kingdom = UK
		/// </summary>
		public override string FriendlyName 
		{ 
			get { return (string)this[Country.Columns.FriendlyName]; } 
			set { this[Country.Columns.FriendlyName] = value; } 
		}
		/// <summary>
		/// Postcode type for user sign-up, venue entry and spotter sign-up
		/// </summary>
		public override int PostcodeType 
		{ 
			get { return (int)this[Country.Columns.PostcodeType]; } 
			set { this[Country.Columns.PostcodeType] = value; } 
		}
		/// <summary>
		/// Does the country have loads of events?
		/// This restricts the past events on the FP to show only those with events etc.
		/// </summary>
		public override bool Mature 
		{ 
			get { return (bool)this[Country.Columns.Mature]; } 
			set { this[Country.Columns.Mature] = value; } 
		}
		/// <summary>
		/// Should the place names be followed by the region abbreviation? Should the All Places page have details about the Regions?
		/// </summary>
		public override bool UseRegion 
		{ 
			get { return (bool)this[Country.Columns.UseRegion]; } 
			set { this[Country.Columns.UseRegion] = value; } 
		}
		/// <summary>
		/// What does this country call its regions (e.g. "State" for the US).
		/// </summary>
		public override string RegionName 
		{ 
			get { return (string)this[Country.Columns.RegionName]; } 
			set { this[Country.Columns.RegionName] = value; } 
		}
		/// <summary>
		/// Is the country enabled on the site?
		/// </summary>
		public override bool Enabled 
		{ 
			get { return (bool)this[Country.Columns.Enabled]; } 
			set { this[Country.Columns.Enabled] = value; } 
		}
		/// <summary>
		/// Minimum events a place should have before being included on the place menu
		/// </summary>
		public override int MinEventsForPlaceMenu 
		{ 
			get { return (int)this[Country.Columns.MinEventsForPlaceMenu]; } 
			set { this[Country.Columns.MinEventsForPlaceMenu] = value; } 
		}
		/// <summary>
		/// International phone dialing prefix for this country
		/// </summary>
		public override int DialingCode 
		{ 
			get { return (int)this[Country.Columns.DialingCode]; } 
			set { this[Country.Columns.DialingCode] = value; } 
		}
		/// <summary>
		/// Total events in this country
		/// </summary>
		public override int TotalEvents 
		{ 
			get { return (int)this[Country.Columns.TotalEvents]; } 
			set { this[Country.Columns.TotalEvents] = value; } 
		}
		/// <summary>
		/// Name used in url's
		/// </summary>
		public override string UrlName 
		{ 
			get { return (string)this[Country.Columns.UrlName]; } 
			set { this[Country.Columns.UrlName] = value; } 
		}
		/// <summary>
		/// Custom Html shown on the country page.
		/// </summary>
		public override string CustomHtml 
		{ 
			get { return (string)this[Country.Columns.CustomHtml]; } 
			set { this[Country.Columns.CustomHtml] = value; } 
		}
		/// <summary>
		/// Royal mail postage zone
		/// </summary>
		public override PostageZones PostageZone
		{
			get { return (PostageZones)this[Country.Columns.PostageZone]; }
			set { this[Country.Columns.PostageZone] = value; }
		}
		#endregion

		#region GetCapitalOrLargestEnabledPlace
		public Place GetCapitalOrLargestEnabledPlace()
		{
			PlaceSet psCapital = new PlaceSet(new Query(new Q(Place.Columns.CountryK, K), new Q(Place.Columns.IsCountryCapital, true), new Q(Place.Columns.Enabled, true)));
			if (psCapital.Count > 0)
			{
				return psCapital[0];
			}
			else
			{
				PlaceSet psPopulation = new PlaceSet(new Query(new Q(Place.Columns.CountryK, K), new OrderBy(Place.Columns.Population, OrderBy.OrderDirection.Descending), 1));
				if (psPopulation.Count > 0)
				{
					return psPopulation[0];
				}
				else
				{
					return null;
				}
			}
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

		#region IsConnectedTo(ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			return objectType.Equals(Model.Entities.ObjectType.Country) && this.K == objectK;
		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Country.CanBeConnectedToStatic(o);
		}
		#endregion


		#region UrlForeignGalleries
		public string UrlForeignGalleries(params string[] par)
		{
			return UrlInfo.MakeUrl(this.UrlFilterPart, "foreigngalleries", par);
		}
		#endregion
		#region UrlForeignGalleriesDate
		public string UrlForeignGalleriesDate(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}/{3}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower(), d.Day.ToString("00")), "foreigngalleries", par);
		}
		#endregion
		#region UrlForeignGalleriesMonth
		public string UrlForeignGalleriesMonth(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower()), "foreigngalleries", par);
		}
		#endregion

		#region LinkColumns
		public static ColumnSet LinkColumns
		{
			get
			{
				return new ColumnSet(
					Country.Columns.K,
					Country.Columns.Name,
					Country.Columns.FriendlyName, 
					Country.Columns.UrlName);
			}
		}
		#endregion

		#region UpdateTotalEvents(Transaction transaction)
		public void UpdateTotalEvents(Transaction transaction)
		{
			Query q = new Query();
			q.TableElement=Event.PlaceJoin;
			q.QueryCondition=new Q(Place.Columns.CountryK,this.K);
			q.ReturnCountOnly=true;
			EventSet allEvents = new EventSet(q);
			this.TotalEvents = allEvents.Count;
			this.Update(transaction);
		}
		#endregion
		
		#region BobSets
		#region Regions
		public RegionSet Regions
		{
			get
			{
				if (regions==null)
				{
					regions = new RegionSet(new Query(new Q(Bobs.Region.Columns.CountryK, this.K),new OrderBy(Bobs.Region.Columns.Name)));
				}
				return regions;
			}
			set
			{
				regions = value;
			}
		}
		private RegionSet regions;
		#endregion
		#endregion

		#region Countries
		public static CountrySet Countries(params int[] countryKs)
		{
			Query countryQuery = new Query();
			countryQuery.Columns = new ColumnSet(Country.Columns.Name, Country.Columns.K);
			if (countryKs != null && countryKs.Length > 0)
			{
				countryQuery.QueryCondition = new Q(Country.Columns.K, countryKs);
			}
			countryQuery.CacheDuration = TimeSpan.FromDays(1);
			return new CountrySet(countryQuery);
		}

		#endregion

		#region Url
		public void UpdateChildUrlFragments(bool Cascade)
		{
			Update uPlaces = new Update();
			uPlaces.Table=TablesEnum.Place;
			uPlaces.Changes.Add(new Assign(Place.Columns.UrlFragment,UrlFilterPart));
			uPlaces.Where=new Q(Place.Columns.CountryK,this.K);
			uPlaces.Run();

			if (Cascade)
			{
				Query q = new Query();
				q.NoLock=true;
				q.QueryCondition=new And(new Q(Place.Columns.CountryK,this.K),new Q(Place.Columns.Enabled,true));
				q.Columns=new ColumnSet(
					Place.Columns.K, 
					Place.Columns.UrlFragment, 
					Place.Columns.UrlName, 
					Place.Columns.RegionAbbreviation);
				PlaceSet ps = new PlaceSet(q);
				foreach (Place p in ps)
				{
					try
					{
						Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Place, p.K, true);
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
				new Q(Thread.Columns.ParentObjectType,Model.Entities.ObjectType.Country),
				new Q(Thread.Columns.ParentObjectK,this.K));
			uThreads.Run();

			Update uArticles = new Update();
			uArticles.Table=TablesEnum.Article;
			uArticles.Changes.Add(new Assign(Article.Columns.UrlFragment,UrlFilterPart));
			uArticles.Where=new And(
				new Q(Article.Columns.ParentObjectType,Model.Entities.ObjectType.Country),
				new Q(Article.Columns.ParentObjectK,this.K));
			uArticles.Run();

			if (Cascade)
			{
				Query q = new Query();
				q.NoLock=true;
				q.QueryCondition=new And(
					new Q(Article.Columns.ParentObjectType,Model.Entities.ObjectType.Country),
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
		public string UrlFragment
		{
			get
			{
				return "";
			}
		}
		public string UrlFilterPart
		{
			get
			{
				return this.UrlName;
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

		#region UrlHotTopics
		public string UrlHotTopics(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] {this.UrlName,""}, par);
			return UrlInfo.PageUrl("hotforums",fullParams);
		}
		#endregion

		#region FlagUrl
		public string FlagUrl()
		{
			return "/gfx/flags1/tn_"+this.Code2Letter.ToLower()+".gif";
		}
		#endregion

		//#region SmallFlagUrl
		//public string SmallFlagUrl()
		//{
		//    return "/gfx/flags/f0-"+this.Code2Letter.ToLower()+".gif";
		//}
		//#endregion

		#region UrlCalendar
		/// <summary>
		/// Set day to 0 for default
		/// </summary>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <param name="Day"></param>
		/// <param name="par"></param>
		/// <returns></returns>
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
			q.TableElement=Event.PlaceJoin;
			q.QueryCondition=new And(new Q(Place.Columns.CountryK,this.K),DateTimeQ);
			EventSet es = new EventSet(q);
			if (es.Count==1)
				return es[0];
			else
				return null;
		}
		#endregion

		#region Country Place Filter
		public static int FilterK
		{
			get
			{
				int val = 224;

				if (HttpContext.Current.Request.Cookies["CountryFilter"] != null)
				{
					try
					{
						val = int.Parse(HttpContext.Current.Request.Cookies["CountryFilter"].Value);
					}
					catch 
					{
						val = IpCountry.ClientCountryK();
						Cambro.Web.Helpers.SetCookie("CountryFilter", val.ToString(), true);
					}
				}
				else
				{
					val = IpCountry.ClientCountryK();
					Cambro.Web.Helpers.SetCookie("CountryFilter", val.ToString(), true);
				}
				return val;
			}
			set
			{
				Cambro.Web.Helpers.SetCookie("CountryFilter", value.ToString(), true);
			}
		}

		public static int ClientCountryK
		{
			get
			{
				if (HttpContext.Current.Items["GetClientCountryK"] == null)
				{
					try
					{
						HttpContext.Current.Items["GetClientCountryK"] = IpCountry.ClientCountryK();
					}
					catch { }
				}

				try
				{
					return (int)HttpContext.Current.Items["GetClientCountryK"];
				}
				catch
				{
					return 0;
				}
			}
		}

		public static Q PlaceFilterQ
		{
			get
			{
				if (FilterK==0)
					return new Q(true);
				else
					return new Q(Place.Columns.CountryK,FilterK);
			}
		}
		public static string PlaceFilterSqlString
		{
			get
			{
				if (FilterK==0)
					return " (1=1) ";
				else
					return " (Place.CountryK=" + FilterK + ") ";
			}
		}
		public static Q ThreadFilterQ
		{
			get
			{
				if (FilterK==0)
					return new Q(true);
				else
					return new Q(Thread.Columns.CountryK,FilterK);
			}
		}
		#endregion

		#region IsCurrentUsrAdmin
		public bool IsCurrentUsrAdmin
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
							new Q(Admin.Columns.ObjectType,Admin.AdminObjectType.Country),
							new Q(Admin.Columns.ObjectK,this.K)
							)
						)
					);
					return adminSet.Count!=0;
				}
			}
		}
		#endregion

		#region IDiscuss Members

		public int TotalComments
		{
			get
			{
				return 0;
			}
			set
			{
				
			}
		}
		Q IDiscussable.QueryConditionForGettingThreads
		{
			get
			{
				return Country.ThreadsQ(K);
			}
		}
		#region UrlDiscussion
		public string UrlDiscussion(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,"chat",par);
		}
		#endregion

		public void UpdateTotalComments(Transaction transaction)
		{
			// TODO:  Add Country.UpdateTotalComments implementation
		}

		public static Q ThreadsQ(int CountryK)
		{
			return new Q(Thread.Columns.CountryK,CountryK);
		}

		#endregion

		#region Current
		public static Country Current
		{
			get
			{
				if (HttpContext.Current==null)
					return null;
				if (HttpContext.Current.Items["Country_Current"]!=null)
					return (Country)HttpContext.Current.Items["Country_Current"];
				try
				{
					if (HttpContext.Current.Items["Country_Current"]==null)
					{
						if (FilterK==0)
							HttpContext.Current.Items["Country_Current"] = new Country(224);
						else
							HttpContext.Current.Items["Country_Current"] = new Country(FilterK);
					}
					return (Country)HttpContext.Current.Items["Country_Current"];
				}
				catch
				{
					return null;
				}
			}
			set
			{
				HttpContext.Current.Items.Remove("Country_Current");
			}
		}
		#endregion
		
		#region BillingRegionEnum
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

		#region IBobType Members

		public string TypeName
		{
			get
			{
				return "Country";
			}
		}
		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Country;
			}
		}
		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion

		bool IDiscussable.ShowPrivateThreads { get { return false; } }
		IDiscussable IDiscussable.UsedDiscussable { get { return this; } }
		bool IDiscussable.OnlyShowThreads { get { return false; } }

		public static CountrySet GetTop(int top)
		{
			Query qTop = new Query();
			qTop.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
			qTop.OrderBy = new OrderBy(Country.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
			qTop.QueryCondition = new Q(Country.Columns.Enabled, true);
			qTop.TopRecords = top;
			return new CountrySet(qTop);
		}

		public static CountrySet GetAll()
		{
			Query qAll = new Query();
			qAll.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
			qAll.OrderBy = new OrderBy(Country.Columns.FriendlyName);
			qAll.QueryCondition = new Q(Country.Columns.Enabled, true);
			return new CountrySet(qAll);
		}
	}
	#endregion

}
