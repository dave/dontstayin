Type.registerNamespace('SpottedScript.Pages.MyDetails');
SpottedScript.Pages.MyDetails.View=function(clientId){SpottedScript.Pages.MyDetails.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.MyDetails.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.MyDetails.View.registerClass('SpottedScript.Pages.MyDetails.View',SpottedScript.DsiUserControl.View);
