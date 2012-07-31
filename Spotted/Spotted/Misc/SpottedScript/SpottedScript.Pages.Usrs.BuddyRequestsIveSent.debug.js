Type.registerNamespace('SpottedScript.Pages.Usrs.BuddyRequestsIveSent');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View
SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View = function SpottedScript_Pages_Usrs_BuddyRequestsIveSent_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View.prototype = {
    clientId: null,
    get_usrBrowser: function SpottedScript_Pages_Usrs_BuddyRequestsIveSent_View$get_usrBrowser() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_usrBrowser');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_BuddyRequestsIveSent_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View.registerClass('SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
