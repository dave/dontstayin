Type.registerNamespace('SpottedScript.Blank.Invoice');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Invoice.View
SpottedScript.Blank.Invoice.View = function SpottedScript_Blank_Invoice_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Invoice.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Invoice.View.prototype = {
    clientId: null,
    get_itemsDataGrid: function SpottedScript_Blank_Invoice_View$get_itemsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ItemsDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Blank_Invoice_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Invoice.View.registerClass('SpottedScript.Blank.Invoice.View', SpottedScript.BlankUserControl.View);
