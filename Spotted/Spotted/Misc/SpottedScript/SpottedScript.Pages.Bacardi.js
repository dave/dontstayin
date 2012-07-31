Type.registerNamespace('SpottedScript.Pages.Bacardi');
SpottedScript.Pages.Bacardi.View=function(clientId){SpottedScript.Pages.Bacardi.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Bacardi.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Bacardi.View.registerClass('SpottedScript.Pages.Bacardi.View',SpottedScript.DsiUserControl.View);
