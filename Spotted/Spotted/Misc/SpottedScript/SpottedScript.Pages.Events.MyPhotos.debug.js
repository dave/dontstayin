Type.registerNamespace('SpottedScript.Pages.Events.MyPhotos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.MyPhotos.View
SpottedScript.Pages.Events.MyPhotos.View = function SpottedScript_Pages_Events_MyPhotos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.MyPhotos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.MyPhotos.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Events_MyPhotos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.MyPhotos.View.registerClass('SpottedScript.Pages.Events.MyPhotos.View', SpottedScript.DsiUserControl.View);
