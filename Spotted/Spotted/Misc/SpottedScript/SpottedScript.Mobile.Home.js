Type.registerNamespace('SpottedScript.Mobile.Home');
SpottedScript.Mobile.Home.View=function(clientId){SpottedScript.Mobile.Home.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Mobile.Home.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Mobile.Home.View.registerClass('SpottedScript.Mobile.Home.View',SpottedScript.MobileUserControl.View);
