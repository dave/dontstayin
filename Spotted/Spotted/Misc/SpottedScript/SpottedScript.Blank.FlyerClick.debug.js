Type.registerNamespace('SpottedScript.Blank.FlyerClick');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.FlyerClick.View
SpottedScript.Blank.FlyerClick.View = function SpottedScript_Blank_FlyerClick_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.FlyerClick.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.FlyerClick.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_FlyerClick_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.FlyerClick.View.registerClass('SpottedScript.Blank.FlyerClick.View', SpottedScript.BlankUserControl.View);
