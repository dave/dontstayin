Type.registerNamespace('SpottedScript.Pages.SearchResults');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.SearchResults.View
SpottedScript.Pages.SearchResults.View = function SpottedScript_Pages_SearchResults_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.SearchResults.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.SearchResults.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_SearchResults_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.SearchResults.View.registerClass('SpottedScript.Pages.SearchResults.View', SpottedScript.DsiUserControl.View);
