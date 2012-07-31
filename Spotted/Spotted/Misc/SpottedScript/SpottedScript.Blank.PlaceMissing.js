Type.registerNamespace('SpottedScript.Blank.PlaceMissing');
SpottedScript.Blank.PlaceMissing.View=function(clientId){SpottedScript.Blank.PlaceMissing.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.PlaceMissing.View.prototype={clientId:null,get_introHeader:function(){return document.getElementById(this.clientId+'_IntroHeader');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.PlaceMissing.View.registerClass('SpottedScript.Blank.PlaceMissing.View',SpottedScript.BlankUserControl.View);
