Type.registerNamespace('SpottedScript.Controls.PhotoControl');
SpottedScript.Controls.PhotoControl.BannerStub=function(){}
SpottedScript.Controls.PhotoControl.BannerStub.prototype={k:0,html:null,script:null}
SpottedScript.Controls.PhotoControl.PhotoStub=function(){}
SpottedScript.Controls.PhotoControl.PhotoStub.prototype={k:0,url:null,iconPath:null,webPath:null,thumbPath:null,width:0,height:0,thumbWidth:0,thumbHeight:0,takenByDetailsHtml:null,usrLink:null,photoVideoLabel:null,isPhoto:false,isVideo:false,videoMedWidth:0,videoMedHeight:0,videoMedPath:null,usrsInPhotoHtml:null,usrIsInPhoto:false,isFavourite:false,isInCompetitionGroup:false,canEnterCompetition:false,quickBrowserUrl:null,downloadPhotoLinkHtml:null,linkToPhotoUrl:null,embedThisPhotoHtml:null,photoUsageAdminString:null,threadK:0,commentsCount:0,chatRoomGuid:null,rolloverMouseOverText:null}
SpottedScript.Controls.PhotoControl.PhotoResult=function(){}
SpottedScript.Controls.PhotoControl.PhotoResult.prototype={photos:null,lastPage:0}
SpottedScript.Controls.PhotoControl.Controller=function(view){SpottedScript.Controls.PhotoControl.Controller.initializeBase(this,[[view.get_uiPhoto(),view.get_uiFlashHolder()]]);this.$5=view;this.$C=Boolean.parse(view.get_uiDisplayMakeFrontPageOptions().value);$addHandler(view.get_uiPhoto(),'click',Function.createDelegate(this,this.$1D));$addHandler(view.get_uiPrevPhotoButton(),'click',Function.createDelegate(this,this.$1E));$addHandler(view.get_uiNextPhotoButton(),'click',Function.createDelegate(this,this.$1D));if(view.get_uiUsrSpottedToggleButton()!=null){$addHandler(view.get_uiUsrSpottedToggleButton(),'click',Function.createDelegate(this,this.$25));}if(Boolean.parse(view.get_uiUsrIsLoggedIn().value)){$addHandler(view.get_uiIsFavouritePhotoToggleButton(),'click',Function.createDelegate(this,this.$27));}this.$8=Boolean.parse(view.get_uiDisableBanner().value);this.$9=Boolean.parse(view.get_uiOverlayEnabled().value);this.$B=Number.parseInvariant(view.get_uiOverlayWidth().value);this.$A=Number.parseInvariant(view.get_uiOverlayHeight().value);if(view.get_uiBuddySpottedButton()!=null){$addHandler(view.get_uiBuddySpottedButton(),'click',Function.createDelegate(this,this.$23));}if(view.get_uiUseAsProfilePictureButton()!=null){view.get_uiUseAsProfilePictureButton().setAttribute('onclick','');$addHandler(view.get_uiUseAsProfilePictureButton(),'click',Function.createDelegate(this,this.$20));}if(view.get_uiAddToCompetitionGroup()!=null){$addHandler(view.get_uiAddToCompetitionGroup(),'click',Function.createDelegate(this,this.$E));}}
SpottedScript.Controls.PhotoControl.Controller.prototype={$5:null,$6:null,$7:0,$8:false,$9:false,$A:0,$B:0,get_currentPhoto:function(){return this.$6[this.$7];},$C:false,$D:null,$E:function($p0){$p0.preventDefault();Spotted.WebServices.Controls.PhotoControl.Service.setAsCompetitionGroupPhoto(this.get_currentPhoto().k,!this.get_currentPhoto().isInCompetitionGroup,Function.createDelegate(this,this.$F),null,!this.get_currentPhoto().isInCompetitionGroup,-1);},$F:function($p0,$p1,$p2){this.get_currentPhoto().isInCompetitionGroup=$p1;this.$10();},$10:function(){if(this.get_currentPhoto().canEnterCompetition){this.$5.get_uiAddToCompetitionGroup().style.display='';this.$5.get_uiAddToCompetitionGroupImg().src=(this.get_currentPhoto().isInCompetitionGroup)?this.$5.get_uiAddToCompetitionGroupImgRemoveButtonUrl().value:this.$5.get_uiAddToCompetitionGroupImgAddButtonUrl().value;}else{this.$5.get_uiAddToCompetitionGroup().style.display='none';}},isGallerySelectedChanged:function(gallerySelected){this.$5.get_uiPhotoGalleryLinkHolder().style.display=(gallerySelected)?'none':'';},$11:function(){var $0=this.get_currentPhoto();if($0.isPhoto){this.$31();this.$33();}else if($0.isVideo){this.$30();this.$32();}this.$5.get_uiPhotoGalleryLink().href=$0.url;this.$5.get_uiTakenByDetailsSpan().innerHTML=$0.takenByDetailsHtml;this.$5.get_uiCopyrightUsrLinkSpan().innerHTML=$0.usrLink;this.$2F();this.$5.get_uiPhotoVideoLabel1().innerHTML=$0.photoVideoLabel;this.$5.get_uiPhotoVideoLabel2().innerHTML=$0.photoVideoLabel;this.$5.get_uiLinkToThis().value=$0.linkToPhotoUrl;if(this.$5.get_uiAddToGroupLink()!=null){this.$5.get_uiAddToGroupLink().href=$0.linkToPhotoUrl+'/addtogroup';}this.$5.get_uiSendLink().href=$0.linkToPhotoUrl+'/send';this.$5.get_uiReportLink().href=$0.linkToPhotoUrl+'/report';this.$5.get_uiEmbedThis().value=$0.embedThisPhotoHtml;this.$5.get_uiDownloadPhotoLinkHtml().innerHTML=$0.downloadPhotoLinkHtml;this.$12();this.$2A();this.$10();if(this.$5.get_uiBuddyValidator()!=null){this.$5.get_uiBuddyValidator().style.display='none';}try{this.$5.get_uiPhotoUsage().innerHTML=$0.photoUsageAdminString;}catch($3){}Spotted.WebServices.Controls.PhotoControl.Service.incrementViews($0.k,null,null,null,-1);var $1=document.getElementById('NavAdmin_uiModerateTagsAnchor');if($1!=null){$1.href='/pages/moderatephototags/photo-'+$0.k;}var $2=document.getElementById('NavAdmin_uiAdministrateChatAnchor');if($2!=null){if($0.threadK>0){$2.href='/pages/chatadmin/k-'+$0.threadK;$2.style.display='';}else{$2.style.display='none';}}},$12:function(){if(this.$5.get_uiUsrSpottedToggleButton()!=null){this.$5.get_uiUsrSpottedToggleButton().innerHTML=(this.get_currentPhoto().usrIsInPhoto)?'I\'m not in this photo':'I\'ve been spotted!';}},$13:function($p0,$p1){if(!Boolean.parse(this.$5.get_uiFirstTimeLoading().value)){SpottedScript.Controls.Banners.Generator.Controller.refreshAllBanners();}this.$14(($p1).photoSet,($p1).selectedIndex);},$14:function($p0,$p1){this.$6=$p0;this.$16(this,new SpottedScript.IntEventArgs($p1));var $0=new Array($p0.length);for(var $1=0;$1<$0.length;$1++){$0[$1]=$p0[$1].k;}window.setTimeout(Function.createDelegate(this,this.$1F),1000);},$15:null,$16:function($p0,$p1){this.$7=($p1).value;if(Boolean.parse(this.$5.get_uiFirstTimeLoading().value)&&!this.$6[this.$7].isVideo){this.$5.get_uiFirstTimeLoading().value=false.toString();this.$1C();}else{this.$5.get_uiFirstTimeLoading().value=false.toString();this.$11();this.$37();this.$1A();if(this.$15!=null){this.$15(this,new SpottedScript.Controls.PhotoBrowser.PhotoEventArgs(this.get_currentPhoto()));}}this.$5.get_uiPhoto().focus();},$17:null,$18:0,$19:500,$1A:function(){this.$18++;window.setTimeout(Function.createDelegate(this,this.$1B),this.$19);},$1B:function(){this.$18--;if(this.$18<=0){this.$1C();}},$1C:function(){if(this.$17!=null){this.$17(this,new SpottedScript.Controls.PhotoBrowser.PhotoEventArgs(this.get_currentPhoto()));}},$1D:function($p0){$p0.preventDefault();if(this.$0!=null){this.$0(this,Sys.EventArgs.Empty);}this.$5.get_uiPhoto().focus();},$1E:function($p0){$p0.preventDefault();if(this.$1!=null){this.$1(this,Sys.EventArgs.Empty);}},$1F:function(){for(var $0=0;$0<this.$6.length;$0++){if(this.$6[$0]!=null){var $1=document.createElement('img');$1.src=this.$6[$0].webPath;}}},$20:function($p0){$p0.preventDefault();WhenLoggedIn(Function.createDelegate(this,function(){
this.$21();}));},$21:function(){if(!this.get_currentPhoto().usrIsInPhoto){if(confirm('Are you sure you are in this photo?')){this.$2B(-1,this.get_currentPhoto().k,true);this.$22();}}else{this.$22();}},$22:function(){eval('window.location = \'/pages/mypicture/type-pic/k-'+this.get_currentPhoto().k+'\'');},$23:function($p0){$p0.preventDefault();WhenLoggedIn(Function.createDelegate(this,function(){
this.$24();}));},$24:function(){var $0=0;try{$0=Number.parseInvariant(this.$5.get_uiBuddyChooser().get_$2());}catch($1){}if($0>0){this.$5.get_uiBuddyValidator().style.display='none';this.$5.get_uiBuddyChooser().set_$3('');this.$5.get_uiBuddyChooser().set_$2('');this.$2B($0,this.get_currentPhoto().k,true);}else{this.$5.get_uiBuddyValidator().style.display='';}},$25:function($p0){$p0.preventDefault();WhenLoggedIn(Function.createDelegate(this,function(){
this.$26();}));},$26:function(){this.$2B(-1,this.get_currentPhoto().k,!this.get_currentPhoto().usrIsInPhoto);},$27:function($p0){$p0.preventDefault();WhenLoggedIn(Function.createDelegate(this,function(){
this.$28();}));},$28:function(){Spotted.WebServices.Controls.PhotoControl.Service.setIsFavouritePhoto(this.get_currentPhoto().k,!this.get_currentPhoto().isFavourite,Function.createDelegate(this,this.$29),Function.createDelegate(null,Utils.Trace.webServiceFailure),!this.get_currentPhoto().isFavourite,-1);},$29:function($p0,$p1,$p2){this.get_currentPhoto().isFavourite=$p1;this.$2A();},$2A:function(){if(this.$5.get_uiIsFavouritePhotoToggleButton()!=null){this.$5.get_uiIsFavouritePhotoToggleButton().innerHTML=(this.get_currentPhoto().isFavourite)?'Remove from my favourites':'Add to my favourites';}},$2B:function($p0,$p1,$p2){if($p0>0){Spotted.WebServices.Controls.PhotoControl.Service.setUsrSpottedInPhoto($p0,$p1,$p2,Function.createDelegate(this,this.$2C),Function.createDelegate(null,Utils.Trace.webServiceFailure),false,-1);}else{Spotted.WebServices.Controls.PhotoControl.Service.setCurrentUsrSpottedInPhoto($p1,$p2,Function.createDelegate(this,this.$2D),Function.createDelegate(null,Utils.Trace.webServiceFailure),$p2,-1);}},$2C:function($p0,$p1,$p2){this.$2E($p0[0],$p0[1]);},$2D:function($p0,$p1,$p2){this.get_currentPhoto().usrIsInPhoto=$p1;this.$12();this.$2E($p0[0],$p0[1]);},$2E:function($p0,$p1){this.get_currentPhoto().usrsInPhotoHtml=$p0;this.get_currentPhoto().rolloverMouseOverText=$p1;this.$2F();if(this.$D!=null){this.$D(this,new SpottedScript.IntEventArgs(this.$7));}},$2F:function(){this.$5.get_uiUsrsInPhotoSpan().style.display=(this.get_currentPhoto().usrsInPhotoHtml.length>0)?'':'none';this.$5.get_uiUsrsInPhotoSpan().innerHTML=this.get_currentPhoto().usrsInPhotoHtml;},$30:function(){this.$5.get_uiFlashHolder().style.display='';var $0=this.get_currentPhoto();eval(String.format('var PhotoBrowser_so = new SWFObject(\'/misc/flvplayer.swf\', \'PhotoBrowser_mymovie\', {0}, {1}, 7, \'#FFFFFF\');\r\nPhotoBrowser_so.addParam(\'wmode\', \'transparent\');\r\nPhotoBrowser_so.addVariable(\'file\', \'{2}\');\r\nPhotoBrowser_so.addVariable(\'jpg\', \'{3}\');\r\nPhotoBrowser_so.addVariable(\'autoStart\', \'0\');\r\nPhotoBrowser_so.write(\'{4}\');',$0.videoMedWidth,$0.videoMedHeight+20,$0.videoMedPath,$0.webPath,this.$5.get_uiFlashHolder().id));},$31:function(){this.$5.get_uiFlashHolder().style.display='none';},$32:function(){this.$5.get_uiPhotoHolder().style.display='none';},$33:function(){this.$5.get_uiPhoto().src='/gfx/1pix-black.gif';window.setTimeout(Function.createDelegate(this,this.$34),0);},$34:function(){var $0=this.get_currentPhoto();this.$5.get_uiPhoto().src=$0.webPath;this.$5.get_uiPhoto().style.height=$0.height+'px';this.$5.get_uiPhoto().style.width=$0.width+'px';this.$5.get_uiPhoto().style.display='';this.$5.get_uiPhotoHolder().style.display='';if(this.$9){if($0.width<500&&$0.height<500){this.$5.get_uiPhotoOverlay().style.left=((600-$0.width)/2).toString()+'px';}else{this.$5.get_uiPhotoOverlay().style.right=((600-$0.width)/2).toString()+'px';}}},$35:null,$36:0,$37:function(){if(!this.$8){if(this.$35==null){this.$39();}else{this.$36++;if(this.$36>=this.$35.length){this.$36=0;}this.$38();}}},$38:function(){if(this.$35[this.$36]==null){this.$5.get_uiBannerHolder().style.display='none';return;}this.$5.get_uiBannerHolder().style.display='';if(this.$35[this.$36].html.length>0){this.$5.get_uiBannerPlaceHolder().innerHTML=this.$35[this.$36].html;}else{this.$5.get_uiBannerPlaceHolder().innerHTML='';eval(this.$35[this.$36].script);}Spotted.WebServices.Controls.PhotoControl.Service.registerBannerHit(this.$35[this.$36].k,null,null,null,-1);},$39:function(){Spotted.WebServices.Controls.PhotoControl.Service.getBanners(this.$5.get_uiBannerPlaceHolder().id,Function.createDelegate(this,this.$3A),Function.createDelegate(null,Utils.Trace.webServiceFailure),null,-1);},$3A:function($p0,$p1,$p2){this.$35=$p0;this.$36=0;this.$38();}}
SpottedScript.Controls.PhotoControl.View=function(clientId){this.clientId=clientId;}
SpottedScript.Controls.PhotoControl.View.prototype={clientId:null,get_uiContent:function(){return document.getElementById(this.clientId+'_uiContent');},get_uiPrevPhotoButtonDiv:function(){return document.getElementById(this.clientId+'_uiPrevPhotoButtonDiv');},get_uiPrevPhotoButton:function(){return document.getElementById(this.clientId+'_uiPrevPhotoButton');},get_uiNextPhotoButtonDiv:function(){return document.getElementById(this.clientId+'_uiNextPhotoButtonDiv');},get_uiNextPhotoButton:function(){return document.getElementById(this.clientId+'_uiNextPhotoButton');},get_uiBannerHolder:function(){return document.getElementById(this.clientId+'_uiBannerHolder');},get_uiBannerPlaceHolder:function(){return document.getElementById(this.clientId+'_uiBannerPlaceHolder');},get_bannerPhoto:function(){return eval(this.clientId+'_BannerPhotoController');},get_uiPhotoDiv:function(){return document.getElementById(this.clientId+'_uiPhotoDiv');},get_uiPhotoHolder:function(){return document.getElementById(this.clientId+'_uiPhotoHolder');},get_uiPhoto:function(){return document.getElementById(this.clientId+'_uiPhoto');},get_uiPhotoOverlay:function(){return document.getElementById(this.clientId+'_uiPhotoOverlay');},get_uiFlashHolder:function(){return document.getElementById(this.clientId+'_uiFlashHolder');},get_uiPhotoGalleryLinkHolder:function(){return document.getElementById(this.clientId+'_uiPhotoGalleryLinkHolder');},get_uiPhotoGalleryLink:function(){return document.getElementById(this.clientId+'_uiPhotoGalleryLink');},get_uiTakenByDetailsSpan:function(){return document.getElementById(this.clientId+'_uiTakenByDetailsSpan');},get_uiUsrsInPhotoSpan:function(){return document.getElementById(this.clientId+'_uiUsrsInPhotoSpan');},get_uiBuddyChooserPanel:function(){return document.getElementById(this.clientId+'_uiBuddyChooserPanel');},get_uiUsrSpottedToggleButton:function(){return document.getElementById(this.clientId+'_uiUsrSpottedToggleButton');},get_uiUseAsProfilePictureButton:function(){return document.getElementById(this.clientId+'_uiUseAsProfilePictureButton');},get_uiBuddyChooserPanelInner:function(){return document.getElementById(this.clientId+'_uiBuddyChooserPanelInner');},get_uiBuddyChooser:function(){return eval(this.clientId+'_uiBuddyChooserController');},get_uiBuddySpottedButton:function(){return document.getElementById(this.clientId+'_uiBuddySpottedButton');},get_uiBuddyValidator:function(){return document.getElementById(this.clientId+'_uiBuddyValidator');},get_uiCompetitionPanel:function(){return document.getElementById(this.clientId+'_uiCompetitionPanel');},get_uiAddToCompetitionGroup:function(){return document.getElementById(this.clientId+'_uiAddToCompetitionGroup');},get_uiAddToCompetitionGroupImg:function(){return document.getElementById(this.clientId+'_uiAddToCompetitionGroupImg');},get_uiCompetitionGroupLink:function(){return document.getElementById(this.clientId+'_uiCompetitionGroupLink');},get_uiAddToCompetitionGroupImgAddButtonUrl:function(){return document.getElementById(this.clientId+'_uiAddToCompetitionGroupImgAddButtonUrl');},get_uiAddToCompetitionGroupImgRemoveButtonUrl:function(){return document.getElementById(this.clientId+'_uiAddToCompetitionGroupImgRemoveButtonUrl');},get_uiQuickBrowserUrl:function(){return document.getElementById(this.clientId+'_uiQuickBrowserUrl');},get_uiIsFavouritePhotoToggleButton:function(){return document.getElementById(this.clientId+'_uiIsFavouritePhotoToggleButton');},get_uiSendLink:function(){return document.getElementById(this.clientId+'_uiSendLink');},get_uiReportLink:function(){return document.getElementById(this.clientId+'_uiReportLink');},get_uiAddToGroupTopPhotosSpan:function(){return document.getElementById(this.clientId+'_uiAddToGroupTopPhotosSpan');},get_uiAddToGroupLink:function(){return document.getElementById(this.clientId+'_uiAddToGroupLink');},get_uiAddToFrontPageSpan:function(){return document.getElementById(this.clientId+'_uiAddToFrontPageSpan');},get_uiDownloadPhotoLinkHtml:function(){return document.getElementById(this.clientId+'_uiDownloadPhotoLinkHtml');},get_uiCopyrightUsrLinkSpan:function(){return document.getElementById(this.clientId+'_uiCopyrightUsrLinkSpan');},get_uiPhotoVideoLabel1:function(){return document.getElementById(this.clientId+'_uiPhotoVideoLabel1');},get_uiLinkToThis:function(){return document.getElementById(this.clientId+'_uiLinkToThis');},get_uiPhotoVideoLabel2:function(){return document.getElementById(this.clientId+'_uiPhotoVideoLabel2');},get_uiEmbedThis:function(){return document.getElementById(this.clientId+'_uiEmbedThis');},get_photoOfWeekDiv:function(){return document.getElementById(this.clientId+'_PhotoOfWeekDiv');},get_div1:function(){return document.getElementById(this.clientId+'_Div1');},get_uiPhotoUsage:function(){return document.getElementById(this.clientId+'_uiPhotoUsage');},get_uiDisplayMakeFrontPageOptions:function(){return document.getElementById(this.clientId+'_uiDisplayMakeFrontPageOptions');},get_uiUsrIsLoggedIn:function(){return document.getElementById(this.clientId+'_uiUsrIsLoggedIn');},get_uiDisableBanner:function(){return document.getElementById(this.clientId+'_uiDisableBanner');},get_uiFirstTimeLoading:function(){return document.getElementById(this.clientId+'_uiFirstTimeLoading');},get_uiOverlayEnabled:function(){return document.getElementById(this.clientId+'_uiOverlayEnabled');},get_uiOverlayWidth:function(){return document.getElementById(this.clientId+'_uiOverlayWidth');},get_uiOverlayHeight:function(){return document.getElementById(this.clientId+'_uiOverlayHeight');}}
SpottedScript.Controls.PhotoControl.BannerStub.registerClass('SpottedScript.Controls.PhotoControl.BannerStub');
SpottedScript.Controls.PhotoControl.PhotoStub.registerClass('SpottedScript.Controls.PhotoControl.PhotoStub');
SpottedScript.Controls.PhotoControl.PhotoResult.registerClass('SpottedScript.Controls.PhotoControl.PhotoResult');
SpottedScript.Controls.PhotoControl.Controller.registerClass('SpottedScript.Controls.PhotoControl.Controller',SpottedScript.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl);
SpottedScript.Controls.PhotoControl.View.registerClass('SpottedScript.Controls.PhotoControl.View');
