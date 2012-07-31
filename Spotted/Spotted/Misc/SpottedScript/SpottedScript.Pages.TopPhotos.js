Type.registerNamespace('SpottedScript.Pages.TopPhotos');
SpottedScript.Pages.TopPhotos.View=function(clientId){SpottedScript.Pages.TopPhotos.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.TopPhotos.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.TopPhotos.View.registerClass('SpottedScript.Pages.TopPhotos.View',SpottedScript.DsiUserControl.View);
