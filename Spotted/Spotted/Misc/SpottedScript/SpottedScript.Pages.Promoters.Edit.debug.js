Type.registerNamespace('SpottedScript.Pages.Promoters.Edit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Edit.View
SpottedScript.Pages.Promoters.Edit.View = function SpottedScript_Pages_Promoters_Edit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Edit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Edit.View.prototype = {
    clientId: null,
    get_panelSignUpForm: function SpottedScript_Pages_Promoters_Edit_View$get_panelSignUpForm() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSignUpForm');
    },
    get_panelSignUpFormHeading: function SpottedScript_Pages_Promoters_Edit_View$get_panelSignUpFormHeading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSignUpFormHeading');
    },
    get_duplicateAccountP: function SpottedScript_Pages_Promoters_Edit_View$get_duplicateAccountP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DuplicateAccountP');
    },
    get_validationsummary1: function SpottedScript_Pages_Promoters_Edit_View$get_validationsummary1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary1');
    },
    get_contactName: function SpottedScript_Pages_Promoters_Edit_View$get_contactName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ContactName');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Promoters_Edit_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_name: function SpottedScript_Pages_Promoters_Edit_View$get_name() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Name');
    },
    get_requiredfieldvalidator2: function SpottedScript_Pages_Promoters_Edit_View$get_requiredfieldvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator2');
    },
    get_phoneNumber: function SpottedScript_Pages_Promoters_Edit_View$get_phoneNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhoneNumber');
    },
    get_requiredfieldvalidator5: function SpottedScript_Pages_Promoters_Edit_View$get_requiredfieldvalidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator5');
    },
    get_addressStreet: function SpottedScript_Pages_Promoters_Edit_View$get_addressStreet() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressStreet');
    },
    get_addressArea: function SpottedScript_Pages_Promoters_Edit_View$get_addressArea() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressArea');
    },
    get_addressTown: function SpottedScript_Pages_Promoters_Edit_View$get_addressTown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressTown');
    },
    get_addressCounty: function SpottedScript_Pages_Promoters_Edit_View$get_addressCounty() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressCounty');
    },
    get_addressPostcode: function SpottedScript_Pages_Promoters_Edit_View$get_addressPostcode() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressPostcode');
    },
    get_addressCountry: function SpottedScript_Pages_Promoters_Edit_View$get_addressCountry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddressCountry');
    },
    get_requiredfieldvalidator6: function SpottedScript_Pages_Promoters_Edit_View$get_requiredfieldvalidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator6');
    },
    get_requiredfieldvalidator7: function SpottedScript_Pages_Promoters_Edit_View$get_requiredfieldvalidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator7');
    },
    get_requiredfieldvalidator8: function SpottedScript_Pages_Promoters_Edit_View$get_requiredfieldvalidator8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator8');
    },
    get_requiredfieldvalidator9: function SpottedScript_Pages_Promoters_Edit_View$get_requiredfieldvalidator9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator9');
    },
    get_customvalidator1: function SpottedScript_Pages_Promoters_Edit_View$get_customvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator1');
    },
    get_vatStatusDropDownList: function SpottedScript_Pages_Promoters_Edit_View$get_vatStatusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VatStatusDropDownList');
    },
    get_vatNumberTextBox: function SpottedScript_Pages_Promoters_Edit_View$get_vatNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VatNumberTextBox');
    },
    get_vatCountryDropDownList: function SpottedScript_Pages_Promoters_Edit_View$get_vatCountryDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VatCountryDropDownList');
    },
    get_vatStatusRequiredFieldValidator: function SpottedScript_Pages_Promoters_Edit_View$get_vatStatusRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VatStatusRequiredFieldValidator');
    },
    get_vatStatusCustomValidator: function SpottedScript_Pages_Promoters_Edit_View$get_vatStatusCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VatStatusCustomValidator');
    },
    get_extraContactDetailsPanel: function SpottedScript_Pages_Promoters_Edit_View$get_extraContactDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExtraContactDetailsPanel');
    },
    get_jobTitle: function SpottedScript_Pages_Promoters_Edit_View$get_jobTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_JobTitle');
    },
    get_personalTitle: function SpottedScript_Pages_Promoters_Edit_View$get_personalTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PersonalTitle');
    },
    get_phoneNumber2: function SpottedScript_Pages_Promoters_Edit_View$get_phoneNumber2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhoneNumber2');
    },
    get_accountsName: function SpottedScript_Pages_Promoters_Edit_View$get_accountsName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccountsName');
    },
    get_accountsEmail: function SpottedScript_Pages_Promoters_Edit_View$get_accountsEmail() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccountsEmail');
    },
    get_accountsEmailCustomValidator: function SpottedScript_Pages_Promoters_Edit_View$get_accountsEmailCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccountsEmailCustomValidator');
    },
    get_accountsPhone: function SpottedScript_Pages_Promoters_Edit_View$get_accountsPhone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccountsPhone');
    },
    get_webAddress: function SpottedScript_Pages_Promoters_Edit_View$get_webAddress() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WebAddress');
    },
    get_sector: function SpottedScript_Pages_Promoters_Edit_View$get_sector() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Sector');
    },
    get_uiAgency: function SpottedScript_Pages_Promoters_Edit_View$get_uiAgency() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAgency');
    },
    get_salesCampaignDropDown: function SpottedScript_Pages_Promoters_Edit_View$get_salesCampaignDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesCampaignDropDown');
    },
    get_bankAccountDetailsPanel: function SpottedScript_Pages_Promoters_Edit_View$get_bankAccountDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountDetailsPanel');
    },
    get_bankNameTextBox: function SpottedScript_Pages_Promoters_Edit_View$get_bankNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankNameTextBox');
    },
    get_bankAccountNameTextBox: function SpottedScript_Pages_Promoters_Edit_View$get_bankAccountNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNameTextBox');
    },
    get_bankAccountNumberTextBox: function SpottedScript_Pages_Promoters_Edit_View$get_bankAccountNumberTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountNumberTextBox');
    },
    get_bankAccountSortCodeTextBox: function SpottedScript_Pages_Promoters_Edit_View$get_bankAccountSortCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankAccountSortCodeTextBox');
    },
    get_accountTypeRadioEvents: function SpottedScript_Pages_Promoters_Edit_View$get_accountTypeRadioEvents() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccountTypeRadioEvents');
    },
    get_accountTypeRadioNoEvents: function SpottedScript_Pages_Promoters_Edit_View$get_accountTypeRadioNoEvents() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccountTypeRadioNoEvents');
    },
    get_brandsTr: function SpottedScript_Pages_Promoters_Edit_View$get_brandsTr() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandsTr');
    },
    get_uiBrandMultiSelector: function SpottedScript_Pages_Promoters_Edit_View$get_uiBrandMultiSelector() {
        /// <value type="ScriptSharpLibrary.MultiSelectorBehaviour"></value>
        return eval(this.clientId + '_uiBrandMultiSelectorBehaviour');
    },
    get_customfieldvalidator3: function SpottedScript_Pages_Promoters_Edit_View$get_customfieldvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customfieldvalidator3');
    },
    get_venuesRadioYes: function SpottedScript_Pages_Promoters_Edit_View$get_venuesRadioYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesRadioYes');
    },
    get_venuesRadioNo: function SpottedScript_Pages_Promoters_Edit_View$get_venuesRadioNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesRadioNo');
    },
    get_venuesTr: function SpottedScript_Pages_Promoters_Edit_View$get_venuesTr() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesTr');
    },
    get_uiVenuesMultiSelector: function SpottedScript_Pages_Promoters_Edit_View$get_uiVenuesMultiSelector() {
        /// <value type="ScriptSharpLibrary.MultiSelectorBehaviour"></value>
        return eval(this.clientId + '_uiVenuesMultiSelectorBehaviour');
    },
    get_customValidator2: function SpottedScript_Pages_Promoters_Edit_View$get_customValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator2');
    },
    get_noAccessP: function SpottedScript_Pages_Promoters_Edit_View$get_noAccessP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoAccessP');
    },
    get_primaryUsrSpan: function SpottedScript_Pages_Promoters_Edit_View$get_primaryUsrSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrimaryUsrSpan');
    },
    get_accessP: function SpottedScript_Pages_Promoters_Edit_View$get_accessP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccessP');
    },
    get_singleAccountUser: function SpottedScript_Pages_Promoters_Edit_View$get_singleAccountUser() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SingleAccountUser');
    },
    get_accessJustMeRadio: function SpottedScript_Pages_Promoters_Edit_View$get_accessJustMeRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccessJustMeRadio');
    },
    get_multiAccountUsers: function SpottedScript_Pages_Promoters_Edit_View$get_multiAccountUsers() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MultiAccountUsers');
    },
    get_accessMultiRadio: function SpottedScript_Pages_Promoters_Edit_View$get_accessMultiRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccessMultiRadio');
    },
    get_noAccountUsers: function SpottedScript_Pages_Promoters_Edit_View$get_noAccountUsers() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoAccountUsers');
    },
    get_accessNoAccountUsersRadio: function SpottedScript_Pages_Promoters_Edit_View$get_accessNoAccountUsersRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AccessNoAccountUsersRadio');
    },
    get_multiAccess: function SpottedScript_Pages_Promoters_Edit_View$get_multiAccess() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MultiAccess');
    },
    get_uiAccessUsersMultiSelector: function SpottedScript_Pages_Promoters_Edit_View$get_uiAccessUsersMultiSelector() {
        /// <value type="ScriptSharpLibrary.MultiSelectorBehaviour"></value>
        return eval(this.clientId + '_uiAccessUsersMultiSelectorBehaviour');
    },
    get_uiPrimaryUserDropDown: function SpottedScript_Pages_Promoters_Edit_View$get_uiPrimaryUserDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPrimaryUserDropDown');
    },
    get_termsPanel: function SpottedScript_Pages_Promoters_Edit_View$get_termsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TermsPanel');
    },
    get_h11: function SpottedScript_Pages_Promoters_Edit_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_termsCheckbox: function SpottedScript_Pages_Promoters_Edit_View$get_termsCheckbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TermsCheckbox');
    },
    get_customvalidator7: function SpottedScript_Pages_Promoters_Edit_View$get_customvalidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator7');
    },
    get_h1dsf1: function SpottedScript_Pages_Promoters_Edit_View$get_h1dsf1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1dsf1');
    },
    get_savePromoterButton: function SpottedScript_Pages_Promoters_Edit_View$get_savePromoterButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SavePromoterButton');
    },
    get_promoterSavedLabel: function SpottedScript_Pages_Promoters_Edit_View$get_promoterSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterSavedLabel');
    },
    get_validationsummary2: function SpottedScript_Pages_Promoters_Edit_View$get_validationsummary2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary2');
    },
    get_panelBrandVenueError: function SpottedScript_Pages_Promoters_Edit_View$get_panelBrandVenueError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelBrandVenueError');
    },
    get_h12: function SpottedScript_Pages_Promoters_Edit_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_brandErrorPanel: function SpottedScript_Pages_Promoters_Edit_View$get_brandErrorPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandErrorPanel');
    },
    get_brandErrorLabel: function SpottedScript_Pages_Promoters_Edit_View$get_brandErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandErrorLabel');
    },
    get_venueErrorPanel: function SpottedScript_Pages_Promoters_Edit_View$get_venueErrorPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueErrorPanel');
    },
    get_venueErrorLabel: function SpottedScript_Pages_Promoters_Edit_View$get_venueErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueErrorLabel');
    },
    get_panelAlreadyPromoter: function SpottedScript_Pages_Promoters_Edit_View$get_panelAlreadyPromoter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAlreadyPromoter');
    },
    get_h12cxvvcx: function SpottedScript_Pages_Promoters_Edit_View$get_h12cxvvcx() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12cxvvcx');
    },
    get_panelPic: function SpottedScript_Pages_Promoters_Edit_View$get_panelPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPic');
    },
    get_h15: function SpottedScript_Pages_Promoters_Edit_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_pic: function SpottedScript_Pages_Promoters_Edit_View$get_pic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Pic');
    },
    get_panelAddDone: function SpottedScript_Pages_Promoters_Edit_View$get_panelAddDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAddDone');
    },
    get_h13: function SpottedScript_Pages_Promoters_Edit_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_panelEditDone: function SpottedScript_Pages_Promoters_Edit_View$get_panelEditDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEditDone');
    },
    get_promoterintro1: function SpottedScript_Pages_Promoters_Edit_View$get_promoterintro1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Promoterintro1');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Edit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Edit.View.registerClass('SpottedScript.Pages.Promoters.Edit.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
