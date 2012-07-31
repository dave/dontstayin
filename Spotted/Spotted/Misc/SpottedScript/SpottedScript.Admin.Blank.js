Type.registerNamespace('SpottedScript.Admin.Blank');
SpottedScript.Admin.Blank.View=function(clientId){SpottedScript.Admin.Blank.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.Blank.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.Blank.View.registerClass('SpottedScript.Admin.Blank.View',SpottedScript.AdminUserControl.View);
