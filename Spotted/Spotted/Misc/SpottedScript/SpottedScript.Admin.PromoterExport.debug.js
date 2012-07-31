Type.registerNamespace('SpottedScript.Admin.PromoterExport');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.PromoterExport.View
SpottedScript.Admin.PromoterExport.View = function SpottedScript_Admin_PromoterExport_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.PromoterExport.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.PromoterExport.View.prototype = {
    clientId: null,
    get_button1: function SpottedScript_Admin_PromoterExport_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_genericContainerPage: function SpottedScript_Admin_PromoterExport_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.PromoterExport.View.registerClass('SpottedScript.Admin.PromoterExport.View', SpottedScript.AdminUserControl.View);
