Type.registerNamespace('SpottedScript.Pages.Shadownap');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Shadownap.View
SpottedScript.Pages.Shadownap.View = function SpottedScript_Pages_Shadownap_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Shadownap.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Shadownap.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Shadownap_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Shadownap.View.registerClass('SpottedScript.Pages.Shadownap.View', SpottedScript.DsiUserControl.View);
