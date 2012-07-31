Type.registerNamespace('SpottedScript.Blank.LegalTermsUser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.LegalTermsUser.View
SpottedScript.Blank.LegalTermsUser.View = function SpottedScript_Blank_LegalTermsUser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.LegalTermsUser.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.LegalTermsUser.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Blank_LegalTermsUser_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Blank_LegalTermsUser_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.LegalTermsUser.View.registerClass('SpottedScript.Blank.LegalTermsUser.View', SpottedScript.BlankUserControl.View);
