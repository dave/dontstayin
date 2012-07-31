Type.registerNamespace('SpottedScript.Admin.CreditScreen');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.CreditScreen.View
SpottedScript.Admin.CreditScreen.View = function SpottedScript_Admin_CreditScreen_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.CreditScreen.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.CreditScreen.View.prototype = {
    clientId: null,
    get_h1_1: function SpottedScript_Admin_CreditScreen_View$get_h1_1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1_1');
    },
    get_mainPanel: function SpottedScript_Admin_CreditScreen_View$get_mainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MainPanel');
    },
    get_creditKLabel: function SpottedScript_Admin_CreditScreen_View$get_creditKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditKLabel');
    },
    get_creditKValueLabel: function SpottedScript_Admin_CreditScreen_View$get_creditKValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditKValueLabel');
    },
    get_creditKTextBox: function SpottedScript_Admin_CreditScreen_View$get_creditKTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditKTextBox');
    },
    get_createdDateLabel: function SpottedScript_Admin_CreditScreen_View$get_createdDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreatedDateLabel');
    },
    get_createdDateTextBox: function SpottedScript_Admin_CreditScreen_View$get_createdDateTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreatedDateTextBox');
    },
    get_invoiceKLabel: function SpottedScript_Admin_CreditScreen_View$get_invoiceKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceKLabel');
    },
    get_invoiceKValueLabel: function SpottedScript_Admin_CreditScreen_View$get_invoiceKValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceKValueLabel');
    },
    get_viewInvoiceHyperLink: function SpottedScript_Admin_CreditScreen_View$get_viewInvoiceHyperLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewInvoiceHyperLink');
    },
    get_paidDateLabel: function SpottedScript_Admin_CreditScreen_View$get_paidDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidDateLabel');
    },
    get_paidDateTextBox: function SpottedScript_Admin_CreditScreen_View$get_paidDateTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidDateTextBox');
    },
    get_promoterLabel: function SpottedScript_Admin_CreditScreen_View$get_promoterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterLabel');
    },
    get_promoterValueLabel: function SpottedScript_Admin_CreditScreen_View$get_promoterValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterValueLabel');
    },
    get_promoterKHiddenTextBox: function SpottedScript_Admin_CreditScreen_View$get_promoterKHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterKHiddenTextBox');
    },
    get_taxDateLabel: function SpottedScript_Admin_CreditScreen_View$get_taxDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TaxDateLabel');
    },
    get_overrideTaxDateCheckBox: function SpottedScript_Admin_CreditScreen_View$get_overrideTaxDateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideTaxDateCheckBox');
    },
    get_overrideTaxDatePanel: function SpottedScript_Admin_CreditScreen_View$get_overrideTaxDatePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideTaxDatePanel');
    },
    get_taxDateCal: function SpottedScript_Admin_CreditScreen_View$get_taxDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_TaxDateCalController');
    },
    get_taxDateValueLabel: function SpottedScript_Admin_CreditScreen_View$get_taxDateValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TaxDateValueLabel');
    },
    get_taxDateCustomValidator: function SpottedScript_Admin_CreditScreen_View$get_taxDateCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TaxDateCustomValidator');
    },
    get_userLabel: function SpottedScript_Admin_CreditScreen_View$get_userLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserLabel');
    },
    get_userValueLabel: function SpottedScript_Admin_CreditScreen_View$get_userValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserValueLabel');
    },
    get_userKHiddenTextBox: function SpottedScript_Admin_CreditScreen_View$get_userKHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserKHiddenTextBox');
    },
    get_priceLabel: function SpottedScript_Admin_CreditScreen_View$get_priceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PriceLabel');
    },
    get_priceTextBox: function SpottedScript_Admin_CreditScreen_View$get_priceTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PriceTextBox');
    },
    get_actionUserLabel: function SpottedScript_Admin_CreditScreen_View$get_actionUserLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserLabel');
    },
    get_actionUserValueLabel: function SpottedScript_Admin_CreditScreen_View$get_actionUserValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserValueLabel');
    },
    get_actionUserKHiddenTextBox: function SpottedScript_Admin_CreditScreen_View$get_actionUserKHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserKHiddenTextBox');
    },
    get_vatLabel: function SpottedScript_Admin_CreditScreen_View$get_vatLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VATLabel');
    },
    get_vatTextBox: function SpottedScript_Admin_CreditScreen_View$get_vatTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VATTextBox');
    },
    get_paidLabel: function SpottedScript_Admin_CreditScreen_View$get_paidLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidLabel');
    },
    get_paidCheckBox: function SpottedScript_Admin_CreditScreen_View$get_paidCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidCheckBox');
    },
    get_paidImage: function SpottedScript_Admin_CreditScreen_View$get_paidImage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidImage');
    },
    get_notPaidImage: function SpottedScript_Admin_CreditScreen_View$get_notPaidImage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotPaidImage');
    },
    get_totalLabel: function SpottedScript_Admin_CreditScreen_View$get_totalLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalLabel');
    },
    get_totalTextBox: function SpottedScript_Admin_CreditScreen_View$get_totalTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTextBox');
    },
    get_vatCodeLabel: function SpottedScript_Admin_CreditScreen_View$get_vatCodeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VATCodeLabel');
    },
    get_vatCodeTextBox: function SpottedScript_Admin_CreditScreen_View$get_vatCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VATCodeTextBox');
    },
    get_vatCodeNumberHiddenTextBox: function SpottedScript_Admin_CreditScreen_View$get_vatCodeNumberHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VATCodeNumberHiddenTextBox');
    },
    get_salesUsrLabel: function SpottedScript_Admin_CreditScreen_View$get_salesUsrLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrLabel');
    },
    get_salesUsrValueLabel: function SpottedScript_Admin_CreditScreen_View$get_salesUsrValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrValueLabel');
    },
    get_salesUserKHiddenTextBox: function SpottedScript_Admin_CreditScreen_View$get_salesUserKHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUserKHiddenTextBox');
    },
    get_salesAmountLabel: function SpottedScript_Admin_CreditScreen_View$get_salesAmountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesAmountLabel');
    },
    get_salesAmountTextBox: function SpottedScript_Admin_CreditScreen_View$get_salesAmountTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesAmountTextBox');
    },
    get_salesAmountCustomValidator: function SpottedScript_Admin_CreditScreen_View$get_salesAmountCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesAmountCustomValidator');
    },
    get_notesLabel: function SpottedScript_Admin_CreditScreen_View$get_notesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotesLabel');
    },
    get_notesAddOnlyTextBox: function SpottedScript_Admin_CreditScreen_View$get_notesAddOnlyTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotesAddOnlyTextBox');
    },
    get_creditItemsPanel: function SpottedScript_Admin_CreditScreen_View$get_creditItemsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditItemsPanel');
    },
    get_creditItemsGridView: function SpottedScript_Admin_CreditScreen_View$get_creditItemsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditItemsGridView');
    },
    get_transfersPanel: function SpottedScript_Admin_CreditScreen_View$get_transfersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransfersPanel');
    },
    get_creditTransferGridView: function SpottedScript_Admin_CreditScreen_View$get_creditTransferGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditTransferGridView');
    },
    get_createTransferHyperLink: function SpottedScript_Admin_CreditScreen_View$get_createTransferHyperLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateTransferHyperLink');
    },
    get_searchForTransferHyperLink: function SpottedScript_Admin_CreditScreen_View$get_searchForTransferHyperLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchForTransferHyperLink');
    },
    get_processingVal: function SpottedScript_Admin_CreditScreen_View$get_processingVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProcessingVal');
    },
    get_creditValidationSummary: function SpottedScript_Admin_CreditScreen_View$get_creditValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditValidationSummary');
    },
    get_downloadButton: function SpottedScript_Admin_CreditScreen_View$get_downloadButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DownloadButton');
    },
    get_saveButton: function SpottedScript_Admin_CreditScreen_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveButton');
    },
    get_cancelButton: function SpottedScript_Admin_CreditScreen_View$get_cancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelButton');
    },
    get_genericContainerPage: function SpottedScript_Admin_CreditScreen_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.CreditScreen.View.registerClass('SpottedScript.Admin.CreditScreen.View', SpottedScript.AdminUserControl.View);
