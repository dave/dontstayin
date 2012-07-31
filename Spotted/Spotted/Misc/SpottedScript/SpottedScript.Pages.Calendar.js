Type.registerNamespace('SpottedScript.Pages.Calendar');
SpottedScript.Pages.Calendar.View=function(clientId){SpottedScript.Pages.Calendar.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Calendar.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Calendar.View.registerClass('SpottedScript.Pages.Calendar.View',SpottedScript.DsiUserControl.View);
