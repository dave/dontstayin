Type.registerNamespace('SpottedScript.Pages.Usrs.BuddyRequests');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.BuddyRequests.View
SpottedScript.Pages.Usrs.BuddyRequests.View = function SpottedScript_Pages_Usrs_BuddyRequests_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.BuddyRequests.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.BuddyRequests.View.prototype = {
    clientId: null,
    get_h18: function SpottedScript_Pages_Usrs_BuddyRequests_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_uiNoBuddyRequestsPanel: function SpottedScript_Pages_Usrs_BuddyRequests_View$get_uiNoBuddyRequestsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNoBuddyRequestsPanel');
    },
    get_uiBuddyRequestsPanel: function SpottedScript_Pages_Usrs_BuddyRequests_View$get_uiBuddyRequestsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyRequestsPanel');
    },
    get_uiMultiButtonsPanel: function SpottedScript_Pages_Usrs_BuddyRequests_View$get_uiMultiButtonsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMultiButtonsPanel');
    },
    get_uiBuddiesRequested: function SpottedScript_Pages_Usrs_BuddyRequests_View$get_uiBuddiesRequested() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddiesRequested');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_BuddyRequests_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.BuddyRequests.View.registerClass('SpottedScript.Pages.Usrs.BuddyRequests.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
