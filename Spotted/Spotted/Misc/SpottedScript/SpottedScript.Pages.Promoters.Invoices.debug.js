Type.registerNamespace('SpottedScript.Pages.Promoters.Invoices');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Invoices.View
SpottedScript.Pages.Promoters.Invoices.View = function SpottedScript_Pages_Promoters_Invoices_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Invoices.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Invoices.View.prototype = {
    clientId: null,
    get_payment: function SpottedScript_Pages_Promoters_Invoices_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_promoterIntro: function SpottedScript_Pages_Promoters_Invoices_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_h11: function SpottedScript_Pages_Promoters_Invoices_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_promoterIntro1: function SpottedScript_Pages_Promoters_Invoices_View$get_promoterIntro1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro1');
    },
    get_mainPanel: function SpottedScript_Pages_Promoters_Invoices_View$get_mainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MainPanel');
    },
    get_h1Header: function SpottedScript_Pages_Promoters_Invoices_View$get_h1Header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Header');
    },
    get_balanceLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_balanceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BalanceLabel');
    },
    get_balanceValueLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_balanceValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BalanceValueLabel');
    },
    get_outstandingBalanceLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_outstandingBalanceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OutstandingBalanceLabel');
    },
    get_creditLimitTR: function SpottedScript_Pages_Promoters_Invoices_View$get_creditLimitTR() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditLimitTR');
    },
    get_creditLimitLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_creditLimitLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditLimitLabel');
    },
    get_creditLimitValueLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_creditLimitValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditLimitValueLabel');
    },
    get_fundsAvailableTR: function SpottedScript_Pages_Promoters_Invoices_View$get_fundsAvailableTR() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsAvailableTR');
    },
    get_fundsAvailableLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_fundsAvailableLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsAvailableLabel');
    },
    get_fundsAvailableValueLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_fundsAvailableValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsAvailableValueLabel');
    },
    get_ticketFundsTR: function SpottedScript_Pages_Promoters_Invoices_View$get_ticketFundsTR() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsTR');
    },
    get_ticketFundsValueLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_ticketFundsValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsValueLabel');
    },
    get_headerPanel: function SpottedScript_Pages_Promoters_Invoices_View$get_headerPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderPanel');
    },
    get_viewSummaryLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_viewSummaryLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewSummaryLabel');
    },
    get_monthDropDownList: function SpottedScript_Pages_Promoters_Invoices_View$get_monthDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MonthDropDownList');
    },
    get_yearDropDownList: function SpottedScript_Pages_Promoters_Invoices_View$get_yearDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_YearDropDownList');
    },
    get_viewSummaryButton: function SpottedScript_Pages_Promoters_Invoices_View$get_viewSummaryButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewSummaryButton');
    },
    get_viewStatementHyperLink: function SpottedScript_Pages_Promoters_Invoices_View$get_viewStatementHyperLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewStatementHyperLink');
    },
    get_viewSummaryCustomValidator: function SpottedScript_Pages_Promoters_Invoices_View$get_viewSummaryCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewSummaryCustomValidator');
    },
    get_summaryPanel: function SpottedScript_Pages_Promoters_Invoices_View$get_summaryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SummaryPanel');
    },
    get_h1Summmary: function SpottedScript_Pages_Promoters_Invoices_View$get_h1Summmary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Summmary');
    },
    get_summaryHeaderLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_summaryHeaderLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SummaryHeaderLabel');
    },
    get_payOutstandingButton: function SpottedScript_Pages_Promoters_Invoices_View$get_payOutstandingButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayOutstandingButton');
    },
    get_payNowButton: function SpottedScript_Pages_Promoters_Invoices_View$get_payNowButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayNowButton');
    },
    get_setupTransferButton: function SpottedScript_Pages_Promoters_Invoices_View$get_setupTransferButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetupTransferButton');
    },
    get_filterLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_filterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FilterLabel');
    },
    get_filterDropDownList: function SpottedScript_Pages_Promoters_Invoices_View$get_filterDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FilterDropDownList');
    },
    get_promoterAccountItemRepeater: function SpottedScript_Pages_Promoters_Invoices_View$get_promoterAccountItemRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterAccountItemRepeater');
    },
    get_paginationPanel: function SpottedScript_Pages_Promoters_Invoices_View$get_paginationPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaginationPanel');
    },
    get_prevPageLinkButton: function SpottedScript_Pages_Promoters_Invoices_View$get_prevPageLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrevPageLinkButton');
    },
    get_nextPageLinkButton: function SpottedScript_Pages_Promoters_Invoices_View$get_nextPageLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextPageLinkButton');
    },
    get_adminPanel: function SpottedScript_Pages_Promoters_Invoices_View$get_adminPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminPanel');
    },
    get_adminInvoiceLinkLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_adminInvoiceLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminInvoiceLinkLabel');
    },
    get_adminTransferLinkLabel: function SpottedScript_Pages_Promoters_Invoices_View$get_adminTransferLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminTransferLinkLabel');
    },
    get_paymentPanel: function SpottedScript_Pages_Promoters_Invoices_View$get_paymentPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaymentPanel');
    },
    get_h13: function SpottedScript_Pages_Promoters_Invoices_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_button2: function SpottedScript_Pages_Promoters_Invoices_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_paidMessagePanel: function SpottedScript_Pages_Promoters_Invoices_View$get_paidMessagePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidMessagePanel');
    },
    get_h14: function SpottedScript_Pages_Promoters_Invoices_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_setupTransferPanel: function SpottedScript_Pages_Promoters_Invoices_View$get_setupTransferPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetupTransferPanel');
    },
    get_h15: function SpottedScript_Pages_Promoters_Invoices_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_setupPayment: function SpottedScript_Pages_Promoters_Invoices_View$get_setupPayment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetupPayment');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Invoices_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Invoices.View.registerClass('SpottedScript.Pages.Promoters.Invoices.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
