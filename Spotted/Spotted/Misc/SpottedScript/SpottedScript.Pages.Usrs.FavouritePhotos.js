Type.registerNamespace('SpottedScript.Pages.Usrs.FavouritePhotos');
SpottedScript.Pages.Usrs.FavouritePhotos.Controller=function(view){SpottedScript.Pages.Usrs.FavouritePhotos.Controller.initializeBase(this);this.$5=view;this.setupController();}
SpottedScript.Pages.Usrs.FavouritePhotos.Controller.prototype={$5:null,get_photoControl:function(){return this.$5.get_uiPhotoControl();},get_photoBrowser:function(){return this.$5.get_uiPhotoBrowser();},get_threadControl:function(){return this.$5.get_uiThreadControl();},$6:null,get_photoProvider:function(){return this.$6||(this.$6=new SpottedScript.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider(Number.parseInvariant(this.$5.get_uiUsrK().value)));},get_latestChatController:function(){return this.$5.get_uiLatestChat();}}
SpottedScript.Pages.Usrs.FavouritePhotos.View=function(clientId){SpottedScript.Pages.Usrs.FavouritePhotos.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Usrs.FavouritePhotos.View.prototype={clientId:null,get_uiTitle:function(){return document.getElementById(this.clientId+'_uiTitle');},get_uiPhotoBrowser:function(){return eval(this.clientId+'_uiPhotoBrowserController');},get_uiPhotoControl:function(){return eval(this.clientId+'_uiPhotoControlController');},get_uiUpdatePanel:function(){return document.getElementById(this.clientId+'_uiUpdatePanel');},get_uiLatestChat:function(){return eval(this.clientId+'_uiLatestChatController');},get_uiThreadControl:function(){return eval(this.clientId+'_uiThreadControlController');},get_uiUsrK:function(){return document.getElementById(this.clientId+'_uiUsrK');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Usrs.FavouritePhotos.Controller.registerClass('SpottedScript.Pages.Usrs.FavouritePhotos.Controller',SpottedScript.Controls.PhotoBrowser.PhotosController);
SpottedScript.Pages.Usrs.FavouritePhotos.View.registerClass('SpottedScript.Pages.Usrs.FavouritePhotos.View',SpottedScript.DsiUserControl.View);
