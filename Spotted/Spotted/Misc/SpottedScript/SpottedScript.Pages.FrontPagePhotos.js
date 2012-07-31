Type.registerNamespace('SpottedScript.Pages.FrontPagePhotos');
SpottedScript.Pages.FrontPagePhotos.View=function(clientId){SpottedScript.Pages.FrontPagePhotos.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.FrontPagePhotos.View.prototype={clientId:null,get_h18:function(){return document.getElementById(this.clientId+'_H18');},get_h19:function(){return document.getElementById(this.clientId+'_H19');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.FrontPagePhotos.View.registerClass('SpottedScript.Pages.FrontPagePhotos.View',SpottedScript.DsiUserControl.View);
