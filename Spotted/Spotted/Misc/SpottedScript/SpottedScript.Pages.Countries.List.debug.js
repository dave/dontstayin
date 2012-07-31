Type.registerNamespace('SpottedScript.Pages.Countries.List');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Countries.List.View
SpottedScript.Pages.Countries.List.View = function SpottedScript_Pages_Countries_List_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Countries.List.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Countries.List.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Countries_List_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Countries.List.View.registerClass('SpottedScript.Pages.Countries.List.View', SpottedScript.DsiUserControl.View);
