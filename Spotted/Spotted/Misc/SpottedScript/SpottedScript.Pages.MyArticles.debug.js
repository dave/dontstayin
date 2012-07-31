Type.registerNamespace('SpottedScript.Pages.MyArticles');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyArticles.View
SpottedScript.Pages.MyArticles.View = function SpottedScript_Pages_MyArticles_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyArticles.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyArticles.View.prototype = {
    clientId: null,
    get_h14: function SpottedScript_Pages_MyArticles_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_currentArticlesPanel: function SpottedScript_Pages_MyArticles_View$get_currentArticlesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CurrentArticlesPanel');
    },
    get_noArticlesDataGridPanel: function SpottedScript_Pages_MyArticles_View$get_noArticlesDataGridPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoArticlesDataGridPanel');
    },
    get_articlesDataGridPanel: function SpottedScript_Pages_MyArticles_View$get_articlesDataGridPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticlesDataGridPanel');
    },
    get_articlesDataGrid: function SpottedScript_Pages_MyArticles_View$get_articlesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticlesDataGrid');
    },
    get_addArticlePanel: function SpottedScript_Pages_MyArticles_View$get_addArticlePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticlePanel');
    },
    get_h11: function SpottedScript_Pages_MyArticles_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_addArticleSubjectMatterPanel: function SpottedScript_Pages_MyArticles_View$get_addArticleSubjectMatterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleSubjectMatterPanel');
    },
    get_addArticleScopeEvent: function SpottedScript_Pages_MyArticles_View$get_addArticleScopeEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleScopeEvent');
    },
    get_addArticleScopeVenue: function SpottedScript_Pages_MyArticles_View$get_addArticleScopeVenue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleScopeVenue');
    },
    get_addArticleScopePlace: function SpottedScript_Pages_MyArticles_View$get_addArticleScopePlace() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleScopePlace');
    },
    get_addArticleScopeCountry: function SpottedScript_Pages_MyArticles_View$get_addArticleScopeCountry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleScopeCountry');
    },
    get_addArticleScopeGeneral: function SpottedScript_Pages_MyArticles_View$get_addArticleScopeGeneral() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleScopeGeneral');
    },
    get_addArticleScopeMultiFinderPanel: function SpottedScript_Pages_MyArticles_View$get_addArticleScopeMultiFinderPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleScopeMultiFinderPanel');
    },
    get_customvalidator13: function SpottedScript_Pages_MyArticles_View$get_customvalidator13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator13');
    },
    get_addArticleTitleTextBox: function SpottedScript_Pages_MyArticles_View$get_addArticleTitleTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleTitleTextBox');
    },
    get_customValidator8: function SpottedScript_Pages_MyArticles_View$get_customValidator8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator8');
    },
    get_addArticleSummaryTextBox: function SpottedScript_Pages_MyArticles_View$get_addArticleSummaryTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddArticleSummaryTextBox');
    },
    get_customValidator3: function SpottedScript_Pages_MyArticles_View$get_customValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator3');
    },
    get_customValidator4: function SpottedScript_Pages_MyArticles_View$get_customValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator4');
    },
    get_addArticleBodyHtml: function SpottedScript_Pages_MyArticles_View$get_addArticleBodyHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_AddArticleBodyHtmlController');
    },
    get_h13: function SpottedScript_Pages_MyArticles_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_button1: function SpottedScript_Pages_MyArticles_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button2: function SpottedScript_Pages_MyArticles_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_editArticleIndexPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleIndexPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleIndexPanel');
    },
    get_h18: function SpottedScript_Pages_MyArticles_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_editArticleIndexPanelArticleNameLabel: function SpottedScript_Pages_MyArticles_View$get_editArticleIndexPanelArticleNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleIndexPanelArticleNameLabel');
    },
    get_editArticleIndexPanelPreviewAnchor: function SpottedScript_Pages_MyArticles_View$get_editArticleIndexPanelPreviewAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleIndexPanelPreviewAnchor');
    },
    get_editArticleIndexPublishPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleIndexPublishPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleIndexPublishPanel');
    },
    get_button3: function SpottedScript_Pages_MyArticles_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_editArticleIndexToDoPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleIndexToDoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleIndexToDoPanel');
    },
    get_editArticleContentTab: function SpottedScript_Pages_MyArticles_View$get_editArticleContentTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleContentTab');
    },
    get_editArticleTitleTab: function SpottedScript_Pages_MyArticles_View$get_editArticleTitleTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleTitleTab');
    },
    get_editArticleLinkTab: function SpottedScript_Pages_MyArticles_View$get_editArticleLinkTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleLinkTab');
    },
    get_editArticleIconTab: function SpottedScript_Pages_MyArticles_View$get_editArticleIconTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleIconTab');
    },
    get_editArticlePhotosTab: function SpottedScript_Pages_MyArticles_View$get_editArticlePhotosTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePhotosTab');
    },
    get_editArticleAdminTab: function SpottedScript_Pages_MyArticles_View$get_editArticleAdminTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleAdminTab');
    },
    get_editArticleTitleSummaryPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleTitleSummaryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleTitleSummaryPanel');
    },
    get_editArticleTitleTextBox: function SpottedScript_Pages_MyArticles_View$get_editArticleTitleTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleTitleTextBox');
    },
    get_requiredfieldvalidator4: function SpottedScript_Pages_MyArticles_View$get_requiredfieldvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator4');
    },
    get_regularexpressionvalidator3: function SpottedScript_Pages_MyArticles_View$get_regularexpressionvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Regularexpressionvalidator3');
    },
    get_customValidator9: function SpottedScript_Pages_MyArticles_View$get_customValidator9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator9');
    },
    get_customValidator10: function SpottedScript_Pages_MyArticles_View$get_customValidator10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator10');
    },
    get_editArticleSummaryTextBox: function SpottedScript_Pages_MyArticles_View$get_editArticleSummaryTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleSummaryTextBox');
    },
    get_requiredfieldvalidator5: function SpottedScript_Pages_MyArticles_View$get_requiredfieldvalidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator5');
    },
    get_regularexpressionvalidator4: function SpottedScript_Pages_MyArticles_View$get_regularexpressionvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Regularexpressionvalidator4');
    },
    get_customValidator11: function SpottedScript_Pages_MyArticles_View$get_customValidator11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator11');
    },
    get_customValidator12: function SpottedScript_Pages_MyArticles_View$get_customValidator12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator12');
    },
    get_button4: function SpottedScript_Pages_MyArticles_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_editArticleSubjectMatterPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleSubjectMatterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleSubjectMatterPanel');
    },
    get_editArticleScopeEvent: function SpottedScript_Pages_MyArticles_View$get_editArticleScopeEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleScopeEvent');
    },
    get_editArticleScopeVenue: function SpottedScript_Pages_MyArticles_View$get_editArticleScopeVenue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleScopeVenue');
    },
    get_editArticleScopePlace: function SpottedScript_Pages_MyArticles_View$get_editArticleScopePlace() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleScopePlace');
    },
    get_editArticleScopeCountry: function SpottedScript_Pages_MyArticles_View$get_editArticleScopeCountry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleScopeCountry');
    },
    get_editArticleScopeGeneral: function SpottedScript_Pages_MyArticles_View$get_editArticleScopeGeneral() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleScopeGeneral');
    },
    get_editArticleScopeMultiFinderPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleScopeMultiFinderPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleScopeMultiFinderPanel');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_MyArticles_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_button10: function SpottedScript_Pages_MyArticles_View$get_button10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button10');
    },
    get_editArticlePicturePanel: function SpottedScript_Pages_MyArticles_View$get_editArticlePicturePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePicturePanel');
    },
    get_editArticlePicture: function SpottedScript_Pages_MyArticles_View$get_editArticlePicture() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePicture');
    },
    get_editArticleBodyPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleBodyPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleBodyPanel');
    },
    get_editArticleBodyPageRepeater: function SpottedScript_Pages_MyArticles_View$get_editArticleBodyPageRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleBodyPageRepeater');
    },
    get_editArticleParaPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPanel');
    },
    get_h110: function SpottedScript_Pages_MyArticles_View$get_h110() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H110');
    },
    get_editArticleParaTypeTitle: function SpottedScript_Pages_MyArticles_View$get_editArticleParaTypeTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaTypeTitle');
    },
    get_editArticleParaTypePara: function SpottedScript_Pages_MyArticles_View$get_editArticleParaTypePara() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaTypePara');
    },
    get_customValidator5: function SpottedScript_Pages_MyArticles_View$get_customValidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator5');
    },
    get_customValidator6: function SpottedScript_Pages_MyArticles_View$get_customValidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator6');
    },
    get_editArticleParaHtml: function SpottedScript_Pages_MyArticles_View$get_editArticleParaHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_EditArticleParaHtmlController');
    },
    get_button5: function SpottedScript_Pages_MyArticles_View$get_button5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button5');
    },
    get_button6: function SpottedScript_Pages_MyArticles_View$get_button6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button6');
    },
    get_button7: function SpottedScript_Pages_MyArticles_View$get_button7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button7');
    },
    get_editArticleParaPhotoPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoPanel');
    },
    get_h114: function SpottedScript_Pages_MyArticles_View$get_h114() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H114');
    },
    get_editArticleParaPhotoPositionTop: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoPositionTop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoPositionTop');
    },
    get_editArticleParaPhotoPositionLeft: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoPositionLeft() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoPositionLeft');
    },
    get_editArticleParaPhotoPositionRight: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoPositionRight() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoPositionRight');
    },
    get_editArticleParaPhotoPositionBottom: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoPositionBottom() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoPositionBottom');
    },
    get_editArticleParaPhotoPositionHidden: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoPositionHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoPositionHidden');
    },
    get_customvalidator7: function SpottedScript_Pages_MyArticles_View$get_customvalidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator7');
    },
    get_h113: function SpottedScript_Pages_MyArticles_View$get_h113() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H113');
    },
    get_editArticleParaPhotoSourceUploadedCheck: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceUploadedCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceUploadedCheck');
    },
    get_editArticleParaPhotoUploadLink: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoUploadLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoUploadLink');
    },
    get_editArticleParaPhotoSourceIFrame: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceIFrame() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceIFrame');
    },
    get_photosIFrame: function SpottedScript_Pages_MyArticles_View$get_photosIFrame() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosIFrame');
    },
    get_editArticleParaPhotoSourceEventPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceEventPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceEventPanel');
    },
    get_editArticleParaPhotoSourceEventCheck: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceEventCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceEventCheck');
    },
    get_editArticleParaPhotoSourceEventGalleriesP: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceEventGalleriesP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceEventGalleriesP');
    },
    get_editArticleParaPhotoSourceEventGalleryDropDown: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceEventGalleryDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceEventGalleryDropDown');
    },
    get_editArticleParaPhotoSourceEventIFrame: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceEventIFrame() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceEventIFrame');
    },
    get_photosEventIFrame: function SpottedScript_Pages_MyArticles_View$get_photosEventIFrame() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosEventIFrame');
    },
    get_editArticleParaPhotoHidden: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoHidden');
    },
    get_editArticleParaPhotoSourceMiscCheck: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceMiscCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceMiscCheck');
    },
    get_editArticleParaPhotoSourceMiscRefP: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceMiscRefP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceMiscRefP');
    },
    get_editArticleParaPhotoSourceKTextBox: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSourceKTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSourceKTextBox');
    },
    get_button8: function SpottedScript_Pages_MyArticles_View$get_button8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button8');
    },
    get_editArticleParaPhotoUpdatePreviewsError: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoUpdatePreviewsError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoUpdatePreviewsError');
    },
    get_editArticleParaPhotoShowPhotoButton: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoShowPhotoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoShowPhotoButton');
    },
    get_editArticleParaPhotoCropperPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoCropperPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoCropperPanel');
    },
    get_h115: function SpottedScript_Pages_MyArticles_View$get_h115() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H115');
    },
    get_editArticleParaPhotoSaveNoResizeP: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoSaveNoResizeP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoSaveNoResizeP');
    },
    get_linkButton1: function SpottedScript_Pages_MyArticles_View$get_linkButton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkButton1');
    },
    get_editArticleParaPhotoCropper: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoCropper() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoCropper');
    },
    get_editArticleParaPhotoVideoPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoVideoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoVideoPanel');
    },
    get_dfgkjgs: function SpottedScript_Pages_MyArticles_View$get_dfgkjgs() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_dfgkjgs');
    },
    get_editArticleParaPhotoVideo: function SpottedScript_Pages_MyArticles_View$get_editArticleParaPhotoVideo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleParaPhotoVideo');
    },
    get_h117: function SpottedScript_Pages_MyArticles_View$get_h117() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H117');
    },
    get_button9: function SpottedScript_Pages_MyArticles_View$get_button9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button9');
    },
    get_button11: function SpottedScript_Pages_MyArticles_View$get_button11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button11');
    },
    get_editArticlePhotoUploadPanel: function SpottedScript_Pages_MyArticles_View$get_editArticlePhotoUploadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePhotoUploadPanel');
    },
    get_editArticlePhotoUploadLink: function SpottedScript_Pages_MyArticles_View$get_editArticlePhotoUploadLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePhotoUploadLink');
    },
    get_editArticlePhotoNoPhotosPanel: function SpottedScript_Pages_MyArticles_View$get_editArticlePhotoNoPhotosPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePhotoNoPhotosPanel');
    },
    get_h112: function SpottedScript_Pages_MyArticles_View$get_h112() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H112');
    },
    get_editArticlePhotoGalleryDataList: function SpottedScript_Pages_MyArticles_View$get_editArticlePhotoGalleryDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePhotoGalleryDataList');
    },
    get_editArticlePhotoEditLink: function SpottedScript_Pages_MyArticles_View$get_editArticlePhotoEditLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePhotoEditLink');
    },
    get_editArticleAdminPanel: function SpottedScript_Pages_MyArticles_View$get_editArticleAdminPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleAdminPanel');
    },
    get_editArticleAdminStatusDrop: function SpottedScript_Pages_MyArticles_View$get_editArticleAdminStatusDrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleAdminStatusDrop');
    },
    get_editArticleStatusDisplay: function SpottedScript_Pages_MyArticles_View$get_editArticleStatusDisplay() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleStatusDisplay');
    },
    get_updatePanel2: function SpottedScript_Pages_MyArticles_View$get_updatePanel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UpdatePanel2');
    },
    get_editArticleRelevanceGeneral: function SpottedScript_Pages_MyArticles_View$get_editArticleRelevanceGeneral() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleRelevanceGeneral');
    },
    get_editArticleRelevanceCountry: function SpottedScript_Pages_MyArticles_View$get_editArticleRelevanceCountry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleRelevanceCountry');
    },
    get_editArticleRelevancePlace: function SpottedScript_Pages_MyArticles_View$get_editArticleRelevancePlace() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleRelevancePlace');
    },
    get_editArticleRelevanceVenue: function SpottedScript_Pages_MyArticles_View$get_editArticleRelevanceVenue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleRelevanceVenue');
    },
    get_editArticleRelevanceEvent: function SpottedScript_Pages_MyArticles_View$get_editArticleRelevanceEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleRelevanceEvent');
    },
    get_editArticleRelevanceFrontPageAboveFold: function SpottedScript_Pages_MyArticles_View$get_editArticleRelevanceFrontPageAboveFold() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleRelevanceFrontPageAboveFold');
    },
    get_editArticleExtended: function SpottedScript_Pages_MyArticles_View$get_editArticleExtended() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleExtended');
    },
    get_editArticleMixmag: function SpottedScript_Pages_MyArticles_View$get_editArticleMixmag() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleMixmag');
    },
    get_mixmagSectionsListBox: function SpottedScript_Pages_MyArticles_View$get_mixmagSectionsListBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MixmagSectionsListBox');
    },
    get_button12: function SpottedScript_Pages_MyArticles_View$get_button12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button12');
    },
    get_cantEditPanel: function SpottedScript_Pages_MyArticles_View$get_cantEditPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CantEditPanel');
    },
    get_addArticleScopeMultiPicker: function SpottedScript_Pages_MyArticles_View$get_addArticleScopeMultiPicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_AddArticleScopeMultiPickerController');
    },
    get_editArticleTitleSummaryPanelSavedLabel: function SpottedScript_Pages_MyArticles_View$get_editArticleTitleSummaryPanelSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleTitleSummaryPanelSavedLabel');
    },
    get_editArticleSubjectMatterPanelSavedLabel: function SpottedScript_Pages_MyArticles_View$get_editArticleSubjectMatterPanelSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticleSubjectMatterPanelSavedLabel');
    },
    get_editArticlePicturePanelSavedLabel: function SpottedScript_Pages_MyArticles_View$get_editArticlePicturePanelSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArticlePicturePanelSavedLabel');
    },
    get_editArticleScopeMultiPicker: function SpottedScript_Pages_MyArticles_View$get_editArticleScopeMultiPicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_EditArticleScopeMultiPickerController');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyArticles_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyArticles.View.registerClass('SpottedScript.Pages.MyArticles.View', SpottedScript.DsiUserControl.View);
