//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.TicketSearch
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
		public DivElement TicketSearchCriteriaPanel {get {if (_TicketSearchCriteriaPanel == null) {_TicketSearchCriteriaPanel = (DivElement)Document.GetElementById(clientId + "_TicketSearchCriteriaPanel");}; return _TicketSearchCriteriaPanel;}} private DivElement _TicketSearchCriteriaPanel;
		public jQueryObject TicketSearchCriteriaPanelJ {get {if (_TicketSearchCriteriaPanelJ == null) {_TicketSearchCriteriaPanelJ = jQuery.Select("#" + clientId + "_TicketSearchCriteriaPanel");}; return _TicketSearchCriteriaPanelJ;}} private jQueryObject _TicketSearchCriteriaPanelJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPromotersAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPromotersAutoCompleteBehaviour");}}
		public InputElement FirstNameTextBox {get {if (_FirstNameTextBox == null) {_FirstNameTextBox = (InputElement)Document.GetElementById(clientId + "_FirstNameTextBox");}; return _FirstNameTextBox;}} private InputElement _FirstNameTextBox;
		public jQueryObject FirstNameTextBoxJ {get {if (_FirstNameTextBoxJ == null) {_FirstNameTextBoxJ = jQuery.Select("#" + clientId + "_FirstNameTextBox");}; return _FirstNameTextBoxJ;}} private jQueryObject _FirstNameTextBoxJ;
		public InputElement TicketRunKTextBox {get {if (_TicketRunKTextBox == null) {_TicketRunKTextBox = (InputElement)Document.GetElementById(clientId + "_TicketRunKTextBox");}; return _TicketRunKTextBox;}} private InputElement _TicketRunKTextBox;
		public jQueryObject TicketRunKTextBoxJ {get {if (_TicketRunKTextBoxJ == null) {_TicketRunKTextBoxJ = jQuery.Select("#" + clientId + "_TicketRunKTextBox");}; return _TicketRunKTextBoxJ;}} private jQueryObject _TicketRunKTextBoxJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiUsersAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiUsersAutoCompleteBehaviour");}}
		public InputElement LastNameTextBox {get {if (_LastNameTextBox == null) {_LastNameTextBox = (InputElement)Document.GetElementById(clientId + "_LastNameTextBox");}; return _LastNameTextBox;}} private InputElement _LastNameTextBox;
		public jQueryObject LastNameTextBoxJ {get {if (_LastNameTextBoxJ == null) {_LastNameTextBoxJ = jQuery.Select("#" + clientId + "_LastNameTextBox");}; return _LastNameTextBoxJ;}} private jQueryObject _LastNameTextBoxJ;
		public SelectElement StatusDropDownList {get {if (_StatusDropDownList == null) {_StatusDropDownList = (SelectElement)Document.GetElementById(clientId + "_StatusDropDownList");}; return _StatusDropDownList;}} private SelectElement _StatusDropDownList;
		public jQueryObject StatusDropDownListJ {get {if (_StatusDropDownListJ == null) {_StatusDropDownListJ = jQuery.Select("#" + clientId + "_StatusDropDownList");}; return _StatusDropDownListJ;}} private jQueryObject _StatusDropDownListJ;
		public InputElement CardDigitsTextBox {get {if (_CardDigitsTextBox == null) {_CardDigitsTextBox = (InputElement)Document.GetElementById(clientId + "_CardDigitsTextBox");}; return _CardDigitsTextBox;}} private InputElement _CardDigitsTextBox;
		public jQueryObject CardDigitsTextBoxJ {get {if (_CardDigitsTextBoxJ == null) {_CardDigitsTextBoxJ = jQuery.Select("#" + clientId + "_CardDigitsTextBox");}; return _CardDigitsTextBoxJ;}} private jQueryObject _CardDigitsTextBoxJ;
		public InputElement PostCodeTextBox {get {if (_PostCodeTextBox == null) {_PostCodeTextBox = (InputElement)Document.GetElementById(clientId + "_PostCodeTextBox");}; return _PostCodeTextBox;}} private InputElement _PostCodeTextBox;
		public jQueryObject PostCodeTextBoxJ {get {if (_PostCodeTextBoxJ == null) {_PostCodeTextBoxJ = jQuery.Select("#" + clientId + "_PostCodeTextBox");}; return _PostCodeTextBoxJ;}} private jQueryObject _PostCodeTextBoxJ;
		public SelectElement FeedbackDropDownList {get {if (_FeedbackDropDownList == null) {_FeedbackDropDownList = (SelectElement)Document.GetElementById(clientId + "_FeedbackDropDownList");}; return _FeedbackDropDownList;}} private SelectElement _FeedbackDropDownList;
		public jQueryObject FeedbackDropDownListJ {get {if (_FeedbackDropDownListJ == null) {_FeedbackDropDownListJ = jQuery.Select("#" + clientId + "_FeedbackDropDownList");}; return _FeedbackDropDownListJ;}} private jQueryObject _FeedbackDropDownListJ;
		public Element SearchButton {get {if (_SearchButton == null) {_SearchButton = (Element)Document.GetElementById(clientId + "_SearchButton");}; return _SearchButton;}} private Element _SearchButton;
		public jQueryObject SearchButtonJ {get {if (_SearchButtonJ == null) {_SearchButtonJ = jQuery.Select("#" + clientId + "_SearchButton");}; return _SearchButtonJ;}} private jQueryObject _SearchButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element ClearButton {get {if (_ClearButton == null) {_ClearButton = (Element)Document.GetElementById(clientId + "_ClearButton");}; return _ClearButton;}} private Element _ClearButton;
		public jQueryObject ClearButtonJ {get {if (_ClearButtonJ == null) {_ClearButtonJ = jQuery.Select("#" + clientId + "_ClearButton");}; return _ClearButtonJ;}} private jQueryObject _ClearButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element ErrorLabel {get {if (_ErrorLabel == null) {_ErrorLabel = (Element)Document.GetElementById(clientId + "_ErrorLabel");}; return _ErrorLabel;}} private Element _ErrorLabel;
		public jQueryObject ErrorLabelJ {get {if (_ErrorLabelJ == null) {_ErrorLabelJ = jQuery.Select("#" + clientId + "_ErrorLabel");}; return _ErrorLabelJ;}} private jQueryObject _ErrorLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SearchResultsTicketsGridView {get {if (_SearchResultsTicketsGridView == null) {_SearchResultsTicketsGridView = (Element)Document.GetElementById(clientId + "_SearchResultsTicketsGridView");}; return _SearchResultsTicketsGridView;}} private Element _SearchResultsTicketsGridView;
		public jQueryObject SearchResultsTicketsGridViewJ {get {if (_SearchResultsTicketsGridViewJ == null) {_SearchResultsTicketsGridViewJ = jQuery.Select("#" + clientId + "_SearchResultsTicketsGridView");}; return _SearchResultsTicketsGridViewJ;}} private jQueryObject _SearchResultsTicketsGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element SearchResultsMessageLabel {get {if (_SearchResultsMessageLabel == null) {_SearchResultsMessageLabel = (Element)Document.GetElementById(clientId + "_SearchResultsMessageLabel");}; return _SearchResultsMessageLabel;}} private Element _SearchResultsMessageLabel;
		public jQueryObject SearchResultsMessageLabelJ {get {if (_SearchResultsMessageLabelJ == null) {_SearchResultsMessageLabelJ = jQuery.Select("#" + clientId + "_SearchResultsMessageLabel");}; return _SearchResultsMessageLabelJ;}} private jQueryObject _SearchResultsMessageLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
