Type.registerNamespace('SpottedScript.Blank.ReportGenerator');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.ReportGenerator.View
SpottedScript.Blank.ReportGenerator.View = function SpottedScript_Blank_ReportGenerator_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.ReportGenerator.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.ReportGenerator.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_ReportGenerator_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.ReportGenerator.View.registerClass('SpottedScript.Blank.ReportGenerator.View', SpottedScript.BlankUserControl.View);
