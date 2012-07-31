Type.registerNamespace('SpottedScript.Styled.LegalTermsUser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.LegalTermsUser.View
SpottedScript.Styled.LegalTermsUser.View = function SpottedScript_Styled_LegalTermsUser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.LegalTermsUser.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.LegalTermsUser.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Styled_LegalTermsUser_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.LegalTermsUser.View.registerClass('SpottedScript.Styled.LegalTermsUser.View', SpottedScript.StyledUserControl.View);
