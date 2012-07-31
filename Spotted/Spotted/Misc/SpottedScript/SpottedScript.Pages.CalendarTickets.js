Type.registerNamespace('SpottedScript.Pages.CalendarTickets');
SpottedScript.Pages.CalendarTickets.View=function(clientId){SpottedScript.Pages.CalendarTickets.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.CalendarTickets.View.prototype={clientId:null,get_calendarTicketsContent:function(){return document.getElementById(this.clientId+'_CalendarTicketsContent');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.CalendarTickets.View.registerClass('SpottedScript.Pages.CalendarTickets.View',SpottedScript.DsiUserControl.View);
