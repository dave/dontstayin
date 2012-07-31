Type.registerNamespace('SpottedScript.Pages.Promoters.Events');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Events.View
SpottedScript.Pages.Promoters.Events.View = function SpottedScript_Pages_Promoters_Events_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Events.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Events.View.prototype = {
    clientId: null,
    get_promoterintro2: function SpottedScript_Pages_Promoters_Events_View$get_promoterintro2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Promoterintro2');
    },
    get_h12: function SpottedScript_Pages_Promoters_Events_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_eventsGridView: function SpottedScript_Pages_Promoters_Events_View$get_eventsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsGridView');
    },
    get_panelEventsList: function SpottedScript_Pages_Promoters_Events_View$get_panelEventsList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEventsList');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Events_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Events.View.registerClass('SpottedScript.Pages.Promoters.Events.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
