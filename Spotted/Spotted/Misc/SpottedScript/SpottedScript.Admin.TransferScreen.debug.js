Type.registerNamespace('SpottedScript.Admin.TransferScreen');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.TransferScreen.View
SpottedScript.Admin.TransferScreen.View = function SpottedScript_Admin_TransferScreen_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.TransferScreen.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.TransferScreen.View.prototype = {
    clientId: null,
    get_h14: function SpottedScript_Admin_TransferScreen_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_mainPanel: function SpottedScript_Admin_TransferScreen_View$get_mainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MainPanel');
    },
    get_transferKLabel: function SpottedScript_Admin_TransferScreen_View$get_transferKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferKLabel');
    },
    get_transferKValueLabel: function SpottedScript_Admin_TransferScreen_View$get_transferKValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferKValueLabel');
    },
    get_methodLabel: function SpottedScript_Admin_TransferScreen_View$get_methodLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MethodLabel');
    },
    get_methodDropDownList: function SpottedScript_Admin_TransferScreen_View$get_methodDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MethodDropDownList');
    },
    get_methodTextBox: function SpottedScript_Admin_TransferScreen_View$get_methodTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MethodTextBox');
    },
    get_promoterLabel: function SpottedScript_Admin_TransferScreen_View$get_promoterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterLabel');
    },
    get_uiPromoterAutoComplete: function SpottedScript_Admin_TransferScreen_View$get_uiPromoterAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromoterAutoCompleteBehaviour');
    },
    get_promoterValueLabel: function SpottedScript_Admin_TransferScreen_View$get_promoterValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterValueLabel');
    },
    get_statusLabel: function SpottedScript_Admin_TransferScreen_View$get_statusLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusLabel');
    },
    get_statusDropDownList: function SpottedScript_Admin_TransferScreen_View$get_statusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusDropDownList');
    },
    get_statusTextBox: function SpottedScript_Admin_TransferScreen_View$get_statusTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusTextBox');
    },
    get_userLabel: function SpottedScript_Admin_TransferScreen_View$get_userLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserLabel');
    },
    get_uiUsersAutoComplete: function SpottedScript_Admin_TransferScreen_View$get_uiUsersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiUsersAutoCompleteBehaviour');
    },
    get_userDropDownList: function SpottedScript_Admin_TransferScreen_View$get_userDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserDropDownList');
    },
    get_userValueLabel: function SpottedScript_Admin_TransferScreen_View$get_userValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserValueLabel');
    },
    get_promoterAndUserCustomValidator: function SpottedScript_Admin_TransferScreen_View$get_promoterAndUserCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterAndUserCustomValidator');
    },
    get_amountLabel: function SpottedScript_Admin_TransferScreen_View$get_amountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AmountLabel');
    },
    get_amountTextBox: function SpottedScript_Admin_TransferScreen_View$get_amountTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AmountTextBox');
    },
    get_amountRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_amountRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AmountRequiredFieldValidator');
    },
    get_actionUserLabel: function SpottedScript_Admin_TransferScreen_View$get_actionUserLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserLabel');
    },
    get_uiActionUserAutoComplete: function SpottedScript_Admin_TransferScreen_View$get_uiActionUserAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiActionUserAutoCompleteBehaviour');
    },
    get_actionUserValueLabel: function SpottedScript_Admin_TransferScreen_View$get_actionUserValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserValueLabel');
    },
    get_actionUserRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_actionUserRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUserRequiredFieldValidator');
    },
    get_createdDateLabel: function SpottedScript_Admin_TransferScreen_View$get_createdDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreatedDateLabel');
    },
    get_createdDateTextBox: function SpottedScript_Admin_TransferScreen_View$get_createdDateTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreatedDateTextBox');
    },
    get_typeLabel: function SpottedScript_Admin_TransferScreen_View$get_typeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TypeLabel');
    },
    get_typeDropDownList: function SpottedScript_Admin_TransferScreen_View$get_typeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TypeDropDownList');
    },
    get_typeTextBox: function SpottedScript_Admin_TransferScreen_View$get_typeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TypeTextBox');
    },
    get_completionDateLabel: function SpottedScript_Admin_TransferScreen_View$get_completionDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompletionDateLabel');
    },
    get_completionDateTextBox: function SpottedScript_Admin_TransferScreen_View$get_completionDateTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompletionDateTextBox');
    },
    get_refundLabel: function SpottedScript_Admin_TransferScreen_View$get_refundLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundLabel');
    },
    get_refundHyperLink: function SpottedScript_Admin_TransferScreen_View$get_refundHyperLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundHyperLink');
    },
    get_transferRefundKHiddenTextBox: function SpottedScript_Admin_TransferScreen_View$get_transferRefundKHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferRefundKHiddenTextBox');
    },
    get_notesLabel: function SpottedScript_Admin_TransferScreen_View$get_notesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotesLabel');
    },
    get_notesAddOnlyTextBox: function SpottedScript_Admin_TransferScreen_View$get_notesAddOnlyTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotesAddOnlyTextBox');
    },
    get_cardDetailsPanel: function SpottedScript_Admin_TransferScreen_View$get_cardDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardDetailsPanel');
    },
    get_cardTypeLabel: function SpottedScript_Admin_TransferScreen_View$get_cardTypeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardTypeLabel');
    },
    get_cardTypeDropDownList: function SpottedScript_Admin_TransferScreen_View$get_cardTypeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardTypeDropDownList');
    },
    get_cardTypeTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardTypeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardTypeTextBox');
    },
    get_cardNumberLabel: function SpottedScript_Admin_TransferScreen_View$get_cardNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardNumberLabel');
    },
    get_cardNumberTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardNumberTextBox');
    },
    get_cardNameLabel: function SpottedScript_Admin_TransferScreen_View$get_cardNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardNameLabel');
    },
    get_cardNameTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardNameTextBox');
    },
    get_cardNameValueLabel: function SpottedScript_Admin_TransferScreen_View$get_cardNameValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardNameValueLabel');
    },
    get_cardCV2Label: function SpottedScript_Admin_TransferScreen_View$get_cardCV2Label() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardCV2Label');
    },
    get_cardCV2TextBox: function SpottedScript_Admin_TransferScreen_View$get_cardCV2TextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardCV2TextBox');
    },
    get_cardAddressLabel: function SpottedScript_Admin_TransferScreen_View$get_cardAddressLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardAddressLabel');
    },
    get_cardAddress1TextBox: function SpottedScript_Admin_TransferScreen_View$get_cardAddress1TextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardAddress1TextBox');
    },
    get_cardAddress1ValueLabel: function SpottedScript_Admin_TransferScreen_View$get_cardAddress1ValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardAddress1ValueLabel');
    },
    get_cardStartDateLabel: function SpottedScript_Admin_TransferScreen_View$get_cardStartDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardStartDateLabel');
    },
    get_cardStartDateMonthTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardStartDateMonthTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardStartDateMonthTextBox');
    },
    get_cardStartDateDividerLabel: function SpottedScript_Admin_TransferScreen_View$get_cardStartDateDividerLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardStartDateDividerLabel');
    },
    get_cardStartDateYearTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardStartDateYearTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardStartDateYearTextBox');
    },
    get_cardExpiryDateLabel: function SpottedScript_Admin_TransferScreen_View$get_cardExpiryDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardExpiryDateLabel');
    },
    get_cardExpiryDateMonthTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardExpiryDateMonthTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardExpiryDateMonthTextBox');
    },
    get_cardExpiryDateDividerLabel: function SpottedScript_Admin_TransferScreen_View$get_cardExpiryDateDividerLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardExpiryDateDividerLabel');
    },
    get_cardExpiryDateYearTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardExpiryDateYearTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardExpiryDateYearTextBox');
    },
    get_cardPostCodeLabel: function SpottedScript_Admin_TransferScreen_View$get_cardPostCodeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardPostCodeLabel');
    },
    get_cardPostCodeTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardPostCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardPostCodeTextBox');
    },
    get_cardIssueNumberLabel: function SpottedScript_Admin_TransferScreen_View$get_cardIssueNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardIssueNumberLabel');
    },
    get_cardIssueNumberTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardIssueNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardIssueNumberTextBox');
    },
    get_cardAdminDetailsPanel: function SpottedScript_Admin_TransferScreen_View$get_cardAdminDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardAdminDetailsPanel');
    },
    get_cardAuthorizationCodeLabel: function SpottedScript_Admin_TransferScreen_View$get_cardAuthorizationCodeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardAuthorizationCodeLabel');
    },
    get_cardAuthorizationCodeTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardAuthorizationCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardAuthorizationCodeTextBox');
    },
    get_cardAuthorizationCodeValueLabel: function SpottedScript_Admin_TransferScreen_View$get_cardAuthorizationCodeValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardAuthorizationCodeValueLabel');
    },
    get_cardResponseMessageLabel: function SpottedScript_Admin_TransferScreen_View$get_cardResponseMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseMessageLabel');
    },
    get_cardResponseMessageTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardResponseMessageTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseMessageTextBox');
    },
    get_cardResponseMessageValueLabel: function SpottedScript_Admin_TransferScreen_View$get_cardResponseMessageValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseMessageValueLabel');
    },
    get_cardResponseCV2AVSLabel: function SpottedScript_Admin_TransferScreen_View$get_cardResponseCV2AVSLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseCV2AVSLabel');
    },
    get_cardResponseCV2AVSTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardResponseCV2AVSTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseCV2AVSTextBox');
    },
    get_cardResponseCV2AVSValueLabel: function SpottedScript_Admin_TransferScreen_View$get_cardResponseCV2AVSValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseCV2AVSValueLabel');
    },
    get_cardResponseRespCodeLabel: function SpottedScript_Admin_TransferScreen_View$get_cardResponseRespCodeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseRespCodeLabel');
    },
    get_cardResponseRespCodeTextBox: function SpottedScript_Admin_TransferScreen_View$get_cardResponseRespCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseRespCodeTextBox');
    },
    get_cardResponseRespCodeValueLabel: function SpottedScript_Admin_TransferScreen_View$get_cardResponseRespCodeValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardResponseRespCodeValueLabel');
    },
    get_bankDetailsPanel: function SpottedScript_Admin_TransferScreen_View$get_bankDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankDetailsPanel');
    },
    get_bankNameLabel: function SpottedScript_Admin_TransferScreen_View$get_bankNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankNameLabel');
    },
    get_bankNameTextBox: function SpottedScript_Admin_TransferScreen_View$get_bankNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankNameTextBox');
    },
    get_bankNameValueLabel: function SpottedScript_Admin_TransferScreen_View$get_bankNameValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankNameValueLabel');
    },
    get_bankAccountNumberLabel: function SpottedScript_Admin_TransferScreen_View$get_bankAccountNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNumberLabel');
    },
    get_bankAccountNumberTextBox: function SpottedScript_Admin_TransferScreen_View$get_bankAccountNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNumberTextBox');
    },
    get_bankAccountNumberValueLabel: function SpottedScript_Admin_TransferScreen_View$get_bankAccountNumberValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNumberValueLabel');
    },
    get_bankAccountNameLabel: function SpottedScript_Admin_TransferScreen_View$get_bankAccountNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNameLabel');
    },
    get_bankAccountNameTextBox: function SpottedScript_Admin_TransferScreen_View$get_bankAccountNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNameTextBox');
    },
    get_bankAccountNameValueLabel: function SpottedScript_Admin_TransferScreen_View$get_bankAccountNameValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNameValueLabel');
    },
    get_bankTransferNumberLabel: function SpottedScript_Admin_TransferScreen_View$get_bankTransferNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankTransferNumberLabel');
    },
    get_bankTransferNumberTextBox: function SpottedScript_Admin_TransferScreen_View$get_bankTransferNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankTransferNumberTextBox');
    },
    get_bankTransferNumberValueLabel: function SpottedScript_Admin_TransferScreen_View$get_bankTransferNumberValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankTransferNumberValueLabel');
    },
    get_bankSortCodeLabel: function SpottedScript_Admin_TransferScreen_View$get_bankSortCodeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankSortCodeLabel');
    },
    get_bankSortCodeTextBox: function SpottedScript_Admin_TransferScreen_View$get_bankSortCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankSortCodeTextBox');
    },
    get_bankSortCodeValueLabel: function SpottedScript_Admin_TransferScreen_View$get_bankSortCodeValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankSortCodeValueLabel');
    },
    get_chequeDetailsPanel: function SpottedScript_Admin_TransferScreen_View$get_chequeDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChequeDetailsPanel');
    },
    get_chequeReferenceNumberLabel: function SpottedScript_Admin_TransferScreen_View$get_chequeReferenceNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChequeReferenceNumberLabel');
    },
    get_chequeReferenceNumberTextBox: function SpottedScript_Admin_TransferScreen_View$get_chequeReferenceNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChequeReferenceNumberTextBox');
    },
    get_chequeReferenceNumberValueLabel: function SpottedScript_Admin_TransferScreen_View$get_chequeReferenceNumberValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChequeReferenceNumberValueLabel');
    },
    get_invoiceTransferPanel: function SpottedScript_Admin_TransferScreen_View$get_invoiceTransferPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceTransferPanel');
    },
    get_invoiceCreditLabel: function SpottedScript_Admin_TransferScreen_View$get_invoiceCreditLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceCreditLabel');
    },
    get_invoiceTransferGridView: function SpottedScript_Admin_TransferScreen_View$get_invoiceTransferGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceTransferGridView');
    },
    get_companyDropDownList: function SpottedScript_Admin_TransferScreen_View$get_companyDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompanyDropDownList');
    },
    get_refundTransferPanel: function SpottedScript_Admin_TransferScreen_View$get_refundTransferPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundTransferPanel');
    },
    get_refundTransferLabel: function SpottedScript_Admin_TransferScreen_View$get_refundTransferLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundTransferLabel');
    },
    get_refundTransferGridView: function SpottedScript_Admin_TransferScreen_View$get_refundTransferGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundTransferGridView');
    },
    get_transferValidationSummary: function SpottedScript_Admin_TransferScreen_View$get_transferValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TransferValidationSummary');
    },
    get_cardNameRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_cardNameRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardNameRequiredFieldValidator');
    },
    get_cardAddressRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_cardAddressRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardAddressRequiredFieldValidator');
    },
    get_cardPostCodeRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_cardPostCodeRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardPostCodeRequiredFieldValidator');
    },
    get_cardNumberCustomValidator: function SpottedScript_Admin_TransferScreen_View$get_cardNumberCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardNumberCustomValidator');
    },
    get_cardCV2RequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_cardCV2RequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardCV2RequiredFieldValidator');
    },
    get_cardCV2RegularExpressionValidator: function SpottedScript_Admin_TransferScreen_View$get_cardCV2RegularExpressionValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardCV2RegularExpressionValidator');
    },
    get_cardStartDateCustomValidator: function SpottedScript_Admin_TransferScreen_View$get_cardStartDateCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardStartDateCustomValidator');
    },
    get_cardExpiryDateCustomValidator: function SpottedScript_Admin_TransferScreen_View$get_cardExpiryDateCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardExpiryDateCustomValidator');
    },
    get_bankNameRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_bankNameRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankNameRequiredFieldValidator');
    },
    get_bankAccountNameRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_bankAccountNameRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNameRequiredFieldValidator');
    },
    get_bankSortCodeRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_bankSortCodeRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankSortCodeRequiredFieldValidator');
    },
    get_bankAccountNumberRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_bankAccountNumberRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNumberRequiredFieldValidator');
    },
    get_bankTransferRequiredFieldValidator: function SpottedScript_Admin_TransferScreen_View$get_bankTransferRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankTransferRequiredFieldValidator');
    },
    get_processingCustomValidator: function SpottedScript_Admin_TransferScreen_View$get_processingCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProcessingCustomValidator');
    },
    get_processingVal: function SpottedScript_Admin_TransferScreen_View$get_processingVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProcessingVal');
    },
    get_refundAmountLabel: function SpottedScript_Admin_TransferScreen_View$get_refundAmountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundAmountLabel');
    },
    get_refundAmountTextBox: function SpottedScript_Admin_TransferScreen_View$get_refundAmountTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundAmountTextBox');
    },
    get_refundButton: function SpottedScript_Admin_TransferScreen_View$get_refundButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundButton');
    },
    get_createCampaignCreditsButton: function SpottedScript_Admin_TransferScreen_View$get_createCampaignCreditsButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateCampaignCreditsButton');
    },
    get_downloadButton: function SpottedScript_Admin_TransferScreen_View$get_downloadButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DownloadButton');
    },
    get_saveButton: function SpottedScript_Admin_TransferScreen_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveButton');
    },
    get_cancelButton: function SpottedScript_Admin_TransferScreen_View$get_cancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelButton');
    },
    get_genericContainerPage: function SpottedScript_Admin_TransferScreen_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.TransferScreen.View.registerClass('SpottedScript.Admin.TransferScreen.View', SpottedScript.AdminUserControl.View);
