Type.registerNamespace('SpottedScript.Pages.Promoters.Unconfirmed');
SpottedScript.Pages.Promoters.Unconfirmed.View=function(clientId){SpottedScript.Pages.Promoters.Unconfirmed.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.Unconfirmed.View.prototype={clientId:null,get_panelUnconfirmed:function(){return document.getElementById(this.clientId+'_PanelUnconfirmed');},get_promoterIntro:function(){return document.getElementById(this.clientId+'_PromoterIntro');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.Unconfirmed.View.registerClass('SpottedScript.Pages.Promoters.Unconfirmed.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
