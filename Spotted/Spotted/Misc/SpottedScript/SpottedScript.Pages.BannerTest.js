Type.registerNamespace('SpottedScript.Pages.BannerTest');
SpottedScript.Pages.BannerTest.View=function(clientId){SpottedScript.Pages.BannerTest.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.BannerTest.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.BannerTest.View.registerClass('SpottedScript.Pages.BannerTest.View',SpottedScript.DsiUserControl.View);
