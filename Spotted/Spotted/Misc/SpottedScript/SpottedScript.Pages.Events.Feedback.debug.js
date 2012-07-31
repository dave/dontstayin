Type.registerNamespace('SpottedScript.Pages.Events.Feedback');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Feedback.View
SpottedScript.Pages.Events.Feedback.View = function SpottedScript_Pages_Events_Feedback_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Feedback.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Feedback.View.prototype = {
    clientId: null,
    get_eventTicketFeedbackPanel: function SpottedScript_Pages_Events_Feedback_View$get_eventTicketFeedbackPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventTicketFeedbackPanel');
    },
    get_ticketsHeading: function SpottedScript_Pages_Events_Feedback_View$get_ticketsHeading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsHeading');
    },
    get_ticketEventDataList: function SpottedScript_Pages_Events_Feedback_View$get_ticketEventDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketEventDataList');
    },
    get_ticketFeedbackP: function SpottedScript_Pages_Events_Feedback_View$get_ticketFeedbackP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFeedbackP');
    },
    get_usrTicketFeedbackHeaderLabel: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketFeedbackHeaderLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketFeedbackHeaderLabel');
    },
    get_usrTicketResponseGoodLinkButtonDiv: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketResponseGoodLinkButtonDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseGoodLinkButtonDiv');
    },
    get_usrTicketResponseGoodLinkButton: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketResponseGoodLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseGoodLinkButton');
    },
    get_usrTicketResponseGoodDiv: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketResponseGoodDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseGoodDiv');
    },
    get_usrTicketResponseBadLinkButtonDiv: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketResponseBadLinkButtonDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseBadLinkButtonDiv');
    },
    get_usrTicketResponseBadLinkButton: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketResponseBadLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseBadLinkButton');
    },
    get_usrTicketResponseBadDiv: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketResponseBadDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketResponseBadDiv');
    },
    get_usrTicketFeedbackTextDiv: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketFeedbackTextDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketFeedbackTextDiv');
    },
    get_usrTicketFeedbackTextBox: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketFeedbackTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketFeedbackTextBox');
    },
    get_usrTicketFeedbackLabel: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketFeedbackLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketFeedbackLabel');
    },
    get_usrTicketFeedbackTextSubmitButton: function SpottedScript_Pages_Events_Feedback_View$get_usrTicketFeedbackTextSubmitButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrTicketFeedbackTextSubmitButton');
    },
    get_successfulTicketEventPanel: function SpottedScript_Pages_Events_Feedback_View$get_successfulTicketEventPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuccessfulTicketEventPanel');
    },
    get_joinBrandRegularsGroup: function SpottedScript_Pages_Events_Feedback_View$get_joinBrandRegularsGroup() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_JoinBrandRegularsGroup');
    },
    get_brandGroupRepeater: function SpottedScript_Pages_Events_Feedback_View$get_brandGroupRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandGroupRepeater');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Feedback_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Feedback.View.registerClass('SpottedScript.Pages.Events.Feedback.View', SpottedScript.Pages.Events.EventUserControl.View);
