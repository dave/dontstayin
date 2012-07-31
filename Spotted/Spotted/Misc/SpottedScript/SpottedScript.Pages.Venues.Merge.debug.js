Type.registerNamespace('SpottedScript.Pages.Venues.Merge');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Venues.Merge.View
SpottedScript.Pages.Venues.Merge.View = function SpottedScript_Pages_Venues_Merge_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Venues.Merge.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Venues.Merge.View.prototype = {
    clientId: null,
    get_uiMasterVenuePicker: function SpottedScript_Pages_Venues_Merge_View$get_uiMasterVenuePicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_uiMasterVenuePickerController');
    },
    get_uiMergeVenuePicker: function SpottedScript_Pages_Venues_Merge_View$get_uiMergeVenuePicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_uiMergeVenuePickerController');
    },
    get_panelMerge: function SpottedScript_Pages_Venues_Merge_View$get_panelMerge() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMerge');
    },
    get_h11: function SpottedScript_Pages_Venues_Merge_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_uiMasterVenueAutoComplete: function SpottedScript_Pages_Venues_Merge_View$get_uiMasterVenueAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiMasterVenueAutoCompleteBehaviour');
    },
    get_uiMergeVenueAutoComplete: function SpottedScript_Pages_Venues_Merge_View$get_uiMergeVenueAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiMergeVenueAutoCompleteBehaviour');
    },
    get_mergeButton: function SpottedScript_Pages_Venues_Merge_View$get_mergeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MergeButton');
    },
    get_genericContainerPage: function SpottedScript_Pages_Venues_Merge_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Venues.Merge.View.registerClass('SpottedScript.Pages.Venues.Merge.View', SpottedScript.DsiUserControl.View);
