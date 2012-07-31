Type.registerNamespace('SpottedScript.Pages.Events.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Home.View
SpottedScript.Pages.Events.Home.View = function SpottedScript_Pages_Events_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Home.View.prototype = {
    clientId: null,
    get_promoterPanel: function SpottedScript_Pages_Events_Home_View$get_promoterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterPanel');
    },
    get_photoUploadPanel: function SpottedScript_Pages_Events_Home_View$get_photoUploadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoUploadPanel');
    },
    get_h13: function SpottedScript_Pages_Events_Home_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_panelUpload: function SpottedScript_Pages_Events_Home_View$get_panelUpload() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelUpload');
    },
    get_spotterRequestPanel: function SpottedScript_Pages_Events_Home_View$get_spotterRequestPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestPanel');
    },
    get_spotterRequestName: function SpottedScript_Pages_Events_Home_View$get_spotterRequestName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestName');
    },
    get_spotterRequestPhone: function SpottedScript_Pages_Events_Home_View$get_spotterRequestPhone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestPhone');
    },
    get_spotterRequestNonSpotter: function SpottedScript_Pages_Events_Home_View$get_spotterRequestNonSpotter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestNonSpotter');
    },
    get_spotterRequestSpotter: function SpottedScript_Pages_Events_Home_View$get_spotterRequestSpotter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestSpotter');
    },
    get_ticketsPanel: function SpottedScript_Pages_Events_Home_View$get_ticketsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsPanel');
    },
    get_ticketsHeading: function SpottedScript_Pages_Events_Home_View$get_ticketsHeading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsHeading');
    },
    get_buyTicketsP: function SpottedScript_Pages_Events_Home_View$get_buyTicketsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyTicketsP');
    },
    get_runningTicketRunsRepeater: function SpottedScript_Pages_Events_Home_View$get_runningTicketRunsRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RunningTicketRunsRepeater');
    },
    get_eTicketReminderP: function SpottedScript_Pages_Events_Home_View$get_eTicketReminderP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ETicketReminderP');
    },
    get_ticketNoteP: function SpottedScript_Pages_Events_Home_View$get_ticketNoteP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketNoteP');
    },
    get_ticketNoteLabel: function SpottedScript_Pages_Events_Home_View$get_ticketNoteLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketNoteLabel');
    },
    get_processingVal: function SpottedScript_Pages_Events_Home_View$get_processingVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProcessingVal');
    },
    get_ticketValidationSummary: function SpottedScript_Pages_Events_Home_View$get_ticketValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketValidationSummary');
    },
    get_myTicketsPanel: function SpottedScript_Pages_Events_Home_View$get_myTicketsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsPanel');
    },
    get_myTicketsHeading: function SpottedScript_Pages_Events_Home_View$get_myTicketsHeading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsHeading');
    },
    get_ticketPurchaseResults: function SpottedScript_Pages_Events_Home_View$get_ticketPurchaseResults() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPurchaseResults');
    },
    get_myTicketsRepeater: function SpottedScript_Pages_Events_Home_View$get_myTicketsRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsRepeater');
    },
    get_ticketFeedbackP: function SpottedScript_Pages_Events_Home_View$get_ticketFeedbackP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFeedbackP');
    },
    get_usrTicketFeedbackHeaderLabel: function SpottedScript_Pages_Events_Home_View$get_usrTicketFeedbackHeaderLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketFeedbackHeaderLabel');
    },
    get_usrTicketResponseGoodLinkButtonDiv: function SpottedScript_Pages_Events_Home_View$get_usrTicketResponseGoodLinkButtonDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseGoodLinkButtonDiv');
    },
    get_usrTicketResponseGoodLinkButton: function SpottedScript_Pages_Events_Home_View$get_usrTicketResponseGoodLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseGoodLinkButton');
    },
    get_usrTicketResponseGoodDiv: function SpottedScript_Pages_Events_Home_View$get_usrTicketResponseGoodDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseGoodDiv');
    },
    get_usrTicketResponseBadLinkButtonDiv: function SpottedScript_Pages_Events_Home_View$get_usrTicketResponseBadLinkButtonDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseBadLinkButtonDiv');
    },
    get_usrTicketResponseBadLinkButton: function SpottedScript_Pages_Events_Home_View$get_usrTicketResponseBadLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseBadLinkButton');
    },
    get_usrTicketResponseBadDiv: function SpottedScript_Pages_Events_Home_View$get_usrTicketResponseBadDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseBadDiv');
    },
    get_usrTicketFeedbackLabel: function SpottedScript_Pages_Events_Home_View$get_usrTicketFeedbackLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketFeedbackLabel');
    },
    get_ticketPurchaseJavascriptLabel: function SpottedScript_Pages_Events_Home_View$get_ticketPurchaseJavascriptLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketPurchaseJavascriptLabel');
    },
    get_usrEventAttendListPanel: function SpottedScript_Pages_Events_Home_View$get_usrEventAttendListPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventAttendListPanel');
    },
    get_usrEventAttendListLabelFuture: function SpottedScript_Pages_Events_Home_View$get_usrEventAttendListLabelFuture() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventAttendListLabelFuture');
    },
    get_usrEventAttendListLabelPast: function SpottedScript_Pages_Events_Home_View$get_usrEventAttendListLabelPast() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventAttendListLabelPast');
    },
    get_uiAttendedEvent: function SpottedScript_Pages_Events_Home_View$get_uiAttendedEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAttendedEvent');
    },
    get_usrEventSpotterButtonsPanel: function SpottedScript_Pages_Events_Home_View$get_usrEventSpotterButtonsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventSpotterButtonsPanel');
    },
    get_usrEventSpotterFutureLabel: function SpottedScript_Pages_Events_Home_View$get_usrEventSpotterFutureLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventSpotterFutureLabel');
    },
    get_usrEventSpotterPastLabel: function SpottedScript_Pages_Events_Home_View$get_usrEventSpotterPastLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventSpotterPastLabel');
    },
    get_usrEventSpotterYes: function SpottedScript_Pages_Events_Home_View$get_usrEventSpotterYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventSpotterYes');
    },
    get_usrEventSpotterNo: function SpottedScript_Pages_Events_Home_View$get_usrEventSpotterNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventSpotterNo');
    },
    get_eventCapacityP: function SpottedScript_Pages_Events_Home_View$get_eventCapacityP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventCapacityP');
    },
    get_eventCapacityLabel: function SpottedScript_Pages_Events_Home_View$get_eventCapacityLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventCapacityLabel');
    },
    get_usrEventAttendP: function SpottedScript_Pages_Events_Home_View$get_usrEventAttendP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrEventAttendP');
    },
    get_spottedByPanel: function SpottedScript_Pages_Events_Home_View$get_spottedByPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpottedByPanel');
    },
    get_spottedByTextLabel: function SpottedScript_Pages_Events_Home_View$get_spottedByTextLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpottedByTextLabel');
    },
    get_h11: function SpottedScript_Pages_Events_Home_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_button1: function SpottedScript_Pages_Events_Home_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_flyerPanel: function SpottedScript_Pages_Events_Home_View$get_flyerPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlyerPanel');
    },
    get_h14: function SpottedScript_Pages_Events_Home_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_eventSelectedPanel: function SpottedScript_Pages_Events_Home_View$get_eventSelectedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventSelectedPanel');
    },
    get_spotterSignUpPanel: function SpottedScript_Pages_Events_Home_View$get_spotterSignUpPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSignUpPanel');
    },
    get_noSpotterSignUpPanel: function SpottedScript_Pages_Events_Home_View$get_noSpotterSignUpPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoSpotterSignUpPanel');
    },
    get_spotterSignUpLink1: function SpottedScript_Pages_Events_Home_View$get_spotterSignUpLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSignUpLink1');
    },
    get_spotterSignUpLink2: function SpottedScript_Pages_Events_Home_View$get_spotterSignUpLink2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSignUpLink2');
    },
    get_infoPanel: function SpottedScript_Pages_Events_Home_View$get_infoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoPanel');
    },
    get_eventBodyTitle: function SpottedScript_Pages_Events_Home_View$get_eventBodyTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventBodyTitle');
    },
    get_musicTypeLabel: function SpottedScript_Pages_Events_Home_View$get_musicTypeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTypeLabel');
    },
    get_reviewsPanel: function SpottedScript_Pages_Events_Home_View$get_reviewsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewsPanel');
    },
    get_musicTypeP: function SpottedScript_Pages_Events_Home_View$get_musicTypeP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTypeP');
    },
    get_latest: function SpottedScript_Pages_Events_Home_View$get_latest() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Latest');
    },
    get_galleriesPanel: function SpottedScript_Pages_Events_Home_View$get_galleriesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesPanel');
    },
    get_galleryDataList: function SpottedScript_Pages_Events_Home_View$get_galleryDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleryDataList');
    },
    get_noSpotterPanel: function SpottedScript_Pages_Events_Home_View$get_noSpotterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoSpotterPanel');
    },
    get_todayEventsPanel: function SpottedScript_Pages_Events_Home_View$get_todayEventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TodayEventsPanel');
    },
    get_todayEventsHeader: function SpottedScript_Pages_Events_Home_View$get_todayEventsHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TodayEventsHeader');
    },
    get_todayEventsDataList: function SpottedScript_Pages_Events_Home_View$get_todayEventsDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TodayEventsDataList');
    },
    get_afterPartyPanel: function SpottedScript_Pages_Events_Home_View$get_afterPartyPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AfterPartyPanel');
    },
    get_afterPartyDataList: function SpottedScript_Pages_Events_Home_View$get_afterPartyDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AfterPartyDataList');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Home.View.registerClass('SpottedScript.Pages.Events.Home.View', SpottedScript.Pages.Events.EventUserControl.View);
