Type.registerNamespace('SpottedScript.Admin.SalesCalls');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesCalls.View
SpottedScript.Admin.SalesCalls.View = function SpottedScript_Admin_SalesCalls_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesCalls.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesCalls.View.prototype = {
    clientId: null,
    get_cal: function SpottedScript_Admin_SalesCalls_View$get_cal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Cal');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesCalls_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesCalls.View.registerClass('SpottedScript.Admin.SalesCalls.View', SpottedScript.AdminUserControl.View);
