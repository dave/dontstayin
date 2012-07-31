//! WebServices.debug.js
//

(function() {

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
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['typeName'] = typeName;
    p['methodName'] = methodName;
    p['args'] = args;
    var o = Js.Library.WebServiceHelper.options('ClientRequest', '/GenericPage.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'ClientRequest');
    };
    $.ajax(o);
}


Type.registerNamespace('Spotted.WebServices');

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
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetTags', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetTags');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getTagSearchString = function Spotted_WebServices_AutoComplete$getTagSearchString(prefixText, count, success, failure, userContext, timeout) {
    /// <param name="prefixText" type="String">
    /// </param>
    /// <param name="count" type="Number" integer="true">
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
    p['prefixText'] = prefixText;
    p['count'] = count;
    var o = Js.Library.WebServiceHelper.options('GetTagSearchString', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetTagSearchString');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getGroupMembers = function Spotted_WebServices_AutoComplete$getGroupMembers(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['text'] = text;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetGroupMembers', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetGroupMembers');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getUsrsPublic = function Spotted_WebServices_AutoComplete$getUsrsPublic(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetUsrsPublic', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetUsrsPublic');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getBuddiesThenUsrs = function Spotted_WebServices_AutoComplete$getBuddiesThenUsrs(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetBuddiesThenUsrs', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetBuddiesThenUsrs');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getBuddies = function Spotted_WebServices_AutoComplete$getBuddies(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetBuddies', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetBuddies');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getBrands = function Spotted_WebServices_AutoComplete$getBrands(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetBrands', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetBrands');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getPromotersWithK = function Spotted_WebServices_AutoComplete$getPromotersWithK(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetPromotersWithK', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPromotersWithK');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getUsersWithK = function Spotted_WebServices_AutoComplete$getUsersWithK(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetUsersWithK', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetUsersWithK');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getPlacesEnabled = function Spotted_WebServices_AutoComplete$getPlacesEnabled(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['text'] = text;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetPlacesEnabled', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPlacesEnabled');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getVenuesFull = function Spotted_WebServices_AutoComplete$getVenuesFull(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['text'] = text;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetVenuesFull', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetVenuesFull');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getVenues = function Spotted_WebServices_AutoComplete$getVenues(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetVenues', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetVenues');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getEvents = function Spotted_WebServices_AutoComplete$getEvents(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['text'] = text;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetEvents', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetEvents');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getPlaces = function Spotted_WebServices_AutoComplete$getPlaces(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['text'] = text;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetPlaces', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPlaces');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getSiteSearchResults = function Spotted_WebServices_AutoComplete$getSiteSearchResults(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetSiteSearchResults', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetSiteSearchResults');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getGroups = function Spotted_WebServices_AutoComplete$getGroups(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetGroups', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetGroups');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getGroupsNoBrands = function Spotted_WebServices_AutoComplete$getGroupsNoBrands(maxNumberOfItemsToGet, text, parameters, success, failure, userContext, timeout) {
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['text'] = text;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetGroupsNoBrands', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetGroupsNoBrands');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getObjects = function Spotted_WebServices_AutoComplete$getObjects(text, maxNumberOfItemsToGet, parameters, success, failure, userContext, timeout) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['text'] = text;
    p['maxNumberOfItemsToGet'] = maxNumberOfItemsToGet;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetObjects', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetObjects');
    };
    $.ajax(o);
}
Spotted.WebServices.AutoComplete.getCountries = function Spotted_WebServices_AutoComplete$getCountries(get, text, parameters, success, failure, userContext, timeout) {
    /// <param name="get" type="Number" integer="true">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parameters" type="Object">
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
    p['get'] = get;
    p['text'] = text;
    p['parameters'] = parameters;
    var o = Js.Library.WebServiceHelper.options('GetCountries', '/WebServices/AutoComplete.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetCountries');
    };
    $.ajax(o);
}


Spotted.GenericPage.registerClass('Spotted.GenericPage');
Spotted.WebServices.AutoComplete.registerClass('Spotted.WebServices.AutoComplete');
})();

//! This script was generated using Script# v0.7.4.0
