Type.registerNamespace('SpottedScript.Admin.DeletePic');
SpottedScript.Admin.DeletePic.View=function(clientId){SpottedScript.Admin.DeletePic.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.DeletePic.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.DeletePic.View.registerClass('SpottedScript.Admin.DeletePic.View',SpottedScript.AdminUserControl.View);
