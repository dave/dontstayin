Type.registerNamespace('SpottedScript.Blank.BuyTickets');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BuyTickets.View
SpottedScript.Blank.BuyTickets.View = function SpottedScript_Blank_BuyTickets_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BuyTickets.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BuyTickets.View.prototype = {
    clientId: null,
    get_h13dx: function SpottedScript_Blank_BuyTickets_View$get_h13dx() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13dx');
    },
    get_genericContainerPage: function SpottedScript_Blank_BuyTickets_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BuyTickets.View.registerClass('SpottedScript.Blank.BuyTickets.View', SpottedScript.BlankUserControl.View);
