Type.registerNamespace('SpottedScript.Controls.BuddyImporter');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.BuddyImporter.View
SpottedScript.Controls.BuddyImporter.View = function SpottedScript_Controls_BuddyImporter_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.BuddyImporter.View.prototype = {
    clientId: null,
    get_uiEmailCredentialsPanel: function SpottedScript_Controls_BuddyImporter_View$get_uiEmailCredentialsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEmailCredentialsPanel');
    },
    get_uiEmailText: function SpottedScript_Controls_BuddyImporter_View$get_uiEmailText() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEmailText');
    },
    get_uiEmailProviderDropDown: function SpottedScript_Controls_BuddyImporter_View$get_uiEmailProviderDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEmailProviderDropDown');
    },
    get_uiPasswordText: function SpottedScript_Controls_BuddyImporter_View$get_uiPasswordText() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPasswordText');
    },
    get_uiErrorBadCredentialsLabel: function SpottedScript_Controls_BuddyImporter_View$get_uiErrorBadCredentialsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiErrorBadCredentialsLabel');
    },
    get_uiErrorUnknownEmailProvider: function SpottedScript_Controls_BuddyImporter_View$get_uiErrorUnknownEmailProvider() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiErrorUnknownEmailProvider');
    },
    get_uiGetEmailContactsButton: function SpottedScript_Controls_BuddyImporter_View$get_uiGetEmailContactsButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGetEmailContactsButton');
    },
    get_uiSelectContactsPanel: function SpottedScript_Controls_BuddyImporter_View$get_uiSelectContactsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSelectContactsPanel');
    },
    get_uiAlreadyBuddiesLabel: function SpottedScript_Controls_BuddyImporter_View$get_uiAlreadyBuddiesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAlreadyBuddiesLabel');
    },
    get_uiNonBuddyMembersLabel: function SpottedScript_Controls_BuddyImporter_View$get_uiNonBuddyMembersLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNonBuddyMembersLabel');
    },
    get_uiToggleSelectAllMemberContactsCheckBox: function SpottedScript_Controls_BuddyImporter_View$get_uiToggleSelectAllMemberContactsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiToggleSelectAllMemberContactsCheckBox');
    },
    get_uiSelectMemberContactsDiv: function SpottedScript_Controls_BuddyImporter_View$get_uiSelectMemberContactsDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSelectMemberContactsDiv');
    },
    get_uiSelectMemberContactsGridView: function SpottedScript_Controls_BuddyImporter_View$get_uiSelectMemberContactsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSelectMemberContactsGridView');
    },
    get_uiNonMembersLabel: function SpottedScript_Controls_BuddyImporter_View$get_uiNonMembersLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNonMembersLabel');
    },
    get_uiToggleSelectAllNonMemberContactsCheckBox: function SpottedScript_Controls_BuddyImporter_View$get_uiToggleSelectAllNonMemberContactsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiToggleSelectAllNonMemberContactsCheckBox');
    },
    get_uiSelectNonMemberContactsDiv: function SpottedScript_Controls_BuddyImporter_View$get_uiSelectNonMemberContactsDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSelectNonMemberContactsDiv');
    },
    get_uiSelectNonMemberContactsGridView: function SpottedScript_Controls_BuddyImporter_View$get_uiSelectNonMemberContactsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSelectNonMemberContactsGridView');
    },
    get_uiSuccess: function SpottedScript_Controls_BuddyImporter_View$get_uiSuccess() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSuccess');
    },
    get_uiNoContactsAddedLabel: function SpottedScript_Controls_BuddyImporter_View$get_uiNoContactsAddedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNoContactsAddedLabel');
    },
    get_uiBuddiesRequestedList: function SpottedScript_Controls_BuddyImporter_View$get_uiBuddiesRequestedList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddiesRequestedList');
    },
    get_uiEmailsSentList: function SpottedScript_Controls_BuddyImporter_View$get_uiEmailsSentList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEmailsSentList');
    }
}
SpottedScript.Controls.BuddyImporter.View.registerClass('SpottedScript.Controls.BuddyImporter.View');
