Type.registerNamespace('SpottedScript.Pages.Promoters.TicketRun');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.TicketRun.View
SpottedScript.Pages.Promoters.TicketRun.View = function SpottedScript_Pages_Promoters_TicketRun_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.TicketRun.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.TicketRun.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_TicketRun_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_noEventsPanel: function SpottedScript_Pages_Promoters_TicketRun_View$get_noEventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoEventsPanel');
    },
    get_hasEventsPanel: function SpottedScript_Pages_Promoters_TicketRun_View$get_hasEventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HasEventsPanel');
    },
    get_errorMessageP: function SpottedScript_Pages_Promoters_TicketRun_View$get_errorMessageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorMessageP');
    },
    get_addEditTicketRunPanel: function SpottedScript_Pages_Promoters_TicketRun_View$get_addEditTicketRunPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddEditTicketRunPanel');
    },
    get_addEditTicketRunH1: function SpottedScript_Pages_Promoters_TicketRun_View$get_addEditTicketRunH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddEditTicketRunH1');
    },
    get_ticketRunDefaultNoteLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketRunDefaultNoteLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunDefaultNoteLabel');
    },
    get_ticketRunNote: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketRunNote() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunNote');
    },
    get_ticketRunTable: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketRunTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunTable');
    },
    get_eventLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_eventLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLabel');
    },
    get_nonEventSpecificDiv: function SpottedScript_Pages_Promoters_TicketRun_View$get_nonEventSpecificDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NonEventSpecificDiv');
    },
    get_eventDropDownList: function SpottedScript_Pages_Promoters_TicketRun_View$get_eventDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventDropDownList');
    },
    get_ticketPriceTextBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketPriceTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPriceTextBox');
    },
    get_priceAndBookingFeeLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_priceAndBookingFeeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PriceAndBookingFeeLabel');
    },
    get_ticketPriceRequiredFieldValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketPriceRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPriceRequiredFieldValidator');
    },
    get_ticketBookingFeeRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketBookingFeeRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketBookingFeeRow');
    },
    get_bookingFeeTextBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_bookingFeeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookingFeeTextBox');
    },
    get_overrideBookingFeeCheckBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_overrideBookingFeeCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideBookingFeeCheckBox');
    },
    get_bookingFeeLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_bookingFeeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookingFeeLabel');
    },
    get_advOptionsCheckRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_advOptionsCheckRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvOptionsCheckRow');
    },
    get_advancedOptionsCheckBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_advancedOptionsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedOptionsCheckBox');
    },
    get_ticketNameRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketNameRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketNameRow');
    },
    get_ticketNameTextBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketNameTextBox');
    },
    get_ticketNameHelperLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketNameHelperLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketNameHelperLabel');
    },
    get_ticketDescriptionRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketDescriptionRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketDescriptionRow');
    },
    get_ticketDescriptionTextBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketDescriptionTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketDescriptionTextBox');
    },
    get_ticketDescriptionHelperLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketDescriptionHelperLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketDescriptionHelperLabel');
    },
    get_ticketDescriptionLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketDescriptionLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketDescriptionLabel');
    },
    get_brandsRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_brandsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandsRow');
    },
    get_brandDropDownList: function SpottedScript_Pages_Promoters_TicketRun_View$get_brandDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandDropDownList');
    },
    get_brandLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_brandLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandLabel');
    },
    get_followsTicketRunRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_followsTicketRunRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FollowsTicketRunRow');
    },
    get_followsTicketRunDropDownList: function SpottedScript_Pages_Promoters_TicketRun_View$get_followsTicketRunDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FollowsTicketRunDropDownList');
    },
    get_followsTicketRunHelperLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_followsTicketRunHelperLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FollowsTicketRunHelperLabel');
    },
    get_followsTicketRunLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_followsTicketRunLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FollowsTicketRunLabel');
    },
    get_startDateRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_startDateRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StartDateRow');
    },
    get_startTicketRunTable: function SpottedScript_Pages_Promoters_TicketRun_View$get_startTicketRunTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StartTicketRunTable');
    },
    get_startTicketRunCal: function SpottedScript_Pages_Promoters_TicketRun_View$get_startTicketRunCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_StartTicketRunCalController');
    },
    get_startTicketRunTime: function SpottedScript_Pages_Promoters_TicketRun_View$get_startTicketRunTime() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StartTicketRunTime');
    },
    get_startTicketRunLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_startTicketRunLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StartTicketRunLabel');
    },
    get_endDateRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_endDateRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EndDateRow');
    },
    get_endTicketRunTable: function SpottedScript_Pages_Promoters_TicketRun_View$get_endTicketRunTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EndTicketRunTable');
    },
    get_endTicketRunCal: function SpottedScript_Pages_Promoters_TicketRun_View$get_endTicketRunCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_EndTicketRunCalController');
    },
    get_endTicketRunTime: function SpottedScript_Pages_Promoters_TicketRun_View$get_endTicketRunTime() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EndTicketRunTime');
    },
    get_endTicketRunLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_endTicketRunLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EndTicketRunLabel');
    },
    get_ticketsSoldRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketsSoldRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsSoldRow');
    },
    get_ticketsSoldLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketsSoldLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsSoldLabel');
    },
    get_maxTicketsRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_maxTicketsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxTicketsRow');
    },
    get_maxTicketsTextBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_maxTicketsTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxTicketsTextBox');
    },
    get_maxTicketsRangeValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_maxTicketsRangeValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxTicketsRangeValidator');
    },
    get_contactEmailRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_contactEmailRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ContactEmailRow');
    },
    get_contactEmailTextBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_contactEmailTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ContactEmailTextBox');
    },
    get_contactEmailRequiredFieldValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_contactEmailRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ContactEmailRequiredFieldValidator');
    },
    get_contactEmailRegularExpressionValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_contactEmailRegularExpressionValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ContactEmailRegularExpressionValidator');
    },
    get_orderInTheListRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_orderInTheListRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OrderInTheListRow');
    },
    get_orderInTheListTextBox: function SpottedScript_Pages_Promoters_TicketRun_View$get_orderInTheListTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OrderInTheListTextBox');
    },
    get_orderInTheListHelperLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_orderInTheListHelperLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OrderInTheListHelperLabel');
    },
    get_orderInTheListRangeValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_orderInTheListRangeValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OrderInTheListRangeValidator');
    },
    get_updateButtonsRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_updateButtonsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UpdateButtonsRow');
    },
    get_goToConfirmationButton: function SpottedScript_Pages_Promoters_TicketRun_View$get_goToConfirmationButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GoToConfirmationButton');
    },
    get_pauseResumeButton: function SpottedScript_Pages_Promoters_TicketRun_View$get_pauseResumeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PauseResumeButton');
    },
    get_stopButton: function SpottedScript_Pages_Promoters_TicketRun_View$get_stopButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StopButton');
    },
    get_confirmationButtonsRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_confirmationButtonsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ConfirmationButtonsRow');
    },
    get_backButton: function SpottedScript_Pages_Promoters_TicketRun_View$get_backButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BackButton');
    },
    get_advancedOptionsButton: function SpottedScript_Pages_Promoters_TicketRun_View$get_advancedOptionsButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedOptionsButton');
    },
    get_saveTicketRunButton: function SpottedScript_Pages_Promoters_TicketRun_View$get_saveTicketRunButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveTicketRunButton');
    },
    get_refundButtonRow: function SpottedScript_Pages_Promoters_TicketRun_View$get_refundButtonRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundButtonRow');
    },
    get_refundButton: function SpottedScript_Pages_Promoters_TicketRun_View$get_refundButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundButton');
    },
    get_ticketPriceCustomValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketPriceCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPriceCustomValidator');
    },
    get_ticketDescriptionCustomValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketDescriptionCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketDescriptionCustomValidator');
    },
    get_startDateCustomValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_startDateCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StartDateCustomValidator');
    },
    get_endDateCustomValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_endDateCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EndDateCustomValidator');
    },
    get_circularTicketRunDependencyCustomValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_circularTicketRunDependencyCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CircularTicketRunDependencyCustomValidator');
    },
    get_maxTicketsCustomValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_maxTicketsCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxTicketsCustomValidator');
    },
    get_savingErrorCustomValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_savingErrorCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SavingErrorCustomValidator');
    },
    get_errorMessageCustomValidator: function SpottedScript_Pages_Promoters_TicketRun_View$get_errorMessageCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorMessageCustomValidator');
    },
    get_ticketRunValidationSummary: function SpottedScript_Pages_Promoters_TicketRun_View$get_ticketRunValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunValidationSummary');
    },
    get_javascriptLabel: function SpottedScript_Pages_Promoters_TicketRun_View$get_javascriptLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_JavascriptLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_TicketRun_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.TicketRun.View.registerClass('SpottedScript.Pages.Promoters.TicketRun.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
