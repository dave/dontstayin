Type.registerNamespace('SpottedScript.Styled.Calendar');
SpottedScript.Styled.Calendar.View=function(clientId){SpottedScript.Styled.Calendar.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Styled.Calendar.View.prototype={clientId:null,get_eventLinkRepeater:function(){return document.getElementById(this.clientId+'_EventLinkRepeater');},get_noEventsLabel:function(){return document.getElementById(this.clientId+'_NoEventsLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Styled.Calendar.View.registerClass('SpottedScript.Styled.Calendar.View',SpottedScript.StyledUserControl.View);
