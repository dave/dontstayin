//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.MyVenues
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
		public DivElement VenuesPanel {get {if (_VenuesPanel == null) {_VenuesPanel = (DivElement)Document.GetElementById(clientId + "_VenuesPanel");}; return _VenuesPanel;}} private DivElement _VenuesPanel;
		public jQueryObject VenuesPanelJ {get {if (_VenuesPanelJ == null) {_VenuesPanelJ = jQuery.Select("#" + clientId + "_VenuesPanel");}; return _VenuesPanelJ;}} private jQueryObject _VenuesPanelJ;
		public Element VenuesDataGrid {get {if (_VenuesDataGrid == null) {_VenuesDataGrid = (Element)Document.GetElementById(clientId + "_VenuesDataGrid");}; return _VenuesDataGrid;}} private Element _VenuesDataGrid;
		public jQueryObject VenuesDataGridJ {get {if (_VenuesDataGridJ == null) {_VenuesDataGridJ = jQuery.Select("#" + clientId + "_VenuesDataGrid");}; return _VenuesDataGridJ;}} private jQueryObject _VenuesDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
