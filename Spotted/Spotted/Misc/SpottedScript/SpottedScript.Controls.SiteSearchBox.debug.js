Type.registerNamespace('SpottedScript.Controls.SiteSearchBox');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.SiteSearchBox.Controller
SpottedScript.Controls.SiteSearchBox.Controller = function SpottedScript_Controls_SiteSearchBox_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.SiteSearchBox.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.SiteSearchBox.View">
    /// </field>
    /// <field name="_oldTransformReceivedSuggestions" type="ScriptSharpLibrary.TransformSuggestions">
    /// </field>
    /// <field name="_oldItemChosen" type="ScriptSharpLibrary.KeyStringPairAction">
    /// </field>
    /// <field name="_oldOnSuggestionsRequested" type="ScriptSharpLibrary.Action">
    /// </field>
    this._view = view;
    this._oldTransformReceivedSuggestions = this._view.get_uiAuto().transformReceivedSuggestions;
    this._view.get_uiAuto().transformReceivedSuggestions = Function.createDelegate(this, this._transformReceivedSuggestions);
    this._oldItemChosen = this._view.get_uiAuto().itemChosen;
    this._view.get_uiAuto().itemChosen = Function.createDelegate(this, this._itemChosen);
    this._oldOnSuggestionsRequested = view.get_uiAuto().onSuggestionsRequested;
    view.get_uiAuto().onSuggestionsRequested = Function.createDelegate(this, this._onSuggestionsRequested);
}
SpottedScript.Controls.SiteSearchBox.Controller.prototype = {
    _view: null,
    _oldTransformReceivedSuggestions: null,
    _oldItemChosen: null,
    _oldOnSuggestionsRequested: null,
    _onSuggestionsRequested: function SpottedScript_Controls_SiteSearchBox_Controller$_onSuggestionsRequested() {
        this._view.get_uiAuto().addSuggestion(this._getGoogleSearchSuggestion());
    },
    _itemChosen: function SpottedScript_Controls_SiteSearchBox_Controller$_itemChosen(suggestion) {
        /// <param name="suggestion" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        if (suggestion.value === '{google}') {
            eval('window.location = \'http://www.google.co.uk/search?q=site:dontstayin.com+' + encodeURI(suggestion.key).replace(new RegExp('\'', 'g'), '\\\'') + '\';');
        }
        else {
            eval('window.location = \'' + suggestion.value + '\';');
        }
    },
    _transformReceivedSuggestions: function SpottedScript_Controls_SiteSearchBox_Controller$_transformReceivedSuggestions(suggestions, maxNumberToGet) {
        /// <param name="suggestions" type="Array" elementType="Suggestion">
        /// </param>
        /// <param name="maxNumberToGet" type="Number" integer="true">
        /// </param>
        /// <returns type="Array" elementType="Suggestion"></returns>
        ScriptSharpLibrary.Suggestion.addSuggestionAtTop(suggestions, this._getGoogleSearchSuggestion());
        if (this._oldTransformReceivedSuggestions != null) {
            suggestions = this._oldTransformReceivedSuggestions(suggestions, maxNumberToGet);
        }
        return suggestions;
    },
    _getGoogleSearchSuggestion: function SpottedScript_Controls_SiteSearchBox_Controller$_getGoogleSearchSuggestion() {
        /// <returns type="ScriptSharpLibrary.Suggestion"></returns>
        var googleSearchSuggestion = new ScriptSharpLibrary.Suggestion();
        googleSearchSuggestion.html = ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml('/gfx/icon40-google.png', 'Search DSI using Google', '');
        googleSearchSuggestion.text = this._view.get_uiAuto().get_text();
        googleSearchSuggestion.value = '{google}';
        return googleSearchSuggestion;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.SiteSearchBox.View
SpottedScript.Controls.SiteSearchBox.View = function SpottedScript_Controls_SiteSearchBox_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.SiteSearchBox.View.prototype = {
    clientId: null,
    get_uiAuto: function SpottedScript_Controls_SiteSearchBox_View$get_uiAuto() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiAutoBehaviour');
    },
    get_googleSearchCode: function SpottedScript_Controls_SiteSearchBox_View$get_googleSearchCode() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GoogleSearchCode');
    }
}
SpottedScript.Controls.SiteSearchBox.Controller.registerClass('SpottedScript.Controls.SiteSearchBox.Controller');
SpottedScript.Controls.SiteSearchBox.View.registerClass('SpottedScript.Controls.SiteSearchBox.View');
