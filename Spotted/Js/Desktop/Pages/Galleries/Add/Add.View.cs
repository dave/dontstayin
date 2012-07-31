//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Galleries.Add
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
		public DivElement NoEditArticlePanel {get {if (_NoEditArticlePanel == null) {_NoEditArticlePanel = (DivElement)Document.GetElementById(clientId + "_NoEditArticlePanel");}; return _NoEditArticlePanel;}} private DivElement _NoEditArticlePanel;
		public jQueryObject NoEditArticlePanelJ {get {if (_NoEditArticlePanelJ == null) {_NoEditArticlePanelJ = jQuery.Select("#" + clientId + "_NoEditArticlePanel");}; return _NoEditArticlePanelJ;}} private jQueryObject _NoEditArticlePanelJ;
		public DivElement CantAddGallery {get {if (_CantAddGallery == null) {_CantAddGallery = (DivElement)Document.GetElementById(clientId + "_CantAddGallery");}; return _CantAddGallery;}} private DivElement _CantAddGallery;
		public jQueryObject CantAddGalleryJ {get {if (_CantAddGalleryJ == null) {_CantAddGalleryJ = jQuery.Select("#" + clientId + "_CantAddGallery");}; return _CantAddGalleryJ;}} private jQueryObject _CantAddGalleryJ;
		public DivElement PanelNoPhoto {get {if (_PanelNoPhoto == null) {_PanelNoPhoto = (DivElement)Document.GetElementById(clientId + "_PanelNoPhoto");}; return _PanelNoPhoto;}} private DivElement _PanelNoPhoto;
		public jQueryObject PanelNoPhotoJ {get {if (_PanelNoPhotoJ == null) {_PanelNoPhotoJ = jQuery.Select("#" + clientId + "_PanelNoPhoto");}; return _PanelNoPhotoJ;}} private jQueryObject _PanelNoPhotoJ;
		public AnchorElement EditCurrentGalleryLink {get {if (_EditCurrentGalleryLink == null) {_EditCurrentGalleryLink = (AnchorElement)Document.GetElementById(clientId + "_EditCurrentGalleryLink");}; return _EditCurrentGalleryLink;}} private AnchorElement _EditCurrentGalleryLink;
		public jQueryObject EditCurrentGalleryLinkJ {get {if (_EditCurrentGalleryLinkJ == null) {_EditCurrentGalleryLinkJ = jQuery.Select("#" + clientId + "_EditCurrentGalleryLink");}; return _EditCurrentGalleryLinkJ;}} private jQueryObject _EditCurrentGalleryLinkJ;
		public DivElement EventHasGalleriesPanel {get {if (_EventHasGalleriesPanel == null) {_EventHasGalleriesPanel = (DivElement)Document.GetElementById(clientId + "_EventHasGalleriesPanel");}; return _EventHasGalleriesPanel;}} private DivElement _EventHasGalleriesPanel;
		public jQueryObject EventHasGalleriesPanelJ {get {if (_EventHasGalleriesPanelJ == null) {_EventHasGalleriesPanelJ = jQuery.Select("#" + clientId + "_EventHasGalleriesPanel");}; return _EventHasGalleriesPanelJ;}} private jQueryObject _EventHasGalleriesPanelJ;
		public Element GalleriesDataGrid {get {if (_GalleriesDataGrid == null) {_GalleriesDataGrid = (Element)Document.GetElementById(clientId + "_GalleriesDataGrid");}; return _GalleriesDataGrid;}} private Element _GalleriesDataGrid;
		public jQueryObject GalleriesDataGridJ {get {if (_GalleriesDataGridJ == null) {_GalleriesDataGridJ = jQuery.Select("#" + clientId + "_GalleriesDataGrid");}; return _GalleriesDataGridJ;}} private jQueryObject _GalleriesDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public DivElement FutureEventPanel {get {if (_FutureEventPanel == null) {_FutureEventPanel = (DivElement)Document.GetElementById(clientId + "_FutureEventPanel");}; return _FutureEventPanel;}} private DivElement _FutureEventPanel;
		public jQueryObject FutureEventPanelJ {get {if (_FutureEventPanelJ == null) {_FutureEventPanelJ = jQuery.Select("#" + clientId + "_FutureEventPanel");}; return _FutureEventPanelJ;}} private jQueryObject _FutureEventPanelJ;
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
