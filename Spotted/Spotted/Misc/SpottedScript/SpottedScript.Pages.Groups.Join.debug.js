Type.registerNamespace('SpottedScript.Pages.Groups.Join');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Join.View
SpottedScript.Pages.Groups.Join.View = function SpottedScript_Pages_Groups_Join_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Join.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Join.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Groups_Join_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Join.View.registerClass('SpottedScript.Pages.Groups.Join.View', SpottedScript.DsiUserControl.View);
