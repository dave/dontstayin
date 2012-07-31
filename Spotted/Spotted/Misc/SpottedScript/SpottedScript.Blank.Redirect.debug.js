Type.registerNamespace('SpottedScript.Blank.Redirect');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Redirect.View
SpottedScript.Blank.Redirect.View = function SpottedScript_Blank_Redirect_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Redirect.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Redirect.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_Redirect_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Redirect.View.registerClass('SpottedScript.Blank.Redirect.View', SpottedScript.BlankUserControl.View);
