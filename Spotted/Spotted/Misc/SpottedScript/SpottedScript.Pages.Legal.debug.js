Type.registerNamespace('SpottedScript.Pages.Legal');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Legal.View
SpottedScript.Pages.Legal.View = function SpottedScript_Pages_Legal_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Legal.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Legal.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Legal_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Legal.View.registerClass('SpottedScript.Pages.Legal.View', SpottedScript.DsiUserControl.View);
