Type.registerNamespace('SpottedScript.Pages.TicketsCalendar');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.TicketsCalendar.View
SpottedScript.Pages.TicketsCalendar.View = function SpottedScript_Pages_TicketsCalendar_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.TicketsCalendar.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.TicketsCalendar.View.prototype = {
    clientId: null,
    get_ticketsCalendarContent: function SpottedScript_Pages_TicketsCalendar_View$get_ticketsCalendarContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsCalendarContent');
    },
    get_genericContainerPage: function SpottedScript_Pages_TicketsCalendar_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.TicketsCalendar.View.registerClass('SpottedScript.Pages.TicketsCalendar.View', SpottedScript.DsiUserControl.View);
