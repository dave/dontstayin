Type.registerNamespace('SpottedScript.Admin.DonationIcons');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.DonationIcons.View
SpottedScript.Admin.DonationIcons.View = function SpottedScript_Admin_DonationIcons_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.DonationIcons.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.DonationIcons.View.prototype = {
    clientId: null,
    get_uiK: function SpottedScript_Admin_DonationIcons_View$get_uiK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiK');
    },
    get_uiName: function SpottedScript_Admin_DonationIcons_View$get_uiName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiName');
    },
    get_uiGuid: function SpottedScript_Admin_DonationIcons_View$get_uiGuid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGuid');
    },
    get_uiExtension: function SpottedScript_Admin_DonationIcons_View$get_uiExtension() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiExtension');
    },
    get_uiText: function SpottedScript_Admin_DonationIcons_View$get_uiText() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiText');
    },
    get_uiActivationDate: function SpottedScript_Admin_DonationIcons_View$get_uiActivationDate() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiActivationDateController');
    },
    get_uiActivationTime: function SpottedScript_Admin_DonationIcons_View$get_uiActivationTime() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiActivationTime');
    },
    get_uiEnabled: function SpottedScript_Admin_DonationIcons_View$get_uiEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEnabled');
    },
    get_uiThreadK: function SpottedScript_Admin_DonationIcons_View$get_uiThreadK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiThreadK');
    },
    get_uiVatable: function SpottedScript_Admin_DonationIcons_View$get_uiVatable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiVatable');
    },
    get_uiLink: function SpottedScript_Admin_DonationIcons_View$get_uiLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiLink');
    },
    get_genericContainerPage: function SpottedScript_Admin_DonationIcons_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.DonationIcons.View.registerClass('SpottedScript.Admin.DonationIcons.View', SpottedScript.AdminUserControl.View);
