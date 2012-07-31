//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Articles.Photos
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
		public Element uiArticleInfoSpan {get {if (_uiArticleInfoSpan == null) {_uiArticleInfoSpan = (Element)Document.GetElementById(clientId + "_uiArticleInfoSpan");}; return _uiArticleInfoSpan;}} private Element _uiArticleInfoSpan;
		public jQueryObject uiArticleInfoSpanJ {get {if (_uiArticleInfoSpanJ == null) {_uiArticleInfoSpanJ = jQuery.Select("#" + clientId + "_uiArticleInfoSpan");}; return _uiArticleInfoSpanJ;}} private jQueryObject _uiArticleInfoSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Js.Controls.PhotoBrowser.Controller uiPhotoBrowser {get {return (Js.Controls.PhotoBrowser.Controller) Script.Eval(clientId + "_uiPhotoBrowserController");}}
		public Js.Controls.PhotoControl.Controller uiPhotoControl {get {return (Js.Controls.PhotoControl.Controller) Script.Eval(clientId + "_uiPhotoControlController");}}
		public Js.Controls.LatestChat.Controller uiLatestChat {get {return (Js.Controls.LatestChat.Controller) Script.Eval(clientId + "_uiLatestChatController");}}
		public Js.Controls.ThreadControl.Controller uiThreadControl {get {return (Js.Controls.ThreadControl.Controller) Script.Eval(clientId + "_uiThreadControlController");}}
		public InputElement uiArticleK {get {if (_uiArticleK == null) {_uiArticleK = (InputElement)Document.GetElementById(clientId + "_uiArticleK");}; return _uiArticleK;}} private InputElement _uiArticleK;
		public jQueryObject uiArticleKJ {get {if (_uiArticleKJ == null) {_uiArticleKJ = jQuery.Select("#" + clientId + "_uiArticleK");}; return _uiArticleKJ;}} private jQueryObject _uiArticleKJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
