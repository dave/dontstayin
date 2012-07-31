Type.registerNamespace('SpottedScript.Admin.TicketFundsRelease');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.TicketFundsRelease.View
SpottedScript.Admin.TicketFundsRelease.View = function SpottedScript_Admin_TicketFundsRelease_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.TicketFundsRelease.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.TicketFundsRelease.View.prototype = {
    clientId: null,
    get_allTicketRunsPanel: function SpottedScript_Admin_TicketFundsRelease_View$get_allTicketRunsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllTicketRunsPanel');
    },
    get_selectAwaitingFundsReleaseLinkButton: function SpottedScript_Admin_TicketFundsRelease_View$get_selectAwaitingFundsReleaseLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectAwaitingFundsReleaseLinkButton');
    },
    get_selectLockedFundsLinkButton: function SpottedScript_Admin_TicketFundsRelease_View$get_selectLockedFundsLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectLockedFundsLinkButton');
    },
    get_selectAllTicketFundsLinkButton: function SpottedScript_Admin_TicketFundsRelease_View$get_selectAllTicketFundsLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectAllTicketFundsLinkButton');
    },
    get_searchTable: function SpottedScript_Admin_TicketFundsRelease_View$get_searchTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTable');
    },
    get_uiPromotersAutoComplete: function SpottedScript_Admin_TicketFundsRelease_View$get_uiPromotersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromotersAutoCompleteBehaviour');
    },
    get_fromDateCal: function SpottedScript_Admin_TicketFundsRelease_View$get_fromDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_FromDateCalController');
    },
    get_showZeroMoneyTicketRunsCheckBox: function SpottedScript_Admin_TicketFundsRelease_View$get_showZeroMoneyTicketRunsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ShowZeroMoneyTicketRunsCheckBox');
    },
    get_salesUsrDropDownList: function SpottedScript_Admin_TicketFundsRelease_View$get_salesUsrDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrDropDownList');
    },
    get_toDateCal: function SpottedScript_Admin_TicketFundsRelease_View$get_toDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_ToDateCalController');
    },
    get_statusDropDownList: function SpottedScript_Admin_TicketFundsRelease_View$get_statusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusDropDownList');
    },
    get_searchButton: function SpottedScript_Admin_TicketFundsRelease_View$get_searchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchButton');
    },
    get_clearButton: function SpottedScript_Admin_TicketFundsRelease_View$get_clearButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClearButton');
    },
    get_salesPersonQuickButtonTable: function SpottedScript_Admin_TicketFundsRelease_View$get_salesPersonQuickButtonTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesPersonQuickButtonTable');
    },
    get_myPromotersRadioButton: function SpottedScript_Admin_TicketFundsRelease_View$get_myPromotersRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyPromotersRadioButton');
    },
    get_myPromotersWithFundsNotSpentRadioButton: function SpottedScript_Admin_TicketFundsRelease_View$get_myPromotersWithFundsNotSpentRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyPromotersWithFundsNotSpentRadioButton');
    },
    get_myPromotersWithFundsNotReleasedRadioButton: function SpottedScript_Admin_TicketFundsRelease_View$get_myPromotersWithFundsNotReleasedRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyPromotersWithFundsNotReleasedRadioButton');
    },
    get_myPromotersWithFundsSoonToBeReleasedRadioButton: function SpottedScript_Admin_TicketFundsRelease_View$get_myPromotersWithFundsSoonToBeReleasedRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyPromotersWithFundsSoonToBeReleasedRadioButton');
    },
    get_ticketPromoterEventFundsGridView: function SpottedScript_Admin_TicketFundsRelease_View$get_ticketPromoterEventFundsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPromoterEventFundsGridView');
    },
    get_searchResultsMessageLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_searchResultsMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsMessageLabel');
    },
    get_totalsP: function SpottedScript_Admin_TicketFundsRelease_View$get_totalsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalsP');
    },
    get_totalFundsLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_totalFundsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalFundsLabel');
    },
    get_totalBookingFeesLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_totalBookingFeesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalBookingFeesLabel');
    },
    get_totalTicketsLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_totalTicketsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTicketsLabel');
    },
    get_paginationPanel: function SpottedScript_Admin_TicketFundsRelease_View$get_paginationPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaginationPanel');
    },
    get_prevPageLinkButton: function SpottedScript_Admin_TicketFundsRelease_View$get_prevPageLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrevPageLinkButton');
    },
    get_nextPageLinkButton: function SpottedScript_Admin_TicketFundsRelease_View$get_nextPageLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextPageLinkButton');
    },
    get_ticketPromoterEventDetailsPanel: function SpottedScript_Admin_TicketFundsRelease_View$get_ticketPromoterEventDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPromoterEventDetailsPanel');
    },
    get_h1EditTitle: function SpottedScript_Admin_TicketFundsRelease_View$get_h1EditTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1EditTitle');
    },
    get_eventLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_eventLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLabel');
    },
    get_promoterTicketsEnabledLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_promoterTicketsEnabledLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterTicketsEnabledLabel');
    },
    get_eventEndedLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_eventEndedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventEndedLabel');
    },
    get_validUserResponsesLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_validUserResponsesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ValidUserResponsesLabel');
    },
    get_validIPDuplicateLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_validIPDuplicateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ValidIPDuplicateLabel');
    },
    get_validIPCountryLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_validIPCountryLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ValidIPCountryLabel');
    },
    get_validBrowserGuidsLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_validBrowserGuidsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ValidBrowserGuidsLabel');
    },
    get_promoterLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_promoterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterLabel');
    },
    get_ticketRunsLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_ticketRunsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunsLabel');
    },
    get_amountLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_amountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AmountLabel');
    },
    get_totalVatLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_totalVatLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalVatLabel');
    },
    get_bookingFeesLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_bookingFeesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookingFeesLabel');
    },
    get_lockTextRow: function SpottedScript_Admin_TicketFundsRelease_View$get_lockTextRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LockTextRow');
    },
    get_lockTextLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_lockTextLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LockTextLabel');
    },
    get_manualLockRow: function SpottedScript_Admin_TicketFundsRelease_View$get_manualLockRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManualLockRow');
    },
    get_manualLockLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_manualLockLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManualLockLabel');
    },
    get_manualLockNotSetDiv: function SpottedScript_Admin_TicketFundsRelease_View$get_manualLockNotSetDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManualLockNotSetDiv');
    },
    get_manualLockNoteTextBox: function SpottedScript_Admin_TicketFundsRelease_View$get_manualLockNoteTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManualLockNoteTextBox');
    },
    get_manualLockRequiredFieldValidator: function SpottedScript_Admin_TicketFundsRelease_View$get_manualLockRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManualLockRequiredFieldValidator');
    },
    get_manualLockButton: function SpottedScript_Admin_TicketFundsRelease_View$get_manualLockButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManualLockButton');
    },
    get_overrideLockRow: function SpottedScript_Admin_TicketFundsRelease_View$get_overrideLockRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideLockRow');
    },
    get_overrideLockLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_overrideLockLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideLockLabel');
    },
    get_overrideLockNotSetDiv: function SpottedScript_Admin_TicketFundsRelease_View$get_overrideLockNotSetDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideLockNotSetDiv');
    },
    get_overrideLockNoteTextBox: function SpottedScript_Admin_TicketFundsRelease_View$get_overrideLockNoteTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideLockNoteTextBox');
    },
    get_overrideTextRequiredFieldValidator: function SpottedScript_Admin_TicketFundsRelease_View$get_overrideTextRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideTextRequiredFieldValidator');
    },
    get_overrideLockButton: function SpottedScript_Admin_TicketFundsRelease_View$get_overrideLockButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideLockButton');
    },
    get_rerunFundsLocksChecksRow: function SpottedScript_Admin_TicketFundsRelease_View$get_rerunFundsLocksChecksRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RerunFundsLocksChecksRow');
    },
    get_rerunFundsLocksChecksButton: function SpottedScript_Admin_TicketFundsRelease_View$get_rerunFundsLocksChecksButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RerunFundsLocksChecksButton');
    },
    get_fundsReleasedRow: function SpottedScript_Admin_TicketFundsRelease_View$get_fundsReleasedRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsReleasedRow');
    },
    get_fundsReleasedLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_fundsReleasedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsReleasedLabel');
    },
    get_releaseTransferLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_releaseTransferLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReleaseTransferLabel');
    },
    get_ticketFundsAppliedRow: function SpottedScript_Admin_TicketFundsRelease_View$get_ticketFundsAppliedRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsAppliedRow');
    },
    get_invoicesAppliedToLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_invoicesAppliedToLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoicesAppliedToLabel');
    },
    get_ticketPaymentRow: function SpottedScript_Admin_TicketFundsRelease_View$get_ticketPaymentRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPaymentRow');
    },
    get_ticketPaymentMadeLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_ticketPaymentMadeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPaymentMadeLabel');
    },
    get_ticketPaymentTransferLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_ticketPaymentTransferLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPaymentTransferLabel');
    },
    get_payFundsToPromoterBankAccountRow: function SpottedScript_Admin_TicketFundsRelease_View$get_payFundsToPromoterBankAccountRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayFundsToPromoterBankAccountRow');
    },
    get_payFundsToPromoterBankAccountButton: function SpottedScript_Admin_TicketFundsRelease_View$get_payFundsToPromoterBankAccountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayFundsToPromoterBankAccountButton');
    },
    get_createCampaignCreditsButton: function SpottedScript_Admin_TicketFundsRelease_View$get_createCampaignCreditsButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateCampaignCreditsButton');
    },
    get_processingVal: function SpottedScript_Admin_TicketFundsRelease_View$get_processingVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProcessingVal');
    },
    get_ticketFundsReleaseValidationSummary: function SpottedScript_Admin_TicketFundsRelease_View$get_ticketFundsReleaseValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsReleaseValidationSummary');
    },
    get_getLatestEventsWithTicketsButton: function SpottedScript_Admin_TicketFundsRelease_View$get_getLatestEventsWithTicketsButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GetLatestEventsWithTicketsButton');
    },
    get_releaseFundsOptionsJavascriptLabel: function SpottedScript_Admin_TicketFundsRelease_View$get_releaseFundsOptionsJavascriptLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReleaseFundsOptionsJavascriptLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_TicketFundsRelease_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.TicketFundsRelease.View.registerClass('SpottedScript.Admin.TicketFundsRelease.View', SpottedScript.AdminUserControl.View);
