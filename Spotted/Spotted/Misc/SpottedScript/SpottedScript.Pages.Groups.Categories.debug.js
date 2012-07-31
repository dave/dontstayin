Type.registerNamespace('SpottedScript.Pages.Groups.Categories');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Categories.View
SpottedScript.Pages.Groups.Categories.View = function SpottedScript_Pages_Groups_Categories_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Categories.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Categories.View.prototype = {
    clientId: null,
    get_uiList: function SpottedScript_Pages_Groups_Categories_View$get_uiList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiList');
    },
    get_genericContainerPage: function SpottedScript_Pages_Groups_Categories_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Categories.View.registerClass('SpottedScript.Pages.Groups.Categories.View', SpottedScript.DsiUserControl.View);
