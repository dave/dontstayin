Type.registerNamespace('SpottedScript.Styled.Test');
SpottedScript.Styled.Test.View=function(clientId){SpottedScript.Styled.Test.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Styled.Test.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Styled.Test.View.registerClass('SpottedScript.Styled.Test.View',SpottedScript.StyledUserControl.View);
