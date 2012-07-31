Type.registerNamespace('SpottedScript.Pages.ModerateNews');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.ModerateNews.View
SpottedScript.Pages.ModerateNews.View = function SpottedScript_Pages_ModerateNews_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.ModerateNews.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.ModerateNews.View.prototype = {
    clientId: null,
    get_h16: function SpottedScript_Pages_ModerateNews_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_h13: function SpottedScript_Pages_ModerateNews_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_h11: function SpottedScript_Pages_ModerateNews_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_itemsPanel: function SpottedScript_Pages_ModerateNews_View$get_itemsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ItemsPanel');
    },
    get_itemsRepeater: function SpottedScript_Pages_ModerateNews_View$get_itemsRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ItemsRepeater');
    },
    get_outputP: function SpottedScript_Pages_ModerateNews_View$get_outputP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OutputP');
    },
    get_moderatorsDataGrid: function SpottedScript_Pages_ModerateNews_View$get_moderatorsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorsDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Pages_ModerateNews_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.ModerateNews.View.registerClass('SpottedScript.Pages.ModerateNews.View', SpottedScript.DsiUserControl.View);
