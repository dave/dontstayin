Type.registerNamespace('SpottedScript.Pages.Groups.Categories');
SpottedScript.Pages.Groups.Categories.View=function(clientId){SpottedScript.Pages.Groups.Categories.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Groups.Categories.View.prototype={clientId:null,get_uiList:function(){return document.getElementById(this.clientId+'_uiList');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Groups.Categories.View.registerClass('SpottedScript.Pages.Groups.Categories.View',SpottedScript.DsiUserControl.View);
