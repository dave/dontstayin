Type.registerNamespace('SpottedScript.Blank.Error');
SpottedScript.Blank.Error.View=function(clientId){SpottedScript.Blank.Error.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Error.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Error.View.registerClass('SpottedScript.Blank.Error.View',SpottedScript.BlankUserControl.View);
