Type.registerNamespace('SpottedScript.Blank.Doorlist');
SpottedScript.Blank.Doorlist.View=function(clientId){SpottedScript.Blank.Doorlist.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Doorlist.View.prototype={clientId:null,get_uiDoorlist:function(){return document.getElementById(this.clientId+'_uiDoorlist');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Doorlist.View.registerClass('SpottedScript.Blank.Doorlist.View',SpottedScript.BlankUserControl.View);
