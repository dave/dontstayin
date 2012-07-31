//mappings.Add("AjaxControlToolkit.AutoCompleteExtender", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.SearchBoxControl
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public InputElement uiSearchQuery {get {if (_uiSearchQuery == null) {_uiSearchQuery = (InputElement)Document.GetElementById(clientId + "_uiSearchQuery");}; return _uiSearchQuery;}} private InputElement _uiSearchQuery;
		public jQueryObject uiSearchQueryJ {get {if (_uiSearchQueryJ == null) {_uiSearchQueryJ = jQuery.Select("#" + clientId + "_uiSearchQuery");}; return _uiSearchQueryJ;}} private jQueryObject _uiSearchQueryJ;
		public Element uiTagAutoComplete {get {if (_uiTagAutoComplete == null) {_uiTagAutoComplete = (Element)Document.GetElementById(clientId + "_uiTagAutoComplete");}; return _uiTagAutoComplete;}} private Element _uiTagAutoComplete;
		public jQueryObject uiTagAutoCompleteJ {get {if (_uiTagAutoCompleteJ == null) {_uiTagAutoCompleteJ = jQuery.Select("#" + clientId + "_uiTagAutoComplete");}; return _uiTagAutoCompleteJ;}} private jQueryObject _uiTagAutoCompleteJ;//mappings.Add("AjaxControlToolkit.AutoCompleteExtender", ElementGetter("Element"));
		public Element uiSubmitSearchButton {get {if (_uiSubmitSearchButton == null) {_uiSubmitSearchButton = (Element)Document.GetElementById(clientId + "_uiSubmitSearchButton");}; return _uiSubmitSearchButton;}} private Element _uiSubmitSearchButton;
		public jQueryObject uiSubmitSearchButtonJ {get {if (_uiSubmitSearchButtonJ == null) {_uiSubmitSearchButtonJ = jQuery.Select("#" + clientId + "_uiSubmitSearchButton");}; return _uiSubmitSearchButtonJ;}} private jQueryObject _uiSubmitSearchButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
	}
}
