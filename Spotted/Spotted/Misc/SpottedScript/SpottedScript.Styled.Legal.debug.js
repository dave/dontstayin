Type.registerNamespace('SpottedScript.Styled.Legal');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.Legal.View
SpottedScript.Styled.Legal.View = function SpottedScript_Styled_Legal_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.Legal.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.Legal.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Styled_Legal_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.Legal.View.registerClass('SpottedScript.Styled.Legal.View', SpottedScript.StyledUserControl.View);
