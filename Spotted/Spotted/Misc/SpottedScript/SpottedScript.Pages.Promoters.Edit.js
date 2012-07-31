Type.registerNamespace('SpottedScript.Pages.Promoters.Edit');
SpottedScript.Pages.Promoters.Edit.View=function(clientId){SpottedScript.Pages.Promoters.Edit.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.Edit.View.prototype={clientId:null,get_panelSignUpForm:function(){return document.getElementById(this.clientId+'_PanelSignUpForm');},get_panelSignUpFormHeading:function(){return document.getElementById(this.clientId+'_PanelSignUpFormHeading');},get_duplicateAccountP:function(){return document.getElementById(this.clientId+'_DuplicateAccountP');},get_validationsummary1:function(){return document.getElementById(this.clientId+'_Validationsummary1');},get_contactName:function(){return document.getElementById(this.clientId+'_ContactName');},get_requiredFieldValidator1:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator1');},get_name:function(){return document.getElementById(this.clientId+'_Name');},get_requiredfieldvalidator2:function(){return document.getElementById(this.clientId+'_Requiredfieldvalidator2');},get_phoneNumber:function(){return document.getElementById(this.clientId+'_PhoneNumber');},get_requiredfieldvalidator5:function(){return document.getElementById(this.clientId+'_Requiredfieldvalidator5');},get_addressStreet:function(){return document.getElementById(this.clientId+'_AddressStreet');},get_addressArea:function(){return document.getElementById(this.clientId+'_AddressArea');},get_addressTown:function(){return document.getElementById(this.clientId+'_AddressTown');},get_addressCounty:function(){return document.getElementById(this.clientId+'_AddressCounty');},get_addressPostcode:function(){return document.getElementById(this.clientId+'_AddressPostcode');},get_addressCountry:function(){return document.getElementById(this.clientId+'_AddressCountry');},get_requiredfieldvalidator6:function(){return document.getElementById(this.clientId+'_Requiredfieldvalidator6');},get_requiredfieldvalidator7:function(){return document.getElementById(this.clientId+'_Requiredfieldvalidator7');},get_requiredfieldvalidator8:function(){return document.getElementById(this.clientId+'_Requiredfieldvalidator8');},get_requiredfieldvalidator9:function(){return document.getElementById(this.clientId+'_Requiredfieldvalidator9');},get_customvalidator1:function(){return document.getElementById(this.clientId+'_Customvalidator1');},get_vatStatusDropDownList:function(){return document.getElementById(this.clientId+'_VatStatusDropDownList');},get_vatNumberTextBox:function(){return document.getElementById(this.clientId+'_VatNumberTextBox');},get_vatCountryDropDownList:function(){return document.getElementById(this.clientId+'_VatCountryDropDownList');},get_vatStatusRequiredFieldValidator:function(){return document.getElementById(this.clientId+'_VatStatusRequiredFieldValidator');},get_vatStatusCustomValidator:function(){return document.getElementById(this.clientId+'_VatStatusCustomValidator');},get_extraContactDetailsPanel:function(){return document.getElementById(this.clientId+'_ExtraContactDetailsPanel');},get_jobTitle:function(){return document.getElementById(this.clientId+'_JobTitle');},get_personalTitle:function(){return document.getElementById(this.clientId+'_PersonalTitle');},get_phoneNumber2:function(){return document.getElementById(this.clientId+'_PhoneNumber2');},get_accountsName:function(){return document.getElementById(this.clientId+'_AccountsName');},get_accountsEmail:function(){return document.getElementById(this.clientId+'_AccountsEmail');},get_accountsEmailCustomValidator:function(){return document.getElementById(this.clientId+'_AccountsEmailCustomValidator');},get_accountsPhone:function(){return document.getElementById(this.clientId+'_AccountsPhone');},get_webAddress:function(){return document.getElementById(this.clientId+'_WebAddress');},get_sector:function(){return document.getElementById(this.clientId+'_Sector');},get_uiAgency:function(){return document.getElementById(this.clientId+'_uiAgency');},get_salesCampaignDropDown:function(){return document.getElementById(this.clientId+'_SalesCampaignDropDown');},get_bankAccountDetailsPanel:function(){return document.getElementById(this.clientId+'_BankAccountDetailsPanel');},get_bankNameTextBox:function(){return document.getElementById(this.clientId+'_BankNameTextBox');},get_bankAccountNameTextBox:function(){return document.getElementById(this.clientId+'_BankAccountNameTextBox');},get_bankAccountNumberTextBox:function(){return document.getElementById(this.clientId+'_BankAccountNumberTextBox');},get_bankAccountSortCodeTextBox:function(){return document.getElementById(this.clientId+'_BankAccountSortCodeTextBox');},get_accountTypeRadioEvents:function(){return document.getElementById(this.clientId+'_AccountTypeRadioEvents');},get_accountTypeRadioNoEvents:function(){return document.getElementById(this.clientId+'_AccountTypeRadioNoEvents');},get_brandsTr:function(){return document.getElementById(this.clientId+'_BrandsTr');},get_uiBrandMultiSelector:function(){return eval(this.clientId+'_uiBrandMultiSelectorBehaviour');},get_customfieldvalidator3:function(){return document.getElementById(this.clientId+'_Customfieldvalidator3');},get_venuesRadioYes:function(){return document.getElementById(this.clientId+'_VenuesRadioYes');},get_venuesRadioNo:function(){return document.getElementById(this.clientId+'_VenuesRadioNo');},get_venuesTr:function(){return document.getElementById(this.clientId+'_VenuesTr');},get_uiVenuesMultiSelector:function(){return eval(this.clientId+'_uiVenuesMultiSelectorBehaviour');},get_customValidator2:function(){return document.getElementById(this.clientId+'_CustomValidator2');},get_noAccessP:function(){return document.getElementById(this.clientId+'_NoAccessP');},get_primaryUsrSpan:function(){return document.getElementById(this.clientId+'_PrimaryUsrSpan');},get_accessP:function(){return document.getElementById(this.clientId+'_AccessP');},get_singleAccountUser:function(){return document.getElementById(this.clientId+'_SingleAccountUser');},get_accessJustMeRadio:function(){return document.getElementById(this.clientId+'_AccessJustMeRadio');},get_multiAccountUsers:function(){return document.getElementById(this.clientId+'_MultiAccountUsers');},get_accessMultiRadio:function(){return document.getElementById(this.clientId+'_AccessMultiRadio');},get_noAccountUsers:function(){return document.getElementById(this.clientId+'_NoAccountUsers');},get_accessNoAccountUsersRadio:function(){return document.getElementById(this.clientId+'_AccessNoAccountUsersRadio');},get_multiAccess:function(){return document.getElementById(this.clientId+'_MultiAccess');},get_uiAccessUsersMultiSelector:function(){return eval(this.clientId+'_uiAccessUsersMultiSelectorBehaviour');},get_uiPrimaryUserDropDown:function(){return document.getElementById(this.clientId+'_uiPrimaryUserDropDown');},get_termsPanel:function(){return document.getElementById(this.clientId+'_TermsPanel');},get_h11:function(){return document.getElementById(this.clientId+'_H11');},get_termsCheckbox:function(){return document.getElementById(this.clientId+'_TermsCheckbox');},get_customvalidator7:function(){return document.getElementById(this.clientId+'_Customvalidator7');},get_h1dsf1:function(){return document.getElementById(this.clientId+'_H1dsf1');},get_savePromoterButton:function(){return document.getElementById(this.clientId+'_SavePromoterButton');},get_promoterSavedLabel:function(){return document.getElementById(this.clientId+'_PromoterSavedLabel');},get_validationsummary2:function(){return document.getElementById(this.clientId+'_Validationsummary2');},get_panelBrandVenueError:function(){return document.getElementById(this.clientId+'_PanelBrandVenueError');},get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_brandErrorPanel:function(){return document.getElementById(this.clientId+'_BrandErrorPanel');},get_brandErrorLabel:function(){return document.getElementById(this.clientId+'_BrandErrorLabel');},get_venueErrorPanel:function(){return document.getElementById(this.clientId+'_VenueErrorPanel');},get_venueErrorLabel:function(){return document.getElementById(this.clientId+'_VenueErrorLabel');},get_panelAlreadyPromoter:function(){return document.getElementById(this.clientId+'_PanelAlreadyPromoter');},get_h12cxvvcx:function(){return document.getElementById(this.clientId+'_H12cxvvcx');},get_panelPic:function(){return document.getElementById(this.clientId+'_PanelPic');},get_h15:function(){return document.getElementById(this.clientId+'_H15');},get_pic:function(){return document.getElementById(this.clientId+'_Pic');},get_panelAddDone:function(){return document.getElementById(this.clientId+'_PanelAddDone');},get_h13:function(){return document.getElementById(this.clientId+'_H13');},get_panelEditDone:function(){return document.getElementById(this.clientId+'_PanelEditDone');},get_promoterintro1:function(){return document.getElementById(this.clientId+'_Promoterintro1');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.Edit.View.registerClass('SpottedScript.Pages.Promoters.Edit.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
