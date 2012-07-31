//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Cropper", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Photos.FrontPageCrop
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
		public Element ParentName {get {if (_ParentName == null) {_ParentName = (Element)Document.GetElementById(clientId + "_ParentName");}; return _ParentName;}} private Element _ParentName;
		public jQueryObject ParentNameJ {get {if (_ParentNameJ == null) {_ParentNameJ = jQuery.Select("#" + clientId + "_ParentName");}; return _ParentNameJ;}} private jQueryObject _ParentNameJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public ImageElement CurrentImage {get {if (_CurrentImage == null) {_CurrentImage = (ImageElement)Document.GetElementById(clientId + "_CurrentImage");}; return _CurrentImage;}} private ImageElement _CurrentImage;
		public jQueryObject CurrentImageJ {get {if (_CurrentImageJ == null) {_CurrentImageJ = jQuery.Select("#" + clientId + "_CurrentImage");}; return _CurrentImageJ;}} private jQueryObject _CurrentImageJ;
		public Element Cropper {get {if (_Cropper == null) {_Cropper = (Element)Document.GetElementById(clientId + "_Cropper");}; return _Cropper;}} private Element _Cropper;
		public jQueryObject CropperJ {get {if (_CropperJ == null) {_CropperJ = jQuery.Select("#" + clientId + "_Cropper");}; return _CropperJ;}} private jQueryObject _CropperJ;//mappings.Add("Spotted.Controls.Cropper", ElementGetter("Element"));
		public CheckBoxElement CheckBox {get {if (_CheckBox == null) {_CheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_CheckBox");}; return _CheckBox;}} private CheckBoxElement _CheckBox;
		public jQueryObject CheckBoxJ {get {if (_CheckBoxJ == null) {_CheckBoxJ = jQuery.Select("#" + clientId + "_CheckBox");}; return _CheckBoxJ;}} private jQueryObject _CheckBoxJ;
		public Js.CustomControls.Cal.Controller Date {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_DateController");}}
		public InputElement Caption {get {if (_Caption == null) {_Caption = (InputElement)Document.GetElementById(clientId + "_Caption");}; return _Caption;}} private InputElement _Caption;
		public jQueryObject CaptionJ {get {if (_CaptionJ == null) {_CaptionJ = jQuery.Select("#" + clientId + "_Caption");}; return _CaptionJ;}} private jQueryObject _CaptionJ;
		public CheckBoxElement ColourBlackOnWhite {get {if (_ColourBlackOnWhite == null) {_ColourBlackOnWhite = (CheckBoxElement)Document.GetElementById(clientId + "_ColourBlackOnWhite");}; return _ColourBlackOnWhite;}} private CheckBoxElement _ColourBlackOnWhite;
		public jQueryObject ColourBlackOnWhiteJ {get {if (_ColourBlackOnWhiteJ == null) {_ColourBlackOnWhiteJ = jQuery.Select("#" + clientId + "_ColourBlackOnWhite");}; return _ColourBlackOnWhiteJ;}} private jQueryObject _ColourBlackOnWhiteJ;
		public CheckBoxElement ColourWhiteOnBlack {get {if (_ColourWhiteOnBlack == null) {_ColourWhiteOnBlack = (CheckBoxElement)Document.GetElementById(clientId + "_ColourWhiteOnBlack");}; return _ColourWhiteOnBlack;}} private CheckBoxElement _ColourWhiteOnBlack;
		public jQueryObject ColourWhiteOnBlackJ {get {if (_ColourWhiteOnBlackJ == null) {_ColourWhiteOnBlackJ = jQuery.Select("#" + clientId + "_ColourWhiteOnBlack");}; return _ColourWhiteOnBlackJ;}} private jQueryObject _ColourWhiteOnBlackJ;
		public CheckBoxElement CornerTopLeft {get {if (_CornerTopLeft == null) {_CornerTopLeft = (CheckBoxElement)Document.GetElementById(clientId + "_CornerTopLeft");}; return _CornerTopLeft;}} private CheckBoxElement _CornerTopLeft;
		public jQueryObject CornerTopLeftJ {get {if (_CornerTopLeftJ == null) {_CornerTopLeftJ = jQuery.Select("#" + clientId + "_CornerTopLeft");}; return _CornerTopLeftJ;}} private jQueryObject _CornerTopLeftJ;
		public CheckBoxElement CornerTopRight {get {if (_CornerTopRight == null) {_CornerTopRight = (CheckBoxElement)Document.GetElementById(clientId + "_CornerTopRight");}; return _CornerTopRight;}} private CheckBoxElement _CornerTopRight;
		public jQueryObject CornerTopRightJ {get {if (_CornerTopRightJ == null) {_CornerTopRightJ = jQuery.Select("#" + clientId + "_CornerTopRight");}; return _CornerTopRightJ;}} private jQueryObject _CornerTopRightJ;
		public CheckBoxElement CornerBottomLeft {get {if (_CornerBottomLeft == null) {_CornerBottomLeft = (CheckBoxElement)Document.GetElementById(clientId + "_CornerBottomLeft");}; return _CornerBottomLeft;}} private CheckBoxElement _CornerBottomLeft;
		public jQueryObject CornerBottomLeftJ {get {if (_CornerBottomLeftJ == null) {_CornerBottomLeftJ = jQuery.Select("#" + clientId + "_CornerBottomLeft");}; return _CornerBottomLeftJ;}} private jQueryObject _CornerBottomLeftJ;
		public CheckBoxElement CornerBottomRight {get {if (_CornerBottomRight == null) {_CornerBottomRight = (CheckBoxElement)Document.GetElementById(clientId + "_CornerBottomRight");}; return _CornerBottomRight;}} private CheckBoxElement _CornerBottomRight;
		public jQueryObject CornerBottomRightJ {get {if (_CornerBottomRightJ == null) {_CornerBottomRightJ = jQuery.Select("#" + clientId + "_CornerBottomRight");}; return _CornerBottomRightJ;}} private jQueryObject _CornerBottomRightJ;
		public Element Button3 {get {if (_Button3 == null) {_Button3 = (Element)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private Element _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
