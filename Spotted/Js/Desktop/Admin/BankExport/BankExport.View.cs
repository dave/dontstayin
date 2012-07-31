//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.BankExport
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
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement SearchBankExportPanel {get {if (_SearchBankExportPanel == null) {_SearchBankExportPanel = (DivElement)Document.GetElementById(clientId + "_SearchBankExportPanel");}; return _SearchBankExportPanel;}} private DivElement _SearchBankExportPanel;
		public jQueryObject SearchBankExportPanelJ {get {if (_SearchBankExportPanelJ == null) {_SearchBankExportPanelJ = jQuery.Select("#" + clientId + "_SearchBankExportPanel");}; return _SearchBankExportPanelJ;}} private jQueryObject _SearchBankExportPanelJ;
		public Element BankExportRadioButtonList {get {if (_BankExportRadioButtonList == null) {_BankExportRadioButtonList = (Element)Document.GetElementById(clientId + "_BankExportRadioButtonList");}; return _BankExportRadioButtonList;}} private Element _BankExportRadioButtonList;
		public jQueryObject BankExportRadioButtonListJ {get {if (_BankExportRadioButtonListJ == null) {_BankExportRadioButtonListJ = jQuery.Select("#" + clientId + "_BankExportRadioButtonList");}; return _BankExportRadioButtonListJ;}} private jQueryObject _BankExportRadioButtonListJ;//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
		public Element SearchBankExportHeader {get {if (_SearchBankExportHeader == null) {_SearchBankExportHeader = (Element)Document.GetElementById(clientId + "_SearchBankExportHeader");}; return _SearchBankExportHeader;}} private Element _SearchBankExportHeader;
		public jQueryObject SearchBankExportHeaderJ {get {if (_SearchBankExportHeaderJ == null) {_SearchBankExportHeaderJ = jQuery.Select("#" + clientId + "_SearchBankExportHeader");}; return _SearchBankExportHeaderJ;}} private jQueryObject _SearchBankExportHeaderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element BankExportLinkP {get {if (_BankExportLinkP == null) {_BankExportLinkP = (Element)Document.GetElementById(clientId + "_BankExportLinkP");}; return _BankExportLinkP;}} private Element _BankExportLinkP;
		public jQueryObject BankExportLinkPJ {get {if (_BankExportLinkPJ == null) {_BankExportLinkPJ = jQuery.Select("#" + clientId + "_BankExportLinkP");}; return _BankExportLinkPJ;}} private jQueryObject _BankExportLinkPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element BankExportGeneratorLinkLabel {get {if (_BankExportGeneratorLinkLabel == null) {_BankExportGeneratorLinkLabel = (Element)Document.GetElementById(clientId + "_BankExportGeneratorLinkLabel");}; return _BankExportGeneratorLinkLabel;}} private Element _BankExportGeneratorLinkLabel;
		public jQueryObject BankExportGeneratorLinkLabelJ {get {if (_BankExportGeneratorLinkLabelJ == null) {_BankExportGeneratorLinkLabelJ = jQuery.Select("#" + clientId + "_BankExportGeneratorLinkLabel");}; return _BankExportGeneratorLinkLabelJ;}} private jQueryObject _BankExportGeneratorLinkLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SearchCriteriaTable {get {if (_SearchCriteriaTable == null) {_SearchCriteriaTable = (Element)Document.GetElementById(clientId + "_SearchCriteriaTable");}; return _SearchCriteriaTable;}} private Element _SearchCriteriaTable;
		public jQueryObject SearchCriteriaTableJ {get {if (_SearchCriteriaTableJ == null) {_SearchCriteriaTableJ = jQuery.Select("#" + clientId + "_SearchCriteriaTable");}; return _SearchCriteriaTableJ;}} private jQueryObject _SearchCriteriaTableJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public SelectElement StatusDropDownList {get {if (_StatusDropDownList == null) {_StatusDropDownList = (SelectElement)Document.GetElementById(clientId + "_StatusDropDownList");}; return _StatusDropDownList;}} private SelectElement _StatusDropDownList;
		public jQueryObject StatusDropDownListJ {get {if (_StatusDropDownListJ == null) {_StatusDropDownListJ = jQuery.Select("#" + clientId + "_StatusDropDownList");}; return _StatusDropDownListJ;}} private jQueryObject _StatusDropDownListJ;
		public SelectElement TypeDropDownList {get {if (_TypeDropDownList == null) {_TypeDropDownList = (SelectElement)Document.GetElementById(clientId + "_TypeDropDownList");}; return _TypeDropDownList;}} private SelectElement _TypeDropDownList;
		public jQueryObject TypeDropDownListJ {get {if (_TypeDropDownListJ == null) {_TypeDropDownListJ = jQuery.Select("#" + clientId + "_TypeDropDownList");}; return _TypeDropDownListJ;}} private jQueryObject _TypeDropDownListJ;
		public Js.CustomControls.Cal.Controller ExportDateCal {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_ExportDateCalController");}}
		public InputElement BatchRefTextBox {get {if (_BatchRefTextBox == null) {_BatchRefTextBox = (InputElement)Document.GetElementById(clientId + "_BatchRefTextBox");}; return _BatchRefTextBox;}} private InputElement _BatchRefTextBox;
		public jQueryObject BatchRefTextBoxJ {get {if (_BatchRefTextBoxJ == null) {_BatchRefTextBoxJ = jQuery.Select("#" + clientId + "_BatchRefTextBox");}; return _BatchRefTextBoxJ;}} private jQueryObject _BatchRefTextBoxJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPromoterHtmlAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPromoterHtmlAutoCompleteBehaviour");}}
		public InputElement SearchButton {get {if (_SearchButton == null) {_SearchButton = (InputElement)Document.GetElementById(clientId + "_SearchButton");}; return _SearchButton;}} private InputElement _SearchButton;
		public jQueryObject SearchButtonJ {get {if (_SearchButtonJ == null) {_SearchButtonJ = jQuery.Select("#" + clientId + "_SearchButton");}; return _SearchButtonJ;}} private jQueryObject _SearchButtonJ;
		public InputElement ClearButton {get {if (_ClearButton == null) {_ClearButton = (InputElement)Document.GetElementById(clientId + "_ClearButton");}; return _ClearButton;}} private InputElement _ClearButton;
		public jQueryObject ClearButtonJ {get {if (_ClearButtonJ == null) {_ClearButtonJ = jQuery.Select("#" + clientId + "_ClearButton");}; return _ClearButtonJ;}} private jQueryObject _ClearButtonJ;
		public Element SearchBankExportGridView {get {if (_SearchBankExportGridView == null) {_SearchBankExportGridView = (Element)Document.GetElementById(clientId + "_SearchBankExportGridView");}; return _SearchBankExportGridView;}} private Element _SearchBankExportGridView;
		public jQueryObject SearchBankExportGridViewJ {get {if (_SearchBankExportGridViewJ == null) {_SearchBankExportGridViewJ = jQuery.Select("#" + clientId + "_SearchBankExportGridView");}; return _SearchBankExportGridViewJ;}} private jQueryObject _SearchBankExportGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element SearchResultsMessageLabel {get {if (_SearchResultsMessageLabel == null) {_SearchResultsMessageLabel = (Element)Document.GetElementById(clientId + "_SearchResultsMessageLabel");}; return _SearchResultsMessageLabel;}} private Element _SearchResultsMessageLabel;
		public jQueryObject SearchResultsMessageLabelJ {get {if (_SearchResultsMessageLabelJ == null) {_SearchResultsMessageLabelJ = jQuery.Select("#" + clientId + "_SearchResultsMessageLabel");}; return _SearchResultsMessageLabelJ;}} private jQueryObject _SearchResultsMessageLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SearchSummaryTable {get {if (_SearchSummaryTable == null) {_SearchSummaryTable = (Element)Document.GetElementById(clientId + "_SearchSummaryTable");}; return _SearchSummaryTable;}} private Element _SearchSummaryTable;
		public jQueryObject SearchSummaryTableJ {get {if (_SearchSummaryTableJ == null) {_SearchSummaryTableJ = jQuery.Select("#" + clientId + "_SearchSummaryTable");}; return _SearchSummaryTableJ;}} private jQueryObject _SearchSummaryTableJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public Element FundsClientToCurrentLabel {get {if (_FundsClientToCurrentLabel == null) {_FundsClientToCurrentLabel = (Element)Document.GetElementById(clientId + "_FundsClientToCurrentLabel");}; return _FundsClientToCurrentLabel;}} private Element _FundsClientToCurrentLabel;
		public jQueryObject FundsClientToCurrentLabelJ {get {if (_FundsClientToCurrentLabelJ == null) {_FundsClientToCurrentLabelJ = jQuery.Select("#" + clientId + "_FundsClientToCurrentLabel");}; return _FundsClientToCurrentLabelJ;}} private jQueryObject _FundsClientToCurrentLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FundsClientToPromoterLabel {get {if (_FundsClientToPromoterLabel == null) {_FundsClientToPromoterLabel = (Element)Document.GetElementById(clientId + "_FundsClientToPromoterLabel");}; return _FundsClientToPromoterLabel;}} private Element _FundsClientToPromoterLabel;
		public jQueryObject FundsClientToPromoterLabelJ {get {if (_FundsClientToPromoterLabelJ == null) {_FundsClientToPromoterLabelJ = jQuery.Select("#" + clientId + "_FundsClientToPromoterLabel");}; return _FundsClientToPromoterLabelJ;}} private jQueryObject _FundsClientToPromoterLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FundsCurrentToPromoterLabel {get {if (_FundsCurrentToPromoterLabel == null) {_FundsCurrentToPromoterLabel = (Element)Document.GetElementById(clientId + "_FundsCurrentToPromoterLabel");}; return _FundsCurrentToPromoterLabel;}} private Element _FundsCurrentToPromoterLabel;
		public jQueryObject FundsCurrentToPromoterLabelJ {get {if (_FundsCurrentToPromoterLabelJ == null) {_FundsCurrentToPromoterLabelJ = jQuery.Select("#" + clientId + "_FundsCurrentToPromoterLabel");}; return _FundsCurrentToPromoterLabelJ;}} private jQueryObject _FundsCurrentToPromoterLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FundsCurrentToClientLabel {get {if (_FundsCurrentToClientLabel == null) {_FundsCurrentToClientLabel = (Element)Document.GetElementById(clientId + "_FundsCurrentToClientLabel");}; return _FundsCurrentToClientLabel;}} private Element _FundsCurrentToClientLabel;
		public jQueryObject FundsCurrentToClientLabelJ {get {if (_FundsCurrentToClientLabelJ == null) {_FundsCurrentToClientLabelJ = jQuery.Select("#" + clientId + "_FundsCurrentToClientLabel");}; return _FundsCurrentToClientLabelJ;}} private jQueryObject _FundsCurrentToClientLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
