Type.registerNamespace('SpottedScript.Pages.SearchResults');
SpottedScript.Pages.SearchResults.View=function(clientId){SpottedScript.Pages.SearchResults.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.SearchResults.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.SearchResults.View.registerClass('SpottedScript.Pages.SearchResults.View',SpottedScript.DsiUserControl.View);
