Type.registerNamespace('SpottedScript.Admin.Stats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.Stats.View
SpottedScript.Admin.Stats.View = function SpottedScript_Admin_Stats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.Stats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.Stats.View.prototype = {
    clientId: null,
    get_usersOnline5MinLabel: function SpottedScript_Admin_Stats_View$get_usersOnline5MinLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsersOnline5MinLabel');
    },
    get_maxUsersOnline5MinLabel: function SpottedScript_Admin_Stats_View$get_maxUsersOnline5MinLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxUsersOnline5MinLabel');
    },
    get_maxUsersOnline5MinDateLabel: function SpottedScript_Admin_Stats_View$get_maxUsersOnline5MinDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxUsersOnline5MinDateLabel');
    },
    get_usersOnline30MinLabel: function SpottedScript_Admin_Stats_View$get_usersOnline30MinLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsersOnline30MinLabel');
    },
    get_maxUsersOnline30MinLabel: function SpottedScript_Admin_Stats_View$get_maxUsersOnline30MinLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MaxUsersOnline30MinLabel');
    },
    get_h11: function SpottedScript_Admin_Stats_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_genericContainerPage: function SpottedScript_Admin_Stats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.Stats.View.registerClass('SpottedScript.Admin.Stats.View', SpottedScript.AdminUserControl.View);
