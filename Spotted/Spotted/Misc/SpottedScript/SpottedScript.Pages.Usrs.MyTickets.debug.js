Type.registerNamespace('SpottedScript.Pages.Usrs.MyTickets');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.MyTickets.View
SpottedScript.Pages.Usrs.MyTickets.View = function SpottedScript_Pages_Usrs_MyTickets_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.MyTickets.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.MyTickets.View.prototype = {
    clientId: null,
    get_myTicketsPanel: function SpottedScript_Pages_Usrs_MyTickets_View$get_myTicketsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsPanel');
    },
    get_myTicketsHeading: function SpottedScript_Pages_Usrs_MyTickets_View$get_myTicketsHeading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTicketsHeading');
    },
    get_selectCurrentDateRangeLinkButton: function SpottedScript_Pages_Usrs_MyTickets_View$get_selectCurrentDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectCurrentDateRangeLinkButton');
    },
    get_selectPastDateRangeLinkButton: function SpottedScript_Pages_Usrs_MyTickets_View$get_selectPastDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectPastDateRangeLinkButton');
    },
    get_selectAllDateRangeLinkButton: function SpottedScript_Pages_Usrs_MyTickets_View$get_selectAllDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectAllDateRangeLinkButton');
    },
    get_uiHasETickets: function SpottedScript_Pages_Usrs_MyTickets_View$get_uiHasETickets() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiHasETickets');
    },
    get_myEventTicketsRepeater: function SpottedScript_Pages_Usrs_MyTickets_View$get_myEventTicketsRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyEventTicketsRepeater');
    },
    get_noTickets: function SpottedScript_Pages_Usrs_MyTickets_View$get_noTickets() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoTickets');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_MyTickets_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.MyTickets.View.registerClass('SpottedScript.Pages.Usrs.MyTickets.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
