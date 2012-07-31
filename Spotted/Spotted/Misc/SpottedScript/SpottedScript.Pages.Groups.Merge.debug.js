Type.registerNamespace('SpottedScript.Pages.Groups.Merge');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Merge.View
SpottedScript.Pages.Groups.Merge.View = function SpottedScript_Pages_Groups_Merge_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Merge.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Merge.View.prototype = {
    clientId: null,
    get_panelMerge: function SpottedScript_Pages_Groups_Merge_View$get_panelMerge() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMerge');
    },
    get_header: function SpottedScript_Pages_Groups_Merge_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_uiMasterGroupAutoComplete: function SpottedScript_Pages_Groups_Merge_View$get_uiMasterGroupAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiMasterGroupAutoCompleteBehaviour');
    },
    get_uiMergeGroupAutoComplete: function SpottedScript_Pages_Groups_Merge_View$get_uiMergeGroupAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiMergeGroupAutoCompleteBehaviour');
    },
    get_mergeButton: function SpottedScript_Pages_Groups_Merge_View$get_mergeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MergeButton');
    },
    get_genericContainerPage: function SpottedScript_Pages_Groups_Merge_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Merge.View.registerClass('SpottedScript.Pages.Groups.Merge.View', SpottedScript.DsiUserControl.View);
