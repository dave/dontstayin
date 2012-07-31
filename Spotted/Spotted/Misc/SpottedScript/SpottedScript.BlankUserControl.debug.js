Type.registerNamespace('SpottedScript.BlankUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.BlankUserControl.View
SpottedScript.BlankUserControl.View = function SpottedScript_BlankUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.BlankUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.BlankUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_BlankUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.BlankUserControl.View.registerClass('SpottedScript.BlankUserControl.View', SpottedScript.GenericUserControl.View);
