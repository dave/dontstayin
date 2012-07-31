Type.registerNamespace('SpottedScript.Admin.SalesStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesStats.View
SpottedScript.Admin.SalesStats.View = function SpottedScript_Admin_SalesStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesStats.View.prototype = {
    clientId: null,
    get_callsDataGrid: function SpottedScript_Admin_SalesStats_View$get_callsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CallsDataGrid');
    },
    get_dailySalesDataGrid: function SpottedScript_Admin_SalesStats_View$get_dailySalesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DailySalesDataGrid');
    },
    get_monthlySalesDataGrid: function SpottedScript_Admin_SalesStats_View$get_monthlySalesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MonthlySalesDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesStats.View.registerClass('SpottedScript.Admin.SalesStats.View', SpottedScript.AdminUserControl.View);
