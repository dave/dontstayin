Type.registerNamespace('SpottedScript.Admin.EventsWithNoSpendPromoters');
SpottedScript.Admin.EventsWithNoSpendPromoters.View=function(clientId){SpottedScript.Admin.EventsWithNoSpendPromoters.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.EventsWithNoSpendPromoters.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.EventsWithNoSpendPromoters.View.registerClass('SpottedScript.Admin.EventsWithNoSpendPromoters.View',SpottedScript.AdminUserControl.View);
