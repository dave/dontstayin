//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.UpdatePanel", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.Photos
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
		public Element UsrIntro {get {if (_UsrIntro == null) {_UsrIntro = (Element)Document.GetElementById(clientId + "_UsrIntro");}; return _UsrIntro;}} private Element _UsrIntro;
		public jQueryObject UsrIntroJ {get {if (_UsrIntroJ == null) {_UsrIntroJ = jQuery.Select("#" + clientId + "_UsrIntro");}; return _UsrIntroJ;}} private jQueryObject _UsrIntroJ;//mappings.Add("Spotted.CustomControls.UsrIntro", ElementGetter("Element"));
		public Element TakenBySpan {get {if (_TakenBySpan == null) {_TakenBySpan = (Element)Document.GetElementById(clientId + "_TakenBySpan");}; return _TakenBySpan;}} private Element _TakenBySpan;
		public jQueryObject TakenBySpanJ {get {if (_TakenBySpanJ == null) {_TakenBySpanJ = jQuery.Select("#" + clientId + "_TakenBySpan");}; return _TakenBySpanJ;}} private jQueryObject _TakenBySpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Js.Controls.PhotoBrowser.Controller uiPhotoBrowser {get {return (Js.Controls.PhotoBrowser.Controller) Script.Eval(clientId + "_uiPhotoBrowserController");}}
		public Js.Controls.PhotoControl.Controller uiPhotoControl {get {return (Js.Controls.PhotoControl.Controller) Script.Eval(clientId + "_uiPhotoControlController");}}
		public Element uiUpdatePanel {get {if (_uiUpdatePanel == null) {_uiUpdatePanel = (Element)Document.GetElementById(clientId + "_uiUpdatePanel");}; return _uiUpdatePanel;}} private Element _uiUpdatePanel;
		public jQueryObject uiUpdatePanelJ {get {if (_uiUpdatePanelJ == null) {_uiUpdatePanelJ = jQuery.Select("#" + clientId + "_uiUpdatePanel");}; return _uiUpdatePanelJ;}} private jQueryObject _uiUpdatePanelJ;//mappings.Add("System.Web.UI.UpdatePanel", ElementGetter("Element"));
		public Js.Controls.LatestChat.Controller uiLatestChat {get {return (Js.Controls.LatestChat.Controller) Script.Eval(clientId + "_uiLatestChatController");}}
		public Js.Controls.ThreadControl.Controller uiThreadControl {get {return (Js.Controls.ThreadControl.Controller) Script.Eval(clientId + "_uiThreadControlController");}}
		public InputElement uiUsrK {get {if (_uiUsrK == null) {_uiUsrK = (InputElement)Document.GetElementById(clientId + "_uiUsrK");}; return _uiUsrK;}} private InputElement _uiUsrK;
		public jQueryObject uiUsrKJ {get {if (_uiUsrKJ == null) {_uiUsrKJ = jQuery.Select("#" + clientId + "_uiUsrK");}; return _uiUsrKJ;}} private jQueryObject _uiUsrKJ;
		public InputElement uiSpottedByUsrK {get {if (_uiSpottedByUsrK == null) {_uiSpottedByUsrK = (InputElement)Document.GetElementById(clientId + "_uiSpottedByUsrK");}; return _uiSpottedByUsrK;}} private InputElement _uiSpottedByUsrK;
		public jQueryObject uiSpottedByUsrKJ {get {if (_uiSpottedByUsrKJ == null) {_uiSpottedByUsrKJ = jQuery.Select("#" + clientId + "_uiSpottedByUsrK");}; return _uiSpottedByUsrKJ;}} private jQueryObject _uiSpottedByUsrKJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
