Type.registerNamespace('SpottedScript.Admin.SalesProactive');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesProactive.View
SpottedScript.Admin.SalesProactive.View = function SpottedScript_Admin_SalesProactive_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesProactive.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesProactive.View.prototype = {
    clientId: null,
    get_salesEstimateFilterDropDownList: function SpottedScript_Admin_SalesProactive_View$get_salesEstimateFilterDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesEstimateFilterDropDownList');
    },
    get_sectorFilterDropDownList: function SpottedScript_Admin_SalesProactive_View$get_sectorFilterDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SectorFilterDropDownList');
    },
    get_promoterDataGrid: function SpottedScript_Admin_SalesProactive_View$get_promoterDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterDataGrid');
    },
    get_expiredDataGrid: function SpottedScript_Admin_SalesProactive_View$get_expiredDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExpiredDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesProactive_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesProactive.View.registerClass('SpottedScript.Admin.SalesProactive.View', SpottedScript.AdminUserControl.View);
