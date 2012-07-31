Type.registerNamespace('SpottedScript.Blank.Secure');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Secure.View
SpottedScript.Blank.Secure.View = function SpottedScript_Blank_Secure_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Secure.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Secure.View.prototype = {
    clientId: null,
    get_introHeader: function SpottedScript_Blank_Secure_View$get_introHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroHeader');
    },
    get_genericContainerPage: function SpottedScript_Blank_Secure_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Secure.View.registerClass('SpottedScript.Blank.Secure.View', SpottedScript.BlankUserControl.View);
