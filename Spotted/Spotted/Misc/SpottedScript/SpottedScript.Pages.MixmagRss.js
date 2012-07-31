Type.registerNamespace('SpottedScript.Pages.MixmagRss');
SpottedScript.Pages.MixmagRss.View=function(clientId){SpottedScript.Pages.MixmagRss.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.MixmagRss.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.MixmagRss.View.registerClass('SpottedScript.Pages.MixmagRss.View',SpottedScript.DsiUserControl.View);
