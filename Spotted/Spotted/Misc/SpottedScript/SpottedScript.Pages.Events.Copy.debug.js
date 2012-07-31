Type.registerNamespace('SpottedScript.Pages.Events.Copy');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Copy.View
SpottedScript.Pages.Events.Copy.View = function SpottedScript_Pages_Events_Copy_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Copy.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Copy.View.prototype = {
    clientId: null,
    get_panelStart: function SpottedScript_Pages_Events_Copy_View$get_panelStart() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelStart');
    },
    get_h11: function SpottedScript_Pages_Events_Copy_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_uiEventPicker: function SpottedScript_Pages_Events_Copy_View$get_uiEventPicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_uiEventPickerController');
    },
    get_newDateCalendar: function SpottedScript_Pages_Events_Copy_View$get_newDateCalendar() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_NewDateCalendarController');
    },
    get_button1: function SpottedScript_Pages_Events_Copy_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_customValidator1: function SpottedScript_Pages_Events_Copy_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_customvalidator2: function SpottedScript_Pages_Events_Copy_View$get_customvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator2');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Events_Copy_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_panelConflict: function SpottedScript_Pages_Events_Copy_View$get_panelConflict() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelConflict');
    },
    get_panelConflictDataGrid: function SpottedScript_Pages_Events_Copy_View$get_panelConflictDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelConflictDataGrid');
    },
    get_panelConflictCheckbox: function SpottedScript_Pages_Events_Copy_View$get_panelConflictCheckbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelConflictCheckbox');
    },
    get_button2: function SpottedScript_Pages_Events_Copy_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_button3: function SpottedScript_Pages_Events_Copy_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_customvalidator3: function SpottedScript_Pages_Events_Copy_View$get_customvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator3');
    },
    get_panelReview: function SpottedScript_Pages_Events_Copy_View$get_panelReview() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelReview');
    },
    get_h12: function SpottedScript_Pages_Events_Copy_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_reviewVenue: function SpottedScript_Pages_Events_Copy_View$get_reviewVenue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewVenue');
    },
    get_reviewDate: function SpottedScript_Pages_Events_Copy_View$get_reviewDate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewDate');
    },
    get_reviewName: function SpottedScript_Pages_Events_Copy_View$get_reviewName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewName');
    },
    get_reviewStartTime: function SpottedScript_Pages_Events_Copy_View$get_reviewStartTime() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewStartTime');
    },
    get_reviewShortDetailsHtml: function SpottedScript_Pages_Events_Copy_View$get_reviewShortDetailsHtml() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewShortDetailsHtml');
    },
    get_reviewLongDetailsHtml: function SpottedScript_Pages_Events_Copy_View$get_reviewLongDetailsHtml() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewLongDetailsHtml');
    },
    get_reviewCapacity: function SpottedScript_Pages_Events_Copy_View$get_reviewCapacity() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewCapacity');
    },
    get_reviewBrandTr: function SpottedScript_Pages_Events_Copy_View$get_reviewBrandTr() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewBrandTr');
    },
    get_reviewBrand: function SpottedScript_Pages_Events_Copy_View$get_reviewBrand() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewBrand');
    },
    get_reviewMusicTypes: function SpottedScript_Pages_Events_Copy_View$get_reviewMusicTypes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewMusicTypes');
    },
    get_reviewNoPicTd: function SpottedScript_Pages_Events_Copy_View$get_reviewNoPicTd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewNoPicTd');
    },
    get_reviewPicTd: function SpottedScript_Pages_Events_Copy_View$get_reviewPicTd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewPicTd');
    },
    get_reviewPicImg: function SpottedScript_Pages_Events_Copy_View$get_reviewPicImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewPicImg');
    },
    get_reviewEditAnchor: function SpottedScript_Pages_Events_Copy_View$get_reviewEditAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewEditAnchor');
    },
    get_linkButton1: function SpottedScript_Pages_Events_Copy_View$get_linkButton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkButton1');
    },
    get_panelSaved: function SpottedScript_Pages_Events_Copy_View$get_panelSaved() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSaved');
    },
    get_h14: function SpottedScript_Pages_Events_Copy_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_ucBannerPreview: function SpottedScript_Pages_Events_Copy_View$get_ucBannerPreview() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ucBannerPreview');
    },
    get_h13: function SpottedScript_Pages_Events_Copy_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_panelSavedEventLink: function SpottedScript_Pages_Events_Copy_View$get_panelSavedEventLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSavedEventLink');
    },
    get_panelSavedSignUpLink: function SpottedScript_Pages_Events_Copy_View$get_panelSavedSignUpLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSavedSignUpLink');
    },
    get_linkButton2: function SpottedScript_Pages_Events_Copy_View$get_linkButton2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkButton2');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Copy_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Copy.View.registerClass('SpottedScript.Pages.Events.Copy.View', SpottedScript.DsiUserControl.View);
