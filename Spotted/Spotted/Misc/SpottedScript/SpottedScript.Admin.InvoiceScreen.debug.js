Type.registerNamespace('SpottedScript.Admin.InvoiceScreen');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.InvoiceScreen.View
SpottedScript.Admin.InvoiceScreen.View = function SpottedScript_Admin_InvoiceScreen_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.InvoiceScreen.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.InvoiceScreen.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Admin_InvoiceScreen_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_mainPanel: function SpottedScript_Admin_InvoiceScreen_View$get_mainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MainPanel');
    },
    get_errorLabel: function SpottedScript_Admin_InvoiceScreen_View$get_errorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorLabel');
    },
    get_invoiceKLabel: function SpottedScript_Admin_InvoiceScreen_View$get_invoiceKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceKLabel');
    },
    get_invoiceKValueLabel: function SpottedScript_Admin_InvoiceScreen_View$get_invoiceKValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceKValueLabel');
    },
    get_createdDateLabel: function SpottedScript_Admin_InvoiceScreen_View$get_createdDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreatedDateLabel');
    },
    get_createdDateTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_createdDateTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreatedDateTextBox');
    },
    get_promoterLabel: function SpottedScript_Admin_InvoiceScreen_View$get_promoterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterLabel');
    },
    get_uiPromotersAutoComplete: function SpottedScript_Admin_InvoiceScreen_View$get_uiPromotersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromotersAutoCompleteBehaviour');
    },
    get_promoterValueLabel: function SpottedScript_Admin_InvoiceScreen_View$get_promoterValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterValueLabel');
    },
    get_promoterAvailableFundsLabel: function SpottedScript_Admin_InvoiceScreen_View$get_promoterAvailableFundsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterAvailableFundsLabel');
    },
    get_dueDateLabel: function SpottedScript_Admin_InvoiceScreen_View$get_dueDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DueDateLabel');
    },
    get_overrideDueDateCheckBox: function SpottedScript_Admin_InvoiceScreen_View$get_overrideDueDateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideDueDateCheckBox');
    },
    get_overrideDueDatePanel: function SpottedScript_Admin_InvoiceScreen_View$get_overrideDueDatePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideDueDatePanel');
    },
    get_dueDateCal: function SpottedScript_Admin_InvoiceScreen_View$get_dueDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_DueDateCalController');
    },
    get_dueDateValueLabel: function SpottedScript_Admin_InvoiceScreen_View$get_dueDateValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DueDateValueLabel');
    },
    get_dueDateCustomValidator: function SpottedScript_Admin_InvoiceScreen_View$get_dueDateCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DueDateCustomValidator');
    },
    get_userLabel: function SpottedScript_Admin_InvoiceScreen_View$get_userLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserLabel');
    },
    get_uiUsersAutoComplete: function SpottedScript_Admin_InvoiceScreen_View$get_uiUsersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiUsersAutoCompleteBehaviour');
    },
    get_userDropDownList: function SpottedScript_Admin_InvoiceScreen_View$get_userDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserDropDownList');
    },
    get_userValueLabel: function SpottedScript_Admin_InvoiceScreen_View$get_userValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserValueLabel');
    },
    get_promoterAndUserCustomValidator: function SpottedScript_Admin_InvoiceScreen_View$get_promoterAndUserCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterAndUserCustomValidator');
    },
    get_taxDateLabel: function SpottedScript_Admin_InvoiceScreen_View$get_taxDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TaxDateLabel');
    },
    get_overrideTaxDateCheckBox: function SpottedScript_Admin_InvoiceScreen_View$get_overrideTaxDateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideTaxDateCheckBox');
    },
    get_overrideTaxDatePanel: function SpottedScript_Admin_InvoiceScreen_View$get_overrideTaxDatePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideTaxDatePanel');
    },
    get_taxDateCal: function SpottedScript_Admin_InvoiceScreen_View$get_taxDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_TaxDateCalController');
    },
    get_taxDateValueLabel: function SpottedScript_Admin_InvoiceScreen_View$get_taxDateValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TaxDateValueLabel');
    },
    get_taxDateCustomValidator: function SpottedScript_Admin_InvoiceScreen_View$get_taxDateCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TaxDateCustomValidator');
    },
    get_actionUserLabel: function SpottedScript_Admin_InvoiceScreen_View$get_actionUserLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserLabel');
    },
    get_uiActionUserAutoComplete: function SpottedScript_Admin_InvoiceScreen_View$get_uiActionUserAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiActionUserAutoCompleteBehaviour');
    },
    get_actionUserValueLabel: function SpottedScript_Admin_InvoiceScreen_View$get_actionUserValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserValueLabel');
    },
    get_actionUserRequiredFieldValidator: function SpottedScript_Admin_InvoiceScreen_View$get_actionUserRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserRequiredFieldValidator');
    },
    get_paidDateLabel: function SpottedScript_Admin_InvoiceScreen_View$get_paidDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidDateLabel');
    },
    get_paidDateTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_paidDateTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidDateTextBox');
    },
    get_paidLabel: function SpottedScript_Admin_InvoiceScreen_View$get_paidLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidLabel');
    },
    get_paidCheckBox: function SpottedScript_Admin_InvoiceScreen_View$get_paidCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidCheckBox');
    },
    get_paidImage: function SpottedScript_Admin_InvoiceScreen_View$get_paidImage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidImage');
    },
    get_notPaidImage: function SpottedScript_Admin_InvoiceScreen_View$get_notPaidImage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotPaidImage');
    },
    get_priceLabel: function SpottedScript_Admin_InvoiceScreen_View$get_priceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PriceLabel');
    },
    get_priceTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_priceTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PriceTextBox');
    },
    get_vatCodeLabel: function SpottedScript_Admin_InvoiceScreen_View$get_vatCodeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VATCodeLabel');
    },
    get_vatCodeDropDownList: function SpottedScript_Admin_InvoiceScreen_View$get_vatCodeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VatCodeDropDownList');
    },
    get_vatCodeTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_vatCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VatCodeTextBox');
    },
    get_vatLabel: function SpottedScript_Admin_InvoiceScreen_View$get_vatLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VATLabel');
    },
    get_vatTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_vatTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VATTextBox');
    },
    get_salesUsrLabel: function SpottedScript_Admin_InvoiceScreen_View$get_salesUsrLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrLabel');
    },
    get_salesUsrDropDownList: function SpottedScript_Admin_InvoiceScreen_View$get_salesUsrDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrDropDownList');
    },
    get_salesUsrValueLabel: function SpottedScript_Admin_InvoiceScreen_View$get_salesUsrValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrValueLabel');
    },
    get_totalLabel: function SpottedScript_Admin_InvoiceScreen_View$get_totalLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalLabel');
    },
    get_totalTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_totalTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTextBox');
    },
    get_salesAmountLabel: function SpottedScript_Admin_InvoiceScreen_View$get_salesAmountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesAmountLabel');
    },
    get_salesAmountTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_salesAmountTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesAmountTextBox');
    },
    get_salesAmountCustomValidator: function SpottedScript_Admin_InvoiceScreen_View$get_salesAmountCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesAmountCustomValidator');
    },
    get_amountDueLabel: function SpottedScript_Admin_InvoiceScreen_View$get_amountDueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AmountDueLabel');
    },
    get_amountDueTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_amountDueTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AmountDueTextBox');
    },
    get_purchaseOrderNumberLabel: function SpottedScript_Admin_InvoiceScreen_View$get_purchaseOrderNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PurchaseOrderNumberLabel');
    },
    get_purchaseOrderNumberTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_purchaseOrderNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PurchaseOrderNumberTextBox');
    },
    get_uiAgencyDiscountLabel: function SpottedScript_Admin_InvoiceScreen_View$get_uiAgencyDiscountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAgencyDiscountLabel');
    },
    get_uiAgencyDiscountTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_uiAgencyDiscountTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAgencyDiscountTextBox');
    },
    get_notesLabel: function SpottedScript_Admin_InvoiceScreen_View$get_notesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotesLabel');
    },
    get_notesAddOnlyTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_notesAddOnlyTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotesAddOnlyTextBox');
    },
    get_invoiceItemsPanel: function SpottedScript_Admin_InvoiceScreen_View$get_invoiceItemsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceItemsPanel');
    },
    get_addBannerInvoiceItemRow: function SpottedScript_Admin_InvoiceScreen_View$get_addBannerInvoiceItemRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddBannerInvoiceItemRow');
    },
    get_addBannerInvoiceItemDropDownList: function SpottedScript_Admin_InvoiceScreen_View$get_addBannerInvoiceItemDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddBannerInvoiceItemDropDownList');
    },
    get_addBannerInvoiceItemButton: function SpottedScript_Admin_InvoiceScreen_View$get_addBannerInvoiceItemButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddBannerInvoiceItemButton');
    },
    get_invoiceItemsGridView: function SpottedScript_Admin_InvoiceScreen_View$get_invoiceItemsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceItemsGridView');
    },
    get_invoiceItemsMessageLabel: function SpottedScript_Admin_InvoiceScreen_View$get_invoiceItemsMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceItemsMessageLabel');
    },
    get_transfersPanel: function SpottedScript_Admin_InvoiceScreen_View$get_transfersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransfersPanel');
    },
    get_invoiceTransferGridView: function SpottedScript_Admin_InvoiceScreen_View$get_invoiceTransferGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceTransferGridView');
    },
    get_createTransferHyperLink: function SpottedScript_Admin_InvoiceScreen_View$get_createTransferHyperLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateTransferHyperLink');
    },
    get_searchForTransferHyperLink: function SpottedScript_Admin_InvoiceScreen_View$get_searchForTransferHyperLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchForTransferHyperLink');
    },
    get_creditsPanel: function SpottedScript_Admin_InvoiceScreen_View$get_creditsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditsPanel');
    },
    get_creditInvoiceButton: function SpottedScript_Admin_InvoiceScreen_View$get_creditInvoiceButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditInvoiceButton');
    },
    get_refundInvoiceButton: function SpottedScript_Admin_InvoiceScreen_View$get_refundInvoiceButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundInvoiceButton');
    },
    get_invoiceCreditGridView: function SpottedScript_Admin_InvoiceScreen_View$get_invoiceCreditGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceCreditGridView');
    },
    get_processingVal: function SpottedScript_Admin_InvoiceScreen_View$get_processingVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProcessingVal');
    },
    get_noEditCustomVal: function SpottedScript_Admin_InvoiceScreen_View$get_noEditCustomVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoEditCustomVal');
    },
    get_invoiceValidationSummary: function SpottedScript_Admin_InvoiceScreen_View$get_invoiceValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceValidationSummary');
    },
    get_overrideEmailRecipientTD: function SpottedScript_Admin_InvoiceScreen_View$get_overrideEmailRecipientTD() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideEmailRecipientTD');
    },
    get_overrideEmailRecipientTextBox: function SpottedScript_Admin_InvoiceScreen_View$get_overrideEmailRecipientTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideEmailRecipientTextBox');
    },
    get_emailRecipientCustomValidator: function SpottedScript_Admin_InvoiceScreen_View$get_emailRecipientCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailRecipientCustomValidator');
    },
    get_overrideEmailRecipientsCheckBox: function SpottedScript_Admin_InvoiceScreen_View$get_overrideEmailRecipientsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideEmailRecipientsCheckBox');
    },
    get_emailButton: function SpottedScript_Admin_InvoiceScreen_View$get_emailButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailButton');
    },
    get_emailSentLabel: function SpottedScript_Admin_InvoiceScreen_View$get_emailSentLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailSentLabel');
    },
    get_emailFailedLabel: function SpottedScript_Admin_InvoiceScreen_View$get_emailFailedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailFailedLabel');
    },
    get_downloadButton: function SpottedScript_Admin_InvoiceScreen_View$get_downloadButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DownloadButton');
    },
    get_saveButton: function SpottedScript_Admin_InvoiceScreen_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveButton');
    },
    get_cancelButton: function SpottedScript_Admin_InvoiceScreen_View$get_cancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelButton');
    },
    get_genericContainerPage: function SpottedScript_Admin_InvoiceScreen_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.InvoiceScreen.View.registerClass('SpottedScript.Admin.InvoiceScreen.View', SpottedScript.AdminUserControl.View);
