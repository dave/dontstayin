Type.registerNamespace('SpottedScript.Pages.MyEvents');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyEvents.View
SpottedScript.Pages.MyEvents.View = function SpottedScript_Pages_MyEvents_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyEvents.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyEvents.View.prototype = {
    clientId: null,
    get_h11: function SpottedScript_Pages_MyEvents_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_eventsPanel: function SpottedScript_Pages_MyEvents_View$get_eventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsPanel');
    },
    get_eventsDataGrid: function SpottedScript_Pages_MyEvents_View$get_eventsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyEvents_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyEvents.View.registerClass('SpottedScript.Pages.MyEvents.View', SpottedScript.DsiUserControl.View);
