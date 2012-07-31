Type.registerNamespace('SpottedScript.Pages.Usrs.Buddies');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.Buddies.View
SpottedScript.Pages.Usrs.Buddies.View = function SpottedScript_Pages_Usrs_Buddies_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.Buddies.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.Buddies.View.prototype = {
    clientId: null,
    get_usrBrowser: function SpottedScript_Pages_Usrs_Buddies_View$get_usrBrowser() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_usrBrowser');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_Buddies_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.Buddies.View.registerClass('SpottedScript.Pages.Usrs.Buddies.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
