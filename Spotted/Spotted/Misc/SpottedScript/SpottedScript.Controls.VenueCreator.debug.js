Type.registerNamespace('SpottedScript.Controls.VenueCreator');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.VenueCreator.CountryInfo
SpottedScript.Controls.VenueCreator.CountryInfo = function SpottedScript_Controls_VenueCreator_CountryInfo() {
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
}
SpottedScript.Controls.VenueCreator.CountryInfo.prototype = {
    name: null,
    k: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.VenueCreator.PlaceInfo
SpottedScript.Controls.VenueCreator.PlaceInfo = function SpottedScript_Controls_VenueCreator_PlaceInfo() {
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="country" type="SpottedScript.Controls.VenueCreator.CountryInfo">
    /// </field>
}
SpottedScript.Controls.VenueCreator.PlaceInfo.nameWithCountry = function SpottedScript_Controls_VenueCreator_PlaceInfo$nameWithCountry(pi) {
    /// <param name="pi" type="SpottedScript.Controls.VenueCreator.PlaceInfo">
    /// </param>
    /// <returns type="String"></returns>
    return pi.name + ', ' + pi.country.name;
}
SpottedScript.Controls.VenueCreator.PlaceInfo.prototype = {
    name: null,
    k: 0,
    country: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.VenueCreator.VenueInfo
SpottedScript.Controls.VenueCreator.VenueInfo = function SpottedScript_Controls_VenueCreator_VenueInfo() {
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="place" type="SpottedScript.Controls.VenueCreator.PlaceInfo">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="picPath" type="String">
    /// </field>
}
SpottedScript.Controls.VenueCreator.VenueInfo.nameWithPlace = function SpottedScript_Controls_VenueCreator_VenueInfo$nameWithPlace(vi) {
    /// <param name="vi" type="SpottedScript.Controls.VenueCreator.VenueInfo">
    /// </param>
    /// <returns type="String"></returns>
    return vi.name + ', ' + SpottedScript.Controls.VenueCreator.PlaceInfo.nameWithCountry(vi.place);
}
SpottedScript.Controls.VenueCreator.VenueInfo.prototype = {
    name: null,
    k: 0,
    place: null,
    url: null,
    picPath: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.VenueCreator.Controller
SpottedScript.Controls.VenueCreator.Controller = function SpottedScript_Controls_VenueCreator_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.VenueCreator.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.VenueCreator.View">
    /// </field>
    /// <field name="_callback" type="SpottedScript.Controls.VenueCreator.VenueInfoCallback">
    /// </field>
    /// <field name="_instance" type="SpottedScript.Controls.VenueCreator.Controller" static="true">
    /// </field>
    /// <field name="_oldTransformReceivedSuggestions" type="ScriptSharpLibrary.TransformSuggestions">
    /// </field>
    /// <field name="_oldNameSuggestItemChosen" type="ScriptSharpLibrary.KeyStringPairAction">
    /// </field>
    /// <field name="_oldPlaceItemChosen" type="ScriptSharpLibrary.KeyStringPairAction">
    /// </field>
    /// <field name="_regex" type="RegExp">
    /// </field>
    /// <field name="_oldBorder" type="String">
    /// </field>
    this._regex = new RegExp('^((([A-PR-UWYZ](\\d([A-HJKSTUW]|\\d)?|[A-HK-Y]\\d([ABEHMNPRVWXY]|\\d)?))\\s*(\\d[ABD-HJLNP-UW-Z]{2})?)|GIR\\s*0AA)$', 'i');
    if (SpottedScript.Controls.VenueCreator.Controller._instance == null) {
        SpottedScript.Controls.VenueCreator.Controller._instance = this;
        this._view = view;
    }
    this._oldTransformReceivedSuggestions = this._view.get_uiNameSuggest().transformReceivedSuggestions;
    this._view.get_uiNameSuggest().transformReceivedSuggestions = Function.createDelegate(this, this._transformReceivedSuggestions);
    this._oldNameSuggestItemChosen = view.get_uiNameSuggest().itemChosen;
    this._view.get_uiNameSuggest().itemChosen = Function.createDelegate(this, this._nameSuggestItemChosen);
    this._oldPlaceItemChosen = this._view.get_uiPlace().itemChosen;
    this._view.get_uiPlace().itemChosen = Function.createDelegate(this, this._placeItemChosen);
    this._view.get_uiPlace().parameters.set('returnInfo', true);
    $addHandler(view.get_uiPostCode(), 'blur', Function.createDelegate(this, this._onPostCodeBlur));
    $addHandler(view.get_uiContainer(), 'keydown', Function.createDelegate(this, this._onContainerKeyDown));
}
SpottedScript.Controls.VenueCreator.Controller.prototype = {
    _view: null,
    _callback: null,
    _oldTransformReceivedSuggestions: null,
    _oldNameSuggestItemChosen: null,
    _oldPlaceItemChosen: null,
    _onContainerKeyDown: function SpottedScript_Controls_VenueCreator_Controller$_onContainerKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode === Sys.UI.Key.esc) {
            this._venueChosen(null, null, null);
        }
    },
    _oldBorder: null,
    _onPostCodeBlur: function SpottedScript_Controls_VenueCreator_Controller$_onPostCodeBlur(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this._view.get_uiPostCode().value === '' || this._view.get_uiPostCode().disabled || this._regex.test(this._view.get_uiPostCode().value)) {
            if (this._oldBorder != null) {
                this._view.get_uiPostCode().style.border = '';
            }
        }
        else {
            this._oldBorder = this._view.get_uiPostCode().style.border;
            this._view.get_uiPostCode().focus();
        }
    },
    _placeItemChosen: function SpottedScript_Controls_VenueCreator_Controller$_placeItemChosen(suggestion) {
        /// <param name="suggestion" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        var placeInfo = Sys.Serialization.JavaScriptSerializer.deserialize(suggestion.value);
        this._setPlaceText(placeInfo.country.k);
        this._view.get_uiNameSuggest().parameters.set('placeK', placeInfo.k);
        if (this._oldPlaceItemChosen != null) {
            this._oldPlaceItemChosen(suggestion);
        }
    },
    _setPlaceText: function SpottedScript_Controls_VenueCreator_Controller$_setPlaceText(countryK) {
        /// <param name="countryK" type="Number" integer="true">
        /// </param>
        this._view.get_uiPostCode().disabled = (countryK !== 224);
        if (this._view.get_uiPostCode().disabled) {
            this._view.get_uiPostCode().value = 'UK only';
        }
    },
    _nameSuggestItemChosen: function SpottedScript_Controls_VenueCreator_Controller$_nameSuggestItemChosen(suggestion) {
        /// <param name="suggestion" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        if (suggestion.value === '{addMethod:quick}') {
            var pi = Sys.Serialization.JavaScriptSerializer.deserialize(this._view.get_uiPlace().get_value());
            Spotted.WebServices.Controls.VenueCreator.Service.createVenue(this._view.get_uiNameSuggest().get_text(), pi.k, (this._view.get_uiPostCode().disabled) ? '' : this._view.get_uiPostCode().value, Function.createDelegate(this, this._venueChosen), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, 5000);
        }
        else if (suggestion.value === '{filloutFields}') {
            this._view.get_uiPlace().focus();
        }
        else {
            this._venueChosen(Sys.Serialization.JavaScriptSerializer.deserialize(suggestion.value), null, null);
        }
        if (this._oldNameSuggestItemChosen != null) {
            this._oldNameSuggestItemChosen(suggestion);
        }
    },
    _venueChosen: function SpottedScript_Controls_VenueCreator_Controller$_venueChosen(result, userContext, methodName) {
        /// <param name="result" type="SpottedScript.Controls.VenueCreator.VenueInfo">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._view.get_uiContainer().style.display = 'none';
        if (this._callback != null) {
            this._callback(result);
        }
    },
    _transformReceivedSuggestions: function SpottedScript_Controls_VenueCreator_Controller$_transformReceivedSuggestions(suggestions, maxNumberOfItemsToGet) {
        /// <param name="suggestions" type="Array" elementType="Suggestion">
        /// </param>
        /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
        /// </param>
        /// <returns type="Array" elementType="Suggestion"></returns>
        if (this._view.get_uiPlace().get_value() != null && this._view.get_uiNameSuggest().get_text() !== '') {
            var venueString = this._view.get_uiNameSuggest().get_text() + ', ' + this._view.get_uiPlace().get_text();
            var quickAdd = new ScriptSharpLibrary.Suggestion();
            quickAdd.html = ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml('@/gfx/icon40-eventcreator-venue-add.png', 'Add <b>' + venueString + '</b> as a new venue', 'You\'ll be able to add more details later if you like');
            quickAdd.value = '{addMethod:quick}';
            quickAdd.text = this._view.get_uiNameSuggest().get_text();
            ScriptSharpLibrary.Suggestion.addSuggestion(suggestions, quickAdd, maxNumberOfItemsToGet);
        }
        else {
            var notInDbSuggestion = new ScriptSharpLibrary.Suggestion();
            notInDbSuggestion.html = ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml('/gfx/icon40-eventcreator-venue-noadd.png', 'Can\'t add <b>' + this._view.get_uiNameSuggest().get_text() + '</b> as a new place', 'Fill out all the fields');
            notInDbSuggestion.value = '{filloutFields}';
            notInDbSuggestion.text = notInDbSuggestion.value;
            ScriptSharpLibrary.Suggestion.addSuggestion(suggestions, notInDbSuggestion, maxNumberOfItemsToGet);
        }
        if (this._oldTransformReceivedSuggestions != null) {
            suggestions = this._oldTransformReceivedSuggestions(suggestions, maxNumberOfItemsToGet);
        }
        return suggestions;
    },
    createVenue: function SpottedScript_Controls_VenueCreator_Controller$createVenue(name, place, callbackIn) {
        /// <param name="name" type="String">
        /// </param>
        /// <param name="place" type="ScriptSharpLibrary.KeyValuePair">
        /// </param>
        /// <param name="callbackIn" type="SpottedScript.Controls.VenueCreator.VenueInfoCallback">
        /// </param>
        this._view.get_uiContainer().style.display = '';
        this._view.get_uiNameSuggest().focus();
        if (name != null) {
            this._view.get_uiNameSuggest().set_value(name);
            this._view.get_uiNameSuggest().set_text(name);
            this._view.get_uiPlace().focus();
        }
        if (place != null) {
            this._view.get_uiPlace().set_text(place.key);
            this._view.get_uiPlace().set_value(place.value.toString());
        }
        if (place != null && name != null) {
            this._view.get_uiNameSuggest().focus();
            this._view.get_uiNameSuggest().requestSuggestions();
        }
        this._callback = callbackIn;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.VenueCreator.View
SpottedScript.Controls.VenueCreator.View = function SpottedScript_Controls_VenueCreator_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.VenueCreator.View.prototype = {
    clientId: null,
    get_uiContainer: function SpottedScript_Controls_VenueCreator_View$get_uiContainer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiContainer');
    },
    get_h1: function SpottedScript_Controls_VenueCreator_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_uiPlace: function SpottedScript_Controls_VenueCreator_View$get_uiPlace() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPlaceBehaviour');
    },
    get_uiNameSuggest: function SpottedScript_Controls_VenueCreator_View$get_uiNameSuggest() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiNameSuggestBehaviour');
    },
    get_uiPostCode: function SpottedScript_Controls_VenueCreator_View$get_uiPostCode() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPostCode');
    }
}
SpottedScript.Controls.VenueCreator.CountryInfo.registerClass('SpottedScript.Controls.VenueCreator.CountryInfo');
SpottedScript.Controls.VenueCreator.PlaceInfo.registerClass('SpottedScript.Controls.VenueCreator.PlaceInfo');
SpottedScript.Controls.VenueCreator.VenueInfo.registerClass('SpottedScript.Controls.VenueCreator.VenueInfo');
SpottedScript.Controls.VenueCreator.Controller.registerClass('SpottedScript.Controls.VenueCreator.Controller');
SpottedScript.Controls.VenueCreator.View.registerClass('SpottedScript.Controls.VenueCreator.View');
SpottedScript.Controls.VenueCreator.Controller._instance = null;
