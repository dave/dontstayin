Type.registerNamespace('SpottedScript.Pages.Galleries.Edit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Galleries.Edit.View
SpottedScript.Pages.Galleries.Edit.View = function SpottedScript_Pages_Galleries_Edit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Galleries.Edit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Galleries.Edit.View.prototype = {
    clientId: null,
    get_infoPanel: function SpottedScript_Pages_Galleries_Edit_View$get_infoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoPanel');
    },
    get_h11: function SpottedScript_Pages_Galleries_Edit_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_titleImgCell: function SpottedScript_Pages_Galleries_Edit_View$get_titleImgCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TitleImgCell');
    },
    get_titlePicImg: function SpottedScript_Pages_Galleries_Edit_View$get_titlePicImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TitlePicImg');
    },
    get_galleryName: function SpottedScript_Pages_Galleries_Edit_View$get_galleryName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleryName');
    },
    get_button1: function SpottedScript_Pages_Galleries_Edit_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_requiredfieldvalidator1: function SpottedScript_Pages_Galleries_Edit_View$get_requiredfieldvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator1');
    },
    get_watchCheckBox: function SpottedScript_Pages_Galleries_Edit_View$get_watchCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchCheckBox');
    },
    get_backToEditArticlePanel: function SpottedScript_Pages_Galleries_Edit_View$get_backToEditArticlePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BackToEditArticlePanel');
    },
    get_sdH11: function SpottedScript_Pages_Galleries_Edit_View$get_sdH11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_sdH11');
    },
    get_photoUploadPanel: function SpottedScript_Pages_Galleries_Edit_View$get_photoUploadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoUploadPanel');
    },
    get_h13: function SpottedScript_Pages_Galleries_Edit_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_panelUpload: function SpottedScript_Pages_Galleries_Edit_View$get_panelUpload() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelUpload');
    },
    get_updatePanel1: function SpottedScript_Pages_Galleries_Edit_View$get_updatePanel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UpdatePanel1');
    },
    get_uiYourPhotos: function SpottedScript_Pages_Galleries_Edit_View$get_uiYourPhotos() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiYourPhotos');
    },
    get_h12: function SpottedScript_Pages_Galleries_Edit_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_galleryEmptyPanel: function SpottedScript_Pages_Galleries_Edit_View$get_galleryEmptyPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleryEmptyPanel');
    },
    get_button4: function SpottedScript_Pages_Galleries_Edit_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_photoProcessingPanel: function SpottedScript_Pages_Galleries_Edit_View$get_photoProcessingPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoProcessingPanel');
    },
    get_button2: function SpottedScript_Pages_Galleries_Edit_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_photoProcessingDataList: function SpottedScript_Pages_Galleries_Edit_View$get_photoProcessingDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoProcessingDataList');
    },
    get_photoModeratePanel: function SpottedScript_Pages_Galleries_Edit_View$get_photoModeratePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoModeratePanel');
    },
    get_photoModerateDataList: function SpottedScript_Pages_Galleries_Edit_View$get_photoModerateDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoModerateDataList');
    },
    get_photoEnabledPanel: function SpottedScript_Pages_Galleries_Edit_View$get_photoEnabledPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoEnabledPanel');
    },
    get_photoEnabledDataList: function SpottedScript_Pages_Galleries_Edit_View$get_photoEnabledDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoEnabledDataList');
    },
    get_photoActionsPanel: function SpottedScript_Pages_Galleries_Edit_View$get_photoActionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoActionsPanel');
    },
    get_deleteSelectedButton: function SpottedScript_Pages_Galleries_Edit_View$get_deleteSelectedButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteSelectedButton');
    },
    get_deleteSelectedOutput: function SpottedScript_Pages_Galleries_Edit_View$get_deleteSelectedOutput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteSelectedOutput');
    },
    get_genericContainerPage: function SpottedScript_Pages_Galleries_Edit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Galleries.Edit.View.registerClass('SpottedScript.Pages.Galleries.Edit.View', SpottedScript.DsiUserControl.View);
