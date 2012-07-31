Type.registerNamespace('SpottedScript.Pages.Promoters.Files');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Files.View
SpottedScript.Pages.Promoters.Files.View = function SpottedScript_Pages_Promoters_Files_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Files.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Files.View.prototype = {
    clientId: null,
    get_header: function SpottedScript_Pages_Promoters_Files_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_panelDelete: function SpottedScript_Pages_Promoters_Files_View$get_panelDelete() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDelete');
    },
    get_panelList: function SpottedScript_Pages_Promoters_Files_View$get_panelList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelList');
    },
    get_miscDataGrid: function SpottedScript_Pages_Promoters_Files_View$get_miscDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MiscDataGrid');
    },
    get_miscNoFilesP: function SpottedScript_Pages_Promoters_Files_View$get_miscNoFilesP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MiscNoFilesP');
    },
    get_miscDataGridP: function SpottedScript_Pages_Promoters_Files_View$get_miscDataGridP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MiscDataGridP');
    },
    get_panelUpload: function SpottedScript_Pages_Promoters_Files_View$get_panelUpload() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelUpload');
    },
    get_panelView: function SpottedScript_Pages_Promoters_Files_View$get_panelView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelView');
    },
    get_viewBackAnchor: function SpottedScript_Pages_Promoters_Files_View$get_viewBackAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewBackAnchor');
    },
    get_viewNameAnchor: function SpottedScript_Pages_Promoters_Files_View$get_viewNameAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewNameAnchor');
    },
    get_viewUrlTextBox: function SpottedScript_Pages_Promoters_Files_View$get_viewUrlTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewUrlTextBox');
    },
    get_viewImageHtmlTextBox: function SpottedScript_Pages_Promoters_Files_View$get_viewImageHtmlTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewImageHtmlTextBox');
    },
    get_viewNameCell: function SpottedScript_Pages_Promoters_Files_View$get_viewNameCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewNameCell');
    },
    get_viewImageWidthCell: function SpottedScript_Pages_Promoters_Files_View$get_viewImageWidthCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewImageWidthCell');
    },
    get_viewImageHeightCell: function SpottedScript_Pages_Promoters_Files_View$get_viewImageHeightCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewImageHeightCell');
    },
    get_viewImageFileSizeCell: function SpottedScript_Pages_Promoters_Files_View$get_viewImageFileSizeCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewImageFileSizeCell');
    },
    get_viewLeaderboardImg: function SpottedScript_Pages_Promoters_Files_View$get_viewLeaderboardImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewLeaderboardImg');
    },
    get_viewHotboxImg: function SpottedScript_Pages_Promoters_Files_View$get_viewHotboxImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewHotboxImg');
    },
    get_viewPhotoBannerImg: function SpottedScript_Pages_Promoters_Files_View$get_viewPhotoBannerImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewPhotoBannerImg');
    },
    get_viewEmailBannerImg: function SpottedScript_Pages_Promoters_Files_View$get_viewEmailBannerImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewEmailBannerImg');
    },
    get_viewSkyscraperImg: function SpottedScript_Pages_Promoters_Files_View$get_viewSkyscraperImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewSkyscraperImg');
    },
    get_viewLeaderboardLabel: function SpottedScript_Pages_Promoters_Files_View$get_viewLeaderboardLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewLeaderboardLabel');
    },
    get_viewHotboxLabel: function SpottedScript_Pages_Promoters_Files_View$get_viewHotboxLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewHotboxLabel');
    },
    get_viewPhotoBannerLabel: function SpottedScript_Pages_Promoters_Files_View$get_viewPhotoBannerLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewPhotoBannerLabel');
    },
    get_viewEmailBannerLabel: function SpottedScript_Pages_Promoters_Files_View$get_viewEmailBannerLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewEmailBannerLabel');
    },
    get_viewSkyscraperLabel: function SpottedScript_Pages_Promoters_Files_View$get_viewSkyscraperLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewSkyscraperLabel');
    },
    get_viewImageHtmlTr: function SpottedScript_Pages_Promoters_Files_View$get_viewImageHtmlTr() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewImageHtmlTr');
    },
    get_promoterIntro: function SpottedScript_Pages_Promoters_Files_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_h11: function SpottedScript_Pages_Promoters_Files_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_inputFile: function SpottedScript_Pages_Promoters_Files_View$get_inputFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InputFile');
    },
    get_h12: function SpottedScript_Pages_Promoters_Files_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_viewBannerBody: function SpottedScript_Pages_Promoters_Files_View$get_viewBannerBody() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewBannerBody');
    },
    get_viewImageBody: function SpottedScript_Pages_Promoters_Files_View$get_viewImageBody() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewImageBody');
    },
    get_viewBrokenImg: function SpottedScript_Pages_Promoters_Files_View$get_viewBrokenImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewBrokenImg');
    },
    get_viewBrokenLabel: function SpottedScript_Pages_Promoters_Files_View$get_viewBrokenLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewBrokenLabel');
    },
    get_requiredFlashVersionTr: function SpottedScript_Pages_Promoters_Files_View$get_requiredFlashVersionTr() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFlashVersionTr');
    },
    get_requiredFlashVersion: function SpottedScript_Pages_Promoters_Files_View$get_requiredFlashVersion() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFlashVersion');
    },
    get_updateFlashVersionDone: function SpottedScript_Pages_Promoters_Files_View$get_updateFlashVersionDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UpdateFlashVersionDone');
    },
    get_sizeWidth: function SpottedScript_Pages_Promoters_Files_View$get_sizeWidth() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SizeWidth');
    },
    get_sizeHeight: function SpottedScript_Pages_Promoters_Files_View$get_sizeHeight() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SizeHeight');
    },
    get_button1: function SpottedScript_Pages_Promoters_Files_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_updateSizeDone: function SpottedScript_Pages_Promoters_Files_View$get_updateSizeDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UpdateSizeDone');
    },
    get_h13: function SpottedScript_Pages_Promoters_Files_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Files_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Files.View.registerClass('SpottedScript.Pages.Promoters.Files.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
