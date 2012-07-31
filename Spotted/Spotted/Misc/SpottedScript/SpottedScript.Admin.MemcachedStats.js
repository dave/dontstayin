Type.registerNamespace('SpottedScript.Admin.MemcachedStats');
SpottedScript.Admin.MemcachedStats.View=function(clientId){SpottedScript.Admin.MemcachedStats.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.MemcachedStats.View.prototype={clientId:null,get_uiMemcachedStatsGridView:function(){return document.getElementById(this.clientId+'_uiMemcachedStatsGridView');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.MemcachedStats.View.registerClass('SpottedScript.Admin.MemcachedStats.View',SpottedScript.AdminUserControl.View);
