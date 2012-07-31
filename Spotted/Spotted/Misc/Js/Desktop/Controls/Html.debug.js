//! Html.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.Html');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Html.Controller

Js.Controls.Html.Controller = function Js_Controls_Html_Controller(view) {
    /// <param name="view" type="Js.Controls.Html.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.Html.View">
    /// </field>
    this._view = view;
    if (view.get_linkUrlButton() != null) {
        view.get_linkUrlButtonJ().click(ss.Delegate.create(this, this._linkUrlButtonClicked));
        view.get_linkUrlPanelBackButtonJ().click(ss.Delegate.create(this, this._linkUrlBackButtonClicked));
        view.get_flashSwfUrlButtonJ().click(ss.Delegate.create(this, this._flashSwfUrlButtonClicked));
        view.get_flashSwfUrlPanelBackButtonJ().click(ss.Delegate.create(this, this._flashSwfUrlBackButtonClicked));
        view.get_videoFlvButtonJ().click(ss.Delegate.create(this, this._videoFlvUrlButtonClicked));
        view.get_videoFlvPanelBackButtonJ().click(ss.Delegate.create(this, this._videoFlvUrlBackButtonClicked));
        view.get_advancedTagsToggleButtonJ().click(ss.Delegate.create(this, this._advancedTagsToggleButtonClicked));
        view.get_advancedParseNowButtonJ().click(ss.Delegate.create(this, this._advancedParseNowButtonClicked));
        view.get_previewButtonJ().click(ss.Delegate.create(this, this._previewButtonClicked));
        view.get_hidePreviewButtonJ().click(ss.Delegate.create(this, this._hidePreviewButtonClicked));
    }
}
Js.Controls.Html.Controller.prototype = {
    _view: null,
    
    get_saveButton: function Js_Controls_Html_Controller$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return this._view.get_saveButton();
    },
    
    get_saveButtonJ: function Js_Controls_Html_Controller$get_saveButtonJ() {
        /// <value type="jQueryObject"></value>
        return this._view.get_saveButtonJ();
    },
    
    get_rawHtml: function Js_Controls_Html_Controller$get_rawHtml() {
        /// <value type="String"></value>
        return this._view.get_htmlTextBox().value;
    },
    
    get_formatting: function Js_Controls_Html_Controller$get_formatting() {
        /// <value type="Boolean"></value>
        return this._view.get_advancedFormattingTrueRadio().checked;
    },
    
    clearHtml: function Js_Controls_Html_Controller$clearHtml() {
        this._view.get_htmlTextBox().value = '';
    },
    
    _linkUrlButtonClicked: function Js_Controls_Html_Controller$_linkUrlButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._setLinkPanelVisibility(true);
    },
    
    _linkUrlBackButtonClicked: function Js_Controls_Html_Controller$_linkUrlBackButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._setLinkPanelVisibility(false);
    },
    
    _setLinkPanelVisibility: function Js_Controls_Html_Controller$_setLinkPanelVisibility(moreOptions) {
        /// <param name="moreOptions" type="Boolean">
        /// </param>
        this._view.get_linkMainPanel().style.display = (moreOptions) ? 'none' : '';
        this._view.get_linkUrlPanel().style.display = (moreOptions) ? '' : 'none';
    },
    
    _flashSwfUrlButtonClicked: function Js_Controls_Html_Controller$_flashSwfUrlButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._setFlashSwfPanelVisibility(true);
    },
    
    _flashSwfUrlBackButtonClicked: function Js_Controls_Html_Controller$_flashSwfUrlBackButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._setFlashSwfPanelVisibility(false);
    },
    
    _setFlashSwfPanelVisibility: function Js_Controls_Html_Controller$_setFlashSwfPanelVisibility(moreOptions) {
        /// <param name="moreOptions" type="Boolean">
        /// </param>
        this._view.get_flashMainPanel().style.display = (moreOptions) ? 'none' : '';
        this._view.get_flashSwfUrlPanel().style.display = (moreOptions) ? '' : 'none';
    },
    
    _videoFlvUrlButtonClicked: function Js_Controls_Html_Controller$_videoFlvUrlButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._setVideoFlvPanelVisibility(true);
    },
    
    _videoFlvUrlBackButtonClicked: function Js_Controls_Html_Controller$_videoFlvUrlBackButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._setVideoFlvPanelVisibility(false);
    },
    
    _setVideoFlvPanelVisibility: function Js_Controls_Html_Controller$_setVideoFlvPanelVisibility(moreOptions) {
        /// <param name="moreOptions" type="Boolean">
        /// </param>
        this._view.get_videoMainPanel().style.display = (moreOptions) ? 'none' : '';
        this._view.get_videoFlvPanel().style.display = (moreOptions) ? '' : 'none';
    },
    
    _advancedTagsToggleButtonClicked: function Js_Controls_Html_Controller$_advancedTagsToggleButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._view.get_advancedTagsPanel().style.display = (this._view.get_advancedTagsPanel().style.display === 'none') ? '' : 'none';
    },
    
    _advancedParseNowButtonClicked: function Js_Controls_Html_Controller$_advancedParseNowButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        Js.Library.Misc.showWaitingCursor();
        Js.Controls.CommentsDisplay.Service.cleanHtml(this._view.get_htmlTextBox().value, ss.Delegate.create(this, this._cleanHtmlSuccess), null, null, -1);
    },
    
    _cleanHtmlSuccess: function Js_Controls_Html_Controller$_cleanHtmlSuccess(cleanHtml, userContext, methodName) {
        /// <param name="cleanHtml" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Js.Library.Misc.hideWaitingCursor();
        this._view.get_htmlTextBox().value = cleanHtml;
    },
    
    _previewButtonClicked: function Js_Controls_Html_Controller$_previewButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(ss.Delegate.create(this, function() {
            Js.Controls.CommentsDisplay.Service.getPreviewHtml((!!this._view.get_uiPreviewType().value) ? parseInt(this._view.get_uiPreviewType().value) : 0, this._view.get_htmlTextBox().value, this.get_formatting(), ss.Delegate.create(this, this._getPreviewHtmlSuccess), null, null, -1);
        }));
    },
    
    _getPreviewHtmlSuccess: function Js_Controls_Html_Controller$_getPreviewHtmlSuccess(htmlAndScript, userContext, methodName) {
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
    
    _hidePreviewButtonClicked: function Js_Controls_Html_Controller$_hidePreviewButtonClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._view.get_previewButton().innerHTML = 'Preview';
        this._view.get_hidePreviewButton().style.display = 'none';
        this._view.get_previewPanelContainer().style.display = 'none';
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Html.View

Js.Controls.Html.View = function Js_Controls_Html_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_HelpersDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_HelpersDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LinkAnchor$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LinkAnchorJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_ImageAnchor$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_ImageAnchorJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoAnchor$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoAnchorJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_MixmagAnchor$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_MixmagAnchorJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashAnchor$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashAnchorJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedAnchor$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedAnchorJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_MixmagDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_MixmagDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LinkDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LinkDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LinkMainPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LinkMainPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LinkUrlButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LinkUrlButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LinkUrlPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LinkUrlPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LinkUrlPanelBackButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LinkUrlPanelBackButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LinkUrlTextBox$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LinkUrlTextBoxJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_ImageDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_ImageDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_ImageMainPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_ImageMainPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoMainPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoMainPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoFlvButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoFlvButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoFlvPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoFlvPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoFlvPanelBackButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoFlvPanelBackButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoFlvUrlTextBox$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoFlvUrlTextBoxJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoFlvWidthTextBox$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoFlvWidthTextBoxJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_VideoFlvHeightTextBox$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_VideoFlvHeightTextBoxJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashMainPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashMainPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashSwfUrlButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashSwfUrlButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashSwfUrlPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashSwfUrlPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashSwfUrlPanelBackButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashSwfUrlPanelBackButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashSwfUrlUrlTextBox$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashSwfUrlUrlTextBoxJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashSwfUrlWidthTextBox$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashSwfUrlWidthTextBoxJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashSwfUrlHeightTextBox$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashSwfUrlHeightTextBoxJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FlashSwfUrlDrawDropDownList$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FlashSwfUrlDrawDropDownListJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedFormattingPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedFormattingPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedFormattingTrueRadio$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedFormattingTrueRadioJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedFormattingFalseRadio$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedFormattingFalseRadioJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedContainerPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedContainerPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedContainerTrueRadio$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedContainerTrueRadioJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedContainerFalseRadio$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedContainerFalseRadioJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedParseNowPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedParseNowPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedParseNowButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedParseNowButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedTagsToggleButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedTagsToggleButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_AdvancedTagsPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_AdvancedTagsPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_TextBoxDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_TextBoxDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_HtmlTextBox$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_HtmlTextBoxJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_DisabledMessageDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_DisabledMessageDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_ButtonsContainer$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_ButtonsContainerJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_SaveDiv$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_SaveDivJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_SaveButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_SaveButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_PreviewButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_PreviewButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_HidePreviewButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_HidePreviewButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_PreviewPanelContainer$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_PreviewPanelContainerJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_PreviewPanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_PreviewPanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiEnabled$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiEnabledJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiPreviewType$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPreviewTypeJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_HelperPanelDisplayState$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_HelperPanelDisplayStateJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Controls.Html.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Controls.Html.View.prototype = {
    clientId: null,
    
    get_helpersDiv: function Js_Controls_Html_View$get_helpersDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._HelpersDiv$2 == null) {
            this._HelpersDiv$2 = document.getElementById(this.clientId + '_HelpersDiv');
        }
        return this._HelpersDiv$2;
    },
    
    _HelpersDiv$2: null,
    
    get_helpersDivJ: function Js_Controls_Html_View$get_helpersDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._HelpersDivJ$2 == null) {
            this._HelpersDivJ$2 = $('#' + this.clientId + '_HelpersDiv');
        }
        return this._HelpersDivJ$2;
    },
    
    _HelpersDivJ$2: null,
    
    get_linkAnchor: function Js_Controls_Html_View$get_linkAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._LinkAnchor$2 == null) {
            this._LinkAnchor$2 = document.getElementById(this.clientId + '_LinkAnchor');
        }
        return this._LinkAnchor$2;
    },
    
    _LinkAnchor$2: null,
    
    get_linkAnchorJ: function Js_Controls_Html_View$get_linkAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._LinkAnchorJ$2 == null) {
            this._LinkAnchorJ$2 = $('#' + this.clientId + '_LinkAnchor');
        }
        return this._LinkAnchorJ$2;
    },
    
    _LinkAnchorJ$2: null,
    
    get_imageAnchor: function Js_Controls_Html_View$get_imageAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._ImageAnchor$2 == null) {
            this._ImageAnchor$2 = document.getElementById(this.clientId + '_ImageAnchor');
        }
        return this._ImageAnchor$2;
    },
    
    _ImageAnchor$2: null,
    
    get_imageAnchorJ: function Js_Controls_Html_View$get_imageAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._ImageAnchorJ$2 == null) {
            this._ImageAnchorJ$2 = $('#' + this.clientId + '_ImageAnchor');
        }
        return this._ImageAnchorJ$2;
    },
    
    _ImageAnchorJ$2: null,
    
    get_videoAnchor: function Js_Controls_Html_View$get_videoAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoAnchor$2 == null) {
            this._VideoAnchor$2 = document.getElementById(this.clientId + '_VideoAnchor');
        }
        return this._VideoAnchor$2;
    },
    
    _VideoAnchor$2: null,
    
    get_videoAnchorJ: function Js_Controls_Html_View$get_videoAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoAnchorJ$2 == null) {
            this._VideoAnchorJ$2 = $('#' + this.clientId + '_VideoAnchor');
        }
        return this._VideoAnchorJ$2;
    },
    
    _VideoAnchorJ$2: null,
    
    get_mixmagAnchor: function Js_Controls_Html_View$get_mixmagAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._MixmagAnchor$2 == null) {
            this._MixmagAnchor$2 = document.getElementById(this.clientId + '_MixmagAnchor');
        }
        return this._MixmagAnchor$2;
    },
    
    _MixmagAnchor$2: null,
    
    get_mixmagAnchorJ: function Js_Controls_Html_View$get_mixmagAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._MixmagAnchorJ$2 == null) {
            this._MixmagAnchorJ$2 = $('#' + this.clientId + '_MixmagAnchor');
        }
        return this._MixmagAnchorJ$2;
    },
    
    _MixmagAnchorJ$2: null,
    
    get_flashAnchor: function Js_Controls_Html_View$get_flashAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashAnchor$2 == null) {
            this._FlashAnchor$2 = document.getElementById(this.clientId + '_FlashAnchor');
        }
        return this._FlashAnchor$2;
    },
    
    _FlashAnchor$2: null,
    
    get_flashAnchorJ: function Js_Controls_Html_View$get_flashAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashAnchorJ$2 == null) {
            this._FlashAnchorJ$2 = $('#' + this.clientId + '_FlashAnchor');
        }
        return this._FlashAnchorJ$2;
    },
    
    _FlashAnchorJ$2: null,
    
    get_advancedAnchor: function Js_Controls_Html_View$get_advancedAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedAnchor$2 == null) {
            this._AdvancedAnchor$2 = document.getElementById(this.clientId + '_AdvancedAnchor');
        }
        return this._AdvancedAnchor$2;
    },
    
    _AdvancedAnchor$2: null,
    
    get_advancedAnchorJ: function Js_Controls_Html_View$get_advancedAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedAnchorJ$2 == null) {
            this._AdvancedAnchorJ$2 = $('#' + this.clientId + '_AdvancedAnchor');
        }
        return this._AdvancedAnchorJ$2;
    },
    
    _AdvancedAnchorJ$2: null,
    
    get_mixmagDiv: function Js_Controls_Html_View$get_mixmagDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._MixmagDiv$2 == null) {
            this._MixmagDiv$2 = document.getElementById(this.clientId + '_MixmagDiv');
        }
        return this._MixmagDiv$2;
    },
    
    _MixmagDiv$2: null,
    
    get_mixmagDivJ: function Js_Controls_Html_View$get_mixmagDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._MixmagDivJ$2 == null) {
            this._MixmagDivJ$2 = $('#' + this.clientId + '_MixmagDiv');
        }
        return this._MixmagDivJ$2;
    },
    
    _MixmagDivJ$2: null,
    
    get_linkDiv: function Js_Controls_Html_View$get_linkDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._LinkDiv$2 == null) {
            this._LinkDiv$2 = document.getElementById(this.clientId + '_LinkDiv');
        }
        return this._LinkDiv$2;
    },
    
    _LinkDiv$2: null,
    
    get_linkDivJ: function Js_Controls_Html_View$get_linkDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._LinkDivJ$2 == null) {
            this._LinkDivJ$2 = $('#' + this.clientId + '_LinkDiv');
        }
        return this._LinkDivJ$2;
    },
    
    _LinkDivJ$2: null,
    
    get_linkMainPanel: function Js_Controls_Html_View$get_linkMainPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._LinkMainPanel$2 == null) {
            this._LinkMainPanel$2 = document.getElementById(this.clientId + '_LinkMainPanel');
        }
        return this._LinkMainPanel$2;
    },
    
    _LinkMainPanel$2: null,
    
    get_linkMainPanelJ: function Js_Controls_Html_View$get_linkMainPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._LinkMainPanelJ$2 == null) {
            this._LinkMainPanelJ$2 = $('#' + this.clientId + '_LinkMainPanel');
        }
        return this._LinkMainPanelJ$2;
    },
    
    _LinkMainPanelJ$2: null,
    
    get_linkUrlButton: function Js_Controls_Html_View$get_linkUrlButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._LinkUrlButton$2 == null) {
            this._LinkUrlButton$2 = document.getElementById(this.clientId + '_LinkUrlButton');
        }
        return this._LinkUrlButton$2;
    },
    
    _LinkUrlButton$2: null,
    
    get_linkUrlButtonJ: function Js_Controls_Html_View$get_linkUrlButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._LinkUrlButtonJ$2 == null) {
            this._LinkUrlButtonJ$2 = $('#' + this.clientId + '_LinkUrlButton');
        }
        return this._LinkUrlButtonJ$2;
    },
    
    _LinkUrlButtonJ$2: null,
    
    get_linkUrlPanel: function Js_Controls_Html_View$get_linkUrlPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._LinkUrlPanel$2 == null) {
            this._LinkUrlPanel$2 = document.getElementById(this.clientId + '_LinkUrlPanel');
        }
        return this._LinkUrlPanel$2;
    },
    
    _LinkUrlPanel$2: null,
    
    get_linkUrlPanelJ: function Js_Controls_Html_View$get_linkUrlPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._LinkUrlPanelJ$2 == null) {
            this._LinkUrlPanelJ$2 = $('#' + this.clientId + '_LinkUrlPanel');
        }
        return this._LinkUrlPanelJ$2;
    },
    
    _LinkUrlPanelJ$2: null,
    
    get_linkUrlPanelBackButton: function Js_Controls_Html_View$get_linkUrlPanelBackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._LinkUrlPanelBackButton$2 == null) {
            this._LinkUrlPanelBackButton$2 = document.getElementById(this.clientId + '_LinkUrlPanelBackButton');
        }
        return this._LinkUrlPanelBackButton$2;
    },
    
    _LinkUrlPanelBackButton$2: null,
    
    get_linkUrlPanelBackButtonJ: function Js_Controls_Html_View$get_linkUrlPanelBackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._LinkUrlPanelBackButtonJ$2 == null) {
            this._LinkUrlPanelBackButtonJ$2 = $('#' + this.clientId + '_LinkUrlPanelBackButton');
        }
        return this._LinkUrlPanelBackButtonJ$2;
    },
    
    _LinkUrlPanelBackButtonJ$2: null,
    
    get_linkUrlTextBox: function Js_Controls_Html_View$get_linkUrlTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._LinkUrlTextBox$2 == null) {
            this._LinkUrlTextBox$2 = document.getElementById(this.clientId + '_LinkUrlTextBox');
        }
        return this._LinkUrlTextBox$2;
    },
    
    _LinkUrlTextBox$2: null,
    
    get_linkUrlTextBoxJ: function Js_Controls_Html_View$get_linkUrlTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._LinkUrlTextBoxJ$2 == null) {
            this._LinkUrlTextBoxJ$2 = $('#' + this.clientId + '_LinkUrlTextBox');
        }
        return this._LinkUrlTextBoxJ$2;
    },
    
    _LinkUrlTextBoxJ$2: null,
    
    get_imageDiv: function Js_Controls_Html_View$get_imageDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._ImageDiv$2 == null) {
            this._ImageDiv$2 = document.getElementById(this.clientId + '_ImageDiv');
        }
        return this._ImageDiv$2;
    },
    
    _ImageDiv$2: null,
    
    get_imageDivJ: function Js_Controls_Html_View$get_imageDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._ImageDivJ$2 == null) {
            this._ImageDivJ$2 = $('#' + this.clientId + '_ImageDiv');
        }
        return this._ImageDivJ$2;
    },
    
    _ImageDivJ$2: null,
    
    get_imageMainPanel: function Js_Controls_Html_View$get_imageMainPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._ImageMainPanel$2 == null) {
            this._ImageMainPanel$2 = document.getElementById(this.clientId + '_ImageMainPanel');
        }
        return this._ImageMainPanel$2;
    },
    
    _ImageMainPanel$2: null,
    
    get_imageMainPanelJ: function Js_Controls_Html_View$get_imageMainPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._ImageMainPanelJ$2 == null) {
            this._ImageMainPanelJ$2 = $('#' + this.clientId + '_ImageMainPanel');
        }
        return this._ImageMainPanelJ$2;
    },
    
    _ImageMainPanelJ$2: null,
    
    get_videoDiv: function Js_Controls_Html_View$get_videoDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoDiv$2 == null) {
            this._VideoDiv$2 = document.getElementById(this.clientId + '_VideoDiv');
        }
        return this._VideoDiv$2;
    },
    
    _VideoDiv$2: null,
    
    get_videoDivJ: function Js_Controls_Html_View$get_videoDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoDivJ$2 == null) {
            this._VideoDivJ$2 = $('#' + this.clientId + '_VideoDiv');
        }
        return this._VideoDivJ$2;
    },
    
    _VideoDivJ$2: null,
    
    get_videoMainPanel: function Js_Controls_Html_View$get_videoMainPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoMainPanel$2 == null) {
            this._VideoMainPanel$2 = document.getElementById(this.clientId + '_VideoMainPanel');
        }
        return this._VideoMainPanel$2;
    },
    
    _VideoMainPanel$2: null,
    
    get_videoMainPanelJ: function Js_Controls_Html_View$get_videoMainPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoMainPanelJ$2 == null) {
            this._VideoMainPanelJ$2 = $('#' + this.clientId + '_VideoMainPanel');
        }
        return this._VideoMainPanelJ$2;
    },
    
    _VideoMainPanelJ$2: null,
    
    get_videoFlvButton: function Js_Controls_Html_View$get_videoFlvButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoFlvButton$2 == null) {
            this._VideoFlvButton$2 = document.getElementById(this.clientId + '_VideoFlvButton');
        }
        return this._VideoFlvButton$2;
    },
    
    _VideoFlvButton$2: null,
    
    get_videoFlvButtonJ: function Js_Controls_Html_View$get_videoFlvButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoFlvButtonJ$2 == null) {
            this._VideoFlvButtonJ$2 = $('#' + this.clientId + '_VideoFlvButton');
        }
        return this._VideoFlvButtonJ$2;
    },
    
    _VideoFlvButtonJ$2: null,
    
    get_videoFlvPanel: function Js_Controls_Html_View$get_videoFlvPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoFlvPanel$2 == null) {
            this._VideoFlvPanel$2 = document.getElementById(this.clientId + '_VideoFlvPanel');
        }
        return this._VideoFlvPanel$2;
    },
    
    _VideoFlvPanel$2: null,
    
    get_videoFlvPanelJ: function Js_Controls_Html_View$get_videoFlvPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoFlvPanelJ$2 == null) {
            this._VideoFlvPanelJ$2 = $('#' + this.clientId + '_VideoFlvPanel');
        }
        return this._VideoFlvPanelJ$2;
    },
    
    _VideoFlvPanelJ$2: null,
    
    get_videoFlvPanelBackButton: function Js_Controls_Html_View$get_videoFlvPanelBackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoFlvPanelBackButton$2 == null) {
            this._VideoFlvPanelBackButton$2 = document.getElementById(this.clientId + '_VideoFlvPanelBackButton');
        }
        return this._VideoFlvPanelBackButton$2;
    },
    
    _VideoFlvPanelBackButton$2: null,
    
    get_videoFlvPanelBackButtonJ: function Js_Controls_Html_View$get_videoFlvPanelBackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoFlvPanelBackButtonJ$2 == null) {
            this._VideoFlvPanelBackButtonJ$2 = $('#' + this.clientId + '_VideoFlvPanelBackButton');
        }
        return this._VideoFlvPanelBackButtonJ$2;
    },
    
    _VideoFlvPanelBackButtonJ$2: null,
    
    get_videoFlvUrlTextBox: function Js_Controls_Html_View$get_videoFlvUrlTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoFlvUrlTextBox$2 == null) {
            this._VideoFlvUrlTextBox$2 = document.getElementById(this.clientId + '_VideoFlvUrlTextBox');
        }
        return this._VideoFlvUrlTextBox$2;
    },
    
    _VideoFlvUrlTextBox$2: null,
    
    get_videoFlvUrlTextBoxJ: function Js_Controls_Html_View$get_videoFlvUrlTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoFlvUrlTextBoxJ$2 == null) {
            this._VideoFlvUrlTextBoxJ$2 = $('#' + this.clientId + '_VideoFlvUrlTextBox');
        }
        return this._VideoFlvUrlTextBoxJ$2;
    },
    
    _VideoFlvUrlTextBoxJ$2: null,
    
    get_videoFlvWidthTextBox: function Js_Controls_Html_View$get_videoFlvWidthTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoFlvWidthTextBox$2 == null) {
            this._VideoFlvWidthTextBox$2 = document.getElementById(this.clientId + '_VideoFlvWidthTextBox');
        }
        return this._VideoFlvWidthTextBox$2;
    },
    
    _VideoFlvWidthTextBox$2: null,
    
    get_videoFlvWidthTextBoxJ: function Js_Controls_Html_View$get_videoFlvWidthTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoFlvWidthTextBoxJ$2 == null) {
            this._VideoFlvWidthTextBoxJ$2 = $('#' + this.clientId + '_VideoFlvWidthTextBox');
        }
        return this._VideoFlvWidthTextBoxJ$2;
    },
    
    _VideoFlvWidthTextBoxJ$2: null,
    
    get_videoFlvHeightTextBox: function Js_Controls_Html_View$get_videoFlvHeightTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._VideoFlvHeightTextBox$2 == null) {
            this._VideoFlvHeightTextBox$2 = document.getElementById(this.clientId + '_VideoFlvHeightTextBox');
        }
        return this._VideoFlvHeightTextBox$2;
    },
    
    _VideoFlvHeightTextBox$2: null,
    
    get_videoFlvHeightTextBoxJ: function Js_Controls_Html_View$get_videoFlvHeightTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._VideoFlvHeightTextBoxJ$2 == null) {
            this._VideoFlvHeightTextBoxJ$2 = $('#' + this.clientId + '_VideoFlvHeightTextBox');
        }
        return this._VideoFlvHeightTextBoxJ$2;
    },
    
    _VideoFlvHeightTextBoxJ$2: null,
    
    get_flashDiv: function Js_Controls_Html_View$get_flashDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashDiv$2 == null) {
            this._FlashDiv$2 = document.getElementById(this.clientId + '_FlashDiv');
        }
        return this._FlashDiv$2;
    },
    
    _FlashDiv$2: null,
    
    get_flashDivJ: function Js_Controls_Html_View$get_flashDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashDivJ$2 == null) {
            this._FlashDivJ$2 = $('#' + this.clientId + '_FlashDiv');
        }
        return this._FlashDivJ$2;
    },
    
    _FlashDivJ$2: null,
    
    get_flashMainPanel: function Js_Controls_Html_View$get_flashMainPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashMainPanel$2 == null) {
            this._FlashMainPanel$2 = document.getElementById(this.clientId + '_FlashMainPanel');
        }
        return this._FlashMainPanel$2;
    },
    
    _FlashMainPanel$2: null,
    
    get_flashMainPanelJ: function Js_Controls_Html_View$get_flashMainPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashMainPanelJ$2 == null) {
            this._FlashMainPanelJ$2 = $('#' + this.clientId + '_FlashMainPanel');
        }
        return this._FlashMainPanelJ$2;
    },
    
    _FlashMainPanelJ$2: null,
    
    get_flashSwfUrlButton: function Js_Controls_Html_View$get_flashSwfUrlButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashSwfUrlButton$2 == null) {
            this._FlashSwfUrlButton$2 = document.getElementById(this.clientId + '_FlashSwfUrlButton');
        }
        return this._FlashSwfUrlButton$2;
    },
    
    _FlashSwfUrlButton$2: null,
    
    get_flashSwfUrlButtonJ: function Js_Controls_Html_View$get_flashSwfUrlButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashSwfUrlButtonJ$2 == null) {
            this._FlashSwfUrlButtonJ$2 = $('#' + this.clientId + '_FlashSwfUrlButton');
        }
        return this._FlashSwfUrlButtonJ$2;
    },
    
    _FlashSwfUrlButtonJ$2: null,
    
    get_flashSwfUrlPanel: function Js_Controls_Html_View$get_flashSwfUrlPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashSwfUrlPanel$2 == null) {
            this._FlashSwfUrlPanel$2 = document.getElementById(this.clientId + '_FlashSwfUrlPanel');
        }
        return this._FlashSwfUrlPanel$2;
    },
    
    _FlashSwfUrlPanel$2: null,
    
    get_flashSwfUrlPanelJ: function Js_Controls_Html_View$get_flashSwfUrlPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashSwfUrlPanelJ$2 == null) {
            this._FlashSwfUrlPanelJ$2 = $('#' + this.clientId + '_FlashSwfUrlPanel');
        }
        return this._FlashSwfUrlPanelJ$2;
    },
    
    _FlashSwfUrlPanelJ$2: null,
    
    get_flashSwfUrlPanelBackButton: function Js_Controls_Html_View$get_flashSwfUrlPanelBackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashSwfUrlPanelBackButton$2 == null) {
            this._FlashSwfUrlPanelBackButton$2 = document.getElementById(this.clientId + '_FlashSwfUrlPanelBackButton');
        }
        return this._FlashSwfUrlPanelBackButton$2;
    },
    
    _FlashSwfUrlPanelBackButton$2: null,
    
    get_flashSwfUrlPanelBackButtonJ: function Js_Controls_Html_View$get_flashSwfUrlPanelBackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashSwfUrlPanelBackButtonJ$2 == null) {
            this._FlashSwfUrlPanelBackButtonJ$2 = $('#' + this.clientId + '_FlashSwfUrlPanelBackButton');
        }
        return this._FlashSwfUrlPanelBackButtonJ$2;
    },
    
    _FlashSwfUrlPanelBackButtonJ$2: null,
    
    get_flashSwfUrlUrlTextBox: function Js_Controls_Html_View$get_flashSwfUrlUrlTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashSwfUrlUrlTextBox$2 == null) {
            this._FlashSwfUrlUrlTextBox$2 = document.getElementById(this.clientId + '_FlashSwfUrlUrlTextBox');
        }
        return this._FlashSwfUrlUrlTextBox$2;
    },
    
    _FlashSwfUrlUrlTextBox$2: null,
    
    get_flashSwfUrlUrlTextBoxJ: function Js_Controls_Html_View$get_flashSwfUrlUrlTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashSwfUrlUrlTextBoxJ$2 == null) {
            this._FlashSwfUrlUrlTextBoxJ$2 = $('#' + this.clientId + '_FlashSwfUrlUrlTextBox');
        }
        return this._FlashSwfUrlUrlTextBoxJ$2;
    },
    
    _FlashSwfUrlUrlTextBoxJ$2: null,
    
    get_flashSwfUrlWidthTextBox: function Js_Controls_Html_View$get_flashSwfUrlWidthTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashSwfUrlWidthTextBox$2 == null) {
            this._FlashSwfUrlWidthTextBox$2 = document.getElementById(this.clientId + '_FlashSwfUrlWidthTextBox');
        }
        return this._FlashSwfUrlWidthTextBox$2;
    },
    
    _FlashSwfUrlWidthTextBox$2: null,
    
    get_flashSwfUrlWidthTextBoxJ: function Js_Controls_Html_View$get_flashSwfUrlWidthTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashSwfUrlWidthTextBoxJ$2 == null) {
            this._FlashSwfUrlWidthTextBoxJ$2 = $('#' + this.clientId + '_FlashSwfUrlWidthTextBox');
        }
        return this._FlashSwfUrlWidthTextBoxJ$2;
    },
    
    _FlashSwfUrlWidthTextBoxJ$2: null,
    
    get_flashSwfUrlHeightTextBox: function Js_Controls_Html_View$get_flashSwfUrlHeightTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashSwfUrlHeightTextBox$2 == null) {
            this._FlashSwfUrlHeightTextBox$2 = document.getElementById(this.clientId + '_FlashSwfUrlHeightTextBox');
        }
        return this._FlashSwfUrlHeightTextBox$2;
    },
    
    _FlashSwfUrlHeightTextBox$2: null,
    
    get_flashSwfUrlHeightTextBoxJ: function Js_Controls_Html_View$get_flashSwfUrlHeightTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashSwfUrlHeightTextBoxJ$2 == null) {
            this._FlashSwfUrlHeightTextBoxJ$2 = $('#' + this.clientId + '_FlashSwfUrlHeightTextBox');
        }
        return this._FlashSwfUrlHeightTextBoxJ$2;
    },
    
    _FlashSwfUrlHeightTextBoxJ$2: null,
    
    get_flashSwfUrlDrawDropDownList: function Js_Controls_Html_View$get_flashSwfUrlDrawDropDownList() {
        /// <value type="Object" domElement="true"></value>
        if (this._FlashSwfUrlDrawDropDownList$2 == null) {
            this._FlashSwfUrlDrawDropDownList$2 = document.getElementById(this.clientId + '_FlashSwfUrlDrawDropDownList');
        }
        return this._FlashSwfUrlDrawDropDownList$2;
    },
    
    _FlashSwfUrlDrawDropDownList$2: null,
    
    get_flashSwfUrlDrawDropDownListJ: function Js_Controls_Html_View$get_flashSwfUrlDrawDropDownListJ() {
        /// <value type="jQueryObject"></value>
        if (this._FlashSwfUrlDrawDropDownListJ$2 == null) {
            this._FlashSwfUrlDrawDropDownListJ$2 = $('#' + this.clientId + '_FlashSwfUrlDrawDropDownList');
        }
        return this._FlashSwfUrlDrawDropDownListJ$2;
    },
    
    _FlashSwfUrlDrawDropDownListJ$2: null,
    
    get_advancedDiv: function Js_Controls_Html_View$get_advancedDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedDiv$2 == null) {
            this._AdvancedDiv$2 = document.getElementById(this.clientId + '_AdvancedDiv');
        }
        return this._AdvancedDiv$2;
    },
    
    _AdvancedDiv$2: null,
    
    get_advancedDivJ: function Js_Controls_Html_View$get_advancedDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedDivJ$2 == null) {
            this._AdvancedDivJ$2 = $('#' + this.clientId + '_AdvancedDiv');
        }
        return this._AdvancedDivJ$2;
    },
    
    _AdvancedDivJ$2: null,
    
    get_advancedFormattingPanel: function Js_Controls_Html_View$get_advancedFormattingPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedFormattingPanel$2 == null) {
            this._AdvancedFormattingPanel$2 = document.getElementById(this.clientId + '_AdvancedFormattingPanel');
        }
        return this._AdvancedFormattingPanel$2;
    },
    
    _AdvancedFormattingPanel$2: null,
    
    get_advancedFormattingPanelJ: function Js_Controls_Html_View$get_advancedFormattingPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedFormattingPanelJ$2 == null) {
            this._AdvancedFormattingPanelJ$2 = $('#' + this.clientId + '_AdvancedFormattingPanel');
        }
        return this._AdvancedFormattingPanelJ$2;
    },
    
    _AdvancedFormattingPanelJ$2: null,
    
    get_advancedFormattingTrueRadio: function Js_Controls_Html_View$get_advancedFormattingTrueRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedFormattingTrueRadio$2 == null) {
            this._AdvancedFormattingTrueRadio$2 = document.getElementById(this.clientId + '_AdvancedFormattingTrueRadio');
        }
        return this._AdvancedFormattingTrueRadio$2;
    },
    
    _AdvancedFormattingTrueRadio$2: null,
    
    get_advancedFormattingTrueRadioJ: function Js_Controls_Html_View$get_advancedFormattingTrueRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedFormattingTrueRadioJ$2 == null) {
            this._AdvancedFormattingTrueRadioJ$2 = $('#' + this.clientId + '_AdvancedFormattingTrueRadio');
        }
        return this._AdvancedFormattingTrueRadioJ$2;
    },
    
    _AdvancedFormattingTrueRadioJ$2: null,
    
    get_advancedFormattingFalseRadio: function Js_Controls_Html_View$get_advancedFormattingFalseRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedFormattingFalseRadio$2 == null) {
            this._AdvancedFormattingFalseRadio$2 = document.getElementById(this.clientId + '_AdvancedFormattingFalseRadio');
        }
        return this._AdvancedFormattingFalseRadio$2;
    },
    
    _AdvancedFormattingFalseRadio$2: null,
    
    get_advancedFormattingFalseRadioJ: function Js_Controls_Html_View$get_advancedFormattingFalseRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedFormattingFalseRadioJ$2 == null) {
            this._AdvancedFormattingFalseRadioJ$2 = $('#' + this.clientId + '_AdvancedFormattingFalseRadio');
        }
        return this._AdvancedFormattingFalseRadioJ$2;
    },
    
    _AdvancedFormattingFalseRadioJ$2: null,
    
    get_advancedContainerPanel: function Js_Controls_Html_View$get_advancedContainerPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedContainerPanel$2 == null) {
            this._AdvancedContainerPanel$2 = document.getElementById(this.clientId + '_AdvancedContainerPanel');
        }
        return this._AdvancedContainerPanel$2;
    },
    
    _AdvancedContainerPanel$2: null,
    
    get_advancedContainerPanelJ: function Js_Controls_Html_View$get_advancedContainerPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedContainerPanelJ$2 == null) {
            this._AdvancedContainerPanelJ$2 = $('#' + this.clientId + '_AdvancedContainerPanel');
        }
        return this._AdvancedContainerPanelJ$2;
    },
    
    _AdvancedContainerPanelJ$2: null,
    
    get_advancedContainerTrueRadio: function Js_Controls_Html_View$get_advancedContainerTrueRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedContainerTrueRadio$2 == null) {
            this._AdvancedContainerTrueRadio$2 = document.getElementById(this.clientId + '_AdvancedContainerTrueRadio');
        }
        return this._AdvancedContainerTrueRadio$2;
    },
    
    _AdvancedContainerTrueRadio$2: null,
    
    get_advancedContainerTrueRadioJ: function Js_Controls_Html_View$get_advancedContainerTrueRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedContainerTrueRadioJ$2 == null) {
            this._AdvancedContainerTrueRadioJ$2 = $('#' + this.clientId + '_AdvancedContainerTrueRadio');
        }
        return this._AdvancedContainerTrueRadioJ$2;
    },
    
    _AdvancedContainerTrueRadioJ$2: null,
    
    get_advancedContainerFalseRadio: function Js_Controls_Html_View$get_advancedContainerFalseRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedContainerFalseRadio$2 == null) {
            this._AdvancedContainerFalseRadio$2 = document.getElementById(this.clientId + '_AdvancedContainerFalseRadio');
        }
        return this._AdvancedContainerFalseRadio$2;
    },
    
    _AdvancedContainerFalseRadio$2: null,
    
    get_advancedContainerFalseRadioJ: function Js_Controls_Html_View$get_advancedContainerFalseRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedContainerFalseRadioJ$2 == null) {
            this._AdvancedContainerFalseRadioJ$2 = $('#' + this.clientId + '_AdvancedContainerFalseRadio');
        }
        return this._AdvancedContainerFalseRadioJ$2;
    },
    
    _AdvancedContainerFalseRadioJ$2: null,
    
    get_advancedParseNowPanel: function Js_Controls_Html_View$get_advancedParseNowPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedParseNowPanel$2 == null) {
            this._AdvancedParseNowPanel$2 = document.getElementById(this.clientId + '_AdvancedParseNowPanel');
        }
        return this._AdvancedParseNowPanel$2;
    },
    
    _AdvancedParseNowPanel$2: null,
    
    get_advancedParseNowPanelJ: function Js_Controls_Html_View$get_advancedParseNowPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedParseNowPanelJ$2 == null) {
            this._AdvancedParseNowPanelJ$2 = $('#' + this.clientId + '_AdvancedParseNowPanel');
        }
        return this._AdvancedParseNowPanelJ$2;
    },
    
    _AdvancedParseNowPanelJ$2: null,
    
    get_advancedParseNowButton: function Js_Controls_Html_View$get_advancedParseNowButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedParseNowButton$2 == null) {
            this._AdvancedParseNowButton$2 = document.getElementById(this.clientId + '_AdvancedParseNowButton');
        }
        return this._AdvancedParseNowButton$2;
    },
    
    _AdvancedParseNowButton$2: null,
    
    get_advancedParseNowButtonJ: function Js_Controls_Html_View$get_advancedParseNowButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedParseNowButtonJ$2 == null) {
            this._AdvancedParseNowButtonJ$2 = $('#' + this.clientId + '_AdvancedParseNowButton');
        }
        return this._AdvancedParseNowButtonJ$2;
    },
    
    _AdvancedParseNowButtonJ$2: null,
    
    get_advancedTagsToggleButton: function Js_Controls_Html_View$get_advancedTagsToggleButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedTagsToggleButton$2 == null) {
            this._AdvancedTagsToggleButton$2 = document.getElementById(this.clientId + '_AdvancedTagsToggleButton');
        }
        return this._AdvancedTagsToggleButton$2;
    },
    
    _AdvancedTagsToggleButton$2: null,
    
    get_advancedTagsToggleButtonJ: function Js_Controls_Html_View$get_advancedTagsToggleButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedTagsToggleButtonJ$2 == null) {
            this._AdvancedTagsToggleButtonJ$2 = $('#' + this.clientId + '_AdvancedTagsToggleButton');
        }
        return this._AdvancedTagsToggleButtonJ$2;
    },
    
    _AdvancedTagsToggleButtonJ$2: null,
    
    get_advancedTagsPanel: function Js_Controls_Html_View$get_advancedTagsPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AdvancedTagsPanel$2 == null) {
            this._AdvancedTagsPanel$2 = document.getElementById(this.clientId + '_AdvancedTagsPanel');
        }
        return this._AdvancedTagsPanel$2;
    },
    
    _AdvancedTagsPanel$2: null,
    
    get_advancedTagsPanelJ: function Js_Controls_Html_View$get_advancedTagsPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AdvancedTagsPanelJ$2 == null) {
            this._AdvancedTagsPanelJ$2 = $('#' + this.clientId + '_AdvancedTagsPanel');
        }
        return this._AdvancedTagsPanelJ$2;
    },
    
    _AdvancedTagsPanelJ$2: null,
    
    get_textBoxDiv: function Js_Controls_Html_View$get_textBoxDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._TextBoxDiv$2 == null) {
            this._TextBoxDiv$2 = document.getElementById(this.clientId + '_TextBoxDiv');
        }
        return this._TextBoxDiv$2;
    },
    
    _TextBoxDiv$2: null,
    
    get_textBoxDivJ: function Js_Controls_Html_View$get_textBoxDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._TextBoxDivJ$2 == null) {
            this._TextBoxDivJ$2 = $('#' + this.clientId + '_TextBoxDiv');
        }
        return this._TextBoxDivJ$2;
    },
    
    _TextBoxDivJ$2: null,
    
    get_htmlTextBox: function Js_Controls_Html_View$get_htmlTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._HtmlTextBox$2 == null) {
            this._HtmlTextBox$2 = document.getElementById(this.clientId + '_HtmlTextBox');
        }
        return this._HtmlTextBox$2;
    },
    
    _HtmlTextBox$2: null,
    
    get_htmlTextBoxJ: function Js_Controls_Html_View$get_htmlTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._HtmlTextBoxJ$2 == null) {
            this._HtmlTextBoxJ$2 = $('#' + this.clientId + '_HtmlTextBox');
        }
        return this._HtmlTextBoxJ$2;
    },
    
    _HtmlTextBoxJ$2: null,
    
    get_disabledMessageDiv: function Js_Controls_Html_View$get_disabledMessageDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._DisabledMessageDiv$2 == null) {
            this._DisabledMessageDiv$2 = document.getElementById(this.clientId + '_DisabledMessageDiv');
        }
        return this._DisabledMessageDiv$2;
    },
    
    _DisabledMessageDiv$2: null,
    
    get_disabledMessageDivJ: function Js_Controls_Html_View$get_disabledMessageDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._DisabledMessageDivJ$2 == null) {
            this._DisabledMessageDivJ$2 = $('#' + this.clientId + '_DisabledMessageDiv');
        }
        return this._DisabledMessageDivJ$2;
    },
    
    _DisabledMessageDivJ$2: null,
    
    get_buttonsContainer: function Js_Controls_Html_View$get_buttonsContainer() {
        /// <value type="Object" domElement="true"></value>
        if (this._ButtonsContainer$2 == null) {
            this._ButtonsContainer$2 = document.getElementById(this.clientId + '_ButtonsContainer');
        }
        return this._ButtonsContainer$2;
    },
    
    _ButtonsContainer$2: null,
    
    get_buttonsContainerJ: function Js_Controls_Html_View$get_buttonsContainerJ() {
        /// <value type="jQueryObject"></value>
        if (this._ButtonsContainerJ$2 == null) {
            this._ButtonsContainerJ$2 = $('#' + this.clientId + '_ButtonsContainer');
        }
        return this._ButtonsContainerJ$2;
    },
    
    _ButtonsContainerJ$2: null,
    
    get_saveDiv: function Js_Controls_Html_View$get_saveDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._SaveDiv$2 == null) {
            this._SaveDiv$2 = document.getElementById(this.clientId + '_SaveDiv');
        }
        return this._SaveDiv$2;
    },
    
    _SaveDiv$2: null,
    
    get_saveDivJ: function Js_Controls_Html_View$get_saveDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._SaveDivJ$2 == null) {
            this._SaveDivJ$2 = $('#' + this.clientId + '_SaveDiv');
        }
        return this._SaveDivJ$2;
    },
    
    _SaveDivJ$2: null,
    
    get_saveButton: function Js_Controls_Html_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._SaveButton$2 == null) {
            this._SaveButton$2 = document.getElementById(this.clientId + '_SaveButton');
        }
        return this._SaveButton$2;
    },
    
    _SaveButton$2: null,
    
    get_saveButtonJ: function Js_Controls_Html_View$get_saveButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._SaveButtonJ$2 == null) {
            this._SaveButtonJ$2 = $('#' + this.clientId + '_SaveButton');
        }
        return this._SaveButtonJ$2;
    },
    
    _SaveButtonJ$2: null,
    
    get_previewButton: function Js_Controls_Html_View$get_previewButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._PreviewButton$2 == null) {
            this._PreviewButton$2 = document.getElementById(this.clientId + '_PreviewButton');
        }
        return this._PreviewButton$2;
    },
    
    _PreviewButton$2: null,
    
    get_previewButtonJ: function Js_Controls_Html_View$get_previewButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._PreviewButtonJ$2 == null) {
            this._PreviewButtonJ$2 = $('#' + this.clientId + '_PreviewButton');
        }
        return this._PreviewButtonJ$2;
    },
    
    _PreviewButtonJ$2: null,
    
    get_hidePreviewButton: function Js_Controls_Html_View$get_hidePreviewButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._HidePreviewButton$2 == null) {
            this._HidePreviewButton$2 = document.getElementById(this.clientId + '_HidePreviewButton');
        }
        return this._HidePreviewButton$2;
    },
    
    _HidePreviewButton$2: null,
    
    get_hidePreviewButtonJ: function Js_Controls_Html_View$get_hidePreviewButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._HidePreviewButtonJ$2 == null) {
            this._HidePreviewButtonJ$2 = $('#' + this.clientId + '_HidePreviewButton');
        }
        return this._HidePreviewButtonJ$2;
    },
    
    _HidePreviewButtonJ$2: null,
    
    get_previewPanelContainer: function Js_Controls_Html_View$get_previewPanelContainer() {
        /// <value type="Object" domElement="true"></value>
        if (this._PreviewPanelContainer$2 == null) {
            this._PreviewPanelContainer$2 = document.getElementById(this.clientId + '_PreviewPanelContainer');
        }
        return this._PreviewPanelContainer$2;
    },
    
    _PreviewPanelContainer$2: null,
    
    get_previewPanelContainerJ: function Js_Controls_Html_View$get_previewPanelContainerJ() {
        /// <value type="jQueryObject"></value>
        if (this._PreviewPanelContainerJ$2 == null) {
            this._PreviewPanelContainerJ$2 = $('#' + this.clientId + '_PreviewPanelContainer');
        }
        return this._PreviewPanelContainerJ$2;
    },
    
    _PreviewPanelContainerJ$2: null,
    
    get_previewPanel: function Js_Controls_Html_View$get_previewPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._PreviewPanel$2 == null) {
            this._PreviewPanel$2 = document.getElementById(this.clientId + '_PreviewPanel');
        }
        return this._PreviewPanel$2;
    },
    
    _PreviewPanel$2: null,
    
    get_previewPanelJ: function Js_Controls_Html_View$get_previewPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._PreviewPanelJ$2 == null) {
            this._PreviewPanelJ$2 = $('#' + this.clientId + '_PreviewPanel');
        }
        return this._PreviewPanelJ$2;
    },
    
    _PreviewPanelJ$2: null,
    
    get_uiEnabled: function Js_Controls_Html_View$get_uiEnabled() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiEnabled$2 == null) {
            this._uiEnabled$2 = document.getElementById(this.clientId + '_uiEnabled');
        }
        return this._uiEnabled$2;
    },
    
    _uiEnabled$2: null,
    
    get_uiEnabledJ: function Js_Controls_Html_View$get_uiEnabledJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiEnabledJ$2 == null) {
            this._uiEnabledJ$2 = $('#' + this.clientId + '_uiEnabled');
        }
        return this._uiEnabledJ$2;
    },
    
    _uiEnabledJ$2: null,
    
    get_uiPreviewType: function Js_Controls_Html_View$get_uiPreviewType() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPreviewType$2 == null) {
            this._uiPreviewType$2 = document.getElementById(this.clientId + '_uiPreviewType');
        }
        return this._uiPreviewType$2;
    },
    
    _uiPreviewType$2: null,
    
    get_uiPreviewTypeJ: function Js_Controls_Html_View$get_uiPreviewTypeJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPreviewTypeJ$2 == null) {
            this._uiPreviewTypeJ$2 = $('#' + this.clientId + '_uiPreviewType');
        }
        return this._uiPreviewTypeJ$2;
    },
    
    _uiPreviewTypeJ$2: null,
    
    get_helperPanelDisplayState: function Js_Controls_Html_View$get_helperPanelDisplayState() {
        /// <value type="Object" domElement="true"></value>
        if (this._HelperPanelDisplayState$2 == null) {
            this._HelperPanelDisplayState$2 = document.getElementById(this.clientId + '_HelperPanelDisplayState');
        }
        return this._HelperPanelDisplayState$2;
    },
    
    _HelperPanelDisplayState$2: null,
    
    get_helperPanelDisplayStateJ: function Js_Controls_Html_View$get_helperPanelDisplayStateJ() {
        /// <value type="jQueryObject"></value>
        if (this._HelperPanelDisplayStateJ$2 == null) {
            this._HelperPanelDisplayStateJ$2 = $('#' + this.clientId + '_HelperPanelDisplayState');
        }
        return this._HelperPanelDisplayStateJ$2;
    },
    
    _HelperPanelDisplayStateJ$2: null,
    
    get_genericContainerPage: function Js_Controls_Html_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Controls_Html_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Controls.Html.Controller.registerClass('Js.Controls.Html.Controller');
Js.Controls.Html.View.registerClass('Js.Controls.Html.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
