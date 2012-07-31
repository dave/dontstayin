Type.registerNamespace('SpottedScript.Blank.ResetPhoneIp');
SpottedScript.Blank.ResetPhoneIp.View=function(clientId){SpottedScript.Blank.ResetPhoneIp.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.ResetPhoneIp.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.ResetPhoneIp.View.registerClass('SpottedScript.Blank.ResetPhoneIp.View',SpottedScript.BlankUserControl.View);
