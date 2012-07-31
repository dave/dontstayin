//! AutoCompleteLibrary.debug.js
//

(function($) {

Type.registerNamespace('Js.AutoCompleteLibrary');

////////////////////////////////////////////////////////////////////////////////
// Js.AutoCompleteLibrary.EventInfo

Js.AutoCompleteLibrary.EventInfo = function Js_AutoCompleteLibrary_EventInfo() {
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="date" type="Date">
    /// </field>
    /// <field name="venueInfo" type="Js.AutoCompleteLibrary.VenueInfo">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="picPath" type="String">
    /// </field>
}
Js.AutoCompleteLibrary.EventInfo.eventFullName = function Js_AutoCompleteLibrary_EventInfo$eventFullName(ei) {
    /// <param name="ei" type="Js.AutoCompleteLibrary.EventInfo">
    /// </param>
    /// <returns type="String"></returns>
    return ei.name + ' on ' + ei.date.format('ddd dd/MM/yyyy') + ' @ ' + Js.AutoCompleteLibrary.VenueInfo.nameWithPlace(ei.venueInfo);
}
Js.AutoCompleteLibrary.EventInfo.prototype = {
    name: null,
    k: 0,
    date: null,
    venueInfo: null,
    url: null,
    picPath: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.AutoCompleteLibrary.PlaceInfo

Js.AutoCompleteLibrary.PlaceInfo = function Js_AutoCompleteLibrary_PlaceInfo() {
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="country" type="Js.AutoCompleteLibrary.CountryInfo">
    /// </field>
}
Js.AutoCompleteLibrary.PlaceInfo.nameWithCountry = function Js_AutoCompleteLibrary_PlaceInfo$nameWithCountry(pi) {
    /// <param name="pi" type="Js.AutoCompleteLibrary.PlaceInfo">
    /// </param>
    /// <returns type="String"></returns>
    return pi.name + ', ' + pi.country.name;
}
Js.AutoCompleteLibrary.PlaceInfo.prototype = {
    name: null,
    k: 0,
    country: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.AutoCompleteLibrary.Suggestion

Js.AutoCompleteLibrary.Suggestion = function Js_AutoCompleteLibrary_Suggestion() {
    /// <field name="html" type="String">
    /// </field>
    /// <field name="text" type="String">
    /// </field>
    /// <field name="value" type="String">
    /// </field>
    /// <field name="priority" type="Number" integer="true">
    /// </field>
}
Js.AutoCompleteLibrary.Suggestion.getPicTitleDetailTemplateHtml = function Js_AutoCompleteLibrary_Suggestion$getPicTitleDetailTemplateHtml(imageSrc, title, detail) {
    /// <param name="imageSrc" type="String">
    /// </param>
    /// <param name="title" type="String">
    /// </param>
    /// <param name="detail" type="String">
    /// </param>
    /// <returns type="String"></returns>
    return String.format("<table cellspacing='0' cellpadding='0'><tr><td width='40'><img src='{0}' border=0 width=40 hspace=0 height=40 style='border-right:1px solid #999999;display:block;'/></td><td style='padding:2px 3px 2px 3px;' valign='top'><b>{1}</b><br />{2}</td></tr></table>", imageSrc, title, detail);
}
Js.AutoCompleteLibrary.Suggestion.addSuggestion = function Js_AutoCompleteLibrary_Suggestion$addSuggestion(suggestions, suggestion, maxNumberOfItemsToGet) {
    /// <param name="suggestions" type="Array" elementType="Suggestion">
    /// </param>
    /// <param name="suggestion" type="Js.AutoCompleteLibrary.Suggestion">
    /// </param>
    /// <param name="maxNumberOfItemsToGet" type="Number" integer="true">
    /// </param>
    suggestion.priority = (suggestions.length > 0) ? suggestions[suggestions.length - 1].priority : 1;
    suggestions[(suggestions.length < maxNumberOfItemsToGet) ? suggestions.length : suggestions.length - 1] = suggestion;
}
Js.AutoCompleteLibrary.Suggestion.prototype = {
    html: null,
    text: null,
    value: null,
    priority: 0
}


////////////////////////////////////////////////////////////////////////////////
// Js.AutoCompleteLibrary.VenueInfo

Js.AutoCompleteLibrary.VenueInfo = function Js_AutoCompleteLibrary_VenueInfo() {
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="place" type="Js.AutoCompleteLibrary.PlaceInfo">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="picPath" type="String">
    /// </field>
}
Js.AutoCompleteLibrary.VenueInfo.nameWithPlace = function Js_AutoCompleteLibrary_VenueInfo$nameWithPlace(vi) {
    /// <param name="vi" type="Js.AutoCompleteLibrary.VenueInfo">
    /// </param>
    /// <returns type="String"></returns>
    return vi.name + ', ' + Js.AutoCompleteLibrary.PlaceInfo.nameWithCountry(vi.place);
}
Js.AutoCompleteLibrary.VenueInfo.prototype = {
    name: null,
    k: 0,
    place: null,
    url: null,
    picPath: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.AutoCompleteLibrary.CountryInfo

Js.AutoCompleteLibrary.CountryInfo = function Js_AutoCompleteLibrary_CountryInfo() {
    /// <field name="name" type="String">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
}
Js.AutoCompleteLibrary.CountryInfo.prototype = {
    name: null,
    k: 0
}


Js.AutoCompleteLibrary.EventInfo.registerClass('Js.AutoCompleteLibrary.EventInfo');
Js.AutoCompleteLibrary.PlaceInfo.registerClass('Js.AutoCompleteLibrary.PlaceInfo');
Js.AutoCompleteLibrary.Suggestion.registerClass('Js.AutoCompleteLibrary.Suggestion');
Js.AutoCompleteLibrary.VenueInfo.registerClass('Js.AutoCompleteLibrary.VenueInfo');
Js.AutoCompleteLibrary.CountryInfo.registerClass('Js.AutoCompleteLibrary.CountryInfo');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
