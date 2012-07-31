//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.Tags
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
		public Element uiTitle {get {if (_uiTitle == null) {_uiTitle = (Element)Document.GetElementById(clientId + "_uiTitle");}; return _uiTitle;}} private Element _uiTitle;
		public jQueryObject uiTitleJ {get {if (_uiTitleJ == null) {_uiTitleJ = jQuery.Select("#" + clientId + "_uiTitle");}; return _uiTitleJ;}} private jQueryObject _uiTitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement uiFiltersPanel {get {if (_uiFiltersPanel == null) {_uiFiltersPanel = (DivElement)Document.GetElementById(clientId + "_uiFiltersPanel");}; return _uiFiltersPanel;}} private DivElement _uiFiltersPanel;
		public jQueryObject uiFiltersPanelJ {get {if (_uiFiltersPanelJ == null) {_uiFiltersPanelJ = jQuery.Select("#" + clientId + "_uiFiltersPanel");}; return _uiFiltersPanelJ;}} private jQueryObject _uiFiltersPanelJ;
		public InputElement uiTagTextFilter {get {if (_uiTagTextFilter == null) {_uiTagTextFilter = (InputElement)Document.GetElementById(clientId + "_uiTagTextFilter");}; return _uiTagTextFilter;}} private InputElement _uiTagTextFilter;
		public jQueryObject uiTagTextFilterJ {get {if (_uiTagTextFilterJ == null) {_uiTagTextFilterJ = jQuery.Select("#" + clientId + "_uiTagTextFilter");}; return _uiTagTextFilterJ;}} private jQueryObject _uiTagTextFilterJ;
		public Element uiBlockedFilter {get {if (_uiBlockedFilter == null) {_uiBlockedFilter = (Element)Document.GetElementById(clientId + "_uiBlockedFilter");}; return _uiBlockedFilter;}} private Element _uiBlockedFilter;
		public jQueryObject uiBlockedFilterJ {get {if (_uiBlockedFilterJ == null) {_uiBlockedFilterJ = jQuery.Select("#" + clientId + "_uiBlockedFilter");}; return _uiBlockedFilterJ;}} private jQueryObject _uiBlockedFilterJ;//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
		public Element uiShowInTagCloudFilter {get {if (_uiShowInTagCloudFilter == null) {_uiShowInTagCloudFilter = (Element)Document.GetElementById(clientId + "_uiShowInTagCloudFilter");}; return _uiShowInTagCloudFilter;}} private Element _uiShowInTagCloudFilter;
		public jQueryObject uiShowInTagCloudFilterJ {get {if (_uiShowInTagCloudFilterJ == null) {_uiShowInTagCloudFilterJ = jQuery.Select("#" + clientId + "_uiShowInTagCloudFilter");}; return _uiShowInTagCloudFilterJ;}} private jQueryObject _uiShowInTagCloudFilterJ;//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
		public Element uiFilter {get {if (_uiFilter == null) {_uiFilter = (Element)Document.GetElementById(clientId + "_uiFilter");}; return _uiFilter;}} private Element _uiFilter;
		public jQueryObject uiFilterJ {get {if (_uiFilterJ == null) {_uiFilterJ = jQuery.Select("#" + clientId + "_uiFilter");}; return _uiFilterJ;}} private jQueryObject _uiFilterJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiResultsTitle {get {if (_uiResultsTitle == null) {_uiResultsTitle = (Element)Document.GetElementById(clientId + "_uiResultsTitle");}; return _uiResultsTitle;}} private Element _uiResultsTitle;
		public jQueryObject uiResultsTitleJ {get {if (_uiResultsTitleJ == null) {_uiResultsTitleJ = jQuery.Select("#" + clientId + "_uiResultsTitle");}; return _uiResultsTitleJ;}} private jQueryObject _uiResultsTitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement uiResultsPanel {get {if (_uiResultsPanel == null) {_uiResultsPanel = (DivElement)Document.GetElementById(clientId + "_uiResultsPanel");}; return _uiResultsPanel;}} private DivElement _uiResultsPanel;
		public jQueryObject uiResultsPanelJ {get {if (_uiResultsPanelJ == null) {_uiResultsPanelJ = jQuery.Select("#" + clientId + "_uiResultsPanel");}; return _uiResultsPanelJ;}} private jQueryObject _uiResultsPanelJ;
		public Element uiSaveChanges {get {if (_uiSaveChanges == null) {_uiSaveChanges = (Element)Document.GetElementById(clientId + "_uiSaveChanges");}; return _uiSaveChanges;}} private Element _uiSaveChanges;
		public jQueryObject uiSaveChangesJ {get {if (_uiSaveChangesJ == null) {_uiSaveChangesJ = jQuery.Select("#" + clientId + "_uiSaveChanges");}; return _uiSaveChangesJ;}} private jQueryObject _uiSaveChangesJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiResults {get {if (_uiResults == null) {_uiResults = (Element)Document.GetElementById(clientId + "_uiResults");}; return _uiResults;}} private Element _uiResults;
		public jQueryObject uiResultsJ {get {if (_uiResultsJ == null) {_uiResultsJ = jQuery.Select("#" + clientId + "_uiResults");}; return _uiResultsJ;}} private jQueryObject _uiResultsJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
