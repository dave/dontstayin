Type.registerNamespace('SpottedScript.Admin.SalesActive');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesActive.View
SpottedScript.Admin.SalesActive.View = function SpottedScript_Admin_SalesActive_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesActive.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesActive.View.prototype = {
    clientId: null,
    get_salesEstimateFilterDropDownList: function SpottedScript_Admin_SalesActive_View$get_salesEstimateFilterDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesEstimateFilterDropDownList');
    },
    get_sectorFilterDropDownList: function SpottedScript_Admin_SalesActive_View$get_sectorFilterDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SectorFilterDropDownList');
    },
    get_promoterDataGrid: function SpottedScript_Admin_SalesActive_View$get_promoterDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterDataGrid');
    },
    get_expiredDataGrid: function SpottedScript_Admin_SalesActive_View$get_expiredDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExpiredDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesActive_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesActive.View.registerClass('SpottedScript.Admin.SalesActive.View', SpottedScript.AdminUserControl.View);
