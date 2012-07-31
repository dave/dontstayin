Type.registerNamespace('SpottedScript.Pages.Connect');
SpottedScript.Pages.Connect.View=function(clientId){SpottedScript.Pages.Connect.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Connect.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Connect.View.registerClass('SpottedScript.Pages.Connect.View',SpottedScript.DsiUserControl.View);
