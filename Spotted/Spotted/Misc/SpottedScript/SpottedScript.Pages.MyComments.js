Type.registerNamespace('SpottedScript.Pages.MyComments');
SpottedScript.Pages.MyComments.View=function(clientId){SpottedScript.Pages.MyComments.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.MyComments.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.MyComments.View.registerClass('SpottedScript.Pages.MyComments.View',SpottedScript.DsiUserControl.View);
