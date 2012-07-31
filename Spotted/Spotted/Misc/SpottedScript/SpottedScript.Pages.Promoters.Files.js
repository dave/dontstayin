Type.registerNamespace('SpottedScript.Pages.Promoters.Files');
SpottedScript.Pages.Promoters.Files.View=function(clientId){SpottedScript.Pages.Promoters.Files.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.Files.View.prototype={clientId:null,get_header:function(){return document.getElementById(this.clientId+'_Header');},get_panelDelete:function(){return document.getElementById(this.clientId+'_PanelDelete');},get_panelList:function(){return document.getElementById(this.clientId+'_PanelList');},get_miscDataGrid:function(){return document.getElementById(this.clientId+'_MiscDataGrid');},get_miscNoFilesP:function(){return document.getElementById(this.clientId+'_MiscNoFilesP');},get_miscDataGridP:function(){return document.getElementById(this.clientId+'_MiscDataGridP');},get_panelUpload:function(){return document.getElementById(this.clientId+'_PanelUpload');},get_panelView:function(){return document.getElementById(this.clientId+'_PanelView');},get_viewBackAnchor:function(){return document.getElementById(this.clientId+'_ViewBackAnchor');},get_viewNameAnchor:function(){return document.getElementById(this.clientId+'_ViewNameAnchor');},get_viewUrlTextBox:function(){return document.getElementById(this.clientId+'_ViewUrlTextBox');},get_viewImageHtmlTextBox:function(){return document.getElementById(this.clientId+'_ViewImageHtmlTextBox');},get_viewNameCell:function(){return document.getElementById(this.clientId+'_ViewNameCell');},get_viewImageWidthCell:function(){return document.getElementById(this.clientId+'_ViewImageWidthCell');},get_viewImageHeightCell:function(){return document.getElementById(this.clientId+'_ViewImageHeightCell');},get_viewImageFileSizeCell:function(){return document.getElementById(this.clientId+'_ViewImageFileSizeCell');},get_viewLeaderboardImg:function(){return document.getElementById(this.clientId+'_ViewLeaderboardImg');},get_viewHotboxImg:function(){return document.getElementById(this.clientId+'_ViewHotboxImg');},get_viewPhotoBannerImg:function(){return document.getElementById(this.clientId+'_ViewPhotoBannerImg');},get_viewEmailBannerImg:function(){return document.getElementById(this.clientId+'_ViewEmailBannerImg');},get_viewSkyscraperImg:function(){return document.getElementById(this.clientId+'_ViewSkyscraperImg');},get_viewLeaderboardLabel:function(){return document.getElementById(this.clientId+'_ViewLeaderboardLabel');},get_viewHotboxLabel:function(){return document.getElementById(this.clientId+'_ViewHotboxLabel');},get_viewPhotoBannerLabel:function(){return document.getElementById(this.clientId+'_ViewPhotoBannerLabel');},get_viewEmailBannerLabel:function(){return document.getElementById(this.clientId+'_ViewEmailBannerLabel');},get_viewSkyscraperLabel:function(){return document.getElementById(this.clientId+'_ViewSkyscraperLabel');},get_viewImageHtmlTr:function(){return document.getElementById(this.clientId+'_ViewImageHtmlTr');},get_promoterIntro:function(){return document.getElementById(this.clientId+'_PromoterIntro');},get_h11:function(){return document.getElementById(this.clientId+'_H11');},get_inputFile:function(){return document.getElementById(this.clientId+'_InputFile');},get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_viewBannerBody:function(){return document.getElementById(this.clientId+'_ViewBannerBody');},get_viewImageBody:function(){return document.getElementById(this.clientId+'_ViewImageBody');},get_viewBrokenImg:function(){return document.getElementById(this.clientId+'_ViewBrokenImg');},get_viewBrokenLabel:function(){return document.getElementById(this.clientId+'_ViewBrokenLabel');},get_requiredFlashVersionTr:function(){return document.getElementById(this.clientId+'_RequiredFlashVersionTr');},get_requiredFlashVersion:function(){return document.getElementById(this.clientId+'_RequiredFlashVersion');},get_updateFlashVersionDone:function(){return document.getElementById(this.clientId+'_UpdateFlashVersionDone');},get_sizeWidth:function(){return document.getElementById(this.clientId+'_SizeWidth');},get_sizeHeight:function(){return document.getElementById(this.clientId+'_SizeHeight');},get_button1:function(){return document.getElementById(this.clientId+'_Button1');},get_updateSizeDone:function(){return document.getElementById(this.clientId+'_UpdateSizeDone');},get_h13:function(){return document.getElementById(this.clientId+'_H13');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.Files.View.registerClass('SpottedScript.Pages.Promoters.Files.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
