//! ClientControls.debug.js
//

(function($) {

Type.registerNamespace('Js.ClientControls.HtmlAutoComplete');

////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.HtmlAutoComplete.RemoteSuggestionsGetter

Js.ClientControls.HtmlAutoComplete.RemoteSuggestionsGetter = function Js_ClientControls_HtmlAutoComplete_RemoteSuggestionsGetter() {
    /// <field name="onSuggestionsGot" type="Function">
    /// </field>
    /// <field name="onSuggestionsRequested" type="Function">
    /// </field>
    /// <field name="onAllSuggestionsReceived" type="Function">
    /// </field>
    /// <field name="onSuggestionReceived" type="Function">
    /// </field>
    /// <field name="onAbortCurrentRequest" type="Function">
    /// </field>
}
Js.ClientControls.HtmlAutoComplete.RemoteSuggestionsGetter.prototype = {
    onSuggestionsGot: null,
    onSuggestionsRequested: null,
    onAllSuggestionsReceived: null,
    onSuggestionReceived: null,
    onAbortCurrentRequest: null,
    
    _requestSuggestions: function Js_ClientControls_HtmlAutoComplete_RemoteSuggestionsGetter$_requestSuggestions(text, parameters, maxNumberOfItemsToGet) {
        /// <param name="text" type="String">
        /// </param>
        /// <param name="parameters" type="Object">
        /// </param>
        /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
        /// </param>
        if (this.get__isMakingRequest()) {
            this._abortCurrentRequest();
        }
        if (text.length < 50) {
            this.makeRequest(text, parameters, maxNumberOfItemsToGet);
        }
        if (this.onSuggestionsRequested != null) {
            this.onSuggestionsRequested();
        }
    },
    
    _abortCurrentRequest: function Js_ClientControls_HtmlAutoComplete_RemoteSuggestionsGetter$_abortCurrentRequest() {
        this._doAbortCurrentRequest();
        if (this.onAbortCurrentRequest != null) {
            this.onAbortCurrentRequest();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter

Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter = function Js_ClientControls_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter(url, methodName) {
    /// <param name="url" type="String">
    /// </param>
    /// <param name="methodName" type="String">
    /// </param>
    /// <field name="_url$1" type="String">
    /// </field>
    /// <field name="methodName" type="String">
    /// </field>
    /// <field name="_ajaxRequest$1" type="jQueryXmlHttpRequest">
    /// </field>
    Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter.initializeBase(this);
    this._url$1 = url;
    this.methodName = methodName;
}
Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter.prototype = {
    _url$1: null,
    methodName: null,
    _ajaxRequest$1: null,
    
    makeRequest: function Js_ClientControls_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter$makeRequest(text, requestParameters, maxNumberOfItemsToGet) {
        /// <param name="text" type="String">
        /// </param>
        /// <param name="requestParameters" type="Object">
        /// </param>
        /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
        /// </param>
        var parameters = {};
        parameters['text'] = text;
        parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
        parameters['parameters'] = requestParameters;
        if (this._ajaxRequest$1 != null) {
            try {
                this._ajaxRequest$1.abort();
            }
            catch ($e1) {
            }
        }
        var o = {};
        o.url = this._url$1 + '/' + this.methodName;
        o.timeout = 10000;
        o.type = 'POST';
        o.async = true;
        o.cache = false;
        o.contentType = 'application/json; charset=utf-8';
        o.data = JSON.stringify(parameters);
        o.dataType = 'json';
        o.error = function(request, error, exception) {
        };
        o.success = ss.Delegate.create(this, function(data, textStatus, request) {
            this._successCallback$1((data)['d'], text, this.methodName);
        });
        this._ajaxRequest$1 = $.ajax(o);
    },
    
    _successCallback$1: function Js_ClientControls_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter$_successCallback$1(rawResult, userContext, methodName) {
        /// <param name="rawResult" type="Object">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.onSuggestionReceived != null) {
            this.onSuggestionReceived(rawResult);
        }
        if (this.onAllSuggestionsReceived != null) {
            this.onAllSuggestionsReceived();
        }
    },
    
    get__isMakingRequest: function Js_ClientControls_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter$get__isMakingRequest() {
        /// <value type="Boolean"></value>
        return this._ajaxRequest$1 != null;
    },
    
    _doAbortCurrentRequest: function Js_ClientControls_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter$_doAbortCurrentRequest() {
        if (this._ajaxRequest$1 != null) {
            this._ajaxRequest$1.abort();
            this._ajaxRequest$1 = null;
        }
    }
}


Type.registerNamespace('Js.ClientControls');

////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.SuggestionsCollection

Js.ClientControls.SuggestionsCollection = function Js_ClientControls_SuggestionsCollection() {
    /// <field name="onSuggestionsChanged" type="Function">
    /// </field>
    /// <field name="_suggestions" type="Array">
    /// </field>
    this._suggestions = [];
}
Js.ClientControls.SuggestionsCollection.prototype = {
    onSuggestionsChanged: null,
    
    add: function Js_ClientControls_SuggestionsCollection$add(newSuggestion) {
        /// <param name="newSuggestion" type="Js.AutoCompleteLibrary.Suggestion">
        /// </param>
        this._addWithoutSortOrNotify(newSuggestion);
        this._sort();
        if (this.onSuggestionsChanged != null) {
            this.onSuggestionsChanged();
        }
    },
    
    _addWithoutSortOrNotify: function Js_ClientControls_SuggestionsCollection$_addWithoutSortOrNotify(newSuggestion) {
        /// <param name="newSuggestion" type="Js.AutoCompleteLibrary.Suggestion">
        /// </param>
        var i = 0;
        for (i = 0; i < this._suggestions.length; i++) {
            var current = this._suggestions[i];
            if (current.value === newSuggestion.value) {
                if (newSuggestion.priority < current.priority) {
                    newSuggestion.priority = current.priority;
                }
                break;
            }
        }
        this._suggestions[i] = newSuggestion;
    },
    
    addRange: function Js_ClientControls_SuggestionsCollection$addRange(newSuggestions) {
        /// <param name="newSuggestions" type="Array" elementType="Suggestion">
        /// </param>
        for (var i = 0; i < newSuggestions.length; i++) {
            this._addWithoutSortOrNotify(newSuggestions[i]);
        }
        this._sort();
        if (this.onSuggestionsChanged != null) {
            this.onSuggestionsChanged();
        }
    },
    
    _sort: function Js_ClientControls_SuggestionsCollection$_sort() {
        this._suggestions.sort(function(a, b) {
            return (b).priority - (a).priority;
        });
    },
    
    removeAt: function Js_ClientControls_SuggestionsCollection$removeAt(index) {
        /// <param name="index" type="Number" integer="true">
        /// </param>
        var length = this._suggestions.length;
        var newSuggestions = [];
        for (var i = 0; i < this._suggestions.length; i++) {
            if (i === index) {
                continue;
            }
            newSuggestions[newSuggestions.length] = this._suggestions[i];
        }
        this._suggestions = newSuggestions;
        if (length !== this._suggestions.length && this.onSuggestionsChanged != null) {
            this.onSuggestionsChanged();
        }
    },
    
    clear: function Js_ClientControls_SuggestionsCollection$clear() {
        this._suggestions = [];
    },
    
    get_count: function Js_ClientControls_SuggestionsCollection$get_count() {
        /// <value type="Number" integer="true"></value>
        return this._suggestions.length;
    },
    get_item: function Js_ClientControls_SuggestionsCollection$get_item(index) {
        /// <param name="index" type="Number" integer="true">
        /// </param>
        /// <param name="value" type="Js.AutoCompleteLibrary.Suggestion">
        /// </param>
        /// <returns type="Js.AutoCompleteLibrary.Suggestion"></returns>
        return (this._suggestions[index]);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.SuggestionsGetter

Js.ClientControls.SuggestionsGetter = function Js_ClientControls_SuggestionsGetter(webServiceUrl, webServiceCommand, maxNumberOfItemsToGet) {
    /// <param name="webServiceUrl" type="String">
    /// </param>
    /// <param name="webServiceCommand" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <field name="suggestionsGot" type="Function">
    /// </field>
    /// <field name="_ajaxRequest" type="jQueryXmlHttpRequest">
    /// </field>
    /// <field name="_webServiceUrl" type="String">
    /// </field>
    /// <field name="_webServiceCommand" type="String">
    /// </field>
    /// <field name="_maxNumberOfItemsToGet" type="Number" integer="true">
    /// </field>
    /// <field name="suggestions" type="Array" elementType="Suggestion">
    /// </field>
    this._webServiceUrl = webServiceUrl;
    this._webServiceCommand = webServiceCommand;
    this._maxNumberOfItemsToGet = maxNumberOfItemsToGet;
}
Js.ClientControls.SuggestionsGetter.prototype = {
    suggestionsGot: null,
    _ajaxRequest: null,
    _webServiceUrl: null,
    _webServiceCommand: null,
    _maxNumberOfItemsToGet: 0,
    suggestions: null,
    
    requestSuggestions: function Js_ClientControls_SuggestionsGetter$requestSuggestions(textSoFar) {
        /// <param name="textSoFar" type="String">
        /// </param>
        var parameters = {};
        parameters['text'] = textSoFar;
        parameters['maxNumberOfItemsToGet'] = this._maxNumberOfItemsToGet;
        this.cancelRequests();
        var o = {};
        o.url = this._webServiceUrl + '/' + this._webServiceCommand;
        o.timeout = 10000;
        o.type = 'POST';
        o.async = true;
        o.cache = false;
        o.contentType = 'application/json; charset=utf-8';
        o.data = JSON.stringify(parameters);
        o.dataType = 'json';
        o.error = ss.Delegate.create(this, function(request, error, exception) {
            this.failureCallback(new Js.Library.WebServiceError(Type.getInstanceType(exception).toString(), error, exception.toString(), request.status, request.status === 408), textSoFar, this._webServiceCommand);
        });
        o.success = ss.Delegate.create(this, function(data, textStatus, request) {
            this.successCallback((data)['d'], textSoFar, this._webServiceCommand);
        });
        this._ajaxRequest = $.ajax(o);
    },
    
    cancelRequests: function Js_ClientControls_SuggestionsGetter$cancelRequests() {
        if (this._ajaxRequest != null) {
            this._ajaxRequest.abort();
            this._ajaxRequest = null;
        }
    },
    
    successCallback: function Js_ClientControls_SuggestionsGetter$successCallback(result, userContext, methodName) {
        /// <param name="result" type="Object">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.suggestions = result;
        if (this.suggestionsGot != null) {
            this.suggestionsGot(this, ss.EventArgs.Empty);
        }
    },
    
    failureCallback: function Js_ClientControls_SuggestionsGetter$failureCallback(error, userContext, methodName) {
        /// <param name="error" type="Js.Library.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.WatermarkExtender

Js.ClientControls.WatermarkExtender = function Js_ClientControls_WatermarkExtender(el, watermark) {
    /// <param name="el" type="Object" domElement="true">
    /// </param>
    /// <param name="watermark" type="String">
    /// </param>
    /// <field name="_el" type="Object" domElement="true">
    /// </field>
    /// <field name="_watermark" type="String">
    /// </field>
    /// <field name="_timeoutId" type="Number" integer="true">
    /// </field>
    this._el = el;
    this._watermark = watermark;
    $(el).focus(ss.Delegate.create(this, this.onFocus));
    $(el).blur(ss.Delegate.create(this, this.onBlur));
    (el)['readOnly'] = null;
}
Js.ClientControls.WatermarkExtender.prototype = {
    _el: null,
    _watermark: null,
    _timeoutId: 0,
    
    onBlur: function Js_ClientControls_WatermarkExtender$onBlur(ev) {
        /// <param name="ev" type="jQueryEvent">
        /// </param>
        this._timeoutId = window.setTimeout(ss.Delegate.create(this, this.addWatermark), 300);
    },
    
    onFocus: function Js_ClientControls_WatermarkExtender$onFocus(ev) {
        /// <param name="ev" type="jQueryEvent">
        /// </param>
        window.clearTimeout(this._timeoutId);
        if (this._el.value === this._watermark) {
            this._el.className = '';
            this._el.value = '';
        }
    },
    
    addWatermark: function Js_ClientControls_WatermarkExtender$addWatermark() {
        if (!this._el.value) {
            this._el.className = 'Watermark';
            this._el.value = this._watermark;
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.HtmlAutoCompleteAttributes

Js.ClientControls.HtmlAutoCompleteAttributes = function Js_ClientControls_HtmlAutoCompleteAttributes() {
    /// <field name="webServiceUrl" type="String" static="true">
    /// </field>
    /// <field name="webServiceMethod" type="String" static="true">
    /// </field>
    /// <field name="maxNumberOfItemsToGet" type="String" static="true">
    /// </field>
    /// <field name="minimumPopupWidth" type="String" static="true">
    /// </field>
    /// <field name="maximumPopupWidth" type="String" static="true">
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
    /// <field name="popupLeftOffset" type="String" static="true">
    /// </field>
    /// <field name="popupTopOffset" type="String" static="true">
    /// </field>
    /// <field name="rightAlign" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.HtmlAutoCompleteBehaviour

Js.ClientControls.HtmlAutoCompleteBehaviour = function Js_ClientControls_HtmlAutoCompleteBehaviour(input, hiddenInput, anchor, isSuggest, parametersHiddenField) {
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
    /// <field name="_popupLeftOffset" type="Number" integer="true">
    /// </field>
    /// <field name="_popupTopOffset" type="Number" integer="true">
    /// </field>
    /// <field name="_rightAlign" type="Boolean">
    /// </field>
    /// <field name="onSuggestionsRequested" type="Function">
    /// </field>
    /// <field name="_remoteSuggestionsGetter" type="Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter">
    /// </field>
    /// <field name="suggestions" type="Js.ClientControls.SuggestionsCollection">
    /// </field>
    /// <field name="onhighlight" type="Function">
    /// </field>
    /// <field name="itemChosen" type="Function">
    /// </field>
    /// <field name="onTextPasted" type="Function">
    /// </field>
    /// <field name="_input" type="Object" domElement="true">
    /// </field>
    /// <field name="_hiddenInput" type="Object" domElement="true">
    /// </field>
    /// <field name="_anchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_popupMenu" type="Js.ClientControls.PopupMenu">
    /// </field>
    /// <field name="_mode" type="Js.ClientControls._htmlAutoCompleteMode">
    /// </field>
    /// <field name="_watermarker" type="Js.ClientControls.WatermarkExtender">
    /// </field>
    /// <field name="parameters" type="Js.ClientControls.PairListField">
    /// </field>
    /// <field name="onFocus" type="Function">
    /// </field>
    /// <field name="transformReceivedSuggestions" type="Function">
    /// </field>
    /// <field name="_ajaxIcon" type="Object" domElement="true">
    /// </field>
    /// <field name="_currentTimer" type="Number" integer="true">
    /// </field>
    this.suggestions = new Js.ClientControls.SuggestionsCollection();
    this._currentTimer = -2;
    this._remoteSuggestionsGetter = new Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter(input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.webServiceUrl).value, input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.webServiceMethod).value);
    this._mode = (!!isSuggest) ? 1 : 2;
    this._input = input;
    this._hiddenInput = hiddenInput;
    this._anchor = (anchor == null) ? input : anchor;
    $(input).blur(ss.Delegate.create(this, function(e) {
        window.setTimeout(ss.Delegate.create(this, this._doBlur), 250);
    }));
    $(input).keydown(ss.Delegate.create(this, this._handleKeyDown));
    $(input).keyup(ss.Delegate.create(this, this._handleKeyUp));
    $(input).focus(ss.Delegate.create(this, this._callOnFocus));
    var waterMarkNode = input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.watermark);
    if (waterMarkNode != null) {
        this._watermarker = new Js.ClientControls.WatermarkExtender(input, input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.watermark).value);
    }
    var popupLeftNode = input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.popupLeftOffset);
    this._popupLeftOffset = (popupLeftNode == null) ? 0 : parseInt(popupLeftNode.value);
    var popupTopNode = input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.popupTopOffset);
    this._popupTopOffset = (popupTopNode == null) ? 0 : parseInt(popupTopNode.value);
    var rightAlignNode = input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.rightAlign);
    this._rightAlign = (rightAlignNode == null) ? false : Boolean.parse(rightAlignNode.value);
    if (input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.popupLeftOffset) != null) {
        this._popupLeftOffset = parseInt(input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.popupLeftOffset).value);
    }
    this.parameters = new Js.ClientControls.PairListField(parametersHiddenField);
    this.suggestions.onSuggestionsChanged = ss.Delegate.create(this, function() {
        this.displaySuggestionsInPopupMenu();
    });
    this._remoteSuggestionsGetter.onAllSuggestionsReceived = ss.Delegate.create(this, function() {
        this._removeLowPrioritySuggestionsAndSetRemainingSuggestionsToLowPriority();
        this._hideAjaxIcon();
    });
    this._remoteSuggestionsGetter.onSuggestionsRequested = ss.Delegate.create(this, function() {
        this._showAjaxIcon();
    });
    this._remoteSuggestionsGetter.onSuggestionReceived = ss.Delegate.create(this, function(newSuggestions) {
        Js.Library.Trace.write('Received' + newSuggestions.length + 'suggestions');
        if (this.transformReceivedSuggestions != null) {
            this._addSuggestions(this.transformReceivedSuggestions(newSuggestions, this.get__maxNumberOfItemsToGet()));
        }
        else {
            this._addSuggestions(newSuggestions);
        }
    });
    this._remoteSuggestionsGetter.onAbortCurrentRequest = ss.Delegate.create(this, function() {
        this._hideAjaxIcon();
    });
}
Js.ClientControls.HtmlAutoCompleteBehaviour.prototype = {
    _popupLeftOffset: 0,
    _popupTopOffset: 0,
    _rightAlign: false,
    onSuggestionsRequested: null,
    _remoteSuggestionsGetter: null,
    onhighlight: null,
    itemChosen: null,
    onTextPasted: null,
    _input: null,
    _hiddenInput: null,
    _anchor: null,
    _popupMenu: null,
    
    setWebMethod: function Js_ClientControls_HtmlAutoCompleteBehaviour$setWebMethod(methodName) {
        /// <param name="methodName" type="String">
        /// </param>
        this._remoteSuggestionsGetter.methodName = methodName;
    },
    
    _mode: 0,
    _watermarker: null,
    parameters: null,
    onFocus: null,
    
    _callOnFocus: function Js_ClientControls_HtmlAutoCompleteBehaviour$_callOnFocus(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(ss.Delegate.create(this, function() {
            this.focus();
            if (this.onFocus != null) {
                this.onFocus(e);
            }
        }));
    },
    
    transformReceivedSuggestions: null,
    _ajaxIcon: null,
    
    _hideAjaxIcon: function Js_ClientControls_HtmlAutoCompleteBehaviour$_hideAjaxIcon() {
        if (this._ajaxIcon != null) {
            this._ajaxIcon.style.display = 'none';
        }
    },
    
    _showAjaxIcon: function Js_ClientControls_HtmlAutoCompleteBehaviour$_showAjaxIcon() {
        if (this._ajaxIcon == null) {
            this._ajaxIcon = document.createElement('IMG');
            this._ajaxIcon.src = '/Gfx/autocomplete-loading.gif';
            this._ajaxIcon.style.height = '16px';
            this._ajaxIcon.style.width = '16px';
            this._ajaxIcon.style.position = 'absolute';
            var offset = $(this._anchor).offset();
            this._ajaxIcon.style.left = (offset.left + this._anchor.clientWidth - 18) + 'px';
            this._ajaxIcon.style.top = (offset.top + 2) + 'px';
            this._ajaxIcon.style.zIndex = 200;
            document.body.appendChild(this._ajaxIcon);
        }
        this._ajaxIcon.style.display = '';
    },
    
    addSuggestion: function Js_ClientControls_HtmlAutoCompleteBehaviour$addSuggestion(newSuggestion) {
        /// <param name="newSuggestion" type="Js.AutoCompleteLibrary.Suggestion">
        /// </param>
        this._addSuggestions([ newSuggestion ]);
    },
    
    _addSuggestions: function Js_ClientControls_HtmlAutoCompleteBehaviour$_addSuggestions(newSuggestions) {
        /// <param name="newSuggestions" type="Array" elementType="Suggestion">
        /// </param>
        Js.Library.Trace.write('Adding ' + newSuggestions.length + ' suggestions. Suggestions length = ' + this.suggestions.get_count());
        this.suggestions.addRange(newSuggestions);
        while (this.suggestions.get_count() > this.get__maxNumberOfItemsToGet()) {
            Js.Library.Trace.write('Suggestions length ' + this.suggestions.get_count() + ' Removing suggestion');
            this.suggestions.removeAt(this.suggestions.get_count() - 1);
        }
        Js.Library.Trace.write('Finished adding ' + newSuggestions.length + ' suggestions. Suggestions length = ' + this.suggestions.get_count());
    },
    
    displaySuggestionsInPopupMenu: function Js_ClientControls_HtmlAutoCompleteBehaviour$displaySuggestionsInPopupMenu() {
        Js.Library.Trace.write('DisplaySuggestionsInPopupMenu');
        this.get__popupMenu().clear();
        for (var i = 0; i < this.suggestions.get_count(); i++) {
            var suggestion = this.suggestions.get_item(i);
            var pair = new Js.ClientControls.KeyValuePair();
            pair.key = suggestion.html;
            var value = new Js.ClientControls.KeyValuePair();
            value.key = suggestion.text;
            value.value = (this._mode === 2) ? suggestion.value : suggestion.text;
            pair.value = value;
            this.get__popupMenu().addItem(pair);
        }
        this._highlightFirstSuggestion();
        this.get__popupMenu().show(this._anchor, this.get__minWidth(), this.get__maxWidth(), this._popupTopOffset, this._popupLeftOffset, this._rightAlign);
    },
    
    _highlightFirstSuggestion: function Js_ClientControls_HtmlAutoCompleteBehaviour$_highlightFirstSuggestion() {
        if (this._mode === 2 && this.get__popupMenu().get_currentlyHighlightedItem() == null) {
            this.get__popupMenu().set_indexOfCurrentlyHighlightedItem(0);
        }
    },
    
    _removeLowPrioritySuggestionsAndSetRemainingSuggestionsToLowPriority: function Js_ClientControls_HtmlAutoCompleteBehaviour$_removeLowPrioritySuggestionsAndSetRemainingSuggestionsToLowPriority() {
        this._removeLowPrioritySuggestions();
        this._setSuggestionsToLowPriority();
    },
    
    _removeLowPrioritySuggestions: function Js_ClientControls_HtmlAutoCompleteBehaviour$_removeLowPrioritySuggestions() {
        for (var i = 0; i < this.suggestions.get_count(); i++) {
            if (this.suggestions.get_item(i).priority === -1) {
                this.suggestions.removeAt(i);
                i--;
                continue;
            }
        }
    },
    
    _setSuggestionsToLowPriority: function Js_ClientControls_HtmlAutoCompleteBehaviour$_setSuggestionsToLowPriority() {
        for (var i = 0; i < this.suggestions.get_count(); i++) {
            this.suggestions.get_item(i).priority = -1;
        }
    },
    
    get__popupMenu: function Js_ClientControls_HtmlAutoCompleteBehaviour$get__popupMenu() {
        /// <value type="Js.ClientControls.PopupMenu"></value>
        if (this._popupMenu == null) {
            var cssAtt = this._input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.popupMenuClassName);
            var highlightedCssAtt = this._input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName);
            this._popupMenu = new Js.ClientControls.PopupMenu((cssAtt == null) ? Js.ClientControls.HtmlAutoCompleteAttributes.popupMenuClassName : cssAtt.value, (highlightedCssAtt == null) ? Js.ClientControls.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName : highlightedCssAtt.value);
            this._popupMenu.itemClick = ss.Delegate.create(this, this._itemSelected);
            this._popupMenu.itemHighlighted = this.onhighlight;
        }
        return this._popupMenu;
    },
    
    _doBlur: function Js_ClientControls_HtmlAutoCompleteBehaviour$_doBlur() {
        if (!this._hiddenInput.value) {
            this._cancel();
        }
        else {
            this.get__popupMenu().hide();
        }
    },
    
    _handleKeyDown: function Js_ClientControls_HtmlAutoCompleteBehaviour$_handleKeyDown(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (e.which === 38) {
            if (this.get__popupMenu() != null) {
                this.get__popupMenu().set_indexOfCurrentlyHighlightedItem(this.get__popupMenu().get_indexOfCurrentlyHighlightedItem() - 1);
            }
        }
        else if (e.which === 40) {
            if (this.get__popupMenu() != null) {
                this.get__popupMenu().set_indexOfCurrentlyHighlightedItem(this.get__popupMenu().get_indexOfCurrentlyHighlightedItem() + 1);
            }
        }
        else if (e.which === 27) {
            if (this.get__popupMenu().get_currentlyHighlightedItem() != null) {
                this.clear();
                e.preventDefault();
            }
            else {
                this._cancel();
            }
        }
        else if ((e.which === 9 && !e.shiftKey) || e.which === 13) {
            if (this._mode === 1) {
                if (this.get__suggestionIsHighlighted()) {
                    e.preventDefault();
                    this._itemSelected(this.get__popupMenu().get_currentlyHighlightedItem());
                    this.get__popupMenu().clear();
                    return;
                }
                else {
                    if (this.get__validSelectionHasBeenMade()) {
                        var pair = new Js.ClientControls.KeyValuePair();
                        pair.key = this._input.value;
                        pair.value = this._input.value;
                        this._itemSelected(pair);
                        return;
                    }
                }
            }
            else if (this._mode === 2) {
                if (this.get__suggestionIsHighlighted() && !this.get__validSelectionHasBeenMade()) {
                    if (e.which === 13) {
                        e.preventDefault();
                    }
                    this._itemSelected(this.get__popupMenu().get_currentlyHighlightedItem());
                    return;
                }
            }
        }
        else if (e.which === 8 && this._mode === 2) {
            this.suggestions.clear();
            if (this.get__validSelectionHasBeenMade()) {
                this._hiddenInput.value = '';
                this._input.value = '';
            }
            else {
                this._hiddenInput.value = '';
                this.requestSuggestions();
            }
        }
        else if (e.which === 46 && this._mode === 2) {
            this._hiddenInput.value = '';
            this.requestSuggestions();
        }
    },
    
    _handleKeyUp: function Js_ClientControls_HtmlAutoCompleteBehaviour$_handleKeyUp(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (e.which !== 40 && e.which !== 35 && e.which !== 13 && e.which !== 27 && e.which !== 36 && e.which !== 37 && e.which !== 34 && e.which !== 33 && e.which !== 38) {
            if (this._mode === 1) {
                this._hiddenInput.value = this._input.value;
            }
            this.requestSuggestions();
        }
    },
    
    requestSuggestions: function Js_ClientControls_HtmlAutoCompleteBehaviour$requestSuggestions() {
        this._setSuggestionsToLowPriority();
        if (this._mode === 2) {
            this.set_value('');
        }
        if (this.onSuggestionsRequested != null) {
            Js.Library.Trace.write('OnSuggestionsRequested');
            this.onSuggestionsRequested();
        }
        if (this._currentTimer !== -2) {
            window.clearTimeout(this._currentTimer);
        }
        if (this._input.value.trim().length > 0) {
            this._currentTimer = window.setTimeout(ss.Delegate.create(this, function(o) {
                this._remoteSuggestionsGetter._requestSuggestions(this._input.value, this.parameters._toDictionary(), this.get__maxNumberOfItemsToGet());
            }), 200);
        }
    },
    
    get__validSelectionHasBeenMade: function Js_ClientControls_HtmlAutoCompleteBehaviour$get__validSelectionHasBeenMade() {
        /// <value type="Boolean"></value>
        return this._hiddenInput.value != null && !!this._hiddenInput.value;
    },
    
    get__suggestionIsHighlighted: function Js_ClientControls_HtmlAutoCompleteBehaviour$get__suggestionIsHighlighted() {
        /// <value type="Boolean"></value>
        return this.get__popupMenu().get_currentlyHighlightedItem() != null;
    },
    
    _cancel: function Js_ClientControls_HtmlAutoCompleteBehaviour$_cancel() {
        this.clear();
        this._input.value = '';
        this._hiddenInput.value = '';
        this._remoteSuggestionsGetter._abortCurrentRequest();
    },
    
    clear: function Js_ClientControls_HtmlAutoCompleteBehaviour$clear() {
        this.suggestions.clear();
        this.get__popupMenu().clear();
        this._remoteSuggestionsGetter._abortCurrentRequest();
    },
    
    get__maxNumberOfItemsToGet: function Js_ClientControls_HtmlAutoCompleteBehaviour$get__maxNumberOfItemsToGet() {
        /// <value type="Number" integer="true"></value>
        var att = this._input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.maxNumberOfItemsToGet);
        return (att == null) ? 10 : parseInt(att.value);
    },
    
    get__minWidth: function Js_ClientControls_HtmlAutoCompleteBehaviour$get__minWidth() {
        /// <value type="Number" integer="true"></value>
        var att = this._input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.minimumPopupWidth);
        return (att == null) ? 0 : parseInt(att.value);
    },
    
    get__maxWidth: function Js_ClientControls_HtmlAutoCompleteBehaviour$get__maxWidth() {
        /// <value type="Number" integer="true"></value>
        var att = this._input.getAttributeNode(Js.ClientControls.HtmlAutoCompleteAttributes.maximumPopupWidth);
        return (att == null) ? 0 : parseInt(att.value);
    },
    
    _itemSelected: function Js_ClientControls_HtmlAutoCompleteBehaviour$_itemSelected(value) {
        /// <param name="value" type="Object">
        /// </param>
        this._remoteSuggestionsGetter._abortCurrentRequest();
        var pair = value;
        this._input.value = pair.key;
        this._hiddenInput.value = pair.value;
        this.clear();
        if (this.itemChosen != null) {
            this.itemChosen(pair);
        }
        var att = this._input.getAttributeNode('AutoPostBackFunction');
        if (att != null) {
            eval(att.value);
        }
    },
    
    get_text: function Js_ClientControls_HtmlAutoCompleteBehaviour$get_text() {
        /// <value type="String"></value>
        return this._input.value;
    },
    set_text: function Js_ClientControls_HtmlAutoCompleteBehaviour$set_text(value) {
        /// <value type="String"></value>
        this._input.value = value;
        return value;
    },
    
    get_value: function Js_ClientControls_HtmlAutoCompleteBehaviour$get_value() {
        /// <value type="String"></value>
        return this._hiddenInput.value;
    },
    set_value: function Js_ClientControls_HtmlAutoCompleteBehaviour$set_value(value) {
        /// <value type="String"></value>
        this._hiddenInput.value = value;
        return value;
    },
    
    get_currentlyHighlighedItem: function Js_ClientControls_HtmlAutoCompleteBehaviour$get_currentlyHighlighedItem() {
        /// <value type="Object"></value>
        return this.get__popupMenu().get_currentlyHighlightedItem();
    },
    
    focus: function Js_ClientControls_HtmlAutoCompleteBehaviour$focus() {
        this._input.focus();
        if (this._watermarker != null) {
            this._watermarker.onFocus(null);
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.KeyValuePair

Js.ClientControls.KeyValuePair = function Js_ClientControls_KeyValuePair() {
    /// <field name="key" type="String">
    /// </field>
    /// <field name="value" type="Object">
    /// </field>
}
Js.ClientControls.KeyValuePair.prototype = {
    key: null,
    value: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.KeyStringPair

Js.ClientControls.KeyStringPair = function Js_ClientControls_KeyStringPair() {
    /// <field name="key" type="String">
    /// </field>
    /// <field name="value" type="String">
    /// </field>
}
Js.ClientControls.KeyStringPair.prototype = {
    key: null,
    value: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.MultiSelectorAttributes

Js.ClientControls.MultiSelectorAttributes = function Js_ClientControls_MultiSelectorAttributes() {
    /// <field name="selections" type="String" static="true">
    /// </field>
    /// <field name="multiSelectorDelButtonCss" type="String" static="true">
    /// </field>
    /// <field name="multiSelectorListBoxCss" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.MultiSelectorBehaviour

Js.ClientControls.MultiSelectorBehaviour = function Js_ClientControls_MultiSelectorBehaviour(container, htmlAutoComplete, hiddenOutput) {
    /// <param name="container" type="Object" domElement="true">
    /// </param>
    /// <param name="htmlAutoComplete" type="Js.ClientControls.HtmlAutoCompleteBehaviour">
    /// </param>
    /// <param name="hiddenOutput" type="Object" domElement="true">
    /// </param>
    /// <field name="itemAdded" type="Function">
    /// </field>
    /// <field name="itemRemoved" type="Function">
    /// </field>
    /// <field name="htmlAutoComplete" type="Js.ClientControls.HtmlAutoCompleteBehaviour">
    /// </field>
    /// <field name="container" type="Object" domElement="true">
    /// </field>
    /// <field name="_hiddenOutput" type="Object" domElement="true">
    /// </field>
    /// <field name="_selections" type="Js.ClientControls.PairListField">
    /// </field>
    this.container = container;
    this._hiddenOutput = hiddenOutput;
    this.htmlAutoComplete = htmlAutoComplete;
    this.htmlAutoComplete.itemChosen = ss.Delegate.create(this, this._htmlAutoComplete_ItemChosen);
    this._selections = new Js.ClientControls.PairListField(hiddenOutput);
    this._initialiseInitialSelections();
    $(this.container).click(ss.Delegate.create(this, this._onClick));
}
Js.ClientControls.MultiSelectorBehaviour.prototype = {
    itemAdded: null,
    itemRemoved: null,
    htmlAutoComplete: null,
    container: null,
    _hiddenOutput: null,
    _selections: null,
    
    _initialiseInitialSelections: function Js_ClientControls_MultiSelectorBehaviour$_initialiseInitialSelections() {
        this._selections._clear();
        for (var i = 0; i < this.container.childNodes.length; i++) {
            var child = this.container.childNodes[i];
            if (child.nodeType === 1 && child.childNodes.length > 1 && child.childNodes[0].tagName !== 'INPUT') {
                $(child.childNodes[1]).click(ss.Delegate.create(this, this._deleteButtonClick));
                this._selections.set(child.childNodes[0].nodeValue, child.getAttributeNode('val').value);
            }
        }
    },
    
    _onClick: function Js_ClientControls_MultiSelectorBehaviour$_onClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this.htmlAutoComplete.focus();
    },
    
    addItem: function Js_ClientControls_MultiSelectorBehaviour$addItem(text, value) {
        /// <param name="text" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        if (this._selections.containsKey(text)) {
            return;
        }
        var item = document.createElement('LI');
        var itemCssAttribute = this.container.getAttributeNode('MultiSelectorListBoxItem');
        item.className = (itemCssAttribute != null) ? itemCssAttribute.value : 'MultiSelectorListBoxItem';
        var deleteButton = document.createElement('SPAN');
        deleteButton.innerHTML = '&nbsp;&nbsp;';
        var delCssAttribute = this.container.getAttributeNode('MultiSelectorDelButton');
        deleteButton.className = (delCssAttribute != null) ? delCssAttribute.value : 'MultiSelectorDelButton';
        $(deleteButton).click(ss.Delegate.create(this, this._deleteButtonClick));
        item.appendChild(deleteButton);
        var textSpan = document.createElement('SPAN');
        textSpan.innerHTML = text;
        item.appendChild(textSpan);
        var textAtt = document.createAttribute('text');
        textAtt.value = text;
        item.setAttributeNode(textAtt);
        var valueAtt = document.createAttribute('val');
        valueAtt.value = value;
        item.setAttributeNode(valueAtt);
        var childToInsertBefore = this.container.lastChild;
        while (!childToInsertBefore.childNodes.length || childToInsertBefore.childNodes[0].tagName !== 'INPUT') {
            childToInsertBefore = childToInsertBefore.previousSibling;
        }
        this.container.insertBefore(item, childToInsertBefore);
        this._selections.set(text, value);
        if (this.itemAdded != null) {
            this.itemAdded(text, value);
        }
    },
    
    _htmlAutoComplete_ItemChosen: function Js_ClientControls_MultiSelectorBehaviour$_htmlAutoComplete_ItemChosen(item) {
        /// <param name="item" type="Js.ClientControls.KeyStringPair">
        /// </param>
        this.addItem(item.key, item.value);
        this.htmlAutoComplete.set_text('');
        this.htmlAutoComplete.set_value('');
        this.htmlAutoComplete.focus();
    },
    
    _deleteButtonClick: function Js_ClientControls_MultiSelectorBehaviour$_deleteButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        $(e.target).unbind('click').unbind('mouseover');
        this.removeItem(e.target.parentNode);
    },
    
    removeItem: function Js_ClientControls_MultiSelectorBehaviour$removeItem(item) {
        /// <param name="item" type="Object" domElement="true">
        /// </param>
        var text = item.getAttributeNode('text').value;
        var value = item.getAttributeNode('val').value;
        this._selections.set(text, null);
        item.parentNode.removeChild(item);
        this.htmlAutoComplete.focus();
        if (this.itemRemoved != null) {
            this.itemRemoved(text, value);
        }
    },
    
    getSelections: function Js_ClientControls_MultiSelectorBehaviour$getSelections() {
        /// <returns type="Js.ClientControls.PairListField"></returns>
        return this._selections;
    },
    
    clear: function Js_ClientControls_MultiSelectorBehaviour$clear() {
        for (var i = this.container.childNodes.length - 1; i >= 0; i--) {
            var child = this.container.childNodes[i];
            if (child.nodeType === 1 && child.childNodes[0].tagName !== 'INPUT') {
                $(child.childNodes[1]).unbind('click').unbind('mouseover');
                this._selections.set(child.childNodes[0].nodeValue, null);
                child.parentNode.removeChild(child);
            }
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.ClientControls.PairListField

Js.ClientControls.PairListField = function Js_ClientControls_PairListField(hiddenField) {
    /// <param name="hiddenField" type="Object" domElement="true">
    /// </param>
    /// <field name="_hiddenField" type="Object" domElement="true">
    /// </field>
    this._hiddenField = hiddenField;
}
Js.ClientControls.PairListField.prototype = {
    _hiddenField: null,
    
    _add: function Js_ClientControls_PairListField$_add(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        if (this._hiddenField.value.length > 0) {
            this._hiddenField.value += ';';
        }
        this._hiddenField.value += escape(key) + ':' + escape(value);
    },
    
    set: function Js_ClientControls_PairListField$set(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="Object">
        /// </param>
        this._removeByKey(key);
        if (value != null) {
            if (this._hiddenField.value.length > 0) {
                this._hiddenField.value += ';';
            }
            if (Type.canCast(value, Date)) {
                this._hiddenField.value += escape(key) + ':' + escape((value).toDateString());
            }
            else {
                this._hiddenField.value += escape(key) + ':' + escape(value.toString());
            }
        }
    },
    
    removeAt: function Js_ClientControls_PairListField$removeAt(index) {
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
    
    get_count: function Js_ClientControls_PairListField$get_count() {
        /// <value type="Number" integer="true"></value>
        if (this._hiddenField.value.length > 0) {
            return this._hiddenField.value.split(';').length;
        }
        else {
            return 0;
        }
    },
    
    _removeByKey: function Js_ClientControls_PairListField$_removeByKey(key) {
        /// <param name="key" type="String">
        /// </param>
        this.removeAt(this.indexOf(key));
    },
    
    containsKey: function Js_ClientControls_PairListField$containsKey(key) {
        /// <param name="key" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.indexOf(key) > -1;
    },
    
    indexOf: function Js_ClientControls_PairListField$indexOf(key) {
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
    
    _writeValuesToHiddenInput: function Js_ClientControls_PairListField$_writeValuesToHiddenInput(array) {
        /// <param name="array" type="Array">
        /// </param>
        this._hiddenField.value = '';
        var tempArray = [];
        for (var i = 0; i < array.length; i++) {
            var pair = array[i];
            tempArray[i] = escape(pair[0]) + ':' + escape(pair[1]);
        }
        this._hiddenField.value = tempArray.join(';');
    },
    
    toArray: function Js_ClientControls_PairListField$toArray() {
        /// <returns type="Array"></returns>
        var array = [];
        if (this._hiddenField.value.length > 0) {
            var pairs = this._hiddenField.value.split(';');
            for (var i = 0; i < pairs.length; i++) {
                var pair = pairs[i].split(':');
                pair[0] = unescape(pair[0]);
                pair[1] = unescape(pair[1]);
                array[array.length] = pair;
            }
        }
        return array;
    },
    
    _clear: function Js_ClientControls_PairListField$_clear() {
        this._hiddenField.value = '';
    },
    
    _toDictionary: function Js_ClientControls_PairListField$_toDictionary() {
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
// Js.ClientControls.PopupMenu

Js.ClientControls.PopupMenu = function Js_ClientControls_PopupMenu(cssClass, highlightedItemCssClass) {
    /// <param name="cssClass" type="String">
    /// </param>
    /// <param name="highlightedItemCssClass" type="String">
    /// </param>
    /// <field name="itemClick" type="Function">
    /// </field>
    /// <field name="itemHighlighted" type="Function">
    /// </field>
    /// <field name="_container" type="Object" domElement="true">
    /// </field>
    /// <field name="_highlightedItemCssClass" type="String">
    /// </field>
    /// <field name="_indexOfCurrentlyHighlightedItem" type="Number" integer="true">
    /// </field>
    this._indexOfCurrentlyHighlightedItem = -1;
    this._container = document.createElement('DIV');
    this._container.className = (cssClass == null) ? 'PopupMenu' : cssClass;
    this._highlightedItemCssClass = (highlightedItemCssClass == null) ? 'PopupMenuHighlightedItem' : highlightedItemCssClass;
    this._container.style.display = 'none';
    document.body.appendChild(this._container);
}
Js.ClientControls.PopupMenu.prototype = {
    itemClick: null,
    itemHighlighted: null,
    _container: null,
    _highlightedItemCssClass: null,
    
    addItem: function Js_ClientControls_PopupMenu$addItem(pair) {
        /// <param name="pair" type="Js.ClientControls.KeyValuePair">
        /// </param>
        var div = document.createElement('DIV');
        var divAsArray = div;
        divAsArray['value'] = pair.value;
        if (pair.key.indexOf('<') > -1) {
            div.innerHTML = pair.key;
        }
        else {
            div.innerHTML = '<div class="DefaultPopupMenuItem">' + pair.key + '</div>';
        }
        this._container.appendChild(div);
        $(div).click(ss.Delegate.create(this, this._itemClickHandler));
        $(div).mouseover(ss.Delegate.create(this, this._itemMouseEnterHandler));
    },
    
    _itemClickHandler: function Js_ClientControls_PopupMenu$_itemClickHandler(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (this.itemClick != null) {
            this.itemClick(this.get_currentlyHighlightedItem());
        }
    },
    
    _itemMouseEnterHandler: function Js_ClientControls_PopupMenu$_itemMouseEnterHandler(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var el = e.target;
        while (el.parentNode !== this._container) {
            el = el.parentNode;
        }
        for (var i = 0; i < this._container.childNodes.length; i++) {
            if (this._container.childNodes[i] === el) {
                this.set_indexOfCurrentlyHighlightedItem(i);
                return;
            }
        }
    },
    
    clear: function Js_ClientControls_PopupMenu$clear() {
        while (this._container.childNodes.length > 0) {
            $(this._container.childNodes[0]).unbind('click').unbind('mouseover');
            this._container.removeChild(this._container.childNodes[0]);
        }
        this._indexOfCurrentlyHighlightedItem = -1;
        this._container.style.display = 'none';
    },
    
    show: function Js_ClientControls_PopupMenu$show(anchor, minWidth, maxWidth, topOffset, leftOffset, rightAlign) {
        /// <param name="anchor" type="Object" domElement="true">
        /// </param>
        /// <param name="minWidth" type="Number" integer="true">
        /// </param>
        /// <param name="maxWidth" type="Number" integer="true">
        /// </param>
        /// <param name="topOffset" type="Number" integer="true">
        /// </param>
        /// <param name="leftOffset" type="Number" integer="true">
        /// </param>
        /// <param name="rightAlign" type="Boolean">
        /// </param>
        this._container.style.display = (this._container.childNodes.length > 0) ? '' : 'none';
        if (this._container.childNodes.length > 0) {
            var jq = $(anchor);
            var anchorOffsetHeight = (anchor == null) ? 0 : anchor.offsetHeight;
            var anchorOffsetWidth = (anchor == null) ? 0 : anchor.offsetWidth;
            if (!minWidth && !maxWidth) {
                minWidth = anchorOffsetWidth;
                maxWidth = anchorOffsetWidth;
            }
            var nudgeLeft = ($.browser.msie) ? -2 : 0;
            var nudgeTop = ($.browser.msie) ? -2 : 0;
            var nudgeWidth = ($.browser.msie) ? 0 : -2;
            this._container.style.left = jq.offset().left + nudgeLeft + leftOffset + 'px';
            this._container.style.top = jq.offset().top + anchorOffsetHeight + nudgeTop - 1 + topOffset + 'px';
            this._container.style.width = 'auto';
            if (this._container.offsetWidth < minWidth) {
                this._container.style.width = minWidth + nudgeWidth + 'px';
            }
            else if (this._container.offsetWidth > maxWidth) {
                this._container.style.width = maxWidth + nudgeWidth + 'px';
            }
            if (rightAlign) {
                Js.Library.Trace.write('' + jq.offset().left + ', ' + nudgeLeft + ', ' + anchor.clientWidth + ', ' + this._container.clientWidth);
                this._container.style.left = (jq.offset().left + nudgeLeft + anchor.clientWidth - this._container.clientWidth) + 'px';
            }
        }
    },
    
    hide: function Js_ClientControls_PopupMenu$hide() {
        eval('try{htm();}catch(e){}');
        this._container.style.display = 'none';
    },
    
    get_indexOfCurrentlyHighlightedItem: function Js_ClientControls_PopupMenu$get_indexOfCurrentlyHighlightedItem() {
        /// <value type="Number" integer="true"></value>
        return (this._indexOfCurrentlyHighlightedItem < this._container.childNodes.length) ? this._indexOfCurrentlyHighlightedItem : -1;
    },
    set_indexOfCurrentlyHighlightedItem: function Js_ClientControls_PopupMenu$set_indexOfCurrentlyHighlightedItem(value) {
        /// <value type="Number" integer="true"></value>
        if (this.get__currentlyHighlightedElement() != null) {
            this.get__currentlyHighlightedElement().className = '';
        }
        this._indexOfCurrentlyHighlightedItem = (value + this._container.childNodes.length) % this._container.childNodes.length;
        if (this.get__currentlyHighlightedElement() != null) {
            this.get__currentlyHighlightedElement().className = this._highlightedItemCssClass;
            if (this.itemHighlighted != null) {
                this.itemHighlighted();
            }
        }
        return value;
    },
    
    get_currentlyHighlightedItem: function Js_ClientControls_PopupMenu$get_currentlyHighlightedItem() {
        /// <value type="Object"></value>
        if (this.get__currentlyHighlightedElement() == null) {
            return null;
        }
        else {
            return (this.get__currentlyHighlightedElement())['value'];
        }
    },
    
    get__currentlyHighlightedElement: function Js_ClientControls_PopupMenu$get__currentlyHighlightedElement() {
        /// <value type="Object" domElement="true"></value>
        if (this.get_indexOfCurrentlyHighlightedItem() === -1) {
            return null;
        }
        else {
            return this._container.childNodes[this.get_indexOfCurrentlyHighlightedItem()];
        }
    },
    
    _findPosLeft: function Js_ClientControls_PopupMenu$_findPosLeft(el) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var curleft = 0;
        if (el.offsetParent != null) {
            do {
                curleft += el.offsetLeft;
                el = el.offsetParent;
            } while (el != null);
        }
        return curleft;
    },
    
    _findPosTop: function Js_ClientControls_PopupMenu$_findPosTop(el) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var curtop = 0;
        if (el.offsetParent != null) {
            do {
                curtop += el.offsetTop;
                el = el.offsetParent;
            } while (el != null);
        }
        return curtop;
    }
}


Js.ClientControls.HtmlAutoComplete.RemoteSuggestionsGetter.registerClass('Js.ClientControls.HtmlAutoComplete.RemoteSuggestionsGetter');
Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter.registerClass('Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter', Js.ClientControls.HtmlAutoComplete.RemoteSuggestionsGetter);
Js.ClientControls.SuggestionsCollection.registerClass('Js.ClientControls.SuggestionsCollection');
Js.ClientControls.SuggestionsGetter.registerClass('Js.ClientControls.SuggestionsGetter');
Js.ClientControls.WatermarkExtender.registerClass('Js.ClientControls.WatermarkExtender');
Js.ClientControls.HtmlAutoCompleteAttributes.registerClass('Js.ClientControls.HtmlAutoCompleteAttributes');
Js.ClientControls.HtmlAutoCompleteBehaviour.registerClass('Js.ClientControls.HtmlAutoCompleteBehaviour');
Js.ClientControls.KeyValuePair.registerClass('Js.ClientControls.KeyValuePair');
Js.ClientControls.KeyStringPair.registerClass('Js.ClientControls.KeyStringPair');
Js.ClientControls.MultiSelectorAttributes.registerClass('Js.ClientControls.MultiSelectorAttributes');
Js.ClientControls.MultiSelectorBehaviour.registerClass('Js.ClientControls.MultiSelectorBehaviour');
Js.ClientControls.PairListField.registerClass('Js.ClientControls.PairListField');
Js.ClientControls.PopupMenu.registerClass('Js.ClientControls.PopupMenu');
Js.ClientControls.HtmlAutoCompleteAttributes.webServiceUrl = 'WebServiceUrl';
Js.ClientControls.HtmlAutoCompleteAttributes.webServiceMethod = 'WebServiceMethod';
Js.ClientControls.HtmlAutoCompleteAttributes.maxNumberOfItemsToGet = 'MaxNumberOfItemsToGet';
Js.ClientControls.HtmlAutoCompleteAttributes.minimumPopupWidth = 'MinimumPopupWidth';
Js.ClientControls.HtmlAutoCompleteAttributes.maximumPopupWidth = 'MaximumPopupWidth';
Js.ClientControls.HtmlAutoCompleteAttributes.popupMenuClassName = 'PopupMenu';
Js.ClientControls.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName = 'PopupMenuHighlightedItem';
Js.ClientControls.HtmlAutoCompleteAttributes.excludedItems = 'ExcludedItems';
Js.ClientControls.HtmlAutoCompleteAttributes.listBoxItemValue = 'ListBoxItemValue';
Js.ClientControls.HtmlAutoCompleteAttributes.onselectionmade = 'onselectionmade';
Js.ClientControls.HtmlAutoCompleteAttributes.onhighlight = 'onhighlight';
Js.ClientControls.HtmlAutoCompleteAttributes.onpopupcancel = 'onpopupcancel';
Js.ClientControls.HtmlAutoCompleteAttributes.watermark = 'Watermark';
Js.ClientControls.HtmlAutoCompleteAttributes.popupLeftOffset = 'PopupLeftOffset';
Js.ClientControls.HtmlAutoCompleteAttributes.popupTopOffset = 'PopupTopOffset';
Js.ClientControls.HtmlAutoCompleteAttributes.rightAlign = 'RightAlign';
Js.ClientControls.MultiSelectorAttributes.selections = 'Selections';
Js.ClientControls.MultiSelectorAttributes.multiSelectorDelButtonCss = 'MultiSelectorDelButton';
Js.ClientControls.MultiSelectorAttributes.multiSelectorListBoxCss = 'MultiSelectorListBoxItem';
})(jQuery);

//! This script was generated using Script# v0.7.4.0
