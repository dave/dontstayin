Type.registerNamespace('SpottedScript.Blank.Banned');
SpottedScript.Blank.Banned.View=function(clientId){SpottedScript.Blank.Banned.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Banned.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Banned.View.registerClass('SpottedScript.Blank.Banned.View',SpottedScript.BlankUserControl.View);
