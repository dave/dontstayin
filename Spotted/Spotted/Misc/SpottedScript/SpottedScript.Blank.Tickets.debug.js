Type.registerNamespace('SpottedScript.Blank.Tickets');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Tickets.View
SpottedScript.Blank.Tickets.View = function SpottedScript_Blank_Tickets_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Tickets.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Tickets.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Blank_Tickets_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_tab: function SpottedScript_Blank_Tickets_View$get_tab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Tab');
    },
    get_genericContainerPage: function SpottedScript_Blank_Tickets_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Tickets.View.registerClass('SpottedScript.Blank.Tickets.View', SpottedScript.BlankUserControl.View);
