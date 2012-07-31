Type.registerNamespace('SpottedScript.Pages.Usrs.UsrUserControl');
SpottedScript.Pages.Usrs.UsrUserControl.View=function(clientId){SpottedScript.Pages.Usrs.UsrUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Usrs.UsrUserControl.View.prototype={clientId:null,get_bannedUserPanel:function(){return document.getElementById(this.clientId+'_BannedUserPanel');},get_unsubscribedUserPanel:function(){return document.getElementById(this.clientId+'_UnsubscribedUserPanel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Usrs.UsrUserControl.View.registerClass('SpottedScript.Pages.Usrs.UsrUserControl.View',SpottedScript.DsiUserControl.View);
