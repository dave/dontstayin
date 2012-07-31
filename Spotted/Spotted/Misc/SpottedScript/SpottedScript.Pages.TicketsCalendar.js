Type.registerNamespace('SpottedScript.Pages.TicketsCalendar');
SpottedScript.Pages.TicketsCalendar.View=function(clientId){SpottedScript.Pages.TicketsCalendar.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.TicketsCalendar.View.prototype={clientId:null,get_ticketsCalendarContent:function(){return document.getElementById(this.clientId+'_TicketsCalendarContent');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.TicketsCalendar.View.registerClass('SpottedScript.Pages.TicketsCalendar.View',SpottedScript.DsiUserControl.View);
