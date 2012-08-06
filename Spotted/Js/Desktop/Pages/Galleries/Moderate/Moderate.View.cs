//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Galleries.Moderate
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
		public Element H16ds {get {if (_H16ds == null) {_H16ds = (Element)Document.GetElementById(clientId + "_H16ds");}; return _H16ds;}} private Element _H16ds;
		public jQueryObject H16dsJ {get {if (_H16dsJ == null) {_H16dsJ = jQuery.Select("#" + clientId + "_H16ds");}; return _H16dsJ;}} private jQueryObject _H16dsJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H18 {get {if (_H18 == null) {_H18 = (Element)Document.GetElementById(clientId + "_H18");}; return _H18;}} private Element _H18;
		public jQueryObject H18J {get {if (_H18J == null) {_H18J = jQuery.Select("#" + clientId + "_H18");}; return _H18J;}} private jQueryObject _H18J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement Button5 {get {if (_Button5 == null) {_Button5 = (InputElement)Document.GetElementById(clientId + "_Button5");}; return _Button5;}} private InputElement _Button5;
		public jQueryObject Button5J {get {if (_Button5J == null) {_Button5J = jQuery.Select("#" + clientId + "_Button5");}; return _Button5J;}} private jQueryObject _Button5J;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H15 {get {if (_H15 == null) {_H15 = (Element)Document.GetElementById(clientId + "_H15");}; return _H15;}} private Element _H15;
		public jQueryObject H15J {get {if (_H15J == null) {_H15J = jQuery.Select("#" + clientId + "_H15");}; return _H15J;}} private jQueryObject _H15J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Button3 {get {if (_Button3 == null) {_Button3 = (Element)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private Element _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H17 {get {if (_H17 == null) {_H17 = (Element)Document.GetElementById(clientId + "_H17");}; return _H17;}} private Element _H17;
		public jQueryObject H17J {get {if (_H17J == null) {_H17J = jQuery.Select("#" + clientId + "_H17");}; return _H17J;}} private jQueryObject _H17J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button4 {get {if (_Button4 == null) {_Button4 = (Element)Document.GetElementById(clientId + "_Button4");}; return _Button4;}} private Element _Button4;
		public jQueryObject Button4J {get {if (_Button4J == null) {_Button4J = jQuery.Select("#" + clientId + "_Button4");}; return _Button4J;}} private jQueryObject _Button4J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Button6 {get {if (_Button6 == null) {_Button6 = (Element)Document.GetElementById(clientId + "_Button6");}; return _Button6;}} private Element _Button6;
		public jQueryObject Button6J {get {if (_Button6J == null) {_Button6J = jQuery.Select("#" + clientId + "_Button6");}; return _Button6J;}} private jQueryObject _Button6J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GalleriesDataGrid {get {if (_GalleriesDataGrid == null) {_GalleriesDataGrid = (Element)Document.GetElementById(clientId + "_GalleriesDataGrid");}; return _GalleriesDataGrid;}} private Element _GalleriesDataGrid;
		public jQueryObject GalleriesDataGridJ {get {if (_GalleriesDataGridJ == null) {_GalleriesDataGridJ = jQuery.Select("#" + clientId + "_GalleriesDataGrid");}; return _GalleriesDataGridJ;}} private jQueryObject _GalleriesDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element PhotoDataList {get {if (_PhotoDataList == null) {_PhotoDataList = (Element)Document.GetElementById(clientId + "_PhotoDataList");}; return _PhotoDataList;}} private Element _PhotoDataList;
		public jQueryObject PhotoDataListJ {get {if (_PhotoDataListJ == null) {_PhotoDataListJ = jQuery.Select("#" + clientId + "_PhotoDataList");}; return _PhotoDataListJ;}} private jQueryObject _PhotoDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public DivElement PhotosPanel {get {if (_PhotosPanel == null) {_PhotosPanel = (DivElement)Document.GetElementById(clientId + "_PhotosPanel");}; return _PhotosPanel;}} private DivElement _PhotosPanel;
		public jQueryObject PhotosPanelJ {get {if (_PhotosPanelJ == null) {_PhotosPanelJ = jQuery.Select("#" + clientId + "_PhotosPanel");}; return _PhotosPanelJ;}} private jQueryObject _PhotosPanelJ;
		public Element DeleteButton {get {if (_DeleteButton == null) {_DeleteButton = (Element)Document.GetElementById(clientId + "_DeleteButton");}; return _DeleteButton;}} private Element _DeleteButton;
		public jQueryObject DeleteButtonJ {get {if (_DeleteButtonJ == null) {_DeleteButtonJ = jQuery.Select("#" + clientId + "_DeleteButton");}; return _DeleteButtonJ;}} private jQueryObject _DeleteButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element DeleteSelectedButton {get {if (_DeleteSelectedButton == null) {_DeleteSelectedButton = (Element)Document.GetElementById(clientId + "_DeleteSelectedButton");}; return _DeleteSelectedButton;}} private Element _DeleteSelectedButton;
		public jQueryObject DeleteSelectedButtonJ {get {if (_DeleteSelectedButtonJ == null) {_DeleteSelectedButtonJ = jQuery.Select("#" + clientId + "_DeleteSelectedButton");}; return _DeleteSelectedButtonJ;}} private jQueryObject _DeleteSelectedButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element DeleteButton1 {get {if (_DeleteButton1 == null) {_DeleteButton1 = (Element)Document.GetElementById(clientId + "_DeleteButton1");}; return _DeleteButton1;}} private Element _DeleteButton1;
		public jQueryObject DeleteButton1J {get {if (_DeleteButton1J == null) {_DeleteButton1J = jQuery.Select("#" + clientId + "_DeleteButton1");}; return _DeleteButton1J;}} private jQueryObject _DeleteButton1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element DeleteSelectedButton1 {get {if (_DeleteSelectedButton1 == null) {_DeleteSelectedButton1 = (Element)Document.GetElementById(clientId + "_DeleteSelectedButton1");}; return _DeleteSelectedButton1;}} private Element _DeleteSelectedButton1;
		public jQueryObject DeleteSelectedButton1J {get {if (_DeleteSelectedButton1J == null) {_DeleteSelectedButton1J = jQuery.Select("#" + clientId + "_DeleteSelectedButton1");}; return _DeleteSelectedButton1J;}} private jQueryObject _DeleteSelectedButton1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public InputElement AdminNoteTextBox {get {if (_AdminNoteTextBox == null) {_AdminNoteTextBox = (InputElement)Document.GetElementById(clientId + "_AdminNoteTextBox");}; return _AdminNoteTextBox;}} private InputElement _AdminNoteTextBox;
		public jQueryObject AdminNoteTextBoxJ {get {if (_AdminNoteTextBoxJ == null) {_AdminNoteTextBoxJ = jQuery.Select("#" + clientId + "_AdminNoteTextBox");}; return _AdminNoteTextBoxJ;}} private jQueryObject _AdminNoteTextBoxJ;
		public DivElement GalleriesPanel {get {if (_GalleriesPanel == null) {_GalleriesPanel = (DivElement)Document.GetElementById(clientId + "_GalleriesPanel");}; return _GalleriesPanel;}} private DivElement _GalleriesPanel;
		public jQueryObject GalleriesPanelJ {get {if (_GalleriesPanelJ == null) {_GalleriesPanelJ = jQuery.Select("#" + clientId + "_GalleriesPanel");}; return _GalleriesPanelJ;}} private jQueryObject _GalleriesPanelJ;
		public DivElement DonePanel {get {if (_DonePanel == null) {_DonePanel = (DivElement)Document.GetElementById(clientId + "_DonePanel");}; return _DonePanel;}} private DivElement _DonePanel;
		public jQueryObject DonePanelJ {get {if (_DonePanelJ == null) {_DonePanelJ = jQuery.Select("#" + clientId + "_DonePanel");}; return _DonePanelJ;}} private jQueryObject _DonePanelJ;
		public DivElement InfoPanel {get {if (_InfoPanel == null) {_InfoPanel = (DivElement)Document.GetElementById(clientId + "_InfoPanel");}; return _InfoPanel;}} private DivElement _InfoPanel;
		public jQueryObject InfoPanelJ {get {if (_InfoPanelJ == null) {_InfoPanelJ = jQuery.Select("#" + clientId + "_InfoPanel");}; return _InfoPanelJ;}} private jQueryObject _InfoPanelJ;
		public Element SelectedOutputP {get {if (_SelectedOutputP == null) {_SelectedOutputP = (Element)Document.GetElementById(clientId + "_SelectedOutputP");}; return _SelectedOutputP;}} private Element _SelectedOutputP;
		public jQueryObject SelectedOutputPJ {get {if (_SelectedOutputPJ == null) {_SelectedOutputPJ = jQuery.Select("#" + clientId + "_SelectedOutputP");}; return _SelectedOutputPJ;}} private jQueryObject _SelectedOutputPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
