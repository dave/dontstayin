Type.registerNamespace('SpottedScript.Admin.SubmitSuccessfulTransfers');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SubmitSuccessfulTransfers.View
SpottedScript.Admin.SubmitSuccessfulTransfers.View = function SpottedScript_Admin_SubmitSuccessfulTransfers_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SubmitSuccessfulTransfers.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SubmitSuccessfulTransfers.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Admin_SubmitSuccessfulTransfers_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_successfulTransferPanel: function SpottedScript_Admin_SubmitSuccessfulTransfers_View$get_successfulTransferPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuccessfulTransferPanel');
    },
    get_errorLabel: function SpottedScript_Admin_SubmitSuccessfulTransfers_View$get_errorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorLabel');
    },
    get_successfulTransferGridView: function SpottedScript_Admin_SubmitSuccessfulTransfers_View$get_successfulTransferGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuccessfulTransferGridView');
    },
    get_saveButton: function SpottedScript_Admin_SubmitSuccessfulTransfers_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveButton');
    },
    get_cancelButton: function SpottedScript_Admin_SubmitSuccessfulTransfers_View$get_cancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelButton');
    },
    get_genericContainerPage: function SpottedScript_Admin_SubmitSuccessfulTransfers_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SubmitSuccessfulTransfers.View.registerClass('SpottedScript.Admin.SubmitSuccessfulTransfers.View', SpottedScript.AdminUserControl.View);
