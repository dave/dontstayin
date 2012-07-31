Type.registerNamespace('SpottedScript.Admin.RecentlyEndedBanners');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.RecentlyEndedBanners.View
SpottedScript.Admin.RecentlyEndedBanners.View = function SpottedScript_Admin_RecentlyEndedBanners_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.RecentlyEndedBanners.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.RecentlyEndedBanners.View.prototype = {
    clientId: null,
    get_uiFirstDate: function SpottedScript_Admin_RecentlyEndedBanners_View$get_uiFirstDate() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiFirstDateController');
    },
    get_uiSecondDate: function SpottedScript_Admin_RecentlyEndedBanners_View$get_uiSecondDate() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiSecondDateController');
    },
    get_uiChangeDateRange: function SpottedScript_Admin_RecentlyEndedBanners_View$get_uiChangeDateRange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiChangeDateRange');
    },
    get_uiBanners: function SpottedScript_Admin_RecentlyEndedBanners_View$get_uiBanners() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBanners');
    },
    get_genericContainerPage: function SpottedScript_Admin_RecentlyEndedBanners_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.RecentlyEndedBanners.View.registerClass('SpottedScript.Admin.RecentlyEndedBanners.View', SpottedScript.AdminUserControl.View);
