Type.registerNamespace('SpottedScript.Admin.WeightedPages');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.WeightedPages.View
SpottedScript.Admin.WeightedPages.View = function SpottedScript_Admin_WeightedPages_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.WeightedPages.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.WeightedPages.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Admin_WeightedPages_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.WeightedPages.View.registerClass('SpottedScript.Admin.WeightedPages.View', SpottedScript.AdminUserControl.View);
