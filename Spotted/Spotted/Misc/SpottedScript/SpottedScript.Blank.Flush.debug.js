Type.registerNamespace('SpottedScript.Blank.Flush');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Flush.View
SpottedScript.Blank.Flush.View = function SpottedScript_Blank_Flush_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Flush.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Flush.View.prototype = {
    clientId: null,
    get_lab1: function SpottedScript_Blank_Flush_View$get_lab1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Lab1');
    },
    get_genericContainerPage: function SpottedScript_Blank_Flush_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Flush.View.registerClass('SpottedScript.Blank.Flush.View', SpottedScript.BlankUserControl.View);
