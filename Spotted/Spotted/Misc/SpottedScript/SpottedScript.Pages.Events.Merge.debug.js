Type.registerNamespace('SpottedScript.Pages.Events.Merge');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Merge.View
SpottedScript.Pages.Events.Merge.View = function SpottedScript_Pages_Events_Merge_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Merge.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Merge.View.prototype = {
    clientId: null,
    get_panelMerge: function SpottedScript_Pages_Events_Merge_View$get_panelMerge() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMerge');
    },
    get_h11: function SpottedScript_Pages_Events_Merge_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_uiMainEventFinder: function SpottedScript_Pages_Events_Merge_View$get_uiMainEventFinder() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_uiMainEventFinderController');
    },
    get_uiMergeEventFinder: function SpottedScript_Pages_Events_Merge_View$get_uiMergeEventFinder() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_uiMergeEventFinderController');
    },
    get_mergeButton: function SpottedScript_Pages_Events_Merge_View$get_mergeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MergeButton');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Merge_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Merge.View.registerClass('SpottedScript.Pages.Events.Merge.View', SpottedScript.DsiUserControl.View);
