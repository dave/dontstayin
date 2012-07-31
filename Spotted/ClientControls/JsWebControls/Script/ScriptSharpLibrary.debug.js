// ScriptSharpLibrary.js
//


Type.registerNamespace('ScriptSharpLibrary');

////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary._htmlAutoCompleteMode

ScriptSharpLibrary._htmlAutoCompleteMode = function() { 
    /// <field name="suggest" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="complete" type="Number" integer="true" static="true">
    /// </field>
};
ScriptSharpLibrary._htmlAutoCompleteMode.prototype = {
    suggest: 1, 
    complete: 2
}
ScriptSharpLibrary._htmlAutoCompleteMode.registerEnum('ScriptSharpLibrary._htmlAutoCompleteMode', false);


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.PairListField

ScriptSharpLibrary.PairListField = function ScriptSharpLibrary_PairListField(hiddenField) {
    /// <param name="hiddenField" type="Object" domElement="true">
    /// </param>
    /// <field name="hiddenField" type="Object" domElement="true">
    /// </field>
    this.hiddenField = hiddenField;
}
ScriptSharpLibrary.PairListField.prototype = {
    hiddenField: null,
    
    _add: function ScriptSharpLibrary_PairListField$_add(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        if (this.hiddenField.value.length > 0) {
            this.hiddenField.value += ';';
        }
        this.hiddenField.value += escape(key) + ':' + escape(value);
    },
    
    set: function ScriptSharpLibrary_PairListField$set(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        this.removeByKey(key);
        if (this.hiddenField.value.length > 0) {
            this.hiddenField.value += ';';
        }
        this.hiddenField.value += escape(key) + ':' + escape(value);
    },
    
    removeAt: function ScriptSharpLibrary_PairListField$removeAt(index) {
        /// <param name="index" type="Number" integer="true">
        /// </param>
        var values = this.toArray();
        var newValues = [];
        for (var i = 0; i < values.length; i++) {
            if (i === index) {
                continue;
            }
            else {
                newValues[newValues.length] = values[i];
            }
        }
        this._writeValuesToHiddenInput(newValues);
    },
    
    get_count: function ScriptSharpLibrary_PairListField$get_count() {
        /// <value type="Number" integer="true"></value>
        if (this.hiddenField.value.length > 0) {
            return this.hiddenField.value.split(';').length;
        }
        else {
            return 0;
        }
    },
    
    removeByKey: function ScriptSharpLibrary_PairListField$removeByKey(key) {
        /// <param name="key" type="String">
        /// </param>
        this.removeAt(this.indexOf(key));
    },
    
    containsKey: function ScriptSharpLibrary_PairListField$containsKey(key) {
        /// <param name="key" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.indexOf(key) > -1;
    },
    
    indexOf: function ScriptSharpLibrary_PairListField$indexOf(key) {
        /// <param name="key" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var values = this.toArray();
        for (var i = 0; i < values.length; i++) {
            if ((values[i])[0] === key) {
                return i;
            }
        }
        return -1;
    },
    
    _writeValuesToHiddenInput: function ScriptSharpLibrary_PairListField$_writeValuesToHiddenInput(array) {
        /// <param name="array" type="Array">
        /// </param>
        this.hiddenField.value = '';
        var tempArray = [];
        for (var i = 0; i < array.length; i++) {
            var pair = array[i];
            tempArray[i] = escape(pair[0]) + ':' + escape(pair[1]);
        }
        this.hiddenField.value = tempArray.join(';');
    },
    
    toArray: function ScriptSharpLibrary_PairListField$toArray() {
        /// <returns type="Array"></returns>
        var array = [];
        if (this.hiddenField.value.length > 0) {
            var pairs = this.hiddenField.value.split(';');
            for (var i = 0; i < pairs.length; i++) {
                var pair = pairs[i].split(':');
                pair[0] = unescape(pair[0]);
                pair[1] = unescape(pair[1]);
                array[array.length] = pair;
            }
        }
        return array;
    },
    
    _clear: function ScriptSharpLibrary_PairListField$_clear() {
        this.hiddenField.value = '';
    },
    
    _toDictionary: function ScriptSharpLibrary_PairListField$_toDictionary() {
        /// <returns type="Object"></returns>
        var values = this.toArray();
        var dictionary = {};
        for (var i = 0; i < values.length; i++) {
            var pair = values[i];
            dictionary[pair[0]] = pair[1];
        }
        return dictionary;
    }
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.MultiSelectorAttributes

ScriptSharpLibrary.MultiSelectorAttributes = function ScriptSharpLibrary_MultiSelectorAttributes() {
    /// <field name="selections" type="String" static="true">
    /// </field>
    /// <field name="multiSelectorDelButtonCss" type="String" static="true">
    /// </field>
    /// <field name="multiSelectorListBoxCss" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.HelloWorld

ScriptSharpLibrary.HelloWorld = function ScriptSharpLibrary_HelloWorld(te) {
    /// <param name="te" type="Object" domElement="true">
    /// </param>
    /// <field name="te" type="Object" domElement="true">
    /// </field>
    this.te = te;
    te.innerHTML = 'hello';
    var handler = Function.createDelegate(this, this.doSomething);
    Sys.UI.DomEvent.addHandler(te, 'click', handler);
}
ScriptSharpLibrary.HelloWorld.prototype = {
    te: null,
    
    doSomething: function ScriptSharpLibrary_HelloWorld$doSomething(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        this.te.innerHTML = 'goodbye at ' + (new Date()).getTime();
    }
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.HtmlAutoCompleteAttributes

ScriptSharpLibrary.HtmlAutoCompleteAttributes = function ScriptSharpLibrary_HtmlAutoCompleteAttributes() {
    /// <field name="webServiceUrl" type="String" static="true">
    /// </field>
    /// <field name="webServiceMethod" type="String" static="true">
    /// </field>
    /// <field name="maxNumberOfItemsToGet" type="String" static="true">
    /// </field>
    /// <field name="popupMenuClassName" type="String" static="true">
    /// </field>
    /// <field name="popupMenuHighlightedItemClassName" type="String" static="true">
    /// </field>
    /// <field name="excludedItems" type="String" static="true">
    /// </field>
    /// <field name="listBoxItemValue" type="String" static="true">
    /// </field>
    /// <field name="onselectionmade" type="String" static="true">
    /// </field>
    /// <field name="onhighlight" type="String" static="true">
    /// </field>
    /// <field name="onpopupcancel" type="String" static="true">
    /// </field>
    /// <field name="watermark" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.HtmlAutoCompleteBehaviour

ScriptSharpLibrary.HtmlAutoCompleteBehaviour = function ScriptSharpLibrary_HtmlAutoCompleteBehaviour(input, hiddenInput, anchor, isSuggest, parametersHiddenField) {
    /// <param name="input" type="Object" domElement="true">
    /// </param>
    /// <param name="hiddenInput" type="Object" domElement="true">
    /// </param>
    /// <param name="anchor" type="Object" domElement="true">
    /// </param>
    /// <param name="isSuggest" type="Boolean">
    /// </param>
    /// <param name="parametersHiddenField" type="Object" domElement="true">
    /// </param>
    /// <field name="onhighlight" type="DOMEventHandler">
    /// </field>
    /// <field name="itemChosen" type="DOMEventHandler">
    /// </field>
    /// <field name="input" type="Object" domElement="true">
    /// </field>
    /// <field name="hiddenInput" type="Object" domElement="true">
    /// </field>
    /// <field name="anchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_popupMenu" type="ScriptSharpLibrary.PopupMenu">
    /// </field>
    /// <field name="webRequest" type="Sys.Net.WebRequest">
    /// </field>
    /// <field name="mode" type="ScriptSharpLibrary._htmlAutoCompleteMode">
    /// </field>
    /// <field name="watermarker" type="ScriptSharpLibrary.WatermarkExtender">
    /// </field>
    /// <field name="parameters" type="ScriptSharpLibrary.PairListField">
    /// </field>
    this.mode = (isSuggest) ? ScriptSharpLibrary._htmlAutoCompleteMode.suggest : ScriptSharpLibrary._htmlAutoCompleteMode.complete;
    this.input = input;
    this.hiddenInput = hiddenInput;
    this.anchor = (!anchor) ? input : anchor;
    Sys.UI.DomEvent.addHandler(input, 'blur', Function.createDelegate(this, this.handleBlur));
    Sys.UI.DomEvent.addHandler(input, 'keydown', Function.createDelegate(this, this.handleKeyDown));
    Sys.UI.DomEvent.addHandler(input, 'keyup', Function.createDelegate(this, this.handleKeyUp));
    if (input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.watermark)) {
        this.watermarker = new ScriptSharpLibrary.WatermarkExtender(input, input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.watermark).value);
    }
    this.parameters = new ScriptSharpLibrary.PairListField(parametersHiddenField);
}
ScriptSharpLibrary.HtmlAutoCompleteBehaviour.prototype = {
    onhighlight: null,
    itemChosen: null,
    input: null,
    hiddenInput: null,
    anchor: null,
    _popupMenu: null,
    
    get_popupMenu: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_popupMenu() {
        /// <value type="ScriptSharpLibrary.PopupMenu"></value>
        if (!this._popupMenu) {
            var cssAtt = this.input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuClassName);
            var highlightedCssAtt = this.input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName);
            this._popupMenu = new ScriptSharpLibrary.PopupMenu((!cssAtt) ? ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuClassName : cssAtt.value, (!highlightedCssAtt) ? ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName : highlightedCssAtt.value);
            this._popupMenu.itemClick = Function.createDelegate(this, this._itemSelected);
            this._popupMenu.itemHighlighted = this.onhighlight;
        }
        return this._popupMenu;
    },
    
    webRequest: null,
    mode: 0,
    watermarker: null,
    parameters: null,
    
    handleBlur: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$handleBlur(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        window.setTimeout(Function.createDelegate(this, this.doBlur), 250);
    },
    
    doBlur: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$doBlur() {
        if (this.hiddenInput.value === '') {
            this._cancel();
        }
        else {
            this.get_popupMenu().hide();
        }
    },
    
    handleKeyDown: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$handleKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode === Sys.UI.Key.up) {
            if (this.get_popupMenu()) {
                this.get_popupMenu().set_indexOfCurrentlyHighlightedItem(this.get_popupMenu().get_indexOfCurrentlyHighlightedItem() - 1);
            }
        }
        else if (e.keyCode === Sys.UI.Key.down) {
            if (this.get_popupMenu()) {
                this.get_popupMenu().set_indexOfCurrentlyHighlightedItem(this.get_popupMenu().get_indexOfCurrentlyHighlightedItem() + 1);
            }
        }
        else if (e.keyCode === Sys.UI.Key.esc) {
            if (this.get_popupMenu().get_currentlyHighlightedItem()) {
                this.get_popupMenu().clear();
                this.get_popupMenu().hide();
                e.preventDefault();
            }
            else {
                this._cancel();
            }
        }
        else if (e.keyCode === Sys.UI.Key.tab || e.keyCode === Sys.UI.Key.enter) {
            if (this.get_validSelectionHasBeenMade()) {
                return;
            }
            if (this.get_suggestionIsHighlighted()) {
                e.preventDefault();
                this._itemSelected(this.get_popupMenu().get_currentlyHighlightedItem());
                return;
            }
            else {
                if (this.mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
                    this._cancel();
                }
            }
        }
        else if (e.keyCode === Sys.UI.Key.backspace && this.mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
            if (this.get_validSelectionHasBeenMade()) {
                this.hiddenInput.value = '';
                this.input.value = '';
            }
            else {
                this.hiddenInput.value = '';
                this._requestSuggestions();
            }
        }
        else if (e.keyCode === Sys.UI.Key.del && this.mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
            this.hiddenInput.value = '';
            this._requestSuggestions();
        }
    },
    
    handleKeyUp: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$handleKeyUp(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode !== Sys.UI.Key.down && e.keyCode !== Sys.UI.Key.end && e.keyCode !== Sys.UI.Key.enter && e.keyCode !== Sys.UI.Key.esc && e.keyCode !== Sys.UI.Key.home && e.keyCode !== Sys.UI.Key.left && e.keyCode !== Sys.UI.Key.pageDown && e.keyCode !== Sys.UI.Key.pageUp && e.keyCode !== Sys.UI.Key.tab && e.keyCode !== Sys.UI.Key.up) {
            if (this.mode === ScriptSharpLibrary._htmlAutoCompleteMode.suggest) {
                this.hiddenInput.value = this.input.value;
            }
            this._requestSuggestions();
        }
    },
    
    get_validSelectionHasBeenMade: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_validSelectionHasBeenMade() {
        /// <value type="Boolean"></value>
        return this.hiddenInput.value && this.hiddenInput.value !== '';
    },
    
    get_suggestionIsHighlighted: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_suggestionIsHighlighted() {
        /// <value type="Boolean"></value>
        return this.get_popupMenu().get_currentlyHighlightedItem();
    },
    
    _cancel: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_cancel() {
        this.get_popupMenu().clear();
        this.get_popupMenu().hide();
        this.input.value = '';
        this.hiddenInput.value = '';
    },
    
    _requestSuggestions: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_requestSuggestions() {
        var textSoFar = this.input.value;
        if (this.mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
            this.set_value('');
        }
        var parameters = {};
        parameters['text'] = textSoFar;
        var att = this.input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.maxNumberOfItemsToGet);
        parameters['maxNumberOfItemsToGet'] = (!att) ? 5 : Number.parseInvariant(att.value);
        parameters['parameters'] = this.parameters._toDictionary();
        if (this.webRequest && this.webRequest.get_executor().get_started()) {
            this.webRequest.get_executor().abort();
        }
        this.webRequest = Sys.Net.WebServiceProxy.invoke(this.input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.webServiceUrl).value, this.input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.webServiceMethod).value, true, parameters, Function.createDelegate(this, this.successCallback), Function.createDelegate(this, this.failureCallback), textSoFar, -1);
    },
    
    successCallback: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$successCallback(rawResult, userContext, methodName) {
        /// <param name="rawResult" type="Object">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        var suggestions = rawResult;
        this.get_popupMenu().clear();
        for (var i = 0; i < suggestions.length; i++) {
            var suggestion = suggestions[i];
            var pair = new ScriptSharpLibrary.KeyValuePair();
            pair.key = suggestion.text;
            pair.value = suggestion.value;
            this.get_popupMenu().addItem(suggestion.html, pair);
        }
        if (this.mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
            this.get_popupMenu().set_indexOfCurrentlyHighlightedItem(0);
        }
        this.get_popupMenu().show(this.anchor);
    },
    
    _itemSelected: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_itemSelected(value) {
        /// <param name="value" type="Object">
        /// </param>
        this.input.value = (value).key;
        this.hiddenInput.value = (value).value;
        this.get_popupMenu().clear();
        this.get_popupMenu().hide();
        if (this.itemChosen) {
            this.itemChosen();
        }
        var att = this.input.getAttributeNode('AutoPostBackFunction');
        if (att) {
            eval(att.value);
        }
    },
    
    get_text: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_text() {
        /// <value type="String"></value>
        return this.input.value;
    },
    set_text: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$set_text(value) {
        /// <value type="String"></value>
        this.input.value = value;
        return value;
    },
    
    get_value: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_value() {
        /// <value type="String"></value>
        return this.hiddenInput.value;
    },
    set_value: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$set_value(value) {
        /// <value type="String"></value>
        this.hiddenInput.value = value;
        return value;
    },
    
    get_currentlyHighlighedItem: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_currentlyHighlighedItem() {
        /// <value type="Object"></value>
        return this.get_popupMenu().get_currentlyHighlightedItem();
    },
    
    failureCallback: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$failureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        alert(error.get_message());
    },
    
    focus: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$focus() {
        this.input.focus();
        if (this.watermarker) {
            this.watermarker.onFocus(null);
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.Suggestion

ScriptSharpLibrary.Suggestion = function ScriptSharpLibrary_Suggestion() {
    /// <field name="html" type="String">
    /// </field>
    /// <field name="text" type="String">
    /// </field>
    /// <field name="value" type="String">
    /// </field>
}
ScriptSharpLibrary.Suggestion.prototype = {
    html: null,
    text: null,
    value: null
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.SuggestionsGetter

ScriptSharpLibrary.SuggestionsGetter = function ScriptSharpLibrary_SuggestionsGetter(webServiceUrl, webServiceCommand, maxNumberOfItemsToGet) {
    /// <param name="webServiceUrl" type="String">
    /// </param>
    /// <param name="webServiceCommand" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <field name="suggestionsGot" type="Sys.EventHandler">
    /// </field>
    /// <field name="webRequest" type="Sys.Net.WebRequest">
    /// </field>
    /// <field name="webServiceUrl" type="String">
    /// </field>
    /// <field name="webServiceCommand" type="String">
    /// </field>
    /// <field name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </field>
    /// <field name="suggestions" type="Array" elementType="Suggestion">
    /// </field>
    this.webServiceUrl = webServiceUrl;
    this.webServiceCommand = webServiceCommand;
    this.maxNumberOfItemsToGet = maxNumberOfItemsToGet;
}
ScriptSharpLibrary.SuggestionsGetter.prototype = {
    suggestionsGot: null,
    webRequest: null,
    webServiceUrl: null,
    webServiceCommand: null,
    maxNumberOfItemsToGet: 0,
    suggestions: null,
    
    requestSuggestions: function ScriptSharpLibrary_SuggestionsGetter$requestSuggestions(textSoFar) {
        /// <param name="textSoFar" type="String">
        /// </param>
        var parameters = {};
        parameters['text'] = textSoFar;
        parameters['maxNumberOfItemsToGet'] = this.maxNumberOfItemsToGet;
        this.cancelRequests();
        this.webRequest = Sys.Net.WebServiceProxy.invoke(this.webServiceUrl, this.webServiceCommand, true, parameters, Function.createDelegate(this, this.successCallback), Function.createDelegate(this, this.failureCallback), textSoFar, -1);
    },
    
    cancelRequests: function ScriptSharpLibrary_SuggestionsGetter$cancelRequests() {
        if (this.webRequest) {
            this.webRequest.get_executor().abort();
            this.webRequest = null;
        }
    },
    
    successCallback: function ScriptSharpLibrary_SuggestionsGetter$successCallback(result, userContext, methodName) {
        /// <param name="result" type="Object">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.suggestions = result;
        if (this.suggestionsGot) {
            this.suggestionsGot(this, Sys.EventArgs.Empty);
        }
    },
    
    failureCallback: function ScriptSharpLibrary_SuggestionsGetter$failureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
    }
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.MultiSelectorBehaviour

ScriptSharpLibrary.MultiSelectorBehaviour = function ScriptSharpLibrary_MultiSelectorBehaviour(container, htmlAutoComplete, hiddenOutput) {
    /// <param name="container" type="Object" domElement="true">
    /// </param>
    /// <param name="htmlAutoComplete" type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour">
    /// </param>
    /// <param name="hiddenOutput" type="Object" domElement="true">
    /// </param>
    /// <field name="itemAdded" type="ScriptSharpLibrary.ItemChangeDelegate">
    /// </field>
    /// <field name="itemRemoved" type="ScriptSharpLibrary.ItemChangeDelegate">
    /// </field>
    /// <field name="htmlAutoComplete" type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour">
    /// </field>
    /// <field name="container" type="Object" domElement="true">
    /// </field>
    /// <field name="hiddenOutput" type="Object" domElement="true">
    /// </field>
    /// <field name="_selections" type="ScriptSharpLibrary.PairListField">
    /// </field>
    this.container = container;
    this.hiddenOutput = hiddenOutput;
    this.htmlAutoComplete = htmlAutoComplete;
    this.htmlAutoComplete.itemChosen = Function.createDelegate(this, this.htmlAutoComplete_ItemChosen);
    this._selections = new ScriptSharpLibrary.PairListField(hiddenOutput);
    this._initialiseInitialSelections();
    Sys.UI.DomEvent.addHandler(this.container, 'click', Function.createDelegate(this, this._onClick));
}
ScriptSharpLibrary.MultiSelectorBehaviour.prototype = {
    itemAdded: null,
    itemRemoved: null,
    htmlAutoComplete: null,
    container: null,
    hiddenOutput: null,
    _selections: null,
    
    _initialiseInitialSelections: function ScriptSharpLibrary_MultiSelectorBehaviour$_initialiseInitialSelections() {
        this._selections._clear();
        for (var i = 0; i < this.container.childNodes.length; i++) {
            var child = this.container.childNodes[i];
            if (child.nodeType === 1 && child.childNodes[0].tagName !== 'INPUT') {
                Sys.UI.DomEvent.addHandler(child.childNodes[1], 'click', Function.createDelegate(this, this.deleteButtonClick));
                this._selections.set(child.childNodes[0].nodeValue, child.getAttributeNode('val').value);
            }
        }
    },
    
    _onClick: function ScriptSharpLibrary_MultiSelectorBehaviour$_onClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this.htmlAutoComplete.focus();
    },
    
    addItem: function ScriptSharpLibrary_MultiSelectorBehaviour$addItem(text, value) {
        /// <param name="text" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        var item = document.createElement('LI');
        var itemCssAttribute = this.container.getAttributeNode(ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorListBoxCss);
        item.className = (itemCssAttribute) ? itemCssAttribute.value : ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorListBoxCss;
        item.innerHTML = text;
        var deleteButton = document.createElement('SPAN');
        deleteButton.innerHTML = '&nbsp;&nbsp;';
        var delCssAttribute = this.container.getAttributeNode(ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorDelButtonCss);
        deleteButton.className = (delCssAttribute) ? delCssAttribute.value : ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorDelButtonCss;
        var textAtt = document.createAttribute('text');
        textAtt.value = text;
        item.setAttributeNode(textAtt);
        var valueAtt = document.createAttribute('val');
        valueAtt.value = value;
        item.setAttributeNode(valueAtt);
        Sys.UI.DomEvent.addHandler(deleteButton, 'click', Function.createDelegate(this, this.deleteButtonClick));
        item.appendChild(deleteButton);
        var childToInsertBefore = this.container.lastChild;
        while (!childToInsertBefore.childNodes.length || childToInsertBefore.childNodes[0].tagName !== 'INPUT') {
            childToInsertBefore = childToInsertBefore.previousSibling;
        }
        this.container.insertBefore(item, childToInsertBefore);
        this._selections.set(text, value);
        if (this.itemAdded) {
            this.itemAdded(text, value);
        }
    },
    
    htmlAutoComplete_ItemChosen: function ScriptSharpLibrary_MultiSelectorBehaviour$htmlAutoComplete_ItemChosen() {
        if (!this._selections.containsKey(this.htmlAutoComplete.get_text())) {
            this.addItem(this.htmlAutoComplete.get_text(), this.htmlAutoComplete.get_value());
        }
        else {
            debugger;;
        }
        this.htmlAutoComplete.set_text('');
        this.htmlAutoComplete.set_value('');
        this.htmlAutoComplete.focus();
    },
    
    deleteButtonClick: function ScriptSharpLibrary_MultiSelectorBehaviour$deleteButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var text = e.target.parentNode.getAttributeNode('text').value;
        var value = e.target.parentNode.getAttributeNode('val').value;
        this._selections.removeByKey(text);
        Sys.UI.DomEvent.clearHandlers(e.target);
        e.target.parentNode.parentNode.removeChild(e.target.parentNode);
        this.htmlAutoComplete.focus();
        if (this.itemRemoved) {
            this.itemRemoved(text, value);
        }
    },
    
    getSelections: function ScriptSharpLibrary_MultiSelectorBehaviour$getSelections() {
        /// <returns type="ScriptSharpLibrary.PairListField"></returns>
        return this._selections;
    },
    
    clear: function ScriptSharpLibrary_MultiSelectorBehaviour$clear() {
        for (var i = this.container.childNodes.length - 1; i >= 0; i--) {
            var child = this.container.childNodes[i];
            if (child.nodeType === 1 && child.childNodes[0].tagName !== 'INPUT') {
                Sys.UI.DomEvent.clearHandlers(child.childNodes[1]);
                this._selections.removeByKey(child.childNodes[0].nodeValue);
                child.parentNode.removeChild(child);
            }
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.PopupMenu

ScriptSharpLibrary.PopupMenu = function ScriptSharpLibrary_PopupMenu(cssClass, highlightedItemCssClass) {
    /// <param name="cssClass" type="String">
    /// </param>
    /// <param name="highlightedItemCssClass" type="String">
    /// </param>
    /// <field name="itemClick" type="ScriptSharpLibrary.ItemClickDelegate">
    /// </field>
    /// <field name="itemHighlighted" type="DOMEventHandler">
    /// </field>
    /// <field name="container" type="Object" domElement="true">
    /// </field>
    /// <field name="highlightedItemCssClass" type="String">
    /// </field>
    /// <field name="indexOfCurrentlyHighlightedItem" type="Number" integer="true">
    /// </field>
    this.indexOfCurrentlyHighlightedItem = -1;
    this.container = document.createElement('DIV');
    this.container.className = (!cssClass) ? 'PopupMenu' : cssClass;
    this.highlightedItemCssClass = (!highlightedItemCssClass) ? 'PopupMenuHighlightedItem' : highlightedItemCssClass;
    this.container.style.display = 'none';
    document.body.appendChild(this.container);
}
ScriptSharpLibrary.PopupMenu.prototype = {
    itemClick: null,
    itemHighlighted: null,
    container: null,
    highlightedItemCssClass: null,
    
    addItem: function ScriptSharpLibrary_PopupMenu$addItem(html, value) {
        /// <param name="html" type="String">
        /// </param>
        /// <param name="value" type="Object">
        /// </param>
        var div = document.createElement('DIV');
        var divAsArray = div;
        divAsArray['value'] = value;
        div.innerHTML = html;
        this.container.appendChild(div);
        Sys.UI.DomEvent.addHandler(div, 'click', Function.createDelegate(this, this._itemClickHandler));
        Sys.UI.DomEvent.addHandler(div, 'mouseover', Function.createDelegate(this, this._itemMouseEnterHandler));
    },
    
    _itemClickHandler: function ScriptSharpLibrary_PopupMenu$_itemClickHandler(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this.itemClick) {
            this.itemClick(this.get_currentlyHighlightedItem());
        }
    },
    
    _itemMouseEnterHandler: function ScriptSharpLibrary_PopupMenu$_itemMouseEnterHandler(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var el = e.target;
        while (el.parentNode !== this.container) {
            el = el.parentNode;
        }
        for (var i = 0; i < this.container.childNodes.length; i++) {
            if (this.container.childNodes[i] === el) {
                this.set_indexOfCurrentlyHighlightedItem(i);
                return;
            }
        }
    },
    
    clear: function ScriptSharpLibrary_PopupMenu$clear() {
        while (this.container.childNodes.length > 0) {
            Sys.UI.DomEvent.clearHandlers(this.container.childNodes[0]);
            this.container.removeChild(this.container.childNodes[0]);
        }
        this.indexOfCurrentlyHighlightedItem = -1;
    },
    
    show: function ScriptSharpLibrary_PopupMenu$show(anchor) {
        /// <param name="anchor" type="Object" domElement="true">
        /// </param>
        var jq = jQuery(anchor);
        this.container.style.left = jq.offset().left + 'px';
        this.container.style.top = jq.offset().top + jq.height() + 3 + 'px';
        this.container.style.width = jq.width() + 'px';
        this.container.style.display = (this.container.childNodes.length > 0) ? '' : 'none';
    },
    
    hide: function ScriptSharpLibrary_PopupMenu$hide() {
        eval('try{htm();}catch(e){}');
        this.container.style.display = 'none';
    },
    
    get_indexOfCurrentlyHighlightedItem: function ScriptSharpLibrary_PopupMenu$get_indexOfCurrentlyHighlightedItem() {
        /// <value type="Number" integer="true"></value>
        return (this.indexOfCurrentlyHighlightedItem < this.container.childNodes.length) ? this.indexOfCurrentlyHighlightedItem : -1;
    },
    set_indexOfCurrentlyHighlightedItem: function ScriptSharpLibrary_PopupMenu$set_indexOfCurrentlyHighlightedItem(value) {
        /// <value type="Number" integer="true"></value>
        if (this.get_currentlyHighlightedElement()) {
            this.get_currentlyHighlightedElement().className = '';
        }
        this.indexOfCurrentlyHighlightedItem = (value + this.container.childNodes.length) % this.container.childNodes.length;
        if (this.get_currentlyHighlightedElement()) {
            this.get_currentlyHighlightedElement().className = this.highlightedItemCssClass;
            if (this.itemHighlighted) {
                this.itemHighlighted();
            }
        }
        return value;
    },
    
    get_currentlyHighlightedItem: function ScriptSharpLibrary_PopupMenu$get_currentlyHighlightedItem() {
        /// <value type="Object"></value>
        if (!this.get_currentlyHighlightedElement()) {
            return null;
        }
        else {
            return (this.get_currentlyHighlightedElement())['value'];
        }
    },
    
    get_currentlyHighlightedElement: function ScriptSharpLibrary_PopupMenu$get_currentlyHighlightedElement() {
        /// <value type="Object" domElement="true"></value>
        if (this.get_indexOfCurrentlyHighlightedItem() === -1) {
            return null;
        }
        else {
            return this.container.childNodes[this.get_indexOfCurrentlyHighlightedItem()];
        }
    },
    
    findPosLeft: function ScriptSharpLibrary_PopupMenu$findPosLeft(el) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var curleft = 0;
        if (el.offsetParent) {
            do {
                curleft += el.offsetLeft;
                el = el.offsetParent;
            } while (el);
        }
        return curleft;
    },
    
    findPosTop: function ScriptSharpLibrary_PopupMenu$findPosTop(el) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var curtop = 0;
        if (el.offsetParent) {
            do {
                curtop += el.offsetTop;
                el = el.offsetParent;
            } while (el);
        }
        return curtop;
    }
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.KeyValuePair

ScriptSharpLibrary.KeyValuePair = function ScriptSharpLibrary_KeyValuePair() {
    /// <field name="key" type="String">
    /// </field>
    /// <field name="value" type="String">
    /// </field>
}
ScriptSharpLibrary.KeyValuePair.prototype = {
    key: null,
    value: null
}


////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.WatermarkExtender

ScriptSharpLibrary.WatermarkExtender = function ScriptSharpLibrary_WatermarkExtender(el, watermark) {
    /// <param name="el" type="Object" domElement="true">
    /// </param>
    /// <param name="watermark" type="String">
    /// </param>
    /// <field name="el" type="Object" domElement="true">
    /// </field>
    /// <field name="watermark" type="String">
    /// </field>
    /// <field name="timeoutId" type="Number" integer="true">
    /// </field>
    this.el = el;
    this.watermark = watermark;
    Sys.UI.DomEvent.addHandler(el, 'focus', Function.createDelegate(this, this.onFocus));
    Sys.UI.DomEvent.addHandler(el, 'blur', Function.createDelegate(this, this.onBlur));
    (el)['readOnly'] = null;
}
ScriptSharpLibrary.WatermarkExtender.prototype = {
    el: null,
    watermark: null,
    timeoutId: 0,
    
    onBlur: function ScriptSharpLibrary_WatermarkExtender$onBlur(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        this.timeoutId = window.setTimeout(Function.createDelegate(this, this.addWatermark), 300);
    },
    
    onFocus: function ScriptSharpLibrary_WatermarkExtender$onFocus(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        window.clearTimeout(this.timeoutId);
        if (this.el.value === this.watermark) {
            this.el.className = '';
            this.el.value = '';
        }
    },
    
    addWatermark: function ScriptSharpLibrary_WatermarkExtender$addWatermark() {
        if (this.el.value === '') {
            this.el.className = 'Watermark';
            this.el.value = this.watermark;
        }
    }
}


ScriptSharpLibrary.PairListField.registerClass('ScriptSharpLibrary.PairListField');
ScriptSharpLibrary.MultiSelectorAttributes.registerClass('ScriptSharpLibrary.MultiSelectorAttributes');
ScriptSharpLibrary.HelloWorld.registerClass('ScriptSharpLibrary.HelloWorld');
ScriptSharpLibrary.HtmlAutoCompleteAttributes.registerClass('ScriptSharpLibrary.HtmlAutoCompleteAttributes');
ScriptSharpLibrary.HtmlAutoCompleteBehaviour.registerClass('ScriptSharpLibrary.HtmlAutoCompleteBehaviour');
ScriptSharpLibrary.Suggestion.registerClass('ScriptSharpLibrary.Suggestion');
ScriptSharpLibrary.SuggestionsGetter.registerClass('ScriptSharpLibrary.SuggestionsGetter');
ScriptSharpLibrary.MultiSelectorBehaviour.registerClass('ScriptSharpLibrary.MultiSelectorBehaviour');
ScriptSharpLibrary.PopupMenu.registerClass('ScriptSharpLibrary.PopupMenu');
ScriptSharpLibrary.KeyValuePair.registerClass('ScriptSharpLibrary.KeyValuePair');
ScriptSharpLibrary.WatermarkExtender.registerClass('ScriptSharpLibrary.WatermarkExtender');
ScriptSharpLibrary.MultiSelectorAttributes.selections = 'Selections';
ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorDelButtonCss = 'MultiSelectorDelButton';
ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorListBoxCss = 'MultiSelectorListBoxItem';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.webServiceUrl = 'WebServiceUrl';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.webServiceMethod = 'WebServiceMethod';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.maxNumberOfItemsToGet = 'MaxNumberOfItemsToGet';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuClassName = 'PopupMenu';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName = 'PopupMenuHighlightedItem';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.excludedItems = 'ExcludedItems';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.listBoxItemValue = 'ListBoxItemValue';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.onselectionmade = 'onselectionmade';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.onhighlight = 'onhighlight';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.onpopupcancel = 'onpopupcancel';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.watermark = 'Watermark';

// ---- Do not remove this footer ----
// Generated using Script# v0.5.0.0 (http://projects.nikhilk.net)
// -----------------------------------
