//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Styled.TicketConfirmation
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
		public Element TicketConfirmationLabel {get {if (_TicketConfirmationLabel == null) {_TicketConfirmationLabel = (Element)Document.GetElementById(clientId + "_TicketConfirmationLabel");}; return _TicketConfirmationLabel;}} private Element _TicketConfirmationLabel;
		public jQueryObject TicketConfirmationLabelJ {get {if (_TicketConfirmationLabelJ == null) {_TicketConfirmationLabelJ = jQuery.Select("#" + clientId + "_TicketConfirmationLabel");}; return _TicketConfirmationLabelJ;}} private jQueryObject _TicketConfirmationLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element EventLinkLabel {get {if (_EventLinkLabel == null) {_EventLinkLabel = (Element)Document.GetElementById(clientId + "_EventLinkLabel");}; return _EventLinkLabel;}} private Element _EventLinkLabel;
		public jQueryObject EventLinkLabelJ {get {if (_EventLinkLabelJ == null) {_EventLinkLabelJ = jQuery.Select("#" + clientId + "_EventLinkLabel");}; return _EventLinkLabelJ;}} private jQueryObject _EventLinkLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
