//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.AddThread
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public DivElement AddThreadNotGroupMemberPanel {get {if (_AddThreadNotGroupMemberPanel == null) {_AddThreadNotGroupMemberPanel = (DivElement)Document.GetElementById(clientId + "_AddThreadNotGroupMemberPanel");}; return _AddThreadNotGroupMemberPanel;}} private DivElement _AddThreadNotGroupMemberPanel;
		public jQueryObject AddThreadNotGroupMemberPanelJ {get {if (_AddThreadNotGroupMemberPanelJ == null) {_AddThreadNotGroupMemberPanelJ = jQuery.Select("#" + clientId + "_AddThreadNotGroupMemberPanel");}; return _AddThreadNotGroupMemberPanelJ;}} private jQueryObject _AddThreadNotGroupMemberPanelJ;
		public AnchorElement AddThreadNotGroupMemberGroupPageAnchor {get {if (_AddThreadNotGroupMemberGroupPageAnchor == null) {_AddThreadNotGroupMemberGroupPageAnchor = (AnchorElement)Document.GetElementById(clientId + "_AddThreadNotGroupMemberGroupPageAnchor");}; return _AddThreadNotGroupMemberGroupPageAnchor;}} private AnchorElement _AddThreadNotGroupMemberGroupPageAnchor;
		public jQueryObject AddThreadNotGroupMemberGroupPageAnchorJ {get {if (_AddThreadNotGroupMemberGroupPageAnchorJ == null) {_AddThreadNotGroupMemberGroupPageAnchorJ = jQuery.Select("#" + clientId + "_AddThreadNotGroupMemberGroupPageAnchor");}; return _AddThreadNotGroupMemberGroupPageAnchorJ;}} private jQueryObject _AddThreadNotGroupMemberGroupPageAnchorJ;
		public DivElement AddThreadLoginPanel {get {if (_AddThreadLoginPanel == null) {_AddThreadLoginPanel = (DivElement)Document.GetElementById(clientId + "_AddThreadLoginPanel");}; return _AddThreadLoginPanel;}} private DivElement _AddThreadLoginPanel;
		public jQueryObject AddThreadLoginPanelJ {get {if (_AddThreadLoginPanelJ == null) {_AddThreadLoginPanelJ = jQuery.Select("#" + clientId + "_AddThreadLoginPanel");}; return _AddThreadLoginPanelJ;}} private jQueryObject _AddThreadLoginPanelJ;
		public DivElement AddThreadEmailVerifyPanel {get {if (_AddThreadEmailVerifyPanel == null) {_AddThreadEmailVerifyPanel = (DivElement)Document.GetElementById(clientId + "_AddThreadEmailVerifyPanel");}; return _AddThreadEmailVerifyPanel;}} private DivElement _AddThreadEmailVerifyPanel;
		public jQueryObject AddThreadEmailVerifyPanelJ {get {if (_AddThreadEmailVerifyPanelJ == null) {_AddThreadEmailVerifyPanelJ = jQuery.Select("#" + clientId + "_AddThreadEmailVerifyPanel");}; return _AddThreadEmailVerifyPanelJ;}} private jQueryObject _AddThreadEmailVerifyPanelJ;
		public Element Requiredfieldvalidator1 {get {if (_Requiredfieldvalidator1 == null) {_Requiredfieldvalidator1 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator1");}; return _Requiredfieldvalidator1;}} private Element _Requiredfieldvalidator1;
		public jQueryObject Requiredfieldvalidator1J {get {if (_Requiredfieldvalidator1J == null) {_Requiredfieldvalidator1J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator1");}; return _Requiredfieldvalidator1J;}} private jQueryObject _Requiredfieldvalidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element Requiredfieldvalidator2 {get {if (_Requiredfieldvalidator2 == null) {_Requiredfieldvalidator2 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator2");}; return _Requiredfieldvalidator2;}} private Element _Requiredfieldvalidator2;
		public jQueryObject Requiredfieldvalidator2J {get {if (_Requiredfieldvalidator2J == null) {_Requiredfieldvalidator2J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator2");}; return _Requiredfieldvalidator2J;}} private jQueryObject _Requiredfieldvalidator2J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public DivElement AddThreadAdvancedPanel {get {if (_AddThreadAdvancedPanel == null) {_AddThreadAdvancedPanel = (DivElement)Document.GetElementById(clientId + "_AddThreadAdvancedPanel");}; return _AddThreadAdvancedPanel;}} private DivElement _AddThreadAdvancedPanel;
		public jQueryObject AddThreadAdvancedPanelJ {get {if (_AddThreadAdvancedPanelJ == null) {_AddThreadAdvancedPanelJ = jQuery.Select("#" + clientId + "_AddThreadAdvancedPanel");}; return _AddThreadAdvancedPanelJ;}} private jQueryObject _AddThreadAdvancedPanelJ;
		public Element AddThreadPublicRadioButtonSpan {get {if (_AddThreadPublicRadioButtonSpan == null) {_AddThreadPublicRadioButtonSpan = (Element)Document.GetElementById(clientId + "_AddThreadPublicRadioButtonSpan");}; return _AddThreadPublicRadioButtonSpan;}} private Element _AddThreadPublicRadioButtonSpan;
		public jQueryObject AddThreadPublicRadioButtonSpanJ {get {if (_AddThreadPublicRadioButtonSpanJ == null) {_AddThreadPublicRadioButtonSpanJ = jQuery.Select("#" + clientId + "_AddThreadPublicRadioButtonSpan");}; return _AddThreadPublicRadioButtonSpanJ;}} private jQueryObject _AddThreadPublicRadioButtonSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element AddThreadPrivateRadioButtonSpan {get {if (_AddThreadPrivateRadioButtonSpan == null) {_AddThreadPrivateRadioButtonSpan = (Element)Document.GetElementById(clientId + "_AddThreadPrivateRadioButtonSpan");}; return _AddThreadPrivateRadioButtonSpan;}} private Element _AddThreadPrivateRadioButtonSpan;
		public jQueryObject AddThreadPrivateRadioButtonSpanJ {get {if (_AddThreadPrivateRadioButtonSpanJ == null) {_AddThreadPrivateRadioButtonSpanJ = jQuery.Select("#" + clientId + "_AddThreadPrivateRadioButtonSpan");}; return _AddThreadPrivateRadioButtonSpanJ;}} private jQueryObject _AddThreadPrivateRadioButtonSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element AddThreadGroupRadioButtonSpan {get {if (_AddThreadGroupRadioButtonSpan == null) {_AddThreadGroupRadioButtonSpan = (Element)Document.GetElementById(clientId + "_AddThreadGroupRadioButtonSpan");}; return _AddThreadGroupRadioButtonSpan;}} private Element _AddThreadGroupRadioButtonSpan;
		public jQueryObject AddThreadGroupRadioButtonSpanJ {get {if (_AddThreadGroupRadioButtonSpanJ == null) {_AddThreadGroupRadioButtonSpanJ = jQuery.Select("#" + clientId + "_AddThreadGroupRadioButtonSpan");}; return _AddThreadGroupRadioButtonSpanJ;}} private jQueryObject _AddThreadGroupRadioButtonSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public SelectElement AddThreadGroupDropDown {get {if (_AddThreadGroupDropDown == null) {_AddThreadGroupDropDown = (SelectElement)Document.GetElementById(clientId + "_AddThreadGroupDropDown");}; return _AddThreadGroupDropDown;}} private SelectElement _AddThreadGroupDropDown;
		public jQueryObject AddThreadGroupDropDownJ {get {if (_AddThreadGroupDropDownJ == null) {_AddThreadGroupDropDownJ = jQuery.Select("#" + clientId + "_AddThreadGroupDropDown");}; return _AddThreadGroupDropDownJ;}} private jQueryObject _AddThreadGroupDropDownJ;
		public Element AddThreadGroupPrivateCheckBoxSpan {get {if (_AddThreadGroupPrivateCheckBoxSpan == null) {_AddThreadGroupPrivateCheckBoxSpan = (Element)Document.GetElementById(clientId + "_AddThreadGroupPrivateCheckBoxSpan");}; return _AddThreadGroupPrivateCheckBoxSpan;}} private Element _AddThreadGroupPrivateCheckBoxSpan;
		public jQueryObject AddThreadGroupPrivateCheckBoxSpanJ {get {if (_AddThreadGroupPrivateCheckBoxSpanJ == null) {_AddThreadGroupPrivateCheckBoxSpanJ = jQuery.Select("#" + clientId + "_AddThreadGroupPrivateCheckBoxSpan");}; return _AddThreadGroupPrivateCheckBoxSpanJ;}} private jQueryObject _AddThreadGroupPrivateCheckBoxSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element AddThreadNewsCheckBoxSpan {get {if (_AddThreadNewsCheckBoxSpan == null) {_AddThreadNewsCheckBoxSpan = (Element)Document.GetElementById(clientId + "_AddThreadNewsCheckBoxSpan");}; return _AddThreadNewsCheckBoxSpan;}} private Element _AddThreadNewsCheckBoxSpan;
		public jQueryObject AddThreadNewsCheckBoxSpanJ {get {if (_AddThreadNewsCheckBoxSpanJ == null) {_AddThreadNewsCheckBoxSpanJ = jQuery.Select("#" + clientId + "_AddThreadNewsCheckBoxSpan");}; return _AddThreadNewsCheckBoxSpanJ;}} private jQueryObject _AddThreadNewsCheckBoxSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element AddThreadEventCheckBoxSpan {get {if (_AddThreadEventCheckBoxSpan == null) {_AddThreadEventCheckBoxSpan = (Element)Document.GetElementById(clientId + "_AddThreadEventCheckBoxSpan");}; return _AddThreadEventCheckBoxSpan;}} private Element _AddThreadEventCheckBoxSpan;
		public jQueryObject AddThreadEventCheckBoxSpanJ {get {if (_AddThreadEventCheckBoxSpanJ == null) {_AddThreadEventCheckBoxSpanJ = jQuery.Select("#" + clientId + "_AddThreadEventCheckBoxSpan");}; return _AddThreadEventCheckBoxSpanJ;}} private jQueryObject _AddThreadEventCheckBoxSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public SelectElement AddThreadEventDropDown {get {if (_AddThreadEventDropDown == null) {_AddThreadEventDropDown = (SelectElement)Document.GetElementById(clientId + "_AddThreadEventDropDown");}; return _AddThreadEventDropDown;}} private SelectElement _AddThreadEventDropDown;
		public jQueryObject AddThreadEventDropDownJ {get {if (_AddThreadEventDropDownJ == null) {_AddThreadEventDropDownJ = jQuery.Select("#" + clientId + "_AddThreadEventDropDown");}; return _AddThreadEventDropDownJ;}} private jQueryObject _AddThreadEventDropDownJ;
		public Element AddThreadSealedCheckBoxSpan {get {if (_AddThreadSealedCheckBoxSpan == null) {_AddThreadSealedCheckBoxSpan = (Element)Document.GetElementById(clientId + "_AddThreadSealedCheckBoxSpan");}; return _AddThreadSealedCheckBoxSpan;}} private Element _AddThreadSealedCheckBoxSpan;
		public jQueryObject AddThreadSealedCheckBoxSpanJ {get {if (_AddThreadSealedCheckBoxSpanJ == null) {_AddThreadSealedCheckBoxSpanJ = jQuery.Select("#" + clientId + "_AddThreadSealedCheckBoxSpan");}; return _AddThreadSealedCheckBoxSpanJ;}} private jQueryObject _AddThreadSealedCheckBoxSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element AddThreadInviteCheckBoxSpan {get {if (_AddThreadInviteCheckBoxSpan == null) {_AddThreadInviteCheckBoxSpan = (Element)Document.GetElementById(clientId + "_AddThreadInviteCheckBoxSpan");}; return _AddThreadInviteCheckBoxSpan;}} private Element _AddThreadInviteCheckBoxSpan;
		public jQueryObject AddThreadInviteCheckBoxSpanJ {get {if (_AddThreadInviteCheckBoxSpanJ == null) {_AddThreadInviteCheckBoxSpanJ = jQuery.Select("#" + clientId + "_AddThreadInviteCheckBoxSpan");}; return _AddThreadInviteCheckBoxSpanJ;}} private jQueryObject _AddThreadInviteCheckBoxSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public DivElement AddThreadInvitePanel {get {if (_AddThreadInvitePanel == null) {_AddThreadInvitePanel = (DivElement)Document.GetElementById(clientId + "_AddThreadInvitePanel");}; return _AddThreadInvitePanel;}} private DivElement _AddThreadInvitePanel;
		public jQueryObject AddThreadInvitePanelJ {get {if (_AddThreadInvitePanelJ == null) {_AddThreadInvitePanelJ = jQuery.Select("#" + clientId + "_AddThreadInvitePanel");}; return _AddThreadInvitePanelJ;}} private jQueryObject _AddThreadInvitePanelJ;
		public Js.Controls.MultiBuddyChooser.Controller uiMultiBuddyChooser {get {return (Js.Controls.MultiBuddyChooser.Controller) Script.Eval(clientId + "_uiMultiBuddyChooserController");}}
		public Element InlineScript1 {get {if (_InlineScript1 == null) {_InlineScript1 = (Element)Document.GetElementById(clientId + "_InlineScript1");}; return _InlineScript1;}} private Element _InlineScript1;
		public jQueryObject InlineScript1J {get {if (_InlineScript1J == null) {_InlineScript1J = jQuery.Select("#" + clientId + "_InlineScript1");}; return _InlineScript1J;}} private jQueryObject _InlineScript1J;//mappings.Add("Spotted.CustomControls.InlineScript", ElementGetter("Element"));
		public InputElement AddThreadSubjectTextBox {get {if (_AddThreadSubjectTextBox == null) {_AddThreadSubjectTextBox = (InputElement)Document.GetElementById(clientId + "_AddThreadSubjectTextBox");}; return _AddThreadSubjectTextBox;}} private InputElement _AddThreadSubjectTextBox;
		public jQueryObject AddThreadSubjectTextBoxJ {get {if (_AddThreadSubjectTextBoxJ == null) {_AddThreadSubjectTextBoxJ = jQuery.Select("#" + clientId + "_AddThreadSubjectTextBox");}; return _AddThreadSubjectTextBoxJ;}} private jQueryObject _AddThreadSubjectTextBoxJ;
		public Js.Controls.Html.Controller CommentHtml {get {return (Js.Controls.Html.Controller) Script.Eval(clientId + "_CommentHtmlController");}}
		public CheckBoxElement AddThreadPublicRadioButton {get {if (_AddThreadPublicRadioButton == null) {_AddThreadPublicRadioButton = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadPublicRadioButton");}; return _AddThreadPublicRadioButton;}} private CheckBoxElement _AddThreadPublicRadioButton;
		public jQueryObject AddThreadPublicRadioButtonJ {get {if (_AddThreadPublicRadioButtonJ == null) {_AddThreadPublicRadioButtonJ = jQuery.Select("#" + clientId + "_AddThreadPublicRadioButton");}; return _AddThreadPublicRadioButtonJ;}} private jQueryObject _AddThreadPublicRadioButtonJ;
		public CheckBoxElement AddThreadPrivateRadioButton {get {if (_AddThreadPrivateRadioButton == null) {_AddThreadPrivateRadioButton = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadPrivateRadioButton");}; return _AddThreadPrivateRadioButton;}} private CheckBoxElement _AddThreadPrivateRadioButton;
		public jQueryObject AddThreadPrivateRadioButtonJ {get {if (_AddThreadPrivateRadioButtonJ == null) {_AddThreadPrivateRadioButtonJ = jQuery.Select("#" + clientId + "_AddThreadPrivateRadioButton");}; return _AddThreadPrivateRadioButtonJ;}} private jQueryObject _AddThreadPrivateRadioButtonJ;
		public CheckBoxElement AddThreadGroupRadioButton {get {if (_AddThreadGroupRadioButton == null) {_AddThreadGroupRadioButton = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadGroupRadioButton");}; return _AddThreadGroupRadioButton;}} private CheckBoxElement _AddThreadGroupRadioButton;
		public jQueryObject AddThreadGroupRadioButtonJ {get {if (_AddThreadGroupRadioButtonJ == null) {_AddThreadGroupRadioButtonJ = jQuery.Select("#" + clientId + "_AddThreadGroupRadioButton");}; return _AddThreadGroupRadioButtonJ;}} private jQueryObject _AddThreadGroupRadioButtonJ;
		public CheckBoxElement AddThreadAdvancedCheckBox {get {if (_AddThreadAdvancedCheckBox == null) {_AddThreadAdvancedCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadAdvancedCheckBox");}; return _AddThreadAdvancedCheckBox;}} private CheckBoxElement _AddThreadAdvancedCheckBox;
		public jQueryObject AddThreadAdvancedCheckBoxJ {get {if (_AddThreadAdvancedCheckBoxJ == null) {_AddThreadAdvancedCheckBoxJ = jQuery.Select("#" + clientId + "_AddThreadAdvancedCheckBox");}; return _AddThreadAdvancedCheckBoxJ;}} private jQueryObject _AddThreadAdvancedCheckBoxJ;
		public CheckBoxElement AddThreadGroupPrivateCheckBox {get {if (_AddThreadGroupPrivateCheckBox == null) {_AddThreadGroupPrivateCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadGroupPrivateCheckBox");}; return _AddThreadGroupPrivateCheckBox;}} private CheckBoxElement _AddThreadGroupPrivateCheckBox;
		public jQueryObject AddThreadGroupPrivateCheckBoxJ {get {if (_AddThreadGroupPrivateCheckBoxJ == null) {_AddThreadGroupPrivateCheckBoxJ = jQuery.Select("#" + clientId + "_AddThreadGroupPrivateCheckBox");}; return _AddThreadGroupPrivateCheckBoxJ;}} private jQueryObject _AddThreadGroupPrivateCheckBoxJ;
		public CheckBoxElement AddThreadEventCheckBox {get {if (_AddThreadEventCheckBox == null) {_AddThreadEventCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadEventCheckBox");}; return _AddThreadEventCheckBox;}} private CheckBoxElement _AddThreadEventCheckBox;
		public jQueryObject AddThreadEventCheckBoxJ {get {if (_AddThreadEventCheckBoxJ == null) {_AddThreadEventCheckBoxJ = jQuery.Select("#" + clientId + "_AddThreadEventCheckBox");}; return _AddThreadEventCheckBoxJ;}} private jQueryObject _AddThreadEventCheckBoxJ;
		public CheckBoxElement AddThreadSealedCheckBox {get {if (_AddThreadSealedCheckBox == null) {_AddThreadSealedCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadSealedCheckBox");}; return _AddThreadSealedCheckBox;}} private CheckBoxElement _AddThreadSealedCheckBox;
		public jQueryObject AddThreadSealedCheckBoxJ {get {if (_AddThreadSealedCheckBoxJ == null) {_AddThreadSealedCheckBoxJ = jQuery.Select("#" + clientId + "_AddThreadSealedCheckBox");}; return _AddThreadSealedCheckBoxJ;}} private jQueryObject _AddThreadSealedCheckBoxJ;
		public CheckBoxElement AddThreadNewsCheckBox {get {if (_AddThreadNewsCheckBox == null) {_AddThreadNewsCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadNewsCheckBox");}; return _AddThreadNewsCheckBox;}} private CheckBoxElement _AddThreadNewsCheckBox;
		public jQueryObject AddThreadNewsCheckBoxJ {get {if (_AddThreadNewsCheckBoxJ == null) {_AddThreadNewsCheckBoxJ = jQuery.Select("#" + clientId + "_AddThreadNewsCheckBox");}; return _AddThreadNewsCheckBoxJ;}} private jQueryObject _AddThreadNewsCheckBoxJ;
		public CheckBoxElement AddThreadInviteCheckBox {get {if (_AddThreadInviteCheckBox == null) {_AddThreadInviteCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_AddThreadInviteCheckBox");}; return _AddThreadInviteCheckBox;}} private CheckBoxElement _AddThreadInviteCheckBox;
		public jQueryObject AddThreadInviteCheckBoxJ {get {if (_AddThreadInviteCheckBoxJ == null) {_AddThreadInviteCheckBoxJ = jQuery.Select("#" + clientId + "_AddThreadInviteCheckBox");}; return _AddThreadInviteCheckBoxJ;}} private jQueryObject _AddThreadInviteCheckBoxJ;
	}
}
