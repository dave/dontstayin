//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Styled.Home
{
	public partial class View
		 : Js.StyledUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element EventLinkRepeater {get {if (_EventLinkRepeater == null) {_EventLinkRepeater = (Element)Document.GetElementById(clientId + "_EventLinkRepeater");}; return _EventLinkRepeater;}} private Element _EventLinkRepeater;
		public jQueryObject EventLinkRepeaterJ {get {if (_EventLinkRepeaterJ == null) {_EventLinkRepeaterJ = jQuery.Select("#" + clientId + "_EventLinkRepeater");}; return _EventLinkRepeaterJ;}} private jQueryObject _EventLinkRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element NoEventsLabel {get {if (_NoEventsLabel == null) {_NoEventsLabel = (Element)Document.GetElementById(clientId + "_NoEventsLabel");}; return _NoEventsLabel;}} private Element _NoEventsLabel;
		public jQueryObject NoEventsLabelJ {get {if (_NoEventsLabelJ == null) {_NoEventsLabelJ = jQuery.Select("#" + clientId + "_NoEventsLabel");}; return _NoEventsLabelJ;}} private jQueryObject _NoEventsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
