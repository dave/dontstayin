Type.registerNamespace('SpottedScript.Pages.Promoters.Plus');
SpottedScript.Pages.Promoters.Plus.View=function(clientId){SpottedScript.Pages.Promoters.Plus.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.Plus.View.prototype={clientId:null,get_promoterIntro1:function(){return document.getElementById(this.clientId+'_PromoterIntro1');},get_h1fd4:function(){return document.getElementById(this.clientId+'_H1fd4');},get_h14:function(){return document.getElementById(this.clientId+'_H14');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.Plus.View.registerClass('SpottedScript.Pages.Promoters.Plus.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
