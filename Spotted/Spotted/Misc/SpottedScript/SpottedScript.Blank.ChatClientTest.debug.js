Type.registerNamespace('SpottedScript.Blank.ChatClientTest');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.ChatClientTest.View
SpottedScript.Blank.ChatClientTest.View = function SpottedScript_Blank_ChatClientTest_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.ChatClientTest.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.ChatClientTest.View.prototype = {
    clientId: null,
    get_chatClient: function SpottedScript_Blank_ChatClientTest_View$get_chatClient() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatClient');
    },
    get_navChatClient: function SpottedScript_Blank_ChatClientTest_View$get_navChatClient() {
        /// <value type="SpottedScript.Controls.ChatClient.Controller"></value>
        return eval(this.clientId + '_NavChatClientController');
    },
    get_genericContainerPage: function SpottedScript_Blank_ChatClientTest_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.ChatClientTest.View.registerClass('SpottedScript.Blank.ChatClientTest.View', SpottedScript.BlankUserControl.View);
