Type.registerNamespace('SpottedScript.Pages.Promoters.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Home.View
SpottedScript.Pages.Promoters.Home.View = function SpottedScript_Pages_Promoters_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Home.View.prototype = {
    clientId: null,
    get_panelNotEnabled: function SpottedScript_Pages_Promoters_Home_View$get_panelNotEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNotEnabled');
    },
    get_panelEnabled: function SpottedScript_Pages_Promoters_Home_View$get_panelEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEnabled');
    },
    get_promoterIntro: function SpottedScript_Pages_Promoters_Home_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_usersPanel: function SpottedScript_Pages_Promoters_Home_View$get_usersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsersPanel');
    },
    get_usersRepeater: function SpottedScript_Pages_Promoters_Home_View$get_usersRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsersRepeater');
    },
    get_venuesPanel: function SpottedScript_Pages_Promoters_Home_View$get_venuesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesPanel');
    },
    get_noVenuesPanel: function SpottedScript_Pages_Promoters_Home_View$get_noVenuesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoVenuesPanel');
    },
    get_venueDataGrid: function SpottedScript_Pages_Promoters_Home_View$get_venueDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueDataGrid');
    },
    get_brandsPanel: function SpottedScript_Pages_Promoters_Home_View$get_brandsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandsPanel');
    },
    get_noBrandsPanel: function SpottedScript_Pages_Promoters_Home_View$get_noBrandsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoBrandsPanel');
    },
    get_brandDataGrid: function SpottedScript_Pages_Promoters_Home_View$get_brandDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandDataGrid');
    },
    get_guestlistPanel: function SpottedScript_Pages_Promoters_Home_View$get_guestlistPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GuestlistPanel');
    },
    get_noGuestlistPanel: function SpottedScript_Pages_Promoters_Home_View$get_noGuestlistPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoGuestlistPanel');
    },
    get_eventsPanel: function SpottedScript_Pages_Promoters_Home_View$get_eventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsPanel');
    },
    get_eventsGridView: function SpottedScript_Pages_Promoters_Home_View$get_eventsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsGridView');
    },
    get_eventsPanelEventsNoEvents: function SpottedScript_Pages_Promoters_Home_View$get_eventsPanelEventsNoEvents() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsPanelEventsNoEvents');
    },
    get_eventsPanelEvents: function SpottedScript_Pages_Promoters_Home_View$get_eventsPanelEvents() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsPanelEvents');
    },
    get_adminPanel: function SpottedScript_Pages_Promoters_Home_View$get_adminPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminPanel');
    },
    get_adminH1: function SpottedScript_Pages_Promoters_Home_View$get_adminH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminH1');
    },
    get_adminDiv: function SpottedScript_Pages_Promoters_Home_View$get_adminDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminDiv');
    },
    get_statusP: function SpottedScript_Pages_Promoters_Home_View$get_statusP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusP');
    },
    get_salesAccountTable: function SpottedScript_Pages_Promoters_Home_View$get_salesAccountTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesAccountTable');
    },
    get_salesUsrTD1: function SpottedScript_Pages_Promoters_Home_View$get_salesUsrTD1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrTD1');
    },
    get_salesStatusTD1: function SpottedScript_Pages_Promoters_Home_View$get_salesStatusTD1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesStatusTD1');
    },
    get_salesStatusExpiresTD1: function SpottedScript_Pages_Promoters_Home_View$get_salesStatusExpiresTD1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesStatusExpiresTD1');
    },
    get_salesUsrTD2: function SpottedScript_Pages_Promoters_Home_View$get_salesUsrTD2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrTD2');
    },
    get_salesPersonsDropDownList: function SpottedScript_Pages_Promoters_Home_View$get_salesPersonsDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesPersonsDropDownList');
    },
    get_salesStatusTD2: function SpottedScript_Pages_Promoters_Home_View$get_salesStatusTD2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesStatusTD2');
    },
    get_salesStatusDropDownList: function SpottedScript_Pages_Promoters_Home_View$get_salesStatusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesStatusDropDownList');
    },
    get_salesStatusExpiresTD2: function SpottedScript_Pages_Promoters_Home_View$get_salesStatusExpiresTD2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesStatusExpiresTD2');
    },
    get_salesStatusExpiresCal: function SpottedScript_Pages_Promoters_Home_View$get_salesStatusExpiresCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_SalesStatusExpiresCalController');
    },
    get_saveSalesAccountButton: function SpottedScript_Pages_Promoters_Home_View$get_saveSalesAccountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveSalesAccountButton');
    },
    get_salesAccountSavedLabel: function SpottedScript_Pages_Promoters_Home_View$get_salesAccountSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesAccountSavedLabel');
    },
    get_adminUsrP: function SpottedScript_Pages_Promoters_Home_View$get_adminUsrP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminUsrP');
    },
    get_importantCallsPanel: function SpottedScript_Pages_Promoters_Home_View$get_importantCallsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImportantCallsPanel');
    },
    get_importantCallsGridView: function SpottedScript_Pages_Promoters_Home_View$get_importantCallsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImportantCallsGridView');
    },
    get_callsPanel: function SpottedScript_Pages_Promoters_Home_View$get_callsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CallsPanel');
    },
    get_callsDiv: function SpottedScript_Pages_Promoters_Home_View$get_callsDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CallsDiv');
    },
    get_callsGridView: function SpottedScript_Pages_Promoters_Home_View$get_callsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CallsGridView');
    },
    get_noteTextBox: function SpottedScript_Pages_Promoters_Home_View$get_noteTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoteTextBox');
    },
    get_importantNoteCheckBox: function SpottedScript_Pages_Promoters_Home_View$get_importantNoteCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImportantNoteCheckBox');
    },
    get_saveNoteButton: function SpottedScript_Pages_Promoters_Home_View$get_saveNoteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveNoteButton');
    },
    get_noteSavedLabel: function SpottedScript_Pages_Promoters_Home_View$get_noteSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoteSavedLabel');
    },
    get_salesHoldLabel: function SpottedScript_Pages_Promoters_Home_View$get_salesHoldLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesHoldLabel');
    },
    get_nextCallCal: function SpottedScript_Pages_Promoters_Home_View$get_nextCallCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_NextCallCalController');
    },
    get_nextCallTime: function SpottedScript_Pages_Promoters_Home_View$get_nextCallTime() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextCallTime');
    },
    get_snoozeDropDownList: function SpottedScript_Pages_Promoters_Home_View$get_snoozeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SnoozeDropDownList');
    },
    get_nextCallTimeVal: function SpottedScript_Pages_Promoters_Home_View$get_nextCallTimeVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextCallTimeVal');
    },
    get_alarmCheckBox: function SpottedScript_Pages_Promoters_Home_View$get_alarmCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AlarmCheckBox');
    },
    get_nextCallSaveButton: function SpottedScript_Pages_Promoters_Home_View$get_nextCallSaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextCallSaveButton');
    },
    get_button1: function SpottedScript_Pages_Promoters_Home_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_saveNextCallDoneLabel: function SpottedScript_Pages_Promoters_Home_View$get_saveNextCallDoneLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveNextCallDoneLabel');
    },
    get_saveNextCallErrorLabel: function SpottedScript_Pages_Promoters_Home_View$get_saveNextCallErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveNextCallErrorLabel');
    },
    get_salesEstimate: function SpottedScript_Pages_Promoters_Home_View$get_salesEstimate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesEstimate');
    },
    get_salesEstimateSavedLabel: function SpottedScript_Pages_Promoters_Home_View$get_salesEstimateSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesEstimateSavedLabel');
    },
    get_adminPhoneNumbersDropDown: function SpottedScript_Pages_Promoters_Home_View$get_adminPhoneNumbersDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminPhoneNumbersDropDown');
    },
    get_salesCallButton: function SpottedScript_Pages_Promoters_Home_View$get_salesCallButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesCallButton');
    },
    get_miscCallButton: function SpottedScript_Pages_Promoters_Home_View$get_miscCallButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MiscCallButton');
    },
    get_changeNumberButton: function SpottedScript_Pages_Promoters_Home_View$get_changeNumberButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChangeNumberButton');
    },
    get_salesCallError: function SpottedScript_Pages_Promoters_Home_View$get_salesCallError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesCallError');
    },
    get_takeIncomingCallButton: function SpottedScript_Pages_Promoters_Home_View$get_takeIncomingCallButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TakeIncomingCallButton');
    },
    get_incomingCallError: function SpottedScript_Pages_Promoters_Home_View$get_incomingCallError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IncomingCallError');
    },
    get_activateButton: function SpottedScript_Pages_Promoters_Home_View$get_activateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActivateButton');
    },
    get_disableButton: function SpottedScript_Pages_Promoters_Home_View$get_disableButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DisableButton');
    },
    get_noBrandsLabel: function SpottedScript_Pages_Promoters_Home_View$get_noBrandsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoBrandsLabel');
    },
    get_adminBrandRepeater: function SpottedScript_Pages_Promoters_Home_View$get_adminBrandRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminBrandRepeater');
    },
    get_noVenuesLabel: function SpottedScript_Pages_Promoters_Home_View$get_noVenuesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoVenuesLabel');
    },
    get_adminVenueRepeater: function SpottedScript_Pages_Promoters_Home_View$get_adminVenueRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminVenueRepeater');
    },
    get_salesSummaryPanel: function SpottedScript_Pages_Promoters_Home_View$get_salesSummaryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesSummaryPanel');
    },
    get_salesSummaryTable: function SpottedScript_Pages_Promoters_Home_View$get_salesSummaryTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesSummaryTable');
    },
    get_ticketSalesSummaryTable: function SpottedScript_Pages_Promoters_Home_View$get_ticketSalesSummaryTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketSalesSummaryTable');
    },
    get_totalTicketRunsLabel: function SpottedScript_Pages_Promoters_Home_View$get_totalTicketRunsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTicketRunsLabel');
    },
    get_totalTicketsSoldLabel: function SpottedScript_Pages_Promoters_Home_View$get_totalTicketsSoldLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTicketsSoldLabel');
    },
    get_ticketFundsReleasedLabel: function SpottedScript_Pages_Promoters_Home_View$get_ticketFundsReleasedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsReleasedLabel');
    },
    get_ticketFundsInWaitingLabel: function SpottedScript_Pages_Promoters_Home_View$get_ticketFundsInWaitingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsInWaitingLabel');
    },
    get_ticketFundsAvailableLabel: function SpottedScript_Pages_Promoters_Home_View$get_ticketFundsAvailableLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsAvailableLabel');
    },
    get_ticketFundsToCampaignCreditsRow: function SpottedScript_Pages_Promoters_Home_View$get_ticketFundsToCampaignCreditsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsToCampaignCreditsRow');
    },
    get_createCampaignCreditsButton: function SpottedScript_Pages_Promoters_Home_View$get_createCampaignCreditsButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateCampaignCreditsButton');
    },
    get_createCampaignCreditsResponseLabel: function SpottedScript_Pages_Promoters_Home_View$get_createCampaignCreditsResponseLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreateCampaignCreditsResponseLabel');
    },
    get_adminEditPanel: function SpottedScript_Pages_Promoters_Home_View$get_adminEditPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminEditPanel');
    },
    get_creditLimitTextBox: function SpottedScript_Pages_Promoters_Home_View$get_creditLimitTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditLimitTextBox');
    },
    get_creditLimitCustomValidator: function SpottedScript_Pages_Promoters_Home_View$get_creditLimitCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditLimitCustomValidator');
    },
    get_invoiceDueDaysTextBox: function SpottedScript_Pages_Promoters_Home_View$get_invoiceDueDaysTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceDueDaysTextBox');
    },
    get_invoiceDueDaysRangeValidator: function SpottedScript_Pages_Promoters_Home_View$get_invoiceDueDaysRangeValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceDueDaysRangeValidator');
    },
    get_overrideInvoiceDueDaysCheckBox: function SpottedScript_Pages_Promoters_Home_View$get_overrideInvoiceDueDaysCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideInvoiceDueDaysCheckBox');
    },
    get_enableTicketsCheckBox: function SpottedScript_Pages_Promoters_Home_View$get_enableTicketsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EnableTicketsCheckBox');
    },
    get_saveEditPromoterCustomVal: function SpottedScript_Pages_Promoters_Home_View$get_saveEditPromoterCustomVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveEditPromoterCustomVal');
    },
    get_uiEnableSuppressReminderEmailCheckBox: function SpottedScript_Pages_Promoters_Home_View$get_uiEnableSuppressReminderEmailCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEnableSuppressReminderEmailCheckBox');
    },
    get_disableOverdueRedirectCheckBox: function SpottedScript_Pages_Promoters_Home_View$get_disableOverdueRedirectCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DisableOverdueRedirectCheckBox');
    },
    get_overrideAutoApplyTicketFundsToInvoicesCheckBox: function SpottedScript_Pages_Promoters_Home_View$get_overrideAutoApplyTicketFundsToInvoicesCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideAutoApplyTicketFundsToInvoicesCheckBox');
    },
    get_discountTextBox: function SpottedScript_Pages_Promoters_Home_View$get_discountTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscountTextBox');
    },
    get_rangeValidator1: function SpottedScript_Pages_Promoters_Home_View$get_rangeValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RangeValidator1');
    },
    get_saveEditPromoterButton: function SpottedScript_Pages_Promoters_Home_View$get_saveEditPromoterButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveEditPromoterButton');
    },
    get_initSkeletonAccountPanel: function SpottedScript_Pages_Promoters_Home_View$get_initSkeletonAccountPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitSkeletonAccountPanel');
    },
    get_uiInitSkeletonAccountAutoComplete: function SpottedScript_Pages_Promoters_Home_View$get_uiInitSkeletonAccountAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiInitSkeletonAccountAutoCompleteBehaviour');
    },
    get_initSkeletonAccountCodeLabel: function SpottedScript_Pages_Promoters_Home_View$get_initSkeletonAccountCodeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitSkeletonAccountCodeLabel');
    },
    get_h12: function SpottedScript_Pages_Promoters_Home_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_noEventsPanel: function SpottedScript_Pages_Promoters_Home_View$get_noEventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoEventsPanel');
    },
    get_h15f: function SpottedScript_Pages_Promoters_Home_View$get_h15f() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15f');
    },
    get_introOfferPanel: function SpottedScript_Pages_Promoters_Home_View$get_introOfferPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroOfferPanel');
    },
    get_h15fg: function SpottedScript_Pages_Promoters_Home_View$get_h15fg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15fg');
    },
    get_offerExpireTimeSpan: function SpottedScript_Pages_Promoters_Home_View$get_offerExpireTimeSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OfferExpireTimeSpan');
    },
    get_salesUsrPanel: function SpottedScript_Pages_Promoters_Home_View$get_salesUsrPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrPanel');
    },
    get_salesUsrDSIPhotoLinkLabel: function SpottedScript_Pages_Promoters_Home_View$get_salesUsrDSIPhotoLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrDSIPhotoLinkLabel');
    },
    get_salesUsrNameLabel: function SpottedScript_Pages_Promoters_Home_View$get_salesUsrNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrNameLabel');
    },
    get_salesUsrNumberLabel: function SpottedScript_Pages_Promoters_Home_View$get_salesUsrNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrNumberLabel');
    },
    get_salesUsrEmailLabel: function SpottedScript_Pages_Promoters_Home_View$get_salesUsrEmailLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrEmailLabel');
    },
    get_salesUsrDSILinkLabel: function SpottedScript_Pages_Promoters_Home_View$get_salesUsrDSILinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesUsrDSILinkLabel');
    },
    get_accountBalanceLabel: function SpottedScript_Pages_Promoters_Home_View$get_accountBalanceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccountBalanceLabel');
    },
    get_creditLabel: function SpottedScript_Pages_Promoters_Home_View$get_creditLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditLabel');
    },
    get_ticketFundsP: function SpottedScript_Pages_Promoters_Home_View$get_ticketFundsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsP');
    },
    get_invoicesOutstandingP: function SpottedScript_Pages_Promoters_Home_View$get_invoicesOutstandingP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoicesOutstandingP');
    },
    get_invoicesOutstandingPayP: function SpottedScript_Pages_Promoters_Home_View$get_invoicesOutstandingPayP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoicesOutstandingPayP');
    },
    get_h15: function SpottedScript_Pages_Promoters_Home_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_sellTicketsDisabled: function SpottedScript_Pages_Promoters_Home_View$get_sellTicketsDisabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SellTicketsDisabled');
    },
    get_sellTicketsEnabled: function SpottedScript_Pages_Promoters_Home_View$get_sellTicketsEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SellTicketsEnabled');
    },
    get_uiConfirmCardDetailsLink: function SpottedScript_Pages_Promoters_Home_View$get_uiConfirmCardDetailsLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiConfirmCardDetailsLink');
    },
    get_h15sadf: function SpottedScript_Pages_Promoters_Home_View$get_h15sadf() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15sadf');
    },
    get_h1: function SpottedScript_Pages_Promoters_Home_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_quickViewPanel: function SpottedScript_Pages_Promoters_Home_View$get_quickViewPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuickViewPanel');
    },
    get_h14: function SpottedScript_Pages_Promoters_Home_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_brandsPanelOuter: function SpottedScript_Pages_Promoters_Home_View$get_brandsPanelOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandsPanelOuter');
    },
    get_venuesPanelOuter: function SpottedScript_Pages_Promoters_Home_View$get_venuesPanelOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesPanelOuter');
    },
    get_domainsPanelOuter: function SpottedScript_Pages_Promoters_Home_View$get_domainsPanelOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DomainsPanelOuter');
    },
    get_domainsPanel: function SpottedScript_Pages_Promoters_Home_View$get_domainsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DomainsPanel');
    },
    get_domainsDataGrid: function SpottedScript_Pages_Promoters_Home_View$get_domainsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DomainsDataGrid');
    },
    get_noDomainsPanel: function SpottedScript_Pages_Promoters_Home_View$get_noDomainsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoDomainsPanel');
    },
    get_moreOptionsPanel: function SpottedScript_Pages_Promoters_Home_View$get_moreOptionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MoreOptionsPanel');
    },
    get_h15sd: function SpottedScript_Pages_Promoters_Home_View$get_h15sd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15sd');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Home.View.registerClass('SpottedScript.Pages.Promoters.Home.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
