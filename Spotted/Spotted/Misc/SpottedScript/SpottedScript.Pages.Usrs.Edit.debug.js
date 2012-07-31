Type.registerNamespace('SpottedScript.Pages.Usrs.Edit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.Edit.View
SpottedScript.Pages.Usrs.Edit.View = function SpottedScript_Pages_Usrs_Edit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.Edit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.Edit.View.prototype = {
    clientId: null,
    get_h11: function SpottedScript_Pages_Usrs_Edit_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_prefsUpdatePanel: function SpottedScript_Pages_Usrs_Edit_View$get_prefsUpdatePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrefsUpdatePanel');
    },
    get_validationSummary: function SpottedScript_Pages_Usrs_Edit_View$get_validationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ValidationSummary');
    },
    get_successDiv: function SpottedScript_Pages_Usrs_Edit_View$get_successDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuccessDiv');
    },
    get_firstName: function SpottedScript_Pages_Usrs_Edit_View$get_firstName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FirstName');
    },
    get_lastName: function SpottedScript_Pages_Usrs_Edit_View$get_lastName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LastName');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Usrs_Edit_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_requiredfieldvalidator3: function SpottedScript_Pages_Usrs_Edit_View$get_requiredfieldvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator3');
    },
    get_nickName: function SpottedScript_Pages_Usrs_Edit_View$get_nickName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NickName');
    },
    get_requiredfieldvalidator49999: function SpottedScript_Pages_Usrs_Edit_View$get_requiredfieldvalidator49999() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator49999');
    },
    get_regularexpressionvalidator99: function SpottedScript_Pages_Usrs_Edit_View$get_regularexpressionvalidator99() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Regularexpressionvalidator99');
    },
    get_customvalidator5: function SpottedScript_Pages_Usrs_Edit_View$get_customvalidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator5');
    },
    get_email: function SpottedScript_Pages_Usrs_Edit_View$get_email() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Email');
    },
    get_requiredfieldvalidator2: function SpottedScript_Pages_Usrs_Edit_View$get_requiredfieldvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator2');
    },
    get_emailRegex: function SpottedScript_Pages_Usrs_Edit_View$get_emailRegex() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailRegex');
    },
    get_emailDuplicateValidator: function SpottedScript_Pages_Usrs_Edit_View$get_emailDuplicateValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_emailDuplicateValidator');
    },
    get_dialingCodeDropDown: function SpottedScript_Pages_Usrs_Edit_View$get_dialingCodeDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeDropDown');
    },
    get_mobileNumber: function SpottedScript_Pages_Usrs_Edit_View$get_mobileNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MobileNumber');
    },
    get_dialingCodeOtherSpan: function SpottedScript_Pages_Usrs_Edit_View$get_dialingCodeOtherSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeOtherSpan');
    },
    get_dialingCodeOther: function SpottedScript_Pages_Usrs_Edit_View$get_dialingCodeOther() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeOther');
    },
    get_sexMale: function SpottedScript_Pages_Usrs_Edit_View$get_sexMale() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SexMale');
    },
    get_sexFemale: function SpottedScript_Pages_Usrs_Edit_View$get_sexFemale() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SexFemale');
    },
    get_customValidator1: function SpottedScript_Pages_Usrs_Edit_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_dateOfBirthYear: function SpottedScript_Pages_Usrs_Edit_View$get_dateOfBirthYear() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthYear');
    },
    get_dateOfBirthMonth: function SpottedScript_Pages_Usrs_Edit_View$get_dateOfBirthMonth() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthMonth');
    },
    get_dateOfBirthDay: function SpottedScript_Pages_Usrs_Edit_View$get_dateOfBirthDay() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthDay');
    },
    get_customvalidator8: function SpottedScript_Pages_Usrs_Edit_View$get_customvalidator8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator8');
    },
    get_customvalidator9: function SpottedScript_Pages_Usrs_Edit_View$get_customvalidator9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator9');
    },
    get_favouriteMusicDropDownList: function SpottedScript_Pages_Usrs_Edit_View$get_favouriteMusicDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouriteMusicDropDownList');
    },
    get_customvalidator4: function SpottedScript_Pages_Usrs_Edit_View$get_customvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator4');
    },
    get_homeTownPlacePicker: function SpottedScript_Pages_Usrs_Edit_View$get_homeTownPlacePicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_HomeTownPlacePickerController');
    },
    get_addressStreetTextBox: function SpottedScript_Pages_Usrs_Edit_View$get_addressStreetTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressStreetTextBox');
    },
    get_addressAreaTextBox: function SpottedScript_Pages_Usrs_Edit_View$get_addressAreaTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressAreaTextBox');
    },
    get_addressTownTextBox: function SpottedScript_Pages_Usrs_Edit_View$get_addressTownTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressTownTextBox');
    },
    get_addressCountyTextBox: function SpottedScript_Pages_Usrs_Edit_View$get_addressCountyTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressCountyTextBox');
    },
    get_addressPostcodeTextBox: function SpottedScript_Pages_Usrs_Edit_View$get_addressPostcodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressPostcodeTextBox');
    },
    get_addressCountryDropDownList: function SpottedScript_Pages_Usrs_Edit_View$get_addressCountryDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressCountryDropDownList');
    },
    get_isDjYes: function SpottedScript_Pages_Usrs_Edit_View$get_isDjYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IsDjYes');
    },
    get_isDjNo: function SpottedScript_Pages_Usrs_Edit_View$get_isDjNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IsDjNo');
    },
    get_customValidatorIsDj: function SpottedScript_Pages_Usrs_Edit_View$get_customValidatorIsDj() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidatorIsDj');
    },
    get_prefsUpdateButton: function SpottedScript_Pages_Usrs_Edit_View$get_prefsUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrefsUpdateButton');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_Edit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.Edit.View.registerClass('SpottedScript.Pages.Usrs.Edit.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
