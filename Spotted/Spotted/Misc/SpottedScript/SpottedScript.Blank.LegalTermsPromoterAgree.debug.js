Type.registerNamespace('SpottedScript.Blank.LegalTermsPromoterAgree');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.LegalTermsPromoterAgree.View
SpottedScript.Blank.LegalTermsPromoterAgree.View = function SpottedScript_Blank_LegalTermsPromoterAgree_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.LegalTermsPromoterAgree.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.LegalTermsPromoterAgree.View.prototype = {
    clientId: null,
    get_validationsummary1: function SpottedScript_Blank_LegalTermsPromoterAgree_View$get_validationsummary1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary1');
    },
    get_termsCheckbox: function SpottedScript_Blank_LegalTermsPromoterAgree_View$get_termsCheckbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TermsCheckbox');
    },
    get_customvalidator7: function SpottedScript_Blank_LegalTermsPromoterAgree_View$get_customvalidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator7');
    },
    get_prefsUpdateButton: function SpottedScript_Blank_LegalTermsPromoterAgree_View$get_prefsUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrefsUpdateButton');
    },
    get_genericContainerPage: function SpottedScript_Blank_LegalTermsPromoterAgree_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.LegalTermsPromoterAgree.View.registerClass('SpottedScript.Blank.LegalTermsPromoterAgree.View', SpottedScript.BlankUserControl.View);
