//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.Tabbing.Tabs
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiTabsDiv {get {if (_uiTabsDiv == null) {_uiTabsDiv = (Element)Document.GetElementById(clientId + "_uiTabsDiv");}; return _uiTabsDiv;}} private Element _uiTabsDiv;
		public jQueryObject uiTabsDivJ {get {if (_uiTabsDivJ == null) {_uiTabsDivJ = jQuery.Select("#" + clientId + "_uiTabsDiv");}; return _uiTabsDivJ;}} private jQueryObject _uiTabsDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiTabsLinksContainer {get {if (_uiTabsLinksContainer == null) {_uiTabsLinksContainer = (Element)Document.GetElementById(clientId + "_uiTabsLinksContainer");}; return _uiTabsLinksContainer;}} private Element _uiTabsLinksContainer;
		public jQueryObject uiTabsLinksContainerJ {get {if (_uiTabsLinksContainerJ == null) {_uiTabsLinksContainerJ = jQuery.Select("#" + clientId + "_uiTabsLinksContainer");}; return _uiTabsLinksContainerJ;}} private jQueryObject _uiTabsLinksContainerJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement uiTabControllerNames {get {if (_uiTabControllerNames == null) {_uiTabControllerNames = (InputElement)Document.GetElementById(clientId + "_uiTabControllerNames");}; return _uiTabControllerNames;}} private InputElement _uiTabControllerNames;
		public jQueryObject uiTabControllerNamesJ {get {if (_uiTabControllerNamesJ == null) {_uiTabControllerNamesJ = jQuery.Select("#" + clientId + "_uiTabControllerNames");}; return _uiTabControllerNamesJ;}} private jQueryObject _uiTabControllerNamesJ;
		public InputElement uiTabTitleIds {get {if (_uiTabTitleIds == null) {_uiTabTitleIds = (InputElement)Document.GetElementById(clientId + "_uiTabTitleIds");}; return _uiTabTitleIds;}} private InputElement _uiTabTitleIds;
		public jQueryObject uiTabTitleIdsJ {get {if (_uiTabTitleIdsJ == null) {_uiTabTitleIdsJ = jQuery.Select("#" + clientId + "_uiTabTitleIds");}; return _uiTabTitleIdsJ;}} private jQueryObject _uiTabTitleIdsJ;
	}
}
