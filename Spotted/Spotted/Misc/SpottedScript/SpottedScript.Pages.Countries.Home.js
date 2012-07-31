Type.registerNamespace('SpottedScript.Pages.Countries.Home');
SpottedScript.Pages.Countries.Home.View=function(clientId){SpottedScript.Pages.Countries.Home.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Countries.Home.View.prototype={clientId:null,get_homeContentTopUc:function(){return document.getElementById(this.clientId+'_HomeContentTopUc');},get_latest:function(){return document.getElementById(this.clientId+'_Latest');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Countries.Home.View.registerClass('SpottedScript.Pages.Countries.Home.View',SpottedScript.DsiUserControl.View);
