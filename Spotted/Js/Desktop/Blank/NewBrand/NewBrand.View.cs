//mappings.Add("Spotted.Controls.Pic", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RegularExpressionValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.NewBrand
{
	public partial class View
		 : Js.BlankUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public InputElement NameTextBox {get {if (_NameTextBox == null) {_NameTextBox = (InputElement)Document.GetElementById(clientId + "_NameTextBox");}; return _NameTextBox;}} private InputElement _NameTextBox;
		public jQueryObject NameTextBoxJ {get {if (_NameTextBoxJ == null) {_NameTextBoxJ = jQuery.Select("#" + clientId + "_NameTextBox");}; return _NameTextBoxJ;}} private jQueryObject _NameTextBoxJ;
		public DivElement PanelName {get {if (_PanelName == null) {_PanelName = (DivElement)Document.GetElementById(clientId + "_PanelName");}; return _PanelName;}} private DivElement _PanelName;
		public jQueryObject PanelNameJ {get {if (_PanelNameJ == null) {_PanelNameJ = jQuery.Select("#" + clientId + "_PanelName");}; return _PanelNameJ;}} private jQueryObject _PanelNameJ;
		public DivElement PanelPic {get {if (_PanelPic == null) {_PanelPic = (DivElement)Document.GetElementById(clientId + "_PanelPic");}; return _PanelPic;}} private DivElement _PanelPic;
		public jQueryObject PanelPicJ {get {if (_PanelPicJ == null) {_PanelPicJ = jQuery.Select("#" + clientId + "_PanelPic");}; return _PanelPicJ;}} private jQueryObject _PanelPicJ;
		public DivElement PicUploadPanel {get {if (_PicUploadPanel == null) {_PicUploadPanel = (DivElement)Document.GetElementById(clientId + "_PicUploadPanel");}; return _PicUploadPanel;}} private DivElement _PicUploadPanel;
		public jQueryObject PicUploadPanelJ {get {if (_PicUploadPanelJ == null) {_PicUploadPanelJ = jQuery.Select("#" + clientId + "_PicUploadPanel");}; return _PicUploadPanelJ;}} private jQueryObject _PicUploadPanelJ;
		public Element Pic {get {if (_Pic == null) {_Pic = (Element)Document.GetElementById(clientId + "_Pic");}; return _Pic;}} private Element _Pic;
		public jQueryObject PicJ {get {if (_PicJ == null) {_PicJ = jQuery.Select("#" + clientId + "_Pic");}; return _PicJ;}} private jQueryObject _PicJ;//mappings.Add("Spotted.Controls.Pic", ElementGetter("Element"));
		public DivElement PanelDone {get {if (_PanelDone == null) {_PanelDone = (DivElement)Document.GetElementById(clientId + "_PanelDone");}; return _PanelDone;}} private DivElement _PanelDone;
		public jQueryObject PanelDoneJ {get {if (_PanelDoneJ == null) {_PanelDoneJ = jQuery.Select("#" + clientId + "_PanelDone");}; return _PanelDoneJ;}} private jQueryObject _PanelDoneJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button3 {get {if (_Button3 == null) {_Button3 = (Element)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private Element _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element RegularExpressionValidator1 {get {if (_RegularExpressionValidator1 == null) {_RegularExpressionValidator1 = (Element)Document.GetElementById(clientId + "_RegularExpressionValidator1");}; return _RegularExpressionValidator1;}} private Element _RegularExpressionValidator1;
		public jQueryObject RegularExpressionValidator1J {get {if (_RegularExpressionValidator1J == null) {_RegularExpressionValidator1J = jQuery.Select("#" + clientId + "_RegularExpressionValidator1");}; return _RegularExpressionValidator1J;}} private jQueryObject _RegularExpressionValidator1J;//mappings.Add("System.Web.UI.WebControls.RegularExpressionValidator", ElementGetter("Element"));
		public Element CustomValidator1 {get {if (_CustomValidator1 == null) {_CustomValidator1 = (Element)Document.GetElementById(clientId + "_CustomValidator1");}; return _CustomValidator1;}} private Element _CustomValidator1;
		public jQueryObject CustomValidator1J {get {if (_CustomValidator1J == null) {_CustomValidator1J = jQuery.Select("#" + clientId + "_CustomValidator1");}; return _CustomValidator1J;}} private jQueryObject _CustomValidator1J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element H15 {get {if (_H15 == null) {_H15 = (Element)Document.GetElementById(clientId + "_H15");}; return _H15;}} private Element _H15;
		public jQueryObject H15J {get {if (_H15J == null) {_H15J = jQuery.Select("#" + clientId + "_H15");}; return _H15J;}} private jQueryObject _H15J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
