Type.registerNamespace('SpottedScript.Pages.ClearMyInbox');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.ClearMyInbox.View
SpottedScript.Pages.ClearMyInbox.View = function SpottedScript_Pages_ClearMyInbox_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.ClearMyInbox.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.ClearMyInbox.View.prototype = {
    clientId: null,
    get_confirmDiv: function SpottedScript_Pages_ClearMyInbox_View$get_confirmDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ConfirmDiv');
    },
    get_password: function SpottedScript_Pages_ClearMyInbox_View$get_password() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password');
    },
    get_error: function SpottedScript_Pages_ClearMyInbox_View$get_error() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Error');
    },
    get_done: function SpottedScript_Pages_ClearMyInbox_View$get_done() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Done');
    },
    get_genericContainerPage: function SpottedScript_Pages_ClearMyInbox_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.ClearMyInbox.View.registerClass('SpottedScript.Pages.ClearMyInbox.View', SpottedScript.DsiUserControl.View);
