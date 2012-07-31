Type.registerNamespace('SpottedScript.Admin.IncomePaymentDate');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.IncomePaymentDate.View
SpottedScript.Admin.IncomePaymentDate.View = function SpottedScript_Admin_IncomePaymentDate_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.IncomePaymentDate.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.IncomePaymentDate.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Admin_IncomePaymentDate_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.IncomePaymentDate.View.registerClass('SpottedScript.Admin.IncomePaymentDate.View', SpottedScript.AdminUserControl.View);
