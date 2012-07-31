//mappings.Add("System.Web.UI.HtmlControls.HtmlContainerControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.LatestChat
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element ExternalHeader {get {if (_ExternalHeader == null) {_ExternalHeader = (Element)Document.GetElementById(clientId + "_ExternalHeader");}; return _ExternalHeader;}} private Element _ExternalHeader;
		public jQueryObject ExternalHeaderJ {get {if (_ExternalHeaderJ == null) {_ExternalHeaderJ = jQuery.Select("#" + clientId + "_ExternalHeader");}; return _ExternalHeaderJ;}} private jQueryObject _ExternalHeaderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlContainerControl", ElementGetter("Element"));
		public DivElement ExternalHolder {get {if (_ExternalHolder == null) {_ExternalHolder = (DivElement)Document.GetElementById(clientId + "_ExternalHolder");}; return _ExternalHolder;}} private DivElement _ExternalHolder;
		public jQueryObject ExternalHolderJ {get {if (_ExternalHolderJ == null) {_ExternalHolderJ = jQuery.Select("#" + clientId + "_ExternalHolder");}; return _ExternalHolderJ;}} private jQueryObject _ExternalHolderJ;
		public DivElement Holder {get {if (_Holder == null) {_Holder = (DivElement)Document.GetElementById(clientId + "_Holder");}; return _Holder;}} private DivElement _Holder;
		public jQueryObject HolderJ {get {if (_HolderJ == null) {_HolderJ = jQuery.Select("#" + clientId + "_Holder");}; return _HolderJ;}} private jQueryObject _HolderJ;
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element InnerHolder {get {if (_InnerHolder == null) {_InnerHolder = (Element)Document.GetElementById(clientId + "_InnerHolder");}; return _InnerHolder;}} private Element _InnerHolder;
		public jQueryObject InnerHolderJ {get {if (_InnerHolderJ == null) {_InnerHolderJ = jQuery.Select("#" + clientId + "_InnerHolder");}; return _InnerHolderJ;}} private jQueryObject _InnerHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement ThreadsNoPermissionPanel {get {if (_ThreadsNoPermissionPanel == null) {_ThreadsNoPermissionPanel = (DivElement)Document.GetElementById(clientId + "_ThreadsNoPermissionPanel");}; return _ThreadsNoPermissionPanel;}} private DivElement _ThreadsNoPermissionPanel;
		public jQueryObject ThreadsNoPermissionPanelJ {get {if (_ThreadsNoPermissionPanelJ == null) {_ThreadsNoPermissionPanelJ = jQuery.Select("#" + clientId + "_ThreadsNoPermissionPanel");}; return _ThreadsNoPermissionPanelJ;}} private jQueryObject _ThreadsNoPermissionPanelJ;
		public AnchorElement ThreadsNoPermissionJoinAnchor {get {if (_ThreadsNoPermissionJoinAnchor == null) {_ThreadsNoPermissionJoinAnchor = (AnchorElement)Document.GetElementById(clientId + "_ThreadsNoPermissionJoinAnchor");}; return _ThreadsNoPermissionJoinAnchor;}} private AnchorElement _ThreadsNoPermissionJoinAnchor;
		public jQueryObject ThreadsNoPermissionJoinAnchorJ {get {if (_ThreadsNoPermissionJoinAnchorJ == null) {_ThreadsNoPermissionJoinAnchorJ = jQuery.Select("#" + clientId + "_ThreadsNoPermissionJoinAnchor");}; return _ThreadsNoPermissionJoinAnchorJ;}} private jQueryObject _ThreadsNoPermissionJoinAnchorJ;
		public DivElement ThreadsPanel {get {if (_ThreadsPanel == null) {_ThreadsPanel = (DivElement)Document.GetElementById(clientId + "_ThreadsPanel");}; return _ThreadsPanel;}} private DivElement _ThreadsPanel;
		public jQueryObject ThreadsPanelJ {get {if (_ThreadsPanelJ == null) {_ThreadsPanelJ = jQuery.Select("#" + clientId + "_ThreadsPanel");}; return _ThreadsPanelJ;}} private jQueryObject _ThreadsPanelJ;
		public Element BrandChatControlsP {get {if (_BrandChatControlsP == null) {_BrandChatControlsP = (Element)Document.GetElementById(clientId + "_BrandChatControlsP");}; return _BrandChatControlsP;}} private Element _BrandChatControlsP;
		public jQueryObject BrandChatControlsPJ {get {if (_BrandChatControlsPJ == null) {_BrandChatControlsPJ = jQuery.Select("#" + clientId + "_BrandChatControlsP");}; return _BrandChatControlsPJ;}} private jQueryObject _BrandChatControlsPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ShowGroupChatEnabled {get {if (_ShowGroupChatEnabled == null) {_ShowGroupChatEnabled = (Element)Document.GetElementById(clientId + "_ShowGroupChatEnabled");}; return _ShowGroupChatEnabled;}} private Element _ShowGroupChatEnabled;
		public jQueryObject ShowGroupChatEnabledJ {get {if (_ShowGroupChatEnabledJ == null) {_ShowGroupChatEnabledJ = jQuery.Select("#" + clientId + "_ShowGroupChatEnabled");}; return _ShowGroupChatEnabledJ;}} private jQueryObject _ShowGroupChatEnabledJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element ShowGroupChatLinkButton {get {if (_ShowGroupChatLinkButton == null) {_ShowGroupChatLinkButton = (Element)Document.GetElementById(clientId + "_ShowGroupChatLinkButton");}; return _ShowGroupChatLinkButton;}} private Element _ShowGroupChatLinkButton;
		public jQueryObject ShowGroupChatLinkButtonJ {get {if (_ShowGroupChatLinkButtonJ == null) {_ShowGroupChatLinkButtonJ = jQuery.Select("#" + clientId + "_ShowGroupChatLinkButton");}; return _ShowGroupChatLinkButtonJ;}} private jQueryObject _ShowGroupChatLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element ShowBrandChatLinkButton {get {if (_ShowBrandChatLinkButton == null) {_ShowBrandChatLinkButton = (Element)Document.GetElementById(clientId + "_ShowBrandChatLinkButton");}; return _ShowBrandChatLinkButton;}} private Element _ShowBrandChatLinkButton;
		public jQueryObject ShowBrandChatLinkButtonJ {get {if (_ShowBrandChatLinkButtonJ == null) {_ShowBrandChatLinkButtonJ = jQuery.Select("#" + clientId + "_ShowBrandChatLinkButton");}; return _ShowBrandChatLinkButtonJ;}} private jQueryObject _ShowBrandChatLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element ShowBrandChatEnabled {get {if (_ShowBrandChatEnabled == null) {_ShowBrandChatEnabled = (Element)Document.GetElementById(clientId + "_ShowBrandChatEnabled");}; return _ShowBrandChatEnabled;}} private Element _ShowBrandChatEnabled;
		public jQueryObject ShowBrandChatEnabledJ {get {if (_ShowBrandChatEnabledJ == null) {_ShowBrandChatEnabledJ = jQuery.Select("#" + clientId + "_ShowBrandChatEnabled");}; return _ShowBrandChatEnabledJ;}} private jQueryObject _ShowBrandChatEnabledJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element InlineScript1 {get {if (_InlineScript1 == null) {_InlineScript1 = (Element)Document.GetElementById(clientId + "_InlineScript1");}; return _InlineScript1;}} private Element _InlineScript1;
		public jQueryObject InlineScript1J {get {if (_InlineScript1J == null) {_InlineScript1J = jQuery.Select("#" + clientId + "_InlineScript1");}; return _InlineScript1J;}} private jQueryObject _InlineScript1J;//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
		public Element ThreadsDataGrid {get {if (_ThreadsDataGrid == null) {_ThreadsDataGrid = (Element)Document.GetElementById(clientId + "_ThreadsDataGrid");}; return _ThreadsDataGrid;}} private Element _ThreadsDataGrid;
		public jQueryObject ThreadsDataGridJ {get {if (_ThreadsDataGridJ == null) {_ThreadsDataGridJ = jQuery.Select("#" + clientId + "_ThreadsDataGrid");}; return _ThreadsDataGridJ;}} private jQueryObject _ThreadsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element CommentsFooter {get {if (_CommentsFooter == null) {_CommentsFooter = (Element)Document.GetElementById(clientId + "_CommentsFooter");}; return _CommentsFooter;}} private Element _CommentsFooter;
		public jQueryObject CommentsFooterJ {get {if (_CommentsFooterJ == null) {_CommentsFooterJ = jQuery.Select("#" + clientId + "_CommentsFooter");}; return _CommentsFooterJ;}} private jQueryObject _CommentsFooterJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement MoreThreadsAnchor {get {if (_MoreThreadsAnchor == null) {_MoreThreadsAnchor = (AnchorElement)Document.GetElementById(clientId + "_MoreThreadsAnchor");}; return _MoreThreadsAnchor;}} private AnchorElement _MoreThreadsAnchor;
		public jQueryObject MoreThreadsAnchorJ {get {if (_MoreThreadsAnchorJ == null) {_MoreThreadsAnchorJ = jQuery.Select("#" + clientId + "_MoreThreadsAnchor");}; return _MoreThreadsAnchorJ;}} private jQueryObject _MoreThreadsAnchorJ;
		public Element MoreThreadsCountLabel {get {if (_MoreThreadsCountLabel == null) {_MoreThreadsCountLabel = (Element)Document.GetElementById(clientId + "_MoreThreadsCountLabel");}; return _MoreThreadsCountLabel;}} private Element _MoreThreadsCountLabel;
		public jQueryObject MoreThreadsCountLabelJ {get {if (_MoreThreadsCountLabelJ == null) {_MoreThreadsCountLabelJ = jQuery.Select("#" + clientId + "_MoreThreadsCountLabel");}; return _MoreThreadsCountLabelJ;}} private jQueryObject _MoreThreadsCountLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public InputElement uiObjectType {get {if (_uiObjectType == null) {_uiObjectType = (InputElement)Document.GetElementById(clientId + "_uiObjectType");}; return _uiObjectType;}} private InputElement _uiObjectType;
		public jQueryObject uiObjectTypeJ {get {if (_uiObjectTypeJ == null) {_uiObjectTypeJ = jQuery.Select("#" + clientId + "_uiObjectType");}; return _uiObjectTypeJ;}} private jQueryObject _uiObjectTypeJ;
		public InputElement uiObjectK {get {if (_uiObjectK == null) {_uiObjectK = (InputElement)Document.GetElementById(clientId + "_uiObjectK");}; return _uiObjectK;}} private InputElement _uiObjectK;
		public jQueryObject uiObjectKJ {get {if (_uiObjectKJ == null) {_uiObjectKJ = jQuery.Select("#" + clientId + "_uiObjectK");}; return _uiObjectKJ;}} private jQueryObject _uiObjectKJ;
		public InputElement uiThreadsCount {get {if (_uiThreadsCount == null) {_uiThreadsCount = (InputElement)Document.GetElementById(clientId + "_uiThreadsCount");}; return _uiThreadsCount;}} private InputElement _uiThreadsCount;
		public jQueryObject uiThreadsCountJ {get {if (_uiThreadsCountJ == null) {_uiThreadsCountJ = jQuery.Select("#" + clientId + "_uiThreadsCount");}; return _uiThreadsCountJ;}} private jQueryObject _uiThreadsCountJ;
		public InputElement uiHasGroupObjectFilter {get {if (_uiHasGroupObjectFilter == null) {_uiHasGroupObjectFilter = (InputElement)Document.GetElementById(clientId + "_uiHasGroupObjectFilter");}; return _uiHasGroupObjectFilter;}} private InputElement _uiHasGroupObjectFilter;
		public jQueryObject uiHasGroupObjectFilterJ {get {if (_uiHasGroupObjectFilterJ == null) {_uiHasGroupObjectFilterJ = jQuery.Select("#" + clientId + "_uiHasGroupObjectFilter");}; return _uiHasGroupObjectFilterJ;}} private jQueryObject _uiHasGroupObjectFilterJ;
	}
}
