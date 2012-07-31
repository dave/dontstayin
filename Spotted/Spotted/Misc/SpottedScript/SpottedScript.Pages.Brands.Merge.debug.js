Type.registerNamespace('SpottedScript.Pages.Brands.Merge');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Brands.Merge.View
SpottedScript.Pages.Brands.Merge.View = function SpottedScript_Pages_Brands_Merge_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Brands.Merge.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Brands.Merge.View.prototype = {
    clientId: null,
    get_panelMerge: function SpottedScript_Pages_Brands_Merge_View$get_panelMerge() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMerge');
    },
    get_header: function SpottedScript_Pages_Brands_Merge_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_uiMasterBrandUrl: function SpottedScript_Pages_Brands_Merge_View$get_uiMasterBrandUrl() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiMasterBrandUrlBehaviour');
    },
    get_uiMergeBrandUrl: function SpottedScript_Pages_Brands_Merge_View$get_uiMergeBrandUrl() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiMergeBrandUrlBehaviour');
    },
    get_mergeButton: function SpottedScript_Pages_Brands_Merge_View$get_mergeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MergeButton');
    },
    get_genericContainerPage: function SpottedScript_Pages_Brands_Merge_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Brands.Merge.View.registerClass('SpottedScript.Pages.Brands.Merge.View', SpottedScript.DsiUserControl.View);
