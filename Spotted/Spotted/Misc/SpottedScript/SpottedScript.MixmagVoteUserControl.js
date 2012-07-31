Type.registerNamespace('SpottedScript.MixmagVoteUserControl');
SpottedScript.MixmagVoteUserControl.View=function(clientId){SpottedScript.MixmagVoteUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MixmagVoteUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MixmagVoteUserControl.View.registerClass('SpottedScript.MixmagVoteUserControl.View',SpottedScript.GenericUserControl.View);
