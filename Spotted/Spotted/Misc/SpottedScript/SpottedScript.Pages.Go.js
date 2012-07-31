Type.registerNamespace('SpottedScript.Pages.Go');
SpottedScript.Pages.Go.View=function(clientId){SpottedScript.Pages.Go.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Go.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Go.View.registerClass('SpottedScript.Pages.Go.View',SpottedScript.DsiUserControl.View);
