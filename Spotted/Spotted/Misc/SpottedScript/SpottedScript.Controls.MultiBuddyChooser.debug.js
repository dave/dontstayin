Type.registerNamespace('SpottedScript.Controls.MultiBuddyChooser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MultiBuddyChooser.Controller
SpottedScript.Controls.MultiBuddyChooser.Controller = function SpottedScript_Controls_MultiBuddyChooser_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.MultiBuddyChooser.View">
    /// </param>
    /// <field name="_contextPlaces" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_contextMusicTypes" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_allPlaces" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_allMusicTypes" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_view" type="SpottedScript.Controls.MultiBuddyChooser.View">
    /// </field>
    /// <field name="_createUserFromEmailBehaviour" type="SpottedScript.Behaviours.CreateUserFromEmail.Controller">
    /// </field>
    /// <field name="_createUsersFromEmailsBehaviour" type="SpottedScript.Behaviours.CreateUsersFromEmails.Controller">
    /// </field>
    /// <field name="_oldItemRemoved" type="ScriptSharpLibrary.ItemChangeDelegate">
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
    $addHandler(this._view.get_uiAddByMusicAndPlace(), 'click', Function.createDelegate(this, this._addByMusicAndPlaceButtonClick));
    $addHandler(this._view.get_uiAddAllButton(), 'click', Function.createDelegate(this, this._addAllButtonClick));
    $addHandler(this._view.get_uiShowAllTownsAndMusic(), 'click', Function.createDelegate(this, this._showAllTownsAndMusicCheckBoxClick));
    $addHandler(this._view.get_uiShowAddAll(), 'click', Function.createDelegate(this, this._showAddAll));
    $addHandler(this._view.get_uiShowAddBy(), 'click', Function.createDelegate(this, this._showAddBy));
    $addHandler(this._view.get_uiShowBuddyList(), 'click', Function.createDelegate(this, this._showBuddyList));
    $addHandler(this._view.get_uiJustBuddiesRadio(), 'click', Function.createDelegate(this, this._autoCompleteQueryGroupClick));
    $addHandler(this._view.get_uiAllMembersRadio(), 'click', Function.createDelegate(this, this._autoCompleteQueryGroupClick));
    this._copyValuesFromSelectListToArray(this._view.get_uiMusicTypes(), this._contextMusicTypes);
    this._copyValuesFromSelectListToArray(this._view.get_uiPlaces(), this._contextPlaces);
    this._createUserFromEmailBehaviour = new SpottedScript.Behaviours.CreateUserFromEmail.Controller(this._view.get_uiBuddyMultiSelector().htmlAutoComplete);
    this._createUsersFromEmailsBehaviour = new SpottedScript.Behaviours.CreateUsersFromEmails.Controller(this._view.get_uiBuddyMultiSelector().htmlAutoComplete);
    this._oldItemRemoved = this._view.get_uiBuddyMultiSelector().itemRemoved;
    this._view.get_uiBuddyMultiSelector().itemRemoved = Function.createDelegate(this, this._onMultiSelectorItemRemoved);
}
SpottedScript.Controls.MultiBuddyChooser.Controller._clearSelect = function SpottedScript_Controls_MultiBuddyChooser_Controller$_clearSelect(el) {
    /// <param name="el" type="Object" domElement="true">
    /// </param>
    while (el.options.length > 0) {
        el.removeChild(el.options[el.options.length - 1]);
    }
}
SpottedScript.Controls.MultiBuddyChooser.Controller.prototype = {
    get_selectedValues: function SpottedScript_Controls_MultiBuddyChooser_Controller$get_selectedValues() {
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
    _autoCompleteQueryGroupClick: function SpottedScript_Controls_MultiBuddyChooser_Controller$_autoCompleteQueryGroupClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this._view.get_uiJustBuddiesRadio().checked) {
            this._view.get_uiBuddyMultiSelector().htmlAutoComplete.setWebMethod('GetBuddies');
        }
        else {
            this._view.get_uiBuddyMultiSelector().htmlAutoComplete.setWebMethod('GetBuddiesThenUsrs');
        }
    },
    _onMultiSelectorItemRemoved: function SpottedScript_Controls_MultiBuddyChooser_Controller$_onMultiSelectorItemRemoved(key, value) {
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
            if (this._view.get_uiBuddyList().childNodes.length === 0) {
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
    _showBuddyList: function SpottedScript_Controls_MultiBuddyChooser_Controller$_showBuddyList(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this._view.get_uiShowBuddyList().checked && !this._buddyListLoaded) {
            Spotted.WebServices.Controls.MultiBuddyChooser.Service.getBuddiesSelectListHtml(Function.createDelegate(this, this._getBuddiesCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, 0);
            this._setBuddyListInnerHTML('<OPTION value=\'-1\'>Loading...</OPTION>');
        }
        this._view.get_uiBuddyListPanel().style.display = (this._view.get_uiShowBuddyList().checked) ? '' : 'none';
    },
    _getBuddiesCallback: function SpottedScript_Controls_MultiBuddyChooser_Controller$_getBuddiesCallback(result, userContext, methodName) {
        /// <param name="result" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        $clearHandlers(this._view.get_uiBuddyList());
        this._setBuddyListInnerHTML(result);
        $addHandler(this._view.get_uiBuddyList(), 'click', Function.createDelegate(this, this._buddyListClicked));
        $addHandler(this._view.get_uiBuddyList(), 'keydown', Function.createDelegate(this, this._buddyListKeyPressed));
        this._clickedOptions = {};
        this._buddyListLoaded = true;
        this._view.get_uiBuddyListPanel().style.display = '';
        this._view.get_uiBuddyList().focus();
        if (this._view.get_uiBuddyList().childNodes.length > 0) {
            (this._view.get_uiBuddyList().childNodes[0]).selected = true;
        }
    },
    _buddyListKeyPressed: function SpottedScript_Controls_MultiBuddyChooser_Controller$_buddyListKeyPressed(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode === Sys.UI.Key.space || e.keyCode === Sys.UI.Key.enter) {
            this._view.get_uiBuddyList().click();
        }
    },
    _setBuddyListInnerHTML: function SpottedScript_Controls_MultiBuddyChooser_Controller$_setBuddyListInnerHTML(innerHTML) {
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
    _buddyListClicked: function SpottedScript_Controls_MultiBuddyChooser_Controller$_buddyListClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
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
    _showAddAll: function SpottedScript_Controls_MultiBuddyChooser_Controller$_showAddAll(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._view.get_uiAddAll().style.display = (this._view.get_uiShowAddAll().checked) ? '' : 'none';
    },
    _showAddBy: function SpottedScript_Controls_MultiBuddyChooser_Controller$_showAddBy(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._view.get_uiAddBy().style.display = (this._view.get_uiShowAddBy().checked) ? '' : 'none';
    },
    _copyValuesFromSelectListToArray: function SpottedScript_Controls_MultiBuddyChooser_Controller$_copyValuesFromSelectListToArray(el, options) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <param name="options" type="Array" elementType="Object" elementDomElement="true">
        /// </param>
        for (var i = 0; i < el.options.length; i++) {
            options[options.length] = el.options[i];
        }
    },
    _addAllButtonClick: function SpottedScript_Controls_MultiBuddyChooser_Controller$_addAllButtonClick(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        this._addByGeneric(ev, true);
    },
    _addByMusicAndPlaceButtonClick: function SpottedScript_Controls_MultiBuddyChooser_Controller$_addByMusicAndPlaceButtonClick(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        this._addByGeneric(ev, false);
    },
    _addByGeneric: function SpottedScript_Controls_MultiBuddyChooser_Controller$_addByGeneric(ev, addAll) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        /// <param name="addAll" type="Boolean">
        /// </param>
        var text = 'All buddies';
        var value = '';
        if (!addAll) {
            text += (this._view.get_uiPlaces().value === '-1') ? '' : ' who visit ' + this._view.get_uiPlaces().options[this._view.get_uiPlaces().selectedIndex].innerHTML;
            text += (this._view.get_uiMusicTypes().value === '1') ? '' : (((this._view.get_uiPlaces().value === '-1') ? ' who' : ' and') + ' listen to ' + this._view.get_uiMusicTypes().options[this._view.get_uiMusicTypes().selectedIndex].innerHTML.trim());
            value = '{\'MusicTypeK\' : \'' + this._view.get_uiMusicTypes().value + '\',\'PlaceK\' : \'' + this._view.get_uiPlaces().value + '\'}';
        }
        else {
            value = '{\'MusicTypeK\' : \'1\',\'PlaceK\' : \'-1\'}';
        }
        var id = 'expandClicker' + Math.floor(Math.random() * 10000000);
        this._view.get_uiBuddyMultiSelector().addItem(text + ' - <a href=\"\" id=\"' + id + '\" class=\"MultiSelectorExpandButton\" onmouseover=\"stt(\'Expand this to show buddies (might take a while)\');\" onmouseout=\"htm();\">show</a>', value);
        $addHandler(document.getElementById(id), 'click', Function.createDelegate(this, function(e) {
            try{htm();}catch(e){};
            e.preventDefault();
            Spotted.WebServices.Controls.MultiBuddyChooser.Service.resolveUsrsFromMultiBuddyChooserValues([ value ], Function.createDelegate(this, function(result, context, methodName) {
                try {
                    this._view.get_uiBuddyMultiSelector().removeItem(document.getElementById(id).parentNode.parentNode);
                }
                catch ($e1) {
                }
                var $dict2 = result;
                for (var $key3 in $dict2) {
                    var item = { key: $key3, value: $dict2[$key3] };
                    this._view.get_uiBuddyMultiSelector().addItem(item.key, item.value);
                }
            }), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, 30000);
        }));
        ev.preventDefault();
    },
    _fillOptionArrayFromValues: function SpottedScript_Controls_MultiBuddyChooser_Controller$_fillOptionArrayFromValues(options, array) {
        /// <param name="options" type="Array" elementType="Pair">
        /// </param>
        /// <param name="array" type="Array" elementType="Object" elementDomElement="true">
        /// </param>
        for (var i = 0; i < options.length; i++) {
            var de = options[i];
            var el = document.createElement('OPTION');
            el.innerHTML = unescape(de.key).replace('&', '&amp;').replace(' ', '&nbsp;');
            el.value = de.value;
            array[array.length] = el;
        }
    },
    _showAllTownsAndMusicCheckBoxClick: function SpottedScript_Controls_MultiBuddyChooser_Controller$_showAllTownsAndMusicCheckBoxClick(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        if (this._allPlaces.length === 0) {
            Spotted.WebServices.Controls.MultiBuddyChooser.Service.getPlacesAndMusicTypes(Function.createDelegate(this, function(result, userContext, methodName) {
                if (this._allPlaces.length === 0) {
                    this._fillOptionArrayFromValues(result.musicTypes, this._allMusicTypes);
                    this._fillOptionArrayFromValues(result.places, this._allPlaces);
                }
                this._fillSelect(this._view.get_uiMusicTypes(), (this._view.get_uiShowAllTownsAndMusic().checked) ? this._allMusicTypes : this._contextMusicTypes);
                this._fillSelect(this._view.get_uiPlaces(), (this._view.get_uiShowAllTownsAndMusic().checked) ? this._allPlaces : this._contextPlaces);
            }), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, 30000);
            return;
        }
        else {
            this._fillSelect(this._view.get_uiMusicTypes(), (this._view.get_uiShowAllTownsAndMusic().checked) ? this._allMusicTypes : this._contextMusicTypes);
            this._fillSelect(this._view.get_uiPlaces(), (this._view.get_uiShowAllTownsAndMusic().checked) ? this._allPlaces : this._contextPlaces);
        }
    },
    _fillSelect: function SpottedScript_Controls_MultiBuddyChooser_Controller$_fillSelect(el, options) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <param name="options" type="Array" elementType="Object" elementDomElement="true">
        /// </param>
        SpottedScript.Controls.MultiBuddyChooser.Controller._clearSelect(el);
        for (var i = 0; i < options.length; i++) {
            el.appendChild(options[i]);
        }
    },
    _clear: function SpottedScript_Controls_MultiBuddyChooser_Controller$_clear() {
        try {
            this._view.get_uiBuddyMultiSelector().clear();
        }
        catch ($e1) {
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK
SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK = function SpottedScript_Controls_MultiBuddyChooser_MusicTypeKAndPlaceK() {
    /// <field name="musicTypeK" type="Number" integer="true">
    /// </field>
    /// <field name="placeK" type="Number" integer="true">
    /// </field>
}
SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK.prototype = {
    musicTypeK: 0,
    placeK: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult
SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult = function SpottedScript_Controls_MultiBuddyChooser_GetMusicTypesAndPlacesResult() {
    /// <field name="places" type="Array" elementType="Pair">
    /// </field>
    /// <field name="musicTypes" type="Array" elementType="Pair">
    /// </field>
}
SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult.prototype = {
    places: null,
    musicTypes: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MultiBuddyChooser.Pair
SpottedScript.Controls.MultiBuddyChooser.Pair = function SpottedScript_Controls_MultiBuddyChooser_Pair() {
    /// <field name="key" type="String">
    /// </field>
    /// <field name="value" type="String">
    /// </field>
}
SpottedScript.Controls.MultiBuddyChooser.Pair.prototype = {
    key: null,
    value: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MultiBuddyChooser.View
SpottedScript.Controls.MultiBuddyChooser.View = function SpottedScript_Controls_MultiBuddyChooser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.MultiBuddyChooser.View.prototype = {
    clientId: null,
    get_uiBuddyMultiSelector: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiBuddyMultiSelector() {
        /// <value type="ScriptSharpLibrary.MultiSelectorBehaviour"></value>
        return eval(this.clientId + '_uiBuddyMultiSelectorBehaviour');
    },
    get_uiJustBuddiesRadio: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiJustBuddiesRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiJustBuddiesRadio');
    },
    get_uiAllMembersRadio: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiAllMembersRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAllMembersRadio');
    },
    get_uiShowBuddyList: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiShowBuddyList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiShowBuddyList');
    },
    get_uiBuddyListPanel: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiBuddyListPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyListPanel');
    },
    get_uiBuddyList: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiBuddyList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyList');
    },
    get_uiShowAddAll: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiShowAddAll() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiShowAddAll');
    },
    get_uiAddAll: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiAddAll() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddAll');
    },
    get_uiAddAllButton: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiAddAllButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddAllButton');
    },
    get_uiShowAddBy: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiShowAddBy() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiShowAddBy');
    },
    get_uiAddBy: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiAddBy() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddBy');
    },
    get_uiPlaces: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiPlaces() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPlaces');
    },
    get_uiMusicTypes: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiMusicTypes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMusicTypes');
    },
    get_uiAddByMusicAndPlace: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiAddByMusicAndPlace() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddByMusicAndPlace');
    },
    get_uiShowAllTownsAndMusic: function SpottedScript_Controls_MultiBuddyChooser_View$get_uiShowAllTownsAndMusic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiShowAllTownsAndMusic');
    }
}
SpottedScript.Controls.MultiBuddyChooser.Controller.registerClass('SpottedScript.Controls.MultiBuddyChooser.Controller');
SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK.registerClass('SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK');
SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult.registerClass('SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult');
SpottedScript.Controls.MultiBuddyChooser.Pair.registerClass('SpottedScript.Controls.MultiBuddyChooser.Pair');
SpottedScript.Controls.MultiBuddyChooser.View.registerClass('SpottedScript.Controls.MultiBuddyChooser.View');
