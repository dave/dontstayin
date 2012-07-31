//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.TicketRuns
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
		public DivElement AllTicketRunsPanel {get {if (_AllTicketRunsPanel == null) {_AllTicketRunsPanel = (DivElement)Document.GetElementById(clientId + "_AllTicketRunsPanel");}; return _AllTicketRunsPanel;}} private DivElement _AllTicketRunsPanel;
		public jQueryObject AllTicketRunsPanelJ {get {if (_AllTicketRunsPanelJ == null) {_AllTicketRunsPanelJ = jQuery.Select("#" + clientId + "_AllTicketRunsPanel");}; return _AllTicketRunsPanelJ;}} private jQueryObject _AllTicketRunsPanelJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPromotersAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPromotersAutoCompleteBehaviour");}}
		public InputElement TicketRunKTextBox {get {if (_TicketRunKTextBox == null) {_TicketRunKTextBox = (InputElement)Document.GetElementById(clientId + "_TicketRunKTextBox");}; return _TicketRunKTextBox;}} private InputElement _TicketRunKTextBox;
		public jQueryObject TicketRunKTextBoxJ {get {if (_TicketRunKTextBoxJ == null) {_TicketRunKTextBoxJ = jQuery.Select("#" + clientId + "_TicketRunKTextBox");}; return _TicketRunKTextBoxJ;}} private jQueryObject _TicketRunKTextBoxJ;
		public CheckBoxElement OnlyShowTicketRunsWithSoldTicketsCheckBox {get {if (_OnlyShowTicketRunsWithSoldTicketsCheckBox == null) {_OnlyShowTicketRunsWithSoldTicketsCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_OnlyShowTicketRunsWithSoldTicketsCheckBox");}; return _OnlyShowTicketRunsWithSoldTicketsCheckBox;}} private CheckBoxElement _OnlyShowTicketRunsWithSoldTicketsCheckBox;
		public jQueryObject OnlyShowTicketRunsWithSoldTicketsCheckBoxJ {get {if (_OnlyShowTicketRunsWithSoldTicketsCheckBoxJ == null) {_OnlyShowTicketRunsWithSoldTicketsCheckBoxJ = jQuery.Select("#" + clientId + "_OnlyShowTicketRunsWithSoldTicketsCheckBox");}; return _OnlyShowTicketRunsWithSoldTicketsCheckBoxJ;}} private jQueryObject _OnlyShowTicketRunsWithSoldTicketsCheckBoxJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiEventAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiEventAutoCompleteBehaviour");}}
		public SelectElement StatusDropDownList {get {if (_StatusDropDownList == null) {_StatusDropDownList = (SelectElement)Document.GetElementById(clientId + "_StatusDropDownList");}; return _StatusDropDownList;}} private SelectElement _StatusDropDownList;
		public jQueryObject StatusDropDownListJ {get {if (_StatusDropDownListJ == null) {_StatusDropDownListJ = jQuery.Select("#" + clientId + "_StatusDropDownList");}; return _StatusDropDownListJ;}} private jQueryObject _StatusDropDownListJ;
		public Element SearchButton {get {if (_SearchButton == null) {_SearchButton = (Element)Document.GetElementById(clientId + "_SearchButton");}; return _SearchButton;}} private Element _SearchButton;
		public jQueryObject SearchButtonJ {get {if (_SearchButtonJ == null) {_SearchButtonJ = jQuery.Select("#" + clientId + "_SearchButton");}; return _SearchButtonJ;}} private jQueryObject _SearchButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element ClearButton {get {if (_ClearButton == null) {_ClearButton = (Element)Document.GetElementById(clientId + "_ClearButton");}; return _ClearButton;}} private Element _ClearButton;
		public jQueryObject ClearButtonJ {get {if (_ClearButtonJ == null) {_ClearButtonJ = jQuery.Select("#" + clientId + "_ClearButton");}; return _ClearButtonJ;}} private jQueryObject _ClearButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H1Title {get {if (_H1Title == null) {_H1Title = (Element)Document.GetElementById(clientId + "_H1Title");}; return _H1Title;}} private Element _H1Title;
		public jQueryObject H1TitleJ {get {if (_H1TitleJ == null) {_H1TitleJ = jQuery.Select("#" + clientId + "_H1Title");}; return _H1TitleJ;}} private jQueryObject _H1TitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element TicketRunsGridView {get {if (_TicketRunsGridView == null) {_TicketRunsGridView = (Element)Document.GetElementById(clientId + "_TicketRunsGridView");}; return _TicketRunsGridView;}} private Element _TicketRunsGridView;
		public jQueryObject TicketRunsGridViewJ {get {if (_TicketRunsGridViewJ == null) {_TicketRunsGridViewJ = jQuery.Select("#" + clientId + "_TicketRunsGridView");}; return _TicketRunsGridViewJ;}} private jQueryObject _TicketRunsGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element SearchResultsMessageLabel {get {if (_SearchResultsMessageLabel == null) {_SearchResultsMessageLabel = (Element)Document.GetElementById(clientId + "_SearchResultsMessageLabel");}; return _SearchResultsMessageLabel;}} private Element _SearchResultsMessageLabel;
		public jQueryObject SearchResultsMessageLabelJ {get {if (_SearchResultsMessageLabelJ == null) {_SearchResultsMessageLabelJ = jQuery.Select("#" + clientId + "_SearchResultsMessageLabel");}; return _SearchResultsMessageLabelJ;}} private jQueryObject _SearchResultsMessageLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element TicketRunsJavascriptLabel {get {if (_TicketRunsJavascriptLabel == null) {_TicketRunsJavascriptLabel = (Element)Document.GetElementById(clientId + "_TicketRunsJavascriptLabel");}; return _TicketRunsJavascriptLabel;}} private Element _TicketRunsJavascriptLabel;
		public jQueryObject TicketRunsJavascriptLabelJ {get {if (_TicketRunsJavascriptLabelJ == null) {_TicketRunsJavascriptLabelJ = jQuery.Select("#" + clientId + "_TicketRunsJavascriptLabel");}; return _TicketRunsJavascriptLabelJ;}} private jQueryObject _TicketRunsJavascriptLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
