Type.registerNamespace('SpottedScript.MixmagGreatest.Vote');
SpottedScript.MixmagGreatest.Vote.View=function(clientId){SpottedScript.MixmagGreatest.Vote.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MixmagGreatest.Vote.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MixmagGreatest.Vote.View.registerClass('SpottedScript.MixmagGreatest.Vote.View',SpottedScript.MixmagGreatestUserControl.View);
