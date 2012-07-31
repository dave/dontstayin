//mappings.Add("Spotted.Pages.Articles.HomeContent", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Articles.Home
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
		public Element HomeContent {get {if (_HomeContent == null) {_HomeContent = (Element)Document.GetElementById(clientId + "_HomeContent");}; return _HomeContent;}} private Element _HomeContent;
		public jQueryObject HomeContentJ {get {if (_HomeContentJ == null) {_HomeContentJ = jQuery.Select("#" + clientId + "_HomeContent");}; return _HomeContentJ;}} private jQueryObject _HomeContentJ;//mappings.Add("Spotted.Pages.Articles.HomeContent", ElementGetter("Element"));
		public Js.Controls.LatestChat.Controller LatestChat {get {return (Js.Controls.LatestChat.Controller) Script.Eval(clientId + "_LatestChatController");}}
		public Js.Controls.ThreadControl.Controller ThreadControl {get {return (Js.Controls.ThreadControl.Controller) Script.Eval(clientId + "_ThreadControlController");}}
		public InputElement uiThreadK {get {if (_uiThreadK == null) {_uiThreadK = (InputElement)Document.GetElementById(clientId + "_uiThreadK");}; return _uiThreadK;}} private InputElement _uiThreadK;
		public jQueryObject uiThreadKJ {get {if (_uiThreadKJ == null) {_uiThreadKJ = jQuery.Select("#" + clientId + "_uiThreadK");}; return _uiThreadKJ;}} private jQueryObject _uiThreadKJ;
		public InputElement uiArticleK {get {if (_uiArticleK == null) {_uiArticleK = (InputElement)Document.GetElementById(clientId + "_uiArticleK");}; return _uiArticleK;}} private InputElement _uiArticleK;
		public jQueryObject uiArticleKJ {get {if (_uiArticleKJ == null) {_uiArticleKJ = jQuery.Select("#" + clientId + "_uiArticleK");}; return _uiArticleKJ;}} private jQueryObject _uiArticleKJ;
		public InputElement uiPageNumber {get {if (_uiPageNumber == null) {_uiPageNumber = (InputElement)Document.GetElementById(clientId + "_uiPageNumber");}; return _uiPageNumber;}} private InputElement _uiPageNumber;
		public jQueryObject uiPageNumberJ {get {if (_uiPageNumberJ == null) {_uiPageNumberJ = jQuery.Select("#" + clientId + "_uiPageNumber");}; return _uiPageNumberJ;}} private jQueryObject _uiPageNumberJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
