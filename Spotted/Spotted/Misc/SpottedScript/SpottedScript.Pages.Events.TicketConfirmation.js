Type.registerNamespace('SpottedScript.Pages.Events.TicketConfirmation');
SpottedScript.Pages.Events.TicketConfirmation.View=function(clientId){SpottedScript.Pages.Events.TicketConfirmation.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Events.TicketConfirmation.View.prototype={clientId:null,get_eventTicketConfirmationPanel:function(){return document.getElementById(this.clientId+'_EventTicketConfirmationPanel');},get_ticketsHeading:function(){return document.getElementById(this.clientId+'_TicketsHeading');},get_ticketConfirmationLabel:function(){return document.getElementById(this.clientId+'_TicketConfirmationLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Events.TicketConfirmation.View.registerClass('SpottedScript.Pages.Events.TicketConfirmation.View',SpottedScript.Pages.Events.EventUserControl.View);
