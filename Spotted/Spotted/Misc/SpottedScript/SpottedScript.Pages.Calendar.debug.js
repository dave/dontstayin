Type.registerNamespace('SpottedScript.Pages.Calendar');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Calendar.View
SpottedScript.Pages.Calendar.View = function SpottedScript_Pages_Calendar_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Calendar.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Calendar.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Calendar_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Calendar.View.registerClass('SpottedScript.Pages.Calendar.View', SpottedScript.DsiUserControl.View);
