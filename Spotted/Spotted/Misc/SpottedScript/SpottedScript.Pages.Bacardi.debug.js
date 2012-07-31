Type.registerNamespace('SpottedScript.Pages.Bacardi');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Bacardi.View
SpottedScript.Pages.Bacardi.View = function SpottedScript_Pages_Bacardi_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Bacardi.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Bacardi.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Bacardi_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Bacardi.View.registerClass('SpottedScript.Pages.Bacardi.View', SpottedScript.DsiUserControl.View);
