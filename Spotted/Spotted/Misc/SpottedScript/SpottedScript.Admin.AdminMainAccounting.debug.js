Type.registerNamespace('SpottedScript.Admin.AdminMainAccounting');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.AdminMainAccounting.View
SpottedScript.Admin.AdminMainAccounting.View = function SpottedScript_Admin_AdminMainAccounting_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.AdminMainAccounting.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.AdminMainAccounting.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Admin_AdminMainAccounting_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_adminMainAccountingPanel: function SpottedScript_Admin_AdminMainAccounting_View$get_adminMainAccountingPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminMainAccountingPanel');
    },
    get_createNewInvoiceButton: function SpottedScript_Admin_AdminMainAccounting_View$get_createNewInvoiceButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateNewInvoiceButton');
    },
    get_createNewTransferButton: function SpottedScript_Admin_AdminMainAccounting_View$get_createNewTransferButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateNewTransferButton');
    },
    get_submitSuccessfulTransfersButton: function SpottedScript_Admin_AdminMainAccounting_View$get_submitSuccessfulTransfersButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SubmitSuccessfulTransfersButton');
    },
    get_createNewCampaignCreditButton: function SpottedScript_Admin_AdminMainAccounting_View$get_createNewCampaignCreditButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateNewCampaignCreditButton');
    },
    get_createNewInsertionOrder: function SpottedScript_Admin_AdminMainAccounting_View$get_createNewInsertionOrder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateNewInsertionOrder');
    },
    get_sageFromDateLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_sageFromDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SageFromDateLabel');
    },
    get_sageFromDateCal: function SpottedScript_Admin_AdminMainAccounting_View$get_sageFromDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_SageFromDateCalController');
    },
    get_exportToSageTypeDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_exportToSageTypeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExportToSageTypeDropDownList');
    },
    get_exportToSageButton: function SpottedScript_Admin_AdminMainAccounting_View$get_exportToSageButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExportToSageButton');
    },
    get_sageToDateLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_sageToDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SageToDateLabel');
    },
    get_sageToDateCal: function SpottedScript_Admin_AdminMainAccounting_View$get_sageToDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_SageToDateCalController');
    },
    get_sageErrorLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_sageErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SageErrorLabel');
    },
    get_typeLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_typeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TypeLabel');
    },
    get_typeDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_typeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TypeDropDownList');
    },
    get_nominalCodeLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_nominalCodeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NominalCodeLabel');
    },
    get_transferTypeLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_transferTypeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferTypeLabel');
    },
    get_invoiceTypeLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_invoiceTypeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceTypeLabel');
    },
    get_nominalCodeTextBox: function SpottedScript_Admin_AdminMainAccounting_View$get_nominalCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NominalCodeTextBox');
    },
    get_transferTypeDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_transferTypeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferTypeDropDownList');
    },
    get_invoiceTypeDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_invoiceTypeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceTypeDropDownList');
    },
    get_transferCompanyDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_transferCompanyDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferCompanyDropDownList');
    },
    get_kNumberLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_kNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KNumberLabel');
    },
    get_kNumberTextBox: function SpottedScript_Admin_AdminMainAccounting_View$get_kNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KNumberTextBox');
    },
    get_statusLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_statusLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusLabel');
    },
    get_statusDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_statusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusDropDownList');
    },
    get_promoterLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_promoterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterLabel');
    },
    get_invoiceItemTypeLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_invoiceItemTypeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceItemTypeLabel');
    },
    get_uiPromotersAutoComplete: function SpottedScript_Admin_AdminMainAccounting_View$get_uiPromotersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromotersAutoCompleteBehaviour');
    },
    get_invoiceItemTypeDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_invoiceItemTypeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceItemTypeDropDownList');
    },
    get_fromDateLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_fromDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FromDateLabel');
    },
    get_fromDateCal: function SpottedScript_Admin_AdminMainAccounting_View$get_fromDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_FromDateCalController');
    },
    get_userLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_userLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserLabel');
    },
    get_uiUserAutoComplete: function SpottedScript_Admin_AdminMainAccounting_View$get_uiUserAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiUserAutoCompleteBehaviour');
    },
    get_toDateLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_toDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ToDateLabel');
    },
    get_toDateCal: function SpottedScript_Admin_AdminMainAccounting_View$get_toDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_ToDateCalController');
    },
    get_salesUserLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_salesUserLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUserLabel');
    },
    get_transferMethodLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_transferMethodLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferMethodLabel');
    },
    get_salesUserDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_salesUserDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUserDropDownList');
    },
    get_transferMethodDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_transferMethodDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferMethodDropDownList');
    },
    get_dateTypeLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_dateTypeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateTypeLabel');
    },
    get_dateTypeDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_dateTypeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateTypeDropDownList');
    },
    get_bankAccountLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_bankAccountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountLabel');
    },
    get_bankAccountDropDownList: function SpottedScript_Admin_AdminMainAccounting_View$get_bankAccountDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountDropDownList');
    },
    get_searchButton: function SpottedScript_Admin_AdminMainAccounting_View$get_searchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchButton');
    },
    get_clearButton: function SpottedScript_Admin_AdminMainAccounting_View$get_clearButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClearButton');
    },
    get_searchResultsMessageLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_searchResultsMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsMessageLabel');
    },
    get_searchResultsInvoiceGridView: function SpottedScript_Admin_AdminMainAccounting_View$get_searchResultsInvoiceGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsInvoiceGridView');
    },
    get_searchResultsTransferGridView: function SpottedScript_Admin_AdminMainAccounting_View$get_searchResultsTransferGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsTransferGridView');
    },
    get_searchResultsInvoiceItemGridView: function SpottedScript_Admin_AdminMainAccounting_View$get_searchResultsInvoiceItemGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsInvoiceItemGridView');
    },
    get_searchResultsInsertionOrderGridView: function SpottedScript_Admin_AdminMainAccounting_View$get_searchResultsInsertionOrderGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsInsertionOrderGridView');
    },
    get_searchResultsCampaignCreditGridView: function SpottedScript_Admin_AdminMainAccounting_View$get_searchResultsCampaignCreditGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsCampaignCreditGridView');
    },
    get_totalExVatRow: function SpottedScript_Admin_AdminMainAccounting_View$get_totalExVatRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalExVatRow');
    },
    get_totalExVatLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_totalExVatLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalExVatLabel');
    },
    get_totalExVatValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_totalExVatValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalExVatValueLabel');
    },
    get_ticketSalesExVATValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_ticketSalesExVATValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketSalesExVATValueLabel');
    },
    get_bookingFeeExVATValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_bookingFeeExVATValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookingFeeExVATValueLabel');
    },
    get_totalVatRow: function SpottedScript_Admin_AdminMainAccounting_View$get_totalVatRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalVatRow');
    },
    get_totalVatLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_totalVatLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalVatLabel');
    },
    get_totalVatValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_totalVatValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalVatValueLabel');
    },
    get_ticketSalesVATValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_ticketSalesVATValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketSalesVATValueLabel');
    },
    get_bookingFeeVATValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_bookingFeeVATValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookingFeeVATValueLabel');
    },
    get_totalRow: function SpottedScript_Admin_AdminMainAccounting_View$get_totalRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalRow');
    },
    get_totalLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_totalLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalLabel');
    },
    get_totalValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_totalValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalValueLabel');
    },
    get_ticketSalesTotalValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_ticketSalesTotalValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketSalesTotalValueLabel');
    },
    get_bookingFeeTotalValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_bookingFeeTotalValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookingFeeTotalValueLabel');
    },
    get_totalTransferRow: function SpottedScript_Admin_AdminMainAccounting_View$get_totalTransferRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTransferRow');
    },
    get_totalTransferValueLabel: function SpottedScript_Admin_AdminMainAccounting_View$get_totalTransferValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTransferValueLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_AdminMainAccounting_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.AdminMainAccounting.View.registerClass('SpottedScript.Admin.AdminMainAccounting.View', SpottedScript.AdminUserControl.View);
