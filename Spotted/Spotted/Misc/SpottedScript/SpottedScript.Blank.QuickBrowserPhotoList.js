Type.registerNamespace('SpottedScript.Blank.QuickBrowserPhotoList');
SpottedScript.Blank.QuickBrowserPhotoList.View=function(clientId){SpottedScript.Blank.QuickBrowserPhotoList.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.QuickBrowserPhotoList.View.prototype={clientId:null,get_photoListContent:function(){return document.getElementById(this.clientId+'_PhotoListContent');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.QuickBrowserPhotoList.View.registerClass('SpottedScript.Blank.QuickBrowserPhotoList.View',SpottedScript.BlankUserControl.View);
