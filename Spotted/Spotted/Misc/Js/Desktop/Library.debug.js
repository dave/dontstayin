//! Library.debug.js
//

(function() {

Type.registerNamespace('Js.Library');

////////////////////////////////////////////////////////////////////////////////
// Js.Library.ObjectType

Js.Library.ObjectType = function() { 
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
Js.Library.ObjectType.prototype = {
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
Js.Library.ObjectType.registerEnum('Js.Library.ObjectType', false);


////////////////////////////////////////////////////////////////////////////////
// Js.Library.U

Js.Library.U = function Js_Library_U() {
}
Js.Library.U.toString = function Js_Library_U$toString(o) {
    /// <param name="o" type="Object">
    /// </param>
    /// <returns type="String"></returns>
    return Js.Library.U._toStringWithOffset(o, '');
}
Js.Library.U._toStringWithOffset = function Js_Library_U$_toStringWithOffset(o, offset) {
    /// <param name="o" type="Object">
    /// </param>
    /// <param name="offset" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var tab = '    ';
    var s = '';
    if (Type.canCast(o, Date)) {
        s += (o).toDateString() + ' ' + (o).toTimeString();
    }
    else if (Type.canCast(o, Array)) {
        var a = o;
        s += '\n' + offset + '{\n';
        for (var i = 0; i < a.length; i++) {
            s += offset + tab + '[' + i.toString() + '] : ' + Js.Library.U._toStringWithOffset(a[i], offset + tab) + '\n';
        }
        s += offset + '}';
    }
    else if (Type.canCast(o, Boolean)) {
        s += o.toString();
    }
    else if (Type.canCast(o, Number)) {
        s += o.toString();
    }
    else if (Type.canCast(o, String)) {
        var s1 = o.toString().replaceAll('\n', '');
        s += (s1.length > 256) ? (s1.substr(0, 256) + '(...)') : s1;
    }
    else if (Type.canCast(o, Object)) {
        var d = o;
        s += '\n' + offset + '{\n';
        var $enum1 = ss.IEnumerator.getEnumerator(Object.keys(d));
        while ($enum1.moveNext()) {
            var key = $enum1.current;
            s += offset + tab + key + ' : ' + Js.Library.U._toStringWithOffset(d[key], offset + tab) + '\n';
        }
        s += offset + '}';
    }
    return s;
}
Js.Library.U.get = function Js_Library_U$get(d, query) {
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
                return Js.Library.U.getFromDictionaryByQuery(d, queryArr[i]);
            }
            else {
                d = Js.Library.U.getFromDictionaryByQuery(d, queryArr[i]);
            }
        }
        return null;
    }
    catch ($e1) {
        return null;
    }
}
Js.Library.U.exists = function Js_Library_U$exists(d, query) {
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
                if (Js.Library.U.getFromDictionaryByQuery(d, queryArr[i]) == null) {
                    return false;
                }
            }
            else {
                d = Js.Library.U.getFromDictionaryByQuery(d, queryArr[i]);
            }
        }
        return true;
    }
    catch ($e1) {
        return false;
    }
}
Js.Library.U.getFromDictionaryByQuery = function Js_Library_U$getFromDictionaryByQuery(d, query) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="query" type="String">
    /// </param>
    /// <returns type="Object"></returns>
    if (!query || query === '*') {
        return Js.Library.U.getFromDictionaryByIndex(d, 0);
    }
    else {
        return d[query];
    }
}
Js.Library.U.getFromDictionaryByIndex = function Js_Library_U$getFromDictionaryByIndex(d, index) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="index" type="Number" integer="true">
    /// </param>
    /// <returns type="Object"></returns>
    try {
        return d[Object.keys(d)[index]];
    }
    catch ($e1) {
        return null;
    }
}
Js.Library.U.isTrue = function Js_Library_U$isTrue(d, query) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="query" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    try {
        if (Js.Library.U.exists(d, query)) {
            var o = Js.Library.U.get(d, query);
            if (Type.canCast(o, Boolean)) {
                return o;
            }
        }
        return false;
    }
    catch ($e1) {
        return false;
    }
}
Js.Library.U.hasValue = function Js_Library_U$hasValue(d, query) {
    /// <param name="d" type="Object">
    /// </param>
    /// <param name="query" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    try {
        if (Js.Library.U.exists(d, query)) {
            return Js.Library.U.get(d, query) != null;
        }
        return false;
    }
    catch ($e1) {
        return false;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Library.Misc

Js.Library.Misc = function Js_Library_Misc() {
}
Js.Library.Misc.objectToString = function Js_Library_Misc$objectToString(o) {
    /// <param name="o" type="Object">
    /// </param>
    /// <returns type="String"></returns>
    return Js.Library.Misc._objectToString(o, 0);
}
Js.Library.Misc._objectToString = function Js_Library_Misc$_objectToString(o, indent) {
    /// <param name="o" type="Object">
    /// </param>
    /// <param name="indent" type="Number" integer="true">
    /// </param>
    /// <returns type="String"></returns>
    if (Type.canCast(o, String)) {
        return o;
    }
    else if (Type.canCast(o, Object)) {
        var s = '\n[';
        var d = o;
        var $enum1 = ss.IEnumerator.getEnumerator(Object.keys(d));
        while ($enum1.moveNext()) {
            var key = $enum1.current;
            for (var i = 0; i < indent; i++) {
                s += ' ';
            }
            s += key + ': ' + Js.Library.Misc._objectToString(d[key], indent + 2) + '\n';
        }
        s += ']';
        return s;
    }
    else {
        return '';
    }
}
Js.Library.Misc.getPicUrlFromGuid = function Js_Library_Misc$getPicUrlFromGuid(guid) {
    /// <param name="guid" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var s = eval("StoragePath('" + guid + "');");
    return s;
}
Js.Library.Misc.redirect = function Js_Library_Misc$redirect(url) {
    /// <param name="url" type="String">
    /// </param>
    eval("window.location = '" + url + "'");
}
Js.Library.Misc.addHoverText = function Js_Library_Misc$addHoverText(el, hoverText) {
    /// <param name="el" type="Object" domElement="true">
    /// </param>
    /// <param name="hoverText" type="String">
    /// </param>
    el.addEventListener('mouseover', function() {
        Js.Library.Misc._stt(hoverText);
    }, false);
    el.addEventListener('mouseout', function() {
        Js.Library.Misc._htm();
    }, false);
}
Js.Library.Misc.redirectToAnchor = function Js_Library_Misc$redirectToAnchor(anchorName) {
    /// <param name="anchorName" type="String">
    /// </param>
    window.location.hash = anchorName;
}
Js.Library.Misc.showWaitingCursor = function Js_Library_Misc$showWaitingCursor() {
    eval('ShowWaitingCursor();');
}
Js.Library.Misc.hideWaitingCursor = function Js_Library_Misc$hideWaitingCursor() {
    eval('HideWaitingCursor();');
}
Js.Library.Misc._stt = function Js_Library_Misc$_stt(p) {
    /// <param name="p" type="String">
    /// </param>
    eval("stt('" + p + "');");
}
Js.Library.Misc._htm = function Js_Library_Misc$_htm() {
    eval('htm();');
}
Js.Library.Misc.get_browserIsFirefox = function Js_Library_Misc$get_browserIsFirefox() {
    /// <value type="Boolean"></value>
    return $.browser.mozilla;
}
Js.Library.Misc.get_browserIsIE = function Js_Library_Misc$get_browserIsIE() {
    /// <value type="Boolean"></value>
    return $.browser.msie;
}
Js.Library.Misc.get_browserVersion = function Js_Library_Misc$get_browserVersion() {
    /// <value type="Number"></value>
    var version = 1;
    try {
        version = parseFloat($.browser.version);
    }
    catch ($e1) {
    }
    return version;
}
Js.Library.Misc.combineAction = function Js_Library_Misc$combineAction(runFirst, runSecond) {
    /// <param name="runFirst" type="Function">
    /// </param>
    /// <param name="runSecond" type="Function">
    /// </param>
    /// <returns type="Function"></returns>
    var composite = function() {
        if (runFirst != null) {
            runFirst();
        }
        if (runSecond != null) {
            runSecond();
        }
    };
    return composite;
}
Js.Library.Misc.combineEventHandler = function Js_Library_Misc$combineEventHandler(runFirst, runSecond) {
    /// <param name="runFirst" type="Function">
    /// </param>
    /// <param name="runSecond" type="Function">
    /// </param>
    /// <returns type="Function"></returns>
    var composite = function(sender, e) {
        if (runFirst != null) {
            runFirst(sender, e);
        }
        if (runSecond != null) {
            runSecond(sender, e);
        }
    };
    return composite;
}
Js.Library.Misc._debug = function Js_Library_Misc$_debug() {
    debugger;;
}


////////////////////////////////////////////////////////////////////////////////
// Js.Library.IntEventArgs

Js.Library.IntEventArgs = function Js_Library_IntEventArgs(value) {
    /// <param name="value" type="Number" integer="true">
    /// </param>
    /// <field name="value" type="Number" integer="true">
    /// </field>
    Js.Library.IntEventArgs.initializeBase(this);
    this.value = value;
}
Js.Library.IntEventArgs.prototype = {
    value: 0
}


////////////////////////////////////////////////////////////////////////////////
// Js.Library.WebServiceError

Js.Library.WebServiceError = function Js_Library_WebServiceError(exceptionType, message, stackTrace, statusCode, timedOut) {
    /// <param name="exceptionType" type="String">
    /// </param>
    /// <param name="message" type="String">
    /// </param>
    /// <param name="stackTrace" type="String">
    /// </param>
    /// <param name="statusCode" type="Number" integer="true">
    /// </param>
    /// <param name="timedOut" type="Boolean">
    /// </param>
    /// <field name="exceptionType" type="String">
    /// </field>
    /// <field name="message" type="String">
    /// </field>
    /// <field name="stackTrace" type="String">
    /// </field>
    /// <field name="statusCode" type="Number" integer="true">
    /// </field>
    /// <field name="timedOut" type="Boolean">
    /// </field>
    this.exceptionType = exceptionType;
    this.message = message;
    this.stackTrace = stackTrace;
    this.statusCode = statusCode;
    this.timedOut = timedOut;
}
Js.Library.WebServiceError.prototype = {
    exceptionType: null,
    message: null,
    stackTrace: null,
    statusCode: 0,
    timedOut: false
}


////////////////////////////////////////////////////////////////////////////////
// Js.Library.WebServiceHelper

Js.Library.WebServiceHelper = function Js_Library_WebServiceHelper() {
}
Js.Library.WebServiceHelper.options = function Js_Library_WebServiceHelper$options(methodName, url, parameters, failure, userContext, timeout) {
    /// <param name="methodName" type="String">
    /// </param>
    /// <param name="url" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
    /// </param>
    /// <param name="failure" type="Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    /// <returns type="Object"></returns>
    var o = {};
    o.url = url + '/' + methodName;
    o.timeout = timeout;
    o.type = 'POST';
    o.async = true;
    o.cache = false;
    o.contentType = 'application/json; charset=utf-8';
    o.data = JSON.stringify(parameters);
    o.dataType = 'json';
    o.error = function(request, error, exception) {
        failure(new Js.Library.WebServiceError(Type.getInstanceType(exception).toString(), error, exception.toString(), request.status, request.status === 408), userContext, methodName);
    };
    return o;
}


////////////////////////////////////////////////////////////////////////////////
// Js.Library.Trace

Js.Library.Trace = function Js_Library_Trace() {
}
Js.Library.Trace.write = function Js_Library_Trace$write(message) {
    /// <param name="message" type="String">
    /// </param>
}
Js.Library.Trace.webServiceFailure = function Js_Library_Trace$webServiceFailure(error, userContext, methodName) {
    /// <param name="error" type="Js.Library.WebServiceError">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="methodName" type="String">
    /// </param>
    Js.Library.Trace.write('Message: ' + error.message + '<br>Type: ' + error.exceptionType + '<br>Stack trace: ' + error.stackTrace + '<br>Status code: ' + error.statusCode + '<br>Timed out: ' + error.timedOut);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Library._traceObjects

Js.Library._traceObjects = function Js_Library__traceObjects() {
    /// <field name="_traceWindow" type="Object" domElement="true" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Js.Library.StringBuilderJs

Js.Library.StringBuilderJs = function Js_Library_StringBuilderJs() {
    /// <field name="_stringArray" type="Array">
    /// </field>
    this._stringArray = [];
}
Js.Library.StringBuilderJs.prototype = {
    _stringArray: null,
    
    append: function Js_Library_StringBuilderJs$append(s) {
        /// <param name="s" type="String">
        /// </param>
        this._stringArray[this._stringArray.length] = s;
    },
    
    toString: function Js_Library_StringBuilderJs$toString() {
        /// <returns type="String"></returns>
        return this._stringArray.join('');
    },
    
    appendAttribute: function Js_Library_StringBuilderJs$appendAttribute(name, value) {
        /// <param name="name" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        this._stringArray[this._stringArray.length] = ' ';
        this._stringArray[this._stringArray.length] = name;
        this._stringArray[this._stringArray.length] = '="';
        this._stringArray[this._stringArray.length] = value.replaceAll('"', '&#34;');
        this._stringArray[this._stringArray.length] = '"';
    }
}


Type.registerNamespace('Js.DsiUserControl');

////////////////////////////////////////////////////////////////////////////////
// Js.DsiUserControl.View

Js.DsiUserControl.View = function Js_DsiUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    Js.DsiUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.DsiUserControl.View.prototype = {
    clientId: null,
    
    get_genericContainerPage: function Js_DsiUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}


Type.registerNamespace('Js.GenericUserControl');

////////////////////////////////////////////////////////////////////////////////
// Js.GenericUserControl.View

Js.GenericUserControl.View = function Js_GenericUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
Js.GenericUserControl.View.prototype = {
    clientId: null,
    
    get_genericContainerPage: function Js_GenericUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}


Js.Library.U.registerClass('Js.Library.U');
Js.Library.Misc.registerClass('Js.Library.Misc');
Js.Library.IntEventArgs.registerClass('Js.Library.IntEventArgs', ss.EventArgs);
Js.Library.WebServiceError.registerClass('Js.Library.WebServiceError');
Js.Library.WebServiceHelper.registerClass('Js.Library.WebServiceHelper');
Js.Library.Trace.registerClass('Js.Library.Trace');
Js.Library._traceObjects.registerClass('Js.Library._traceObjects');
Js.Library.StringBuilderJs.registerClass('Js.Library.StringBuilderJs');
Js.GenericUserControl.View.registerClass('Js.GenericUserControl.View');
Js.DsiUserControl.View.registerClass('Js.DsiUserControl.View', Js.GenericUserControl.View);
Js.Library._traceObjects._traceWindow = null;
})();

//! This script was generated using Script# v0.7.4.0
