//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.BannerCheck
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
		public DivElement PanelList {get {if (_PanelList == null) {_PanelList = (DivElement)Document.GetElementById(clientId + "_PanelList");}; return _PanelList;}} private DivElement _PanelList;
		public jQueryObject PanelListJ {get {if (_PanelListJ == null) {_PanelListJ = jQuery.Select("#" + clientId + "_PanelList");}; return _PanelListJ;}} private jQueryObject _PanelListJ;
		public Element MiscDataGrid {get {if (_MiscDataGrid == null) {_MiscDataGrid = (Element)Document.GetElementById(clientId + "_MiscDataGrid");}; return _MiscDataGrid;}} private Element _MiscDataGrid;
		public jQueryObject MiscDataGridJ {get {if (_MiscDataGridJ == null) {_MiscDataGridJ = jQuery.Select("#" + clientId + "_MiscDataGrid");}; return _MiscDataGridJ;}} private jQueryObject _MiscDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement PanelView {get {if (_PanelView == null) {_PanelView = (DivElement)Document.GetElementById(clientId + "_PanelView");}; return _PanelView;}} private DivElement _PanelView;
		public jQueryObject PanelViewJ {get {if (_PanelViewJ == null) {_PanelViewJ = jQuery.Select("#" + clientId + "_PanelView");}; return _PanelViewJ;}} private jQueryObject _PanelViewJ;
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element ViewPopupP {get {if (_ViewPopupP == null) {_ViewPopupP = (Element)Document.GetElementById(clientId + "_ViewPopupP");}; return _ViewPopupP;}} private Element _ViewPopupP;
		public jQueryObject ViewPopupPJ {get {if (_ViewPopupPJ == null) {_ViewPopupPJ = jQuery.Select("#" + clientId + "_ViewPopupP");}; return _ViewPopupPJ;}} private jQueryObject _ViewPopupPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement ViewFlashPanel {get {if (_ViewFlashPanel == null) {_ViewFlashPanel = (DivElement)Document.GetElementById(clientId + "_ViewFlashPanel");}; return _ViewFlashPanel;}} private DivElement _ViewFlashPanel;
		public jQueryObject ViewFlashPanelJ {get {if (_ViewFlashPanelJ == null) {_ViewFlashPanelJ = jQuery.Select("#" + clientId + "_ViewFlashPanel");}; return _ViewFlashPanelJ;}} private jQueryObject _ViewFlashPanelJ;
		public CheckBoxElement ViewFlashLinkTagYes {get {if (_ViewFlashLinkTagYes == null) {_ViewFlashLinkTagYes = (CheckBoxElement)Document.GetElementById(clientId + "_ViewFlashLinkTagYes");}; return _ViewFlashLinkTagYes;}} private CheckBoxElement _ViewFlashLinkTagYes;
		public jQueryObject ViewFlashLinkTagYesJ {get {if (_ViewFlashLinkTagYesJ == null) {_ViewFlashLinkTagYesJ = jQuery.Select("#" + clientId + "_ViewFlashLinkTagYes");}; return _ViewFlashLinkTagYesJ;}} private jQueryObject _ViewFlashLinkTagYesJ;
		public CheckBoxElement ViewFlashLinkTagNo {get {if (_ViewFlashLinkTagNo == null) {_ViewFlashLinkTagNo = (CheckBoxElement)Document.GetElementById(clientId + "_ViewFlashLinkTagNo");}; return _ViewFlashLinkTagNo;}} private CheckBoxElement _ViewFlashLinkTagNo;
		public jQueryObject ViewFlashLinkTagNoJ {get {if (_ViewFlashLinkTagNoJ == null) {_ViewFlashLinkTagNoJ = jQuery.Select("#" + clientId + "_ViewFlashLinkTagNo");}; return _ViewFlashLinkTagNoJ;}} private jQueryObject _ViewFlashLinkTagNoJ;
		public Element PassButton {get {if (_PassButton == null) {_PassButton = (Element)Document.GetElementById(clientId + "_PassButton");}; return _PassButton;}} private Element _PassButton;
		public jQueryObject PassButtonJ {get {if (_PassButtonJ == null) {_PassButtonJ = jQuery.Select("#" + clientId + "_PassButton");}; return _PassButtonJ;}} private jQueryObject _PassButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element FailButton {get {if (_FailButton == null) {_FailButton = (Element)Document.GetElementById(clientId + "_FailButton");}; return _FailButton;}} private Element _FailButton;
		public jQueryObject FailButtonJ {get {if (_FailButtonJ == null) {_FailButtonJ = jQuery.Select("#" + clientId + "_FailButton");}; return _FailButtonJ;}} private jQueryObject _FailButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public InputElement FailTextBox {get {if (_FailTextBox == null) {_FailTextBox = (InputElement)Document.GetElementById(clientId + "_FailTextBox");}; return _FailTextBox;}} private InputElement _FailTextBox;
		public jQueryObject FailTextBoxJ {get {if (_FailTextBoxJ == null) {_FailTextBoxJ = jQuery.Select("#" + clientId + "_FailTextBox");}; return _FailTextBoxJ;}} private jQueryObject _FailTextBoxJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
