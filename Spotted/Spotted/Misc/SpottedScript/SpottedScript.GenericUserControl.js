Type.registerNamespace('SpottedScript.GenericUserControl');
SpottedScript.GenericUserControl.View=function(clientId){this.clientId=clientId;}
SpottedScript.GenericUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.GenericUserControl.View.registerClass('SpottedScript.GenericUserControl.View');
