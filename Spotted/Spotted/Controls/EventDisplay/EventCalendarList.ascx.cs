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

namespace Spotted.Controls.EventDisplay
{
	public partial class EventCalendarList : System.Web.UI.UserControl
	{
		public EventSet DataSource { get; set; }
		public bool LinkToEventGallery { get; set; }

		public override void DataBind()
		{
			if (DataSource != null)
				BindEventsList(DataSource);
		}

		private void BindEventsList(EventSet events)
		{
			DateTime currentDateTime = DateTime.MinValue;
			int currentEventK = 0;
			foreach (Event e in events)
			{
				if (currentEventK != e.K)
				{
					currentEventK = e.K;
					if (currentDateTime != e.DateTime)
					{
						currentDateTime = e.DateTime;
						this.Controls.Add(CreateDateRow(e.DateTime));
					}
					this.Controls.Add(CreateEventRow(e));
				}
			}
		}

		private HtmlGenericControl CreateDateRow(DateTime dateTime)
		{
			HtmlGenericControl p = new HtmlGenericControl("p");
			p.Attributes.Add("class", "CalendarDayHead");
			p.InnerText = dateTime.ToString("ddd dd MMM");

			return p;
		}

		private HtmlGenericControl CreateEventRow(Event e)
		{
			EventDisplay eventDisplay = (EventDisplay)this.LoadControl(EventDisplayControlSource);
			eventDisplay.CurrentEvent = e;
			eventDisplay.LinkToEventGallery = this.LinkToEventGallery;

			HtmlGenericControl div = new HtmlGenericControl("div");
			div.Style.Add("padding", "0 8 0 8");
			div.Controls.Add(eventDisplay);

			return div;
		}

		private string eventDisplayControlSource;
		private string EventDisplayControlSource
		{
			get
			{
				if (eventDisplayControlSource == null)
				{
					if (this.DataSource.Count < 20)
						eventDisplayControlSource = "/Controls/EventDisplay/EventDisplay0.ascx";
					else if (this.DataSource.Count < 50)
						eventDisplayControlSource = "/Controls/EventDisplay/EventDisplay1.ascx";
					else
						eventDisplayControlSource = "/Controls/EventDisplay/EventDisplay2.ascx";
				}
				return eventDisplayControlSource;
			}
		}
	}
}
