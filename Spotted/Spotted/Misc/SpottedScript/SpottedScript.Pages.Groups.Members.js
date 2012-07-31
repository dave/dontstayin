Type.registerNamespace('SpottedScript.Pages.Groups.Members');
SpottedScript.Pages.Groups.Members.View=function(clientId){SpottedScript.Pages.Groups.Members.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Groups.Members.View.prototype={clientId:null,get_usrBrowser:function(){return document.getElementById(this.clientId+'_usrBrowser');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Groups.Members.View.registerClass('SpottedScript.Pages.Groups.Members.View',SpottedScript.DsiUserControl.View);
