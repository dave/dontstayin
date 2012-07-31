Type.registerNamespace('SpottedScript.BlankUserControl');
SpottedScript.BlankUserControl.View=function(clientId){SpottedScript.BlankUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.BlankUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.BlankUserControl.View.registerClass('SpottedScript.BlankUserControl.View',SpottedScript.GenericUserControl.View);
