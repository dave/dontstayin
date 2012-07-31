Type.registerNamespace('SpottedScript.Admin.StripUsr');
SpottedScript.Admin.StripUsr.View=function(clientId){SpottedScript.Admin.StripUsr.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.StripUsr.View.prototype={clientId:null,get_objectKTextBox:function(){return document.getElementById(this.clientId+'_ObjectKTextBox');},get_deleteButton:function(){return document.getElementById(this.clientId+'_DeleteButton');},get_doneLabel:function(){return document.getElementById(this.clientId+'_DoneLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.StripUsr.View.registerClass('SpottedScript.Admin.StripUsr.View',SpottedScript.AdminUserControl.View);
