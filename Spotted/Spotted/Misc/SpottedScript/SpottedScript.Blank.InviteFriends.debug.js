Type.registerNamespace('SpottedScript.Blank.InviteFriends');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.InviteFriends.View
SpottedScript.Blank.InviteFriends.View = function SpottedScript_Blank_InviteFriends_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.InviteFriends.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.InviteFriends.View.prototype = {
    clientId: null,
    get_uiSkipButton: function SpottedScript_Blank_InviteFriends_View$get_uiSkipButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSkipButton');
    },
    get_uiGoToSiteButton: function SpottedScript_Blank_InviteFriends_View$get_uiGoToSiteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGoToSiteButton');
    },
    get_uiBuddyImporterDiv: function SpottedScript_Blank_InviteFriends_View$get_uiBuddyImporterDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyImporterDiv');
    },
    get_uiBuddyImporter: function SpottedScript_Blank_InviteFriends_View$get_uiBuddyImporter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyImporter');
    },
    get_uiFinishedPanel: function SpottedScript_Blank_InviteFriends_View$get_uiFinishedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFinishedPanel');
    },
    get_genericContainerPage: function SpottedScript_Blank_InviteFriends_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.InviteFriends.View.registerClass('SpottedScript.Blank.InviteFriends.View', SpottedScript.BlankUserControl.View);
