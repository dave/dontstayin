Type.registerNamespace('SpottedScript.Pages.Guestlists');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Guestlists.View
SpottedScript.Pages.Guestlists.View = function SpottedScript_Pages_Guestlists_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Guestlists.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Guestlists.View.prototype = {
    clientId: null,
    get_h17: function SpottedScript_Pages_Guestlists_View$get_h17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17');
    },
    get_button1: function SpottedScript_Pages_Guestlists_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button2: function SpottedScript_Pages_Guestlists_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_otherEventDataList: function SpottedScript_Pages_Guestlists_View$get_otherEventDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OtherEventDataList');
    },
    get_currentEventDataList: function SpottedScript_Pages_Guestlists_View$get_currentEventDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CurrentEventDataList');
    },
    get_genericContainerPage: function SpottedScript_Pages_Guestlists_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Guestlists.View.registerClass('SpottedScript.Pages.Guestlists.View', SpottedScript.DsiUserControl.View);
