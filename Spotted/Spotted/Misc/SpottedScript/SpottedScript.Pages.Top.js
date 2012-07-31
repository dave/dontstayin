Type.registerNamespace('SpottedScript.Pages.Top');
SpottedScript.Pages.Top.View=function(clientId){SpottedScript.Pages.Top.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Top.View.prototype={clientId:null,get_topContent:function(){return document.getElementById(this.clientId+'_TopContent');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Top.View.registerClass('SpottedScript.Pages.Top.View',SpottedScript.DsiUserControl.View);
