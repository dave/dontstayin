Type.registerNamespace('SpottedScript.Pages.Stats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Stats.View
SpottedScript.Pages.Stats.View = function SpottedScript_Pages_Stats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Stats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Stats.View.prototype = {
    clientId: null,
    get_usersOnline5MinLabel: function SpottedScript_Pages_Stats_View$get_usersOnline5MinLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsersOnline5MinLabel');
    },
    get_maxUsersOnline5MinDateLabel: function SpottedScript_Pages_Stats_View$get_maxUsersOnline5MinDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxUsersOnline5MinDateLabel');
    },
    get_maxUsersOnline5MinLabel: function SpottedScript_Pages_Stats_View$get_maxUsersOnline5MinLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxUsersOnline5MinLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Stats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Stats.View.registerClass('SpottedScript.Pages.Stats.View', SpottedScript.DsiUserControl.View);
