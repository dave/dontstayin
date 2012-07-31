Type.registerNamespace('SpottedScript.Blank.Unsubscribe');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Unsubscribe.View
SpottedScript.Blank.Unsubscribe.View = function SpottedScript_Blank_Unsubscribe_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Unsubscribe.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Unsubscribe.View.prototype = {
    clientId: null,
    get_subscribedPanel: function SpottedScript_Blank_Unsubscribe_View$get_subscribedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SubscribedPanel');
    },
    get_unsubscribedPanel: function SpottedScript_Blank_Unsubscribe_View$get_unsubscribedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UnsubscribedPanel');
    },
    get_cancelPanel: function SpottedScript_Blank_Unsubscribe_View$get_cancelPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelPanel');
    },
    get_addedByUsrDiv: function SpottedScript_Blank_Unsubscribe_View$get_addedByUsrDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddedByUsrDiv');
    },
    get_unsubscribeButton: function SpottedScript_Blank_Unsubscribe_View$get_unsubscribeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UnsubscribeButton');
    },
    get_subscribeButton: function SpottedScript_Blank_Unsubscribe_View$get_subscribeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SubscribeButton');
    },
    get_div1: function SpottedScript_Blank_Unsubscribe_View$get_div1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Div1');
    },
    get_logOffButton: function SpottedScript_Blank_Unsubscribe_View$get_logOffButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LogOffButton');
    },
    get_button2: function SpottedScript_Blank_Unsubscribe_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_genericContainerPage: function SpottedScript_Blank_Unsubscribe_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Unsubscribe.View.registerClass('SpottedScript.Blank.Unsubscribe.View', SpottedScript.BlankUserControl.View);
