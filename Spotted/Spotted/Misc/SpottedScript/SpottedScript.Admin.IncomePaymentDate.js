Type.registerNamespace('SpottedScript.Admin.IncomePaymentDate');
SpottedScript.Admin.IncomePaymentDate.View=function(clientId){SpottedScript.Admin.IncomePaymentDate.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.IncomePaymentDate.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.IncomePaymentDate.View.registerClass('SpottedScript.Admin.IncomePaymentDate.View',SpottedScript.AdminUserControl.View);
