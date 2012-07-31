Type.registerNamespace('SpottedScript.Styled.EventDetail');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.EventDetail.View
SpottedScript.Styled.EventDetail.View = function SpottedScript_Styled_EventDetail_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.EventDetail.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.EventDetail.View.prototype = {
    clientId: null,
    get_eventHeader: function SpottedScript_Styled_EventDetail_View$get_eventHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventHeader');
    },
    get_eventPic: function SpottedScript_Styled_EventDetail_View$get_eventPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventPic');
    },
    get_runningTicketRunsForPromoterRepeater: function SpottedScript_Styled_EventDetail_View$get_runningTicketRunsForPromoterRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RunningTicketRunsForPromoterRepeater');
    },
    get_noTicketsAvailableDiv: function SpottedScript_Styled_EventDetail_View$get_noTicketsAvailableDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoTicketsAvailableDiv');
    },
    get_ticketNoteP: function SpottedScript_Styled_EventDetail_View$get_ticketNoteP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketNoteP');
    },
    get_ticketNoteLabel: function SpottedScript_Styled_EventDetail_View$get_ticketNoteLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketNoteLabel');
    },
    get_processingVal: function SpottedScript_Styled_EventDetail_View$get_processingVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProcessingVal');
    },
    get_ticketValidationSummary: function SpottedScript_Styled_EventDetail_View$get_ticketValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketValidationSummary');
    },
    get_eventShortDescription: function SpottedScript_Styled_EventDetail_View$get_eventShortDescription() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventShortDescription');
    },
    get_genericContainerPage: function SpottedScript_Styled_EventDetail_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.EventDetail.View.registerClass('SpottedScript.Styled.EventDetail.View', SpottedScript.StyledUserControl.View);
