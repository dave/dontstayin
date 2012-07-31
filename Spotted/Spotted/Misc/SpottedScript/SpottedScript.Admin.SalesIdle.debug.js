Type.registerNamespace('SpottedScript.Admin.SalesIdle');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesIdle.View
SpottedScript.Admin.SalesIdle.View = function SpottedScript_Admin_SalesIdle_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesIdle.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesIdle.View.prototype = {
    clientId: null,
    get_pageNumberP: function SpottedScript_Admin_SalesIdle_View$get_pageNumberP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageNumberP');
    },
    get_promoterDataGrid: function SpottedScript_Admin_SalesIdle_View$get_promoterDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesIdle_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesIdle.View.registerClass('SpottedScript.Admin.SalesIdle.View', SpottedScript.AdminUserControl.View);
