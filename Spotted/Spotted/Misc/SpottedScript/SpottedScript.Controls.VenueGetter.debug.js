Type.registerNamespace('SpottedScript.Controls.VenueGetter');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.VenueGetter.Controller
SpottedScript.Controls.VenueGetter.Controller = function SpottedScript_Controls_VenueGetter_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.VenueGetter.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.VenueGetter.View">
    /// </field>
    /// <field name="_oldTransformReceivedSuggestions" type="ScriptSharpLibrary.TransformSuggestions">
    /// </field>
    /// <field name="_oldItemChosen" type="ScriptSharpLibrary.KeyStringPairAction">
    /// </field>
    this._view = view;
    this._oldTransformReceivedSuggestions = view.get_uiAuto().transformReceivedSuggestions;
    view.get_uiAuto().transformReceivedSuggestions = Function.createDelegate(this, this._transformReceivedSuggestions);
    this._oldItemChosen = view.get_uiAuto().itemChosen;
    view.get_uiAuto().itemChosen = Function.createDelegate(this, this._itemChosen);
    view.get_uiAuto().parameters.set('returnInfo', true);
    $addHandler(view.get_uiSelectedItemPanel(), 'click', Function.createDelegate(this, this._onSelectedItemClick));
    SpottedScript.Misc.addHoverText(view.get_uiSelectedItemPanel(), 'Click here to change');
}
SpottedScript.Controls.VenueGetter.Controller.prototype = {
    _view: null,
    _oldTransformReceivedSuggestions: null,
    _oldItemChosen: null,
    _onSelectedItemClick: function SpottedScript_Controls_VenueGetter_Controller$_onSelectedItemClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._focus();
        this._view.get_uiAuto().requestSuggestions();
    },
    setVenue: function SpottedScript_Controls_VenueGetter_Controller$setVenue(venueInfo) {
        /// <param name="venueInfo" type="SpottedScript.Controls.VenueCreator.VenueInfo">
        /// </param>
        if (venueInfo != null) {
            this._view.get_uiSelectedItemPanel().style.display = '';
            this._view.get_uiSelectedItemPanel().innerHTML = ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml(venueInfo.picPath, venueInfo.name, SpottedScript.Controls.VenueCreator.PlaceInfo.nameWithCountry(venueInfo.place));
            this._view.get_uiAuto().set_text(venueInfo.name + ', ' + venueInfo.place.name + ', ' + venueInfo.place.country.name);
            this._view.get_uiAuto().set_value(Sys.Serialization.JavaScriptSerializer.serialize(venueInfo));
            this._view.get_uiAuto()._input.style.display = 'none';
            this._view.get_uiSelectedItemPanel().style.display = '';
        }
        else {
            this._view.get_uiAuto()._input.style.display = '';
            this._view.get_uiSelectedItemPanel().style.display = 'none';
            this._view.get_uiAuto().set_text('');
            this._view.get_uiAuto().set_value('');
        }
    },
    _getVenue: function SpottedScript_Controls_VenueGetter_Controller$_getVenue() {
        /// <returns type="SpottedScript.Controls.VenueCreator.VenueInfo"></returns>
        try {
            return Sys.Serialization.JavaScriptSerializer.deserialize(this._view.get_uiAuto().get_value());
        }
        catch (ex) {
            return null;
        }
    },
    _itemChosen: function SpottedScript_Controls_VenueGetter_Controller$_itemChosen(suggestion) {
        /// <param name="suggestion" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        if (suggestion.value.startsWith('|create|')) {
            var parts = suggestion.value.split('|');
            var venueName = parts[parts.length - 1];
            SpottedScript.Controls.VenueCreator.Controller._instance.createVenue(venueName, null, Function.createDelegate(this, this.setVenue));
        }
        else {
            this.setVenue(Sys.Serialization.JavaScriptSerializer.deserialize(suggestion.value));
            if (this._oldItemChosen != null) {
                this._oldItemChosen(suggestion);
            }
        }
    },
    _transformReceivedSuggestions: function SpottedScript_Controls_VenueGetter_Controller$_transformReceivedSuggestions(suggestions, maxNumberOfItemsToGet) {
        /// <param name="suggestions" type="Array" elementType="Suggestion">
        /// </param>
        /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
        /// </param>
        /// <returns type="Array" elementType="Suggestion"></returns>
        if (this._oldTransformReceivedSuggestions != null) {
            suggestions = this._oldTransformReceivedSuggestions(suggestions, maxNumberOfItemsToGet);
        }
        if (this._view.get_uiAuto().get_text() !== '' && this._view.get_uiAuto().get_value() == null) {
            var createNewVenueSuggestion = new ScriptSharpLibrary.Suggestion();
            createNewVenueSuggestion.html = ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml('/gfx/icon40-eventcreator-add.png', 'Add <b>' + this._view.get_uiAuto().get_text() + '</b>', 'Other people will be able to add details about the venue later');
            createNewVenueSuggestion.value = '|create|' + this._view.get_uiAuto().get_text() + '';
            createNewVenueSuggestion.text = createNewVenueSuggestion.value;
            ScriptSharpLibrary.Suggestion.addSuggestion(suggestions, createNewVenueSuggestion, maxNumberOfItemsToGet);
        }
        return suggestions;
    },
    _focus: function SpottedScript_Controls_VenueGetter_Controller$_focus() {
        this._view.get_uiAuto()._input.style.display = '';
        this._view.get_uiSelectedItemPanel().style.display = 'none';
        this._view.get_uiAuto().focus();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.VenueGetter.View
SpottedScript.Controls.VenueGetter.View = function SpottedScript_Controls_VenueGetter_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.VenueGetter.View.prototype = {
    clientId: null,
    get_uiOuterPanel: function SpottedScript_Controls_VenueGetter_View$get_uiOuterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiOuterPanel');
    },
    get_uiAuto: function SpottedScript_Controls_VenueGetter_View$get_uiAuto() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiAutoBehaviour');
    },
    get_uiSelectedItemPanel: function SpottedScript_Controls_VenueGetter_View$get_uiSelectedItemPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSelectedItemPanel');
    }
}
SpottedScript.Controls.VenueGetter.Controller.registerClass('SpottedScript.Controls.VenueGetter.Controller');
SpottedScript.Controls.VenueGetter.View.registerClass('SpottedScript.Controls.VenueGetter.View');
