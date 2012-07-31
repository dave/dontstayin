//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.EventHighlight
{
	public partial class View
		 : Js.Pages.Promoters.PromoterUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element PromoterIntro {get {if (_PromoterIntro == null) {_PromoterIntro = (Element)Document.GetElementById(clientId + "_PromoterIntro");}; return _PromoterIntro;}} private Element _PromoterIntro;
		public jQueryObject PromoterIntroJ {get {if (_PromoterIntroJ == null) {_PromoterIntroJ = jQuery.Select("#" + clientId + "_PromoterIntro");}; return _PromoterIntroJ;}} private jQueryObject _PromoterIntroJ;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public AnchorElement IntroBannerListLink {get {if (_IntroBannerListLink == null) {_IntroBannerListLink = (AnchorElement)Document.GetElementById(clientId + "_IntroBannerListLink");}; return _IntroBannerListLink;}} private AnchorElement _IntroBannerListLink;
		public jQueryObject IntroBannerListLinkJ {get {if (_IntroBannerListLinkJ == null) {_IntroBannerListLinkJ = jQuery.Select("#" + clientId + "_IntroBannerListLink");}; return _IntroBannerListLinkJ;}} private jQueryObject _IntroBannerListLinkJ;
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H15 {get {if (_H15 == null) {_H15 = (Element)Document.GetElementById(clientId + "_H15");}; return _H15;}} private Element _H15;
		public jQueryObject H15J {get {if (_H15J == null) {_H15J = jQuery.Select("#" + clientId + "_H15");}; return _H15J;}} private jQueryObject _H15J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement Button2 {get {if (_Button2 == null) {_Button2 = (InputElement)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private InputElement _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;
		public Element H16 {get {if (_H16 == null) {_H16 = (Element)Document.GetElementById(clientId + "_H16");}; return _H16;}} private Element _H16;
		public jQueryObject H16J {get {if (_H16J == null) {_H16J = jQuery.Select("#" + clientId + "_H16");}; return _H16J;}} private jQueryObject _H16J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element RecommendedCell {get {if (_RecommendedCell == null) {_RecommendedCell = (Element)Document.GetElementById(clientId + "_RecommendedCell");}; return _RecommendedCell;}} private Element _RecommendedCell;
		public jQueryObject RecommendedCellJ {get {if (_RecommendedCellJ == null) {_RecommendedCellJ = jQuery.Select("#" + clientId + "_RecommendedCell");}; return _RecommendedCellJ;}} private jQueryObject _RecommendedCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element RecommendedCellPrice {get {if (_RecommendedCellPrice == null) {_RecommendedCellPrice = (Element)Document.GetElementById(clientId + "_RecommendedCellPrice");}; return _RecommendedCellPrice;}} private Element _RecommendedCellPrice;
		public jQueryObject RecommendedCellPriceJ {get {if (_RecommendedCellPriceJ == null) {_RecommendedCellPriceJ = jQuery.Select("#" + clientId + "_RecommendedCellPrice");}; return _RecommendedCellPriceJ;}} private jQueryObject _RecommendedCellPriceJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element Payment {get {if (_Payment == null) {_Payment = (Element)Document.GetElementById(clientId + "_Payment");}; return _Payment;}} private Element _Payment;
		public jQueryObject PaymentJ {get {if (_PaymentJ == null) {_PaymentJ = jQuery.Select("#" + clientId + "_Payment");}; return _PaymentJ;}} private jQueryObject _PaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public DivElement ChoicePanel {get {if (_ChoicePanel == null) {_ChoicePanel = (DivElement)Document.GetElementById(clientId + "_ChoicePanel");}; return _ChoicePanel;}} private DivElement _ChoicePanel;
		public jQueryObject ChoicePanelJ {get {if (_ChoicePanelJ == null) {_ChoicePanelJ = jQuery.Select("#" + clientId + "_ChoicePanel");}; return _ChoicePanelJ;}} private jQueryObject _ChoicePanelJ;
		public DivElement PayPanel {get {if (_PayPanel == null) {_PayPanel = (DivElement)Document.GetElementById(clientId + "_PayPanel");}; return _PayPanel;}} private DivElement _PayPanel;
		public jQueryObject PayPanelJ {get {if (_PayPanelJ == null) {_PayPanelJ = jQuery.Select("#" + clientId + "_PayPanel");}; return _PayPanelJ;}} private jQueryObject _PayPanelJ;
		public DivElement PayDonePanel {get {if (_PayDonePanel == null) {_PayDonePanel = (DivElement)Document.GetElementById(clientId + "_PayDonePanel");}; return _PayDonePanel;}} private DivElement _PayDonePanel;
		public jQueryObject PayDonePanelJ {get {if (_PayDonePanelJ == null) {_PayDonePanelJ = jQuery.Select("#" + clientId + "_PayDonePanel");}; return _PayDonePanelJ;}} private jQueryObject _PayDonePanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
