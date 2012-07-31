//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Articles
{
	public partial class View
		 : Js.Pages.Promoters.PromoterUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element H110 {get {if (_H110 == null) {_H110 = (Element)Document.GetElementById(clientId + "_H110");}; return _H110;}} private Element _H110;
		public jQueryObject H110J {get {if (_H110J == null) {_H110J = jQuery.Select("#" + clientId + "_H110");}; return _H110J;}} private jQueryObject _H110J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H111 {get {if (_H111 == null) {_H111 = (Element)Document.GetElementById(clientId + "_H111");}; return _H111;}} private Element _H111;
		public jQueryObject H111J {get {if (_H111J == null) {_H111J = jQuery.Select("#" + clientId + "_H111");}; return _H111J;}} private jQueryObject _H111J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element PromoterIntro {get {if (_PromoterIntro == null) {_PromoterIntro = (Element)Document.GetElementById(clientId + "_PromoterIntro");}; return _PromoterIntro;}} private Element _PromoterIntro;
		public jQueryObject PromoterIntroJ {get {if (_PromoterIntroJ == null) {_PromoterIntroJ = jQuery.Select("#" + clientId + "_PromoterIntro");}; return _PromoterIntroJ;}} private jQueryObject _PromoterIntroJ;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public DivElement ArticlePanel {get {if (_ArticlePanel == null) {_ArticlePanel = (DivElement)Document.GetElementById(clientId + "_ArticlePanel");}; return _ArticlePanel;}} private DivElement _ArticlePanel;
		public jQueryObject ArticlePanelJ {get {if (_ArticlePanelJ == null) {_ArticlePanelJ = jQuery.Select("#" + clientId + "_ArticlePanel");}; return _ArticlePanelJ;}} private jQueryObject _ArticlePanelJ;
		public DivElement NoArticlePanel {get {if (_NoArticlePanel == null) {_NoArticlePanel = (DivElement)Document.GetElementById(clientId + "_NoArticlePanel");}; return _NoArticlePanel;}} private DivElement _NoArticlePanel;
		public jQueryObject NoArticlePanelJ {get {if (_NoArticlePanelJ == null) {_NoArticlePanelJ = jQuery.Select("#" + clientId + "_NoArticlePanel");}; return _NoArticlePanelJ;}} private jQueryObject _NoArticlePanelJ;
		public AnchorElement ArticleAddLink {get {if (_ArticleAddLink == null) {_ArticleAddLink = (AnchorElement)Document.GetElementById(clientId + "_ArticleAddLink");}; return _ArticleAddLink;}} private AnchorElement _ArticleAddLink;
		public jQueryObject ArticleAddLinkJ {get {if (_ArticleAddLinkJ == null) {_ArticleAddLinkJ = jQuery.Select("#" + clientId + "_ArticleAddLink");}; return _ArticleAddLinkJ;}} private jQueryObject _ArticleAddLinkJ;
		public Element ArticleDataGrid {get {if (_ArticleDataGrid == null) {_ArticleDataGrid = (Element)Document.GetElementById(clientId + "_ArticleDataGrid");}; return _ArticleDataGrid;}} private Element _ArticleDataGrid;
		public jQueryObject ArticleDataGridJ {get {if (_ArticleDataGridJ == null) {_ArticleDataGridJ = jQuery.Select("#" + clientId + "_ArticleDataGrid");}; return _ArticleDataGridJ;}} private jQueryObject _ArticleDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
