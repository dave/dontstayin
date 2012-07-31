Type.registerNamespace('SpottedScript.Pages.Online');
SpottedScript.Pages.Online.View=function(clientId){SpottedScript.Pages.Online.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Online.View.prototype={clientId:null,get_onlineLabel:function(){return document.getElementById(this.clientId+'_OnlineLabel');},get_onlineP:function(){return document.getElementById(this.clientId+'_OnlineP');},get_onlineDataList:function(){return document.getElementById(this.clientId+'_OnlineDataList');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Online.View.registerClass('SpottedScript.Pages.Online.View',SpottedScript.DsiUserControl.View);
