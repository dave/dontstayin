Type.registerNamespace('SpottedScript.Pages.Rooms');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Rooms.View
SpottedScript.Pages.Rooms.View = function SpottedScript_Pages_Rooms_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Rooms.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Rooms.View.prototype = {
    clientId: null,
    get_messagesHeader: function SpottedScript_Pages_Rooms_View$get_messagesHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MessagesHeader');
    },
    get_popupAminationsOn: function SpottedScript_Pages_Rooms_View$get_popupAminationsOn() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PopupAminationsOn');
    },
    get_popupAminationsOff: function SpottedScript_Pages_Rooms_View$get_popupAminationsOff() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PopupAminationsOff');
    },
    get_testing: function SpottedScript_Pages_Rooms_View$get_testing() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Testing');
    },
    get_genericContainerPage: function SpottedScript_Pages_Rooms_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Rooms.View.registerClass('SpottedScript.Pages.Rooms.View', SpottedScript.DsiUserControl.View);
