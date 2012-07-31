Type.registerNamespace('SpottedScript.Pages.Camp');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Camp.View
SpottedScript.Pages.Camp.View = function SpottedScript_Pages_Camp_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Camp.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Camp.View.prototype = {
    clientId: null,
    get_quantityPanel: function SpottedScript_Pages_Camp_View$get_quantityPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuantityPanel');
    },
    get_nameLabel: function SpottedScript_Pages_Camp_View$get_nameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NameLabel');
    },
    get_alreadyHaveTicketsP: function SpottedScript_Pages_Camp_View$get_alreadyHaveTicketsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AlreadyHaveTicketsP');
    },
    get_alreadyHaveTicketsLabel: function SpottedScript_Pages_Camp_View$get_alreadyHaveTicketsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AlreadyHaveTicketsLabel');
    },
    get_ticketsQuantityTextBox: function SpottedScript_Pages_Camp_View$get_ticketsQuantityTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsQuantityTextBox');
    },
    get_button1: function SpottedScript_Pages_Camp_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_quantityErrorPanel: function SpottedScript_Pages_Camp_View$get_quantityErrorPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuantityErrorPanel');
    },
    get_quantityErrorP: function SpottedScript_Pages_Camp_View$get_quantityErrorP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuantityErrorP');
    },
    get_button2: function SpottedScript_Pages_Camp_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_payPanel: function SpottedScript_Pages_Camp_View$get_payPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayPanel');
    },
    get_ticketPayment: function SpottedScript_Pages_Camp_View$get_ticketPayment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPayment');
    },
    get_donePanel: function SpottedScript_Pages_Camp_View$get_donePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonePanel');
    },
    get_doneQuantityLabel: function SpottedScript_Pages_Camp_View$get_doneQuantityLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoneQuantityLabel');
    },
    get_soldOutPanel: function SpottedScript_Pages_Camp_View$get_soldOutPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SoldOutPanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Camp_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Camp.View.registerClass('SpottedScript.Pages.Camp.View', SpottedScript.DsiUserControl.View);
