Type.registerNamespace('SpottedScript.Controls.EventCreator');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventCreator.Controller
SpottedScript.Controls.EventCreator.Controller = function SpottedScript_Controls_EventCreator_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.EventCreator.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.EventCreator.View">
    /// </field>
    /// <field name="_callback" type="SpottedScript.Controls.EventCreator.EventInfoCallback">
    /// </field>
    /// <field name="instance" type="SpottedScript.Controls.EventCreator.Controller" static="true">
    /// </field>
    /// <field name="oldOnFocus" type="Sys.UI.DomEventHandler">
    /// </field>
    /// <field name="_oldTransformReceivedSuggestions" type="ScriptSharpLibrary.TransformSuggestions">
    /// </field>
    /// <field name="_oldItemChosen" type="ScriptSharpLibrary.KeyStringPairAction">
    /// </field>
    /// <field name="_eventInfo" type="SpottedScript.Controls.EventCreator.EventInfo">
    /// </field>
    if (SpottedScript.Controls.EventCreator.Controller.instance == null) {
        SpottedScript.Controls.EventCreator.Controller.instance = this;
        this._view = view;
    }
    this.oldOnFocus = this._view.get_uiEventName().onFocus;
    this._view.get_uiEventName().onFocus = Function.createDelegate(this, this._onEventNameFocus);
    this._oldTransformReceivedSuggestions = this._view.get_uiEventName().transformReceivedSuggestions;
    this._view.get_uiEventName().transformReceivedSuggestions = Function.createDelegate(this, this._transformReceivedSuggestions);
    this._oldItemChosen = this._view.get_uiEventName().itemChosen;
    this._view.get_uiEventName().itemChosen = Function.createDelegate(this, this._itemChosen);
    view.get_uiEventName().parameters.set('returnInfo', true);
    $addHandler(view.get_uiContainer(), 'keydown', Function.createDelegate(this, this._onContainerKeyDown));
    $addHandler(view.get_uiAdd(), 'click', Function.createDelegate(this, this._onAddClick));
    SpottedScript.Misc.addHoverText(view.get_uiSummary(), 'click to select an event');
}
SpottedScript.Controls.EventCreator.Controller.prototype = {
    _view: null,
    _callback: null,
    oldOnFocus: null,
    _oldTransformReceivedSuggestions: null,
    _oldItemChosen: null,
    _eventInfo: null,
    _onAddClick: function SpottedScript_Controls_EventCreator_Controller$_onAddClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var brands = [];
        if (this._view.get_uiBrand().get_value().length > 0) {
            brands[0] = Number.parseInvariant(this._view.get_uiBrand().get_value());
        }
        Spotted.WebServices.Controls.EventCreator.Service.addEvent(this._view.get_uiCal().getDate(), this._view.get_uiVenueGetter()._getVenue().k, this._view.get_uiEventName().get_text(), this._view.get_uiSummary().value, brands, Function.createDelegate(this, this._addEventSuccess), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, 5000);
        e.preventDefault();
    },
    _onContainerKeyDown: function SpottedScript_Controls_EventCreator_Controller$_onContainerKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode === Sys.UI.Key.esc) {
            this._eventChosen(null);
        }
    },
    _itemChosen: function SpottedScript_Controls_EventCreator_Controller$_itemChosen(suggestion) {
        /// <param name="suggestion" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        if (suggestion.value === '{addMethod:quick}') {
            jQuery(this._view.get_uiAddOptionsPanel()).slideDown(Function.createDelegate(this, function() {
                this._view.get_uiSummary().focus();
            }));
            this._view.get_uiEventName().set_text(suggestion.key);
        }
        else if (suggestion.value === '{filloutFields}') {
            if (this._view.get_uiCal().getDate() == null) {
                this._view.get_uiCal()._focus();
            }
            else {
                this._view.get_uiVenueGetter()._focus();
            }
            this._view.get_uiEventName().set_text('');
            this._view.get_uiEventName().set_value('');
        }
        else {
            this._eventChosen(Sys.Serialization.JavaScriptSerializer.deserialize(suggestion.value));
        }
    },
    _eventChosen: function SpottedScript_Controls_EventCreator_Controller$_eventChosen(chosenEvent) {
        /// <param name="chosenEvent" type="SpottedScript.Controls.EventCreator.EventInfo">
        /// </param>
        this._view.get_uiContainer().style.display = 'none';
        this._callback(chosenEvent);
    },
    _addEventSuccess: function SpottedScript_Controls_EventCreator_Controller$_addEventSuccess(result, userContext, methodName) {
        /// <param name="result" type="SpottedScript.Controls.EventCreator.EventInfo">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._eventChosen(result);
    },
    _transformReceivedSuggestions: function SpottedScript_Controls_EventCreator_Controller$_transformReceivedSuggestions(suggestions, maxNumberOfItemsToGet) {
        /// <param name="suggestions" type="Array" elementType="Suggestion">
        /// </param>
        /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
        /// </param>
        /// <returns type="Array" elementType="Suggestion"></returns>
        if (this._view.get_uiVenueGetter()._getVenue() != null && this._view.get_uiCal().getDate() != null && this._view.get_uiEventName().get_value() === '') {
            var eventString = this._view.get_uiEventName().get_text() + ' @ ' + this._view.get_uiVenueGetter()._getVenue().name + ' on ' + this._view.get_uiCal().getDate().format('ddd dd/MM/yyyy');
            var quickAdd = new ScriptSharpLibrary.Suggestion();
            quickAdd.html = ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml('@/gfx/icon40-eventcreator-add.png', 'Add <b>' + eventString + '</b>', 'You\'ll be able to add more details later if you like');
            quickAdd.value = '{addMethod:quick}';
            quickAdd.text = this._view.get_uiEventName().get_text();
            ScriptSharpLibrary.Suggestion.addSuggestion(suggestions, quickAdd, maxNumberOfItemsToGet);
        }
        else {
            var notInDbSuggestion = new ScriptSharpLibrary.Suggestion();
            notInDbSuggestion.html = ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml('/gfx/icon40-eventcreator-noadd.png', 'Can\'t add <b>' + this._view.get_uiEventName().get_text() + '</b>', 'Fill out all the fields');
            notInDbSuggestion.value = '{filloutFields}';
            notInDbSuggestion.text = this._view.get_uiEventName().get_text();
            ScriptSharpLibrary.Suggestion.addSuggestion(suggestions, notInDbSuggestion, maxNumberOfItemsToGet);
        }
        if (this._oldTransformReceivedSuggestions != null) {
            suggestions = this._oldTransformReceivedSuggestions(suggestions, maxNumberOfItemsToGet);
        }
        return suggestions;
    },
    _onEventNameFocus: function SpottedScript_Controls_EventCreator_Controller$_onEventNameFocus(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this._view.get_uiCal().getDate() != null) {
            this._view.get_uiEventName().parameters.set('date', this._view.get_uiCal().getDate());
        }
        else {
            this._view.get_uiEventName().parameters.set('date', null);
        }
        var vi = this._view.get_uiVenueGetter()._getVenue();
        if (vi == null) {
            this._view.get_uiEventName().parameters.set('venueK', null);
        }
        else {
            this._view.get_uiEventName().parameters.set('venueK', this._view.get_uiVenueGetter()._getVenue().k);
        }
        if (this.oldOnFocus != null) {
            this.oldOnFocus(e);
        }
    },
    createEventUsingEventInfo: function SpottedScript_Controls_EventCreator_Controller$createEventUsingEventInfo(eventInfo, callback) {
        /// <param name="eventInfo" type="SpottedScript.Controls.EventCreator.EventInfo">
        /// </param>
        /// <param name="callback" type="SpottedScript.Controls.EventCreator.EventInfoCallback">
        /// </param>
        if (eventInfo == null) {
            this.showPopup(null, null, null, callback);
        }
        else {
            this.showPopup(eventInfo.date, eventInfo.venueInfo, eventInfo.name, callback);
        }
    },
    showPopup: function SpottedScript_Controls_EventCreator_Controller$showPopup(date, info, text, callback) {
        /// <param name="date" type="Date">
        /// </param>
        /// <param name="info" type="SpottedScript.Controls.VenueCreator.VenueInfo">
        /// </param>
        /// <param name="text" type="String">
        /// </param>
        /// <param name="callback" type="SpottedScript.Controls.EventCreator.EventInfoCallback">
        /// </param>
        this._view.get_uiContainer().style.display = '';
        this._callback = callback;
        this._view.get_uiEventName().set_text((text != null) ? text : '');
        this._view.get_uiEventName().focus();
        if (info != null) {
            this._view.get_uiVenueGetter().setVenue(info);
        }
        else {
            this._view.get_uiVenueGetter()._focus();
        }
        if (date != null) {
            this._view.get_uiCal().setDate(date);
        }
        else {
            this._view.get_uiCal()._focus();
        }
        if (date != null && info != null && callback != null) {
            this._view.get_uiEventName().requestSuggestions();
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventCreator.EventInfo
SpottedScript.Controls.EventCreator.EventInfo = function SpottedScript_Controls_EventCreator_EventInfo() {
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="date" type="Date">
    /// </field>
    /// <field name="venueInfo" type="SpottedScript.Controls.VenueCreator.VenueInfo">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="picPath" type="String">
    /// </field>
}
SpottedScript.Controls.EventCreator.EventInfo.eventFullName = function SpottedScript_Controls_EventCreator_EventInfo$eventFullName(ei) {
    /// <param name="ei" type="SpottedScript.Controls.EventCreator.EventInfo">
    /// </param>
    /// <returns type="String"></returns>
    return ei.name + ' on ' + ei.date.format('ddd dd/MM/yyyy') + ' @ ' + SpottedScript.Controls.VenueCreator.VenueInfo.nameWithPlace(ei.venueInfo);
}
SpottedScript.Controls.EventCreator.EventInfo.prototype = {
    name: null,
    k: 0,
    date: null,
    venueInfo: null,
    url: null,
    picPath: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventCreator.View
SpottedScript.Controls.EventCreator.View = function SpottedScript_Controls_EventCreator_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.EventCreator.View.prototype = {
    clientId: null,
    get_uiContainer: function SpottedScript_Controls_EventCreator_View$get_uiContainer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiContainer');
    },
    get_h1: function SpottedScript_Controls_EventCreator_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_uiCal: function SpottedScript_Controls_EventCreator_View$get_uiCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiCalController');
    },
    get_uiVenueGetter: function SpottedScript_Controls_EventCreator_View$get_uiVenueGetter() {
        /// <value type="SpottedScript.Controls.VenueGetter.Controller"></value>
        return eval(this.clientId + '_uiVenueGetterController');
    },
    get_uiEventName: function SpottedScript_Controls_EventCreator_View$get_uiEventName() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiEventNameBehaviour');
    },
    get_uiAddOptionsPanel: function SpottedScript_Controls_EventCreator_View$get_uiAddOptionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddOptionsPanel');
    },
    get_uiSummary: function SpottedScript_Controls_EventCreator_View$get_uiSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSummary');
    },
    get_uiBrand: function SpottedScript_Controls_EventCreator_View$get_uiBrand() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiBrandBehaviour');
    },
    get_uiAdd: function SpottedScript_Controls_EventCreator_View$get_uiAdd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAdd');
    }
}
SpottedScript.Controls.EventCreator.Controller.registerClass('SpottedScript.Controls.EventCreator.Controller');
SpottedScript.Controls.EventCreator.EventInfo.registerClass('SpottedScript.Controls.EventCreator.EventInfo');
SpottedScript.Controls.EventCreator.View.registerClass('SpottedScript.Controls.EventCreator.View');
SpottedScript.Controls.EventCreator.Controller.instance = null;
