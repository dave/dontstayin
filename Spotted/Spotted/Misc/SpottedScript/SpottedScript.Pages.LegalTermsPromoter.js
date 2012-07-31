Type.registerNamespace('SpottedScript.Pages.LegalTermsPromoter');
SpottedScript.Pages.LegalTermsPromoter.View=function(clientId){SpottedScript.Pages.LegalTermsPromoter.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.LegalTermsPromoter.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.LegalTermsPromoter.View.registerClass('SpottedScript.Pages.LegalTermsPromoter.View',SpottedScript.DsiUserControl.View);
