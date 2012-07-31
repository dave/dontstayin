Type.registerNamespace('SpottedScript.Pages.LegalTermsPromoter');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.LegalTermsPromoter.View
SpottedScript.Pages.LegalTermsPromoter.View = function SpottedScript_Pages_LegalTermsPromoter_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.LegalTermsPromoter.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.LegalTermsPromoter.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_LegalTermsPromoter_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.LegalTermsPromoter.View.registerClass('SpottedScript.Pages.LegalTermsPromoter.View', SpottedScript.DsiUserControl.View);
