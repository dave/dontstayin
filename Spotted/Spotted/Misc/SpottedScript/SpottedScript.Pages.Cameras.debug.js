Type.registerNamespace('SpottedScript.Pages.Cameras');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Cameras.View
SpottedScript.Pages.Cameras.View = function SpottedScript_Pages_Cameras_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Cameras.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Cameras.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Cameras_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Cameras.View.registerClass('SpottedScript.Pages.Cameras.View', SpottedScript.DsiUserControl.View);
