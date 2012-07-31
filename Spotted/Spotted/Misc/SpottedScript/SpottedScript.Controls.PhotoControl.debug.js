Type.registerNamespace('SpottedScript.Controls.PhotoControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoControl.BannerStub
SpottedScript.Controls.PhotoControl.BannerStub = function SpottedScript_Controls_PhotoControl_BannerStub() {
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="html" type="String">
    /// </field>
    /// <field name="script" type="String">
    /// </field>
}
SpottedScript.Controls.PhotoControl.BannerStub.prototype = {
    k: 0,
    html: null,
    script: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoControl.PhotoStub
SpottedScript.Controls.PhotoControl.PhotoStub = function SpottedScript_Controls_PhotoControl_PhotoStub() {
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
SpottedScript.Controls.PhotoControl.PhotoStub.prototype = {
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
// SpottedScript.Controls.PhotoControl.PhotoResult
SpottedScript.Controls.PhotoControl.PhotoResult = function SpottedScript_Controls_PhotoControl_PhotoResult() {
    /// <field name="photos" type="Array" elementType="PhotoStub">
    /// </field>
    /// <field name="lastPage" type="Number" integer="true">
    /// </field>
}
SpottedScript.Controls.PhotoControl.PhotoResult.prototype = {
    photos: null,
    lastPage: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoControl.Controller
SpottedScript.Controls.PhotoControl.Controller = function SpottedScript_Controls_PhotoControl_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.PhotoControl.View">
    /// </param>
    /// <field name="_view$1" type="SpottedScript.Controls.PhotoControl.View">
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
    /// <field name="_onRolloverMouseOverTextChanged" type="Sys.EventHandler">
    /// </field>
    /// <field name="_onPhotoChanged" type="Sys.EventHandler">
    /// </field>
    /// <field name="_onPhotoChangedAfterDelay" type="Sys.EventHandler">
    /// </field>
    /// <field name="_delayCount$1" type="Number" integer="true">
    /// </field>
    /// <field name="_delayMilliseconds$1" type="Number" integer="true">
    /// </field>
    /// <field name="_banners$1" type="Array" elementType="BannerStub">
    /// </field>
    /// <field name="_currentBannerIndex$1" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.PhotoControl.Controller.initializeBase(this, [ [ view.get_uiPhoto(), view.get_uiFlashHolder() ] ]);
    this._view$1 = view;
    this._displayMakeFrontPageOptions$1 = Boolean.parse(view.get_uiDisplayMakeFrontPageOptions().value);
    $addHandler(view.get_uiPhoto(), 'click', Function.createDelegate(this, this._nextPhotoClick$1));
    $addHandler(view.get_uiPrevPhotoButton(), 'click', Function.createDelegate(this, this._prevPhotoClick$1));
    $addHandler(view.get_uiNextPhotoButton(), 'click', Function.createDelegate(this, this._nextPhotoClick$1));
    if (view.get_uiUsrSpottedToggleButton() != null) {
        $addHandler(view.get_uiUsrSpottedToggleButton(), 'click', Function.createDelegate(this, this._usrSpottedToggleButtonClick$1));
    }
    if (Boolean.parse(view.get_uiUsrIsLoggedIn().value)) {
        $addHandler(view.get_uiIsFavouritePhotoToggleButton(), 'click', Function.createDelegate(this, this._isFavouritePhotoToggleButtonClick$1));
    }
    this._disableBanner$1 = Boolean.parse(view.get_uiDisableBanner().value);
    this._overlayEnabled$1 = Boolean.parse(view.get_uiOverlayEnabled().value);
    this._overlayWidth$1 = Number.parseInvariant(view.get_uiOverlayWidth().value);
    this._overlayHeight$1 = Number.parseInvariant(view.get_uiOverlayHeight().value);
    if (view.get_uiBuddySpottedButton() != null) {
        $addHandler(view.get_uiBuddySpottedButton(), 'click', Function.createDelegate(this, this._buddySpottedButtonClick$1));
    }
    if (view.get_uiUseAsProfilePictureButton() != null) {
        view.get_uiUseAsProfilePictureButton().setAttribute('onclick', '');
        $addHandler(view.get_uiUseAsProfilePictureButton(), 'click', Function.createDelegate(this, this._useAsProfilePictureButtonClick$1));
    }
    if (view.get_uiAddToCompetitionGroup() != null) {
        $addHandler(view.get_uiAddToCompetitionGroup(), 'click', Function.createDelegate(this, this._addToCompetitionGroupPhoto$1));
    }
}
SpottedScript.Controls.PhotoControl.Controller.prototype = {
    _view$1: null,
    _photoSet$1: null,
    _selectedIndex$1: 0,
    _disableBanner$1: false,
    _overlayEnabled$1: false,
    _overlayHeight$1: 0,
    _overlayWidth$1: 0,
    get_currentPhoto: function SpottedScript_Controls_PhotoControl_Controller$get_currentPhoto() {
        /// <value type="SpottedScript.Controls.PhotoControl.PhotoStub"></value>
        return this._photoSet$1[this._selectedIndex$1];
    },
    _displayMakeFrontPageOptions$1: false,
    _onRolloverMouseOverTextChanged: null,
    _addToCompetitionGroupPhoto$1: function SpottedScript_Controls_PhotoControl_Controller$_addToCompetitionGroupPhoto$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        Spotted.WebServices.Controls.PhotoControl.Service.setAsCompetitionGroupPhoto(this.get_currentPhoto().k, !this.get_currentPhoto().isInCompetitionGroup, Function.createDelegate(this, this._addToCompetitionGroupPhotoSuccess$1), null, !this.get_currentPhoto().isInCompetitionGroup, -1);
    },
    _addToCompetitionGroupPhotoSuccess$1: function SpottedScript_Controls_PhotoControl_Controller$_addToCompetitionGroupPhotoSuccess$1(nullObject, isInCompetitionGroup, methodName) {
        /// <param name="nullObject" type="Object">
        /// </param>
        /// <param name="isInCompetitionGroup" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.get_currentPhoto().isInCompetitionGroup = isInCompetitionGroup;
        this._setAddToCompetitionGroupImgSrc$1();
    },
    _setAddToCompetitionGroupImgSrc$1: function SpottedScript_Controls_PhotoControl_Controller$_setAddToCompetitionGroupImgSrc$1() {
        if (this.get_currentPhoto().canEnterCompetition) {
            this._view$1.get_uiAddToCompetitionGroup().style.display = '';
            this._view$1.get_uiAddToCompetitionGroupImg().src = (this.get_currentPhoto().isInCompetitionGroup) ? this._view$1.get_uiAddToCompetitionGroupImgRemoveButtonUrl().value : this._view$1.get_uiAddToCompetitionGroupImgAddButtonUrl().value;
        }
        else {
            this._view$1.get_uiAddToCompetitionGroup().style.display = 'none';
        }
    },
    isGallerySelectedChanged: function SpottedScript_Controls_PhotoControl_Controller$isGallerySelectedChanged(gallerySelected) {
        /// <param name="gallerySelected" type="Boolean">
        /// </param>
        this._view$1.get_uiPhotoGalleryLinkHolder().style.display = (gallerySelected) ? 'none' : '';
    },
    _setDetails$1: function SpottedScript_Controls_PhotoControl_Controller$_setDetails$1() {
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
        Spotted.WebServices.Controls.PhotoControl.Service.incrementViews(photo.k, null, null, null, -1);
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
    _setSpottedToggleButtonText$1: function SpottedScript_Controls_PhotoControl_Controller$_setSpottedToggleButtonText$1() {
        if (this._view$1.get_uiUsrSpottedToggleButton() != null) {
            this._view$1.get_uiUsrSpottedToggleButton().innerHTML = (this.get_currentPhoto().usrIsInPhoto) ? 'I\'m not in this photo' : 'I\'ve been spotted!';
        }
    },
    _photoSetChanged: function SpottedScript_Controls_PhotoControl_Controller$_photoSetChanged(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (!Boolean.parse(this._view$1.get_uiFirstTimeLoading().value)) {
            SpottedScript.Controls.Banners.Generator.Controller.refreshAllBanners();
        }
        this._setPhotoSet$1((e).photoSet, (e).selectedIndex);
    },
    _setPhotoSet$1: function SpottedScript_Controls_PhotoControl_Controller$_setPhotoSet$1(photoSet, selectedIndex) {
        /// <param name="photoSet" type="Array" elementType="PhotoStub">
        /// </param>
        /// <param name="selectedIndex" type="Number" integer="true">
        /// </param>
        this._photoSet$1 = photoSet;
        this._photoChanged(this, new SpottedScript.IntEventArgs(selectedIndex));
        var photoKs = new Array(photoSet.length);
        for (var i = 0; i < photoKs.length; i++) {
            photoKs[i] = photoSet[i].k;
        }
        window.setTimeout(Function.createDelegate(this, this._preloadAllImages$1), 1000);
    },
    _onPhotoChanged: null,
    _photoChanged: function SpottedScript_Controls_PhotoControl_Controller$_photoChanged(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
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
            if (this._onPhotoChanged != null) {
                this._onPhotoChanged(this, new SpottedScript.Controls.PhotoBrowser.PhotoEventArgs(this.get_currentPhoto()));
            }
        }
        this._view$1.get_uiPhoto().focus();
    },
    _onPhotoChangedAfterDelay: null,
    _delayCount$1: 0,
    _delayMilliseconds$1: 500,
    _triggerDelayEvents$1: function SpottedScript_Controls_PhotoControl_Controller$_triggerDelayEvents$1() {
        this._delayCount$1++;
        window.setTimeout(Function.createDelegate(this, this._testDelayCount$1), this._delayMilliseconds$1);
    },
    _testDelayCount$1: function SpottedScript_Controls_PhotoControl_Controller$_testDelayCount$1() {
        this._delayCount$1--;
        if (this._delayCount$1 <= 0) {
            this._doDelayEvents$1();
        }
    },
    _doDelayEvents$1: function SpottedScript_Controls_PhotoControl_Controller$_doDelayEvents$1() {
        if (this._onPhotoChangedAfterDelay != null) {
            this._onPhotoChangedAfterDelay(this, new SpottedScript.Controls.PhotoBrowser.PhotoEventArgs(this.get_currentPhoto()));
        }
    },
    _nextPhotoClick$1: function SpottedScript_Controls_PhotoControl_Controller$_nextPhotoClick$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._onPhotoNextClick != null) {
            this._onPhotoNextClick(this, Sys.EventArgs.Empty);
        }
        this._view$1.get_uiPhoto().focus();
    },
    _prevPhotoClick$1: function SpottedScript_Controls_PhotoControl_Controller$_prevPhotoClick$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._onPhotoPrevClick != null) {
            this._onPhotoPrevClick(this, Sys.EventArgs.Empty);
        }
    },
    _preloadAllImages$1: function SpottedScript_Controls_PhotoControl_Controller$_preloadAllImages$1() {
        for (var i = 0; i < this._photoSet$1.length; i++) {
            if (this._photoSet$1[i] != null) {
                var img = document.createElement('img');
                img.src = this._photoSet$1[i].webPath;
            }
        }
    },
    _useAsProfilePictureButtonClick$1: function SpottedScript_Controls_PhotoControl_Controller$_useAsProfilePictureButtonClick$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(Function.createDelegate(this, function() {
            this._useAsProfilePictureButtonClickInner$1();
        }));
    },
    _useAsProfilePictureButtonClickInner$1: function SpottedScript_Controls_PhotoControl_Controller$_useAsProfilePictureButtonClickInner$1() {
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
    _redirectToProfilePhotoPage$1: function SpottedScript_Controls_PhotoControl_Controller$_redirectToProfilePhotoPage$1() {
        eval('window.location = \'/pages/mypicture/type-pic/k-' + this.get_currentPhoto().k + '\'');
    },
    _buddySpottedButtonClick$1: function SpottedScript_Controls_PhotoControl_Controller$_buddySpottedButtonClick$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(Function.createDelegate(this, function() {
            this._buddySpottedButtonClickInner$1();
        }));
    },
    _buddySpottedButtonClickInner$1: function SpottedScript_Controls_PhotoControl_Controller$_buddySpottedButtonClickInner$1() {
        var usrK = 0;
        try {
            usrK = Number.parseInvariant(this._view$1.get_uiBuddyChooser().get__value());
        }
        catch ($e1) {
        }
        if (usrK > 0) {
            this._view$1.get_uiBuddyValidator().style.display = 'none';
            this._view$1.get_uiBuddyChooser().set__text('');
            this._view$1.get_uiBuddyChooser().set__value('');
            this._setUsrSpottedInPhoto$1(usrK, this.get_currentPhoto().k, true);
        }
        else {
            this._view$1.get_uiBuddyValidator().style.display = '';
        }
    },
    _usrSpottedToggleButtonClick$1: function SpottedScript_Controls_PhotoControl_Controller$_usrSpottedToggleButtonClick$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(Function.createDelegate(this, function() {
            this._usrSpottedToggleButtonClickInner$1();
        }));
    },
    _usrSpottedToggleButtonClickInner$1: function SpottedScript_Controls_PhotoControl_Controller$_usrSpottedToggleButtonClickInner$1() {
        this._setUsrSpottedInPhoto$1(-1, this.get_currentPhoto().k, !this.get_currentPhoto().usrIsInPhoto);
    },
    _isFavouritePhotoToggleButtonClick$1: function SpottedScript_Controls_PhotoControl_Controller$_isFavouritePhotoToggleButtonClick$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(Function.createDelegate(this, function() {
            this._isFavouritePhotoToggleButtonClickInner$1();
        }));
    },
    _isFavouritePhotoToggleButtonClickInner$1: function SpottedScript_Controls_PhotoControl_Controller$_isFavouritePhotoToggleButtonClickInner$1() {
        Spotted.WebServices.Controls.PhotoControl.Service.setIsFavouritePhoto(this.get_currentPhoto().k, !this.get_currentPhoto().isFavourite, Function.createDelegate(this, this._isFavouritePhotoToggleButtonClickSuccessCallback$1), Function.createDelegate(null, Utils.Trace.webServiceFailure), !this.get_currentPhoto().isFavourite, -1);
    },
    _isFavouritePhotoToggleButtonClickSuccessCallback$1: function SpottedScript_Controls_PhotoControl_Controller$_isFavouritePhotoToggleButtonClickSuccessCallback$1(nullObject, isFavourite, methodName) {
        /// <param name="nullObject" type="Object">
        /// </param>
        /// <param name="isFavourite" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.get_currentPhoto().isFavourite = isFavourite;
        this._setIsFavouritePhotoToggleButtonText$1();
    },
    _setIsFavouritePhotoToggleButtonText$1: function SpottedScript_Controls_PhotoControl_Controller$_setIsFavouritePhotoToggleButtonText$1() {
        if (this._view$1.get_uiIsFavouritePhotoToggleButton() != null) {
            this._view$1.get_uiIsFavouritePhotoToggleButton().innerHTML = (this.get_currentPhoto().isFavourite) ? 'Remove from my favourites' : 'Add to my favourites';
        }
    },
    _setUsrSpottedInPhoto$1: function SpottedScript_Controls_PhotoControl_Controller$_setUsrSpottedInPhoto$1(spottedUsrK, photoK, isInPhoto) {
        /// <param name="spottedUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="photoK" type="Number" integer="true">
        /// </param>
        /// <param name="isInPhoto" type="Boolean">
        /// </param>
        if (spottedUsrK > 0) {
            Spotted.WebServices.Controls.PhotoControl.Service.setUsrSpottedInPhoto(spottedUsrK, photoK, isInPhoto, Function.createDelegate(this, this._setUsrSpottedInPhotoSuccessCallback$1), Function.createDelegate(null, Utils.Trace.webServiceFailure), false, -1);
        }
        else {
            Spotted.WebServices.Controls.PhotoControl.Service.setCurrentUsrSpottedInPhoto(photoK, isInPhoto, Function.createDelegate(this, this._setCurrentUsrSpottedInPhotoSuccessCallback$1), Function.createDelegate(null, Utils.Trace.webServiceFailure), isInPhoto, -1);
        }
    },
    _setUsrSpottedInPhotoSuccessCallback$1: function SpottedScript_Controls_PhotoControl_Controller$_setUsrSpottedInPhotoSuccessCallback$1(newTexts, userContext, methodName) {
        /// <param name="newTexts" type="Array" elementType="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._setUsrInPhotoDetails$1(newTexts[0], newTexts[1]);
    },
    _setCurrentUsrSpottedInPhotoSuccessCallback$1: function SpottedScript_Controls_PhotoControl_Controller$_setCurrentUsrSpottedInPhotoSuccessCallback$1(newTexts, usrIsInPhoto, methodName) {
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
    _setUsrInPhotoDetails$1: function SpottedScript_Controls_PhotoControl_Controller$_setUsrInPhotoDetails$1(usrsInPhotoHtml, rolloverMouseOverText) {
        /// <param name="usrsInPhotoHtml" type="String">
        /// </param>
        /// <param name="rolloverMouseOverText" type="String">
        /// </param>
        this.get_currentPhoto().usrsInPhotoHtml = usrsInPhotoHtml;
        this.get_currentPhoto().rolloverMouseOverText = rolloverMouseOverText;
        this._setUsrsInPhoto$1();
        if (this._onRolloverMouseOverTextChanged != null) {
            this._onRolloverMouseOverTextChanged(this, new SpottedScript.IntEventArgs(this._selectedIndex$1));
        }
    },
    _setUsrsInPhoto$1: function SpottedScript_Controls_PhotoControl_Controller$_setUsrsInPhoto$1() {
        this._view$1.get_uiUsrsInPhotoSpan().style.display = (this.get_currentPhoto().usrsInPhotoHtml.length > 0) ? '' : 'none';
        this._view$1.get_uiUsrsInPhotoSpan().innerHTML = this.get_currentPhoto().usrsInPhotoHtml;
    },
    _showVideo$1: function SpottedScript_Controls_PhotoControl_Controller$_showVideo$1() {
        this._view$1.get_uiFlashHolder().style.display = '';
        var p = this.get_currentPhoto();
        eval(String.format('var PhotoBrowser_so = new SWFObject(\'/misc/flvplayer.swf\', \'PhotoBrowser_mymovie\', {0}, {1}, 7, \'#FFFFFF\');\r\nPhotoBrowser_so.addParam(\'wmode\', \'transparent\');\r\nPhotoBrowser_so.addVariable(\'file\', \'{2}\');\r\nPhotoBrowser_so.addVariable(\'jpg\', \'{3}\');\r\nPhotoBrowser_so.addVariable(\'autoStart\', \'0\');\r\nPhotoBrowser_so.write(\'{4}\');', p.videoMedWidth, p.videoMedHeight + 20, p.videoMedPath, p.webPath, this._view$1.get_uiFlashHolder().id));
    },
    _hideVideo$1: function SpottedScript_Controls_PhotoControl_Controller$_hideVideo$1() {
        this._view$1.get_uiFlashHolder().style.display = 'none';
    },
    _hidePhoto$1: function SpottedScript_Controls_PhotoControl_Controller$_hidePhoto$1() {
        this._view$1.get_uiPhotoHolder().style.display = 'none';
    },
    _showPhoto$1: function SpottedScript_Controls_PhotoControl_Controller$_showPhoto$1() {
        this._view$1.get_uiPhoto().src = '/gfx/1pix-black.gif';
        window.setTimeout(Function.createDelegate(this, this._showPhoto2$1), 0);
    },
    _showPhoto2$1: function SpottedScript_Controls_PhotoControl_Controller$_showPhoto2$1() {
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
    _showNewBanner$1: function SpottedScript_Controls_PhotoControl_Controller$_showNewBanner$1() {
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
    _showBanner$1: function SpottedScript_Controls_PhotoControl_Controller$_showBanner$1() {
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
        Spotted.WebServices.Controls.PhotoControl.Service.registerBannerHit(this._banners$1[this._currentBannerIndex$1].k, null, null, null, -1);
    },
    _getBannersAndSetCurrentBanner$1: function SpottedScript_Controls_PhotoControl_Controller$_getBannersAndSetCurrentBanner$1() {
        Spotted.WebServices.Controls.PhotoControl.Service.getBanners(this._view$1.get_uiBannerPlaceHolder().id, Function.createDelegate(this, this._getNewBannerHtmlAndRegisterHitSuccessCallback$1), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, -1);
    },
    _getNewBannerHtmlAndRegisterHitSuccessCallback$1: function SpottedScript_Controls_PhotoControl_Controller$_getNewBannerHtmlAndRegisterHitSuccessCallback$1(banners, context, methodName) {
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
// SpottedScript.Controls.PhotoControl.View
SpottedScript.Controls.PhotoControl.View = function SpottedScript_Controls_PhotoControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.PhotoControl.View.prototype = {
    clientId: null,
    get_uiContent: function SpottedScript_Controls_PhotoControl_View$get_uiContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiContent');
    },
    get_uiPrevPhotoButtonDiv: function SpottedScript_Controls_PhotoControl_View$get_uiPrevPhotoButtonDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPrevPhotoButtonDiv');
    },
    get_uiPrevPhotoButton: function SpottedScript_Controls_PhotoControl_View$get_uiPrevPhotoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPrevPhotoButton');
    },
    get_uiNextPhotoButtonDiv: function SpottedScript_Controls_PhotoControl_View$get_uiNextPhotoButtonDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNextPhotoButtonDiv');
    },
    get_uiNextPhotoButton: function SpottedScript_Controls_PhotoControl_View$get_uiNextPhotoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNextPhotoButton');
    },
    get_uiBannerHolder: function SpottedScript_Controls_PhotoControl_View$get_uiBannerHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBannerHolder');
    },
    get_uiBannerPlaceHolder: function SpottedScript_Controls_PhotoControl_View$get_uiBannerPlaceHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBannerPlaceHolder');
    },
    get_bannerPhoto: function SpottedScript_Controls_PhotoControl_View$get_bannerPhoto() {
        /// <value type="SpottedScript.Controls.Banners.Generator.Controller"></value>
        return eval(this.clientId + '_BannerPhotoController');
    },
    get_uiPhotoDiv: function SpottedScript_Controls_PhotoControl_View$get_uiPhotoDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoDiv');
    },
    get_uiPhotoHolder: function SpottedScript_Controls_PhotoControl_View$get_uiPhotoHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoHolder');
    },
    get_uiPhoto: function SpottedScript_Controls_PhotoControl_View$get_uiPhoto() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhoto');
    },
    get_uiPhotoOverlay: function SpottedScript_Controls_PhotoControl_View$get_uiPhotoOverlay() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoOverlay');
    },
    get_uiFlashHolder: function SpottedScript_Controls_PhotoControl_View$get_uiFlashHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFlashHolder');
    },
    get_uiPhotoGalleryLinkHolder: function SpottedScript_Controls_PhotoControl_View$get_uiPhotoGalleryLinkHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoGalleryLinkHolder');
    },
    get_uiPhotoGalleryLink: function SpottedScript_Controls_PhotoControl_View$get_uiPhotoGalleryLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoGalleryLink');
    },
    get_uiTakenByDetailsSpan: function SpottedScript_Controls_PhotoControl_View$get_uiTakenByDetailsSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTakenByDetailsSpan');
    },
    get_uiUsrsInPhotoSpan: function SpottedScript_Controls_PhotoControl_View$get_uiUsrsInPhotoSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUsrsInPhotoSpan');
    },
    get_uiBuddyChooserPanel: function SpottedScript_Controls_PhotoControl_View$get_uiBuddyChooserPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyChooserPanel');
    },
    get_uiUsrSpottedToggleButton: function SpottedScript_Controls_PhotoControl_View$get_uiUsrSpottedToggleButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUsrSpottedToggleButton');
    },
    get_uiUseAsProfilePictureButton: function SpottedScript_Controls_PhotoControl_View$get_uiUseAsProfilePictureButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUseAsProfilePictureButton');
    },
    get_uiBuddyChooserPanelInner: function SpottedScript_Controls_PhotoControl_View$get_uiBuddyChooserPanelInner() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyChooserPanelInner');
    },
    get_uiBuddyChooser: function SpottedScript_Controls_PhotoControl_View$get_uiBuddyChooser() {
        /// <value type="SpottedScript.Controls.BuddyChooser.Controller"></value>
        return eval(this.clientId + '_uiBuddyChooserController');
    },
    get_uiBuddySpottedButton: function SpottedScript_Controls_PhotoControl_View$get_uiBuddySpottedButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddySpottedButton');
    },
    get_uiBuddyValidator: function SpottedScript_Controls_PhotoControl_View$get_uiBuddyValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyValidator');
    },
    get_uiCompetitionPanel: function SpottedScript_Controls_PhotoControl_View$get_uiCompetitionPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCompetitionPanel');
    },
    get_uiAddToCompetitionGroup: function SpottedScript_Controls_PhotoControl_View$get_uiAddToCompetitionGroup() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddToCompetitionGroup');
    },
    get_uiAddToCompetitionGroupImg: function SpottedScript_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddToCompetitionGroupImg');
    },
    get_uiCompetitionGroupLink: function SpottedScript_Controls_PhotoControl_View$get_uiCompetitionGroupLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCompetitionGroupLink');
    },
    get_uiAddToCompetitionGroupImgAddButtonUrl: function SpottedScript_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImgAddButtonUrl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddToCompetitionGroupImgAddButtonUrl');
    },
    get_uiAddToCompetitionGroupImgRemoveButtonUrl: function SpottedScript_Controls_PhotoControl_View$get_uiAddToCompetitionGroupImgRemoveButtonUrl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddToCompetitionGroupImgRemoveButtonUrl');
    },
    get_uiQuickBrowserUrl: function SpottedScript_Controls_PhotoControl_View$get_uiQuickBrowserUrl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiQuickBrowserUrl');
    },
    get_uiIsFavouritePhotoToggleButton: function SpottedScript_Controls_PhotoControl_View$get_uiIsFavouritePhotoToggleButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiIsFavouritePhotoToggleButton');
    },
    get_uiSendLink: function SpottedScript_Controls_PhotoControl_View$get_uiSendLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSendLink');
    },
    get_uiReportLink: function SpottedScript_Controls_PhotoControl_View$get_uiReportLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiReportLink');
    },
    get_uiAddToGroupTopPhotosSpan: function SpottedScript_Controls_PhotoControl_View$get_uiAddToGroupTopPhotosSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddToGroupTopPhotosSpan');
    },
    get_uiAddToGroupLink: function SpottedScript_Controls_PhotoControl_View$get_uiAddToGroupLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddToGroupLink');
    },
    get_uiAddToFrontPageSpan: function SpottedScript_Controls_PhotoControl_View$get_uiAddToFrontPageSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddToFrontPageSpan');
    },
    get_uiDownloadPhotoLinkHtml: function SpottedScript_Controls_PhotoControl_View$get_uiDownloadPhotoLinkHtml() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDownloadPhotoLinkHtml');
    },
    get_uiCopyrightUsrLinkSpan: function SpottedScript_Controls_PhotoControl_View$get_uiCopyrightUsrLinkSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCopyrightUsrLinkSpan');
    },
    get_uiPhotoVideoLabel1: function SpottedScript_Controls_PhotoControl_View$get_uiPhotoVideoLabel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoVideoLabel1');
    },
    get_uiLinkToThis: function SpottedScript_Controls_PhotoControl_View$get_uiLinkToThis() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiLinkToThis');
    },
    get_uiPhotoVideoLabel2: function SpottedScript_Controls_PhotoControl_View$get_uiPhotoVideoLabel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoVideoLabel2');
    },
    get_uiEmbedThis: function SpottedScript_Controls_PhotoControl_View$get_uiEmbedThis() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEmbedThis');
    },
    get_photoOfWeekDiv: function SpottedScript_Controls_PhotoControl_View$get_photoOfWeekDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoOfWeekDiv');
    },
    get_div1: function SpottedScript_Controls_PhotoControl_View$get_div1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Div1');
    },
    get_uiPhotoUsage: function SpottedScript_Controls_PhotoControl_View$get_uiPhotoUsage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoUsage');
    },
    get_uiDisplayMakeFrontPageOptions: function SpottedScript_Controls_PhotoControl_View$get_uiDisplayMakeFrontPageOptions() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDisplayMakeFrontPageOptions');
    },
    get_uiUsrIsLoggedIn: function SpottedScript_Controls_PhotoControl_View$get_uiUsrIsLoggedIn() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUsrIsLoggedIn');
    },
    get_uiDisableBanner: function SpottedScript_Controls_PhotoControl_View$get_uiDisableBanner() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDisableBanner');
    },
    get_uiFirstTimeLoading: function SpottedScript_Controls_PhotoControl_View$get_uiFirstTimeLoading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFirstTimeLoading');
    },
    get_uiOverlayEnabled: function SpottedScript_Controls_PhotoControl_View$get_uiOverlayEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiOverlayEnabled');
    },
    get_uiOverlayWidth: function SpottedScript_Controls_PhotoControl_View$get_uiOverlayWidth() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiOverlayWidth');
    },
    get_uiOverlayHeight: function SpottedScript_Controls_PhotoControl_View$get_uiOverlayHeight() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiOverlayHeight');
    }
}
SpottedScript.Controls.PhotoControl.BannerStub.registerClass('SpottedScript.Controls.PhotoControl.BannerStub');
SpottedScript.Controls.PhotoControl.PhotoStub.registerClass('SpottedScript.Controls.PhotoControl.PhotoStub');
SpottedScript.Controls.PhotoControl.PhotoResult.registerClass('SpottedScript.Controls.PhotoControl.PhotoResult');
SpottedScript.Controls.PhotoControl.Controller.registerClass('SpottedScript.Controls.PhotoControl.Controller', SpottedScript.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl);
SpottedScript.Controls.PhotoControl.View.registerClass('SpottedScript.Controls.PhotoControl.View');
