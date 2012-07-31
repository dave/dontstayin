Type.registerNamespace('SpottedScript.AdminUserControl');
SpottedScript.AdminUserControl.View=function(clientId){SpottedScript.AdminUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.AdminUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.AdminUserControl.View.registerClass('SpottedScript.AdminUserControl.View',SpottedScript.GenericUserControl.View);
