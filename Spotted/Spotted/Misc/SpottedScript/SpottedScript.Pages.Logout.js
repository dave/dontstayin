Type.registerNamespace('SpottedScript.Pages.Logout');
SpottedScript.Pages.Logout.View=function(clientId){SpottedScript.Pages.Logout.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Logout.View.prototype={clientId:null,get_autoLogout_Value:function(){return document.getElementById(this.clientId+'_AutoLogout_Value');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Logout.View.registerClass('SpottedScript.Pages.Logout.View',SpottedScript.DsiUserControl.View);
