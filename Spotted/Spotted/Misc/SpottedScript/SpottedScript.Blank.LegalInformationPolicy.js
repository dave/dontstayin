Type.registerNamespace('SpottedScript.Blank.LegalInformationPolicy');
SpottedScript.Blank.LegalInformationPolicy.View=function(clientId){SpottedScript.Blank.LegalInformationPolicy.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.LegalInformationPolicy.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.LegalInformationPolicy.View.registerClass('SpottedScript.Blank.LegalInformationPolicy.View',SpottedScript.BlankUserControl.View);
