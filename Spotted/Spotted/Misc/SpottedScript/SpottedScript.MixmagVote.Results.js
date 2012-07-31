Type.registerNamespace('SpottedScript.MixmagVote.Results');
SpottedScript.MixmagVote.Results.View=function(clientId){SpottedScript.MixmagVote.Results.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MixmagVote.Results.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MixmagVote.Results.View.registerClass('SpottedScript.MixmagVote.Results.View',SpottedScript.MixmagVoteUserControl.View);
