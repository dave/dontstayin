Type.registerNamespace('SpottedScript.Pages.Promoters.BannersPending');
SpottedScript.Pages.Promoters.BannersPending.View=function(clientId){SpottedScript.Pages.Promoters.BannersPending.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.BannersPending.View.prototype={clientId:null,get_promoterIntro:function(){return document.getElementById(this.clientId+'_PromoterIntro');},get_h1Title:function(){return document.getElementById(this.clientId+'_H1Title');},get_noPendingBannersLabel:function(){return document.getElementById(this.clientId+'_NoPendingBannersLabel');},get_bookBannersPanel:function(){return document.getElementById(this.clientId+'_BookBannersPanel');},get_bannerGrid:function(){return document.getElementById(this.clientId+'_BannerGrid');},get_bookBannersButton:function(){return document.getElementById(this.clientId+'_BookBannersButton');},get_ensureBannersSelectedValidator:function(){return document.getElementById(this.clientId+'_EnsureBannersSelectedValidator');},get_paymentPanel:function(){return document.getElementById(this.clientId+'_PaymentPanel');},get_payment:function(){return document.getElementById(this.clientId+'_Payment');},get_cancelButton:function(){return document.getElementById(this.clientId+'_CancelButton');},get_confirmedPanel:function(){return document.getElementById(this.clientId+'_ConfirmedPanel');},get_bookedBannersGridView:function(){return document.getElementById(this.clientId+'_BookedBannersGridView');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.BannersPending.View.registerClass('SpottedScript.Pages.Promoters.BannersPending.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
