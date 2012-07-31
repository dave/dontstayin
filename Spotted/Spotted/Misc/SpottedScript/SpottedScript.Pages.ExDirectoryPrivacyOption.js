Type.registerNamespace('SpottedScript.Pages.ExDirectoryPrivacyOption');
SpottedScript.Pages.ExDirectoryPrivacyOption.View=function(clientId){SpottedScript.Pages.ExDirectoryPrivacyOption.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.ExDirectoryPrivacyOption.View.prototype={clientId:null,get_uiExDirectory:function(){return document.getElementById(this.clientId+'_uiExDirectory');},get_uiSuccess:function(){return document.getElementById(this.clientId+'_uiSuccess');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.ExDirectoryPrivacyOption.View.registerClass('SpottedScript.Pages.ExDirectoryPrivacyOption.View',SpottedScript.DsiUserControl.View);
