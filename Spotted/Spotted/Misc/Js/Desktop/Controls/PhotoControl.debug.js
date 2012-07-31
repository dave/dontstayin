//! PhotoControl.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.PhotoControl');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoControl.BannerStub

Js.Controls.PhotoControl.BannerStub = function Js_Controls_PhotoControl_BannerStub() {
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="html" type="String">
    /// </field>
    /// <field name="script" type="String">
    /// </field>
}
Js.Controls.PhotoControl.BannerStub.prototype = {
    k: 0,
    html: null,
    script: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoControl.Controller

Js.Controls.PhotoControl.Controller = function Js_Controls_PhotoControl_Controller(view) {
    /// <param name="view" type="Js.Controls.PhotoControl.View">
    /// </param>
    /// <field name="_view$1" type="Js.Controls.PhotoControl.View">
    /// </field>
    /// <field name="_photoSet$1" type="Array" elementType="PhotoStub">
    /// </field>
    /// <field name="_selectedIndex$1" type="Number" integer="true">
    /// </field>
    /// <field name="_disableBanner$1" type="Boolean">
    /// </field>
    /// <field name="_overlayEnabled$1" type="Boolean">
    /// </field>
    /// <field name="_overlayHeight$1" type="Number" integer="true">
    /// </field>
    /// <field name="_overlayWidth$1" type="Number" integer="true">
    /// </field>
    /// <field name="_displayMakeFrontPageOptions$1" type="Boolean">
    /// </field>
    /// <field name="onRolloverMouseOverTextChanged" type="Function">
    /// </field>
    /// <field name="onPhotoChanged" type="Function">
    /// </field>
    /// <field name="onPhotoChangedAfterDelay" type="Function">
    /// </field>
    /// <field name="_delayCount$1" type="Number" integer="true">
    /// </field>
    /// <field name="_delayMilliseconds$1" type="Number" integer="true">
    /// </field>
    /// <field name="_banners$1" type="Array" elementType="BannerStub">
    /// </field>
    /// <field name="_currentBannerIndex$1" type="Number" integer="true">
    /// </field>
    Js.Controls.PhotoControl.Controller.initializeBase(this, [ [ view.get_uiPhotoJ(), view.get_uiFlashHolderJ() ] ]);
    this._view$1 = view;
    this._displayMakeFrontPageOptions$1 = Boolean.parse(view.get_uiDisplayMakeFrontPageOptions().value);
    view.get_uiPhotoJ().click(ss.Delegate.create(this, this._nextPhotoClick$1));
    view.get_uiPrevPhotoButtonJ().click(ss.Delegate.create(this, this._prevPhotoClick$1));
    view.get_uiNextPhotoButtonJ().click(ss.Delegate.create(this, this._nextPhotoClick$1));
    if (view.get_uiUsrSpottedToggleButton() != null) {
        view.get_uiUsrSpottedToggleButtonJ().click(ss.Delegate.create(this, this._usrSpottedToggleButtonClick$1));
    }
    if (Boolean.parse(view.get_uiUsrIsLoggedIn().value)) {
        view.get_uiIsFavouritePhotoToggleButtonJ().click(ss.Delegate.create(this, this._isFavouritePhotoToggleButtonClick$1));
    }
    this._disableBanner$1 = Boolean.parse(view.get_uiDisableBanner().value);
    this._overlayEnabled$1 = Boolean.parse(view.get_uiOverlayEnabled().value);
    this._overlayWidth$1 = parseInt(view.get_uiOverlayWidth().value);
    this._overlayHeight$1 = parseInt(view.get_uiOverlayHeight().value);
    if (view.get_uiBuddySpottedButton() != null) {
        view.get_uiBuddySpottedButtonJ().click(ss.Delegate.create(this, this._buddySpottedButtonClick$1));
    }
    if (view.get_uiUseAsProfilePictureButton() != null) {
        view.get_uiUseAsProfilePictureButton().setAttribute('onclick', '');
        view.get_uiUseAsProfilePictureButtonJ().click(ss.Delegate.create(this, this._useAsProfilePictureButtonClick$1));
    }
    if (view.get_uiAddToCompetitionGroup() != null) {
        view.get_uiAddToCompetitionGroupJ().click(ss.Delegate.create(this, this._addToCompetitionGroupPhoto$1));
    }
}
Js.Controls.PhotoControl.Controller.prototype = {
    _view$1: null,
    _photoSet$1: null,
    _selectedIndex$1: 0,
    _disableBanner$1: false,
    _overlayEnabled$1: false,
    _overlayHeight$1: 0,
    _overlayWidth$1: 0,
    
    get_currentPhoto: function Js_Controls_PhotoControl_Controller$get_currentPhoto() {
        /// <value type="Js.Controls.PhotoControl.PhotoStub"></value>
        return this._photoSet$1[this._selectedIndex$1];
    },
    
    _displayMakeFrontPageOptions$1: false,
    onRolloverMouseOverTextChanged: null,
    
    _addToCompetitionGroupPhoto$1: function Js_Controls_PhotoControl_Controller$_addToCompetitionGroupPhoto$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        Js.Controls.PhotoControl.Service.setAsCompetitionGroupPhoto(this.get_currentPhoto().k, !this.get_currentPhoto().isInCompetitionGroup, ss.Delegate.create(this, this._addToCompetitionGroupPhotoSuccess$1), null, !this.get_currentPhoto().isInCompetitionGroup, -1);
    },
    
    _addToCompetitionGroupPhotoSuccess$1: function Js_Controls_PhotoControl_Controller$_addToCompetitionGroupPhotoSuccess$1(nullObject, isInCompetitionGroup, methodName) {
        /// <param name="nullObject" type="Object">
        /// </param>
        /// <param name="isInCompetitionGroup" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.get_currentPhoto().isInCompetitionGroup = isInCompetitionGroup;
        this._setAddToCompetitionGroupImgSrc$1();
    },
    
    _setAddToCompetitionGroupImgSrc$1: function Js_Controls_PhotoControl_Controller$_setAddToCompetitionGroupImgSrc$1() {
        if (this.get_currentPhoto().canEnterCompetition) {
            this._view$1.get_uiAddToCompetitionGroup().style.display = '';
            this._view$1.get_uiAddToCompetitionGroupImg().src = (this.get_currentPhoto().isInCompetitionGroup) ? this._view$1.get_uiAddToCompetitionGroupImgRemoveButtonUrl().value : this._view$1.get_uiAddToCompetitionGroupImgAddButtonUrl().value;
        }
        else {
            this._view$1.get_uiAddToCompetitionGroup().style.display = 'none';
        }
    },
    
    isGallerySelectedChanged: function Js_Controls_PhotoControl_Controller$isGallerySelectedChanged(gallerySelected) {
        /// <param name="gallerySelected" type="Boolean">
        /// </param>
        this._view$1.get_uiPhotoGalleryLinkHolder().style.display = (gallerySelected) ? 'none' : '';
    },
    
    _setDetails$1: function Js_Controls_PhotoControl_Controller$_setDetails$1() {
        var photo = this.get_currentPhoto();
        if (photo.isPhoto) {
            this._hideVideo$1();
            this._showPhoto$1();
        }
        else if (photo.isVideo) {
            this._showVideo$1();
            this._hidePhoto$1();
        }
        this._view$1.get_uiPhotoGalleryLink().href = photo.url;
        this._view$1.get_uiTakenByDetailsSpan().innerHTML = photo.takenByDetailsHtml;
        this._view$1.get_uiCopyrightUsrLinkSpan().innerHTML = photo.usrLink;
        this._setUsrsInPhoto$1();
        this._view$1.get_uiPhotoVideoLabel1().innerHTML = photo.photoVideoLabel;
        this._view$1.get_uiPhotoVideoLabel2().innerHTML = photo.photoVideoLabel;
        this._view$1.get_uiLinkToThis().value = photo.linkToPhotoUrl;
        if (this._view$1.get_uiAddToGroupLink() != null) {
            this._view$1.get_uiAddToGroupLink().href = photo.linkToPhotoUrl + '/addtogroup';
        }
        this._view$1.get_uiSendLink().href = photo.linkToPhotoUrl + '/send';
        this._view$1.get_uiReportLink().href = photo.linkToPhotoUrl + '/report';
        this._view$1.get_uiEmbedThis().value = photo.embedThisPhotoHtml;
        this._view$1.get_uiDownloadPhotoLinkHtml().innerHTML = photo.downloadPhotoLinkHtml;
        this._setSpottedToggleButtonText$1();
        this._setIsFavouritePhotoToggleButtonText$1();
        this._setAddToCompetitionGroupImgSrc$1();
        if (this._view$1.get_uiBuddyValidator() != null) {
            this._view$1.get_uiBuddyValidator().style.display = 'none';
        }
        try {
            this._view$1.get_uiPhotoUsage().innerHTML = photo.photoUsageAdminString;
        }
        catch ($e1) {
        }
        Js.Controls.PhotoControl.Service.incrementViews(photo.k, null, null, null, -1);
        var uiModerateThisPhotosTagsLink = document.getElementById('NavAdmin_uiModerateTagsAnchor');
        if (uiModerateThisPhotosTagsLink != null) {
            uiModerateThisPhotosTagsLink.href = '/pages/moderatephototags/photo-' + photo.k;
        }
        var uiAdministrateChatAnchor = document.getElementById('NavAdmin_uiAdministrateChatAnchor');
        if (uiAdministrateChatAnchor != null) {
            if (photo.threadK > 0) {
                uiAdministrateChatAnchor.href = '/pages/chatadmin/k-' + photo.threadK;
                uiAdministrateChatAnchor.style.display = '';
            }
            else {
                uiAdministrateChatAnchor.style.display = 'none';
            }
        }
    },
    
    _setSpottedToggleButtonText$1: function Js_Controls_PhotoControl_Controller$_setSpottedToggleButtonText$1() {
        if (this._view$1.get_uiUsrSpottedToggleButton() != null) {
            this._view$1.get_uiUsrSpottedToggleButton().innerHTML = (this.get_currentPhoto().usrIsInPhoto) ? "I'm not in this photo" : "I've been spotted!";
        }
    },
    
    photoSetChanged: function Js_Controls_PhotoControl_Controller$photoSetChanged(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (!Boolean.parse(this._view$1.get_uiFirstTimeLoading().value)) {
            Js.Controls.Banners.Generator.Controller.refreshAllBanners();
        }
        this._setPhotoSet$1((e).photoSet, (e).selectedIndex);
    },
    
    _setPhotoSet$1: function Js_Controls_PhotoControl_Controller$_setPhotoSet$1(photoSet, selectedIndex) {
        /// <param name="photoSet" type="Array" elementType="PhotoStub">
        /// </param>
        /// <param name="selectedIndex" type="Number" integer="true">
        /// </param>
        this._photoSet$1 = photoSet;
        this.photoChanged(this, new Js.Library.IntEventArgs(selectedIndex));
        var photoKs = new Array(photoSet.length);
        for (var i = 0; i < photoKs.length; i++) {
            photoKs[i] = photoSet[i].k;
        }
        window.setTimeout(ss.Delegate.create(this, this._preloadAllImages$1), 1000);
    },
    
    onPhotoChanged: null,
    
    photoChanged: function Js_Controls_PhotoControl_Controller$photoChanged(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._selectedIndex$1 = (e).value;
        if (Boolean.parse(this._view$1.get_uiFirstTimeLoading().value) && !this._photoSet$1[this._selectedIndex$1].isVideo) {
            this._view$1.get_uiFirstTimeLoading().value = false.toString();
            this._doDelayEvents$1();
        }
        else {
            this._view$1.get_uiFirstTimeLoading().value = false.toString();
            this._setDetails$1();
            this._showNewBanner$1();
            this._triggerDelayEvents$1();
            if (this.onPhotoChanged != null) {
                this.onPhotoChanged(this, new Js.Controls.PhotoBrowser.PhotoEventArgs(this.get_currentPhoto()));
            }
        }
        this._view$1.get_uiPhoto().focus();
    },
    
    onPhotoChangedAfterDelay: null,
    _delayCount$1: 0,
    _delayMilliseconds$1: 500,
    
    _triggerDelayEvents$1: function Js_Controls_PhotoControl_Controller$_triggerDelayEvents$1() {
        this._delayCount$1++;
        window.setTimeout(ss.Delegate.create(this, this._testDelayCount$1), this._delayMilliseconds$1);
    },
    
    _testDelayCount$1: function Js_Controls_PhotoControl_Controller$_testDelayCount$1() {
        this._delayCount$1--;
        if (this._delayCount$1 <= 0) {
            this._doDelayEvents$1();
        }
    },
    
    _doDelayEvents$1: function Js_Controls_PhotoControl_Controller$_doDelayEvents$1() {
        if (this.onPhotoChangedAfterDelay != null) {
            this.onPhotoChangedAfterDelay(this, new Js.Controls.PhotoBrowser.PhotoEventArgs(this.get_currentPhoto()));
        }
    },
    
    _nextPhotoClick$1: function Js_Controls_PhotoControl_Controller$_nextPhotoClick$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this.onPhotoNextClick != null) {
            this.onPhotoNextClick(this, ss.EventArgs.Empty);
        }
        this._view$1.get_uiPhoto().focus();
    },
    
    _prevPhotoClick$1: function Js_Controls_PhotoControl_Controller$_prevPhotoClick$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this.onPhotoPrevClick != null) {
            this.onPhotoPrevClick(this, ss.EventArgs.Empty);
        }
    },
    
    _preloadAllImages$1: function Js_Controls_PhotoControl_Controller$_preloadAllImages$1() {
        for (var i = 0; i < this._photoSet$1.length; i++) {
            if (this._photoSet$1[i] != null) {
                var img = document.createElement('img');
                img.src = this._photoSet$1[i].webPath;
            }
        }
    },
    
    _useAsProfilePictureButtonClick$1: function Js_Controls_PhotoControl_Controller$_useAsProfilePictureButtonClick$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(ss.Delegate.create(this, function() {
            this._useAsProfilePictureButtonClickInner$1();
        }));
    },
    
    _useAsProfilePictureButtonClickInner$1: function Js_Controls_PhotoControl_Controller$_useAsProfilePictureButtonClickInner$1() {
        if (!this.get_currentPhoto().usrIsInPhoto) {
            if (confirm('Are you sure you are in this photo?')) {
                this._setUsrSpottedInPhoto$1(-1, this.get_currentPhoto().k, true);
                this._redirectToProfilePhotoPage$1();
            }
        }
        else {
            this._redirectToProfilePhotoPage$1();
        }
    },
    
    _redirectToProfilePhotoPage$1: function Js_Controls_PhotoControl_Controller$_redirectToProfilePhotoPage$1() {
        eval("window.location = '/pages/mypicture/type-pic/k-" + this.get_currentPhoto().k + "'");
    },
    
    _buddySpottedButtonClick$1: function Js_Controls_PhotoControl_Controller$_buddySpottedButtonClick$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(ss.Delegate.create(this, function() {
            this._buddySpottedButtonClickInner$1();
        }));
    },
    
    _buddySpottedButtonClickInner$1: function Js_Controls_PhotoControl_Controller$_buddySpottedButtonClickInner$1() {
        var usrK = 0;
        try {
            usrK = parseInt(this._view$1.get_uiBuddyChooser().get_value());
        }
        catch ($e1) {
        }
        if (usrK > 0) {
            this._view$1.get_uiBuddyValidator().style.display = 'none';
            this._view$1.get_uiBuddyChooser().set_text('');
            this._view$1.get_uiBuddyChooser().set_value('');
            this._setUsrSpottedInPhoto$1(usrK, this.get_currentPhoto().k, true);
        }
        else {
            this._view$1.get_uiBuddyValidator().style.display = '';
        }
    },
    
    _usrSpottedToggleButtonClick$1: function Js_Controls_PhotoControl_Controller$_usrSpottedToggleButtonClick$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(ss.Delegate.create(this, function() {
            this._usrSpottedToggleButtonClickInner$1();
        }));
    },
    
    _usrSpottedToggleButtonClickInner$1: function Js_Controls_PhotoControl_Controller$_usrSpottedToggleButtonClickInner$1() {
        this._setUsrSpottedInPhoto$1(-1, this.get_currentPhoto().k, !this.get_currentPhoto().usrIsInPhoto);
    },
    
    _isFavouritePhotoToggleButtonClick$1: function Js_Controls_PhotoControl_Controller$_isFavouritePhotoToggleButtonClick$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(ss.Delegate.create(this, function() {
            this._isFavouritePhotoToggleButtonClickInner$1();
        }));
    },
    
    _isFavouritePhotoToggleButtonClickInner$1: function Js_Controls_PhotoControl_Controller$_isFavouritePhotoToggleButtonClickInner$1() {
        Js.Controls.PhotoControl.Service.setIsFavouritePhoto(this.get_currentPhoto().k, !this.get_currentPhoto().isFavourite, ss.Delegate.create(this, this._isFavouritePhotoToggleButtonClickSuccessCallback$1), Js.Library.Trace.webServiceFailure, !this.get_currentPhoto().isFavourite, -1);
    },
    
    _isFavouritePhotoToggleButtonClickSuccessCallback$1: function Js_Controls_PhotoControl_Controller$_isFavouritePhotoToggleButtonClickSuccessCallback$1(nullObject, isFavourite, methodName) {
        /// <param name="nullObject" type="Object">
        /// </param>
        /// <param name="isFavourite" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.get_currentPhoto().isFavourite = isFavourite;
        this._setIsFavouritePhotoToggleButtonText$1();
    },
    
    _setIsFavouritePhotoToggleButtonText$1: function Js_Controls_PhotoControl_Controller$_setIsFavouritePhotoToggleButtonText$1() {
        if (this._view$1.get_uiIsFavouritePhotoToggleButton() != null) {
            this._view$1.get_uiIsFavouritePhotoToggleButton().innerHTML = (this.get_currentPhoto().isFavourite) ? 'Remove from my favourites' : 'Add to my favourites';
        }
    },
    
    _setUsrSpottedInPhoto$1: function Js_Controls_PhotoControl_Controller$_setUsrSpottedInPhoto$1(spottedUsrK, photoK, isInPhoto) {
        /// <param name="spottedUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="photoK" type="Number" integer="true">
        /// </param>
        /// <param name="isInPhoto" type="Boolean">
        /// </param>
        if (spottedUsrK > 0) {
            Js.Controls.PhotoControl.Service.setUsrSpottedInPhoto(spottedUsrK, photoK, isInPhoto, ss.Delegate.create(this, this._setUsrSpottedInPhotoSuccessCallback$1), Js.Library.Trace.webServiceFailure, false, -1);
        }
        else {
            Js.Controls.PhotoControl.Service.setCurrentUsrSpottedInPhoto(photoK, isInPhoto, ss.Delegate.create(this, this._setCurrentUsrSpottedInPhotoSuccessCallback$1), Js.Library.Trace.webServiceFailure, isInPhoto, -1);
        }
    },
    
    _setUsrSpottedInPhotoSuccessCallback$1: function Js_Controls_PhotoControl_Controller$_setUsrSpottedInPhotoSuccessCallback$1(newTexts, userContext, methodName) {
        /// <param name="newTexts" type="Array" elementType="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._setUsrInPhotoDetails$1(newTexts[0], newTexts[1]);
    },
    
    _setCurrentUsrSpottedInPhotoSuccessCallback$1: function Js_Controls_PhotoControl_Controller$_setCurrentUsrSpottedInPhotoSuccessCallback$1(newTexts, usrIsInPhoto, methodName) {
        /// <param name="newTexts" type="Array" elementType="String">
        /// </param>
        /// <param name="usrIsInPhoto" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.get_currentPhoto().usrIsInPhoto = usrIsInPhoto;
        this._setSpottedToggleButtonText$1();
        this._setUsrInPhotoDetails$1(newTexts[0], newTexts[1]);
    },
    
    _setUsrInPhotoDetails$1: function Js_Controls_PhotoControl_Controller$_setUsrInPhotoDetails$1(usrsInPhotoHtml, rolloverMouseOverText) {
        /// <param name="usrsInPhotoHtml" type="String">
        /// </param>
        /// <param name="rolloverMouseOverText" type="String">
        /// </param>
        this.get_currentPhoto().usrsInPhotoHtml = usrsInPhotoHtml;
        this.get_currentPhoto().rolloverMouseOverText = rolloverMouseOverText;
        this._setUsrsInPhoto$1();
        if (this.onRolloverMouseOverTextChanged != null) {
            this.onRolloverMouseOverTextChanged(this, new Js.Library.IntEventArgs(this._selectedIndex$1));
        }
    },
    
    _setUsrsInPhoto$1: function Js_Controls_PhotoControl_Controller$_setUsrsInPhoto$1() {
        this._view$1.get_uiUsrsInPhotoSpan().style.display = (this.get_currentPhoto().usrsInPhotoHtml.length > 0) ? '' : 'none';
        this._view$1.get_uiUsrsInPhotoSpan().innerHTML = this.get_currentPhoto().usrsInPhotoHtml;
    },
    
    _showVideo$1: function Js_Controls_PhotoControl_Controller$_showVideo$1() {
        this._view$1.get_uiFlashHolder().style.display = '';
        var p = this.get_currentPhoto();
        eval(String.format("var PhotoBrowser_so = new SWFObject('/misc/flvplayer.swf', 'PhotoBrowser_mymovie', {0}, {1}, 7, '#FFFFFF');\r\nPhotoBrowser_so.addParam('wmode', 'transparent');\r\nPhotoBrowser_so.addVariable('file', '{2}');\r\nPhotoBrowser_so.addVariable('jpg', '{3}');\r\nPhotoBrowser_so.addVariable('autoStart', '0');\r\nPhotoBrowser_so.write('{4}');", p.videoMedWidth, p.videoMedHeight + 20, p.videoMedPath, p.webPath, this._view$1.get_uiFlashHolder().id));
    },
    
    _hideVideo$1: function Js_Controls_PhotoControl_Controller$_hideVideo$1() {
        this._view$1.get_uiFlashHolder().style.display = 'none';
    },
    
    _hidePhoto$1: function Js_Controls_PhotoControl_Controller$_hidePhoto$1() {
        this._view$1.get_uiPhotoHolder().style.display = 'none';
    },
    
    _showPhoto$1: function Js_Controls_PhotoControl_Controller$_showPhoto$1() {
        this._view$1.get_uiPhoto().src = '/gfx/1pix-black.gif';
        window.setTimeout(ss.Delegate.create(this, this._showPhoto2$1), 0);
    },
    
    _showPhoto2$1: function Js_Controls_PhotoControl_Controller$_showPhoto2$1() {
        var photo = this.get_currentPhoto();
        this._view$1.get_uiPhoto().src = photo.webPath;
        this._view$1.get_uiPhoto().style.height = photo.height + 'px';
        this._view$1.get_uiPhoto().style.width = photo.width + 'px';
        this._view$1.get_uiPhoto().style.display = '';
        this._view$1.get_uiPhotoHolder().style.display = '';
        if (this._overlayEnabled$1) {
            if (photo.width < 500 && photo.height < 500) {
                this._view$1.get_uiPhotoOverlay().style.left = ((600 - photo.width) / 2).toString() + 'px';
            }
            else {
                this._view$1.get_uiPhotoOverlay().style.right = ((600 - photo.width) / 2).toString() + 'px';
            }
        }
    },
    
    _banners$1: null,
    _currentBannerIndex$1: 0,
    
    _showNewBanner$1: function Js_Controls_PhotoControl_Controller$_showNewBanner$1() {
        if (!this._disableBanner$1) {
            if (this._banners$1 == null) {
                this._getBannersAndSetCurrentBanner$1();
            }
            else {
                this._currentBannerIndex$1++;
                if (this._currentBannerIndex$1 >= this._banners$1.length) {
                    this._currentBannerIndex$1 = 0;
                }
                this._showBanner$1();
            }
        }
    },
    
    _showBanner$1: function Js_Controls_PhotoControl_Controller$_showBanner$1() {
        if (this._banners$1[this._currentBannerIndex$1] == null) {
            this._view$1.get_uiBannerHolder().style.display = 'none';
            return;
        }
        this._view$1.get_uiBannerHolder().style.display = '';
        if (this._banners$1[this._currentBannerIndex$1].html.length > 0) {
            this._view$1.get_uiBannerPlaceHolder().innerHTML = this._banners$1[this._currentBannerIndex$1].html;
        }
        else {
            this._view$1.get_uiBannerPlaceHolder().innerHTML = '';
            eval(this._banners$1[this._currentBannerIndex$1].script);
        }
        Js.Controls.PhotoControl.Service.registerBannerHit(this._banners$1[this._currentBannerIndex$1].k, null, null, null, -1);
    },
    
    _getBannersAndSetCurrentBanner$1: function Js_Controls_PhotoControl_Controller$_getBannersAndSetCurrentBanner$1() {
        Js.Controls.PhotoControl.Service.getBanners(this._view$1.get_uiBannerPlaceHolder().id, ss.Delegate.create(this, this._getNewBannerHtmlAndRegisterHitSuccessCallback$1), Js.Library.Trace.webServiceFailure, null, -1);
    },
    
    _getNewBannerHtmlAndRegisterHitSuccessCallback$1: function Js_Controls_PhotoControl_Controller$_getNewBannerHtmlAndRegisterHitSuccessCallback$1(banners, context, methodName) {
        /// <param name="banners" type="Array" elementType="BannerStub">
        /// </param>
        /// <param name="context" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._banners$1 = banners;
        this._currentBannerIndex$1 = 0;
        this._showBanner$1();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoControl.Service

Js.Controls.PhotoControl.Service = function Js_Controls_PhotoControl_Service() {
}
Js.Controls.PhotoControl.Service.getBanners = function Js_Controls_PhotoControl_Service$getBanners(placeholderClientID, success, failure, userContext, timeout) {
    /// <param name="placeholderClientID" type="String">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['placeholderClientID'] = placeholderClientID;
    var o = Js.Library.WebServiceHelper.options('GetBanners', '/WebServices/Controls/PhotoControl/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetBanners');
    };
    $.ajax(o);
}
Js.Controls.PhotoControl.Service.registerBannerHit = function Js_Controls_PhotoControl_Service$registerBannerHit(bannerK, success, failure, userContext, timeout) {
    /// <param name="bannerK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['bannerK'] = bannerK;
    var o = Js.Library.WebServiceHelper.options('RegisterBannerHit', '/WebServices/Controls/PhotoControl/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'RegisterBannerHit');
    };
    $.ajax(o);
}
Js.Controls.PhotoControl.Service.incrementViews = function Js_Controls_PhotoControl_Service$incrementViews(photoK, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['photoK'] = photoK;
    var o = Js.Library.WebServiceHelper.options('IncrementViews', '/WebServices/Controls/PhotoControl/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'IncrementViews');
    };
    $.ajax(o);
}
Js.Controls.PhotoControl.Service.setIsFavouritePhoto = function Js_Controls_PhotoControl_Service$setIsFavouritePhoto(photoK, isFavourite, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isFavourite" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['photoK'] = photoK;
    p['isFavourite'] = isFavourite;
    var o = Js.Library.WebServiceHelper.options('SetIsFavouritePhoto', '/WebServices/Controls/PhotoControl/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'SetIsFavouritePhoto');
    };
    $.ajax(o);
}
Js.Controls.PhotoControl.Service.setCurrentUsrSpottedInPhoto = function Js_Controls_PhotoControl_Service$setCurrentUsrSpottedInPhoto(photoK, isInPhoto, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isInPhoto" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['photoK'] = photoK;
    p['isInPhoto'] = isInPhoto;
    var o = Js.Library.WebServiceHelper.options('SetCurrentUsrSpottedInPhoto', '/WebServices/Controls/PhotoControl/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'SetCurrentUsrSpottedInPhoto');
    };
    $.ajax(o);
}
Js.Controls.PhotoControl.Service.setUsrSpottedInPhoto = function Js_Controls_PhotoControl_Service$setUsrSpottedInPhoto(spottedUsrK, photoK, isInPhoto, success, failure, userContext, timeout) {
    /// <param name="spottedUsrK" type="Number" integer="true">
    /// </param>
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isInPhoto" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['spottedUsrK'] = spottedUsrK;
    p['photoK'] = photoK;
    p['isInPhoto'] = isInPhoto;
    var o = Js.Library.WebServiceHelper.options('SetUsrSpottedInPhoto', '/WebServices/Controls/PhotoControl/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'SetUsrSpottedInPhoto');
    };
    $.ajax(o);
}
Js.Controls.PhotoControl.Service.setAsCompetitionGroupPhoto = function Js_Controls_PhotoControl_Service$setAsCompetitionGroupPhoto(photoK, isCompetitionPhoto, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isCompetitionPhoto" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['photoK'] = photoK;
    p['isCompetitionPhoto'] = isCompetitionPhoto;
    var o = Js.Library.WebServiceHelper.options('SetAsCompetitionGroupPhoto', '/WebServices/Controls/PhotoControl/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'SetAsCompetitionGroupPhoto');
    };
    $.ajax(o);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoControl.View

Js.Controls.PhotoControl.View = function Js_Controls_PhotoControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiContent" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiContentJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPrevPhotoButtonDiv" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPrevPhotoButtonDivJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPrevPhotoButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPrevPhotoButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiNextPhotoButtonDiv" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiNextPhotoButtonDivJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiNextPhotoButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiNextPhotoButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBannerHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBannerHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBannerPlaceHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBannerPlaceHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoDiv" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoDivJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhoto" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoOverlay" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoOverlayJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiFlashHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiFlashHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoGalleryLinkHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoGalleryLinkHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoGalleryLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoGalleryLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiTakenByDetailsSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiTakenByDetailsSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiUsrsInPhotoSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUsrsInPhotoSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBuddyChooserPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBuddyChooserPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiUsrSpottedToggleButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUsrSpottedToggleButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiUseAsProfilePictureButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUseAsProfilePictureButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBuddyChooserPanelInner" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBuddyChooserPanelInnerJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBuddySpottedButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBuddySpottedButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBuddyValidator" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBuddyValidatorJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiCompetitionPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCompetitionPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddToCompetitionGroup" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddToCompetitionGroupJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddToCompetitionGroupImg" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddToCompetitionGroupImgJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiCompetitionGroupLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCompetitionGroupLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddToCompetitionGroupImgAddButtonUrl" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddToCompetitionGroupImgAddButtonUrlJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddToCompetitionGroupImgRemoveButtonUrl" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddToCompetitionGroupImgRemoveButtonUrlJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiQuickBrowserUrl" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiQuickBrowserUrlJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiIsFavouritePhotoToggleButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiIsFavouritePhotoToggleButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiSendLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiSendLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiReportLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiReportLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddToGroupTopPhotosSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddToGroupTopPhotosSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddToGroupLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddToGroupLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddToFrontPageSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddToFrontPageSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiDownloadPhotoLinkHtml" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiDownloadPhotoLinkHtmlJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiCopyrightUsrLinkSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCopyrightUsrLinkSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoVideoLabel1" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoVideoLabel1J" type="jQueryObject">
    /// </field>
    /// <field name="_uiLinkToThis" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiLinkToThisJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoVideoLabel2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoVideoLabel2J" type="jQueryObject">
    /// </field>
    /// <field name="_uiEmbedThis" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiEmbedThisJ" type="jQueryObject">
    /// </field>
    /// <field name="_PhotoOfWeekDiv" type="Object" domElement="true">
    /// </field>
    /// <field name="_PhotoOfWeekDivJ" type="jQueryObject">
    /// </field>
    /// <field name="_Div1" type="Object" domElement="true">
    /// </field>
    /// <field name="_Div1J" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoUsage" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoUsageJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiDisplayMakeFrontPageOptions" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiDisplayMakeFrontPageOptionsJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiUsrIsLoggedIn" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUsrIsLoggedInJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiDisableBanner" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiDisableBannerJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiFirstTimeLoading" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiFirstTimeLoadingJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiOverlayEnabled" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiOverlayEnabledJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiOverlayWidth" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiOverlayWidthJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiOverlayHeight" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiOverlayHeightJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.PhotoControl.View.prototype = {
    clientId: null,
    
    get_uiContent: function Js_Controls_PhotoControl_View$get_uiContent() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiContent == null) {
            this._uiContent = document.getElementById(this.clientId + '_uiContent');
        }
        return this._uiContent;
    },
    
    _uiContent: null,
    
    get_uiContentJ: function Js_Controls_PhotoControl_View$get_uiContentJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiContentJ == null) {
            this._uiContentJ = $('#' + this.clientId + '_uiContent');
        }
        return this._uiContentJ;
    },
    
    _uiContentJ: null,
    
    get_uiPrevPhotoButtonDiv: function Js_Controls_PhotoControl_View$get_uiPrevPhotoButtonDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPrevPhotoButtonDiv == null) {
            this._uiPrevPhotoButtonDiv = document.getElementById(this.clientId + '_uiPrevPhotoButtonDiv');
        }
        return this._uiPrevPhotoButtonDiv;
    },
    
    _uiPrevPhotoButtonDiv: null,
    
    get_uiPrevPhotoButtonDivJ: function Js_Controls_PhotoControl_View$get_uiPrevPhotoButtonDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPrevPhotoButtonDivJ == null) {
            this._uiPrevPhotoButtonDivJ = $('#' + this.clientId + '_uiPrevPhotoButtonDiv');
        }
        return this._uiPrevPhotoButtonDivJ;
    },
    
    _uiPrevPhotoButtonDivJ: null,
    
    get_uiPrevPhotoButton: function Js_Controls_PhotoControl_View$get_uiPrevPhotoButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPrevPhotoButton == null) {
            this._uiPrevPhotoButton = document.getElementById(this.clientId + '_uiPrevPhotoButton');
        }
        return this._uiPrevPhotoButton;
    },
    
    _uiPrevPhotoButton: null,
    
    get_uiPrevPhotoButtonJ: function Js_Controls_PhotoControl_View$get_uiPrevPhotoButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPrevPhotoButtonJ == null) {
            this._uiPrevPhotoButtonJ = $('#' + this.clientId + '_uiPrevPhotoButton');
        }
        return this._uiPrevPhotoButtonJ;
    },
    
    _uiPrevPhotoButtonJ: null,
    
    get_uiNextPhotoButtonDiv: function Js_Controls_PhotoControl_View$get_uiNextPhotoButtonDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiNextPhotoButtonDiv == null) {
            this._uiNextPhotoButtonDiv = document.getElementById(this.clientId + '_uiNextPhotoButtonDiv');
        }
        return this._uiNextPhotoButtonDiv;
    },
    
    _uiNextPhotoButtonDiv: null,
    
    get_uiNextPhotoButtonDivJ: function Js_Controls_PhotoControl_View$get_uiNextPhotoButtonDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiNextPhotoButtonDivJ == null) {
            this._uiNextPhotoButtonDivJ = $('#' + this.clientId + '_uiNextPhotoButtonDiv');
        }
        return this._uiNextPhotoButtonDivJ;
    },
    
    _uiNextPhotoButtonDivJ: null,
    
    get_uiNextPhotoButton: function Js_Controls_PhotoControl_View$get_uiNextPhotoButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiNextPhotoButton == null) {
            this._uiNextPhotoButton = document.getElementById(this.clientId + '_uiNextPhotoButton');
        }
        return this._uiNextPhotoButton;
    },
    
    _uiNextPhotoButton: null,
    
    get_uiNextPhotoButtonJ: function Js_Controls_PhotoControl_View$get_uiNextPhotoButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiNextPhotoButtonJ == null) {
            this._uiNextPhotoButtonJ = $('#' + this.clientId + '_uiNextPhotoButton');
        }
        return this._uiNextPhotoButtonJ;
    },
    
    _uiNextPhotoButtonJ: null,
    
    get_uiBannerHolder: function Js_Controls_PhotoControl_View$get_uiBannerHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBannerHolder == null) {
            this._uiBannerHolder = document.getElementById(this.clientId + '_uiBannerHolder');
        }
        return this._uiBannerHolder;
    },
    
    _uiBannerHolder: null,
    
    get_uiBannerHolderJ: function Js_Controls_PhotoControl_View$get_uiBannerHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBannerHolderJ == null) {
            this._uiBannerHolderJ = $('#' + this.clientId + '_uiBannerHolder');
        }
        return this._uiBannerHolderJ;
    },
    
    _uiBannerHolderJ: null,
    
    get_uiBannerPlaceHolder: function Js_Controls_PhotoControl_View$get_uiBannerPlaceHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBannerPlaceHolder == null) {
            this._uiBannerPlaceHolder = document.getElementById(this.clientId + '_uiBannerPlaceHolder');
        }
        return this._uiBannerPlaceHolder;
    },
    
    _uiBannerPlaceHolder: null,
    
    get_uiBannerPlaceHolderJ: function Js_Controls_PhotoControl_View$get_uiBannerPlaceHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBannerPlaceHolderJ == null) {
            this._uiBannerPlaceHolderJ = $('#' + this.clientId + '_uiBannerPlaceHolder');
        }
        return this._uiBannerPlaceHolderJ;
    },
    
    _uiBannerPlaceHolderJ: null,
    
    get_bannerPhoto: function Js_Controls_PhotoControl_View$get_bannerPhoto() {
        /// <value type="Js.Controls.Banners.Generator.Controller"></value>
        return eval(this.clientId + '_BannerPhotoController');
    },
    
    get_uiPhotoDiv: function Js_Controls_PhotoControl_View$get_uiPhotoDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoDiv == null) {
            this._uiPhotoDiv = document.getElementById(this.clientId + '_uiPhotoDiv');
        }
        return this._uiPhotoDiv;
    },
    
    _uiPhotoDiv: null,
    
    get_uiPhotoDivJ: function Js_Controls_PhotoControl_View$get_uiPhotoDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoDivJ == null) {
            this._uiPhotoDivJ = $('#' + this.clientId + '_uiPhotoDiv');
        }
        return this._uiPhotoDivJ;
    },
    
    _uiPhotoDivJ: null,
    
    get_uiPhotoHolder: function Js_Controls_PhotoControl_View$get_uiPhotoHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoHolder == null) {
            this._uiPhotoHolder = document.getElementById(this.clientId + '_uiPhotoHolder');
        }
        return this._uiPhotoHolder;
    },
    
    _uiPhotoHolder: null,
    
    get_uiPhotoHolderJ: function Js_Controls_PhotoControl_View$get_uiPhotoHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoHolderJ == null) {
            this._uiPhotoHolderJ = $('#' + this.clientId + '_uiPhotoHolder');
        }
        return this._uiPhotoHolderJ;
    },
    
    _uiPhotoHolderJ: null,
    
    get_uiPhoto: function Js_Controls_PhotoControl_View$get_uiPhoto() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhoto == null) {
            this._uiPhoto = document.getElementById(this.clientId + '_uiPhoto');
        }
        return this._uiPhoto;
    },
    
    _uiPhoto: null,
    
    get_uiPhotoJ: function Js_Controls_PhotoControl_View$get_uiPhotoJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoJ == null) {
            this._uiPhotoJ = $('#' + this.clientId + '_uiPhoto');
        }
        return this._uiPhotoJ;
    },
    
    _uiPhotoJ: null,
    
    get_uiPhotoOverlay: function Js_Controls_PhotoControl_View$get_uiPhotoOverlay() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoOverlay == null) {
            this._uiPhotoOverlay = document.getElementById(this.clientId + '_uiPhotoOverlay');
        }
        return this._uiPhotoOverlay;
    },
    
    _uiPhotoOverlay: null,
    
    get_uiPhotoOverlayJ: function Js_Controls_PhotoControl_View$get_uiPhotoOverlayJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoOverlayJ == null) {
            this._uiPhotoOverlayJ = $('#' + this.clientId + '_uiPhotoOverlay');
        }
        return this._uiPhotoOverlayJ;
    },
    
    _uiPhotoOverlayJ: null,
    
    get_uiFlashHolder: function Js_Controls_PhotoControl_View$get_uiFlashHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiFlashHolder == null) {
            this._uiFlashHolder = document.getElementById(this.clientId + '_uiFlashHolder');
        }
        return this._uiFlashHolder;
    },
    
    _uiFlashHolder: null,
    
    get_uiFlashHolderJ: function Js_Controls_PhotoControl_View$get_uiFlashHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiFlashHolderJ == null) {
            this._uiFlashHolderJ = $('#' + this.clientId + '_uiFlashHolder');
        }
        return this._uiFlashHolderJ;
    },
    
    _uiFlashHolderJ: null,
    
    get_uiPhotoGalleryLinkHolder: function Js_Controls_PhotoControl_View$get_uiPhotoGalleryLinkHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoGalleryLinkHolder == null) {
            this._uiPhotoGalleryLinkHolder = document.getElementById(this.clientId + '_uiPhotoGalleryLinkHolder');
        }
        return this._uiPhotoGalleryLinkHolder;
    },
    
    _uiPhotoGalleryLinkHolder: null,
    
    get_uiPhotoGalleryLinkHolderJ: function Js_Controls_PhotoControl_View$get_uiPhotoGalleryLinkHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoGalleryLinkHolderJ == null) {
            this._uiPhotoGalleryLinkHolderJ = $('#' + this.clientId + '_uiPhotoGalleryLinkHolder');
        }
        return this._uiPhotoGalleryLinkHolderJ;
    },
    
    _uiPhotoGalleryLinkHolderJ: null,
    
    get_uiPhotoGalleryLink: function Js_Controls_PhotoControl_View$get_uiPhotoGalleryLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoGalleryLink == null) {
            this._uiPhotoGalleryLink = document.getElementById(this.clientId + '_uiPhotoGalleryLink');
        }
        return this._uiPhotoGalleryLink;
    },
    
    _uiPhotoGalleryLink: null,
    
    get_uiPhotoGalleryLinkJ: function Js_Controls_PhotoControl_View$get_uiPhotoGalleryLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoGalleryLinkJ == null) {
            this._uiPhotoGalleryLinkJ = $('#' + this.clientId + '_uiPhotoGalleryLink');
        }
        return this._uiPhotoGalleryLinkJ;
    },
    
    _uiPhotoGalleryLinkJ: null,
    
    get_uiTakenByDetailsSpan: function Js_Controls_PhotoControl_View$get_uiTakenByDetailsSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiTakenByDetailsSpan == null) {
            this._uiTakenByDetailsSpan = document.getElementById(this.clientId + '_uiTakenByDetailsSpan');
        }
        return this._uiTakenByDetailsSpan;
    },
    
    _uiTakenByDetailsSpan: null,
    
    get_uiTakenByDetailsSpanJ: function Js_Controls_PhotoControl_View$get_uiTakenByDetailsSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiTakenByDetailsSpanJ == null) {
            this._uiTakenByDetailsSpanJ = $('#' + this.clientId + '_uiTakenByDetailsSpan');
        }
        return this._uiTakenByDetailsSpanJ;
    },
    
    _uiTakenByDetailsSpanJ: null,
    
    get_uiUsrsInPhotoSpan: function Js_Controls_PhotoControl_View$get_uiUsrsInPhotoSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUsrsInPhotoSpan == null) {
            this._uiUsrsInPhotoSpan = document.getElementById(this.clientId + '_uiUsrsInPhotoSpan');
        }
        return this._uiUsrsInPhotoSpan;
    },
    
    _uiUsrsInPhotoSpan: null,
    
    get_uiUsrsInPhotoSpanJ: function Js_Controls_PhotoControl_View$get_uiUsrsInPhotoSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUsrsInPhotoSpanJ == null) {
            this._uiUsrsInPhotoSpanJ = $('#' + this.clientId + '_uiUsrsInPhotoSpan');
        }
        return this._uiUsrsInPhotoSpanJ;
    },
    
    _uiUsrsInPhotoSpanJ: null,
    
    get_uiBuddyChooserPanel: function Js_Controls_PhotoControl_View$get_uiBuddyChooserPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBuddyChooserPanel == null) {
            this._uiBuddyChooserPanel = document.getElementById(this.clientId + '_uiBuddyChooserPanel');
        }
        return this._uiBuddyChooserPanel;
    },
    
    _uiBuddyChooserPanel: null,
    
    get_uiBuddyChooserPanelJ: function Js_Controls_PhotoControl_View$get_uiBuddyChooserPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBuddyChooserPanelJ == null) {
            this._uiBuddyChooserPanelJ = $('#' + this.clientId + '_uiBuddyChooserPanel');
        }
        return this._uiBuddyChooserPanelJ;
    },
    
    _uiBuddyChooserPanelJ: null,
    
    get_uiUsrSpottedToggleButton: function Js_Controls_PhotoControl_View$get_uiUsrSpottedToggleButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUsrSpottedToggleButton == null) {
            this._uiUsrSpottedToggleButton = document.getElementById(this.clientId + '_uiUsrSpottedToggleButton');
        }
        return this._uiUsrSpottedToggleButton;
    },
    
    _uiUsrSpottedToggleButton: null,
    
    get_uiUsrSpottedToggleButtonJ: function Js_Controls_PhotoControl_View$get_uiUsrSpottedToggleButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUsrSpottedToggleButtonJ == null) {
            this._uiUsrSpottedToggleButtonJ = $('#' + this.clientId + '_uiUsrSpottedToggleButton');
        }
        return this._uiUsrSpottedToggleButtonJ;
    },
    
    _uiUsrSpottedToggleButtonJ: null,
    
    get_uiUseAsProfilePictureButton: function Js_Controls_PhotoControl_View$get_uiUseAsProfilePictureButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUseAsProfilePictureButton == null) {
            this._uiUseAsProfilePictureButton = document.getElementById(this.clientId + '_uiUseAsProfilePictureButton');
        }
        return this._uiUseAsProfilePictureButton;
    },
    
    _uiUseAsProfilePictureButton: null,
    
    get_uiUseAsProfilePictureButtonJ: function Js_Controls_PhotoControl_View$get_uiUseAsProfilePictureButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUseAsProfilePictureButtonJ == null) {
            this._uiUseAsProfilePictureButtonJ = $('#' + this.clientId + '_uiUseAsProfilePictureButton');
        }
        return this._uiUseAsProfilePictureButtonJ;
    },
    
    _uiUseAsProfilePictureButtonJ: null,
    
    get_uiBuddyChooserPanelInner: function Js_Controls_PhotoControl_View$get_uiBuddyChooserPanelInner() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBuddyChooserPanelInner == null) {
            this._uiBuddyChooserPanelInner = document.getElementById(this.clientId + '_uiBuddyChooserPanelInner');
        }
        return this._uiBuddyChooserPanelInner;
    },
    
    _uiBuddyChooserPanelInner: null,
    
    get_uiBuddyChooserPanelInnerJ: function Js_Controls_PhotoControl_View$get_uiBuddyChooserPanelInnerJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBuddyChooserPanelInnerJ == null) {
            this._uiBuddyChooserPanelInnerJ = $('#' + this.clientId + '_uiBuddyChooserPanelInner');
        }
        return this._uiBuddyChooserPanelInnerJ;
    },
    
    _uiBuddyChooserPanelInnerJ: null,
    
    get_uiBuddyChooser: function Js_Controls_PhotoControl_View$get_uiBuddyChooser() {
        /// <value type="Js.Controls.BuddyChooser.Controller"></value>
        return eval(this.clientId + '_uiBuddyChooserController');
    },
    
    get_uiBuddySpottedButton: function Js_Controls_PhotoControl_View$get_uiBuddySpottedButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBuddySpottedButton == null) {
            this._uiBuddySpottedButton = document.getElementById(this.clientId + '_uiBuddySpottedButton');
        }
        return this._uiBuddySpottedButton;
    },
    
    _uiBuddySpottedButton: null,
    
    get_uiBuddySpottedButtonJ: function Js_Controls_PhotoControl_View$get_uiBuddySpottedButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBuddySpottedButtonJ == null) {
            this._uiBuddySpottedButtonJ = $('#' + this.clientId + '_uiBuddySpottedButton');
        }
        return this._uiBuddySpottedButtonJ;
    },
    
    _uiBuddySpottedButtonJ: null,
    
    get_uiBuddyValidator: function Js_Controls_PhotoControl_View$get_uiBuddyValidator() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBuddyValidator == null) {
            this._uiBuddyValidator = document.getElementById(this.clientId + '_uiBuddyValidator');
        }
        return this._uiBuddyValidator;
    },
    
    _uiBuddyValidator: null,
    
    get_uiBuddyValidatorJ: function Js_Controls_PhotoControl_View$get_uiBuddyValidatorJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBuddyValidatorJ == null) {
            this._uiBuddyValidatorJ = $('#' + this.clientId + '_uiBuddyValidator');
        }
        return this._uiBuddyValidatorJ;
    },
    
    _uiBuddyValidatorJ: null,
    
    get_uiCompetitionPanel: function Js_Controls_PhotoControl_View$get_uiCompetitionPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCompetitionPanel == null) {
            this._uiCompetitionPanel = document.getElementById(this.clientId + '_uiCompetitionPanel');
        }
        return this._uiCompetitionPanel;
    },
    
    _uiCompetitionPanel: null,
    
    get_uiCompetitionPanelJ: function Js_Controls_PhotoControl_View$get_uiCompetitionPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCompetitionPanelJ == null) {
            this._uiCompetitionPanelJ = $('#' + this.clientId + '_uiCompetitionPanel');
        }
        return this._uiCompetitionPanelJ;
    },
    
    _uiCompetitionPanelJ: null,
    
    get_uiAddToCompetitionGroup: function Js_Controls_PhotoControl_View$get_uiAddToCompetitionGroup() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddToCompetitionGroup == null) {
            this._uiAddToCompetitionGroup = document.getElementById(this.clientId + '_uiAddToCompetitionGroup');
        }
        return this._uiAddToCompetitionGroup;
    },
    
    _uiAddToCompetitionGroup: null,
    
    get_uiAddToCompetitionGroupJ: function Js_Controls_PhotoControl_View$get_uiAddToCompetitionGroupJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddToCompetitionGroupJ == null) {
            this._uiAddToCompetitionGroupJ = $('#' + this.clientId + '_uiAddToCompetitionGroup');
        }
        return this._uiAddToCompetitionGroupJ;
    },
    
    _uiAddToCompetitionGroupJ: null,
    
    get_uiAddToCompetitionGroupImg: function Js_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImg() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddToCompetitionGroupImg == null) {
            this._uiAddToCompetitionGroupImg = document.getElementById(this.clientId + '_uiAddToCompetitionGroupImg');
        }
        return this._uiAddToCompetitionGroupImg;
    },
    
    _uiAddToCompetitionGroupImg: null,
    
    get_uiAddToCompetitionGroupImgJ: function Js_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImgJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddToCompetitionGroupImgJ == null) {
            this._uiAddToCompetitionGroupImgJ = $('#' + this.clientId + '_uiAddToCompetitionGroupImg');
        }
        return this._uiAddToCompetitionGroupImgJ;
    },
    
    _uiAddToCompetitionGroupImgJ: null,
    
    get_uiCompetitionGroupLink: function Js_Controls_PhotoControl_View$get_uiCompetitionGroupLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCompetitionGroupLink == null) {
            this._uiCompetitionGroupLink = document.getElementById(this.clientId + '_uiCompetitionGroupLink');
        }
        return this._uiCompetitionGroupLink;
    },
    
    _uiCompetitionGroupLink: null,
    
    get_uiCompetitionGroupLinkJ: function Js_Controls_PhotoControl_View$get_uiCompetitionGroupLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCompetitionGroupLinkJ == null) {
            this._uiCompetitionGroupLinkJ = $('#' + this.clientId + '_uiCompetitionGroupLink');
        }
        return this._uiCompetitionGroupLinkJ;
    },
    
    _uiCompetitionGroupLinkJ: null,
    
    get_uiAddToCompetitionGroupImgAddButtonUrl: function Js_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImgAddButtonUrl() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddToCompetitionGroupImgAddButtonUrl == null) {
            this._uiAddToCompetitionGroupImgAddButtonUrl = document.getElementById(this.clientId + '_uiAddToCompetitionGroupImgAddButtonUrl');
        }
        return this._uiAddToCompetitionGroupImgAddButtonUrl;
    },
    
    _uiAddToCompetitionGroupImgAddButtonUrl: null,
    
    get_uiAddToCompetitionGroupImgAddButtonUrlJ: function Js_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImgAddButtonUrlJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddToCompetitionGroupImgAddButtonUrlJ == null) {
            this._uiAddToCompetitionGroupImgAddButtonUrlJ = $('#' + this.clientId + '_uiAddToCompetitionGroupImgAddButtonUrl');
        }
        return this._uiAddToCompetitionGroupImgAddButtonUrlJ;
    },
    
    _uiAddToCompetitionGroupImgAddButtonUrlJ: null,
    
    get_uiAddToCompetitionGroupImgRemoveButtonUrl: function Js_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImgRemoveButtonUrl() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddToCompetitionGroupImgRemoveButtonUrl == null) {
            this._uiAddToCompetitionGroupImgRemoveButtonUrl = document.getElementById(this.clientId + '_uiAddToCompetitionGroupImgRemoveButtonUrl');
        }
        return this._uiAddToCompetitionGroupImgRemoveButtonUrl;
    },
    
    _uiAddToCompetitionGroupImgRemoveButtonUrl: null,
    
    get_uiAddToCompetitionGroupImgRemoveButtonUrlJ: function Js_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImgRemoveButtonUrlJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddToCompetitionGroupImgRemoveButtonUrlJ == null) {
            this._uiAddToCompetitionGroupImgRemoveButtonUrlJ = $('#' + this.clientId + '_uiAddToCompetitionGroupImgRemoveButtonUrl');
        }
        return this._uiAddToCompetitionGroupImgRemoveButtonUrlJ;
    },
    
    _uiAddToCompetitionGroupImgRemoveButtonUrlJ: null,
    
    get_uiQuickBrowserUrl: function Js_Controls_PhotoControl_View$get_uiQuickBrowserUrl() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiQuickBrowserUrl == null) {
            this._uiQuickBrowserUrl = document.getElementById(this.clientId + '_uiQuickBrowserUrl');
        }
        return this._uiQuickBrowserUrl;
    },
    
    _uiQuickBrowserUrl: null,
    
    get_uiQuickBrowserUrlJ: function Js_Controls_PhotoControl_View$get_uiQuickBrowserUrlJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiQuickBrowserUrlJ == null) {
            this._uiQuickBrowserUrlJ = $('#' + this.clientId + '_uiQuickBrowserUrl');
        }
        return this._uiQuickBrowserUrlJ;
    },
    
    _uiQuickBrowserUrlJ: null,
    
    get_uiIsFavouritePhotoToggleButton: function Js_Controls_PhotoControl_View$get_uiIsFavouritePhotoToggleButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiIsFavouritePhotoToggleButton == null) {
            this._uiIsFavouritePhotoToggleButton = document.getElementById(this.clientId + '_uiIsFavouritePhotoToggleButton');
        }
        return this._uiIsFavouritePhotoToggleButton;
    },
    
    _uiIsFavouritePhotoToggleButton: null,
    
    get_uiIsFavouritePhotoToggleButtonJ: function Js_Controls_PhotoControl_View$get_uiIsFavouritePhotoToggleButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiIsFavouritePhotoToggleButtonJ == null) {
            this._uiIsFavouritePhotoToggleButtonJ = $('#' + this.clientId + '_uiIsFavouritePhotoToggleButton');
        }
        return this._uiIsFavouritePhotoToggleButtonJ;
    },
    
    _uiIsFavouritePhotoToggleButtonJ: null,
    
    get_uiSendLink: function Js_Controls_PhotoControl_View$get_uiSendLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiSendLink == null) {
            this._uiSendLink = document.getElementById(this.clientId + '_uiSendLink');
        }
        return this._uiSendLink;
    },
    
    _uiSendLink: null,
    
    get_uiSendLinkJ: function Js_Controls_PhotoControl_View$get_uiSendLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiSendLinkJ == null) {
            this._uiSendLinkJ = $('#' + this.clientId + '_uiSendLink');
        }
        return this._uiSendLinkJ;
    },
    
    _uiSendLinkJ: null,
    
    get_uiReportLink: function Js_Controls_PhotoControl_View$get_uiReportLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiReportLink == null) {
            this._uiReportLink = document.getElementById(this.clientId + '_uiReportLink');
        }
        return this._uiReportLink;
    },
    
    _uiReportLink: null,
    
    get_uiReportLinkJ: function Js_Controls_PhotoControl_View$get_uiReportLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiReportLinkJ == null) {
            this._uiReportLinkJ = $('#' + this.clientId + '_uiReportLink');
        }
        return this._uiReportLinkJ;
    },
    
    _uiReportLinkJ: null,
    
    get_uiAddToGroupTopPhotosSpan: function Js_Controls_PhotoControl_View$get_uiAddToGroupTopPhotosSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddToGroupTopPhotosSpan == null) {
            this._uiAddToGroupTopPhotosSpan = document.getElementById(this.clientId + '_uiAddToGroupTopPhotosSpan');
        }
        return this._uiAddToGroupTopPhotosSpan;
    },
    
    _uiAddToGroupTopPhotosSpan: null,
    
    get_uiAddToGroupTopPhotosSpanJ: function Js_Controls_PhotoControl_View$get_uiAddToGroupTopPhotosSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddToGroupTopPhotosSpanJ == null) {
            this._uiAddToGroupTopPhotosSpanJ = $('#' + this.clientId + '_uiAddToGroupTopPhotosSpan');
        }
        return this._uiAddToGroupTopPhotosSpanJ;
    },
    
    _uiAddToGroupTopPhotosSpanJ: null,
    
    get_uiAddToGroupLink: function Js_Controls_PhotoControl_View$get_uiAddToGroupLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddToGroupLink == null) {
            this._uiAddToGroupLink = document.getElementById(this.clientId + '_uiAddToGroupLink');
        }
        return this._uiAddToGroupLink;
    },
    
    _uiAddToGroupLink: null,
    
    get_uiAddToGroupLinkJ: function Js_Controls_PhotoControl_View$get_uiAddToGroupLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddToGroupLinkJ == null) {
            this._uiAddToGroupLinkJ = $('#' + this.clientId + '_uiAddToGroupLink');
        }
        return this._uiAddToGroupLinkJ;
    },
    
    _uiAddToGroupLinkJ: null,
    
    get_uiAddToFrontPageSpan: function Js_Controls_PhotoControl_View$get_uiAddToFrontPageSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddToFrontPageSpan == null) {
            this._uiAddToFrontPageSpan = document.getElementById(this.clientId + '_uiAddToFrontPageSpan');
        }
        return this._uiAddToFrontPageSpan;
    },
    
    _uiAddToFrontPageSpan: null,
    
    get_uiAddToFrontPageSpanJ: function Js_Controls_PhotoControl_View$get_uiAddToFrontPageSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddToFrontPageSpanJ == null) {
            this._uiAddToFrontPageSpanJ = $('#' + this.clientId + '_uiAddToFrontPageSpan');
        }
        return this._uiAddToFrontPageSpanJ;
    },
    
    _uiAddToFrontPageSpanJ: null,
    
    get_uiDownloadPhotoLinkHtml: function Js_Controls_PhotoControl_View$get_uiDownloadPhotoLinkHtml() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiDownloadPhotoLinkHtml == null) {
            this._uiDownloadPhotoLinkHtml = document.getElementById(this.clientId + '_uiDownloadPhotoLinkHtml');
        }
        return this._uiDownloadPhotoLinkHtml;
    },
    
    _uiDownloadPhotoLinkHtml: null,
    
    get_uiDownloadPhotoLinkHtmlJ: function Js_Controls_PhotoControl_View$get_uiDownloadPhotoLinkHtmlJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiDownloadPhotoLinkHtmlJ == null) {
            this._uiDownloadPhotoLinkHtmlJ = $('#' + this.clientId + '_uiDownloadPhotoLinkHtml');
        }
        return this._uiDownloadPhotoLinkHtmlJ;
    },
    
    _uiDownloadPhotoLinkHtmlJ: null,
    
    get_uiCopyrightUsrLinkSpan: function Js_Controls_PhotoControl_View$get_uiCopyrightUsrLinkSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCopyrightUsrLinkSpan == null) {
            this._uiCopyrightUsrLinkSpan = document.getElementById(this.clientId + '_uiCopyrightUsrLinkSpan');
        }
        return this._uiCopyrightUsrLinkSpan;
    },
    
    _uiCopyrightUsrLinkSpan: null,
    
    get_uiCopyrightUsrLinkSpanJ: function Js_Controls_PhotoControl_View$get_uiCopyrightUsrLinkSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCopyrightUsrLinkSpanJ == null) {
            this._uiCopyrightUsrLinkSpanJ = $('#' + this.clientId + '_uiCopyrightUsrLinkSpan');
        }
        return this._uiCopyrightUsrLinkSpanJ;
    },
    
    _uiCopyrightUsrLinkSpanJ: null,
    
    get_uiPhotoVideoLabel1: function Js_Controls_PhotoControl_View$get_uiPhotoVideoLabel1() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoVideoLabel1 == null) {
            this._uiPhotoVideoLabel1 = document.getElementById(this.clientId + '_uiPhotoVideoLabel1');
        }
        return this._uiPhotoVideoLabel1;
    },
    
    _uiPhotoVideoLabel1: null,
    
    get_uiPhotoVideoLabel1J: function Js_Controls_PhotoControl_View$get_uiPhotoVideoLabel1J() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoVideoLabel1J == null) {
            this._uiPhotoVideoLabel1J = $('#' + this.clientId + '_uiPhotoVideoLabel1');
        }
        return this._uiPhotoVideoLabel1J;
    },
    
    _uiPhotoVideoLabel1J: null,
    
    get_uiLinkToThis: function Js_Controls_PhotoControl_View$get_uiLinkToThis() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiLinkToThis == null) {
            this._uiLinkToThis = document.getElementById(this.clientId + '_uiLinkToThis');
        }
        return this._uiLinkToThis;
    },
    
    _uiLinkToThis: null,
    
    get_uiLinkToThisJ: function Js_Controls_PhotoControl_View$get_uiLinkToThisJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiLinkToThisJ == null) {
            this._uiLinkToThisJ = $('#' + this.clientId + '_uiLinkToThis');
        }
        return this._uiLinkToThisJ;
    },
    
    _uiLinkToThisJ: null,
    
    get_uiPhotoVideoLabel2: function Js_Controls_PhotoControl_View$get_uiPhotoVideoLabel2() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoVideoLabel2 == null) {
            this._uiPhotoVideoLabel2 = document.getElementById(this.clientId + '_uiPhotoVideoLabel2');
        }
        return this._uiPhotoVideoLabel2;
    },
    
    _uiPhotoVideoLabel2: null,
    
    get_uiPhotoVideoLabel2J: function Js_Controls_PhotoControl_View$get_uiPhotoVideoLabel2J() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoVideoLabel2J == null) {
            this._uiPhotoVideoLabel2J = $('#' + this.clientId + '_uiPhotoVideoLabel2');
        }
        return this._uiPhotoVideoLabel2J;
    },
    
    _uiPhotoVideoLabel2J: null,
    
    get_uiEmbedThis: function Js_Controls_PhotoControl_View$get_uiEmbedThis() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiEmbedThis == null) {
            this._uiEmbedThis = document.getElementById(this.clientId + '_uiEmbedThis');
        }
        return this._uiEmbedThis;
    },
    
    _uiEmbedThis: null,
    
    get_uiEmbedThisJ: function Js_Controls_PhotoControl_View$get_uiEmbedThisJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiEmbedThisJ == null) {
            this._uiEmbedThisJ = $('#' + this.clientId + '_uiEmbedThis');
        }
        return this._uiEmbedThisJ;
    },
    
    _uiEmbedThisJ: null,
    
    get_photoOfWeekDiv: function Js_Controls_PhotoControl_View$get_photoOfWeekDiv() {
        /// <value type="Object" domElement="true"></value>
        if (this._PhotoOfWeekDiv == null) {
            this._PhotoOfWeekDiv = document.getElementById(this.clientId + '_PhotoOfWeekDiv');
        }
        return this._PhotoOfWeekDiv;
    },
    
    _PhotoOfWeekDiv: null,
    
    get_photoOfWeekDivJ: function Js_Controls_PhotoControl_View$get_photoOfWeekDivJ() {
        /// <value type="jQueryObject"></value>
        if (this._PhotoOfWeekDivJ == null) {
            this._PhotoOfWeekDivJ = $('#' + this.clientId + '_PhotoOfWeekDiv');
        }
        return this._PhotoOfWeekDivJ;
    },
    
    _PhotoOfWeekDivJ: null,
    
    get_div1: function Js_Controls_PhotoControl_View$get_div1() {
        /// <value type="Object" domElement="true"></value>
        if (this._Div1 == null) {
            this._Div1 = document.getElementById(this.clientId + '_Div1');
        }
        return this._Div1;
    },
    
    _Div1: null,
    
    get_div1J: function Js_Controls_PhotoControl_View$get_div1J() {
        /// <value type="jQueryObject"></value>
        if (this._Div1J == null) {
            this._Div1J = $('#' + this.clientId + '_Div1');
        }
        return this._Div1J;
    },
    
    _Div1J: null,
    
    get_uiPhotoUsage: function Js_Controls_PhotoControl_View$get_uiPhotoUsage() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoUsage == null) {
            this._uiPhotoUsage = document.getElementById(this.clientId + '_uiPhotoUsage');
        }
        return this._uiPhotoUsage;
    },
    
    _uiPhotoUsage: null,
    
    get_uiPhotoUsageJ: function Js_Controls_PhotoControl_View$get_uiPhotoUsageJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoUsageJ == null) {
            this._uiPhotoUsageJ = $('#' + this.clientId + '_uiPhotoUsage');
        }
        return this._uiPhotoUsageJ;
    },
    
    _uiPhotoUsageJ: null,
    
    get_uiDisplayMakeFrontPageOptions: function Js_Controls_PhotoControl_View$get_uiDisplayMakeFrontPageOptions() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiDisplayMakeFrontPageOptions == null) {
            this._uiDisplayMakeFrontPageOptions = document.getElementById(this.clientId + '_uiDisplayMakeFrontPageOptions');
        }
        return this._uiDisplayMakeFrontPageOptions;
    },
    
    _uiDisplayMakeFrontPageOptions: null,
    
    get_uiDisplayMakeFrontPageOptionsJ: function Js_Controls_PhotoControl_View$get_uiDisplayMakeFrontPageOptionsJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiDisplayMakeFrontPageOptionsJ == null) {
            this._uiDisplayMakeFrontPageOptionsJ = $('#' + this.clientId + '_uiDisplayMakeFrontPageOptions');
        }
        return this._uiDisplayMakeFrontPageOptionsJ;
    },
    
    _uiDisplayMakeFrontPageOptionsJ: null,
    
    get_uiUsrIsLoggedIn: function Js_Controls_PhotoControl_View$get_uiUsrIsLoggedIn() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUsrIsLoggedIn == null) {
            this._uiUsrIsLoggedIn = document.getElementById(this.clientId + '_uiUsrIsLoggedIn');
        }
        return this._uiUsrIsLoggedIn;
    },
    
    _uiUsrIsLoggedIn: null,
    
    get_uiUsrIsLoggedInJ: function Js_Controls_PhotoControl_View$get_uiUsrIsLoggedInJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUsrIsLoggedInJ == null) {
            this._uiUsrIsLoggedInJ = $('#' + this.clientId + '_uiUsrIsLoggedIn');
        }
        return this._uiUsrIsLoggedInJ;
    },
    
    _uiUsrIsLoggedInJ: null,
    
    get_uiDisableBanner: function Js_Controls_PhotoControl_View$get_uiDisableBanner() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiDisableBanner == null) {
            this._uiDisableBanner = document.getElementById(this.clientId + '_uiDisableBanner');
        }
        return this._uiDisableBanner;
    },
    
    _uiDisableBanner: null,
    
    get_uiDisableBannerJ: function Js_Controls_PhotoControl_View$get_uiDisableBannerJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiDisableBannerJ == null) {
            this._uiDisableBannerJ = $('#' + this.clientId + '_uiDisableBanner');
        }
        return this._uiDisableBannerJ;
    },
    
    _uiDisableBannerJ: null,
    
    get_uiFirstTimeLoading: function Js_Controls_PhotoControl_View$get_uiFirstTimeLoading() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiFirstTimeLoading == null) {
            this._uiFirstTimeLoading = document.getElementById(this.clientId + '_uiFirstTimeLoading');
        }
        return this._uiFirstTimeLoading;
    },
    
    _uiFirstTimeLoading: null,
    
    get_uiFirstTimeLoadingJ: function Js_Controls_PhotoControl_View$get_uiFirstTimeLoadingJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiFirstTimeLoadingJ == null) {
            this._uiFirstTimeLoadingJ = $('#' + this.clientId + '_uiFirstTimeLoading');
        }
        return this._uiFirstTimeLoadingJ;
    },
    
    _uiFirstTimeLoadingJ: null,
    
    get_uiOverlayEnabled: function Js_Controls_PhotoControl_View$get_uiOverlayEnabled() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiOverlayEnabled == null) {
            this._uiOverlayEnabled = document.getElementById(this.clientId + '_uiOverlayEnabled');
        }
        return this._uiOverlayEnabled;
    },
    
    _uiOverlayEnabled: null,
    
    get_uiOverlayEnabledJ: function Js_Controls_PhotoControl_View$get_uiOverlayEnabledJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiOverlayEnabledJ == null) {
            this._uiOverlayEnabledJ = $('#' + this.clientId + '_uiOverlayEnabled');
        }
        return this._uiOverlayEnabledJ;
    },
    
    _uiOverlayEnabledJ: null,
    
    get_uiOverlayWidth: function Js_Controls_PhotoControl_View$get_uiOverlayWidth() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiOverlayWidth == null) {
            this._uiOverlayWidth = document.getElementById(this.clientId + '_uiOverlayWidth');
        }
        return this._uiOverlayWidth;
    },
    
    _uiOverlayWidth: null,
    
    get_uiOverlayWidthJ: function Js_Controls_PhotoControl_View$get_uiOverlayWidthJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiOverlayWidthJ == null) {
            this._uiOverlayWidthJ = $('#' + this.clientId + '_uiOverlayWidth');
        }
        return this._uiOverlayWidthJ;
    },
    
    _uiOverlayWidthJ: null,
    
    get_uiOverlayHeight: function Js_Controls_PhotoControl_View$get_uiOverlayHeight() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiOverlayHeight == null) {
            this._uiOverlayHeight = document.getElementById(this.clientId + '_uiOverlayHeight');
        }
        return this._uiOverlayHeight;
    },
    
    _uiOverlayHeight: null,
    
    get_uiOverlayHeightJ: function Js_Controls_PhotoControl_View$get_uiOverlayHeightJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiOverlayHeightJ == null) {
            this._uiOverlayHeightJ = $('#' + this.clientId + '_uiOverlayHeight');
        }
        return this._uiOverlayHeightJ;
    },
    
    _uiOverlayHeightJ: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoControl.PhotoStub

