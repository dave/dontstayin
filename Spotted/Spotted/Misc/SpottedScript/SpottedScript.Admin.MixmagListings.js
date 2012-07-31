Type.registerNamespace('SpottedScript.Admin.MixmagListings');
SpottedScript.Admin.MixmagListings.View=function(clientId){SpottedScript.Admin.MixmagListings.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.MixmagListings.View.prototype={clientId:null,get_issueDrop:function(){return document.getElementById(this.clientId+'_IssueDrop');},get_zoneDrop:function(){return document.getElementById(this.clientId+'_ZoneDrop');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.MixmagListings.View.registerClass('SpottedScript.Admin.MixmagListings.View',SpottedScript.AdminUserControl.View);
