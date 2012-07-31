Type.registerNamespace('SpottedScript.Pages.Groups.Join');
SpottedScript.Pages.Groups.Join.View=function(clientId){SpottedScript.Pages.Groups.Join.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Groups.Join.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Groups.Join.View.registerClass('SpottedScript.Pages.Groups.Join.View',SpottedScript.DsiUserControl.View);
