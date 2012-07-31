Type.registerNamespace('SpottedScript.Pages.Merchandise');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Merchandise.View
SpottedScript.Pages.Merchandise.View = function SpottedScript_Pages_Merchandise_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Merchandise.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Merchandise.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Merchandise_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Merchandise.View.registerClass('SpottedScript.Pages.Merchandise.View', SpottedScript.DsiUserControl.View);
