Type.registerNamespace('SpottedScript.Admin.IncomeEarningDate');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.IncomeEarningDate.View
SpottedScript.Admin.IncomeEarningDate.View = function SpottedScript_Admin_IncomeEarningDate_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.IncomeEarningDate.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.IncomeEarningDate.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Admin_IncomeEarningDate_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.IncomeEarningDate.View.registerClass('SpottedScript.Admin.IncomeEarningDate.View', SpottedScript.AdminUserControl.View);
