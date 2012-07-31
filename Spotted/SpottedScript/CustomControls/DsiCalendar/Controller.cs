using System;
using JQ;
using System.DHTML;
using SpottedScript.Controls.EventCreator;
using EventCreatorController = SpottedScript.Controls.EventCreator.Controller;

namespace SpottedScript.CustomControls.DsiCalendar
{
	
	public class Controller
	{
		public Controller(View view)
		{
			JQueryAPI.JQuery((object) Window.Document).ready(OnReady);
		}

		private DOMElement clicked = null;
		private void OnReady()
		{
			JQueryAPI.JQuery(".CalendarAddLink").click(delegate(object o)
			{
				clicked = (DOMElement) Script.Literal("{0}.originalEvent.srcElement.parentElement", o);
				DateTime dt = new DateTime((string) Script.Literal("{0}.date", clicked));
				EventCreatorController.Instance.ShowPopup(dt, null, null, EventAdded);
				return false;
			});

		}

		private void EventAdded(EventInfo eventInfo)
		{
			if (eventInfo != null)
			{
				DivElement div = (DivElement) Document.CreateElement("div");
				div.ClassName = "CalendarItemToday";
				div.InnerHTML = String.Format("<b>{0}</b>", eventInfo.name);
				clicked.InsertBefore(div);
				clicked = null;
			}
		}
	}
}
