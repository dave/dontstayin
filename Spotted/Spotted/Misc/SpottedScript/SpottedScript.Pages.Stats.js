Type.registerNamespace('SpottedScript.Pages.Stats');
SpottedScript.Pages.Stats.View=function(clientId){SpottedScript.Pages.Stats.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Stats.View.prototype={clientId:null,get_usersOnline5MinLabel:function(){return document.getElementById(this.clientId+'_UsersOnline5MinLabel');},get_maxUsersOnline5MinDateLabel:function(){return document.getElementById(this.clientId+'_MaxUsersOnline5MinDateLabel');},get_maxUsersOnline5MinLabel:function(){return document.getElementById(this.clientId+'_MaxUsersOnline5MinLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Stats.View.registerClass('SpottedScript.Pages.Stats.View',SpottedScript.DsiUserControl.View);
