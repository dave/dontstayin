Type.registerNamespace('SpottedScript.Blank.Doorlist');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Doorlist.View
SpottedScript.Blank.Doorlist.View = function SpottedScript_Blank_Doorlist_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Doorlist.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Doorlist.View.prototype = {
    clientId: null,
    get_uiDoorlist: function SpottedScript_Blank_Doorlist_View$get_uiDoorlist() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDoorlist');
    },
    get_genericContainerPage: function SpottedScript_Blank_Doorlist_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Doorlist.View.registerClass('SpottedScript.Blank.Doorlist.View', SpottedScript.BlankUserControl.View);
