Type.registerNamespace('SpottedScript.Blank.ParaPhotoList');
SpottedScript.Blank.ParaPhotoList.View=function(clientId){SpottedScript.Blank.ParaPhotoList.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.ParaPhotoList.View.prototype={clientId:null,get_photosDataList:function(){return document.getElementById(this.clientId+'_PhotosDataList');},get_noPhotosDiv:function(){return document.getElementById(this.clientId+'_NoPhotosDiv');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.ParaPhotoList.View.registerClass('SpottedScript.Blank.ParaPhotoList.View',SpottedScript.BlankUserControl.View);
