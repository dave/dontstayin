Type.registerNamespace('SpottedScript.Controls.Html');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Html.Controller
SpottedScript.Controls.Html.Controller = function SpottedScript_Controls_Html_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.Html.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.Html.View">
    /// </field>
    this._view = view;
    if (view.get_linkUrlButton() != null) {
        $addHandler(view.get_linkUrlButton(), 'click', Function.createDelegate(this, this._linkUrlButtonClicked));
        $addHandler(view.get_linkUrlPanelBackButton(), 'click', Function.createDelegate(this, this._linkUrlBackButtonClicked));
        $addHandler(view.get_flashSwfUrlButton(), 'click', Function.createDelegate(this, this._flashSwfUrlButtonClicked));
        $addHandler(view.get_flashSwfUrlPanelBackButton(), 'click', Function.createDelegate(this, this._flashSwfUrlBackButtonClicked));
        $addHandler(view.get_videoFlvButton(), 'click', Function.createDelegate(this, this._videoFlvUrlButtonClicked));
        $addHandler(view.get_videoFlvPanelBackButton(), 'click', Function.createDelegate(this, this._videoFlvUrlBackButtonClicked));
        $addHandler(view.get_advancedTagsToggleButton(), 'click', Function.createDelegate(this, this._advancedTagsToggleButtonClicked));
        $addHandler(view.get_advancedParseNowButton(), 'click', Function.createDelegate(this, this._advancedParseNowButtonClicked));
        $addHandler(view.get_previewButton(), 'click', Function.createDelegate(this, this._previewButtonClicked));
        $addHandler(view.get_hidePreviewButton(), 'click', Function.createDelegate(this, this._hidePreviewButtonClicked));
    }
}
SpottedScript.Controls.Html.Controller.prototype = {
    _view: null,
    get__saveButton: function SpottedScript_Controls_Html_Controller$get__saveButton() {
        /// <value type="Object" domElement="true"></value>
        return this._view.get_saveButton();
    },
    get__rawHtml: function SpottedScript_Controls_Html_Controller$get__rawHtml() {
        /// <value type="String"></value>
        return this._view.get_htmlTextBox().value;
    },
    get__formatting: function SpottedScript_Controls_Html_Controller$get__formatting() {
        /// <value type="Boolean"></value>
        return this._view.get_advancedFormattingTrueRadio().checked;
    },
    _clearHtml: function SpottedScript_Controls_Html_Controller$_clearHtml() {
        this._view.get_htmlTextBox().value = '';
    },
    _linkUrlButtonClicked: function SpottedScript_Controls_Html_Controller$_linkUrlButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._setLinkPanelVisibility(true);
    },
    _linkUrlBackButtonClicked: function SpottedScript_Controls_Html_Controller$_linkUrlBackButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._setLinkPanelVisibility(false);
    },
    _setLinkPanelVisibility: function SpottedScript_Controls_Html_Controller$_setLinkPanelVisibility(moreOptions) {
        /// <param name="moreOptions" type="Boolean">
        /// </param>
        this._view.get_linkMainPanel().style.display = (moreOptions) ? 'none' : '';
        this._view.get_linkUrlPanel().style.display = (moreOptions) ? '' : 'none';
    },
    _flashSwfUrlButtonClicked: function SpottedScript_Controls_Html_Controller$_flashSwfUrlButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._setFlashSwfPanelVisibility(true);
    },
    _flashSwfUrlBackButtonClicked: function SpottedScript_Controls_Html_Controller$_flashSwfUrlBackButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._setFlashSwfPanelVisibility(false);
    },
    _setFlashSwfPanelVisibility: function SpottedScript_Controls_Html_Controller$_setFlashSwfPanelVisibility(moreOptions) {
        /// <param name="moreOptions" type="Boolean">
        /// </param>
        this._view.get_flashMainPanel().style.display = (moreOptions) ? 'none' : '';
        this._view.get_flashSwfUrlPanel().style.display = (moreOptions) ? '' : 'none';
    },
    _videoFlvUrlButtonClicked: function SpottedScript_Controls_Html_Controller$_videoFlvUrlButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._setVideoFlvPanelVisibility(true);
    },
    _videoFlvUrlBackButtonClicked: function SpottedScript_Controls_Html_Controller$_videoFlvUrlBackButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._setVideoFlvPanelVisibility(false);
    },
    _setVideoFlvPanelVisibility: function SpottedScript_Controls_Html_Controller$_setVideoFlvPanelVisibility(moreOptions) {
        /// <param name="moreOptions" type="Boolean">
        /// </param>
        this._view.get_videoMainPanel().style.display = (moreOptions) ? 'none' : '';
        this._view.get_videoFlvPanel().style.display = (moreOptions) ? '' : 'none';
    },
    _advancedTagsToggleButtonClicked: function SpottedScript_Controls_Html_Controller$_advancedTagsToggleButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._view.get_advancedTagsPanel().style.display = (this._view.get_advancedTagsPanel().style.display === 'none') ? '' : 'none';
    },
    _advancedParseNowButtonClicked: function SpottedScript_Controls_Html_Controller$_advancedParseNowButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        SpottedScript.Misc.showWaitingCursor();
        Spotted.WebServices.Controls.CommentsDisplay.Service.cleanHtml(this._view.get_htmlTextBox().value, Function.createDelegate(this, this._cleanHtmlSuccess), null, null, -1);
    },
    _cleanHtmlSuccess: function SpottedScript_Controls_Html_Controller$_cleanHtmlSuccess(cleanHtml, userContext, methodName) {
        /// <param name="cleanHtml" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        SpottedScript.Misc.hideWaitingCursor();
        this._view.get_htmlTextBox().value = cleanHtml;
    },
    _previewButtonClicked: function SpottedScript_Controls_Html_Controller$_previewButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(Function.createDelegate(this, function() {
            Spotted.WebServices.Controls.CommentsDisplay.Service.getPreviewHtml((this._view.get_uiPreviewType().value !== '') ? Number.parseInvariant(this._view.get_uiPreviewType().value) : 0, this._view.get_htmlTextBox().value, this.get__formatting(), Function.createDelegate(this, this._getPreviewHtmlSuccess), null, null, -1);
        }));
    },
    _getPreviewHtmlSuccess: function SpottedScript_Controls_Html_Controller$_getPreviewHtmlSuccess(htmlAndScript, userContext, methodName) {
        /// <param name="htmlAndScript" type="Array" elementType="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._view.get_previewPanel().innerHTML = htmlAndScript[0];
        eval(htmlAndScript[1]);
        this._view.get_hidePreviewButton().style.display = '';
        this._view.get_previewButton().innerHTML = 'Update preview';
        this._view.get_previewPanelContainer().style.display = '';
    },
    _hidePreviewButtonClicked: function SpottedScript_Controls_Html_Controller$_hidePreviewButtonClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._view.get_previewButton().innerHTML = 'Preview';
        this._view.get_hidePreviewButton().style.display = 'none';
        this._view.get_previewPanelContainer().style.display = 'none';
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Html.View
SpottedScript.Controls.Html.View = function SpottedScript_Controls_Html_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.Html.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.Html.View.prototype = {
    clientId: null,
    get_helpersDiv: function SpottedScript_Controls_Html_View$get_helpersDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HelpersDiv');
    },
    get_linkAnchor: function SpottedScript_Controls_Html_View$get_linkAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkAnchor');
    },
    get_imageAnchor: function SpottedScript_Controls_Html_View$get_imageAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImageAnchor');
    },
    get_videoAnchor: function SpottedScript_Controls_Html_View$get_videoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoAnchor');
    },
    get_mixmagAnchor: function SpottedScript_Controls_Html_View$get_mixmagAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MixmagAnchor');
    },
    get_flashAnchor: function SpottedScript_Controls_Html_View$get_flashAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashAnchor');
    },
    get_advancedAnchor: function SpottedScript_Controls_Html_View$get_advancedAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedAnchor');
    },
    get_mixmagDiv: function SpottedScript_Controls_Html_View$get_mixmagDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MixmagDiv');
    },
    get_linkDiv: function SpottedScript_Controls_Html_View$get_linkDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkDiv');
    },
    get_linkMainPanel: function SpottedScript_Controls_Html_View$get_linkMainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkMainPanel');
    },
    get_linkUrlButton: function SpottedScript_Controls_Html_View$get_linkUrlButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkUrlButton');
    },
    get_linkUrlPanel: function SpottedScript_Controls_Html_View$get_linkUrlPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkUrlPanel');
    },
    get_linkUrlPanelBackButton: function SpottedScript_Controls_Html_View$get_linkUrlPanelBackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkUrlPanelBackButton');
    },
    get_linkUrlTextBox: function SpottedScript_Controls_Html_View$get_linkUrlTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkUrlTextBox');
    },
    get_imageDiv: function SpottedScript_Controls_Html_View$get_imageDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImageDiv');
    },
    get_imageMainPanel: function SpottedScript_Controls_Html_View$get_imageMainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImageMainPanel');
    },
    get_videoDiv: function SpottedScript_Controls_Html_View$get_videoDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoDiv');
    },
    get_videoMainPanel: function SpottedScript_Controls_Html_View$get_videoMainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoMainPanel');
    },
    get_videoFlvButton: function SpottedScript_Controls_Html_View$get_videoFlvButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoFlvButton');
    },
    get_videoFlvPanel: function SpottedScript_Controls_Html_View$get_videoFlvPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoFlvPanel');
    },
    get_videoFlvPanelBackButton: function SpottedScript_Controls_Html_View$get_videoFlvPanelBackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoFlvPanelBackButton');
    },
    get_videoFlvUrlTextBox: function SpottedScript_Controls_Html_View$get_videoFlvUrlTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoFlvUrlTextBox');
    },
    get_videoFlvWidthTextBox: function SpottedScript_Controls_Html_View$get_videoFlvWidthTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoFlvWidthTextBox');
    },
    get_videoFlvHeightTextBox: function SpottedScript_Controls_Html_View$get_videoFlvHeightTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VideoFlvHeightTextBox');
    },
    get_flashDiv: function SpottedScript_Controls_Html_View$get_flashDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashDiv');
    },
    get_flashMainPanel: function SpottedScript_Controls_Html_View$get_flashMainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashMainPanel');
    },
    get_flashSwfUrlButton: function SpottedScript_Controls_Html_View$get_flashSwfUrlButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashSwfUrlButton');
    },
    get_flashSwfUrlPanel: function SpottedScript_Controls_Html_View$get_flashSwfUrlPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashSwfUrlPanel');
    },
    get_flashSwfUrlPanelBackButton: function SpottedScript_Controls_Html_View$get_flashSwfUrlPanelBackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashSwfUrlPanelBackButton');
    },
    get_flashSwfUrlUrlTextBox: function SpottedScript_Controls_Html_View$get_flashSwfUrlUrlTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashSwfUrlUrlTextBox');
    },
    get_flashSwfUrlWidthTextBox: function SpottedScript_Controls_Html_View$get_flashSwfUrlWidthTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashSwfUrlWidthTextBox');
    },
    get_flashSwfUrlHeightTextBox: function SpottedScript_Controls_Html_View$get_flashSwfUrlHeightTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashSwfUrlHeightTextBox');
    },
    get_flashSwfUrlDrawDropDownList: function SpottedScript_Controls_Html_View$get_flashSwfUrlDrawDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FlashSwfUrlDrawDropDownList');
    },
    get_advancedDiv: function SpottedScript_Controls_Html_View$get_advancedDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedDiv');
    },
    get_advancedFormattingPanel: function SpottedScript_Controls_Html_View$get_advancedFormattingPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedFormattingPanel');
    },
    get_advancedFormattingTrueRadio: function SpottedScript_Controls_Html_View$get_advancedFormattingTrueRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedFormattingTrueRadio');
    },
    get_advancedFormattingFalseRadio: function SpottedScript_Controls_Html_View$get_advancedFormattingFalseRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedFormattingFalseRadio');
    },
    get_advancedContainerPanel: function SpottedScript_Controls_Html_View$get_advancedContainerPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedContainerPanel');
    },
    get_advancedContainerTrueRadio: function SpottedScript_Controls_Html_View$get_advancedContainerTrueRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedContainerTrueRadio');
    },
    get_advancedContainerFalseRadio: function SpottedScript_Controls_Html_View$get_advancedContainerFalseRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedContainerFalseRadio');
    },
    get_advancedParseNowPanel: function SpottedScript_Controls_Html_View$get_advancedParseNowPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedParseNowPanel');
    },
    get_advancedParseNowButton: function SpottedScript_Controls_Html_View$get_advancedParseNowButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedParseNowButton');
    },
    get_advancedTagsToggleButton: function SpottedScript_Controls_Html_View$get_advancedTagsToggleButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedTagsToggleButton');
    },
    get_advancedTagsPanel: function SpottedScript_Controls_Html_View$get_advancedTagsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdvancedTagsPanel');
    },
    get_textBoxDiv: function SpottedScript_Controls_Html_View$get_textBoxDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TextBoxDiv');
    },
    get_htmlTextBox: function SpottedScript_Controls_Html_View$get_htmlTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HtmlTextBox');
    },
    get_disabledMessageDiv: function SpottedScript_Controls_Html_View$get_disabledMessageDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DisabledMessageDiv');
    },
    get_buttonsContainer: function SpottedScript_Controls_Html_View$get_buttonsContainer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ButtonsContainer');
    },
    get_saveDiv: function SpottedScript_Controls_Html_View$get_saveDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveDiv');
    },
    get_saveButton: function SpottedScript_Controls_Html_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveButton');
    },
    get_previewButton: function SpottedScript_Controls_Html_View$get_previewButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PreviewButton');
    },
    get_hidePreviewButton: function SpottedScript_Controls_Html_View$get_hidePreviewButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HidePreviewButton');
    },
    get_previewPanelContainer: function SpottedScript_Controls_Html_View$get_previewPanelContainer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PreviewPanelContainer');
    },
    get_previewPanel: function SpottedScript_Controls_Html_View$get_previewPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PreviewPanel');
    },
    get_uiEnabled: function SpottedScript_Controls_Html_View$get_uiEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEnabled');
    },
    get_uiPreviewType: function SpottedScript_Controls_Html_View$get_uiPreviewType() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPreviewType');
    },
    get_helperPanelDisplayState: function SpottedScript_Controls_Html_View$get_helperPanelDisplayState() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HelperPanelDisplayState');
    },
    get_genericContainerPage: function SpottedScript_Controls_Html_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Controls.Html.Controller.registerClass('SpottedScript.Controls.Html.Controller');
SpottedScript.Controls.Html.View.registerClass('SpottedScript.Controls.Html.View', SpottedScript.DsiUserControl.View);
