Type.registerNamespace('SpottedScript.Blank.BannerEditMusic');
SpottedScript.Blank.BannerEditMusic.View=function(clientId){SpottedScript.Blank.BannerEditMusic.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.BannerEditMusic.View.prototype={clientId:null,get_uiMusicTypesControl:function(){return document.getElementById(this.clientId+'_uiMusicTypesControl');},get_uiSaveButton:function(){return document.getElementById(this.clientId+'_uiSaveButton');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.BannerEditMusic.View.registerClass('SpottedScript.Blank.BannerEditMusic.View',SpottedScript.BlankUserControl.View);
