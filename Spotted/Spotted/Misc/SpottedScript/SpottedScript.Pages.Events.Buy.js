Type.registerNamespace('SpottedScript.Pages.Events.Buy');
SpottedScript.Pages.Events.Buy.View=function(clientId){SpottedScript.Pages.Events.Buy.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Events.Buy.View.prototype={clientId:null,get_ticketsPlaceholder:function(){return document.getElementById(this.clientId+'_TicketsPlaceholder');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Events.Buy.View.registerClass('SpottedScript.Pages.Events.Buy.View',SpottedScript.Pages.Events.EventUserControl.View);
