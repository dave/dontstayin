Type.registerNamespace('SpottedScript.Mobile.Home1');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Mobile.Home1.View
SpottedScript.Mobile.Home1.View = function SpottedScript_Mobile_Home1_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Mobile.Home1.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Mobile.Home1.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Mobile_Home1_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Mobile.Home1.View.registerClass('SpottedScript.Mobile.Home1.View', SpottedScript.MobileUserControl.View);
