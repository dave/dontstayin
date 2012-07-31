Type.registerNamespace('SpottedScript.Admin.Logs');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.Logs.View
SpottedScript.Admin.Logs.View = function SpottedScript_Admin_Logs_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.Logs.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.Logs.View.prototype = {
    clientId: null,
    get_times: function SpottedScript_Admin_Logs_View$get_times() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Times');
    },
    get_cal: function SpottedScript_Admin_Logs_View$get_cal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Cal');
    },
    get_genericContainerPage: function SpottedScript_Admin_Logs_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.Logs.View.registerClass('SpottedScript.Admin.Logs.View', SpottedScript.AdminUserControl.View);
