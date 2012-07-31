Type.registerNamespace('SpottedScript.Pages.Photos.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Photos.Home.View
SpottedScript.Pages.Photos.Home.View = function SpottedScript_Pages_Photos_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Photos.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Photos.Home.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Photos_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Photos.Home.View.registerClass('SpottedScript.Pages.Photos.Home.View', SpottedScript.DsiUserControl.View);
