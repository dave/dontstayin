Type.registerNamespace('SpottedScript.Blank.PhoneIdleScreen');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.PhoneIdleScreen.View
SpottedScript.Blank.PhoneIdleScreen.View = function SpottedScript_Blank_PhoneIdleScreen_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.PhoneIdleScreen.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.PhoneIdleScreen.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_PhoneIdleScreen_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.PhoneIdleScreen.View.registerClass('SpottedScript.Blank.PhoneIdleScreen.View', SpottedScript.BlankUserControl.View);
