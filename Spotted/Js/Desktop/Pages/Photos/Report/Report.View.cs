//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Photos.Report
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
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public ImageElement PhotoImg {get {if (_PhotoImg == null) {_PhotoImg = (ImageElement)Document.GetElementById(clientId + "_PhotoImg");}; return _PhotoImg;}} private ImageElement _PhotoImg;
		public jQueryObject PhotoImgJ {get {if (_PhotoImgJ == null) {_PhotoImgJ = jQuery.Select("#" + clientId + "_PhotoImg");}; return _PhotoImgJ;}} private jQueryObject _PhotoImgJ;
		public AnchorElement PhotoAnchor {get {if (_PhotoAnchor == null) {_PhotoAnchor = (AnchorElement)Document.GetElementById(clientId + "_PhotoAnchor");}; return _PhotoAnchor;}} private AnchorElement _PhotoAnchor;
		public jQueryObject PhotoAnchorJ {get {if (_PhotoAnchorJ == null) {_PhotoAnchorJ = jQuery.Select("#" + clientId + "_PhotoAnchor");}; return _PhotoAnchorJ;}} private jQueryObject _PhotoAnchorJ;
		public InputElement ReportMessageTextBox {get {if (_ReportMessageTextBox == null) {_ReportMessageTextBox = (InputElement)Document.GetElementById(clientId + "_ReportMessageTextBox");}; return _ReportMessageTextBox;}} private InputElement _ReportMessageTextBox;
		public jQueryObject ReportMessageTextBoxJ {get {if (_ReportMessageTextBoxJ == null) {_ReportMessageTextBoxJ = jQuery.Select("#" + clientId + "_ReportMessageTextBox");}; return _ReportMessageTextBoxJ;}} private jQueryObject _ReportMessageTextBoxJ;
		public Element ReportP {get {if (_ReportP == null) {_ReportP = (Element)Document.GetElementById(clientId + "_ReportP");}; return _ReportP;}} private Element _ReportP;
		public jQueryObject ReportPJ {get {if (_ReportPJ == null) {_ReportPJ = jQuery.Select("#" + clientId + "_ReportP");}; return _ReportPJ;}} private jQueryObject _ReportPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement BuddyCheckBoxesPanel {get {if (_BuddyCheckBoxesPanel == null) {_BuddyCheckBoxesPanel = (DivElement)Document.GetElementById(clientId + "_BuddyCheckBoxesPanel");}; return _BuddyCheckBoxesPanel;}} private DivElement _BuddyCheckBoxesPanel;
		public jQueryObject BuddyCheckBoxesPanelJ {get {if (_BuddyCheckBoxesPanelJ == null) {_BuddyCheckBoxesPanelJ = jQuery.Select("#" + clientId + "_BuddyCheckBoxesPanel");}; return _BuddyCheckBoxesPanelJ;}} private jQueryObject _BuddyCheckBoxesPanelJ;
		public Element BuddyCheckBoxList {get {if (_BuddyCheckBoxList == null) {_BuddyCheckBoxList = (Element)Document.GetElementById(clientId + "_BuddyCheckBoxList");}; return _BuddyCheckBoxList;}} private Element _BuddyCheckBoxList;
		public jQueryObject BuddyCheckBoxListJ {get {if (_BuddyCheckBoxListJ == null) {_BuddyCheckBoxListJ = jQuery.Select("#" + clientId + "_BuddyCheckBoxList");}; return _BuddyCheckBoxListJ;}} private jQueryObject _BuddyCheckBoxListJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public CheckBoxElement BuddyCheckBoxAll {get {if (_BuddyCheckBoxAll == null) {_BuddyCheckBoxAll = (CheckBoxElement)Document.GetElementById(clientId + "_BuddyCheckBoxAll");}; return _BuddyCheckBoxAll;}} private CheckBoxElement _BuddyCheckBoxAll;
		public jQueryObject BuddyCheckBoxAllJ {get {if (_BuddyCheckBoxAllJ == null) {_BuddyCheckBoxAllJ = jQuery.Select("#" + clientId + "_BuddyCheckBoxAll");}; return _BuddyCheckBoxAllJ;}} private jQueryObject _BuddyCheckBoxAllJ;
		public DivElement DonePanel {get {if (_DonePanel == null) {_DonePanel = (DivElement)Document.GetElementById(clientId + "_DonePanel");}; return _DonePanel;}} private DivElement _DonePanel;
		public jQueryObject DonePanelJ {get {if (_DonePanelJ == null) {_DonePanelJ = jQuery.Select("#" + clientId + "_DonePanel");}; return _DonePanelJ;}} private jQueryObject _DonePanelJ;
		public DivElement ReportPanel {get {if (_ReportPanel == null) {_ReportPanel = (DivElement)Document.GetElementById(clientId + "_ReportPanel");}; return _ReportPanel;}} private DivElement _ReportPanel;
		public jQueryObject ReportPanelJ {get {if (_ReportPanelJ == null) {_ReportPanelJ = jQuery.Select("#" + clientId + "_ReportPanel");}; return _ReportPanelJ;}} private jQueryObject _ReportPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
