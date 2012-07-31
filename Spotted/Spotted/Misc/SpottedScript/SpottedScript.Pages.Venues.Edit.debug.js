Type.registerNamespace('SpottedScript.Pages.Venues.Edit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Venues.Edit.View
SpottedScript.Pages.Venues.Edit.View = function SpottedScript_Pages_Venues_Edit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Venues.Edit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Venues.Edit.View.prototype = {
    clientId: null,
    get_panelDetails: function SpottedScript_Pages_Venues_Edit_View$get_panelDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetails');
    },
    get_panelDetailsPlaceDiv: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsPlaceDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsPlaceDiv');
    },
    get_requiredFieldValidator4: function SpottedScript_Pages_Venues_Edit_View$get_requiredFieldValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator4');
    },
    get_panelDetailsPlaceLockedDiv: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsPlaceLockedDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsPlaceLockedDiv');
    },
    get_panelDetailsPlaceLockedLabel: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsPlaceLockedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsPlaceLockedLabel');
    },
    get_panelDetailsPostcodeDiv: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsPostcodeDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsPostcodeDiv');
    },
    get_panelDetailsPostcodeTextBox: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsPostcodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsPostcodeTextBox');
    },
    get_customValidator2: function SpottedScript_Pages_Venues_Edit_View$get_customValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator2');
    },
    get_panelDetailsNameDiv: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsNameDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsNameDiv');
    },
    get_panelDetailsVenueName: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsVenueName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsVenueName');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Venues_Edit_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_panelDetailsVenueRegularEventsYes: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsVenueRegularEventsYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsVenueRegularEventsYes');
    },
    get_panelDetailsVenueRegularEventsNo: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsVenueRegularEventsNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsVenueRegularEventsNo');
    },
    get_customValidator1: function SpottedScript_Pages_Venues_Edit_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_panelDetailsVenueCapacity: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsVenueCapacity() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsVenueCapacity');
    },
    get_requiredFieldValidator2: function SpottedScript_Pages_Venues_Edit_View$get_requiredFieldValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator2');
    },
    get_compareValidator1: function SpottedScript_Pages_Venues_Edit_View$get_compareValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator1');
    },
    get_panelDetailsVenueDetailsHtml: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsVenueDetailsHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_PanelDetailsVenueDetailsHtmlController');
    },
    get_requiredFieldValidator3: function SpottedScript_Pages_Venues_Edit_View$get_requiredFieldValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator3');
    },
    get_button1: function SpottedScript_Pages_Venues_Edit_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button2: function SpottedScript_Pages_Venues_Edit_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_button3: function SpottedScript_Pages_Venues_Edit_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_customvalidator3: function SpottedScript_Pages_Venues_Edit_View$get_customvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator3');
    },
    get_panelPic: function SpottedScript_Pages_Venues_Edit_View$get_panelPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPic');
    },
    get_picUc: function SpottedScript_Pages_Venues_Edit_View$get_picUc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUc');
    },
    get_panelSaved: function SpottedScript_Pages_Venues_Edit_View$get_panelSaved() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSaved');
    },
    get_panelSavedAddEventLink: function SpottedScript_Pages_Venues_Edit_View$get_panelSavedAddEventLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSavedAddEventLink');
    },
    get_panelSavedVenueLink: function SpottedScript_Pages_Venues_Edit_View$get_panelSavedVenueLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSavedVenueLink');
    },
    get_headerH1: function SpottedScript_Pages_Venues_Edit_View$get_headerH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderH1');
    },
    get_panelEditError: function SpottedScript_Pages_Venues_Edit_View$get_panelEditError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEditError');
    },
    get_panelDetailsPlacePicker: function SpottedScript_Pages_Venues_Edit_View$get_panelDetailsPlacePicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_PanelDetailsPlacePickerController');
    },
    get_panelPostcodeCheck: function SpottedScript_Pages_Venues_Edit_View$get_panelPostcodeCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPostcodeCheck');
    },
    get_panelPostcodeCheckDataGrid: function SpottedScript_Pages_Venues_Edit_View$get_panelPostcodeCheckDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPostcodeCheckDataGrid');
    },
    get_panelPostcodeCheckNewCheckBox: function SpottedScript_Pages_Venues_Edit_View$get_panelPostcodeCheckNewCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPostcodeCheckNewCheckBox');
    },
    get_genericContainerPage: function SpottedScript_Pages_Venues_Edit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Venues.Edit.View.registerClass('SpottedScript.Pages.Venues.Edit.View', SpottedScript.DsiUserControl.View);
