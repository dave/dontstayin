Type.registerNamespace('SpottedScript.Pages.Events.EventUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.EventUserControl.View
SpottedScript.Pages.Events.EventUserControl.View = function SpottedScript_Pages_Events_EventUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.EventUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.EventUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Events_EventUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.EventUserControl.View.registerClass('SpottedScript.Pages.Events.EventUserControl.View', SpottedScript.DsiUserControl.View);
