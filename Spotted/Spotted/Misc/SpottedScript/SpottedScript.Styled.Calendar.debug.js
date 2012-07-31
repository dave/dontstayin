Type.registerNamespace('SpottedScript.Styled.Calendar');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.Calendar.View
SpottedScript.Styled.Calendar.View = function SpottedScript_Styled_Calendar_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.Calendar.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.Calendar.View.prototype = {
    clientId: null,
    get_eventLinkRepeater: function SpottedScript_Styled_Calendar_View$get_eventLinkRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLinkRepeater');
    },
    get_noEventsLabel: function SpottedScript_Styled_Calendar_View$get_noEventsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoEventsLabel');
    },
    get_genericContainerPage: function SpottedScript_Styled_Calendar_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.Calendar.View.registerClass('SpottedScript.Styled.Calendar.View', SpottedScript.StyledUserControl.View);
