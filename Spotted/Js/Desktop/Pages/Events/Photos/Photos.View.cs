//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.UpdatePanel", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Events.Photos
{
	public partial class View
		 : Js.Pages.Events.EventUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element GalleryHasNoPhotos {get {if (_GalleryHasNoPhotos == null) {_GalleryHasNoPhotos = (Element)Document.GetElementById(clientId + "_GalleryHasNoPhotos");}; return _GalleryHasNoPhotos;}} private Element _GalleryHasNoPhotos;
		public jQueryObject GalleryHasNoPhotosJ {get {if (_GalleryHasNoPhotosJ == null) {_GalleryHasNoPhotosJ = jQuery.Select("#" + clientId + "_GalleryHasNoPhotos");}; return _GalleryHasNoPhotosJ;}} private jQueryObject _GalleryHasNoPhotosJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement NoPhotosRetryLink {get {if (_NoPhotosRetryLink == null) {_NoPhotosRetryLink = (AnchorElement)Document.GetElementById(clientId + "_NoPhotosRetryLink");}; return _NoPhotosRetryLink;}} private AnchorElement _NoPhotosRetryLink;
		public jQueryObject NoPhotosRetryLinkJ {get {if (_NoPhotosRetryLinkJ == null) {_NoPhotosRetryLinkJ = jQuery.Select("#" + clientId + "_NoPhotosRetryLink");}; return _NoPhotosRetryLinkJ;}} private jQueryObject _NoPhotosRetryLinkJ;
		public AnchorElement NoPhotosEventLink {get {if (_NoPhotosEventLink == null) {_NoPhotosEventLink = (AnchorElement)Document.GetElementById(clientId + "_NoPhotosEventLink");}; return _NoPhotosEventLink;}} private AnchorElement _NoPhotosEventLink;
		public jQueryObject NoPhotosEventLinkJ {get {if (_NoPhotosEventLinkJ == null) {_NoPhotosEventLinkJ = jQuery.Select("#" + clientId + "_NoPhotosEventLink");}; return _NoPhotosEventLinkJ;}} private jQueryObject _NoPhotosEventLinkJ;
		public AnchorElement NoPhotosGalleryEditLink {get {if (_NoPhotosGalleryEditLink == null) {_NoPhotosGalleryEditLink = (AnchorElement)Document.GetElementById(clientId + "_NoPhotosGalleryEditLink");}; return _NoPhotosGalleryEditLink;}} private AnchorElement _NoPhotosGalleryEditLink;
		public jQueryObject NoPhotosGalleryEditLinkJ {get {if (_NoPhotosGalleryEditLinkJ == null) {_NoPhotosGalleryEditLinkJ = jQuery.Select("#" + clientId + "_NoPhotosGalleryEditLink");}; return _NoPhotosGalleryEditLinkJ;}} private jQueryObject _NoPhotosGalleryEditLinkJ;
		public Element GalleryHasPhotos {get {if (_GalleryHasPhotos == null) {_GalleryHasPhotos = (Element)Document.GetElementById(clientId + "_GalleryHasPhotos");}; return _GalleryHasPhotos;}} private Element _GalleryHasPhotos;
		public jQueryObject GalleryHasPhotosJ {get {if (_GalleryHasPhotosJ == null) {_GalleryHasPhotosJ = jQuery.Select("#" + clientId + "_GalleryHasPhotos");}; return _GalleryHasPhotosJ;}} private jQueryObject _GalleryHasPhotosJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiTitle {get {if (_uiTitle == null) {_uiTitle = (Element)Document.GetElementById(clientId + "_uiTitle");}; return _uiTitle;}} private Element _uiTitle;
		public jQueryObject uiTitleJ {get {if (_uiTitleJ == null) {_uiTitleJ = jQuery.Select("#" + clientId + "_uiTitle");}; return _uiTitleJ;}} private jQueryObject _uiTitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element uiEventInfoSpan {get {if (_uiEventInfoSpan == null) {_uiEventInfoSpan = (Element)Document.GetElementById(clientId + "_uiEventInfoSpan");}; return _uiEventInfoSpan;}} private Element _uiEventInfoSpan;
		public jQueryObject uiEventInfoSpanJ {get {if (_uiEventInfoSpanJ == null) {_uiEventInfoSpanJ = jQuery.Select("#" + clientId + "_uiEventInfoSpan");}; return _uiEventInfoSpanJ;}} private jQueryObject _uiEventInfoSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public SelectElement uiCurrentGallery {get {if (_uiCurrentGallery == null) {_uiCurrentGallery = (SelectElement)Document.GetElementById(clientId + "_uiCurrentGallery");}; return _uiCurrentGallery;}} private SelectElement _uiCurrentGallery;
		public jQueryObject uiCurrentGalleryJ {get {if (_uiCurrentGalleryJ == null) {_uiCurrentGalleryJ = jQuery.Select("#" + clientId + "_uiCurrentGallery");}; return _uiCurrentGalleryJ;}} private jQueryObject _uiCurrentGalleryJ;
		public Js.Controls.PhotoBrowser.Controller uiPhotoBrowser {get {return (Js.Controls.PhotoBrowser.Controller) Script.Eval(clientId + "_uiPhotoBrowserController");}}
		public Js.Controls.PhotoControl.Controller uiPhotoControl {get {return (Js.Controls.PhotoControl.Controller) Script.Eval(clientId + "_uiPhotoControlController");}}
		public Element uiUpdatePanel {get {if (_uiUpdatePanel == null) {_uiUpdatePanel = (Element)Document.GetElementById(clientId + "_uiUpdatePanel");}; return _uiUpdatePanel;}} private Element _uiUpdatePanel;
		public jQueryObject uiUpdatePanelJ {get {if (_uiUpdatePanelJ == null) {_uiUpdatePanelJ = jQuery.Select("#" + clientId + "_uiUpdatePanel");}; return _uiUpdatePanelJ;}} private jQueryObject _uiUpdatePanelJ;//mappings.Add("System.Web.UI.UpdatePanel", ElementGetter("Element"));
		public Js.Controls.LatestChat.Controller uiLatestChat {get {return (Js.Controls.LatestChat.Controller) Script.Eval(clientId + "_uiLatestChatController");}}
		public Js.Controls.ThreadControl.Controller uiThreadControl {get {return (Js.Controls.ThreadControl.Controller) Script.Eval(clientId + "_uiThreadControlController");}}
		public InputElement uiGalleryK {get {if (_uiGalleryK == null) {_uiGalleryK = (InputElement)Document.GetElementById(clientId + "_uiGalleryK");}; return _uiGalleryK;}} private InputElement _uiGalleryK;
		public jQueryObject uiGalleryKJ {get {if (_uiGalleryKJ == null) {_uiGalleryKJ = jQuery.Select("#" + clientId + "_uiGalleryK");}; return _uiGalleryKJ;}} private jQueryObject _uiGalleryKJ;
		public InputElement uiEventK {get {if (_uiEventK == null) {_uiEventK = (InputElement)Document.GetElementById(clientId + "_uiEventK");}; return _uiEventK;}} private InputElement _uiEventK;
		public jQueryObject uiEventKJ {get {if (_uiEventKJ == null) {_uiEventKJ = jQuery.Select("#" + clientId + "_uiEventK");}; return _uiEventKJ;}} private jQueryObject _uiEventKJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
