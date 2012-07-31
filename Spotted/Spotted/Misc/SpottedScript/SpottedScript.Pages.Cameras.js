Type.registerNamespace('SpottedScript.Pages.Cameras');
SpottedScript.Pages.Cameras.View=function(clientId){SpottedScript.Pages.Cameras.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Cameras.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Cameras.View.registerClass('SpottedScript.Pages.Cameras.View',SpottedScript.DsiUserControl.View);
