Type.registerNamespace('SpottedScript.Pages.Promoters.StyledSetup');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.StyledSetup.View
SpottedScript.Pages.Promoters.StyledSetup.View = function SpottedScript_Pages_Promoters_StyledSetup_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.StyledSetup.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.StyledSetup.View.prototype = {
    clientId: null,
    get_h1Header: function SpottedScript_Pages_Promoters_StyledSetup_View$get_h1Header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Header');
    },
    get_customiseOptionsPanel: function SpottedScript_Pages_Promoters_StyledSetup_View$get_customiseOptionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomiseOptionsPanel');
    },
    get_cssDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_cssDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CssDropDownList');
    },
    get_inputCssFile: function SpottedScript_Pages_Promoters_StyledSetup_View$get_inputCssFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InputCssFile');
    },
    get_uploadCssButton: function SpottedScript_Pages_Promoters_StyledSetup_View$get_uploadCssButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UploadCssButton');
    },
    get_cssUrlHiddenTextBox: function SpottedScript_Pages_Promoters_StyledSetup_View$get_cssUrlHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CssUrlHiddenTextBox');
    },
    get_inputLogoFile: function SpottedScript_Pages_Promoters_StyledSetup_View$get_inputLogoFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InputLogoFile');
    },
    get_uploadLogoButton: function SpottedScript_Pages_Promoters_StyledSetup_View$get_uploadLogoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UploadLogoButton');
    },
    get_logoUrlHiddenTextBox: function SpottedScript_Pages_Promoters_StyledSetup_View$get_logoUrlHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LogoUrlHiddenTextBox');
    },
    get_logoAlignDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_logoAlignDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LogoAlignDropDownList');
    },
    get_inputBackgroundFile: function SpottedScript_Pages_Promoters_StyledSetup_View$get_inputBackgroundFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InputBackgroundFile');
    },
    get_uploadBackgroundButton: function SpottedScript_Pages_Promoters_StyledSetup_View$get_uploadBackgroundButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UploadBackgroundButton');
    },
    get_backgroundUrlHiddenTextBox: function SpottedScript_Pages_Promoters_StyledSetup_View$get_backgroundUrlHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BackgroundUrlHiddenTextBox');
    },
    get_noRepeatBackgroundCheckBox: function SpottedScript_Pages_Promoters_StyledSetup_View$get_noRepeatBackgroundCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoRepeatBackgroundCheckBox');
    },
    get_customiseTable: function SpottedScript_Pages_Promoters_StyledSetup_View$get_customiseTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomiseTable');
    },
    get_fontFamilyDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_fontFamilyDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FontFamilyDropDownList');
    },
    get_bodyTextColourInput: function SpottedScript_Pages_Promoters_StyledSetup_View$get_bodyTextColourInput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BodyTextColourInput');
    },
    get_bodyBackgroundColourInput: function SpottedScript_Pages_Promoters_StyledSetup_View$get_bodyBackgroundColourInput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BodyBackgroundColourInput');
    },
    get_bodyFontSizeDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_bodyFontSizeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BodyFontSizeDropDownList');
    },
    get_bodyFontWeightDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_bodyFontWeightDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BodyFontWeightDropDownList');
    },
    get_bodyTextDecorationDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_bodyTextDecorationDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BodyTextDecorationDropDownList');
    },
    get_bodyTextAlignDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_bodyTextAlignDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BodyTextAlignDropDownList');
    },
    get_headerTextColourInput: function SpottedScript_Pages_Promoters_StyledSetup_View$get_headerTextColourInput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderTextColourInput');
    },
    get_headerBackgroundColourInput: function SpottedScript_Pages_Promoters_StyledSetup_View$get_headerBackgroundColourInput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderBackgroundColourInput');
    },
    get_headerFontSizeDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_headerFontSizeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderFontSizeDropDownList');
    },
    get_headerFontWeightDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_headerFontWeightDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderFontWeightDropDownList');
    },
    get_headerTextDecorationDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_headerTextDecorationDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderTextDecorationDropDownList');
    },
    get_headerTextAlignDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_headerTextAlignDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderTextAlignDropDownList');
    },
    get_linksTextColourInput: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksTextColourInput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksTextColourInput');
    },
    get_linksBackgroundColourInput: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksBackgroundColourInput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksBackgroundColourInput');
    },
    get_linksFontSizeDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksFontSizeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksFontSizeDropDownList');
    },
    get_linksFontWeightDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksFontWeightDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksFontWeightDropDownList');
    },
    get_linksTextDecorationDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksTextDecorationDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksTextDecorationDropDownList');
    },
    get_linksTextAlignDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksTextAlignDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksTextAlignDropDownList');
    },
    get_linksHoverTextColourInput: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksHoverTextColourInput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksHoverTextColourInput');
    },
    get_linksHoverBackgroundColourInput: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksHoverBackgroundColourInput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksHoverBackgroundColourInput');
    },
    get_linksHoverFontSizeDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksHoverFontSizeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksHoverFontSizeDropDownList');
    },
    get_linksHoverFontWeightDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksHoverFontWeightDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksHoverFontWeightDropDownList');
    },
    get_linksHoverTextDecorationDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksHoverTextDecorationDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksHoverTextDecorationDropDownList');
    },
    get_linksHoverTextAlignDropDownList: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linksHoverTextAlignDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinksHoverTextAlignDropDownList');
    },
    get_saveCustomStyleButton: function SpottedScript_Pages_Promoters_StyledSetup_View$get_saveCustomStyleButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveCustomStyleButton');
    },
    get_linkToStyledPages: function SpottedScript_Pages_Promoters_StyledSetup_View$get_linkToStyledPages() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkToStyledPages');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_StyledSetup_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.StyledSetup.View.registerClass('SpottedScript.Pages.Promoters.StyledSetup.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
