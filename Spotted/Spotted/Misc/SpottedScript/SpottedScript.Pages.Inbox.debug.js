Type.registerNamespace('SpottedScript.Pages.Inbox');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Inbox.View
SpottedScript.Pages.Inbox.View = function SpottedScript_Pages_Inbox_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Inbox.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Inbox.View.prototype = {
    clientId: null,
    get_panelInbox: function SpottedScript_Pages_Inbox_View$get_panelInbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelInbox');
    },
    get_inboxIconHelp: function SpottedScript_Pages_Inbox_View$get_inboxIconHelp() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxIconHelp');
    },
    get_inboxEmails: function SpottedScript_Pages_Inbox_View$get_inboxEmails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxEmails');
    },
    get_inboxEmailsYes: function SpottedScript_Pages_Inbox_View$get_inboxEmailsYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxEmailsYes');
    },
    get_inboxEmailsNo: function SpottedScript_Pages_Inbox_View$get_inboxEmailsNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxEmailsNo');
    },
    get_button1: function SpottedScript_Pages_Inbox_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_clearMyInbox: function SpottedScript_Pages_Inbox_View$get_clearMyInbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClearMyInbox');
    },
    get_password: function SpottedScript_Pages_Inbox_View$get_password() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password');
    },
    get_button2: function SpottedScript_Pages_Inbox_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_error: function SpottedScript_Pages_Inbox_View$get_error() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Error');
    },
    get_h12: function SpottedScript_Pages_Inbox_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_inlineScript1: function SpottedScript_Pages_Inbox_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript1');
    },
    get_addThread: function SpottedScript_Pages_Inbox_View$get_addThread() {
        /// <value type="SpottedScript.Controls.AddThread.Controller"></value>
        return eval(this.clientId + '_AddThreadController');
    },
    get_inboxControl: function SpottedScript_Pages_Inbox_View$get_inboxControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxControl');
    },
    get_genericContainerPage: function SpottedScript_Pages_Inbox_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Inbox.View.registerClass('SpottedScript.Pages.Inbox.View', SpottedScript.DsiUserControl.View);
