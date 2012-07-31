//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.MyEvents
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
		public DivElement EventsPanel {get {if (_EventsPanel == null) {_EventsPanel = (DivElement)Document.GetElementById(clientId + "_EventsPanel");}; return _EventsPanel;}} private DivElement _EventsPanel;
		public jQueryObject EventsPanelJ {get {if (_EventsPanelJ == null) {_EventsPanelJ = jQuery.Select("#" + clientId + "_EventsPanel");}; return _EventsPanelJ;}} private jQueryObject _EventsPanelJ;
		public Element EventsDataGrid {get {if (_EventsDataGrid == null) {_EventsDataGrid = (Element)Document.GetElementById(clientId + "_EventsDataGrid");}; return _EventsDataGrid;}} private Element _EventsDataGrid;
		public jQueryObject EventsDataGridJ {get {if (_EventsDataGridJ == null) {_EventsDataGridJ = jQuery.Select("#" + clientId + "_EventsDataGrid");}; return _EventsDataGridJ;}} private jQueryObject _EventsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
