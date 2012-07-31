Type.registerNamespace('SpottedScript.Blank.Terms');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Terms.View
SpottedScript.Blank.Terms.View = function SpottedScript_Blank_Terms_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Terms.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Terms.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Blank_Terms_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Blank_Terms_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Terms.View.registerClass('SpottedScript.Blank.Terms.View', SpottedScript.BlankUserControl.View);
