Type.registerNamespace('SpottedScript.Styled.LegalInformationPolicy');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.LegalInformationPolicy.View
SpottedScript.Styled.LegalInformationPolicy.View = function SpottedScript_Styled_LegalInformationPolicy_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.LegalInformationPolicy.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.LegalInformationPolicy.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Styled_LegalInformationPolicy_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.LegalInformationPolicy.View.registerClass('SpottedScript.Styled.LegalInformationPolicy.View', SpottedScript.StyledUserControl.View);
