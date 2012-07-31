using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Controls
{
	public partial class Calendar : System.Web.UI.UserControl
	{
		#region Properties
		public bool Personalise { get; set; }
		public bool HotTickets { get; set; }
		public bool Tickets { get; set; }
		public bool FreeGuestlist { get; set; }
		#endregion

		#region ContainerPage
		Master.DsiPage ContainerPage
		{
			get { return (Master.DsiPage)Page; }
		}
		#endregion

		#region Settings from Url
		int BrandK
		{
			get
			{
				if (ContainerPage.Url.HasBrandObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["BrandK"];
			}
		}
		int GroupK
		{
			get
			{
				if (ContainerPage.Url.HasGroupObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["GroupK"];
			}
		}
		int VenueK
		{
			get
			{
				if (ContainerPage.Url.HasVenueObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["VenueK"];
			}
		}
		int PlaceK
		{
			get
			{
				if (ContainerPage.Url.HasPlaceObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["PlaceK"];
			}
		}
		int CountryK
		{
			get
			{
				if (ContainerPage.Url.HasCountryObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["CountryK"];
			}
		}
		int Day
		{
			get
			{
				if (ContainerPage.Url.HasDayFilter)
					return ContainerPage.Url.DateFilter.Day;
				else if (ContainerPage.Url["D"].IsInt)
					return ContainerPage.Url["D"];
				else
					return 0;
			}
		}
		int Month
		{
			get
			{
				if (ContainerPage.Url.HasYearFilter || ContainerPage.Url.HasMonthFilter || ContainerPage.Url.HasDayFilter)
					return ContainerPage.Url.DateFilter.Month;
				else if (ContainerPage.Url["M"].IsInt)
					return ContainerPage.Url["M"];
				else
					return DateTime.Now.Month;
			}
		}
		int Year
		{
			get
			{
				if (ContainerPage.Url.HasYearFilter || ContainerPage.Url.HasMonthFilter || ContainerPage.Url.HasDayFilter)
					return ContainerPage.Url.DateFilter.Year;
				else if (ContainerPage.Url["Y"].IsInt)
					return ContainerPage.Url["Y"];
				else
					return DateTime.Now.Year;
			}
		}
		protected bool BuddyDisplay
		{
			get { return (ContainerPage.Url["Type"].Equals("Buddy")); }
		}
		#endregion
		#region FilterCountry
		Country FilterCountry
		{
			get
			{
				if (filterCountry == null && CountryK > 0)
					filterCountry = new Country(CountryK);
				return filterCountry;
			}
			set
			{
				filterCountry = value;
			}
		}
		Country filterCountry;
		#endregion

		private Event.EventsForDisplay eventsForDisplay = new Event.EventsForDisplay();
		public void Page_Init()
		{
			eventsForDisplay.PlaceK = this.PlaceK;
			eventsForDisplay.BuddyDisplay = this.BuddyDisplay;
			eventsForDisplay.BrandK = this.BrandK;
			eventsForDisplay.CountryK = this.CountryK;
			eventsForDisplay.Day = this.Day;
			eventsForDisplay.GroupK = this.GroupK;
			eventsForDisplay.Tickets = this.Tickets;
			eventsForDisplay.HotTickets = this.HotTickets;
			eventsForDisplay.FreeGuestlist = this.FreeGuestlist;
			eventsForDisplay.VenueK = this.VenueK;
			eventsForDisplay.Personalise = this.Personalise;
		}

		#region UrlCalendarGeneric
		public static string UrlCalendarGeneric(string Application, int Year, int Month, int Day, int SkipDay, params string[] par)
		{
			DateTime month = new DateTime(Year, Month, 1);
			string dayString = Day == 0 ? "" : ("/" + Day.ToString("00"));
			string url = UrlInfo.MakeUrl(Year + "/" + month.ToString("MMM").ToLower() + dayString, Application, par);
			string skip = "";
			if (SkipDay > 0)
				skip = "#Day" + new DateTime(Year, Month, SkipDay).ToString("yyyyMMdd");
			return url + skip;
		}
		#endregion
		#region UrlCalendarMonth
		public static string UrlCalendarMonth(bool Tickets, bool FreeGuestlist, int Year, int Month, int SkipDay, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, Year, Month, 0, SkipDay, par);
		}
		public static string UrlCalendarMonth(int Year, int Month, int SkipDay, params string[] par)
		{
			return UrlCalendarGeneric(null, Year, Month, 0, SkipDay, par);
		}
		#endregion
		#region UrlCalendarDay
		public static string UrlCalendarDay(bool Tickets, bool FreeGuestlist, int Year, int Month, int Day, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, Year, Month, Day, 0, par);
		}
		public static string UrlCalendarDay(int Year, int Month, int Day, params string[] par)
		{
			return UrlCalendarGeneric(null, Year, Month, Day, 0, par);
		}
		#endregion
		#region UrlCalendar
		public static string UrlCalendar(bool Tickets, bool FreeGuestlist, params string[] par)
		{
			return UrlCalendarGeneric(FreeGuestlist ? "free" : Tickets ? "tickets" : null, DateTime.Today.Year, DateTime.Today.Month, 0, DateTime.Today.Day, par);
		}
		public static string UrlCalendar(params string[] par)
		{
			return UrlCalendarGeneric(null, DateTime.Today.Year, DateTime.Today.Month, 0, DateTime.Today.Day, par);
		}
		#endregion

		void selectTab(Control c)
		{
			EventFinderTab.Attributes["class"] = c == EventFinderTab ? "TabbedHeading Selected" : "TabbedHeading";
			MyCalendarTab.Attributes["class"] = c == MyCalendarTab ? "TabbedHeading Selected" : "TabbedHeading";
			BuddyCalendarTab.Attributes["class"] = c == BuddyCalendarTab ? "TabbedHeading Selected" : "TabbedHeading";
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			if (this.Visible)
			{
				if (Personalise)
					Usr.KickUserIfNotLoggedIn("You must be logged in to use the My Calendar or Buddy Calendar pages.");

				if (BuddyDisplay)
					selectTab(BuddyCalendarTab);
				else if (Personalise)
					selectTab(MyCalendarTab);
				//else
					//CalendarTab.Visible=true;


				HotTicketsIntroPanel.Visible = HotTickets;
				NotHotTicketsIntroPanel.Visible = !HotTickets;

				if (Personalise && !BuddyDisplay)
				{
					if (Usr.Current.MusicTypesFavouriteCount == 0)
						Response.Redirect("/pages/mymusic");
					if (Usr.Current.PlacesVisitCount == 0)
						Response.Redirect("/pages/placesivisit");
				}
				
				
				MusicTypeDropDownPanel.Visible = !Personalise && !(eventsForDisplay.FilterByVenue || eventsForDisplay.FilterByBrand || eventsForDisplay.FilterByGroup);
				MusicFilterLabel1.Visible = MusicTypeDropDownPanel.Visible;
				MusicFilterLabel2.Visible = MusicTypeDropDownPanel.Visible;
				if (eventsForDisplay.FilterByVenue || eventsForDisplay.FilterByBrand || eventsForDisplay.FilterByGroup)
					eventsForDisplay.IgnoreMusicType = true;

				#region //Set up intro panel
				if (HotTickets)
				{
					TopIcon.Src = "/gfx/icon-hottickets.png";
					EventFinderTab.InnerText = "Hot tickets";
					

					HotTicketsIntroPanelWorldwideP.Visible = eventsForDisplay.FilterWorldwide;
					HotTicketsIntroPanelBrandP.Visible = eventsForDisplay.FilterByBrand;
					HotTicketsIntroPanelNonBrandP.Visible = eventsForDisplay.FilterByVenue || eventsForDisplay.FilterByPlace || eventsForDisplay.FilterByCountry || eventsForDisplay.FilterByGroup;

					if (eventsForDisplay.FilterWorldwide)
					{
						HotTicketsIntroPanelWorldwideHomeCountryLink.InnerText = HotTicketsIntroPanelWorldwideHomeCountryLink.InnerText.Replace("???", Country.Current.FriendlyName);
						HotTicketsIntroPanelWorldwideHomeCountryLink.HRef = Country.Current.UrlApp("hottickets");
					}
				}
				else
				{
					AllEventsIntroPanel.Visible = eventsForDisplay.FilterWorldwide;
					ObjectCalendarIntroPanel.Visible = eventsForDisplay.FilterByVenue || eventsForDisplay.FilterByPlace || eventsForDisplay.FilterByCountry || eventsForDisplay.FilterByBrand || eventsForDisplay.FilterByGroup;
					MyCalendarIntroPanel.Visible = Personalise && !Tickets && !BuddyDisplay;
					BuddyCalendarIntroPanel.Visible = BuddyDisplay;
					TicketsCalendarIntroPanel.Visible = Personalise && Tickets;

					if (Tickets)
						TopIcon.Src = "/gfx/icon-tickets.png";
					else if (FreeGuestlist)
						TopIcon.Src = "/gfx/icon-freeguestlist.png";

					if (Tickets)
						EventFinderTab.InnerText = "Tickets";
					else if (FreeGuestlist)
						EventFinderTab.InnerText = "Free Guestlist";

					if (Tickets)
					{
						AllEventsIntroPanelEventsLabel1.Text = "events with tickets available";
						AllEventsIntroPanelEventsLabel2.Text = "events with tickets available";
						AllEventsIntroPanelEventsLabel3.Text = "events with tickets available";
					}
					else if (FreeGuestlist)
					{
						AllEventsIntroPanelEventsLabel1.Text = "Free Guestlist events";
						AllEventsIntroPanelEventsLabel2.Text = "Free Guestlist events";
						AllEventsIntroPanelEventsLabel3.Text = "Free Guestlist events";
					}
					else
					{
						AllEventsIntroPanelEventsLabel1.Text = "events";
						AllEventsIntroPanelEventsLabel2.Text = "events";
						AllEventsIntroPanelEventsLabel3.Text = "events";
					}

					AllEventsIntroPanelHomeCountryLink.InnerText = AllEventsIntroPanelHomeCountryLink.InnerText.Replace("???", Country.Current.FriendlyName);
					if (DateTime.Now.Month == Month && DateTime.Now.Year == Year && Day == 0)
						AllEventsIntroPanelHomeCountryLink.HRef = Country.Current.UrlCalendar(Tickets, FreeGuestlist);
					else
						AllEventsIntroPanelHomeCountryLink.HRef = Country.Current.UrlCalendarDay(Tickets, FreeGuestlist, Year, Month, Day);


				}
				#endregion


				string dateFilterString = "";

				if (HotTickets)
				{
					MonthViewPanel.Visible = false;
					DayViewPanel.Visible = false;
					HotTicketsEventListPanel.Visible = true;

					EventSet es = eventsForDisplay.GetTopHotTicketEvents();

					HotTicketsEventListNoEventsP.Visible = es.Count == 0;
					HotTicketsEventListEventsDiv.Visible = es.Count > 0;
					if (es.Count > 0)
					{
						HotTicketsEventList.DataSource = es;
						HotTicketsEventList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventList0.ascx");
						HotTicketsEventList.DataBind();
					}
				}
				else if (Day == 0)
				{
					MonthViewPanel.Visible = true;
					DayViewPanel.Visible = false;
					HotTicketsEventListPanel.Visible = false;

					#region firstCellDate, lastCellDate
					DateTime firstOfMonth = new DateTime(Year, Month, 1);
					DateTime firstCellDate = firstOfMonth.Previous(DayOfWeek.Monday, true);

					DateTime lastOfMonth = firstOfMonth.AddDays(DateTime.DaysInMonth(Year, Month) - 1);
					DateTime lastCellDate = lastOfMonth.Next(DayOfWeek.Sunday, true);
					#endregion


					dateFilterString = firstOfMonth.ToString("MMMM yyyy");

					#region get event set
					EventSet es = eventsForDisplay.GetEventsBetweenDates(firstCellDate, lastCellDate);
					#endregion

					#region bind to calendar
					CustomControls.DsiCalendar uiCal = this.BuddyDisplay ? new CustomControls.BuddyCalendar() : new CustomControls.DsiCalendar();
					uiCalPlaceHolder.Controls.Add(uiCal);

					uiCal.ShowCountryFriendlyName = !(eventsForDisplay.FilterByCountry || eventsForDisplay.FilterByPlace || eventsForDisplay.FilterByVenue);
					uiCal.ShowPlace = !(eventsForDisplay.FilterByPlace || eventsForDisplay.FilterByVenue);
					uiCal.ShowVenue = !eventsForDisplay.FilterByVenue;
					uiCal.Tickets = Tickets;
					uiCal.Month = Month;
					uiCal.AllEvents = es;
					uiCal.StartDate = firstCellDate;
					uiCal.EndDate = lastCellDate;
					#endregion

					#region set up next / prev month links

					string nextMonthUrl = ChangeMonthUrl(lastOfMonth.AddDays(1).Month, lastOfMonth.AddDays(1).Year);
					string prevMonthUrl = ChangeMonthUrl(firstOfMonth.AddDays(-1).Month, firstOfMonth.AddDays(-1).Year);

					#region set up links
					uiCal.NextMonthUrl = nextMonthUrl;
					uiCal.PrevMonthUrl = prevMonthUrl;

					MonthNameLabel.Text = firstOfMonth.ToString("MMMM") + " " + Year.ToString();
					MonthNameLabel1.Text = firstOfMonth.ToString("MMMM") + " " + Year.ToString();

					BackLink.InnerHtml = "&lt; " + firstOfMonth.AddDays(-1).ToString("MMMM");
					BackLink1.InnerHtml = "&lt; " + firstOfMonth.AddDays(-1).ToString("MMMM");
					BackLink.HRef = prevMonthUrl;
					BackLink1.HRef = prevMonthUrl;

					NextLink.InnerHtml = lastOfMonth.AddDays(1).ToString("MMMM") + " &gt;";
					NextLink1.InnerHtml = lastOfMonth.AddDays(1).ToString("MMMM") + " &gt;";
					NextLink.HRef = nextMonthUrl;
					NextLink1.HRef = nextMonthUrl;
					#endregion

					if (uiCal.AllEvents.Count == 0)
					{
						Event latestPastEvent = eventsForDisplay.GetLatestPastEvent(firstOfMonth);
						if (latestPastEvent == null)
						{
							#region disable the back link if we have no past events
							BackLink.HRef = "";
							BackLink1.HRef = "";
							BackLink.Disabled = true;
							BackLink1.Disabled = true;
							#endregion
						}
						else
						{
							#region set up the back link with the month of the latest past event
							BackLink.HRef = ChangeMonthUrl(latestPastEvent.DateTime.Month, latestPastEvent.DateTime.Year);
							BackLink1.HRef = ChangeMonthUrl(latestPastEvent.DateTime.Month, latestPastEvent.DateTime.Year);
							BackLink.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("MMMM");
							BackLink1.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("MMMM");
							if (latestPastEvent.DateTime.Year != Year)
							{
								BackLink.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("MMMM") + " " + latestPastEvent.DateTime.Year.ToString();
								BackLink1.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("MMMM") + " " + latestPastEvent.DateTime.Year.ToString();
							}
							#endregion
						}

						Event nextFutureEvent = eventsForDisplay.GetNextFutureEvent(lastOfMonth);
						if (nextFutureEvent == null)
						{
							#region disable the forward link if we have no future events
							NextLink.HRef = "";
							NextLink1.HRef = "";
							NextLink.Disabled = true;
							NextLink1.Disabled = true;
							#endregion
						}
						else
						{
							#region set up the back link with the month of the first future event
							NextLink.HRef = ChangeMonthUrl(nextFutureEvent.DateTime.Month, nextFutureEvent.DateTime.Year);
							NextLink1.HRef = ChangeMonthUrl(nextFutureEvent.DateTime.Month, nextFutureEvent.DateTime.Year);
							NextLink.InnerHtml = nextFutureEvent.DateTime.ToString("MMMM") + " &gt;";
							NextLink1.InnerHtml = nextFutureEvent.DateTime.ToString("MMMM") + " &gt;";
							if (nextFutureEvent.DateTime.Year != Year)
							{
								NextLink.InnerHtml = nextFutureEvent.DateTime.ToString("MMMM") + " " + nextFutureEvent.DateTime.Year.ToString() + " &gt;";
								NextLink1.InnerHtml = nextFutureEvent.DateTime.ToString("MMMM") + " " + nextFutureEvent.DateTime.Year.ToString() + " &gt;";
							}
							#endregion
						}

						#region ensure links are fully disabled
						if (BackLink.Disabled)
							BackLink.Attributes["class"] = "DisabledAnchor";
						if (BackLink1.Disabled)
							BackLink1.Attributes["class"] = "DisabledAnchor";
						if (NextLink.Disabled)
							NextLink.Attributes["class"] = "DisabledAnchor";
						if (NextLink1.Disabled)
							NextLink1.Attributes["class"] = "DisabledAnchor";
						#endregion
					}
					#endregion
				}
				else
				{
					MonthViewPanel.Visible = false;
					DayViewPanel.Visible = true;
					HotTicketsEventListPanel.Visible = false;

					DateTime day = new DateTime(Year, Month, Day);
					dateFilterString = day.ToString("dddd dd MMMM yyyy");

					#region get event set
					EventSet es = eventsForDisplay.GetEventsForDay(day);
					#endregion

					DayViewNoEventsP.Visible = es.Count == 0;
					DayViewEventsDiv.Visible = es.Count > 0;
					if (es.Count > 0)
					{
						DayViewDataList.DataSource = es;

						if (es.Count < 20)
							DayViewDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventList0.ascx");
						else if (es.Count < 50)
							DayViewDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventList1.ascx");
						else
							DayViewDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventList2.ascx");

						//DayViewDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventList2.ascx");

						DayViewDataList.DataBind();

					}

					#region set up next / prev day links

					DayMonthLink.InnerText = "Show calendar for " + day.ToString("MMMM yyyy");
					DayMonthLink1.InnerText = "Show calendar for " + day.ToString("MMMM yyyy");
					DayMonthLink.HRef = ChangeMonthUrl(day.Month, day.Year);
					DayMonthLink1.HRef = ChangeMonthUrl(day.Month, day.Year);

					string nextDayUrl = ChangeDayUrl(day.AddDays(1).Month, day.AddDays(1).Year, day.AddDays(1).Day);
					string prevDayUrl = ChangeDayUrl(day.AddDays(-1).Month, day.AddDays(-1).Year, day.AddDays(-1).Day);

					#region set up links
					DayNameLabel.Text = dateFilterString;
					DayNameLabel1.Text = dateFilterString;

					DayBackLink.InnerHtml = "&lt; " + day.AddDays(-1).ToString("dddd dd");
					DayBackLink1.InnerHtml = "&lt; " + day.AddDays(-1).ToString("dddd dd");
					DayBackLink.HRef = prevDayUrl;
					DayBackLink1.HRef = prevDayUrl;

					DayNextLink.InnerHtml = day.AddDays(1).ToString("dddd dd") + " &gt;";
					DayNextLink1.InnerHtml = day.AddDays(1).ToString("dddd dd") + " &gt;";
					DayNextLink.HRef = nextDayUrl;
					DayNextLink1.HRef = nextDayUrl;
					#endregion

					if (es.Count == 0)
					{
						Event latestPastEvent = eventsForDisplay.GetLatestPastEvent(day);
						if (latestPastEvent == null)
						{
							#region disable the back link if we have no past events
							DayBackLink.HRef = "";
							DayBackLink1.HRef = "";
							DayBackLink.Disabled = true;
							DayBackLink1.Disabled = true;
							#endregion
						}
						else
						{
							#region set up the back link with the month of the latest past event
							DayBackLink.HRef = ChangeDayUrl(latestPastEvent.DateTime.Month, latestPastEvent.DateTime.Year, latestPastEvent.DateTime.Day);
							DayBackLink1.HRef = ChangeDayUrl(latestPastEvent.DateTime.Month, latestPastEvent.DateTime.Year, latestPastEvent.DateTime.Day);

							if (latestPastEvent.DateTime.Year != Year)
							{
								DayBackLink.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("dddd dd MMMM yyyy");
								DayBackLink1.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("dddd dd MMMM yyyy");
							}
							else if (latestPastEvent.DateTime.Month != Month)
							{
								DayBackLink.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("dddd dd MMMM");
								DayBackLink1.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("dddd dd MMMM");
							}
							else
							{
								DayBackLink.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("dddd dd");
								DayBackLink1.InnerHtml = "&lt; " + latestPastEvent.DateTime.ToString("dddd dd");
							}
							#endregion
						}

						Event nextFutureEvent = eventsForDisplay.GetNextFutureEvent(day);
						if (nextFutureEvent == null)
						{
							#region disable the forward link if we have no future events
							DayNextLink.HRef = "";
							DayNextLink1.HRef = "";
							DayNextLink.Disabled = true;
							DayNextLink1.Disabled = true;
							#endregion
						}
						else
						{
							#region set up the back link with the month of the first future event
							DayNextLink.HRef = ChangeDayUrl(nextFutureEvent.DateTime.Month, nextFutureEvent.DateTime.Year, nextFutureEvent.DateTime.Day);
							DayNextLink1.HRef = ChangeDayUrl(nextFutureEvent.DateTime.Month, nextFutureEvent.DateTime.Year, nextFutureEvent.DateTime.Day);

							if (nextFutureEvent.DateTime.Year != Year)
							{
								DayNextLink.InnerHtml = nextFutureEvent.DateTime.ToString("dddd dd MMMM yyyy") + " &gt;";
								DayNextLink1.InnerHtml = nextFutureEvent.DateTime.ToString("dddd dd MMMM yyyy") + " &gt;";
							}
							if (nextFutureEvent.DateTime.Month != Month)
							{
								DayNextLink.InnerHtml = nextFutureEvent.DateTime.ToString("dddd dd MMMM") + " &gt;";
								DayNextLink1.InnerHtml = nextFutureEvent.DateTime.ToString("dddd dd MMMM") + " &gt;";
							}
							else
							{
								DayNextLink.InnerHtml = nextFutureEvent.DateTime.ToString("dddd dd") + " &gt;";
								DayNextLink1.InnerHtml = nextFutureEvent.DateTime.ToString("dddd dd") + " &gt;";
							}
							#endregion
						}

						#region ensure links are fully disabled
						if (DayBackLink.Disabled)
							DayBackLink.Attributes["class"] = "DisabledAnchor";
						if (DayBackLink1.Disabled)
							DayBackLink1.Attributes["class"] = "DisabledAnchor";
						if (DayNextLink.Disabled)
							DayNextLink.Attributes["class"] = "DisabledAnchor";
						if (DayNextLink1.Disabled)
							DayNextLink1.Attributes["class"] = "DisabledAnchor";
						#endregion
					}
					#endregion
				}

				#region Set up intro text and page title
				if (HotTickets)
				{
					if (eventsForDisplay.FilterByBrand)
					{
						Brand b = new Brand(BrandK);
						HotTicketsIntroPanelBrandLink.HRef = b.Url();
						HotTicketsIntroPanelBrandLink.InnerText = b.Name;
						((Spotted.Master.DsiPage)this.Page).SetPageTitle("Hot tickets for " + b.FriendlyName + " events", b.FriendlyName);
						HotTicketsIntroPanelTicketsCalendarLink.HRef = b.UrlCalendar(true, false);

					}
					else if (eventsForDisplay.FilterByGroup)
					{
						Group g = new Group(GroupK);
						HotTicketsIntroPanelNonBrandInAtLabel.Text = "recommended by";
						HotTicketsIntroPanelNonBrandObjectLink.HRef = g.Url();
						HotTicketsIntroPanelNonBrandObjectLink.InnerText = g.FriendlyName;
						((Spotted.Master.DsiPage)this.Page).SetPageTitle("Hot tickets recommended by " + g.FriendlyName, g.FriendlyName);
						HotTicketsIntroPanelTicketsCalendarLink.HRef = g.UrlCalendar(true, false);
					}
					else if (eventsForDisplay.FilterByVenue)
					{
						HotTicketsIntroPanelNonBrandInAtLabel.Text = "at";
						Venue v = new Venue(VenueK);
						HotTicketsIntroPanelNonBrandObjectLink.InnerText = v.FriendlyName;
						HotTicketsIntroPanelNonBrandObjectLink.HRef = v.Url();
						((Spotted.Master.DsiPage)this.Page).SetPageTitle("Hot tickets for events at " + v.FriendlyName, v.Name);
						HotTicketsIntroPanelTicketsCalendarLink.HRef = v.UrlCalendar(true, false);
					}
					else if (eventsForDisplay.FilterByPlace)
					{
						HotTicketsIntroPanelNonBrandInAtLabel.Text = "in";
						Place p = new Place(PlaceK);
						HotTicketsIntroPanelNonBrandObjectLink.InnerText = p.FriendlyName;
						HotTicketsIntroPanelNonBrandObjectLink.HRef = p.Url();
						((Spotted.Master.DsiPage)this.Page).SetPageTitle("Hot tickets for events in " + p.FriendlyName, p.Name);
						HotTicketsIntroPanelTicketsCalendarLink.HRef = p.UrlCalendar(true, false);
					}
					else if (eventsForDisplay.FilterByCountry)
					{
						HotTicketsIntroPanelNonBrandInAtLabel.Text = "in";
						HotTicketsIntroPanelNonBrandObjectLink.InnerText = FilterCountry.FriendlyName;
						HotTicketsIntroPanelNonBrandObjectLink.HRef = FilterCountry.Url();
						((Spotted.Master.DsiPage)this.Page).SetPageTitle("Hot tickets for events in " + FilterCountry.FriendlyName, FilterCountry.Name);
						HotTicketsIntroPanelTicketsCalendarLink.HRef = FilterCountry.UrlCalendar(true, false);
					}
					else if (eventsForDisplay.FilterWorldwide)
					{
						((Spotted.Master.DsiPage)this.Page).SetPageTitle("Hot tickets worldwide");
						HotTicketsIntroPanelTicketsCalendarLink.HRef = Calendar.UrlCalendar(true, false);
					}
				}
				else
				{
					ObjectCalendarIntroBrand.Visible = eventsForDisplay.FilterByBrand;
					ObjectCalendarIntroNonBrand.Visible = !eventsForDisplay.FilterByBrand;
					if (eventsForDisplay.FilterByBrand)
					{
						Brand b = new Brand(BrandK);
						ObjectCalendarIntroBrandAnchor.HRef = b.Url();
						ObjectCalendarIntroBrandAnchor.InnerText = b.Name;
						((Spotted.Master.DsiPage)this.Page).SetPageTitle(b.FriendlyName + (FreeGuestlist ? " Free Guestlist" : "") + " calendar for " + dateFilterString, b.FriendlyName);

					}
					else if (eventsForDisplay.FilterByGroup)
					{
						Group g = new Group(GroupK);
						ObjectCalendarIntroInAtLabel.Text = "recommended by";
						ObjectCalendarIntroObjectLink.HRef = g.Url();
						ObjectCalendarIntroObjectLink.InnerText = g.FriendlyName;
						((Spotted.Master.DsiPage)this.Page).SetPageTitle(g.FriendlyName + (FreeGuestlist ? " Free Guestlist" : "") + " calendar for " + dateFilterString, g.FriendlyName);
					}
					else if (eventsForDisplay.FilterByVenue)
					{
						ObjectCalendarIntroInAtLabel.Text = "at";
						Venue v = new Venue(VenueK);
						ObjectCalendarIntroObjectLink.InnerText = v.FriendlyName;
						ObjectCalendarIntroObjectLink.HRef = v.Url();
						((Spotted.Master.DsiPage)this.Page).SetPageTitle(v.FriendlyName + (FreeGuestlist ? " Free Guestlist" : "") + " events calendar for " + dateFilterString, v.Name);
					}
					else if (eventsForDisplay.FilterByPlace)
					{
						ObjectCalendarIntroInAtLabel.Text = "in";
						Place p = new Place(PlaceK);
						ObjectCalendarIntroObjectLink.InnerText = p.FriendlyName;
						ObjectCalendarIntroObjectLink.HRef = p.Url();
						((Spotted.Master.DsiPage)this.Page).SetPageTitle(p.FriendlyName + (FreeGuestlist ? " Free Guestlist" : "") + " events calendar for " + dateFilterString, p.Name);
					}
					else if (eventsForDisplay.FilterByCountry)
					{
						ObjectCalendarIntroInAtLabel.Text = "in";
						ObjectCalendarIntroObjectLink.InnerText = FilterCountry.FriendlyName;
						ObjectCalendarIntroObjectLink.HRef = FilterCountry.Url();
						((Spotted.Master.DsiPage)this.Page).SetPageTitle((FreeGuestlist ? "Free Guestlist calendar" : " Calendar") + " for " + FilterCountry.FriendlyName + ", " + dateFilterString, FilterCountry.Name);
					}
					else if (eventsForDisplay.FilterWorldwide)
					{
						((Spotted.Master.DsiPage)this.Page).SetPageTitle((FreeGuestlist ? "Free Guestlist calendar" : " Calendar") + " for " + dateFilterString);
					}
					if (Personalise && !BuddyDisplay)
						((Spotted.Master.DsiPage)this.Page).SetPageTitle("My calendar");
					else if (BuddyDisplay)
						((Spotted.Master.DsiPage)this.Page).SetPageTitle("Buddy calendar");
				}
				#endregion
			}
		}

		#region string ChangeMonthUrl
		string ChangeMonthUrl(int month, int year)
		{
			return ChangeDayUrl(month, year, 0);
		}
		#endregion
		#region string ChangeDayUrl
		string ChangeDayUrl(int month, int year, int day)
		{
			if (ContainerPage.Url.HasObjectFilter && ContainerPage.Url.ObjectFilterBob is ICalendar)
			{
				return ((ICalendar)ContainerPage.Url.ObjectFilterBob).UrlCalendarDay(Tickets, FreeGuestlist, year, month, day);
			}
			else if (Personalise || ContainerPage.Url["Type"].Exists)
			{
				ArrayList al = new ArrayList();
				al.Add("M");
				al.Add(month.ToString());
				al.Add("Y");
				al.Add(year.ToString());
				
				if (day != 0)
				{
					al.Add("D");
					al.Add(day.ToString());
				}

				if (ContainerPage.Url["Type"].Exists)
				{
					al.Add("Type");
					al.Add((string)ContainerPage.Url["Type"]);
				}

				if (Tickets)
					return UrlInfo.PageUrl("TicketsCalendar", ((string[])al.ToArray(typeof(string))));
				else if (Personalise)
					return UrlInfo.PageUrl("MyCalendar", ((string[])al.ToArray(typeof(string))));
				else
					return UrlInfo.PageUrl("Calendar", ((string[])al.ToArray(typeof(string))));
			}
			else if (eventsForDisplay.FilterByVenue || eventsForDisplay.FilterByPlace || eventsForDisplay.FilterByCountry || eventsForDisplay.FilterByBrand || eventsForDisplay.FilterByGroup)
			{
				return ((ICalendar)eventsForDisplay.FilterBob).UrlCalendarDay(Tickets, FreeGuestlist, year, month, day);
			}
			else
				return UrlCalendarDay(Tickets, FreeGuestlist, year, month, day);
		}
		#endregion
	}
}
