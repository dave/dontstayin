Type.registerNamespace('SpottedScript.Styled.Pay');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.Pay.View
SpottedScript.Styled.Pay.View = function SpottedScript_Styled_Pay_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.Pay.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.Pay.View.prototype = {
    clientId: null,
    get_eventHeader: function SpottedScript_Styled_Pay_View$get_eventHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventHeader');
    },
    get_ticketDetails: function SpottedScript_Styled_Pay_View$get_ticketDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketDetails');
    },
    get_payment: function SpottedScript_Styled_Pay_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_errorMessageLabel: function SpottedScript_Styled_Pay_View$get_errorMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorMessageLabel');
    },
    get_cancelTicketPaymentButton: function SpottedScript_Styled_Pay_View$get_cancelTicketPaymentButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelTicketPaymentButton');
    },
    get_myTicketsPanel: function SpottedScript_Styled_Pay_View$get_myTicketsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsPanel');
    },
    get_myTicketsRepeater: function SpottedScript_Styled_Pay_View$get_myTicketsRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsRepeater');
    },
    get_genericContainerPage: function SpottedScript_Styled_Pay_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.Pay.View.registerClass('SpottedScript.Styled.Pay.View', SpottedScript.StyledUserControl.View);
