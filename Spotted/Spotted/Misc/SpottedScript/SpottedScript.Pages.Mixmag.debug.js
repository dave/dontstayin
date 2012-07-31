Type.registerNamespace('SpottedScript.Pages.Mixmag');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Mixmag.View
SpottedScript.Pages.Mixmag.View = function SpottedScript_Pages_Mixmag_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Mixmag.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Mixmag.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Mixmag_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Mixmag.View.registerClass('SpottedScript.Pages.Mixmag.View', SpottedScript.DsiUserControl.View);
