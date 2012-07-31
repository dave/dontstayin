Type.registerNamespace('SpottedScript.Pages.Venues.GoogleMap');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Venues.GoogleMap.View
SpottedScript.Pages.Venues.GoogleMap.View = function SpottedScript_Pages_Venues_GoogleMap_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Venues.GoogleMap.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Venues.GoogleMap.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Venues_GoogleMap_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Venues.GoogleMap.View.registerClass('SpottedScript.Pages.Venues.GoogleMap.View', SpottedScript.DsiUserControl.View);
