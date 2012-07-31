Type.registerNamespace('SpottedScript.Blank.BankExportToLloydsAsXML');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BankExportToLloydsAsXML.View
SpottedScript.Blank.BankExportToLloydsAsXML.View = function SpottedScript_Blank_BankExportToLloydsAsXML_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BankExportToLloydsAsXML.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BankExportToLloydsAsXML.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_BankExportToLloydsAsXML_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BankExportToLloydsAsXML.View.registerClass('SpottedScript.Blank.BankExportToLloydsAsXML.View', SpottedScript.BlankUserControl.View);
