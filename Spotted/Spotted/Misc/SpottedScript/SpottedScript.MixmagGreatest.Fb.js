Type.registerNamespace('SpottedScript.MixmagGreatest.Fb');
SpottedScript.MixmagGreatest.Fb.View=function(clientId){SpottedScript.MixmagGreatest.Fb.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MixmagGreatest.Fb.View.prototype={clientId:null,get_facebookComments:function(){return document.getElementById(this.clientId+'_FacebookComments');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MixmagGreatest.Fb.View.registerClass('SpottedScript.MixmagGreatest.Fb.View',SpottedScript.MixmagGreatestUserControl.View);
