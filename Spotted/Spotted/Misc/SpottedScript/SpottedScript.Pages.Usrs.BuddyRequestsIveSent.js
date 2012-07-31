Type.registerNamespace('SpottedScript.Pages.Usrs.BuddyRequestsIveSent');
SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View=function(clientId){SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View.prototype={clientId:null,get_usrBrowser:function(){return document.getElementById(this.clientId+'_usrBrowser');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View.registerClass('SpottedScript.Pages.Usrs.BuddyRequestsIveSent.View',SpottedScript.Pages.Usrs.UsrUserControl.View);
