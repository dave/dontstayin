using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;
using System.Collections;
using System.Collections.Generic;

namespace Spotted.CustomControls
{
	public class BuddyCalendar : DsiCalendar
	{
		public override string EventTag { get { return "small"; } }
		public override void RenderEvent(HtmlTextWriter w, Event ev)
		{
			w.RenderBeginTag("b");
			int buddiesCount = 0;
			foreach (Usr usr in GetBuddiesGoingToEvent(ev))
			{
				if (usr.NickName.Length > 0)
				{
					if (buddiesCount > 0)
						w.Write(", ");
					if (usr.Pic.ToString().Length > 0 && usr.Pic != Guid.Empty)
						w.AddAttribute("onmouseover", "stm(['<img src=" + Storage.Path(usr.Pic) + " width=100 height=100 class=Block />']);");
					else
						w.AddAttribute("onmouseover", "stmn();");
					w.AddAttribute("onmouseout", "htm();");
					w.AddAttribute("href", "/members/" + usr.NickName.ToLower());
					w.RenderBeginTag("a");
					w.Write(usr.NickName);
					w.RenderEndTag();//a
					buddiesCount++;
				}
			}
			w.Write(": ");
			w.RenderEndTag();//b

			base.RenderEvent(w, ev);
		}

		public override bool HilightThisEvent(Event ev)
		{
			return AllEvents.Any(e => e.K == ev.K && (bool)ev.ExtraSelectElements["CurrentUsrAttendedEvent"]);
		}

		#region DistinctBuddies
		class DistinctBuddyComparer : IEqualityComparer<Event>
		{
			// compares hash code first before checking Equals if different.
			public int GetHashCode(Event x)
			{
				return (int)x.ExtraSelectElements["Usr_K"];
			}

			public bool Equals(Event x, Event y)
			{
				return (int)x.ExtraSelectElements["Usr_K"] == (int)y.ExtraSelectElements["Usr_K"];
			}
		}
		#endregion


