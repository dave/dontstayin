Type.registerNamespace('SpottedScript.Admin.SalesCampaigns');
SpottedScript.Admin.SalesCampaigns.View=function(clientId){SpottedScript.Admin.SalesCampaigns.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.SalesCampaigns.View.prototype={clientId:null,get_campaignsDataGrid:function(){return document.getElementById(this.clientId+'_CampaignsDataGrid');},get_addName:function(){return document.getElementById(this.clientId+'_AddName');},get_addDescription:function(){return document.getElementById(this.clientId+'_AddDescription');},get_addStartDate:function(){return eval(this.clientId+'_AddStartDateController');},get_addEndDate:function(){return eval(this.clientId+'_AddEndDateController');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.SalesCampaigns.View.registerClass('SpottedScript.Admin.SalesCampaigns.View',SpottedScript.AdminUserControl.View);
