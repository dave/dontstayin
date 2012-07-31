Type.registerNamespace('SpottedScript.Blank.PhoneIdleScreen');
SpottedScript.Blank.PhoneIdleScreen.View=function(clientId){SpottedScript.Blank.PhoneIdleScreen.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.PhoneIdleScreen.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.PhoneIdleScreen.View.registerClass('SpottedScript.Blank.PhoneIdleScreen.View',SpottedScript.BlankUserControl.View);
