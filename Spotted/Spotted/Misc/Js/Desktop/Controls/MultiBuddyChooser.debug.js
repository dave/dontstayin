//! MultiBuddyChooser.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.MultiBuddyChooser');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.CreateUserFromEmailController

Js.Controls.MultiBuddyChooser.CreateUserFromEmailController = function Js_Controls_MultiBuddyChooser_CreateUserFromEmailController(selector) {
    /// <param name="selector" type="Js.ClientControls.HtmlAutoCompleteBehaviour">
    /// </param>
    /// <field name="_oldItemChosen" type="Js.ClientControls.Function">
    /// </field>
    /// <field name="_selector" type="Js.ClientControls.HtmlAutoCompleteBehaviour">
    /// </field>
    /// <field name="_oldOnSuggestionsRequested" type="Function">
    /// </field>
    /// <field name="_emailRegex" type="String" static="true">
    /// </field>
    this._selector = selector;
    this._oldOnSuggestionsRequested = selector.onSuggestionsRequested;
    selector.onSuggestionsRequested = ss.Delegate.create(this, this._checkForEmailAddress);
}
Js.Controls.MultiBuddyChooser.CreateUserFromEmailController._getPicTitleDetailTemplateHtml = function Js_Controls_MultiBuddyChooser_CreateUserFromEmailController$_getPicTitleDetailTemplateHtml(imageSrc, title, detail) {
    /// <param name="imageSrc" type="String">
    /// </param>
    /// <param name="title" type="String">
    /// </param>
    /// <param name="detail" type="String">
    /// </param>
    /// <returns type="String"></returns>
    return String.format("<table cellspacing='0' cellpadding='0'><tr><td width='40'><img src='{0}' border=0 width=40 hspace=0 height=40 /></td><td style='padding:3px;' valign='top'><b>{1}</b><br />{2}</td></tr></table>", imageSrc, title, detail);
}
Js.Controls.MultiBuddyChooser.CreateUserFromEmailController.prototype = {
    _oldItemChosen: null,
    _selector: null,
    _oldOnSuggestionsRequested: null,
    
    _itemChosen: function Js_Controls_MultiBuddyChooser_CreateUserFromEmailController$_itemChosen(item) {
        /// <param name="item" type="Js.ClientControls.KeyStringPair">
        /// </param>
        if (item.value.startsWith("{'email':")) {
            var value = eval('(' + item.value + ')');
            Js.Controls.MultiBuddyChooser.Service.createUsrFromEmailAndReturnK(value.email, ss.Delegate.create(this, this._createUsrFromEmailAndReturnKSuccessCallback), Js.Library.Trace.webServiceFailure, item, 3000);
        }
        else {
            if (this._oldItemChosen != null) {
                this._oldItemChosen(item);
            }
        }
    },
    
    _createUsrFromEmailAndReturnKSuccessCallback: function Js_Controls_MultiBuddyChooser_CreateUserFromEmailController$_createUsrFromEmailAndReturnKSuccessCallback(result, userContext, methodName) {
        /// <param name="result" type="Number" integer="true">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        var pair = userContext;
        pair.value = result.toString();
        this._selector.set_value(pair.value);
        if (this._oldItemChosen != null) {
            this._oldItemChosen(pair);
        }
    },
    
    _checkForEmailAddress: function Js_Controls_MultiBuddyChooser_CreateUserFromEmailController$_checkForEmailAddress() {
        Js.Library.Trace.write('CheckingForEmailAddress');
        var regex = new RegExp('^[^\\@\\s]+\\@[A-Za-z0-9\\-]{1}[.A-Za-z0-9\\-]+\\.[.A-Za-z0-9\\-]*[A-Za-z0-9]$');
        var email = this._selector.get_text().trim();
        if (regex.test(email)) {
            var suggestion = new Js.AutoCompleteLibrary.Suggestion();
            suggestion.html = Js.Controls.MultiBuddyChooser.CreateUserFromEmailController._getPicTitleDetailTemplateHtml('/gfx/icon40-inbox.png', 'Add ' + this._selector.get_text().trim() + ' as a buddy', 'When they join DontStayIn they will be added as one of your buddies.  If you type a name after the email address you can use that in future to find this person.');
            suggestion.text = this._selector.get_text();
            suggestion.value = "{'email': '" + escape(this._selector.get_text()) + "'}";
            suggestion.priority = this._selector.get_text().length * 100;
            for (var i = 0; i < this._selector.suggestions.get_count(); i++) {
                if (this._selector.suggestions.get_item(i).value.startsWith("{'email': ")) {
                    this._selector.suggestions.removeAt(i);
                    break;
                }
            }
            this._selector.addSuggestion(suggestion);
            this._selector.displaySuggestionsInPopupMenu();
        }
        if (this._oldOnSuggestionsRequested != null) {
            this._oldOnSuggestionsRequested();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController

Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController = function Js_Controls_MultiBuddyChooser_CreateUsersFromEmailsController(selector) {
    /// <param name="selector" type="Js.ClientControls.HtmlAutoCompleteBehaviour">
    /// </param>
    /// <field name="_oldOnSuggestionsRequested" type="Function">
    /// </field>
    /// <field name="_selector" type="Js.ClientControls.HtmlAutoCompleteBehaviour">
    /// </field>
    /// <field name="_emailsRegex" type="String" static="true">
    /// </field>
    this._selector = selector;
    this._oldOnSuggestionsRequested = selector.onSuggestionsRequested;
    selector.onSuggestionsRequested = ss.Delegate.create(this, this._checkForEmailAddresses);
}
Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController._getPicTitleDetailTemplateHtml = function Js_Controls_MultiBuddyChooser_CreateUsersFromEmailsController$_getPicTitleDetailTemplateHtml(imageSrc, title, detail) {
    /// <param name="imageSrc" type="String">
    /// </param>
    /// <param name="title" type="String">
    /// </param>
    /// <param name="detail" type="String">
    /// </param>
    /// <returns type="String"></returns>
    return String.format("<table cellspacing='0' cellpadding='0'><tr><td width='40'><img src='{0}' border=0 width=40 hspace=0 height=40 /></td><td style='padding:3px;' valign='top'><b>{1}</b><br />{2}</td></tr></table>", imageSrc, title, detail);
}
Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController.prototype = {
    _oldOnSuggestionsRequested: null,
    _selector: null,
    
    _checkForEmailAddresses: function Js_Controls_MultiBuddyChooser_CreateUsersFromEmailsController$_checkForEmailAddresses() {
        Js.Library.Trace.write('CheckingForEmailAddresses');
        var regExp = new RegExp('\\s|,|;| +', 'g');
        var text = this._selector.get_text().replace(regExp, ' ');
        while (text.indexOf('  ') > -1) {
            text = text.replaceAll('  ', ' ');
        }
        text = text.trim();
        for (var i = this._selector.suggestions.get_count() - 1; i > -1; i--) {
            Js.Library.Trace.write(this._selector.suggestions.get_item(i).value);
            if (this._selector.suggestions.get_item(i).value.startsWith("{'emails': ")) {
                this._selector.suggestions.removeAt(i);
                break;
            }
        }
        var regex = new RegExp('^( *([^\\@\\s]+\\@[A-Za-z0-9\\-]{1}[.A-Za-z0-9\\-]+\\.[.A-Za-z0-9\\-]*[A-Za-z0-9]) *){2,}$');
        if (regex.test(text)) {
            this._selector.set_text(text);
            this._selector.suggestions.clear();
            var emails = text.split(' ');
            var suggestion = new Js.AutoCompleteLibrary.Suggestion();
            suggestion.html = Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController._getPicTitleDetailTemplateHtml('/gfx/icon40-inbox.png', 'Add ' + emails.length + ' email addresses as buddies', 'Next time you want to include these email addresses, just add all your buddies and they will be included.');
            suggestion.text = emails.length + ' email addresses as buddies';
            suggestion.value = "{'emails': '" + escape(text) + "', 'buddies':'true'}";
            suggestion.priority = this._selector.get_text().length * 100;
            this._selector.addSuggestion(suggestion);
            var suggestion2 = new Js.AutoCompleteLibrary.Suggestion();
            suggestion2.html = Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController._getPicTitleDetailTemplateHtml('/gfx/icon40-inbox.png', 'Add ' + emails.length + ' email addresses, but NOT as buddies', "Next time you want to include these email addresses you'll have to copy them in again");
            suggestion2.text = emails.length + ' email addresses';
            suggestion2.value = "{'emails': '" + escape(text) + "', 'buddies':'false'}";
            suggestion2.priority = this._selector.get_text().length * 100;
            this._selector.addSuggestion(suggestion2);
            this._selector.displaySuggestionsInPopupMenu();
        }
        if (this._oldOnSuggestionsRequested != null) {
            this._oldOnSuggestionsRequested();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.EmailsSuggestionValue

Js.Controls.MultiBuddyChooser.EmailsSuggestionValue = function Js_Controls_MultiBuddyChooser_EmailsSuggestionValue() {
    /// <field name="emails" type="String">
    /// </field>
    /// <field name="buddies" type="Boolean">
    /// </field>
}
Js.Controls.MultiBuddyChooser.EmailsSuggestionValue.prototype = {
    emails: null,
    buddies: false
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.EmailSuggestionValue

Js.Controls.MultiBuddyChooser.EmailSuggestionValue = function Js_Controls_MultiBuddyChooser_EmailSuggestionValue() {
    /// <field name="email" type="String">
    /// </field>
}
Js.Controls.MultiBuddyChooser.EmailSuggestionValue.prototype = {
    email: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult

Js.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult = function Js_Controls_MultiBuddyChooser_GetMusicTypesAndPlacesResult() {
    /// <field name="places" type="Array" elementType="Pair">
    /// </field>
    /// <field name="musicTypes" type="Array" elementType="Pair">
    /// </field>
}
Js.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult.prototype = {
    places: null,
    musicTypes: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.Pair

Js.Controls.MultiBuddyChooser.Pair = function Js_Controls_MultiBuddyChooser_Pair() {
    /// <field name="key" type="String">
    /// </field>
    /// <field name="value" type="String">
    /// </field>
}
Js.Controls.MultiBuddyChooser.Pair.prototype = {
    key: null,
    value: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.Controller

Js.Controls.MultiBuddyChooser.Controller = function Js_Controls_MultiBuddyChooser_Controller(view) {
    /// <param name="view" type="Js.Controls.MultiBuddyChooser.View">
    /// </param>
    /// <field name="_contextPlaces" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_contextMusicTypes" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_allPlaces" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_allMusicTypes" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_view" type="Js.Controls.MultiBuddyChooser.View">
    /// </field>
    /// <field name="_createUserFromEmailBehaviour" type="Js.Controls.MultiBuddyChooser.CreateUserFromEmailController">
    /// </field>
    /// <field name="_createUsersFromEmailsBehaviour" type="Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController">
    /// </field>
    /// <field name="_oldItemRemoved" type="Js.ClientControls.Function">
    /// </field>
    /// <field name="_buddyListLoaded" type="Boolean">
    /// </field>
    /// <field name="_clickedOptions" type="Object">
    /// </field>
    this._contextPlaces = [];
    this._contextMusicTypes = [];
    this._allPlaces = [];
    this._allMusicTypes = [];
    this._clickedOptions = {};
    this._view = view;
    view.get_uiAddByMusicAndPlaceJ().click(ss.Delegate.create(this, this._addByMusicAndPlaceButtonClick));
    view.get_uiAddAllButtonJ().click(ss.Delegate.create(this, this._addAllButtonClick));
    view.get_uiShowAllTownsAndMusicJ().click(ss.Delegate.create(this, this._showAllTownsAndMusicCheckBoxClick));
    view.get_uiShowAddAllJ().click(ss.Delegate.create(this, this._showAddAll));
    view.get_uiShowAddByJ().click(ss.Delegate.create(this, this._showAddBy));
    view.get_uiShowBuddyListJ().click(ss.Delegate.create(this, this._showBuddyList));
    view.get_uiJustBuddiesRadioJ().click(ss.Delegate.create(this, this._autoCompleteQueryGroupClick));
    view.get_uiAllMembersRadioJ().click(ss.Delegate.create(this, this._autoCompleteQueryGroupClick));
    this._copyValuesFromSelectListToArray(this._view.get_uiMusicTypes(), this._contextMusicTypes);
    this._copyValuesFromSelectListToArray(this._view.get_uiPlaces(), this._contextPlaces);
    this._createUserFromEmailBehaviour = new Js.Controls.MultiBuddyChooser.CreateUserFromEmailController(this._view.get_uiBuddyMultiSelector().htmlAutoComplete);
    this._createUsersFromEmailsBehaviour = new Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController(this._view.get_uiBuddyMultiSelector().htmlAutoComplete);
    this._oldItemRemoved = this._view.get_uiBuddyMultiSelector().itemRemoved;
    this._view.get_uiBuddyMultiSelector().itemRemoved = ss.Delegate.create(this, this._onMultiSelectorItemRemoved);
}
Js.Controls.MultiBuddyChooser.Controller._clearSelect = function Js_Controls_MultiBuddyChooser_Controller$_clearSelect(el) {
    /// <param name="el" type="Object" domElement="true">
    /// </param>
    while (el.options.length > 0) {
        el.removeChild(el.options[el.options.length - 1]);
    }
}
Js.Controls.MultiBuddyChooser.Controller.prototype = {
    
    get_selectedValues: function Js_Controls_MultiBuddyChooser_Controller$get_selectedValues() {
        /// <value type="Array" elementType="String"></value>
        var selections = this._view.get_uiBuddyMultiSelector().getSelections().toArray();
        var selectedValues = new Array(selections.length);
        for (var i = 0; i < selections.length; i++) {
            selectedValues[i] = (selections[i])[1];
        }
        return selectedValues;
    },
    
    _view: null,
    _createUserFromEmailBehaviour: null,
    _createUsersFromEmailsBehaviour: null,
    
    _autoCompleteQueryGroupClick: function Js_Controls_MultiBuddyChooser_Controller$_autoCompleteQueryGroupClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (this._view.get_uiJustBuddiesRadio().checked) {
            this._view.get_uiBuddyMultiSelector().htmlAutoComplete.setWebMethod('GetBuddies');
        }
        else {
            this._view.get_uiBuddyMultiSelector().htmlAutoComplete.setWebMethod('GetBuddiesThenUsrs');
        }
    },
    
    _onMultiSelectorItemRemoved: function Js_Controls_MultiBuddyChooser_Controller$_onMultiSelectorItemRemoved(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        var optionText = this._clickedOptions[value];
        if (optionText != null) {
            delete this._clickedOptions[value];
            var option = document.createElement('OPTION');
            option.innerHTML = optionText;
            option.value = value;
            var i = 0;
            if (!this._view.get_uiBuddyList().childNodes.length) {
                this._view.get_uiBuddyList().appendChild(option);
            }
            else {
                if (this._view.get_uiBuddyList().childNodes.length < 150) {
                    while (this._view.get_uiBuddyList().childNodes[i].innerHTML.localeCompare(optionText) < 0 && i < this._view.get_uiBuddyList().childNodes.length - 1) {
                        i++;
                    }
                }
                this._view.get_uiBuddyList().insertBefore(option, this._view.get_uiBuddyList().childNodes[i]);
            }
        }
        if (this._oldItemRemoved != null) {
            this._oldItemRemoved(key, value);
        }
    },
    
    _oldItemRemoved: null,
    _buddyListLoaded: false,
    
    _showBuddyList: function Js_Controls_MultiBuddyChooser_Controller$_showBuddyList(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (this._view.get_uiShowBuddyList().checked && !this._buddyListLoaded) {
            Js.Controls.MultiBuddyChooser.Service.getBuddiesSelectListHtml(ss.Delegate.create(this, this._getBuddiesCallback), Js.Library.Trace.webServiceFailure, null, 0);
            this._setBuddyListInnerHTML("<OPTION value='-1'>Loading...</OPTION>");
        }
        this._view.get_uiBuddyListPanel().style.display = (this._view.get_uiShowBuddyList().checked) ? '' : 'none';
    },
    
    _getBuddiesCallback: function Js_Controls_MultiBuddyChooser_Controller$_getBuddiesCallback(result, userContext, methodName) {
        /// <param name="result" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._view.get_uiBuddyListJ().unbind('click').unbind('keydown');
        this._setBuddyListInnerHTML(result);
        this._view.get_uiBuddyListJ().click(ss.Delegate.create(this, this._buddyListClicked));
        this._view.get_uiBuddyListJ().keydown(ss.Delegate.create(this, this._buddyListKeyPressed));
        this._clickedOptions = {};
        this._buddyListLoaded = true;
        this._view.get_uiBuddyListPanel().style.display = '';
        this._view.get_uiBuddyList().focus();
        if (this._view.get_uiBuddyList().childNodes.length > 0) {
            (this._view.get_uiBuddyList().childNodes[0]).selected = true;
        }
    },
    
    _buddyListKeyPressed: function Js_Controls_MultiBuddyChooser_Controller$_buddyListKeyPressed(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (e.which === 32 || e.which === 13) {
            this._view.get_uiBuddyList().click();
        }
    },
    
    _setBuddyListInnerHTML: function Js_Controls_MultiBuddyChooser_Controller$_setBuddyListInnerHTML(innerHTML) {
        /// <param name="innerHTML" type="String">
        /// </param>
        if (ie) {
            this._view.get_uiBuddyList().innerHTML = '';
            var selectHTML = this._view.get_uiBuddyListPanel().innerHTML;
            this._view.get_uiBuddyListPanel().innerHTML = selectHTML.substring(0, selectHTML.indexOf('</SELECT>')) + innerHTML + '</SELECT>';
        }
        else {
            this._view.get_uiBuddyList().innerHTML = innerHTML;
        }
    },
    
    _buddyListClicked: function Js_Controls_MultiBuddyChooser_Controller$_buddyListClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var selectedIndex = this._view.get_uiBuddyList().selectedIndex;
        if (selectedIndex > -1) {
            var scrollTop = this._view.get_uiBuddyList().scrollTop;
            var option = this._view.get_uiBuddyList().options[selectedIndex];
            this._clickedOptions[option.value] = option.innerHTML;
            this._view.get_uiBuddyMultiSelector().addItem(option.innerHTML, option.value);
            this._view.get_uiBuddyList().selectedIndex = -1;
            this._view.get_uiBuddyList().remove(selectedIndex);
            this._view.get_uiBuddyList().scrollTop = scrollTop;
        }
        if (this._view.get_uiBuddyList().childNodes.length > selectedIndex && selectedIndex !== -1) {
            (this._view.get_uiBuddyList().childNodes[selectedIndex]).selected = true;
        }
        this._view.get_uiBuddyList().focus();
    },
    
    _showAddAll: function Js_Controls_MultiBuddyChooser_Controller$_showAddAll(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._view.get_uiAddAll().style.display = (this._view.get_uiShowAddAll().checked) ? '' : 'none';
    },
    
    _showAddBy: function Js_Controls_MultiBuddyChooser_Controller$_showAddBy(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._view.get_uiAddBy().style.display = (this._view.get_uiShowAddBy().checked) ? '' : 'none';
    },
    
    _copyValuesFromSelectListToArray: function Js_Controls_MultiBuddyChooser_Controller$_copyValuesFromSelectListToArray(el, options) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <param name="options" type="Array" elementType="Object" elementDomElement="true">
        /// </param>
        for (var i = 0; i < el.options.length; i++) {
            options[options.length] = el.options[i];
        }
    },
    
    _addAllButtonClick: function Js_Controls_MultiBuddyChooser_Controller$_addAllButtonClick(ev) {
        /// <param name="ev" type="jQueryEvent">
        /// </param>
        this._addByGeneric(ev, true);
    },
    
    _addByMusicAndPlaceButtonClick: function Js_Controls_MultiBuddyChooser_Controller$_addByMusicAndPlaceButtonClick(ev) {
        /// <param name="ev" type="jQueryEvent">
        /// </param>
        this._addByGeneric(ev, false);
    },
    
    _addByGeneric: function Js_Controls_MultiBuddyChooser_Controller$_addByGeneric(ev, addAll) {
        /// <param name="ev" type="jQueryEvent">
        /// </param>
        /// <param name="addAll" type="Boolean">
        /// </param>
        var text = 'All buddies';
        var value = '';
        if (!addAll) {
            text += (this._view.get_uiPlaces().value === '-1') ? '' : ' who visit ' + this._view.get_uiPlaces().options[this._view.get_uiPlaces().selectedIndex].innerHTML;
            text += (this._view.get_uiMusicTypes().value === '1') ? '' : (((this._view.get_uiPlaces().value === '-1') ? ' who' : ' and') + ' listen to ' + this._view.get_uiMusicTypes().options[this._view.get_uiMusicTypes().selectedIndex].innerHTML.trim());
            value = "{'MusicTypeK' : '" + this._view.get_uiMusicTypes().value + "','PlaceK' : '" + this._view.get_uiPlaces().value + "'}";
        }
        else {
            value = "{'MusicTypeK' : '1','PlaceK' : '-1'}";
        }
        var id = 'expandClicker' + Math.floor(Math.random() * 10000000);
        this._view.get_uiBuddyMultiSelector().addItem(text + ' - <a href="" id="' + id + "\" class=\"MultiSelectorExpandButton\" onmouseover=\"stt('Expand this to show buddies (might take a while)');\" onmouseout=\"htm();\">show</a>", value);
        $(document.getElementById(id)).click(ss.Delegate.create(this, function(e) {
            try{htm();}catch(e){};
            e.preventDefault();
            Js.Controls.MultiBuddyChooser.Service.resolveUsrsFromMultiBuddyChooserValues([ value ], ss.Delegate.create(this, function(result, context, methodName) {
                try {
                    this._view.get_uiBuddyMultiSelector().removeItem(document.getElementById(id).parentNode.parentNode);
                }
                catch ($e1) {
                }
                var $enum2 = ss.IEnumerator.getEnumerator(Object.keys(result));
                while ($enum2.moveNext()) {
                    var key = $enum2.current;
                    this._view.get_uiBuddyMultiSelector().addItem(key, result[key]);
                }
            }), Js.Library.Trace.webServiceFailure, null, 30000);
        }));
        ev.preventDefault();
    },
    
    _fillOptionArrayFromValues: function Js_Controls_MultiBuddyChooser_Controller$_fillOptionArrayFromValues(options, array) {
        /// <param name="options" type="Array" elementType="Pair">
        /// </param>
        /// <param name="array" type="Array" elementType="Object" elementDomElement="true">
        /// </param>
        for (var i = 0; i < options.length; i++) {
            var de = options[i];
            var el = document.createElement('OPTION');
            el.innerHTML = unescape(de.key).replaceAll('&', '&amp;').replaceAll(' ', '&nbsp;');
            el.value = de.value;
            array[array.length] = el;
        }
    },
    
    _showAllTownsAndMusicCheckBoxClick: function Js_Controls_MultiBuddyChooser_Controller$_showAllTownsAndMusicCheckBoxClick(ev) {
        /// <param name="ev" type="jQueryEvent">
        /// </param>
        if (!this._allPlaces.length) {
            Js.Controls.MultiBuddyChooser.Service.getPlacesAndMusicTypes(ss.Delegate.create(this, function(result, userContext, methodName) {
                if (!this._allPlaces.length) {
                    this._fillOptionArrayFromValues(result.musicTypes, this._allMusicTypes);
                    this._fillOptionArrayFromValues(result.places, this._allPlaces);
                }
                this._fillSelect(this._view.get_uiMusicTypes(), (this._view.get_uiShowAllTownsAndMusic().checked) ? this._allMusicTypes : this._contextMusicTypes);
                this._fillSelect(this._view.get_uiPlaces(), (this._view.get_uiShowAllTownsAndMusic().checked) ? this._allPlaces : this._contextPlaces);
            }), Js.Library.Trace.webServiceFailure, null, 30000);
            return;
        }
        else {
            this._fillSelect(this._view.get_uiMusicTypes(), (this._view.get_uiShowAllTownsAndMusic().checked) ? this._allMusicTypes : this._contextMusicTypes);
            this._fillSelect(this._view.get_uiPlaces(), (this._view.get_uiShowAllTownsAndMusic().checked) ? this._allPlaces : this._contextPlaces);
        }
    },
    
    _fillSelect: function Js_Controls_MultiBuddyChooser_Controller$_fillSelect(el, options) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <param name="options" type="Array" elementType="Object" elementDomElement="true">
        /// </param>
        Js.Controls.MultiBuddyChooser.Controller._clearSelect(el);
        for (var i = 0; i < options.length; i++) {
            el.appendChild(options[i]);
        }
    },
    
    clear: function Js_Controls_MultiBuddyChooser_Controller$clear() {
        try {
            this._view.get_uiBuddyMultiSelector().clear();
        }
        catch ($e1) {
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.Service

Js.Controls.MultiBuddyChooser.Service = function Js_Controls_MultiBuddyChooser_Service() {
}
Js.Controls.MultiBuddyChooser.Service.getPlacesAndMusicTypes = function Js_Controls_MultiBuddyChooser_Service$getPlacesAndMusicTypes(success, failure, userContext, timeout) {
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    var o = Js.Library.WebServiceHelper.options('GetPlacesAndMusicTypes', '/WebServices/Controls/MultiBuddyChooser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPlacesAndMusicTypes');
    };
    $.ajax(o);
}
Js.Controls.MultiBuddyChooser.Service.resolveUsrsFromMultiBuddyChooserValues = function Js_Controls_MultiBuddyChooser_Service$resolveUsrsFromMultiBuddyChooserValues(values, success, failure, userContext, timeout) {
    /// <param name="values" type="Array" elementType="String">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['values'] = values;
    var o = Js.Library.WebServiceHelper.options('ResolveUsrsFromMultiBuddyChooserValues', '/WebServices/Controls/MultiBuddyChooser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'ResolveUsrsFromMultiBuddyChooserValues');
    };
    $.ajax(o);
}
Js.Controls.MultiBuddyChooser.Service.getBuddiesSelectListHtml = function Js_Controls_MultiBuddyChooser_Service$getBuddiesSelectListHtml(success, failure, userContext, timeout) {
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    var o = Js.Library.WebServiceHelper.options('GetBuddiesSelectListHtml', '/WebServices/Controls/MultiBuddyChooser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetBuddiesSelectListHtml');
    };
    $.ajax(o);
}
Js.Controls.MultiBuddyChooser.Service.createUsrFromEmailAndReturnK = function Js_Controls_MultiBuddyChooser_Service$createUsrFromEmailAndReturnK(textEnteredByUser, success, failure, userContext, timeout) {
    /// <param name="textEnteredByUser" type="String">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['textEnteredByUser'] = textEnteredByUser;
    var o = Js.Library.WebServiceHelper.options('CreateUsrFromEmailAndReturnK', '/WebServices/Controls/MultiBuddyChooser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'CreateUsrFromEmailAndReturnK');
    };
    $.ajax(o);
}
Js.Controls.MultiBuddyChooser.Service.createUsrsFromEmails = function Js_Controls_MultiBuddyChooser_Service$createUsrsFromEmails(spaceSeparatedListOfEmailAddresses, addAsBuddies, success, failure, userContext, timeout) {
    /// <param name="spaceSeparatedListOfEmailAddresses" type="String">
    /// </param>
    /// <param name="addAsBuddies" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['spaceSeparatedListOfEmailAddresses'] = spaceSeparatedListOfEmailAddresses;
    p['addAsBuddies'] = addAsBuddies;
    var o = Js.Library.WebServiceHelper.options('CreateUsrsFromEmails', '/WebServices/Controls/MultiBuddyChooser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'CreateUsrsFromEmails');
    };
    $.ajax(o);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.View

Js.Controls.MultiBuddyChooser.View = function Js_Controls_MultiBuddyChooser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiJustBuddiesRadio" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiJustBuddiesRadioJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAllMembersRadio" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAllMembersRadioJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiShowBuddyList" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiShowBuddyListJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBuddyListPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBuddyListPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBuddyList" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBuddyListJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiShowAddAll" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiShowAddAllJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddAll" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddAllJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddAllButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddAllButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiShowAddBy" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiShowAddByJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddBy" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddByJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPlaces" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPlacesJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiMusicTypes" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiMusicTypesJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddByMusicAndPlace" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddByMusicAndPlaceJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiShowAllTownsAndMusic" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiShowAllTownsAndMusicJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.MultiBuddyChooser.View.prototype = {
    clientId: null,
    
    get_uiBuddyMultiSelector: function Js_Controls_MultiBuddyChooser_View$get_uiBuddyMultiSelector() {
        /// <value type="Js.ClientControls.MultiSelectorBehaviour"></value>
        return eval(this.clientId + '_uiBuddyMultiSelectorBehaviour');
    },
    
    get_uiJustBuddiesRadio: function Js_Controls_MultiBuddyChooser_View$get_uiJustBuddiesRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiJustBuddiesRadio == null) {
            this._uiJustBuddiesRadio = document.getElementById(this.clientId + '_uiJustBuddiesRadio');
        }
        return this._uiJustBuddiesRadio;
    },
    
    _uiJustBuddiesRadio: null,
    
    get_uiJustBuddiesRadioJ: function Js_Controls_MultiBuddyChooser_View$get_uiJustBuddiesRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiJustBuddiesRadioJ == null) {
            this._uiJustBuddiesRadioJ = $('#' + this.clientId + '_uiJustBuddiesRadio');
        }
        return this._uiJustBuddiesRadioJ;
    },
    
    _uiJustBuddiesRadioJ: null,
    
    get_uiAllMembersRadio: function Js_Controls_MultiBuddyChooser_View$get_uiAllMembersRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAllMembersRadio == null) {
            this._uiAllMembersRadio = document.getElementById(this.clientId + '_uiAllMembersRadio');
        }
        return this._uiAllMembersRadio;
    },
    
    _uiAllMembersRadio: null,
    
    get_uiAllMembersRadioJ: function Js_Controls_MultiBuddyChooser_View$get_uiAllMembersRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAllMembersRadioJ == null) {
            this._uiAllMembersRadioJ = $('#' + this.clientId + '_uiAllMembersRadio');
        }
        return this._uiAllMembersRadioJ;
    },
    
    _uiAllMembersRadioJ: null,
    
    get_uiShowBuddyList: function Js_Controls_MultiBuddyChooser_View$get_uiShowBuddyList() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiShowBuddyList == null) {
            this._uiShowBuddyList = document.getElementById(this.clientId + '_uiShowBuddyList');
        }
        return this._uiShowBuddyList;
    },
    
    _uiShowBuddyList: null,
    
    get_uiShowBuddyListJ: function Js_Controls_MultiBuddyChooser_View$get_uiShowBuddyListJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiShowBuddyListJ == null) {
            this._uiShowBuddyListJ = $('#' + this.clientId + '_uiShowBuddyList');
        }
        return this._uiShowBuddyListJ;
    },
    
    _uiShowBuddyListJ: null,
    
    get_uiBuddyListPanel: function Js_Controls_MultiBuddyChooser_View$get_uiBuddyListPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBuddyListPanel == null) {
            this._uiBuddyListPanel = document.getElementById(this.clientId + '_uiBuddyListPanel');
        }
        return this._uiBuddyListPanel;
    },
    
    _uiBuddyListPanel: null,
    
    get_uiBuddyListPanelJ: function Js_Controls_MultiBuddyChooser_View$get_uiBuddyListPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBuddyListPanelJ == null) {
            this._uiBuddyListPanelJ = $('#' + this.clientId + '_uiBuddyListPanel');
        }
        return this._uiBuddyListPanelJ;
    },
    
    _uiBuddyListPanelJ: null,
    
    get_uiBuddyList: function Js_Controls_MultiBuddyChooser_View$get_uiBuddyList() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBuddyList == null) {
            this._uiBuddyList = document.getElementById(this.clientId + '_uiBuddyList');
        }
        return this._uiBuddyList;
    },
    
    _uiBuddyList: null,
    
    get_uiBuddyListJ: function Js_Controls_MultiBuddyChooser_View$get_uiBuddyListJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBuddyListJ == null) {
            this._uiBuddyListJ = $('#' + this.clientId + '_uiBuddyList');
        }
        return this._uiBuddyListJ;
    },
    
    _uiBuddyListJ: null,
    
    get_uiShowAddAll: function Js_Controls_MultiBuddyChooser_View$get_uiShowAddAll() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiShowAddAll == null) {
            this._uiShowAddAll = document.getElementById(this.clientId + '_uiShowAddAll');
        }
        return this._uiShowAddAll;
    },
    
    _uiShowAddAll: null,
    
    get_uiShowAddAllJ: function Js_Controls_MultiBuddyChooser_View$get_uiShowAddAllJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiShowAddAllJ == null) {
            this._uiShowAddAllJ = $('#' + this.clientId + '_uiShowAddAll');
        }
        return this._uiShowAddAllJ;
    },
    
    _uiShowAddAllJ: null,
    
    get_uiAddAll: function Js_Controls_MultiBuddyChooser_View$get_uiAddAll() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddAll == null) {
            this._uiAddAll = document.getElementById(this.clientId + '_uiAddAll');
        }
        return this._uiAddAll;
    },
    
    _uiAddAll: null,
    
    get_uiAddAllJ: function Js_Controls_MultiBuddyChooser_View$get_uiAddAllJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddAllJ == null) {
            this._uiAddAllJ = $('#' + this.clientId + '_uiAddAll');
        }
        return this._uiAddAllJ;
    },
    
    _uiAddAllJ: null,
    
    get_uiAddAllButton: function Js_Controls_MultiBuddyChooser_View$get_uiAddAllButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddAllButton == null) {
            this._uiAddAllButton = document.getElementById(this.clientId + '_uiAddAllButton');
        }
        return this._uiAddAllButton;
    },
    
    _uiAddAllButton: null,
    
    get_uiAddAllButtonJ: function Js_Controls_MultiBuddyChooser_View$get_uiAddAllButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddAllButtonJ == null) {
            this._uiAddAllButtonJ = $('#' + this.clientId + '_uiAddAllButton');
        }
        return this._uiAddAllButtonJ;
    },
    
    _uiAddAllButtonJ: null,
    
    get_uiShowAddBy: function Js_Controls_MultiBuddyChooser_View$get_uiShowAddBy() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiShowAddBy == null) {
            this._uiShowAddBy = document.getElementById(this.clientId + '_uiShowAddBy');
        }
        return this._uiShowAddBy;
    },
    
    _uiShowAddBy: null,
    
    get_uiShowAddByJ: function Js_Controls_MultiBuddyChooser_View$get_uiShowAddByJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiShowAddByJ == null) {
            this._uiShowAddByJ = $('#' + this.clientId + '_uiShowAddBy');
        }
        return this._uiShowAddByJ;
    },
    
    _uiShowAddByJ: null,
    
    get_uiAddBy: function Js_Controls_MultiBuddyChooser_View$get_uiAddBy() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddBy == null) {
            this._uiAddBy = document.getElementById(this.clientId + '_uiAddBy');
        }
        return this._uiAddBy;
    },
    
    _uiAddBy: null,
    
    get_uiAddByJ: function Js_Controls_MultiBuddyChooser_View$get_uiAddByJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddByJ == null) {
            this._uiAddByJ = $('#' + this.clientId + '_uiAddBy');
        }
        return this._uiAddByJ;
    },
    
    _uiAddByJ: null,
    
    get_uiPlaces: function Js_Controls_MultiBuddyChooser_View$get_uiPlaces() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPlaces == null) {
            this._uiPlaces = document.getElementById(this.clientId + '_uiPlaces');
        }
        return this._uiPlaces;
    },
    
    _uiPlaces: null,
    
    get_uiPlacesJ: function Js_Controls_MultiBuddyChooser_View$get_uiPlacesJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPlacesJ == null) {
            this._uiPlacesJ = $('#' + this.clientId + '_uiPlaces');
        }
        return this._uiPlacesJ;
    },
    
    _uiPlacesJ: null,
    
    get_uiMusicTypes: function Js_Controls_MultiBuddyChooser_View$get_uiMusicTypes() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiMusicTypes == null) {
            this._uiMusicTypes = document.getElementById(this.clientId + '_uiMusicTypes');
        }
        return this._uiMusicTypes;
    },
    
    _uiMusicTypes: null,
    
    get_uiMusicTypesJ: function Js_Controls_MultiBuddyChooser_View$get_uiMusicTypesJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiMusicTypesJ == null) {
            this._uiMusicTypesJ = $('#' + this.clientId + '_uiMusicTypes');
        }
        return this._uiMusicTypesJ;
    },
    
    _uiMusicTypesJ: null,
    
    get_uiAddByMusicAndPlace: function Js_Controls_MultiBuddyChooser_View$get_uiAddByMusicAndPlace() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddByMusicAndPlace == null) {
            this._uiAddByMusicAndPlace = document.getElementById(this.clientId + '_uiAddByMusicAndPlace');
        }
        return this._uiAddByMusicAndPlace;
    },
    
    _uiAddByMusicAndPlace: null,
    
    get_uiAddByMusicAndPlaceJ: function Js_Controls_MultiBuddyChooser_View$get_uiAddByMusicAndPlaceJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddByMusicAndPlaceJ == null) {
            this._uiAddByMusicAndPlaceJ = $('#' + this.clientId + '_uiAddByMusicAndPlace');
        }
        return this._uiAddByMusicAndPlaceJ;
    },
    
    _uiAddByMusicAndPlaceJ: null,
    
    get_uiShowAllTownsAndMusic: function Js_Controls_MultiBuddyChooser_View$get_uiShowAllTownsAndMusic() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiShowAllTownsAndMusic == null) {
            this._uiShowAllTownsAndMusic = document.getElementById(this.clientId + '_uiShowAllTownsAndMusic');
        }
        return this._uiShowAllTownsAndMusic;
    },
    
    _uiShowAllTownsAndMusic: null,
    
    get_uiShowAllTownsAndMusicJ: function Js_Controls_MultiBuddyChooser_View$get_uiShowAllTownsAndMusicJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiShowAllTownsAndMusicJ == null) {
            this._uiShowAllTownsAndMusicJ = $('#' + this.clientId + '_uiShowAllTownsAndMusic');
        }
        return this._uiShowAllTownsAndMusicJ;
    },
    
    _uiShowAllTownsAndMusicJ: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK

