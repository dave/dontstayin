Type.registerNamespace('SpottedScript.Admin.FlyerStats');
SpottedScript.Admin.FlyerStats.View=function(clientId){SpottedScript.Admin.FlyerStats.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.FlyerStats.View.prototype={clientId:null,get_uiGridView:function(){return document.getElementById(this.clientId+'_uiGridView');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.FlyerStats.View.registerClass('SpottedScript.Admin.FlyerStats.View',SpottedScript.AdminUserControl.View);
