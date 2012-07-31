Type.registerNamespace('SpottedScript.Pages.TalkToFrank');
SpottedScript.Pages.TalkToFrank.View=function(clientId){SpottedScript.Pages.TalkToFrank.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.TalkToFrank.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.TalkToFrank.View.registerClass('SpottedScript.Pages.TalkToFrank.View',SpottedScript.DsiUserControl.View);
