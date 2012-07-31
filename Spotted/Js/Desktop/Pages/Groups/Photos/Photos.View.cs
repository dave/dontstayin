//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.UpdatePanel", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Groups.Photos
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
		public Js.Controls.PhotoBrowser.Controller uiPhotoBrowser {get {return (Js.Controls.PhotoBrowser.Controller) Script.Eval(clientId + "_uiPhotoBrowserController");}}
		public Js.Controls.PhotoControl.Controller uiPhotoControl {get {return (Js.Controls.PhotoControl.Controller) Script.Eval(clientId + "_uiPhotoControlController");}}
		public Element uiUpdatePanel {get {if (_uiUpdatePanel == null) {_uiUpdatePanel = (Element)Document.GetElementById(clientId + "_uiUpdatePanel");}; return _uiUpdatePanel;}} private Element _uiUpdatePanel;
		public jQueryObject uiUpdatePanelJ {get {if (_uiUpdatePanelJ == null) {_uiUpdatePanelJ = jQuery.Select("#" + clientId + "_uiUpdatePanel");}; return _uiUpdatePanelJ;}} private jQueryObject _uiUpdatePanelJ;//mappings.Add("System.Web.UI.UpdatePanel", ElementGetter("Element"));
		public Js.Controls.LatestChat.Controller uiLatestChat {get {return (Js.Controls.LatestChat.Controller) Script.Eval(clientId + "_uiLatestChatController");}}
		public Js.Controls.ThreadControl.Controller uiThreadControl {get {return (Js.Controls.ThreadControl.Controller) Script.Eval(clientId + "_uiThreadControlController");}}
		public InputElement uiGroupK {get {if (_uiGroupK == null) {_uiGroupK = (InputElement)Document.GetElementById(clientId + "_uiGroupK");}; return _uiGroupK;}} private InputElement _uiGroupK;
		public jQueryObject uiGroupKJ {get {if (_uiGroupKJ == null) {_uiGroupKJ = jQuery.Select("#" + clientId + "_uiGroupK");}; return _uiGroupKJ;}} private jQueryObject _uiGroupKJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
