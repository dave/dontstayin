Type.registerNamespace('SpottedScript.Admin.BannerPositionAvailability');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.BannerPositionAvailability.View
SpottedScript.Admin.BannerPositionAvailability.View = function SpottedScript_Admin_BannerPositionAvailability_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.BannerPositionAvailability.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.BannerPositionAvailability.View.prototype = {
    clientId: null,
    get_slots0: function SpottedScript_Admin_BannerPositionAvailability_View$get_slots0() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots0');
    },
    get_slots1: function SpottedScript_Admin_BannerPositionAvailability_View$get_slots1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots1');
    },
    get_slots4: function SpottedScript_Admin_BannerPositionAvailability_View$get_slots4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots4');
    },
    get_slots3: function SpottedScript_Admin_BannerPositionAvailability_View$get_slots3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots3');
    },
    get_slots2: function SpottedScript_Admin_BannerPositionAvailability_View$get_slots2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Slots2');
    },
    get_genericContainerPage: function SpottedScript_Admin_BannerPositionAvailability_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.BannerPositionAvailability.View.registerClass('SpottedScript.Admin.BannerPositionAvailability.View', SpottedScript.AdminUserControl.View);
