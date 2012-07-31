Type.registerNamespace('SpottedScript.Styled.Legal');
SpottedScript.Styled.Legal.View=function(clientId){SpottedScript.Styled.Legal.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Styled.Legal.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Styled.Legal.View.registerClass('SpottedScript.Styled.Legal.View',SpottedScript.StyledUserControl.View);
