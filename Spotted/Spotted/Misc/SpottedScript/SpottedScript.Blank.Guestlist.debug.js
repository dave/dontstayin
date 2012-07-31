Type.registerNamespace('SpottedScript.Blank.Guestlist');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Guestlist.View
SpottedScript.Blank.Guestlist.View = function SpottedScript_Blank_Guestlist_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Guestlist.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Guestlist.View.prototype = {
    clientId: null,
    get_guestlistDataList: function SpottedScript_Blank_Guestlist_View$get_guestlistDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GuestlistDataList');
    },
    get_eventLabel: function SpottedScript_Blank_Guestlist_View$get_eventLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLabel');
    },
    get_priceLabel: function SpottedScript_Blank_Guestlist_View$get_priceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PriceLabel');
    },
    get_genericContainerPage: function SpottedScript_Blank_Guestlist_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Guestlist.View.registerClass('SpottedScript.Blank.Guestlist.View', SpottedScript.BlankUserControl.View);
