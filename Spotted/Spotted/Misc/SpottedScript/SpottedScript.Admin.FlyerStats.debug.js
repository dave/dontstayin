Type.registerNamespace('SpottedScript.Admin.FlyerStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.FlyerStats.View
SpottedScript.Admin.FlyerStats.View = function SpottedScript_Admin_FlyerStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.FlyerStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.FlyerStats.View.prototype = {
    clientId: null,
    get_uiGridView: function SpottedScript_Admin_FlyerStats_View$get_uiGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGridView');
    },
    get_genericContainerPage: function SpottedScript_Admin_FlyerStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.FlyerStats.View.registerClass('SpottedScript.Admin.FlyerStats.View', SpottedScript.AdminUserControl.View);
