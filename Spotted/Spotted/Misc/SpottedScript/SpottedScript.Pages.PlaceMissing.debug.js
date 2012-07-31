Type.registerNamespace('SpottedScript.Pages.PlaceMissing');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.PlaceMissing.View
SpottedScript.Pages.PlaceMissing.View = function SpottedScript_Pages_PlaceMissing_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.PlaceMissing.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.PlaceMissing.View.prototype = {
    clientId: null,
    get_introHeader: function SpottedScript_Pages_PlaceMissing_View$get_introHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroHeader');
    },
    get_genericContainerPage: function SpottedScript_Pages_PlaceMissing_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.PlaceMissing.View.registerClass('SpottedScript.Pages.PlaceMissing.View', SpottedScript.DsiUserControl.View);
