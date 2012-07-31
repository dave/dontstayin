//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.Invite
{
	public partial class View
		 : Js.Pages.Usrs.UsrUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement PanelInvite {get {if (_PanelInvite == null) {_PanelInvite = (DivElement)Document.GetElementById(clientId + "_PanelInvite");}; return _PanelInvite;}} private DivElement _PanelInvite;
		public jQueryObject PanelInviteJ {get {if (_PanelInviteJ == null) {_PanelInviteJ = jQuery.Select("#" + clientId + "_PanelInvite");}; return _PanelInviteJ;}} private jQueryObject _PanelInviteJ;
		public Element Header {get {if (_Header == null) {_Header = (Element)Document.GetElementById(clientId + "_Header");}; return _Header;}} private Element _Header;
		public jQueryObject HeaderJ {get {if (_HeaderJ == null) {_HeaderJ = jQuery.Select("#" + clientId + "_Header");}; return _HeaderJ;}} private jQueryObject _HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement InviteUsrAnchor {get {if (_InviteUsrAnchor == null) {_InviteUsrAnchor = (AnchorElement)Document.GetElementById(clientId + "_InviteUsrAnchor");}; return _InviteUsrAnchor;}} private AnchorElement _InviteUsrAnchor;
		public jQueryObject InviteUsrAnchorJ {get {if (_InviteUsrAnchorJ == null) {_InviteUsrAnchorJ = jQuery.Select("#" + clientId + "_InviteUsrAnchor");}; return _InviteUsrAnchorJ;}} private jQueryObject _InviteUsrAnchorJ;
		public SelectElement GroupDropDown {get {if (_GroupDropDown == null) {_GroupDropDown = (SelectElement)Document.GetElementById(clientId + "_GroupDropDown");}; return _GroupDropDown;}} private SelectElement _GroupDropDown;
		public jQueryObject GroupDropDownJ {get {if (_GroupDropDownJ == null) {_GroupDropDownJ = jQuery.Select("#" + clientId + "_GroupDropDown");}; return _GroupDropDownJ;}} private jQueryObject _GroupDropDownJ;
		public DivElement InviteErrorPanel {get {if (_InviteErrorPanel == null) {_InviteErrorPanel = (DivElement)Document.GetElementById(clientId + "_InviteErrorPanel");}; return _InviteErrorPanel;}} private DivElement _InviteErrorPanel;
		public jQueryObject InviteErrorPanelJ {get {if (_InviteErrorPanelJ == null) {_InviteErrorPanelJ = jQuery.Select("#" + clientId + "_InviteErrorPanel");}; return _InviteErrorPanelJ;}} private jQueryObject _InviteErrorPanelJ;
		public DivElement InviteSentPanel {get {if (_InviteSentPanel == null) {_InviteSentPanel = (DivElement)Document.GetElementById(clientId + "_InviteSentPanel");}; return _InviteSentPanel;}} private DivElement _InviteSentPanel;
		public jQueryObject InviteSentPanelJ {get {if (_InviteSentPanelJ == null) {_InviteSentPanelJ = jQuery.Select("#" + clientId + "_InviteSentPanel");}; return _InviteSentPanelJ;}} private jQueryObject _InviteSentPanelJ;
		public Element InviteErrorMessage {get {if (_InviteErrorMessage == null) {_InviteErrorMessage = (Element)Document.GetElementById(clientId + "_InviteErrorMessage");}; return _InviteErrorMessage;}} private Element _InviteErrorMessage;
		public jQueryObject InviteErrorMessageJ {get {if (_InviteErrorMessageJ == null) {_InviteErrorMessageJ = jQuery.Select("#" + clientId + "_InviteErrorMessage");}; return _InviteErrorMessageJ;}} private jQueryObject _InviteErrorMessageJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element InviteSentMessage {get {if (_InviteSentMessage == null) {_InviteSentMessage = (Element)Document.GetElementById(clientId + "_InviteSentMessage");}; return _InviteSentMessage;}} private Element _InviteSentMessage;
		public jQueryObject InviteSentMessageJ {get {if (_InviteSentMessageJ == null) {_InviteSentMessageJ = jQuery.Select("#" + clientId + "_InviteSentMessage");}; return _InviteSentMessageJ;}} private jQueryObject _InviteSentMessageJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement InviteMessage {get {if (_InviteMessage == null) {_InviteMessage = (InputElement)Document.GetElementById(clientId + "_InviteMessage");}; return _InviteMessage;}} private InputElement _InviteMessage;
		public jQueryObject InviteMessageJ {get {if (_InviteMessageJ == null) {_InviteMessageJ = jQuery.Select("#" + clientId + "_InviteMessage");}; return _InviteMessageJ;}} private jQueryObject _InviteMessageJ;
		public DivElement PanelNoGroups {get {if (_PanelNoGroups == null) {_PanelNoGroups = (DivElement)Document.GetElementById(clientId + "_PanelNoGroups");}; return _PanelNoGroups;}} private DivElement _PanelNoGroups;
		public jQueryObject PanelNoGroupsJ {get {if (_PanelNoGroupsJ == null) {_PanelNoGroupsJ = jQuery.Select("#" + clientId + "_PanelNoGroups");}; return _PanelNoGroupsJ;}} private jQueryObject _PanelNoGroupsJ;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
