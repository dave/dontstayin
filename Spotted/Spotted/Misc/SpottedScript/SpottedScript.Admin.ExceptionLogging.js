Type.registerNamespace('SpottedScript.Admin.ExceptionLogging');
SpottedScript.Admin.ExceptionLogging.View=function(clientId){SpottedScript.Admin.ExceptionLogging.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.ExceptionLogging.View.prototype={clientId:null,get_gridView:function(){return document.getElementById(this.clientId+'_GridView');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.ExceptionLogging.View.registerClass('SpottedScript.Admin.ExceptionLogging.View',SpottedScript.AdminUserControl.View);
