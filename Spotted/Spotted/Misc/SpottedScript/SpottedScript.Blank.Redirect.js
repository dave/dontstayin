Type.registerNamespace('SpottedScript.Blank.Redirect');
SpottedScript.Blank.Redirect.View=function(clientId){SpottedScript.Blank.Redirect.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Redirect.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Redirect.View.registerClass('SpottedScript.Blank.Redirect.View',SpottedScript.BlankUserControl.View);
