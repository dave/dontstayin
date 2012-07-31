Type.registerNamespace('SpottedScript.Blank.Flush');
SpottedScript.Blank.Flush.View=function(clientId){SpottedScript.Blank.Flush.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Flush.View.prototype={clientId:null,get_lab1:function(){return document.getElementById(this.clientId+'_Lab1');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Flush.View.registerClass('SpottedScript.Blank.Flush.View',SpottedScript.BlankUserControl.View);
