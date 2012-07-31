Type.registerNamespace('SpottedScript.Blank.Mixmag');
SpottedScript.Blank.Mixmag.View=function(clientId){SpottedScript.Blank.Mixmag.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Mixmag.View.prototype={clientId:null,get_button3:function(){return document.getElementById(this.clientId+'_Button3');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Mixmag.View.registerClass('SpottedScript.Blank.Mixmag.View',SpottedScript.BlankUserControl.View);
