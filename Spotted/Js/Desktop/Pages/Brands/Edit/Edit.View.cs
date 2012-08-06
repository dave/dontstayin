//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Pic", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Brands.Edit
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
		public DivElement PanelManage {get {if (_PanelManage == null) {_PanelManage = (DivElement)Document.GetElementById(clientId + "_PanelManage");}; return _PanelManage;}} private DivElement _PanelManage;
		public jQueryObject PanelManageJ {get {if (_PanelManageJ == null) {_PanelManageJ = jQuery.Select("#" + clientId + "_PanelManage");}; return _PanelManageJ;}} private jQueryObject _PanelManageJ;
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement ManageBrandAnchor {get {if (_ManageBrandAnchor == null) {_ManageBrandAnchor = (AnchorElement)Document.GetElementById(clientId + "_ManageBrandAnchor");}; return _ManageBrandAnchor;}} private AnchorElement _ManageBrandAnchor;
		public jQueryObject ManageBrandAnchorJ {get {if (_ManageBrandAnchorJ == null) {_ManageBrandAnchorJ = jQuery.Select("#" + clientId + "_ManageBrandAnchor");}; return _ManageBrandAnchorJ;}} private jQueryObject _ManageBrandAnchorJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement ManageNameTextBox {get {if (_ManageNameTextBox == null) {_ManageNameTextBox = (InputElement)Document.GetElementById(clientId + "_ManageNameTextBox");}; return _ManageNameTextBox;}} private InputElement _ManageNameTextBox;
		public jQueryObject ManageNameTextBoxJ {get {if (_ManageNameTextBoxJ == null) {_ManageNameTextBoxJ = jQuery.Select("#" + clientId + "_ManageNameTextBox");}; return _ManageNameTextBoxJ;}} private jQueryObject _ManageNameTextBoxJ;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H15 {get {if (_H15 == null) {_H15 = (Element)Document.GetElementById(clientId + "_H15");}; return _H15;}} private Element _H15;
		public jQueryObject H15J {get {if (_H15J == null) {_H15J = jQuery.Select("#" + clientId + "_H15");}; return _H15J;}} private jQueryObject _H15J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement ManagePicUploadPanel {get {if (_ManagePicUploadPanel == null) {_ManagePicUploadPanel = (DivElement)Document.GetElementById(clientId + "_ManagePicUploadPanel");}; return _ManagePicUploadPanel;}} private DivElement _ManagePicUploadPanel;
		public jQueryObject ManagePicUploadPanelJ {get {if (_ManagePicUploadPanelJ == null) {_ManagePicUploadPanelJ = jQuery.Select("#" + clientId + "_ManagePicUploadPanel");}; return _ManagePicUploadPanelJ;}} private jQueryObject _ManagePicUploadPanelJ;
		public Element ManagePic {get {if (_ManagePic == null) {_ManagePic = (Element)Document.GetElementById(clientId + "_ManagePic");}; return _ManagePic;}} private Element _ManagePic;
		public jQueryObject ManagePicJ {get {if (_ManagePicJ == null) {_ManagePicJ = jQuery.Select("#" + clientId + "_ManagePic");}; return _ManagePicJ;}} private jQueryObject _ManagePicJ;//mappings.Add("Spotted.Controls.Pic", ElementGetter("Element"));
		public DivElement SuperAdminOptions {get {if (_SuperAdminOptions == null) {_SuperAdminOptions = (DivElement)Document.GetElementById(clientId + "_SuperAdminOptions");}; return _SuperAdminOptions;}} private DivElement _SuperAdminOptions;
		public jQueryObject SuperAdminOptionsJ {get {if (_SuperAdminOptionsJ == null) {_SuperAdminOptionsJ = jQuery.Select("#" + clientId + "_SuperAdminOptions");}; return _SuperAdminOptionsJ;}} private jQueryObject _SuperAdminOptionsJ;
		public Element H16 {get {if (_H16 == null) {_H16 = (Element)Document.GetElementById(clientId + "_H16");}; return _H16;}} private Element _H16;
		public jQueryObject H16J {get {if (_H16J == null) {_H16J = jQuery.Select("#" + clientId + "_H16");}; return _H16J;}} private jQueryObject _H16J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element ManageDeleteButton {get {if (_ManageDeleteButton == null) {_ManageDeleteButton = (Element)Document.GetElementById(clientId + "_ManageDeleteButton");}; return _ManageDeleteButton;}} private Element _ManageDeleteButton;
		public jQueryObject ManageDeleteButtonJ {get {if (_ManageDeleteButtonJ == null) {_ManageDeleteButtonJ = jQuery.Select("#" + clientId + "_ManageDeleteButton");}; return _ManageDeleteButtonJ;}} private jQueryObject _ManageDeleteButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element ManageDeleteDone {get {if (_ManageDeleteDone == null) {_ManageDeleteDone = (Element)Document.GetElementById(clientId + "_ManageDeleteDone");}; return _ManageDeleteDone;}} private Element _ManageDeleteDone;
		public jQueryObject ManageDeleteDoneJ {get {if (_ManageDeleteDoneJ == null) {_ManageDeleteDoneJ = jQuery.Select("#" + clientId + "_ManageDeleteDone");}; return _ManageDeleteDoneJ;}} private jQueryObject _ManageDeleteDoneJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiManageGotoAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiManageGotoAutoCompleteBehaviour");}}
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element RenameError {get {if (_RenameError == null) {_RenameError = (Element)Document.GetElementById(clientId + "_RenameError");}; return _RenameError;}} private Element _RenameError;
		public jQueryObject RenameErrorJ {get {if (_RenameErrorJ == null) {_RenameErrorJ = jQuery.Select("#" + clientId + "_RenameError");}; return _RenameErrorJ;}} private jQueryObject _RenameErrorJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
