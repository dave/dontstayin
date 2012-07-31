//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Galleries.Paged
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
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public ImageElement GalleryPicImg {get {if (_GalleryPicImg == null) {_GalleryPicImg = (ImageElement)Document.GetElementById(clientId + "_GalleryPicImg");}; return _GalleryPicImg;}} private ImageElement _GalleryPicImg;
		public jQueryObject GalleryPicImgJ {get {if (_GalleryPicImgJ == null) {_GalleryPicImgJ = jQuery.Select("#" + clientId + "_GalleryPicImg");}; return _GalleryPicImgJ;}} private jQueryObject _GalleryPicImgJ;
		public AnchorElement EventLink {get {if (_EventLink == null) {_EventLink = (AnchorElement)Document.GetElementById(clientId + "_EventLink");}; return _EventLink;}} private AnchorElement _EventLink;
		public jQueryObject EventLinkJ {get {if (_EventLinkJ == null) {_EventLinkJ = jQuery.Select("#" + clientId + "_EventLink");}; return _EventLinkJ;}} private jQueryObject _EventLinkJ;
		public AnchorElement EventVenueLink {get {if (_EventVenueLink == null) {_EventVenueLink = (AnchorElement)Document.GetElementById(clientId + "_EventVenueLink");}; return _EventVenueLink;}} private AnchorElement _EventVenueLink;
		public jQueryObject EventVenueLinkJ {get {if (_EventVenueLinkJ == null) {_EventVenueLinkJ = jQuery.Select("#" + clientId + "_EventVenueLink");}; return _EventVenueLinkJ;}} private jQueryObject _EventVenueLinkJ;
		public AnchorElement EventPlaceLink {get {if (_EventPlaceLink == null) {_EventPlaceLink = (AnchorElement)Document.GetElementById(clientId + "_EventPlaceLink");}; return _EventPlaceLink;}} private AnchorElement _EventPlaceLink;
		public jQueryObject EventPlaceLinkJ {get {if (_EventPlaceLinkJ == null) {_EventPlaceLinkJ = jQuery.Select("#" + clientId + "_EventPlaceLink");}; return _EventPlaceLinkJ;}} private jQueryObject _EventPlaceLinkJ;
		public AnchorElement DiscussionLink {get {if (_DiscussionLink == null) {_DiscussionLink = (AnchorElement)Document.GetElementById(clientId + "_DiscussionLink");}; return _DiscussionLink;}} private AnchorElement _DiscussionLink;
		public jQueryObject DiscussionLinkJ {get {if (_DiscussionLinkJ == null) {_DiscussionLinkJ = jQuery.Select("#" + clientId + "_DiscussionLink");}; return _DiscussionLinkJ;}} private jQueryObject _DiscussionLinkJ;
		public AnchorElement OwnerLink {get {if (_OwnerLink == null) {_OwnerLink = (AnchorElement)Document.GetElementById(clientId + "_OwnerLink");}; return _OwnerLink;}} private AnchorElement _OwnerLink;
		public jQueryObject OwnerLinkJ {get {if (_OwnerLinkJ == null) {_OwnerLinkJ = jQuery.Select("#" + clientId + "_OwnerLink");}; return _OwnerLinkJ;}} private jQueryObject _OwnerLinkJ;
		public AnchorElement LinkBack {get {if (_LinkBack == null) {_LinkBack = (AnchorElement)Document.GetElementById(clientId + "_LinkBack");}; return _LinkBack;}} private AnchorElement _LinkBack;
		public jQueryObject LinkBackJ {get {if (_LinkBackJ == null) {_LinkBackJ = jQuery.Select("#" + clientId + "_LinkBack");}; return _LinkBackJ;}} private jQueryObject _LinkBackJ;
		public AnchorElement QuickBrowserLink {get {if (_QuickBrowserLink == null) {_QuickBrowserLink = (AnchorElement)Document.GetElementById(clientId + "_QuickBrowserLink");}; return _QuickBrowserLink;}} private AnchorElement _QuickBrowserLink;
		public jQueryObject QuickBrowserLinkJ {get {if (_QuickBrowserLinkJ == null) {_QuickBrowserLinkJ = jQuery.Select("#" + clientId + "_QuickBrowserLink");}; return _QuickBrowserLinkJ;}} private jQueryObject _QuickBrowserLinkJ;
		public Element EventDate {get {if (_EventDate == null) {_EventDate = (Element)Document.GetElementById(clientId + "_EventDate");}; return _EventDate;}} private Element _EventDate;
		public jQueryObject EventDateJ {get {if (_EventDateJ == null) {_EventDateJ = jQuery.Select("#" + clientId + "_EventDate");}; return _EventDateJ;}} private jQueryObject _EventDateJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PicCell {get {if (_PicCell == null) {_PicCell = (Element)Document.GetElementById(clientId + "_PicCell");}; return _PicCell;}} private Element _PicCell;
		public jQueryObject PicCellJ {get {if (_PicCellJ == null) {_PicCellJ = jQuery.Select("#" + clientId + "_PicCell");}; return _PicCellJ;}} private jQueryObject _PicCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element EventLinkP {get {if (_EventLinkP == null) {_EventLinkP = (Element)Document.GetElementById(clientId + "_EventLinkP");}; return _EventLinkP;}} private Element _EventLinkP;
		public jQueryObject EventLinkPJ {get {if (_EventLinkPJ == null) {_EventLinkPJ = jQuery.Select("#" + clientId + "_EventLinkP");}; return _EventLinkPJ;}} private jQueryObject _EventLinkPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ArticleLinkP {get {if (_ArticleLinkP == null) {_ArticleLinkP = (Element)Document.GetElementById(clientId + "_ArticleLinkP");}; return _ArticleLinkP;}} private Element _ArticleLinkP;
		public jQueryObject ArticleLinkPJ {get {if (_ArticleLinkPJ == null) {_ArticleLinkPJ = jQuery.Select("#" + clientId + "_ArticleLinkP");}; return _ArticleLinkPJ;}} private jQueryObject _ArticleLinkPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement ArticleLink {get {if (_ArticleLink == null) {_ArticleLink = (AnchorElement)Document.GetElementById(clientId + "_ArticleLink");}; return _ArticleLink;}} private AnchorElement _ArticleLink;
		public jQueryObject ArticleLinkJ {get {if (_ArticleLinkJ == null) {_ArticleLinkJ = jQuery.Select("#" + clientId + "_ArticleLink");}; return _ArticleLinkJ;}} private jQueryObject _ArticleLinkJ;
		public Element DiscussionLinkCommentsLabel {get {if (_DiscussionLinkCommentsLabel == null) {_DiscussionLinkCommentsLabel = (Element)Document.GetElementById(clientId + "_DiscussionLinkCommentsLabel");}; return _DiscussionLinkCommentsLabel;}} private Element _DiscussionLinkCommentsLabel;
		public jQueryObject DiscussionLinkCommentsLabelJ {get {if (_DiscussionLinkCommentsLabelJ == null) {_DiscussionLinkCommentsLabelJ = jQuery.Select("#" + clientId + "_DiscussionLinkCommentsLabel");}; return _DiscussionLinkCommentsLabelJ;}} private jQueryObject _DiscussionLinkCommentsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element DiscussionLinkTargetLabel {get {if (_DiscussionLinkTargetLabel == null) {_DiscussionLinkTargetLabel = (Element)Document.GetElementById(clientId + "_DiscussionLinkTargetLabel");}; return _DiscussionLinkTargetLabel;}} private Element _DiscussionLinkTargetLabel;
		public jQueryObject DiscussionLinkTargetLabelJ {get {if (_DiscussionLinkTargetLabelJ == null) {_DiscussionLinkTargetLabelJ = jQuery.Select("#" + clientId + "_DiscussionLinkTargetLabel");}; return _DiscussionLinkTargetLabelJ;}} private jQueryObject _DiscussionLinkTargetLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement NoPhotosPanel {get {if (_NoPhotosPanel == null) {_NoPhotosPanel = (DivElement)Document.GetElementById(clientId + "_NoPhotosPanel");}; return _NoPhotosPanel;}} private DivElement _NoPhotosPanel;
		public jQueryObject NoPhotosPanelJ {get {if (_NoPhotosPanelJ == null) {_NoPhotosPanelJ = jQuery.Select("#" + clientId + "_NoPhotosPanel");}; return _NoPhotosPanelJ;}} private jQueryObject _NoPhotosPanelJ;
		public DivElement PhotosPanel {get {if (_PhotosPanel == null) {_PhotosPanel = (DivElement)Document.GetElementById(clientId + "_PhotosPanel");}; return _PhotosPanel;}} private DivElement _PhotosPanel;
		public jQueryObject PhotosPanelJ {get {if (_PhotosPanelJ == null) {_PhotosPanelJ = jQuery.Select("#" + clientId + "_PhotosPanel");}; return _PhotosPanelJ;}} private jQueryObject _PhotosPanelJ;
		public Element PhotosDataList {get {if (_PhotosDataList == null) {_PhotosDataList = (Element)Document.GetElementById(clientId + "_PhotosDataList");}; return _PhotosDataList;}} private Element _PhotosDataList;
		public jQueryObject PhotosDataListJ {get {if (_PhotosDataListJ == null) {_PhotosDataListJ = jQuery.Select("#" + clientId + "_PhotosDataList");}; return _PhotosDataListJ;}} private jQueryObject _PhotosDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element PhotoPageLinksP {get {if (_PhotoPageLinksP == null) {_PhotoPageLinksP = (Element)Document.GetElementById(clientId + "_PhotoPageLinksP");}; return _PhotoPageLinksP;}} private Element _PhotoPageLinksP;
		public jQueryObject PhotoPageLinksPJ {get {if (_PhotoPageLinksPJ == null) {_PhotoPageLinksPJ = jQuery.Select("#" + clientId + "_PhotoPageLinksP");}; return _PhotoPageLinksPJ;}} private jQueryObject _PhotoPageLinksPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element PhotoPageLinksP1 {get {if (_PhotoPageLinksP1 == null) {_PhotoPageLinksP1 = (Element)Document.GetElementById(clientId + "_PhotoPageLinksP1");}; return _PhotoPageLinksP1;}} private Element _PhotoPageLinksP1;
		public jQueryObject PhotoPageLinksP1J {get {if (_PhotoPageLinksP1J == null) {_PhotoPageLinksP1J = jQuery.Select("#" + clientId + "_PhotoPageLinksP1");}; return _PhotoPageLinksP1J;}} private jQueryObject _PhotoPageLinksP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement MiscInfoPanel {get {if (_MiscInfoPanel == null) {_MiscInfoPanel = (DivElement)Document.GetElementById(clientId + "_MiscInfoPanel");}; return _MiscInfoPanel;}} private DivElement _MiscInfoPanel;
		public jQueryObject MiscInfoPanelJ {get {if (_MiscInfoPanelJ == null) {_MiscInfoPanelJ = jQuery.Select("#" + clientId + "_MiscInfoPanel");}; return _MiscInfoPanelJ;}} private jQueryObject _MiscInfoPanelJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
