//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.MyGroups
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
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element InlineScript3 {get {if (_InlineScript3 == null) {_InlineScript3 = (Element)Document.GetElementById(clientId + "_InlineScript3");}; return _InlineScript3;}} private Element _InlineScript3;
		public jQueryObject InlineScript3J {get {if (_InlineScript3J == null) {_InlineScript3J = jQuery.Select("#" + clientId + "_InlineScript3");}; return _InlineScript3J;}} private jQueryObject _InlineScript3J;//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
		public Js.Controls.AddThread.Controller AddThread {get {return (Js.Controls.AddThread.Controller) Script.Eval(clientId + "_AddThreadController");}}
		public Element AddThreadLinkP {get {if (_AddThreadLinkP == null) {_AddThreadLinkP = (Element)Document.GetElementById(clientId + "_AddThreadLinkP");}; return _AddThreadLinkP;}} private Element _AddThreadLinkP;
		public jQueryObject AddThreadLinkPJ {get {if (_AddThreadLinkPJ == null) {_AddThreadLinkPJ = jQuery.Select("#" + clientId + "_AddThreadLinkP");}; return _AddThreadLinkPJ;}} private jQueryObject _AddThreadLinkPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement AddThreadStatusHidden {get {if (_AddThreadStatusHidden == null) {_AddThreadStatusHidden = (InputElement)Document.GetElementById(clientId + "_AddThreadStatusHidden");}; return _AddThreadStatusHidden;}} private InputElement _AddThreadStatusHidden;
		public jQueryObject AddThreadStatusHiddenJ {get {if (_AddThreadStatusHiddenJ == null) {_AddThreadStatusHiddenJ = jQuery.Select("#" + clientId + "_AddThreadStatusHidden");}; return _AddThreadStatusHiddenJ;}} private jQueryObject _AddThreadStatusHiddenJ;
		public DivElement AddThreadPanel {get {if (_AddThreadPanel == null) {_AddThreadPanel = (DivElement)Document.GetElementById(clientId + "_AddThreadPanel");}; return _AddThreadPanel;}} private DivElement _AddThreadPanel;
		public jQueryObject AddThreadPanelJ {get {if (_AddThreadPanelJ == null) {_AddThreadPanelJ = jQuery.Select("#" + clientId + "_AddThreadPanel");}; return _AddThreadPanelJ;}} private jQueryObject _AddThreadPanelJ;
		public DivElement GroupsPanel {get {if (_GroupsPanel == null) {_GroupsPanel = (DivElement)Document.GetElementById(clientId + "_GroupsPanel");}; return _GroupsPanel;}} private DivElement _GroupsPanel;
		public jQueryObject GroupsPanelJ {get {if (_GroupsPanelJ == null) {_GroupsPanelJ = jQuery.Select("#" + clientId + "_GroupsPanel");}; return _GroupsPanelJ;}} private jQueryObject _GroupsPanelJ;
		public Element GroupsDataGrid {get {if (_GroupsDataGrid == null) {_GroupsDataGrid = (Element)Document.GetElementById(clientId + "_GroupsDataGrid");}; return _GroupsDataGrid;}} private Element _GroupsDataGrid;
		public jQueryObject GroupsDataGridJ {get {if (_GroupsDataGridJ == null) {_GroupsDataGridJ = jQuery.Select("#" + clientId + "_GroupsDataGrid");}; return _GroupsDataGridJ;}} private jQueryObject _GroupsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public DivElement PanelGroupsList {get {if (_PanelGroupsList == null) {_PanelGroupsList = (DivElement)Document.GetElementById(clientId + "_PanelGroupsList");}; return _PanelGroupsList;}} private DivElement _PanelGroupsList;
		public jQueryObject PanelGroupsListJ {get {if (_PanelGroupsListJ == null) {_PanelGroupsListJ = jQuery.Select("#" + clientId + "_PanelGroupsList");}; return _PanelGroupsListJ;}} private jQueryObject _PanelGroupsListJ;
		public DivElement PanelNoGroups {get {if (_PanelNoGroups == null) {_PanelNoGroups = (DivElement)Document.GetElementById(clientId + "_PanelNoGroups");}; return _PanelNoGroups;}} private DivElement _PanelNoGroups;
		public jQueryObject PanelNoGroupsJ {get {if (_PanelNoGroupsJ == null) {_PanelNoGroupsJ = jQuery.Select("#" + clientId + "_PanelNoGroups");}; return _PanelNoGroupsJ;}} private jQueryObject _PanelNoGroupsJ;
		public DivElement PanelInvites {get {if (_PanelInvites == null) {_PanelInvites = (DivElement)Document.GetElementById(clientId + "_PanelInvites");}; return _PanelInvites;}} private DivElement _PanelInvites;
		public jQueryObject PanelInvitesJ {get {if (_PanelInvitesJ == null) {_PanelInvitesJ = jQuery.Select("#" + clientId + "_PanelInvites");}; return _PanelInvitesJ;}} private jQueryObject _PanelInvitesJ;
		public Element InvitesDataGrid {get {if (_InvitesDataGrid == null) {_InvitesDataGrid = (Element)Document.GetElementById(clientId + "_InvitesDataGrid");}; return _InvitesDataGrid;}} private Element _InvitesDataGrid;
		public jQueryObject InvitesDataGridJ {get {if (_InvitesDataGridJ == null) {_InvitesDataGridJ = jQuery.Select("#" + clientId + "_InvitesDataGrid");}; return _InvitesDataGridJ;}} private jQueryObject _InvitesDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
