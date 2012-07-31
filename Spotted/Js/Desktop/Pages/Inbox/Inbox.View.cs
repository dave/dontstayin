//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Inbox", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Inbox
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
		public DivElement PanelInbox {get {if (_PanelInbox == null) {_PanelInbox = (DivElement)Document.GetElementById(clientId + "_PanelInbox");}; return _PanelInbox;}} private DivElement _PanelInbox;
		public jQueryObject PanelInboxJ {get {if (_PanelInboxJ == null) {_PanelInboxJ = jQuery.Select("#" + clientId + "_PanelInbox");}; return _PanelInboxJ;}} private jQueryObject _PanelInboxJ;
		public Element InboxIconHelp {get {if (_InboxIconHelp == null) {_InboxIconHelp = (Element)Document.GetElementById(clientId + "_InboxIconHelp");}; return _InboxIconHelp;}} private Element _InboxIconHelp;
		public jQueryObject InboxIconHelpJ {get {if (_InboxIconHelpJ == null) {_InboxIconHelpJ = jQuery.Select("#" + clientId + "_InboxIconHelp");}; return _InboxIconHelpJ;}} private jQueryObject _InboxIconHelpJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element InboxEmails {get {if (_InboxEmails == null) {_InboxEmails = (Element)Document.GetElementById(clientId + "_InboxEmails");}; return _InboxEmails;}} private Element _InboxEmails;
		public jQueryObject InboxEmailsJ {get {if (_InboxEmailsJ == null) {_InboxEmailsJ = jQuery.Select("#" + clientId + "_InboxEmails");}; return _InboxEmailsJ;}} private jQueryObject _InboxEmailsJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement InboxEmailsYes {get {if (_InboxEmailsYes == null) {_InboxEmailsYes = (CheckBoxElement)Document.GetElementById(clientId + "_InboxEmailsYes");}; return _InboxEmailsYes;}} private CheckBoxElement _InboxEmailsYes;
		public jQueryObject InboxEmailsYesJ {get {if (_InboxEmailsYesJ == null) {_InboxEmailsYesJ = jQuery.Select("#" + clientId + "_InboxEmailsYes");}; return _InboxEmailsYesJ;}} private jQueryObject _InboxEmailsYesJ;
		public CheckBoxElement InboxEmailsNo {get {if (_InboxEmailsNo == null) {_InboxEmailsNo = (CheckBoxElement)Document.GetElementById(clientId + "_InboxEmailsNo");}; return _InboxEmailsNo;}} private CheckBoxElement _InboxEmailsNo;
		public jQueryObject InboxEmailsNoJ {get {if (_InboxEmailsNoJ == null) {_InboxEmailsNoJ = jQuery.Select("#" + clientId + "_InboxEmailsNo");}; return _InboxEmailsNoJ;}} private jQueryObject _InboxEmailsNoJ;
		public InputElement Button1 {get {if (_Button1 == null) {_Button1 = (InputElement)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private InputElement _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;
		public Element ClearMyInbox {get {if (_ClearMyInbox == null) {_ClearMyInbox = (Element)Document.GetElementById(clientId + "_ClearMyInbox");}; return _ClearMyInbox;}} private Element _ClearMyInbox;
		public jQueryObject ClearMyInboxJ {get {if (_ClearMyInboxJ == null) {_ClearMyInboxJ = jQuery.Select("#" + clientId + "_ClearMyInbox");}; return _ClearMyInboxJ;}} private jQueryObject _ClearMyInboxJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement Password {get {if (_Password == null) {_Password = (InputElement)Document.GetElementById(clientId + "_Password");}; return _Password;}} private InputElement _Password;
		public jQueryObject PasswordJ {get {if (_PasswordJ == null) {_PasswordJ = jQuery.Select("#" + clientId + "_Password");}; return _PasswordJ;}} private jQueryObject _PasswordJ;
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Error {get {if (_Error == null) {_Error = (Element)Document.GetElementById(clientId + "_Error");}; return _Error;}} private Element _Error;
		public jQueryObject ErrorJ {get {if (_ErrorJ == null) {_ErrorJ = jQuery.Select("#" + clientId + "_Error");}; return _ErrorJ;}} private jQueryObject _ErrorJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element InlineScript1 {get {if (_InlineScript1 == null) {_InlineScript1 = (Element)Document.GetElementById(clientId + "_InlineScript1");}; return _InlineScript1;}} private Element _InlineScript1;
		public jQueryObject InlineScript1J {get {if (_InlineScript1J == null) {_InlineScript1J = jQuery.Select("#" + clientId + "_InlineScript1");}; return _InlineScript1J;}} private jQueryObject _InlineScript1J;//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
		public Js.Controls.AddThread.Controller AddThread {get {return (Js.Controls.AddThread.Controller) Script.Eval(clientId + "_AddThreadController");}}
		public Element InboxControl {get {if (_InboxControl == null) {_InboxControl = (Element)Document.GetElementById(clientId + "_InboxControl");}; return _InboxControl;}} private Element _InboxControl;
		public jQueryObject InboxControlJ {get {if (_InboxControlJ == null) {_InboxControlJ = jQuery.Select("#" + clientId + "_InboxControl");}; return _InboxControlJ;}} private jQueryObject _InboxControlJ;//mappings.Add("Spotted.Controls.Inbox", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
