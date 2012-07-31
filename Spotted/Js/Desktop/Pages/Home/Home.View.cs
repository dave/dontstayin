//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.OutBox", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.ExploreBox", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.Pages.HomeContent", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Latest", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Home
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
		public Element TopPhotoHolder {get {if (_TopPhotoHolder == null) {_TopPhotoHolder = (Element)Document.GetElementById(clientId + "_TopPhotoHolder");}; return _TopPhotoHolder;}} private Element _TopPhotoHolder;
		public jQueryObject TopPhotoHolderJ {get {if (_TopPhotoHolderJ == null) {_TopPhotoHolderJ = jQuery.Select("#" + clientId + "_TopPhotoHolder");}; return _TopPhotoHolderJ;}} private jQueryObject _TopPhotoHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element PhotoLinksHolder {get {if (_PhotoLinksHolder == null) {_PhotoLinksHolder = (Element)Document.GetElementById(clientId + "_PhotoLinksHolder");}; return _PhotoLinksHolder;}} private Element _PhotoLinksHolder;
		public jQueryObject PhotoLinksHolderJ {get {if (_PhotoLinksHolderJ == null) {_PhotoLinksHolderJ = jQuery.Select("#" + clientId + "_PhotoLinksHolder");}; return _PhotoLinksHolderJ;}} private jQueryObject _PhotoLinksHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element PhotoSoptterHolder {get {if (_PhotoSoptterHolder == null) {_PhotoSoptterHolder = (Element)Document.GetElementById(clientId + "_PhotoSoptterHolder");}; return _PhotoSoptterHolder;}} private Element _PhotoSoptterHolder;
		public jQueryObject PhotoSoptterHolderJ {get {if (_PhotoSoptterHolderJ == null) {_PhotoSoptterHolderJ = jQuery.Select("#" + clientId + "_PhotoSoptterHolder");}; return _PhotoSoptterHolderJ;}} private jQueryObject _PhotoSoptterHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement PhotoSpotterLink {get {if (_PhotoSpotterLink == null) {_PhotoSpotterLink = (AnchorElement)Document.GetElementById(clientId + "_PhotoSpotterLink");}; return _PhotoSpotterLink;}} private AnchorElement _PhotoSpotterLink;
		public jQueryObject PhotoSpotterLinkJ {get {if (_PhotoSpotterLinkJ == null) {_PhotoSpotterLinkJ = jQuery.Select("#" + clientId + "_PhotoSpotterLink");}; return _PhotoSpotterLinkJ;}} private jQueryObject _PhotoSpotterLinkJ;
		public Element TopPhotoArchiveHolder {get {if (_TopPhotoArchiveHolder == null) {_TopPhotoArchiveHolder = (Element)Document.GetElementById(clientId + "_TopPhotoArchiveHolder");}; return _TopPhotoArchiveHolder;}} private Element _TopPhotoArchiveHolder;
		public jQueryObject TopPhotoArchiveHolderJ {get {if (_TopPhotoArchiveHolderJ == null) {_TopPhotoArchiveHolderJ = jQuery.Select("#" + clientId + "_TopPhotoArchiveHolder");}; return _TopPhotoArchiveHolderJ;}} private jQueryObject _TopPhotoArchiveHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement PhotoAnchor {get {if (_PhotoAnchor == null) {_PhotoAnchor = (AnchorElement)Document.GetElementById(clientId + "_PhotoAnchor");}; return _PhotoAnchor;}} private AnchorElement _PhotoAnchor;
		public jQueryObject PhotoAnchorJ {get {if (_PhotoAnchorJ == null) {_PhotoAnchorJ = jQuery.Select("#" + clientId + "_PhotoAnchor");}; return _PhotoAnchorJ;}} private jQueryObject _PhotoAnchorJ;
		public ImageElement PhotoImg {get {if (_PhotoImg == null) {_PhotoImg = (ImageElement)Document.GetElementById(clientId + "_PhotoImg");}; return _PhotoImg;}} private ImageElement _PhotoImg;
		public jQueryObject PhotoImgJ {get {if (_PhotoImgJ == null) {_PhotoImgJ = jQuery.Select("#" + clientId + "_PhotoImg");}; return _PhotoImgJ;}} private jQueryObject _PhotoImgJ;
		public Element PhotoCaption {get {if (_PhotoCaption == null) {_PhotoCaption = (Element)Document.GetElementById(clientId + "_PhotoCaption");}; return _PhotoCaption;}} private Element _PhotoCaption;
		public jQueryObject PhotoCaptionJ {get {if (_PhotoCaptionJ == null) {_PhotoCaptionJ = jQuery.Select("#" + clientId + "_PhotoCaption");}; return _PhotoCaptionJ;}} private jQueryObject _PhotoCaptionJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement NewArticlesPanel {get {if (_NewArticlesPanel == null) {_NewArticlesPanel = (DivElement)Document.GetElementById(clientId + "_NewArticlesPanel");}; return _NewArticlesPanel;}} private DivElement _NewArticlesPanel;
		public jQueryObject NewArticlesPanelJ {get {if (_NewArticlesPanelJ == null) {_NewArticlesPanelJ = jQuery.Select("#" + clientId + "_NewArticlesPanel");}; return _NewArticlesPanelJ;}} private jQueryObject _NewArticlesPanelJ;
		public Element NewArticlesDataList {get {if (_NewArticlesDataList == null) {_NewArticlesDataList = (Element)Document.GetElementById(clientId + "_NewArticlesDataList");}; return _NewArticlesDataList;}} private Element _NewArticlesDataList;
		public jQueryObject NewArticlesDataListJ {get {if (_NewArticlesDataListJ == null) {_NewArticlesDataListJ = jQuery.Select("#" + clientId + "_NewArticlesDataList");}; return _NewArticlesDataListJ;}} private jQueryObject _NewArticlesDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element OutBox {get {if (_OutBox == null) {_OutBox = (Element)Document.GetElementById(clientId + "_OutBox");}; return _OutBox;}} private Element _OutBox;
		public jQueryObject OutBoxJ {get {if (_OutBoxJ == null) {_OutBoxJ = jQuery.Select("#" + clientId + "_OutBox");}; return _OutBoxJ;}} private jQueryObject _OutBoxJ;//mappings.Add("Spotted.Controls.OutBox", ElementGetter("Element"));
		public Element ExploreBox {get {if (_ExploreBox == null) {_ExploreBox = (Element)Document.GetElementById(clientId + "_ExploreBox");}; return _ExploreBox;}} private Element _ExploreBox;
		public jQueryObject ExploreBoxJ {get {if (_ExploreBoxJ == null) {_ExploreBoxJ = jQuery.Select("#" + clientId + "_ExploreBox");}; return _ExploreBoxJ;}} private jQueryObject _ExploreBoxJ;//mappings.Add("Spotted.Controls.ExploreBox", ElementGetter("Element"));
		public InputElement SpotterCode {get {if (_SpotterCode == null) {_SpotterCode = (InputElement)Document.GetElementById(clientId + "_SpotterCode");}; return _SpotterCode;}} private InputElement _SpotterCode;
		public jQueryObject SpotterCodeJ {get {if (_SpotterCodeJ == null) {_SpotterCodeJ = jQuery.Select("#" + clientId + "_SpotterCode");}; return _SpotterCodeJ;}} private jQueryObject _SpotterCodeJ;
		public InputElement SpotterCodeButton {get {if (_SpotterCodeButton == null) {_SpotterCodeButton = (InputElement)Document.GetElementById(clientId + "_SpotterCodeButton");}; return _SpotterCodeButton;}} private InputElement _SpotterCodeButton;
		public jQueryObject SpotterCodeButtonJ {get {if (_SpotterCodeButtonJ == null) {_SpotterCodeButtonJ = jQuery.Select("#" + clientId + "_SpotterCodeButton");}; return _SpotterCodeButtonJ;}} private jQueryObject _SpotterCodeButtonJ;
		public Element SpotterCodeError {get {if (_SpotterCodeError == null) {_SpotterCodeError = (Element)Document.GetElementById(clientId + "_SpotterCodeError");}; return _SpotterCodeError;}} private Element _SpotterCodeError;
		public jQueryObject SpotterCodeErrorJ {get {if (_SpotterCodeErrorJ == null) {_SpotterCodeErrorJ = jQuery.Select("#" + clientId + "_SpotterCodeError");}; return _SpotterCodeErrorJ;}} private jQueryObject _SpotterCodeErrorJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element HomeContent {get {if (_HomeContent == null) {_HomeContent = (Element)Document.GetElementById(clientId + "_HomeContent");}; return _HomeContent;}} private Element _HomeContent;
		public jQueryObject HomeContentJ {get {if (_HomeContentJ == null) {_HomeContentJ = jQuery.Select("#" + clientId + "_HomeContent");}; return _HomeContentJ;}} private jQueryObject _HomeContentJ;//mappings.Add("Spotted.Pages.HomeContent", ElementGetter("Element"));
		public Element Latest {get {if (_Latest == null) {_Latest = (Element)Document.GetElementById(clientId + "_Latest");}; return _Latest;}} private Element _Latest;
		public jQueryObject LatestJ {get {if (_LatestJ == null) {_LatestJ = jQuery.Select("#" + clientId + "_Latest");}; return _LatestJ;}} private jQueryObject _LatestJ;//mappings.Add("Spotted.Controls.Latest", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
