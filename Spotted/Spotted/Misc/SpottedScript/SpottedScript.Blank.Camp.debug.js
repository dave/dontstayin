Type.registerNamespace('SpottedScript.Blank.Camp');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Camp.View
SpottedScript.Blank.Camp.View = function SpottedScript_Blank_Camp_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Camp.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Camp.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_Camp_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Camp.View.registerClass('SpottedScript.Blank.Camp.View', SpottedScript.DsiUserControl.View);
