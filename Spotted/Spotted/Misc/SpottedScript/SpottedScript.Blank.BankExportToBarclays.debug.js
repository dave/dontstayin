Type.registerNamespace('SpottedScript.Blank.BankExportToBarclays');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BankExportToBarclays.View
SpottedScript.Blank.BankExportToBarclays.View = function SpottedScript_Blank_BankExportToBarclays_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BankExportToBarclays.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BankExportToBarclays.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_BankExportToBarclays_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BankExportToBarclays.View.registerClass('SpottedScript.Blank.BankExportToBarclays.View', SpottedScript.BlankUserControl.View);
