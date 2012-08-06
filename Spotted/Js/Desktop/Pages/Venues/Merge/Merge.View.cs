//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Venues.Merge
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
		public DivElement PanelMerge {get {if (_PanelMerge == null) {_PanelMerge = (DivElement)Document.GetElementById(clientId + "_PanelMerge");}; return _PanelMerge;}} private DivElement _PanelMerge;
		public jQueryObject PanelMergeJ {get {if (_PanelMergeJ == null) {_PanelMergeJ = jQuery.Select("#" + clientId + "_PanelMerge");}; return _PanelMergeJ;}} private jQueryObject _PanelMergeJ;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiMasterVenueAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiMasterVenueAutoCompleteBehaviour");}}
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiMergeVenueAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiMergeVenueAutoCompleteBehaviour");}}
		public Element MergeButton {get {if (_MergeButton == null) {_MergeButton = (Element)Document.GetElementById(clientId + "_MergeButton");}; return _MergeButton;}} private Element _MergeButton;
		public jQueryObject MergeButtonJ {get {if (_MergeButtonJ == null) {_MergeButtonJ = jQuery.Select("#" + clientId + "_MergeButton");}; return _MergeButtonJ;}} private jQueryObject _MergeButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Js.Controls.Picker.Controller uiMasterVenuePicker {get {return (Js.Controls.Picker.Controller) Script.Eval(clientId + "_uiMasterVenuePickerController");}}
		public Js.Controls.Picker.Controller uiMergeVenuePicker {get {return (Js.Controls.Picker.Controller) Script.Eval(clientId + "_uiMergeVenuePickerController");}}
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
