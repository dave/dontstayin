//mappings.Add("Spotted.Controls.DonateText.DonateTextControl", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Literal", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Icons
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
		public DivElement DonateLoggedOut {get {if (_DonateLoggedOut == null) {_DonateLoggedOut = (DivElement)Document.GetElementById(clientId + "_DonateLoggedOut");}; return _DonateLoggedOut;}} private DivElement _DonateLoggedOut;
		public jQueryObject DonateLoggedOutJ {get {if (_DonateLoggedOutJ == null) {_DonateLoggedOutJ = jQuery.Select("#" + clientId + "_DonateLoggedOut");}; return _DonateLoggedOutJ;}} private jQueryObject _DonateLoggedOutJ;
		public Element uiDonateText {get {if (_uiDonateText == null) {_uiDonateText = (Element)Document.GetElementById(clientId + "_uiDonateText");}; return _uiDonateText;}} private Element _uiDonateText;
		public jQueryObject uiDonateTextJ {get {if (_uiDonateTextJ == null) {_uiDonateTextJ = jQuery.Select("#" + clientId + "_uiDonateText");}; return _uiDonateTextJ;}} private jQueryObject _uiDonateTextJ;//mappings.Add("Spotted.Controls.DonateText.DonateTextControl", ElementGetter("Element"));
		public DivElement DonateLoggedIn {get {if (_DonateLoggedIn == null) {_DonateLoggedIn = (DivElement)Document.GetElementById(clientId + "_DonateLoggedIn");}; return _DonateLoggedIn;}} private DivElement _DonateLoggedIn;
		public jQueryObject DonateLoggedInJ {get {if (_DonateLoggedInJ == null) {_DonateLoggedInJ = jQuery.Select("#" + clientId + "_DonateLoggedIn");}; return _DonateLoggedInJ;}} private jQueryObject _DonateLoggedInJ;
		public Element Payment {get {if (_Payment == null) {_Payment = (Element)Document.GetElementById(clientId + "_Payment");}; return _Payment;}} private Element _Payment;
		public jQueryObject PaymentJ {get {if (_PaymentJ == null) {_PaymentJ = jQuery.Select("#" + clientId + "_Payment");}; return _PaymentJ;}} private jQueryObject _PaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public DivElement DonatedMessagePanel {get {if (_DonatedMessagePanel == null) {_DonatedMessagePanel = (DivElement)Document.GetElementById(clientId + "_DonatedMessagePanel");}; return _DonatedMessagePanel;}} private DivElement _DonatedMessagePanel;
		public jQueryObject DonatedMessagePanelJ {get {if (_DonatedMessagePanelJ == null) {_DonatedMessagePanelJ = jQuery.Select("#" + clientId + "_DonatedMessagePanel");}; return _DonatedMessagePanelJ;}} private jQueryObject _DonatedMessagePanelJ;
		public DivElement DonateRemainPanel {get {if (_DonateRemainPanel == null) {_DonateRemainPanel = (DivElement)Document.GetElementById(clientId + "_DonateRemainPanel");}; return _DonateRemainPanel;}} private DivElement _DonateRemainPanel;
		public jQueryObject DonateRemainPanelJ {get {if (_DonateRemainPanelJ == null) {_DonateRemainPanelJ = jQuery.Select("#" + clientId + "_DonateRemainPanel");}; return _DonateRemainPanelJ;}} private jQueryObject _DonateRemainPanelJ;
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element DonationIconsHtml {get {if (_DonationIconsHtml == null) {_DonationIconsHtml = (Element)Document.GetElementById(clientId + "_DonationIconsHtml");}; return _DonationIconsHtml;}} private Element _DonationIconsHtml;
		public jQueryObject DonationIconsHtmlJ {get {if (_DonationIconsHtmlJ == null) {_DonationIconsHtmlJ = jQuery.Select("#" + clientId + "_DonationIconsHtml");}; return _DonationIconsHtmlJ;}} private jQueryObject _DonationIconsHtmlJ;//mappings.Add("System.Web.UI.WebControls.Literal", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
