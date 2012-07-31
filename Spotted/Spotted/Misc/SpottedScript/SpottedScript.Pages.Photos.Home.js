Type.registerNamespace('SpottedScript.Pages.Photos.Home');
SpottedScript.Pages.Photos.Home.View=function(clientId){SpottedScript.Pages.Photos.Home.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Photos.Home.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Photos.Home.View.registerClass('SpottedScript.Pages.Photos.Home.View',SpottedScript.DsiUserControl.View);
