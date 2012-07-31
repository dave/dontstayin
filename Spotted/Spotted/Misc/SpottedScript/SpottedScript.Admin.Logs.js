Type.registerNamespace('SpottedScript.Admin.Logs');
SpottedScript.Admin.Logs.View=function(clientId){SpottedScript.Admin.Logs.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.Logs.View.prototype={clientId:null,get_times:function(){return document.getElementById(this.clientId+'_Times');},get_cal:function(){return document.getElementById(this.clientId+'_Cal');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.Logs.View.registerClass('SpottedScript.Admin.Logs.View',SpottedScript.AdminUserControl.View);
