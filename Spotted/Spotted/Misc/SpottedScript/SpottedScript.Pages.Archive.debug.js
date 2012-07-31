Type.registerNamespace('SpottedScript.Pages.Archive');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Archive.View
SpottedScript.Pages.Archive.View = function SpottedScript_Pages_Archive_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Archive.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Archive.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Archive_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Archive.View.registerClass('SpottedScript.Pages.Archive.View', SpottedScript.DsiUserControl.View);
