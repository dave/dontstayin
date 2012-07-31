Type.registerNamespace('SpottedScript.Admin.BannerPositionAvailabilityNew');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.BannerPositionAvailabilityNew.View
SpottedScript.Admin.BannerPositionAvailabilityNew.View = function SpottedScript_Admin_BannerPositionAvailabilityNew_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.BannerPositionAvailabilityNew.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.BannerPositionAvailabilityNew.View.prototype = {
    clientId: null,
    get_slots0: function SpottedScript_Admin_BannerPositionAvailabilityNew_View$get_slots0() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots0');
    },
    get_slots1: function SpottedScript_Admin_BannerPositionAvailabilityNew_View$get_slots1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots1');
    },
    get_slots4: function SpottedScript_Admin_BannerPositionAvailabilityNew_View$get_slots4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots4');
    },
    get_slots3: function SpottedScript_Admin_BannerPositionAvailabilityNew_View$get_slots3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots3');
    },
    get_slots2: function SpottedScript_Admin_BannerPositionAvailabilityNew_View$get_slots2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots2');
    },
    get_genericContainerPage: function SpottedScript_Admin_BannerPositionAvailabilityNew_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.BannerPositionAvailabilityNew.View.registerClass('SpottedScript.Admin.BannerPositionAvailabilityNew.View', SpottedScript.AdminUserControl.View);
