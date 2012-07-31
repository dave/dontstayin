//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Cal", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.MyPhotos
{
	public partial class View
		 : Js.Pages.Usrs.UsrUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element UsrIntro {get {if (_UsrIntro == null) {_UsrIntro = (Element)Document.GetElementById(clientId + "_UsrIntro");}; return _UsrIntro;}} private Element _UsrIntro;
		public jQueryObject UsrIntroJ {get {if (_UsrIntroJ == null) {_UsrIntroJ = jQuery.Select("#" + clientId + "_UsrIntro");}; return _UsrIntroJ;}} private jQueryObject _UsrIntroJ;//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
		public Element TakenBySpan {get {if (_TakenBySpan == null) {_TakenBySpan = (Element)Document.GetElementById(clientId + "_TakenBySpan");}; return _TakenBySpan;}} private Element _TakenBySpan;
		public jQueryObject TakenBySpanJ {get {if (_TakenBySpanJ == null) {_TakenBySpanJ = jQuery.Select("#" + clientId + "_TakenBySpan");}; return _TakenBySpanJ;}} private jQueryObject _TakenBySpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element Cal {get {if (_Cal == null) {_Cal = (Element)Document.GetElementById(clientId + "_Cal");}; return _Cal;}} private Element _Cal;
		public jQueryObject CalJ {get {if (_CalJ == null) {_CalJ = jQuery.Select("#" + clientId + "_Cal");}; return _CalJ;}} private jQueryObject _CalJ;//mappings.Add("Spotted.Controls.Cal", ElementGetter("Element"));
		public DivElement PanelPhotos {get {if (_PanelPhotos == null) {_PanelPhotos = (DivElement)Document.GetElementById(clientId + "_PanelPhotos");}; return _PanelPhotos;}} private DivElement _PanelPhotos;
		public jQueryObject PanelPhotosJ {get {if (_PanelPhotosJ == null) {_PanelPhotosJ = jQuery.Select("#" + clientId + "_PanelPhotos");}; return _PanelPhotosJ;}} private jQueryObject _PanelPhotosJ;
		public DivElement PhotosPanel {get {if (_PhotosPanel == null) {_PhotosPanel = (DivElement)Document.GetElementById(clientId + "_PhotosPanel");}; return _PhotosPanel;}} private DivElement _PhotosPanel;
		public jQueryObject PhotosPanelJ {get {if (_PhotosPanelJ == null) {_PhotosPanelJ = jQuery.Select("#" + clientId + "_PhotosPanel");}; return _PhotosPanelJ;}} private jQueryObject _PhotosPanelJ;
		public DivElement NoPhotosPanel {get {if (_NoPhotosPanel == null) {_NoPhotosPanel = (DivElement)Document.GetElementById(clientId + "_NoPhotosPanel");}; return _NoPhotosPanel;}} private DivElement _NoPhotosPanel;
		public jQueryObject NoPhotosPanelJ {get {if (_NoPhotosPanelJ == null) {_NoPhotosPanelJ = jQuery.Select("#" + clientId + "_NoPhotosPanel");}; return _NoPhotosPanelJ;}} private jQueryObject _NoPhotosPanelJ;
		public Element PhotosPageP {get {if (_PhotosPageP == null) {_PhotosPageP = (Element)Document.GetElementById(clientId + "_PhotosPageP");}; return _PhotosPageP;}} private Element _PhotosPageP;
		public jQueryObject PhotosPagePJ {get {if (_PhotosPagePJ == null) {_PhotosPagePJ = jQuery.Select("#" + clientId + "_PhotosPageP");}; return _PhotosPagePJ;}} private jQueryObject _PhotosPagePJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element PhotosPageP1 {get {if (_PhotosPageP1 == null) {_PhotosPageP1 = (Element)Document.GetElementById(clientId + "_PhotosPageP1");}; return _PhotosPageP1;}} private Element _PhotosPageP1;
		public jQueryObject PhotosPageP1J {get {if (_PhotosPageP1J == null) {_PhotosPageP1J = jQuery.Select("#" + clientId + "_PhotosPageP1");}; return _PhotosPageP1J;}} private jQueryObject _PhotosPageP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement PhotosNextPageLink {get {if (_PhotosNextPageLink == null) {_PhotosNextPageLink = (AnchorElement)Document.GetElementById(clientId + "_PhotosNextPageLink");}; return _PhotosNextPageLink;}} private AnchorElement _PhotosNextPageLink;
		public jQueryObject PhotosNextPageLinkJ {get {if (_PhotosNextPageLinkJ == null) {_PhotosNextPageLinkJ = jQuery.Select("#" + clientId + "_PhotosNextPageLink");}; return _PhotosNextPageLinkJ;}} private jQueryObject _PhotosNextPageLinkJ;
		public AnchorElement PhotosNextPageLink1 {get {if (_PhotosNextPageLink1 == null) {_PhotosNextPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_PhotosNextPageLink1");}; return _PhotosNextPageLink1;}} private AnchorElement _PhotosNextPageLink1;
		public jQueryObject PhotosNextPageLink1J {get {if (_PhotosNextPageLink1J == null) {_PhotosNextPageLink1J = jQuery.Select("#" + clientId + "_PhotosNextPageLink1");}; return _PhotosNextPageLink1J;}} private jQueryObject _PhotosNextPageLink1J;
		public AnchorElement PhotosPrevPageLink {get {if (_PhotosPrevPageLink == null) {_PhotosPrevPageLink = (AnchorElement)Document.GetElementById(clientId + "_PhotosPrevPageLink");}; return _PhotosPrevPageLink;}} private AnchorElement _PhotosPrevPageLink;
		public jQueryObject PhotosPrevPageLinkJ {get {if (_PhotosPrevPageLinkJ == null) {_PhotosPrevPageLinkJ = jQuery.Select("#" + clientId + "_PhotosPrevPageLink");}; return _PhotosPrevPageLinkJ;}} private jQueryObject _PhotosPrevPageLinkJ;
		public AnchorElement PhotosPrevPageLink1 {get {if (_PhotosPrevPageLink1 == null) {_PhotosPrevPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_PhotosPrevPageLink1");}; return _PhotosPrevPageLink1;}} private AnchorElement _PhotosPrevPageLink1;
		public jQueryObject PhotosPrevPageLink1J {get {if (_PhotosPrevPageLink1J == null) {_PhotosPrevPageLink1J = jQuery.Select("#" + clientId + "_PhotosPrevPageLink1");}; return _PhotosPrevPageLink1J;}} private jQueryObject _PhotosPrevPageLink1J;
		public Element PhotosDataList {get {if (_PhotosDataList == null) {_PhotosDataList = (Element)Document.GetElementById(clientId + "_PhotosDataList");}; return _PhotosDataList;}} private Element _PhotosDataList;
		public jQueryObject PhotosDataListJ {get {if (_PhotosDataListJ == null) {_PhotosDataListJ = jQuery.Select("#" + clientId + "_PhotosDataList");}; return _PhotosDataListJ;}} private jQueryObject _PhotosDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
