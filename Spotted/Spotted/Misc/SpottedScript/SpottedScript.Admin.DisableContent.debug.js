Type.registerNamespace('SpottedScript.Admin.DisableContent');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.DisableContent.View
SpottedScript.Admin.DisableContent.View = function SpottedScript_Admin_DisableContent_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.DisableContent.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.DisableContent.View.prototype = {
    clientId: null,
    get_photoK: function SpottedScript_Admin_DisableContent_View$get_photoK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoK');
    },
    get_outLabel: function SpottedScript_Admin_DisableContent_View$get_outLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OutLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_DisableContent_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.DisableContent.View.registerClass('SpottedScript.Admin.DisableContent.View', SpottedScript.AdminUserControl.View);
