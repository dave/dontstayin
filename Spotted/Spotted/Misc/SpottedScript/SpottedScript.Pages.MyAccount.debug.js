Type.registerNamespace('SpottedScript.Pages.MyAccount');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyAccount.View
SpottedScript.Pages.MyAccount.View = function SpottedScript_Pages_MyAccount_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyAccount.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyAccount.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_MyAccount_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyAccount.View.registerClass('SpottedScript.Pages.MyAccount.View', SpottedScript.DsiUserControl.View);
