Type.registerNamespace('SpottedScript.Blank.Test');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Test.View
SpottedScript.Blank.Test.View = function SpottedScript_Blank_Test_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Test.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Test.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_Test_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Test.View.registerClass('SpottedScript.Blank.Test.View', SpottedScript.BlankUserControl.View);
