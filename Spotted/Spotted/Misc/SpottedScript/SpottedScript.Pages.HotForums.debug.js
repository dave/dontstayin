Type.registerNamespace('SpottedScript.Pages.HotForums');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.HotForums.View
SpottedScript.Pages.HotForums.View = function SpottedScript_Pages_HotForums_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.HotForums.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.HotForums.View.prototype = {
    clientId: null,
    get_h11: function SpottedScript_Pages_HotForums_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_panelBoardList: function SpottedScript_Pages_HotForums_View$get_panelBoardList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelBoardList');
    },
    get_boardPlacePanel: function SpottedScript_Pages_HotForums_View$get_boardPlacePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BoardPlacePanel');
    },
    get_boardEventPanel: function SpottedScript_Pages_HotForums_View$get_boardEventPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BoardEventPanel');
    },
    get_boardThreadPanel: function SpottedScript_Pages_HotForums_View$get_boardThreadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BoardThreadPanel');
    },
    get_boardDataGrid: function SpottedScript_Pages_HotForums_View$get_boardDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BoardDataGrid');
    },
    get_boardPlaceDataGrid: function SpottedScript_Pages_HotForums_View$get_boardPlaceDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BoardPlaceDataGrid');
    },
    get_boardEventDataGrid: function SpottedScript_Pages_HotForums_View$get_boardEventDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BoardEventDataGrid');
    },
    get_boardThreadDataGrid: function SpottedScript_Pages_HotForums_View$get_boardThreadDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BoardThreadDataGrid');
    },
    get_hotTopicsHomeCountryLink: function SpottedScript_Pages_HotForums_View$get_hotTopicsHomeCountryLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTopicsHomeCountryLink');
    },
    get_hotTopicsCountryLink: function SpottedScript_Pages_HotForums_View$get_hotTopicsCountryLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTopicsCountryLink');
    },
    get_hotTopicsCountryPanel: function SpottedScript_Pages_HotForums_View$get_hotTopicsCountryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTopicsCountryPanel');
    },
    get_hotTopicsWorldwidePanel: function SpottedScript_Pages_HotForums_View$get_hotTopicsWorldwidePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTopicsWorldwidePanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_HotForums_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.HotForums.View.registerClass('SpottedScript.Pages.HotForums.View', SpottedScript.DsiUserControl.View);
