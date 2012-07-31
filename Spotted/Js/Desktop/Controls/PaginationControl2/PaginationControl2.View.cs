//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.UrlButton", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.PaginationControl2
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiContainer {get {if (_uiContainer == null) {_uiContainer = (Element)Document.GetElementById(clientId + "_uiContainer");}; return _uiContainer;}} private Element _uiContainer;
		public jQueryObject uiContainerJ {get {if (_uiContainerJ == null) {_uiContainerJ = jQuery.Select("#" + clientId + "_uiContainer");}; return _uiContainerJ;}} private jQueryObject _uiContainerJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiPrevPage {get {if (_uiPrevPage == null) {_uiPrevPage = (Element)Document.GetElementById(clientId + "_uiPrevPage");}; return _uiPrevPage;}} private Element _uiPrevPage;
		public jQueryObject uiPrevPageJ {get {if (_uiPrevPageJ == null) {_uiPrevPageJ = jQuery.Select("#" + clientId + "_uiPrevPage");}; return _uiPrevPageJ;}} private jQueryObject _uiPrevPageJ;//mappings.Add("Spotted.CustomControls.UrlButton", ElementGetter("Element"));
		public Element uiCurrentPage {get {if (_uiCurrentPage == null) {_uiCurrentPage = (Element)Document.GetElementById(clientId + "_uiCurrentPage");}; return _uiCurrentPage;}} private Element _uiCurrentPage;
		public jQueryObject uiCurrentPageJ {get {if (_uiCurrentPageJ == null) {_uiCurrentPageJ = jQuery.Select("#" + clientId + "_uiCurrentPage");}; return _uiCurrentPageJ;}} private jQueryObject _uiCurrentPageJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiLastPage {get {if (_uiLastPage == null) {_uiLastPage = (Element)Document.GetElementById(clientId + "_uiLastPage");}; return _uiLastPage;}} private Element _uiLastPage;
		public jQueryObject uiLastPageJ {get {if (_uiLastPageJ == null) {_uiLastPageJ = jQuery.Select("#" + clientId + "_uiLastPage");}; return _uiLastPageJ;}} private jQueryObject _uiLastPageJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiNextPage {get {if (_uiNextPage == null) {_uiNextPage = (Element)Document.GetElementById(clientId + "_uiNextPage");}; return _uiNextPage;}} private Element _uiNextPage;
		public jQueryObject uiNextPageJ {get {if (_uiNextPageJ == null) {_uiNextPageJ = jQuery.Select("#" + clientId + "_uiNextPage");}; return _uiNextPageJ;}} private jQueryObject _uiNextPageJ;//mappings.Add("Spotted.CustomControls.UrlButton", ElementGetter("Element"));
	}
}
