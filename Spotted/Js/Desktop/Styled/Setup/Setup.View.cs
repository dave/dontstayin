//mappings.Add("System.Web.UI.HtmlControls.HtmlInputFile", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Styled.Setup
{
	public partial class View
		 : Js.StyledUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element InputCssFile {get {if (_InputCssFile == null) {_InputCssFile = (Element)Document.GetElementById(clientId + "_InputCssFile");}; return _InputCssFile;}} private Element _InputCssFile;
		public jQueryObject InputCssFileJ {get {if (_InputCssFileJ == null) {_InputCssFileJ = jQuery.Select("#" + clientId + "_InputCssFile");}; return _InputCssFileJ;}} private jQueryObject _InputCssFileJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlInputFile", ElementGetter("Element"));
		public InputElement UploadCssButton {get {if (_UploadCssButton == null) {_UploadCssButton = (InputElement)Document.GetElementById(clientId + "_UploadCssButton");}; return _UploadCssButton;}} private InputElement _UploadCssButton;
		public jQueryObject UploadCssButtonJ {get {if (_UploadCssButtonJ == null) {_UploadCssButtonJ = jQuery.Select("#" + clientId + "_UploadCssButton");}; return _UploadCssButtonJ;}} private jQueryObject _UploadCssButtonJ;
		public InputElement CssUrlHiddenTextBox {get {if (_CssUrlHiddenTextBox == null) {_CssUrlHiddenTextBox = (InputElement)Document.GetElementById(clientId + "_CssUrlHiddenTextBox");}; return _CssUrlHiddenTextBox;}} private InputElement _CssUrlHiddenTextBox;
		public jQueryObject CssUrlHiddenTextBoxJ {get {if (_CssUrlHiddenTextBoxJ == null) {_CssUrlHiddenTextBoxJ = jQuery.Select("#" + clientId + "_CssUrlHiddenTextBox");}; return _CssUrlHiddenTextBoxJ;}} private jQueryObject _CssUrlHiddenTextBoxJ;
		public Element InputLogoFile {get {if (_InputLogoFile == null) {_InputLogoFile = (Element)Document.GetElementById(clientId + "_InputLogoFile");}; return _InputLogoFile;}} private Element _InputLogoFile;
		public jQueryObject InputLogoFileJ {get {if (_InputLogoFileJ == null) {_InputLogoFileJ = jQuery.Select("#" + clientId + "_InputLogoFile");}; return _InputLogoFileJ;}} private jQueryObject _InputLogoFileJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlInputFile", ElementGetter("Element"));
		public InputElement UploadLogoButton {get {if (_UploadLogoButton == null) {_UploadLogoButton = (InputElement)Document.GetElementById(clientId + "_UploadLogoButton");}; return _UploadLogoButton;}} private InputElement _UploadLogoButton;
		public jQueryObject UploadLogoButtonJ {get {if (_UploadLogoButtonJ == null) {_UploadLogoButtonJ = jQuery.Select("#" + clientId + "_UploadLogoButton");}; return _UploadLogoButtonJ;}} private jQueryObject _UploadLogoButtonJ;
		public InputElement LogoUrlHiddenTextBox {get {if (_LogoUrlHiddenTextBox == null) {_LogoUrlHiddenTextBox = (InputElement)Document.GetElementById(clientId + "_LogoUrlHiddenTextBox");}; return _LogoUrlHiddenTextBox;}} private InputElement _LogoUrlHiddenTextBox;
		public jQueryObject LogoUrlHiddenTextBoxJ {get {if (_LogoUrlHiddenTextBoxJ == null) {_LogoUrlHiddenTextBoxJ = jQuery.Select("#" + clientId + "_LogoUrlHiddenTextBox");}; return _LogoUrlHiddenTextBoxJ;}} private jQueryObject _LogoUrlHiddenTextBoxJ;
		public Element InputBackgroundFile {get {if (_InputBackgroundFile == null) {_InputBackgroundFile = (Element)Document.GetElementById(clientId + "_InputBackgroundFile");}; return _InputBackgroundFile;}} private Element _InputBackgroundFile;
		public jQueryObject InputBackgroundFileJ {get {if (_InputBackgroundFileJ == null) {_InputBackgroundFileJ = jQuery.Select("#" + clientId + "_InputBackgroundFile");}; return _InputBackgroundFileJ;}} private jQueryObject _InputBackgroundFileJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlInputFile", ElementGetter("Element"));
		public InputElement UploadBackgroundButton {get {if (_UploadBackgroundButton == null) {_UploadBackgroundButton = (InputElement)Document.GetElementById(clientId + "_UploadBackgroundButton");}; return _UploadBackgroundButton;}} private InputElement _UploadBackgroundButton;
		public jQueryObject UploadBackgroundButtonJ {get {if (_UploadBackgroundButtonJ == null) {_UploadBackgroundButtonJ = jQuery.Select("#" + clientId + "_UploadBackgroundButton");}; return _UploadBackgroundButtonJ;}} private jQueryObject _UploadBackgroundButtonJ;
		public InputElement BackgroundUrlHiddenTextBox {get {if (_BackgroundUrlHiddenTextBox == null) {_BackgroundUrlHiddenTextBox = (InputElement)Document.GetElementById(clientId + "_BackgroundUrlHiddenTextBox");}; return _BackgroundUrlHiddenTextBox;}} private InputElement _BackgroundUrlHiddenTextBox;
		public jQueryObject BackgroundUrlHiddenTextBoxJ {get {if (_BackgroundUrlHiddenTextBoxJ == null) {_BackgroundUrlHiddenTextBoxJ = jQuery.Select("#" + clientId + "_BackgroundUrlHiddenTextBox");}; return _BackgroundUrlHiddenTextBoxJ;}} private jQueryObject _BackgroundUrlHiddenTextBoxJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
