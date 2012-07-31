Type.registerNamespace('SpottedScript.Admin.MemcachedStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.MemcachedStats.View
SpottedScript.Admin.MemcachedStats.View = function SpottedScript_Admin_MemcachedStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.MemcachedStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.MemcachedStats.View.prototype = {
    clientId: null,
    get_uiMemcachedStatsGridView: function SpottedScript_Admin_MemcachedStats_View$get_uiMemcachedStatsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMemcachedStatsGridView');
    },
    get_genericContainerPage: function SpottedScript_Admin_MemcachedStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.MemcachedStats.View.registerClass('SpottedScript.Admin.MemcachedStats.View', SpottedScript.AdminUserControl.View);
