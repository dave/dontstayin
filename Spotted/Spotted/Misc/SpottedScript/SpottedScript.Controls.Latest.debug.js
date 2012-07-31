Type.registerNamespace('SpottedScript.Controls.Latest');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Latest.View
SpottedScript.Controls.Latest.View = function SpottedScript_Controls_Latest_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.Latest.View.prototype = {
    clientId: null,
    get_chatHeader: function SpottedScript_Controls_Latest_View$get_chatHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatHeader');
    },
    get_hotTopicsHeader: function SpottedScript_Controls_Latest_View$get_hotTopicsHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTopicsHeader');
    },
    get_chatHeaderSpan: function SpottedScript_Controls_Latest_View$get_chatHeaderSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatHeaderSpan');
    },
    get_chatHolder: function SpottedScript_Controls_Latest_View$get_chatHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatHolder');
    },
    get_addThreadLinkPanel: function SpottedScript_Controls_Latest_View$get_addThreadLinkPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadLinkPanel');
    },
    get_inlineScript1: function SpottedScript_Controls_Latest_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript1');
    },
    get_addThreadStatusHidden: function SpottedScript_Controls_Latest_View$get_addThreadStatusHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadStatusHidden');
    },
    get_addThreadLinkP: function SpottedScript_Controls_Latest_View$get_addThreadLinkP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadLinkP');
    },
    get_addThreadPanel: function SpottedScript_Controls_Latest_View$get_addThreadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPanel');
    },
    get_addThread: function SpottedScript_Controls_Latest_View$get_addThread() {
        /// <value type="SpottedScript.Controls.AddThread.Controller"></value>
        return eval(this.clientId + '_AddThreadController');
    },
    get_latestChatUcHolder: function SpottedScript_Controls_Latest_View$get_latestChatUcHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LatestChatUcHolder');
    },
    get_latestChatUc: function SpottedScript_Controls_Latest_View$get_latestChatUc() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_LatestChatUcController');
    },
    get_latestHotTopicsUcHolder: function SpottedScript_Controls_Latest_View$get_latestHotTopicsUcHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LatestHotTopicsUcHolder');
    },
    get_tabClientScript: function SpottedScript_Controls_Latest_View$get_tabClientScript() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TabClientScript');
    },
    get_latestEventList1: function SpottedScript_Controls_Latest_View$get_latestEventList1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LatestEventList1');
    },
    get_latestHotTopicsUc: function SpottedScript_Controls_Latest_View$get_latestHotTopicsUc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LatestHotTopicsUc');
    },
    get_eventBox: function SpottedScript_Controls_Latest_View$get_eventBox() {
        /// <value type="SpottedScript.Controls.EventBox.Controller"></value>
        return eval(this.clientId + '_EventBoxController');
    },
    get_chatHolderOuter: function SpottedScript_Controls_Latest_View$get_chatHolderOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatHolderOuter');
    },
    get_latestContentUc: function SpottedScript_Controls_Latest_View$get_latestContentUc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LatestContentUc');
    }
}
SpottedScript.Controls.Latest.View.registerClass('SpottedScript.Controls.Latest.View');
