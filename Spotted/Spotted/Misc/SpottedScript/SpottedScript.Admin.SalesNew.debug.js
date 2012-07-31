Type.registerNamespace('SpottedScript.Admin.SalesNew');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesNew.View
SpottedScript.Admin.SalesNew.View = function SpottedScript_Admin_SalesNew_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesNew.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesNew.View.prototype = {
    clientId: null,
    get_newPromoterDataGrid: function SpottedScript_Admin_SalesNew_View$get_newPromoterDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewPromoterDataGrid');
    },
    get_callBacksDataGrid: function SpottedScript_Admin_SalesNew_View$get_callBacksDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CallBacksDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesNew_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesNew.View.registerClass('SpottedScript.Admin.SalesNew.View', SpottedScript.AdminUserControl.View);
