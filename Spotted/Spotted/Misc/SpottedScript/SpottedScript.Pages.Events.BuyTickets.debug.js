Type.registerNamespace('SpottedScript.Pages.Events.BuyTickets');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.BuyTickets.View
SpottedScript.Pages.Events.BuyTickets.View = function SpottedScript_Pages_Events_BuyTickets_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.BuyTickets.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.BuyTickets.View.prototype = {
    clientId: null,
    get_eventSelectedPanel: function SpottedScript_Pages_Events_BuyTickets_View$get_eventSelectedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventSelectedPanel');
    },
    get_promoterPanel: function SpottedScript_Pages_Events_BuyTickets_View$get_promoterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterPanel');
    },
    get_payForTicketsPanel: function SpottedScript_Pages_Events_BuyTickets_View$get_payForTicketsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayForTicketsPanel');
    },
    get_payForTicketsHeading: function SpottedScript_Pages_Events_BuyTickets_View$get_payForTicketsHeading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayForTicketsHeading');
    },
    get_payment: function SpottedScript_Pages_Events_BuyTickets_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_errorMessageLabel: function SpottedScript_Pages_Events_BuyTickets_View$get_errorMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorMessageLabel');
    },
    get_cancelTicketPaymentButton: function SpottedScript_Pages_Events_BuyTickets_View$get_cancelTicketPaymentButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelTicketPaymentButton');
    },
    get_myTicketsPanel: function SpottedScript_Pages_Events_BuyTickets_View$get_myTicketsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsPanel');
    },
    get_h1: function SpottedScript_Pages_Events_BuyTickets_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_myTicketsRepeater: function SpottedScript_Pages_Events_BuyTickets_View$get_myTicketsRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsRepeater');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_BuyTickets_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.BuyTickets.View.registerClass('SpottedScript.Pages.Events.BuyTickets.View', SpottedScript.Pages.Events.EventUserControl.View);
