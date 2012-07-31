Type.registerNamespace('SpottedScript.Admin.SalesLetterFollowUp');
SpottedScript.Admin.SalesLetterFollowUp.View=function(clientId){SpottedScript.Admin.SalesLetterFollowUp.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.SalesLetterFollowUp.View.prototype={clientId:null,get_promoterDataGrid:function(){return document.getElementById(this.clientId+'_PromoterDataGrid');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.SalesLetterFollowUp.View.registerClass('SpottedScript.Admin.SalesLetterFollowUp.View',SpottedScript.AdminUserControl.View);
