Type.registerNamespace('SpottedScript.Pages.LoginTest');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.LoginTest.View
SpottedScript.Pages.LoginTest.View = function SpottedScript_Pages_LoginTest_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.LoginTest.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.LoginTest.View.prototype = {
    clientId: null,
    get_errorP: function SpottedScript_Pages_LoginTest_View$get_errorP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorP');
    },
    get_genericContainerPage: function SpottedScript_Pages_LoginTest_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.LoginTest.View.registerClass('SpottedScript.Pages.LoginTest.View', SpottedScript.DsiUserControl.View);
