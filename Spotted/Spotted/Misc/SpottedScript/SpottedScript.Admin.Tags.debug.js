Type.registerNamespace('SpottedScript.Admin.Tags');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.Tags.View
SpottedScript.Admin.Tags.View = function SpottedScript_Admin_Tags_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.Tags.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.Tags.View.prototype = {
    clientId: null,
    get_uiTitle: function SpottedScript_Admin_Tags_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_uiFiltersPanel: function SpottedScript_Admin_Tags_View$get_uiFiltersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFiltersPanel');
    },
    get_uiTagTextFilter: function SpottedScript_Admin_Tags_View$get_uiTagTextFilter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTagTextFilter');
    },
    get_uiBlockedFilter: function SpottedScript_Admin_Tags_View$get_uiBlockedFilter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBlockedFilter');
    },
    get_uiShowInTagCloudFilter: function SpottedScript_Admin_Tags_View$get_uiShowInTagCloudFilter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiShowInTagCloudFilter');
    },
    get_uiFilter: function SpottedScript_Admin_Tags_View$get_uiFilter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFilter');
    },
    get_uiResultsTitle: function SpottedScript_Admin_Tags_View$get_uiResultsTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiResultsTitle');
    },
    get_uiResultsPanel: function SpottedScript_Admin_Tags_View$get_uiResultsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiResultsPanel');
    },
    get_uiSaveChanges: function SpottedScript_Admin_Tags_View$get_uiSaveChanges() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSaveChanges');
    },
    get_uiResults: function SpottedScript_Admin_Tags_View$get_uiResults() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiResults');
    },
    get_genericContainerPage: function SpottedScript_Admin_Tags_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.Tags.View.registerClass('SpottedScript.Admin.Tags.View', SpottedScript.AdminUserControl.View);
