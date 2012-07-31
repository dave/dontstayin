Type.registerNamespace('SpottedScript.MixmagGreatestUserControl');
SpottedScript.MixmagGreatestUserControl.View=function(clientId){SpottedScript.MixmagGreatestUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MixmagGreatestUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MixmagGreatestUserControl.View.registerClass('SpottedScript.MixmagGreatestUserControl.View',SpottedScript.GenericUserControl.View);
