//mappings.Add("Spotted.Pages.TicketsCalendarContent", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.TicketsCalendar
{
	public partial class View
		 : Js.DsiUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element TicketsCalendarContent {get {if (_TicketsCalendarContent == null) {_TicketsCalendarContent = (Element)Document.GetElementById(clientId + "_TicketsCalendarContent");}; return _TicketsCalendarContent;}} private Element _TicketsCalendarContent;
		public jQueryObject TicketsCalendarContentJ {get {if (_TicketsCalendarContentJ == null) {_TicketsCalendarContentJ = jQuery.Select("#" + clientId + "_TicketsCalendarContent");}; return _TicketsCalendarContentJ;}} private jQueryObject _TicketsCalendarContentJ;//mappings.Add("Spotted.Pages.TicketsCalendarContent", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
