Type.registerNamespace('SpottedScript.Pages.LegalTermsUser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.LegalTermsUser.View
SpottedScript.Pages.LegalTermsUser.View = function SpottedScript_Pages_LegalTermsUser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.LegalTermsUser.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.LegalTermsUser.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_LegalTermsUser_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.LegalTermsUser.View.registerClass('SpottedScript.Pages.LegalTermsUser.View', SpottedScript.DsiUserControl.View);
