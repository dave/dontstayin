Type.registerNamespace('SpottedScript.Pages.Countries.List');
SpottedScript.Pages.Countries.List.View=function(clientId){SpottedScript.Pages.Countries.List.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Countries.List.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Countries.List.View.registerClass('SpottedScript.Pages.Countries.List.View',SpottedScript.DsiUserControl.View);
