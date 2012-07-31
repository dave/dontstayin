Type.registerNamespace('SpottedScript.Admin.DisableContent');
SpottedScript.Admin.DisableContent.View=function(clientId){SpottedScript.Admin.DisableContent.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.DisableContent.View.prototype={clientId:null,get_photoK:function(){return document.getElementById(this.clientId+'_PhotoK');},get_outLabel:function(){return document.getElementById(this.clientId+'_OutLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.DisableContent.View.registerClass('SpottedScript.Admin.DisableContent.View',SpottedScript.AdminUserControl.View);
