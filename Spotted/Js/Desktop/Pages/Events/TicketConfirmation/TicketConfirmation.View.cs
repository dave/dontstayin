//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Events.TicketConfirmation
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
		public DivElement EventTicketConfirmationPanel {get {if (_EventTicketConfirmationPanel == null) {_EventTicketConfirmationPanel = (DivElement)Document.GetElementById(clientId + "_EventTicketConfirmationPanel");}; return _EventTicketConfirmationPanel;}} private DivElement _EventTicketConfirmationPanel;
		public jQueryObject EventTicketConfirmationPanelJ {get {if (_EventTicketConfirmationPanelJ == null) {_EventTicketConfirmationPanelJ = jQuery.Select("#" + clientId + "_EventTicketConfirmationPanel");}; return _EventTicketConfirmationPanelJ;}} private jQueryObject _EventTicketConfirmationPanelJ;
		public Element TicketsHeading {get {if (_TicketsHeading == null) {_TicketsHeading = (Element)Document.GetElementById(clientId + "_TicketsHeading");}; return _TicketsHeading;}} private Element _TicketsHeading;
		public jQueryObject TicketsHeadingJ {get {if (_TicketsHeadingJ == null) {_TicketsHeadingJ = jQuery.Select("#" + clientId + "_TicketsHeading");}; return _TicketsHeadingJ;}} private jQueryObject _TicketsHeadingJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element TicketConfirmationLabel {get {if (_TicketConfirmationLabel == null) {_TicketConfirmationLabel = (Element)Document.GetElementById(clientId + "_TicketConfirmationLabel");}; return _TicketConfirmationLabel;}} private Element _TicketConfirmationLabel;
		public jQueryObject TicketConfirmationLabelJ {get {if (_TicketConfirmationLabelJ == null) {_TicketConfirmationLabelJ = jQuery.Select("#" + clientId + "_TicketConfirmationLabel");}; return _TicketConfirmationLabelJ;}} private jQueryObject _TicketConfirmationLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
