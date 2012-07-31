Type.registerNamespace('SpottedScript.Blank.Flyer');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Flyer.View
SpottedScript.Blank.Flyer.View = function SpottedScript_Blank_Flyer_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Flyer.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Flyer.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_Flyer_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Flyer.View.registerClass('SpottedScript.Blank.Flyer.View', SpottedScript.BlankUserControl.View);
