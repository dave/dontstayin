//mappings.Add("Spotted.Pages.CalendarTicketsContent", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.CalendarTickets
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
		public Element CalendarTicketsContent {get {if (_CalendarTicketsContent == null) {_CalendarTicketsContent = (Element)Document.GetElementById(clientId + "_CalendarTicketsContent");}; return _CalendarTicketsContent;}} private Element _CalendarTicketsContent;
		public jQueryObject CalendarTicketsContentJ {get {if (_CalendarTicketsContentJ == null) {_CalendarTicketsContentJ = jQuery.Select("#" + clientId + "_CalendarTicketsContent");}; return _CalendarTicketsContentJ;}} private jQueryObject _CalendarTicketsContentJ;//mappings.Add("Spotted.Pages.CalendarTicketsContent", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
