//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.Inbox
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public DivElement InboxFilterPanel {get {if (_InboxFilterPanel == null) {_InboxFilterPanel = (DivElement)Document.GetElementById(clientId + "_InboxFilterPanel");}; return _InboxFilterPanel;}} private DivElement _InboxFilterPanel;
		public jQueryObject InboxFilterPanelJ {get {if (_InboxFilterPanelJ == null) {_InboxFilterPanelJ = jQuery.Select("#" + clientId + "_InboxFilterPanel");}; return _InboxFilterPanelJ;}} private jQueryObject _InboxFilterPanelJ;
		public Element InboxFilterP {get {if (_InboxFilterP == null) {_InboxFilterP = (Element)Document.GetElementById(clientId + "_InboxFilterP");}; return _InboxFilterP;}} private Element _InboxFilterP;
		public jQueryObject InboxFilterPJ {get {if (_InboxFilterPJ == null) {_InboxFilterPJ = jQuery.Select("#" + clientId + "_InboxFilterP");}; return _InboxFilterPJ;}} private jQueryObject _InboxFilterPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement FilterPanel {get {if (_FilterPanel == null) {_FilterPanel = (DivElement)Document.GetElementById(clientId + "_FilterPanel");}; return _FilterPanel;}} private DivElement _FilterPanel;
		public jQueryObject FilterPanelJ {get {if (_FilterPanelJ == null) {_FilterPanelJ = jQuery.Select("#" + clientId + "_FilterPanel");}; return _FilterPanelJ;}} private jQueryObject _FilterPanelJ;
		public CheckBoxElement BuddyPostCheckBox {get {if (_BuddyPostCheckBox == null) {_BuddyPostCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_BuddyPostCheckBox");}; return _BuddyPostCheckBox;}} private CheckBoxElement _BuddyPostCheckBox;
		public jQueryObject BuddyPostCheckBoxJ {get {if (_BuddyPostCheckBoxJ == null) {_BuddyPostCheckBoxJ = jQuery.Select("#" + clientId + "_BuddyPostCheckBox");}; return _BuddyPostCheckBoxJ;}} private jQueryObject _BuddyPostCheckBoxJ;
		public SelectElement BuddyDropDownList {get {if (_BuddyDropDownList == null) {_BuddyDropDownList = (SelectElement)Document.GetElementById(clientId + "_BuddyDropDownList");}; return _BuddyDropDownList;}} private SelectElement _BuddyDropDownList;
		public jQueryObject BuddyDropDownListJ {get {if (_BuddyDropDownListJ == null) {_BuddyDropDownListJ = jQuery.Select("#" + clientId + "_BuddyDropDownList");}; return _BuddyDropDownListJ;}} private jQueryObject _BuddyDropDownListJ;
		public CheckBoxElement GroupPostCheckBox {get {if (_GroupPostCheckBox == null) {_GroupPostCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_GroupPostCheckBox");}; return _GroupPostCheckBox;}} private CheckBoxElement _GroupPostCheckBox;
		public jQueryObject GroupPostCheckBoxJ {get {if (_GroupPostCheckBoxJ == null) {_GroupPostCheckBoxJ = jQuery.Select("#" + clientId + "_GroupPostCheckBox");}; return _GroupPostCheckBoxJ;}} private jQueryObject _GroupPostCheckBoxJ;
		public SelectElement GroupDropDownList {get {if (_GroupDropDownList == null) {_GroupDropDownList = (SelectElement)Document.GetElementById(clientId + "_GroupDropDownList");}; return _GroupDropDownList;}} private SelectElement _GroupDropDownList;
		public jQueryObject GroupDropDownListJ {get {if (_GroupDropDownListJ == null) {_GroupDropDownListJ = jQuery.Select("#" + clientId + "_GroupDropDownList");}; return _GroupDropDownListJ;}} private jQueryObject _GroupDropDownListJ;
		public DivElement NoThreadsPanel {get {if (_NoThreadsPanel == null) {_NoThreadsPanel = (DivElement)Document.GetElementById(clientId + "_NoThreadsPanel");}; return _NoThreadsPanel;}} private DivElement _NoThreadsPanel;
		public jQueryObject NoThreadsPanelJ {get {if (_NoThreadsPanelJ == null) {_NoThreadsPanelJ = jQuery.Select("#" + clientId + "_NoThreadsPanel");}; return _NoThreadsPanelJ;}} private jQueryObject _NoThreadsPanelJ;
		public InputElement NoThreadsRefreshButton {get {if (_NoThreadsRefreshButton == null) {_NoThreadsRefreshButton = (InputElement)Document.GetElementById(clientId + "_NoThreadsRefreshButton");}; return _NoThreadsRefreshButton;}} private InputElement _NoThreadsRefreshButton;
		public jQueryObject NoThreadsRefreshButtonJ {get {if (_NoThreadsRefreshButtonJ == null) {_NoThreadsRefreshButtonJ = jQuery.Select("#" + clientId + "_NoThreadsRefreshButton");}; return _NoThreadsRefreshButtonJ;}} private jQueryObject _NoThreadsRefreshButtonJ;
		public DivElement ThreadsPanel {get {if (_ThreadsPanel == null) {_ThreadsPanel = (DivElement)Document.GetElementById(clientId + "_ThreadsPanel");}; return _ThreadsPanel;}} private DivElement _ThreadsPanel;
		public jQueryObject ThreadsPanelJ {get {if (_ThreadsPanelJ == null) {_ThreadsPanelJ = jQuery.Select("#" + clientId + "_ThreadsPanel");}; return _ThreadsPanelJ;}} private jQueryObject _ThreadsPanelJ;
		public Element ThreadsPageLinksP {get {if (_ThreadsPageLinksP == null) {_ThreadsPageLinksP = (Element)Document.GetElementById(clientId + "_ThreadsPageLinksP");}; return _ThreadsPageLinksP;}} private Element _ThreadsPageLinksP;
		public jQueryObject ThreadsPageLinksPJ {get {if (_ThreadsPageLinksPJ == null) {_ThreadsPageLinksPJ = jQuery.Select("#" + clientId + "_ThreadsPageLinksP");}; return _ThreadsPageLinksPJ;}} private jQueryObject _ThreadsPageLinksPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ThreadsPageP {get {if (_ThreadsPageP == null) {_ThreadsPageP = (Element)Document.GetElementById(clientId + "_ThreadsPageP");}; return _ThreadsPageP;}} private Element _ThreadsPageP;
		public jQueryObject ThreadsPagePJ {get {if (_ThreadsPagePJ == null) {_ThreadsPagePJ = jQuery.Select("#" + clientId + "_ThreadsPageP");}; return _ThreadsPagePJ;}} private jQueryObject _ThreadsPagePJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement ThreadsPrevPageLink {get {if (_ThreadsPrevPageLink == null) {_ThreadsPrevPageLink = (AnchorElement)Document.GetElementById(clientId + "_ThreadsPrevPageLink");}; return _ThreadsPrevPageLink;}} private AnchorElement _ThreadsPrevPageLink;
		public jQueryObject ThreadsPrevPageLinkJ {get {if (_ThreadsPrevPageLinkJ == null) {_ThreadsPrevPageLinkJ = jQuery.Select("#" + clientId + "_ThreadsPrevPageLink");}; return _ThreadsPrevPageLinkJ;}} private jQueryObject _ThreadsPrevPageLinkJ;
		public AnchorElement ThreadsNextPageLink {get {if (_ThreadsNextPageLink == null) {_ThreadsNextPageLink = (AnchorElement)Document.GetElementById(clientId + "_ThreadsNextPageLink");}; return _ThreadsNextPageLink;}} private AnchorElement _ThreadsNextPageLink;
		public jQueryObject ThreadsNextPageLinkJ {get {if (_ThreadsNextPageLinkJ == null) {_ThreadsNextPageLinkJ = jQuery.Select("#" + clientId + "_ThreadsNextPageLink");}; return _ThreadsNextPageLinkJ;}} private jQueryObject _ThreadsNextPageLinkJ;
		public Element InlineScript3 {get {if (_InlineScript3 == null) {_InlineScript3 = (Element)Document.GetElementById(clientId + "_InlineScript3");}; return _InlineScript3;}} private Element _InlineScript3;
		public jQueryObject InlineScript3J {get {if (_InlineScript3J == null) {_InlineScript3J = jQuery.Select("#" + clientId + "_InlineScript3");}; return _InlineScript3J;}} private jQueryObject _InlineScript3J;//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
		public Element ThreadsDataGrid {get {if (_ThreadsDataGrid == null) {_ThreadsDataGrid = (Element)Document.GetElementById(clientId + "_ThreadsDataGrid");}; return _ThreadsDataGrid;}} private Element _ThreadsDataGrid;
		public jQueryObject ThreadsDataGridJ {get {if (_ThreadsDataGridJ == null) {_ThreadsDataGridJ = jQuery.Select("#" + clientId + "_ThreadsDataGrid");}; return _ThreadsDataGridJ;}} private jQueryObject _ThreadsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public InputElement RefreshButton {get {if (_RefreshButton == null) {_RefreshButton = (InputElement)Document.GetElementById(clientId + "_RefreshButton");}; return _RefreshButton;}} private InputElement _RefreshButton;
		public jQueryObject RefreshButtonJ {get {if (_RefreshButtonJ == null) {_RefreshButtonJ = jQuery.Select("#" + clientId + "_RefreshButton");}; return _RefreshButtonJ;}} private jQueryObject _RefreshButtonJ;
		public Element ThreadsPageP1 {get {if (_ThreadsPageP1 == null) {_ThreadsPageP1 = (Element)Document.GetElementById(clientId + "_ThreadsPageP1");}; return _ThreadsPageP1;}} private Element _ThreadsPageP1;
		public jQueryObject ThreadsPageP1J {get {if (_ThreadsPageP1J == null) {_ThreadsPageP1J = jQuery.Select("#" + clientId + "_ThreadsPageP1");}; return _ThreadsPageP1J;}} private jQueryObject _ThreadsPageP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement ThreadsPrevPageLink1 {get {if (_ThreadsPrevPageLink1 == null) {_ThreadsPrevPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_ThreadsPrevPageLink1");}; return _ThreadsPrevPageLink1;}} private AnchorElement _ThreadsPrevPageLink1;
		public jQueryObject ThreadsPrevPageLink1J {get {if (_ThreadsPrevPageLink1J == null) {_ThreadsPrevPageLink1J = jQuery.Select("#" + clientId + "_ThreadsPrevPageLink1");}; return _ThreadsPrevPageLink1J;}} private jQueryObject _ThreadsPrevPageLink1J;
		public AnchorElement ThreadsNextPageLink1 {get {if (_ThreadsNextPageLink1 == null) {_ThreadsNextPageLink1 = (AnchorElement)Document.GetElementById(clientId + "_ThreadsNextPageLink1");}; return _ThreadsNextPageLink1;}} private AnchorElement _ThreadsNextPageLink1;
		public jQueryObject ThreadsNextPageLink1J {get {if (_ThreadsNextPageLink1J == null) {_ThreadsNextPageLink1J = jQuery.Select("#" + clientId + "_ThreadsNextPageLink1");}; return _ThreadsNextPageLink1J;}} private jQueryObject _ThreadsNextPageLink1J;
		public Element ThreadsPageLinksP1 {get {if (_ThreadsPageLinksP1 == null) {_ThreadsPageLinksP1 = (Element)Document.GetElementById(clientId + "_ThreadsPageLinksP1");}; return _ThreadsPageLinksP1;}} private Element _ThreadsPageLinksP1;
		public jQueryObject ThreadsPageLinksP1J {get {if (_ThreadsPageLinksP1J == null) {_ThreadsPageLinksP1J = jQuery.Select("#" + clientId + "_ThreadsPageLinksP1");}; return _ThreadsPageLinksP1J;}} private jQueryObject _ThreadsPageLinksP1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
	}
}
