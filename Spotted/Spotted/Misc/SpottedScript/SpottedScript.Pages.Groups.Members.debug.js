Type.registerNamespace('SpottedScript.Pages.Groups.Members');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Members.View
SpottedScript.Pages.Groups.Members.View = function SpottedScript_Pages_Groups_Members_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Members.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Members.View.prototype = {
    clientId: null,
    get_usrBrowser: function SpottedScript_Pages_Groups_Members_View$get_usrBrowser() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_usrBrowser');
    },
    get_genericContainerPage: function SpottedScript_Pages_Groups_Members_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Members.View.registerClass('SpottedScript.Pages.Groups.Members.View', SpottedScript.DsiUserControl.View);
