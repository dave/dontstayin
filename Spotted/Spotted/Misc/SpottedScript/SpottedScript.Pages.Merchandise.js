Type.registerNamespace('SpottedScript.Pages.Merchandise');
SpottedScript.Pages.Merchandise.View=function(clientId){SpottedScript.Pages.Merchandise.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Merchandise.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Merchandise.View.registerClass('SpottedScript.Pages.Merchandise.View',SpottedScript.DsiUserControl.View);
