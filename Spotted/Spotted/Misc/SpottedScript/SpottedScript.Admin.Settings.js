Type.registerNamespace('SpottedScript.Admin.Settings');
SpottedScript.Admin.Settings.View=function(clientId){SpottedScript.Admin.Settings.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.Settings.View.prototype={clientId:null,get_btnSave:function(){return document.getElementById(this.clientId+'_btnSave');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.Settings.View.registerClass('SpottedScript.Admin.Settings.View',SpottedScript.AdminUserControl.View);
