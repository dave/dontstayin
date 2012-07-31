using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using ArchiveObjectType = Model.Entities.ArchiveObjectType;

namespace Spotted.Pages
{
	public partial class ArchiveContent : System.Web.UI.UserControl
	{
		protected Spotted.Controls.Archive Arch;

		string Link(int Year, int Month, int Day, ArchiveObjectType Type)
		{
			if (ContainerPage.Url.ObjectFilterBob != null && ContainerPage.Url.ObjectFilterBob is IHasArchive)
				return ((IHasArchive)ContainerPage.Url.ObjectFilterBob).UrlArchiveDate(Year, Month, Day, Type, MixmagPar);
			else
				return Spotted.Controls.Archive.GetUrl(Year, Month, Day, Type, MixmagPar, "");
		}

		public ArchiveObjectType Type
		{
			get
			{
				if (ContainerPage.Url[0].Key.Equals("galleries"))
					return ArchiveObjectType.Gallery;
				else if (ContainerPage.Url[0].Key.Equals("news"))
					return ArchiveObjectType.News;
				else if (ContainerPage.Url[0].Key.Equals("reviews"))
					return ArchiveObjectType.Review;
				else if (ContainerPage.Url[0].Key.Equals("competitions"))
					return ArchiveObjectType.Comp;
				else if (ContainerPage.Url[0].Key.Equals("articles"))
					return ArchiveObjectType.Article;
				else if (ContainerPage.Url[0].Key.Equals("guestlists"))
					return ArchiveObjectType.Guestlist;
				else
					return ArchiveObjectType.News;
			}
		}

		public bool IsMixmagArchive
		{
			get
			{
				return Type == ArchiveObjectType.Article && ContainerPage.Url.Count > 1 && ContainerPage.Url[1].Key.Equals("mixmag");
			}
		}

		public string[] MixmagPar
		{
			get
			{
				if (IsMixmagArchive)
					return new string[] { "mixmag", "" };
				else
					return new string[] { };
			}
		}

