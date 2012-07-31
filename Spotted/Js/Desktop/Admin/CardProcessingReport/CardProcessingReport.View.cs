//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.CardProcessingReport
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
		public DivElement CardnetProcessingPanel {get {if (_CardnetProcessingPanel == null) {_CardnetProcessingPanel = (DivElement)Document.GetElementById(clientId + "_CardnetProcessingPanel");}; return _CardnetProcessingPanel;}} private DivElement _CardnetProcessingPanel;
		public jQueryObject CardnetProcessingPanelJ {get {if (_CardnetProcessingPanelJ == null) {_CardnetProcessingPanelJ = jQuery.Select("#" + clientId + "_CardnetProcessingPanel");}; return _CardnetProcessingPanelJ;}} private jQueryObject _CardnetProcessingPanelJ;
		public Element CardnetProcessingSearchCriteriaTable {get {if (_CardnetProcessingSearchCriteriaTable == null) {_CardnetProcessingSearchCriteriaTable = (Element)Document.GetElementById(clientId + "_CardnetProcessingSearchCriteriaTable");}; return _CardnetProcessingSearchCriteriaTable;}} private Element _CardnetProcessingSearchCriteriaTable;
		public jQueryObject CardnetProcessingSearchCriteriaTableJ {get {if (_CardnetProcessingSearchCriteriaTableJ == null) {_CardnetProcessingSearchCriteriaTableJ = jQuery.Select("#" + clientId + "_CardnetProcessingSearchCriteriaTable");}; return _CardnetProcessingSearchCriteriaTableJ;}} private jQueryObject _CardnetProcessingSearchCriteriaTableJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public Js.CustomControls.Cal.Controller FromDateCal {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_FromDateCalController");}}
		public Js.CustomControls.Cal.Controller ToDateCal {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_ToDateCalController");}}
		public InputElement SearchButton {get {if (_SearchButton == null) {_SearchButton = (InputElement)Document.GetElementById(clientId + "_SearchButton");}; return _SearchButton;}} private InputElement _SearchButton;
		public jQueryObject SearchButtonJ {get {if (_SearchButtonJ == null) {_SearchButtonJ = jQuery.Select("#" + clientId + "_SearchButton");}; return _SearchButtonJ;}} private jQueryObject _SearchButtonJ;
		public Element CardnetAccountGridView {get {if (_CardnetAccountGridView == null) {_CardnetAccountGridView = (Element)Document.GetElementById(clientId + "_CardnetAccountGridView");}; return _CardnetAccountGridView;}} private Element _CardnetAccountGridView;
		public jQueryObject CardnetAccountGridViewJ {get {if (_CardnetAccountGridViewJ == null) {_CardnetAccountGridViewJ = jQuery.Select("#" + clientId + "_CardnetAccountGridView");}; return _CardnetAccountGridViewJ;}} private jQueryObject _CardnetAccountGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element SumAccountLabel {get {if (_SumAccountLabel == null) {_SumAccountLabel = (Element)Document.GetElementById(clientId + "_SumAccountLabel");}; return _SumAccountLabel;}} private Element _SumAccountLabel;
		public jQueryObject SumAccountLabelJ {get {if (_SumAccountLabelJ == null) {_SumAccountLabelJ = jQuery.Select("#" + clientId + "_SumAccountLabel");}; return _SumAccountLabelJ;}} private jQueryObject _SumAccountLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SumTransferCountLabel {get {if (_SumTransferCountLabel == null) {_SumTransferCountLabel = (Element)Document.GetElementById(clientId + "_SumTransferCountLabel");}; return _SumTransferCountLabel;}} private Element _SumTransferCountLabel;
		public jQueryObject SumTransferCountLabelJ {get {if (_SumTransferCountLabelJ == null) {_SumTransferCountLabelJ = jQuery.Select("#" + clientId + "_SumTransferCountLabel");}; return _SumTransferCountLabelJ;}} private jQueryObject _SumTransferCountLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
