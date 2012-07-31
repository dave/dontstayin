Type.registerNamespace('SpottedScript.Pages.PlaceMissing');
SpottedScript.Pages.PlaceMissing.View=function(clientId){SpottedScript.Pages.PlaceMissing.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.PlaceMissing.View.prototype={clientId:null,get_introHeader:function(){return document.getElementById(this.clientId+'_IntroHeader');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.PlaceMissing.View.registerClass('SpottedScript.Pages.PlaceMissing.View',SpottedScript.DsiUserControl.View);
