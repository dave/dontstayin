Type.registerNamespace('SpottedScript.Pages.Promoters.PromoterUserControl');
SpottedScript.Pages.Promoters.PromoterUserControl.View=function(clientId){SpottedScript.Pages.Promoters.PromoterUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.PromoterUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.PromoterUserControl.View.registerClass('SpottedScript.Pages.Promoters.PromoterUserControl.View',SpottedScript.DsiUserControl.View);
