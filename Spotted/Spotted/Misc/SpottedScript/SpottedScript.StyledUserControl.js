Type.registerNamespace('SpottedScript.StyledUserControl');
SpottedScript.StyledUserControl.View=function(clientId){SpottedScript.StyledUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.StyledUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.StyledUserControl.View.registerClass('SpottedScript.StyledUserControl.View',SpottedScript.GenericUserControl.View);
