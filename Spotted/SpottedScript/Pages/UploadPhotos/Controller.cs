using System;
using System.DHTML;
using ScriptSharpLibrary;
using Sys;
using Utils;
using Sys.UI;
using JQ;
using SpottedScript.Controls.Picker;


namespace SpottedScript.Pages.UploadPhotos
{
	
	public class Controller
	{
		public View view;
		public Controller(View v)
		{
			this.view = v;

			view.Picker.EventSelectionSepcificationChanged = new SpottedScript.Controls.Picker.EventSelectionEvent(EventSelectionChange);
			
			if (Misc.BrowserIsIE)
				JQueryAPI.JQuery(Document.Body).ready(new Action(initialise));
			else
				initialise();
		}
		void initialise()
		{
			DomEvent.AddHandler(view.BackLink, "click", new DomEventHandler(backLinkClick));
			DomEvent.AddHandler(view.ForwardLink, "click", new DomEventHandler(forwardLinkClick));
		}
		public void backLinkClick(DomEvent e)
		{
			e.PreventDefault();
			view.Picker.DateModify(-7, "d");
		}
		public void forwardLinkClick(DomEvent e)
		{
			e.PreventDefault();
			view.Picker.DateModify(7, "d");
		}
		void updateDate(DateStub newDate)
		{
			int year = new DateTime().GetFullYear();
			DateStub previousMonday = newDate.PreviousMonday();
			DateStub nextSunday = newDate.NextSunday();
			if (newDate.NextSunday().Month == newDate.PreviousMonday().Month)
			{
				view.MonthLabel.InnerHTML = previousMonday.Day + " - " + nextSunday.Day + " " + nextSunday.MonthNameFull + (nextSunday.Year != year ? (" " + nextSunday.Year.ToString()) : "");
			}
			else
			{
				view.MonthLabel.InnerHTML = previousMonday.Day + " " + previousMonday.MonthNameFull + " - " + nextSunday.Day + " " + nextSunday.MonthNameFull + (nextSunday.Year != year ? (" " + nextSunday.Year.ToString()) : "");
			}
		}
		string previousSpecificationState = "";
		public void EventSelectionChange(object o, EventSelectionArgs e)
		{
			bool hasBrand = e.Specification != null && e.Specification.Brand != null && e.Specification.Brand.K > 0;
			bool hasPlace = e.Specification != null && e.Specification.Place != null && e.Specification.Place.K > 0;
			bool hasVenue = e.Specification != null && e.Specification.Venue != null && e.Specification.Venue.K > 0;
			bool hasMusic = e.Specification != null && e.Specification.Music != null && e.Specification.Music.K > 0;
			bool hasMe = e.Specification != null && e.Specification.Me;
			
			if (!hasMe && !hasBrand && !hasVenue && !(hasMusic && hasPlace))
			{
				view.CalendarHolderOuter.Style.Display = "none";
				return;
			}

			string specificationState = 
				"brand-" + (e.Specification.Brand == null ? "0" : e.Specification.Brand.K.ToString()) + "|" +
				"place-" + (e.Specification.Place == null ? "0" : e.Specification.Place.K.ToString()) + "|" +
				"venue-" + (e.Specification.Venue == null ? "0" : e.Specification.Venue.K.ToString()) + "|" +
				"music-" + (e.Specification.Music == null ? "0" : e.Specification.Music.K.ToString()) + "|" +
				"date-" + (e.Specification.Date == null ? "0" : e.Specification.Date.ToString()) + "|" +
				"me-" + (e.Specification.Me ? "1" : "0");

			if (specificationState != previousSpecificationState)
			{
				previousSpecificationState = specificationState;

				string url = "/support/getuncached.aspx?type=calendar&addgallery=1" +
					"&brandk=" + (e.Specification.Brand == null ? "0" : e.Specification.Brand.K.ToString()) +
					"&placek=" + (e.Specification.Place == null ? "0" : e.Specification.Place.K.ToString()) +
					"&venuek=" + (e.Specification.Venue == null ? "0" : e.Specification.Venue.K.ToString()) +
					"&musictypek=" + (e.Specification.Music == null ? "0" : e.Specification.Music.K.ToString()) +
					"&date=" + (e.Specification.Date == null ? "0" : e.Specification.Date.ToString()) +
					"&me=" + (e.Specification.Me ? "1" : "0");

				updateDate(e.Specification.Date);

				requestId++;

				int currentRequestId = requestId;
				int currentLoadId = loadId;

				jQuery.get(
					url,
					null,
					new ActionGet(gotCalendar),
					null,
					currentRequestId.ToString());

				Window.SetTimeout(
					delegate
					{
						if (loadId == currentLoadId)
						{
							view.CalendarLoadingOverlay.Style.Height = view.CalendarHolder.OffsetHeight.ToString() + "px";
							view.CalendarLoadingOverlay.Style.Display = "";
							view.LoadingLabel.Style.Display = "";
							view.MonthLabel.Style.Display = "none";
						}
					},
					100);
			}
			else
			{
				view.CalendarHolderOuter.Style.Display = "";
				view.CalendarLoadingOverlay.Style.Display = "none";
				view.LoadingLabel.Style.Display = "none";
				view.MonthLabel.Style.Display = "";
			}
		}

		int requestId = 0;
		int loadId = 0;
		void gotCalendar(string data, string textStatus, XMLHttpRequest req, string args)
		{
			
			int requestIdFromArgs = int.ParseInvariant((string)args);
			if (requestId == requestIdFromArgs)
			{
				loadId++;
				view.CalendarHolder.InnerHTML = data;
				view.CalendarHolderOuter.Style.Display = "";
				view.CalendarLoadingOverlay.Style.Display = "none";
				view.LoadingLabel.Style.Display = "none";
				view.MonthLabel.Style.Display = "";
			}
		}

		#region Debug
		int debugCount = 0;
		void Debug(string text)
		{
			view.Debug.Style.Display = "";
			debugCount++;
			view.Debug.Value = debugCount.ToString() + " " + text + "\n" + view.Debug.Value;
		}
		#endregion
	}
	
}
