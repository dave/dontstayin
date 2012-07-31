Type.registerNamespace('SpottedScript.Blank.Welcome');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Welcome.View
SpottedScript.Blank.Welcome.View = function SpottedScript_Blank_Welcome_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Welcome.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Welcome.View.prototype = {
    clientId: null,
    get_welcomeHeaderInvite: function SpottedScript_Blank_Welcome_View$get_welcomeHeaderInvite() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WelcomeHeaderInvite');
    },
    get_welcomeHeaderSignUp: function SpottedScript_Blank_Welcome_View$get_welcomeHeaderSignUp() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WelcomeHeaderSignUp');
    },
    get_welcomePart1Header: function SpottedScript_Blank_Welcome_View$get_welcomePart1Header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WelcomePart1Header');
    },
    get_validationsummary1: function SpottedScript_Blank_Welcome_View$get_validationsummary1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary1');
    },
    get_uiAddedByUsrsDiv: function SpottedScript_Blank_Welcome_View$get_uiAddedByUsrsDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddedByUsrsDiv');
    },
    get_uiAddedByUsrLabel: function SpottedScript_Blank_Welcome_View$get_uiAddedByUsrLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddedByUsrLabel');
    },
    get_uiAddedByUsrsGridView: function SpottedScript_Blank_Welcome_View$get_uiAddedByUsrsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddedByUsrsGridView');
    },
    get_uiAddedByGroupsDiv: function SpottedScript_Blank_Welcome_View$get_uiAddedByGroupsDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddedByGroupsDiv');
    },
    get_uiAddedByGroupLabel: function SpottedScript_Blank_Welcome_View$get_uiAddedByGroupLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddedByGroupLabel');
    },
    get_uiAddedByGroupsGridView: function SpottedScript_Blank_Welcome_View$get_uiAddedByGroupsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddedByGroupsGridView');
    },
    get_nickName: function SpottedScript_Blank_Welcome_View$get_nickName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NickName');
    },
    get_requiredfieldvalidator1: function SpottedScript_Blank_Welcome_View$get_requiredfieldvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator1');
    },
    get_regularexpressionvalidator99: function SpottedScript_Blank_Welcome_View$get_regularexpressionvalidator99() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Regularexpressionvalidator99');
    },
    get_customvalidator1: function SpottedScript_Blank_Welcome_View$get_customvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator1');
    },
    get_firstName: function SpottedScript_Blank_Welcome_View$get_firstName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FirstName');
    },
    get_requiredfieldvalidator2: function SpottedScript_Blank_Welcome_View$get_requiredfieldvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator2');
    },
    get_lastName: function SpottedScript_Blank_Welcome_View$get_lastName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LastName');
    },
    get_requiredfieldvalidator3: function SpottedScript_Blank_Welcome_View$get_requiredfieldvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator3');
    },
    get_email: function SpottedScript_Blank_Welcome_View$get_email() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Email');
    },
    get_requiredfieldvalidator4: function SpottedScript_Blank_Welcome_View$get_requiredfieldvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator4');
    },
    get_emailRegex: function SpottedScript_Blank_Welcome_View$get_emailRegex() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailRegex');
    },
    get_emailDuplicateValidator: function SpottedScript_Blank_Welcome_View$get_emailDuplicateValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailDuplicateValidator');
    },
    get_passwordTr: function SpottedScript_Blank_Welcome_View$get_passwordTr() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PasswordTr');
    },
    get_password1: function SpottedScript_Blank_Welcome_View$get_password1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password1');
    },
    get_password2: function SpottedScript_Blank_Welcome_View$get_password2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password2');
    },
    get_requiredFieldValidator5: function SpottedScript_Blank_Welcome_View$get_requiredFieldValidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator5');
    },
    get_regularExpressionValidator1: function SpottedScript_Blank_Welcome_View$get_regularExpressionValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator1');
    },
    get_compareValidator1: function SpottedScript_Blank_Welcome_View$get_compareValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator1');
    },
    get_dialingCodeDropDown: function SpottedScript_Blank_Welcome_View$get_dialingCodeDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeDropDown');
    },
    get_dialingCodeOtherSpan: function SpottedScript_Blank_Welcome_View$get_dialingCodeOtherSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeOtherSpan');
    },
    get_dialingCodeOther: function SpottedScript_Blank_Welcome_View$get_dialingCodeOther() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeOther');
    },
    get_mobileNumber: function SpottedScript_Blank_Welcome_View$get_mobileNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MobileNumber');
    },
    get_sexMale: function SpottedScript_Blank_Welcome_View$get_sexMale() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SexMale');
    },
    get_sexFemale: function SpottedScript_Blank_Welcome_View$get_sexFemale() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SexFemale');
    },
    get_customvalidator2: function SpottedScript_Blank_Welcome_View$get_customvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator2');
    },
    get_dateOfBirthDay: function SpottedScript_Blank_Welcome_View$get_dateOfBirthDay() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthDay');
    },
    get_dateOfBirthMonth: function SpottedScript_Blank_Welcome_View$get_dateOfBirthMonth() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthMonth');
    },
    get_dateOfBirthYear: function SpottedScript_Blank_Welcome_View$get_dateOfBirthYear() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthYear');
    },
    get_customvalidator8: function SpottedScript_Blank_Welcome_View$get_customvalidator8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator8');
    },
    get_customvalidator9: function SpottedScript_Blank_Welcome_View$get_customvalidator9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator9');
    },
    get_favouriteMusicDropDownList: function SpottedScript_Blank_Welcome_View$get_favouriteMusicDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouriteMusicDropDownList');
    },
    get_customvalidator6: function SpottedScript_Blank_Welcome_View$get_customvalidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator6');
    },
    get_isDjYes: function SpottedScript_Blank_Welcome_View$get_isDjYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IsDjYes');
    },
    get_isDjNo: function SpottedScript_Blank_Welcome_View$get_isDjNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IsDjNo');
    },
    get_customValidatorIsDj: function SpottedScript_Blank_Welcome_View$get_customValidatorIsDj() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidatorIsDj');
    },
    get_homeTownDropDownList: function SpottedScript_Blank_Welcome_View$get_homeTownDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HomeTownDropDownList');
    },
    get_customvalidator4: function SpottedScript_Blank_Welcome_View$get_customvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator4');
    },
    get_sendSpottedEmails: function SpottedScript_Blank_Welcome_View$get_sendSpottedEmails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendSpottedEmails');
    },
    get_sendInvites: function SpottedScript_Blank_Welcome_View$get_sendInvites() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendInvites');
    },
    get_sendFlyers: function SpottedScript_Blank_Welcome_View$get_sendFlyers() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendFlyers');
    },
    get_sendSpottedTexts: function SpottedScript_Blank_Welcome_View$get_sendSpottedTexts() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendSpottedTexts');
    },
    get_hipChallengeTextBox: function SpottedScript_Blank_Welcome_View$get_hipChallengeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HipChallengeTextBox');
    },
    get_hipImage: function SpottedScript_Blank_Welcome_View$get_hipImage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HipImage');
    },
    get_customvalidator10: function SpottedScript_Blank_Welcome_View$get_customvalidator10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator10');
    },
    get_termsCheckbox: function SpottedScript_Blank_Welcome_View$get_termsCheckbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TermsCheckbox');
    },
    get_customvalidator7: function SpottedScript_Blank_Welcome_View$get_customvalidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator7');
    },
    get_prefsUpdateButton: function SpottedScript_Blank_Welcome_View$get_prefsUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrefsUpdateButton');
    },
    get_welcomePart2And3: function SpottedScript_Blank_Welcome_View$get_welcomePart2And3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WelcomePart2And3');
    },
    get_logOffButton: function SpottedScript_Blank_Welcome_View$get_logOffButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LogOffButton');
    },
    get_unsubscribeButton: function SpottedScript_Blank_Welcome_View$get_unsubscribeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UnsubscribeButton');
    },
    get_genericContainerPage: function SpottedScript_Blank_Welcome_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Welcome.View.registerClass('SpottedScript.Blank.Welcome.View', SpottedScript.BlankUserControl.View);
