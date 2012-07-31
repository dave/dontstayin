//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Brands.Merge
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
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiMasterBrandUrl {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiMasterBrandUrlBehaviour");}}
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiMergeBrandUrl {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiMergeBrandUrlBehaviour");}}
		public Element MergeButton {get {if (_MergeButton == null) {_MergeButton = (Element)Document.GetElementById(clientId + "_MergeButton");}; return _MergeButton;}} private Element _MergeButton;
		public jQueryObject MergeButtonJ {get {if (_MergeButtonJ == null) {_MergeButtonJ = jQuery.Select("#" + clientId + "_MergeButton");}; return _MergeButtonJ;}} private jQueryObject _MergeButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
