Type.registerNamespace('SpottedScript.Pages.Promoters.EventHighlight');
SpottedScript.Pages.Promoters.EventHighlight.View=function(clientId){SpottedScript.Pages.Promoters.EventHighlight.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.EventHighlight.View.prototype={clientId:null,get_promoterIntro:function(){return document.getElementById(this.clientId+'_PromoterIntro');},get_introBannerListLink:function(){return document.getElementById(this.clientId+'_IntroBannerListLink');},get_h13:function(){return document.getElementById(this.clientId+'_H13');},get_button1:function(){return document.getElementById(this.clientId+'_Button1');},get_h15:function(){return document.getElementById(this.clientId+'_H15');},get_button2:function(){return document.getElementById(this.clientId+'_Button2');},get_h16:function(){return document.getElementById(this.clientId+'_H16');},get_recommendedCell:function(){return document.getElementById(this.clientId+'_RecommendedCell');},get_recommendedCellPrice:function(){return document.getElementById(this.clientId+'_RecommendedCellPrice');},get_payment:function(){return document.getElementById(this.clientId+'_Payment');},get_choicePanel:function(){return document.getElementById(this.clientId+'_ChoicePanel');},get_payPanel:function(){return document.getElementById(this.clientId+'_PayPanel');},get_payDonePanel:function(){return document.getElementById(this.clientId+'_PayDonePanel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.EventHighlight.View.registerClass('SpottedScript.Pages.Promoters.EventHighlight.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
