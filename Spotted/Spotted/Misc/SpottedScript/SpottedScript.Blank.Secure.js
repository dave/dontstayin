Type.registerNamespace('SpottedScript.Blank.Secure');
SpottedScript.Blank.Secure.View=function(clientId){SpottedScript.Blank.Secure.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Secure.View.prototype={clientId:null,get_introHeader:function(){return document.getElementById(this.clientId+'_IntroHeader');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Secure.View.registerClass('SpottedScript.Blank.Secure.View',SpottedScript.BlankUserControl.View);
