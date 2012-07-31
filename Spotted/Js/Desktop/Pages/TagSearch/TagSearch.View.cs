//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.TagCloud", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.SearchBoxControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.TagSearch
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
		public Element uiTagCloud {get {if (_uiTagCloud == null) {_uiTagCloud = (Element)Document.GetElementById(clientId + "_uiTagCloud");}; return _uiTagCloud;}} private Element _uiTagCloud;
		public jQueryObject uiTagCloudJ {get {if (_uiTagCloudJ == null) {_uiTagCloudJ = jQuery.Select("#" + clientId + "_uiTagCloud");}; return _uiTagCloudJ;}} private jQueryObject _uiTagCloudJ;//mappings.Add("Spotted.Controls.TagCloud", ElementGetter("Element"));
		public DivElement uiSearchBoxPanel {get {if (_uiSearchBoxPanel == null) {_uiSearchBoxPanel = (DivElement)Document.GetElementById(clientId + "_uiSearchBoxPanel");}; return _uiSearchBoxPanel;}} private DivElement _uiSearchBoxPanel;
		public jQueryObject uiSearchBoxPanelJ {get {if (_uiSearchBoxPanelJ == null) {_uiSearchBoxPanelJ = jQuery.Select("#" + clientId + "_uiSearchBoxPanel");}; return _uiSearchBoxPanelJ;}} private jQueryObject _uiSearchBoxPanelJ;
		public Element uiSearchBoxControl {get {if (_uiSearchBoxControl == null) {_uiSearchBoxControl = (Element)Document.GetElementById(clientId + "_uiSearchBoxControl");}; return _uiSearchBoxControl;}} private Element _uiSearchBoxControl;
		public jQueryObject uiSearchBoxControlJ {get {if (_uiSearchBoxControlJ == null) {_uiSearchBoxControlJ = jQuery.Select("#" + clientId + "_uiSearchBoxControl");}; return _uiSearchBoxControlJ;}} private jQueryObject _uiSearchBoxControlJ;//mappings.Add("Spotted.Controls.SearchBoxControl", ElementGetter("Element"));
		public Js.Controls.PhotoBrowser.Controller uiPhotoBrowser {get {return (Js.Controls.PhotoBrowser.Controller) Script.Eval(clientId + "_uiPhotoBrowserController");}}
		public Js.Controls.PhotoControl.Controller uiPhotoControl {get {return (Js.Controls.PhotoControl.Controller) Script.Eval(clientId + "_uiPhotoControlController");}}
		public Js.Controls.LatestChat.Controller uiLatestChat {get {return (Js.Controls.LatestChat.Controller) Script.Eval(clientId + "_uiLatestChatController");}}
		public Js.Controls.ThreadControl.Controller uiThreadControl {get {return (Js.Controls.ThreadControl.Controller) Script.Eval(clientId + "_uiThreadControlController");}}
		public InputElement uiTagK {get {if (_uiTagK == null) {_uiTagK = (InputElement)Document.GetElementById(clientId + "_uiTagK");}; return _uiTagK;}} private InputElement _uiTagK;
		public jQueryObject uiTagKJ {get {if (_uiTagKJ == null) {_uiTagKJ = jQuery.Select("#" + clientId + "_uiTagK");}; return _uiTagKJ;}} private jQueryObject _uiTagKJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