Js.Controls.PhotoControl.PhotoStub = function Js_Controls_PhotoControl_PhotoStub() {
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="iconPath" type="String">
    /// </field>
    /// <field name="webPath" type="String">
    /// </field>
    /// <field name="thumbPath" type="String">
    /// </field>
    /// <field name="width" type="Number" integer="true">
    /// </field>
    /// <field name="height" type="Number" integer="true">
    /// </field>
    /// <field name="thumbWidth" type="Number" integer="true">
    /// </field>
    /// <field name="thumbHeight" type="Number" integer="true">
    /// </field>
    /// <field name="takenByDetailsHtml" type="String">
    /// </field>
    /// <field name="usrLink" type="String">
    /// </field>
    /// <field name="photoVideoLabel" type="String">
    /// </field>
    /// <field name="isPhoto" type="Boolean">
    /// </field>
    /// <field name="isVideo" type="Boolean">
    /// </field>
    /// <field name="videoMedWidth" type="Number" integer="true">
    /// </field>
    /// <field name="videoMedHeight" type="Number" integer="true">
    /// </field>
    /// <field name="videoMedPath" type="String">
    /// </field>
    /// <field name="usrsInPhotoHtml" type="String">
    /// </field>
    /// <field name="usrIsInPhoto" type="Boolean">
    /// </field>
    /// <field name="isFavourite" type="Boolean">
    /// </field>
    /// <field name="isInCompetitionGroup" type="Boolean">
    /// </field>
    /// <field name="canEnterCompetition" type="Boolean">
    /// </field>
    /// <field name="quickBrowserUrl" type="String">
    /// </field>
    /// <field name="downloadPhotoLinkHtml" type="String">
    /// </field>
    /// <field name="linkToPhotoUrl" type="String">
    /// </field>
    /// <field name="embedThisPhotoHtml" type="String">
    /// </field>
    /// <field name="photoUsageAdminString" type="String">
    /// </field>
    /// <field name="threadK" type="Number" integer="true">
    /// </field>
    /// <field name="commentsCount" type="Number" integer="true">
    /// </field>
    /// <field name="chatRoomGuid" type="String">
    /// </field>
    /// <field name="rolloverMouseOverText" type="String">
    /// </field>
}
Js.Controls.PhotoControl.PhotoStub.prototype = {
    k: 0,
    url: null,
    iconPath: null,
    webPath: null,
    thumbPath: null,
    width: 0,
    height: 0,
    thumbWidth: 0,
    thumbHeight: 0,
    takenByDetailsHtml: null,
    usrLink: null,
    photoVideoLabel: null,
    isPhoto: false,
    isVideo: false,
    videoMedWidth: 0,
    videoMedHeight: 0,
    videoMedPath: null,
    usrsInPhotoHtml: null,
    usrIsInPhoto: false,
    isFavourite: false,
    isInCompetitionGroup: false,
    canEnterCompetition: false,
    quickBrowserUrl: null,
    downloadPhotoLinkHtml: null,
    linkToPhotoUrl: null,
    embedThisPhotoHtml: null,
    photoUsageAdminString: null,
    threadK: 0,
    commentsCount: 0,
    chatRoomGuid: null,
    rolloverMouseOverText: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoControl.PhotoResult

Js.Controls.PhotoControl.PhotoResult = function Js_Controls_PhotoControl_PhotoResult() {
    /// <field name="photos" type="Array" elementType="PhotoStub">
    /// </field>
    /// <field name="lastPage" type="Number" integer="true">
    /// </field>
}
Js.Controls.PhotoControl.PhotoResult.prototype = {
    photos: null,
    lastPage: 0
}


Type.registerNamespace('Js.Controls.PhotoBrowser');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.PhotoEventArgs

Js.Controls.PhotoBrowser.PhotoEventArgs = function Js_Controls_PhotoBrowser_PhotoEventArgs(photo) {
    /// <param name="photo" type="Js.Controls.PhotoControl.PhotoStub">
    /// </param>
    /// <field name="photo" type="Js.Controls.PhotoControl.PhotoStub">
    /// </field>
    Js.Controls.PhotoBrowser.PhotoEventArgs.initializeBase(this);
    this.photo = photo;
}
Js.Controls.PhotoBrowser.PhotoEventArgs.prototype = {
    photo: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.PhotoSetEventArgs

Js.Controls.PhotoBrowser.PhotoSetEventArgs = function Js_Controls_PhotoBrowser_PhotoSetEventArgs(photoSet, selectedIndex) {
    /// <param name="photoSet" type="Array" elementType="PhotoStub">
    /// </param>
    /// <param name="selectedIndex" type="Number" integer="true">
    /// </param>
    /// <field name="photoSet" type="Array" elementType="PhotoStub">
    /// </field>
    /// <field name="selectedIndex" type="Number" integer="true">
    /// </field>
    Js.Controls.PhotoBrowser.PhotoSetEventArgs.initializeBase(this);
    this.photoSet = photoSet;
    this.selectedIndex = selectedIndex;
}
Js.Controls.PhotoBrowser.PhotoSetEventArgs.prototype = {
    photoSet: null,
    selectedIndex: 0
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl

Js.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl = function Js_Controls_PhotoBrowser_PhotoBrowsingUsingKeysControl(focusControls) {
    /// <param name="focusControls" type="Array" elementType="jQueryObject">
    /// </param>
    /// <field name="onPhotoNextClick" type="Function">
    /// </field>
    /// <field name="onPhotoPrevClick" type="Function">
    /// </field>
    /// <field name="onPhotoUpClick" type="Function">
    /// </field>
    /// <field name="onPhotoDownClick" type="Function">
    /// </field>
    /// <field name="onArrowKeyPress" type="Function">
    /// </field>
    for (var i = 0; i < focusControls.length; i++) {
        focusControls[i].keydown(ss.Delegate.create(this, this.keyDown));
    }
}
Js.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl.prototype = {
    onPhotoNextClick: null,
    onPhotoPrevClick: null,
    onPhotoUpClick: null,
    onPhotoDownClick: null,
    onArrowKeyPress: null,
    
    keyDown: function Js_Controls_PhotoBrowser_PhotoBrowsingUsingKeysControl$keyDown(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (e.which === 39) {
            if (this.onArrowKeyPress != null) {
                this.onArrowKeyPress(this, ss.EventArgs.Empty);
            }
            if (this.onPhotoNextClick != null) {
                this.onPhotoNextClick(this, ss.EventArgs.Empty);
            }
            e.preventDefault();
        }
        else if (e.which === 37) {
            if (this.onArrowKeyPress != null) {
                this.onArrowKeyPress(this, ss.EventArgs.Empty);
            }
            if (this.onPhotoPrevClick != null) {
                this.onPhotoPrevClick(this, ss.EventArgs.Empty);
            }
            e.preventDefault();
        }
        else {
            this.nonArrowKeyDown(e);
        }
    },
    
    nonArrowKeyDown: function Js_Controls_PhotoBrowser_PhotoBrowsingUsingKeysControl$nonArrowKeyDown(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
    }
}


Js.Controls.PhotoControl.BannerStub.registerClass('Js.Controls.PhotoControl.BannerStub');
Js.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl.registerClass('Js.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl');
Js.Controls.PhotoControl.Controller.registerClass('Js.Controls.PhotoControl.Controller', Js.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl);
Js.Controls.PhotoControl.Service.registerClass('Js.Controls.PhotoControl.Service');
Js.Controls.PhotoControl.View.registerClass('Js.Controls.PhotoControl.View');
Js.Controls.PhotoControl.PhotoStub.registerClass('Js.Controls.PhotoControl.PhotoStub');
Js.Controls.PhotoControl.PhotoResult.registerClass('Js.Controls.PhotoControl.PhotoResult');
Js.Controls.PhotoBrowser.PhotoEventArgs.registerClass('Js.Controls.PhotoBrowser.PhotoEventArgs', ss.EventArgs);
Js.Controls.PhotoBrowser.PhotoSetEventArgs.registerClass('Js.Controls.PhotoBrowser.PhotoSetEventArgs', ss.EventArgs);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
