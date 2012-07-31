Type.registerNamespace('SpottedScript.Styled.Login');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Styled.Login.View
SpottedScript.Styled.Login.View = function SpottedScript_Styled_Login_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Styled.Login.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Styled.Login.View.prototype = {
    clientId: null,
    get_alreadySignedInP: function SpottedScript_Styled_Login_View$get_alreadySignedInP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AlreadySignedInP');
    },
    get_userFirstNameLabel: function SpottedScript_Styled_Login_View$get_userFirstNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserFirstNameLabel');
    },
    get_continueButton: function SpottedScript_Styled_Login_View$get_continueButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ContinueButton');
    },
    get_loginEmailTextBox: function SpottedScript_Styled_Login_View$get_loginEmailTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoginEmailTextBox');
    },
    get_loginEmailRequiredFieldValidator: function SpottedScript_Styled_Login_View$get_loginEmailRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoginEmailRequiredFieldValidator');
    },
    get_loginPasswordTextBox: function SpottedScript_Styled_Login_View$get_loginPasswordTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoginPasswordTextBox');
    },
    get_loginPasswordRequiredFieldValidator: function SpottedScript_Styled_Login_View$get_loginPasswordRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoginPasswordRequiredFieldValidator');
    },
    get_loginButton: function SpottedScript_Styled_Login_View$get_loginButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoginButton');
    },
    get_loginCustomValidator: function SpottedScript_Styled_Login_View$get_loginCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoginCustomValidator');
    },
    get_loginValidationSummary: function SpottedScript_Styled_Login_View$get_loginValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoginValidationSummary');
    },
    get_registrationFirstNameTextBox: function SpottedScript_Styled_Login_View$get_registrationFirstNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationFirstNameTextBox');
    },
    get_registrationFirstNameRequiredFieldValidator: function SpottedScript_Styled_Login_View$get_registrationFirstNameRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationFirstNameRequiredFieldValidator');
    },
    get_registrationLastNameTextBox: function SpottedScript_Styled_Login_View$get_registrationLastNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationLastNameTextBox');
    },
    get_registrationLastNameRequiredFieldValidator: function SpottedScript_Styled_Login_View$get_registrationLastNameRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationLastNameRequiredFieldValidator');
    },
    get_registrationEmailTextBox: function SpottedScript_Styled_Login_View$get_registrationEmailTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationEmailTextBox');
    },
    get_registrationEmailRequiredFieldValidator: function SpottedScript_Styled_Login_View$get_registrationEmailRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationEmailRequiredFieldValidator');
    },
    get_registrationEmailRegularExpressionValidator: function SpottedScript_Styled_Login_View$get_registrationEmailRegularExpressionValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationEmailRegularExpressionValidator');
    },
    get_registrationPasswordTextBox: function SpottedScript_Styled_Login_View$get_registrationPasswordTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationPasswordTextBox');
    },
    get_registrationPasswordRequiredFieldValidator: function SpottedScript_Styled_Login_View$get_registrationPasswordRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationPasswordRequiredFieldValidator');
    },
    get_registrationConfirmPasswordTextBox: function SpottedScript_Styled_Login_View$get_registrationConfirmPasswordTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationConfirmPasswordTextBox');
    },
    get_registrationConfirmPasswordRequiredFieldValidator: function SpottedScript_Styled_Login_View$get_registrationConfirmPasswordRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationConfirmPasswordRequiredFieldValidator');
    },
    get_registrationComparePasswordsValidator: function SpottedScript_Styled_Login_View$get_registrationComparePasswordsValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationComparePasswordsValidator');
    },
    get_termsCheckBox: function SpottedScript_Styled_Login_View$get_termsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TermsCheckBox');
    },
    get_termsCheckBoxCustomValidator: function SpottedScript_Styled_Login_View$get_termsCheckBoxCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TermsCheckBoxCustomValidator');
    },
    get_registrationButton: function SpottedScript_Styled_Login_View$get_registrationButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationButton');
    },
    get_registrationCustomValidator: function SpottedScript_Styled_Login_View$get_registrationCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationCustomValidator');
    },
    get_registrationValidationSummary: function SpottedScript_Styled_Login_View$get_registrationValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegistrationValidationSummary');
    },
    get_javascriptLabel: function SpottedScript_Styled_Login_View$get_javascriptLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_JavascriptLabel');
    },
    get_genericContainerPage: function SpottedScript_Styled_Login_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Styled.Login.View.registerClass('SpottedScript.Styled.Login.View', SpottedScript.StyledUserControl.View);
