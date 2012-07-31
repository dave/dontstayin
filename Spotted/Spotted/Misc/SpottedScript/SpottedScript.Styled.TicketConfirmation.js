Type.registerNamespace('SpottedScript.Styled.TicketConfirmation');
SpottedScript.Styled.TicketConfirmation.View=function(clientId){SpottedScript.Styled.TicketConfirmation.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Styled.TicketConfirmation.View.prototype={clientId:null,get_ticketConfirmationLabel:function(){return document.getElementById(this.clientId+'_TicketConfirmationLabel');},get_eventLinkLabel:function(){return document.getElementById(this.clientId+'_EventLinkLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Styled.TicketConfirmation.View.registerClass('SpottedScript.Styled.TicketConfirmation.View',SpottedScript.StyledUserControl.View);
