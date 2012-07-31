Type.registerNamespace('SpottedScript.Styled.Setup');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.Setup.View
SpottedScript.Styled.Setup.View = function SpottedScript_Styled_Setup_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.Setup.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.Setup.View.prototype = {
    clientId: null,
    get_inputCssFile: function SpottedScript_Styled_Setup_View$get_inputCssFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InputCssFile');
    },
    get_uploadCssButton: function SpottedScript_Styled_Setup_View$get_uploadCssButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UploadCssButton');
    },
    get_cssUrlHiddenTextBox: function SpottedScript_Styled_Setup_View$get_cssUrlHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CssUrlHiddenTextBox');
    },
    get_inputLogoFile: function SpottedScript_Styled_Setup_View$get_inputLogoFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InputLogoFile');
    },
    get_uploadLogoButton: function SpottedScript_Styled_Setup_View$get_uploadLogoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UploadLogoButton');
    },
    get_logoUrlHiddenTextBox: function SpottedScript_Styled_Setup_View$get_logoUrlHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LogoUrlHiddenTextBox');
    },
    get_inputBackgroundFile: function SpottedScript_Styled_Setup_View$get_inputBackgroundFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InputBackgroundFile');
    },
    get_uploadBackgroundButton: function SpottedScript_Styled_Setup_View$get_uploadBackgroundButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UploadBackgroundButton');
    },
    get_backgroundUrlHiddenTextBox: function SpottedScript_Styled_Setup_View$get_backgroundUrlHiddenTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BackgroundUrlHiddenTextBox');
    },
    get_genericContainerPage: function SpottedScript_Styled_Setup_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.Setup.View.registerClass('SpottedScript.Styled.Setup.View', SpottedScript.StyledUserControl.View);
