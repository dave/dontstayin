Type.registerNamespace('SpottedScript.MixmagVote.Stop');
SpottedScript.MixmagVote.Stop.View=function(clientId){SpottedScript.MixmagVote.Stop.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MixmagVote.Stop.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MixmagVote.Stop.View.registerClass('SpottedScript.MixmagVote.Stop.View',SpottedScript.MixmagVoteUserControl.View);
