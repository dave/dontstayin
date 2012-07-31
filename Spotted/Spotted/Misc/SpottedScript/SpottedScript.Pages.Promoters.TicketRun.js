Type.registerNamespace('SpottedScript.Pages.Promoters.TicketRun');
SpottedScript.Pages.Promoters.TicketRun.View=function(clientId){SpottedScript.Pages.Promoters.TicketRun.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.TicketRun.View.prototype={clientId:null,get_promoterIntro:function(){return document.getElementById(this.clientId+'_PromoterIntro');},get_noEventsPanel:function(){return document.getElementById(this.clientId+'_NoEventsPanel');},get_hasEventsPanel:function(){return document.getElementById(this.clientId+'_HasEventsPanel');},get_errorMessageP:function(){return document.getElementById(this.clientId+'_ErrorMessageP');},get_addEditTicketRunPanel:function(){return document.getElementById(this.clientId+'_AddEditTicketRunPanel');},get_addEditTicketRunH1:function(){return document.getElementById(this.clientId+'_AddEditTicketRunH1');},get_ticketRunDefaultNoteLabel:function(){return document.getElementById(this.clientId+'_TicketRunDefaultNoteLabel');},get_ticketRunNote:function(){return document.getElementById(this.clientId+'_TicketRunNote');},get_ticketRunTable:function(){return document.getElementById(this.clientId+'_TicketRunTable');},get_eventLabel:function(){return document.getElementById(this.clientId+'_EventLabel');},get_nonEventSpecificDiv:function(){return document.getElementById(this.clientId+'_NonEventSpecificDiv');},get_eventDropDownList:function(){return document.getElementById(this.clientId+'_EventDropDownList');},get_ticketPriceTextBox:function(){return document.getElementById(this.clientId+'_TicketPriceTextBox');},get_priceAndBookingFeeLabel:function(){return document.getElementById(this.clientId+'_PriceAndBookingFeeLabel');},get_ticketPriceRequiredFieldValidator:function(){return document.getElementById(this.clientId+'_TicketPriceRequiredFieldValidator');},get_ticketBookingFeeRow:function(){return document.getElementById(this.clientId+'_TicketBookingFeeRow');},get_bookingFeeTextBox:function(){return document.getElementById(this.clientId+'_BookingFeeTextBox');},get_overrideBookingFeeCheckBox:function(){return document.getElementById(this.clientId+'_OverrideBookingFeeCheckBox');},get_bookingFeeLabel:function(){return document.getElementById(this.clientId+'_BookingFeeLabel');},get_advOptionsCheckRow:function(){return document.getElementById(this.clientId+'_AdvOptionsCheckRow');},get_advancedOptionsCheckBox:function(){return document.getElementById(this.clientId+'_AdvancedOptionsCheckBox');},get_ticketNameRow:function(){return document.getElementById(this.clientId+'_TicketNameRow');},get_ticketNameTextBox:function(){return document.getElementById(this.clientId+'_TicketNameTextBox');},get_ticketNameHelperLabel:function(){return document.getElementById(this.clientId+'_TicketNameHelperLabel');},get_ticketDescriptionRow:function(){return document.getElementById(this.clientId+'_TicketDescriptionRow');},get_ticketDescriptionTextBox:function(){return document.getElementById(this.clientId+'_TicketDescriptionTextBox');},get_ticketDescriptionHelperLabel:function(){return document.getElementById(this.clientId+'_TicketDescriptionHelperLabel');},get_ticketDescriptionLabel:function(){return document.getElementById(this.clientId+'_TicketDescriptionLabel');},get_brandsRow:function(){return document.getElementById(this.clientId+'_BrandsRow');},get_brandDropDownList:function(){return document.getElementById(this.clientId+'_BrandDropDownList');},get_brandLabel:function(){return document.getElementById(this.clientId+'_BrandLabel');},get_followsTicketRunRow:function(){return document.getElementById(this.clientId+'_FollowsTicketRunRow');},get_followsTicketRunDropDownList:function(){return document.getElementById(this.clientId+'_FollowsTicketRunDropDownList');},get_followsTicketRunHelperLabel:function(){return document.getElementById(this.clientId+'_FollowsTicketRunHelperLabel');},get_followsTicketRunLabel:function(){return document.getElementById(this.clientId+'_FollowsTicketRunLabel');},get_startDateRow:function(){return document.getElementById(this.clientId+'_StartDateRow');},get_startTicketRunTable:function(){return document.getElementById(this.clientId+'_StartTicketRunTable');},get_startTicketRunCal:function(){return eval(this.clientId+'_StartTicketRunCalController');},get_startTicketRunTime:function(){return document.getElementById(this.clientId+'_StartTicketRunTime');},get_startTicketRunLabel:function(){return document.getElementById(this.clientId+'_StartTicketRunLabel');},get_endDateRow:function(){return document.getElementById(this.clientId+'_EndDateRow');},get_endTicketRunTable:function(){return document.getElementById(this.clientId+'_EndTicketRunTable');},get_endTicketRunCal:function(){return eval(this.clientId+'_EndTicketRunCalController');},get_endTicketRunTime:function(){return document.getElementById(this.clientId+'_EndTicketRunTime');},get_endTicketRunLabel:function(){return document.getElementById(this.clientId+'_EndTicketRunLabel');},get_ticketsSoldRow:function(){return document.getElementById(this.clientId+'_TicketsSoldRow');},get_ticketsSoldLabel:function(){return document.getElementById(this.clientId+'_TicketsSoldLabel');},get_maxTicketsRow:function(){return document.getElementById(this.clientId+'_MaxTicketsRow');},get_maxTicketsTextBox:function(){return document.getElementById(this.clientId+'_MaxTicketsTextBox');},get_maxTicketsRangeValidator:function(){return document.getElementById(this.clientId+'_MaxTicketsRangeValidator');},get_contactEmailRow:function(){return document.getElementById(this.clientId+'_ContactEmailRow');},get_contactEmailTextBox:function(){return document.getElementById(this.clientId+'_ContactEmailTextBox');},get_contactEmailRequiredFieldValidator:function(){return document.getElementById(this.clientId+'_ContactEmailRequiredFieldValidator');},get_contactEmailRegularExpressionValidator:function(){return document.getElementById(this.clientId+'_ContactEmailRegularExpressionValidator');},get_orderInTheListRow:function(){return document.getElementById(this.clientId+'_OrderInTheListRow');},get_orderInTheListTextBox:function(){return document.getElementById(this.clientId+'_OrderInTheListTextBox');},get_orderInTheListHelperLabel:function(){return document.getElementById(this.clientId+'_OrderInTheListHelperLabel');},get_orderInTheListRangeValidator:function(){return document.getElementById(this.clientId+'_OrderInTheListRangeValidator');},get_updateButtonsRow:function(){return document.getElementById(this.clientId+'_UpdateButtonsRow');},get_goToConfirmationButton:function(){return document.getElementById(this.clientId+'_GoToConfirmationButton');},get_pauseResumeButton:function(){return document.getElementById(this.clientId+'_PauseResumeButton');},get_stopButton:function(){return document.getElementById(this.clientId+'_StopButton');},get_confirmationButtonsRow:function(){return document.getElementById(this.clientId+'_ConfirmationButtonsRow');},get_backButton:function(){return document.getElementById(this.clientId+'_BackButton');},get_advancedOptionsButton:function(){return document.getElementById(this.clientId+'_AdvancedOptionsButton');},get_saveTicketRunButton:function(){return document.getElementById(this.clientId+'_SaveTicketRunButton');},get_refundButtonRow:function(){return document.getElementById(this.clientId+'_RefundButtonRow');},get_refundButton:function(){return document.getElementById(this.clientId+'_RefundButton');},get_ticketPriceCustomValidator:function(){return document.getElementById(this.clientId+'_TicketPriceCustomValidator');},get_ticketDescriptionCustomValidator:function(){return document.getElementById(this.clientId+'_TicketDescriptionCustomValidator');},get_startDateCustomValidator:function(){return document.getElementById(this.clientId+'_StartDateCustomValidator');},get_endDateCustomValidator:function(){return document.getElementById(this.clientId+'_EndDateCustomValidator');},get_circularTicketRunDependencyCustomValidator:function(){return document.getElementById(this.clientId+'_CircularTicketRunDependencyCustomValidator');},get_maxTicketsCustomValidator:function(){return document.getElementById(this.clientId+'_MaxTicketsCustomValidator');},get_savingErrorCustomValidator:function(){return document.getElementById(this.clientId+'_SavingErrorCustomValidator');},get_errorMessageCustomValidator:function(){return document.getElementById(this.clientId+'_ErrorMessageCustomValidator');},get_ticketRunValidationSummary:function(){return document.getElementById(this.clientId+'_TicketRunValidationSummary');},get_javascriptLabel:function(){return document.getElementById(this.clientId+'_JavascriptLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.TicketRun.View.registerClass('SpottedScript.Pages.Promoters.TicketRun.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
