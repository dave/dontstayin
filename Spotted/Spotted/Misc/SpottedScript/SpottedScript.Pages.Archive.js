Type.registerNamespace('SpottedScript.Pages.Archive');
SpottedScript.Pages.Archive.View=function(clientId){SpottedScript.Pages.Archive.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Archive.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Archive.View.registerClass('SpottedScript.Pages.Archive.View',SpottedScript.DsiUserControl.View);
