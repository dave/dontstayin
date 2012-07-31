Type.registerNamespace('SpottedScript.Admin.SalesIdle');
SpottedScript.Admin.SalesIdle.View=function(clientId){SpottedScript.Admin.SalesIdle.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.SalesIdle.View.prototype={clientId:null,get_pageNumberP:function(){return document.getElementById(this.clientId+'_PageNumberP');},get_promoterDataGrid:function(){return document.getElementById(this.clientId+'_PromoterDataGrid');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.SalesIdle.View.registerClass('SpottedScript.Admin.SalesIdle.View',SpottedScript.AdminUserControl.View);
