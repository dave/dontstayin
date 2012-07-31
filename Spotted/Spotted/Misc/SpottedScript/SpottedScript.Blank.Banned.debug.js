Type.registerNamespace('SpottedScript.Blank.Banned');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Banned.View
SpottedScript.Blank.Banned.View = function SpottedScript_Blank_Banned_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Banned.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Banned.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_Banned_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Banned.View.registerClass('SpottedScript.Blank.Banned.View', SpottedScript.BlankUserControl.View);
