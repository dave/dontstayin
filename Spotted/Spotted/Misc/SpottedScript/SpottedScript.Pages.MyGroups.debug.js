Type.registerNamespace('SpottedScript.Pages.MyGroups');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyGroups.View
SpottedScript.Pages.MyGroups.View = function SpottedScript_Pages_MyGroups_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyGroups.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyGroups.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Pages_MyGroups_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h11: function SpottedScript_Pages_MyGroups_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_inlineScript3: function SpottedScript_Pages_MyGroups_View$get_inlineScript3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript3');
    },
    get_addThread: function SpottedScript_Pages_MyGroups_View$get_addThread() {
        /// <value type="SpottedScript.Controls.AddThread.Controller"></value>
        return eval(this.clientId + '_AddThreadController');
    },
    get_addThreadLinkP: function SpottedScript_Pages_MyGroups_View$get_addThreadLinkP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadLinkP');
    },
    get_addThreadStatusHidden: function SpottedScript_Pages_MyGroups_View$get_addThreadStatusHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadStatusHidden');
    },
    get_addThreadPanel: function SpottedScript_Pages_MyGroups_View$get_addThreadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPanel');
    },
    get_groupsPanel: function SpottedScript_Pages_MyGroups_View$get_groupsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupsPanel');
    },
    get_groupsDataGrid: function SpottedScript_Pages_MyGroups_View$get_groupsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupsDataGrid');
    },
    get_panelGroupsList: function SpottedScript_Pages_MyGroups_View$get_panelGroupsList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupsList');
    },
    get_panelNoGroups: function SpottedScript_Pages_MyGroups_View$get_panelNoGroups() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoGroups');
    },
    get_panelInvites: function SpottedScript_Pages_MyGroups_View$get_panelInvites() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelInvites');
    },
    get_invitesDataGrid: function SpottedScript_Pages_MyGroups_View$get_invitesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvitesDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyGroups_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyGroups.View.registerClass('SpottedScript.Pages.MyGroups.View', SpottedScript.DsiUserControl.View);
