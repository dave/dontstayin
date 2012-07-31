Type.registerNamespace('SpottedScript.Pages.MyEvents');
SpottedScript.Pages.MyEvents.View=function(clientId){SpottedScript.Pages.MyEvents.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.MyEvents.View.prototype={clientId:null,get_h11:function(){return document.getElementById(this.clientId+'_H11');},get_eventsPanel:function(){return document.getElementById(this.clientId+'_EventsPanel');},get_eventsDataGrid:function(){return document.getElementById(this.clientId+'_EventsDataGrid');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.MyEvents.View.registerClass('SpottedScript.Pages.MyEvents.View',SpottedScript.DsiUserControl.View);
