Type.registerNamespace('SpottedScript.Blank.Flyer');
SpottedScript.Blank.Flyer.View=function(clientId){SpottedScript.Blank.Flyer.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Flyer.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Flyer.View.registerClass('SpottedScript.Blank.Flyer.View',SpottedScript.BlankUserControl.View);