		private IEnumerable GetBuddiesGoingToEvent(Event ev)
		{
			return AllEvents.Where(e => e.K == ev.K).Distinct(new DistinctBuddyComparer()).ToList().ConvertAll(e => new Usr() { K = (int)e.ExtraSelectElements["Usr_K"], Pic = e.ExtraSelectElements["Usr_Pic"].ToString().Length > 0 ? new Guid(e.ExtraSelectElements["Usr_Pic"].ToString()) : Guid.Empty, NickName = e.ExtraSelectElements["Usr_NickName"].ToString() });
		}
	}
	#region DsiCalendar
	public class DsiCalendar : Control
	{
		public DsiCalendar() { }
		public int Month { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public EventSet AllEvents { get; set; }
		public bool ShowPlace { get; set; }
		public bool ShowVenue { get; set; }
		public bool Tickets { get; set; }
		public string NextMonthUrl { get; set; }
		public string PrevMonthUrl { get; set; }
		public bool ShowCountryFriendlyName = true;
		public bool ShowAddGalleryButton { get; set; }
		#region Render
		protected override void Render(HtmlTextWriter w)
		{
			w.AddAttribute("border", "0");
			w.AddAttribute("width", "100%");
			w.AddAttribute("cellpadding", "0");
			w.AddAttribute("cellspacing", "0");
			w.AddAttribute("class", "CalendarTable");
			w.RenderBeginTag("table");
			w.RenderBeginTag("tr");
			for (DateTime d = StartDate; d <= EndDate; d = d.AddDays(1))
			{
				int width1 = d.DayOfWeek.Equals(DayOfWeek.Friday) || d.DayOfWeek.Equals(DayOfWeek.Saturday) ? 110 : 82;
				w.AddAttribute("width", width1 + "px");
				w.AddAttribute("valign", "top");
				if (d == DateTime.Now.Date)
					w.AddAttribute("class", "CalendarDay Today");
				else if (d.Month != Month)
					w.AddAttribute("class", "CalendarDay OtherMonth");
				else if (d.DayOfWeek.Equals(DayOfWeek.Saturday) || d.DayOfWeek.Equals(DayOfWeek.Friday))
					w.AddAttribute("class", "CalendarDay Weekend");
				else
					w.AddAttribute("class", "CalendarDay Weekday");
				w.AddAttribute("height", "90");
				w.RenderBeginTag("td");
				{
					w.AddAttribute("name", "Day" + d.Date.ToString("yyyyMMdd"));
					w.RenderBeginTag("a");
					w.RenderEndTag();
					w.AddAttribute("class", d.Date == DateTime.Today ? "CalendarDayHead Today" : "CalendarDayHead");
					w.RenderBeginTag("div");
					{
						if (d.DayOfWeek.Equals(DayOfWeek.Monday) && !d.Equals(StartDate))
						{
							w.AddAttribute("href", "#Day" + d.AddDays(-1).ToString("yyyyMMdd"));
							w.RenderBeginTag("a");
							w.Write("&lt;");
							w.RenderEndTag();
							w.Write("&nbsp;");
						}
						else if (d.Equals(StartDate) && PrevMonthUrl != null && PrevMonthUrl.Length != 0)
						{
							w.AddAttribute("href", PrevMonthUrl + "#Day" + d.AddDays(-1).ToString("yyyyMMdd"));
							w.RenderBeginTag("a");
							w.Write("&lt;");
							w.RenderEndTag();
							w.Write("&nbsp;");
						}
						if (d.Date == DateTime.Today)
						{
							w.Write("Today");
						}
						else
						{
							w.Write(d.Date.ToString("ddd dd"));
						}
						if (d.DayOfWeek.Equals(DayOfWeek.Sunday) && !d.Equals(EndDate))
						{
							w.AddAttribute("href", "#Day" + d.AddDays(1).ToString("yyyyMMdd"));
							w.Write("&nbsp;");
							w.RenderBeginTag("a");
							w.Write("&gt;");
							w.RenderEndTag();
						}
						else if (d.Equals(EndDate) && NextMonthUrl != null && NextMonthUrl.Length != 0)
						{
							w.AddAttribute("href", NextMonthUrl + "#Day" + d.AddDays(1).ToString("yyyyMMdd"));
							w.Write("&nbsp;");
							w.RenderBeginTag("a");
							w.Write("&gt;");
							w.RenderEndTag();
						}
					}
					w.RenderEndTag();//div
					w.AddAttribute("class", "CalendarDayBody");
					w.RenderBeginTag("div");
					//w.Write("<a class=\"CalendarAddLink\" href=\"pages/events/edit\" date=\"" + d.ToString("dd MMM yyyy") + "\"><span>add<//span></a>");
					foreach (Event ev in EventsOnDay(d))
					{
						if (HilightThisEvent(ev))
							w.AddAttribute("class", "CalendarEvent Hilight");
						else
							w.AddAttribute("class", "CalendarEvent");

						int width = 76;
						if (ev.DateTime.DayOfWeek.Equals(DayOfWeek.Friday) || ev.DateTime.DayOfWeek.Equals(DayOfWeek.Saturday))
							width = 104;

						if (ev.DateTime == DateTime.Today)
							width = width - 2;

						string mouseover = "";
						string mouseout = "";
						if (HilightThisEvent(ev))
						{
							width = width - 8;
						}
						else
						{
							//mouseover = "this.className='CalendarEvent Hilight';this.style.width='" + (width - 8).ToString() + "px';";
							//mouseout = "this.className='CalendarEvent';this.style.width='" + width.ToString() + "px';";
						}

						w.AddStyleAttribute("width", width.ToString() + "px");

						//w.AddAttribute("onmouseover", "document.getElementById('Details" + ev.K + "').style.display='';" + mouseover);
						//w.AddAttribute("onmouseout", "document.getElementById('Details" + ev.K + "').style.display='none';" + mouseout);

						w.RenderBeginTag("div");
						RenderEvent(w, ev);
						w.RenderEndTag();//div
					}

					//w.Write("<small>Missing your event? <a href=\"/pages/events/edit\">Add it here</a>.</small>");

					w.RenderEndTag();//div
				}
				w.RenderEndTag();//td

				if (d.DayOfWeek.Equals(DayOfWeek.Sunday) && !d.Equals(StartDate))
					w.RenderEndTag();//tr
				if (d.DayOfWeek.Equals(DayOfWeek.Sunday) && !d.Equals(EndDate))
					w.RenderBeginTag("tr");
			}
			w.RenderEndTag();//table
			w.Write("<p><center><small>Missing your event? <a href=\"/pages/events/edit\">Add it here</a>.</small></center></p>");
			base.Render(w);
		}

		public virtual void RenderEvent(HtmlTextWriter w, Event ev)
		{
			string regionUrlPart = "";
			if (ev.ExtraSelectElements["Place_RegionAbbreviation"].ToString().Length > 0)
				regionUrlPart = "/" + ev.ExtraSelectElements["Place_RegionAbbreviation"].ToString();

			string placeUrl = "/" + ev.ExtraSelectElements["Country_UrlName"].ToString() + regionUrlPart + "/" + ev.ExtraSelectElements["Place_UrlName"].ToString();
			string placeName = ev.ExtraSelectElements["Place_Name"].ToString();

			string countryK = ev.ExtraSelectElements["Place_CountryK"].ToString();
			if (ShowCountryFriendlyName && int.Parse(countryK) != Country.FilterK)
			{
				string countryFriendlyName = ev.ExtraSelectElements["Country_FriendlyName"].ToString();
				placeName = placeName + " (" + countryFriendlyName + ")";
			}

			string venueUrl = placeUrl + "/" + ev.ExtraSelectElements["Venue_UrlName"].ToString();
			string venueName = ev.ExtraSelectElements["Venue_Name"].ToString();

			string eventUrl = venueUrl + "/" + ev.DateTime.ToString("yyyy") + "/" + ev.DateTime.ToString("MMM").ToLower() + "/" + ev.DateTime.ToString("dd") + "/" + "event-" + ev.K.ToString();

			w.Write(ev.TitleNoteHtmlCalendar);
			//w.RenderBeginTag(EventTag);
			w.AddAttribute("href", eventUrl);
			w.RenderBeginTag("a");

			if (ev.IsTicketsAvailable)
			{
				//Tickets icon
				w.AddAttribute("src", "/gfx/icon-tickets-small.png");
				w.AddAttribute("width", "20");
				w.AddAttribute("height", "16");
				w.AddAttribute("align", "left");
				w.AddAttribute("border", "0");
				w.AddAttribute("onmouseover", "stt('Tickets available');");
				w.AddAttribute("onmouseout", "htm();");
				w.AddStyleAttribute("margin-top", "2px");
				w.AddStyleAttribute("margin-right", "3px");
				w.RenderBeginTag("img");
				w.RenderEndTag();//img
			}

			if (ev.SpotterRequest.HasValue && ev.SpotterRequest.Value)
			{
				//Free Guestlist
				w.AddAttribute("src", "/gfx/icon-freeguestlist-small.png");
				w.AddAttribute("width", "20");
				w.AddAttribute("height", "16");
				w.AddAttribute("align", "left");
				w.AddAttribute("border", "0");
				w.AddAttribute("onmouseover", "stt('Free Guestlist available');");
				w.AddAttribute("onmouseout", "htm();");
				w.AddStyleAttribute("margin-top", "2px");
				w.AddStyleAttribute("margin-right", "3px");
				w.RenderBeginTag("img");
				w.RenderEndTag();//img
			}

			w.Write(ev.Name);
			w.RenderEndTag();//a
			//w.RenderEndTag();//b or small

			w.AddAttribute("id", "Details" + ev.K);
			//w.AddStyleAttribute("display", "none");
			w.RenderBeginTag("span");
			{
				w.RenderBeginTag("small");
				{
					if (ShowVenue)
					{
						w.Write(" @ ");
						w.AddAttribute("href", venueUrl);
						w.RenderBeginTag("a");
						w.Write(venueName);
						w.RenderEndTag();//a
					}
					if (ShowPlace)
					{
						w.Write(" in ");
						w.AddAttribute("href", placeUrl);
						w.RenderBeginTag("a");
						w.Write(placeName);
						w.RenderEndTag();//a
					}

					if (ShowAddGalleryButton)
					{
						w.AddStyleAttribute("width", "55px");
						w.AddStyleAttribute("display", "block");
						w.AddStyleAttribute("padding", "3px");
						w.AddStyleAttribute("margin-top", "3px");
						w.AddStyleAttribute("margin-bottom", "3px");
						w.AddAttribute("onclick", "window.location='/pages/galleries/add/eventk-" + ev.K.ToString() + "';return false;");
						w.RenderBeginTag("button");
						w.Write("Add<br />gallery");
						w.RenderEndTag();//button
					}

					if (ev.MusicTypesString.Length != 0)
					{

						w.Write(" (");
						w.Write(ev.MusicTypesString.TruncateWithDots(50));
						w.Write(")");

					}
				}
				w.RenderEndTag();//small
			}
			w.RenderEndTag();//span


		}
		

		private IEnumerable EventsOnDay(DateTime d)
		{
			foreach (Event e in DistinctEvents)
			{
				if (e.DateTime.Equals(d))
					yield return e;
			}
		}

		public virtual string EventTag
		{
			get { return "b"; }
		}

		#region DistinctEvents
		class DistinctEventComparer : IEqualityComparer<Event>
		{
			// compares hash code first before checking Equals if different.
			public int GetHashCode(Event x)
			{
				return x.K;
			}

			public bool Equals(Event x, Event y)
			{
				return x.K == y.K;
			}
		}

		public IEnumerable<Event> DistinctEvents
		{
			get { return AllEvents.Distinct(new DistinctEventComparer()); }
		}
		#endregion

		public virtual bool HilightThisEvent(Event ev)
		{
			return !Tickets && ev.HasHilight;
		}
		#endregion
	}
	#endregion

}
