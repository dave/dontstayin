Type.registerNamespace('SpottedScript.Pages.Events.EventUserControl');
SpottedScript.Pages.Events.EventUserControl.View=function(clientId){SpottedScript.Pages.Events.EventUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Events.EventUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Events.EventUserControl.View.registerClass('SpottedScript.Pages.Events.EventUserControl.View',SpottedScript.DsiUserControl.View);
