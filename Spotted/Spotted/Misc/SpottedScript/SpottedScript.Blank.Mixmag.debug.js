Type.registerNamespace('SpottedScript.Blank.Mixmag');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Mixmag.View
SpottedScript.Blank.Mixmag.View = function SpottedScript_Blank_Mixmag_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Mixmag.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Mixmag.View.prototype = {
    clientId: null,
    get_button3: function SpottedScript_Blank_Mixmag_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_genericContainerPage: function SpottedScript_Blank_Mixmag_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Mixmag.View.registerClass('SpottedScript.Blank.Mixmag.View', SpottedScript.BlankUserControl.View);
