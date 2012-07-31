Type.registerNamespace('SpottedScript.Styled.Test');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.Test.View
SpottedScript.Styled.Test.View = function SpottedScript_Styled_Test_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.Test.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.Test.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Styled_Test_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.Test.View.registerClass('SpottedScript.Styled.Test.View', SpottedScript.StyledUserControl.View);
