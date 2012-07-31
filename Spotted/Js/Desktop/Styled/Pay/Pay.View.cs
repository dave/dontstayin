//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Styled.Pay
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
		public Element EventHeader {get {if (_EventHeader == null) {_EventHeader = (Element)Document.GetElementById(clientId + "_EventHeader");}; return _EventHeader;}} private Element _EventHeader;
		public jQueryObject EventHeaderJ {get {if (_EventHeaderJ == null) {_EventHeaderJ = jQuery.Select("#" + clientId + "_EventHeader");}; return _EventHeaderJ;}} private jQueryObject _EventHeaderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element TicketDetails {get {if (_TicketDetails == null) {_TicketDetails = (Element)Document.GetElementById(clientId + "_TicketDetails");}; return _TicketDetails;}} private Element _TicketDetails;
		public jQueryObject TicketDetailsJ {get {if (_TicketDetailsJ == null) {_TicketDetailsJ = jQuery.Select("#" + clientId + "_TicketDetails");}; return _TicketDetailsJ;}} private jQueryObject _TicketDetailsJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element Payment {get {if (_Payment == null) {_Payment = (Element)Document.GetElementById(clientId + "_Payment");}; return _Payment;}} private Element _Payment;
		public jQueryObject PaymentJ {get {if (_PaymentJ == null) {_PaymentJ = jQuery.Select("#" + clientId + "_Payment");}; return _PaymentJ;}} private jQueryObject _PaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public Element ErrorMessageLabel {get {if (_ErrorMessageLabel == null) {_ErrorMessageLabel = (Element)Document.GetElementById(clientId + "_ErrorMessageLabel");}; return _ErrorMessageLabel;}} private Element _ErrorMessageLabel;
		public jQueryObject ErrorMessageLabelJ {get {if (_ErrorMessageLabelJ == null) {_ErrorMessageLabelJ = jQuery.Select("#" + clientId + "_ErrorMessageLabel");}; return _ErrorMessageLabelJ;}} private jQueryObject _ErrorMessageLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public InputElement CancelTicketPaymentButton {get {if (_CancelTicketPaymentButton == null) {_CancelTicketPaymentButton = (InputElement)Document.GetElementById(clientId + "_CancelTicketPaymentButton");}; return _CancelTicketPaymentButton;}} private InputElement _CancelTicketPaymentButton;
		public jQueryObject CancelTicketPaymentButtonJ {get {if (_CancelTicketPaymentButtonJ == null) {_CancelTicketPaymentButtonJ = jQuery.Select("#" + clientId + "_CancelTicketPaymentButton");}; return _CancelTicketPaymentButtonJ;}} private jQueryObject _CancelTicketPaymentButtonJ;
		public DivElement MyTicketsPanel {get {if (_MyTicketsPanel == null) {_MyTicketsPanel = (DivElement)Document.GetElementById(clientId + "_MyTicketsPanel");}; return _MyTicketsPanel;}} private DivElement _MyTicketsPanel;
		public jQueryObject MyTicketsPanelJ {get {if (_MyTicketsPanelJ == null) {_MyTicketsPanelJ = jQuery.Select("#" + clientId + "_MyTicketsPanel");}; return _MyTicketsPanelJ;}} private jQueryObject _MyTicketsPanelJ;
		public Element MyTicketsRepeater {get {if (_MyTicketsRepeater == null) {_MyTicketsRepeater = (Element)Document.GetElementById(clientId + "_MyTicketsRepeater");}; return _MyTicketsRepeater;}} private Element _MyTicketsRepeater;
		public jQueryObject MyTicketsRepeaterJ {get {if (_MyTicketsRepeaterJ == null) {_MyTicketsRepeaterJ = jQuery.Select("#" + clientId + "_MyTicketsRepeater");}; return _MyTicketsRepeaterJ;}} private jQueryObject _MyTicketsRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
