Type.registerNamespace('SpottedScript.Admin.IncomeEarningDate');
SpottedScript.Admin.IncomeEarningDate.View=function(clientId){SpottedScript.Admin.IncomeEarningDate.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.IncomeEarningDate.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.IncomeEarningDate.View.registerClass('SpottedScript.Admin.IncomeEarningDate.View',SpottedScript.AdminUserControl.View);
