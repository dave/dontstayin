Type.registerNamespace('SpottedScript.Pages.FriendInviter');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.FriendInviter.View
SpottedScript.Pages.FriendInviter.View = function SpottedScript_Pages_FriendInviter_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.FriendInviter.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.FriendInviter.View.prototype = {
    clientId: null,
    get_uiH1: function SpottedScript_Pages_FriendInviter_View$get_uiH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiH1');
    },
    get_uiIntroPanel: function SpottedScript_Pages_FriendInviter_View$get_uiIntroPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiIntroPanel');
    },
    get_uiBuddyImporter: function SpottedScript_Pages_FriendInviter_View$get_uiBuddyImporter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyImporter');
    },
    get_uiSuccessPanel: function SpottedScript_Pages_FriendInviter_View$get_uiSuccessPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSuccessPanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_FriendInviter_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.FriendInviter.View.registerClass('SpottedScript.Pages.FriendInviter.View', SpottedScript.DsiUserControl.View);
