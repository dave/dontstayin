Type.registerNamespace('SpottedScript.Mobile.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Mobile.Home.View
SpottedScript.Mobile.Home.View = function SpottedScript_Mobile_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Mobile.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Mobile.Home.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Mobile_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Mobile.Home.View.registerClass('SpottedScript.Mobile.Home.View', SpottedScript.MobileUserControl.View);
