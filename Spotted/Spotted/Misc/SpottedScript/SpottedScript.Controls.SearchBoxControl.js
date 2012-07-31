Type.registerNamespace('SpottedScript.Controls.SearchBoxControl');
SpottedScript.Controls.SearchBoxControl.View=function(clientId){this.clientId=clientId;}
SpottedScript.Controls.SearchBoxControl.View.prototype={clientId:null,get_uiSearchQuery:function(){return document.getElementById(this.clientId+'_uiSearchQuery');},get_uiTagAutoComplete:function(){return document.getElementById(this.clientId+'_uiTagAutoComplete');},get_uiSubmitSearchButton:function(){return document.getElementById(this.clientId+'_uiSubmitSearchButton');}}
SpottedScript.Controls.SearchBoxControl.View.registerClass('SpottedScript.Controls.SearchBoxControl.View');
