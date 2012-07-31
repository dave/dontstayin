Type.registerNamespace('SpottedScript.Controls.Picker');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.Controller
SpottedScript.Controls.Picker.Controller = function SpottedScript_Controls_Picker_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.Picker.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.Picker.View">
    /// </field>
    /// <field name="_firstEverNavigate" type="Boolean">
    /// </field>
    /// <field name="eventSelectionSepcificationChanged" type="SpottedScript.Controls.Picker.EventSelectionEvent">
    /// </field>
    /// <field name="_navigateSearchTypePreviousValue" type="String">
    /// </field>
    /// <field name="selectedKeyChanged" type="SpottedScript.Controls.Picker.StringEvent">
    /// </field>
    /// <field name="_keyControlIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_navigateKeyPreviousValue" type="String">
    /// </field>
    /// <field name="selectedSpotterChanged" type="SpottedScript.Controls.Picker.StringEvent">
    /// </field>
    /// <field name="_spotterControlIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_navigateSpotterPreviousValue" type="String">
    /// </field>
    /// <field name="selectedBrandChanged" type="SpottedScript.Controls.Picker.ObjectEvent">
    /// </field>
    /// <field name="_brandDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_navigateBrandPreviousValue" type="String">
    /// </field>
    /// <field name="selectedCountryChanged" type="SpottedScript.Controls.Picker.ObjectEvent">
    /// </field>
    /// <field name="_countryDropDownJ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="_countryDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_navigateCountryPreviousValue" type="String">
    /// </field>
    /// <field name="selectedPlaceChanged" type="SpottedScript.Controls.Picker.ObjectEvent">
    /// </field>
    /// <field name="_placeDropDownJ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="_placeDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_placeDropDownCountryK" type="Number" integer="true">
    /// </field>
    /// <field name="_placeDropDownPreviouslySelectedIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_navigatePlacePreviousValue" type="String">
    /// </field>
    /// <field name="selectedVenueChanged" type="SpottedScript.Controls.Picker.ObjectEvent">
    /// </field>
    /// <field name="_venueDropDownJ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="_venueByLetterDropDownJ" type="JQ.JQueryObject">
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
    /// <field name="selectedMusicChanged" type="SpottedScript.Controls.Picker.ObjectEvent">
    /// </field>
    /// <field name="_musicDropDownJ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="_musicDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_musicDropDownPreviouslySelectedIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_navigateMusicPreviousValue" type="String">
    /// </field>
    /// <field name="selectedDateChanged" type="SpottedScript.Controls.Picker.DateEvent">
    /// </field>
    /// <field name="_dateDayDropDownJ" type="JQ.JQueryObject">
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
    /// <field name="selectedEventChanged" type="SpottedScript.Controls.Picker.ObjectEvent">
    /// </field>
    /// <field name="_eventListBoxJ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="_eventDropDownIsInitialised" type="Boolean">
    /// </field>
    /// <field name="_eventDropDownVenueK" type="Number" integer="true">
    /// </field>
    /// <field name="_eventDropDownBrandK" type="Number" integer="true">
    /// </field>
    /// <field name="_eventDropDownKey" type="String">
    /// </field>
    /// <field name="_eventDropDownDate" type="SpottedScript.Controls.Picker.DateStub">
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
    this._regexQuote = new RegExp('\'');
    this._view = view;
    Sys.Application.add_navigate(Function.createDelegate(this, this._navigate));
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.Controls.Picker.Controller.prototype = {
    _view: null,
    _initialise: function SpottedScript_Controls_Picker_Controller$_initialise() {
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
        if (SpottedScript.Misc.get_browserIsIE()) {
            this._addHistory('', '');
        }
        this._updateUI();
        this._initialiseFirstUnknownControl();
        this._view.get_holder().style.display = '';
    },
    _firstEverNavigate: true,
    _navigate: function SpottedScript_Controls_Picker_Controller$_navigate(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Sys.HistoryEventArgs">
        /// </param>
        if (e.get_state()[this._view.clientId + '_SearchType'] != null || e.get_state()[this._view.clientId + '_Key'] != null || e.get_state()[this._view.clientId + '_Spotter'] != null || e.get_state()[this._view.clientId + '_Brand'] != null || e.get_state()[this._view.clientId + '_Country'] != null || e.get_state()[this._view.clientId + '_Place'] != null || e.get_state()[this._view.clientId + '_Venue'] != null || e.get_state()[this._view.clientId + '_Music'] != null || e.get_state()[this._view.clientId + '_Date'] != null || e.get_state()[this._view.clientId + '_Event'] != null) {
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
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(this.getCurrentEventSelectionSepcification()));
                }
                if (this.get__event() != null && (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.key || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue) && this.selectedEventChanged != null) {
                    this.selectedEventChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(this.get__event()));
                }
                if (this.get__venue() != null && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue && this.selectedVenueChanged != null) {
                    this.selectedVenueChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(this.get__venue()));
                }
                if (this.get__place() != null && (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) && this.selectedPlaceChanged != null) {
                    this.selectedPlaceChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(this.get__place()));
                }
                if (this.get__country() != null && (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) && this.selectedCountryChanged != null) {
                    this.selectedCountryChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(this.get__country()));
                }
                if (this.get__brand() != null && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand && this.selectedBrandChanged != null) {
                    this.selectedBrandChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(this.get__brand()));
                }
                if (this.get__spotter() != null && this.get__spotter().length > 0 && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.spotter && this.selectedSpotterChanged != null) {
                    this.selectedSpotterChanged(this, new SpottedScript.Controls.Picker.StringArgs(this.get__spotter()));
                }
                if (this.get__key() != null && this.get__key().length > 0 && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.key && this.selectedKeyChanged != null) {
                    this.selectedKeyChanged(this, new SpottedScript.Controls.Picker.StringArgs(this.get__key()));
                }
            }
        }
    },
    _getVal: function SpottedScript_Controls_Picker_Controller$_getVal(e, item) {
        /// <param name="e" type="Sys.HistoryEventArgs">
        /// </param>
        /// <param name="item" type="String">
        /// </param>
        /// <returns type="String"></returns>
        return (e.get_state()[this._view.clientId + '_' + item] == null) ? '' : decodeURI(e.get_state()[this._view.clientId + '_' + item].toString());
    },
    _updateUI: function SpottedScript_Controls_Picker_Controller$_updateUI() {
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
    eventSelectionSepcificationChanged: null,
    getCurrentEventSelectionSepcification: function SpottedScript_Controls_Picker_Controller$getCurrentEventSelectionSepcification() {
        /// <returns type="SpottedScript.Controls.Picker.EventSelectionSpecification"></returns>
        return new SpottedScript.Controls.Picker.EventSelectionSpecification((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) ? this.get__brand() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__place() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue) ? this.get__venue() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__music() : null, this.get__date(), this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.me);
    },
    fireEventSelectionChange: function SpottedScript_Controls_Picker_Controller$fireEventSelectionChange() {
        if (this.eventSelectionSepcificationChanged != null) {
            this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(this.getCurrentEventSelectionSepcification()));
        }
    },
    get__searchType: function SpottedScript_Controls_Picker_Controller$get__searchType() {
        /// <value type="SpottedScript.Controls.Picker.Shared.SearchTypes"></value>
        if (this.get__searchTypeHasMultipleOptions()) {
            return (this._view.get_selectedSearchTypeHidden().value === 'key') ? SpottedScript.Controls.Picker.Shared.SearchTypes.key : (this._view.get_selectedSearchTypeHidden().value === 'me') ? SpottedScript.Controls.Picker.Shared.SearchTypes.me : (this._view.get_selectedSearchTypeHidden().value === 'spotter') ? SpottedScript.Controls.Picker.Shared.SearchTypes.spotter : (this._view.get_selectedSearchTypeHidden().value === 'venue') ? SpottedScript.Controls.Picker.Shared.SearchTypes.venue : (this._view.get_selectedSearchTypeHidden().value === 'brand') ? SpottedScript.Controls.Picker.Shared.SearchTypes.brand : (this._view.get_selectedSearchTypeHidden().value === 'music') ? SpottedScript.Controls.Picker.Shared.SearchTypes.music : (this._view.get_selectedSearchTypeHidden().value === 'google') ? SpottedScript.Controls.Picker.Shared.SearchTypes.google : SpottedScript.Controls.Picker.Shared.SearchTypes.unknown;
        }
        else if (this.get__optionKey()) {
            return SpottedScript.Controls.Picker.Shared.SearchTypes.key;
        }
        else if (this.get__optionMe()) {
            return SpottedScript.Controls.Picker.Shared.SearchTypes.me;
        }
        else if (this.get__optionSpotter()) {
            return SpottedScript.Controls.Picker.Shared.SearchTypes.spotter;
        }
        else if (this.get__optionBrand()) {
            return SpottedScript.Controls.Picker.Shared.SearchTypes.brand;
        }
        else if (this.get__optionCountry()) {
            return SpottedScript.Controls.Picker.Shared.SearchTypes.venue;
        }
        else if (this.get__optionMusic()) {
            return SpottedScript.Controls.Picker.Shared.SearchTypes.music;
        }
        else if (this.get__optionGoogle()) {
            return SpottedScript.Controls.Picker.Shared.SearchTypes.google;
        }
        else {
            return SpottedScript.Controls.Picker.Shared.SearchTypes.unknown;
        }
    },
    get__searchTypeHasMultipleOptions: function SpottedScript_Controls_Picker_Controller$get__searchTypeHasMultipleOptions() {
        /// <value type="Boolean"></value>
        var options = ((this.get__optionKey()) ? 1 : 0) + ((this.get__optionMe()) ? 1 : 0) + ((this.get__optionSpotter()) ? 1 : 0) + ((this.get__optionBrand()) ? 1 : 0) + ((this.get__optionCountry()) ? 1 : 0) + ((this.get__optionMusic()) ? 1 : 0) + ((this.get__optionGoogle()) ? 1 : 0);
        return options > 1;
    },
    _initialiseSearchType: function SpottedScript_Controls_Picker_Controller$_initialiseSearchType() {
        $addHandler(this._view.get_searchTypeKey(), 'click', Function.createDelegate(this, this._searchTypeRadioClick));
        $addHandler(this._view.get_searchTypeMe(), 'click', Function.createDelegate(this, this._searchTypeRadioClick));
        $addHandler(this._view.get_searchTypeSpotter(), 'click', Function.createDelegate(this, this._searchTypeRadioClick));
        $addHandler(this._view.get_searchTypeVenue(), 'click', Function.createDelegate(this, this._searchTypeRadioClick));
        $addHandler(this._view.get_searchTypeBrand(), 'click', Function.createDelegate(this, this._searchTypeRadioClick));
        $addHandler(this._view.get_searchTypeMusic(), 'click', Function.createDelegate(this, this._searchTypeRadioClick));
        $addHandler(this._view.get_searchTypeGoogle(), 'click', Function.createDelegate(this, this._searchTypeRadioClick));
        this._view.get_searchTypeKey().parentNode.style.display = (this.get__optionKey()) ? 'block' : 'none';
        this._view.get_searchTypeMe().parentNode.style.display = (this.get__optionMe()) ? 'block' : 'none';
        this._view.get_searchTypeSpotter().parentNode.style.display = (this.get__optionSpotter()) ? 'block' : 'none';
        this._view.get_searchTypeVenue().parentNode.style.display = (this.get__optionVenue()) ? 'block' : 'none';
        this._view.get_searchTypeBrand().parentNode.style.display = (this.get__optionBrand()) ? 'block' : 'none';
        this._view.get_searchTypeMusic().parentNode.style.display = (this.get__optionMusic()) ? 'block' : 'none';
        this._view.get_searchTypeGoogle().parentNode.style.display = (this.get__optionGoogle()) ? 'block' : 'none';
    },
    _searchTypeRadioClick: function SpottedScript_Controls_Picker_Controller$_searchTypeRadioClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var value = (this._view.get_searchTypeKey().checked) ? 'key' : (this._view.get_searchTypeMe().checked) ? 'me' : (this._view.get_searchTypeSpotter().checked) ? 'spotter' : (this._view.get_searchTypeVenue().checked) ? 'venue' : (this._view.get_searchTypeBrand().checked) ? 'brand' : (this._view.get_searchTypeMusic().checked) ? 'music' : (this._view.get_searchTypeGoogle().checked) ? 'google' : 'unknown';
        if (value === 'unknown') {
            if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.key) {
                this._view.get_searchTypeKey().checked = true;
            }
            else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.me) {
                this._view.get_searchTypeMe().checked = true;
            }
            else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.spotter) {
                this._view.get_searchTypeSpotter().checked = true;
            }
            else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue) {
                this._view.get_searchTypeVenue().checked = true;
            }
            else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) {
                this._view.get_searchTypeBrand().checked = true;
            }
            else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) {
                this._view.get_searchTypeMusic().checked = true;
            }
            else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.google) {
                this._view.get_searchTypeGoogle().checked = true;
            }
            if (!this._firstEverNavigate) {
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            this._addHistory('SearchType', value);
            this._initialiseFirstUnknownControl();
            this._initialiseEventDropDown();
            if (!this._firstEverNavigate) {
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(this.getCurrentEventSelectionSepcification()));
                }
            }
        }
    },
    _initialiseFirstUnknownControl: function SpottedScript_Controls_Picker_Controller$_initialiseFirstUnknownControl() {
        if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.spotter) {
            if (this.get__spotter().length === 0) {
                this._initialiseSpotterControl();
            }
        }
        else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.key) {
            if (this.get__key().length === 0) {
                this._initialiseKeyControl();
            }
        }
        else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) {
            if (this.get__brand() == null) {
                this._initialiseBrandDropDown();
            }
            else if (this.get__event() == null) {
                this._initialiseEventDropDown();
            }
        }
        else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) {
            if (this.get__country() == null) {
                this._initialiseCountryDropDown();
            }
            else if (this.get__place() == null) {
                this._initialisePlaceDropDown();
            }
            else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue && this.get__venue() == null || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) {
                if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue && this.get__venue() == null) {
                    this._initialiseVenueDropDown(null);
                }
                else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) {
                    this._initialiseMusicDropDown();
                }
            }
            else if (this.get__event() == null) {
                this._initialiseEventDropDown();
            }
        }
        else if (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.google) {
        }
    },
    _navigateSearchTypePreviousValue: '',
    _navigateSearchType: function SpottedScript_Controls_Picker_Controller$_navigateSearchType(value) {
        /// <param name="value" type="String">
        /// </param>
        if (value.length === 0 || value === this._navigateSearchTypePreviousValue) {
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
    },
    _updateUISearchType: function SpottedScript_Controls_Picker_Controller$_updateUISearchType() {
        this._view.get_searchTypeHolder().style.display = (this.get__searchTypeHasMultipleOptions()) ? '' : 'none';
    },
    get__optionKey: function SpottedScript_Controls_Picker_Controller$get__optionKey() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionKeyHidden().value);
    },
    selectedKeyChanged: null,
    _keyControlIsInitialised: false,
    get__key: function SpottedScript_Controls_Picker_Controller$get__key() {
        /// <value type="String"></value>
        if (!this.get__optionKey() || this._view.get_selectedKeyHidden().value == null) {
            return '';
        }
        return this._view.get_selectedKeyHidden().value;
    },
    _initialiseKey: function SpottedScript_Controls_Picker_Controller$_initialiseKey() {
        if (!this.get__optionKey()) {
            return;
        }
        $addHandler(this._view.get_keySearchButton(), 'click', Function.createDelegate(this, this._keyChange));
        $addHandler(this._view.get_keySelectedChangeLink(), 'click', Function.createDelegate(this, this._keySelectedChangeLinkClick));
    },
    _keySelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_keySelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialiseKeyControl();
    },
    _initialiseKeyControl: function SpottedScript_Controls_Picker_Controller$_initialiseKeyControl() {
        if (!this.get__optionKey()) {
            return;
        }
        this._keyControlIsInitialised = true;
        if (this.get__key() != null && this.get__key().length > 0) {
            this._view.get_keyTextBox().value = this.get__key();
        }
        this._updateUIKey(true);
    },
    _keyChange: function SpottedScript_Controls_Picker_Controller$_keyChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._view.get_keyTextBox().value == null || this._view.get_keyTextBox().value.length === 0) {
            return;
        }
        this._addHistory('Key', this._view.get_keyTextBox().value);
        this._initialiseEventDropDown();
    },
    _navigateKeyPreviousValue: '',
    _navigateKey: function SpottedScript_Controls_Picker_Controller$_navigateKey(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionKey() || value.length === 0 || value === this._navigateKeyPreviousValue) {
            return;
        }
        if (value === '0') {
            this._view.get_keyTextBox().value = '';
            this._view.get_keySelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedKeyHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedKeyChanged != null) {
                    this.selectedKeyChanged(this, new SpottedScript.Controls.Picker.StringArgs(null));
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
                    this.selectedKeyChanged(this, new SpottedScript.Controls.Picker.StringArgs(value));
                }
            }
        }
        this._navigateKeyPreviousValue = value;
    },
    _updateUIKey: function SpottedScript_Controls_Picker_Controller$_updateUIKey(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_keyHolder().style.display = (this.get__optionKey() && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.key) ? '' : 'none';
        this._view.get_keySelectedHolder().style.display = (!this._keyControlIsInitialised) ? '' : 'none';
        this._view.get_keyChoiceHolder().style.display = (this._keyControlIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIEvent();
        }
    },
    get__optionMe: function SpottedScript_Controls_Picker_Controller$get__optionMe() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionMeHidden().value);
    },
    _initialiseMe: function SpottedScript_Controls_Picker_Controller$_initialiseMe() {
    },
    get__optionSpotter: function SpottedScript_Controls_Picker_Controller$get__optionSpotter() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionSpotterHidden().value);
    },
    selectedSpotterChanged: null,
    _spotterControlIsInitialised: false,
    get__spotter: function SpottedScript_Controls_Picker_Controller$get__spotter() {
        /// <value type="String"></value>
        if (!this.get__optionSpotter() || this._view.get_selectedSpotterHidden().value == null) {
            return '';
        }
        return this._view.get_selectedSpotterHidden().value;
    },
    _initialiseSpotter: function SpottedScript_Controls_Picker_Controller$_initialiseSpotter() {
        if (!this.get__optionSpotter()) {
            return;
        }
        $addHandler(this._view.get_spotterSearchButton(), 'click', Function.createDelegate(this, this._spotterChange));
        $addHandler(this._view.get_spotterSelectedChangeLink(), 'click', Function.createDelegate(this, this._spotterSelectedChangeLinkClick));
    },
    _spotterSelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_spotterSelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialiseSpotterControl();
    },
    _initialiseSpotterControl: function SpottedScript_Controls_Picker_Controller$_initialiseSpotterControl() {
        if (!this.get__optionSpotter()) {
            return;
        }
        this._spotterControlIsInitialised = true;
        if (this.get__spotter() != null && this.get__spotter().length > 0) {
            this._view.get_spotterTextBox().value = this.get__spotter();
        }
        this._updateUISpotter();
    },
    _spotterChange: function SpottedScript_Controls_Picker_Controller$_spotterChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._view.get_spotterTextBox().value == null || this._view.get_spotterTextBox().value.length === 0) {
            return;
        }
        this._addHistory('Spotter', this._view.get_spotterTextBox().value);
    },
    _navigateSpotterPreviousValue: '',
    _navigateSpotter: function SpottedScript_Controls_Picker_Controller$_navigateSpotter(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionSpotter() || value.length === 0 || value === this._navigateSpotterPreviousValue) {
            return;
        }
        if (value.length === 0) {
            this._view.get_spotterTextBox().value = '';
            this._view.get_spotterSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedSpotterHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedSpotterChanged != null) {
                    this.selectedSpotterChanged(this, new SpottedScript.Controls.Picker.StringArgs(null));
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
                    this.selectedSpotterChanged(this, new SpottedScript.Controls.Picker.StringArgs(value));
                }
            }
        }
        this._navigateSpotterPreviousValue = value;
    },
    _updateUISpotter: function SpottedScript_Controls_Picker_Controller$_updateUISpotter() {
        this._view.get_spotterHolder().style.display = (this.get__optionSpotter() && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.spotter) ? '' : 'none';
        this._view.get_spotterSelectedHolder().style.display = (!this._spotterControlIsInitialised) ? '' : 'none';
        this._view.get_spotterChoiceHolder().style.display = (this._spotterControlIsInitialised) ? '' : 'none';
    },
    get__optionBrand: function SpottedScript_Controls_Picker_Controller$get__optionBrand() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionBrandHidden().value);
    },
    selectedBrandChanged: null,
    _brandDropDownIsInitialised: false,
    get__brand: function SpottedScript_Controls_Picker_Controller$get__brand() {
        /// <value type="SpottedScript.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionBrand() || this._view.get_selectedBrandHidden().value == null || this._view.get_selectedBrandHidden().value.length === 0) {
            return null;
        }
        return SpottedScript.Controls.Picker.ObjectStub.fromString(this._view.get_selectedBrandHidden().value);
    },
    _initialiseBrand: function SpottedScript_Controls_Picker_Controller$_initialiseBrand() {
        if (!this.get__optionBrand()) {
            return;
        }
        this._view.get_brandAutoComplete().itemChosen = Function.createDelegate(this, this._brandDropDownChange);
        $addHandler(this._view.get_brandSelectedChangeLink(), 'click', Function.createDelegate(this, this._brandSelectedChangeLinkClick));
    },
    _brandSelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_brandSelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialiseBrandDropDown();
    },
    _initialiseBrandDropDown: function SpottedScript_Controls_Picker_Controller$_initialiseBrandDropDown() {
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
    _brandDropDownChange: function SpottedScript_Controls_Picker_Controller$_brandDropDownChange(e) {
        /// <param name="e" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        if (e.value == null || e.value.length === 0) {
            return;
        }
        var brand = new SpottedScript.Controls.Picker.ObjectStub(Number.parseInvariant(e.value), e.key);
        if (brand.k === 0) {
            return;
        }
        else {
            this._addHistory('Brand', brand.toString());
            this._initialiseDateDropDowns();
            this._initialiseEventDropDown();
        }
    },
    _navigateBrandPreviousValue: '',
    _navigateBrand: function SpottedScript_Controls_Picker_Controller$_navigateBrand(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionBrand() || value.length === 0 || value === this._navigateBrandPreviousValue) {
            return;
        }
        if (value === '0') {
            this._view.get_brandAutoComplete().set_text('');
            this._view.get_brandAutoComplete().set_value('');
            this._view.get_brandSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedBrandHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedBrandChanged != null) {
                    this.selectedBrandChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var brand = SpottedScript.Controls.Picker.ObjectStub.fromString(value);
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
                    this.selectedBrandChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(brand));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(new SpottedScript.Controls.Picker.EventSelectionSpecification((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) ? brand : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__place() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue) ? this.get__venue() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__music() : null, this.get__date(), this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.me)));
                }
            }
        }
        this._navigateBrandPreviousValue = value;
    },
    _updateUIBrand: function SpottedScript_Controls_Picker_Controller$_updateUIBrand(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_brandHolder().style.display = (this.get__optionBrand() && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) ? '' : 'none';
        this._view.get_brandSelectedHolder().style.display = (!this._brandDropDownIsInitialised) ? '' : 'none';
        this._view.get_brandChoiceHolder().style.display = (this._brandDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIDate(true);
        }
    },
    get__optionCountry: function SpottedScript_Controls_Picker_Controller$get__optionCountry() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionCountryHidden().value);
    },
    selectedCountryChanged: null,
    _countryDropDownJ: null,
    _countryDropDownIsInitialised: false,
    get__country: function SpottedScript_Controls_Picker_Controller$get__country() {
        /// <value type="SpottedScript.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionCountry() || this._view.get_selectedCountryHidden().value == null || this._view.get_selectedCountryHidden().value.length === 0) {
            return null;
        }
        return SpottedScript.Controls.Picker.ObjectStub.fromString(this._view.get_selectedCountryHidden().value);
    },
    _initialiseCountry: function SpottedScript_Controls_Picker_Controller$_initialiseCountry() {
        if (!this.get__optionCountry()) {
            return;
        }
        this._countryDropDownJ = jQuery(this._view.get_countryDropDown());
        $addHandler(this._view.get_countryDropDown(), 'change', Function.createDelegate(this, this._countryDropDownChange));
        $addHandler(this._view.get_countrySelectedChangeLink(), 'click', Function.createDelegate(this, this._countrySelectedChangeLinkClick));
    },
    _countrySelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_countrySelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialiseCountryDropDown();
    },
    _initialiseCountryDropDown: function SpottedScript_Controls_Picker_Controller$_initialiseCountryDropDown() {
        if (!this.get__optionCountry()) {
            return;
        }
        if (!this._countryDropDownIsInitialised) {
            this._countryDropDownJ.ajaxAddOption('/support/getcached.aspx?type=country', null, false, Function.createDelegate(this, this._countryDropDownInitialised), null);
        }
        else {
            this._countryDropDownInitialised();
        }
    },
    _countryDropDownInitialised: function SpottedScript_Controls_Picker_Controller$_countryDropDownInitialised() {
        this._countryDropDownIsInitialised = true;
        if (this.get__country() != null) {
            var setValue = this._setK(this._view.get_countryDropDown(), this.get__country().k);
            if (!setValue) {
                this._addHistorys([ 'Country', '0', 'Place', '0', 'Venue', '0', 'Event', '0' ]);
            }
        }
        this._updateUICountry(true);
    },
    _countryDropDownChange: function SpottedScript_Controls_Picker_Controller$_countryDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var country = new SpottedScript.Controls.Picker.ObjectStub(this._getK(this._view.get_countryDropDown()), this._getText(this._view.get_countryDropDown()));
        if (country.k === 0) {
            if (this.get__country() == null) {
                this._setIndex(this._view.get_countryDropDown(), 0);
            }
            else {
                this._setK(this._view.get_countryDropDown(), this.get__country().k);
            }
        }
        else {
            this._addHistory('Country', country.toString());
            this._initialisePlaceDropDown();
        }
    },
    _navigateCountryPreviousValue: '',
    _navigateCountry: function SpottedScript_Controls_Picker_Controller$_navigateCountry(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionCountry() || value.length === 0 || value === this._navigateCountryPreviousValue) {
            return;
        }
        if (value === '0') {
            this._setIndex(this._view.get_countryDropDown(), 0);
            this._view.get_countrySelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedCountryHidden().value = '';
            if (this.selectedCountryChanged != null) {
                this.selectedCountryChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(null));
            }
        }
        else {
            var country = SpottedScript.Controls.Picker.ObjectStub.fromString(value);
            if (this._countryDropDownIsInitialised && this._getK(this._view.get_countryDropDown()) !== country.k) {
                this._setK(this._view.get_countryDropDown(), country.k);
            }
            if (!this._countryDropDownIsInitialised) {
                this._view.get_countrySelectedLabel().innerHTML = country.name;
            }
            this._view.get_selectedCountryHidden().value = country.toString();
            if (this.selectedCountryChanged != null) {
                this.selectedCountryChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(country));
            }
        }
        this._navigateCountryPreviousValue = value;
    },
    _updateUICountry: function SpottedScript_Controls_Picker_Controller$_updateUICountry(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_countryHolder().style.display = (this.get__optionCountry() && (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music)) ? '' : 'none';
        this._view.get_countrySelectedHolder().style.display = (!this._countryDropDownIsInitialised) ? '' : 'none';
        this._view.get_countryChoiceHolder().style.display = (this._countryDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIPlace(true);
        }
    },
    get__optionPlace: function SpottedScript_Controls_Picker_Controller$get__optionPlace() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionPlaceHidden().value);
    },
    selectedPlaceChanged: null,
    _placeDropDownJ: null,
    _placeDropDownIsInitialised: false,
    _placeDropDownCountryK: 0,
    _placeDropDownPreviouslySelectedIndex: 0,
    get__place: function SpottedScript_Controls_Picker_Controller$get__place() {
        /// <value type="SpottedScript.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionPlace() || this._view.get_selectedPlaceHidden().value == null || this._view.get_selectedPlaceHidden().value.length === 0) {
            return null;
        }
        return SpottedScript.Controls.Picker.ObjectStub.fromString(this._view.get_selectedPlaceHidden().value);
    },
    _initialisePlace: function SpottedScript_Controls_Picker_Controller$_initialisePlace() {
        if (!this.get__optionPlace()) {
            return;
        }
        this._placeDropDownJ = jQuery(this._view.get_placeDropDown());
        $addHandler(this._view.get_placeDropDown(), 'change', Function.createDelegate(this, this._placeDropDownChange));
        $addHandler(this._view.get_placeSelectedChangeLink(), 'click', Function.createDelegate(this, this._placeSelectedChangeLinkClick));
    },
    _placeSelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_placeSelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialisePlaceDropDown();
    },
    _initialisePlaceDropDown: function SpottedScript_Controls_Picker_Controller$_initialisePlaceDropDown() {
        if (!this.get__optionPlace()) {
            return;
        }
        if (!this._placeDropDownIsInitialised || this._placeDropDownCountryK !== this.get__country().k) {
            this._placeDropDownCountryK = this.get__country().k;
            this._placeDropDownJ.ajaxAddOption('/support/getcached.aspx?type=place&countryk=' + this.get__country().k + '&return=k', null, false, Function.createDelegate(this, this._placeDropDownInitialised), null);
        }
        else {
            this._placeDropDownInitialised();
        }
    },
    _placeDropDownInitialised: function SpottedScript_Controls_Picker_Controller$_placeDropDownInitialised() {
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
    _placeDropDownChange: function SpottedScript_Controls_Picker_Controller$_placeDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var k = this._getK(this._view.get_placeDropDown());
        if (k > 0) {
            var place = new SpottedScript.Controls.Picker.ObjectStub(this._getK(this._view.get_placeDropDown()), this._getText(this._view.get_placeDropDown()));
            this._addHistory('Place', place.toString());
            this._initialiseVenueDropDown(null);
            this._initialiseDateDropDowns();
            this._initialiseMusicDropDown();
        }
        else {
            this._setIndex(this._view.get_placeDropDown(), this._placeDropDownPreviouslySelectedIndex);
        }
        this._placeDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_placeDropDown());
    },
    _navigatePlacePreviousValue: '',
    _navigatePlace: function SpottedScript_Controls_Picker_Controller$_navigatePlace(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionPlace() || value.length === 0 || value === this._navigatePlacePreviousValue) {
            return;
        }
        if (value === '0') {
            this._setIndex(this._view.get_placeDropDown(), 0);
            this._view.get_placeSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedPlaceHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedPlaceChanged != null) {
                    this.selectedPlaceChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var place = SpottedScript.Controls.Picker.ObjectStub.fromString(value);
            if (this._placeDropDownIsInitialised && this._getK(this._view.get_placeDropDown()) !== place.k) {
                this._setK(this._view.get_placeDropDown(), place.k);
            }
            if (!this._placeDropDownIsInitialised) {
                this._view.get_placeSelectedLabel().innerHTML = place.name;
            }
            this._view.get_selectedPlaceHidden().value = place.toString();
            if (!this._firstEverNavigate) {
                if (this.selectedPlaceChanged != null) {
                    this.selectedPlaceChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(place));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(new SpottedScript.Controls.Picker.EventSelectionSpecification((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) ? this.get__brand() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? place : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue) ? this.get__venue() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__music() : null, this.get__date(), this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.me)));
                }
            }
        }
        this._navigatePlacePreviousValue = value;
    },
    _updateUIPlace: function SpottedScript_Controls_Picker_Controller$_updateUIPlace(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_placeHolder().style.display = (this.get__optionPlace() && (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) && this.get__country() != null && (!this._placeDropDownIsInitialised || this._placeDropDownCountryK === this.get__country().k)) ? '' : 'none';
        this._view.get_placeSelectedHolder().style.display = (!this._placeDropDownIsInitialised) ? '' : 'none';
        this._view.get_placeChoiceHolder().style.display = (this._placeDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIVenue(true);
        }
    },
    get__optionVenue: function SpottedScript_Controls_Picker_Controller$get__optionVenue() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionVenueHidden().value);
    },
    selectedVenueChanged: null,
    _venueDropDownJ: null,
    _venueByLetterDropDownJ: null,
    _venueDropDownIsInitialised: false,
    _venueDropDownPlaceK: 0,
    _venueByLetterDropDownIsInitialised: false,
    _venueByLetterDropDownPlaceK: 0,
    _venueByLetterDropDownLetter: null,
    _venueDropDownPreviouslySelectedIndex: 0,
    get__venue: function SpottedScript_Controls_Picker_Controller$get__venue() {
        /// <value type="SpottedScript.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionVenue() || this._view.get_selectedVenueHidden().value == null || this._view.get_selectedVenueHidden().value.length === 0) {
            return null;
        }
        return SpottedScript.Controls.Picker.ObjectStub.fromString(this._view.get_selectedVenueHidden().value);
    },
    get__venueDropDownIsVenueSelectedCurrently: function SpottedScript_Controls_Picker_Controller$get__venueDropDownIsVenueSelectedCurrently() {
        /// <value type="Boolean"></value>
        return this.get__venueDropDownVenueSelectedCurrently() > 0;
    },
    get__venueDropDownVenueSelectedCurrently: function SpottedScript_Controls_Picker_Controller$get__venueDropDownVenueSelectedCurrently() {
        /// <value type="Number" integer="true"></value>
        return this._getK(this._view.get_venueDropDown());
    },
    get__venueDropDownIsLetterSelectedCurrently: function SpottedScript_Controls_Picker_Controller$get__venueDropDownIsLetterSelectedCurrently() {
        /// <value type="Boolean"></value>
        return this.get__venueDropDownLetterSelectedCurrently().length > 0;
    },
    get__venueDropDownLetterSelectedCurrently: function SpottedScript_Controls_Picker_Controller$get__venueDropDownLetterSelectedCurrently() {
        /// <value type="String"></value>
        var value = this._getValue(this._view.get_venueDropDown());
        if (value.indexOf('*') > -1) {
            return value.substr(value.length - 1, 1);
        }
        else {
            return '';
        }
    },
    get__venueByLetterDropDownIsVenueSelectedCurrently: function SpottedScript_Controls_Picker_Controller$get__venueByLetterDropDownIsVenueSelectedCurrently() {
        /// <value type="Boolean"></value>
        return this.get__venueByLetterDropDownVenueSelectedCurrently() > 0;
    },
    get__venueByLetterDropDownVenueSelectedCurrently: function SpottedScript_Controls_Picker_Controller$get__venueByLetterDropDownVenueSelectedCurrently() {
        /// <value type="Number" integer="true"></value>
        return this._getK(this._view.get_venueByLetterDropDown());
    },
    _initialiseVenue: function SpottedScript_Controls_Picker_Controller$_initialiseVenue() {
        if (!this.get__optionVenue()) {
            return;
        }
        this._venueDropDownJ = jQuery(this._view.get_venueDropDown());
        this._venueByLetterDropDownJ = jQuery(this._view.get_venueByLetterDropDown());
        $addHandler(this._view.get_venueDropDown(), 'change', Function.createDelegate(this, this._venueDropDownChange));
        $addHandler(this._view.get_venueByLetterDropDown(), 'change', Function.createDelegate(this, this._venueByLetterDropDownChange));
        $addHandler(this._view.get_venueSelectedChangeLink(), 'click', Function.createDelegate(this, this._venueSelectedChangeLinkClick));
    },
    _venueSelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_venueSelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialiseVenueDropDown(null);
    },
    _initialiseVenueDropDown: function SpottedScript_Controls_Picker_Controller$_initialiseVenueDropDown(changeVenue) {
        /// <param name="changeVenue" type="SpottedScript.Controls.Picker.ObjectStub">
        /// </param>
        if (!this.get__optionVenue()) {
            return;
        }
        if (!this._venueDropDownIsInitialised || this._venueDropDownPlaceK !== this.get__place().k) {
            this._venueDropDownPlaceK = this.get__place().k;
            this._venueDropDownJ.ajaxAddOption('/support/getcached.aspx?type=venue&placek=' + this.get__place().k, null, false, Function.createDelegate(this, this._venueDropDownInitialised), [ changeVenue ]);
        }
        else {
            this._venueDropDownInitialised(changeVenue);
        }
    },
    _venueDropDownInitialised: function SpottedScript_Controls_Picker_Controller$_venueDropDownInitialised(changeVenue) {
        /// <param name="changeVenue" type="SpottedScript.Controls.Picker.ObjectStub">
        /// </param>
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
    _venueDropDownChange: function SpottedScript_Controls_Picker_Controller$_venueDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this.get__venueDropDownIsVenueSelectedCurrently()) {
            var venue = new SpottedScript.Controls.Picker.ObjectStub(this.get__venueDropDownVenueSelectedCurrently(), this._getText(this._view.get_venueDropDown()));
            this._addHistory('Venue', venue.toString());
            this._updateUIVenue(true);
            this._initialiseDateDropDowns();
            this._initialiseEventDropDown();
        }
        else if (this.get__venueDropDownIsLetterSelectedCurrently()) {
            var letter = this.get__venueDropDownLetterSelectedCurrently();
            if (!this._venueByLetterDropDownIsInitialised || this._venueByLetterDropDownPlaceK !== this.get__place().k || this._venueByLetterDropDownLetter !== letter) {
                this._venueByLetterDropDownPlaceK = this.get__place().k;
                this._venueByLetterDropDownLetter = letter;
                this._venueByLetterDropDownJ.ajaxAddOption('/support/getcached.aspx?type=venuebyletter&placek=' + this.get__place().k + '&letter=' + letter, null, false, Function.createDelegate(this, this._venueByLetterDropDownInitialised), null);
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
    _venueByLetterDropDownChange: function SpottedScript_Controls_Picker_Controller$_venueByLetterDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this.get__venueByLetterDropDownIsVenueSelectedCurrently()) {
            var venue = new SpottedScript.Controls.Picker.ObjectStub(this.get__venueByLetterDropDownVenueSelectedCurrently(), this._getText(this._view.get_venueByLetterDropDown()));
            this._addHistory('Venue', venue.toString());
            this._initialiseDateDropDowns();
            this._initialiseEventDropDown();
        }
    },
    _venueByLetterDropDownInitialised: function SpottedScript_Controls_Picker_Controller$_venueByLetterDropDownInitialised() {
        this._venueByLetterDropDownIsInitialised = true;
        this._addHistorys([ 'Venue', '0', 'Event', '0' ]);
        this._updateUIVenue(true);
    },
    _navigateVenuePreviousValue: '',
    _navigateVenue: function SpottedScript_Controls_Picker_Controller$_navigateVenue(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionVenue() || value.length === 0 || value === this._navigateVenuePreviousValue) {
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
                    this.selectedVenueChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var venue = SpottedScript.Controls.Picker.ObjectStub.fromString(value);
            if (this._venueDropDownIsInitialised && this._getK(this._view.get_venueDropDown()) !== venue.k) {
                this._setK(this._view.get_venueDropDown(), venue.k);
            }
            if (!this._venueDropDownIsInitialised) {
                this._view.get_venueSelectedLabel().innerHTML = venue.name;
            }
            this._view.get_selectedVenueHidden().value = venue.toString();
            if (!this._firstEverNavigate) {
                if (this.selectedVenueChanged != null) {
                    this.selectedVenueChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(venue));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(new SpottedScript.Controls.Picker.EventSelectionSpecification((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) ? this.get__brand() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__place() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue) ? venue : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__music() : null, this.get__date(), this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.me)));
                }
            }
        }
        this._navigateVenuePreviousValue = value;
    },
    _updateUIVenue: function SpottedScript_Controls_Picker_Controller$_updateUIVenue(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_venueHolder().style.display = (this.get__optionVenue() && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue && this.get__place() != null && (!this._venueDropDownIsInitialised || this._venueDropDownPlaceK === this.get__place().k)) ? '' : 'none';
        this._view.get_venueByLetterDropDown().style.display = (this.get__optionVenue() && this._venueByLetterDropDownIsInitialised && this.get__place() != null && this._venueByLetterDropDownPlaceK === this.get__place().k && this.get__venueDropDownIsLetterSelectedCurrently() && this._venueByLetterDropDownLetter === this.get__venueDropDownLetterSelectedCurrently()) ? '' : 'none';
        this._view.get_venueSelectedHolder().style.display = (!this._venueDropDownIsInitialised) ? '' : 'none';
        this._view.get_venueChoiceHolder().style.display = (this._venueDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIMusic(true);
        }
    },
    get__optionMusic: function SpottedScript_Controls_Picker_Controller$get__optionMusic() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionMusicHidden().value);
    },
    selectedMusicChanged: null,
    _musicDropDownJ: null,
    _musicDropDownIsInitialised: false,
    _musicDropDownPreviouslySelectedIndex: 0,
    get__music: function SpottedScript_Controls_Picker_Controller$get__music() {
        /// <value type="SpottedScript.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionMusic() || this._view.get_selectedMusicHidden().value == null || this._view.get_selectedMusicHidden().value.length === 0) {
            return null;
        }
        return SpottedScript.Controls.Picker.ObjectStub.fromString(this._view.get_selectedMusicHidden().value);
    },
    get__musicIsSelectedCurrently: function SpottedScript_Controls_Picker_Controller$get__musicIsSelectedCurrently() {
        /// <value type="Boolean"></value>
        return this.get__musicSelectedCurrently() > 0;
    },
    get__musicSelectedCurrently: function SpottedScript_Controls_Picker_Controller$get__musicSelectedCurrently() {
        /// <value type="Number" integer="true"></value>
        return this._getK(this._view.get_musicDropDown());
    },
    _initialiseMusic: function SpottedScript_Controls_Picker_Controller$_initialiseMusic() {
        if (!this.get__optionMusic()) {
            return;
        }
        this._musicDropDownJ = jQuery(this._view.get_musicDropDown());
        $addHandler(this._view.get_musicDropDown(), 'change', Function.createDelegate(this, this._musicDropDownChange));
        $addHandler(this._view.get_musicSelectedChangeLink(), 'click', Function.createDelegate(this, this._musicSelectedChangeLinkClick));
    },
    _musicSelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_musicSelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialiseMusicDropDown();
    },
    _initialiseMusicDropDown: function SpottedScript_Controls_Picker_Controller$_initialiseMusicDropDown() {
        if (!this.get__optionMusic()) {
            return;
        }
        if (!this._musicDropDownIsInitialised) {
            this._musicDropDownJ.ajaxAddOption('/support/getcached.aspx?type=music', null, false, Function.createDelegate(this, this._musicDropDownInitialised), null);
        }
        else {
            this._musicDropDownInitialised();
        }
    },
    _musicDropDownInitialised: function SpottedScript_Controls_Picker_Controller$_musicDropDownInitialised() {
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
    _musicDropDownChange: function SpottedScript_Controls_Picker_Controller$_musicDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this.get__musicIsSelectedCurrently()) {
            var musicTypeName = this._getText(this._view.get_musicDropDown());
            if (musicTypeName.substr(0, 4) === '... ') {
                musicTypeName = musicTypeName.substr(4);
            }
            var music = new SpottedScript.Controls.Picker.ObjectStub(this.get__musicSelectedCurrently(), musicTypeName);
            this._addHistory('Music', music.toString());
            this._initialiseDateDropDowns();
        }
        else {
            this._setIndex(this._view.get_musicDropDown(), this._musicDropDownPreviouslySelectedIndex);
        }
        this._musicDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_musicDropDown());
    },
    _navigateMusicPreviousValue: '',
    _navigateMusic: function SpottedScript_Controls_Picker_Controller$_navigateMusic(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionMusic() || value.length === 0 || value === this._navigateMusicPreviousValue) {
            return;
        }
        if (value === '0') {
            this._setIndex(this._view.get_musicDropDown(), 0);
            this._view.get_musicSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedMusicHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedMusicChanged != null) {
                    this.selectedMusicChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var music = SpottedScript.Controls.Picker.ObjectStub.fromString(value);
            if (this._musicDropDownIsInitialised && this._getK(this._view.get_musicDropDown()) !== music.k) {
                this._setK(this._view.get_musicDropDown(), music.k);
            }
            if (!this._musicDropDownIsInitialised) {
                this._view.get_musicSelectedLabel().innerHTML = music.name;
            }
            this._view.get_selectedMusicHidden().value = music.toString();
            if (!this._firstEverNavigate) {
                if (this.selectedMusicChanged != null) {
                    this.selectedMusicChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(music));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(new SpottedScript.Controls.Picker.EventSelectionSpecification((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) ? this.get__brand() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__place() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue) ? this.get__venue() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? music : null, this.get__date(), this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.me)));
                }
            }
        }
        this._navigateMusicPreviousValue = value;
    },
    _updateUIMusic: function SpottedScript_Controls_Picker_Controller$_updateUIMusic(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_musicHolder().style.display = (this.get__optionMusic() && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music && this.get__place() != null) ? '' : 'none';
        this._view.get_musicSelectedHolder().style.display = (!this._musicDropDownIsInitialised) ? '' : 'none';
        this._view.get_musicChoiceHolder().style.display = (this._musicDropDownIsInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIDate(true);
        }
    },
    get__optionDate: function SpottedScript_Controls_Picker_Controller$get__optionDate() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionDateHidden().value);
    },
    get__optionDateDay: function SpottedScript_Controls_Picker_Controller$get__optionDateDay() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionDateDayHidden().value);
    },
    get__optionDateDayIncrement: function SpottedScript_Controls_Picker_Controller$get__optionDateDayIncrement() {
        /// <value type="Number" integer="true"></value>
        return Number.parseInvariant(this._view.get_optionDateDayIncrementHidden().value);
    },
    selectedDateChanged: null,
    _dateDayDropDownJ: null,
    _dateDropDownsAreInitialised: false,
    get__date: function SpottedScript_Controls_Picker_Controller$get__date() {
        /// <value type="SpottedScript.Controls.Picker.DateStub"></value>
        if (!this.get__optionDate() || this._view.get_selectedDateHidden().value == null || this._view.get_selectedDateHidden().value.length === 0) {
            var d = new Date();
            return new SpottedScript.Controls.Picker.DateStub(d.getFullYear(), d.getMonth() + 1, (this.get__optionDateDay()) ? d.getDate() : 0);
        }
        var date = SpottedScript.Controls.Picker.DateStub.fromString(this._view.get_selectedDateHidden().value);
        if (!this.get__optionDateDay()) {
            date.day = 0;
        }
        return date;
    },
    _dateMonths: null,
    _initialiseDate: function SpottedScript_Controls_Picker_Controller$_initialiseDate() {
        if (!this.get__optionDate()) {
            return;
        }
        this._dateDayDropDownJ = jQuery(this._view.get_dateDayDropDown());
        this._dateMonths = [ '', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec' ];
        $addHandler(this._view.get_dateDayDropDown(), 'change', Function.createDelegate(this, this._dateDayDropDownChange));
        $addHandler(this._view.get_dateMonthDropDown(), 'change', Function.createDelegate(this, this._dateMonthDropDownChange));
        $addHandler(this._view.get_dateYearTextBox(), 'change', Function.createDelegate(this, this._dateYearTextBoxChange));
        $addHandler(this._view.get_dateYearTextBox(), 'keyup', Function.createDelegate(this, this._dateYearTextBoxKeyUp));
        $addHandler(this._view.get_dateYearPlusImg(), 'click', Function.createDelegate(this, this._dateYearPlusClick));
        $addHandler(this._view.get_dateYearMinusImg(), 'click', Function.createDelegate(this, this._dateYearMinusClick));
        $addHandler(this._view.get_dateSelectedChangeLink(), 'click', Function.createDelegate(this, this._dateSelectedChangeLinkClick));
        $addHandler(this._view.get_dateMonthPlusImg(), 'click', Function.createDelegate(this, this._dateMonthPlusClick));
        $addHandler(this._view.get_dateMonthMinusImg(), 'click', Function.createDelegate(this, this._dateMonthMinusClick));
        $addHandler(this._view.get_dateDayPlusImg(), 'click', Function.createDelegate(this, this._dateDayPlusClick));
        $addHandler(this._view.get_dateDayMinusImg(), 'click', Function.createDelegate(this, this._dateDayMinusClick));
    },
    _dateSelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_dateSelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialiseDateDropDowns();
    },
    _initialiseDateDropDowns: function SpottedScript_Controls_Picker_Controller$_initialiseDateDropDowns() {
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
    _dateRefreshDays: function SpottedScript_Controls_Picker_Controller$_dateRefreshDays(year, month) {
        /// <param name="year" type="Number" integer="true">
        /// </param>
        /// <param name="month" type="Number" integer="true">
        /// </param>
        if (!this.get__optionDateDay()) {
            return;
        }
        var days = SpottedScript.Controls.Picker.DateStub.daysInMonth(year, month);
        var firstDayOfWeek = new Date(year, month - 1, 1).getDay();
        if (!this._dateRefreshDaysHasRunBefore || this._dateRefreshDaysPreviousNumberOfDaysInMonth !== days || this._dateRefreshDaysPreviousFirstDayOfWeek !== firstDayOfWeek) {
            this._dateRefreshDaysPreviousNumberOfDaysInMonth = days;
            this._dateRefreshDaysPreviousFirstDayOfWeek = firstDayOfWeek;
            var previouslySelectedIndex = this._getIndex(this._view.get_dateDayDropDown());
            this._dateDayDropDownJ.removeAll();
            for (var i = 1; i <= days; i++) {
                var dayOfWeek = new Date(year, month - 1, i).getDay();
                this._addOption(this._view.get_dateDayDropDown(), i.toString(), ((i < 10) ? '0' : '') + i.toString() + ((dayOfWeek === 6) ? ' Sat' : (dayOfWeek === 0) ? ' Sun' : ''));
            }
            if (previouslySelectedIndex > -1) {
                this._setIndex(this._view.get_dateDayDropDown(), previouslySelectedIndex);
            }
            this._dateRefreshDaysHasRunBefore = true;
        }
    },
    _dateDayDropDownChange: function SpottedScript_Controls_Picker_Controller$_dateDayDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var newDay = this._getValueInt(this._view.get_dateDayDropDown());
        var newDate = new SpottedScript.Controls.Picker.DateStub(this.get__date().year, this.get__date().month, newDay);
        this._addHistory('Date', newDate.toString());
        this._initialiseEventDropDown();
    },
    _dateMonthDropDownChange: function SpottedScript_Controls_Picker_Controller$_dateMonthDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var newMonth = this._getValueInt(this._view.get_dateMonthDropDown());
        this._dateRefreshDays(this.get__date().year, newMonth);
        var newDay = (this.get__optionDateDay()) ? this._getValueInt(this._view.get_dateDayDropDown()) : 0;
        var newDate = new SpottedScript.Controls.Picker.DateStub(this.get__date().year, newMonth, newDay);
        this._addHistory('Date', newDate.toString());
        this._initialiseEventDropDown();
    },
    _dateYearTextBoxKeyUp: function SpottedScript_Controls_Picker_Controller$_dateYearTextBoxKeyUp(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._dateYearTextBoxKeyUpChange(false);
    },
    _dateYearTextBoxChange: function SpottedScript_Controls_Picker_Controller$_dateYearTextBoxChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._dateYearTextBoxKeyUpChange(true);
    },
    _dateYearTextBoxKeyUpChange: function SpottedScript_Controls_Picker_Controller$_dateYearTextBoxKeyUpChange(change) {
        /// <param name="change" type="Boolean">
        /// </param>
        try {
            var regex = new RegExp('[^0-9]', 'g');
            if (!regex.test(this._view.get_dateYearTextBox().value)) {
                var newYear = Number.parseInvariant(this._view.get_dateYearTextBox().value);
                if (newYear > 1900 && newYear < 2100) {
                    if (newYear !== this.get__date().year) {
                        this._dateRefreshDays(newYear, this.get__date().month);
                        var newDay = (this.get__optionDateDay()) ? this._getValueInt(this._view.get_dateDayDropDown()) : 0;
                        var newDate = new SpottedScript.Controls.Picker.DateStub(newYear, this.get__date().month, newDay);
                        this._addHistory('Date', newDate.toString());
                        this._initialiseEventDropDown();
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
    _dateYearPlusClick: function SpottedScript_Controls_Picker_Controller$_dateYearPlusClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._datePlusMinus(1, 'y');
    },
    _dateYearMinusClick: function SpottedScript_Controls_Picker_Controller$_dateYearMinusClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._datePlusMinus(-1, 'y');
    },
    _dateMonthPlusClick: function SpottedScript_Controls_Picker_Controller$_dateMonthPlusClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._datePlusMinus(1, 'm');
    },
    _dateMonthMinusClick: function SpottedScript_Controls_Picker_Controller$_dateMonthMinusClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._datePlusMinus(-1, 'm');
    },
    _dateDayPlusClick: function SpottedScript_Controls_Picker_Controller$_dateDayPlusClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._datePlusMinus(this.get__optionDateDayIncrement(), 'd');
    },
    _dateDayMinusClick: function SpottedScript_Controls_Picker_Controller$_dateDayMinusClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._datePlusMinus(-this.get__optionDateDayIncrement(), 'd');
    },
    _dateChangePrivate: function SpottedScript_Controls_Picker_Controller$_dateChangePrivate(newDate) {
        /// <param name="newDate" type="SpottedScript.Controls.Picker.DateStub">
        /// </param>
        var oldDate = new SpottedScript.Controls.Picker.DateStub(this.get__date().year, this.get__date().month, this.get__date().day);
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
        this._initialiseEventDropDown();
    },
    _datePlusMinus: function SpottedScript_Controls_Picker_Controller$_datePlusMinus(modifier, unit) {
        /// <param name="modifier" type="Number" integer="true">
        /// </param>
        /// <param name="unit" type="String">
        /// </param>
        /// <returns type="SpottedScript.Controls.Picker.DateStub"></returns>
        var newDate = new SpottedScript.Controls.Picker.DateStub(this.get__date().year, this.get__date().month, this.get__date().day).modify(modifier, unit);
        this._dateChangePrivate(newDate);
        return newDate;
    },
    dateModify: function SpottedScript_Controls_Picker_Controller$dateModify(modifier, unit) {
        /// <param name="modifier" type="Number" integer="true">
        /// </param>
        /// <param name="unit" type="String">
        /// </param>
        /// <returns type="SpottedScript.Controls.Picker.DateStub"></returns>
        return this._datePlusMinus(modifier, unit);
    },
    dateChange: function SpottedScript_Controls_Picker_Controller$dateChange(newDate) {
        /// <param name="newDate" type="SpottedScript.Controls.Picker.DateStub">
        /// </param>
        this._dateChangePrivate(newDate);
    },
    _navigateDatePreviousValue: '',
    _navigateDate: function SpottedScript_Controls_Picker_Controller$_navigateDate(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionDate() || value.length === 0 || value === this._navigateDatePreviousValue) {
            return;
        }
        if (value === '0') {
            this._view.get_dateSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedDateHidden().value = '';
            if (!this._firstEverNavigate) {
                if (this.selectedDateChanged != null) {
                    this.selectedDateChanged(this, new SpottedScript.Controls.Picker.DateArgs(null));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(null));
                }
            }
        }
        else {
            var date = SpottedScript.Controls.Picker.DateStub.fromString(value);
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
                    this.selectedDateChanged(this, new SpottedScript.Controls.Picker.DateArgs(date));
                }
                if (this.eventSelectionSepcificationChanged != null) {
                    this.eventSelectionSepcificationChanged(this, new SpottedScript.Controls.Picker.EventSelectionArgs(new SpottedScript.Controls.Picker.EventSelectionSpecification((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) ? this.get__brand() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__place() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue) ? this.get__venue() : null, (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) ? this.get__music() : null, date, this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.me)));
                }
            }
        }
        this._navigateDatePreviousValue = value;
    },
    _updateUIDate: function SpottedScript_Controls_Picker_Controller$_updateUIDate(recursive) {
        /// <param name="recursive" type="Boolean">
        /// </param>
        this._view.get_dateHolder().style.display = (this.get__optionDate() && ((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue && this.get__venue() != null) || (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue && !this.get__optionVenue() && this.get__place() != null) || (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music && this.get__place() != null && this.get__music() != null) || (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand && this.get__brand() != null))) ? '' : 'none';
        this._view.get_dateDayHolder().style.display = (this.get__optionDateDay()) ? '' : 'none';
        this._view.get_dateSelectedHolder().style.display = (!this._dateDropDownsAreInitialised) ? '' : 'none';
        this._view.get_dateChoiceHolder().style.display = (this._dateDropDownsAreInitialised) ? '' : 'none';
        if (recursive) {
            this._updateUIEvent();
        }
    },
    get__optionEvent: function SpottedScript_Controls_Picker_Controller$get__optionEvent() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionEventHidden().value);
    },
    selectedEventChanged: null,
    _eventListBoxJ: null,
    _eventDropDownIsInitialised: false,
    _eventDropDownVenueK: 0,
    _eventDropDownBrandK: 0,
    _eventDropDownKey: null,
    _eventDropDownDate: null,
    _eventDropDownPreviouslySelectedIndex: 0,
    get__event: function SpottedScript_Controls_Picker_Controller$get__event() {
        /// <value type="SpottedScript.Controls.Picker.ObjectStub"></value>
        if (!this.get__optionEvent() || this._view.get_selectedEventHidden().value == null || this._view.get_selectedEventHidden().value.length === 0) {
            return null;
        }
        return SpottedScript.Controls.Picker.ObjectStub.fromString(this._view.get_selectedEventHidden().value);
    },
    _initialiseEvent: function SpottedScript_Controls_Picker_Controller$_initialiseEvent() {
        if (!this.get__optionEvent()) {
            return;
        }
        this._eventListBoxJ = jQuery(this._view.get_eventListBox());
        $addHandler(this._view.get_eventListBox(), 'change', Function.createDelegate(this, this._eventDropDownChange));
        $addHandler(this._view.get_eventSelectedChangeLink(), 'click', Function.createDelegate(this, this._eventSelectedChangeLinkClick));
    },
    _eventSelectedChangeLinkClick: function SpottedScript_Controls_Picker_Controller$_eventSelectedChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._initialiseEventDropDown();
    },
    _initialiseEventDropDown: function SpottedScript_Controls_Picker_Controller$_initialiseEventDropDown() {
        if (!this.get__optionEvent()) {
            return;
        }
        if (!this._eventDropDownIsInitialised || ((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) && this.get__venue() != null && this._eventDropDownVenueK !== this.get__venue().k) || (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand && this.get__brand() != null && this._eventDropDownBrandK !== this.get__brand().k) || (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.key && this.get__key() != null && this._eventDropDownKey !== this.get__key()) || this._eventDropDownDate.year !== this.get__date().year || this._eventDropDownDate.month !== this.get__date().month) {
            if ((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) && this.get__venue() != null && this.get__venue().k === 1) {
                return;
            }
            this._eventDropDownDate = this.get__date();
            this._eventDropDownVenueK = (this.get__venue() != null && (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music)) ? this.get__venue().k : 0;
            this._eventDropDownBrandK = (this.get__brand() != null && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand) ? this.get__brand().k : 0;
            this._eventDropDownKey = (this.get__key() != null && this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.key) ? this.get__key() : '0';
            if (this._eventDropDownVenueK === 0 && this._eventDropDownBrandK === 0 && this._eventDropDownKey === '0') {
                return;
            }
            this._requestId++;
            var currentRequestId = this._requestId;
            var currentLoadId = this._loadId;
            this._eventListBoxJ.ajaxAddOption1('/support/getcached.aspx?type=event&key=' + this._eventDropDownKey + '&venuek=' + this._eventDropDownVenueK + '&brandk=' + this._eventDropDownBrandK + '&date=' + this._eventDropDownDate.toString(), null, false, Function.createDelegate(this, this._eventDropDownInitialised), [ currentRequestId ]);
            window.setTimeout(Function.createDelegate(this, function() {
                if (this._loadId === currentLoadId) {
                    this._eventListBoxJ.removeAll();
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
    _eventDropDownInitialised: function SpottedScript_Controls_Picker_Controller$_eventDropDownInitialised(args, data) {
        /// <param name="args" type="Object">
        /// </param>
        /// <param name="data" type="Object">
        /// </param>
        var requestIdFromArgs = Number.parseInvariant(args.toString());
        if (this._requestId === requestIdFromArgs) {
            this._loadId++;
            this._eventListBoxJ.removeAll();
            this._eventListBoxJ.addOption(data, false);
            this._eventDropDownConfigure();
        }
    },
    _eventDropDownConfigure: function SpottedScript_Controls_Picker_Controller$_eventDropDownConfigure() {
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
    _eventDropDownChange: function SpottedScript_Controls_Picker_Controller$_eventDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var k = this._getK(this._view.get_eventListBox());
        if (k > 0) {
            var _event = new SpottedScript.Controls.Picker.ObjectStub(this._getK(this._view.get_eventListBox()), this._getText(this._view.get_eventListBox()));
            this._addHistory('Event', _event.toString());
        }
        else {
            this._setIndex(this._view.get_eventListBox(), this._eventDropDownPreviouslySelectedIndex);
        }
        this._eventDropDownPreviouslySelectedIndex = this._getIndex(this._view.get_eventListBox());
    },
    _navigateEventPreviousValue: '',
    _navigateEvent: function SpottedScript_Controls_Picker_Controller$_navigateEvent(value) {
        /// <param name="value" type="String">
        /// </param>
        if (!this.get__optionEvent() || value.length === 0 || value === this._navigateEventPreviousValue) {
            return;
        }
        if (value === '0') {
            this._setIndex(this._view.get_eventListBox(), 0);
            this._view.get_eventSelectedLabel().innerHTML = 'None selected';
            this._view.get_selectedEventHidden().value = '';
            if (this.selectedEventChanged != null) {
                this.selectedEventChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(null));
            }
        }
        else {
            var _event = SpottedScript.Controls.Picker.ObjectStub.fromString(value);
            if (this._eventDropDownIsInitialised && this._getK(this._view.get_eventListBox()) !== _event.k) {
                this._setK(this._view.get_eventListBox(), _event.k);
            }
            if (!this._eventDropDownIsInitialised) {
                this._view.get_eventSelectedLabel().innerHTML = _event.name.substr(11);
            }
            this._view.get_selectedEventHidden().value = _event.toString();
            if (this.selectedEventChanged != null) {
                this.selectedEventChanged(this, new SpottedScript.Controls.Picker.ObjectArgs(_event));
            }
        }
        this._navigateEventPreviousValue = value;
    },
    _updateUIEvent: function SpottedScript_Controls_Picker_Controller$_updateUIEvent() {
        this._view.get_eventHolder().style.display = (this.get__optionEvent() && (((this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.venue || this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.music) && this.get__venue() != null && this.get__venue().k > 1) || (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.brand && this.get__brand() != null && this.get__brand().k > 1) || (this.get__searchType() === SpottedScript.Controls.Picker.Shared.SearchTypes.key && this.get__key() != null && this.get__key().length > 0))) ? '' : 'none';
        this._view.get_eventSelectedHolder().style.display = (!this._eventDropDownIsInitialised) ? '' : 'none';
        this._view.get_eventChoiceHolder().style.display = (this._eventDropDownIsInitialised) ? '' : 'none';
    },
    get__optionGoogle: function SpottedScript_Controls_Picker_Controller$get__optionGoogle() {
        /// <value type="Boolean"></value>
        return Boolean.parse(this._view.get_optionGoogleHidden().value);
    },
    _addOption: function SpottedScript_Controls_Picker_Controller$_addOption(sel, key, value) {
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
    _addHistory: function SpottedScript_Controls_Picker_Controller$_addHistory(key, value) {
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
        Sys.Application.addHistoryPoint(d);
    },
    _addHistorys: function SpottedScript_Controls_Picker_Controller$_addHistorys(keysAndValues) {
        /// <param name="keysAndValues" type="Array" elementType="String">
        /// </param>
        this._firstEverNavigate = false;
        var d = {};
        for (var i = 0; i < keysAndValues.length; i = i + 2) {
            d[this._view.clientId + '_' + keysAndValues[i]] = encodeURI(keysAndValues[i + 1]).replace(this._regexQuote, '&#39;');
        }
        Sys.Application.addHistoryPoint(d);
    },
    _getValue: function SpottedScript_Controls_Picker_Controller$_getValue(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="String"></returns>
        return (sel.selectedIndex === -1) ? '' : (sel.options[sel.selectedIndex]).value;
    },
    _getValueInt: function SpottedScript_Controls_Picker_Controller$_getValueInt(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return (sel.selectedIndex === -1) ? 0 : Number.parseInvariant((sel.options[sel.selectedIndex]).value);
    },
    _getIndex: function SpottedScript_Controls_Picker_Controller$_getIndex(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return sel.selectedIndex;
    },
    _getK: function SpottedScript_Controls_Picker_Controller$_getK(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        try {
            var value = (sel.options[sel.selectedIndex]).value;
            value = value.substr(5, value.length - 5);
            return Number.parseInvariant(value);
        }
        catch ($e1) {
            return 0;
        }
    },
    _getText: function SpottedScript_Controls_Picker_Controller$_getText(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="String"></returns>
        return (sel.options[sel.selectedIndex]).text;
    },
    _setIndex: function SpottedScript_Controls_Picker_Controller$_setIndex(sel, index) {
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
    _unSelect: function SpottedScript_Controls_Picker_Controller$_unSelect(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        for (var i = 0; i < sel.options.length; i++) {
            var op = sel.options[i];
            op.selected = false;
        }
    },
    _setValue: function SpottedScript_Controls_Picker_Controller$_setValue(sel, value) {
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
    _setK: function SpottedScript_Controls_Picker_Controller$_setK(sel, value) {
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
    _debug: function SpottedScript_Controls_Picker_Controller$_debug(text) {
        /// <param name="text" type="String">
        /// </param>
        this._view.get_debug().style.display = '';
        this._debugCount++;
        this._view.get_debug().value = this._debugCount.toString() + ' ' + text + '\n' + this._view.get_debug().value;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.DateStub
SpottedScript.Controls.Picker.DateStub = function SpottedScript_Controls_Picker_DateStub(year, month, day) {
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
SpottedScript.Controls.Picker.DateStub.fromString = function SpottedScript_Controls_Picker_DateStub$fromString(value) {
    /// <param name="value" type="String">
    /// </param>
    /// <returns type="SpottedScript.Controls.Picker.DateStub"></returns>
    var year = Number.parseInvariant(value.substr(0, 4));
    var month = Number.parseInvariant(value.substr(4, 2));
    var day = Number.parseInvariant(value.substr(6, 2));
    return new SpottedScript.Controls.Picker.DateStub(year, month, day);
}
SpottedScript.Controls.Picker.DateStub.daysInMonth = function SpottedScript_Controls_Picker_DateStub$daysInMonth(year, month) {
    /// <param name="year" type="Number" integer="true">
    /// </param>
    /// <param name="month" type="Number" integer="true">
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    var m = [ 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 ];
    if (month !== 2) {
        return m[month - 1];
    }
    if (year % 4 !== 0) {
        return m[1];
    }
    if (year % 100 === 0 && year % 400 !== 0) {
        return m[1];
    }
    return m[1] + 1;
}
SpottedScript.Controls.Picker.DateStub.prototype = {
    year: 0,
    month: 0,
    day: 0,
    toString: function SpottedScript_Controls_Picker_DateStub$toString() {
        /// <returns type="String"></returns>
        return this.year.toString() + ((this.month < 10) ? '0' : '') + this.month.toString() + ((this.day < 10) ? '0' : '') + this.day.toString();
    },
    toFriendlyString: function SpottedScript_Controls_Picker_DateStub$toFriendlyString() {
        /// <returns type="String"></returns>
        return this.year.toString() + '-' + ((this.month < 10) ? '0' : '') + this.month.toString() + ((this.day > 0) ? ('-' + ((this.day < 10) ? '0' : '') + this.day.toString()) : '');
    },
    toDateTime: function SpottedScript_Controls_Picker_DateStub$toDateTime() {
        /// <returns type="Date"></returns>
        return new Date(this.year, this.month - 1, this.day);
    },
    modify: function SpottedScript_Controls_Picker_DateStub$modify(modifier, unit) {
        /// <param name="modifier" type="Number" integer="true">
        /// </param>
        /// <param name="unit" type="String">
        /// </param>
        /// <returns type="SpottedScript.Controls.Picker.DateStub"></returns>
        var newDate = new SpottedScript.Controls.Picker.DateStub(this.year, this.month, this.day);
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
            var daysInOldMonth = SpottedScript.Controls.Picker.DateStub.daysInMonth(this.year, this.month);
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
                        newDate.day = SpottedScript.Controls.Picker.DateStub.daysInMonth(newDate.year, newDate.month) + this.day + modifier;
                    }
                    else {
                        newDate.month--;
                        newDate.day = SpottedScript.Controls.Picker.DateStub.daysInMonth(newDate.year, newDate.month) + this.day + modifier;
                    }
                }
                else {
                    newDate.day = this.day + modifier;
                }
            }
        }
        return newDate;
    },
    dayOfWeek: function SpottedScript_Controls_Picker_DateStub$dayOfWeek() {
        /// <summary>
        /// Week day
        /// </summary>
        /// <returns type="Number" integer="true"></returns>
        return this.toDateTime().getDay();
    },
    previousMonday: function SpottedScript_Controls_Picker_DateStub$previousMonday() {
        /// <returns type="SpottedScript.Controls.Picker.DateStub"></returns>
        var d = this.dayOfWeek();
        var modifier = (d === 0) ? -6 : (d === 1) ? 0 : (d === 2) ? -1 : (d === 3) ? -2 : (d === 4) ? -3 : (d === 5) ? -4 : (d === 6) ? -5 : 0;
        return this.modify(modifier, 'd');
    },
    nextSunday: function SpottedScript_Controls_Picker_DateStub$nextSunday() {
        /// <returns type="SpottedScript.Controls.Picker.DateStub"></returns>
        var d = this.dayOfWeek();
        var modifier = (d === 0) ? 0 : (d === 1) ? 6 : (d === 2) ? 5 : (d === 3) ? 4 : (d === 4) ? 3 : (d === 5) ? 2 : (d === 6) ? 1 : 0;
        return this.modify(modifier, 'd');
    },
    get_monthNameFull: function SpottedScript_Controls_Picker_DateStub$get_monthNameFull() {
        /// <value type="String"></value>
        var m = this.month;
        return (m === 1) ? 'January' : (m === 2) ? 'February' : (m === 3) ? 'March' : (m === 4) ? 'April' : (m === 5) ? 'May' : (m === 6) ? 'June' : (m === 7) ? 'July' : (m === 8) ? 'August' : (m === 9) ? 'September' : (m === 10) ? 'October' : (m === 11) ? 'November' : (m === 12) ? 'December' : '';
    },
    _debugCount: 0,
    _debug: function SpottedScript_Controls_Picker_DateStub$_debug(text) {
        /// <param name="text" type="String">
        /// </param>
        var debug = document.getElementById('Content_Debug');
        debug.style.display = '';
        this._debugCount++;
        debug.value = this._debugCount.toString() + ' ' + text + '\n' + debug.value;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.ObjectStub
SpottedScript.Controls.Picker.ObjectStub = function SpottedScript_Controls_Picker_ObjectStub(k, name) {
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
SpottedScript.Controls.Picker.ObjectStub.fromString = function SpottedScript_Controls_Picker_ObjectStub$fromString(value) {
    /// <param name="value" type="String">
    /// </param>
    /// <returns type="SpottedScript.Controls.Picker.ObjectStub"></returns>
    if (value == null || value.length === 0 || value === '0') {
        return null;
    }
    var k = Number.parseInvariant(value.substr(0, value.indexOf('-')));
    var name = value.substr(value.indexOf('-') + 1, value.length - value.indexOf('-') - 1);
    return new SpottedScript.Controls.Picker.ObjectStub(k, name);
}
SpottedScript.Controls.Picker.ObjectStub.prototype = {
    name: null,
    k: 0,
    toString: function SpottedScript_Controls_Picker_ObjectStub$toString() {
        /// <returns type="String"></returns>
        return this.k.toString() + '-' + this.name;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.EventSelectionSpecification
SpottedScript.Controls.Picker.EventSelectionSpecification = function SpottedScript_Controls_Picker_EventSelectionSpecification(brand, place, venue, music, date, me) {
    /// <param name="brand" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </param>
    /// <param name="place" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </param>
    /// <param name="venue" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </param>
    /// <param name="music" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </param>
    /// <param name="date" type="SpottedScript.Controls.Picker.DateStub">
    /// </param>
    /// <param name="me" type="Boolean">
    /// </param>
    /// <field name="brand" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </field>
    /// <field name="place" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </field>
    /// <field name="venue" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </field>
    /// <field name="music" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </field>
    /// <field name="date" type="SpottedScript.Controls.Picker.DateStub">
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
SpottedScript.Controls.Picker.EventSelectionSpecification.prototype = {
    brand: null,
    place: null,
    venue: null,
    music: null,
    date: null,
    me: false,
    toString: function SpottedScript_Controls_Picker_EventSelectionSpecification$toString() {
        /// <returns type="String"></returns>
        return 'Brand: ' + ((this.brand == null) ? 'null' : this.brand.toString()) + '<br />' + 'Place: ' + ((this.place == null) ? 'null' : this.place.toString()) + '<br />' + 'Venue: ' + ((this.venue == null) ? 'null' : this.venue.toString()) + '<br />' + 'Music: ' + ((this.music == null) ? 'null' : this.music.toString()) + '<br />' + 'Date: ' + ((this.date == null) ? 'null' : this.date.toFriendlyString()) + '<br />' + 'Me: ' + this.me.toString().toLowerCase();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.StringArgs
SpottedScript.Controls.Picker.StringArgs = function SpottedScript_Controls_Picker_StringArgs(val) {
    /// <param name="val" type="String">
    /// </param>
    /// <field name="val" type="String">
    /// </field>
    SpottedScript.Controls.Picker.StringArgs.initializeBase(this);
    this.val = val;
}
SpottedScript.Controls.Picker.StringArgs.prototype = {
    val: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.ObjectArgs
SpottedScript.Controls.Picker.ObjectArgs = function SpottedScript_Controls_Picker_ObjectArgs(ob) {
    /// <param name="ob" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </param>
    /// <field name="object" type="SpottedScript.Controls.Picker.ObjectStub">
    /// </field>
    SpottedScript.Controls.Picker.ObjectArgs.initializeBase(this);
    this.object = ob;
}
SpottedScript.Controls.Picker.ObjectArgs.prototype = {
    object: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.DateArgs
SpottedScript.Controls.Picker.DateArgs = function SpottedScript_Controls_Picker_DateArgs(d) {
    /// <param name="d" type="SpottedScript.Controls.Picker.DateStub">
    /// </param>
    /// <field name="date" type="SpottedScript.Controls.Picker.DateStub">
    /// </field>
    SpottedScript.Controls.Picker.DateArgs.initializeBase(this);
    this.date = d;
}
SpottedScript.Controls.Picker.DateArgs.prototype = {
    date: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.EventSelectionArgs
SpottedScript.Controls.Picker.EventSelectionArgs = function SpottedScript_Controls_Picker_EventSelectionArgs(specification) {
    /// <param name="specification" type="SpottedScript.Controls.Picker.EventSelectionSpecification">
    /// </param>
    /// <field name="specification" type="SpottedScript.Controls.Picker.EventSelectionSpecification">
    /// </field>
    SpottedScript.Controls.Picker.EventSelectionArgs.initializeBase(this);
    this.specification = specification;
}
SpottedScript.Controls.Picker.EventSelectionArgs.prototype = {
    specification: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.View
SpottedScript.Controls.Picker.View = function SpottedScript_Controls_Picker_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.Picker.View.prototype = {
    clientId: null,
    get_debug: function SpottedScript_Controls_Picker_View$get_debug() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Debug');
    },
    get_holder: function SpottedScript_Controls_Picker_View$get_holder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Holder');
    },
    get_searchTypeHolder: function SpottedScript_Controls_Picker_View$get_searchTypeHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeHolder');
    },
    get_searchTypeKey: function SpottedScript_Controls_Picker_View$get_searchTypeKey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeKey');
    },
    get_searchTypeSpotter: function SpottedScript_Controls_Picker_View$get_searchTypeSpotter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeSpotter');
    },
    get_searchTypeMe: function SpottedScript_Controls_Picker_View$get_searchTypeMe() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeMe');
    },
    get_searchTypeVenue: function SpottedScript_Controls_Picker_View$get_searchTypeVenue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeVenue');
    },
    get_searchTypeBrand: function SpottedScript_Controls_Picker_View$get_searchTypeBrand() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeBrand');
    },
    get_searchTypeMusic: function SpottedScript_Controls_Picker_View$get_searchTypeMusic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeMusic');
    },
    get_searchTypeGoogle: function SpottedScript_Controls_Picker_View$get_searchTypeGoogle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeGoogle');
    },
    get_keyHolder: function SpottedScript_Controls_Picker_View$get_keyHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KeyHolder');
    },
    get_keySelectedHolder: function SpottedScript_Controls_Picker_View$get_keySelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KeySelectedHolder');
    },
    get_keySelectedLabel: function SpottedScript_Controls_Picker_View$get_keySelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KeySelectedLabel');
    },
    get_keySelectedChangeLink: function SpottedScript_Controls_Picker_View$get_keySelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KeySelectedChangeLink');
    },
    get_keyChoiceHolder: function SpottedScript_Controls_Picker_View$get_keyChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KeyChoiceHolder');
    },
    get_keyTextBox: function SpottedScript_Controls_Picker_View$get_keyTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KeyTextBox');
    },
    get_keySearchButton: function SpottedScript_Controls_Picker_View$get_keySearchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_KeySearchButton');
    },
    get_spotterHolder: function SpottedScript_Controls_Picker_View$get_spotterHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterHolder');
    },
    get_spotterSelectedHolder: function SpottedScript_Controls_Picker_View$get_spotterSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSelectedHolder');
    },
    get_spotterSelectedLabel: function SpottedScript_Controls_Picker_View$get_spotterSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSelectedLabel');
    },
    get_spotterSelectedChangeLink: function SpottedScript_Controls_Picker_View$get_spotterSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSelectedChangeLink');
    },
    get_spotterChoiceHolder: function SpottedScript_Controls_Picker_View$get_spotterChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterChoiceHolder');
    },
    get_spotterTextBox: function SpottedScript_Controls_Picker_View$get_spotterTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterTextBox');
    },
    get_spotterSearchButton: function SpottedScript_Controls_Picker_View$get_spotterSearchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSearchButton');
    },
    get_brandHolder: function SpottedScript_Controls_Picker_View$get_brandHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandHolder');
    },
    get_brandSelectedHolder: function SpottedScript_Controls_Picker_View$get_brandSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandSelectedHolder');
    },
    get_brandSelectedLabel: function SpottedScript_Controls_Picker_View$get_brandSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandSelectedLabel');
    },
    get_brandSelectedChangeLink: function SpottedScript_Controls_Picker_View$get_brandSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandSelectedChangeLink');
    },
    get_brandChoiceHolder: function SpottedScript_Controls_Picker_View$get_brandChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandChoiceHolder');
    },
    get_brandAutoComplete: function SpottedScript_Controls_Picker_View$get_brandAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_BrandAutoCompleteBehaviour');
    },
    get_countryHolder: function SpottedScript_Controls_Picker_View$get_countryHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CountryHolder');
    },
    get_countrySelectedHolder: function SpottedScript_Controls_Picker_View$get_countrySelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CountrySelectedHolder');
    },
    get_countrySelectedLabel: function SpottedScript_Controls_Picker_View$get_countrySelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CountrySelectedLabel');
    },
    get_countrySelectedChangeLink: function SpottedScript_Controls_Picker_View$get_countrySelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CountrySelectedChangeLink');
    },
    get_countryChoiceHolder: function SpottedScript_Controls_Picker_View$get_countryChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CountryChoiceHolder');
    },
    get_countryDropDown: function SpottedScript_Controls_Picker_View$get_countryDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CountryDropDown');
    },
    get_placeHolder: function SpottedScript_Controls_Picker_View$get_placeHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceHolder');
    },
    get_placeSelectedHolder: function SpottedScript_Controls_Picker_View$get_placeSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceSelectedHolder');
    },
    get_placeSelectedLabel: function SpottedScript_Controls_Picker_View$get_placeSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceSelectedLabel');
    },
    get_placeSelectedChangeLink: function SpottedScript_Controls_Picker_View$get_placeSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceSelectedChangeLink');
    },
    get_placeChoiceHolder: function SpottedScript_Controls_Picker_View$get_placeChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceChoiceHolder');
    },
    get_placeDropDown: function SpottedScript_Controls_Picker_View$get_placeDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceDropDown');
    },
    get_venueHolder: function SpottedScript_Controls_Picker_View$get_venueHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueHolder');
    },
    get_venueSelectedHolder: function SpottedScript_Controls_Picker_View$get_venueSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueSelectedHolder');
    },
    get_venueSelectedLabel: function SpottedScript_Controls_Picker_View$get_venueSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueSelectedLabel');
    },
    get_venueSelectedChangeLink: function SpottedScript_Controls_Picker_View$get_venueSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueSelectedChangeLink');
    },
    get_venueChoiceHolder: function SpottedScript_Controls_Picker_View$get_venueChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueChoiceHolder');
    },
    get_venueDropDown: function SpottedScript_Controls_Picker_View$get_venueDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueDropDown');
    },
    get_venueByLetterDropDown: function SpottedScript_Controls_Picker_View$get_venueByLetterDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueByLetterDropDown');
    },
    get_musicHolder: function SpottedScript_Controls_Picker_View$get_musicHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicHolder');
    },
    get_musicSelectedHolder: function SpottedScript_Controls_Picker_View$get_musicSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicSelectedHolder');
    },
    get_musicSelectedLabel: function SpottedScript_Controls_Picker_View$get_musicSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicSelectedLabel');
    },
    get_musicSelectedChangeLink: function SpottedScript_Controls_Picker_View$get_musicSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicSelectedChangeLink');
    },
    get_musicChoiceHolder: function SpottedScript_Controls_Picker_View$get_musicChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicChoiceHolder');
    },
    get_musicDropDown: function SpottedScript_Controls_Picker_View$get_musicDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicDropDown');
    },
    get_dateHolder: function SpottedScript_Controls_Picker_View$get_dateHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateHolder');
    },
    get_dateSelectedHolder: function SpottedScript_Controls_Picker_View$get_dateSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateSelectedHolder');
    },
    get_dateSelectedLabel: function SpottedScript_Controls_Picker_View$get_dateSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateSelectedLabel');
    },
    get_dateSelectedChangeLink: function SpottedScript_Controls_Picker_View$get_dateSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateSelectedChangeLink');
    },
    get_dateChoiceHolder: function SpottedScript_Controls_Picker_View$get_dateChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateChoiceHolder');
    },
    get_dateDayHolder: function SpottedScript_Controls_Picker_View$get_dateDayHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateDayHolder');
    },
    get_dateDayDropDown: function SpottedScript_Controls_Picker_View$get_dateDayDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateDayDropDown');
    },
    get_dateDayPlusImg: function SpottedScript_Controls_Picker_View$get_dateDayPlusImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateDayPlusImg');
    },
    get_dateDayMinusImg: function SpottedScript_Controls_Picker_View$get_dateDayMinusImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateDayMinusImg');
    },
    get_dateMonthDropDown: function SpottedScript_Controls_Picker_View$get_dateMonthDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateMonthDropDown');
    },
    get_dateMonthPlusImg: function SpottedScript_Controls_Picker_View$get_dateMonthPlusImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateMonthPlusImg');
    },
    get_dateMonthMinusImg: function SpottedScript_Controls_Picker_View$get_dateMonthMinusImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateMonthMinusImg');
    },
    get_dateYearTextBox: function SpottedScript_Controls_Picker_View$get_dateYearTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateYearTextBox');
    },
    get_dateYearPlusImg: function SpottedScript_Controls_Picker_View$get_dateYearPlusImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateYearPlusImg');
    },
    get_dateYearMinusImg: function SpottedScript_Controls_Picker_View$get_dateYearMinusImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateYearMinusImg');
    },
    get_eventHolder: function SpottedScript_Controls_Picker_View$get_eventHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventHolder');
    },
    get_eventSelectedHolder: function SpottedScript_Controls_Picker_View$get_eventSelectedHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventSelectedHolder');
    },
    get_eventSelectedLabel: function SpottedScript_Controls_Picker_View$get_eventSelectedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventSelectedLabel');
    },
    get_eventSelectedChangeLink: function SpottedScript_Controls_Picker_View$get_eventSelectedChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventSelectedChangeLink');
    },
    get_eventChoiceHolder: function SpottedScript_Controls_Picker_View$get_eventChoiceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventChoiceHolder');
    },
    get_eventListBox: function SpottedScript_Controls_Picker_View$get_eventListBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventListBox');
    },
    get_selectedSearchTypeHidden: function SpottedScript_Controls_Picker_View$get_selectedSearchTypeHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedSearchTypeHidden');
    },
    get_selectedKeyHidden: function SpottedScript_Controls_Picker_View$get_selectedKeyHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedKeyHidden');
    },
    get_selectedSpotterHidden: function SpottedScript_Controls_Picker_View$get_selectedSpotterHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedSpotterHidden');
    },
    get_selectedBrandHidden: function SpottedScript_Controls_Picker_View$get_selectedBrandHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedBrandHidden');
    },
    get_selectedCountryHidden: function SpottedScript_Controls_Picker_View$get_selectedCountryHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedCountryHidden');
    },
    get_selectedPlaceHidden: function SpottedScript_Controls_Picker_View$get_selectedPlaceHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedPlaceHidden');
    },
    get_selectedVenueHidden: function SpottedScript_Controls_Picker_View$get_selectedVenueHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedVenueHidden');
    },
    get_selectedMusicHidden: function SpottedScript_Controls_Picker_View$get_selectedMusicHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedMusicHidden');
    },
    get_selectedDateHidden: function SpottedScript_Controls_Picker_View$get_selectedDateHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedDateHidden');
    },
    get_selectedEventHidden: function SpottedScript_Controls_Picker_View$get_selectedEventHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedEventHidden');
    },
    get_optionKeyHidden: function SpottedScript_Controls_Picker_View$get_optionKeyHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionKeyHidden');
    },
    get_optionMeHidden: function SpottedScript_Controls_Picker_View$get_optionMeHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionMeHidden');
    },
    get_optionSpotterHidden: function SpottedScript_Controls_Picker_View$get_optionSpotterHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionSpotterHidden');
    },
    get_optionBrandHidden: function SpottedScript_Controls_Picker_View$get_optionBrandHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionBrandHidden');
    },
    get_optionCountryHidden: function SpottedScript_Controls_Picker_View$get_optionCountryHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionCountryHidden');
    },
    get_optionPlaceHidden: function SpottedScript_Controls_Picker_View$get_optionPlaceHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionPlaceHidden');
    },
    get_optionVenueHidden: function SpottedScript_Controls_Picker_View$get_optionVenueHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionVenueHidden');
    },
    get_optionMusicHidden: function SpottedScript_Controls_Picker_View$get_optionMusicHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionMusicHidden');
    },
    get_optionDateHidden: function SpottedScript_Controls_Picker_View$get_optionDateHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionDateHidden');
    },
    get_optionDateDayHidden: function SpottedScript_Controls_Picker_View$get_optionDateDayHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionDateDayHidden');
    },
    get_optionDateDayIncrementHidden: function SpottedScript_Controls_Picker_View$get_optionDateDayIncrementHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionDateDayIncrementHidden');
    },
    get_optionEventHidden: function SpottedScript_Controls_Picker_View$get_optionEventHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionEventHidden');
    },
    get_optionGoogleHidden: function SpottedScript_Controls_Picker_View$get_optionGoogleHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionGoogleHidden');
    }
}
Type.registerNamespace('SpottedScript.Controls.Picker.Shared');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Picker.Shared.SearchTypes
SpottedScript.Controls.Picker.Shared.SearchTypes = function() { 
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
SpottedScript.Controls.Picker.Shared.SearchTypes.prototype = {
    unknown: 0, 
    venue: 1, 
    brand: 2, 
    music: 3, 
    google: 4, 
    spotter: 5, 
    me: 6, 
    key: 7
}
SpottedScript.Controls.Picker.Shared.SearchTypes.registerEnum('SpottedScript.Controls.Picker.Shared.SearchTypes', false);
SpottedScript.Controls.Picker.Controller.registerClass('SpottedScript.Controls.Picker.Controller');
SpottedScript.Controls.Picker.DateStub.registerClass('SpottedScript.Controls.Picker.DateStub');
SpottedScript.Controls.Picker.ObjectStub.registerClass('SpottedScript.Controls.Picker.ObjectStub');
SpottedScript.Controls.Picker.EventSelectionSpecification.registerClass('SpottedScript.Controls.Picker.EventSelectionSpecification');
SpottedScript.Controls.Picker.StringArgs.registerClass('SpottedScript.Controls.Picker.StringArgs', Sys.EventArgs);
SpottedScript.Controls.Picker.ObjectArgs.registerClass('SpottedScript.Controls.Picker.ObjectArgs', Sys.EventArgs);
SpottedScript.Controls.Picker.DateArgs.registerClass('SpottedScript.Controls.Picker.DateArgs', Sys.EventArgs);
SpottedScript.Controls.Picker.EventSelectionArgs.registerClass('SpottedScript.Controls.Picker.EventSelectionArgs', Sys.EventArgs);
SpottedScript.Controls.Picker.View.registerClass('SpottedScript.Controls.Picker.View');
