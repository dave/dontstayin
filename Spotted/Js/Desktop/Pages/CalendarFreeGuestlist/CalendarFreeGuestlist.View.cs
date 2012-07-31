//mappings.Add("Spotted.Pages.CalendarFreeGuestlistContent", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.CalendarFreeGuestlist
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
		public Element CalendarFreeGuestlistContent {get {if (_CalendarFreeGuestlistContent == null) {_CalendarFreeGuestlistContent = (Element)Document.GetElementById(clientId + "_CalendarFreeGuestlistContent");}; return _CalendarFreeGuestlistContent;}} private Element _CalendarFreeGuestlistContent;
		public jQueryObject CalendarFreeGuestlistContentJ {get {if (_CalendarFreeGuestlistContentJ == null) {_CalendarFreeGuestlistContentJ = jQuery.Select("#" + clientId + "_CalendarFreeGuestlistContent");}; return _CalendarFreeGuestlistContentJ;}} private jQueryObject _CalendarFreeGuestlistContentJ;//mappings.Add("Spotted.Pages.CalendarFreeGuestlistContent", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
