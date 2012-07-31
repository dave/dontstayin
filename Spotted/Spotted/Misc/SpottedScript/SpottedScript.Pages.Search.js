Type.registerNamespace('SpottedScript.Pages.Search');
SpottedScript.Pages.Search.View=function(clientId){SpottedScript.Pages.Search.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Search.View.prototype={clientId:null,get_h1:function(){return document.getElementById(this.clientId+'_H1');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Search.View.registerClass('SpottedScript.Pages.Search.View',SpottedScript.DsiUserControl.View);
