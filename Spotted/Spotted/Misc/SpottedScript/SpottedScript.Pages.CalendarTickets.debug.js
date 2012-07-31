Type.registerNamespace('SpottedScript.Pages.CalendarTickets');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.CalendarTickets.View
SpottedScript.Pages.CalendarTickets.View = function SpottedScript_Pages_CalendarTickets_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.CalendarTickets.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.CalendarTickets.View.prototype = {
    clientId: null,
    get_calendarTicketsContent: function SpottedScript_Pages_CalendarTickets_View$get_calendarTicketsContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarTicketsContent');
    },
    get_genericContainerPage: function SpottedScript_Pages_CalendarTickets_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.CalendarTickets.View.registerClass('SpottedScript.Pages.CalendarTickets.View', SpottedScript.DsiUserControl.View);
