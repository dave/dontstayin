Type.registerNamespace('SpottedScript.Pages.Mixmag');
SpottedScript.Pages.Mixmag.View=function(clientId){SpottedScript.Pages.Mixmag.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Mixmag.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Mixmag.View.registerClass('SpottedScript.Pages.Mixmag.View',SpottedScript.DsiUserControl.View);
