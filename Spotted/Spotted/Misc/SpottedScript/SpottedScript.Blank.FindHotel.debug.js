Type.registerNamespace('SpottedScript.Blank.FindHotel');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.FindHotel.View
SpottedScript.Blank.FindHotel.View = function SpottedScript_Blank_FindHotel_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.FindHotel.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.FindHotel.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_FindHotel_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.FindHotel.View.registerClass('SpottedScript.Blank.FindHotel.View', SpottedScript.BlankUserControl.View);
