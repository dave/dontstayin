Type.registerNamespace('SpottedScript.Pages.Legal');
SpottedScript.Pages.Legal.View=function(clientId){SpottedScript.Pages.Legal.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Legal.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Legal.View.registerClass('SpottedScript.Pages.Legal.View',SpottedScript.DsiUserControl.View);
