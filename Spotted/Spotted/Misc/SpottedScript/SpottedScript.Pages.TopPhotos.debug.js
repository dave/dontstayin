Type.registerNamespace('SpottedScript.Pages.TopPhotos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.TopPhotos.View
SpottedScript.Pages.TopPhotos.View = function SpottedScript_Pages_TopPhotos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.TopPhotos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.TopPhotos.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_TopPhotos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.TopPhotos.View.registerClass('SpottedScript.Pages.TopPhotos.View', SpottedScript.DsiUserControl.View);
