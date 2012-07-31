Type.registerNamespace('SpottedScript.Blank.PhoneSettings');
SpottedScript.Blank.PhoneSettings.View=function(clientId){SpottedScript.Blank.PhoneSettings.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.PhoneSettings.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.PhoneSettings.View.registerClass('SpottedScript.Blank.PhoneSettings.View',SpottedScript.BlankUserControl.View);
