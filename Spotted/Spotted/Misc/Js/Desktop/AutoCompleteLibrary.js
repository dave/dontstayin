// AutoCompleteLibrary.js
(function($){
Type.registerNamespace('Js.AutoCompleteLibrary');Js.AutoCompleteLibrary.EventInfo=function(){}
Js.AutoCompleteLibrary.EventInfo.eventFullName=function(ei){return ei.name+' on '+ei.date.format('ddd dd/MM/yyyy')+' @ '+Js.AutoCompleteLibrary.VenueInfo.nameWithPlace(ei.venueInfo);}
Js.AutoCompleteLibrary.EventInfo.prototype={name:null,k:0,date:null,venueInfo:null,url:null,picPath:null}
Js.AutoCompleteLibrary.PlaceInfo=function(){}
Js.AutoCompleteLibrary.PlaceInfo.nameWithCountry=function(pi){return pi.name+', '+pi.country.name;}
Js.AutoCompleteLibrary.PlaceInfo.prototype={name:null,k:0,country:null}
Js.AutoCompleteLibrary.Suggestion=function(){}
Js.AutoCompleteLibrary.Suggestion.getPicTitleDetailTemplateHtml=function(imageSrc,title,detail){return String.format("<table cellspacing='0' cellpadding='0'><tr><td width='40'><img src='{0}' border=0 width=40 hspace=0 height=40 style='border-right:1px solid #999999;display:block;'/></td><td style='padding:2px 3px 2px 3px;' valign='top'><b>{1}</b><br />{2}</td></tr></table>",imageSrc,title,detail);}
Js.AutoCompleteLibrary.Suggestion.addSuggestion=function(suggestions,suggestion,maxNumberOfItemsToGet){suggestion.priority=(suggestions.length>0)?suggestions[suggestions.length-1].priority:1;suggestions[(suggestions.length<maxNumberOfItemsToGet)?suggestions.length:suggestions.length-1]=suggestion;}
Js.AutoCompleteLibrary.Suggestion.prototype={html:null,text:null,value:null,priority:0}
Js.AutoCompleteLibrary.VenueInfo=function(){}
Js.AutoCompleteLibrary.VenueInfo.nameWithPlace=function(vi){return vi.name+', '+Js.AutoCompleteLibrary.PlaceInfo.nameWithCountry(vi.place);}
Js.AutoCompleteLibrary.VenueInfo.prototype={name:null,k:0,place:null,url:null,picPath:null}
Js.AutoCompleteLibrary.CountryInfo=function(){}
Js.AutoCompleteLibrary.CountryInfo.prototype={name:null,k:0}
Js.AutoCompleteLibrary.EventInfo.registerClass('Js.AutoCompleteLibrary.EventInfo');Js.AutoCompleteLibrary.PlaceInfo.registerClass('Js.AutoCompleteLibrary.PlaceInfo');Js.AutoCompleteLibrary.Suggestion.registerClass('Js.AutoCompleteLibrary.Suggestion');Js.AutoCompleteLibrary.VenueInfo.registerClass('Js.AutoCompleteLibrary.VenueInfo');Js.AutoCompleteLibrary.CountryInfo.registerClass('Js.AutoCompleteLibrary.CountryInfo');})(jQuery);// This script was generated using Script# v0.7.4.0
