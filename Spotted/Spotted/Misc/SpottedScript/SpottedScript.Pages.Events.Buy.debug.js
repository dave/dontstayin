Type.registerNamespace('SpottedScript.Pages.Events.Buy');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Buy.View
SpottedScript.Pages.Events.Buy.View = function SpottedScript_Pages_Events_Buy_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Buy.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Buy.View.prototype = {
    clientId: null,
    get_ticketsPlaceholder: function SpottedScript_Pages_Events_Buy_View$get_ticketsPlaceholder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsPlaceholder');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Buy_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Buy.View.registerClass('SpottedScript.Pages.Events.Buy.View', SpottedScript.Pages.Events.EventUserControl.View);
