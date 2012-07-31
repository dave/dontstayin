//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.BannersPending
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
		public Element H1Title {get {if (_H1Title == null) {_H1Title = (Element)Document.GetElementById(clientId + "_H1Title");}; return _H1Title;}} private Element _H1Title;
		public jQueryObject H1TitleJ {get {if (_H1TitleJ == null) {_H1TitleJ = jQuery.Select("#" + clientId + "_H1Title");}; return _H1TitleJ;}} private jQueryObject _H1TitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element NoPendingBannersLabel {get {if (_NoPendingBannersLabel == null) {_NoPendingBannersLabel = (Element)Document.GetElementById(clientId + "_NoPendingBannersLabel");}; return _NoPendingBannersLabel;}} private Element _NoPendingBannersLabel;
		public jQueryObject NoPendingBannersLabelJ {get {if (_NoPendingBannersLabelJ == null) {_NoPendingBannersLabelJ = jQuery.Select("#" + clientId + "_NoPendingBannersLabel");}; return _NoPendingBannersLabelJ;}} private jQueryObject _NoPendingBannersLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement BookBannersPanel {get {if (_BookBannersPanel == null) {_BookBannersPanel = (DivElement)Document.GetElementById(clientId + "_BookBannersPanel");}; return _BookBannersPanel;}} private DivElement _BookBannersPanel;
		public jQueryObject BookBannersPanelJ {get {if (_BookBannersPanelJ == null) {_BookBannersPanelJ = jQuery.Select("#" + clientId + "_BookBannersPanel");}; return _BookBannersPanelJ;}} private jQueryObject _BookBannersPanelJ;
		public Element BannerGrid {get {if (_BannerGrid == null) {_BannerGrid = (Element)Document.GetElementById(clientId + "_BannerGrid");}; return _BannerGrid;}} private Element _BannerGrid;
		public jQueryObject BannerGridJ {get {if (_BannerGridJ == null) {_BannerGridJ = jQuery.Select("#" + clientId + "_BannerGrid");}; return _BannerGridJ;}} private jQueryObject _BannerGridJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public InputElement BookBannersButton {get {if (_BookBannersButton == null) {_BookBannersButton = (InputElement)Document.GetElementById(clientId + "_BookBannersButton");}; return _BookBannersButton;}} private InputElement _BookBannersButton;
		public jQueryObject BookBannersButtonJ {get {if (_BookBannersButtonJ == null) {_BookBannersButtonJ = jQuery.Select("#" + clientId + "_BookBannersButton");}; return _BookBannersButtonJ;}} private jQueryObject _BookBannersButtonJ;
		public Element EnsureBannersSelectedValidator {get {if (_EnsureBannersSelectedValidator == null) {_EnsureBannersSelectedValidator = (Element)Document.GetElementById(clientId + "_EnsureBannersSelectedValidator");}; return _EnsureBannersSelectedValidator;}} private Element _EnsureBannersSelectedValidator;
		public jQueryObject EnsureBannersSelectedValidatorJ {get {if (_EnsureBannersSelectedValidatorJ == null) {_EnsureBannersSelectedValidatorJ = jQuery.Select("#" + clientId + "_EnsureBannersSelectedValidator");}; return _EnsureBannersSelectedValidatorJ;}} private jQueryObject _EnsureBannersSelectedValidatorJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public DivElement PaymentPanel {get {if (_PaymentPanel == null) {_PaymentPanel = (DivElement)Document.GetElementById(clientId + "_PaymentPanel");}; return _PaymentPanel;}} private DivElement _PaymentPanel;
		public jQueryObject PaymentPanelJ {get {if (_PaymentPanelJ == null) {_PaymentPanelJ = jQuery.Select("#" + clientId + "_PaymentPanel");}; return _PaymentPanelJ;}} private jQueryObject _PaymentPanelJ;
		public Element Payment {get {if (_Payment == null) {_Payment = (Element)Document.GetElementById(clientId + "_Payment");}; return _Payment;}} private Element _Payment;
		public jQueryObject PaymentJ {get {if (_PaymentJ == null) {_PaymentJ = jQuery.Select("#" + clientId + "_Payment");}; return _PaymentJ;}} private jQueryObject _PaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public InputElement CancelButton {get {if (_CancelButton == null) {_CancelButton = (InputElement)Document.GetElementById(clientId + "_CancelButton");}; return _CancelButton;}} private InputElement _CancelButton;
		public jQueryObject CancelButtonJ {get {if (_CancelButtonJ == null) {_CancelButtonJ = jQuery.Select("#" + clientId + "_CancelButton");}; return _CancelButtonJ;}} private jQueryObject _CancelButtonJ;
		public DivElement ConfirmedPanel {get {if (_ConfirmedPanel == null) {_ConfirmedPanel = (DivElement)Document.GetElementById(clientId + "_ConfirmedPanel");}; return _ConfirmedPanel;}} private DivElement _ConfirmedPanel;
		public jQueryObject ConfirmedPanelJ {get {if (_ConfirmedPanelJ == null) {_ConfirmedPanelJ = jQuery.Select("#" + clientId + "_ConfirmedPanel");}; return _ConfirmedPanelJ;}} private jQueryObject _ConfirmedPanelJ;
		public Element BookedBannersGridView {get {if (_BookedBannersGridView == null) {_BookedBannersGridView = (Element)Document.GetElementById(clientId + "_BookedBannersGridView");}; return _BookedBannersGridView;}} private Element _BookedBannersGridView;
		public jQueryObject BookedBannersGridViewJ {get {if (_BookedBannersGridViewJ == null) {_BookedBannersGridViewJ = jQuery.Select("#" + clientId + "_BookedBannersGridView");}; return _BookedBannersGridViewJ;}} private jQueryObject _BookedBannersGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
