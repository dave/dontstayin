Type.registerNamespace('SpottedScript.Admin.Settings');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.Settings.View
SpottedScript.Admin.Settings.View = function SpottedScript_Admin_Settings_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.Settings.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.Settings.View.prototype = {
    clientId: null,
    get_btnSave: function SpottedScript_Admin_Settings_View$get_btnSave() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_btnSave');
    },
    get_genericContainerPage: function SpottedScript_Admin_Settings_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.Settings.View.registerClass('SpottedScript.Admin.Settings.View', SpottedScript.AdminUserControl.View);
