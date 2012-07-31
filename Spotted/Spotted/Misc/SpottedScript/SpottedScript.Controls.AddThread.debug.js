Type.registerNamespace('SpottedScript.Controls.AddThread');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.AddThread.Controller
SpottedScript.Controls.AddThread.Controller = function SpottedScript_Controls_AddThread_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.AddThread.View">
    /// </param>
    $addHandler(view.get_addThreadAdvancedCheckBox(), 'click', Function.createDelegate(this, function(e) {
        WhenLoggedIn(Function.createDelegate(this, function() {
            view.get_addThreadAdvancedPanel().style.display = (view.get_addThreadAdvancedCheckBox().checked) ? '' : 'none';
        }));
    }));
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.AddThread.View
SpottedScript.Controls.AddThread.View = function SpottedScript_Controls_AddThread_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.AddThread.View.prototype = {
    clientId: null,
    get_addThreadNotGroupMemberPanel: function SpottedScript_Controls_AddThread_View$get_addThreadNotGroupMemberPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadNotGroupMemberPanel');
    },
    get_addThreadNotGroupMemberGroupPageAnchor: function SpottedScript_Controls_AddThread_View$get_addThreadNotGroupMemberGroupPageAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadNotGroupMemberGroupPageAnchor');
    },
    get_addThreadLoginPanel: function SpottedScript_Controls_AddThread_View$get_addThreadLoginPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadLoginPanel');
    },
    get_addThreadEmailVerifyPanel: function SpottedScript_Controls_AddThread_View$get_addThreadEmailVerifyPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadEmailVerifyPanel');
    },
    get_requiredfieldvalidator1: function SpottedScript_Controls_AddThread_View$get_requiredfieldvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator1');
    },
    get_requiredfieldvalidator2: function SpottedScript_Controls_AddThread_View$get_requiredfieldvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator2');
    },
    get_addThreadAdvancedPanel: function SpottedScript_Controls_AddThread_View$get_addThreadAdvancedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadAdvancedPanel');
    },
    get_addThreadPublicRadioButtonSpan: function SpottedScript_Controls_AddThread_View$get_addThreadPublicRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPublicRadioButtonSpan');
    },
    get_addThreadPrivateRadioButtonSpan: function SpottedScript_Controls_AddThread_View$get_addThreadPrivateRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPrivateRadioButtonSpan');
    },
    get_addThreadGroupRadioButtonSpan: function SpottedScript_Controls_AddThread_View$get_addThreadGroupRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupRadioButtonSpan');
    },
    get_addThreadGroupDropDown: function SpottedScript_Controls_AddThread_View$get_addThreadGroupDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupDropDown');
    },
    get_addThreadGroupPrivateCheckBoxSpan: function SpottedScript_Controls_AddThread_View$get_addThreadGroupPrivateCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupPrivateCheckBoxSpan');
    },
    get_addThreadNewsCheckBoxSpan: function SpottedScript_Controls_AddThread_View$get_addThreadNewsCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadNewsCheckBoxSpan');
    },
    get_addThreadEventCheckBoxSpan: function SpottedScript_Controls_AddThread_View$get_addThreadEventCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadEventCheckBoxSpan');
    },
    get_addThreadEventDropDown: function SpottedScript_Controls_AddThread_View$get_addThreadEventDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadEventDropDown');
    },
    get_addThreadSealedCheckBoxSpan: function SpottedScript_Controls_AddThread_View$get_addThreadSealedCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadSealedCheckBoxSpan');
    },
    get_addThreadInviteCheckBoxSpan: function SpottedScript_Controls_AddThread_View$get_addThreadInviteCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadInviteCheckBoxSpan');
    },
    get_addThreadInvitePanel: function SpottedScript_Controls_AddThread_View$get_addThreadInvitePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadInvitePanel');
    },
    get_uiMultiBuddyChooser: function SpottedScript_Controls_AddThread_View$get_uiMultiBuddyChooser() {
        /// <value type="SpottedScript.Controls.MultiBuddyChooser.Controller"></value>
        return eval(this.clientId + '_uiMultiBuddyChooserController');
    },
    get_inlineScript1: function SpottedScript_Controls_AddThread_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript1');
    },
    get_addThreadSubjectTextBox: function SpottedScript_Controls_AddThread_View$get_addThreadSubjectTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadSubjectTextBox');
    },
    get_commentHtml: function SpottedScript_Controls_AddThread_View$get_commentHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_CommentHtmlController');
    },
    get_addThreadPublicRadioButton: function SpottedScript_Controls_AddThread_View$get_addThreadPublicRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPublicRadioButton');
    },
    get_addThreadPrivateRadioButton: function SpottedScript_Controls_AddThread_View$get_addThreadPrivateRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPrivateRadioButton');
    },
    get_addThreadGroupRadioButton: function SpottedScript_Controls_AddThread_View$get_addThreadGroupRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupRadioButton');
    },
    get_addThreadAdvancedCheckBox: function SpottedScript_Controls_AddThread_View$get_addThreadAdvancedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadAdvancedCheckBox');
    },
    get_addThreadGroupPrivateCheckBox: function SpottedScript_Controls_AddThread_View$get_addThreadGroupPrivateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupPrivateCheckBox');
    },
    get_addThreadEventCheckBox: function SpottedScript_Controls_AddThread_View$get_addThreadEventCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadEventCheckBox');
    },
    get_addThreadSealedCheckBox: function SpottedScript_Controls_AddThread_View$get_addThreadSealedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadSealedCheckBox');
    },
    get_addThreadNewsCheckBox: function SpottedScript_Controls_AddThread_View$get_addThreadNewsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadNewsCheckBox');
    },
    get_addThreadInviteCheckBox: function SpottedScript_Controls_AddThread_View$get_addThreadInviteCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadInviteCheckBox');
    }
}
SpottedScript.Controls.AddThread.Controller.registerClass('SpottedScript.Controls.AddThread.Controller');
SpottedScript.Controls.AddThread.View.registerClass('SpottedScript.Controls.AddThread.View');
