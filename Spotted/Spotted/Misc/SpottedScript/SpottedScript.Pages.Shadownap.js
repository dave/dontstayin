Type.registerNamespace('SpottedScript.Pages.Shadownap');
SpottedScript.Pages.Shadownap.View=function(clientId){SpottedScript.Pages.Shadownap.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Shadownap.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Shadownap.View.registerClass('SpottedScript.Pages.Shadownap.View',SpottedScript.DsiUserControl.View);
