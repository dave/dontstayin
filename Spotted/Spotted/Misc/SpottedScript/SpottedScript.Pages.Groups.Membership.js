Type.registerNamespace('SpottedScript.Pages.Groups.Membership');
SpottedScript.Pages.Groups.Membership.View=function(clientId){SpottedScript.Pages.Groups.Membership.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Groups.Membership.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Groups.Membership.View.registerClass('SpottedScript.Pages.Groups.Membership.View',SpottedScript.DsiUserControl.View);
