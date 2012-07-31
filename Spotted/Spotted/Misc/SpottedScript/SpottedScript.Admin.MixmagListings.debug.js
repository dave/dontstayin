Type.registerNamespace('SpottedScript.Admin.MixmagListings');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.MixmagListings.View
SpottedScript.Admin.MixmagListings.View = function SpottedScript_Admin_MixmagListings_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.MixmagListings.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.MixmagListings.View.prototype = {
    clientId: null,
    get_issueDrop: function SpottedScript_Admin_MixmagListings_View$get_issueDrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IssueDrop');
    },
    get_zoneDrop: function SpottedScript_Admin_MixmagListings_View$get_zoneDrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ZoneDrop');
    },
    get_genericContainerPage: function SpottedScript_Admin_MixmagListings_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.MixmagListings.View.registerClass('SpottedScript.Admin.MixmagListings.View', SpottedScript.AdminUserControl.View);
