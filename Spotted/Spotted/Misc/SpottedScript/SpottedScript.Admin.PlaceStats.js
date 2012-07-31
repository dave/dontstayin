Type.registerNamespace('SpottedScript.Admin.PlaceStats');
SpottedScript.Admin.PlaceStats.View=function(clientId){SpottedScript.Admin.PlaceStats.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.PlaceStats.View.prototype={clientId:null,get_placeName:function(){return document.getElementById(this.clientId+'_PlaceName');},get_tab:function(){return document.getElementById(this.clientId+'_Tab');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.PlaceStats.View.registerClass('SpottedScript.Admin.PlaceStats.View',SpottedScript.AdminUserControl.View);
