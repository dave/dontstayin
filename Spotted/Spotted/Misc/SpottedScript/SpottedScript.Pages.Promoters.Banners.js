Type.registerNamespace('SpottedScript.Pages.Promoters.Banners');
SpottedScript.Pages.Promoters.Banners.View=function(clientId){SpottedScript.Pages.Promoters.Banners.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.Banners.View.prototype={clientId:null,get_panelBannerList:function(){return document.getElementById(this.clientId+'_PanelBannerList');},get_bannerListHeader:function(){return document.getElementById(this.clientId+'_BannerListHeader');},get_bannerListAddLink:function(){return document.getElementById(this.clientId+'_BannerListAddLink');},get_bannerListDataGrid:function(){return document.getElementById(this.clientId+'_BannerListDataGrid');},get_promoterintro1:function(){return document.getElementById(this.clientId+'_Promoterintro1');},get_folderDropDown:function(){return document.getElementById(this.clientId+'_FolderDropDown');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.Banners.View.registerClass('SpottedScript.Pages.Promoters.Banners.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
