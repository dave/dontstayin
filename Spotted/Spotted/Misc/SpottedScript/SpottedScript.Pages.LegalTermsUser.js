Type.registerNamespace('SpottedScript.Pages.LegalTermsUser');
SpottedScript.Pages.LegalTermsUser.View=function(clientId){SpottedScript.Pages.LegalTermsUser.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.LegalTermsUser.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.LegalTermsUser.View.registerClass('SpottedScript.Pages.LegalTermsUser.View',SpottedScript.DsiUserControl.View);
