//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.MyGalleries
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
		public DivElement GalleriesPanel {get {if (_GalleriesPanel == null) {_GalleriesPanel = (DivElement)Document.GetElementById(clientId + "_GalleriesPanel");}; return _GalleriesPanel;}} private DivElement _GalleriesPanel;
		public jQueryObject GalleriesPanelJ {get {if (_GalleriesPanelJ == null) {_GalleriesPanelJ = jQuery.Select("#" + clientId + "_GalleriesPanel");}; return _GalleriesPanelJ;}} private jQueryObject _GalleriesPanelJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GalleriesDataGrid {get {if (_GalleriesDataGrid == null) {_GalleriesDataGrid = (Element)Document.GetElementById(clientId + "_GalleriesDataGrid");}; return _GalleriesDataGrid;}} private Element _GalleriesDataGrid;
		public jQueryObject GalleriesDataGridJ {get {if (_GalleriesDataGridJ == null) {_GalleriesDataGridJ = jQuery.Select("#" + clientId + "_GalleriesDataGrid");}; return _GalleriesDataGridJ;}} private jQueryObject _GalleriesDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public DivElement NoGalleriesPanel {get {if (_NoGalleriesPanel == null) {_NoGalleriesPanel = (DivElement)Document.GetElementById(clientId + "_NoGalleriesPanel");}; return _NoGalleriesPanel;}} private DivElement _NoGalleriesPanel;
		public jQueryObject NoGalleriesPanelJ {get {if (_NoGalleriesPanelJ == null) {_NoGalleriesPanelJ = jQuery.Select("#" + clientId + "_NoGalleriesPanel");}; return _NoGalleriesPanelJ;}} private jQueryObject _NoGalleriesPanelJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
