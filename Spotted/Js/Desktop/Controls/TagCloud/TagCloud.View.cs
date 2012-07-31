//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.SearchBoxControl", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.LinkCloud", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.TagCloud
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
		public Element uiTitle {get {if (_uiTitle == null) {_uiTitle = (Element)Document.GetElementById(clientId + "_uiTitle");}; return _uiTitle;}} private Element _uiTitle;
		public jQueryObject uiTitleJ {get {if (_uiTitleJ == null) {_uiTitleJ = jQuery.Select("#" + clientId + "_uiTitle");}; return _uiTitleJ;}} private jQueryObject _uiTitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement uiPanel {get {if (_uiPanel == null) {_uiPanel = (DivElement)Document.GetElementById(clientId + "_uiPanel");}; return _uiPanel;}} private DivElement _uiPanel;
		public jQueryObject uiPanelJ {get {if (_uiPanelJ == null) {_uiPanelJ = jQuery.Select("#" + clientId + "_uiPanel");}; return _uiPanelJ;}} private jQueryObject _uiPanelJ;
		public Element uiSearchControl {get {if (_uiSearchControl == null) {_uiSearchControl = (Element)Document.GetElementById(clientId + "_uiSearchControl");}; return _uiSearchControl;}} private Element _uiSearchControl;
		public jQueryObject uiSearchControlJ {get {if (_uiSearchControlJ == null) {_uiSearchControlJ = jQuery.Select("#" + clientId + "_uiSearchControl");}; return _uiSearchControlJ;}} private jQueryObject _uiSearchControlJ;//mappings.Add("Spotted.Controls.SearchBoxControl", ElementGetter("Element"));
		public Element uiLinkCloud {get {if (_uiLinkCloud == null) {_uiLinkCloud = (Element)Document.GetElementById(clientId + "_uiLinkCloud");}; return _uiLinkCloud;}} private Element _uiLinkCloud;
		public jQueryObject uiLinkCloudJ {get {if (_uiLinkCloudJ == null) {_uiLinkCloudJ = jQuery.Select("#" + clientId + "_uiLinkCloud");}; return _uiLinkCloudJ;}} private jQueryObject _uiLinkCloudJ;//mappings.Add("Spotted.Controls.LinkCloud", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