Js.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK = function Js_Controls_MultiBuddyChooser_MusicTypeKAndPlaceK() {
    /// <field name="musicTypeK" type="Number" integer="true">
    /// </field>
    /// <field name="placeK" type="Number" integer="true">
    /// </field>
}
Js.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK.prototype = {
    musicTypeK: 0,
    placeK: 0
}


Js.Controls.MultiBuddyChooser.CreateUserFromEmailController.registerClass('Js.Controls.MultiBuddyChooser.CreateUserFromEmailController');
Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController.registerClass('Js.Controls.MultiBuddyChooser.CreateUsersFromEmailsController');
Js.Controls.MultiBuddyChooser.EmailsSuggestionValue.registerClass('Js.Controls.MultiBuddyChooser.EmailsSuggestionValue');
Js.Controls.MultiBuddyChooser.EmailSuggestionValue.registerClass('Js.Controls.MultiBuddyChooser.EmailSuggestionValue');
Js.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult.registerClass('Js.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult');
Js.Controls.MultiBuddyChooser.Pair.registerClass('Js.Controls.MultiBuddyChooser.Pair');
Js.Controls.MultiBuddyChooser.Controller.registerClass('Js.Controls.MultiBuddyChooser.Controller');
Js.Controls.MultiBuddyChooser.Service.registerClass('Js.Controls.MultiBuddyChooser.Service');
Js.Controls.MultiBuddyChooser.View.registerClass('Js.Controls.MultiBuddyChooser.View');
Js.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK.registerClass('Js.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
