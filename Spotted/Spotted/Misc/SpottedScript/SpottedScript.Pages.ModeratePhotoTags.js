Type.registerNamespace('SpottedScript.Pages.ModeratePhotoTags');
SpottedScript.Pages.ModeratePhotoTags.View=function(clientId){SpottedScript.Pages.ModeratePhotoTags.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.ModeratePhotoTags.View.prototype={clientId:null,get_h16:function(){return document.getElementById(this.clientId+'_H16');},get_uiPhotoImg:function(){return document.getElementById(this.clientId+'_uiPhotoImg');},get_uiNoTags:function(){return document.getElementById(this.clientId+'_uiNoTags');},get_uiPhotoTags:function(){return document.getElementById(this.clientId+'_uiPhotoTags');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.ModeratePhotoTags.View.registerClass('SpottedScript.Pages.ModeratePhotoTags.View',SpottedScript.DsiUserControl.View);
