Type.registerNamespace('SpottedScript.Blank.LegalTermsPromoter');
SpottedScript.Blank.LegalTermsPromoter.View=function(clientId){SpottedScript.Blank.LegalTermsPromoter.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.LegalTermsPromoter.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.LegalTermsPromoter.View.registerClass('SpottedScript.Blank.LegalTermsPromoter.View',SpottedScript.BlankUserControl.View);
