//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.CaptionCompetition
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
		public Element uiPhotoDataList {get {if (_uiPhotoDataList == null) {_uiPhotoDataList = (Element)Document.GetElementById(clientId + "_uiPhotoDataList");}; return _uiPhotoDataList;}} private Element _uiPhotoDataList;
		public jQueryObject uiPhotoDataListJ {get {if (_uiPhotoDataListJ == null) {_uiPhotoDataListJ = jQuery.Select("#" + clientId + "_uiPhotoDataList");}; return _uiPhotoDataListJ;}} private jQueryObject _uiPhotoDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public AnchorElement uiPhotoUrl {get {if (_uiPhotoUrl == null) {_uiPhotoUrl = (AnchorElement)Document.GetElementById(clientId + "_uiPhotoUrl");}; return _uiPhotoUrl;}} private AnchorElement _uiPhotoUrl;
		public jQueryObject uiPhotoUrlJ {get {if (_uiPhotoUrlJ == null) {_uiPhotoUrlJ = jQuery.Select("#" + clientId + "_uiPhotoUrl");}; return _uiPhotoUrlJ;}} private jQueryObject _uiPhotoUrlJ;
		public ImageElement uiPhoto {get {if (_uiPhoto == null) {_uiPhoto = (ImageElement)Document.GetElementById(clientId + "_uiPhoto");}; return _uiPhoto;}} private ImageElement _uiPhoto;
		public jQueryObject uiPhotoJ {get {if (_uiPhotoJ == null) {_uiPhotoJ = jQuery.Select("#" + clientId + "_uiPhoto");}; return _uiPhotoJ;}} private jQueryObject _uiPhotoJ;
		public InputElement uiCaptionText {get {if (_uiCaptionText == null) {_uiCaptionText = (InputElement)Document.GetElementById(clientId + "_uiCaptionText");}; return _uiCaptionText;}} private InputElement _uiCaptionText;
		public jQueryObject uiCaptionTextJ {get {if (_uiCaptionTextJ == null) {_uiCaptionTextJ = jQuery.Select("#" + clientId + "_uiCaptionText");}; return _uiCaptionTextJ;}} private jQueryObject _uiCaptionTextJ;
		public InputElement uiPost {get {if (_uiPost == null) {_uiPost = (InputElement)Document.GetElementById(clientId + "_uiPost");}; return _uiPost;}} private InputElement _uiPost;
		public jQueryObject uiPostJ {get {if (_uiPostJ == null) {_uiPostJ = jQuery.Select("#" + clientId + "_uiPost");}; return _uiPostJ;}} private jQueryObject _uiPostJ;
		public Js.Controls.CommentsDisplay.Controller uiCommentsDisplay {get {return (Js.Controls.CommentsDisplay.Controller) Script.Eval(clientId + "_uiCommentsDisplayController");}}
		public InputElement ThreadK {get {if (_ThreadK == null) {_ThreadK = (InputElement)Document.GetElementById(clientId + "_ThreadK");}; return _ThreadK;}} private InputElement _ThreadK;
		public jQueryObject ThreadKJ {get {if (_ThreadKJ == null) {_ThreadKJ = jQuery.Select("#" + clientId + "_ThreadK");}; return _ThreadKJ;}} private jQueryObject _ThreadKJ;
		public InputElement PhotoK {get {if (_PhotoK == null) {_PhotoK = (InputElement)Document.GetElementById(clientId + "_PhotoK");}; return _PhotoK;}} private InputElement _PhotoK;
		public jQueryObject PhotoKJ {get {if (_PhotoKJ == null) {_PhotoKJ = jQuery.Select("#" + clientId + "_PhotoK");}; return _PhotoKJ;}} private jQueryObject _PhotoKJ;
		public InputElement PageNumber {get {if (_PageNumber == null) {_PageNumber = (InputElement)Document.GetElementById(clientId + "_PageNumber");}; return _PageNumber;}} private InputElement _PageNumber;
		public jQueryObject PageNumberJ {get {if (_PageNumberJ == null) {_PageNumberJ = jQuery.Select("#" + clientId + "_PageNumber");}; return _PageNumberJ;}} private jQueryObject _PageNumberJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
