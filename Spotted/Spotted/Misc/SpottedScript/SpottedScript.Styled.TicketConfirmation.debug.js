Type.registerNamespace('SpottedScript.Styled.TicketConfirmation');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.TicketConfirmation.View
SpottedScript.Styled.TicketConfirmation.View = function SpottedScript_Styled_TicketConfirmation_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.TicketConfirmation.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.TicketConfirmation.View.prototype = {
    clientId: null,
    get_ticketConfirmationLabel: function SpottedScript_Styled_TicketConfirmation_View$get_ticketConfirmationLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketConfirmationLabel');
    },
    get_eventLinkLabel: function SpottedScript_Styled_TicketConfirmation_View$get_eventLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLinkLabel');
    },
    get_genericContainerPage: function SpottedScript_Styled_TicketConfirmation_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.TicketConfirmation.View.registerClass('SpottedScript.Styled.TicketConfirmation.View', SpottedScript.StyledUserControl.View);
