Type.registerNamespace('SpottedScript.Pages.Usrs.Buddies');
SpottedScript.Pages.Usrs.Buddies.View=function(clientId){SpottedScript.Pages.Usrs.Buddies.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Usrs.Buddies.View.prototype={clientId:null,get_usrBrowser:function(){return document.getElementById(this.clientId+'_usrBrowser');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Usrs.Buddies.View.registerClass('SpottedScript.Pages.Usrs.Buddies.View',SpottedScript.Pages.Usrs.UsrUserControl.View);
