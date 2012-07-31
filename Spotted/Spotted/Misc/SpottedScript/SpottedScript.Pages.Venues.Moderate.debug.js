Type.registerNamespace('SpottedScript.Pages.Venues.Moderate');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Venues.Moderate.View
SpottedScript.Pages.Venues.Moderate.View = function SpottedScript_Pages_Venues_Moderate_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Venues.Moderate.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Venues.Moderate.View.prototype = {
    clientId: null,
    get_h16: function SpottedScript_Pages_Venues_Moderate_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_h13: function SpottedScript_Pages_Venues_Moderate_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_button1: function SpottedScript_Pages_Venues_Moderate_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_h11: function SpottedScript_Pages_Venues_Moderate_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_itemsPanel: function SpottedScript_Pages_Venues_Moderate_View$get_itemsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ItemsPanel');
    },
    get_itemsRepeater: function SpottedScript_Pages_Venues_Moderate_View$get_itemsRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ItemsRepeater');
    },
    get_deleteSelectedButton: function SpottedScript_Pages_Venues_Moderate_View$get_deleteSelectedButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteSelectedButton');
    },
    get_outputP: function SpottedScript_Pages_Venues_Moderate_View$get_outputP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OutputP');
    },
    get_moderatorsDataGrid: function SpottedScript_Pages_Venues_Moderate_View$get_moderatorsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorsDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Pages_Venues_Moderate_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Venues.Moderate.View.registerClass('SpottedScript.Pages.Venues.Moderate.View', SpottedScript.DsiUserControl.View);
