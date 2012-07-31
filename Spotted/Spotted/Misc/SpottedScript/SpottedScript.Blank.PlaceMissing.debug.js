Type.registerNamespace('SpottedScript.Blank.PlaceMissing');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.PlaceMissing.View
SpottedScript.Blank.PlaceMissing.View = function SpottedScript_Blank_PlaceMissing_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.PlaceMissing.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.PlaceMissing.View.prototype = {
    clientId: null,
    get_introHeader: function SpottedScript_Blank_PlaceMissing_View$get_introHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroHeader');
    },
    get_genericContainerPage: function SpottedScript_Blank_PlaceMissing_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.PlaceMissing.View.registerClass('SpottedScript.Blank.PlaceMissing.View', SpottedScript.BlankUserControl.View);
