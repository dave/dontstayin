Type.registerNamespace('SpottedScript.Blank.UnsubscribeEflyers');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.UnsubscribeEflyers.View
SpottedScript.Blank.UnsubscribeEflyers.View = function SpottedScript_Blank_UnsubscribeEflyers_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.UnsubscribeEflyers.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.UnsubscribeEflyers.View.prototype = {
    clientId: null,
    get_uiOptionsPanel: function SpottedScript_Blank_UnsubscribeEflyers_View$get_uiOptionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiOptionsPanel');
    },
    get_uiSendEflyersOptions: function SpottedScript_Blank_UnsubscribeEflyers_View$get_uiSendEflyersOptions() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSendEflyersOptions');
    },
    get_uiSavedPanel: function SpottedScript_Blank_UnsubscribeEflyers_View$get_uiSavedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSavedPanel');
    },
    get_uiSavedSettingLabel: function SpottedScript_Blank_UnsubscribeEflyers_View$get_uiSavedSettingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSavedSettingLabel');
    },
    get_genericContainerPage: function SpottedScript_Blank_UnsubscribeEflyers_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.UnsubscribeEflyers.View.registerClass('SpottedScript.Blank.UnsubscribeEflyers.View', SpottedScript.BlankUserControl.View);
