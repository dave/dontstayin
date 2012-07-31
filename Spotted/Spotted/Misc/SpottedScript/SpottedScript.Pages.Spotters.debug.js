Type.registerNamespace('SpottedScript.Pages.Spotters');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Spotters.View
SpottedScript.Pages.Spotters.View = function SpottedScript_Pages_Spotters_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Spotters.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Spotters.View.prototype = {
    clientId: null,
    get_panelIntro1: function SpottedScript_Pages_Spotters_View$get_panelIntro1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelIntro1');
    },
    get_uiEventPicker: function SpottedScript_Pages_Spotters_View$get_uiEventPicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_uiEventPickerController');
    },
    get_h19: function SpottedScript_Pages_Spotters_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_button1: function SpottedScript_Pages_Spotters_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_panelNoPhoto: function SpottedScript_Pages_Spotters_View$get_panelNoPhoto() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoPhoto');
    },
    get_h2: function SpottedScript_Pages_Spotters_View$get_h2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H2');
    },
    get_panelSignUpForm: function SpottedScript_Pages_Spotters_View$get_panelSignUpForm() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSignUpForm');
    },
    get_h3: function SpottedScript_Pages_Spotters_View$get_h3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H3');
    },
    get_firstName: function SpottedScript_Pages_Spotters_View$get_firstName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FirstName');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Spotters_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_lastName: function SpottedScript_Pages_Spotters_View$get_lastName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LastName');
    },
    get_requiredFieldValidator2: function SpottedScript_Pages_Spotters_View$get_requiredFieldValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator2');
    },
    get_dialingCodeDropDown: function SpottedScript_Pages_Spotters_View$get_dialingCodeDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeDropDown');
    },
    get_dialingCodeOtherSpan: function SpottedScript_Pages_Spotters_View$get_dialingCodeOtherSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeOtherSpan');
    },
    get_dialingCodeOther: function SpottedScript_Pages_Spotters_View$get_dialingCodeOther() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DialingCodeOther');
    },
    get_mobileNumber: function SpottedScript_Pages_Spotters_View$get_mobileNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MobileNumber');
    },
    get_requiredfieldvalidator3: function SpottedScript_Pages_Spotters_View$get_requiredfieldvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator3');
    },
    get_dateOfBirthDay: function SpottedScript_Pages_Spotters_View$get_dateOfBirthDay() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthDay');
    },
    get_dateOfBirthMonth: function SpottedScript_Pages_Spotters_View$get_dateOfBirthMonth() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthMonth');
    },
    get_dateOfBirthYear: function SpottedScript_Pages_Spotters_View$get_dateOfBirthYear() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateOfBirthYear');
    },
    get_customValidator1: function SpottedScript_Pages_Spotters_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_customValidator2: function SpottedScript_Pages_Spotters_View$get_customValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator2');
    },
    get_addressStreet: function SpottedScript_Pages_Spotters_View$get_addressStreet() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressStreet');
    },
    get_addressArea: function SpottedScript_Pages_Spotters_View$get_addressArea() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressArea');
    },
    get_addressTown: function SpottedScript_Pages_Spotters_View$get_addressTown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressTown');
    },
    get_addressCounty: function SpottedScript_Pages_Spotters_View$get_addressCounty() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressCounty');
    },
    get_addressPostcode: function SpottedScript_Pages_Spotters_View$get_addressPostcode() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressPostcode');
    },
    get_addressCountry: function SpottedScript_Pages_Spotters_View$get_addressCountry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressCountry');
    },
    get_requiredFieldValidator4: function SpottedScript_Pages_Spotters_View$get_requiredFieldValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator4');
    },
    get_requiredFieldValidator5: function SpottedScript_Pages_Spotters_View$get_requiredFieldValidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator5');
    },
    get_requiredFieldValidator6: function SpottedScript_Pages_Spotters_View$get_requiredFieldValidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator6');
    },
    get_requiredFieldValidator7: function SpottedScript_Pages_Spotters_View$get_requiredFieldValidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator7');
    },
    get_customValidator3: function SpottedScript_Pages_Spotters_View$get_customValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator3');
    },
    get_photoUsageUse: function SpottedScript_Pages_Spotters_View$get_photoUsageUse() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoUsageUse');
    },
    get_photoUsageContact: function SpottedScript_Pages_Spotters_View$get_photoUsageContact() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoUsageContact');
    },
    get_photoUsageDoNotUse: function SpottedScript_Pages_Spotters_View$get_photoUsageDoNotUse() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoUsageDoNotUse');
    },
    get_saveSpotterButton: function SpottedScript_Pages_Spotters_View$get_saveSpotterButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveSpotterButton');
    },
    get_spotterSavedLabel: function SpottedScript_Pages_Spotters_View$get_spotterSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSavedLabel');
    },
    get_panelChecklist: function SpottedScript_Pages_Spotters_View$get_panelChecklist() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelChecklist');
    },
    get_h4: function SpottedScript_Pages_Spotters_View$get_h4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H4');
    },
    get_customValidator4: function SpottedScript_Pages_Spotters_View$get_customValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator4');
    },
    get_checklist: function SpottedScript_Pages_Spotters_View$get_checklist() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Checklist');
    },
    get_customValidator5: function SpottedScript_Pages_Spotters_View$get_customValidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator5');
    },
    get_button4: function SpottedScript_Pages_Spotters_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_panelEnabled: function SpottedScript_Pages_Spotters_View$get_panelEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEnabled');
    },
    get_guestlistPanel: function SpottedScript_Pages_Spotters_View$get_guestlistPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GuestlistPanel');
    },
    get_h11: function SpottedScript_Pages_Spotters_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_h1: function SpottedScript_Pages_Spotters_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_requestCardsPanel: function SpottedScript_Pages_Spotters_View$get_requestCardsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequestCardsPanel');
    },
    get_h15: function SpottedScript_Pages_Spotters_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_button7: function SpottedScript_Pages_Spotters_View$get_button7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button7');
    },
    get_requestCardsError2: function SpottedScript_Pages_Spotters_View$get_requestCardsError2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequestCardsError2');
    },
    get_requestCardsError3: function SpottedScript_Pages_Spotters_View$get_requestCardsError3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequestCardsError3');
    },
    get_requestCardsError4: function SpottedScript_Pages_Spotters_View$get_requestCardsError4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequestCardsError4');
    },
    get_requestCardsStatusLabel: function SpottedScript_Pages_Spotters_View$get_requestCardsStatusLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequestCardsStatusLabel');
    },
    get_panelStatusCardsInPost: function SpottedScript_Pages_Spotters_View$get_panelStatusCardsInPost() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelStatusCardsInPost');
    },
    get_linkButton1: function SpottedScript_Pages_Spotters_View$get_linkButton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkButton1');
    },
    get_panelStatusNoCardsInPost: function SpottedScript_Pages_Spotters_View$get_panelStatusNoCardsInPost() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelStatusNoCardsInPost');
    },
    get_h18: function SpottedScript_Pages_Spotters_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_spotterAddressCountryName: function SpottedScript_Pages_Spotters_View$get_spotterAddressCountryName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterAddressCountryName');
    },
    get_button5: function SpottedScript_Pages_Spotters_View$get_button5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button5');
    },
    get_optionsPanel: function SpottedScript_Pages_Spotters_View$get_optionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsPanel');
    },
    get_button6: function SpottedScript_Pages_Spotters_View$get_button6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button6');
    },
    get_eventsPanel: function SpottedScript_Pages_Spotters_View$get_eventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsPanel');
    },
    get_h13: function SpottedScript_Pages_Spotters_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_eventsDataGrid: function SpottedScript_Pages_Spotters_View$get_eventsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsDataGrid');
    },
    get_panelSignedUp: function SpottedScript_Pages_Spotters_View$get_panelSignedUp() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSignedUp');
    },
    get_h5: function SpottedScript_Pages_Spotters_View$get_h5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H5');
    },
    get_panelSignedUpEventLink: function SpottedScript_Pages_Spotters_View$get_panelSignedUpEventLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSignedUpEventLink');
    },
    get_panelAlreadySignedUp: function SpottedScript_Pages_Spotters_View$get_panelAlreadySignedUp() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAlreadySignedUp');
    },
    get_h6: function SpottedScript_Pages_Spotters_View$get_h6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H6');
    },
    get_panelAlreadySignedUpEventLabel: function SpottedScript_Pages_Spotters_View$get_panelAlreadySignedUpEventLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAlreadySignedUpEventLabel');
    },
    get_panelAlreadySignedUpEventLink: function SpottedScript_Pages_Spotters_View$get_panelAlreadySignedUpEventLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAlreadySignedUpEventLink');
    },
    get_panelPastEventConfirm: function SpottedScript_Pages_Spotters_View$get_panelPastEventConfirm() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPastEventConfirm');
    },
    get_h7: function SpottedScript_Pages_Spotters_View$get_h7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H7');
    },
    get_panelPastEventConfirmLabel: function SpottedScript_Pages_Spotters_View$get_panelPastEventConfirmLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPastEventConfirmLabel');
    },
    get_button8: function SpottedScript_Pages_Spotters_View$get_button8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button8');
    },
    get_button9: function SpottedScript_Pages_Spotters_View$get_button9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button9');
    },
    get_genericContainerPage: function SpottedScript_Pages_Spotters_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Spotters.View.registerClass('SpottedScript.Pages.Spotters.View', SpottedScript.DsiUserControl.View);
