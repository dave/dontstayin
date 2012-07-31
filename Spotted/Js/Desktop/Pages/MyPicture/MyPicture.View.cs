//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Cropper", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.MyPicture
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
		public DivElement PanelNoPhotosMe {get {if (_PanelNoPhotosMe == null) {_PanelNoPhotosMe = (DivElement)Document.GetElementById(clientId + "_PanelNoPhotosMe");}; return _PanelNoPhotosMe;}} private DivElement _PanelNoPhotosMe;
		public jQueryObject PanelNoPhotosMeJ {get {if (_PanelNoPhotosMeJ == null) {_PanelNoPhotosMeJ = jQuery.Select("#" + clientId + "_PanelNoPhotosMe");}; return _PanelNoPhotosMeJ;}} private jQueryObject _PanelNoPhotosMeJ;
		public Element AllPhotosPageP1 {get {if (_AllPhotosPageP1 == null) {_AllPhotosPageP1 = (Element)Document.GetElementById(clientId + "_AllPhotosPageP1");}; return _AllPhotosPageP1;}} private Element _AllPhotosPageP1;
		public jQueryObject AllPhotosPageP1J {get {if (_AllPhotosPageP1J == null) {_AllPhotosPageP1J = jQuery.Select("#" + clientId + "_AllPhotosPageP1");}; return _AllPhotosPageP1J;}} private jQueryObject _AllPhotosPageP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element AllPhotosPageP2 {get {if (_AllPhotosPageP2 == null) {_AllPhotosPageP2 = (Element)Document.GetElementById(clientId + "_AllPhotosPageP2");}; return _AllPhotosPageP2;}} private Element _AllPhotosPageP2;
		public jQueryObject AllPhotosPageP2J {get {if (_AllPhotosPageP2J == null) {_AllPhotosPageP2J = jQuery.Select("#" + clientId + "_AllPhotosPageP2");}; return _AllPhotosPageP2J;}} private jQueryObject _AllPhotosPageP2J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement AllPhotosPrevPage1 {get {if (_AllPhotosPrevPage1 == null) {_AllPhotosPrevPage1 = (AnchorElement)Document.GetElementById(clientId + "_AllPhotosPrevPage1");}; return _AllPhotosPrevPage1;}} private AnchorElement _AllPhotosPrevPage1;
		public jQueryObject AllPhotosPrevPage1J {get {if (_AllPhotosPrevPage1J == null) {_AllPhotosPrevPage1J = jQuery.Select("#" + clientId + "_AllPhotosPrevPage1");}; return _AllPhotosPrevPage1J;}} private jQueryObject _AllPhotosPrevPage1J;
		public AnchorElement AllPhotosNextPage1 {get {if (_AllPhotosNextPage1 == null) {_AllPhotosNextPage1 = (AnchorElement)Document.GetElementById(clientId + "_AllPhotosNextPage1");}; return _AllPhotosNextPage1;}} private AnchorElement _AllPhotosNextPage1;
		public jQueryObject AllPhotosNextPage1J {get {if (_AllPhotosNextPage1J == null) {_AllPhotosNextPage1J = jQuery.Select("#" + clientId + "_AllPhotosNextPage1");}; return _AllPhotosNextPage1J;}} private jQueryObject _AllPhotosNextPage1J;
		public AnchorElement AllPhotosPrevPage2 {get {if (_AllPhotosPrevPage2 == null) {_AllPhotosPrevPage2 = (AnchorElement)Document.GetElementById(clientId + "_AllPhotosPrevPage2");}; return _AllPhotosPrevPage2;}} private AnchorElement _AllPhotosPrevPage2;
		public jQueryObject AllPhotosPrevPage2J {get {if (_AllPhotosPrevPage2J == null) {_AllPhotosPrevPage2J = jQuery.Select("#" + clientId + "_AllPhotosPrevPage2");}; return _AllPhotosPrevPage2J;}} private jQueryObject _AllPhotosPrevPage2J;
		public AnchorElement AllPhotosNextPage2 {get {if (_AllPhotosNextPage2 == null) {_AllPhotosNextPage2 = (AnchorElement)Document.GetElementById(clientId + "_AllPhotosNextPage2");}; return _AllPhotosNextPage2;}} private AnchorElement _AllPhotosNextPage2;
		public jQueryObject AllPhotosNextPage2J {get {if (_AllPhotosNextPage2J == null) {_AllPhotosNextPage2J = jQuery.Select("#" + clientId + "_AllPhotosNextPage2");}; return _AllPhotosNextPage2J;}} private jQueryObject _AllPhotosNextPage2J;
		public Element Cropper {get {if (_Cropper == null) {_Cropper = (Element)Document.GetElementById(clientId + "_Cropper");}; return _Cropper;}} private Element _Cropper;
		public jQueryObject CropperJ {get {if (_CropperJ == null) {_CropperJ = jQuery.Select("#" + clientId + "_Cropper");}; return _CropperJ;}} private jQueryObject _CropperJ;//mappings.Add("Spotted.Controls.Cropper", ElementGetter("Element"));
		public DivElement PanelUsrPic {get {if (_PanelUsrPic == null) {_PanelUsrPic = (DivElement)Document.GetElementById(clientId + "_PanelUsrPic");}; return _PanelUsrPic;}} private DivElement _PanelUsrPic;
		public jQueryObject PanelUsrPicJ {get {if (_PanelUsrPicJ == null) {_PanelUsrPicJ = jQuery.Select("#" + clientId + "_PanelUsrPic");}; return _PanelUsrPicJ;}} private jQueryObject _PanelUsrPicJ;
		public ImageElement PicImg {get {if (_PicImg == null) {_PicImg = (ImageElement)Document.GetElementById(clientId + "_PicImg");}; return _PicImg;}} private ImageElement _PicImg;
		public jQueryObject PicImgJ {get {if (_PicImgJ == null) {_PicImgJ = jQuery.Select("#" + clientId + "_PicImg");}; return _PicImgJ;}} private jQueryObject _PicImgJ;
		public Element ChatReCropButton {get {if (_ChatReCropButton == null) {_ChatReCropButton = (Element)Document.GetElementById(clientId + "_ChatReCropButton");}; return _ChatReCropButton;}} private Element _ChatReCropButton;
		public jQueryObject ChatReCropButtonJ {get {if (_ChatReCropButtonJ == null) {_ChatReCropButtonJ = jQuery.Select("#" + clientId + "_ChatReCropButton");}; return _ChatReCropButtonJ;}} private jQueryObject _ChatReCropButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element ReCropButton {get {if (_ReCropButton == null) {_ReCropButton = (Element)Document.GetElementById(clientId + "_ReCropButton");}; return _ReCropButton;}} private Element _ReCropButton;
		public jQueryObject ReCropButtonJ {get {if (_ReCropButtonJ == null) {_ReCropButtonJ = jQuery.Select("#" + clientId + "_ReCropButton");}; return _ReCropButtonJ;}} private jQueryObject _ReCropButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element CancelButton {get {if (_CancelButton == null) {_CancelButton = (Element)Document.GetElementById(clientId + "_CancelButton");}; return _CancelButton;}} private Element _CancelButton;
		public jQueryObject CancelButtonJ {get {if (_CancelButtonJ == null) {_CancelButtonJ = jQuery.Select("#" + clientId + "_CancelButton");}; return _CancelButtonJ;}} private jQueryObject _CancelButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public AnchorElement PicAnchor {get {if (_PicAnchor == null) {_PicAnchor = (AnchorElement)Document.GetElementById(clientId + "_PicAnchor");}; return _PicAnchor;}} private AnchorElement _PicAnchor;
		public jQueryObject PicAnchorJ {get {if (_PicAnchorJ == null) {_PicAnchorJ = jQuery.Select("#" + clientId + "_PicAnchor");}; return _PicAnchorJ;}} private jQueryObject _PicAnchorJ;
		public DivElement PanelImages {get {if (_PanelImages == null) {_PanelImages = (DivElement)Document.GetElementById(clientId + "_PanelImages");}; return _PanelImages;}} private DivElement _PanelImages;
		public jQueryObject PanelImagesJ {get {if (_PanelImagesJ == null) {_PanelImagesJ = jQuery.Select("#" + clientId + "_PanelImages");}; return _PanelImagesJ;}} private jQueryObject _PanelImagesJ;
		public DivElement PanelCrop {get {if (_PanelCrop == null) {_PanelCrop = (DivElement)Document.GetElementById(clientId + "_PanelCrop");}; return _PanelCrop;}} private DivElement _PanelCrop;
		public jQueryObject PanelCropJ {get {if (_PanelCropJ == null) {_PanelCropJ = jQuery.Select("#" + clientId + "_PanelCrop");}; return _PanelCropJ;}} private jQueryObject _PanelCropJ;
		public Element ImagesDataList {get {if (_ImagesDataList == null) {_ImagesDataList = (Element)Document.GetElementById(clientId + "_ImagesDataList");}; return _ImagesDataList;}} private Element _ImagesDataList;
		public jQueryObject ImagesDataListJ {get {if (_ImagesDataListJ == null) {_ImagesDataListJ = jQuery.Select("#" + clientId + "_ImagesDataList");}; return _ImagesDataListJ;}} private jQueryObject _ImagesDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement PanelChatPic {get {if (_PanelChatPic == null) {_PanelChatPic = (DivElement)Document.GetElementById(clientId + "_PanelChatPic");}; return _PanelChatPic;}} private DivElement _PanelChatPic;
		public jQueryObject PanelChatPicJ {get {if (_PanelChatPicJ == null) {_PanelChatPicJ = jQuery.Select("#" + clientId + "_PanelChatPic");}; return _PanelChatPicJ;}} private jQueryObject _PanelChatPicJ;
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement ChatPicAnchor {get {if (_ChatPicAnchor == null) {_ChatPicAnchor = (AnchorElement)Document.GetElementById(clientId + "_ChatPicAnchor");}; return _ChatPicAnchor;}} private AnchorElement _ChatPicAnchor;
		public jQueryObject ChatPicAnchorJ {get {if (_ChatPicAnchorJ == null) {_ChatPicAnchorJ = jQuery.Select("#" + clientId + "_ChatPicAnchor");}; return _ChatPicAnchorJ;}} private jQueryObject _ChatPicAnchorJ;
		public ImageElement ChatPicImg {get {if (_ChatPicImg == null) {_ChatPicImg = (ImageElement)Document.GetElementById(clientId + "_ChatPicImg");}; return _ChatPicImg;}} private ImageElement _ChatPicImg;
		public jQueryObject ChatPicImgJ {get {if (_ChatPicImgJ == null) {_ChatPicImgJ = jQuery.Select("#" + clientId + "_ChatPicImg");}; return _ChatPicImgJ;}} private jQueryObject _ChatPicImgJ;
		public Element Button3 {get {if (_Button3 == null) {_Button3 = (Element)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private Element _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
