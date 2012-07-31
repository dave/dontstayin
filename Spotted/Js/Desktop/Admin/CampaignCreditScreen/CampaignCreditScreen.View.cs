//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RangeValidator", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.AddOnlyTextBox", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.CampaignCreditScreen
{
	public partial class View
		 : Js.AdminUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element H1_1 {get {if (_H1_1 == null) {_H1_1 = (Element)Document.GetElementById(clientId + "_H1_1");}; return _H1_1;}} private Element _H1_1;
		public jQueryObject H1_1J {get {if (_H1_1J == null) {_H1_1J = jQuery.Select("#" + clientId + "_H1_1");}; return _H1_1J;}} private jQueryObject _H1_1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement MainPanel {get {if (_MainPanel == null) {_MainPanel = (DivElement)Document.GetElementById(clientId + "_MainPanel");}; return _MainPanel;}} private DivElement _MainPanel;
		public jQueryObject MainPanelJ {get {if (_MainPanelJ == null) {_MainPanelJ = jQuery.Select("#" + clientId + "_MainPanel");}; return _MainPanelJ;}} private jQueryObject _MainPanelJ;
		public Element CampaignCreditKLabel {get {if (_CampaignCreditKLabel == null) {_CampaignCreditKLabel = (Element)Document.GetElementById(clientId + "_CampaignCreditKLabel");}; return _CampaignCreditKLabel;}} private Element _CampaignCreditKLabel;
		public jQueryObject CampaignCreditKLabelJ {get {if (_CampaignCreditKLabelJ == null) {_CampaignCreditKLabelJ = jQuery.Select("#" + clientId + "_CampaignCreditKLabel");}; return _CampaignCreditKLabelJ;}} private jQueryObject _CampaignCreditKLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ActionDateTimeLabel {get {if (_ActionDateTimeLabel == null) {_ActionDateTimeLabel = (Element)Document.GetElementById(clientId + "_ActionDateTimeLabel");}; return _ActionDateTimeLabel;}} private Element _ActionDateTimeLabel;
		public jQueryObject ActionDateTimeLabelJ {get {if (_ActionDateTimeLabelJ == null) {_ActionDateTimeLabelJ = jQuery.Select("#" + clientId + "_ActionDateTimeLabel");}; return _ActionDateTimeLabelJ;}} private jQueryObject _ActionDateTimeLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPromotersAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPromotersAutoCompleteBehaviour");}}
		public Element PromoterValueLabel {get {if (_PromoterValueLabel == null) {_PromoterValueLabel = (Element)Document.GetElementById(clientId + "_PromoterValueLabel");}; return _PromoterValueLabel;}} private Element _PromoterValueLabel;
		public jQueryObject PromoterValueLabelJ {get {if (_PromoterValueLabelJ == null) {_PromoterValueLabelJ = jQuery.Select("#" + clientId + "_PromoterValueLabel");}; return _PromoterValueLabelJ;}} private jQueryObject _PromoterValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PromoterCampaignCreditsLabel {get {if (_PromoterCampaignCreditsLabel == null) {_PromoterCampaignCreditsLabel = (Element)Document.GetElementById(clientId + "_PromoterCampaignCreditsLabel");}; return _PromoterCampaignCreditsLabel;}} private Element _PromoterCampaignCreditsLabel;
		public jQueryObject PromoterCampaignCreditsLabelJ {get {if (_PromoterCampaignCreditsLabelJ == null) {_PromoterCampaignCreditsLabelJ = jQuery.Select("#" + clientId + "_PromoterCampaignCreditsLabel");}; return _PromoterCampaignCreditsLabelJ;}} private jQueryObject _PromoterCampaignCreditsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PromoterRequiredFieldValidator {get {if (_PromoterRequiredFieldValidator == null) {_PromoterRequiredFieldValidator = (Element)Document.GetElementById(clientId + "_PromoterRequiredFieldValidator");}; return _PromoterRequiredFieldValidator;}} private Element _PromoterRequiredFieldValidator;
		public jQueryObject PromoterRequiredFieldValidatorJ {get {if (_PromoterRequiredFieldValidatorJ == null) {_PromoterRequiredFieldValidatorJ = jQuery.Select("#" + clientId + "_PromoterRequiredFieldValidator");}; return _PromoterRequiredFieldValidatorJ;}} private jQueryObject _PromoterRequiredFieldValidatorJ;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public SelectElement UsrDropDownList {get {if (_UsrDropDownList == null) {_UsrDropDownList = (SelectElement)Document.GetElementById(clientId + "_UsrDropDownList");}; return _UsrDropDownList;}} private SelectElement _UsrDropDownList;
		public jQueryObject UsrDropDownListJ {get {if (_UsrDropDownListJ == null) {_UsrDropDownListJ = jQuery.Select("#" + clientId + "_UsrDropDownList");}; return _UsrDropDownListJ;}} private jQueryObject _UsrDropDownListJ;
		public Element UsrValueLabel {get {if (_UsrValueLabel == null) {_UsrValueLabel = (Element)Document.GetElementById(clientId + "_UsrValueLabel");}; return _UsrValueLabel;}} private Element _UsrValueLabel;
		public jQueryObject UsrValueLabelJ {get {if (_UsrValueLabelJ == null) {_UsrValueLabelJ = jQuery.Select("#" + clientId + "_UsrValueLabel");}; return _UsrValueLabelJ;}} private jQueryObject _UsrValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiActionUserAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiActionUserAutoCompleteBehaviour");}}
		public Element ActionUsrValueLabel {get {if (_ActionUsrValueLabel == null) {_ActionUsrValueLabel = (Element)Document.GetElementById(clientId + "_ActionUsrValueLabel");}; return _ActionUsrValueLabel;}} private Element _ActionUsrValueLabel;
		public jQueryObject ActionUsrValueLabelJ {get {if (_ActionUsrValueLabelJ == null) {_ActionUsrValueLabelJ = jQuery.Select("#" + clientId + "_ActionUsrValueLabel");}; return _ActionUsrValueLabelJ;}} private jQueryObject _ActionUsrValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element ActionUsrRequiredFieldValidator {get {if (_ActionUsrRequiredFieldValidator == null) {_ActionUsrRequiredFieldValidator = (Element)Document.GetElementById(clientId + "_ActionUsrRequiredFieldValidator");}; return _ActionUsrRequiredFieldValidator;}} private Element _ActionUsrRequiredFieldValidator;
		public jQueryObject ActionUsrRequiredFieldValidatorJ {get {if (_ActionUsrRequiredFieldValidatorJ == null) {_ActionUsrRequiredFieldValidatorJ = jQuery.Select("#" + clientId + "_ActionUsrRequiredFieldValidator");}; return _ActionUsrRequiredFieldValidatorJ;}} private jQueryObject _ActionUsrRequiredFieldValidatorJ;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public SelectElement BuyableObjectTypeDropDownList {get {if (_BuyableObjectTypeDropDownList == null) {_BuyableObjectTypeDropDownList = (SelectElement)Document.GetElementById(clientId + "_BuyableObjectTypeDropDownList");}; return _BuyableObjectTypeDropDownList;}} private SelectElement _BuyableObjectTypeDropDownList;
		public jQueryObject BuyableObjectTypeDropDownListJ {get {if (_BuyableObjectTypeDropDownListJ == null) {_BuyableObjectTypeDropDownListJ = jQuery.Select("#" + clientId + "_BuyableObjectTypeDropDownList");}; return _BuyableObjectTypeDropDownListJ;}} private jQueryObject _BuyableObjectTypeDropDownListJ;
		public Element BuyableObjectTypeValueLabel {get {if (_BuyableObjectTypeValueLabel == null) {_BuyableObjectTypeValueLabel = (Element)Document.GetElementById(clientId + "_BuyableObjectTypeValueLabel");}; return _BuyableObjectTypeValueLabel;}} private Element _BuyableObjectTypeValueLabel;
		public jQueryObject BuyableObjectTypeValueLabelJ {get {if (_BuyableObjectTypeValueLabelJ == null) {_BuyableObjectTypeValueLabelJ = jQuery.Select("#" + clientId + "_BuyableObjectTypeValueLabel");}; return _BuyableObjectTypeValueLabelJ;}} private jQueryObject _BuyableObjectTypeValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element BuyableObjectKRow {get {if (_BuyableObjectKRow == null) {_BuyableObjectKRow = (Element)Document.GetElementById(clientId + "_BuyableObjectKRow");}; return _BuyableObjectKRow;}} private Element _BuyableObjectKRow;
		public jQueryObject BuyableObjectKRowJ {get {if (_BuyableObjectKRowJ == null) {_BuyableObjectKRowJ = jQuery.Select("#" + clientId + "_BuyableObjectKRow");}; return _BuyableObjectKRowJ;}} private jQueryObject _BuyableObjectKRowJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public InputElement BuyableObjectKTextBox {get {if (_BuyableObjectKTextBox == null) {_BuyableObjectKTextBox = (InputElement)Document.GetElementById(clientId + "_BuyableObjectKTextBox");}; return _BuyableObjectKTextBox;}} private InputElement _BuyableObjectKTextBox;
		public jQueryObject BuyableObjectKTextBoxJ {get {if (_BuyableObjectKTextBoxJ == null) {_BuyableObjectKTextBoxJ = jQuery.Select("#" + clientId + "_BuyableObjectKTextBox");}; return _BuyableObjectKTextBoxJ;}} private jQueryObject _BuyableObjectKTextBoxJ;
		public Element BuyableObjectCustomValidator {get {if (_BuyableObjectCustomValidator == null) {_BuyableObjectCustomValidator = (Element)Document.GetElementById(clientId + "_BuyableObjectCustomValidator");}; return _BuyableObjectCustomValidator;}} private Element _BuyableObjectCustomValidator;
		public jQueryObject BuyableObjectCustomValidatorJ {get {if (_BuyableObjectCustomValidatorJ == null) {_BuyableObjectCustomValidatorJ = jQuery.Select("#" + clientId + "_BuyableObjectCustomValidator");}; return _BuyableObjectCustomValidatorJ;}} private jQueryObject _BuyableObjectCustomValidatorJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public SelectElement InvoiceItemTypeDropDownList {get {if (_InvoiceItemTypeDropDownList == null) {_InvoiceItemTypeDropDownList = (SelectElement)Document.GetElementById(clientId + "_InvoiceItemTypeDropDownList");}; return _InvoiceItemTypeDropDownList;}} private SelectElement _InvoiceItemTypeDropDownList;
		public jQueryObject InvoiceItemTypeDropDownListJ {get {if (_InvoiceItemTypeDropDownListJ == null) {_InvoiceItemTypeDropDownListJ = jQuery.Select("#" + clientId + "_InvoiceItemTypeDropDownList");}; return _InvoiceItemTypeDropDownListJ;}} private jQueryObject _InvoiceItemTypeDropDownListJ;
		public Element InvoiceItemTypeValueLabel {get {if (_InvoiceItemTypeValueLabel == null) {_InvoiceItemTypeValueLabel = (Element)Document.GetElementById(clientId + "_InvoiceItemTypeValueLabel");}; return _InvoiceItemTypeValueLabel;}} private Element _InvoiceItemTypeValueLabel;
		public jQueryObject InvoiceItemTypeValueLabelJ {get {if (_InvoiceItemTypeValueLabelJ == null) {_InvoiceItemTypeValueLabelJ = jQuery.Select("#" + clientId + "_InvoiceItemTypeValueLabel");}; return _InvoiceItemTypeValueLabelJ;}} private jQueryObject _InvoiceItemTypeValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public InputElement CreditsTextBox {get {if (_CreditsTextBox == null) {_CreditsTextBox = (InputElement)Document.GetElementById(clientId + "_CreditsTextBox");}; return _CreditsTextBox;}} private InputElement _CreditsTextBox;
		public jQueryObject CreditsTextBoxJ {get {if (_CreditsTextBoxJ == null) {_CreditsTextBoxJ = jQuery.Select("#" + clientId + "_CreditsTextBox");}; return _CreditsTextBoxJ;}} private jQueryObject _CreditsTextBoxJ;
		public Element CreditsRequiredFieldValidator {get {if (_CreditsRequiredFieldValidator == null) {_CreditsRequiredFieldValidator = (Element)Document.GetElementById(clientId + "_CreditsRequiredFieldValidator");}; return _CreditsRequiredFieldValidator;}} private Element _CreditsRequiredFieldValidator;
		public jQueryObject CreditsRequiredFieldValidatorJ {get {if (_CreditsRequiredFieldValidatorJ == null) {_CreditsRequiredFieldValidatorJ = jQuery.Select("#" + clientId + "_CreditsRequiredFieldValidator");}; return _CreditsRequiredFieldValidatorJ;}} private jQueryObject _CreditsRequiredFieldValidatorJ;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element CreditsRangeValidator {get {if (_CreditsRangeValidator == null) {_CreditsRangeValidator = (Element)Document.GetElementById(clientId + "_CreditsRangeValidator");}; return _CreditsRangeValidator;}} private Element _CreditsRangeValidator;
		public jQueryObject CreditsRangeValidatorJ {get {if (_CreditsRangeValidatorJ == null) {_CreditsRangeValidatorJ = jQuery.Select("#" + clientId + "_CreditsRangeValidator");}; return _CreditsRangeValidatorJ;}} private jQueryObject _CreditsRangeValidatorJ;//mappings.Add("System.Web.UI.WebControls.RangeValidator", ElementGetter("Element"));
		public InputElement DescriptionTextBox {get {if (_DescriptionTextBox == null) {_DescriptionTextBox = (InputElement)Document.GetElementById(clientId + "_DescriptionTextBox");}; return _DescriptionTextBox;}} private InputElement _DescriptionTextBox;
		public jQueryObject DescriptionTextBoxJ {get {if (_DescriptionTextBoxJ == null) {_DescriptionTextBoxJ = jQuery.Select("#" + clientId + "_DescriptionTextBox");}; return _DescriptionTextBoxJ;}} private jQueryObject _DescriptionTextBoxJ;
		public Element NotesAddOnlyTextBox {get {if (_NotesAddOnlyTextBox == null) {_NotesAddOnlyTextBox = (Element)Document.GetElementById(clientId + "_NotesAddOnlyTextBox");}; return _NotesAddOnlyTextBox;}} private Element _NotesAddOnlyTextBox;
		public jQueryObject NotesAddOnlyTextBoxJ {get {if (_NotesAddOnlyTextBoxJ == null) {_NotesAddOnlyTextBoxJ = jQuery.Select("#" + clientId + "_NotesAddOnlyTextBox");}; return _NotesAddOnlyTextBoxJ;}} private jQueryObject _NotesAddOnlyTextBoxJ;//mappings.Add("Spotted.Controls.AddOnlyTextBox", ElementGetter("Element"));
		public Element CampaignCreditValidationSummary {get {if (_CampaignCreditValidationSummary == null) {_CampaignCreditValidationSummary = (Element)Document.GetElementById(clientId + "_CampaignCreditValidationSummary");}; return _CampaignCreditValidationSummary;}} private Element _CampaignCreditValidationSummary;
		public jQueryObject CampaignCreditValidationSummaryJ {get {if (_CampaignCreditValidationSummaryJ == null) {_CampaignCreditValidationSummaryJ = jQuery.Select("#" + clientId + "_CampaignCreditValidationSummary");}; return _CampaignCreditValidationSummaryJ;}} private jQueryObject _CampaignCreditValidationSummaryJ;//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
		public InputElement SaveButton {get {if (_SaveButton == null) {_SaveButton = (InputElement)Document.GetElementById(clientId + "_SaveButton");}; return _SaveButton;}} private InputElement _SaveButton;
		public jQueryObject SaveButtonJ {get {if (_SaveButtonJ == null) {_SaveButtonJ = jQuery.Select("#" + clientId + "_SaveButton");}; return _SaveButtonJ;}} private jQueryObject _SaveButtonJ;
		public InputElement CancelButton {get {if (_CancelButton == null) {_CancelButton = (InputElement)Document.GetElementById(clientId + "_CancelButton");}; return _CancelButton;}} private InputElement _CancelButton;
		public jQueryObject CancelButtonJ {get {if (_CancelButtonJ == null) {_CancelButtonJ = jQuery.Select("#" + clientId + "_CancelButton");}; return _CancelButtonJ;}} private jQueryObject _CancelButtonJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
