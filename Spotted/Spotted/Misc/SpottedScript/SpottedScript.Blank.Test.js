Type.registerNamespace('SpottedScript.Blank.Test');
SpottedScript.Blank.Test.View=function(clientId){SpottedScript.Blank.Test.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Test.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Test.View.registerClass('SpottedScript.Blank.Test.View',SpottedScript.BlankUserControl.View);
