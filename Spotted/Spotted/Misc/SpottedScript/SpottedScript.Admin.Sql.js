Type.registerNamespace('SpottedScript.Admin.Sql');
SpottedScript.Admin.Sql.View=function(clientId){SpottedScript.Admin.Sql.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.Sql.View.prototype={clientId:null,get_password:function(){return document.getElementById(this.clientId+'_Password');},get_query:function(){return document.getElementById(this.clientId+'_Query');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.Sql.View.registerClass('SpottedScript.Admin.Sql.View',SpottedScript.AdminUserControl.View);
