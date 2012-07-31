//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Table", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.CampaignCredits
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
		public DivElement CreditsPanel {get {if (_CreditsPanel == null) {_CreditsPanel = (DivElement)Document.GetElementById(clientId + "_CreditsPanel");}; return _CreditsPanel;}} private DivElement _CreditsPanel;
		public jQueryObject CreditsPanelJ {get {if (_CreditsPanelJ == null) {_CreditsPanelJ = jQuery.Select("#" + clientId + "_CreditsPanel");}; return _CreditsPanelJ;}} private jQueryObject _CreditsPanelJ;
		public InputElement SelectedCredits {get {if (_SelectedCredits == null) {_SelectedCredits = (InputElement)Document.GetElementById(clientId + "_SelectedCredits");}; return _SelectedCredits;}} private InputElement _SelectedCredits;
		public jQueryObject SelectedCreditsJ {get {if (_SelectedCreditsJ == null) {_SelectedCreditsJ = jQuery.Select("#" + clientId + "_SelectedCredits");}; return _SelectedCreditsJ;}} private jQueryObject _SelectedCreditsJ;
		public Element CreditsTable {get {if (_CreditsTable == null) {_CreditsTable = (Element)Document.GetElementById(clientId + "_CreditsTable");}; return _CreditsTable;}} private Element _CreditsTable;
		public jQueryObject CreditsTableJ {get {if (_CreditsTableJ == null) {_CreditsTableJ = jQuery.Select("#" + clientId + "_CreditsTable");}; return _CreditsTableJ;}} private jQueryObject _CreditsTableJ;//mappings.Add("System.Web.UI.WebControls.Table", ElementGetter("Element"));
		public Element BuyButton {get {if (_BuyButton == null) {_BuyButton = (Element)Document.GetElementById(clientId + "_BuyButton");}; return _BuyButton;}} private Element _BuyButton;
		public jQueryObject BuyButtonJ {get {if (_BuyButtonJ == null) {_BuyButtonJ = jQuery.Select("#" + clientId + "_BuyButton");}; return _BuyButtonJ;}} private jQueryObject _BuyButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element EnsureCampaignCreditsSelectedValidator {get {if (_EnsureCampaignCreditsSelectedValidator == null) {_EnsureCampaignCreditsSelectedValidator = (Element)Document.GetElementById(clientId + "_EnsureCampaignCreditsSelectedValidator");}; return _EnsureCampaignCreditsSelectedValidator;}} private Element _EnsureCampaignCreditsSelectedValidator;
		public jQueryObject EnsureCampaignCreditsSelectedValidatorJ {get {if (_EnsureCampaignCreditsSelectedValidatorJ == null) {_EnsureCampaignCreditsSelectedValidatorJ = jQuery.Select("#" + clientId + "_EnsureCampaignCreditsSelectedValidator");}; return _EnsureCampaignCreditsSelectedValidatorJ;}} private jQueryObject _EnsureCampaignCreditsSelectedValidatorJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element CustomCampaignCreditsCustomValidator {get {if (_CustomCampaignCreditsCustomValidator == null) {_CustomCampaignCreditsCustomValidator = (Element)Document.GetElementById(clientId + "_CustomCampaignCreditsCustomValidator");}; return _CustomCampaignCreditsCustomValidator;}} private Element _CustomCampaignCreditsCustomValidator;
		public jQueryObject CustomCampaignCreditsCustomValidatorJ {get {if (_CustomCampaignCreditsCustomValidatorJ == null) {_CustomCampaignCreditsCustomValidatorJ = jQuery.Select("#" + clientId + "_CustomCampaignCreditsCustomValidator");}; return _CustomCampaignCreditsCustomValidatorJ;}} private jQueryObject _CustomCampaignCreditsCustomValidatorJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public DivElement PaymentPanel {get {if (_PaymentPanel == null) {_PaymentPanel = (DivElement)Document.GetElementById(clientId + "_PaymentPanel");}; return _PaymentPanel;}} private DivElement _PaymentPanel;
		public jQueryObject PaymentPanelJ {get {if (_PaymentPanelJ == null) {_PaymentPanelJ = jQuery.Select("#" + clientId + "_PaymentPanel");}; return _PaymentPanelJ;}} private jQueryObject _PaymentPanelJ;
		public Element Payment {get {if (_Payment == null) {_Payment = (Element)Document.GetElementById(clientId + "_Payment");}; return _Payment;}} private Element _Payment;
		public jQueryObject PaymentJ {get {if (_PaymentJ == null) {_PaymentJ = jQuery.Select("#" + clientId + "_Payment");}; return _PaymentJ;}} private jQueryObject _PaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public Element AdminPriceEditP {get {if (_AdminPriceEditP == null) {_AdminPriceEditP = (Element)Document.GetElementById(clientId + "_AdminPriceEditP");}; return _AdminPriceEditP;}} private Element _AdminPriceEditP;
		public jQueryObject AdminPriceEditPJ {get {if (_AdminPriceEditPJ == null) {_AdminPriceEditPJ = jQuery.Select("#" + clientId + "_AdminPriceEditP");}; return _AdminPriceEditPJ;}} private jQueryObject _AdminPriceEditPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement FixPriceTextBox {get {if (_FixPriceTextBox == null) {_FixPriceTextBox = (InputElement)Document.GetElementById(clientId + "_FixPriceTextBox");}; return _FixPriceTextBox;}} private InputElement _FixPriceTextBox;
		public jQueryObject FixPriceTextBoxJ {get {if (_FixPriceTextBoxJ == null) {_FixPriceTextBoxJ = jQuery.Select("#" + clientId + "_FixPriceTextBox");}; return _FixPriceTextBoxJ;}} private jQueryObject _FixPriceTextBoxJ;
		public InputElement FixPriceExVatButton {get {if (_FixPriceExVatButton == null) {_FixPriceExVatButton = (InputElement)Document.GetElementById(clientId + "_FixPriceExVatButton");}; return _FixPriceExVatButton;}} private InputElement _FixPriceExVatButton;
		public jQueryObject FixPriceExVatButtonJ {get {if (_FixPriceExVatButtonJ == null) {_FixPriceExVatButtonJ = jQuery.Select("#" + clientId + "_FixPriceExVatButton");}; return _FixPriceExVatButtonJ;}} private jQueryObject _FixPriceExVatButtonJ;
		public InputElement FixPriceIncVatButton {get {if (_FixPriceIncVatButton == null) {_FixPriceIncVatButton = (InputElement)Document.GetElementById(clientId + "_FixPriceIncVatButton");}; return _FixPriceIncVatButton;}} private InputElement _FixPriceIncVatButton;
		public jQueryObject FixPriceIncVatButtonJ {get {if (_FixPriceIncVatButtonJ == null) {_FixPriceIncVatButtonJ = jQuery.Select("#" + clientId + "_FixPriceIncVatButton");}; return _FixPriceIncVatButtonJ;}} private jQueryObject _FixPriceIncVatButtonJ;
		public InputElement FixPriceDiscountButton {get {if (_FixPriceDiscountButton == null) {_FixPriceDiscountButton = (InputElement)Document.GetElementById(clientId + "_FixPriceDiscountButton");}; return _FixPriceDiscountButton;}} private InputElement _FixPriceDiscountButton;
		public jQueryObject FixPriceDiscountButtonJ {get {if (_FixPriceDiscountButtonJ == null) {_FixPriceDiscountButtonJ = jQuery.Select("#" + clientId + "_FixPriceDiscountButton");}; return _FixPriceDiscountButtonJ;}} private jQueryObject _FixPriceDiscountButtonJ;
		public InputElement ClearFixDiscountButton {get {if (_ClearFixDiscountButton == null) {_ClearFixDiscountButton = (InputElement)Document.GetElementById(clientId + "_ClearFixDiscountButton");}; return _ClearFixDiscountButton;}} private InputElement _ClearFixDiscountButton;
		public jQueryObject ClearFixDiscountButtonJ {get {if (_ClearFixDiscountButtonJ == null) {_ClearFixDiscountButtonJ = jQuery.Select("#" + clientId + "_ClearFixDiscountButton");}; return _ClearFixDiscountButtonJ;}} private jQueryObject _ClearFixDiscountButtonJ;
		public Element BackToCreditOptionsButton {get {if (_BackToCreditOptionsButton == null) {_BackToCreditOptionsButton = (Element)Document.GetElementById(clientId + "_BackToCreditOptionsButton");}; return _BackToCreditOptionsButton;}} private Element _BackToCreditOptionsButton;
		public jQueryObject BackToCreditOptionsButtonJ {get {if (_BackToCreditOptionsButtonJ == null) {_BackToCreditOptionsButtonJ = jQuery.Select("#" + clientId + "_BackToCreditOptionsButton");}; return _BackToCreditOptionsButtonJ;}} private jQueryObject _BackToCreditOptionsButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement SuccessPanel {get {if (_SuccessPanel == null) {_SuccessPanel = (DivElement)Document.GetElementById(clientId + "_SuccessPanel");}; return _SuccessPanel;}} private DivElement _SuccessPanel;
		public jQueryObject SuccessPanelJ {get {if (_SuccessPanelJ == null) {_SuccessPanelJ = jQuery.Select("#" + clientId + "_SuccessPanel");}; return _SuccessPanelJ;}} private jQueryObject _SuccessPanelJ;
		public CheckBoxElement CustomRadioButton {get {if (_CustomRadioButton == null) {_CustomRadioButton = (CheckBoxElement)Document.GetElementById(clientId + "_CustomRadioButton");}; return _CustomRadioButton;}} private CheckBoxElement _CustomRadioButton;
		public jQueryObject CustomRadioButtonJ {get {if (_CustomRadioButtonJ == null) {_CustomRadioButtonJ = jQuery.Select("#" + clientId + "_CustomRadioButton");}; return _CustomRadioButtonJ;}} private jQueryObject _CustomRadioButtonJ;
		public InputElement CustomCreditsTextBox {get {if (_CustomCreditsTextBox == null) {_CustomCreditsTextBox = (InputElement)Document.GetElementById(clientId + "_CustomCreditsTextBox");}; return _CustomCreditsTextBox;}} private InputElement _CustomCreditsTextBox;
		public jQueryObject CustomCreditsTextBoxJ {get {if (_CustomCreditsTextBoxJ == null) {_CustomCreditsTextBoxJ = jQuery.Select("#" + clientId + "_CustomCreditsTextBox");}; return _CustomCreditsTextBoxJ;}} private jQueryObject _CustomCreditsTextBoxJ;
		public Element CustomPriceLabel {get {if (_CustomPriceLabel == null) {_CustomPriceLabel = (Element)Document.GetElementById(clientId + "_CustomPriceLabel");}; return _CustomPriceLabel;}} private Element _CustomPriceLabel;
		public jQueryObject CustomPriceLabelJ {get {if (_CustomPriceLabelJ == null) {_CustomPriceLabelJ = jQuery.Select("#" + clientId + "_CustomPriceLabel");}; return _CustomPriceLabelJ;}} private jQueryObject _CustomPriceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element CustomDiscountLabel {get {if (_CustomDiscountLabel == null) {_CustomDiscountLabel = (Element)Document.GetElementById(clientId + "_CustomDiscountLabel");}; return _CustomDiscountLabel;}} private Element _CustomDiscountLabel;
		public jQueryObject CustomDiscountLabelJ {get {if (_CustomDiscountLabelJ == null) {_CustomDiscountLabelJ = jQuery.Select("#" + clientId + "_CustomDiscountLabel");}; return _CustomDiscountLabelJ;}} private jQueryObject _CustomDiscountLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public InputElement CustomTotalTextBox {get {if (_CustomTotalTextBox == null) {_CustomTotalTextBox = (InputElement)Document.GetElementById(clientId + "_CustomTotalTextBox");}; return _CustomTotalTextBox;}} private InputElement _CustomTotalTextBox;
		public jQueryObject CustomTotalTextBoxJ {get {if (_CustomTotalTextBoxJ == null) {_CustomTotalTextBoxJ = jQuery.Select("#" + clientId + "_CustomTotalTextBox");}; return _CustomTotalTextBoxJ;}} private jQueryObject _CustomTotalTextBoxJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
