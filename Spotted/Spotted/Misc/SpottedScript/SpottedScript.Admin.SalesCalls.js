Type.registerNamespace('SpottedScript.Admin.SalesCalls');
SpottedScript.Admin.SalesCalls.View=function(clientId){SpottedScript.Admin.SalesCalls.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.SalesCalls.View.prototype={clientId:null,get_cal:function(){return document.getElementById(this.clientId+'_Cal');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.SalesCalls.View.registerClass('SpottedScript.Admin.SalesCalls.View',SpottedScript.AdminUserControl.View);
