Type.registerNamespace('SpottedScript.Pages.Usrs.Invite');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.Invite.View
SpottedScript.Pages.Usrs.Invite.View = function SpottedScript_Pages_Usrs_Invite_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.Invite.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.Invite.View.prototype = {
    clientId: null,
    get_panelInvite: function SpottedScript_Pages_Usrs_Invite_View$get_panelInvite() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelInvite');
    },
    get_header: function SpottedScript_Pages_Usrs_Invite_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_inviteUsrAnchor: function SpottedScript_Pages_Usrs_Invite_View$get_inviteUsrAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InviteUsrAnchor');
    },
    get_groupDropDown: function SpottedScript_Pages_Usrs_Invite_View$get_groupDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupDropDown');
    },
    get_inviteErrorPanel: function SpottedScript_Pages_Usrs_Invite_View$get_inviteErrorPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InviteErrorPanel');
    },
    get_inviteSentPanel: function SpottedScript_Pages_Usrs_Invite_View$get_inviteSentPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InviteSentPanel');
    },
    get_inviteErrorMessage: function SpottedScript_Pages_Usrs_Invite_View$get_inviteErrorMessage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InviteErrorMessage');
    },
    get_inviteSentMessage: function SpottedScript_Pages_Usrs_Invite_View$get_inviteSentMessage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InviteSentMessage');
    },
    get_inviteMessage: function SpottedScript_Pages_Usrs_Invite_View$get_inviteMessage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InviteMessage');
    },
    get_panelNoGroups: function SpottedScript_Pages_Usrs_Invite_View$get_panelNoGroups() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoGroups');
    },
    get_button1: function SpottedScript_Pages_Usrs_Invite_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Usrs_Invite_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_h11: function SpottedScript_Pages_Usrs_Invite_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_Invite_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.Invite.View.registerClass('SpottedScript.Pages.Usrs.Invite.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
