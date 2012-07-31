Type.registerNamespace('SpottedScript.Pages.LoginTest');
SpottedScript.Pages.LoginTest.View=function(clientId){SpottedScript.Pages.LoginTest.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.LoginTest.View.prototype={clientId:null,get_errorP:function(){return document.getElementById(this.clientId+'_ErrorP');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.LoginTest.View.registerClass('SpottedScript.Pages.LoginTest.View',SpottedScript.DsiUserControl.View);
