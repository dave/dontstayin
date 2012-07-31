Type.registerNamespace('SpottedScript.Blank.FlyerClick');
SpottedScript.Blank.FlyerClick.View=function(clientId){SpottedScript.Blank.FlyerClick.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.FlyerClick.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.FlyerClick.View.registerClass('SpottedScript.Blank.FlyerClick.View',SpottedScript.BlankUserControl.View);
