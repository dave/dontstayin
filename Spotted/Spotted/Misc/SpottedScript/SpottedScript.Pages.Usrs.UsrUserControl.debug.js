Type.registerNamespace('SpottedScript.Pages.Usrs.UsrUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.UsrUserControl.View
SpottedScript.Pages.Usrs.UsrUserControl.View = function SpottedScript_Pages_Usrs_UsrUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.UsrUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.UsrUserControl.View.prototype = {
    clientId: null,
    get_bannedUserPanel: function SpottedScript_Pages_Usrs_UsrUserControl_View$get_bannedUserPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannedUserPanel');
    },
    get_unsubscribedUserPanel: function SpottedScript_Pages_Usrs_UsrUserControl_View$get_unsubscribedUserPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UnsubscribedUserPanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_UsrUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.UsrUserControl.View.registerClass('SpottedScript.Pages.Usrs.UsrUserControl.View', SpottedScript.DsiUserControl.View);
