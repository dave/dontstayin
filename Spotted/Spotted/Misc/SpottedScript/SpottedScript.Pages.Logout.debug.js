Type.registerNamespace('SpottedScript.Pages.Logout');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Logout.View
SpottedScript.Pages.Logout.View = function SpottedScript_Pages_Logout_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Logout.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Logout.View.prototype = {
    clientId: null,
    get_autoLogout_Value: function SpottedScript_Pages_Logout_View$get_autoLogout_Value() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogout_Value');
    },
    get_genericContainerPage: function SpottedScript_Pages_Logout_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Logout.View.registerClass('SpottedScript.Pages.Logout.View', SpottedScript.DsiUserControl.View);
