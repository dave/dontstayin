Type.registerNamespace('SpottedScript.Pages.Events.Edit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Edit.View
SpottedScript.Pages.Events.Edit.View = function SpottedScript_Pages_Events_Edit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Edit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Edit.View.prototype = {
    clientId: null,
    get_spotterRequestPanel: function SpottedScript_Pages_Events_Edit_View$get_spotterRequestPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestPanel');
    },
    get_spotterRequestName: function SpottedScript_Pages_Events_Edit_View$get_spotterRequestName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestName');
    },
    get_spotterRequestNumber: function SpottedScript_Pages_Events_Edit_View$get_spotterRequestNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestNumber');
    },
    get_spotterRequestYes: function SpottedScript_Pages_Events_Edit_View$get_spotterRequestYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestYes');
    },
    get_spotterRequestNo: function SpottedScript_Pages_Events_Edit_View$get_spotterRequestNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestNo');
    },
    get_headerH1: function SpottedScript_Pages_Events_Edit_View$get_headerH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderH1');
    },
    get_panelSelectionError: function SpottedScript_Pages_Events_Edit_View$get_panelSelectionError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSelectionError');
    },
    get_panelSelectionErrorPicker: function SpottedScript_Pages_Events_Edit_View$get_panelSelectionErrorPicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_PanelSelectionErrorPickerController');
    },
    get_button1: function SpottedScript_Pages_Events_Edit_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Events_Edit_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_panelEditError: function SpottedScript_Pages_Events_Edit_View$get_panelEditError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEditError');
    },
    get_panelDate: function SpottedScript_Pages_Events_Edit_View$get_panelDate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDate');
    },
    get_panelDateLine1P: function SpottedScript_Pages_Events_Edit_View$get_panelDateLine1P() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDateLine1P');
    },
    get_panelDateCalendarP: function SpottedScript_Pages_Events_Edit_View$get_panelDateCalendarP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDateCalendarP');
    },
    get_panelDateCal: function SpottedScript_Pages_Events_Edit_View$get_panelDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_PanelDateCalController');
    },
    get_panelDateFutureConfirmPanel: function SpottedScript_Pages_Events_Edit_View$get_panelDateFutureConfirmPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDateFutureConfirmPanel');
    },
    get_panelDateFutureConfirmCheckBox: function SpottedScript_Pages_Events_Edit_View$get_panelDateFutureConfirmCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDateFutureConfirmCheckBox');
    },
    get_panelDateMessage: function SpottedScript_Pages_Events_Edit_View$get_panelDateMessage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDateMessage');
    },
    get_panelDateNonSelectedError: function SpottedScript_Pages_Events_Edit_View$get_panelDateNonSelectedError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDateNonSelectedError');
    },
    get_panelDateNextButtonP: function SpottedScript_Pages_Events_Edit_View$get_panelDateNextButtonP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDateNextButtonP');
    },
    get_button2: function SpottedScript_Pages_Events_Edit_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_panelDateSaveButtonP: function SpottedScript_Pages_Events_Edit_View$get_panelDateSaveButtonP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDateSaveButtonP');
    },
    get_button4: function SpottedScript_Pages_Events_Edit_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_panelConflict: function SpottedScript_Pages_Events_Edit_View$get_panelConflict() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelConflict');
    },
    get_panelConflictDataGrid: function SpottedScript_Pages_Events_Edit_View$get_panelConflictDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelConflictDataGrid');
    },
    get_panelConflictCheckbox: function SpottedScript_Pages_Events_Edit_View$get_panelConflictCheckbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelConflictCheckbox');
    },
    get_button3: function SpottedScript_Pages_Events_Edit_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_button5: function SpottedScript_Pages_Events_Edit_View$get_button5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button5');
    },
    get_customValidator1: function SpottedScript_Pages_Events_Edit_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_panelDetails: function SpottedScript_Pages_Events_Edit_View$get_panelDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetails');
    },
    get_dateLockedDiv: function SpottedScript_Pages_Events_Edit_View$get_dateLockedDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateLockedDiv');
    },
    get_detailsDateLockedLabel: function SpottedScript_Pages_Events_Edit_View$get_detailsDateLockedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DetailsDateLockedLabel');
    },
    get_detailsDateLockedLabel2: function SpottedScript_Pages_Events_Edit_View$get_detailsDateLockedLabel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DetailsDateLockedLabel2');
    },
    get_panelDetailsVenueDiv: function SpottedScript_Pages_Events_Edit_View$get_panelDetailsVenueDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsVenueDiv');
    },
    get_panelDetailsVenuePicker: function SpottedScript_Pages_Events_Edit_View$get_panelDetailsVenuePicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_PanelDetailsVenuePickerController');
    },
    get_panelDetailsVenueWarningP: function SpottedScript_Pages_Events_Edit_View$get_panelDetailsVenueWarningP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsVenueWarningP');
    },
    get_panelDetailsVenueLockedDiv: function SpottedScript_Pages_Events_Edit_View$get_panelDetailsVenueLockedDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsVenueLockedDiv');
    },
    get_detailsVenueLockedLabel: function SpottedScript_Pages_Events_Edit_View$get_detailsVenueLockedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DetailsVenueLockedLabel');
    },
    get_detailsVenueLockedLabel2: function SpottedScript_Pages_Events_Edit_View$get_detailsVenueLockedLabel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DetailsVenueLockedLabel2');
    },
    get_eventName: function SpottedScript_Pages_Events_Edit_View$get_eventName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventName');
    },
    get_startTimeEvening: function SpottedScript_Pages_Events_Edit_View$get_startTimeEvening() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StartTimeEvening');
    },
    get_startTimeMorning: function SpottedScript_Pages_Events_Edit_View$get_startTimeMorning() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StartTimeMorning');
    },
    get_startTimeDaytime: function SpottedScript_Pages_Events_Edit_View$get_startTimeDaytime() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StartTimeDaytime');
    },
    get_eventShortDetailsHtml: function SpottedScript_Pages_Events_Edit_View$get_eventShortDetailsHtml() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventShortDetailsHtml');
    },
    get_eventLongDetailsHtml: function SpottedScript_Pages_Events_Edit_View$get_eventLongDetailsHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_EventLongDetailsHtmlController');
    },
    get_eventCapacity: function SpottedScript_Pages_Events_Edit_View$get_eventCapacity() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventCapacity');
    },
    get_uiNoBrandsRadioButton: function SpottedScript_Pages_Events_Edit_View$get_uiNoBrandsRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNoBrandsRadioButton');
    },
    get_uiHasBrandsRadioButton: function SpottedScript_Pages_Events_Edit_View$get_uiHasBrandsRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiHasBrandsRadioButton');
    },
    get_uiBrandsMultiSelector: function SpottedScript_Pages_Events_Edit_View$get_uiBrandsMultiSelector() {
        /// <value type="ScriptSharpLibrary.MultiSelectorBehaviour"></value>
        return eval(this.clientId + '_uiBrandsMultiSelectorBehaviour');
    },
    get_panelDetailsSaveP: function SpottedScript_Pages_Events_Edit_View$get_panelDetailsSaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsSaveP');
    },
    get_button8: function SpottedScript_Pages_Events_Edit_View$get_button8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button8');
    },
    get_button6: function SpottedScript_Pages_Events_Edit_View$get_button6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button6');
    },
    get_button7: function SpottedScript_Pages_Events_Edit_View$get_button7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button7');
    },
    get_requiredFieldValidator2: function SpottedScript_Pages_Events_Edit_View$get_requiredFieldValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator2');
    },
    get_regularExpressionValidator2: function SpottedScript_Pages_Events_Edit_View$get_regularExpressionValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator2');
    },
    get_customValidator11: function SpottedScript_Pages_Events_Edit_View$get_customValidator11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator11');
    },
    get_customValidator12: function SpottedScript_Pages_Events_Edit_View$get_customValidator12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator12');
    },
    get_requiredFieldValidator3: function SpottedScript_Pages_Events_Edit_View$get_requiredFieldValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator3');
    },
    get_regularExpressionValidator1: function SpottedScript_Pages_Events_Edit_View$get_regularExpressionValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator1');
    },
    get_customValidator2: function SpottedScript_Pages_Events_Edit_View$get_customValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator2');
    },
    get_customValidator4: function SpottedScript_Pages_Events_Edit_View$get_customValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator4');
    },
    get_requiredFieldValidator4: function SpottedScript_Pages_Events_Edit_View$get_requiredFieldValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator4');
    },
    get_compareValidator1: function SpottedScript_Pages_Events_Edit_View$get_compareValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator1');
    },
    get_brandCustomValidator: function SpottedScript_Pages_Events_Edit_View$get_brandCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandCustomValidator');
    },
    get_panelMusicTypes: function SpottedScript_Pages_Events_Edit_View$get_panelMusicTypes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMusicTypes');
    },
    get_musicTypesUc: function SpottedScript_Pages_Events_Edit_View$get_musicTypesUc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTypesUc');
    },
    get_button12: function SpottedScript_Pages_Events_Edit_View$get_button12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button12');
    },
    get_button13: function SpottedScript_Pages_Events_Edit_View$get_button13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button13');
    },
    get_panelMusicTypesSaveP: function SpottedScript_Pages_Events_Edit_View$get_panelMusicTypesSaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMusicTypesSaveP');
    },
    get_button14: function SpottedScript_Pages_Events_Edit_View$get_button14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button14');
    },
    get_customvalidator3: function SpottedScript_Pages_Events_Edit_View$get_customvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator3');
    },
    get_panelPic: function SpottedScript_Pages_Events_Edit_View$get_panelPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPic');
    },
    get_picUploadDefaultPanel: function SpottedScript_Pages_Events_Edit_View$get_picUploadDefaultPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUploadDefaultPanel');
    },
    get_picUploadDefaultDataList: function SpottedScript_Pages_Events_Edit_View$get_picUploadDefaultDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUploadDefaultDataList');
    },
    get_picUploadPanel: function SpottedScript_Pages_Events_Edit_View$get_picUploadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUploadPanel');
    },
    get_picUcEvent: function SpottedScript_Pages_Events_Edit_View$get_picUcEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUcEvent');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Edit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Edit.View.registerClass('SpottedScript.Pages.Events.Edit.View', SpottedScript.DsiUserControl.View);
