Type.registerNamespace('SpottedScript.MobileUserControl');
SpottedScript.MobileUserControl.View=function(clientId){SpottedScript.MobileUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MobileUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MobileUserControl.View.registerClass('SpottedScript.MobileUserControl.View',SpottedScript.GenericUserControl.View);
