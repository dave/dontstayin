Type.registerNamespace('SpottedScript.Styled.MyTickets');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.MyTickets.View
SpottedScript.Styled.MyTickets.View = function SpottedScript_Styled_MyTickets_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.MyTickets.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.MyTickets.View.prototype = {
    clientId: null,
    get_selectCurrentDateRangeLinkButton: function SpottedScript_Styled_MyTickets_View$get_selectCurrentDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectCurrentDateRangeLinkButton');
    },
    get_selectPastDateRangeLinkButton: function SpottedScript_Styled_MyTickets_View$get_selectPastDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectPastDateRangeLinkButton');
    },
    get_selectAllDateRangeLinkButton: function SpottedScript_Styled_MyTickets_View$get_selectAllDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectAllDateRangeLinkButton');
    },
    get_hasTickets: function SpottedScript_Styled_MyTickets_View$get_hasTickets() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HasTickets');
    },
    get_myEventTicketsRepeater: function SpottedScript_Styled_MyTickets_View$get_myEventTicketsRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyEventTicketsRepeater');
    },
    get_noTickets: function SpottedScript_Styled_MyTickets_View$get_noTickets() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoTickets');
    },
    get_genericContainerPage: function SpottedScript_Styled_MyTickets_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.MyTickets.View.registerClass('SpottedScript.Styled.MyTickets.View', SpottedScript.StyledUserControl.View);
