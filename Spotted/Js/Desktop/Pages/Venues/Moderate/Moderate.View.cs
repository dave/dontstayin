//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Venues.Moderate
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
		public Element H16 {get {if (_H16 == null) {_H16 = (Element)Document.GetElementById(clientId + "_H16");}; return _H16;}} private Element _H16;
		public jQueryObject H16J {get {if (_H16J == null) {_H16J = jQuery.Select("#" + clientId + "_H16");}; return _H16J;}} private jQueryObject _H16J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement ItemsPanel {get {if (_ItemsPanel == null) {_ItemsPanel = (DivElement)Document.GetElementById(clientId + "_ItemsPanel");}; return _ItemsPanel;}} private DivElement _ItemsPanel;
		public jQueryObject ItemsPanelJ {get {if (_ItemsPanelJ == null) {_ItemsPanelJ = jQuery.Select("#" + clientId + "_ItemsPanel");}; return _ItemsPanelJ;}} private jQueryObject _ItemsPanelJ;
		public Element ItemsRepeater {get {if (_ItemsRepeater == null) {_ItemsRepeater = (Element)Document.GetElementById(clientId + "_ItemsRepeater");}; return _ItemsRepeater;}} private Element _ItemsRepeater;
		public jQueryObject ItemsRepeaterJ {get {if (_ItemsRepeaterJ == null) {_ItemsRepeaterJ = jQuery.Select("#" + clientId + "_ItemsRepeater");}; return _ItemsRepeaterJ;}} private jQueryObject _ItemsRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element DeleteSelectedButton {get {if (_DeleteSelectedButton == null) {_DeleteSelectedButton = (Element)Document.GetElementById(clientId + "_DeleteSelectedButton");}; return _DeleteSelectedButton;}} private Element _DeleteSelectedButton;
		public jQueryObject DeleteSelectedButtonJ {get {if (_DeleteSelectedButtonJ == null) {_DeleteSelectedButtonJ = jQuery.Select("#" + clientId + "_DeleteSelectedButton");}; return _DeleteSelectedButtonJ;}} private jQueryObject _DeleteSelectedButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element OutputP {get {if (_OutputP == null) {_OutputP = (Element)Document.GetElementById(clientId + "_OutputP");}; return _OutputP;}} private Element _OutputP;
		public jQueryObject OutputPJ {get {if (_OutputPJ == null) {_OutputPJ = jQuery.Select("#" + clientId + "_OutputP");}; return _OutputPJ;}} private jQueryObject _OutputPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ModeratorsDataGrid {get {if (_ModeratorsDataGrid == null) {_ModeratorsDataGrid = (Element)Document.GetElementById(clientId + "_ModeratorsDataGrid");}; return _ModeratorsDataGrid;}} private Element _ModeratorsDataGrid;
		public jQueryObject ModeratorsDataGridJ {get {if (_ModeratorsDataGridJ == null) {_ModeratorsDataGridJ = jQuery.Select("#" + clientId + "_ModeratorsDataGrid");}; return _ModeratorsDataGridJ;}} private jQueryObject _ModeratorsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
