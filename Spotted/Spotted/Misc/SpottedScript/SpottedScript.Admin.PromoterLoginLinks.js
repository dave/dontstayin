Type.registerNamespace('SpottedScript.Admin.PromoterLoginLinks');
SpottedScript.Admin.PromoterLoginLinks.View=function(clientId){SpottedScript.Admin.PromoterLoginLinks.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.PromoterLoginLinks.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.PromoterLoginLinks.View.registerClass('SpottedScript.Admin.PromoterLoginLinks.View',SpottedScript.AdminUserControl.View);
