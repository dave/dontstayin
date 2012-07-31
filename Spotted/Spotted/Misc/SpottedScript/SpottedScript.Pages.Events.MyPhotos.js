Type.registerNamespace('SpottedScript.Pages.Events.MyPhotos');
SpottedScript.Pages.Events.MyPhotos.View=function(clientId){SpottedScript.Pages.Events.MyPhotos.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Events.MyPhotos.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Events.MyPhotos.View.registerClass('SpottedScript.Pages.Events.MyPhotos.View',SpottedScript.DsiUserControl.View);
