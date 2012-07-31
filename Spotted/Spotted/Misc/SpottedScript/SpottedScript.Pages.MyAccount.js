Type.registerNamespace('SpottedScript.Pages.MyAccount');
SpottedScript.Pages.MyAccount.View=function(clientId){SpottedScript.Pages.MyAccount.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.MyAccount.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.MyAccount.View.registerClass('SpottedScript.Pages.MyAccount.View',SpottedScript.DsiUserControl.View);
