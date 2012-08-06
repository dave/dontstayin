//! Picker.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.Picker');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.SearchTypes

Js.Controls.Picker.SearchTypes = function() { 
    /// <field name="unknown" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="venue" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="brand" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="music" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="google" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="spotter" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="me" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="key" type="Number" integer="true" static="true">
    /// </field>
};
Js.Controls.Picker.SearchTypes.prototype = {
    unknown: 0, 
    venue: 1, 
    brand: 2, 
    music: 3, 
    google: 4, 
    spotter: 5, 
    me: 6, 
    key: 7
}
Js.Controls.Picker.SearchTypes.registerEnum('Js.Controls.Picker.SearchTypes', false);


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.Controller

Js.Controls.Picker.Controller = function Js_Controls_Picker_Controller(view) {
    /// <param name="view" type="Js.Controls.Picker.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.Picker.View">
    /// </field>
    /// <field name="_firstEverNavigate" type="Boolean">
    /// </field>
    /// <field name="_initialHistoryEvent" type="jQueryEvent">
    /// </field>
    /// <field name="_initialNavigate" type="Boolean">
    /// </field>
    /// <field name="_initialiseFinished" type="Boolean">
    /// </field>
    /// <field name="eventSelectionSepcificationChanged" type="Function">
    /// </field>
    /// <field name="_navigateSearchTypePreviousValue" type="String">
    /// </field>
    /// <field name="selectedKeyChanged" type="Function">
    /// </field>
    /// <field name="_keyControlIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_navigateKeyPreviousValue" type="String">
    /// </field>
    /// <field name="selectedSpotterChanged" type="Function">
    /// </field>
    /// <field name="_spotterControlIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_navigateSpotterPreviousValue" type="String">
    /// </field>
    /// <field name="selectedBrandChanged" type="Function">
    /// </field>
    /// <field name="_brandDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_navigateBrandPreviousValue" type="String">
    /// </field>
    /// <field name="selectedCountryChanged" type="Function">
    /// </field>
    /// <field name="_countryDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_navigateCountryPreviousValue" type="String">
    /// </field>
    /// <field name="selectedPlaceChanged" type="Function">
    /// </field>
    /// <field name="_placeDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_placeDropDownCountryK" type="Number" integer="true">
    /// </field>
    /// <field name="_placeDropDownPreviouslySelectedIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_navigatePlacePreviousValue" type="String">
    /// </field>
    /// <field name="selectedVenueChanged" type="Function">
    /// </field>
    /// <field name="_venueDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_venueDropDownPlaceK" type="Number" integer="true">
    /// </field>
    /// <field name="_venueByLetterDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_venueByLetterDropDownPlaceK" type="Number" integer="true">
    /// </field>
    /// <field name="_venueByLetterDropDownLetter" type="String">
    /// </field>
    /// <field name="_venueDropDownPreviouslySelectedIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_navigateVenuePreviousValue" type="String">
    /// </field>
    /// <field name="selectedMusicChanged" type="Function">
    /// </field>
    /// <field name="_musicDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_musicDropDownPreviouslySelectedIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_navigateMusicPreviousValue" type="String">
    /// </field>
    /// <field name="selectedDateChanged" type="Function">
    /// </field>
    /// <field name="_dateDropDownsAreInitialised" type="Boolean">
    /// </field>
    /// <field name="_dateMonths" type="Array" elementType="String">
    /// </field>
    /// <field name="_dateRefreshDaysHasRunBefore" type="Boolean">
    /// </field>
    /// <field name="_dateRefreshDaysPreviousNumberOfDaysInMonth" type="Number" integer="true">
    /// </field>
    /// <field name="_dateRefreshDaysPreviousFirstDayOfWeek" type="Number" integer="true">
    /// </field>
    /// <field name="_navigateDatePreviousValue" type="String">
    /// </field>
    /// <field name="selectedEventChanged" type="Function">
    /// </field>
    /// <field name="_eventDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_eventDropDownVenueK" type="Number" integer="true">
    /// </field>
    /// <field name="_eventDropDownBrandK" type="Number" integer="true">
    /// </field>
    /// <field name="_eventDropDownKey" type="String">
    /// </field>
    /// <field name="_eventDropDownDate" type="Js.Controls.Picker.DateStub">
    /// </field>
    /// <field name="_eventDropDownPreviouslySelectedIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_requestId" type="Number" integer="true">
    /// </field>
    /// <field name="_loadId" type="Number" integer="true">
    /// </field>
    /// <field name="_navigateEventPreviousValue" type="String">
    /// </field>
    /// <field name="_regexQuote" type="RegExp">
    /// </field>
    /// <field name="_debugCount" type="Number" integer="true">
    /// </field>
    this._regexQuote = new RegExp("'");
    this._view = view;
    $(window).bind('hashchange', ss.Delegate.create(this, this._navigate));
    if (Js.Library.Misc.get_browserIsIE()) {
        $(ss.Delegate.create(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
Js.Controls.Picker.Controller.prototype = {
    _view: null,
    
    _initialise: function Js_Controls_Picker_Controller$_initialise() {
        this._initialiseSearchType();
        this._initialiseKey();
        this._initialiseMe();
        this._initialiseSpotter();
        this._initialiseBrand();
        this._initialiseCountry();
        this._initialisePlace();
        this._initialiseVenue();
        this._initialiseMusic();
        this._initialiseDate();
        this._initialiseEvent();
        this._navigateSearchType(this._view.get_selectedSearchTypeHidden().value);
        this._navigateKey(this._view.get_selectedKeyHidden().value);
        this._navigateSpotter(this._view.get_selectedSpotterHidden().value);
        this._navigateBrand(this._view.get_selectedBrandHidden().value);
        this._navigateCountry(this._view.get_selectedCountryHidden().value);
        this._navigatePlace(this._view.get_selectedPlaceHidden().value);
        this._navigateVenue(this._view.get_selectedVenueHidden().value);
        this._navigateMusic(this._view.get_selectedMusicHidden().value);
        this._navigateDate(this._view.get_selectedDateHidden().value);
        this._navigateEvent(this._view.get_selectedEventHidden().value);
        if (Js.Library.Misc.get_browserIsIE()) {
            this._addHistory('', '');
        }
        this._updateUI();
        this._initialiseFirstUnknownControl();
        this._view.get_holder().style.display = '';
        $(window).trigger('hashchange');
        this._initialiseFinished = true;
        if (this._initialNavigate) {
            this._navigate(this._initialHistoryEvent);
        }
    },
    
    _firstEverNavigate: true,
    _initialHistoryEvent: null,
    _initialNavigate: false,
    _initialiseFinished: false,
    
    _navigate: function Js_Controls_Picker_Controller$_navigate(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (!this._initialiseFinished) {
            this._initialNavigate = true;
            this._initialHistoryEvent = e;
            return;
        }
        if ($.bbq.getState(this._view.clientId + '_SearchType') != null || $.bbq.getState(this._view.clientId + '_Key') != null || $.bbq.getState(this._view.clientId + '_Spotter') != null || $.bbq.getState(this._view.clientId + '_Brand') != null || $.bbq.getState(this._view.clientId + '_Country') != null || $.bbq.getState(this._view.clientId + '_Place') != null || $.bbq.getState(this._view.clientId + '_Venue') != null || $.bbq.getState(this._view.clientId + '_Music') != null || $.bbq.getState(this._view.clientId + '_Date') != null || $.bbq.getState(this._view.clientId + '_Event') != null) {
            this._navigateSearchType(this._getVal(e, 'SearchType'));
            this._navigateKey(this._getVal(e, 'Key'));
            this._navigateSpotter(this._getVal(e, 'Spotter'));
            this._navigateBrand(this._getVal(e, 'Brand'));
            this._navigateCountry(this._getVal(e, 'Country'));
            this._navigatePlace(this._getVal(e, 'Place'));
            this._navigateVenue(this._getVal(e, 'Venue'));
            this._navigateMusic(this._getVal(e, 'Music'));
            this._navigateDate(this._getVal(e, 'Date'));
            this._navigateEvent(this._getVal(e, 'Event'));
            if (this._firstEverNavigate) {
                this._firstEverNavigate = false;
                this._updateUI();
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(this.getCurrentEventSelectionSepcification()));
                }
                if (this.get__event() != null && (this.get__searchType() === 7 || this.get__searchType() === 2 || this.get__searchType() === 1) && this.selectedEventChanged != null) {
                    this.selectedEventChanged(this, new Js.Controls.Picker.ObjectArgs(this.get__event()));
                }
                if (this.get__venue() != null && this.get__searchType() === 1 && this.selectedVenueChanged != null) {
                    this.selectedVenueChanged(this, new Js.Controls.Picker.ObjectArgs(this.get__venue()));
                }
                if (this.get__place() != null && (this.get__searchType() === 1 || this.get__searchType() === 3) && this.selectedPlaceChanged != null) {
                    this.selectedPlaceChanged(this, new Js.Controls.Picker.ObjectArgs(this.get__place()));
                }
                if (this.get__country() != null && (this.get__searchType() === 1 || this.get__searchType() === 3) && this.selectedCountryChanged != null) {
                    this.selectedCountryChanged(this, new Js.Controls.Picker.ObjectArgs(this.get__country()));
                }
                if (this.get__brand() != null && this.get__searchType() === 2 && this.selectedBrandChanged != null) {
                    this.selectedBrandChanged(this, new Js.Controls.Picker.ObjectArgs(this.get__brand()));
                }
                if (this.get__spotter() != null && this.get__spotter().length > 0 && this.get__searchType() === 5 && this.selectedSpotterChanged != null) {
                    this.selectedSpotterChanged(this, new Js.Controls.Picker.StringArgs(this.get__spotter()));
                }
                if (this.get__key() != null && this.get__key().length > 0 && this.get__searchType() === 7 && this.selectedKeyChanged != null) {
                    this.selectedKeyChanged(this, new Js.Controls.Picker.StringArgs(this.get__key()));
                }
            }
        }
    },
    
    _getVal: function Js_Controls_Picker_Controller$_getVal(e, item) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        /// <param name="item" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var val = $.bbq.getState(this._view.clientId + '_' + item);
        if (val == null) {
            return '';
        }
        else {
            return decodeURI(val.toString());
        }
    },
    
    _updateUI: function Js_Controls_Picker_Controller$_updateUI() {
        this._updateUISearchType();
        this._updateUIKey(false);
        this._updateUISpotter();
        this._updateUIBrand(false);
        this._updateUICountry(false);
        this._updateUIPlace(false);
        this._updateUIVenue(false);
        this._updateUIMusic(false);
        this._updateUIDate(false);
        this._updateUIEvent();
    },
    
    handlersSet: function Js_Controls_Picker_Controller$handlersSet() {
        this._firstEverNavigate = true;
        if (this._initialNavigate) {
            this._navigate(this._initialHistoryEvent);
        }
    },
    
    eventSelectionSepcificationChanged: null,
    
    getCurrentEventSelectionSepcification: function Js_Controls_Picker_Controller$getCurrentEventSelectionSepcification() {
        /// <returns type="Js.Controls.Picker.EventSelectionSpecification"></returns>
        return new Js.Controls.Picker.EventSelectionSpecification((this.get__searchType() === 2) ? this.get__brand() : null, (this.get__searchType() === 1 || this.get__searchType() === 3) ? this.get__place() : null, (this.get__searchType() === 1) ? this.get__venue() : null, (this.get__searchType() === 3) ? this.get__music() : null, this.get__date(), this.get__searchType() === 6);
    },
    
    fireEventSelectionChange: function Js_Controls_Picker_Controller$fireEventSelectionChange() {
        if (this.eventSelectionSepcificationChanged != null) {
            this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(this.getCurrentEventSelectionSepcification()));
        }
    },
    
    get__searchType: function Js_Controls_Picker_Controller$get__searchType() {
        /// <value type="Js.Controls.Picker.SearchTypes"></value>
        if (this.get__searchTypeHasMultipleOptions()) {
            return (this._view.get_selectedSearchTypeHidden().value === 'key') ? 7 : (this._view.get_selectedSearchTypeHidden().value === 'me') ? 6 : (this._view.get_selectedSearchTypeHidden().value === 'spotter') ? 5 : (this._view.get_selectedSearchTypeHidden().value === 'venue') ? 1 : (this._view.get_selectedSearchTypeHidden().value === 'brand') ? 2 : (this._view.get_selectedSearchTypeHidden().value === 'music') ? 3 : (this._view.get_selectedSearchTypeHidden().value === 'google') ? 4 : 0;
        }
        else if (this.get__optionKey()) {
            return 7;
        }
        else if (this.get__optionMe()) {
            return 6;
        }
        else if (this.get__optionSpotter()) {
            return 5;
        }
        else if (this.get__optionBrand()) {
            return 2;
        }
        else if (this.get__optionCountry()) {
            return 1;
        }
        else if (this.get__optionMusic()) {
            return 3;
        }
        else if (this.get__optionGoogle()) {
            return 4;
        }
        else {
            return 0;
        }
    },
    
    get__searchTypeHasMultipleOptions: function Js_Controls_Picker_Controller$get__searchTypeHasMultipleOptions() {
        /// <value type="Boolean"></value>
        var options = ((this.get__optionKey()) ? 1 : 0) + ((this.get__optionMe()) ? 1 : 0) + ((this.get__optionSpotter()) ? 1 : 0) + ((this.get__optionBrand()) ? 1 : 0) + ((this.get__optionCountry()) ? 1 : 0) + ((this.get__optionMusic()) ? 1 : 0) + ((this.get__optionGoogle()) ? 1 : 0);
        return options > 1;
    },
    
    _initialiseSearchType: function Js_Controls_Picker_Controller$_initialiseSearchType() {
        this._view.get_searchTypeKeyJ().click(ss.Delegate.create(this, this._searchTypeRadioClick));
        this._view.get_searchTypeMeJ().click(ss.Delegate.create(this, this._searchTypeRadioClick));
        this._view.get_searchTypeSpotterJ().click(ss.Delegate.create(this, this._searchTypeRadioClick));
        this._view.get_searchTypeVenueJ().click(ss.Delegate.create(this, this._searchTypeRadioClick));
        this._view.get_searchTypeBrandJ().click(ss.Delegate.create(this, this._searchTypeRadioClick));
        this._view.get_searchTypeMusicJ().click(ss.Delegate.create(this, this._searchTypeRadioClick));
        this._view.get_searchTypeGoogleJ().click(ss.Delegate.create(this, this._searchTypeRadioClick));
        this._view.get_searchTypeKey().parentNode.style.display = (this.get__optionKey()) ? 'block' : 'none';
        this._view.get_searchTypeMe().parentNode.style.display = (this.get__optionMe()) ? 'block' : 'none';
        this._view.get_searchTypeSpotter().parentNode.style.display = (this.get__optionSpotter()) ? 'block' : 'none';
        this._view.get_searchTypeVenue().parentNode.style.display = (this.get__optionVenue()) ? 'block' : 'none';
        this._view.get_searchTypeBrand().parentNode.style.display = (this.get__optionBrand()) ? 'block' : 'none';
        this._view.get_searchTypeMusic().parentNode.style.display = (this.get__optionMusic()) ? 'block' : 'none';
        this._view.get_searchTypeGoogle().parentNode.style.display = (this.get__optionGoogle()) ? 'block' : 'none';
    },
    
    _searchTypeRadioClick: function Js_Controls_Picker_Controller$_searchTypeRadioClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var value = (this._view.get_searchTypeKey().checked) ? 'key' : (this._view.get_searchTypeMe().checked) ? 'me' : (this._view.get_searchTypeSpotter().checked) ? 'spotter' : (this._view.get_searchTypeVenue().checked) ? 'venue' : (this._view.get_searchTypeBrand().checked) ? 'brand' : (this._view.get_searchTypeMusic().checked) ? 'music' : (this._view.get_searchTypeGoogle().checked) ? 'google' : 'unknown';
        if (value === 'unknown') {
            if (this.get__searchType() === 7) {
                this._view.get_searchTypeKey().checked = true;
            }
            else if (this.get__searchType() === 6) {
                this._view.get_searchTypeMe().checked = true;
            }
            else if (this.get__searchType() === 5) {
                this._view.get_searchTypeSpotter().checked = true;
            }
            else if (this.get__searchType() === 1) {
                this._view.get_searchTypeVenue().checked = true;
            }
            else if (this.get__searchType() === 2) {
                this._view.get_searchTypeBrand().checked = true;
            }
            else if (this.get__searchType() === 3) {
                this._view.get_searchTypeMusic().checked = true;
            }
            else if (this.get__searchType() === 4) {
                this._view.get_searchTypeGoogle().checked = true;
            }
            if (!this._firstEverNavigate) {
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            this._addHistory('SearchType', value);
        }
    },
    
    _initialiseFirstUnknownControl: function Js_Controls_Picker_Controller$_initialiseFirstUnknownControl() {
        if (this.get__searchType() === 5) {
            if (!this.get__spotter().length) {
                this._initialiseSpotterControl();
            }
        }
        else if (this.get__searchType() === 7) {
            if (!this.get__key().length) {
                this._initialiseKeyControl();
            }
        }
        else if (this.get__searchType() === 2) {
            if (this.get__brand() == null) {
                this._initialiseBrandDropDown();
            }
            else if (this.get__event() == null) {
                this._initialiseEventDropDown();
            }
        }
        else if (this.get__searchType() === 1 || this.get__searchType() === 3) {
            if (this.get__country() == null) {
                this._initialiseCountryDropDown();
            }
            else if (this.get__place() == null) {
                this._initialisePlaceDropDown();
            }
            else if (this.get__searchType() === 1 && this.get__venue() == null || this.get__searchType() === 3) {
                if (this.get__searchType() === 1 && this.get__venue() == null) {
                    this._initialiseVenueDropDown(null);
                }
                else if (this.get__searchType() === 3) {
                    this._initialiseMusicDropDown();
                }
            }
            else if (this.get__event() == null) {
                this._initialiseEventDropDown();
            }
        }
        else if (this.get__searchType() === 4) {
        }
    },
    
    _navigateSearchTypePreviousValue: '',
    
    _navigateSearchType: function Js_Controls_Picker_Controller$_navigateSearchType(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!value.length || value === this._navigateSearchTypePreviousValue) {
            return;
        }
        if (value === 'key' && !this._view.get_searchTypeKey().checked) {
            this._view.get_searchTypeKey().checked = true;
        }
        if (value === 'me' && !this._view.get_searchTypeMe().checked) {
            this._view.get_searchTypeMe().checked = true;
        }
        if (value === 'spotter' && !this._view.get_searchTypeSpotter().checked) {
            this._view.get_searchTypeSpotter().checked = true;
        }
        if (value === 'venue' && !this._view.get_searchTypeVenue().checked) {
            this._view.get_searchTypeVenue().checked = true;
        }
        if (value === 'brand' && !this._view.get_searchTypeBrand().checked) {
            this._view.get_searchTypeBrand().checked = true;
        }
        if (value === 'music' && !this._view.get_searchTypeMusic().checked) {
            this._view.get_searchTypeMusic().checked = true;
        }
        if (value === 'google' && !this._view.get_searchTypeGoogle().checked) {
            this._view.get_searchTypeGoogle().checked = true;
        }
        this._view.get_selectedSearchTypeHidden().value = value;
        if (!this._firstEverNavigate) {
            this._updateUI();
        }
        this._navigateSearchTypePreviousValue = value;
        this._initialiseFirstUnknownControl();
        this._initialiseEventDropDown();
        if (!this._firstEverNavigate) {
            if (this.eventSelectionSepcificationChanged != null) {
                this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(this.getCurrentEventSelectionSepcification()));
            }
        }
    },
    
    _updateUISearchType: function Js_Controls_Picker_Controller$_updateUISearchType() {
        this._view.get_searchTypeHolder().style.display = (this.get__searchTypeHasMultipleOptions()) ? '' : 'none';
    },
    
    get__optionKey: function Js_Controls_Picker_Controller$get__optionKey() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionKeyHidden().value);
    },
    
    selectedKeyChanged: null,
    _keyControlIsInitialised: false,
    
    get__key: function Js_Controls_Picker_Controller$get__key() {
        /// <value type="String"></value>
        if (!this.get__optionKey() || this._view.get_selectedKeyHidden().value == null) {
            return '';
        }
        return this._view.get_selectedKeyHidden().value;
    },
    
    _initialiseKey: function Js_Controls_Picker_Controller$_initialiseKey() {
        if (!this.get__optionKey()) {
            return;
        }
        this._view.get_keySearchButtonJ().click(ss.Delegate.create(this, this._keyChange));
        this._view.get_keySelectedChangeLinkJ().click(ss.Delegate.create(this, this._keySelectedChangeLinkClick));
    },
    
    _keySelectedChangeLinkClick: function Js_Controls_Picker_Controller$_keySelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialiseKeyControl();
    },
    
    _initialiseKeyControl: function Js_Controls_Picker_Controller$_initialiseKeyControl() {
        if (!this.get__optionKey()) {
            return;
        }
        this._keyControlIsInitialised = true;
        if (this.get__key() != null && this.get__key().length > 0) {
            this._view.get_keyTextBox().value = this.get__key();
        }
        this._updateUIKey(true);
    },
    
    _keyChange: function Js_Controls_Picker_Controller$_keyChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._view.get_keyTextBox().value == null || !this._view.get_keyTextBox().value.length) {
            return;
        }
        this._addHistory('Key', this._view.get_keyTextBox().value);
    },
    
    _navigateKeyPreviousValue: '',
    
    _navigateKey: function Js_Controls_Picker_Controller$_navigateKey(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionKey() || !value.length || value === this._navigateKeyPreviousValue) {
            return;
        }
        if (value === '0') {
            this._view.get_keyTextBox().value = '';
            this._view.get_keySelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedKeyHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedKeyChanged != null) {
                    this.selectedKeyChanged(this, new Js.Controls.Picker.StringArgs(null));
                }
            }
        }
        else {
            if (this._keyControlIsInitialised && this._view.get_keyTextBox().value !== value) {
                this._view.get_keyTextBox().value = value;
            }
            if (!this._keyControlIsInitialised) {
                this._view.get_keySelectedLabel().innerHTML = value;
            }
            this._view.get_selectedKeyHidden().value = value;
            if (!this._firstEverNavigate) {
                if (this.selectedKeyChanged != null) {
                    this.selectedKeyChanged(this, new Js.Controls.Picker.StringArgs(value));
                }
            }
        }
        this._navigateKeyPreviousValue = value;
        this._initialiseEventDropDown();
    },
    
    _updateUIKey: function Js_Controls_Picker_Controller$_updateUIKey(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_keyHolder().style.display = (this.get__optionKey() && this.get__searchType() === 7) ? '' : 'none';
        this._view.get_keySelectedHolder().style.display = (!this._keyControlIsInitialised) ? '' : 'none';
        this._view.get_keyChoiceHolder().style.display = (this._keyControlIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIEvent();
        }
    },
    
    get__optionMe: function Js_Controls_Picker_Controller$get__optionMe() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionMeHidden().value);
    },
    
    _initialiseMe: function Js_Controls_Picker_Controller$_initialiseMe() {
    },
    
    get__optionSpotter: function Js_Controls_Picker_Controller$get__optionSpotter() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionSpotterHidden().value);
    },
    
    selectedSpotterChanged: null,
    _spotterControlIsInitialised: false,
    
    get__spotter: function Js_Controls_Picker_Controller$get__spotter() {
        /// <value type="String"></value>
        if (!this.get__optionSpotter() || this._view.get_selectedSpotterHidden().value == null) {
            return '';
        }
        return this._view.get_selectedSpotterHidden().value;
    },
    
    _initialiseSpotter: function Js_Controls_Picker_Controller$_initialiseSpotter() {
        if (!this.get__optionSpotter()) {
            return;
        }
        this._view.get_spotterSearchButtonJ().click(ss.Delegate.create(this, this._spotterChange));
        this._view.get_spotterSelectedChangeLinkJ().click(ss.Delegate.create(this, this._spotterSelectedChangeLinkClick));
    },
    
    _spotterSelectedChangeLinkClick: function Js_Controls_Picker_Controller$_spotterSelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialiseSpotterControl();
    },
    
    _initialiseSpotterControl: function Js_Controls_Picker_Controller$_initialiseSpotterControl() {
        if (!this.get__optionSpotter()) {
            return;
        }
        this._spotterControlIsInitialised = true;
        if (this.get__spotter() != null && this.get__spotter().length > 0) {
            this._view.get_spotterTextBox().value = this.get__spotter();
        }
        this._updateUISpotter();
    },
    
    _spotterChange: function Js_Controls_Picker_Controller$_spotterChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._view.get_spotterTextBox().value == null || !this._view.get_spotterTextBox().value.length) {
            return;
        }
        this._addHistory('Spotter', this._view.get_spotterTextBox().value);
    },
    
    _navigateSpotterPreviousValue: '',
    
    _navigateSpotter: function Js_Controls_Picker_Controller$_navigateSpotter(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionSpotter() || !value.length || value === this._navigateSpotterPreviousValue) {
            return;
        }
        if (!value.length) {
            this._view.get_spotterTextBox().value = '';
            this._view.get_spotterSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedSpotterHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedSpotterChanged != null) {
                    this.selectedSpotterChanged(this, new Js.Controls.Picker.StringArgs(null));
                }
            }
        }
        else {
            if (this._spotterControlIsInitialised && this._view.get_spotterTextBox().value !== value) {
                this._view.get_spotterTextBox().value = value;
            }
            if (!this._spotterControlIsInitialised) {
                this._view.get_spotterSelectedLabel().innerHTML = value;
            }
            this._view.get_selectedSpotterHidden().value = value;
            if (!this._firstEverNavigate) {
                if (this.selectedSpotterChanged != null) {
                    this.selectedSpotterChanged(this, new Js.Controls.Picker.StringArgs(value));
                }
            }
        }
        this._navigateSpotterPreviousValue = value;
    },
    
    _updateUISpotter: function Js_Controls_Picker_Controller$_updateUISpotter() {
        this._view.get_spotterHolder().style.display = (this.get__optionSpotter() && this.get__searchType() === 5) ? '' : 'none';
        this._view.get_spotterSelectedHolder().style.display = (!this._spotterControlIsInitialised) ? '' : 'none';
        this._view.get_spotterChoiceHolder().style.display = (this._spotterControlIsInitialised) ? '' : 'none';
    },
    
    get__optionBrand: function Js_Controls_Picker_Controller$get__optionBrand() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionBrandHidden().value);
    },
    
    selectedBrandChanged: null,
    _brandDropDownIsInitialised: false,
    
    get__brand: function Js_Controls_Picker_Controller$get__brand() {
        /// <value type="Js.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionBrand() || this._view.get_selectedBrandHidden().value == null || !this._view.get_selectedBrandHidden().value.length) {
            return null;
        }
        return Js.Controls.Picker.ObjectStub.fromString(this._view.get_selectedBrandHidden().value);
    },
    
    _initialiseBrand: function Js_Controls_Picker_Controller$_initialiseBrand() {
        if (!this.get__optionBrand()) {
            return;
        }
        this._view.get_brandAutoComplete().itemChosen = ss.Delegate.create(this, this._brandDropDownChange);
        this._view.get_brandSelectedChangeLinkJ().click(ss.Delegate.create(this, this._brandSelectedChangeLinkClick));
    },
    
    _brandSelectedChangeLinkClick: function Js_Controls_Picker_Controller$_brandSelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialiseBrandDropDown();
    },
    
    _initialiseBrandDropDown: function Js_Controls_Picker_Controller$_initialiseBrandDropDown() {
        if (!this.get__optionBrand()) {
            return;
        }
        this._brandDropDownIsInitialised = true;
        if (this.get__brand() != null) {
            this._view.get_brandAutoComplete().set_value(this.get__brand().k.toString());
            this._view.get_brandAutoComplete().set_text(this.get__brand().name);
        }
        this._updateUIBrand(true);
    },
    
    _brandDropDownChange: function Js_Controls_Picker_Controller$_brandDropDownChange(e) {
        /// <param name="e" type="Js.ClientControls.KeyStringPair">
        /// </param>
        if (e.value == null || !e.value.length) {
            return;
        }
        var brand = new Js.Controls.Picker.ObjectStub(parseInt(e.value, 10), e.key);
        if (!brand.k) {
            return;
        }
        else {
            this._addHistory('Brand', brand.toString());
        }
    },
    
    _navigateBrandPreviousValue: '',
    
    _navigateBrand: function Js_Controls_Picker_Controller$_navigateBrand(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionBrand() || !value.length || value === this._navigateBrandPreviousValue) {
            return;
        }
        if (value === '0') {
            this._view.get_brandAutoComplete().set_text('');
            this._view.get_brandAutoComplete().set_value('');
            this._view.get_brandSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedBrandHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedBrandChanged != null) {
                    this.selectedBrandChanged(this, new Js.Controls.Picker.ObjectArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var brand = Js.Controls.Picker.ObjectStub.fromString(value);
            if (this._brandDropDownIsInitialised && this._view.get_brandAutoComplete().get_value() !== brand.k.toString()) {
                this._view.get_brandAutoComplete().set_value(brand.k.toString());
                this._view.get_brandAutoComplete().set_text(brand.name);
            }
            if (!this._brandDropDownIsInitialised) {
                this._view.get_brandSelectedLabel().innerHTML = brand.name;
            }
            this._view.get_selectedBrandHidden().value = brand.toString();
            if (!this._firstEverNavigate) {
                if (this.selectedBrandChanged != null) {
                    this.selectedBrandChanged(this, new Js.Controls.Picker.ObjectArgs(brand));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(new Js.Controls.Picker.EventSelectionSpecification((this.get__searchType() === 2) ? brand : null, (this.get__searchType() === 1 || this.get__searchType() === 3) ? this.get__place() : null, (this.get__searchType() === 1) ? this.get__venue() : null, (this.get__searchType() === 3) ? this.get__music() : null, this.get__date(), this.get__searchType() === 6)));
                }
            }
        }
        this._navigateBrandPreviousValue = value;
        this._initialiseDateDropDowns();
        this._initialiseEventDropDown();
    },
    
    _updateUIBrand: function Js_Controls_Picker_Controller$_updateUIBrand(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_brandHolder().style.display = (this.get__optionBrand() && this.get__searchType() === 2) ? '' : 'none';
        this._view.get_brandSelectedHolder().style.display = (!this._brandDropDownIsInitialised) ? '' : 'none';
        this._view.get_brandChoiceHolder().style.display = (this._brandDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIDate(true);
        }
    },
    
    get__optionCountry: function Js_Controls_Picker_Controller$get__optionCountry() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionCountryHidden().value);
    },
    
    selectedCountryChanged: null,
    _countryDropDownIsInitialised: false,
    
    get__country: function Js_Controls_Picker_Controller$get__country() {
        /// <value type="Js.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionCountry() || this._view.get_selectedCountryHidden().value == null || !this._view.get_selectedCountryHidden().value.length) {
            return null;
        }
        return Js.Controls.Picker.ObjectStub.fromString(this._view.get_selectedCountryHidden().value);
    },
    
    _initialiseCountry: function Js_Controls_Picker_Controller$_initialiseCountry() {
        if (!this.get__optionCountry()) {
            return;
        }
        this._view.get_countryDropDownJ().change(ss.Delegate.create(this, this._countryDropDownChange));
        this._view.get_countrySelectedChangeLinkJ().click(ss.Delegate.create(this, this._countrySelectedChangeLinkClick));
    },
    
    _countrySelectedChangeLinkClick: function Js_Controls_Picker_Controller$_countrySelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialiseCountryDropDown();
    },
    
    _initialiseCountryDropDown: function Js_Controls_Picker_Controller$_initialiseCountryDropDown() {
        if (!this.get__optionCountry()) {
            return;
        }
        if (!this._countryDropDownIsInitialised) {
            $(this._view.get_countryDropDown()).ajaxAddOption('/support/getcached.aspx?type=country', null, false, ss.Delegate.create(this, this._countryDropDownInitialised), null);
        }
        else {
            this._countryDropDownInitialised(null);
        }
    },
    
    _countryDropDownInitialised: function Js_Controls_Picker_Controller$_countryDropDownInitialised(args) {
        /// <param name="args" type="Object">
        /// </param>
        this._countryDropDownIsInitialised = true;
        if (this.get__country() != null) {
            var setValue = this._setK(this._view.get_countryDropDown(), this.get__country().k);
            if (!setValue) {
                this._addHistorys([ 'Country', '0', 'Place', '0', 'Venue', '0', 'Event', '0' ]);
            }
        }
        this._updateUICountry(true);
    },
    
    _countryDropDownChange: function Js_Controls_Picker_Controller$_countryDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var country = new Js.Controls.Picker.ObjectStub(this._getK(this._view.get_countryDropDown()), this._getText(this._view.get_countryDropDown()));
        if (!country.k) {
            if (this.get__country() == null) {
                this._setIndex(this._view.get_countryDropDown(), 0);
            }
            else {
                this._setK(this._view.get_countryDropDown(), this.get__country().k);
            }
        }
        else {
            this._addHistory('Country', country.toString());
        }
    },
    
    _navigateCountryPreviousValue: '',
    
    _navigateCountry: function Js_Controls_Picker_Controller$_navigateCountry(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionCountry() || !value.length || value === this._navigateCountryPreviousValue) {
            return;
        }
        if (value === '0') {
            this._setIndex(this._view.get_countryDropDown(), 0);
            this._view.get_countrySelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedCountryHidden().value = '';
            if (this.selectedCountryChanged != null) {
                this.selectedCountryChanged(this, new Js.Controls.Picker.ObjectArgs(null));
            }
        }
        else {
            var country = Js.Controls.Picker.ObjectStub.fromString(value);
            if (this._countryDropDownIsInitialised && this._getK(this._view.get_countryDropDown()) !== country.k) {
                this._setK(this._view.get_countryDropDown(), country.k);
            }
            if (!this._countryDropDownIsInitialised) {
                this._view.get_countrySelectedLabel().innerHTML = country.name;
            }
            this._view.get_selectedCountryHidden().value = country.toString();
            if (this.selectedCountryChanged != null) {
                this.selectedCountryChanged(this, new Js.Controls.Picker.ObjectArgs(country));
            }
        }
        this._navigateCountryPreviousValue = value;
        this._initialisePlaceDropDown();
    },
    
    _updateUICountry: function Js_Controls_Picker_Controller$_updateUICountry(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_countryHolder().style.display = (this.get__optionCountry() && (this.get__searchType() === 1 || this.get__searchType() === 3)) ? '' : 'none';
        this._view.get_countrySelectedHolder().style.display = (!this._countryDropDownIsInitialised) ? '' : 'none';
        this._view.get_countryChoiceHolder().style.display = (this._countryDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIPlace(true);
        }
    },
    
    get__optionPlace: function Js_Controls_Picker_Controller$get__optionPlace() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionPlaceHidden().value);
    },
    
    selectedPlaceChanged: null,
    _placeDropDownIsInitialised: false,
    _placeDropDownCountryK: 0,
    _placeDropDownPreviouslySelectedIndex: 0,
    
    get__place: function Js_Controls_Picker_Controller$get__place() {
        /// <value type="Js.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionPlace() || this._view.get_selectedPlaceHidden().value == null || !this._view.get_selectedPlaceHidden().value.length) {
            return null;
        }
        return Js.Controls.Picker.ObjectStub.fromString(this._view.get_selectedPlaceHidden().value);
    },
    
    _initialisePlace: function Js_Controls_Picker_Controller$_initialisePlace() {
        if (!this.get__optionPlace()) {
            return;
        }
        this._view.get_placeDropDownJ().change(ss.Delegate.create(this, this._placeDropDownChange));
        this._view.get_placeSelectedChangeLinkJ().click(ss.Delegate.create(this, this._placeSelectedChangeLinkClick));
    },
    
    _placeSelectedChangeLinkClick: function Js_Controls_Picker_Controller$_placeSelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialisePlaceDropDown();
    },
    
    _initialisePlaceDropDown: function Js_Controls_Picker_Controller$_initialisePlaceDropDown() {
        if (!this.get__optionPlace()) {
            return;
        }
        if (!this._placeDropDownIsInitialised || this._placeDropDownCountryK !== this.get__country().k) {
            this._placeDropDownCountryK = this.get__country().k;
            $(this._view.get_placeDropDown()).ajaxAddOption('/support/getcached.aspx?type=place&countryk=' + this.get__country().k + '&return=k', null, false, ss.Delegate.create(this, this._placeDropDownInitialised), null);
        }
        else {
            this._placeDropDownInitialised(null);
        }
    },
    
    _placeDropDownInitialised: function Js_Controls_Picker_Controller$_placeDropDownInitialised(args) {
        /// <param name="args" type="Object">
        /// </param>
        this._placeDropDownIsInitialised = true;
        if (this.get__place() != null) {
            var setValue = this._setK(this._view.get_placeDropDown(), this.get__place().k);
            if (!setValue) {
                this._addHistorys([ 'Place', '0', 'Venue', '0', 'Event', '0' ]);
            }
        }
        this._placeDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_placeDropDown());
        this._updateUIPlace(true);
    },
    
    _placeDropDownChange: function Js_Controls_Picker_Controller$_placeDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var k = this._getK(this._view.get_placeDropDown());
        if (k > 0) {
            var place = new Js.Controls.Picker.ObjectStub(this._getK(this._view.get_placeDropDown()), this._getText(this._view.get_placeDropDown()));
            this._addHistory('Place', place.toString());
        }
        else {
            this._setIndex(this._view.get_placeDropDown(), this._placeDropDownPreviouslySelectedIndex);
        }
        this._placeDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_placeDropDown());
    },
    
    _navigatePlacePreviousValue: '',
    
    _navigatePlace: function Js_Controls_Picker_Controller$_navigatePlace(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionPlace() || !value.length || value === this._navigatePlacePreviousValue) {
            return;
        }
        if (value === '0') {
            this._setIndex(this._view.get_placeDropDown(), 0);
            this._view.get_placeSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedPlaceHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedPlaceChanged != null) {
                    this.selectedPlaceChanged(this, new Js.Controls.Picker.ObjectArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var place = Js.Controls.Picker.ObjectStub.fromString(value);
            if (this._placeDropDownIsInitialised && this._getK(this._view.get_placeDropDown()) !== place.k) {
                this._setK(this._view.get_placeDropDown(), place.k);
            }
            if (!this._placeDropDownIsInitialised) {
                this._view.get_placeSelectedLabel().innerHTML = place.name;
            }
            this._view.get_selectedPlaceHidden().value = place.toString();
            if (!this._firstEverNavigate) {
                if (this.selectedPlaceChanged != null) {
                    this.selectedPlaceChanged(this, new Js.Controls.Picker.ObjectArgs(place));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(new Js.Controls.Picker.EventSelectionSpecification((this.get__searchType() === 2) ? this.get__brand() : null, (this.get__searchType() === 1 || this.get__searchType() === 3) ? place : null, (this.get__searchType() === 1) ? this.get__venue() : null, (this.get__searchType() === 3) ? this.get__music() : null, this.get__date(), this.get__searchType() === 6)));
                }
            }
        }
        this._navigatePlacePreviousValue = value;
        this._initialiseVenueDropDown(null);
        this._initialiseDateDropDowns();
        this._initialiseMusicDropDown();
    },
    
    _updateUIPlace: function Js_Controls_Picker_Controller$_updateUIPlace(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_placeHolder().style.display = (this.get__optionPlace() && (this.get__searchType() === 1 || this.get__searchType() === 3) && this.get__country() != null && (!this._placeDropDownIsInitialised || this._placeDropDownCountryK === this.get__country().k)) ? '' : 'none';
        this._view.get_placeSelectedHolder().style.display = (!this._placeDropDownIsInitialised) ? '' : 'none';
        this._view.get_placeChoiceHolder().style.display = (this._placeDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIVenue(true);
        }
    },
    
    get__optionVenue: function Js_Controls_Picker_Controller$get__optionVenue() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionVenueHidden().value);
    },
    
    selectedVenueChanged: null,
    _venueDropDownIsInitialised: false,
    _venueDropDownPlaceK: 0,
    _venueByLetterDropDownIsInitialised: false,
    _venueByLetterDropDownPlaceK: 0,
    _venueByLetterDropDownLetter: null,
    _venueDropDownPreviouslySelectedIndex: 0,
    
    get__venue: function Js_Controls_Picker_Controller$get__venue() {
        /// <value type="Js.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionVenue() || this._view.get_selectedVenueHidden().value == null || !this._view.get_selectedVenueHidden().value.length) {
            return null;
        }
        return Js.Controls.Picker.ObjectStub.fromString(this._view.get_selectedVenueHidden().value);
    },
    
    get__venueDropDownIsVenueSelectedCurrently: function Js_Controls_Picker_Controller$get__venueDropDownIsVenueSelectedCurrently() {
        /// <value type="Boolean"></value>
        return this.get__venueDropDownVenueSelectedCurrently() > 0;
    },
    
    get__venueDropDownVenueSelectedCurrently: function Js_Controls_Picker_Controller$get__venueDropDownVenueSelectedCurrently() {
        /// <value type="Number" integer="true"></value>
        return this._getK(this._view.get_venueDropDown());
    },
    
    get__venueDropDownIsLetterSelectedCurrently: function Js_Controls_Picker_Controller$get__venueDropDownIsLetterSelectedCurrently() {
        /// <value type="Boolean"></value>
        return this.get__venueDropDownLetterSelectedCurrently().length > 0;
    },
    
    get__venueDropDownLetterSelectedCurrently: function Js_Controls_Picker_Controller$get__venueDropDownLetterSelectedCurrently() {
        /// <value type="String"></value>
        var value = this._getValue(this._view.get_venueDropDown());
        if (value.indexOf('*') > -1) {
            return value.substr(value.length - 1, 1);
        }
        else {
            return '';
        }
    },
    
    get__venueByLetterDropDownIsVenueSelectedCurrently: function Js_Controls_Picker_Controller$get__venueByLetterDropDownIsVenueSelectedCurrently() {
        /// <value type="Boolean"></value>
        return this.get__venueByLetterDropDownVenueSelectedCurrently() > 0;
    },
    
    get__venueByLetterDropDownVenueSelectedCurrently: function Js_Controls_Picker_Controller$get__venueByLetterDropDownVenueSelectedCurrently() {
        /// <value type="Number" integer="true"></value>
        return this._getK(this._view.get_venueByLetterDropDown());
    },
    
    _initialiseVenue: function Js_Controls_Picker_Controller$_initialiseVenue() {
        if (!this.get__optionVenue()) {
            return;
        }
        this._view.get_venueDropDownJ().change(ss.Delegate.create(this, this._venueDropDownChange));
        this._view.get_venueByLetterDropDownJ().change(ss.Delegate.create(this, this._venueByLetterDropDownChange));
        this._view.get_venueSelectedChangeLinkJ().click(ss.Delegate.create(this, this._venueSelectedChangeLinkClick));
    },
    
    _venueSelectedChangeLinkClick: function Js_Controls_Picker_Controller$_venueSelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialiseVenueDropDown(null);
    },
    
    _initialiseVenueDropDown: function Js_Controls_Picker_Controller$_initialiseVenueDropDown(changeVenue) {
        /// <param name="changeVenue" type="Js.Controls.Picker.ObjectStub">
        /// </param>
        if (!this.get__optionVenue()) {
            return;
        }
        if (!this._venueDropDownIsInitialised || this._venueDropDownPlaceK !== this.get__place().k) {
            this._venueDropDownPlaceK = this.get__place().k;
            $(this._view.get_venueDropDown()).ajaxAddOption('/support/getcached.aspx?type=venue&placek=' + this.get__place().k, null, false, ss.Delegate.create(this, this._venueDropDownInitialised), [ changeVenue ]);
        }
        else {
            this._venueDropDownInitialised(changeVenue);
        }
    },
    
    _venueDropDownInitialised: function Js_Controls_Picker_Controller$_venueDropDownInitialised(args) {
        /// <param name="args" type="Object">
        /// </param>
        var changeVenue = args;
        this._venueDropDownIsInitialised = true;
        if (this.get__venue() != null) {
            var setValue = this._setK(this._view.get_venueDropDown(), this.get__venue().k);
            if (!setValue) {
                this._addHistorys([ 'Venue', '0', 'Event', '0' ]);
            }
        }
        if (changeVenue != null) {
            var setValue = this._setK(this._view.get_venueDropDown(), changeVenue.k);
            if (setValue) {
                this._venueDropDownChange(null);
            }
        }
        this._venueDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_venueDropDown());
        this._updateUIVenue(true);
    },
    
    _venueDropDownChange: function Js_Controls_Picker_Controller$_venueDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (this.get__venueDropDownIsVenueSelectedCurrently()) {
            var venue = new Js.Controls.Picker.ObjectStub(this.get__venueDropDownVenueSelectedCurrently(), this._getText(this._view.get_venueDropDown()));
            this._addHistory('Venue', venue.toString());
        }
        else if (this.get__venueDropDownIsLetterSelectedCurrently()) {
            var letter = this.get__venueDropDownLetterSelectedCurrently();
            if (!this._venueByLetterDropDownIsInitialised || this._venueByLetterDropDownPlaceK !== this.get__place().k || this._venueByLetterDropDownLetter !== letter) {
                this._venueByLetterDropDownPlaceK = this.get__place().k;
                this._venueByLetterDropDownLetter = letter;
                $(this._view.get_venueByLetterDropDown()).ajaxAddOption('/support/getcached.aspx?type=venuebyletter&placek=' + this.get__place().k + '&letter=' + letter, null, false, ss.Delegate.create(this, this._venueByLetterDropDownInitialised), null);
            }
            else {
                this._updateUIVenue(true);
            }
        }
        else {
            this._setIndex(this._view.get_venueDropDown(), this._venueDropDownPreviouslySelectedIndex);
        }
        this._venueDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_venueDropDown());
    },
    
    _venueByLetterDropDownChange: function Js_Controls_Picker_Controller$_venueByLetterDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (this.get__venueByLetterDropDownIsVenueSelectedCurrently()) {
            var venue = new Js.Controls.Picker.ObjectStub(this.get__venueByLetterDropDownVenueSelectedCurrently(), this._getText(this._view.get_venueByLetterDropDown()));
            this._addHistory('Venue', venue.toString());
        }
    },
    
    _venueByLetterDropDownInitialised: function Js_Controls_Picker_Controller$_venueByLetterDropDownInitialised(args) {
        /// <param name="args" type="Object">
        /// </param>
        this._venueByLetterDropDownIsInitialised = true;
        this._addHistorys([ 'Venue', '0', 'Event', '0' ]);
        this._updateUIVenue(true);
    },
    
    _navigateVenuePreviousValue: '',
    
    _navigateVenue: function Js_Controls_Picker_Controller$_navigateVenue(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionVenue() || !value.length || value === this._navigateVenuePreviousValue) {
            return;
        }
        if (value === '0') {
            if (!this.get__venueDropDownIsLetterSelectedCurrently()) {
                this._setIndex(this._view.get_venueDropDown(), 0);
            }
            this._view.get_venueSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedVenueHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedVenueChanged != null) {
                    this.selectedVenueChanged(this, new Js.Controls.Picker.ObjectArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var venue = Js.Controls.Picker.ObjectStub.fromString(value);
            if (this._venueDropDownIsInitialised && this._getK(this._view.get_venueDropDown()) !== venue.k) {
                this._setK(this._view.get_venueDropDown(), venue.k);
            }
            if (!this._venueDropDownIsInitialised) {
                this._view.get_venueSelectedLabel().innerHTML = venue.name;
            }
            this._view.get_selectedVenueHidden().value = venue.toString();
            if (!this._firstEverNavigate) {
                if (this.selectedVenueChanged != null) {
                    this.selectedVenueChanged(this, new Js.Controls.Picker.ObjectArgs(venue));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(new Js.Controls.Picker.EventSelectionSpecification((this.get__searchType() === 2) ? this.get__brand() : null, (this.get__searchType() === 1 || this.get__searchType() === 3) ? this.get__place() : null, (this.get__searchType() === 1) ? venue : null, (this.get__searchType() === 3) ? this.get__music() : null, this.get__date(), this.get__searchType() === 6)));
                }
            }
        }
        this._navigateVenuePreviousValue = value;
        this._updateUIVenue(true);
        this._initialiseDateDropDowns();
        this._initialiseEventDropDown();
    },
    
    _updateUIVenue: function Js_Controls_Picker_Controller$_updateUIVenue(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_venueHolder().style.display = (this.get__optionVenue() && this.get__searchType() === 1 && this.get__place() != null && (!this._venueDropDownIsInitialised || this._venueDropDownPlaceK === this.get__place().k)) ? '' : 'none';
        this._view.get_venueByLetterDropDown().style.display = (this.get__optionVenue() && this._venueByLetterDropDownIsInitialised && this.get__place() != null && this._venueByLetterDropDownPlaceK === this.get__place().k && this.get__venueDropDownIsLetterSelectedCurrently() && this._venueByLetterDropDownLetter === this.get__venueDropDownLetterSelectedCurrently()) ? '' : 'none';
        this._view.get_venueSelectedHolder().style.display = (!this._venueDropDownIsInitialised) ? '' : 'none';
        this._view.get_venueChoiceHolder().style.display = (this._venueDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIMusic(true);
        }
    },
    
    get__optionMusic: function Js_Controls_Picker_Controller$get__optionMusic() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionMusicHidden().value);
    },
    
    selectedMusicChanged: null,
    _musicDropDownIsInitialised: false,
    _musicDropDownPreviouslySelectedIndex: 0,
    
    get__music: function Js_Controls_Picker_Controller$get__music() {
        /// <value type="Js.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionMusic() || this._view.get_selectedMusicHidden().value == null || !this._view.get_selectedMusicHidden().value.length) {
            return null;
        }
        return Js.Controls.Picker.ObjectStub.fromString(this._view.get_selectedMusicHidden().value);
    },
    
    get__musicIsSelectedCurrently: function Js_Controls_Picker_Controller$get__musicIsSelectedCurrently() {
        /// <value type="Boolean"></value>
        return this.get__musicSelectedCurrently() > 0;
    },
    
    get__musicSelectedCurrently: function Js_Controls_Picker_Controller$get__musicSelectedCurrently() {
        /// <value type="Number" integer="true"></value>
        return this._getK(this._view.get_musicDropDown());
    },
    
    _initialiseMusic: function Js_Controls_Picker_Controller$_initialiseMusic() {
        if (!this.get__optionMusic()) {
            return;
        }
        this._view.get_musicDropDownJ().change(ss.Delegate.create(this, this._musicDropDownChange));
        this._view.get_musicSelectedChangeLinkJ().click(ss.Delegate.create(this, this._musicSelectedChangeLinkClick));
    },
    
    _musicSelectedChangeLinkClick: function Js_Controls_Picker_Controller$_musicSelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialiseMusicDropDown();
    },
    
    _initialiseMusicDropDown: function Js_Controls_Picker_Controller$_initialiseMusicDropDown() {
        if (!this.get__optionMusic()) {
            return;
        }
        if (!this._musicDropDownIsInitialised) {
            $(this._view.get_musicDropDown()).ajaxAddOption('/support/getcached.aspx?type=music', null, false, ss.Delegate.create(this, this._musicDropDownInitialised), null);
        }
        else {
            this._musicDropDownInitialised(null);
        }
    },
    
    _musicDropDownInitialised: function Js_Controls_Picker_Controller$_musicDropDownInitialised(args) {
        /// <param name="args" type="Object">
        /// </param>
        this._musicDropDownIsInitialised = true;
        if (this.get__music() != null) {
            var setValue = this._setK(this._view.get_musicDropDown(), this.get__music().k);
            if (!setValue) {
                this._addHistorys([ 'Music', '0', 'Event', '0' ]);
            }
        }
        this._musicDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_musicDropDown());
        this._updateUIMusic(true);
    },
    
    _musicDropDownChange: function Js_Controls_Picker_Controller$_musicDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (this.get__musicIsSelectedCurrently()) {
            var musicTypeName = this._getText(this._view.get_musicDropDown());
            if (musicTypeName.substr(0, 4) === '... ') {
                musicTypeName = musicTypeName.substr(4);
            }
            var music = new Js.Controls.Picker.ObjectStub(this.get__musicSelectedCurrently(), musicTypeName);
            this._addHistory('Music', music.toString());
        }
        else {
            this._setIndex(this._view.get_musicDropDown(), this._musicDropDownPreviouslySelectedIndex);
        }
        this._musicDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_musicDropDown());
    },
    
    _navigateMusicPreviousValue: '',
    
    _navigateMusic: function Js_Controls_Picker_Controller$_navigateMusic(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionMusic() || !value.length || value === this._navigateMusicPreviousValue) {
            return;
        }
        if (value === '0') {
            this._setIndex(this._view.get_musicDropDown(), 0);
            this._view.get_musicSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedMusicHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedMusicChanged != null) {
                    this.selectedMusicChanged(this, new Js.Controls.Picker.ObjectArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var music = Js.Controls.Picker.ObjectStub.fromString(value);
            if (this._musicDropDownIsInitialised && this._getK(this._view.get_musicDropDown()) !== music.k) {
                this._setK(this._view.get_musicDropDown(), music.k);
            }
            if (!this._musicDropDownIsInitialised) {
                this._view.get_musicSelectedLabel().innerHTML = music.name;
            }
            this._view.get_selectedMusicHidden().value = music.toString();
            if (!this._firstEverNavigate) {
                if (this.selectedMusicChanged != null) {
                    this.selectedMusicChanged(this, new Js.Controls.Picker.ObjectArgs(music));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(new Js.Controls.Picker.EventSelectionSpecification((this.get__searchType() === 2) ? this.get__brand() : null, (this.get__searchType() === 1 || this.get__searchType() === 3) ? this.get__place() : null, (this.get__searchType() === 1) ? this.get__venue() : null, (this.get__searchType() === 3) ? music : null, this.get__date(), this.get__searchType() === 6)));
                }
            }
        }
        this._navigateMusicPreviousValue = value;
        this._initialiseDateDropDowns();
    },
    
    _updateUIMusic: function Js_Controls_Picker_Controller$_updateUIMusic(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_musicHolder().style.display = (this.get__optionMusic() && this.get__searchType() === 3 && this.get__place() != null) ? '' : 'none';
        this._view.get_musicSelectedHolder().style.display = (!this._musicDropDownIsInitialised) ? '' : 'none';
        this._view.get_musicChoiceHolder().style.display = (this._musicDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIDate(true);
        }
    },
    
    get__optionDate: function Js_Controls_Picker_Controller$get__optionDate() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionDateHidden().value);
    },
    
    get__optionDateDay: function Js_Controls_Picker_Controller$get__optionDateDay() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionDateDayHidden().value);
    },
    
    get__optionDateDayIncrement: function Js_Controls_Picker_Controller$get__optionDateDayIncrement() {
        /// <value type="Number" integer="true"></value>
        return parseInt(this._view.get_optionDateDayIncrementHidden().value, 10);
    },
    
    selectedDateChanged: null,
    _dateDropDownsAreInitialised: false,
    
    get__date: function Js_Controls_Picker_Controller$get__date() {
        /// <value type="Js.Controls.Picker.DateStub"></value>
        if (!this.get__optionDate() || this._view.get_selectedDateHidden().value == null || !this._view.get_selectedDateHidden().value.length) {
            var d = new Date();
            return new Js.Controls.Picker.DateStub(d.getFullYear(), d.getMonth() + 1, (this.get__optionDateDay()) ? d.getDate() : 0);
        }
        var date = Js.Controls.Picker.DateStub.fromString(this._view.get_selectedDateHidden().value);
        if (!this.get__optionDateDay()) {
            date.day = 0;
        }
        return date;
    },
    
    _dateMonths: null,
    
    _initialiseDate: function Js_Controls_Picker_Controller$_initialiseDate() {
        if (!this.get__optionDate()) {
            return;
        }
        this._dateMonths = [ '', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec' ];
        this._view.get_dateDayDropDownJ().change(ss.Delegate.create(this, this._dateDayDropDownChange));
        this._view.get_dateMonthDropDownJ().change(ss.Delegate.create(this, this._dateMonthDropDownChange));
        this._view.get_dateYearTextBoxJ().change(ss.Delegate.create(this, this._dateYearTextBoxChange));
        this._view.get_dateYearTextBoxJ().keyup(ss.Delegate.create(this, this._dateYearTextBoxKeyUp));
        this._view.get_dateYearPlusImgJ().click(ss.Delegate.create(this, this._dateYearPlusClick));
        this._view.get_dateYearMinusImgJ().click(ss.Delegate.create(this, this._dateYearMinusClick));
        this._view.get_dateSelectedChangeLinkJ().click(ss.Delegate.create(this, this._dateSelectedChangeLinkClick));
        this._view.get_dateMonthPlusImgJ().click(ss.Delegate.create(this, this._dateMonthPlusClick));
        this._view.get_dateMonthMinusImgJ().click(ss.Delegate.create(this, this._dateMonthMinusClick));
        this._view.get_dateDayPlusImgJ().click(ss.Delegate.create(this, this._dateDayPlusClick));
        this._view.get_dateDayMinusImgJ().click(ss.Delegate.create(this, this._dateDayMinusClick));
    },
    
    _dateSelectedChangeLinkClick: function Js_Controls_Picker_Controller$_dateSelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialiseDateDropDowns();
    },
    
    _initialiseDateDropDowns: function Js_Controls_Picker_Controller$_initialiseDateDropDowns() {
        if (!this.get__optionDate()) {
            return;
        }
        if (!this._dateDropDownsAreInitialised) {
            for (var i = 1; i <= 12; i++) {
                this._addOption(this._view.get_dateMonthDropDown(), i.toString(), this._dateMonths[i]);
            }
            var d = this.get__date();
            this._view.get_dateYearTextBox().value = d.year.toString();
            this._setValue(this._view.get_dateMonthDropDown(), d.month.toString());
            this._dateRefreshDays(d.year, d.month);
            this._setValue(this._view.get_dateDayDropDown(), d.day.toString());
            this._dateDropDownsAreInitialised = true;
        }
        this._updateUIDate(true);
    },
    
    _dateRefreshDaysHasRunBefore: false,
    _dateRefreshDaysPreviousNumberOfDaysInMonth: 0,
    _dateRefreshDaysPreviousFirstDayOfWeek: 0,
    
    _dateRefreshDays: function Js_Controls_Picker_Controller$_dateRefreshDays(year, month) {
        /// <param name="year" type="Number" integer="true">
        /// </param>
        /// <param name="month" type="Number" integer="true">
        /// </param>
        if (!this.get__optionDateDay()) {
            return;
        }
        var days = Js.Controls.Picker.DateStub.daysInMonth(year, month);
        var firstDayOfWeek = new Date(year, month - 1, 1).getDay();
        if (!this._dateRefreshDaysHasRunBefore || this._dateRefreshDaysPreviousNumberOfDaysInMonth !== days || this._dateRefreshDaysPreviousFirstDayOfWeek !== firstDayOfWeek) {
            this._dateRefreshDaysPreviousNumberOfDaysInMonth = days;
            this._dateRefreshDaysPreviousFirstDayOfWeek = firstDayOfWeek;
            var previouslySelectedIndex = this._getIndex(this._view.get_dateDayDropDown());
            $(this._view.get_dateDayDropDown()).empty();
            for (var i = 1; i <= days; i++) {
                var dayOfWeek = new Date(year, month - 1, i).getDay();
                this._addOption(this._view.get_dateDayDropDown(), i.toString(), ((i < 10) ? '0' : '') + i.toString() + ((dayOfWeek === 6) ? ' Sat' : (!dayOfWeek) ? ' Sun' : ''));
            }
            if (previouslySelectedIndex > -1) {
                this._setIndex(this._view.get_dateDayDropDown(), previouslySelectedIndex);
            }
            this._dateRefreshDaysHasRunBefore = true;
        }
    },
    
    _dateDayDropDownChange: function Js_Controls_Picker_Controller$_dateDayDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var newDay = this._getValueInt(this._view.get_dateDayDropDown());
        var newDate = new Js.Controls.Picker.DateStub(this.get__date().year, this.get__date().month, newDay);
        this._addHistory('Date', newDate.toString());
    },
    
    _dateMonthDropDownChange: function Js_Controls_Picker_Controller$_dateMonthDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var newMonth = this._getValueInt(this._view.get_dateMonthDropDown());
        this._dateRefreshDays(this.get__date().year, newMonth);
        var newDay = (this.get__optionDateDay()) ? this._getValueInt(this._view.get_dateDayDropDown()) : 0;
        var newDate = new Js.Controls.Picker.DateStub(this.get__date().year, newMonth, newDay);
        this._addHistory('Date', newDate.toString());
    },
    
    _dateYearTextBoxKeyUp: function Js_Controls_Picker_Controller$_dateYearTextBoxKeyUp(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._dateYearTextBoxKeyUpChange(false);
    },
    
    _dateYearTextBoxChange: function Js_Controls_Picker_Controller$_dateYearTextBoxChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._dateYearTextBoxKeyUpChange(true);
    },
    
    _dateYearTextBoxKeyUpChange: function Js_Controls_Picker_Controller$_dateYearTextBoxKeyUpChange(change) {
        /// <param name="change" type="Boolean">
        /// </param>
        try {
            var regex = new RegExp('[^0-9]', 'g');
            if (!regex.test(this._view.get_dateYearTextBox().value)) {
                var newYear = parseInt(this._view.get_dateYearTextBox().value, 10);
                if (newYear > 1900 && newYear < 2100) {
                    if (newYear !== this.get__date().year) {
                        this._dateRefreshDays(newYear, this.get__date().month);
                        var newDay = (this.get__optionDateDay()) ? this._getValueInt(this._view.get_dateDayDropDown()) : 0;
                        var newDate = new Js.Controls.Picker.DateStub(newYear, this.get__date().month, newDay);
                        this._addHistory('Date', newDate.toString());
                    }
                }
                else {
                    if (change) {
                        this._view.get_dateYearTextBox().value = this.get__date().year.toString();
                    }
                }
            }
            else {
                if (change) {
                    this._view.get_dateYearTextBox().value = this.get__date().year.toString();
                }
            }
        }
        catch ($e1) {
            if (change) {
                this._view.get_dateYearTextBox().value = this.get__date().year.toString();
            }
        }
    },
    
    _dateYearPlusClick: function Js_Controls_Picker_Controller$_dateYearPlusClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._datePlusMinus(1, 'y');
    },
    
    _dateYearMinusClick: function Js_Controls_Picker_Controller$_dateYearMinusClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._datePlusMinus(-1, 'y');
    },
    
    _dateMonthPlusClick: function Js_Controls_Picker_Controller$_dateMonthPlusClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._datePlusMinus(1, 'm');
    },
    
    _dateMonthMinusClick: function Js_Controls_Picker_Controller$_dateMonthMinusClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._datePlusMinus(-1, 'm');
    },
    
    _dateDayPlusClick: function Js_Controls_Picker_Controller$_dateDayPlusClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._datePlusMinus(this.get__optionDateDayIncrement(), 'd');
    },
    
    _dateDayMinusClick: function Js_Controls_Picker_Controller$_dateDayMinusClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._datePlusMinus(-this.get__optionDateDayIncrement(), 'd');
    },
    
    _dateChangePrivate: function Js_Controls_Picker_Controller$_dateChangePrivate(newDate) {
        /// <param name="newDate" type="Js.Controls.Picker.DateStub">
        /// </param>
        var oldDate = new Js.Controls.Picker.DateStub(this.get__date().year, this.get__date().month, this.get__date().day);
        if (newDate.year !== oldDate.year || newDate.month !== oldDate.month) {
            this._dateRefreshDays(newDate.year, newDate.month);
        }
        if (newDate.day !== oldDate.day) {
            this._setValue(this._view.get_dateDayDropDown(), newDate.day.toString());
        }
        if (newDate.month !== oldDate.month) {
            this._setValue(this._view.get_dateMonthDropDown(), newDate.month.toString());
        }
        if (newDate.year !== oldDate.year) {
            this._view.get_dateYearTextBox().value = newDate.year.toString();
        }
        if (!this.get__optionDateDay() && newDate.day > 0) {
            newDate.day = 0;
        }
        this._addHistory('Date', newDate.toString());
    },
    
    _datePlusMinus: function Js_Controls_Picker_Controller$_datePlusMinus(modifier, unit) {
        /// <param name="modifier" type="Number" integer="true">
        /// </param>
        /// <param name="unit" type="String">
        /// </param>
        /// <returns type="Js.Controls.Picker.DateStub"></returns>
        var newDate = new Js.Controls.Picker.DateStub(this.get__date().year, this.get__date().month, this.get__date().day).modify(modifier, unit);
        this._dateChangePrivate(newDate);
        return newDate;
    },
    
    dateModify: function Js_Controls_Picker_Controller$dateModify(modifier, unit) {
        /// <param name="modifier" type="Number" integer="true">
        /// </param>
        /// <param name="unit" type="String">
        /// </param>
        /// <returns type="Js.Controls.Picker.DateStub"></returns>
        return this._datePlusMinus(modifier, unit);
    },
    
    dateChange: function Js_Controls_Picker_Controller$dateChange(newDate) {
        /// <param name="newDate" type="Js.Controls.Picker.DateStub">
        /// </param>
        this._dateChangePrivate(newDate);
    },
    
    _navigateDatePreviousValue: '',
    
    _navigateDate: function Js_Controls_Picker_Controller$_navigateDate(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionDate() || !value.length || value === this._navigateDatePreviousValue) {
            return;
        }
        if (value === '0') {
            this._view.get_dateSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedDateHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedDateChanged != null) {
                    this.selectedDateChanged(this, new Js.Controls.Picker.DateArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var date = Js.Controls.Picker.DateStub.fromString(value);
            if (this._dateDropDownsAreInitialised) {
                this._view.get_dateYearTextBox().value = date.year.toString();
                this._setValue(this._view.get_dateMonthDropDown(), date.month.toString());
                this._dateRefreshDays(date.year, date.month);
                this._setValue(this._view.get_dateDayDropDown(), date.day.toString());
            }
            if (!this._dateDropDownsAreInitialised) {
                this._view.get_dateSelectedLabel().innerHTML = date.toFriendlyString();
            }
            this._view.get_selectedDateHidden().value = date.toString();
            if (!this._firstEverNavigate) {
                if (this.selectedDateChanged != null) {
                    this.selectedDateChanged(this, new Js.Controls.Picker.DateArgs(date));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new Js.Controls.Picker.EventSelectionArgs(new Js.Controls.Picker.EventSelectionSpecification((this.get__searchType() === 2) ? this.get__brand() : null, (this.get__searchType() === 1 || this.get__searchType() === 3) ? this.get__place() : null, (this.get__searchType() === 1) ? this.get__venue() : null, (this.get__searchType() === 3) ? this.get__music() : null, date, this.get__searchType() === 6)));
                }
            }
        }
        this._navigateDatePreviousValue = value;
        this._initialiseEventDropDown();
    },
    
    _updateUIDate: function Js_Controls_Picker_Controller$_updateUIDate(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_dateHolder().style.display = (this.get__optionDate() && ((this.get__searchType() === 1 && this.get__venue() != null) || (this.get__searchType() === 1 && !this.get__optionVenue() && this.get__place() != null) || (this.get__searchType() === 3 && this.get__place() != null && this.get__music() != null) || (this.get__searchType() === 2 && this.get__brand() != null))) ? '' : 'none';
        this._view.get_dateDayHolder().style.display = (this.get__optionDateDay()) ? '' : 'none';
        this._view.get_dateSelectedHolder().style.display = (!this._dateDropDownsAreInitialised) ? '' : 'none';
        this._view.get_dateChoiceHolder().style.display = (this._dateDropDownsAreInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIEvent();
        }
    },
    
    get__optionEvent: function Js_Controls_Picker_Controller$get__optionEvent() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionEventHidden().value);
    },
    
    selectedEventChanged: null,
    _eventDropDownIsInitialised: false,
    _eventDropDownVenueK: 0,
    _eventDropDownBrandK: 0,
    _eventDropDownKey: null,
    _eventDropDownDate: null,
    _eventDropDownPreviouslySelectedIndex: 0,
    
    get__event: function Js_Controls_Picker_Controller$get__event() {
        /// <value type="Js.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionEvent() || this._view.get_selectedEventHidden().value == null || !this._view.get_selectedEventHidden().value.length) {
            return null;
        }
        return Js.Controls.Picker.ObjectStub.fromString(this._view.get_selectedEventHidden().value);
    },
    
    _initialiseEvent: function Js_Controls_Picker_Controller$_initialiseEvent() {
        if (!this.get__optionEvent()) {
            return;
        }
        this._view.get_eventListBoxJ().change(ss.Delegate.create(this, this._eventDropDownChange));
        this._view.get_eventSelectedChangeLinkJ().click(ss.Delegate.create(this, this._eventSelectedChangeLinkClick));
    },
    
    _eventSelectedChangeLinkClick: function Js_Controls_Picker_Controller$_eventSelectedChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._initialiseEventDropDown();
    },
    
    _initialiseEventDropDown: function Js_Controls_Picker_Controller$_initialiseEventDropDown() {
        if (!this.get__optionEvent()) {
            return;
        }
        if (!this._eventDropDownIsInitialised || ((this.get__searchType() === 1 || this.get__searchType() === 3) && this.get__venue() != null && this._eventDropDownVenueK !== this.get__venue().k) || (this.get__searchType() === 2 && this.get__brand() != null && this._eventDropDownBrandK !== this.get__brand().k) || (this.get__searchType() === 7 && this.get__key() != null && this._eventDropDownKey !== this.get__key()) || this._eventDropDownDate.year !== this.get__date().year || this._eventDropDownDate.month !== this.get__date().month) {
            if ((this.get__searchType() === 1 || this.get__searchType() === 3) && this.get__venue() != null && this.get__venue().k === 1) {
                return;
            }
            this._eventDropDownDate = this.get__date();
            this._eventDropDownVenueK = (this.get__venue() != null && (this.get__searchType() === 1 || this.get__searchType() === 3)) ? this.get__venue().k : 0;
            this._eventDropDownBrandK = (this.get__brand() != null && this.get__searchType() === 2) ? this.get__brand().k : 0;
            this._eventDropDownKey = (this.get__key() != null && this.get__searchType() === 7) ? this.get__key() : '0';
            if (!this._eventDropDownVenueK && !this._eventDropDownBrandK && this._eventDropDownKey === '0') {
                return;
            }
            this._requestId++;
            var currentRequestId = this._requestId;
            var currentLoadId = this._loadId;
            $(this._view.get_eventListBoxJ()).ajaxAddOption1('/support/getcached.aspx?type=event&key=' + this._eventDropDownKey + '&venuek=' + this._eventDropDownVenueK + '&brandk=' + this._eventDropDownBrandK + '&date=' + this._eventDropDownDate.toString(), null, false, ss.Delegate.create(this, this._eventDropDownInitialised), [ currentRequestId ]);
            window.setTimeout(ss.Delegate.create(this, function() {
                if (this._loadId === currentLoadId) {
                    this._view.get_eventListBoxJ().empty();
                    this._addOption(this._view.get_eventListBox(), '0', 'Loading...');
                }
            }), 100);
        }
        else {
            this._eventDropDownConfigure();
        }
    },
    
    _requestId: 0,
    _loadId: 0,
    
    _eventDropDownInitialised: function Js_Controls_Picker_Controller$_eventDropDownInitialised(args, data) {
        /// <param name="args" type="Object">
        /// </param>
        /// <param name="data" type="Object">
        /// </param>
        var requestIdFromArgs = parseInt(args.toString(), 10);
        if (this._requestId === requestIdFromArgs) {
            this._loadId++;
            this._view.get_eventListBoxJ().empty();
            $(this._view.get_eventListBox()).addOption(data, false);
            this._eventDropDownConfigure();
        }
    },
    
    _eventDropDownConfigure: function Js_Controls_Picker_Controller$_eventDropDownConfigure() {
        this._eventDropDownIsInitialised = true;
        this._view.get_eventListBox().size = (this._view.get_eventListBox().options.length < 3) ? 3 : this._view.get_eventListBox().options.length;
        if (this.get__event() != null) {
            var setValue = this._setK(this._view.get_eventListBox(), this.get__event().k);
            if (!setValue) {
                this._addHistory('Event', '0');
                this._unSelect(this._view.get_eventListBox());
            }
        }
        this._eventDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_eventListBox());
        this._updateUIEvent();
    },
    
    _eventDropDownChange: function Js_Controls_Picker_Controller$_eventDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var k = this._getK(this._view.get_eventListBox());
        if (k > 0) {
            var _event = new Js.Controls.Picker.ObjectStub(this._getK(this._view.get_eventListBox()), this._getText(this._view.get_eventListBox()));
            this._addHistory('Event', _event.toString());
        }
        else {
            this._setIndex(this._view.get_eventListBox(), this._eventDropDownPreviouslySelectedIndex);
        }
        this._eventDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_eventListBox());
    },
    
    _navigateEventPreviousValue: '',
    
    _navigateEvent: function Js_Controls_Picker_Controller$_navigateEvent(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionEvent() || !value.length || value === this._navigateEventPreviousValue) {
            return;
        }
        if (value === '0') {
            this._setIndex(this._view.get_eventListBox(), 0);
            this._view.get_eventSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedEventHidden().value = '';
            if (this.selectedEventChanged != null) {
                this.selectedEventChanged(this, new Js.Controls.Picker.ObjectArgs(null));
            }
        }
        else {
            var _event = Js.Controls.Picker.ObjectStub.fromString(value);
            if (this._eventDropDownIsInitialised && this._getK(this._view.get_eventListBox()) !== _event.k) {
                this._setK(this._view.get_eventListBox(), _event.k);
            }
            if (!this._eventDropDownIsInitialised) {
                this._view.get_eventSelectedLabel().innerHTML = _event.name.substr(11);
            }
            this._view.get_selectedEventHidden().value = _event.toString();
            if (this.selectedEventChanged != null) {
                this.selectedEventChanged(this, new Js.Controls.Picker.ObjectArgs(_event));
            }
        }
        this._navigateEventPreviousValue = value;
    },
    
    _updateUIEvent: function Js_Controls_Picker_Controller$_updateUIEvent() {
        this._view.get_eventHolder().style.display = (this.get__optionEvent() && (((this.get__searchType() === 1 || this.get__searchType() === 3) && this.get__venue() != null && this.get__venue().k > 1) || (this.get__searchType() === 2 && this.get__brand() != null && this.get__brand().k > 1) || (this.get__searchType() === 7 && this.get__key() != null && this.get__key().length > 0))) ? '' : 'none';
        this._view.get_eventSelectedHolder().style.display = (!this._eventDropDownIsInitialised) ? '' : 'none';
        this._view.get_eventChoiceHolder().style.display = (this._eventDropDownIsInitialised) ? '' : 'none';
    },
    
    get__optionGoogle: function Js_Controls_Picker_Controller$get__optionGoogle() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionGoogleHidden().value);
    },
    
    _addOption: function Js_Controls_Picker_Controller$_addOption(sel, key, value) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        var op = document.createElement('OPTION');
        op.text = value;
        op.value = key;
        try {
            sel.add(op, null);
        }
        catch ($e1) {
            sel.add(op);
        }
    },
    
    _addHistory: function Js_Controls_Picker_Controller$_addHistory(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        if (key.length > 0) {
            this._firstEverNavigate = false;
        }
        var d = {};
        if (key.length > 0) {
            d[this._view.clientId + '_' + key] = encodeURI(value).replace(this._regexQuote, '&#39;');
        }
        $.bbq.pushState(d);
    },
    
    _addHistorys: function Js_Controls_Picker_Controller$_addHistorys(keysAndValues) {
        /// <param name="keysAndValues" type="Array" elementType="String">
        /// </param>
        this._firstEverNavigate = false;
        var d = {};
        for (var i = 0; i < keysAndValues.length; i = i + 2) {
            d[this._view.clientId + '_' + keysAndValues[i]] = encodeURI(keysAndValues[i + 1]).replace(this._regexQuote, '&#39;');
        }
        $.bbq.pushState(d);
    },
    
    _getValue: function Js_Controls_Picker_Controller$_getValue(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="String"></returns>
        return (sel.selectedIndex === -1) ? '' : (sel.options[sel.selectedIndex]).value;
    },
    
    _getValueInt: function Js_Controls_Picker_Controller$_getValueInt(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return (sel.selectedIndex === -1) ? 0 : parseInt((sel.options[sel.selectedIndex]).value, 10);
    },
    
    _getIndex: function Js_Controls_Picker_Controller$_getIndex(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return sel.selectedIndex;
    },
    
    _getK: function Js_Controls_Picker_Controller$_getK(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        try {
            var value = (sel.options[sel.selectedIndex]).value;
            value = value.substr(5, value.length - 5);
            return parseInt(value, 10);
        }
        catch ($e1) {
            return 0;
        }
    },
    
    _getText: function Js_Controls_Picker_Controller$_getText(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="String"></returns>
        return (sel.options[sel.selectedIndex]).text;
    },
    
    _setIndex: function Js_Controls_Picker_Controller$_setIndex(sel, index) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <param name="index" type="Number" integer="true">
        /// </param>
        try {
            var op = (index > sel.options.length - 1) ? sel.options[sel.options.length - 1] : sel.options[index];
            op.selected = true;
        }
        catch ($e1) {
        }
    },
    
    _unSelect: function Js_Controls_Picker_Controller$_unSelect(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        for (var i = 0; i < sel.options.length; i++) {
            var op = sel.options[i];
            op.selected = false;
        }
    },
    
    _setValue: function Js_Controls_Picker_Controller$_setValue(sel, value) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        for (var i = 0; i < sel.options.length; i++) {
            var op = sel.options[i];
            if (op.value.toLowerCase() === value.toLowerCase()) {
                op.selected = true;
                return true;
            }
        }
        return false;
    },
    
    _setK: function Js_Controls_Picker_Controller$_setK(sel, value) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <param name="value" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        for (var i = 0; i < sel.options.length; i++) {
            var op = sel.options[i];
            if (op.value.substr(5, op.value.length - 5).toLowerCase() === value.toString().toLowerCase()) {
                op.selected = true;
                return true;
            }
        }
        return false;
    },
    
    _debugCount: 0,
    
    debug: function Js_Controls_Picker_Controller$debug(text) {
        /// <param name="text" type="String">
        /// </param>
        this._view.get_debug().style.display = '';
        this._debugCount++;
        this._view.get_debug().value = this._debugCount.toString() + ' ' + text + '\n' + this._view.get_debug().value;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.DateStub

Js.Controls.Picker.DateStub = function Js_Controls_Picker_DateStub(year, month, day) {
    /// <param name="year" type="Number" integer="true">
    /// </param>
    /// <param name="month" type="Number" integer="true">
    /// </param>
    /// <param name="day" type="Number" integer="true">
    /// </param>
    /// <field name="year" type="Number" integer="true">
    /// </field>
    /// <field name="month" type="Number" integer="true">
    /// </field>
    /// <field name="day" type="Number" integer="true">
    /// </field>
    /// <field name="_debugCount" type="Number" integer="true">
    /// </field>
    this.year = year;
    this.month = month;
    this.day = day;
}
Js.Controls.Picker.DateStub.fromString = function Js_Controls_Picker_DateStub$fromString(value) {
    /// <param name="value" type="String">
    /// </param>
    /// <returns type="Js.Controls.Picker.DateStub"></returns>
    var year = parseInt(value.substr(0, 4), 10);
    var month = parseInt(value.substr(4, 2), 10);
    var day = parseInt(value.substr(6, 2), 10);
    return new Js.Controls.Picker.DateStub(year, month, day);
}
Js.Controls.Picker.DateStub.daysInMonth = function Js_Controls_Picker_DateStub$daysInMonth(year, month) {
    /// <param name="year" type="Number" integer="true">
    /// </param>
    /// <param name="month" type="Number" integer="true">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var m = [ 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 ];
    if (month !== 2) {
        return m[month - 1];
    }
    if (!!(year % 4)) {
        return m[1];
    }
    if (!(year % 100) && !!(year % 400)) {
        return m[1];
    }
    return m[1] + 1;
}
Js.Controls.Picker.DateStub.prototype = {
    year: 0,
    month: 0,
    day: 0,
    
    toString: function Js_Controls_Picker_DateStub$toString() {
        /// <returns type="String"></returns>
        return this.year.toString() + ((this.month < 10) ? '0' : '') + this.month.toString() + ((this.day < 10) ? '0' : '') + this.day.toString();
    },
    
    toFriendlyString: function Js_Controls_Picker_DateStub$toFriendlyString() {
        /// <returns type="String"></returns>
        return this.year.toString() + '-' + ((this.month < 10) ? '0' : '') + this.month.toString() + ((this.day > 0) ? ('-' + ((this.day < 10) ? '0' : '') + this.day.toString()) : '');
    },
    
    toDateTime: function Js_Controls_Picker_DateStub$toDateTime() {
        /// <returns type="Date"></returns>
        return new Date(this.year, this.month - 1, this.day);
    },
    
    modify: function Js_Controls_Picker_DateStub$modify(modifier, unit) {
        /// <param name="modifier" type="Number" integer="true">
        /// </param>
        /// <param name="unit" type="String">
        /// </param>
        /// <returns type="Js.Controls.Picker.DateStub"></returns>
        var newDate = new Js.Controls.Picker.DateStub(this.year, this.month, this.day);
        if (unit === 'y') {
            newDate.year = this.year + modifier;
        }
        else if (unit === 'm') {
            if (modifier > 0) {
                if (this.month + modifier > 12) {
                    newDate.month = this.month + modifier - 12;
                    newDate.year++;
                }
                else {
                    newDate.month = this.month + modifier;
                }
            }
            else {
                if (this.month + modifier < 1) {
                    newDate.month = 12 - (this.month + modifier);
                    newDate.year--;
                }
                else {
                    newDate.month = this.month + modifier;
                }
            }
        }
        else if (unit === 'd') {
            var daysInOldMonth = Js.Controls.Picker.DateStub.daysInMonth(this.year, this.month);
            if (modifier > 0) {
                if (this.day + modifier > daysInOldMonth) {
                    if (this.month === 12) {
                        newDate.year++;
                        newDate.month = 1;
                        newDate.day = this.day + modifier - daysInOldMonth;
                    }
                    else {
                        newDate.month++;
                        newDate.day = this.day + modifier - daysInOldMonth;
                    }
                }
                else {
                    newDate.day = this.day + modifier;
                }
            }
            else {
                if (this.day + modifier < 1) {
                    if (this.month === 1) {
                        newDate.year--;
                        newDate.month = 12;
                        newDate.day = Js.Controls.Picker.DateStub.daysInMonth(newDate.year, newDate.month) + this.day + modifier;
                    }
                    else {
                        newDate.month--;
                        newDate.day = Js.Controls.Picker.DateStub.daysInMonth(newDate.year, newDate.month) + this.day + modifier;
                    }
                }
                else {
                    newDate.day = this.day + modifier;
                }
            }
        }
        return newDate;
    },
    
    dayOfWeek: function Js_Controls_Picker_DateStub$dayOfWeek() {
        /// <summary>
        /// Week day
        /// </summary>
        /// <returns type="Number" integer="true"></returns>
        return this.toDateTime().getDay();
    },
    
    previousMonday: function Js_Controls_Picker_DateStub$previousMonday() {
        /// <returns type="Js.Controls.Picker.DateStub"></returns>
        var d = this.dayOfWeek();
        var modifier = (!d) ? -6 : (d === 1) ? 0 : (d === 2) ? -1 : (d === 3) ? -2 : (d === 4) ? -3 : (d === 5) ? -4 : (d === 6) ? -5 : 0;
        return this.modify(modifier, 'd');
    },
    
    nextSunday: function Js_Controls_Picker_DateStub$nextSunday() {
        /// <returns type="Js.Controls.Picker.DateStub"></returns>
        var d = this.dayOfWeek();
        var modifier = (!d) ? 0 : (d === 1) ? 6 : (d === 2) ? 5 : (d === 3) ? 4 : (d === 4) ? 3 : (d === 5) ? 2 : (d === 6) ? 1 : 0;
        return this.modify(modifier, 'd');
    },
    
    get_monthNameFull: function Js_Controls_Picker_DateStub$get_monthNameFull() {
        /// <value type="String"></value>
        var m = this.month;
        return (m === 1) ? 'January' : (m === 2) ? 'February' : (m === 3) ? 'March' : (m === 4) ? 'April' : (m === 5) ? 'May' : (m === 6) ? 'June' : (m === 7) ? 'July' : (m === 8) ? 'August' : (m === 9) ? 'September' : (m === 10) ? 'October' : (m === 11) ? 'November' : (m === 12) ? 'December' : '';
    },
    
    _debugCount: 0,
    
    _debug: function Js_Controls_Picker_DateStub$_debug(text) {
        /// <param name="text" type="String">
        /// </param>
        var debug = document.getElementById('Content_Debug');
        debug.style.display = '';
        this._debugCount++;
        debug.value = this._debugCount.toString() + ' ' + text + '\n' + debug.value;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.ObjectStub

Js.Controls.Picker.ObjectStub = function Js_Controls_Picker_ObjectStub(k, name) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    /// <param name="name" type="String">
    /// </param>
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
    this.k = k;
    this.name = name;
}
Js.Controls.Picker.ObjectStub.fromString = function Js_Controls_Picker_ObjectStub$fromString(value) {
    /// <param name="value" type="String">
    /// </param>
    /// <returns type="Js.Controls.Picker.ObjectStub"></returns>
    if (value == null || !value.length || value === '0') {
        return null;
    }
    var k = parseInt(value.substr(0, value.indexOf('-')), 10);
    var name = value.substr(value.indexOf('-') + 1, value.length - value.indexOf('-') - 1);
    return new Js.Controls.Picker.ObjectStub(k, name);
}
Js.Controls.Picker.ObjectStub.prototype = {
    name: null,
    k: 0,
    
    toString: function Js_Controls_Picker_ObjectStub$toString() {
        /// <returns type="String"></returns>
        return this.k.toString() + '-' + this.name;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.EventSelectionSpecification

Js.Controls.Picker.EventSelectionSpecification = function Js_Controls_Picker_EventSelectionSpecification(brand, place, venue, music, date, me) {
    /// <param name="brand" type="Js.Controls.Picker.ObjectStub">
    /// </param>
    /// <param name="place" type="Js.Controls.Picker.ObjectStub">
    /// </param>
    /// <param name="venue" type="Js.Controls.Picker.ObjectStub">
    /// </param>
    /// <param name="music" type="Js.Controls.Picker.ObjectStub">
    /// </param>
    /// <param name="date" type="Js.Controls.Picker.DateStub">
    /// </param>
    /// <param name="me" type="Boolean">
    /// </param>
    /// <field name="brand" type="Js.Controls.Picker.ObjectStub">
    /// </field>
    /// <field name="place" type="Js.Controls.Picker.ObjectStub">
    /// </field>
    /// <field name="venue" type="Js.Controls.Picker.ObjectStub">
    /// </field>
    /// <field name="music" type="Js.Controls.Picker.ObjectStub">
    /// </field>
    /// <field name="date" type="Js.Controls.Picker.DateStub">
    /// </field>
    /// <field name="me" type="Boolean">
    /// </field>
    this.brand = brand;
    this.place = place;
    this.venue = venue;
    this.music = music;
    this.date = date;
    this.me = me;
}
Js.Controls.Picker.EventSelectionSpecification.prototype = {
    brand: null,
    place: null,
    venue: null,
    music: null,
    date: null,
    me: false,
    
    toString: function Js_Controls_Picker_EventSelectionSpecification$toString() {
        /// <returns type="String"></returns>
        return 'Brand: ' + ((this.brand == null) ? 'null' : this.brand.toString()) + '<br />' + 'Place: ' + ((this.place == null) ? 'null' : this.place.toString()) + '<br />' + 'Venue: ' + ((this.venue == null) ? 'null' : this.venue.toString()) + '<br />' + 'Music: ' + ((this.music == null) ? 'null' : this.music.toString()) + '<br />' + 'Date: ' + ((this.date == null) ? 'null' : this.date.toFriendlyString()) + '<br />' + 'Me: ' + this.me.toString().toLowerCase();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.StringArgs

Js.Controls.Picker.StringArgs = function Js_Controls_Picker_StringArgs(val) {
    /// <param name="val" type="String">
    /// </param>
    /// <field name="val" type="String">
    /// </field>
    Js.Controls.Picker.StringArgs.initializeBase(this);
    this.val = val;
}
Js.Controls.Picker.StringArgs.prototype = {
    val: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.ObjectArgs

Js.Controls.Picker.ObjectArgs = function Js_Controls_Picker_ObjectArgs(ob) {
    /// <param name="ob" type="Js.Controls.Picker.ObjectStub">
    /// </param>
    /// <field name="object" type="Js.Controls.Picker.ObjectStub">
    /// </field>
    Js.Controls.Picker.ObjectArgs.initializeBase(this);
    this.object = ob;
}
Js.Controls.Picker.ObjectArgs.prototype = {
    object: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.DateArgs

Js.Controls.Picker.DateArgs = function Js_Controls_Picker_DateArgs(d) {
    /// <param name="d" type="Js.Controls.Picker.DateStub">
    /// </param>
    /// <field name="date" type="Js.Controls.Picker.DateStub">
    /// </field>
    Js.Controls.Picker.DateArgs.initializeBase(this);
    this.date = d;
}
Js.Controls.Picker.DateArgs.prototype = {
    date: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.EventSelectionArgs

Js.Controls.Picker.EventSelectionArgs = function Js_Controls_Picker_EventSelectionArgs(specification) {
    /// <param name="specification" type="Js.Controls.Picker.EventSelectionSpecification">
    /// </param>
    /// <field name="specification" type="Js.Controls.Picker.EventSelectionSpecification">
    /// </field>
    Js.Controls.Picker.EventSelectionArgs.initializeBase(this);
    this.specification = specification;
}
Js.Controls.Picker.EventSelectionArgs.prototype = {
    specification: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Picker.View

Js.Controls.Picker.View = function Js_Controls_Picker_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_Debug" type="Object" domElement="true">
    /// </field>
    /// <field name="_DebugJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedSearchTypeHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedSearchTypeHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedKeyHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedKeyHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedSpotterHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedSpotterHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedBrandHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedBrandHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedCountryHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedCountryHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedPlaceHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedPlaceHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedVenueHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedVenueHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedMusicHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedMusicHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedDateHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedDateHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_SelectedEventHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_SelectedEventHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionKeyHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionKeyHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionMeHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionMeHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionSpotterHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionSpotterHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionBrandHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionBrandHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionCountryHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionCountryHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionPlaceHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionPlaceHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionVenueHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionVenueHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionMusicHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionMusicHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionDateHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionDateHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionDateDayHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionDateDayHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionDateDayIncrementHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionDateDayIncrementHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionEventHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionEventHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_OptionGoogleHidden" type="Object" domElement="true">
    /// </field>
    /// <field name="_OptionGoogleHiddenJ" type="jQueryObject">
    /// </field>
    /// <field name="_Holder" type="Object" domElement="true">
    /// </field>
    /// <field name="_HolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_SearchTypeHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_SearchTypeHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_SearchTypeKey" type="Object" domElement="true">
    /// </field>
    /// <field name="_SearchTypeKeyJ" type="jQueryObject">
    /// </field>
    /// <field name="_SearchTypeSpotter" type="Object" domElement="true">
    /// </field>
    /// <field name="_SearchTypeSpotterJ" type="jQueryObject">
    /// </field>
    /// <field name="_SearchTypeMe" type="Object" domElement="true">
    /// </field>
    /// <field name="_SearchTypeMeJ" type="jQueryObject">
    /// </field>
    /// <field name="_SearchTypeVenue" type="Object" domElement="true">
    /// </field>
    /// <field name="_SearchTypeVenueJ" type="jQueryObject">
    /// </field>
    /// <field name="_SearchTypeBrand" type="Object" domElement="true">
    /// </field>
    /// <field name="_SearchTypeBrandJ" type="jQueryObject">
    /// </field>
    /// <field name="_SearchTypeMusic" type="Object" domElement="true">
    /// </field>
    /// <field name="_SearchTypeMusicJ" type="jQueryObject">
    /// </field>
    /// <field name="_SearchTypeGoogle" type="Object" domElement="true">
    /// </field>
    /// <field name="_SearchTypeGoogleJ" type="jQueryObject">
    /// </field>
    /// <field name="_KeyHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_KeyHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_KeySelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_KeySelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_KeySelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_KeySelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_KeySelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_KeySelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_KeyChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_KeyChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_KeyTextBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_KeyTextBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_KeySearchButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_KeySearchButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_SpotterHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_SpotterHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_SpotterSelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_SpotterSelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_SpotterSelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_SpotterSelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_SpotterSelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_SpotterSelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_SpotterChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_SpotterChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_SpotterTextBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_SpotterTextBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_SpotterSearchButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_SpotterSearchButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_BrandHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_BrandHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_BrandSelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_BrandSelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_BrandSelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_BrandSelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_BrandSelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_BrandSelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_BrandChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_BrandChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_CountryHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_CountryHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_CountrySelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_CountrySelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_CountrySelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_CountrySelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_CountrySelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_CountrySelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_CountryChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_CountryChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_CountryDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_CountryDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_PlaceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_PlaceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_PlaceSelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_PlaceSelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_PlaceSelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_PlaceSelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_PlaceSelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_PlaceSelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_PlaceChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_PlaceChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_PlaceDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_PlaceDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_VenueHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_VenueHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_VenueSelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_VenueSelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_VenueSelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_VenueSelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_VenueSelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_VenueSelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_VenueChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_VenueChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_VenueDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_VenueDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_VenueByLetterDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_VenueByLetterDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_MusicHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_MusicHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_MusicSelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_MusicSelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_MusicSelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_MusicSelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_MusicSelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_MusicSelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_MusicChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_MusicChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_MusicDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_MusicDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateSelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateSelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateSelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateSelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateSelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateSelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateDayHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateDayHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateDayDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateDayDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateDayPlusImg" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateDayPlusImgJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateDayMinusImg" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateDayMinusImgJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateMonthDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateMonthDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateMonthPlusImg" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateMonthPlusImgJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateMonthMinusImg" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateMonthMinusImgJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateYearTextBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateYearTextBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateYearPlusImg" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateYearPlusImgJ" type="jQueryObject">
    /// </field>
    /// <field name="_DateYearMinusImg" type="Object" domElement="true">
    /// </field>
    /// <field name="_DateYearMinusImgJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventSelectedHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventSelectedHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventSelectedLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventSelectedLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventSelectedChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventSelectedChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventChoiceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventChoiceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventListBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventListBoxJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.Picker.View.prototype = {
    clientId: null,
    
    get_debug: function Js_Controls_Picker_View$get_debug() {
        /// <value type="Object" domElement="true"></value>
        if (this._Debug == null) {
            this._Debug = document.getElementById(this.clientId + '_Debug');
        }
        return this._Debug;
    },
    
    _Debug: null,
    
    get_debugJ: function Js_Controls_Picker_View$get_debugJ() {
        /// <value type="jQueryObject"></value>
        if (this._DebugJ == null) {
            this._DebugJ = $('#' + this.clientId + '_Debug');
        }
        return this._DebugJ;
    },
    
    _DebugJ: null,
    
    get_selectedSearchTypeHidden: function Js_Controls_Picker_View$get_selectedSearchTypeHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedSearchTypeHidden == null) {
            this._SelectedSearchTypeHidden = document.getElementById(this.clientId + '_SelectedSearchTypeHidden');
        }
        return this._SelectedSearchTypeHidden;
    },
    
    _SelectedSearchTypeHidden: null,
    
    get_selectedSearchTypeHiddenJ: function Js_Controls_Picker_View$get_selectedSearchTypeHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedSearchTypeHiddenJ == null) {
            this._SelectedSearchTypeHiddenJ = $('#' + this.clientId + '_SelectedSearchTypeHidden');
        }
        return this._SelectedSearchTypeHiddenJ;
    },
    
    _SelectedSearchTypeHiddenJ: null,
    
    get_selectedKeyHidden: function Js_Controls_Picker_View$get_selectedKeyHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedKeyHidden == null) {
            this._SelectedKeyHidden = document.getElementById(this.clientId + '_SelectedKeyHidden');
        }
        return this._SelectedKeyHidden;
    },
    
    _SelectedKeyHidden: null,
    
    get_selectedKeyHiddenJ: function Js_Controls_Picker_View$get_selectedKeyHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedKeyHiddenJ == null) {
            this._SelectedKeyHiddenJ = $('#' + this.clientId + '_SelectedKeyHidden');
        }
        return this._SelectedKeyHiddenJ;
    },
    
    _SelectedKeyHiddenJ: null,
    
    get_selectedSpotterHidden: function Js_Controls_Picker_View$get_selectedSpotterHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedSpotterHidden == null) {
            this._SelectedSpotterHidden = document.getElementById(this.clientId + '_SelectedSpotterHidden');
        }
        return this._SelectedSpotterHidden;
    },
    
    _SelectedSpotterHidden: null,
    
    get_selectedSpotterHiddenJ: function Js_Controls_Picker_View$get_selectedSpotterHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedSpotterHiddenJ == null) {
            this._SelectedSpotterHiddenJ = $('#' + this.clientId + '_SelectedSpotterHidden');
        }
        return this._SelectedSpotterHiddenJ;
    },
    
    _SelectedSpotterHiddenJ: null,
    
    get_selectedBrandHidden: function Js_Controls_Picker_View$get_selectedBrandHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedBrandHidden == null) {
            this._SelectedBrandHidden = document.getElementById(this.clientId + '_SelectedBrandHidden');
        }
        return this._SelectedBrandHidden;
    },
    
    _SelectedBrandHidden: null,
    
    get_selectedBrandHiddenJ: function Js_Controls_Picker_View$get_selectedBrandHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedBrandHiddenJ == null) {
            this._SelectedBrandHiddenJ = $('#' + this.clientId + '_SelectedBrandHidden');
        }
        return this._SelectedBrandHiddenJ;
    },
    
    _SelectedBrandHiddenJ: null,
    
    get_selectedCountryHidden: function Js_Controls_Picker_View$get_selectedCountryHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedCountryHidden == null) {
            this._SelectedCountryHidden = document.getElementById(this.clientId + '_SelectedCountryHidden');
        }
        return this._SelectedCountryHidden;
    },
    
    _SelectedCountryHidden: null,
    
    get_selectedCountryHiddenJ: function Js_Controls_Picker_View$get_selectedCountryHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedCountryHiddenJ == null) {
            this._SelectedCountryHiddenJ = $('#' + this.clientId + '_SelectedCountryHidden');
        }
        return this._SelectedCountryHiddenJ;
    },
    
    _SelectedCountryHiddenJ: null,
    
    get_selectedPlaceHidden: function Js_Controls_Picker_View$get_selectedPlaceHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedPlaceHidden == null) {
            this._SelectedPlaceHidden = document.getElementById(this.clientId + '_SelectedPlaceHidden');
        }
        return this._SelectedPlaceHidden;
    },
    
    _SelectedPlaceHidden: null,
    
    get_selectedPlaceHiddenJ: function Js_Controls_Picker_View$get_selectedPlaceHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedPlaceHiddenJ == null) {
            this._SelectedPlaceHiddenJ = $('#' + this.clientId + '_SelectedPlaceHidden');
        }
        return this._SelectedPlaceHiddenJ;
    },
    
    _SelectedPlaceHiddenJ: null,
    
    get_selectedVenueHidden: function Js_Controls_Picker_View$get_selectedVenueHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedVenueHidden == null) {
            this._SelectedVenueHidden = document.getElementById(this.clientId + '_SelectedVenueHidden');
        }
        return this._SelectedVenueHidden;
    },
    
    _SelectedVenueHidden: null,
    
    get_selectedVenueHiddenJ: function Js_Controls_Picker_View$get_selectedVenueHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedVenueHiddenJ == null) {
            this._SelectedVenueHiddenJ = $('#' + this.clientId + '_SelectedVenueHidden');
        }
        return this._SelectedVenueHiddenJ;
    },
    
    _SelectedVenueHiddenJ: null,
    
    get_selectedMusicHidden: function Js_Controls_Picker_View$get_selectedMusicHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedMusicHidden == null) {
            this._SelectedMusicHidden = document.getElementById(this.clientId + '_SelectedMusicHidden');
        }
        return this._SelectedMusicHidden;
    },
    
    _SelectedMusicHidden: null,
    
    get_selectedMusicHiddenJ: function Js_Controls_Picker_View$get_selectedMusicHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedMusicHiddenJ == null) {
            this._SelectedMusicHiddenJ = $('#' + this.clientId + '_SelectedMusicHidden');
        }
        return this._SelectedMusicHiddenJ;
    },
    
    _SelectedMusicHiddenJ: null,
    
    get_selectedDateHidden: function Js_Controls_Picker_View$get_selectedDateHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedDateHidden == null) {
            this._SelectedDateHidden = document.getElementById(this.clientId + '_SelectedDateHidden');
        }
        return this._SelectedDateHidden;
    },
    
    _SelectedDateHidden: null,
    
    get_selectedDateHiddenJ: function Js_Controls_Picker_View$get_selectedDateHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedDateHiddenJ == null) {
            this._SelectedDateHiddenJ = $('#' + this.clientId + '_SelectedDateHidden');
        }
        return this._SelectedDateHiddenJ;
    },
    
    _SelectedDateHiddenJ: null,
    
    get_selectedEventHidden: function Js_Controls_Picker_View$get_selectedEventHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._SelectedEventHidden == null) {
            this._SelectedEventHidden = document.getElementById(this.clientId + '_SelectedEventHidden');
        }
        return this._SelectedEventHidden;
    },
    
    _SelectedEventHidden: null,
    
    get_selectedEventHiddenJ: function Js_Controls_Picker_View$get_selectedEventHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._SelectedEventHiddenJ == null) {
            this._SelectedEventHiddenJ = $('#' + this.clientId + '_SelectedEventHidden');
        }
        return this._SelectedEventHiddenJ;
    },
    
    _SelectedEventHiddenJ: null,
    
    get_optionKeyHidden: function Js_Controls_Picker_View$get_optionKeyHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionKeyHidden == null) {
            this._OptionKeyHidden = document.getElementById(this.clientId + '_OptionKeyHidden');
        }
        return this._OptionKeyHidden;
    },
    
    _OptionKeyHidden: null,
    
    get_optionKeyHiddenJ: function Js_Controls_Picker_View$get_optionKeyHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionKeyHiddenJ == null) {
            this._OptionKeyHiddenJ = $('#' + this.clientId + '_OptionKeyHidden');
        }
        return this._OptionKeyHiddenJ;
    },
    
    _OptionKeyHiddenJ: null,
    
    get_optionMeHidden: function Js_Controls_Picker_View$get_optionMeHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionMeHidden == null) {
            this._OptionMeHidden = document.getElementById(this.clientId + '_OptionMeHidden');
        }
        return this._OptionMeHidden;
    },
    
    _OptionMeHidden: null,
    
    get_optionMeHiddenJ: function Js_Controls_Picker_View$get_optionMeHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionMeHiddenJ == null) {
            this._OptionMeHiddenJ = $('#' + this.clientId + '_OptionMeHidden');
        }
        return this._OptionMeHiddenJ;
    },
    
    _OptionMeHiddenJ: null,
    
    get_optionSpotterHidden: function Js_Controls_Picker_View$get_optionSpotterHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionSpotterHidden == null) {
            this._OptionSpotterHidden = document.getElementById(this.clientId + '_OptionSpotterHidden');
        }
        return this._OptionSpotterHidden;
    },
    
    _OptionSpotterHidden: null,
    
    get_optionSpotterHiddenJ: function Js_Controls_Picker_View$get_optionSpotterHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionSpotterHiddenJ == null) {
            this._OptionSpotterHiddenJ = $('#' + this.clientId + '_OptionSpotterHidden');
        }
        return this._OptionSpotterHiddenJ;
    },
    
    _OptionSpotterHiddenJ: null,
    
    get_optionBrandHidden: function Js_Controls_Picker_View$get_optionBrandHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionBrandHidden == null) {
            this._OptionBrandHidden = document.getElementById(this.clientId + '_OptionBrandHidden');
        }
        return this._OptionBrandHidden;
    },
    
    _OptionBrandHidden: null,
    
    get_optionBrandHiddenJ: function Js_Controls_Picker_View$get_optionBrandHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionBrandHiddenJ == null) {
            this._OptionBrandHiddenJ = $('#' + this.clientId + '_OptionBrandHidden');
        }
        return this._OptionBrandHiddenJ;
    },
    
    _OptionBrandHiddenJ: null,
    
    get_optionCountryHidden: function Js_Controls_Picker_View$get_optionCountryHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionCountryHidden == null) {
            this._OptionCountryHidden = document.getElementById(this.clientId + '_OptionCountryHidden');
        }
        return this._OptionCountryHidden;
    },
    
    _OptionCountryHidden: null,
    
    get_optionCountryHiddenJ: function Js_Controls_Picker_View$get_optionCountryHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionCountryHiddenJ == null) {
            this._OptionCountryHiddenJ = $('#' + this.clientId + '_OptionCountryHidden');
        }
        return this._OptionCountryHiddenJ;
    },
    
    _OptionCountryHiddenJ: null,
    
    get_optionPlaceHidden: function Js_Controls_Picker_View$get_optionPlaceHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionPlaceHidden == null) {
            this._OptionPlaceHidden = document.getElementById(this.clientId + '_OptionPlaceHidden');
        }
        return this._OptionPlaceHidden;
    },
    
    _OptionPlaceHidden: null,
    
    get_optionPlaceHiddenJ: function Js_Controls_Picker_View$get_optionPlaceHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionPlaceHiddenJ == null) {
            this._OptionPlaceHiddenJ = $('#' + this.clientId + '_OptionPlaceHidden');
        }
        return this._OptionPlaceHiddenJ;
    },
    
    _OptionPlaceHiddenJ: null,
    
    get_optionVenueHidden: function Js_Controls_Picker_View$get_optionVenueHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionVenueHidden == null) {
            this._OptionVenueHidden = document.getElementById(this.clientId + '_OptionVenueHidden');
        }
        return this._OptionVenueHidden;
    },
    
    _OptionVenueHidden: null,
    
    get_optionVenueHiddenJ: function Js_Controls_Picker_View$get_optionVenueHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionVenueHiddenJ == null) {
            this._OptionVenueHiddenJ = $('#' + this.clientId + '_OptionVenueHidden');
        }
        return this._OptionVenueHiddenJ;
    },
    
    _OptionVenueHiddenJ: null,
    
    get_optionMusicHidden: function Js_Controls_Picker_View$get_optionMusicHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionMusicHidden == null) {
            this._OptionMusicHidden = document.getElementById(this.clientId + '_OptionMusicHidden');
        }
        return this._OptionMusicHidden;
    },
    
    _OptionMusicHidden: null,
    
    get_optionMusicHiddenJ: function Js_Controls_Picker_View$get_optionMusicHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionMusicHiddenJ == null) {
            this._OptionMusicHiddenJ = $('#' + this.clientId + '_OptionMusicHidden');
        }
        return this._OptionMusicHiddenJ;
    },
    
    _OptionMusicHiddenJ: null,
    
    get_optionDateHidden: function Js_Controls_Picker_View$get_optionDateHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionDateHidden == null) {
            this._OptionDateHidden = document.getElementById(this.clientId + '_OptionDateHidden');
        }
        return this._OptionDateHidden;
    },
    
    _OptionDateHidden: null,
    
    get_optionDateHiddenJ: function Js_Controls_Picker_View$get_optionDateHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionDateHiddenJ == null) {
            this._OptionDateHiddenJ = $('#' + this.clientId + '_OptionDateHidden');
        }
        return this._OptionDateHiddenJ;
    },
    
    _OptionDateHiddenJ: null,
    
    get_optionDateDayHidden: function Js_Controls_Picker_View$get_optionDateDayHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionDateDayHidden == null) {
            this._OptionDateDayHidden = document.getElementById(this.clientId + '_OptionDateDayHidden');
        }
        return this._OptionDateDayHidden;
    },
    
    _OptionDateDayHidden: null,
    
    get_optionDateDayHiddenJ: function Js_Controls_Picker_View$get_optionDateDayHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionDateDayHiddenJ == null) {
            this._OptionDateDayHiddenJ = $('#' + this.clientId + '_OptionDateDayHidden');
        }
        return this._OptionDateDayHiddenJ;
    },
    
    _OptionDateDayHiddenJ: null,
    
    get_optionDateDayIncrementHidden: function Js_Controls_Picker_View$get_optionDateDayIncrementHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionDateDayIncrementHidden == null) {
            this._OptionDateDayIncrementHidden = document.getElementById(this.clientId + '_OptionDateDayIncrementHidden');
        }
        return this._OptionDateDayIncrementHidden;
    },
    
    _OptionDateDayIncrementHidden: null,
    
    get_optionDateDayIncrementHiddenJ: function Js_Controls_Picker_View$get_optionDateDayIncrementHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionDateDayIncrementHiddenJ == null) {
            this._OptionDateDayIncrementHiddenJ = $('#' + this.clientId + '_OptionDateDayIncrementHidden');
        }
        return this._OptionDateDayIncrementHiddenJ;
    },
    
    _OptionDateDayIncrementHiddenJ: null,
    
    get_optionEventHidden: function Js_Controls_Picker_View$get_optionEventHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionEventHidden == null) {
            this._OptionEventHidden = document.getElementById(this.clientId + '_OptionEventHidden');
        }
        return this._OptionEventHidden;
    },
    
    _OptionEventHidden: null,
    
    get_optionEventHiddenJ: function Js_Controls_Picker_View$get_optionEventHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionEventHiddenJ == null) {
            this._OptionEventHiddenJ = $('#' + this.clientId + '_OptionEventHidden');
        }
        return this._OptionEventHiddenJ;
    },
    
    _OptionEventHiddenJ: null,
    
    get_optionGoogleHidden: function Js_Controls_Picker_View$get_optionGoogleHidden() {
        /// <value type="Object" domElement="true"></value>
        if (this._OptionGoogleHidden == null) {
            this._OptionGoogleHidden = document.getElementById(this.clientId + '_OptionGoogleHidden');
        }
        return this._OptionGoogleHidden;
    },
    
    _OptionGoogleHidden: null,
    
    get_optionGoogleHiddenJ: function Js_Controls_Picker_View$get_optionGoogleHiddenJ() {
        /// <value type="jQueryObject"></value>
        if (this._OptionGoogleHiddenJ == null) {
            this._OptionGoogleHiddenJ = $('#' + this.clientId + '_OptionGoogleHidden');
        }
        return this._OptionGoogleHiddenJ;
    },
    
    _OptionGoogleHiddenJ: null,
    
    get_holder: function Js_Controls_Picker_View$get_holder() {
        /// <value type="Object" domElement="true"></value>
        if (this._Holder == null) {
            this._Holder = document.getElementById(this.clientId + '_Holder');
        }
        return this._Holder;
    },
    
    _Holder: null,
    
    get_holderJ: function Js_Controls_Picker_View$get_holderJ() {
        /// <value type="jQueryObject"></value>
        if (this._HolderJ == null) {
            this._HolderJ = $('#' + this.clientId + '_Holder');
        }
        return this._HolderJ;
    },
    
    _HolderJ: null,
    
    get_searchTypeHolder: function Js_Controls_Picker_View$get_searchTypeHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._SearchTypeHolder == null) {
            this._SearchTypeHolder = document.getElementById(this.clientId + '_SearchTypeHolder');
        }
        return this._SearchTypeHolder;
    },
    
    _SearchTypeHolder: null,
    
    get_searchTypeHolderJ: function Js_Controls_Picker_View$get_searchTypeHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._SearchTypeHolderJ == null) {
            this._SearchTypeHolderJ = $('#' + this.clientId + '_SearchTypeHolder');
        }
        return this._SearchTypeHolderJ;
    },
    
    _SearchTypeHolderJ: null,
    
    get_searchTypeKey: function Js_Controls_Picker_View$get_searchTypeKey() {
        /// <value type="Object" domElement="true"></value>
        if (this._SearchTypeKey == null) {
            this._SearchTypeKey = document.getElementById(this.clientId + '_SearchTypeKey');
        }
        return this._SearchTypeKey;
    },
    
    _SearchTypeKey: null,
    
    get_searchTypeKeyJ: function Js_Controls_Picker_View$get_searchTypeKeyJ() {
        /// <value type="jQueryObject"></value>
        if (this._SearchTypeKeyJ == null) {
            this._SearchTypeKeyJ = $('#' + this.clientId + '_SearchTypeKey');
        }
        return this._SearchTypeKeyJ;
    },
    
    _SearchTypeKeyJ: null,
    
    get_searchTypeSpotter: function Js_Controls_Picker_View$get_searchTypeSpotter() {
        /// <value type="Object" domElement="true"></value>
        if (this._SearchTypeSpotter == null) {
            this._SearchTypeSpotter = document.getElementById(this.clientId + '_SearchTypeSpotter');
        }
        return this._SearchTypeSpotter;
    },
    
    _SearchTypeSpotter: null,
    
    get_searchTypeSpotterJ: function Js_Controls_Picker_View$get_searchTypeSpotterJ() {
        /// <value type="jQueryObject"></value>
        if (this._SearchTypeSpotterJ == null) {
            this._SearchTypeSpotterJ = $('#' + this.clientId + '_SearchTypeSpotter');
        }
        return this._SearchTypeSpotterJ;
    },
    
    _SearchTypeSpotterJ: null,
    
    get_searchTypeMe: function Js_Controls_Picker_View$get_searchTypeMe() {
        /// <value type="Object" domElement="true"></value>
        if (this._SearchTypeMe == null) {
            this._SearchTypeMe = document.getElementById(this.clientId + '_SearchTypeMe');
        }
        return this._SearchTypeMe;
    },
    
    _SearchTypeMe: null,
    
    get_searchTypeMeJ: function Js_Controls_Picker_View$get_searchTypeMeJ() {
        /// <value type="jQueryObject"></value>
        if (this._SearchTypeMeJ == null) {
            this._SearchTypeMeJ = $('#' + this.clientId + '_SearchTypeMe');
        }
        return this._SearchTypeMeJ;
    },
    
    _SearchTypeMeJ: null,
    
    get_searchTypeVenue: function Js_Controls_Picker_View$get_searchTypeVenue() {
        /// <value type="Object" domElement="true"></value>
        if (this._SearchTypeVenue == null) {
            this._SearchTypeVenue = document.getElementById(this.clientId + '_SearchTypeVenue');
        }
        return this._SearchTypeVenue;
    },
    
    _SearchTypeVenue: null,
    
    get_searchTypeVenueJ: function Js_Controls_Picker_View$get_searchTypeVenueJ() {
        /// <value type="jQueryObject"></value>
        if (this._SearchTypeVenueJ == null) {
            this._SearchTypeVenueJ = $('#' + this.clientId + '_SearchTypeVenue');
        }
        return this._SearchTypeVenueJ;
    },
    
    _SearchTypeVenueJ: null,
    
    get_searchTypeBrand: function Js_Controls_Picker_View$get_searchTypeBrand() {
        /// <value type="Object" domElement="true"></value>
        if (this._SearchTypeBrand == null) {
            this._SearchTypeBrand = document.getElementById(this.clientId + '_SearchTypeBrand');
        }
        return this._SearchTypeBrand;
    },
    
    _SearchTypeBrand: null,
    
    get_searchTypeBrandJ: function Js_Controls_Picker_View$get_searchTypeBrandJ() {
        /// <value type="jQueryObject"></value>
        if (this._SearchTypeBrandJ == null) {
            this._SearchTypeBrandJ = $('#' + this.clientId + '_SearchTypeBrand');
        }
        return this._SearchTypeBrandJ;
    },
    
    _SearchTypeBrandJ: null,
    
    get_searchTypeMusic: function Js_Controls_Picker_View$get_searchTypeMusic() {
        /// <value type="Object" domElement="true"></value>
        if (this._SearchTypeMusic == null) {
            this._SearchTypeMusic = document.getElementById(this.clientId + '_SearchTypeMusic');
        }
        return this._SearchTypeMusic;
    },
    
    _SearchTypeMusic: null,
    
    get_searchTypeMusicJ: function Js_Controls_Picker_View$get_searchTypeMusicJ() {
        /// <value type="jQueryObject"></value>
        if (this._SearchTypeMusicJ == null) {
            this._SearchTypeMusicJ = $('#' + this.clientId + '_SearchTypeMusic');
        }
        return this._SearchTypeMusicJ;
    },
    
    _SearchTypeMusicJ: null,
    
    get_searchTypeGoogle: function Js_Controls_Picker_View$get_searchTypeGoogle() {
        /// <value type="Object" domElement="true"></value>
        if (this._SearchTypeGoogle == null) {
            this._SearchTypeGoogle = document.getElementById(this.clientId + '_SearchTypeGoogle');
        }
        return this._SearchTypeGoogle;
    },
    
    _SearchTypeGoogle: null,
    
    get_searchTypeGoogleJ: function Js_Controls_Picker_View$get_searchTypeGoogleJ() {
        /// <value type="jQueryObject"></value>
        if (this._SearchTypeGoogleJ == null) {
            this._SearchTypeGoogleJ = $('#' + this.clientId + '_SearchTypeGoogle');
        }
        return this._SearchTypeGoogleJ;
    },
    
    _SearchTypeGoogleJ: null,
    
    get_keyHolder: function Js_Controls_Picker_View$get_keyHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._KeyHolder == null) {
            this._KeyHolder = document.getElementById(this.clientId + '_KeyHolder');
        }
        return this._KeyHolder;
    },
    
    _KeyHolder: null,
    
    get_keyHolderJ: function Js_Controls_Picker_View$get_keyHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._KeyHolderJ == null) {
            this._KeyHolderJ = $('#' + this.clientId + '_KeyHolder');
        }
        return this._KeyHolderJ;
    },
    
    _KeyHolderJ: null,
    
    get_keySelectedHolder: function Js_Controls_Picker_View$get_keySelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._KeySelectedHolder == null) {
            this._KeySelectedHolder = document.getElementById(this.clientId + '_KeySelectedHolder');
        }
        return this._KeySelectedHolder;
    },
    
    _KeySelectedHolder: null,
    
    get_keySelectedHolderJ: function Js_Controls_Picker_View$get_keySelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._KeySelectedHolderJ == null) {
            this._KeySelectedHolderJ = $('#' + this.clientId + '_KeySelectedHolder');
        }
        return this._KeySelectedHolderJ;
    },
    
    _KeySelectedHolderJ: null,
    
    get_keySelectedLabel: function Js_Controls_Picker_View$get_keySelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._KeySelectedLabel == null) {
            this._KeySelectedLabel = document.getElementById(this.clientId + '_KeySelectedLabel');
        }
        return this._KeySelectedLabel;
    },
    
    _KeySelectedLabel: null,
    
    get_keySelectedLabelJ: function Js_Controls_Picker_View$get_keySelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._KeySelectedLabelJ == null) {
            this._KeySelectedLabelJ = $('#' + this.clientId + '_KeySelectedLabel');
        }
        return this._KeySelectedLabelJ;
    },
    
    _KeySelectedLabelJ: null,
    
    get_keySelectedChangeLink: function Js_Controls_Picker_View$get_keySelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._KeySelectedChangeLink == null) {
            this._KeySelectedChangeLink = document.getElementById(this.clientId + '_KeySelectedChangeLink');
        }
        return this._KeySelectedChangeLink;
    },
    
    _KeySelectedChangeLink: null,
    
    get_keySelectedChangeLinkJ: function Js_Controls_Picker_View$get_keySelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._KeySelectedChangeLinkJ == null) {
            this._KeySelectedChangeLinkJ = $('#' + this.clientId + '_KeySelectedChangeLink');
        }
        return this._KeySelectedChangeLinkJ;
    },
    
    _KeySelectedChangeLinkJ: null,
    
    get_keyChoiceHolder: function Js_Controls_Picker_View$get_keyChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._KeyChoiceHolder == null) {
            this._KeyChoiceHolder = document.getElementById(this.clientId + '_KeyChoiceHolder');
        }
        return this._KeyChoiceHolder;
    },
    
    _KeyChoiceHolder: null,
    
    get_keyChoiceHolderJ: function Js_Controls_Picker_View$get_keyChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._KeyChoiceHolderJ == null) {
            this._KeyChoiceHolderJ = $('#' + this.clientId + '_KeyChoiceHolder');
        }
        return this._KeyChoiceHolderJ;
    },
    
    _KeyChoiceHolderJ: null,
    
    get_keyTextBox: function Js_Controls_Picker_View$get_keyTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._KeyTextBox == null) {
            this._KeyTextBox = document.getElementById(this.clientId + '_KeyTextBox');
        }
        return this._KeyTextBox;
    },
    
    _KeyTextBox: null,
    
    get_keyTextBoxJ: function Js_Controls_Picker_View$get_keyTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._KeyTextBoxJ == null) {
            this._KeyTextBoxJ = $('#' + this.clientId + '_KeyTextBox');
        }
        return this._KeyTextBoxJ;
    },
    
    _KeyTextBoxJ: null,
    
    get_keySearchButton: function Js_Controls_Picker_View$get_keySearchButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._KeySearchButton == null) {
            this._KeySearchButton = document.getElementById(this.clientId + '_KeySearchButton');
        }
        return this._KeySearchButton;
    },
    
    _KeySearchButton: null,
    
    get_keySearchButtonJ: function Js_Controls_Picker_View$get_keySearchButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._KeySearchButtonJ == null) {
            this._KeySearchButtonJ = $('#' + this.clientId + '_KeySearchButton');
        }
        return this._KeySearchButtonJ;
    },
    
    _KeySearchButtonJ: null,
    
    get_spotterHolder: function Js_Controls_Picker_View$get_spotterHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._SpotterHolder == null) {
            this._SpotterHolder = document.getElementById(this.clientId + '_SpotterHolder');
        }
        return this._SpotterHolder;
    },
    
    _SpotterHolder: null,
    
    get_spotterHolderJ: function Js_Controls_Picker_View$get_spotterHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._SpotterHolderJ == null) {
            this._SpotterHolderJ = $('#' + this.clientId + '_SpotterHolder');
        }
        return this._SpotterHolderJ;
    },
    
    _SpotterHolderJ: null,
    
    get_spotterSelectedHolder: function Js_Controls_Picker_View$get_spotterSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._SpotterSelectedHolder == null) {
            this._SpotterSelectedHolder = document.getElementById(this.clientId + '_SpotterSelectedHolder');
        }
        return this._SpotterSelectedHolder;
    },
    
    _SpotterSelectedHolder: null,
    
    get_spotterSelectedHolderJ: function Js_Controls_Picker_View$get_spotterSelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._SpotterSelectedHolderJ == null) {
            this._SpotterSelectedHolderJ = $('#' + this.clientId + '_SpotterSelectedHolder');
        }
        return this._SpotterSelectedHolderJ;
    },
    
    _SpotterSelectedHolderJ: null,
    
    get_spotterSelectedLabel: function Js_Controls_Picker_View$get_spotterSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._SpotterSelectedLabel == null) {
            this._SpotterSelectedLabel = document.getElementById(this.clientId + '_SpotterSelectedLabel');
        }
        return this._SpotterSelectedLabel;
    },
    
    _SpotterSelectedLabel: null,
    
    get_spotterSelectedLabelJ: function Js_Controls_Picker_View$get_spotterSelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._SpotterSelectedLabelJ == null) {
            this._SpotterSelectedLabelJ = $('#' + this.clientId + '_SpotterSelectedLabel');
        }
        return this._SpotterSelectedLabelJ;
    },
    
    _SpotterSelectedLabelJ: null,
    
    get_spotterSelectedChangeLink: function Js_Controls_Picker_View$get_spotterSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._SpotterSelectedChangeLink == null) {
            this._SpotterSelectedChangeLink = document.getElementById(this.clientId + '_SpotterSelectedChangeLink');
        }
        return this._SpotterSelectedChangeLink;
    },
    
    _SpotterSelectedChangeLink: null,
    
    get_spotterSelectedChangeLinkJ: function Js_Controls_Picker_View$get_spotterSelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._SpotterSelectedChangeLinkJ == null) {
            this._SpotterSelectedChangeLinkJ = $('#' + this.clientId + '_SpotterSelectedChangeLink');
        }
        return this._SpotterSelectedChangeLinkJ;
    },
    
    _SpotterSelectedChangeLinkJ: null,
    
    get_spotterChoiceHolder: function Js_Controls_Picker_View$get_spotterChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._SpotterChoiceHolder == null) {
            this._SpotterChoiceHolder = document.getElementById(this.clientId + '_SpotterChoiceHolder');
        }
        return this._SpotterChoiceHolder;
    },
    
    _SpotterChoiceHolder: null,
    
    get_spotterChoiceHolderJ: function Js_Controls_Picker_View$get_spotterChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._SpotterChoiceHolderJ == null) {
            this._SpotterChoiceHolderJ = $('#' + this.clientId + '_SpotterChoiceHolder');
        }
        return this._SpotterChoiceHolderJ;
    },
    
    _SpotterChoiceHolderJ: null,
    
    get_spotterTextBox: function Js_Controls_Picker_View$get_spotterTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._SpotterTextBox == null) {
            this._SpotterTextBox = document.getElementById(this.clientId + '_SpotterTextBox');
        }
        return this._SpotterTextBox;
    },
    
    _SpotterTextBox: null,
    
    get_spotterTextBoxJ: function Js_Controls_Picker_View$get_spotterTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._SpotterTextBoxJ == null) {
            this._SpotterTextBoxJ = $('#' + this.clientId + '_SpotterTextBox');
        }
        return this._SpotterTextBoxJ;
    },
    
    _SpotterTextBoxJ: null,
    
    get_spotterSearchButton: function Js_Controls_Picker_View$get_spotterSearchButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._SpotterSearchButton == null) {
            this._SpotterSearchButton = document.getElementById(this.clientId + '_SpotterSearchButton');
        }
        return this._SpotterSearchButton;
    },
    
    _SpotterSearchButton: null,
    
    get_spotterSearchButtonJ: function Js_Controls_Picker_View$get_spotterSearchButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._SpotterSearchButtonJ == null) {
            this._SpotterSearchButtonJ = $('#' + this.clientId + '_SpotterSearchButton');
        }
        return this._SpotterSearchButtonJ;
    },
    
    _SpotterSearchButtonJ: null,
    
    get_brandHolder: function Js_Controls_Picker_View$get_brandHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._BrandHolder == null) {
            this._BrandHolder = document.getElementById(this.clientId + '_BrandHolder');
        }
        return this._BrandHolder;
    },
    
    _BrandHolder: null,
    
    get_brandHolderJ: function Js_Controls_Picker_View$get_brandHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._BrandHolderJ == null) {
            this._BrandHolderJ = $('#' + this.clientId + '_BrandHolder');
        }
        return this._BrandHolderJ;
    },
    
    _BrandHolderJ: null,
    
    get_brandSelectedHolder: function Js_Controls_Picker_View$get_brandSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._BrandSelectedHolder == null) {
            this._BrandSelectedHolder = document.getElementById(this.clientId + '_BrandSelectedHolder');
        }
        return this._BrandSelectedHolder;
    },
    
    _BrandSelectedHolder: null,
    
    get_brandSelectedHolderJ: function Js_Controls_Picker_View$get_brandSelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._BrandSelectedHolderJ == null) {
            this._BrandSelectedHolderJ = $('#' + this.clientId + '_BrandSelectedHolder');
        }
        return this._BrandSelectedHolderJ;
    },
    
    _BrandSelectedHolderJ: null,
    
    get_brandSelectedLabel: function Js_Controls_Picker_View$get_brandSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._BrandSelectedLabel == null) {
            this._BrandSelectedLabel = document.getElementById(this.clientId + '_BrandSelectedLabel');
        }
        return this._BrandSelectedLabel;
    },
    
    _BrandSelectedLabel: null,
    
    get_brandSelectedLabelJ: function Js_Controls_Picker_View$get_brandSelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._BrandSelectedLabelJ == null) {
            this._BrandSelectedLabelJ = $('#' + this.clientId + '_BrandSelectedLabel');
        }
        return this._BrandSelectedLabelJ;
    },
    
    _BrandSelectedLabelJ: null,
    
    get_brandSelectedChangeLink: function Js_Controls_Picker_View$get_brandSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._BrandSelectedChangeLink == null) {
            this._BrandSelectedChangeLink = document.getElementById(this.clientId + '_BrandSelectedChangeLink');
        }
        return this._BrandSelectedChangeLink;
    },
    
    _BrandSelectedChangeLink: null,
    
    get_brandSelectedChangeLinkJ: function Js_Controls_Picker_View$get_brandSelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._BrandSelectedChangeLinkJ == null) {
            this._BrandSelectedChangeLinkJ = $('#' + this.clientId + '_BrandSelectedChangeLink');
        }
        return this._BrandSelectedChangeLinkJ;
    },
    
    _BrandSelectedChangeLinkJ: null,
    
    get_brandChoiceHolder: function Js_Controls_Picker_View$get_brandChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._BrandChoiceHolder == null) {
            this._BrandChoiceHolder = document.getElementById(this.clientId + '_BrandChoiceHolder');
        }
        return this._BrandChoiceHolder;
    },
    
    _BrandChoiceHolder: null,
    
    get_brandChoiceHolderJ: function Js_Controls_Picker_View$get_brandChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._BrandChoiceHolderJ == null) {
            this._BrandChoiceHolderJ = $('#' + this.clientId + '_BrandChoiceHolder');
        }
        return this._BrandChoiceHolderJ;
    },
    
    _BrandChoiceHolderJ: null,
    
    get_brandAutoComplete: function Js_Controls_Picker_View$get_brandAutoComplete() {
        /// <value type="Js.ClientControls.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_BrandAutoCompleteBehaviour');
    },
    
    get_countryHolder: function Js_Controls_Picker_View$get_countryHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._CountryHolder == null) {
            this._CountryHolder = document.getElementById(this.clientId + '_CountryHolder');
        }
        return this._CountryHolder;
    },
    
    _CountryHolder: null,
    
    get_countryHolderJ: function Js_Controls_Picker_View$get_countryHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._CountryHolderJ == null) {
            this._CountryHolderJ = $('#' + this.clientId + '_CountryHolder');
        }
        return this._CountryHolderJ;
    },
    
    _CountryHolderJ: null,
    
    get_countrySelectedHolder: function Js_Controls_Picker_View$get_countrySelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._CountrySelectedHolder == null) {
            this._CountrySelectedHolder = document.getElementById(this.clientId + '_CountrySelectedHolder');
        }
        return this._CountrySelectedHolder;
    },
    
    _CountrySelectedHolder: null,
    
    get_countrySelectedHolderJ: function Js_Controls_Picker_View$get_countrySelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._CountrySelectedHolderJ == null) {
            this._CountrySelectedHolderJ = $('#' + this.clientId + '_CountrySelectedHolder');
        }
        return this._CountrySelectedHolderJ;
    },
    
    _CountrySelectedHolderJ: null,
    
    get_countrySelectedLabel: function Js_Controls_Picker_View$get_countrySelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._CountrySelectedLabel == null) {
            this._CountrySelectedLabel = document.getElementById(this.clientId + '_CountrySelectedLabel');
        }
        return this._CountrySelectedLabel;
    },
    
    _CountrySelectedLabel: null,
    
    get_countrySelectedLabelJ: function Js_Controls_Picker_View$get_countrySelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._CountrySelectedLabelJ == null) {
            this._CountrySelectedLabelJ = $('#' + this.clientId + '_CountrySelectedLabel');
        }
        return this._CountrySelectedLabelJ;
    },
    
    _CountrySelectedLabelJ: null,
    
    get_countrySelectedChangeLink: function Js_Controls_Picker_View$get_countrySelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._CountrySelectedChangeLink == null) {
            this._CountrySelectedChangeLink = document.getElementById(this.clientId + '_CountrySelectedChangeLink');
        }
        return this._CountrySelectedChangeLink;
    },
    
    _CountrySelectedChangeLink: null,
    
    get_countrySelectedChangeLinkJ: function Js_Controls_Picker_View$get_countrySelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._CountrySelectedChangeLinkJ == null) {
            this._CountrySelectedChangeLinkJ = $('#' + this.clientId + '_CountrySelectedChangeLink');
        }
        return this._CountrySelectedChangeLinkJ;
    },
    
    _CountrySelectedChangeLinkJ: null,
    
    get_countryChoiceHolder: function Js_Controls_Picker_View$get_countryChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._CountryChoiceHolder == null) {
            this._CountryChoiceHolder = document.getElementById(this.clientId + '_CountryChoiceHolder');
        }
        return this._CountryChoiceHolder;
    },
    
    _CountryChoiceHolder: null,
    
    get_countryChoiceHolderJ: function Js_Controls_Picker_View$get_countryChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._CountryChoiceHolderJ == null) {
            this._CountryChoiceHolderJ = $('#' + this.clientId + '_CountryChoiceHolder');
        }
        return this._CountryChoiceHolderJ;
    },
    
    _CountryChoiceHolderJ: null,
    
    get_countryDropDown: function Js_Controls_Picker_View$get_countryDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._CountryDropDown == null) {
            this._CountryDropDown = document.getElementById(this.clientId + '_CountryDropDown');
        }
        return this._CountryDropDown;
    },
    
    _CountryDropDown: null,
    
    get_countryDropDownJ: function Js_Controls_Picker_View$get_countryDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._CountryDropDownJ == null) {
            this._CountryDropDownJ = $('#' + this.clientId + '_CountryDropDown');
        }
        return this._CountryDropDownJ;
    },
    
    _CountryDropDownJ: null,
    
    get_placeHolder: function Js_Controls_Picker_View$get_placeHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._PlaceHolder == null) {
            this._PlaceHolder = document.getElementById(this.clientId + '_PlaceHolder');
        }
        return this._PlaceHolder;
    },
    
    _PlaceHolder: null,
    
    get_placeHolderJ: function Js_Controls_Picker_View$get_placeHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._PlaceHolderJ == null) {
            this._PlaceHolderJ = $('#' + this.clientId + '_PlaceHolder');
        }
        return this._PlaceHolderJ;
    },
    
    _PlaceHolderJ: null,
    
    get_placeSelectedHolder: function Js_Controls_Picker_View$get_placeSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._PlaceSelectedHolder == null) {
            this._PlaceSelectedHolder = document.getElementById(this.clientId + '_PlaceSelectedHolder');
        }
        return this._PlaceSelectedHolder;
    },
    
    _PlaceSelectedHolder: null,
    
    get_placeSelectedHolderJ: function Js_Controls_Picker_View$get_placeSelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._PlaceSelectedHolderJ == null) {
            this._PlaceSelectedHolderJ = $('#' + this.clientId + '_PlaceSelectedHolder');
        }
        return this._PlaceSelectedHolderJ;
    },
    
    _PlaceSelectedHolderJ: null,
    
    get_placeSelectedLabel: function Js_Controls_Picker_View$get_placeSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._PlaceSelectedLabel == null) {
            this._PlaceSelectedLabel = document.getElementById(this.clientId + '_PlaceSelectedLabel');
        }
        return this._PlaceSelectedLabel;
    },
    
    _PlaceSelectedLabel: null,
    
    get_placeSelectedLabelJ: function Js_Controls_Picker_View$get_placeSelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._PlaceSelectedLabelJ == null) {
            this._PlaceSelectedLabelJ = $('#' + this.clientId + '_PlaceSelectedLabel');
        }
        return this._PlaceSelectedLabelJ;
    },
    
    _PlaceSelectedLabelJ: null,
    
    get_placeSelectedChangeLink: function Js_Controls_Picker_View$get_placeSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._PlaceSelectedChangeLink == null) {
            this._PlaceSelectedChangeLink = document.getElementById(this.clientId + '_PlaceSelectedChangeLink');
        }
        return this._PlaceSelectedChangeLink;
    },
    
    _PlaceSelectedChangeLink: null,
    
    get_placeSelectedChangeLinkJ: function Js_Controls_Picker_View$get_placeSelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._PlaceSelectedChangeLinkJ == null) {
            this._PlaceSelectedChangeLinkJ = $('#' + this.clientId + '_PlaceSelectedChangeLink');
        }
        return this._PlaceSelectedChangeLinkJ;
    },
    
    _PlaceSelectedChangeLinkJ: null,
    
    get_placeChoiceHolder: function Js_Controls_Picker_View$get_placeChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._PlaceChoiceHolder == null) {
            this._PlaceChoiceHolder = document.getElementById(this.clientId + '_PlaceChoiceHolder');
        }
        return this._PlaceChoiceHolder;
    },
    
    _PlaceChoiceHolder: null,
    
    get_placeChoiceHolderJ: function Js_Controls_Picker_View$get_placeChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._PlaceChoiceHolderJ == null) {
            this._PlaceChoiceHolderJ = $('#' + this.clientId + '_PlaceChoiceHolder');
        }
        return this._PlaceChoiceHolderJ;
    },
    
    _PlaceChoiceHolderJ: null,
    
    get_placeDropDown: function Js_Controls_Picker_View$get_placeDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._PlaceDropDown == null) {
            this._PlaceDropDown = document.getElementById(this.clientId + '_PlaceDropDown');
        }
        return this._PlaceDropDown;
    },
    
    _PlaceDropDown: null,
    
    get_placeDropDownJ: function Js_Controls_Picker_View$get_placeDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._PlaceDropDownJ == null) {
            this._PlaceDropDownJ = $('#' + this.clientId + '_PlaceDropDown');
        }
        return this._PlaceDropDownJ;
    },
    
    _PlaceDropDownJ: null,
    
    get_venueHolder: function Js_Controls_Picker_View$get_venueHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._VenueHolder == null) {
            this._VenueHolder = document.getElementById(this.clientId + '_VenueHolder');
        }
        return this._VenueHolder;
    },
    
    _VenueHolder: null,
    
    get_venueHolderJ: function Js_Controls_Picker_View$get_venueHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._VenueHolderJ == null) {
            this._VenueHolderJ = $('#' + this.clientId + '_VenueHolder');
        }
        return this._VenueHolderJ;
    },
    
    _VenueHolderJ: null,
    
    get_venueSelectedHolder: function Js_Controls_Picker_View$get_venueSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._VenueSelectedHolder == null) {
            this._VenueSelectedHolder = document.getElementById(this.clientId + '_VenueSelectedHolder');
        }
        return this._VenueSelectedHolder;
    },
    
    _VenueSelectedHolder: null,
    
    get_venueSelectedHolderJ: function Js_Controls_Picker_View$get_venueSelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._VenueSelectedHolderJ == null) {
            this._VenueSelectedHolderJ = $('#' + this.clientId + '_VenueSelectedHolder');
        }
        return this._VenueSelectedHolderJ;
    },
    
    _VenueSelectedHolderJ: null,
    
    get_venueSelectedLabel: function Js_Controls_Picker_View$get_venueSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._VenueSelectedLabel == null) {
            this._VenueSelectedLabel = document.getElementById(this.clientId + '_VenueSelectedLabel');
        }
        return this._VenueSelectedLabel;
    },
    
    _VenueSelectedLabel: null,
    
    get_venueSelectedLabelJ: function Js_Controls_Picker_View$get_venueSelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._VenueSelectedLabelJ == null) {
            this._VenueSelectedLabelJ = $('#' + this.clientId + '_VenueSelectedLabel');
        }
        return this._VenueSelectedLabelJ;
    },
    
    _VenueSelectedLabelJ: null,
    
    get_venueSelectedChangeLink: function Js_Controls_Picker_View$get_venueSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._VenueSelectedChangeLink == null) {
            this._VenueSelectedChangeLink = document.getElementById(this.clientId + '_VenueSelectedChangeLink');
        }
        return this._VenueSelectedChangeLink;
    },
    
    _VenueSelectedChangeLink: null,
    
    get_venueSelectedChangeLinkJ: function Js_Controls_Picker_View$get_venueSelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._VenueSelectedChangeLinkJ == null) {
            this._VenueSelectedChangeLinkJ = $('#' + this.clientId + '_VenueSelectedChangeLink');
        }
        return this._VenueSelectedChangeLinkJ;
    },
    
    _VenueSelectedChangeLinkJ: null,
    
    get_venueChoiceHolder: function Js_Controls_Picker_View$get_venueChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._VenueChoiceHolder == null) {
            this._VenueChoiceHolder = document.getElementById(this.clientId + '_VenueChoiceHolder');
        }
        return this._VenueChoiceHolder;
    },
    
    _VenueChoiceHolder: null,
    
    get_venueChoiceHolderJ: function Js_Controls_Picker_View$get_venueChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._VenueChoiceHolderJ == null) {
            this._VenueChoiceHolderJ = $('#' + this.clientId + '_VenueChoiceHolder');
        }
        return this._VenueChoiceHolderJ;
    },
    
    _VenueChoiceHolderJ: null,
    
    get_venueDropDown: function Js_Controls_Picker_View$get_venueDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._VenueDropDown == null) {
            this._VenueDropDown = document.getElementById(this.clientId + '_VenueDropDown');
        }
        return this._VenueDropDown;
    },
    
    _VenueDropDown: null,
    
    get_venueDropDownJ: function Js_Controls_Picker_View$get_venueDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._VenueDropDownJ == null) {
            this._VenueDropDownJ = $('#' + this.clientId + '_VenueDropDown');
        }
        return this._VenueDropDownJ;
    },
    
    _VenueDropDownJ: null,
    
    get_venueByLetterDropDown: function Js_Controls_Picker_View$get_venueByLetterDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._VenueByLetterDropDown == null) {
            this._VenueByLetterDropDown = document.getElementById(this.clientId + '_VenueByLetterDropDown');
        }
        return this._VenueByLetterDropDown;
    },
    
    _VenueByLetterDropDown: null,
    
    get_venueByLetterDropDownJ: function Js_Controls_Picker_View$get_venueByLetterDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._VenueByLetterDropDownJ == null) {
            this._VenueByLetterDropDownJ = $('#' + this.clientId + '_VenueByLetterDropDown');
        }
        return this._VenueByLetterDropDownJ;
    },
    
    _VenueByLetterDropDownJ: null,
    
    get_musicHolder: function Js_Controls_Picker_View$get_musicHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._MusicHolder == null) {
            this._MusicHolder = document.getElementById(this.clientId + '_MusicHolder');
        }
        return this._MusicHolder;
    },
    
    _MusicHolder: null,
    
    get_musicHolderJ: function Js_Controls_Picker_View$get_musicHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._MusicHolderJ == null) {
            this._MusicHolderJ = $('#' + this.clientId + '_MusicHolder');
        }
        return this._MusicHolderJ;
    },
    
    _MusicHolderJ: null,
    
    get_musicSelectedHolder: function Js_Controls_Picker_View$get_musicSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._MusicSelectedHolder == null) {
            this._MusicSelectedHolder = document.getElementById(this.clientId + '_MusicSelectedHolder');
        }
        return this._MusicSelectedHolder;
    },
    
    _MusicSelectedHolder: null,
    
    get_musicSelectedHolderJ: function Js_Controls_Picker_View$get_musicSelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._MusicSelectedHolderJ == null) {
            this._MusicSelectedHolderJ = $('#' + this.clientId + '_MusicSelectedHolder');
        }
        return this._MusicSelectedHolderJ;
    },
    
    _MusicSelectedHolderJ: null,
    
    get_musicSelectedLabel: function Js_Controls_Picker_View$get_musicSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._MusicSelectedLabel == null) {
            this._MusicSelectedLabel = document.getElementById(this.clientId + '_MusicSelectedLabel');
        }
        return this._MusicSelectedLabel;
    },
    
    _MusicSelectedLabel: null,
    
    get_musicSelectedLabelJ: function Js_Controls_Picker_View$get_musicSelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._MusicSelectedLabelJ == null) {
            this._MusicSelectedLabelJ = $('#' + this.clientId + '_MusicSelectedLabel');
        }
        return this._MusicSelectedLabelJ;
    },
    
    _MusicSelectedLabelJ: null,
    
    get_musicSelectedChangeLink: function Js_Controls_Picker_View$get_musicSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._MusicSelectedChangeLink == null) {
            this._MusicSelectedChangeLink = document.getElementById(this.clientId + '_MusicSelectedChangeLink');
        }
        return this._MusicSelectedChangeLink;
    },
    
    _MusicSelectedChangeLink: null,
    
    get_musicSelectedChangeLinkJ: function Js_Controls_Picker_View$get_musicSelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._MusicSelectedChangeLinkJ == null) {
            this._MusicSelectedChangeLinkJ = $('#' + this.clientId + '_MusicSelectedChangeLink');
        }
        return this._MusicSelectedChangeLinkJ;
    },
    
    _MusicSelectedChangeLinkJ: null,
    
    get_musicChoiceHolder: function Js_Controls_Picker_View$get_musicChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._MusicChoiceHolder == null) {
            this._MusicChoiceHolder = document.getElementById(this.clientId + '_MusicChoiceHolder');
        }
        return this._MusicChoiceHolder;
    },
    
    _MusicChoiceHolder: null,
    
    get_musicChoiceHolderJ: function Js_Controls_Picker_View$get_musicChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._MusicChoiceHolderJ == null) {
            this._MusicChoiceHolderJ = $('#' + this.clientId + '_MusicChoiceHolder');
        }
        return this._MusicChoiceHolderJ;
    },
    
    _MusicChoiceHolderJ: null,
    
    get_musicDropDown: function Js_Controls_Picker_View$get_musicDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._MusicDropDown == null) {
            this._MusicDropDown = document.getElementById(this.clientId + '_MusicDropDown');
        }
        return this._MusicDropDown;
    },
    
    _MusicDropDown: null,
    
    get_musicDropDownJ: function Js_Controls_Picker_View$get_musicDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._MusicDropDownJ == null) {
            this._MusicDropDownJ = $('#' + this.clientId + '_MusicDropDown');
        }
        return this._MusicDropDownJ;
    },
    
    _MusicDropDownJ: null,
    
    get_dateHolder: function Js_Controls_Picker_View$get_dateHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateHolder == null) {
            this._DateHolder = document.getElementById(this.clientId + '_DateHolder');
        }
        return this._DateHolder;
    },
    
    _DateHolder: null,
    
    get_dateHolderJ: function Js_Controls_Picker_View$get_dateHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateHolderJ == null) {
            this._DateHolderJ = $('#' + this.clientId + '_DateHolder');
        }
        return this._DateHolderJ;
    },
    
    _DateHolderJ: null,
    
    get_dateSelectedHolder: function Js_Controls_Picker_View$get_dateSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateSelectedHolder == null) {
            this._DateSelectedHolder = document.getElementById(this.clientId + '_DateSelectedHolder');
        }
        return this._DateSelectedHolder;
    },
    
    _DateSelectedHolder: null,
    
    get_dateSelectedHolderJ: function Js_Controls_Picker_View$get_dateSelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateSelectedHolderJ == null) {
            this._DateSelectedHolderJ = $('#' + this.clientId + '_DateSelectedHolder');
        }
        return this._DateSelectedHolderJ;
    },
    
    _DateSelectedHolderJ: null,
    
    get_dateSelectedLabel: function Js_Controls_Picker_View$get_dateSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateSelectedLabel == null) {
            this._DateSelectedLabel = document.getElementById(this.clientId + '_DateSelectedLabel');
        }
        return this._DateSelectedLabel;
    },
    
    _DateSelectedLabel: null,
    
    get_dateSelectedLabelJ: function Js_Controls_Picker_View$get_dateSelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateSelectedLabelJ == null) {
            this._DateSelectedLabelJ = $('#' + this.clientId + '_DateSelectedLabel');
        }
        return this._DateSelectedLabelJ;
    },
    
    _DateSelectedLabelJ: null,
    
    get_dateSelectedChangeLink: function Js_Controls_Picker_View$get_dateSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateSelectedChangeLink == null) {
            this._DateSelectedChangeLink = document.getElementById(this.clientId + '_DateSelectedChangeLink');
        }
        return this._DateSelectedChangeLink;
    },
    
    _DateSelectedChangeLink: null,
    
    get_dateSelectedChangeLinkJ: function Js_Controls_Picker_View$get_dateSelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateSelectedChangeLinkJ == null) {
            this._DateSelectedChangeLinkJ = $('#' + this.clientId + '_DateSelectedChangeLink');
        }
        return this._DateSelectedChangeLinkJ;
    },
    
    _DateSelectedChangeLinkJ: null,
    
    get_dateChoiceHolder: function Js_Controls_Picker_View$get_dateChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateChoiceHolder == null) {
            this._DateChoiceHolder = document.getElementById(this.clientId + '_DateChoiceHolder');
        }
        return this._DateChoiceHolder;
    },
    
    _DateChoiceHolder: null,
    
    get_dateChoiceHolderJ: function Js_Controls_Picker_View$get_dateChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateChoiceHolderJ == null) {
            this._DateChoiceHolderJ = $('#' + this.clientId + '_DateChoiceHolder');
        }
        return this._DateChoiceHolderJ;
    },
    
    _DateChoiceHolderJ: null,
    
    get_dateDayHolder: function Js_Controls_Picker_View$get_dateDayHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateDayHolder == null) {
            this._DateDayHolder = document.getElementById(this.clientId + '_DateDayHolder');
        }
        return this._DateDayHolder;
    },
    
    _DateDayHolder: null,
    
    get_dateDayHolderJ: function Js_Controls_Picker_View$get_dateDayHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateDayHolderJ == null) {
            this._DateDayHolderJ = $('#' + this.clientId + '_DateDayHolder');
        }
        return this._DateDayHolderJ;
    },
    
    _DateDayHolderJ: null,
    
    get_dateDayDropDown: function Js_Controls_Picker_View$get_dateDayDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateDayDropDown == null) {
            this._DateDayDropDown = document.getElementById(this.clientId + '_DateDayDropDown');
        }
        return this._DateDayDropDown;
    },
    
    _DateDayDropDown: null,
    
    get_dateDayDropDownJ: function Js_Controls_Picker_View$get_dateDayDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateDayDropDownJ == null) {
            this._DateDayDropDownJ = $('#' + this.clientId + '_DateDayDropDown');
        }
        return this._DateDayDropDownJ;
    },
    
    _DateDayDropDownJ: null,
    
    get_dateDayPlusImg: function Js_Controls_Picker_View$get_dateDayPlusImg() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateDayPlusImg == null) {
            this._DateDayPlusImg = document.getElementById(this.clientId + '_DateDayPlusImg');
        }
        return this._DateDayPlusImg;
    },
    
    _DateDayPlusImg: null,
    
    get_dateDayPlusImgJ: function Js_Controls_Picker_View$get_dateDayPlusImgJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateDayPlusImgJ == null) {
            this._DateDayPlusImgJ = $('#' + this.clientId + '_DateDayPlusImg');
        }
        return this._DateDayPlusImgJ;
    },
    
    _DateDayPlusImgJ: null,
    
    get_dateDayMinusImg: function Js_Controls_Picker_View$get_dateDayMinusImg() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateDayMinusImg == null) {
            this._DateDayMinusImg = document.getElementById(this.clientId + '_DateDayMinusImg');
        }
        return this._DateDayMinusImg;
    },
    
    _DateDayMinusImg: null,
    
    get_dateDayMinusImgJ: function Js_Controls_Picker_View$get_dateDayMinusImgJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateDayMinusImgJ == null) {
            this._DateDayMinusImgJ = $('#' + this.clientId + '_DateDayMinusImg');
        }
        return this._DateDayMinusImgJ;
    },
    
    _DateDayMinusImgJ: null,
    
    get_dateMonthDropDown: function Js_Controls_Picker_View$get_dateMonthDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateMonthDropDown == null) {
            this._DateMonthDropDown = document.getElementById(this.clientId + '_DateMonthDropDown');
        }
        return this._DateMonthDropDown;
    },
    
    _DateMonthDropDown: null,
    
    get_dateMonthDropDownJ: function Js_Controls_Picker_View$get_dateMonthDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateMonthDropDownJ == null) {
            this._DateMonthDropDownJ = $('#' + this.clientId + '_DateMonthDropDown');
        }
        return this._DateMonthDropDownJ;
    },
    
    _DateMonthDropDownJ: null,
    
    get_dateMonthPlusImg: function Js_Controls_Picker_View$get_dateMonthPlusImg() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateMonthPlusImg == null) {
            this._DateMonthPlusImg = document.getElementById(this.clientId + '_DateMonthPlusImg');
        }
        return this._DateMonthPlusImg;
    },
    
    _DateMonthPlusImg: null,
    
    get_dateMonthPlusImgJ: function Js_Controls_Picker_View$get_dateMonthPlusImgJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateMonthPlusImgJ == null) {
            this._DateMonthPlusImgJ = $('#' + this.clientId + '_DateMonthPlusImg');
        }
        return this._DateMonthPlusImgJ;
    },
    
    _DateMonthPlusImgJ: null,
    
    get_dateMonthMinusImg: function Js_Controls_Picker_View$get_dateMonthMinusImg() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateMonthMinusImg == null) {
            this._DateMonthMinusImg = document.getElementById(this.clientId + '_DateMonthMinusImg');
        }
        return this._DateMonthMinusImg;
    },
    
    _DateMonthMinusImg: null,
    
    get_dateMonthMinusImgJ: function Js_Controls_Picker_View$get_dateMonthMinusImgJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateMonthMinusImgJ == null) {
            this._DateMonthMinusImgJ = $('#' + this.clientId + '_DateMonthMinusImg');
        }
        return this._DateMonthMinusImgJ;
    },
    
    _DateMonthMinusImgJ: null,
    
    get_dateYearTextBox: function Js_Controls_Picker_View$get_dateYearTextBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateYearTextBox == null) {
            this._DateYearTextBox = document.getElementById(this.clientId + '_DateYearTextBox');
        }
        return this._DateYearTextBox;
    },
    
    _DateYearTextBox: null,
    
    get_dateYearTextBoxJ: function Js_Controls_Picker_View$get_dateYearTextBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateYearTextBoxJ == null) {
            this._DateYearTextBoxJ = $('#' + this.clientId + '_DateYearTextBox');
        }
        return this._DateYearTextBoxJ;
    },
    
    _DateYearTextBoxJ: null,
    
    get_dateYearPlusImg: function Js_Controls_Picker_View$get_dateYearPlusImg() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateYearPlusImg == null) {
            this._DateYearPlusImg = document.getElementById(this.clientId + '_DateYearPlusImg');
        }
        return this._DateYearPlusImg;
    },
    
    _DateYearPlusImg: null,
    
    get_dateYearPlusImgJ: function Js_Controls_Picker_View$get_dateYearPlusImgJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateYearPlusImgJ == null) {
            this._DateYearPlusImgJ = $('#' + this.clientId + '_DateYearPlusImg');
        }
        return this._DateYearPlusImgJ;
    },
    
    _DateYearPlusImgJ: null,
    
    get_dateYearMinusImg: function Js_Controls_Picker_View$get_dateYearMinusImg() {
        /// <value type="Object" domElement="true"></value>
        if (this._DateYearMinusImg == null) {
            this._DateYearMinusImg = document.getElementById(this.clientId + '_DateYearMinusImg');
        }
        return this._DateYearMinusImg;
    },
    
    _DateYearMinusImg: null,
    
    get_dateYearMinusImgJ: function Js_Controls_Picker_View$get_dateYearMinusImgJ() {
        /// <value type="jQueryObject"></value>
        if (this._DateYearMinusImgJ == null) {
            this._DateYearMinusImgJ = $('#' + this.clientId + '_DateYearMinusImg');
        }
        return this._DateYearMinusImgJ;
    },
    
    _DateYearMinusImgJ: null,
    
    get_eventHolder: function Js_Controls_Picker_View$get_eventHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventHolder == null) {
            this._EventHolder = document.getElementById(this.clientId + '_EventHolder');
        }
        return this._EventHolder;
    },
    
    _EventHolder: null,
    
    get_eventHolderJ: function Js_Controls_Picker_View$get_eventHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventHolderJ == null) {
            this._EventHolderJ = $('#' + this.clientId + '_EventHolder');
        }
        return this._EventHolderJ;
    },
    
    _EventHolderJ: null,
    
    get_eventSelectedHolder: function Js_Controls_Picker_View$get_eventSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventSelectedHolder == null) {
            this._EventSelectedHolder = document.getElementById(this.clientId + '_EventSelectedHolder');
        }
        return this._EventSelectedHolder;
    },
    
    _EventSelectedHolder: null,
    
    get_eventSelectedHolderJ: function Js_Controls_Picker_View$get_eventSelectedHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventSelectedHolderJ == null) {
            this._EventSelectedHolderJ = $('#' + this.clientId + '_EventSelectedHolder');
        }
        return this._EventSelectedHolderJ;
    },
    
    _EventSelectedHolderJ: null,
    
    get_eventSelectedLabel: function Js_Controls_Picker_View$get_eventSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventSelectedLabel == null) {
            this._EventSelectedLabel = document.getElementById(this.clientId + '_EventSelectedLabel');
        }
        return this._EventSelectedLabel;
    },
    
    _EventSelectedLabel: null,
    
    get_eventSelectedLabelJ: function Js_Controls_Picker_View$get_eventSelectedLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventSelectedLabelJ == null) {
            this._EventSelectedLabelJ = $('#' + this.clientId + '_EventSelectedLabel');
        }
        return this._EventSelectedLabelJ;
    },
    
    _EventSelectedLabelJ: null,
    
    get_eventSelectedChangeLink: function Js_Controls_Picker_View$get_eventSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventSelectedChangeLink == null) {
            this._EventSelectedChangeLink = document.getElementById(this.clientId + '_EventSelectedChangeLink');
        }
        return this._EventSelectedChangeLink;
    },
    
    _EventSelectedChangeLink: null,
    
    get_eventSelectedChangeLinkJ: function Js_Controls_Picker_View$get_eventSelectedChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventSelectedChangeLinkJ == null) {
            this._EventSelectedChangeLinkJ = $('#' + this.clientId + '_EventSelectedChangeLink');
        }
        return this._EventSelectedChangeLinkJ;
    },
    
    _EventSelectedChangeLinkJ: null,
    
    get_eventChoiceHolder: function Js_Controls_Picker_View$get_eventChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventChoiceHolder == null) {
            this._EventChoiceHolder = document.getElementById(this.clientId + '_EventChoiceHolder');
        }
        return this._EventChoiceHolder;
    },
    
    _EventChoiceHolder: null,
    
    get_eventChoiceHolderJ: function Js_Controls_Picker_View$get_eventChoiceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventChoiceHolderJ == null) {
            this._EventChoiceHolderJ = $('#' + this.clientId + '_EventChoiceHolder');
        }
        return this._EventChoiceHolderJ;
    },
    
    _EventChoiceHolderJ: null,
    
    get_eventListBox: function Js_Controls_Picker_View$get_eventListBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventListBox == null) {
            this._EventListBox = document.getElementById(this.clientId + '_EventListBox');
        }
        return this._EventListBox;
    },
    
    _EventListBox: null,
    
    get_eventListBoxJ: function Js_Controls_Picker_View$get_eventListBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventListBoxJ == null) {
            this._EventListBoxJ = $('#' + this.clientId + '_EventListBox');
        }
        return this._EventListBoxJ;
    },
    
    _EventListBoxJ: null
}


Js.Controls.Picker.Controller.registerClass('Js.Controls.Picker.Controller');
Js.Controls.Picker.DateStub.registerClass('Js.Controls.Picker.DateStub');
Js.Controls.Picker.ObjectStub.registerClass('Js.Controls.Picker.ObjectStub');
Js.Controls.Picker.EventSelectionSpecification.registerClass('Js.Controls.Picker.EventSelectionSpecification');
Js.Controls.Picker.StringArgs.registerClass('Js.Controls.Picker.StringArgs', ss.EventArgs);
Js.Controls.Picker.ObjectArgs.registerClass('Js.Controls.Picker.ObjectArgs', ss.EventArgs);
Js.Controls.Picker.DateArgs.registerClass('Js.Controls.Picker.DateArgs', ss.EventArgs);
Js.Controls.Picker.EventSelectionArgs.registerClass('Js.Controls.Picker.EventSelectionArgs', ss.EventArgs);
Js.Controls.Picker.View.registerClass('Js.Controls.Picker.View');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
