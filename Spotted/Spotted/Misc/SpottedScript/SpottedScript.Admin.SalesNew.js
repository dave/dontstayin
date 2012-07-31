Type.registerNamespace('SpottedScript.Admin.SalesNew');
SpottedScript.Admin.SalesNew.View=function(clientId){SpottedScript.Admin.SalesNew.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.SalesNew.View.prototype={clientId:null,get_newPromoterDataGrid:function(){return document.getElementById(this.clientId+'_NewPromoterDataGrid');},get_callBacksDataGrid:function(){return document.getElementById(this.clientId+'_CallBacksDataGrid');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.SalesNew.View.registerClass('SpottedScript.Admin.SalesNew.View',SpottedScript.AdminUserControl.View);
