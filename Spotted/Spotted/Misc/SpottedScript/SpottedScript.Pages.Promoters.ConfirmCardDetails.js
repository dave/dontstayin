Type.registerNamespace('SpottedScript.Pages.Promoters.ConfirmCardDetails');
SpottedScript.Pages.Promoters.ConfirmCardDetails.View=function(clientId){SpottedScript.Pages.Promoters.ConfirmCardDetails.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.ConfirmCardDetails.View.prototype={clientId:null,get_promoterIntro:function(){return document.getElementById(this.clientId+'_PromoterIntro');},get_uiSelect:function(){return document.getElementById(this.clientId+'_uiSelect');},get_uiEvents:function(){return document.getElementById(this.clientId+'_uiEvents');},get_uiNoEvents:function(){return document.getElementById(this.clientId+'_uiNoEvents');},get_uiDoorlistPanel:function(){return document.getElementById(this.clientId+'_uiDoorlistPanel');},get_uiDoorlist:function(){return document.getElementById(this.clientId+'_uiDoorlist');},get_uiSave:function(){return document.getElementById(this.clientId+'_uiSave');},get_uiSomeWrongLabel:function(){return document.getElementById(this.clientId+'_uiSomeWrongLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.ConfirmCardDetails.View.registerClass('SpottedScript.Pages.Promoters.ConfirmCardDetails.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
