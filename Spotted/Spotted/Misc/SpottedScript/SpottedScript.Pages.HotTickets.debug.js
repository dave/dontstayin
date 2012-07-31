Type.registerNamespace('SpottedScript.Pages.HotTickets');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.HotTickets.View
SpottedScript.Pages.HotTickets.View = function SpottedScript_Pages_HotTickets_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.HotTickets.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.HotTickets.View.prototype = {
    clientId: null,
    get_calendar: function SpottedScript_Pages_HotTickets_View$get_calendar() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Calendar');
    },
    get_genericContainerPage: function SpottedScript_Pages_HotTickets_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.HotTickets.View.registerClass('SpottedScript.Pages.HotTickets.View', SpottedScript.DsiUserControl.View);
