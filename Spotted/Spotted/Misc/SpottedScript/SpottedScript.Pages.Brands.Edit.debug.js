Type.registerNamespace('SpottedScript.Pages.Brands.Edit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Brands.Edit.View
SpottedScript.Pages.Brands.Edit.View = function SpottedScript_Pages_Brands_Edit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Brands.Edit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Brands.Edit.View.prototype = {
    clientId: null,
    get_renameError: function SpottedScript_Pages_Brands_Edit_View$get_renameError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RenameError');
    },
    get_panelManage: function SpottedScript_Pages_Brands_Edit_View$get_panelManage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelManage');
    },
    get_h14: function SpottedScript_Pages_Brands_Edit_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_manageBrandAnchor: function SpottedScript_Pages_Brands_Edit_View$get_manageBrandAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManageBrandAnchor');
    },
    get_h12: function SpottedScript_Pages_Brands_Edit_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_manageNameTextBox: function SpottedScript_Pages_Brands_Edit_View$get_manageNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManageNameTextBox');
    },
    get_button1: function SpottedScript_Pages_Brands_Edit_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_h15: function SpottedScript_Pages_Brands_Edit_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_managePicUploadPanel: function SpottedScript_Pages_Brands_Edit_View$get_managePicUploadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManagePicUploadPanel');
    },
    get_managePic: function SpottedScript_Pages_Brands_Edit_View$get_managePic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManagePic');
    },
    get_superAdminOptions: function SpottedScript_Pages_Brands_Edit_View$get_superAdminOptions() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuperAdminOptions');
    },
    get_h16: function SpottedScript_Pages_Brands_Edit_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_manageDeleteButton: function SpottedScript_Pages_Brands_Edit_View$get_manageDeleteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManageDeleteButton');
    },
    get_manageDeleteDone: function SpottedScript_Pages_Brands_Edit_View$get_manageDeleteDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ManageDeleteDone');
    },
    get_uiManageGotoAutoComplete: function SpottedScript_Pages_Brands_Edit_View$get_uiManageGotoAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiManageGotoAutoCompleteBehaviour');
    },
    get_button2: function SpottedScript_Pages_Brands_Edit_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_genericContainerPage: function SpottedScript_Pages_Brands_Edit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Brands.Edit.View.registerClass('SpottedScript.Pages.Brands.Edit.View', SpottedScript.DsiUserControl.View);
