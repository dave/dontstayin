Type.registerNamespace('SpottedScript.MobileUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MobileUserControl.View
SpottedScript.MobileUserControl.View = function SpottedScript_MobileUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MobileUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MobileUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MobileUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MobileUserControl.View.registerClass('SpottedScript.MobileUserControl.View', SpottedScript.GenericUserControl.View);
