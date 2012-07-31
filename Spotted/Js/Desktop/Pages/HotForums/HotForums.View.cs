//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.HotForums
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
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelBoardList {get {if (_PanelBoardList == null) {_PanelBoardList = (DivElement)Document.GetElementById(clientId + "_PanelBoardList");}; return _PanelBoardList;}} private DivElement _PanelBoardList;
		public jQueryObject PanelBoardListJ {get {if (_PanelBoardListJ == null) {_PanelBoardListJ = jQuery.Select("#" + clientId + "_PanelBoardList");}; return _PanelBoardListJ;}} private jQueryObject _PanelBoardListJ;
		public DivElement BoardPlacePanel {get {if (_BoardPlacePanel == null) {_BoardPlacePanel = (DivElement)Document.GetElementById(clientId + "_BoardPlacePanel");}; return _BoardPlacePanel;}} private DivElement _BoardPlacePanel;
		public jQueryObject BoardPlacePanelJ {get {if (_BoardPlacePanelJ == null) {_BoardPlacePanelJ = jQuery.Select("#" + clientId + "_BoardPlacePanel");}; return _BoardPlacePanelJ;}} private jQueryObject _BoardPlacePanelJ;
		public DivElement BoardEventPanel {get {if (_BoardEventPanel == null) {_BoardEventPanel = (DivElement)Document.GetElementById(clientId + "_BoardEventPanel");}; return _BoardEventPanel;}} private DivElement _BoardEventPanel;
		public jQueryObject BoardEventPanelJ {get {if (_BoardEventPanelJ == null) {_BoardEventPanelJ = jQuery.Select("#" + clientId + "_BoardEventPanel");}; return _BoardEventPanelJ;}} private jQueryObject _BoardEventPanelJ;
		public DivElement BoardThreadPanel {get {if (_BoardThreadPanel == null) {_BoardThreadPanel = (DivElement)Document.GetElementById(clientId + "_BoardThreadPanel");}; return _BoardThreadPanel;}} private DivElement _BoardThreadPanel;
		public jQueryObject BoardThreadPanelJ {get {if (_BoardThreadPanelJ == null) {_BoardThreadPanelJ = jQuery.Select("#" + clientId + "_BoardThreadPanel");}; return _BoardThreadPanelJ;}} private jQueryObject _BoardThreadPanelJ;
		public Element BoardDataGrid {get {if (_BoardDataGrid == null) {_BoardDataGrid = (Element)Document.GetElementById(clientId + "_BoardDataGrid");}; return _BoardDataGrid;}} private Element _BoardDataGrid;
		public jQueryObject BoardDataGridJ {get {if (_BoardDataGridJ == null) {_BoardDataGridJ = jQuery.Select("#" + clientId + "_BoardDataGrid");}; return _BoardDataGridJ;}} private jQueryObject _BoardDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element BoardPlaceDataGrid {get {if (_BoardPlaceDataGrid == null) {_BoardPlaceDataGrid = (Element)Document.GetElementById(clientId + "_BoardPlaceDataGrid");}; return _BoardPlaceDataGrid;}} private Element _BoardPlaceDataGrid;
		public jQueryObject BoardPlaceDataGridJ {get {if (_BoardPlaceDataGridJ == null) {_BoardPlaceDataGridJ = jQuery.Select("#" + clientId + "_BoardPlaceDataGrid");}; return _BoardPlaceDataGridJ;}} private jQueryObject _BoardPlaceDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element BoardEventDataGrid {get {if (_BoardEventDataGrid == null) {_BoardEventDataGrid = (Element)Document.GetElementById(clientId + "_BoardEventDataGrid");}; return _BoardEventDataGrid;}} private Element _BoardEventDataGrid;
		public jQueryObject BoardEventDataGridJ {get {if (_BoardEventDataGridJ == null) {_BoardEventDataGridJ = jQuery.Select("#" + clientId + "_BoardEventDataGrid");}; return _BoardEventDataGridJ;}} private jQueryObject _BoardEventDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element BoardThreadDataGrid {get {if (_BoardThreadDataGrid == null) {_BoardThreadDataGrid = (Element)Document.GetElementById(clientId + "_BoardThreadDataGrid");}; return _BoardThreadDataGrid;}} private Element _BoardThreadDataGrid;
		public jQueryObject BoardThreadDataGridJ {get {if (_BoardThreadDataGridJ == null) {_BoardThreadDataGridJ = jQuery.Select("#" + clientId + "_BoardThreadDataGrid");}; return _BoardThreadDataGridJ;}} private jQueryObject _BoardThreadDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public AnchorElement HotTopicsHomeCountryLink {get {if (_HotTopicsHomeCountryLink == null) {_HotTopicsHomeCountryLink = (AnchorElement)Document.GetElementById(clientId + "_HotTopicsHomeCountryLink");}; return _HotTopicsHomeCountryLink;}} private AnchorElement _HotTopicsHomeCountryLink;
		public jQueryObject HotTopicsHomeCountryLinkJ {get {if (_HotTopicsHomeCountryLinkJ == null) {_HotTopicsHomeCountryLinkJ = jQuery.Select("#" + clientId + "_HotTopicsHomeCountryLink");}; return _HotTopicsHomeCountryLinkJ;}} private jQueryObject _HotTopicsHomeCountryLinkJ;
		public AnchorElement HotTopicsCountryLink {get {if (_HotTopicsCountryLink == null) {_HotTopicsCountryLink = (AnchorElement)Document.GetElementById(clientId + "_HotTopicsCountryLink");}; return _HotTopicsCountryLink;}} private AnchorElement _HotTopicsCountryLink;
		public jQueryObject HotTopicsCountryLinkJ {get {if (_HotTopicsCountryLinkJ == null) {_HotTopicsCountryLinkJ = jQuery.Select("#" + clientId + "_HotTopicsCountryLink");}; return _HotTopicsCountryLinkJ;}} private jQueryObject _HotTopicsCountryLinkJ;
		public DivElement HotTopicsCountryPanel {get {if (_HotTopicsCountryPanel == null) {_HotTopicsCountryPanel = (DivElement)Document.GetElementById(clientId + "_HotTopicsCountryPanel");}; return _HotTopicsCountryPanel;}} private DivElement _HotTopicsCountryPanel;
		public jQueryObject HotTopicsCountryPanelJ {get {if (_HotTopicsCountryPanelJ == null) {_HotTopicsCountryPanelJ = jQuery.Select("#" + clientId + "_HotTopicsCountryPanel");}; return _HotTopicsCountryPanelJ;}} private jQueryObject _HotTopicsCountryPanelJ;
		public DivElement HotTopicsWorldwidePanel {get {if (_HotTopicsWorldwidePanel == null) {_HotTopicsWorldwidePanel = (DivElement)Document.GetElementById(clientId + "_HotTopicsWorldwidePanel");}; return _HotTopicsWorldwidePanel;}} private DivElement _HotTopicsWorldwidePanel;
		public jQueryObject HotTopicsWorldwidePanelJ {get {if (_HotTopicsWorldwidePanelJ == null) {_HotTopicsWorldwidePanelJ = jQuery.Select("#" + clientId + "_HotTopicsWorldwidePanel");}; return _HotTopicsWorldwidePanelJ;}} private jQueryObject _HotTopicsWorldwidePanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
