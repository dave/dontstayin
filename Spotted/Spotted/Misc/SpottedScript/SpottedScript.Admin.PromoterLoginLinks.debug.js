Type.registerNamespace('SpottedScript.Admin.PromoterLoginLinks');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.PromoterLoginLinks.View
SpottedScript.Admin.PromoterLoginLinks.View = function SpottedScript_Admin_PromoterLoginLinks_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.PromoterLoginLinks.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.PromoterLoginLinks.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Admin_PromoterLoginLinks_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.PromoterLoginLinks.View.registerClass('SpottedScript.Admin.PromoterLoginLinks.View', SpottedScript.AdminUserControl.View);
