Type.registerNamespace('SpottedScript.Pages.Brands.Join');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Brands.Join.View
SpottedScript.Pages.Brands.Join.View = function SpottedScript_Pages_Brands_Join_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Brands.Join.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Brands.Join.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Brands_Join_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Brands.Join.View.registerClass('SpottedScript.Pages.Brands.Join.View', SpottedScript.DsiUserControl.View);
