Type.registerNamespace('SpottedScript.Pages.Connect');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Connect.View
SpottedScript.Pages.Connect.View = function SpottedScript_Pages_Connect_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Connect.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Connect.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Connect_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Connect.View.registerClass('SpottedScript.Pages.Connect.View', SpottedScript.DsiUserControl.View);
