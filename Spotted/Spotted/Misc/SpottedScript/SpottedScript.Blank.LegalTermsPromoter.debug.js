Type.registerNamespace('SpottedScript.Blank.LegalTermsPromoter');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.LegalTermsPromoter.View
SpottedScript.Blank.LegalTermsPromoter.View = function SpottedScript_Blank_LegalTermsPromoter_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.LegalTermsPromoter.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.LegalTermsPromoter.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Blank_LegalTermsPromoter_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Blank_LegalTermsPromoter_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.LegalTermsPromoter.View.registerClass('SpottedScript.Blank.LegalTermsPromoter.View', SpottedScript.BlankUserControl.View);
