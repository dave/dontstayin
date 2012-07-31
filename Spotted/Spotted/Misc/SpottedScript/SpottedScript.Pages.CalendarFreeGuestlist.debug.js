Type.registerNamespace('SpottedScript.Pages.CalendarFreeGuestlist');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.CalendarFreeGuestlist.View
SpottedScript.Pages.CalendarFreeGuestlist.View = function SpottedScript_Pages_CalendarFreeGuestlist_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.CalendarFreeGuestlist.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.CalendarFreeGuestlist.View.prototype = {
    clientId: null,
    get_calendarFreeGuestlistContent: function SpottedScript_Pages_CalendarFreeGuestlist_View$get_calendarFreeGuestlistContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarFreeGuestlistContent');
    },
    get_genericContainerPage: function SpottedScript_Pages_CalendarFreeGuestlist_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.CalendarFreeGuestlist.View.registerClass('SpottedScript.Pages.CalendarFreeGuestlist.View', SpottedScript.DsiUserControl.View);
