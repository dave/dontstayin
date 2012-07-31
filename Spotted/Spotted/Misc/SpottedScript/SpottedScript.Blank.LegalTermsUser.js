Type.registerNamespace('SpottedScript.Blank.LegalTermsUser');
SpottedScript.Blank.LegalTermsUser.View=function(clientId){SpottedScript.Blank.LegalTermsUser.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.LegalTermsUser.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.LegalTermsUser.View.registerClass('SpottedScript.Blank.LegalTermsUser.View',SpottedScript.BlankUserControl.View);
