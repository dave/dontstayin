Type.registerNamespace('SpottedScript.Admin.BannerHitViewer');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.BannerHitViewer.View
SpottedScript.Admin.BannerHitViewer.View = function SpottedScript_Admin_BannerHitViewer_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.BannerHitViewer.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.BannerHitViewer.View.prototype = {
    clientId: null,
    get_timeslotStart: function SpottedScript_Admin_BannerHitViewer_View$get_timeslotStart() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TimeslotStart');
    },
    get_timeslotEnd: function SpottedScript_Admin_BannerHitViewer_View$get_timeslotEnd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TimeslotEnd');
    },
    get_timeslotInfoRepeater: function SpottedScript_Admin_BannerHitViewer_View$get_timeslotInfoRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TimeslotInfoRepeater');
    },
    get_bannerInfoRepeater: function SpottedScript_Admin_BannerHitViewer_View$get_bannerInfoRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerInfoRepeater');
    },
    get_genericContainerPage: function SpottedScript_Admin_BannerHitViewer_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.BannerHitViewer.View.registerClass('SpottedScript.Admin.BannerHitViewer.View', SpottedScript.AdminUserControl.View);
