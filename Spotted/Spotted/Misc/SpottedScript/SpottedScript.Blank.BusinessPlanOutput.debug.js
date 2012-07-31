Type.registerNamespace('SpottedScript.Blank.BusinessPlanOutput');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BusinessPlanOutput.View
SpottedScript.Blank.BusinessPlanOutput.View = function SpottedScript_Blank_BusinessPlanOutput_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BusinessPlanOutput.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BusinessPlanOutput.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_BusinessPlanOutput_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BusinessPlanOutput.View.registerClass('SpottedScript.Blank.BusinessPlanOutput.View', SpottedScript.BlankUserControl.View);
