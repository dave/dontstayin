//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Events.Buy
{
	public partial class View
		 : Js.Pages.Events.EventUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement TicketsPlaceholder {get {if (_TicketsPlaceholder == null) {_TicketsPlaceholder = (DivElement)Document.GetElementById(clientId + "_TicketsPlaceholder");}; return _TicketsPlaceholder;}} private DivElement _TicketsPlaceholder;
		public jQueryObject TicketsPlaceholderJ {get {if (_TicketsPlaceholderJ == null) {_TicketsPlaceholderJ = jQuery.Select("#" + clientId + "_TicketsPlaceholder");}; return _TicketsPlaceholderJ;}} private jQueryObject _TicketsPlaceholderJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
