Type.registerNamespace('SpottedScript.Mobile.Home1');
SpottedScript.Mobile.Home1.View=function(clientId){SpottedScript.Mobile.Home1.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Mobile.Home1.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Mobile.Home1.View.registerClass('SpottedScript.Mobile.Home1.View',SpottedScript.MobileUserControl.View);
