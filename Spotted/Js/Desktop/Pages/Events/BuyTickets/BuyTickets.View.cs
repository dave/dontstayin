//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Events.BuyTickets
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
		public DivElement EventSelectedPanel {get {if (_EventSelectedPanel == null) {_EventSelectedPanel = (DivElement)Document.GetElementById(clientId + "_EventSelectedPanel");}; return _EventSelectedPanel;}} private DivElement _EventSelectedPanel;
		public jQueryObject EventSelectedPanelJ {get {if (_EventSelectedPanelJ == null) {_EventSelectedPanelJ = jQuery.Select("#" + clientId + "_EventSelectedPanel");}; return _EventSelectedPanelJ;}} private jQueryObject _EventSelectedPanelJ;
		public DivElement PromoterPanel {get {if (_PromoterPanel == null) {_PromoterPanel = (DivElement)Document.GetElementById(clientId + "_PromoterPanel");}; return _PromoterPanel;}} private DivElement _PromoterPanel;
		public jQueryObject PromoterPanelJ {get {if (_PromoterPanelJ == null) {_PromoterPanelJ = jQuery.Select("#" + clientId + "_PromoterPanel");}; return _PromoterPanelJ;}} private jQueryObject _PromoterPanelJ;
		public DivElement PayForTicketsPanel {get {if (_PayForTicketsPanel == null) {_PayForTicketsPanel = (DivElement)Document.GetElementById(clientId + "_PayForTicketsPanel");}; return _PayForTicketsPanel;}} private DivElement _PayForTicketsPanel;
		public jQueryObject PayForTicketsPanelJ {get {if (_PayForTicketsPanelJ == null) {_PayForTicketsPanelJ = jQuery.Select("#" + clientId + "_PayForTicketsPanel");}; return _PayForTicketsPanelJ;}} private jQueryObject _PayForTicketsPanelJ;
		public Element PayForTicketsHeading {get {if (_PayForTicketsHeading == null) {_PayForTicketsHeading = (Element)Document.GetElementById(clientId + "_PayForTicketsHeading");}; return _PayForTicketsHeading;}} private Element _PayForTicketsHeading;
		public jQueryObject PayForTicketsHeadingJ {get {if (_PayForTicketsHeadingJ == null) {_PayForTicketsHeadingJ = jQuery.Select("#" + clientId + "_PayForTicketsHeading");}; return _PayForTicketsHeadingJ;}} private jQueryObject _PayForTicketsHeadingJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Payment {get {if (_Payment == null) {_Payment = (Element)Document.GetElementById(clientId + "_Payment");}; return _Payment;}} private Element _Payment;
		public jQueryObject PaymentJ {get {if (_PaymentJ == null) {_PaymentJ = jQuery.Select("#" + clientId + "_Payment");}; return _PaymentJ;}} private jQueryObject _PaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public Element ErrorMessageLabel {get {if (_ErrorMessageLabel == null) {_ErrorMessageLabel = (Element)Document.GetElementById(clientId + "_ErrorMessageLabel");}; return _ErrorMessageLabel;}} private Element _ErrorMessageLabel;
		public jQueryObject ErrorMessageLabelJ {get {if (_ErrorMessageLabelJ == null) {_ErrorMessageLabelJ = jQuery.Select("#" + clientId + "_ErrorMessageLabel");}; return _ErrorMessageLabelJ;}} private jQueryObject _ErrorMessageLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public InputElement CancelTicketPaymentButton {get {if (_CancelTicketPaymentButton == null) {_CancelTicketPaymentButton = (InputElement)Document.GetElementById(clientId + "_CancelTicketPaymentButton");}; return _CancelTicketPaymentButton;}} private InputElement _CancelTicketPaymentButton;
		public jQueryObject CancelTicketPaymentButtonJ {get {if (_CancelTicketPaymentButtonJ == null) {_CancelTicketPaymentButtonJ = jQuery.Select("#" + clientId + "_CancelTicketPaymentButton");}; return _CancelTicketPaymentButtonJ;}} private jQueryObject _CancelTicketPaymentButtonJ;
		public DivElement MyTicketsPanel {get {if (_MyTicketsPanel == null) {_MyTicketsPanel = (DivElement)Document.GetElementById(clientId + "_MyTicketsPanel");}; return _MyTicketsPanel;}} private DivElement _MyTicketsPanel;
		public jQueryObject MyTicketsPanelJ {get {if (_MyTicketsPanelJ == null) {_MyTicketsPanelJ = jQuery.Select("#" + clientId + "_MyTicketsPanel");}; return _MyTicketsPanelJ;}} private jQueryObject _MyTicketsPanelJ;
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element MyTicketsRepeater {get {if (_MyTicketsRepeater == null) {_MyTicketsRepeater = (Element)Document.GetElementById(clientId + "_MyTicketsRepeater");}; return _MyTicketsRepeater;}} private Element _MyTicketsRepeater;
		public jQueryObject MyTicketsRepeaterJ {get {if (_MyTicketsRepeaterJ == null) {_MyTicketsRepeaterJ = jQuery.Select("#" + clientId + "_MyTicketsRepeater");}; return _MyTicketsRepeaterJ;}} private jQueryObject _MyTicketsRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
