using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Bobs;

namespace Spotted.Controls
{
	public class Archive : Control
	{

		public static string GetUrl(int Year, int Month, int Day, Model.Entities.ArchiveObjectType Type, string[] par, string UrlFilterPart)
		{
			return Vars.GetArchiveUrl(Year, Month, Day, Type, par, UrlFilterPart);

		}

		string Link(int Year, int Month, int Day, Model.Entities.ArchiveObjectType Type)
		{
			if (ContainerPage.Url.ObjectFilterBob != null && ContainerPage.Url.ObjectFilterBob is IHasArchive)
				return ((IHasArchive)ContainerPage.Url.ObjectFilterBob).UrlArchiveDate(Year, Month, Day, Type);
			else
				return GetUrl(Year, Month, Day, Type, new string[] { }, "");
		}
		Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
			}
		}

		public Archive()
		{ }
		#region Month
		public int Month
		{
			get
			{
				return month;
			}
			set
			{
				month = value;
			}
		}
		private int month;
		#endregion
		#region StartDate
		public DateTime StartDate
		{
			get
			{
				return startDate;
			}
			set
			{
				startDate = value;
			}
		}
		private DateTime startDate;
		#endregion
		#region EndDate
		public DateTime EndDate
		{
			get
			{
				return endDate;
			}
			set
			{
				endDate = value;
			}
		}
		private DateTime endDate;
		#endregion
		#region Objects
		public BobSet Objects
		{
			get
			{
				return objects;
			}
			set
			{
				objects = value;
			}
		}
		private BobSet objects;
		#endregion
		#region ShowCountry
		public bool ShowCountry
		{
			get
			{
				return showCountry;
			}
			set
			{
				showCountry = value;
			}
		}
		private bool showCountry;
		#endregion
		#region ShowPlace
		public bool ShowPlace
		{
			get
			{
				return showPlace;
			}
			set
			{
				showPlace = value;
			}
		}
		private bool showPlace;
		#endregion
		#region ShowVenue
		public bool ShowVenue
		{
			get
			{
				return showVenue;
			}
			set
			{
				showVenue = value;
			}
		}
		private bool showVenue;
		#endregion
		#region ShowEvent
		public bool ShowEvent
		{
			get
			{
				return showEvent;
			}
			set
			{
				showEvent = value;
			}
		}
		private bool showEvent;
		#endregion
		#region Type
		public Model.Entities.ArchiveObjectType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
		private Model.Entities.ArchiveObjectType type;
		#endregion
		public bool ShowCountryFriendlyName = true;
		protected override void Render(HtmlTextWriter w)
		{
			int count = 0;
			w.AddAttribute("border", "0");
			w.AddAttribute("width", "100%");
			w.AddAttribute("cellpadding", "0");
			w.AddAttribute("cellspacing", "0");
			w.AddAttribute("class", "CalendarTable");
			w.RenderBeginTag("table");
			w.RenderBeginTag("tr");
			for (DateTime d = StartDate; d <= EndDate; d = d.AddDays(1))
			{
				w.AddAttribute("width", "14%");
				w.AddAttribute("valign", "top");
				if (d == DateTime.Now.Date || (ContainerPage.Url.HasDayFilter && d == ContainerPage.Url.DateFilter))
					w.AddAttribute("class", "CalendarDay Today");
				else if (d.Month != Month)
					w.AddAttribute("class", "CalendarDay OtherMonth");
				else if (d.DayOfWeek.Equals(DayOfWeek.Saturday) || d.DayOfWeek.Equals(DayOfWeek.Friday))
					w.AddAttribute("class", "CalendarDay Weekend");
				else
					w.AddAttribute("class", "CalendarDay Weekday");
				w.AddAttribute("height", "90");
				w.RenderBeginTag("td");
				w.AddAttribute("name", "Day" + d.Date.ToString("yyyyMMdd"));
				w.RenderBeginTag("a");
				w.RenderEndTag();
				w.AddAttribute("class", "CalendarDayHead");
				w.RenderBeginTag("div");
				w.RenderBeginTag("nobr");
				if (d.DayOfWeek.Equals(DayOfWeek.Monday) && !d.Equals(StartDate))
				{
					w.AddAttribute("href", "#Day" + d.AddDays(-1).ToString("yyyyMMdd"));
					w.RenderBeginTag("a");
					w.Write("&lt;");
					w.RenderEndTag();
					w.Write("&nbsp;");
				}
				w.Write(d.Date.ToString("ddd dd"));
				if (d.DayOfWeek.Equals(DayOfWeek.Sunday) && !d.Equals(EndDate))
				{
					w.Write("&nbsp;");
					w.AddAttribute("href", "#Day" + d.AddDays(1).ToString("yyyyMMdd"));
					w.RenderBeginTag("a");
					w.Write("&gt;");
					w.RenderEndTag();
				}
				w.RenderEndTag();//nobr
				w.RenderEndTag();//div
				w.AddAttribute("class", "CalendarDayBody");
				w.RenderBeginTag("div");
				int objectsDone = 0;
				if (Objects.Count > 150)
				{
					while (count < Objects.Count && ((IArchive)Objects.GetFromIndex(count)).ArchiveDateTime.Date.Equals(d))
					{
						objectsDone++;
						count++;
					}
					if (objectsDone > 0)
					{
						w.AddStyleAttribute("padding", "2px");
						w.RenderBeginTag("div");
						w.AddAttribute("href", Link(d.Year, d.Month, d.Day, Type));
						w.RenderBeginTag("a");
						w.Write(objectsDone + " item" + (objectsDone == 1 ? "" : "s"));
						w.RenderEndTag();//a
						w.RenderEndTag();//div
					}
				}
				else
				{
					while (count < Objects.Count && ((IArchive)Objects.GetFromIndex(count)).ArchiveDateTime.Date.Equals(d))
					{
						objectsDone++;
						w.AddAttribute("align", "center");
						w.RenderBeginTag("div");
						w.Write(((IArchive)Objects.GetFromIndex(count)).ArchiveHtml(ShowCountry, ShowPlace, ShowVenue, ShowEvent, 70));
						w.RenderEndTag();//div
						count++;
					}
				}
				//	if (objectsDone==0)
				//	{
				//		w.Write("<div align=\"center\"><small>(none)</small></div>");//"<b>No events</b><br><small>Why don't you <a href=\"/pages/events/edit\">add one</a>?</small>");
				//	}
				w.RenderEndTag();//div
				w.Write("<img src=\"/gfx/1pix.gif\" width=\"70\" height=\"1\">");
				w.RenderEndTag();//td

				if (d.DayOfWeek.Equals(DayOfWeek.Sunday) && !d.Equals(StartDate))
					w.RenderEndTag();//tr
				if (d.DayOfWeek.Equals(DayOfWeek.Sunday) && !d.Equals(EndDate))
					w.RenderBeginTag("tr");
			}
			w.RenderEndTag();//tr
			base.Render(w);
		}
	}
}
