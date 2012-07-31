Type.registerNamespace('SpottedScript.Pages.GroupBrowser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.GroupBrowser.View
SpottedScript.Pages.GroupBrowser.View = function SpottedScript_Pages_GroupBrowser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.GroupBrowser.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.GroupBrowser.View.prototype = {
    clientId: null,
    get_panelGroups: function SpottedScript_Pages_GroupBrowser_View$get_panelGroups() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroups');
    },
    get_groupsDataList: function SpottedScript_Pages_GroupBrowser_View$get_groupsDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupsDataList');
    },
    get_header: function SpottedScript_Pages_GroupBrowser_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_genericContainerPage: function SpottedScript_Pages_GroupBrowser_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.GroupBrowser.View.registerClass('SpottedScript.Pages.GroupBrowser.View', SpottedScript.DsiUserControl.View);
