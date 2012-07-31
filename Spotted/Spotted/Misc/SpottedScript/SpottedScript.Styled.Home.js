Type.registerNamespace('SpottedScript.Styled.Home');
SpottedScript.Styled.Home.View=function(clientId){SpottedScript.Styled.Home.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Styled.Home.View.prototype={clientId:null,get_eventLinkRepeater:function(){return document.getElementById(this.clientId+'_EventLinkRepeater');},get_noEventsLabel:function(){return document.getElementById(this.clientId+'_NoEventsLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Styled.Home.View.registerClass('SpottedScript.Styled.Home.View',SpottedScript.StyledUserControl.View);
