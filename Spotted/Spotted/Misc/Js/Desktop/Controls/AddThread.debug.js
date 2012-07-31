//! AddThread.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.AddThread');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.AddThread.Controller

Js.Controls.AddThread.Controller = function Js_Controls_AddThread_Controller(view) {
    /// <param name="view" type="Js.Controls.AddThread.View">
    /// </param>
    view.get_addThreadAdvancedCheckBoxJ().click(function(e) {
        WhenLoggedIn(function() {
            view.get_addThreadAdvancedPanel().style.display = (view.get_addThreadAdvancedCheckBox().checked) ? '' : 'none';
        });
    });
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.AddThread.View

Js.Controls.AddThread.View = function Js_Controls_AddThread_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_AddThreadNotGroupMemberPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadNotGroupMemberPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadNotGroupMemberGroupPageAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadNotGroupMemberGroupPageAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadLoginPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadLoginPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadEmailVerifyPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadEmailVerifyPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Requiredfieldvalidator1" type="Object" domElement="true">
    /// </field>
    /// <field name="_Requiredfieldvalidator1J" type="jQueryObject">
    /// </field>
    /// <field name="_Requiredfieldvalidator2" type="Object" domElement="true">
    /// </field>
    /// <field name="_Requiredfieldvalidator2J" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadAdvancedPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadAdvancedPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadPublicRadioButtonSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadPublicRadioButtonSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadPrivateRadioButtonSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadPrivateRadioButtonSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupRadioButtonSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupRadioButtonSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupPrivateCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupPrivateCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadNewsCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadNewsCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadEventCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadEventCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadEventDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadEventDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadSealedCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadSealedCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadInviteCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadInviteCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadInvitePanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadInvitePanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_InlineScript1" type="Object" domElement="true">
    /// </field>
    /// <field name="_InlineScript1J" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadSubjectTextBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadSubjectTextBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadPublicRadioButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadPublicRadioButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadPrivateRadioButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadPrivateRadioButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupRadioButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupRadioButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadAdvancedCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadAdvancedCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupPrivateCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupPrivateCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadEventCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadEventCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadSealedCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadSealedCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadNewsCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadNewsCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadInviteCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadInviteCheckBoxJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.AddThread.View.prototype = {
    clientId: null,
    
    get_addThreadNotGroupMemberPanel: function Js_Controls_AddThread_View$get_addThreadNotGroupMemberPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadNotGroupMemberPanel == null) {
            this._AddThreadNotGroupMemberPanel = document.getElementById(this.clientId + '_AddThreadNotGroupMemberPanel');
        }
        return this._AddThreadNotGroupMemberPanel;
    },
    
    _AddThreadNotGroupMemberPanel: null,
    
    get_addThreadNotGroupMemberPanelJ: function Js_Controls_AddThread_View$get_addThreadNotGroupMemberPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadNotGroupMemberPanelJ == null) {
            this._AddThreadNotGroupMemberPanelJ = $('#' + this.clientId + '_AddThreadNotGroupMemberPanel');
        }
        return this._AddThreadNotGroupMemberPanelJ;
    },
    
    _AddThreadNotGroupMemberPanelJ: null,
    
    get_addThreadNotGroupMemberGroupPageAnchor: function Js_Controls_AddThread_View$get_addThreadNotGroupMemberGroupPageAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadNotGroupMemberGroupPageAnchor == null) {
            this._AddThreadNotGroupMemberGroupPageAnchor = document.getElementById(this.clientId + '_AddThreadNotGroupMemberGroupPageAnchor');
        }
        return this._AddThreadNotGroupMemberGroupPageAnchor;
    },
    
    _AddThreadNotGroupMemberGroupPageAnchor: null,
    
    get_addThreadNotGroupMemberGroupPageAnchorJ: function Js_Controls_AddThread_View$get_addThreadNotGroupMemberGroupPageAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadNotGroupMemberGroupPageAnchorJ == null) {
            this._AddThreadNotGroupMemberGroupPageAnchorJ = $('#' + this.clientId + '_AddThreadNotGroupMemberGroupPageAnchor');
        }
        return this._AddThreadNotGroupMemberGroupPageAnchorJ;
    },
    
    _AddThreadNotGroupMemberGroupPageAnchorJ: null,
    
    get_addThreadLoginPanel: function Js_Controls_AddThread_View$get_addThreadLoginPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadLoginPanel == null) {
            this._AddThreadLoginPanel = document.getElementById(this.clientId + '_AddThreadLoginPanel');
        }
        return this._AddThreadLoginPanel;
    },
    
    _AddThreadLoginPanel: null,
    
    get_addThreadLoginPanelJ: function Js_Controls_AddThread_View$get_addThreadLoginPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadLoginPanelJ == null) {
            this._AddThreadLoginPanelJ = $('#' + this.clientId + '_AddThreadLoginPanel');
        }
        return this._AddThreadLoginPanelJ;
    },
    
    _AddThreadLoginPanelJ: null,
    
    get_addThreadEmailVerifyPanel: function Js_Controls_AddThread_View$get_addThreadEmailVerifyPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadEmailVerifyPanel == null) {
            this._AddThreadEmailVerifyPanel = document.getElementById(this.clientId + '_AddThreadEmailVerifyPanel');
        }
        return this._AddThreadEmailVerifyPanel;
    },
    
    _AddThreadEmailVerifyPanel: null,
    
    get_addThreadEmailVerifyPanelJ: function Js_Controls_AddThread_View$get_addThreadEmailVerifyPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadEmailVerifyPanelJ == null) {
            this._AddThreadEmailVerifyPanelJ = $('#' + this.clientId + '_AddThreadEmailVerifyPanel');
        }
        return this._AddThreadEmailVerifyPanelJ;
    },
    
    _AddThreadEmailVerifyPanelJ: null,
    
    get_requiredfieldvalidator1: function Js_Controls_AddThread_View$get_requiredfieldvalidator1() {
        /// <value type="Object" domElement="true"></value>
        if (this._Requiredfieldvalidator1 == null) {
            this._Requiredfieldvalidator1 = document.getElementById(this.clientId + '_Requiredfieldvalidator1');
        }
        return this._Requiredfieldvalidator1;
    },
    
    _Requiredfieldvalidator1: null,
    
    get_requiredfieldvalidator1J: function Js_Controls_AddThread_View$get_requiredfieldvalidator1J() {
        /// <value type="jQueryObject"></value>
        if (this._Requiredfieldvalidator1J == null) {
            this._Requiredfieldvalidator1J = $('#' + this.clientId + '_Requiredfieldvalidator1');
        }
        return this._Requiredfieldvalidator1J;
    },
    
    _Requiredfieldvalidator1J: null,
    
    get_requiredfieldvalidator2: function Js_Controls_AddThread_View$get_requiredfieldvalidator2() {
        /// <value type="Object" domElement="true"></value>
        if (this._Requiredfieldvalidator2 == null) {
            this._Requiredfieldvalidator2 = document.getElementById(this.clientId + '_Requiredfieldvalidator2');
        }
        return this._Requiredfieldvalidator2;
    },
    
    _Requiredfieldvalidator2: null,
    
    get_requiredfieldvalidator2J: function Js_Controls_AddThread_View$get_requiredfieldvalidator2J() {
        /// <value type="jQueryObject"></value>
        if (this._Requiredfieldvalidator2J == null) {
            this._Requiredfieldvalidator2J = $('#' + this.clientId + '_Requiredfieldvalidator2');
        }
        return this._Requiredfieldvalidator2J;
    },
    
    _Requiredfieldvalidator2J: null,
    
    get_addThreadAdvancedPanel: function Js_Controls_AddThread_View$get_addThreadAdvancedPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadAdvancedPanel == null) {
            this._AddThreadAdvancedPanel = document.getElementById(this.clientId + '_AddThreadAdvancedPanel');
        }
        return this._AddThreadAdvancedPanel;
    },
    
    _AddThreadAdvancedPanel: null,
    
    get_addThreadAdvancedPanelJ: function Js_Controls_AddThread_View$get_addThreadAdvancedPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadAdvancedPanelJ == null) {
            this._AddThreadAdvancedPanelJ = $('#' + this.clientId + '_AddThreadAdvancedPanel');
        }
        return this._AddThreadAdvancedPanelJ;
    },
    
    _AddThreadAdvancedPanelJ: null,
    
    get_addThreadPublicRadioButtonSpan: function Js_Controls_AddThread_View$get_addThreadPublicRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadPublicRadioButtonSpan == null) {
            this._AddThreadPublicRadioButtonSpan = document.getElementById(this.clientId + '_AddThreadPublicRadioButtonSpan');
        }
        return this._AddThreadPublicRadioButtonSpan;
    },
    
    _AddThreadPublicRadioButtonSpan: null,
    
    get_addThreadPublicRadioButtonSpanJ: function Js_Controls_AddThread_View$get_addThreadPublicRadioButtonSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadPublicRadioButtonSpanJ == null) {
            this._AddThreadPublicRadioButtonSpanJ = $('#' + this.clientId + '_AddThreadPublicRadioButtonSpan');
        }
        return this._AddThreadPublicRadioButtonSpanJ;
    },
    
    _AddThreadPublicRadioButtonSpanJ: null,
    
    get_addThreadPrivateRadioButtonSpan: function Js_Controls_AddThread_View$get_addThreadPrivateRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadPrivateRadioButtonSpan == null) {
            this._AddThreadPrivateRadioButtonSpan = document.getElementById(this.clientId + '_AddThreadPrivateRadioButtonSpan');
        }
        return this._AddThreadPrivateRadioButtonSpan;
    },
    
    _AddThreadPrivateRadioButtonSpan: null,
    
    get_addThreadPrivateRadioButtonSpanJ: function Js_Controls_AddThread_View$get_addThreadPrivateRadioButtonSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadPrivateRadioButtonSpanJ == null) {
            this._AddThreadPrivateRadioButtonSpanJ = $('#' + this.clientId + '_AddThreadPrivateRadioButtonSpan');
        }
        return this._AddThreadPrivateRadioButtonSpanJ;
    },
    
    _AddThreadPrivateRadioButtonSpanJ: null,
    
    get_addThreadGroupRadioButtonSpan: function Js_Controls_AddThread_View$get_addThreadGroupRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupRadioButtonSpan == null) {
            this._AddThreadGroupRadioButtonSpan = document.getElementById(this.clientId + '_AddThreadGroupRadioButtonSpan');
        }
        return this._AddThreadGroupRadioButtonSpan;
    },
    
    _AddThreadGroupRadioButtonSpan: null,
    
    get_addThreadGroupRadioButtonSpanJ: function Js_Controls_AddThread_View$get_addThreadGroupRadioButtonSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupRadioButtonSpanJ == null) {
            this._AddThreadGroupRadioButtonSpanJ = $('#' + this.clientId + '_AddThreadGroupRadioButtonSpan');
        }
        return this._AddThreadGroupRadioButtonSpanJ;
    },
    
    _AddThreadGroupRadioButtonSpanJ: null,
    
    get_addThreadGroupDropDown: function Js_Controls_AddThread_View$get_addThreadGroupDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupDropDown == null) {
            this._AddThreadGroupDropDown = document.getElementById(this.clientId + '_AddThreadGroupDropDown');
        }
        return this._AddThreadGroupDropDown;
    },
    
    _AddThreadGroupDropDown: null,
    
    get_addThreadGroupDropDownJ: function Js_Controls_AddThread_View$get_addThreadGroupDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupDropDownJ == null) {
            this._AddThreadGroupDropDownJ = $('#' + this.clientId + '_AddThreadGroupDropDown');
        }
        return this._AddThreadGroupDropDownJ;
    },
    
    _AddThreadGroupDropDownJ: null,
    
    get_addThreadGroupPrivateCheckBoxSpan: function Js_Controls_AddThread_View$get_addThreadGroupPrivateCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupPrivateCheckBoxSpan == null) {
            this._AddThreadGroupPrivateCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadGroupPrivateCheckBoxSpan');
        }
        return this._AddThreadGroupPrivateCheckBoxSpan;
    },
    
    _AddThreadGroupPrivateCheckBoxSpan: null,
    
    get_addThreadGroupPrivateCheckBoxSpanJ: function Js_Controls_AddThread_View$get_addThreadGroupPrivateCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupPrivateCheckBoxSpanJ == null) {
            this._AddThreadGroupPrivateCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadGroupPrivateCheckBoxSpan');
        }
        return this._AddThreadGroupPrivateCheckBoxSpanJ;
    },
    
    _AddThreadGroupPrivateCheckBoxSpanJ: null,
    
    get_addThreadNewsCheckBoxSpan: function Js_Controls_AddThread_View$get_addThreadNewsCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadNewsCheckBoxSpan == null) {
            this._AddThreadNewsCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadNewsCheckBoxSpan');
        }
        return this._AddThreadNewsCheckBoxSpan;
    },
    
    _AddThreadNewsCheckBoxSpan: null,
    
    get_addThreadNewsCheckBoxSpanJ: function Js_Controls_AddThread_View$get_addThreadNewsCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadNewsCheckBoxSpanJ == null) {
            this._AddThreadNewsCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadNewsCheckBoxSpan');
        }
        return this._AddThreadNewsCheckBoxSpanJ;
    },
    
    _AddThreadNewsCheckBoxSpanJ: null,
    
    get_addThreadEventCheckBoxSpan: function Js_Controls_AddThread_View$get_addThreadEventCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadEventCheckBoxSpan == null) {
            this._AddThreadEventCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadEventCheckBoxSpan');
        }
        return this._AddThreadEventCheckBoxSpan;
    },
    
    _AddThreadEventCheckBoxSpan: null,
    
    get_addThreadEventCheckBoxSpanJ: function Js_Controls_AddThread_View$get_addThreadEventCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadEventCheckBoxSpanJ == null) {
            this._AddThreadEventCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadEventCheckBoxSpan');
        }
        return this._AddThreadEventCheckBoxSpanJ;
    },
    
    _AddThreadEventCheckBoxSpanJ: null,
    
    get_addThreadEventDropDown: function Js_Controls_AddThread_View$get_addThreadEventDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadEventDropDown == null) {
            this._AddThreadEventDropDown = document.getElementById(this.clientId + '_AddThreadEventDropDown');
        }
        return this._AddThreadEventDropDown;
    },
    
    _AddThreadEventDropDown: null,
    
    get_addThreadEventDropDownJ: function Js_Controls_AddThread_View$get_addThreadEventDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadEventDropDownJ == null) {
            this._AddThreadEventDropDownJ = $('#' + this.clientId + '_AddThreadEventDropDown');
        }
        return this._AddThreadEventDropDownJ;
    },
    
    _AddThreadEventDropDownJ: null,
    
    get_addThreadSealedCheckBoxSpan: function Js_Controls_AddThread_View$get_addThreadSealedCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadSealedCheckBoxSpan == null) {
            this._AddThreadSealedCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadSealedCheckBoxSpan');
        }
        return this._AddThreadSealedCheckBoxSpan;
    },
    
    _AddThreadSealedCheckBoxSpan: null,
    
    get_addThreadSealedCheckBoxSpanJ: function Js_Controls_AddThread_View$get_addThreadSealedCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadSealedCheckBoxSpanJ == null) {
            this._AddThreadSealedCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadSealedCheckBoxSpan');
        }
        return this._AddThreadSealedCheckBoxSpanJ;
    },
    
    _AddThreadSealedCheckBoxSpanJ: null,
    
    get_addThreadInviteCheckBoxSpan: function Js_Controls_AddThread_View$get_addThreadInviteCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadInviteCheckBoxSpan == null) {
            this._AddThreadInviteCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadInviteCheckBoxSpan');
        }
        return this._AddThreadInviteCheckBoxSpan;
    },
    
    _AddThreadInviteCheckBoxSpan: null,
    
    get_addThreadInviteCheckBoxSpanJ: function Js_Controls_AddThread_View$get_addThreadInviteCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadInviteCheckBoxSpanJ == null) {
            this._AddThreadInviteCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadInviteCheckBoxSpan');
        }
        return this._AddThreadInviteCheckBoxSpanJ;
    },
    
    _AddThreadInviteCheckBoxSpanJ: null,
    
    get_addThreadInvitePanel: function Js_Controls_AddThread_View$get_addThreadInvitePanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadInvitePanel == null) {
            this._AddThreadInvitePanel = document.getElementById(this.clientId + '_AddThreadInvitePanel');
        }
        return this._AddThreadInvitePanel;
    },
    
    _AddThreadInvitePanel: null,
    
    get_addThreadInvitePanelJ: function Js_Controls_AddThread_View$get_addThreadInvitePanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadInvitePanelJ == null) {
            this._AddThreadInvitePanelJ = $('#' + this.clientId + '_AddThreadInvitePanel');
        }
        return this._AddThreadInvitePanelJ;
    },
    
    _AddThreadInvitePanelJ: null,
    
    get_uiMultiBuddyChooser: function Js_Controls_AddThread_View$get_uiMultiBuddyChooser() {
        /// <value type="Js.Controls.MultiBuddyChooser.Controller"></value>
        return eval(this.clientId + '_uiMultiBuddyChooserController');
    },
    
    get_inlineScript1: function Js_Controls_AddThread_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        if (this._InlineScript1 == null) {
            this._InlineScript1 = document.getElementById(this.clientId + '_InlineScript1');
        }
        return this._InlineScript1;
    },
    
    _InlineScript1: null,
    
    get_inlineScript1J: function Js_Controls_AddThread_View$get_inlineScript1J() {
        /// <value type="jQueryObject"></value>
        if (this._InlineScript1J == null) {
            this._InlineScript1J = $('#' + this.clientId + '_InlineScript1');
        }
        return this._InlineScript1J;
    },
    
    _InlineScript1J: null,
    
    get_addThreadSubjectTextBox: function Js_Controls_AddThread_View$get_addThreadSubjectTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadSubjectTextBox == null) {
            this._AddThreadSubjectTextBox = document.getElementById(this.clientId + '_AddThreadSubjectTextBox');
        }
        return this._AddThreadSubjectTextBox;
    },
    
    _AddThreadSubjectTextBox: null,
    
    get_addThreadSubjectTextBoxJ: function Js_Controls_AddThread_View$get_addThreadSubjectTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadSubjectTextBoxJ == null) {
            this._AddThreadSubjectTextBoxJ = $('#' + this.clientId + '_AddThreadSubjectTextBox');
        }
        return this._AddThreadSubjectTextBoxJ;
    },
    
    _AddThreadSubjectTextBoxJ: null,
    
    get_commentHtml: function Js_Controls_AddThread_View$get_commentHtml() {
        /// <value type="Js.Controls.Html.Controller"></value>
        return eval(this.clientId + '_CommentHtmlController');
    },
    
    get_addThreadPublicRadioButton: function Js_Controls_AddThread_View$get_addThreadPublicRadioButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadPublicRadioButton == null) {
            this._AddThreadPublicRadioButton = document.getElementById(this.clientId + '_AddThreadPublicRadioButton');
        }
        return this._AddThreadPublicRadioButton;
    },
    
    _AddThreadPublicRadioButton: null,
    
    get_addThreadPublicRadioButtonJ: function Js_Controls_AddThread_View$get_addThreadPublicRadioButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadPublicRadioButtonJ == null) {
            this._AddThreadPublicRadioButtonJ = $('#' + this.clientId + '_AddThreadPublicRadioButton');
        }
        return this._AddThreadPublicRadioButtonJ;
    },
    
    _AddThreadPublicRadioButtonJ: null,
    
    get_addThreadPrivateRadioButton: function Js_Controls_AddThread_View$get_addThreadPrivateRadioButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadPrivateRadioButton == null) {
            this._AddThreadPrivateRadioButton = document.getElementById(this.clientId + '_AddThreadPrivateRadioButton');
        }
        return this._AddThreadPrivateRadioButton;
    },
    
    _AddThreadPrivateRadioButton: null,
    
    get_addThreadPrivateRadioButtonJ: function Js_Controls_AddThread_View$get_addThreadPrivateRadioButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadPrivateRadioButtonJ == null) {
            this._AddThreadPrivateRadioButtonJ = $('#' + this.clientId + '_AddThreadPrivateRadioButton');
        }
        return this._AddThreadPrivateRadioButtonJ;
    },
    
    _AddThreadPrivateRadioButtonJ: null,
    
    get_addThreadGroupRadioButton: function Js_Controls_AddThread_View$get_addThreadGroupRadioButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupRadioButton == null) {
            this._AddThreadGroupRadioButton = document.getElementById(this.clientId + '_AddThreadGroupRadioButton');
        }
        return this._AddThreadGroupRadioButton;
    },
    
    _AddThreadGroupRadioButton: null,
    
    get_addThreadGroupRadioButtonJ: function Js_Controls_AddThread_View$get_addThreadGroupRadioButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupRadioButtonJ == null) {
            this._AddThreadGroupRadioButtonJ = $('#' + this.clientId + '_AddThreadGroupRadioButton');
        }
        return this._AddThreadGroupRadioButtonJ;
    },
    
    _AddThreadGroupRadioButtonJ: null,
    
    get_addThreadAdvancedCheckBox: function Js_Controls_AddThread_View$get_addThreadAdvancedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadAdvancedCheckBox == null) {
            this._AddThreadAdvancedCheckBox = document.getElementById(this.clientId + '_AddThreadAdvancedCheckBox');
        }
        return this._AddThreadAdvancedCheckBox;
    },
    
    _AddThreadAdvancedCheckBox: null,
    
    get_addThreadAdvancedCheckBoxJ: function Js_Controls_AddThread_View$get_addThreadAdvancedCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadAdvancedCheckBoxJ == null) {
            this._AddThreadAdvancedCheckBoxJ = $('#' + this.clientId + '_AddThreadAdvancedCheckBox');
        }
        return this._AddThreadAdvancedCheckBoxJ;
    },
    
    _AddThreadAdvancedCheckBoxJ: null,
    
    get_addThreadGroupPrivateCheckBox: function Js_Controls_AddThread_View$get_addThreadGroupPrivateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupPrivateCheckBox == null) {
            this._AddThreadGroupPrivateCheckBox = document.getElementById(this.clientId + '_AddThreadGroupPrivateCheckBox');
        }
        return this._AddThreadGroupPrivateCheckBox;
    },
    
    _AddThreadGroupPrivateCheckBox: null,
    
    get_addThreadGroupPrivateCheckBoxJ: function Js_Controls_AddThread_View$get_addThreadGroupPrivateCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupPrivateCheckBoxJ == null) {
            this._AddThreadGroupPrivateCheckBoxJ = $('#' + this.clientId + '_AddThreadGroupPrivateCheckBox');
        }
        return this._AddThreadGroupPrivateCheckBoxJ;
    },
    
    _AddThreadGroupPrivateCheckBoxJ: null,
    
    get_addThreadEventCheckBox: function Js_Controls_AddThread_View$get_addThreadEventCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadEventCheckBox == null) {
            this._AddThreadEventCheckBox = document.getElementById(this.clientId + '_AddThreadEventCheckBox');
        }
        return this._AddThreadEventCheckBox;
    },
    
    _AddThreadEventCheckBox: null,
    
    get_addThreadEventCheckBoxJ: function Js_Controls_AddThread_View$get_addThreadEventCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadEventCheckBoxJ == null) {
            this._AddThreadEventCheckBoxJ = $('#' + this.clientId + '_AddThreadEventCheckBox');
        }
        return this._AddThreadEventCheckBoxJ;
    },
    
    _AddThreadEventCheckBoxJ: null,
    
    get_addThreadSealedCheckBox: function Js_Controls_AddThread_View$get_addThreadSealedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadSealedCheckBox == null) {
            this._AddThreadSealedCheckBox = document.getElementById(this.clientId + '_AddThreadSealedCheckBox');
        }
        return this._AddThreadSealedCheckBox;
    },
    
    _AddThreadSealedCheckBox: null,
    
    get_addThreadSealedCheckBoxJ: function Js_Controls_AddThread_View$get_addThreadSealedCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadSealedCheckBoxJ == null) {
            this._AddThreadSealedCheckBoxJ = $('#' + this.clientId + '_AddThreadSealedCheckBox');
        }
        return this._AddThreadSealedCheckBoxJ;
    },
    
    _AddThreadSealedCheckBoxJ: null,
    
    get_addThreadNewsCheckBox: function Js_Controls_AddThread_View$get_addThreadNewsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadNewsCheckBox == null) {
            this._AddThreadNewsCheckBox = document.getElementById(this.clientId + '_AddThreadNewsCheckBox');
        }
        return this._AddThreadNewsCheckBox;
    },
    
    _AddThreadNewsCheckBox: null,
    
    get_addThreadNewsCheckBoxJ: function Js_Controls_AddThread_View$get_addThreadNewsCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadNewsCheckBoxJ == null) {
            this._AddThreadNewsCheckBoxJ = $('#' + this.clientId + '_AddThreadNewsCheckBox');
        }
        return this._AddThreadNewsCheckBoxJ;
    },
    
    _AddThreadNewsCheckBoxJ: null,
    
    get_addThreadInviteCheckBox: function Js_Controls_AddThread_View$get_addThreadInviteCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadInviteCheckBox == null) {
            this._AddThreadInviteCheckBox = document.getElementById(this.clientId + '_AddThreadInviteCheckBox');
        }
        return this._AddThreadInviteCheckBox;
    },
    
    _AddThreadInviteCheckBox: null,
    
    get_addThreadInviteCheckBoxJ: function Js_Controls_AddThread_View$get_addThreadInviteCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadInviteCheckBoxJ == null) {
            this._AddThreadInviteCheckBoxJ = $('#' + this.clientId + '_AddThreadInviteCheckBox');
        }
        return this._AddThreadInviteCheckBoxJ;
    },
    
    _AddThreadInviteCheckBoxJ: null
}


Js.Controls.AddThread.Controller.registerClass('Js.Controls.AddThread.Controller');
Js.Controls.AddThread.View.registerClass('Js.Controls.AddThread.View');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
