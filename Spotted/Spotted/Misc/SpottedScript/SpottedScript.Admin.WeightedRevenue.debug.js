Type.registerNamespace('SpottedScript.Admin.WeightedRevenue');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.WeightedRevenue.View
SpottedScript.Admin.WeightedRevenue.View = function SpottedScript_Admin_WeightedRevenue_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.WeightedRevenue.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.WeightedRevenue.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Admin_WeightedRevenue_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.WeightedRevenue.View.registerClass('SpottedScript.Admin.WeightedRevenue.View', SpottedScript.AdminUserControl.View);
