Type.registerNamespace('SpottedScript.Admin.BannerImpressionStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.BannerImpressionStats.View
SpottedScript.Admin.BannerImpressionStats.View = function SpottedScript_Admin_BannerImpressionStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.BannerImpressionStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.BannerImpressionStats.View.prototype = {
    clientId: null,
    get_uiFirstDate: function SpottedScript_Admin_BannerImpressionStats_View$get_uiFirstDate() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiFirstDateController');
    },
    get_uiSecondDate: function SpottedScript_Admin_BannerImpressionStats_View$get_uiSecondDate() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiSecondDateController');
    },
    get_uiChangeDateRange: function SpottedScript_Admin_BannerImpressionStats_View$get_uiChangeDateRange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiChangeDateRange');
    },
    get_gridView: function SpottedScript_Admin_BannerImpressionStats_View$get_gridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GridView');
    },
    get_genericContainerPage: function SpottedScript_Admin_BannerImpressionStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.BannerImpressionStats.View.registerClass('SpottedScript.Admin.BannerImpressionStats.View', SpottedScript.AdminUserControl.View);
