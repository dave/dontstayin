Type.registerNamespace('SpottedScript.Pages.LegalInformationPolicy');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.LegalInformationPolicy.View
SpottedScript.Pages.LegalInformationPolicy.View = function SpottedScript_Pages_LegalInformationPolicy_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.LegalInformationPolicy.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.LegalInformationPolicy.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_LegalInformationPolicy_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.LegalInformationPolicy.View.registerClass('SpottedScript.Pages.LegalInformationPolicy.View', SpottedScript.DsiUserControl.View);
