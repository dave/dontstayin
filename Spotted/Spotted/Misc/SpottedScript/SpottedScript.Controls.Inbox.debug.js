Type.registerNamespace('SpottedScript.Controls.Inbox');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Inbox.View
SpottedScript.Controls.Inbox.View = function SpottedScript_Controls_Inbox_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.Inbox.View.prototype = {
    clientId: null,
    get_inboxFilterPanel: function SpottedScript_Controls_Inbox_View$get_inboxFilterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxFilterPanel');
    },
    get_inboxFilterP: function SpottedScript_Controls_Inbox_View$get_inboxFilterP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxFilterP');
    },
    get_filterPanel: function SpottedScript_Controls_Inbox_View$get_filterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FilterPanel');
    },
    get_buddyPostCheckBox: function SpottedScript_Controls_Inbox_View$get_buddyPostCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddyPostCheckBox');
    },
    get_buddyDropDownList: function SpottedScript_Controls_Inbox_View$get_buddyDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddyDropDownList');
    },
    get_groupPostCheckBox: function SpottedScript_Controls_Inbox_View$get_groupPostCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPostCheckBox');
    },
    get_groupDropDownList: function SpottedScript_Controls_Inbox_View$get_groupDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupDropDownList');
    },
    get_noThreadsPanel: function SpottedScript_Controls_Inbox_View$get_noThreadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoThreadsPanel');
    },
    get_noThreadsRefreshButton: function SpottedScript_Controls_Inbox_View$get_noThreadsRefreshButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoThreadsRefreshButton');
    },
    get_threadsPanel: function SpottedScript_Controls_Inbox_View$get_threadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPanel');
    },
    get_threadsPageLinksP: function SpottedScript_Controls_Inbox_View$get_threadsPageLinksP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageLinksP');
    },
    get_threadsPageP: function SpottedScript_Controls_Inbox_View$get_threadsPageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP');
    },
    get_threadsPrevPageLink: function SpottedScript_Controls_Inbox_View$get_threadsPrevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink');
    },
    get_threadsNextPageLink: function SpottedScript_Controls_Inbox_View$get_threadsNextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink');
    },
    get_inlineScript3: function SpottedScript_Controls_Inbox_View$get_inlineScript3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript3');
    },
    get_threadsDataGrid: function SpottedScript_Controls_Inbox_View$get_threadsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsDataGrid');
    },
    get_refreshButton: function SpottedScript_Controls_Inbox_View$get_refreshButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefreshButton');
    },
    get_threadsPageP1: function SpottedScript_Controls_Inbox_View$get_threadsPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP1');
    },
    get_threadsPrevPageLink1: function SpottedScript_Controls_Inbox_View$get_threadsPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink1');
    },
    get_threadsNextPageLink1: function SpottedScript_Controls_Inbox_View$get_threadsNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink1');
    },
    get_threadsPageLinksP1: function SpottedScript_Controls_Inbox_View$get_threadsPageLinksP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageLinksP1');
    }
}
SpottedScript.Controls.Inbox.View.registerClass('SpottedScript.Controls.Inbox.View');
