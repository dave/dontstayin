using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Pages
{
	[ClientScript]
	public partial class FindEvents : DsiUserControl
	{
		//protected Controls.NewUserWizardOptions NewUserWizardOptions;
		//protected Controls.Picker Picker;

		public FindEvents()
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SetPageTitle("Find events");
			
			if (ContainerPage.Url["Mode"] == "Calendar")
			{
				NewUserWizardOptions.Visible = false;
				FindEventsHeader.Visible = false;
			}
			else
			{
				CalendarTabHolder.Visible = false;
			}
		}

		public override void DataBind()
		{
			//if (uiFinder.SelectedPlace != null)
			//{
			//    DateTime from = uiFinder.SelectedDate.AddDays(7 * WeeksOffset).Previous(DayOfWeek.Monday, true);
			//    DateTime to = uiFinder.SelectedDate.AddDays(7 * WeeksOffset).Next(DayOfWeek.Sunday, true);
			//    Event.EventsForDisplay events = new Event.EventsForDisplay();
			//    events.IgnoreMusicType = true;

			//    if (uiFinder.SelectedVenue != null)
			//    {
			//        events.VenueK = uiFinder.SelectedVenue.K;
			//    }
			//    else
			//    {
			//        events.PlaceK = uiFinder.SelectedPlace.K;
			//        events.MusicTypeK = uiMusicType.SelectedValue;
			//    }

			//    EventSet es = events.GetEventsBetweenDates(from, to);
			//    uiCalendar.AllEvents = es;
			//    uiCalendar.Month = uiFinder.SelectedMonth;
			//    //uiCalendar.ShowCountryFriendlyName = false;
			//    //uiCalendar.ShowPlace = false;
			//    //uiCalendar.ShowVenue = false;

			//    uiCalendar.ShowCountryFriendlyName = !(events.FilterByCountry || events.FilterByPlace || events.FilterByVenue);
			//    uiCalendar.ShowPlace = !(events.FilterByPlace || events.FilterByVenue);
			//    uiCalendar.ShowVenue = !events.FilterByVenue;

			//    uiCalendar.Tickets = true;
			//    uiCalendar.Month = uiFinder.SelectedMonth;
			//    uiCalendar.StartDate = from;
			//    uiCalendar.EndDate = to;
			//    uiCalendarPanel.Visible = true;

			//    if (es.Count > 0)
			//    {
			//        uiEventCalendarList.DataSource = es;
			//        uiEventCalendarList.DataBind();
			//        uiEventCalendarList.Visible = true;
			//        uiNoEvents.Visible = false;
			//    }
			//    else
			//    {
			//        uiEventCalendarList.Visible = false;
			//        uiNoEvents.Visible = true;
			//    }

			//    DateRange = from.ToString("ddd dd MMM") + " - " + to.ToString("ddd dd MMM");
			//}
			//else
			//{
			//    uiCalendarPanel.Visible = false;
			//}
		}

	}
}
