Type.registerNamespace('SpottedScript.Admin.TicketDetails');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.TicketDetails.View
SpottedScript.Admin.TicketDetails.View = function SpottedScript_Admin_TicketDetails_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.TicketDetails.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.TicketDetails.View.prototype = {
    clientId: null,
    get_ticketDetailsPanel: function SpottedScript_Admin_TicketDetails_View$get_ticketDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketDetailsPanel');
    },
    get_cancelledRow: function SpottedScript_Admin_TicketDetails_View$get_cancelledRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelledRow');
    },
    get_ticketKLabel: function SpottedScript_Admin_TicketDetails_View$get_ticketKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketKLabel');
    },
    get_userNickNameLabel: function SpottedScript_Admin_TicketDetails_View$get_userNickNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserNickNameLabel');
    },
    get_fullNameLabel: function SpottedScript_Admin_TicketDetails_View$get_fullNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FullNameLabel');
    },
    get_purchaseDateLabel: function SpottedScript_Admin_TicketDetails_View$get_purchaseDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PurchaseDateLabel');
    },
    get_eventLabel: function SpottedScript_Admin_TicketDetails_View$get_eventLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLabel');
    },
    get_ticketRunLabel: function SpottedScript_Admin_TicketDetails_View$get_ticketRunLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunLabel');
    },
    get_quantityLabel: function SpottedScript_Admin_TicketDetails_View$get_quantityLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuantityLabel');
    },
    get_priceLabel: function SpottedScript_Admin_TicketDetails_View$get_priceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PriceLabel');
    },
    get_bookingFeeLabel: function SpottedScript_Admin_TicketDetails_View$get_bookingFeeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookingFeeLabel');
    },
    get_invoiceLabel: function SpottedScript_Admin_TicketDetails_View$get_invoiceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceLabel');
    },
    get_cardNumberEndLabel: function SpottedScript_Admin_TicketDetails_View$get_cardNumberEndLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardNumberEndLabel');
    },
    get_codeLabel: function SpottedScript_Admin_TicketDetails_View$get_codeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CodeLabel');
    },
    get_addressLabel: function SpottedScript_Admin_TicketDetails_View$get_addressLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressLabel');
    },
    get_feedbackLabel: function SpottedScript_Admin_TicketDetails_View$get_feedbackLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FeedbackLabel');
    },
    get_feedbackNoteRow: function SpottedScript_Admin_TicketDetails_View$get_feedbackNoteRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FeedbackNoteRow');
    },
    get_feedbackNoteLabel: function SpottedScript_Admin_TicketDetails_View$get_feedbackNoteLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FeedbackNoteLabel');
    },
    get_refundButton: function SpottedScript_Admin_TicketDetails_View$get_refundButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundButton');
    },
    get_refundFullButton: function SpottedScript_Admin_TicketDetails_View$get_refundFullButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundFullButton');
    },
    get_chargePromoterForRefundCheckBox: function SpottedScript_Admin_TicketDetails_View$get_chargePromoterForRefundCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChargePromoterForRefundCheckBox');
    },
    get_chargePromoterAmountTextBox: function SpottedScript_Admin_TicketDetails_View$get_chargePromoterAmountTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChargePromoterAmountTextBox');
    },
    get_processingVal: function SpottedScript_Admin_TicketDetails_View$get_processingVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProcessingVal');
    },
    get_ticketValidationSummary: function SpottedScript_Admin_TicketDetails_View$get_ticketValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketValidationSummary');
    },
    get_genericContainerPage: function SpottedScript_Admin_TicketDetails_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.TicketDetails.View.registerClass('SpottedScript.Admin.TicketDetails.View', SpottedScript.AdminUserControl.View);
