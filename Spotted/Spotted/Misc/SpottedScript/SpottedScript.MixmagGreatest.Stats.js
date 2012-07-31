Type.registerNamespace('SpottedScript.MixmagGreatest.Stats');
SpottedScript.MixmagGreatest.Stats.View=function(clientId){SpottedScript.MixmagGreatest.Stats.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MixmagGreatest.Stats.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MixmagGreatest.Stats.View.registerClass('SpottedScript.MixmagGreatest.Stats.View',SpottedScript.MixmagGreatestUserControl.View);
