Type.registerNamespace('SpottedScript.Pages.Spotters');
SpottedScript.Pages.Spotters.View=function(clientId){SpottedScript.Pages.Spotters.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Spotters.View.prototype={clientId:null,get_panelIntro1:function(){return document.getElementById(this.clientId+'_PanelIntro1');},get_uiEventPicker:function(){return eval(this.clientId+'_uiEventPickerController');},get_h19:function(){return document.getElementById(this.clientId+'_H19');},get_button1:function(){return document.getElementById(this.clientId+'_Button1');},get_panelNoPhoto:function(){return document.getElementById(this.clientId+'_PanelNoPhoto');},get_h2:function(){return document.getElementById(this.clientId+'_H2');},get_panelSignUpForm:function(){return document.getElementById(this.clientId+'_PanelSignUpForm');},get_h3:function(){return document.getElementById(this.clientId+'_H3');},get_firstName:function(){return document.getElementById(this.clientId+'_FirstName');},get_requiredFieldValidator1:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator1');},get_lastName:function(){return document.getElementById(this.clientId+'_LastName');},get_requiredFieldValidator2:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator2');},get_dialingCodeDropDown:function(){return document.getElementById(this.clientId+'_DialingCodeDropDown');},get_dialingCodeOtherSpan:function(){return document.getElementById(this.clientId+'_DialingCodeOtherSpan');},get_dialingCodeOther:function(){return document.getElementById(this.clientId+'_DialingCodeOther');},get_mobileNumber:function(){return document.getElementById(this.clientId+'_MobileNumber');},get_requiredfieldvalidator3:function(){return document.getElementById(this.clientId+'_Requiredfieldvalidator3');},get_dateOfBirthDay:function(){return document.getElementById(this.clientId+'_DateOfBirthDay');},get_dateOfBirthMonth:function(){return document.getElementById(this.clientId+'_DateOfBirthMonth');},get_dateOfBirthYear:function(){return document.getElementById(this.clientId+'_DateOfBirthYear');},get_customValidator1:function(){return document.getElementById(this.clientId+'_CustomValidator1');},get_customValidator2:function(){return document.getElementById(this.clientId+'_CustomValidator2');},get_addressStreet:function(){return document.getElementById(this.clientId+'_AddressStreet');},get_addressArea:function(){return document.getElementById(this.clientId+'_AddressArea');},get_addressTown:function(){return document.getElementById(this.clientId+'_AddressTown');},get_addressCounty:function(){return document.getElementById(this.clientId+'_AddressCounty');},get_addressPostcode:function(){return document.getElementById(this.clientId+'_AddressPostcode');},get_addressCountry:function(){return document.getElementById(this.clientId+'_AddressCountry');},get_requiredFieldValidator4:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator4');},get_requiredFieldValidator5:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator5');},get_requiredFieldValidator6:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator6');},get_requiredFieldValidator7:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator7');},get_customValidator3:function(){return document.getElementById(this.clientId+'_CustomValidator3');},get_photoUsageUse:function(){return document.getElementById(this.clientId+'_PhotoUsageUse');},get_photoUsageContact:function(){return document.getElementById(this.clientId+'_PhotoUsageContact');},get_photoUsageDoNotUse:function(){return document.getElementById(this.clientId+'_PhotoUsageDoNotUse');},get_saveSpotterButton:function(){return document.getElementById(this.clientId+'_SaveSpotterButton');},get_spotterSavedLabel:function(){return document.getElementById(this.clientId+'_SpotterSavedLabel');},get_panelChecklist:function(){return document.getElementById(this.clientId+'_PanelChecklist');},get_h4:function(){return document.getElementById(this.clientId+'_H4');},get_customValidator4:function(){return document.getElementById(this.clientId+'_CustomValidator4');},get_checklist:function(){return document.getElementById(this.clientId+'_Checklist');},get_customValidator5:function(){return document.getElementById(this.clientId+'_CustomValidator5');},get_button4:function(){return document.getElementById(this.clientId+'_Button4');},get_panelEnabled:function(){return document.getElementById(this.clientId+'_PanelEnabled');},get_guestlistPanel:function(){return document.getElementById(this.clientId+'_GuestlistPanel');},get_h11:function(){return document.getElementById(this.clientId+'_H11');},get_h1:function(){return document.getElementById(this.clientId+'_H1');},get_requestCardsPanel:function(){return document.getElementById(this.clientId+'_RequestCardsPanel');},get_h15:function(){return document.getElementById(this.clientId+'_H15');},get_button7:function(){return document.getElementById(this.clientId+'_Button7');},get_requestCardsError2:function(){return document.getElementById(this.clientId+'_RequestCardsError2');},get_requestCardsError3:function(){return document.getElementById(this.clientId+'_RequestCardsError3');},get_requestCardsError4:function(){return document.getElementById(this.clientId+'_RequestCardsError4');},get_requestCardsStatusLabel:function(){return document.getElementById(this.clientId+'_RequestCardsStatusLabel');},get_panelStatusCardsInPost:function(){return document.getElementById(this.clientId+'_PanelStatusCardsInPost');},get_linkButton1:function(){return document.getElementById(this.clientId+'_LinkButton1');},get_panelStatusNoCardsInPost:function(){return document.getElementById(this.clientId+'_PanelStatusNoCardsInPost');},get_h18:function(){return document.getElementById(this.clientId+'_H18');},get_spotterAddressCountryName:function(){return document.getElementById(this.clientId+'_SpotterAddressCountryName');},get_button5:function(){return document.getElementById(this.clientId+'_Button5');},get_optionsPanel:function(){return document.getElementById(this.clientId+'_OptionsPanel');},get_button6:function(){return document.getElementById(this.clientId+'_Button6');},get_eventsPanel:function(){return document.getElementById(this.clientId+'_EventsPanel');},get_h13:function(){return document.getElementById(this.clientId+'_H13');},get_eventsDataGrid:function(){return document.getElementById(this.clientId+'_EventsDataGrid');},get_panelSignedUp:function(){return document.getElementById(this.clientId+'_PanelSignedUp');},get_h5:function(){return document.getElementById(this.clientId+'_H5');},get_panelSignedUpEventLink:function(){return document.getElementById(this.clientId+'_PanelSignedUpEventLink');},get_panelAlreadySignedUp:function(){return document.getElementById(this.clientId+'_PanelAlreadySignedUp');},get_h6:function(){return document.getElementById(this.clientId+'_H6');},get_panelAlreadySignedUpEventLabel:function(){return document.getElementById(this.clientId+'_PanelAlreadySignedUpEventLabel');},get_panelAlreadySignedUpEventLink:function(){return document.getElementById(this.clientId+'_PanelAlreadySignedUpEventLink');},get_panelPastEventConfirm:function(){return document.getElementById(this.clientId+'_PanelPastEventConfirm');},get_h7:function(){return document.getElementById(this.clientId+'_H7');},get_panelPastEventConfirmLabel:function(){return document.getElementById(this.clientId+'_PanelPastEventConfirmLabel');},get_button8:function(){return document.getElementById(this.clientId+'_Button8');},get_button9:function(){return document.getElementById(this.clientId+'_Button9');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Spotters.View.registerClass('SpottedScript.Pages.Spotters.View',SpottedScript.DsiUserControl.View);
