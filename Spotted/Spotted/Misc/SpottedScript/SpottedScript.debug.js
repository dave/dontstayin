// SpottedScript.js
//
Type.registerNamespace('Model.Entities');

////////////////////////////////////////////////////////////////////////////////
// Model.Entities.ObjectType
Model.Entities.ObjectType = function() { 
    /// <field name="photo" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="event" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="venue" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="place" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="none" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="thread" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="country" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="article" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="para" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="brand" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="promoter" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="mainPage" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="usr" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="region" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="gallery" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="group" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="banner" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="guestlistCredit" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="ticket" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="insertionOrder" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="emailSpotlight" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="campaignCredit" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="invoice" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="comp" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="misc" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="usrDonationIcon" type="Number" integer="true" static="true">
    /// </field>
};
Model.Entities.ObjectType.prototype = {
    photo: 1, 
    event: 2, 
    venue: 3, 
    place: 4, 
    none: 5, 
    thread: 6, 
    country: 7, 
    article: 8, 
    para: 9, 
    brand: 10, 
    promoter: 11, 
    mainPage: 999, 
    usr: 12, 
    region: 13, 
    gallery: 14, 
    group: 15, 
    banner: 16, 
    guestlistCredit: 17, 
    ticket: 18, 
    insertionOrder: 19, 
    emailSpotlight: 20, 
    campaignCredit: 21, 
    invoice: 22, 
    comp: 23, 
    misc: 24, 
    usrDonationIcon: 25
}
Model.Entities.ObjectType.registerEnum('Model.Entities.ObjectType', false);
Type.registerNamespace('SpottedScript.Pages.MapBrowser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MapBrowser.MapItem
SpottedScript.Pages.MapBrowser.MapItem = function SpottedScript_Pages_MapBrowser_MapItem() {
    /// <field name="data" type="Object">
    /// </field>
    /// <field name="lat" type="Number">
    /// </field>
    /// <field name="lon" type="Number">
    /// </field>
    /// <field name="hoverText" type="String">
    /// </field>
}
SpottedScript.Pages.MapBrowser.MapItem.prototype = {
    data: null,
    lat: 0,
    lon: 0,
    hoverText: null
}
Type.registerNamespace('SpottedScript.Behaviours.CreateUsersFromEmails');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Behaviours.CreateUsersFromEmails.Controller
SpottedScript.Behaviours.CreateUsersFromEmails.Controller = function SpottedScript_Behaviours_CreateUsersFromEmails_Controller(selector) {
    /// <param name="selector" type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour">
    /// </param>
    /// <field name="_oldOnSuggestionsRequested" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="_selector" type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour">
    /// </field>
    /// <field name="_emailsRegex" type="String" static="true">
    /// </field>
    this._selector = selector;
    this._oldOnSuggestionsRequested = selector.onSuggestionsRequested;
    selector.onSuggestionsRequested = Function.createDelegate(this, this._checkForEmailAddresses);
}
SpottedScript.Behaviours.CreateUsersFromEmails.Controller._getPicTitleDetailTemplateHtml = function SpottedScript_Behaviours_CreateUsersFromEmails_Controller$_getPicTitleDetailTemplateHtml(imageSrc, title, detail) {
    /// <param name="imageSrc" type="String">
    /// </param>
    /// <param name="title" type="String">
    /// </param>
    /// <param name="detail" type="String">
    /// </param>
    /// <returns type="String"></returns>
    return String.format('<table cellspacing=\'0\' cellpadding=\'0\'><tr><td width=\'40\'><img src=\'{0}\' border=0 width=40 hspace=0 height=40 /></td><td style=\'padding:3px;\' valign=\'top\'><b>{1}</b><br />{2}</td></tr></table>', imageSrc, title, detail);
}
SpottedScript.Behaviours.CreateUsersFromEmails.Controller.prototype = {
    _oldOnSuggestionsRequested: null,
    _selector: null,
    _checkForEmailAddresses: function SpottedScript_Behaviours_CreateUsersFromEmails_Controller$_checkForEmailAddresses() {
        Utils.Trace.write('CheckingForEmailAddresses');
        var regExp = new RegExp('\\s|,|;| +', 'g');
        var text = this._selector.get_text().replace(regExp, ' ');
        while (text.indexOf('  ') > -1) {
            text = text.replace('  ', ' ');
        }
        text = text.trim();
        for (var i = this._selector.suggestions.get_count() - 1; i > -1; i--) {
            Utils.Trace.write(this._selector.suggestions.get_item(i).value);
            if (this._selector.suggestions.get_item(i).value.startsWith('{\'emails\': ')) {
                this._selector.suggestions.removeAt(i);
                break;
            }
        }
        var regex = new RegExp(SpottedScript.Behaviours.CreateUsersFromEmails.Controller._emailsRegex);
        if (regex.test(text)) {
            this._selector.set_text(text);
            this._selector.suggestions.clear();
            var emails = text.split(' ');
            var suggestion = new ScriptSharpLibrary.Suggestion();
            suggestion.html = SpottedScript.Behaviours.CreateUsersFromEmails.Controller._getPicTitleDetailTemplateHtml('/gfx/icon40-inbox.png', 'Add ' + emails.length + ' email addresses as buddies', 'Next time you want to include these email addresses, just add all your buddies and they will be included.');
            suggestion.text = emails.length + ' email addresses as buddies';
            suggestion.value = '{\'emails\': \'' + escape(text) + '\', \'buddies\':\'true\'}';
            suggestion.priority = this._selector.get_text().length * 100;
            this._selector.addSuggestion(suggestion);
            var suggestion2 = new ScriptSharpLibrary.Suggestion();
            suggestion2.html = SpottedScript.Behaviours.CreateUsersFromEmails.Controller._getPicTitleDetailTemplateHtml('/gfx/icon40-inbox.png', 'Add ' + emails.length + ' email addresses, but NOT as buddies', 'Next time you want to include these email addresses you\'ll have to copy them in again');
            suggestion2.text = emails.length + ' email addresses';
            suggestion2.value = '{\'emails\': \'' + escape(text) + '\', \'buddies\':\'false\'}';
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
// SpottedScript.Behaviours.CreateUsersFromEmails._emailsSuggestionValue
SpottedScript.Behaviours.CreateUsersFromEmails._emailsSuggestionValue = function SpottedScript_Behaviours_CreateUsersFromEmails__emailsSuggestionValue() {
    /// <field name="emails" type="String">
    /// </field>
    /// <field name="buddies" type="Boolean">
    /// </field>
}
SpottedScript.Behaviours.CreateUsersFromEmails._emailsSuggestionValue.prototype = {
    emails: null,
    buddies: false
}
Type.registerNamespace('SpottedScript.Behaviours.CreateUserFromEmail');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Behaviours.CreateUserFromEmail.Controller
SpottedScript.Behaviours.CreateUserFromEmail.Controller = function SpottedScript_Behaviours_CreateUserFromEmail_Controller(selector) {
    /// <param name="selector" type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour">
    /// </param>
    /// <field name="_oldItemChosen" type="ScriptSharpLibrary.KeyStringPairAction">
    /// </field>
    /// <field name="_selector" type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour">
    /// </field>
    /// <field name="_oldOnSuggestionsRequested" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="_emailRegex" type="String" static="true">
    /// </field>
    this._selector = selector;
    this._oldOnSuggestionsRequested = selector.onSuggestionsRequested;
    selector.onSuggestionsRequested = Function.createDelegate(this, this._checkForEmailAddress);
}
SpottedScript.Behaviours.CreateUserFromEmail.Controller._getPicTitleDetailTemplateHtml = function SpottedScript_Behaviours_CreateUserFromEmail_Controller$_getPicTitleDetailTemplateHtml(imageSrc, title, detail) {
    /// <param name="imageSrc" type="String">
    /// </param>
    /// <param name="title" type="String">
    /// </param>
    /// <param name="detail" type="String">
    /// </param>
    /// <returns type="String"></returns>
    return String.format('<table cellspacing=\'0\' cellpadding=\'0\'><tr><td width=\'40\'><img src=\'{0}\' border=0 width=40 hspace=0 height=40 /></td><td style=\'padding:3px;\' valign=\'top\'><b>{1}</b><br />{2}</td></tr></table>', imageSrc, title, detail);
}
SpottedScript.Behaviours.CreateUserFromEmail.Controller.prototype = {
    _oldItemChosen: null,
    _selector: null,
    _oldOnSuggestionsRequested: null,
    _itemChosen: function SpottedScript_Behaviours_CreateUserFromEmail_Controller$_itemChosen(item) {
        /// <param name="item" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        if (item.value.startsWith('{\'email\':')) {
            var value = eval('(' + item.value + ')');
            Spotted.WebServices.Controls.MultiBuddyChooser.Service.createUsrFromEmailAndReturnK(value.email, Function.createDelegate(this, this._createUsrFromEmailAndReturnKSuccessCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), item, 3000);
        }
        else {
            if (this._oldItemChosen != null) {
                this._oldItemChosen(item);
            }
        }
    },
    _createUsrFromEmailAndReturnKSuccessCallback: function SpottedScript_Behaviours_CreateUserFromEmail_Controller$_createUsrFromEmailAndReturnKSuccessCallback(result, userContext, methodName) {
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
    _checkForEmailAddress: function SpottedScript_Behaviours_CreateUserFromEmail_Controller$_checkForEmailAddress() {
        Utils.Trace.write('CheckingForEmailAddress');
        var regex = new RegExp(SpottedScript.Behaviours.CreateUserFromEmail.Controller._emailRegex);
        var email = this._selector.get_text().trim();
        if (regex.test(email)) {
            var suggestion = new ScriptSharpLibrary.Suggestion();
            suggestion.html = SpottedScript.Behaviours.CreateUserFromEmail.Controller._getPicTitleDetailTemplateHtml('/gfx/icon40-inbox.png', 'Add ' + this._selector.get_text().trim() + ' as a buddy', 'When they join DontStayIn they will be added as one of your buddies.  If you type a name after the email address you can use that in future to find this person.');
            suggestion.text = this._selector.get_text();
            suggestion.value = '{\'email\': \'' + escape(this._selector.get_text()) + '\'}';
            suggestion.priority = this._selector.get_text().length * 100;
            for (var i = 0; i < this._selector.suggestions.get_count(); i++) {
                if (this._selector.suggestions.get_item(i).value.startsWith('{\'email\': ')) {
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
// SpottedScript.Behaviours.CreateUserFromEmail._emailSuggestionValue
SpottedScript.Behaviours.CreateUserFromEmail._emailSuggestionValue = function SpottedScript_Behaviours_CreateUserFromEmail__emailSuggestionValue() {
    /// <field name="email" type="String">
    /// </field>
}
SpottedScript.Behaviours.CreateUserFromEmail._emailSuggestionValue.prototype = {
    email: null
}
Type.registerNamespace('Net.Comet');

////////////////////////////////////////////////////////////////////////////////
// Net.Comet.CometProxy
Net.Comet.CometProxy = function Net_Comet_CometProxy() {
}
Net.Comet.CometProxy.invoke = function Net_Comet_CometProxy$invoke(url, ProcessMessage, onComplete) {
    /// <param name="url" type="String">
    /// </param>
    /// <param name="ProcessMessage" type="Net.Comet.ProcessMessageDelegate">
    /// </param>
    /// <param name="onComplete" type="ScriptSharpLibrary.Action">
    /// </param>
    /// <returns type="Net.Comet.CometRequest"></returns>
    var hiddenIFrame = document.createElement('IFRAME');
    var cometRequest = new Net.Comet.CometRequest(hiddenIFrame);
    var notifyComplete = Function.createDelegate(null, function() {
        cometRequest._notifyComplete();
    });
    cometRequest.onCometRequestComplete = onComplete;
    hiddenIFrame.receive = function(message){ProcessMessage(unescape(message));};;
    hiddenIFrame.notifyComplete = notifyComplete;;
    hiddenIFrame.src = url;
    hiddenIFrame.style.visibility = 'hidden';
    document.body.appendChild(hiddenIFrame);
    return cometRequest;
}
////////////////////////////////////////////////////////////////////////////////
// Net.Comet.CometRequest
Net.Comet.CometRequest = function Net_Comet_CometRequest(iFrame) {
    /// <param name="iFrame" type="Object" domElement="true">
    /// </param>
    /// <field name="onCometRequestComplete" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="_iFrame" type="Object" domElement="true">
    /// </field>
    /// <field name="_completed" type="Boolean">
    /// </field>
    this._iFrame = iFrame;
}
Net.Comet.CometRequest.prototype = {
    onCometRequestComplete: null,
    _iFrame: null,
    _completed: false,
    abort: function Net_Comet_CometRequest$abort() {
        if (!this._completed) {
            this._iFrame.src = '';
            try {
                this._iFrame.parentNode.removeChild(this._iFrame);
            }
            catch ($e1) {
            }
            if (this.onCometRequestComplete != null) {
                this.onCometRequestComplete();
            }
            this._completed = true;
        }
    },
    _notifyComplete: function Net_Comet_CometRequest$_notifyComplete() {
        this.abort();
    }
}
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
// ScriptSharpLibrary.HelloWorld
ScriptSharpLibrary.HelloWorld = function ScriptSharpLibrary_HelloWorld(te) {
    /// <param name="te" type="Object" domElement="true">
    /// </param>
    /// <field name="_te" type="Object" domElement="true">
    /// </field>
    this._te = te;
    te.innerHTML = 'hello';
    var handler = Function.createDelegate(this, this.doSomething);
    $addHandler(te, 'click', handler);
}
ScriptSharpLibrary.HelloWorld.prototype = {
    _te: null,
    doSomething: function ScriptSharpLibrary_HelloWorld$doSomething(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        this._te.innerHTML = 'goodbye at ' + new Date().getTime();
    }
}
////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.HtmlAutoCompleteAttributes
ScriptSharpLibrary.HtmlAutoCompleteAttributes = function ScriptSharpLibrary_HtmlAutoCompleteAttributes() {
    /// <field name="webServiceUrl" type="String" static="true">
    /// </field>
    /// <field name="webServiceMethod" type="String" static="true">
    /// </field>
    /// <field name="cometServiceUrl" type="String" static="true">
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
    /// <field name="_popupLeftOffset" type="Number" integer="true">
    /// </field>
    /// <field name="_popupTopOffset" type="Number" integer="true">
    /// </field>
    /// <field name="_rightAlign" type="Boolean">
    /// </field>
    /// <field name="onSuggestionsRequested" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="_remoteSuggestionsGetter" type="ScriptSharpLibrary.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter">
    /// </field>
    /// <field name="suggestions" type="ScriptSharpLibrary.SuggestionsCollection">
    /// </field>
    /// <field name="onhighlight" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="itemChosen" type="ScriptSharpLibrary.KeyStringPairAction">
    /// </field>
    /// <field name="onTextPasted" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="_input" type="Object" domElement="true">
    /// </field>
    /// <field name="_hiddenInput" type="Object" domElement="true">
    /// </field>
    /// <field name="_anchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_popupMenu" type="ScriptSharpLibrary.PopupMenu">
    /// </field>
    /// <field name="_mode" type="ScriptSharpLibrary._htmlAutoCompleteMode">
    /// </field>
    /// <field name="_watermarker" type="ScriptSharpLibrary.WatermarkExtender">
    /// </field>
    /// <field name="parameters" type="ScriptSharpLibrary.PairListField">
    /// </field>
    /// <field name="onFocus" type="Sys.UI.DomEventHandler">
    /// </field>
    /// <field name="transformReceivedSuggestions" type="ScriptSharpLibrary.TransformSuggestions">
    /// </field>
    /// <field name="_ajaxIcon" type="Object" domElement="true">
    /// </field>
    /// <field name="_currentTimer" type="Number" integer="true">
    /// </field>
    this.suggestions = new ScriptSharpLibrary.SuggestionsCollection();
    this._currentTimer = -2;
    var cometAtt = input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.cometServiceUrl);
    this._remoteSuggestionsGetter = new ScriptSharpLibrary.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter(input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.webServiceUrl).value, input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.webServiceMethod).value);
    this._mode = (isSuggest) ? ScriptSharpLibrary._htmlAutoCompleteMode.suggest : ScriptSharpLibrary._htmlAutoCompleteMode.complete;
    this._input = input;
    this._hiddenInput = hiddenInput;
    this._anchor = (anchor == null) ? input : anchor;
    $addHandler(input, 'blur', Function.createDelegate(this, function(e) {
        window.setTimeout(Function.createDelegate(this, this._doBlur), 250);
    }));
    $addHandler(input, 'keydown', Function.createDelegate(this, this._handleKeyDown));
    $addHandler(input, 'keyup', Function.createDelegate(this, this._handleKeyUp));
    $addHandler(input, 'focus', Function.createDelegate(this, this._callOnFocus));
    var waterMarkNode = input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.watermark);
    if (waterMarkNode != null) {
        this._watermarker = new ScriptSharpLibrary.WatermarkExtender(input, input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.watermark).value);
    }
    var popupLeftNode = input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupLeftOffset);
    this._popupLeftOffset = (popupLeftNode == null) ? 0 : Number.parseInvariant(popupLeftNode.value);
    var popupTopNode = input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupTopOffset);
    this._popupTopOffset = (popupTopNode == null) ? 0 : Number.parseInvariant(popupTopNode.value);
    var rightAlignNode = input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.rightAlign);
    this._rightAlign = (rightAlignNode == null) ? false : Boolean.parse(rightAlignNode.value);
    if (input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupLeftOffset) != null) {
        this._popupLeftOffset = Number.parseInvariant(input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupLeftOffset).value);
    }
    this.parameters = new ScriptSharpLibrary.PairListField(parametersHiddenField);
    this.suggestions.onSuggestionsChanged = Function.createDelegate(this, function() {
        this.displaySuggestionsInPopupMenu();
    });
    this._remoteSuggestionsGetter.onAllSuggestionsReceived = Function.createDelegate(this, function() {
        this._removeLowPrioritySuggestionsAndSetRemainingSuggestionsToLowPriority();
        this._hideAjaxIcon();
    });
    this._remoteSuggestionsGetter.onSuggestionsRequested = Function.createDelegate(this, function() {
        this._showAjaxIcon();
    });
    this._remoteSuggestionsGetter.onSuggestionReceived = Function.createDelegate(this, function(newSuggestions) {
        Utils.Trace.write('Received' + newSuggestions.length + 'suggestions');
        if (this.transformReceivedSuggestions != null) {
            this._addSuggestions(this.transformReceivedSuggestions(newSuggestions, this.get__maxNumberOfItemsToGet()));
        }
        else {
            this._addSuggestions(newSuggestions);
        }
    });
    this._remoteSuggestionsGetter.onAbortCurrentRequest = Function.createDelegate(this, function() {
        this._hideAjaxIcon();
    });
}
ScriptSharpLibrary.HtmlAutoCompleteBehaviour.prototype = {
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
    setWebMethod: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$setWebMethod(methodName) {
        /// <param name="methodName" type="String">
        /// </param>
        this._remoteSuggestionsGetter.methodName = methodName;
    },
    _mode: 0,
    _watermarker: null,
    parameters: null,
    onFocus: null,
    _callOnFocus: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_callOnFocus(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(Function.createDelegate(this, function() {
            this.focus();
            if (this.onFocus != null) {
                this.onFocus(e);
            }
        }));
    },
    transformReceivedSuggestions: null,
    _ajaxIcon: null,
    _hideAjaxIcon: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_hideAjaxIcon() {
        if (this._ajaxIcon != null) {
            this._ajaxIcon.style.display = 'none';
        }
    },
    _showAjaxIcon: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_showAjaxIcon() {
        if (this._ajaxIcon == null) {
            this._ajaxIcon = document.createElement('IMG');
            this._ajaxIcon.src = '/Gfx/autocomplete-loading.gif';
            this._ajaxIcon.style.height = '16px';
            this._ajaxIcon.style.width = '16px';
            this._ajaxIcon.style.position = 'absolute';
            var offset = jQuery(this._anchor).offset();
            this._ajaxIcon.style.left = (offset.left + this._anchor.clientWidth - 18) + 'px';
            this._ajaxIcon.style.top = (offset.top + 2) + 'px';
            this._ajaxIcon.style.zIndex = 200;
            document.body.appendChild(this._ajaxIcon);
        }
        this._ajaxIcon.style.display = '';
    },
    addSuggestion: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$addSuggestion(newSuggestion) {
        /// <param name="newSuggestion" type="ScriptSharpLibrary.Suggestion">
        /// </param>
        this._addSuggestions([ newSuggestion ]);
    },
    _addSuggestions: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_addSuggestions(newSuggestions) {
        /// <param name="newSuggestions" type="Array" elementType="Suggestion">
        /// </param>
        Utils.Trace.write('Adding ' + newSuggestions.length + ' suggestions. Suggestions length = ' + this.suggestions.get_count());
        this.suggestions.addRange(newSuggestions);
        while (this.suggestions.get_count() > this.get__maxNumberOfItemsToGet()) {
            Utils.Trace.write('Suggestions length ' + this.suggestions.get_count() + ' Removing suggestion');
            this.suggestions.removeAt(this.suggestions.get_count() - 1);
        }
        Utils.Trace.write('Finished adding ' + newSuggestions.length + ' suggestions. Suggestions length = ' + this.suggestions.get_count());
    },
    displaySuggestionsInPopupMenu: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$displaySuggestionsInPopupMenu() {
        Utils.Trace.write('DisplaySuggestionsInPopupMenu');
        this.get__popupMenu().clear();
        for (var i = 0; i < this.suggestions.get_count(); i++) {
            var suggestion = this.suggestions.get_item(i);
            var pair = new ScriptSharpLibrary.KeyValuePair();
            pair.key = suggestion.html;
            var value = new ScriptSharpLibrary.KeyValuePair();
            value.key = suggestion.text;
            value.value = (this._mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) ? suggestion.value : suggestion.text;
            pair.value = value;
            this.get__popupMenu().addItem(pair);
        }
        this._highlightFirstSuggestion();
        this.get__popupMenu().show(this._anchor, this.get__minWidth(), this.get__maxWidth(), this._popupTopOffset, this._popupLeftOffset, this._rightAlign);
    },
    _highlightFirstSuggestion: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_highlightFirstSuggestion() {
        if (this._mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete && this.get__popupMenu().get_currentlyHighlightedItem() == null) {
            this.get__popupMenu().set_indexOfCurrentlyHighlightedItem(0);
        }
    },
    _removeLowPrioritySuggestionsAndSetRemainingSuggestionsToLowPriority: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_removeLowPrioritySuggestionsAndSetRemainingSuggestionsToLowPriority() {
        this._removeLowPrioritySuggestions();
        this._setSuggestionsToLowPriority();
    },
    _removeLowPrioritySuggestions: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_removeLowPrioritySuggestions() {
        for (var i = 0; i < this.suggestions.get_count(); i++) {
            if (this.suggestions.get_item(i).priority === -1) {
                this.suggestions.removeAt(i);
                i--;
                continue;
            }
        }
    },
    _setSuggestionsToLowPriority: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_setSuggestionsToLowPriority() {
        for (var i = 0; i < this.suggestions.get_count(); i++) {
            this.suggestions.get_item(i).priority = -1;
        }
    },
    get__popupMenu: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get__popupMenu() {
        /// <value type="ScriptSharpLibrary.PopupMenu"></value>
        if (this._popupMenu == null) {
            var cssAtt = this._input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuClassName);
            var highlightedCssAtt = this._input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName);
            this._popupMenu = new ScriptSharpLibrary.PopupMenu((cssAtt == null) ? ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuClassName : cssAtt.value, (highlightedCssAtt == null) ? ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName : highlightedCssAtt.value);
            this._popupMenu.itemClick = Function.createDelegate(this, this._itemSelected);
            this._popupMenu.itemHighlighted = this.onhighlight;
        }
        return this._popupMenu;
    },
    _doBlur: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_doBlur() {
        if (this._hiddenInput.value === '') {
            this._cancel();
        }
        else {
            this.get__popupMenu().hide();
        }
    },
    _handleKeyDown: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_handleKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode === Sys.UI.Key.up) {
            if (this.get__popupMenu() != null) {
                this.get__popupMenu().set_indexOfCurrentlyHighlightedItem(this.get__popupMenu().get_indexOfCurrentlyHighlightedItem() - 1);
            }
        }
        else if (e.keyCode === Sys.UI.Key.down) {
            if (this.get__popupMenu() != null) {
                this.get__popupMenu().set_indexOfCurrentlyHighlightedItem(this.get__popupMenu().get_indexOfCurrentlyHighlightedItem() + 1);
            }
        }
        else if (e.keyCode === Sys.UI.Key.esc) {
            if (this.get__popupMenu().get_currentlyHighlightedItem() != null) {
                this.clear();
                e.preventDefault();
            }
            else {
                this._cancel();
            }
        }
        else if ((e.keyCode === Sys.UI.Key.tab && !e.shiftKey) || e.keyCode === Sys.UI.Key.enter) {
            if (this._mode === ScriptSharpLibrary._htmlAutoCompleteMode.suggest) {
                if (this.get__suggestionIsHighlighted()) {
                    e.preventDefault();
                    this._itemSelected(this.get__popupMenu().get_currentlyHighlightedItem());
                    this.get__popupMenu().clear();
                    return;
                }
                else {
                    if (this.get__validSelectionHasBeenMade()) {
                        var pair = new ScriptSharpLibrary.KeyValuePair();
                        pair.key = this._input.value;
                        pair.value = this._input.value;
                        this._itemSelected(pair);
                        return;
                    }
                }
            }
            else if (this._mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
                if (this.get__suggestionIsHighlighted() && !this.get__validSelectionHasBeenMade()) {
                    if (e.keyCode === Sys.UI.Key.enter) {
                        e.preventDefault();
                    }
                    this._itemSelected(this.get__popupMenu().get_currentlyHighlightedItem());
                    return;
                }
            }
        }
        else if (e.keyCode === Sys.UI.Key.backspace && this._mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
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
        else if (e.keyCode === Sys.UI.Key.del && this._mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
            this._hiddenInput.value = '';
            this.requestSuggestions();
        }
    },
    _handleKeyUp: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_handleKeyUp(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode !== Sys.UI.Key.down && e.keyCode !== Sys.UI.Key.end && e.keyCode !== Sys.UI.Key.enter && e.keyCode !== Sys.UI.Key.esc && e.keyCode !== Sys.UI.Key.home && e.keyCode !== Sys.UI.Key.left && e.keyCode !== Sys.UI.Key.pageDown && e.keyCode !== Sys.UI.Key.pageUp && e.keyCode !== Sys.UI.Key.up) {
            if (this._mode === ScriptSharpLibrary._htmlAutoCompleteMode.suggest) {
                this._hiddenInput.value = this._input.value;
            }
            this.requestSuggestions();
        }
    },
    requestSuggestions: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$requestSuggestions() {
        this._setSuggestionsToLowPriority();
        if (this._mode === ScriptSharpLibrary._htmlAutoCompleteMode.complete) {
            this.set_value('');
        }
        if (this.onSuggestionsRequested != null) {
            Utils.Trace.write('OnSuggestionsRequested');
            this.onSuggestionsRequested();
        }
        if (this._currentTimer !== -2) {
            window.clearTimeout(this._currentTimer);
        }
        if (this._input.value.trim().length > 0) {
            this._currentTimer = window.setTimeout(Function.createDelegate(this, function() {
                this._remoteSuggestionsGetter._requestSuggestions(this._input.value, this.parameters._toDictionary(), this.get__maxNumberOfItemsToGet());
            }), 200);
        }
    },
    get__validSelectionHasBeenMade: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get__validSelectionHasBeenMade() {
        /// <value type="Boolean"></value>
        return this._hiddenInput.value != null && this._hiddenInput.value !== '';
    },
    get__suggestionIsHighlighted: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get__suggestionIsHighlighted() {
        /// <value type="Boolean"></value>
        return this.get__popupMenu().get_currentlyHighlightedItem() != null;
    },
    _cancel: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_cancel() {
        this.clear();
        this._input.value = '';
        this._hiddenInput.value = '';
        this._remoteSuggestionsGetter._abortCurrentRequest();
    },
    clear: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$clear() {
        this.suggestions.clear();
        this.get__popupMenu().clear();
        this._remoteSuggestionsGetter._abortCurrentRequest();
    },
    get__maxNumberOfItemsToGet: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get__maxNumberOfItemsToGet() {
        /// <value type="Number" integer="true"></value>
        var att = this._input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.maxNumberOfItemsToGet);
        return (att == null) ? 10 : Number.parseInvariant(att.value);
    },
    get__minWidth: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get__minWidth() {
        /// <value type="Number" integer="true"></value>
        var att = this._input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.minimumPopupWidth);
        return (att == null) ? 0 : Number.parseInvariant(att.value);
    },
    get__maxWidth: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get__maxWidth() {
        /// <value type="Number" integer="true"></value>
        var att = this._input.getAttributeNode(ScriptSharpLibrary.HtmlAutoCompleteAttributes.maximumPopupWidth);
        return (att == null) ? 0 : Number.parseInvariant(att.value);
    },
    _itemSelected: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$_itemSelected(value) {
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
    get_text: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_text() {
        /// <value type="String"></value>
        return this._input.value;
    },
    set_text: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$set_text(value) {
        /// <value type="String"></value>
        this._input.value = value;
        return value;
    },
    get_value: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_value() {
        /// <value type="String"></value>
        return this._hiddenInput.value;
    },
    set_value: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$set_value(value) {
        /// <value type="String"></value>
        this._hiddenInput.value = value;
        return value;
    },
    get_currentlyHighlighedItem: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$get_currentlyHighlighedItem() {
        /// <value type="Object"></value>
        return this.get__popupMenu().get_currentlyHighlightedItem();
    },
    focus: function ScriptSharpLibrary_HtmlAutoCompleteBehaviour$focus() {
        this._input.focus();
        if (this._watermarker != null) {
            this._watermarker.onFocus(null);
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.KeyValuePair
ScriptSharpLibrary.KeyValuePair = function ScriptSharpLibrary_KeyValuePair() {
    /// <field name="key" type="String">
    /// </field>
    /// <field name="value" type="Object">
    /// </field>
}
ScriptSharpLibrary.KeyValuePair.prototype = {
    key: null,
    value: null
}
////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.KeyStringPair
ScriptSharpLibrary.KeyStringPair = function ScriptSharpLibrary_KeyStringPair() {
    /// <field name="key" type="String">
    /// </field>
    /// <field name="value" type="String">
    /// </field>
}
ScriptSharpLibrary.KeyStringPair.prototype = {
    key: null,
    value: null
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
    /// <field name="_hiddenOutput" type="Object" domElement="true">
    /// </field>
    /// <field name="_selections" type="ScriptSharpLibrary.PairListField">
    /// </field>
    this.container = container;
    this._hiddenOutput = hiddenOutput;
    this.htmlAutoComplete = htmlAutoComplete;
    this.htmlAutoComplete.itemChosen = Function.createDelegate(this, this._htmlAutoComplete_ItemChosen);
    this._selections = new ScriptSharpLibrary.PairListField(hiddenOutput);
    this._initialiseInitialSelections();
    $addHandler(this.container, 'click', Function.createDelegate(this, this._onClick));
}
ScriptSharpLibrary.MultiSelectorBehaviour.prototype = {
    itemAdded: null,
    itemRemoved: null,
    htmlAutoComplete: null,
    container: null,
    _hiddenOutput: null,
    _selections: null,
    _initialiseInitialSelections: function ScriptSharpLibrary_MultiSelectorBehaviour$_initialiseInitialSelections() {
        this._selections._clear();
        for (var i = 0; i < this.container.childNodes.length; i++) {
            var child = this.container.childNodes[i];
            if (child.nodeType === 1 && child.childNodes.length > 1 && child.childNodes[0].tagName !== 'INPUT') {
                $addHandler(child.childNodes[1], 'click', Function.createDelegate(this, this._deleteButtonClick));
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
        if (this._selections.containsKey(text)) {
            return;
        }
        var item = document.createElement('LI');
        var itemCssAttribute = this.container.getAttributeNode(ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorListBoxCss);
        item.className = (itemCssAttribute != null) ? itemCssAttribute.value : ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorListBoxCss;
        var deleteButton = document.createElement('SPAN');
        deleteButton.innerHTML = '&nbsp;&nbsp;';
        var delCssAttribute = this.container.getAttributeNode(ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorDelButtonCss);
        deleteButton.className = (delCssAttribute != null) ? delCssAttribute.value : ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorDelButtonCss;
        $addHandler(deleteButton, 'click', Function.createDelegate(this, this._deleteButtonClick));
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
        while (childToInsertBefore.childNodes.length === 0 || childToInsertBefore.childNodes[0].tagName !== 'INPUT') {
            childToInsertBefore = childToInsertBefore.previousSibling;
        }
        this.container.insertBefore(item, childToInsertBefore);
        this._selections.set(text, value);
        if (this.itemAdded != null) {
            this.itemAdded(text, value);
        }
    },
    _htmlAutoComplete_ItemChosen: function ScriptSharpLibrary_MultiSelectorBehaviour$_htmlAutoComplete_ItemChosen(item) {
        /// <param name="item" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        this.addItem(item.key, item.value);
        this.htmlAutoComplete.set_text('');
        this.htmlAutoComplete.set_value('');
        this.htmlAutoComplete.focus();
    },
    _deleteButtonClick: function ScriptSharpLibrary_MultiSelectorBehaviour$_deleteButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        $clearHandlers(e.target);
        this.removeItem(e.target.parentNode);
    },
    removeItem: function ScriptSharpLibrary_MultiSelectorBehaviour$removeItem(item) {
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
    getSelections: function ScriptSharpLibrary_MultiSelectorBehaviour$getSelections() {
        /// <returns type="ScriptSharpLibrary.PairListField"></returns>
        return this._selections;
    },
    clear: function ScriptSharpLibrary_MultiSelectorBehaviour$clear() {
        for (var i = this.container.childNodes.length - 1; i >= 0; i--) {
            var child = this.container.childNodes[i];
            if (child.nodeType === 1 && child.childNodes[0].tagName !== 'INPUT') {
                $clearHandlers(child.childNodes[1]);
                this._selections.set(child.childNodes[0].nodeValue, null);
                child.parentNode.removeChild(child);
            }
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.PairListField
ScriptSharpLibrary.PairListField = function ScriptSharpLibrary_PairListField(hiddenField) {
    /// <param name="hiddenField" type="Object" domElement="true">
    /// </param>
    /// <field name="_hiddenField" type="Object" domElement="true">
    /// </field>
    this._hiddenField = hiddenField;
}
ScriptSharpLibrary.PairListField.prototype = {
    _hiddenField: null,
    _add: function ScriptSharpLibrary_PairListField$_add(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        if (this._hiddenField.value.length > 0) {
            this._hiddenField.value += ';';
        }
        this._hiddenField.value += escape(key) + ':' + escape(value);
    },
    set: function ScriptSharpLibrary_PairListField$set(key, value) {
        /// <param name="key" type="String">
        /// </param>
        /// <param name="value" type="Object">
        /// </param>
        this._removeByKey(key);
        if (value != null) {
            if (this._hiddenField.value.length > 0) {
                this._hiddenField.value += ';';
            }
            if (Date.isInstanceOfType(value)) {
                this._hiddenField.value += escape(key) + ':' + escape((value).toDateString());
            }
            else {
                this._hiddenField.value += escape(key) + ':' + escape(value.toString());
            }
        }
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
        if (this._hiddenField.value.length > 0) {
            return this._hiddenField.value.split(';').length;
        }
        else {
            return 0;
        }
    },
    _removeByKey: function ScriptSharpLibrary_PairListField$_removeByKey(key) {
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
        this._hiddenField.value = '';
        var tempArray = [];
        for (var i = 0; i < array.length; i++) {
            var pair = array[i];
            tempArray[i] = escape(pair[0]) + ':' + escape(pair[1]);
        }
        this._hiddenField.value = tempArray.join(';');
    },
    toArray: function ScriptSharpLibrary_PairListField$toArray() {
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
    _clear: function ScriptSharpLibrary_PairListField$_clear() {
        this._hiddenField.value = '';
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
// ScriptSharpLibrary.PopupMenu
ScriptSharpLibrary.PopupMenu = function ScriptSharpLibrary_PopupMenu(cssClass, highlightedItemCssClass) {
    /// <param name="cssClass" type="String">
    /// </param>
    /// <param name="highlightedItemCssClass" type="String">
    /// </param>
    /// <field name="itemClick" type="ScriptSharpLibrary.ItemClickDelegate">
    /// </field>
    /// <field name="itemHighlighted" type="ScriptSharpLibrary.Action">
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
ScriptSharpLibrary.PopupMenu.prototype = {
    itemClick: null,
    itemHighlighted: null,
    _container: null,
    _highlightedItemCssClass: null,
    addItem: function ScriptSharpLibrary_PopupMenu$addItem(pair) {
        /// <param name="pair" type="ScriptSharpLibrary.KeyValuePair">
        /// </param>
        var div = document.createElement('DIV');
        var divAsArray = div;
        divAsArray['value'] = pair.value;
        if (pair.key.indexOf('<') > -1) {
            div.innerHTML = pair.key;
        }
        else {
            div.innerHTML = '<div class=\"DefaultPopupMenuItem\">' + pair.key + '</div>';
        }
        this._container.appendChild(div);
        $addHandler(div, 'click', Function.createDelegate(this, this._itemClickHandler));
        $addHandler(div, 'mouseover', Function.createDelegate(this, this._itemMouseEnterHandler));
    },
    _itemClickHandler: function ScriptSharpLibrary_PopupMenu$_itemClickHandler(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this.itemClick != null) {
            this.itemClick(this.get_currentlyHighlightedItem());
        }
    },
    _itemMouseEnterHandler: function ScriptSharpLibrary_PopupMenu$_itemMouseEnterHandler(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
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
    clear: function ScriptSharpLibrary_PopupMenu$clear() {
        while (this._container.childNodes.length > 0) {
            $clearHandlers(this._container.childNodes[0]);
            this._container.removeChild(this._container.childNodes[0]);
        }
        this._indexOfCurrentlyHighlightedItem = -1;
        this._container.style.display = 'none';
    },
    show: function ScriptSharpLibrary_PopupMenu$show(anchor, minWidth, maxWidth, topOffset, leftOffset, rightAlign) {
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
            var jq = jQuery(anchor);
            var anchorOffsetHeight = (anchor == null) ? 0 : anchor.offsetHeight;
            var anchorOffsetWidth = (anchor == null) ? 0 : anchor.offsetWidth;
            if (minWidth === 0 && maxWidth === 0) {
                minWidth = anchorOffsetWidth;
                maxWidth = anchorOffsetWidth;
            }
            var nudgeLeft = (Sys.Browser.agent === Sys.Browser.InternetExplorer) ? -2 : 0;
            var nudgeTop = (Sys.Browser.agent === Sys.Browser.InternetExplorer) ? -2 : 0;
            var nudgeWidth = (Sys.Browser.agent === Sys.Browser.InternetExplorer) ? 0 : -2;
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
                Utils.Trace.write('' + jq.offset().left + ', ' + nudgeLeft + ', ' + anchor.clientWidth + ', ' + this._container.clientWidth);
                this._container.style.left = (jq.offset().left + nudgeLeft + anchor.clientWidth - this._container.clientWidth) + 'px';
            }
        }
    },
    hide: function ScriptSharpLibrary_PopupMenu$hide() {
        eval('try{htm();}catch(e){}');
        this._container.style.display = 'none';
    },
    get_indexOfCurrentlyHighlightedItem: function ScriptSharpLibrary_PopupMenu$get_indexOfCurrentlyHighlightedItem() {
        /// <value type="Number" integer="true"></value>
        return (this._indexOfCurrentlyHighlightedItem < this._container.childNodes.length) ? this._indexOfCurrentlyHighlightedItem : -1;
    },
    set_indexOfCurrentlyHighlightedItem: function ScriptSharpLibrary_PopupMenu$set_indexOfCurrentlyHighlightedItem(value) {
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
    get_currentlyHighlightedItem: function ScriptSharpLibrary_PopupMenu$get_currentlyHighlightedItem() {
        /// <value type="Object"></value>
        if (this.get__currentlyHighlightedElement() == null) {
            return null;
        }
        else {
            return (this.get__currentlyHighlightedElement())['value'];
        }
    },
    get__currentlyHighlightedElement: function ScriptSharpLibrary_PopupMenu$get__currentlyHighlightedElement() {
        /// <value type="Object" domElement="true"></value>
        if (this.get_indexOfCurrentlyHighlightedItem() === -1) {
            return null;
        }
        else {
            return this._container.childNodes[this.get_indexOfCurrentlyHighlightedItem()];
        }
    },
    _findPosLeft: function ScriptSharpLibrary_PopupMenu$_findPosLeft(el) {
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
    _findPosTop: function ScriptSharpLibrary_PopupMenu$_findPosTop(el) {
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
////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.Suggestion
ScriptSharpLibrary.Suggestion = function ScriptSharpLibrary_Suggestion() {
    /// <field name="html" type="String">
    /// </field>
    /// <field name="text" type="String">
    /// </field>
    /// <field name="value" type="String">
    /// </field>
    /// <field name="priority" type="Number" integer="true">
    /// </field>
}
ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml = function ScriptSharpLibrary_Suggestion$getPicTitleDetailTemplateHtml(imageSrc, title, detail) {
    /// <param name="imageSrc" type="String">
    /// </param>
    /// <param name="title" type="String">
    /// </param>
    /// <param name="detail" type="String">
    /// </param>
    /// <returns type="String"></returns>
    return String.format('<table cellspacing=\'0\' cellpadding=\'0\'><tr><td width=\'40\'><img src=\'{0}\' border=0 width=40 hspace=0 height=40 style=\'border-right:1px solid #999999;display:block;\'/></td><td style=\'padding:2px 3px 2px 3px;\' valign=\'top\'><b>{1}</b><br />{2}</td></tr></table>', imageSrc, title, detail);
}
ScriptSharpLibrary.Suggestion.addSuggestion = function ScriptSharpLibrary_Suggestion$addSuggestion(suggestions, suggestion, maxNumberOfItemsToGet) {
    /// <param name="suggestions" type="Array" elementType="Suggestion">
    /// </param>
    /// <param name="suggestion" type="ScriptSharpLibrary.Suggestion">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    suggestion.priority = (suggestions.length > 0) ? suggestions[suggestions.length - 1].priority : 1;
    suggestions[(suggestions.length < maxNumberOfItemsToGet) ? suggestions.length : suggestions.length - 1] = suggestion;
}
ScriptSharpLibrary.Suggestion.addSuggestionAtTop = function ScriptSharpLibrary_Suggestion$addSuggestionAtTop(suggestions, suggestion) {
    /// <param name="suggestions" type="Array" elementType="Suggestion">
    /// </param>
    /// <param name="suggestion" type="ScriptSharpLibrary.Suggestion">
    /// </param>
    suggestion.priority = (suggestions.length > 0) ? (suggestions[0].priority + 1) : 1;
    for (var i = suggestions.length; i > 0; i--) {
        var temp = suggestions[i - 1];
        suggestions[i - 1] = null;
        suggestions[i] = temp;
    }
    suggestions[0] = suggestion;
}
ScriptSharpLibrary.Suggestion.prototype = {
    html: null,
    text: null,
    value: null,
    priority: 0
}
////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.SuggestionsCollection
ScriptSharpLibrary.SuggestionsCollection = function ScriptSharpLibrary_SuggestionsCollection() {
    /// <field name="onSuggestionsChanged" type="ScriptSharpLibrary.SuggestionsChanged">
    /// </field>
    /// <field name="_suggestions" type="Array">
    /// </field>
    this._suggestions = [];
}
ScriptSharpLibrary.SuggestionsCollection.prototype = {
    onSuggestionsChanged: null,
    add: function ScriptSharpLibrary_SuggestionsCollection$add(newSuggestion) {
        /// <param name="newSuggestion" type="ScriptSharpLibrary.Suggestion">
        /// </param>
        this._addWithoutSortOrNotify(newSuggestion);
        this._sort();
        if (this.onSuggestionsChanged != null) {
            this.onSuggestionsChanged();
        }
    },
    _addWithoutSortOrNotify: function ScriptSharpLibrary_SuggestionsCollection$_addWithoutSortOrNotify(newSuggestion) {
        /// <param name="newSuggestion" type="ScriptSharpLibrary.Suggestion">
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
    addRange: function ScriptSharpLibrary_SuggestionsCollection$addRange(newSuggestions) {
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
    _sort: function ScriptSharpLibrary_SuggestionsCollection$_sort() {
        this._suggestions.sort(Function.createDelegate(this, function(a, b) {
            return (b).priority - (a).priority;
        }));
    },
    removeAt: function ScriptSharpLibrary_SuggestionsCollection$removeAt(index) {
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
    clear: function ScriptSharpLibrary_SuggestionsCollection$clear() {
        this._suggestions = [];
    },
    get_count: function ScriptSharpLibrary_SuggestionsCollection$get_count() {
        /// <value type="Number" integer="true"></value>
        return this._suggestions.length;
    },
    get_item: function ScriptSharpLibrary_SuggestionsCollection$get_item(index) {
        /// <param name="index" type="Number" integer="true">
        /// </param>
        /// <param name="value" type="ScriptSharpLibrary.Suggestion">
        /// </param>
        /// <returns type="ScriptSharpLibrary.Suggestion"></returns>
        return (this._suggestions[index]);
    }
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
    /// <field name="_webRequest" type="Sys.Net.WebRequest">
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
ScriptSharpLibrary.SuggestionsGetter.prototype = {
    suggestionsGot: null,
    _webRequest: null,
    _webServiceUrl: null,
    _webServiceCommand: null,
    _maxNumberOfItemsToGet: 0,
    suggestions: null,
    requestSuggestions: function ScriptSharpLibrary_SuggestionsGetter$requestSuggestions(textSoFar) {
        /// <param name="textSoFar" type="String">
        /// </param>
        var parameters = {};
        parameters['text'] = textSoFar;
        parameters['maxNumberOfItemsToGet'] = this._maxNumberOfItemsToGet;
        this.cancelRequests();
        this._webRequest = Sys.Net.WebServiceProxy.invoke(this._webServiceUrl, this._webServiceCommand, true, parameters, Function.createDelegate(this, this.successCallback), Function.createDelegate(this, this.failureCallback), textSoFar, -1);
    },
    cancelRequests: function ScriptSharpLibrary_SuggestionsGetter$cancelRequests() {
        if (this._webRequest != null) {
            this._webRequest.get_executor().abort();
            this._webRequest = null;
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
        if (this.suggestionsGot != null) {
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
// ScriptSharpLibrary.WatermarkExtender
ScriptSharpLibrary.WatermarkExtender = function ScriptSharpLibrary_WatermarkExtender(el, watermark) {
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
    $addHandler(el, 'focus', Function.createDelegate(this, this.onFocus));
    $addHandler(el, 'blur', Function.createDelegate(this, this.onBlur));
    (el)['readOnly'] = null;
}
ScriptSharpLibrary.WatermarkExtender.prototype = {
    _el: null,
    _watermark: null,
    _timeoutId: 0,
    onBlur: function ScriptSharpLibrary_WatermarkExtender$onBlur(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        this._timeoutId = window.setTimeout(Function.createDelegate(this, this.addWatermark), 300);
    },
    onFocus: function ScriptSharpLibrary_WatermarkExtender$onFocus(ev) {
        /// <param name="ev" type="Sys.UI.DomEvent">
        /// </param>
        window.clearTimeout(this._timeoutId);
        if (this._el.value === this._watermark) {
            this._el.className = '';
            this._el.value = '';
        }
    },
    addWatermark: function ScriptSharpLibrary_WatermarkExtender$addWatermark() {
        if (this._el.value === '') {
            this._el.className = 'Watermark';
            this._el.value = this._watermark;
        }
    }
}
Type.registerNamespace('ScriptSharpLibrary.HtmlAutoComplete');

////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.HtmlAutoComplete._cometRemoteSuggestionsGetter
ScriptSharpLibrary.HtmlAutoComplete._cometRemoteSuggestionsGetter = function ScriptSharpLibrary_HtmlAutoComplete__cometRemoteSuggestionsGetter(url) {
    /// <param name="url" type="String">
    /// </param>
    /// <field name="_url$1" type="String">
    /// </field>
    /// <field name="_cometRequest$1" type="Net.Comet.CometRequest">
    /// </field>
    ScriptSharpLibrary.HtmlAutoComplete._cometRemoteSuggestionsGetter.initializeBase(this);
    this._url$1 = url;
}
ScriptSharpLibrary.HtmlAutoComplete._cometRemoteSuggestionsGetter.prototype = {
    _url$1: null,
    _cometRequest$1: null,
    makeRequest: function ScriptSharpLibrary_HtmlAutoComplete__cometRemoteSuggestionsGetter$makeRequest(text, parameters, maxNumberOfItemsToGet) {
        /// <param name="text" type="String">
        /// </param>
        /// <param name="parameters" type="Object">
        /// </param>
        /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
        /// </param>
        var requestUrl = this._url$1 + '?text=' + escape(text) + '&maxNumberOfItemsToGet=' + maxNumberOfItemsToGet;
        var $dict1 = parameters;
        for (var $key2 in $dict1) {
            var entry = { key: $key2, value: $dict1[$key2] };
            requestUrl += '&' + escape(entry.key) + '=' + escape(entry.value.toString());
        }
        this._cometRequest$1 = Net.Comet.CometProxy.invoke(requestUrl, Function.createDelegate(this, function(message) {
            if (this.onSuggestionReceived != null) {
                this.onSuggestionReceived(eval('(' + message + ')'));
            }
        }), Function.createDelegate(this, function() {
            if (this.onAllSuggestionsReceived != null) {
                this.onAllSuggestionsReceived();
            }
        }));
    },
    get__isMakingRequest: function ScriptSharpLibrary_HtmlAutoComplete__cometRemoteSuggestionsGetter$get__isMakingRequest() {
        /// <value type="Boolean"></value>
        return this._cometRequest$1 != null;
    },
    _doAbortCurrentRequest: function ScriptSharpLibrary_HtmlAutoComplete__cometRemoteSuggestionsGetter$_doAbortCurrentRequest() {
        if (this._cometRequest$1 != null) {
            this._cometRequest$1.abort();
            this._cometRequest$1 = null;
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.HtmlAutoComplete.RemoteSuggestionsGetter
ScriptSharpLibrary.HtmlAutoComplete.RemoteSuggestionsGetter = function ScriptSharpLibrary_HtmlAutoComplete_RemoteSuggestionsGetter() {
    /// <field name="onSuggestionsGot" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="onSuggestionsRequested" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="onAllSuggestionsReceived" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="onSuggestionReceived" type="ScriptSharpLibrary.HtmlAutoComplete.SuggestionArrayAction">
    /// </field>
    /// <field name="onAbortCurrentRequest" type="ScriptSharpLibrary.Action">
    /// </field>
}
ScriptSharpLibrary.HtmlAutoComplete.RemoteSuggestionsGetter.prototype = {
    onSuggestionsGot: null,
    onSuggestionsRequested: null,
    onAllSuggestionsReceived: null,
    onSuggestionReceived: null,
    onAbortCurrentRequest: null,
    _requestSuggestions: function ScriptSharpLibrary_HtmlAutoComplete_RemoteSuggestionsGetter$_requestSuggestions(text, parameters, maxNumberOfItemsToGet) {
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
    _abortCurrentRequest: function ScriptSharpLibrary_HtmlAutoComplete_RemoteSuggestionsGetter$_abortCurrentRequest() {
        this._doAbortCurrentRequest();
        if (this.onAbortCurrentRequest != null) {
            this.onAbortCurrentRequest();
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// ScriptSharpLibrary.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter
ScriptSharpLibrary.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter = function ScriptSharpLibrary_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter(url, methodName) {
    /// <param name="url" type="String">
    /// </param>
    /// <param name="methodName" type="String">
    /// </param>
    /// <field name="_url$1" type="String">
    /// </field>
    /// <field name="methodName" type="String">
    /// </field>
    /// <field name="_webRequest$1" type="Sys.Net.WebRequest">
    /// </field>
    ScriptSharpLibrary.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter.initializeBase(this);
    this._url$1 = url;
    this.methodName = methodName;
}
ScriptSharpLibrary.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter.prototype = {
    _url$1: null,
    methodName: null,
    _webRequest$1: null,
    makeRequest: function ScriptSharpLibrary_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter$makeRequest(text, requestParameters, maxNumberOfItemsToGet) {
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
        if (this._webRequest$1 != null && this._webRequest$1.get_executor().get_started()) {
            this._webRequest$1.get_executor().abort();
        }
        this._webRequest$1 = Sys.Net.WebServiceProxy.invoke(this._url$1, this.methodName, true, parameters, Function.createDelegate(this, this._successCallback$1), Function.createDelegate(null, Utils.Trace.webServiceFailure), text, -1);
    },
    _successCallback$1: function ScriptSharpLibrary_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter$_successCallback$1(rawResult, userContext, methodName) {
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
    get__isMakingRequest: function ScriptSharpLibrary_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter$get__isMakingRequest() {
        /// <value type="Boolean"></value>
        return this._webRequest$1 != null;
    },
    _doAbortCurrentRequest: function ScriptSharpLibrary_HtmlAutoComplete_WebServiceRemoteSuggestionsGetter$_doAbortCurrentRequest() {
        if (this._webRequest$1 != null) {
            this._webRequest$1.get_executor().abort();
            this._webRequest$1 = null;
        }
    }
}
Type.registerNamespace('SpottedScript.Controls.PagedData');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData._iParameterSource
SpottedScript.Controls.PagedData._iParameterSource = function() { 
};
SpottedScript.Controls.PagedData._iParameterSource.prototype = {
    get_parameters : null,
    get_parametersUpdated : null,
    set_parametersUpdated : null
}
SpottedScript.Controls.PagedData._iParameterSource.registerInterface('SpottedScript.Controls.PagedData._iParameterSource');
Type.registerNamespace('SpottedScript.Controls.Tabbing.Tab');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Tabbing.Tab.ITabController
SpottedScript.Controls.Tabbing.Tab.ITabController = function() { 
};
SpottedScript.Controls.Tabbing.Tab.ITabController.prototype = {
    get_uiPanel : null,
    setHeader : null,
    displayInfoForTab : null,
    get_updated : null,
    set_updated : null
}
SpottedScript.Controls.Tabbing.Tab.ITabController.registerInterface('SpottedScript.Controls.Tabbing.Tab.ITabController');
Type.registerNamespace('SpottedScript.CustomControls.DsiCalendar');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.CustomControls.DsiCalendar.Controller
SpottedScript.CustomControls.DsiCalendar.Controller = function SpottedScript_CustomControls_DsiCalendar_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.EventCreator.View">
    /// </param>
    /// <field name="_clicked" type="Object" domElement="true">
    /// </field>
    jQuery(window.document).ready(Function.createDelegate(this, this._onReady));
}
SpottedScript.CustomControls.DsiCalendar.Controller.prototype = {
    _clicked: null,
    _onReady: function SpottedScript_CustomControls_DsiCalendar_Controller$_onReady() {
        jQuery('.CalendarAddLink').click(Function.createDelegate(this, function(o) {
            this._clicked = o.originalEvent.srcElement.parentElement;
            var dt = new Date(this._clicked.date);
            SpottedScript.Controls.EventCreator.Controller.instance.showPopup(dt, null, null, Function.createDelegate(this, this._eventAdded));
            return false;
        }));
    },
    _eventAdded: function SpottedScript_CustomControls_DsiCalendar_Controller$_eventAdded(eventInfo) {
        /// <param name="eventInfo" type="SpottedScript.Controls.EventCreator.EventInfo">
        /// </param>
        if (eventInfo != null) {
            var div = document.createElement('div');
            div.className = 'CalendarItemToday';
            div.innerHTML = String.format('<b>{0}</b>', eventInfo.name);
            this._clicked.insertBefore(div);
            this._clicked = null;
        }
    }
}
Type.registerNamespace('JQ');

////////////////////////////////////////////////////////////////////////////////
// JQ.TabsSelectEventArgs
JQ.TabsSelectEventArgs = function JQ_TabsSelectEventArgs() {
    /// <field name="instance" type="Object">
    /// internal widget instance
    /// </field>
    /// <field name="options" type="Object">
    /// options used to intialize this widget
    /// </field>
    /// <field name="tab" type="Object" domElement="true">
    /// anchor element of the currently shown tab
    /// </field>
    /// <field name="panel" type="Object" domElement="true">
    /// element
    /// </field>
}
JQ.TabsSelectEventArgs.prototype = {
    instance: null,
    options: null,
    tab: null,
    panel: null
}
Type.registerNamespace('Utils');

////////////////////////////////////////////////////////////////////////////////
// Utils.Trace
Utils.Trace = function Utils_Trace() {
}
Utils.Trace.write = function Utils_Trace$write(message) {
    /// <param name="message" type="String">
    /// </param>
}
Utils.Trace.webServiceFailure = function Utils_Trace$webServiceFailure(error, userContext, methodName) {
    /// <param name="error" type="Sys.Net.WebServiceError">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="methodName" type="String">
    /// </param>
    Utils.Trace.write('Message: ' + error.get_message() + '<br>Type: ' + error.get_exceptionType() + '<br>Stack trace: ' + error.get_stackTrace() + '<br>Status code: ' + error.get_statusCode() + '<br>Timed out: ' + error.get_timedOut());
}
////////////////////////////////////////////////////////////////////////////////
// Utils._traceObjects
Utils._traceObjects = function Utils__traceObjects() {
    /// <field name="_traceWindow" type="Object" domElement="true" static="true">
    /// </field>
}
Type.registerNamespace('ImportedUtilities');

////////////////////////////////////////////////////////////////////////////////
// ImportedUtilities.U
ImportedUtilities.U = function ImportedUtilities_U() {
}
ImportedUtilities.U.toString = function ImportedUtilities_U$toString(o) {
    /// <param name="o" type="Object">
    /// </param>
    /// <returns type="String"></returns>
    return ImportedUtilities.U._toStringWithOffset(o, '');
}
ImportedUtilities.U._toStringWithOffset = function ImportedUtilities_U$_toStringWithOffset(o, offset) {
    /// <param name="o" type="Object">
    /// </param>
    /// <param name="offset" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var tab = '    ';
    var s = '';
    if (Date.isInstanceOfType(o)) {
        s += (o).toDateString() + ' ' + (o).toTimeString();
    }
    else if (Array.isInstanceOfType(o)) {
        var a = o;
        s += '\n' + offset + '{\n';
        for (var i = 0; i < a.length; i++) {
            s += offset + tab + '[' + i.toString() + '] : ' + ImportedUtilities.U._toStringWithOffset(a[i], offset + tab) + '\n';
        }
        s += offset + '}';
    }
    else if (Boolean.isInstanceOfType(o)) {
        s += o.toString();
    }
    else if (Number.isInstanceOfType(o)) {
        s += o.toString();
    }
    else if (String.isInstanceOfType(o)) {
        var s1 = o.toString().replace(new RegExp('[\n]', 'g'), '');
        s += (s1.length > 256) ? (s1.substr(0, 256) + '(...)') : s1;
    }
    else if (Object.isInstanceOfType(o)) {
        s += '\n' + offset + '{\n';
        var $dict1 = o;
        for (var $key2 in $dict1) {
            var entry = { key: $key2, value: $dict1[$key2] };
            s += offset + tab + entry.key + ' : ' + ImportedUtilities.U._toStringWithOffset(entry.value, offset + tab) + '\n';
        }
        s += offset + '}';
    }
    return s;
}
ImportedUtilities.U.get = function ImportedUtilities_U$get(d, query) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="query" type="String">
    /// </param>
    /// <returns type="Object"></returns>
    try {
        var queryArr;
        if (query.indexOf('/') > -1) {
            queryArr = query.split('/');
        }
        else {
            queryArr = [ query ];
        }
        for (var i = 0; i < queryArr.length; i++) {
            if (i === queryArr.length - 1) {
                return ImportedUtilities.U.getFromDictionaryByQuery(d, queryArr[i]);
            }
            else {
                d = ImportedUtilities.U.getFromDictionaryByQuery(d, queryArr[i]);
            }
        }
        return null;
    }
    catch ($e1) {
        return null;
    }
}
ImportedUtilities.U.exists = function ImportedUtilities_U$exists(d, query) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="query" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    try {
        var queryArr;
        if (query.indexOf('/') > -1) {
            queryArr = query.split('/');
        }
        else {
            queryArr = [ query ];
        }
        for (var i = 0; i < queryArr.length; i++) {
            if (i === queryArr.length - 1) {
                if (ImportedUtilities.U.getFromDictionaryByQuery(d, queryArr[i]) == null) {
                    return false;
                }
            }
            else {
                d = ImportedUtilities.U.getFromDictionaryByQuery(d, queryArr[i]);
            }
        }
        return true;
    }
    catch ($e1) {
        return false;
    }
}
ImportedUtilities.U.getFromDictionaryByQuery = function ImportedUtilities_U$getFromDictionaryByQuery(d, query) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="query" type="String">
    /// </param>
    /// <returns type="Object"></returns>
    if (query === '' || query === '*') {
        return ImportedUtilities.U.getFromDictionaryByIndex(d, 0);
    }
    else {
        return d[query];
    }
}
ImportedUtilities.U.getFromDictionaryByIndex = function ImportedUtilities_U$getFromDictionaryByIndex(d, index) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="index" type="Number" integer="true">
    /// </param>
    /// <returns type="DictionaryEntry"></returns>
    var i = 0;
    var $dict1 = d;
    for (var $key2 in $dict1) {
        var de = { key: $key2, value: $dict1[$key2] };
        if (i === index) {
            return de;
        }
        i++;
    }
    return null;
}
ImportedUtilities.U.isTrue = function ImportedUtilities_U$isTrue(d, query) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="query" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    try {
        if (ImportedUtilities.U.exists(d, query)) {
            var o = ImportedUtilities.U.get(d, query);
            if (Boolean.isInstanceOfType(o)) {
                return o;
            }
        }
        return false;
    }
    catch ($e1) {
        return false;
    }
}
ImportedUtilities.U.hasValue = function ImportedUtilities_U$hasValue(d, query) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="query" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    try {
        if (ImportedUtilities.U.exists(d, query)) {
            return ImportedUtilities.U.get(d, query) != null;
        }
        return false;
    }
    catch ($e1) {
        return false;
    }
}
Type.registerNamespace('SpottedScript.Utils');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Utils._iProviderService
SpottedScript.Utils._iProviderService = function() { 
};
SpottedScript.Utils._iProviderService.prototype = {
    callWebService : null
}
SpottedScript.Utils._iProviderService.registerInterface('SpottedScript.Utils._iProviderService');
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Utils._iPagedProviderService
SpottedScript.Utils._iPagedProviderService = function() { 
};
SpottedScript.Utils._iPagedProviderService.prototype = {
    callWebService : null
}
SpottedScript.Utils._iPagedProviderService.registerInterface('SpottedScript.Utils._iPagedProviderService');
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Utils._providerService
SpottedScript.Utils._providerService = function SpottedScript_Utils__providerService(servicePath, serviceMethod, timeOut) {
    /// <param name="servicePath" type="String">
    /// </param>
    /// <param name="serviceMethod" type="String">
    /// </param>
    /// <param name="timeOut" type="Number" integer="true">
    /// </param>
    /// <field name="_servicePath" type="String">
    /// </field>
    /// <field name="_serviceMethod" type="String">
    /// </field>
    /// <field name="_timeOut" type="Number" integer="true">
    /// </field>
    /// <field name="_webRequest" type="Sys.Net.WebRequest">
    /// </field>
    /// <field name="_successCallback" type="Sys.Net.WebServiceSuccessCallback">
    /// </field>
    /// <field name="_failureCallback" type="Sys.Net.WebServiceFailureCallback">
    /// </field>
    this._servicePath = servicePath;
    this._serviceMethod = serviceMethod;
    this._timeOut = timeOut;
}
SpottedScript.Utils._providerService.prototype = {
    _servicePath: null,
    _serviceMethod: null,
    _timeOut: 0,
    _webRequest: null,
    _successCallback: null,
    _failureCallback: null,
    callWebService: function SpottedScript_Utils__providerService$callWebService(parameters, successCallback, failureCallback, userContext) {
        /// <param name="parameters" type="Object">
        /// </param>
        /// <param name="successCallback" type="Sys.Net.WebServiceSuccessCallback">
        /// </param>
        /// <param name="failureCallback" type="Sys.Net.WebServiceFailureCallback">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        this._successCallback = successCallback;
        this._failureCallback = failureCallback;
        if (this._webRequest != null) {
            Utils.Trace.write('ABORT');
            this._webRequest.get_executor().abort();
        }
        this._webRequest = Sys.Net.WebServiceProxy.invoke(this._servicePath, this._serviceMethod, false, parameters, Function.createDelegate(this, this._successCallback2), Function.createDelegate(this, this._failureCallback2), userContext, this._timeOut);
    },
    _successCallback2: function SpottedScript_Utils__providerService$_successCallback2(result, userContext, methodName) {
        /// <param name="result" type="Object">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._webRequest = null;
        this._successCallback(result, userContext, methodName);
    },
    _failureCallback2: function SpottedScript_Utils__providerService$_failureCallback2(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._webRequest = null;
        this._failureCallback(error, userContext, methodName);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Utils._pagedProviderService
SpottedScript.Utils._pagedProviderService = function SpottedScript_Utils__pagedProviderService(servicePath, serviceMethod, timeOut) {
    /// <param name="servicePath" type="String">
    /// </param>
    /// <param name="serviceMethod" type="String">
    /// </param>
    /// <param name="timeOut" type="Number" integer="true">
    /// </param>
    SpottedScript.Utils._pagedProviderService.initializeBase(this, [ servicePath, serviceMethod, timeOut ]);
}
SpottedScript.Utils._pagedProviderService.prototype = {
    callWebService: function SpottedScript_Utils__pagedProviderService$callWebService(firstRowIndex, lastRowIndex, parameters, successCallback, failureCallback, userContext) {
        /// <param name="firstRowIndex" type="Number" integer="true">
        /// </param>
        /// <param name="lastRowIndex" type="Number" integer="true">
        /// </param>
        /// <param name="parameters" type="Object">
        /// </param>
        /// <param name="successCallback" type="Sys.Net.WebServiceSuccessCallback">
        /// </param>
        /// <param name="failureCallback" type="Sys.Net.WebServiceFailureCallback">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        var pagedParameters = {};
        pagedParameters['firstRowIndex'] = firstRowIndex;
        pagedParameters['lastRowIndex'] = lastRowIndex;
        var $dict1 = parameters;
        for (var $key2 in $dict1) {
            var de = { key: $key2, value: $dict1[$key2] };
            pagedParameters[de.key] = de.value;
        }
        SpottedScript.Utils._pagedProviderService.callBaseMethod(this, 'callWebService', [ pagedParameters, successCallback, failureCallback, userContext ]);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Utils._cachedPagedProvider
SpottedScript.Utils._cachedPagedProvider = function SpottedScript_Utils__cachedPagedProvider(provider) {
    /// <param name="provider" type="SpottedScript.Utils._iPagedProviderService">
    /// </param>
    /// <field name="_cachePageSizeMultiplier" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_provider" type="SpottedScript.Utils._iPagedProviderService">
    /// </field>
    /// <field name="_key" type="String">
    /// </field>
    /// <field name="_cachedDataStore" type="Object">
    /// </field>
    /// <field name="_totalKnownDataItemsStore" type="Object">
    /// </field>
    /// <field name="_thereIsNoMoreDataInDatabaseStore" type="Object">
    /// </field>
    /// <field name="_dataRetrievedCallback" type="SpottedScript.Utils._dataRetrieved">
    /// </field>
    /// <field name="_noDataRetrievedCallback" type="SpottedScript.Utils._noDataRetrieved">
    /// </field>
    /// <field name="_pageNumber" type="Number" integer="true">
    /// </field>
    /// <field name="_pageSize" type="Number" integer="true">
    /// </field>
    /// <field name="_parameters" type="Object">
    /// </field>
    /// <field name="_firstRowIndexToGet" type="Number" integer="true">
    /// </field>
    /// <field name="_lastRowIndexToGet" type="Number" integer="true">
    /// </field>
    this._firstRowIndexToGet = -1;
    this._lastRowIndexToGet = -1;
    this._provider = provider;
    this._setUpCaches();
}
SpottedScript.Utils._cachedPagedProvider._getKey = function SpottedScript_Utils__cachedPagedProvider$_getKey(parameters) {
    /// <param name="parameters" type="Object">
    /// </param>
    /// <returns type="String"></returns>
    var sb = new Sys.StringBuilder();
    var $dict1 = parameters;
    for (var $key2 in $dict1) {
        var de = { key: $key2, value: $dict1[$key2] };
        sb.append(de.key);
        sb.append(':');
        sb.append(de.value.toString());
        sb.append(';');
    }
    return sb.toString();
}
SpottedScript.Utils._cachedPagedProvider.prototype = {
    _provider: null,
    _key: null,
    _cachedDataStore: null,
    get__cachedData: function SpottedScript_Utils__cachedPagedProvider$get__cachedData() {
        /// <value type="Array"></value>
        return (this._cachedDataStore[this._key] || (this._cachedDataStore[this._key] = []));
    },
    _totalKnownDataItemsStore: null,
    get__totalKnownDataItems: function SpottedScript_Utils__cachedPagedProvider$get__totalKnownDataItems() {
        /// <value type="Number" integer="true"></value>
        return (this._totalKnownDataItemsStore[this._key] || (this._totalKnownDataItemsStore[this._key] = 0));
    },
    set__totalKnownDataItems: function SpottedScript_Utils__cachedPagedProvider$set__totalKnownDataItems(value) {
        /// <value type="Number" integer="true"></value>
        this._totalKnownDataItemsStore[this._key] = value;
        return value;
    },
    _thereIsNoMoreDataInDatabaseStore: null,
    get__thereIsNoMoreDataInDatabase: function SpottedScript_Utils__cachedPagedProvider$get__thereIsNoMoreDataInDatabase() {
        /// <value type="Boolean"></value>
        return this._thereIsNoMoreDataInDatabaseStore[this._key];
    },
    set__thereIsNoMoreDataInDatabase: function SpottedScript_Utils__cachedPagedProvider$set__thereIsNoMoreDataInDatabase(value) {
        /// <value type="Boolean"></value>
        this._thereIsNoMoreDataInDatabaseStore[this._key] = value;
        return value;
    },
    _flush: function SpottedScript_Utils__cachedPagedProvider$_flush() {
        this._setUpCaches();
    },
    _setUpCaches: function SpottedScript_Utils__cachedPagedProvider$_setUpCaches() {
        this._cachedDataStore = {};
        this._totalKnownDataItemsStore = {};
        this._thereIsNoMoreDataInDatabaseStore = {};
    },
    _dataRetrievedCallback: null,
    _noDataRetrievedCallback: null,
    _pageNumber: 0,
    _pageSize: 0,
    _parameters: null,
    _get: function SpottedScript_Utils__cachedPagedProvider$_get(pageNumber, pageSize, parameters, dataRetrievedCallback, noDataRetrievedCallback) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <param name="pageSize" type="Number" integer="true">
        /// </param>
        /// <param name="parameters" type="Object">
        /// </param>
        /// <param name="dataRetrievedCallback" type="SpottedScript.Utils._dataRetrieved">
        /// </param>
        /// <param name="noDataRetrievedCallback" type="SpottedScript.Utils._noDataRetrieved">
        /// </param>
        this._dataRetrievedCallback = dataRetrievedCallback;
        this._noDataRetrievedCallback = noDataRetrievedCallback;
        this._pageNumber = pageNumber;
        this._pageSize = pageSize;
        this._parameters = parameters;
        this._key = SpottedScript.Utils._cachedPagedProvider._getKey(parameters);
        var requestedPageData = new Array(0);
        var someRequestedDataIsNotInTheCacheAlready = false;
        for (var i = (pageNumber - 1) * pageSize; i < pageNumber * pageSize - 1; i++) {
            if (this.get__cachedData()[i] != null) {
                if (SpottedScript.Utils._noMoreData.isInstanceOfType(this.get__cachedData()[i])) {
                    break;
                }
                requestedPageData[requestedPageData.length] = this.get__cachedData()[i];
            }
            else {
                someRequestedDataIsNotInTheCacheAlready = true;
                break;
            }
        }
        if (someRequestedDataIsNotInTheCacheAlready && !this.get__thereIsNoMoreDataInDatabase()) {
            var cachePageSize = SpottedScript.Utils._cachedPagedProvider._cachePageSizeMultiplier * pageSize;
            this._firstRowIndexToGet = Math.floor((pageNumber - 1) / SpottedScript.Utils._cachedPagedProvider._cachePageSizeMultiplier) * cachePageSize;
            this._lastRowIndexToGet = this._firstRowIndexToGet + cachePageSize - 1;
            if (this.get__cachedData() != null) {
                for (; this._firstRowIndexToGet < this._lastRowIndexToGet; this._firstRowIndexToGet++) {
                    if (this.get__cachedData()[this._firstRowIndexToGet] == null) {
                        break;
                    }
                }
            }
            this._provider.callWebService(this._firstRowIndexToGet, this._lastRowIndexToGet + 1, parameters, Function.createDelegate(this, this._successCallback), Function.createDelegate(this, this._failureCallback), null);
        }
        else {
            dataRetrievedCallback(requestedPageData, this.get__totalKnownDataItems(), !this.get__thereIsNoMoreDataInDatabase());
        }
    },
    _failureCallback: function SpottedScript_Utils__cachedPagedProvider$_failureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Utils.Trace.webServiceFailure(error, userContext, methodName);
        this._noDataRetrievedCallback();
    },
    _successCallback: function SpottedScript_Utils__cachedPagedProvider$_successCallback(dataObject, userContext, methodName) {
        /// <param name="dataObject" type="Object">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        var data = dataObject;
        for (var i = 0; i < data.length; i++) {
            this.get__cachedData()[this._firstRowIndexToGet + i] = data[i];
        }
        if (data.length > this._lastRowIndexToGet - this._firstRowIndexToGet + 1) {
            this.set__totalKnownDataItems(this._lastRowIndexToGet + 1);
            this.set__thereIsNoMoreDataInDatabase(false);
        }
        else {
            this.set__totalKnownDataItems(this._firstRowIndexToGet + data.length);
            this.set__thereIsNoMoreDataInDatabase(true);
            this.get__cachedData()[this._firstRowIndexToGet + data.length] = new SpottedScript.Utils._noMoreData();
        }
        this._get(this._pageNumber, this._pageSize, this._parameters, this._dataRetrievedCallback, this._noDataRetrievedCallback);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Utils._noMoreData
SpottedScript.Utils._noMoreData = function SpottedScript_Utils__noMoreData() {
}
Type.registerNamespace('Spotted.System.Text');

////////////////////////////////////////////////////////////////////////////////
// Spotted.System.Text.StringBuilder
Spotted.System.Text.StringBuilder = function Spotted_System_Text_StringBuilder() {
    /// <field name="_stringArray" type="Array">
    /// </field>
    this._stringArray = [];
}
Spotted.System.Text.StringBuilder.prototype = {
    _stringArray: null,
    append: function Spotted_System_Text_StringBuilder$append(s) {
        /// <param name="s" type="String">
        /// </param>
        this._stringArray[this._stringArray.length] = s;
    },
    toString: function Spotted_System_Text_StringBuilder$toString() {
        /// <returns type="String"></returns>
        return this._stringArray.join('');
    },
    appendAttribute: function Spotted_System_Text_StringBuilder$appendAttribute(name, value) {
        /// <param name="name" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        this._stringArray[this._stringArray.length] = ' ';
        this._stringArray[this._stringArray.length] = name;
        this._stringArray[this._stringArray.length] = '=\"';
        this._stringArray[this._stringArray.length] = value.replace(new RegExp('\"', 'g'), '&#34;');
        this._stringArray[this._stringArray.length] = '\"';
    }
}
Type.registerNamespace('SpottedScript');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Misc
SpottedScript.Misc = function SpottedScript_Misc() {
}
SpottedScript.Misc.getPicUrlFromGuid = function SpottedScript_Misc$getPicUrlFromGuid(guid) {
    /// <param name="guid" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var s = eval('StoragePath(\'' + guid + '\');');
    return s;
}
SpottedScript.Misc.redirect = function SpottedScript_Misc$redirect(url) {
    /// <param name="url" type="String">
    /// </param>
    eval('window.location = \'' + url + '\'');
}
SpottedScript.Misc.addHoverText = function SpottedScript_Misc$addHoverText(el, hoverText) {
    /// <param name="el" type="Object" domElement="true">
    /// </param>
    /// <param name="hoverText" type="String">
    /// </param>
    $addHandler(el, 'mouseover', Function.createDelegate(null, function() {
        SpottedScript.Misc._stt(hoverText);
    }));
    $addHandler(el, 'mouseout', Function.createDelegate(null, function() {
        SpottedScript.Misc._htm();
    }));
}
SpottedScript.Misc.redirectToAnchor = function SpottedScript_Misc$redirectToAnchor(anchorName) {
    /// <param name="anchorName" type="String">
    /// </param>
    window.location.hash = anchorName;
}
SpottedScript.Misc.showWaitingCursor = function SpottedScript_Misc$showWaitingCursor() {
    eval('ShowWaitingCursor();');
}
SpottedScript.Misc.hideWaitingCursor = function SpottedScript_Misc$hideWaitingCursor() {
    eval('HideWaitingCursor();');
}
SpottedScript.Misc._stt = function SpottedScript_Misc$_stt(p) {
    /// <param name="p" type="String">
    /// </param>
    eval('stt(\'' + p + '\');');
}
SpottedScript.Misc._htm = function SpottedScript_Misc$_htm() {
    eval('htm();');
}
SpottedScript.Misc.get_browserIsFirefox = function SpottedScript_Misc$get_browserIsFirefox() {
    /// <value type="Boolean"></value>
    return Sys.Browser.agent === Sys.Browser.Firefox;
}
SpottedScript.Misc.get_browserIsIE = function SpottedScript_Misc$get_browserIsIE() {
    /// <value type="Boolean"></value>
    return Sys.Browser.agent === Sys.Browser.InternetExplorer;
}
SpottedScript.Misc.get_browserVersion = function SpottedScript_Misc$get_browserVersion() {
    /// <value type="Number"></value>
    return Sys.Browser.version;
}
SpottedScript.Misc.combineAction = function SpottedScript_Misc$combineAction(runFirst, runSecond) {
    /// <param name="runFirst" type="ScriptSharpLibrary.Action">
    /// </param>
    /// <param name="runSecond" type="ScriptSharpLibrary.Action">
    /// </param>
    /// <returns type="ScriptSharpLibrary.Action"></returns>
    var composite = Function.createDelegate(null, function() {
        if (runFirst != null) {
            runFirst();
        }
        if (runSecond != null) {
            runSecond();
        }
    });
    return composite;
}
SpottedScript.Misc.combineEventHandler = function SpottedScript_Misc$combineEventHandler(runFirst, runSecond) {
    /// <param name="runFirst" type="Sys.EventHandler">
    /// </param>
    /// <param name="runSecond" type="Sys.EventHandler">
    /// </param>
    /// <returns type="Sys.EventHandler"></returns>
    var composite = Function.createDelegate(null, function(sender, e) {
        if (runFirst != null) {
            runFirst(sender, e);
        }
        if (runSecond != null) {
            runSecond(sender, e);
        }
    });
    return composite;
}
SpottedScript.Misc._debug = function SpottedScript_Misc$_debug() {
    debugger;;
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.IntEventArgs
SpottedScript.IntEventArgs = function SpottedScript_IntEventArgs(value) {
    /// <param name="value" type="Number" integer="true">
    /// </param>
    /// <field name="value" type="Number" integer="true">
    /// </field>
    SpottedScript.IntEventArgs.initializeBase(this);
    this.value = value;
}
SpottedScript.IntEventArgs.prototype = {
    value: 0
}
Type.registerNamespace('Spotted.WebServices.Pages.MapBrowser.Tab');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Pages.MapBrowser.Tab.Service
Spotted.WebServices.Pages.MapBrowser.Tab.Service = function Spotted_WebServices_Pages_MapBrowser_Tab_Service() {
}
Spotted.WebServices.Pages.MapBrowser.Tab.Service.getEvents = function Spotted_WebServices_Pages_MapBrowser_Tab_Service$getEvents(firstRowIndex, lastRowIndex, north, south, east, west, showFuture, showPast, orderDesc, musicTypeK, success, failure, userContext, timeout) {
    /// <param name="firstRowIndex" type="Number" integer="true">
    /// </param>
    /// <param name="lastRowIndex" type="Number" integer="true">
    /// </param>
    /// <param name="north" type="Number">
    /// </param>
    /// <param name="south" type="Number">
    /// </param>
    /// <param name="east" type="Number">
    /// </param>
    /// <param name="west" type="Number">
    /// </param>
    /// <param name="showFuture" type="Boolean">
    /// </param>
    /// <param name="showPast" type="Boolean">
    /// </param>
    /// <param name="orderDesc" type="Boolean">
    /// </param>
    /// <param name="musicTypeK" type="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Pages.MapBrowser.Tab.ServiceMapItemArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['firstRowIndex'] = firstRowIndex;
    _parameters['lastRowIndex'] = lastRowIndex;
    _parameters['north'] = north;
    _parameters['south'] = south;
    _parameters['east'] = east;
    _parameters['west'] = west;
    _parameters['showFuture'] = showFuture;
    _parameters['showPast'] = showPast;
    _parameters['orderDesc'] = orderDesc;
    _parameters['musicTypeK'] = musicTypeK;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Pages/MapBrowser/Tab/Service.asmx', 'GetEvents', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Pages.MapBrowser.Tab.Service.getVenues = function Spotted_WebServices_Pages_MapBrowser_Tab_Service$getVenues(firstRowIndex, lastRowIndex, north, south, east, west, success, failure, userContext, timeout) {
    /// <param name="firstRowIndex" type="Number" integer="true">
    /// </param>
    /// <param name="lastRowIndex" type="Number" integer="true">
    /// </param>
    /// <param name="north" type="Number">
    /// </param>
    /// <param name="south" type="Number">
    /// </param>
    /// <param name="east" type="Number">
    /// </param>
    /// <param name="west" type="Number">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Pages.MapBrowser.Tab.ServiceMapItemArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['firstRowIndex'] = firstRowIndex;
    _parameters['lastRowIndex'] = lastRowIndex;
    _parameters['north'] = north;
    _parameters['south'] = south;
    _parameters['east'] = east;
    _parameters['west'] = west;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Pages/MapBrowser/Tab/Service.asmx', 'GetVenues', false, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted');

////////////////////////////////////////////////////////////////////////////////
// Spotted.GenericPage
Spotted.GenericPage = function Spotted_GenericPage() {
}
Spotted.GenericPage.clientRequest = function Spotted_GenericPage$clientRequest(typeName, methodName, args, success, failure, userContext, timeout) {
    /// <param name="typeName" type="String">
    /// </param>
    /// <param name="methodName" type="String">
    /// </param>
    /// <param name="args" type="Array" elementType="Object">
    /// </param>
    /// <param name="success" type="Spotted.GenericPageObjectWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['typeName'] = typeName;
    _parameters['methodName'] = methodName;
    _parameters['args'] = args;
    return Sys.Net.WebServiceProxy.invoke('/GenericPage.asmx', 'ClientRequest', false, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.ChatService
Spotted.WebServices.ChatService = function Spotted_WebServices_ChatService() {
}
Spotted.WebServices.ChatService.send = function Spotted_WebServices_ChatService$send(message, roomGuidString, lastItemGuidString, sessionID, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="message" type="String">
    /// </param>
    /// <param name="roomGuidString" type="String">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceRefreshStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['message'] = message;
    _parameters['roomGuidString'] = roomGuidString;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'Send', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.resetSessionAndGetState = function Spotted_WebServices_ChatService$resetSessionAndGetState(isFirstRequest, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="isFirstRequest" type="Boolean">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceGetStateStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['isFirstRequest'] = isFirstRequest;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'ResetSessionAndGetState', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.deleteArchive = function Spotted_WebServices_ChatService$deleteArchive(roomGuid, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceDeleteArchiveStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['roomGuid'] = roomGuid;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'DeleteArchive', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.getArchive = function Spotted_WebServices_ChatService$getArchive(roomGuid, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceArchiveStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['roomGuid'] = roomGuid;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'GetArchive', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.refresh = function Spotted_WebServices_ChatService$refresh(isFirstRequest, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="isFirstRequest" type="Boolean">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceRefreshStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['isFirstRequest'] = isFirstRequest;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'Refresh', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.moreInfo = function Spotted_WebServices_ChatService$moreInfo(roomGuid, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceMoreInfoStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['roomGuid'] = roomGuid;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'MoreInfo', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.pin = function Spotted_WebServices_ChatService$pin(clientID, roomGuid, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="clientID" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServicePinStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['clientID'] = clientID;
    _parameters['roomGuid'] = roomGuid;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'Pin', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.switchPhotoRoom = function Spotted_WebServices_ChatService$switchPhotoRoom(clientID, roomGuid, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="clientID" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServicePinStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['clientID'] = clientID;
    _parameters['roomGuid'] = roomGuid;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'SwitchPhotoRoom', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.rePin = function Spotted_WebServices_ChatService$rePin(clientID, roomGuid, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="clientID" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceRefreshStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['clientID'] = clientID;
    _parameters['roomGuid'] = roomGuid;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'RePin', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.unPin = function Spotted_WebServices_ChatService$unPin(clientID, roomGuid, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="clientID" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceUnPinStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['clientID'] = clientID;
    _parameters['roomGuid'] = roomGuid;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'UnPin', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.star = function Spotted_WebServices_ChatService$star(clientID, roomGuid, starred, lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="clientID" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="starred" type="Boolean">
    /// </param>
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceRefreshStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['clientID'] = clientID;
    _parameters['roomGuid'] = roomGuid;
    _parameters['starred'] = starred;
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'Star', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.storeUpdatedRoomListOrder = function Spotted_WebServices_ChatService$storeUpdatedRoomListOrder(lastItemGuidString, sessionID, lastActionTicks, pageUrl, roomState, success, failure, userContext, timeout) {
    /// <param name="lastItemGuidString" type="String">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="pageUrl" type="String">
    /// </param>
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceRefreshStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['lastItemGuidString'] = lastItemGuidString;
    _parameters['sessionID'] = sessionID;
    _parameters['lastActionTicks'] = lastActionTicks;
    _parameters['pageUrl'] = pageUrl;
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'StoreUpdatedRoomListOrder', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.storeState = function Spotted_WebServices_ChatService$storeState(roomState, success, failure, userContext, timeout) {
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceBooleanWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['roomState'] = roomState;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'StoreState', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.ChatService.randomWait = function Spotted_WebServices_ChatService$randomWait(min, max, success, failure, userContext, timeout) {
    /// <param name="min" type="Number" integer="true">
    /// </param>
    /// <param name="max" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.ChatServiceVoidWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['min'] = min;
    _parameters['max'] = max;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/ChatService.asmx', 'RandomWait', false, _parameters, success, failure, userContext, timeout);
}
////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.AutoComplete
Spotted.WebServices.AutoComplete = function Spotted_WebServices_AutoComplete() {
}
Spotted.WebServices.AutoComplete.getTags = function Spotted_WebServices_AutoComplete$getTags(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetTags', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getTagSearchString = function Spotted_WebServices_AutoComplete$getTagSearchString(prefixText, count, success, failure, userContext, timeout) {
    /// <param name="prefixText" type="String">
    /// </param>
    /// <param name="count" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteStringArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['prefixText'] = prefixText;
    _parameters['count'] = count;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetTagSearchString', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getGroupMembers = function Spotted_WebServices_AutoComplete$getGroupMembers(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['text'] = text;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetGroupMembers', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getUsrsPublic = function Spotted_WebServices_AutoComplete$getUsrsPublic(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetUsrsPublic', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getBuddiesThenUsrs = function Spotted_WebServices_AutoComplete$getBuddiesThenUsrs(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetBuddiesThenUsrs', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getBuddies = function Spotted_WebServices_AutoComplete$getBuddies(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetBuddies', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getBrands = function Spotted_WebServices_AutoComplete$getBrands(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetBrands', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getPromotersWithK = function Spotted_WebServices_AutoComplete$getPromotersWithK(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetPromotersWithK', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getUsersWithK = function Spotted_WebServices_AutoComplete$getUsersWithK(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetUsersWithK', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getPlacesEnabled = function Spotted_WebServices_AutoComplete$getPlacesEnabled(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['text'] = text;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetPlacesEnabled', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getVenuesFull = function Spotted_WebServices_AutoComplete$getVenuesFull(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['text'] = text;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetVenuesFull', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getVenues = function Spotted_WebServices_AutoComplete$getVenues(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetVenues', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getEvents = function Spotted_WebServices_AutoComplete$getEvents(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['text'] = text;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetEvents', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getPlaces = function Spotted_WebServices_AutoComplete$getPlaces(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['text'] = text;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetPlaces', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getSiteSearchResults = function Spotted_WebServices_AutoComplete$getSiteSearchResults(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetSiteSearchResults', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getGroups = function Spotted_WebServices_AutoComplete$getGroups(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetGroups', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getGroupsNoBrands = function Spotted_WebServices_AutoComplete$getGroupsNoBrands(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['text'] = text;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetGroupsNoBrands', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getObjects = function Spotted_WebServices_AutoComplete$getObjects(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['text'] = text;
    _parameters['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetObjects', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.AutoComplete.getCountries = function Spotted_WebServices_AutoComplete$getCountries(get, text, parameters, success, failure, userContext, timeout) {
    /// <param name="get" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.AutoCompleteSuggestionArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['get'] = get;
    _parameters['text'] = text;
    _parameters['parameters'] = parameters;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/AutoComplete.asmx', 'GetCountries', true, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.MultiBuddyChooser');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.MultiBuddyChooser.Service
Spotted.WebServices.Controls.MultiBuddyChooser.Service = function Spotted_WebServices_Controls_MultiBuddyChooser_Service() {
}
Spotted.WebServices.Controls.MultiBuddyChooser.Service.getPlacesAndMusicTypes = function Spotted_WebServices_Controls_MultiBuddyChooser_Service$getPlacesAndMusicTypes(success, failure, userContext, timeout) {
    /// <param name="success" type="Spotted.WebServices.Controls.MultiBuddyChooser.ServiceGetMusicTypesAndPlacesResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/MultiBuddyChooser/Service.asmx', 'GetPlacesAndMusicTypes', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.MultiBuddyChooser.Service.resolveUsrsFromMultiBuddyChooserValues = function Spotted_WebServices_Controls_MultiBuddyChooser_Service$resolveUsrsFromMultiBuddyChooserValues(values, success, failure, userContext, timeout) {
    /// <param name="values" type="Array" elementType="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.MultiBuddyChooser.ServiceDictionaryWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['values'] = values;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/MultiBuddyChooser/Service.asmx', 'ResolveUsrsFromMultiBuddyChooserValues', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.MultiBuddyChooser.Service.getBuddiesSelectListHtml = function Spotted_WebServices_Controls_MultiBuddyChooser_Service$getBuddiesSelectListHtml(success, failure, userContext, timeout) {
    /// <param name="success" type="Spotted.WebServices.Controls.MultiBuddyChooser.ServiceStringWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/MultiBuddyChooser/Service.asmx', 'GetBuddiesSelectListHtml', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.MultiBuddyChooser.Service.createUsrFromEmailAndReturnK = function Spotted_WebServices_Controls_MultiBuddyChooser_Service$createUsrFromEmailAndReturnK(textEnteredByUser, success, failure, userContext, timeout) {
    /// <param name="textEnteredByUser" type="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.MultiBuddyChooser.ServiceInt32WebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['textEnteredByUser'] = textEnteredByUser;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/MultiBuddyChooser/Service.asmx', 'CreateUsrFromEmailAndReturnK', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.MultiBuddyChooser.Service.createUsrsFromEmails = function Spotted_WebServices_Controls_MultiBuddyChooser_Service$createUsrsFromEmails(spaceSeparatedListOfEmailAddresses, addAsBuddies, success, failure, userContext, timeout) {
    /// <param name="spaceSeparatedListOfEmailAddresses" type="String">
    /// </param>
    /// <param name="addAsBuddies" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.MultiBuddyChooser.ServiceInt32WebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['spaceSeparatedListOfEmailAddresses'] = spaceSeparatedListOfEmailAddresses;
    _parameters['addAsBuddies'] = addAsBuddies;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/MultiBuddyChooser/Service.asmx', 'CreateUsrsFromEmails', false, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.CommentsDisplay');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.CommentsDisplay.Service
Spotted.WebServices.Controls.CommentsDisplay.Service = function Spotted_WebServices_Controls_CommentsDisplay_Service() {
}
Spotted.WebServices.Controls.CommentsDisplay.Service.getThreadComments = function Spotted_WebServices_Controls_CommentsDisplay_Service$getThreadComments(threadK, pageNumber, getCommentsOnly, success, failure, userContext, timeout) {
    /// <param name="threadK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="getCommentsOnly" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceCommentResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['threadK'] = threadK;
    _parameters['pageNumber'] = pageNumber;
    _parameters['getCommentsOnly'] = getCommentsOnly;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'GetThreadComments', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.lolAtComment = function Spotted_WebServices_Controls_CommentsDisplay_Service$lolAtComment(commentK, success, failure, userContext, timeout) {
    /// <param name="commentK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceStringWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['commentK'] = commentK;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'LolAtComment', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.deleteComment = function Spotted_WebServices_Controls_CommentsDisplay_Service$deleteComment(commentK, success, failure, userContext, timeout) {
    /// <param name="commentK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceBooleanWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['commentK'] = commentK;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'DeleteComment', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.setThreadUsr = function Spotted_WebServices_Controls_CommentsDisplay_Service$setThreadUsr(threadK, page, success, failure, userContext, timeout) {
    /// <param name="threadK" type="Number" integer="true">
    /// </param>
    /// <param name="page" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceVoidWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['threadK'] = threadK;
    _parameters['page'] = page;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'SetThreadUsr', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.createNewThreadInGroup = function Spotted_WebServices_Controls_CommentsDisplay_Service$createNewThreadInGroup(groupK, discussableObjectType, discussableObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, isPrivate, success, failure, userContext, timeout) {
    /// <param name="groupK" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="duplicateGuid" type="String">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="isNews" type="Boolean">
    /// </param>
    /// <param name="inviteUsrOptions" type="Array" elementType="String">
    /// </param>
    /// <param name="isPrivate" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceStringWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['groupK'] = groupK;
    _parameters['discussableObjectType'] = discussableObjectType;
    _parameters['discussableObjectK'] = discussableObjectK;
    _parameters['duplicateGuid'] = duplicateGuid;
    _parameters['rawCommentHtml'] = rawCommentHtml;
    _parameters['formatting'] = formatting;
    _parameters['isNews'] = isNews;
    _parameters['inviteUsrOptions'] = inviteUsrOptions;
    _parameters['isPrivate'] = isPrivate;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'CreateNewThreadInGroup', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.createPrivateThread = function Spotted_WebServices_Controls_CommentsDisplay_Service$createPrivateThread(discussableObjectType, discussableObjectK, duplicateGuid, rawCommentHtml, formatting, inviteUsrOptions, isSealed, success, failure, userContext, timeout) {
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="duplicateGuid" type="String">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="inviteUsrOptions" type="Array" elementType="String">
    /// </param>
    /// <param name="isSealed" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceStringWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['discussableObjectType'] = discussableObjectType;
    _parameters['discussableObjectK'] = discussableObjectK;
    _parameters['duplicateGuid'] = duplicateGuid;
    _parameters['rawCommentHtml'] = rawCommentHtml;
    _parameters['formatting'] = formatting;
    _parameters['inviteUsrOptions'] = inviteUsrOptions;
    _parameters['isSealed'] = isSealed;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'CreatePrivateThread', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.createReply = function Spotted_WebServices_Controls_CommentsDisplay_Service$createReply(discussableObjectType, discussableObjectK, threadK, duplicateGuid, rawCommentHtml, formatting, lastKnownCommentK, inviteUsrOptions, success, failure, userContext, timeout) {
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="threadK" type="Number" integer="true">
    /// </param>
    /// <param name="duplicateGuid" type="String">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="lastKnownCommentK" type="Number" integer="true">
    /// </param>
    /// <param name="inviteUsrOptions" type="Array" elementType="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceCommentStubArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['discussableObjectType'] = discussableObjectType;
    _parameters['discussableObjectK'] = discussableObjectK;
    _parameters['threadK'] = threadK;
    _parameters['duplicateGuid'] = duplicateGuid;
    _parameters['rawCommentHtml'] = rawCommentHtml;
    _parameters['formatting'] = formatting;
    _parameters['lastKnownCommentK'] = lastKnownCommentK;
    _parameters['inviteUsrOptions'] = inviteUsrOptions;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'CreateReply', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.createNewPublicThread = function Spotted_WebServices_Controls_CommentsDisplay_Service$createNewPublicThread(discussableObjectType, discussableObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, success, failure, userContext, timeout) {
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="duplicateGuid" type="String">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="isNews" type="Boolean">
    /// </param>
    /// <param name="inviteUsrOptions" type="Array" elementType="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceStringWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['discussableObjectType'] = discussableObjectType;
    _parameters['discussableObjectK'] = discussableObjectK;
    _parameters['duplicateGuid'] = duplicateGuid;
    _parameters['rawCommentHtml'] = rawCommentHtml;
    _parameters['formatting'] = formatting;
    _parameters['isNews'] = isNews;
    _parameters['inviteUsrOptions'] = inviteUsrOptions;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'CreateNewPublicThread', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.createPublicThread = function Spotted_WebServices_Controls_CommentsDisplay_Service$createPublicThread(discussableObjectType, discussableObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, success, failure, userContext, timeout) {
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="duplicateGuid" type="String">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="isNews" type="Boolean">
    /// </param>
    /// <param name="inviteUsrOptions" type="Array" elementType="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceCommentStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['discussableObjectType'] = discussableObjectType;
    _parameters['discussableObjectK'] = discussableObjectK;
    _parameters['duplicateGuid'] = duplicateGuid;
    _parameters['rawCommentHtml'] = rawCommentHtml;
    _parameters['formatting'] = formatting;
    _parameters['isNews'] = isNews;
    _parameters['inviteUsrOptions'] = inviteUsrOptions;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'CreatePublicThread', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.getNewGuid = function Spotted_WebServices_Controls_CommentsDisplay_Service$getNewGuid(success, failure, userContext, timeout) {
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceStringWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'GetNewGuid', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.cleanHtml = function Spotted_WebServices_Controls_CommentsDisplay_Service$cleanHtml(html, success, failure, userContext, timeout) {
    /// <param name="html" type="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceStringWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['html'] = html;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'CleanHtml', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.CommentsDisplay.Service.getPreviewHtml = function Spotted_WebServices_Controls_CommentsDisplay_Service$getPreviewHtml(previewType, rawCommentHtml, formatting, success, failure, userContext, timeout) {
    /// <param name="previewType" type="Number" integer="true">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.CommentsDisplay.ServiceStringArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['previewType'] = previewType;
    _parameters['rawCommentHtml'] = rawCommentHtml;
    _parameters['formatting'] = formatting;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/CommentsDisplay/Service.asmx', 'GetPreviewHtml', false, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.PhotoControl');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.PhotoControl.Service
Spotted.WebServices.Controls.PhotoControl.Service = function Spotted_WebServices_Controls_PhotoControl_Service() {
}
Spotted.WebServices.Controls.PhotoControl.Service.getRecentVideos = function Spotted_WebServices_Controls_PhotoControl_Service$getRecentVideos(pageNumber, success, failure, userContext, timeout) {
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServicePhotoResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['pageNumber'] = pageNumber;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetRecentVideos', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByEventAndPage = function Spotted_WebServices_Controls_PhotoControl_Service$getPhotosByEventAndPage(eventK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="eventK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServicePhotoResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['eventK'] = eventK;
    _parameters['pageNumber'] = pageNumber;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetPhotosByEventAndPage', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByGalleryAndPage = function Spotted_WebServices_Controls_PhotoControl_Service$getPhotosByGalleryAndPage(galleryK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="galleryK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServicePhotoResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['galleryK'] = galleryK;
    _parameters['pageNumber'] = pageNumber;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetPhotosByGalleryAndPage', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByArticle = function Spotted_WebServices_Controls_PhotoControl_Service$getPhotosByArticle(articleK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="articleK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServicePhotoResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['articleK'] = articleK;
    _parameters['pageNumber'] = pageNumber;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetPhotosByArticle', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.getPhotosOfUsr = function Spotted_WebServices_Controls_PhotoControl_Service$getPhotosOfUsr(usrK, pageNumber, spottedByUsrK, success, failure, userContext, timeout) {
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="spottedByUsrK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServicePhotoResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['usrK'] = usrK;
    _parameters['pageNumber'] = pageNumber;
    _parameters['spottedByUsrK'] = spottedByUsrK;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetPhotosOfUsr', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.getFavouritePhotosOfUsr = function Spotted_WebServices_Controls_PhotoControl_Service$getFavouritePhotosOfUsr(usrK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServicePhotoResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['usrK'] = usrK;
    _parameters['pageNumber'] = pageNumber;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetFavouritePhotosOfUsr', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByGroup = function Spotted_WebServices_Controls_PhotoControl_Service$getPhotosByGroup(groupK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="groupK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServicePhotoResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['groupK'] = groupK;
    _parameters['pageNumber'] = pageNumber;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetPhotosByGroup', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByTag = function Spotted_WebServices_Controls_PhotoControl_Service$getPhotosByTag(tagK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="tagK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServicePhotoResultWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['tagK'] = tagK;
    _parameters['pageNumber'] = pageNumber;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetPhotosByTag', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.setCurrentUsrSpottedInPhoto = function Spotted_WebServices_Controls_PhotoControl_Service$setCurrentUsrSpottedInPhoto(photoK, isInPhoto, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isInPhoto" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServiceStringArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['photoK'] = photoK;
    _parameters['isInPhoto'] = isInPhoto;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'SetCurrentUsrSpottedInPhoto', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.setUsrSpottedInPhoto = function Spotted_WebServices_Controls_PhotoControl_Service$setUsrSpottedInPhoto(spottedUsrK, photoK, isInPhoto, success, failure, userContext, timeout) {
    /// <param name="spottedUsrK" type="Number" integer="true">
    /// </param>
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isInPhoto" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServiceStringArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['spottedUsrK'] = spottedUsrK;
    _parameters['photoK'] = photoK;
    _parameters['isInPhoto'] = isInPhoto;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'SetUsrSpottedInPhoto', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.setAsPhotoOfWeek = function Spotted_WebServices_Controls_PhotoControl_Service$setAsPhotoOfWeek(photoK, isPhotoOfWeek, photoOfWeekCaption, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isPhotoOfWeek" type="Boolean">
    /// </param>
    /// <param name="photoOfWeekCaption" type="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServiceVoidWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['photoK'] = photoK;
    _parameters['isPhotoOfWeek'] = isPhotoOfWeek;
    _parameters['photoOfWeekCaption'] = photoOfWeekCaption;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'SetAsPhotoOfWeek', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.setIsFavouritePhoto = function Spotted_WebServices_Controls_PhotoControl_Service$setIsFavouritePhoto(photoK, isFavourite, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isFavourite" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServiceVoidWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['photoK'] = photoK;
    _parameters['isFavourite'] = isFavourite;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'SetIsFavouritePhoto', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.incrementViews = function Spotted_WebServices_Controls_PhotoControl_Service$incrementViews(photoK, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServiceVoidWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['photoK'] = photoK;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'IncrementViews', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.getBanners = function Spotted_WebServices_Controls_PhotoControl_Service$getBanners(placeholderClientID, success, failure, userContext, timeout) {
    /// <param name="placeholderClientID" type="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServiceBannerStubArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['placeholderClientID'] = placeholderClientID;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'GetBanners', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.registerBannerHit = function Spotted_WebServices_Controls_PhotoControl_Service$registerBannerHit(bannerK, success, failure, userContext, timeout) {
    /// <param name="bannerK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServiceVoidWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['bannerK'] = bannerK;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'RegisterBannerHit', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PhotoControl.Service.setAsCompetitionGroupPhoto = function Spotted_WebServices_Controls_PhotoControl_Service$setAsCompetitionGroupPhoto(photoK, isCompetitionPhoto, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isCompetitionPhoto" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PhotoControl.ServiceVoidWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['photoK'] = photoK;
    _parameters['isCompetitionPhoto'] = isCompetitionPhoto;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PhotoControl/Service.asmx', 'SetAsCompetitionGroupPhoto', true, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.EventBox');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.EventBox.Service
Spotted.WebServices.Controls.EventBox.Service = function Spotted_WebServices_Controls_EventBox_Service() {
}
Spotted.WebServices.Controls.EventBox.Service.getEventPage = function Spotted_WebServices_Controls_EventBox_Service$getEventPage(key, success, failure, userContext, timeout) {
    /// <param name="key" type="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.EventBox.ServiceEventPageStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['key'] = key;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/EventBox/Service.asmx', 'GetEventPage', false, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.Banners.Generator');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.Banners.Generator.Service
Spotted.WebServices.Controls.Banners.Generator.Service = function Spotted_WebServices_Controls_Banners_Generator_Service() {
}
Spotted.WebServices.Controls.Banners.Generator.Service.getBanner = function Spotted_WebServices_Controls_Banners_Generator_Service$getBanner(positionAsInt, relevantPlacesCsv, relevantMusicTypesCsv, success, failure, userContext, timeout) {
    /// <param name="positionAsInt" type="Number" integer="true">
    /// </param>
    /// <param name="relevantPlacesCsv" type="String">
    /// </param>
    /// <param name="relevantMusicTypesCsv" type="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.Banners.Generator.ServiceBannerRenderInfoWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['positionAsInt'] = positionAsInt;
    _parameters['relevantPlacesCsv'] = relevantPlacesCsv;
    _parameters['relevantMusicTypesCsv'] = relevantMusicTypesCsv;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/Banners/Generator/Service.asmx', 'GetBanner', false, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.LatestChat');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.LatestChat.Service
Spotted.WebServices.Controls.LatestChat.Service = function Spotted_WebServices_Controls_LatestChat_Service() {
}
Spotted.WebServices.Controls.LatestChat.Service.getThreads = function Spotted_WebServices_Controls_LatestChat_Service$getThreads(objectType, objectK, threadsCount, hasGroupObjectFilter, success, failure, userContext, timeout) {
    /// <param name="objectType" type="Number" integer="true">
    /// </param>
    /// <param name="objectK" type="Number" integer="true">
    /// </param>
    /// <param name="threadsCount" type="Number" integer="true">
    /// </param>
    /// <param name="hasGroupObjectFilter" type="Boolean">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.LatestChat.ServiceThreadStubArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['objectType'] = objectType;
    _parameters['objectK'] = objectK;
    _parameters['threadsCount'] = threadsCount;
    _parameters['hasGroupObjectFilter'] = hasGroupObjectFilter;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/LatestChat/Service.asmx', 'GetThreads', true, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.PlacesChooser');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.PlacesChooser.Service
Spotted.WebServices.Controls.PlacesChooser.Service = function Spotted_WebServices_Controls_PlacesChooser_Service() {
}
Spotted.WebServices.Controls.PlacesChooser.Service.getPlaces = function Spotted_WebServices_Controls_PlacesChooser_Service$getPlaces(north, south, east, west, maximumNumber, success, failure, userContext, timeout) {
    /// <param name="north" type="Number">
    /// </param>
    /// <param name="south" type="Number">
    /// </param>
    /// <param name="east" type="Number">
    /// </param>
    /// <param name="west" type="Number">
    /// </param>
    /// <param name="maximumNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PlacesChooser.ServicePlaceStubArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['north'] = north;
    _parameters['south'] = south;
    _parameters['east'] = east;
    _parameters['west'] = west;
    _parameters['maximumNumber'] = maximumNumber;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PlacesChooser/Service.asmx', 'GetPlaces', false, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.PlacesChooser.Service.getSurroundingPlaces = function Spotted_WebServices_Controls_PlacesChooser_Service$getSurroundingPlaces(centredOnPlaceK, numberOfPlacesToGet, success, failure, userContext, timeout) {
    /// <param name="centredOnPlaceK" type="Number" integer="true">
    /// </param>
    /// <param name="numberOfPlacesToGet" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.PlacesChooser.ServicePlaceStubArrayWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['centredOnPlaceK'] = centredOnPlaceK;
    _parameters['numberOfPlacesToGet'] = numberOfPlacesToGet;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/PlacesChooser/Service.asmx', 'GetSurroundingPlaces', false, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.EventCreator');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.EventCreator.Service
Spotted.WebServices.Controls.EventCreator.Service = function Spotted_WebServices_Controls_EventCreator_Service() {
}
Spotted.WebServices.Controls.EventCreator.Service.addEvent = function Spotted_WebServices_Controls_EventCreator_Service$addEvent(date, venueK, name, shortDetails, brands, success, failure, userContext, timeout) {
    /// <param name="date" type="Date">
    /// </param>
    /// <param name="venueK" type="Number" integer="true">
    /// </param>
    /// <param name="name" type="String">
    /// </param>
    /// <param name="shortDetails" type="String">
    /// </param>
    /// <param name="brands" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.EventCreator.ServiceEventInfoWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['date'] = date;
    _parameters['venueK'] = venueK;
    _parameters['name'] = name;
    _parameters['shortDetails'] = shortDetails;
    _parameters['brands'] = brands;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/EventCreator/Service.asmx', 'AddEvent', false, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.TaggingControl');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.TaggingControl.Service
Spotted.WebServices.Controls.TaggingControl.Service = function Spotted_WebServices_Controls_TaggingControl_Service() {
}
Spotted.WebServices.Controls.TaggingControl.Service.removeTagFromPhoto = function Spotted_WebServices_Controls_TaggingControl_Service$removeTagFromPhoto(tagK, photoK, success, failure, userContext, timeout) {
    /// <param name="tagK" type="Number" integer="true">
    /// </param>
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.TaggingControl.ServiceVoidWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['tagK'] = tagK;
    _parameters['photoK'] = photoK;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/TaggingControl/Service.asmx', 'RemoveTagFromPhoto', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.TaggingControl.Service.addTagToPhoto = function Spotted_WebServices_Controls_TaggingControl_Service$addTagToPhoto(tagText, photoK, success, failure, userContext, timeout) {
    /// <param name="tagText" type="String">
    /// </param>
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.TaggingControl.ServiceTagStubWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['tagText'] = tagText;
    _parameters['photoK'] = photoK;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/TaggingControl/Service.asmx', 'AddTagToPhoto', true, _parameters, success, failure, userContext, timeout);
}
Spotted.WebServices.Controls.TaggingControl.Service.getTagsForPhotoKs = function Spotted_WebServices_Controls_TaggingControl_Service$getTagsForPhotoKs(photoKs, success, failure, userContext, timeout) {
    /// <param name="photoKs" type="Array" elementType="Number" elementInteger="true">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.TaggingControl.ServiceDictionaryWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['photoKs'] = photoKs;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/TaggingControl/Service.asmx', 'GetTagsForPhotoKs', true, _parameters, success, failure, userContext, timeout);
}
Type.registerNamespace('Spotted.WebServices.Controls.VenueCreator');

////////////////////////////////////////////////////////////////////////////////
// Spotted.WebServices.Controls.VenueCreator.Service
Spotted.WebServices.Controls.VenueCreator.Service = function Spotted_WebServices_Controls_VenueCreator_Service() {
}
Spotted.WebServices.Controls.VenueCreator.Service.createVenue = function Spotted_WebServices_Controls_VenueCreator_Service$createVenue(name, placeK, postcode, success, failure, userContext, timeout) {
    /// <param name="name" type="String">
    /// </param>
    /// <param name="placeK" type="Number" integer="true">
    /// </param>
    /// <param name="postcode" type="String">
    /// </param>
    /// <param name="success" type="Spotted.WebServices.Controls.VenueCreator.ServiceVenueInfoWebServiceSuccessCallback">
    /// </param>
    /// <param name="failure" type="Sys.Net.WebServiceFailureCallback">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Sys.Net.WebRequest"></returns>
    var _parameters = {};
    _parameters['name'] = name;
    _parameters['placeK'] = placeK;
    _parameters['postcode'] = postcode;
    return Sys.Net.WebServiceProxy.invoke('/WebServices/Controls/VenueCreator/Service.asmx', 'CreateVenue', true, _parameters, success, failure, userContext, timeout);
}
SpottedScript.Pages.MapBrowser.MapItem.registerClass('SpottedScript.Pages.MapBrowser.MapItem');
SpottedScript.Behaviours.CreateUsersFromEmails.Controller.registerClass('SpottedScript.Behaviours.CreateUsersFromEmails.Controller');
SpottedScript.Behaviours.CreateUsersFromEmails._emailsSuggestionValue.registerClass('SpottedScript.Behaviours.CreateUsersFromEmails._emailsSuggestionValue');
SpottedScript.Behaviours.CreateUserFromEmail.Controller.registerClass('SpottedScript.Behaviours.CreateUserFromEmail.Controller');
SpottedScript.Behaviours.CreateUserFromEmail._emailSuggestionValue.registerClass('SpottedScript.Behaviours.CreateUserFromEmail._emailSuggestionValue');
Net.Comet.CometProxy.registerClass('Net.Comet.CometProxy');
Net.Comet.CometRequest.registerClass('Net.Comet.CometRequest');
ScriptSharpLibrary.HelloWorld.registerClass('ScriptSharpLibrary.HelloWorld');
ScriptSharpLibrary.HtmlAutoCompleteAttributes.registerClass('ScriptSharpLibrary.HtmlAutoCompleteAttributes');
ScriptSharpLibrary.HtmlAutoCompleteBehaviour.registerClass('ScriptSharpLibrary.HtmlAutoCompleteBehaviour');
ScriptSharpLibrary.KeyValuePair.registerClass('ScriptSharpLibrary.KeyValuePair');
ScriptSharpLibrary.KeyStringPair.registerClass('ScriptSharpLibrary.KeyStringPair');
ScriptSharpLibrary.MultiSelectorAttributes.registerClass('ScriptSharpLibrary.MultiSelectorAttributes');
ScriptSharpLibrary.MultiSelectorBehaviour.registerClass('ScriptSharpLibrary.MultiSelectorBehaviour');
ScriptSharpLibrary.PairListField.registerClass('ScriptSharpLibrary.PairListField');
ScriptSharpLibrary.PopupMenu.registerClass('ScriptSharpLibrary.PopupMenu');
ScriptSharpLibrary.Suggestion.registerClass('ScriptSharpLibrary.Suggestion');
ScriptSharpLibrary.SuggestionsCollection.registerClass('ScriptSharpLibrary.SuggestionsCollection');
ScriptSharpLibrary.SuggestionsGetter.registerClass('ScriptSharpLibrary.SuggestionsGetter');
ScriptSharpLibrary.WatermarkExtender.registerClass('ScriptSharpLibrary.WatermarkExtender');
ScriptSharpLibrary.HtmlAutoComplete.RemoteSuggestionsGetter.registerClass('ScriptSharpLibrary.HtmlAutoComplete.RemoteSuggestionsGetter');
ScriptSharpLibrary.HtmlAutoComplete._cometRemoteSuggestionsGetter.registerClass('ScriptSharpLibrary.HtmlAutoComplete._cometRemoteSuggestionsGetter', ScriptSharpLibrary.HtmlAutoComplete.RemoteSuggestionsGetter);
ScriptSharpLibrary.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter.registerClass('ScriptSharpLibrary.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter', ScriptSharpLibrary.HtmlAutoComplete.RemoteSuggestionsGetter);
SpottedScript.CustomControls.DsiCalendar.Controller.registerClass('SpottedScript.CustomControls.DsiCalendar.Controller');
JQ.TabsSelectEventArgs.registerClass('JQ.TabsSelectEventArgs');
Utils.Trace.registerClass('Utils.Trace');
Utils._traceObjects.registerClass('Utils._traceObjects');
ImportedUtilities.U.registerClass('ImportedUtilities.U');
SpottedScript.Utils._providerService.registerClass('SpottedScript.Utils._providerService', null, SpottedScript.Utils._iProviderService);
SpottedScript.Utils._pagedProviderService.registerClass('SpottedScript.Utils._pagedProviderService', SpottedScript.Utils._providerService, SpottedScript.Utils._iPagedProviderService);
SpottedScript.Utils._cachedPagedProvider.registerClass('SpottedScript.Utils._cachedPagedProvider');
SpottedScript.Utils._noMoreData.registerClass('SpottedScript.Utils._noMoreData');
Spotted.System.Text.StringBuilder.registerClass('Spotted.System.Text.StringBuilder');
SpottedScript.Misc.registerClass('SpottedScript.Misc');
SpottedScript.IntEventArgs.registerClass('SpottedScript.IntEventArgs', Sys.EventArgs);
Spotted.WebServices.Pages.MapBrowser.Tab.Service.registerClass('Spotted.WebServices.Pages.MapBrowser.Tab.Service');
Spotted.GenericPage.registerClass('Spotted.GenericPage');
Spotted.WebServices.ChatService.registerClass('Spotted.WebServices.ChatService');
Spotted.WebServices.AutoComplete.registerClass('Spotted.WebServices.AutoComplete');
Spotted.WebServices.Controls.MultiBuddyChooser.Service.registerClass('Spotted.WebServices.Controls.MultiBuddyChooser.Service');
Spotted.WebServices.Controls.CommentsDisplay.Service.registerClass('Spotted.WebServices.Controls.CommentsDisplay.Service');
Spotted.WebServices.Controls.PhotoControl.Service.registerClass('Spotted.WebServices.Controls.PhotoControl.Service');
Spotted.WebServices.Controls.EventBox.Service.registerClass('Spotted.WebServices.Controls.EventBox.Service');
Spotted.WebServices.Controls.Banners.Generator.Service.registerClass('Spotted.WebServices.Controls.Banners.Generator.Service');
Spotted.WebServices.Controls.LatestChat.Service.registerClass('Spotted.WebServices.Controls.LatestChat.Service');
Spotted.WebServices.Controls.PlacesChooser.Service.registerClass('Spotted.WebServices.Controls.PlacesChooser.Service');
Spotted.WebServices.Controls.EventCreator.Service.registerClass('Spotted.WebServices.Controls.EventCreator.Service');
Spotted.WebServices.Controls.TaggingControl.Service.registerClass('Spotted.WebServices.Controls.TaggingControl.Service');
Spotted.WebServices.Controls.VenueCreator.Service.registerClass('Spotted.WebServices.Controls.VenueCreator.Service');
SpottedScript.Behaviours.CreateUsersFromEmails.Controller._emailsRegex = '^( *([^\\@\\s]+\\@[A-Za-z0-9\\-]{1}[.A-Za-z0-9\\-]+\\.[.A-Za-z0-9\\-]*[A-Za-z0-9]) *){2,}$';
SpottedScript.Behaviours.CreateUserFromEmail.Controller._emailRegex = '^[^\\@\\s]+\\@[A-Za-z0-9\\-]{1}[.A-Za-z0-9\\-]+\\.[.A-Za-z0-9\\-]*[A-Za-z0-9]$';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.webServiceUrl = 'WebServiceUrl';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.webServiceMethod = 'WebServiceMethod';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.cometServiceUrl = 'CometServiceUrl';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.maxNumberOfItemsToGet = 'MaxNumberOfItemsToGet';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.minimumPopupWidth = 'MinimumPopupWidth';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.maximumPopupWidth = 'MaximumPopupWidth';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuClassName = 'PopupMenu';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupMenuHighlightedItemClassName = 'PopupMenuHighlightedItem';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.excludedItems = 'ExcludedItems';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.listBoxItemValue = 'ListBoxItemValue';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.onselectionmade = 'onselectionmade';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.onhighlight = 'onhighlight';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.onpopupcancel = 'onpopupcancel';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.watermark = 'Watermark';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupLeftOffset = 'PopupLeftOffset';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.popupTopOffset = 'PopupTopOffset';
ScriptSharpLibrary.HtmlAutoCompleteAttributes.rightAlign = 'RightAlign';
ScriptSharpLibrary.MultiSelectorAttributes.selections = 'Selections';
ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorDelButtonCss = 'MultiSelectorDelButton';
ScriptSharpLibrary.MultiSelectorAttributes.multiSelectorListBoxCss = 'MultiSelectorListBoxItem';
Utils._traceObjects._traceWindow = null;
SpottedScript.Utils._cachedPagedProvider._cachePageSizeMultiplier = 1;
// ---- Do not remove this footer ----
// This script was generated using Script# v0.5.5.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
