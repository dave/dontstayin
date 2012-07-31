Type.registerNamespace('SpottedScript.Admin.WeightedRevenue');
SpottedScript.Admin.WeightedRevenue.View=function(clientId){SpottedScript.Admin.WeightedRevenue.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.WeightedRevenue.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.WeightedRevenue.View.registerClass('SpottedScript.Admin.WeightedRevenue.View',SpottedScript.AdminUserControl.View);
