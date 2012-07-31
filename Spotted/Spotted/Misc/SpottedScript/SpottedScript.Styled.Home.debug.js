Type.registerNamespace('SpottedScript.Styled.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.Home.View
SpottedScript.Styled.Home.View = function SpottedScript_Styled_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.Home.View.prototype = {
    clientId: null,
    get_eventLinkRepeater: function SpottedScript_Styled_Home_View$get_eventLinkRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLinkRepeater');
    },
    get_noEventsLabel: function SpottedScript_Styled_Home_View$get_noEventsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoEventsLabel');
    },
    get_genericContainerPage: function SpottedScript_Styled_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.Home.View.registerClass('SpottedScript.Styled.Home.View', SpottedScript.StyledUserControl.View);
