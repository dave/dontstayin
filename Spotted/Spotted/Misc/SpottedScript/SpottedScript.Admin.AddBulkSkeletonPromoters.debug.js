Type.registerNamespace('SpottedScript.Admin.AddBulkSkeletonPromoters');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.AddBulkSkeletonPromoters.View
SpottedScript.Admin.AddBulkSkeletonPromoters.View = function SpottedScript_Admin_AddBulkSkeletonPromoters_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.AddBulkSkeletonPromoters.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.AddBulkSkeletonPromoters.View.prototype = {
    clientId: null,
    get_sector: function SpottedScript_Admin_AddBulkSkeletonPromoters_View$get_sector() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Sector');
    },
    get_salesCampaignDropDown: function SpottedScript_Admin_AddBulkSkeletonPromoters_View$get_salesCampaignDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesCampaignDropDown');
    },
    get_csv: function SpottedScript_Admin_AddBulkSkeletonPromoters_View$get_csv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Csv');
    },
    get_error: function SpottedScript_Admin_AddBulkSkeletonPromoters_View$get_error() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Error');
    },
    get_genericContainerPage: function SpottedScript_Admin_AddBulkSkeletonPromoters_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.AddBulkSkeletonPromoters.View.registerClass('SpottedScript.Admin.AddBulkSkeletonPromoters.View', SpottedScript.AdminUserControl.View);
