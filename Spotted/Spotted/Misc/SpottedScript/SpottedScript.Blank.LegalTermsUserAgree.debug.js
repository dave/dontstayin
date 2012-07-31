Type.registerNamespace('SpottedScript.Blank.LegalTermsUserAgree');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.LegalTermsUserAgree.View
SpottedScript.Blank.LegalTermsUserAgree.View = function SpottedScript_Blank_LegalTermsUserAgree_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.LegalTermsUserAgree.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.LegalTermsUserAgree.View.prototype = {
    clientId: null,
    get_validationsummary1: function SpottedScript_Blank_LegalTermsUserAgree_View$get_validationsummary1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary1');
    },
    get_termsCheckbox: function SpottedScript_Blank_LegalTermsUserAgree_View$get_termsCheckbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TermsCheckbox');
    },
    get_customvalidator7: function SpottedScript_Blank_LegalTermsUserAgree_View$get_customvalidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator7');
    },
    get_prefsUpdateButton: function SpottedScript_Blank_LegalTermsUserAgree_View$get_prefsUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrefsUpdateButton');
    },
    get_genericContainerPage: function SpottedScript_Blank_LegalTermsUserAgree_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.LegalTermsUserAgree.View.registerClass('SpottedScript.Blank.LegalTermsUserAgree.View', SpottedScript.BlankUserControl.View);
