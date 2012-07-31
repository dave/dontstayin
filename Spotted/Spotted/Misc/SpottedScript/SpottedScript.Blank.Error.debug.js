Type.registerNamespace('SpottedScript.Blank.Error');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Error.View
SpottedScript.Blank.Error.View = function SpottedScript_Blank_Error_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Error.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Error.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_Error_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Error.View.registerClass('SpottedScript.Blank.Error.View', SpottedScript.BlankUserControl.View);
