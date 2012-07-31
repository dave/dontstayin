Type.registerNamespace('SpottedScript.Pages.Online');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Online.View
SpottedScript.Pages.Online.View = function SpottedScript_Pages_Online_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Online.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Online.View.prototype = {
    clientId: null,
    get_onlineLabel: function SpottedScript_Pages_Online_View$get_onlineLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OnlineLabel');
    },
    get_onlineP: function SpottedScript_Pages_Online_View$get_onlineP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OnlineP');
    },
    get_onlineDataList: function SpottedScript_Pages_Online_View$get_onlineDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OnlineDataList');
    },
    get_genericContainerPage: function SpottedScript_Pages_Online_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Online.View.registerClass('SpottedScript.Pages.Online.View', SpottedScript.DsiUserControl.View);
