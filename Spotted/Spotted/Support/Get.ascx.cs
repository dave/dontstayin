using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Collections;

namespace Spotted.Support
{
	public partial class Get : System.Web.UI.UserControl
	{
		int maxLength = 40;
		protected void Page_Load(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
		//	if (Vars.DevEnv)
		//		System.Threading.Thread.Sleep(new Random().Next(2000));

			if (Request.QueryString["type"] == "calendar")
			{
				bool freeGuestlist = Request.QueryString["freeGuestlist"] == null || Request.QueryString["freeGuestlist"].Length == 0 || Request.QueryString["freeGuestlist"] == "0" ? false : true;
				Brand brand = Request.QueryString["brandk"] == null || Request.QueryString["brandk"].Length == 0 || Request.QueryString["brandk"] == "0" ? null : new Brand(int.Parse(Request.QueryString["brandk"]));
				Place place = Request.QueryString["placek"] == null || Request.QueryString["placek"].Length == 0 || Request.QueryString["placek"] == "0" ? null : new Place(int.Parse(Request.QueryString["placek"]));
				Venue venue = Request.QueryString["venuek"] == null || Request.QueryString["venuek"].Length == 0 || Request.QueryString["venuek"] == "0" || Request.QueryString["venuek"] == "1" ? null : new Venue(int.Parse(Request.QueryString["venuek"]));
				int key = Request.QueryString["key"] == null || Request.QueryString["key"].Length == 0 || Request.QueryString["key"] == "0" ? 0 : int.Parse(Request.QueryString["key"]);
				MusicType music = Request.QueryString["musictypek"] == null || Request.QueryString["musictypek"].Length == 0 || Request.QueryString["musictypek"] == "0" ? null : new MusicType(int.Parse(Request.QueryString["musictypek"]));
				bool me = Request.QueryString["me"] != null && Request.QueryString["me"] == "1";
				bool addGalleryButton = Request.QueryString["addgallery"] != null && Request.QueryString["addgallery"] == "1";
				bool allVenues = Request.QueryString["venuek"] != null && Request.QueryString["venuek"] == "1";
				DateTime date = new DateTime(
					int.Parse(Request.QueryString["date"].Substring(0, 4)),
					int.Parse(Request.QueryString["date"].Substring(4, 2)),
					int.Parse(Request.QueryString["date"].Substring(6, 2)) > 0 ? int.Parse(Request.QueryString["date"].Substring(6, 2)) : 1 );
				//if (date == DateTime.Today)
				//	System.Threading.Thread.Sleep(1000);
				DateTime from = date.Previous(DayOfWeek.Monday, true);
				DateTime to = date.Next(DayOfWeek.Sunday, true);
				Event.EventsForDisplay events = new Event.EventsForDisplay();
				events.IgnoreMusicType = true;

				if (me)
				{
					events.AttendedUsrK = Usr.Current.K;
				}
				else if (brand != null)
				{
					events.BrandK = brand.K;
				}
				else if (venue != null)
				{
					events.VenueK = venue.K;
				}
				else if (place != null && music != null)
				{
					events.PlaceK = place.K;
					events.MusicTypeK = music.K;
				}
				else if (place != null && freeGuestlist)
				{
					events.PlaceK = place.K;
					events.FreeGuestlist = freeGuestlist;
				}
				else if (key > 0)
				{

				}
				else
					throw new Exception();

				

				EventSet es;
				if (key == 0)
					es = events.GetEventsBetweenDates(from, to);
				else
					es = new EventSet(new Query(new Q(Event.Columns.K, key)));

				CustomControls.DsiCalendar calendar = new Spotted.CustomControls.DsiCalendar();

				calendar.AllEvents = es;
				calendar.Month = date.Month;
				
				calendar.ShowCountryFriendlyName = !(events.FilterByCountry || events.FilterByPlace || events.FilterByVenue);
				calendar.ShowPlace = !(events.FilterByPlace || events.FilterByVenue);
				calendar.ShowVenue = !events.FilterByVenue;
				calendar.ShowAddGalleryButton = addGalleryButton;

				calendar.Tickets = true;
				calendar.StartDate = from;
				calendar.EndDate = to;

				Out.Controls.Add(calendar);
				
			}
			else
			{
				sb.AppendLine("{");
				if (Request.QueryString["type"] == "music")
				{
					#region Music types

					Query q = new Query();
					q.QueryCondition = new Q(MusicType.Columns.K, QueryOperator.NotEqualTo, 1);
					q.Columns = new ColumnSet(MusicType.Columns.Name, MusicType.Columns.ParentK, MusicType.Columns.K);
					q.OrderBy = new OrderBy(MusicType.Columns.Order, OrderBy.OrderDirection.Ascending);
					q.CacheDuration = TimeSpan.FromDays(1);
					MusicTypeSet mts = new MusicTypeSet(q);
					append(sb, "Select your music...", "0");
					append(sb, "", "");
					foreach (MusicType mt in mts)
					{
						append(sb, (mt.ParentK == 1 ? "" : "... ") + mt.Name, mt.K.ToString());
					}

					#endregion
				}
				else if (Request.QueryString["type"] == "country")
				{
					#region Countries

					append(sb, "Select a country...", "0");
					Query qTop = new Query();
					qTop.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
					qTop.OrderBy = new OrderBy(Country.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
					qTop.QueryCondition = new Q(Country.Columns.Enabled, true);
					qTop.TopRecords = 10;
					qTop.CacheDuration = TimeSpan.FromDays(1);
					CountrySet csTop = new CountrySet(qTop);
					append(sb, "", "");
					append(sb, "--- TOP COUNTRIES ---", "0");
					foreach (Country c in csTop)
					{
						append(sb, c.FriendlyName.TruncateWithDots(maxLength), c.K.ToString());
					}
					Query qAll = new Query();
					qAll.Columns = new ColumnSet(Country.Columns.FriendlyName, Country.Columns.K);
					qAll.OrderBy = new OrderBy(Country.Columns.FriendlyName);
					qAll.QueryCondition = new And(new Q(Country.Columns.Enabled, true), new StringQueryCondition("(SELECT COUNT(*) FROM [Place] WHERE [Place].[Enabled] = 1 AND [Place].[CountryK] = [Country].[K]) > 0"));
					qAll.CacheDuration = TimeSpan.FromDays(1);
					CountrySet csAll = new CountrySet(qAll);
					append(sb, "", "");
					append(sb, "--- ALL COUNTRIES ---", "0");
					foreach (Country c in csAll)
					{
						append(sb, c.FriendlyName.TruncateWithDots(maxLength), c.K.ToString());
					}

					#endregion
				}
				else if (Request.QueryString["type"] == "place")
				{
					#region Places

					int countryK = int.Parse(Request.QueryString["countryk"]);
					Country country = new Country(countryK);

					Query qTop = new Query();
					qTop.Columns = new ColumnSet(Place.Columns.Name, Place.Columns.K, Place.LinkColumns);
					qTop.TopRecords = 10;
					qTop.QueryCondition = new And(new Q(Place.Columns.CountryK, country.K), new Q(Place.Columns.Enabled, true));
					qTop.OrderBy = new OrderBy(Place.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
					PlaceSet psTop = new PlaceSet(qTop);
					if (psTop.Count == 0)
					{
						append(sb, "No towns in our database for this country", "");
					}
					else
					{
						append(sb, "Towns in " + country.FriendlyName.Truncate(maxLength) + "...", "");
						append(sb, "", "");
						if (psTop.Count < 10)
						{
							foreach (Place p in psTop)
								append(sb, p.NamePlainRegion.TruncateWithDots(maxLength), Request.QueryString["return"] == "k" ? p.K.ToString() : p.Url());
						}
						else
						{
							append(sb, "--- TOP TOWNS ---", "");

							foreach (Place p in psTop)
								append(sb, p.NamePlainRegion.TruncateWithDots(maxLength), Request.QueryString["return"] == "k" ? p.K.ToString() : p.Url());

							Query qAll = new Query();
							qAll.Columns = new ColumnSet(Place.Columns.Name, Place.Columns.K, Place.LinkColumns);
							qAll.OrderBy = new OrderBy(Place.Columns.UrlName);
							qAll.QueryCondition = new And(new Q(Place.Columns.CountryK, countryK), new Q(Place.Columns.Enabled, true));
							PlaceSet psAll = new PlaceSet(qAll);
							append(sb, "", "");
							append(sb, "--- ALL TOWNS ---", "");

							foreach (Place p in psAll)
								append(sb, p.NamePlainRegion.TruncateWithDots(maxLength), Request.QueryString["return"] == "k" ? p.K.ToString() : p.Url());

						}
					}
					#endregion
				}
				else if (Request.QueryString["type"] == "venue")
				{
					#region Venues

					int placeK = int.Parse(Request.QueryString["placek"]);
					Place place = new Place(placeK);

					Query qTop = new Query();
					qTop.Columns = new ColumnSet(Venue.Columns.Name, Venue.Columns.K, Venue.LinkColumns);
					qTop.TopRecords = 10;
					qTop.QueryCondition = new Q(Venue.Columns.PlaceK, place.K);
					qTop.OrderBy = new OrderBy(Venue.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
					VenueSet vsTop = new VenueSet(qTop);
					if (vsTop.Count == 0)
					{
						append(sb, "No venues in our database for this town", "");
					}
					else
					{
						append(sb, "Venues in " + place.NamePlainRegion.Truncate(maxLength) + "...", "");
						append(sb, "", "");
						if (Request.QueryString["all"] == "1")
						{
							append(sb, "All venues", "1");
							append(sb, "", "");
						}
						if (vsTop.Count < 10)
						{
							appendVenues(sb, vsTop);
						}
						else
						{
							append(sb, "--- TOP VENUES ---", "");

							appendVenues(sb, vsTop);

							Query qAll = new Query();
							qAll.Columns = new ColumnSet(Venue.Columns.Name, Venue.Columns.K, Venue.LinkColumns);
							qAll.OrderBy = new OrderBy("( CASE WHEN [Venue].[UrlName] LIKE 'the-%' THEN SUBSTRING([Venue].[UrlName], 4, LEN([Venue].[UrlName]) - 4) ELSE [Venue].[UrlName] END )");
							qAll.QueryCondition = new Q(Venue.Columns.PlaceK, placeK);
							VenueSet vsAll = new VenueSet(qAll);
							append(sb, "", "");
							append(sb, "--- ALL VENUES ---", "");

							if (vsAll.Count <= 300)
							{
								appendVenues(sb, vsAll);

							}
							else
							{
								append(sb, "Select the first letter:", "");
								append(sb, "", "");
								append(sb, "0-9", "*0");

								string ch;
								for (int i = 65; i <= 90; i++)
								{
									ch = char.ConvertFromUtf32(i);
									append(sb, ch.ToUpper() + "...", "*" + ch.ToLower());

								}
							}
						}
					}
					#endregion
				}
				else if (Request.QueryString["type"] == "venuebyletter")
				{
					#region Venues

					int placeK = int.Parse(Request.QueryString["placek"]);
					string letter = Request.QueryString["letter"];
					if (letter.Length > 1)
						throw new Exception();
					Place place = new Place(placeK);

					string qu = "";
					if (letter.ToLower() == "0")
					{
						qu = "([Venue].[UrlName] LIKE '[0-9]%' OR [Venue].[UrlName] LIKE 'the-[0-9]%')";
					}
					else if (letter.ToLower() == "t")
					{
						qu = "(([Venue].[UrlName] LIKE 't%' AND [Venue].[UrlName] NOT LIKE 'the-%' ) OR [Venue].[UrlName] LIKE 'the-t%')";
					}
					else
					{
						qu = "([Venue].[UrlName] LIKE '" + letter.ToLower() + "%' OR [Venue].[UrlName] LIKE 'the-" + letter.ToLower() + "%')";
					}
					Query q = new Query();
					q.Columns = new ColumnSet(Venue.Columns.Name, Venue.Columns.K, Venue.LinkColumns);
					//q.OrderBy = new OrderBy(Venue.Columns.UrlName);
					q.OrderBy = new OrderBy("( CASE WHEN [Venue].[UrlName] LIKE 'the-%' THEN SUBSTRING([Venue].[UrlName], 4, LEN([Venue].[UrlName]) - 4) ELSE [Venue].[UrlName] END )");
					q.QueryCondition = new And(
						new Q(Venue.Columns.PlaceK, placeK),
						new StringQueryCondition(qu));
					VenueSet vs = new VenueSet(q);


					if (vs.Count == 0)
					{
						append(sb, "No venues starting with " + letter.ToUpper(), "");
					}
					else
					{
						append(sb, "Venues starting with " + letter.ToUpper() + "...", "");
						append(sb, "", "");

						appendVenues(sb, vs);
					}
					#endregion
				}
				else if (Request.QueryString["type"] == "event")
				{
					#region Events

					int venueK = int.Parse(Request.QueryString["venuek"]);
					int brandK = int.Parse(Request.QueryString["brandk"]);
					int key = int.Parse(Request.QueryString["key"]);
					int year = int.Parse(Request.QueryString["date"].Substring(0, 4));
					int month = int.Parse(Request.QueryString["date"].Substring(4, 2));
					DateTime dateFrom = new DateTime(year, month, 1);
					DateTime dateTo = dateFrom.AddMonths(1);
					Venue venue = venueK > 1 ? new Venue(venueK) : null;
					Brand brand = brandK > 0 ? new Brand(brandK) : null;

					EventSet es;
					if (key == 0)
					{
						Query q = new Query();
						if (brand == null)
							q.Columns = new ColumnSet(Event.Columns.DateTime, Event.Columns.Name, Event.Columns.K);
						else
							q.Columns = new ColumnSet(Event.Columns.DateTime, Event.Columns.Name, Event.Columns.K, Event.FriendlyLinkColumns);
						q.QueryCondition = new And(
							new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, dateFrom),
							new Q(Event.Columns.DateTime, QueryOperator.LessThan, dateTo),
							venue != null ? new Q(Event.Columns.VenueK, venue.K) : new Q(true),
							brand != null ? new Q(EventBrand.Columns.BrandK, brand.K) : new Q(true));
						q.OrderBy = Event.FutureEventOrder;
						if (brandK > 0)
						{
							q.TableElement = new Join(
							Event.CountryAllJoin,
							new TableElement(TablesEnum.EventBrand),
							QueryJoinType.Inner,
							Event.Columns.K,
							EventBrand.Columns.EventK);
						}
						es = new EventSet(q);
					}
					else
						es = new EventSet(new Query(new Q(Event.Columns.K, key)));

					if (es.Count == 0)
					{
						append(sb, "No events in our database for this selection", "");
					}
					else
					{
						//append(sb, "Events at " + venue.FriendlyName.Truncate(maxLength) + ", " + dateFrom.ToString("MMM yyyy") + "...", "");
						//append(sb, "", "");
						Dictionary<string, int> counter = new Dictionary<string, int>();
						foreach (Event ev in es)
						{
							string key1 = eventString(ev, brand != null);
							if (counter.ContainsKey(key1.ToLower()))
								counter[key1.ToLower()]++;
							else
								counter[key1.ToLower()] = 1;
						}


						foreach (Event ev in es)
						{
							string key1 = eventString(ev, brand != null);
							if (counter[key1.ToLower()] > 1)
								key1 = key1.Substring(0, 8) + " - #" + ev.K.ToString() + key1.Substring(8);

							append(sb, key1, ev.K.ToString());
						}

					}
					#endregion
				}
				sb.AppendLine("");
				sb.Append("}");
			}

			Out.Controls.Add(new LiteralControl(sb.ToString()));
		}
		string eventString(Event ev, bool showVenue)
		{
			if (!showVenue)
				return ev.DateTime.ToString("dd (ddd)") + " - " + ev.Name.TruncateWithDots(maxLength);
			else
				return ev.DateTime.ToString("dd (ddd)") + " - " + ev.FriendlyNameNoDateTruncated(20);

		}
		void appendVenues(StringBuilder sb, VenueSet vs)
		{
			Dictionary<string, int> counter = new Dictionary<string, int>();
			foreach (Venue v in vs)
			{
				string key = venueString(v);
				if (counter.ContainsKey(key.ToLower()))
					counter[key.ToLower()]++;
				else
					counter[key.ToLower()] = 1;
			}


			foreach (Venue v in vs)
			{
				string key = venueString(v);
				if (counter[key.ToLower()] > 1)
					key = key + " #" + v.K.ToString();

				append(sb, key, v.K.ToString());
			}
		}
		string venueString(Venue v)
		{
			string name = v.Name.TruncateWithDots(maxLength);
			if (name.ToLower().StartsWith("the "))
			{
				name = name.Substring(4) + " (" + name.Substring(0, 3) + ")";
			}
			return name;
		}
		int valueOrder = 0;
		void append(StringBuilder sb, string name, string value)
		{
			sb.Append(valueOrder > 0 ? ", " : "");
			valueOrder++;
			sb.Append("\"");
			sb.Append(valueOrder.ToString("0000"));
			sb.Append("$");
			sb.Append(value);
			sb.Append("\" : ");
			Cambro.Misc.Utility.JSON.SerializeStringStatic(name, sb);
			sb.AppendLine("");
		}
	}
}
