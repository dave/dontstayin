Type.registerNamespace('SpottedScript.Pages.Events.TicketConfirmation');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.TicketConfirmation.View
SpottedScript.Pages.Events.TicketConfirmation.View = function SpottedScript_Pages_Events_TicketConfirmation_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.TicketConfirmation.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.TicketConfirmation.View.prototype = {
    clientId: null,
    get_eventTicketConfirmationPanel: function SpottedScript_Pages_Events_TicketConfirmation_View$get_eventTicketConfirmationPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventTicketConfirmationPanel');
    },
    get_ticketsHeading: function SpottedScript_Pages_Events_TicketConfirmation_View$get_ticketsHeading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsHeading');
    },
    get_ticketConfirmationLabel: function SpottedScript_Pages_Events_TicketConfirmation_View$get_ticketConfirmationLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketConfirmationLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_TicketConfirmation_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.TicketConfirmation.View.registerClass('SpottedScript.Pages.Events.TicketConfirmation.View', SpottedScript.Pages.Events.EventUserControl.View);
