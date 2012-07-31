//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.PicCropper", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.AddPic
{
	public partial class View
		 : Js.AdminUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement ObjectPanel {get {if (_ObjectPanel == null) {_ObjectPanel = (DivElement)Document.GetElementById(clientId + "_ObjectPanel");}; return _ObjectPanel;}} private DivElement _ObjectPanel;
		public jQueryObject ObjectPanelJ {get {if (_ObjectPanelJ == null) {_ObjectPanelJ = jQuery.Select("#" + clientId + "_ObjectPanel");}; return _ObjectPanelJ;}} private jQueryObject _ObjectPanelJ;
		public SelectElement ObjectTypeList {get {if (_ObjectTypeList == null) {_ObjectTypeList = (SelectElement)Document.GetElementById(clientId + "_ObjectTypeList");}; return _ObjectTypeList;}} private SelectElement _ObjectTypeList;
		public jQueryObject ObjectTypeListJ {get {if (_ObjectTypeListJ == null) {_ObjectTypeListJ = jQuery.Select("#" + clientId + "_ObjectTypeList");}; return _ObjectTypeListJ;}} private jQueryObject _ObjectTypeListJ;
		public InputElement ObjectKTextBox {get {if (_ObjectKTextBox == null) {_ObjectKTextBox = (InputElement)Document.GetElementById(clientId + "_ObjectKTextBox");}; return _ObjectKTextBox;}} private InputElement _ObjectKTextBox;
		public jQueryObject ObjectKTextBoxJ {get {if (_ObjectKTextBoxJ == null) {_ObjectKTextBoxJ = jQuery.Select("#" + clientId + "_ObjectKTextBox");}; return _ObjectKTextBoxJ;}} private jQueryObject _ObjectKTextBoxJ;
		public DivElement ViewPicPanel {get {if (_ViewPicPanel == null) {_ViewPicPanel = (DivElement)Document.GetElementById(clientId + "_ViewPicPanel");}; return _ViewPicPanel;}} private DivElement _ViewPicPanel;
		public jQueryObject ViewPicPanelJ {get {if (_ViewPicPanelJ == null) {_ViewPicPanelJ = jQuery.Select("#" + clientId + "_ViewPicPanel");}; return _ViewPicPanelJ;}} private jQueryObject _ViewPicPanelJ;
		public ImageElement ViewPicImg {get {if (_ViewPicImg == null) {_ViewPicImg = (ImageElement)Document.GetElementById(clientId + "_ViewPicImg");}; return _ViewPicImg;}} private ImageElement _ViewPicImg;
		public jQueryObject ViewPicImgJ {get {if (_ViewPicImgJ == null) {_ViewPicImgJ = jQuery.Select("#" + clientId + "_ViewPicImg");}; return _ViewPicImgJ;}} private jQueryObject _ViewPicImgJ;
		public DivElement CropperPanel {get {if (_CropperPanel == null) {_CropperPanel = (DivElement)Document.GetElementById(clientId + "_CropperPanel");}; return _CropperPanel;}} private DivElement _CropperPanel;
		public jQueryObject CropperPanelJ {get {if (_CropperPanelJ == null) {_CropperPanelJ = jQuery.Select("#" + clientId + "_CropperPanel");}; return _CropperPanelJ;}} private jQueryObject _CropperPanelJ;
		public Element PicCropper {get {if (_PicCropper == null) {_PicCropper = (Element)Document.GetElementById(clientId + "_PicCropper");}; return _PicCropper;}} private Element _PicCropper;
		public jQueryObject PicCropperJ {get {if (_PicCropperJ == null) {_PicCropperJ = jQuery.Select("#" + clientId + "_PicCropper");}; return _PicCropperJ;}} private jQueryObject _PicCropperJ;//mappings.Add("Spotted.Controls.PicCropper", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
