using System;

using SpottedScript.Controls.VenueCreator;

namespace SpottedScript.Controls.EventCreator
{
	public class EventInfo
	{
#if SCRIPT

#else
		public EventInfo(Bobs.Event e)
		{
			this.name = e.Name;
			this.k = e.K;
			this.date = e.DateTime;
			this.venueInfo = new VenueInfo(e.Venue);
			this.url = e.Url();
			this.picPath = e.AnyPicPath;
		}

		public static Bobs.ColumnSet Columns = new Bobs.ColumnSet(VenueInfo.Columns, Bobs.Event.Columns.UrlFragment, Bobs.Event.Columns.VenueK, Bobs.Event.Columns.Name, Bobs.Event.Columns.K, Bobs.Event.Columns.DateTime, Bobs.Event.Columns.VenueK);
#endif
		public static string EventFullName(EventInfo ei)
		{
#if SCRIPT
			return ei.name + " on " + ei.date.Format("ddd dd/MM/yyyy") + " @ " + VenueInfo.NameWithPlace(ei.venueInfo);
#else
			return ei.name + " on " + ei.date.ToString("ddd dd/MM/yyyy") + " @ " + VenueInfo.NameWithPlace(ei.venueInfo);
#endif
		}
		public string name;
		public int k;
		public DateTime date;
		public VenueInfo venueInfo;
		public string url;
		public string picPath;
	}
}