		#region ObjectFilter
		public Q ObjectFilter
		{
			get
			{
				if (Type.Equals(ArchiveObjectType.Article))
				{
					Q objectQ = null;
					if (ContainerPage.Url.HasEventObjectFilter)
						objectQ = new Q(Article.Columns.EventK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasVenueObjectFilter)
						objectQ = new Q(Article.Columns.VenueK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						objectQ = new Q(Article.Columns.PlaceK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						objectQ = new Q(Article.Columns.PlaceK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasCountryObjectFilter)
						objectQ = new Q(Article.Columns.CountryK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						objectQ = new Q(EventBrand.Columns.BrandK, ContainerPage.Url.LogicalFilterBrandK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						objectQ = new Q(GroupEvent.Columns.GroupK, ContainerPage.Url.ObjectFilterK);

					if (IsMixmagArchive)
					{
						if (objectQ != null)
							return new And(objectQ, new Q(Article.Columns.IsMixmagNews, true));
						else
							return new Q(Article.Columns.IsMixmagNews, true);
					}
					else if (objectQ != null)
						return objectQ;
					else
						return new Q(true);

				}
				else if (Type.Equals(ArchiveObjectType.Gallery))
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						return new Q(Event.Columns.K, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasVenueObjectFilter)
						return new Q(Venue.Columns.K, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						return new Q(Place.Columns.K, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasCountryObjectFilter)
						return new Q(Country.Columns.K, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						return new Q(EventBrand.Columns.BrandK, ContainerPage.Url.LogicalFilterBrandK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return new Q(GroupEvent.Columns.GroupK, ContainerPage.Url.ObjectFilterK);
					else
						return new Q(true);
				}
				else if (Type.Equals(ArchiveObjectType.News))
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						return new Q(Thread.Columns.EventK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasVenueObjectFilter)
						return new Q(Thread.Columns.VenueK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						return new Q(Thread.Columns.PlaceK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasCountryObjectFilter)
						return new Q(Thread.Columns.CountryK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						return Group.ThreadsQ(ContainerPage.Url.LogicalFilterBrand.GroupK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return Group.ThreadsQWithLinkedEvents(ContainerPage.Url.ObjectFilterGroup);
					else
						return new Q(true);
				}
				else if (Type.Equals(ArchiveObjectType.Review))
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						return new Q(Thread.Columns.EventK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasVenueObjectFilter)
						return new Q(Thread.Columns.VenueK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						return new Q(Thread.Columns.PlaceK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasCountryObjectFilter)
						return new Q(Thread.Columns.CountryK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						return Brand.ThreadsQEvents(ContainerPage.Url.LogicalFilterBrand);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return Group.ThreadsQWithLinkedEvents(ContainerPage.Url.ObjectFilterGroup);
					else
						return new Q(true);
				}
				else if (Type.Equals(ArchiveObjectType.Guestlist))
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						return new Q(Event.Columns.K, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasVenueObjectFilter)
						return new Q(Event.Columns.VenueK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						return new Q(Venue.Columns.PlaceK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasCountryObjectFilter)
						return new Q(Place.Columns.CountryK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						return new Q(EventBrand.Columns.BrandK, ContainerPage.Url.LogicalFilterBrandK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return new Q(GroupEvent.Columns.GroupK, ContainerPage.Url.ObjectFilterK);
					else
						return new Q(true);
				}
				else if (Type.Equals(ArchiveObjectType.Comp))
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						return new Q(Comp.Columns.EventK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasVenueObjectFilter)
						return new Q(Event.Columns.VenueK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						return new Q(Venue.Columns.PlaceK, ContainerPage.Url.ObjectFilterK);
					else if (ContainerPage.Url.HasCountryObjectFilter)
						return new Or(
							new Q(Place.Columns.CountryK, ContainerPage.Url.ObjectFilterK),
							new Q(Comp.Columns.EventK, 0)
						);
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						return Brand.CompQ(ContainerPage.Url.LogicalFilterBrandK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return new Q(GroupEvent.Columns.GroupK, ContainerPage.Url.ObjectFilterK);
					else
						return new Q(true);
				}
				return new Q(true);
			}
		}
		#endregion

		#region GeneralFilter
		public Q GeneralFilter
		{
			get
			{
				if (Type.Equals(ArchiveObjectType.Gallery))
				{
					return new Q(Gallery.Columns.LivePhotos, QueryOperator.GreaterThan, 0);
				}
				else if (Type.Equals(ArchiveObjectType.Article))
				{
					return Article.EnabledQueryCondition;
				}
				else if (Type.Equals(ArchiveObjectType.News))
				{
					return new And(
						new Q(Thread.Columns.IsNews, true),
						new Q(Thread.Columns.Private, false),
						new Q(Thread.Columns.GroupPrivate, false),
						new Q(Thread.Columns.PrivateGroup, false));
				}
				else if (Type.Equals(ArchiveObjectType.Review))
				{
					return new And(
						new Q(Thread.Columns.IsReview, true),
						new Q(Thread.Columns.Private, false),
						new Q(Thread.Columns.GroupPrivate, false),
						new Q(Thread.Columns.PrivateGroup, false));
				}
				else if (Type.Equals(ArchiveObjectType.Guestlist))
				{
					return new And(
						new Q(Event.Columns.HasGuestlist, true),
						new Q(Event.Columns.GuestlistPromotion, true),
						new Q(Event.Columns.GuestlistLimit, QueryOperator.GreaterThan, Event.Columns.GuestlistCount, true),
						new Q(Event.Columns.GuestlistPrice, QueryOperator.LessThan, Event.Columns.GuestlistRegularPrice, true),
						new Q(Event.Columns.GuestlistOpen, true),
						new Q(Event.Columns.GuestlistFinished, false));
				}
				else if (Type.Equals(ArchiveObjectType.Comp))
				{
					return new And(
						new Q(Comp.Columns.Status, Comp.StatusEnum.Enabled),
						new Q(Comp.Columns.DateTimeStart, QueryOperator.LessThan, DateTime.Now));
				}
				return new Q(true);
			}
		}
		#endregion

		#region TableElement
		public TableElement TableElement
		{
			get
			{
				if (Type.Equals(ArchiveObjectType.Gallery))
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						return Gallery.EventVenueJoin;
					else if (ContainerPage.Url.HasVenueObjectFilter)
						return Gallery.EventVenueJoin;
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						return Gallery.EventVenuePlaceJoin;
					else if (ContainerPage.Url.HasCountryObjectFilter)
						return new Join(Gallery.EventVenuePlaceJoin, Country.Columns.K, Place.Columns.CountryK);
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						return new Join(Gallery.Columns.EventK, EventBrand.Columns.EventK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return new Join(Gallery.Columns.EventK, GroupEvent.Columns.EventK);
					else
						return new TableElement(TablesEnum.Gallery);
				}
				else if (Type.Equals(ArchiveObjectType.Article))
				{
					if (ContainerPage.Url.HasBrandLogicalFilter)
						return new Join(Article.Columns.EventK, EventBrand.Columns.EventK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return new Join(Article.Columns.EventK, GroupEvent.Columns.EventK);
					else
						return new TableElement(TablesEnum.Article);
				}
				else if (Type.Equals(ArchiveObjectType.News) || Type.Equals(ArchiveObjectType.Review))
				{
					if (ContainerPage.Url.HasBrandLogicalFilter)
						return Thread.EventBrandJoin;
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return Thread.EventGroupJoin;
					else
						return new TableElement(TablesEnum.Thread);
				}
				if (Type.Equals(ArchiveObjectType.Guestlist))
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						return new TableElement(TablesEnum.Event);
					else if (ContainerPage.Url.HasVenueObjectFilter)
						return new TableElement(TablesEnum.Event);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						return Event.VenueAllJoin;
					else if (ContainerPage.Url.HasCountryObjectFilter)
						return Event.PlaceAllJoin;
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						return new Join(Event.Columns.K, EventBrand.Columns.EventK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return new Join(Event.Columns.K, GroupEvent.Columns.EventK);
					else
						return new TableElement(TablesEnum.Event);
				}
				else if (Type.Equals(ArchiveObjectType.Comp))
				{
					if (ContainerPage.Url.HasEventObjectFilter)
						return new TableElement(TablesEnum.Comp);
					else if (ContainerPage.Url.HasVenueObjectFilter)
						return new Join(Comp.Columns.EventK, Event.Columns.K);
					else if (ContainerPage.Url.HasPlaceObjectFilter)
						return new Join(new Join(Comp.Columns.EventK, Event.Columns.K), new TableElement(TablesEnum.Venue), QueryJoinType.Inner, Event.Columns.VenueK, Venue.Columns.K);
					else if (ContainerPage.Url.HasCountryObjectFilter)
						return new Join(new Join(new JoinLeft(Comp.Columns.EventK, Event.Columns.K), new TableElement(TablesEnum.Venue), QueryJoinType.Left, Event.Columns.VenueK, Venue.Columns.K), new TableElement(TablesEnum.Place), QueryJoinType.Left, Venue.Columns.PlaceK, Place.Columns.K);
					else if (ContainerPage.Url.HasBrandLogicalFilter)
						return new Join(Comp.Columns.EventK, EventBrand.Columns.EventK);
					else if (ContainerPage.Url.HasGroupObjectFilter)
						return new Join(Comp.Columns.EventK, GroupEvent.Columns.EventK);
					else
						return new TableElement(TablesEnum.Comp);
				}
				else
					return new TableElement(TablesEnum.Article);


			}
		}
		#endregion

		#region DateColumn
		object DateColumn
		{
			get
			{
				if (Type.Equals(ArchiveObjectType.Gallery))
				{
					return Gallery.Columns.CreateDateTime;
				}
				else if (Type.Equals(ArchiveObjectType.Article))
				{
					return Article.Columns.EnabledDateTime;
				}
				else if (Type.Equals(ArchiveObjectType.News) || Type.Equals(ArchiveObjectType.Review))
				{
					return Thread.Columns.DateTime;
				}
				else if (Type.Equals(ArchiveObjectType.Guestlist))
				{
					return Event.Columns.DateTime;
				}
				else if (Type.Equals(ArchiveObjectType.Comp))
				{
					return Comp.Columns.DateTimeClose;
				}
				else
					return Article.Columns.EnabledDateTime;

			}
		}
		#endregion

		#region OrderBy
		public OrderBy OrderBy
		{
			get
			{
				if (Type.Equals(ArchiveObjectType.Comp))
				{
					return new OrderBy(new OrderBy(DateColumn), new OrderBy(Comp.Columns.PrizeValueRange, OrderBy.OrderDirection.Descending));
				}
				else
					return new OrderBy(DateColumn);
			}
		}
		#endregion

		protected bool ShowCountry
		{
			get
			{
				return !(ContainerPage.Url.HasCountryObjectFilter
					|| ContainerPage.Url.HasPlaceObjectFilter
					|| ContainerPage.Url.HasVenueObjectFilter
					|| ContainerPage.Url.HasEventObjectFilter);
			}
		}

		protected bool ShowPlace
		{
			get
			{
				return !(ContainerPage.Url.HasPlaceObjectFilter
					|| ContainerPage.Url.HasVenueObjectFilter
					|| ContainerPage.Url.HasEventObjectFilter);
			}
		}

		protected bool ShowVenue
		{
			get
			{
				return !(ContainerPage.Url.HasVenueObjectFilter || ContainerPage.Url.HasEventObjectFilter);
			}
		}
		protected bool ShowEvent
		{
			get
			{
				return !ContainerPage.Url.HasEventObjectFilter;
			}
		}


		public Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
			}
		}
		protected Label MonthNameLabel, MonthNameLabel1;
		protected HtmlAnchor BackLink, BackLink1, NextLink, NextLink1;
		protected HtmlGenericControl DayItemsP, ItemsHiddenP;
		protected Repeater DayRepeater;
		protected Spotted.CustomControls.h1 Header;
		protected HtmlGenericControl TitleSpan;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (this.Visible)
			{
				string name = "";
				if (Type.Equals(ArchiveObjectType.Gallery))
				{
					name = "Galleries";
					TitleSpan.InnerHtml = "galleries";
				}
				else if (Type.Equals(ArchiveObjectType.Article))
				{
					if (IsMixmagArchive)
					{
						name = "Mixmag articles";
						TitleSpan.InnerHtml = " <a href=\"/pages/mixmag\"><img src=\"/gfx/logo-mixmag-small.png\" border=\"0\" align=\"absmiddle\" width=\"100\" height=\"22\"></a> articles";
					}
					else
					{
						name = "Articles";
						TitleSpan.InnerHtml = "articles";
					}
				}
				else if (Type.Equals(ArchiveObjectType.Comp))
				{
					name = "Competitions";
					TitleSpan.InnerHtml = "competitions";
				}
				else if (Type.Equals(ArchiveObjectType.News))
				{
					name = "News";
					TitleSpan.InnerHtml = "news";
				}
				else if (Type.Equals(ArchiveObjectType.Review))
				{
					name = "Reviews";
					TitleSpan.InnerHtml = "reviews";
				}
				else if (Type.Equals(ArchiveObjectType.Guestlist))
				{
					name = "Guestlists";
					TitleSpan.InnerHtml = "guestlists";
				}

				name += " archive";
				TitleSpan.InnerHtml += " archive";

				if (ContainerPage.Url.HasObjectFilter && ContainerPage.Url.ObjectFilterBob is IHasArchive)
					name += " for " + ((IName)ContainerPage.Url.ObjectFilterBob).FriendlyName;

				if (ContainerPage.Url.HasCountryObjectFilter)
				{
					TitleSpan.InnerHtml += " for <a href=\"" + ContainerPage.Url.ObjectFilterCountry.Url() + "\">" + ContainerPage.Url.ObjectFilterCountry.FriendlyName + "</a>";
				}
				else if (ContainerPage.Url.HasPlaceObjectFilter)
				{
					TitleSpan.InnerHtml += " for <a href=\"" + ContainerPage.Url.ObjectFilterPlace.Url() + "\">" + ContainerPage.Url.ObjectFilterPlace.Name + "</a>";
				}
				else if (ContainerPage.Url.HasVenueObjectFilter)
				{
					TitleSpan.InnerHtml += " for <a href=\"" + ContainerPage.Url.ObjectFilterVenue.Url() + "\">" + ContainerPage.Url.ObjectFilterVenue.Name + "</a> in <a href=\"" + ContainerPage.Url.ObjectFilterVenue.Place.Url() + "\">" + ContainerPage.Url.ObjectFilterVenue.Place.Name + "</a>";
				}
				else if (ContainerPage.Url.HasObjectFilter && ContainerPage.Url.ObjectFilterBob is IPage && ContainerPage.Url.ObjectFilterBob is IName)
				{
					TitleSpan.InnerHtml += " for <a href=\"" + ((IPage)ContainerPage.Url.ObjectFilterBob).Url() + "\">" + ((IName)ContainerPage.Url.ObjectFilterBob).Name + "</a>";
				}

				name += " - " + ContainerPage.Url.DateFilter.ToString("MMMM") + " " + ContainerPage.Url.DateFilter.Year.ToString();

				Header.InnerText = name;
				ContainerPage.SetPageTitle(name);

				#region firstCellDate, lastCellDate
				DateTime firstOfMonth = new DateTime(ContainerPage.Url.DateFilter.Year, ContainerPage.Url.DateFilter.Month, 1);
				DateTime firstCellDate = firstOfMonth.AddDays(-(int)firstOfMonth.DayOfWeek + 1);
				if (firstOfMonth.DayOfWeek.Equals(DayOfWeek.Sunday))
					firstCellDate = firstOfMonth.AddDays(-6);

				DateTime lastOfMonth = firstOfMonth.AddDays(System.DateTime.DaysInMonth(ContainerPage.Url.DateFilter.Year, ContainerPage.Url.DateFilter.Month) - 1);
				int daysToAdd = 7 - (int)lastOfMonth.DayOfWeek;
				if (daysToAdd == 7)
					daysToAdd = 0;
				DateTime lastCellDate = lastOfMonth.AddDays(daysToAdd);
				if (lastOfMonth.DayOfWeek.Equals(DayOfWeek.Sunday))
					lastCellDate = lastOfMonth;
				#endregion

				Query queryAll = new Query();
				queryAll.QueryCondition = new And(
					ObjectFilter,
					GeneralFilter,
					new Q(DateColumn, QueryOperator.GreaterThanOrEqualTo, firstCellDate),
					new Q(DateColumn, QueryOperator.LessThan, lastCellDate.AddDays(1))
				);
				queryAll.OrderBy = OrderBy;
				queryAll.TableElement = TableElement;

				BobSet bs = null;
				if (Type.Equals(ArchiveObjectType.Gallery))
					bs = new GallerySet(queryAll);
				else if (Type.Equals(ArchiveObjectType.Article))
					bs = new ArticleSet(queryAll);
				else if (Type.Equals(ArchiveObjectType.Comp))
					bs = new CompSet(queryAll);
				else if (Type.Equals(ArchiveObjectType.News) || Type.Equals(ArchiveObjectType.Review))
					bs = new ThreadSet(queryAll);
				else if (Type.Equals(ArchiveObjectType.Guestlist))
					bs = new EventSet(queryAll);

				ItemsHiddenP.Visible = bs.Count > 150;

				if (ContainerPage.Url.HasDayFilter)
				{
					Query queryDay = new Query();
					queryDay.QueryCondition = new And(
						ObjectFilter,
						GeneralFilter,
						new Q(DateColumn, QueryOperator.GreaterThanOrEqualTo, ContainerPage.Url.DateFilter),
						new Q(DateColumn, QueryOperator.LessThan, ContainerPage.Url.DateFilter.AddDays(1))
					);
					queryDay.OrderBy = OrderBy;
					queryDay.TableElement = TableElement;

					if (Type.Equals(ArchiveObjectType.Gallery))
						DayRepeater.DataSource = new GallerySet(queryDay);
					else if (Type.Equals(ArchiveObjectType.Article))
						DayRepeater.DataSource = new ArticleSet(queryDay);
					else if (Type.Equals(ArchiveObjectType.Comp))
						DayRepeater.DataSource = new CompSet(queryDay);
					else if (Type.Equals(ArchiveObjectType.News) || Type.Equals(ArchiveObjectType.Review))
						DayRepeater.DataSource = new ThreadSet(queryDay);
					else if (Type.Equals(ArchiveObjectType.Guestlist))
						DayRepeater.DataSource = new EventSet(queryDay);

					DayRepeater.DataBind();
				}
				else
					DayItemsP.Visible = false;


				Arch.ShowCountry = ShowCountry;
				Arch.ShowPlace = ShowPlace;
				Arch.ShowVenue = ShowVenue;
				Arch.ShowEvent = ShowEvent;
				Arch.Objects = bs;
				Arch.Type = Type;
				Arch.Month = ContainerPage.Url.DateFilter.Month;
				Arch.StartDate = firstCellDate;
				Arch.EndDate = lastCellDate;

				MonthNameLabel.Text = firstOfMonth.ToString("MMMM") + " " + ContainerPage.Url.DateFilter.Year.ToString();
				MonthNameLabel1.Text = firstOfMonth.ToString("MMMM") + " " + ContainerPage.Url.DateFilter.Year.ToString();

				BackLink.InnerHtml = "&lt; " + firstOfMonth.AddDays(-1).ToString("MMMM");
				BackLink1.InnerHtml = "&lt; " + firstOfMonth.AddDays(-1).ToString("MMMM");
				BackLink.HRef = Link(firstOfMonth.AddDays(-1).Year, firstOfMonth.AddDays(-1).Month, 0, Type);
				BackLink1.HRef = Link(firstOfMonth.AddDays(-1).Year, firstOfMonth.AddDays(-1).Month, 0, Type);

				NextLink.InnerHtml = lastOfMonth.AddDays(1).ToString("MMMM") + " &gt;";
				NextLink1.InnerHtml = lastOfMonth.AddDays(1).ToString("MMMM") + " &gt;";
				NextLink.HRef = Link(lastOfMonth.AddDays(1).Year, lastOfMonth.AddDays(1).Month, 0, Type);
				NextLink1.HRef = Link(lastOfMonth.AddDays(1).Year, lastOfMonth.AddDays(1).Month, 0, Type);

				#region Set up back / next buttons
				if (bs.Count == 0)
				{
					#region moreFutureBs
					Query moreFutureQuery = new Query();
					moreFutureQuery.QueryCondition = new And(
						ObjectFilter,
						GeneralFilter,
						new Q(DateColumn, QueryOperator.GreaterThanOrEqualTo, new DateTime(lastOfMonth.AddDays(1).Year, lastOfMonth.AddDays(1).Month, 1))
						);
					moreFutureQuery.TopRecords = 1;
					moreFutureQuery.OrderBy = new OrderBy(DateColumn, OrderBy.OrderDirection.Ascending);
					moreFutureQuery.TableElement = TableElement;
					BobSet moreFutureBs = null;
					if (Type.Equals(ArchiveObjectType.Gallery))
						moreFutureBs = new GallerySet(moreFutureQuery);
					else if (Type.Equals(ArchiveObjectType.Article))
						moreFutureBs = new ArticleSet(moreFutureQuery);
					else if (Type.Equals(ArchiveObjectType.Comp))
						moreFutureBs = new CompSet(moreFutureQuery);
					else if (Type.Equals(ArchiveObjectType.News) || Type.Equals(ArchiveObjectType.Review))
						moreFutureBs = new ThreadSet(moreFutureQuery);
					else if (Type.Equals(ArchiveObjectType.Guestlist))
						moreFutureBs = new EventSet(moreFutureQuery);
					#endregion
					#region morePastBs
					Query morePastQuery = new Query();
					morePastQuery.QueryCondition = new And(
						ObjectFilter,
						GeneralFilter,
						new Q(DateColumn, QueryOperator.LessThan, new DateTime(ContainerPage.Url.DateFilter.Year, ContainerPage.Url.DateFilter.Month, 1))
					);
					morePastQuery.TopRecords = 1;
					morePastQuery.OrderBy = new OrderBy(DateColumn, OrderBy.OrderDirection.Descending);
					morePastQuery.TableElement = TableElement;
					BobSet morePastBs = null;
					if (Type.Equals(ArchiveObjectType.Gallery))
						morePastBs = new GallerySet(morePastQuery);
					else if (Type.Equals(ArchiveObjectType.Article))
						morePastBs = new ArticleSet(morePastQuery);
					else if (Type.Equals(ArchiveObjectType.Comp))
						morePastBs = new CompSet(morePastQuery);
					else if (Type.Equals(ArchiveObjectType.News) || Type.Equals(ArchiveObjectType.Review))
						morePastBs = new ThreadSet(morePastQuery);
					else if (Type.Equals(ArchiveObjectType.Guestlist))
						morePastBs = new EventSet(morePastQuery);
					#endregion

					if (morePastBs.Count == 0)
					{
						BackLink.HRef = "";
						BackLink1.HRef = "";
						BackLink.Disabled = true;
						BackLink1.Disabled = true;
					}
					else
					{
						IArchive latest = (IArchive)morePastBs.GetFromIndex(0);
						BackLink.HRef = Link(latest.ArchiveDateTime.Year, latest.ArchiveDateTime.Month, 0, Type);
						BackLink1.HRef = Link(latest.ArchiveDateTime.Year, latest.ArchiveDateTime.Month, 0, Type);
						BackLink.InnerHtml = "&lt; " + latest.ArchiveDateTime.ToString("MMMM");
						BackLink1.InnerHtml = "&lt; " + latest.ArchiveDateTime.ToString("MMMM");
						if (latest.ArchiveDateTime.Year != ContainerPage.Url.DateFilter.Year)
						{
							BackLink.InnerHtml = "&lt; " + latest.ArchiveDateTime.ToString("MMMM") + " " + latest.ArchiveDateTime.Year.ToString();
							BackLink1.InnerHtml = "&lt; " + latest.ArchiveDateTime.ToString("MMMM") + " " + latest.ArchiveDateTime.Year.ToString();
						}
					}

					if (moreFutureBs.Count == 0)
					{
						NextLink.HRef = "";
						NextLink1.HRef = "";
						NextLink.Disabled = true;
						NextLink1.Disabled = true;
					}
					else
					{
						IArchive first = (IArchive)moreFutureBs.GetFromIndex(0);
						NextLink.HRef = Link(first.ArchiveDateTime.Year, first.ArchiveDateTime.Month, 0, Type);
						NextLink1.HRef = Link(first.ArchiveDateTime.Year, first.ArchiveDateTime.Month, 0, Type);
						NextLink.InnerHtml = first.ArchiveDateTime.ToString("MMMM") + " &gt;";
						NextLink1.InnerHtml = first.ArchiveDateTime.ToString("MMMM") + " &gt;";
						if (first.ArchiveDateTime.Year != ContainerPage.Url.DateFilter.Year)
						{
							NextLink.InnerHtml = first.ArchiveDateTime.ToString("MMMM") + " " + first.ArchiveDateTime.Year.ToString() + " &gt;";
							NextLink1.InnerHtml = first.ArchiveDateTime.ToString("MMMM") + " " + first.ArchiveDateTime.Year.ToString() + " &gt;";
						}
					}
					if (BackLink.Disabled)
						BackLink.Attributes["class"] = "DisabledAnchor";
					if (BackLink1.Disabled)
						BackLink1.Attributes["class"] = "DisabledAnchor";
					if (NextLink.Disabled)
						NextLink.Attributes["class"] = "DisabledAnchor";
					if (NextLink1.Disabled)
						NextLink1.Attributes["class"] = "DisabledAnchor";
				}
				#endregion
			}
		}

	}
}
