Type.registerNamespace('SpottedScript.Admin.SalesCampaigns');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesCampaigns.View
SpottedScript.Admin.SalesCampaigns.View = function SpottedScript_Admin_SalesCampaigns_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesCampaigns.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesCampaigns.View.prototype = {
    clientId: null,
    get_campaignsDataGrid: function SpottedScript_Admin_SalesCampaigns_View$get_campaignsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CampaignsDataGrid');
    },
    get_addName: function SpottedScript_Admin_SalesCampaigns_View$get_addName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddName');
    },
    get_addDescription: function SpottedScript_Admin_SalesCampaigns_View$get_addDescription() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddDescription');
    },
    get_addStartDate: function SpottedScript_Admin_SalesCampaigns_View$get_addStartDate() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_AddStartDateController');
    },
    get_addEndDate: function SpottedScript_Admin_SalesCampaigns_View$get_addEndDate() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_AddEndDateController');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesCampaigns_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesCampaigns.View.registerClass('SpottedScript.Admin.SalesCampaigns.View', SpottedScript.AdminUserControl.View);
