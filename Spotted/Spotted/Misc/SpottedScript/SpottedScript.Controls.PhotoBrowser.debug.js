Type.registerNamespace('SpottedScript.Controls.PhotoBrowser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.Controller
SpottedScript.Controls.PhotoBrowser.Controller = function SpottedScript_Controls_PhotoBrowser_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.PhotoBrowser.View">
    /// </param>
    /// <field name="_view$1" type="SpottedScript.Controls.PhotoBrowser.View">
    /// </field>
    /// <field name="_cells$1" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_iconsPerPage$1" type="Number" integer="true">
    /// </field>
    /// <field name="_iconsPerRow$1" type="Number" integer="true">
    /// </field>
    /// <field name="_iconSize$1" type="Number" integer="true">
    /// </field>
    /// <field name="_photoProvider$1" type="SpottedScript.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    /// <field name="_selectedIndex$1" type="Number" integer="true">
    /// </field>
    /// <field name="_onChangePhoto" type="Sys.EventHandler">
    /// </field>
    /// <field name="_onChangePhotoSet" type="Sys.EventHandler">
    /// </field>
    /// <field name="_photoSetIsLoadingFromServer$1" type="Boolean">
    /// </field>
    /// <field name="_firstLoad$1" type="Boolean">
    /// </field>
    SpottedScript.Controls.PhotoBrowser.Controller.initializeBase(this, [ [ view.get_uiPhotoRepeaterContainer() ] ]);
    this._view$1 = view;
    this._iconsPerRow$1 = Number.parseInvariant(view.get_uiIconsPerRow().value);
    this._iconSize$1 = Number.parseInvariant(view.get_uiIconSize().value);
    this._iconsPerPage$1 = Number.parseInvariant(view.get_uiIconsPerPage().value);
    view.get_uiPaginationControl()._onPageChanged = Function.createDelegate(this, this._loadBrowserItems$1);
    this._cells$1 = new Array(this._iconsPerPage$1);
    for (var i = 0; i < this._cells$1.length; i++) {
        this._cells$1[i] = document.getElementById(view.get_uiTableCellsPrefix().value + i);
    }
    for (var i = 0; i < this._cells$1.length; i++) {
        $addHandler(this._cells$1[i].childNodes[0], 'click', Function.createDelegate(this, this._photoClick$1));
    }
    this.set__selectedIndex(this._highlightedCellIndex$1());
    this._onPhotoNextClick = Function.createDelegate(this, this._moveToNextPhoto);
    this._onPhotoPrevClick = Function.createDelegate(this, this._moveToPreviousPhoto);
    this._onPhotoUpClick = Function.createDelegate(this, this._moveToPhotoAbove);
    this._onPhotoDownClick = Function.createDelegate(this, this._moveToPhotoBelow);
    this._onArrowKeyPress = Function.createDelegate(this, this._arrowKeyPress$1);
}
SpottedScript.Controls.PhotoBrowser.Controller.prototype = {
    _view$1: null,
    _cells$1: null,
    _iconsPerPage$1: 0,
    _iconsPerRow$1: 0,
    _iconSize$1: 0,
    _photoProvider$1: null,
    get__photoProvider: function SpottedScript_Controls_PhotoBrowser_Controller$get__photoProvider() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.PhotoProvider"></value>
        return this._photoProvider$1;
    },
    set__photoProvider: function SpottedScript_Controls_PhotoBrowser_Controller$set__photoProvider(value) {
        /// <value type="SpottedScript.Controls.PhotoBrowser.PhotoProvider"></value>
        this._photoProvider$1 = value;
        return value;
    },
    get__paginationControl: function SpottedScript_Controls_PhotoBrowser_Controller$get__paginationControl() {
        /// <value type="SpottedScript.Controls.PaginationControl2.Controller"></value>
        return this._view$1.get_uiPaginationControl();
    },
    _selectedIndex$1: 0,
    get__selectedIndex: function SpottedScript_Controls_PhotoBrowser_Controller$get__selectedIndex() {
        /// <value type="Number" integer="true"></value>
        return this._selectedIndex$1;
    },
    set__selectedIndex: function SpottedScript_Controls_PhotoBrowser_Controller$set__selectedIndex(value) {
        /// <value type="Number" integer="true"></value>
        this._selectedIndex$1 = value;
        return value;
    },
    _onChangePhoto: null,
    _onChangePhotoSet: null,
    _arrowKeyPress$1: function SpottedScript_Controls_PhotoBrowser_Controller$_arrowKeyPress$1(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
    },
    _photoClick$1: function SpottedScript_Controls_PhotoBrowser_Controller$_photoClick$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        for (var i = 0; i < this._cells$1.length; i++) {
            if (this._cells$1[i] === e.target.parentNode.parentNode) {
                this.set__selectedIndex(i);
                break;
            }
        }
        this._highlightCell$1();
        if (this._onChangePhoto != null) {
            this._onChangePhoto(this, new SpottedScript.IntEventArgs(this.get__selectedIndex()));
        }
    },
    _highlightedCellIndex$1: function SpottedScript_Controls_PhotoBrowser_Controller$_highlightedCellIndex$1() {
        /// <returns type="Number" integer="true"></returns>
        for (var i = 0; i < this._cells$1.length; i++) {
            if (this._cells$1[i].className === 'PhotoBrowserCellHighlight') {
                return i;
            }
        }
        return 0;
    },
    _highlightCell$1: function SpottedScript_Controls_PhotoBrowser_Controller$_highlightCell$1() {
        if (!this._selectedIndexValid$1()) {
            this._correctSelectedIndex$1();
        }
        for (var i = 0; i < this._cells$1.length; i++) {
            this._cells$1[i].className = '';
            var image = this._cells$1[i].childNodes[0].childNodes[1];
            if (image.className !== 'PhotoBrowserImage') {
                image.className = 'PhotoBrowserImage';
                image.style.width = this._iconSize$1.toString() + 'px';
                image.style.height = this._iconSize$1.toString() + 'px';
            }
            var imageBlowUp = this._cells$1[i].childNodes[0].childNodes[2];
            if (imageBlowUp.className !== 'PhotoBrowserImage') {
                imageBlowUp.className = 'PhotoBrowserImage';
                imageBlowUp.style.marginTop = '0px';
                imageBlowUp.style.marginLeft = '0px';
            }
        }
        this._cells$1[this.get__selectedIndex()].className = 'PhotoBrowserCellHighlight';
        var imageHilight = this._cells$1[this.get__selectedIndex()].childNodes[0].childNodes[1];
        imageHilight.className = 'PhotoBrowserImageHighlight';
        imageHilight.style.width = (this._iconSize$1 - 2).toString() + 'px';
        imageHilight.style.height = (this._iconSize$1 - 2).toString() + 'px';
        var imageHilightBlowUp = this._cells$1[this.get__selectedIndex()].childNodes[0].childNodes[2];
        imageHilightBlowUp.className = 'PhotoBrowserImageHighlight';
        imageHilightBlowUp.style.marginTop = '-1px';
        imageHilightBlowUp.style.marginLeft = '-1px';
    },
    _photoSetIsLoadingFromServer$1: false,
    _firstLoad$1: true,
    doPostLoadPhotoSetActions: function SpottedScript_Controls_PhotoBrowser_Controller$doPostLoadPhotoSetActions(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this._photoSetIsLoadingFromServer$1 = false;
        if (!this._firstLoad$1) {
            this._setBrowserPhotos$1();
            this._correctSelectedIndex$1();
            this._highlightCell$1();
        }
        this._firstLoad$1 = false;
        this._view$1.get_uiPaginationControl().set__lastPage(this.get__photoProvider().get_currentPhotoSet().lastPage);
        if (this._onChangePhotoSet != null) {
            this._onChangePhotoSet(this, new SpottedScript.Controls.PhotoBrowser.PhotoSetEventArgs(this.get__photoProvider().get_currentPhotoSet().photos, this.get__selectedIndex()));
        }
    },
    photoSetIsLoadingFromServer: function SpottedScript_Controls_PhotoBrowser_Controller$photoSetIsLoadingFromServer(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this._photoSetIsLoadingFromServer$1 = true;
    },
    _setBrowserPhotos$1: function SpottedScript_Controls_PhotoBrowser_Controller$_setBrowserPhotos$1() {
        for (var i = 0; i < this._cells$1.length; i++) {
            if (i < this.get__photoProvider().get_currentPhotoSet().photos.length) {
                (this._cells$1[i].childNodes[0].childNodes[1]).src = '/gfx/1pix.gif';
            }
            else {
                break;
            }
        }
        for (var i = 0; i < this._cells$1.length; i++) {
            if (i < this.get__photoProvider().get_currentPhotoSet().photos.length) {
                this._cells$1[i].style.display = '';
                this._setRolloverMouseOverText$1(i);
                (this._cells$1[i].childNodes[0].childNodes[1]).src = this.get__photoProvider().get_currentPhotoSet().photos[i].iconPath;
                var blowUp = (this._cells$1[i].childNodes[0].childNodes[2]);
                blowUp.src = this.get__photoProvider().get_currentPhotoSet().photos[i].thumbPath;
                blowUp.style.width = this.get__photoProvider().get_currentPhotoSet().photos[i].thumbWidth.toString() + 'px';
                blowUp.style.height = this.get__photoProvider().get_currentPhotoSet().photos[i].thumbHeight.toString() + 'px';
                blowUp.style.top = (-(this.get__photoProvider().get_currentPhotoSet().photos[i].thumbHeight - 75) / 2).toString() + 'px';
                blowUp.style.left = (-(this.get__photoProvider().get_currentPhotoSet().photos[i].thumbWidth - 75) / 2).toString() + 'px';
            }
            else {
                this._cells$1[i].style.display = 'none';
            }
        }
    },
    _loadBrowserItems$1: function SpottedScript_Controls_PhotoBrowser_Controller$_loadBrowserItems$1(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this.get__photoProvider().loadPhotos((e).value);
    },
    _moveToSelectedIndex$1: function SpottedScript_Controls_PhotoBrowser_Controller$_moveToSelectedIndex$1() {
        this._highlightCell$1();
        if (this._onChangePhoto != null) {
            this._onChangePhoto(this, new SpottedScript.IntEventArgs(this.get__selectedIndex()));
        }
    },
    _selectedIndexValid$1: function SpottedScript_Controls_PhotoBrowser_Controller$_selectedIndexValid$1() {
        /// <returns type="Boolean"></returns>
        return this.get__selectedIndex() >= 0 && this.get__selectedIndex() < this.get__photoProvider().get_currentPhotoSet().photos.length;
    },
    _correctSelectedIndex$1: function SpottedScript_Controls_PhotoBrowser_Controller$_correctSelectedIndex$1() {
        if (this.get__selectedIndex() < 0) {
            this.set__selectedIndex(0);
        }
        else if (this.get__selectedIndex() >= this.get__photoProvider().get_currentPhotoSet().photos.length) {
            this.set__selectedIndex(this.get__photoProvider().get_currentPhotoSet().photos.length - 1);
        }
    },
    _moveToNextPhoto: function SpottedScript_Controls_PhotoBrowser_Controller$_moveToNextPhoto(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (!this._photoSetIsLoadingFromServer$1) {
            this.set__selectedIndex(this.get__selectedIndex() + 1) - 1;
            if (this.get__selectedIndex() >= this.get__photoProvider().get_currentPhotoSet().photos.length || this.get__photoProvider().get_currentPhotoSet().photos[this.get__selectedIndex()] == null) {
                this.set__selectedIndex(0);
                this._view$1.get_uiPaginationControl()._moveToNextPage();
            }
            else {
                this._moveToSelectedIndex$1();
            }
        }
    },
    _moveToPreviousPhoto: function SpottedScript_Controls_PhotoBrowser_Controller$_moveToPreviousPhoto(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (!this._photoSetIsLoadingFromServer$1) {
            this.set__selectedIndex(this.get__selectedIndex() - 1) + 1;
            if (this.get__selectedIndex() < 0) {
                this.set__selectedIndex(this.get__selectedIndex() + this._cells$1.length);
                this._view$1.get_uiPaginationControl()._moveToPreviousPage();
            }
            else {
                this._moveToSelectedIndex$1();
            }
        }
    },
    _moveToPhotoAbove: function SpottedScript_Controls_PhotoBrowser_Controller$_moveToPhotoAbove(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this.set__selectedIndex(this.get__selectedIndex() - this._iconsPerRow$1);
        if (this.get__selectedIndex() < 0) {
            this.set__selectedIndex(this.get__selectedIndex() + this._cells$1.length);
            this._view$1.get_uiPaginationControl()._moveToPreviousPage();
        }
        else {
            this._moveToSelectedIndex$1();
        }
    },
    _moveToPhotoBelow: function SpottedScript_Controls_PhotoBrowser_Controller$_moveToPhotoBelow(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this.get__selectedIndex() === this.get__photoProvider().get_currentPhotoSet().photos.length - 1 || this.get__selectedIndex() + this._iconsPerRow$1 >= this._cells$1.length) {
            this._setSelectedIndexToBeInFirstRow$1();
            this._view$1.get_uiPaginationControl()._moveToNextPage();
        }
        else {
            this.set__selectedIndex(this.get__selectedIndex() + this._iconsPerRow$1);
            this._moveToSelectedIndex$1();
        }
    },
    _setSelectedIndexToBeInFirstRow$1: function SpottedScript_Controls_PhotoBrowser_Controller$_setSelectedIndexToBeInFirstRow$1() {
        while (this.get__selectedIndex() < 0) {
            this.set__selectedIndex(this.get__selectedIndex() + this._iconsPerRow$1);
        }
        while (this.get__selectedIndex() >= this._iconsPerRow$1) {
            this.set__selectedIndex(this.get__selectedIndex() - this._iconsPerRow$1);
        }
    },
    _rolloverMouseOverTextChanged: function SpottedScript_Controls_PhotoBrowser_Controller$_rolloverMouseOverTextChanged(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var index = (e).value;
        this._setRolloverMouseOverText$1(index);
    },
    _setRolloverMouseOverText$1: function SpottedScript_Controls_PhotoBrowser_Controller$_setRolloverMouseOverText$1(index) {
        /// <param name="index" type="Number" integer="true">
        /// </param>
        this._cells$1[index].childNodes[0].childNodes[0].setAttribute('rolloverMouseOverText', this.get__photoProvider().get_currentPhotoSet().photos[index].rolloverMouseOverText);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.PhotoEventArgs
SpottedScript.Controls.PhotoBrowser.PhotoEventArgs = function SpottedScript_Controls_PhotoBrowser_PhotoEventArgs(photo) {
    /// <param name="photo" type="SpottedScript.Controls.PhotoControl.PhotoStub">
    /// </param>
    /// <field name="photo" type="SpottedScript.Controls.PhotoControl.PhotoStub">
    /// </field>
    SpottedScript.Controls.PhotoBrowser.PhotoEventArgs.initializeBase(this);
    this.photo = photo;
}
SpottedScript.Controls.PhotoBrowser.PhotoEventArgs.prototype = {
    photo: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.PhotoSetEventArgs
SpottedScript.Controls.PhotoBrowser.PhotoSetEventArgs = function SpottedScript_Controls_PhotoBrowser_PhotoSetEventArgs(photoSet, selectedIndex) {
    /// <param name="photoSet" type="Array" elementType="PhotoStub">
    /// </param>
    /// <param name="selectedIndex" type="Number" integer="true">
    /// </param>
    /// <field name="photoSet" type="Array" elementType="PhotoStub">
    /// </field>
    /// <field name="selectedIndex" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.PhotoBrowser.PhotoSetEventArgs.initializeBase(this);
    this.photoSet = photoSet;
    this.selectedIndex = selectedIndex;
}
SpottedScript.Controls.PhotoBrowser.PhotoSetEventArgs.prototype = {
    photoSet: null,
    selectedIndex: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl
SpottedScript.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl = function SpottedScript_Controls_PhotoBrowser_PhotoBrowsingUsingKeysControl(focusControls) {
    /// <param name="focusControls" type="Array" elementType="Object" elementDomElement="true">
    /// </param>
    /// <field name="_onPhotoNextClick" type="Sys.EventHandler">
    /// </field>
    /// <field name="_onPhotoPrevClick" type="Sys.EventHandler">
    /// </field>
    /// <field name="_onPhotoUpClick" type="Sys.EventHandler">
    /// </field>
    /// <field name="_onPhotoDownClick" type="Sys.EventHandler">
    /// </field>
    /// <field name="_onArrowKeyPress" type="Sys.EventHandler">
    /// </field>
    for (var i = 0; i < focusControls.length; i++) {
        $addHandler(focusControls[i], 'keydown', Function.createDelegate(this, this.keyDown));
    }
}
SpottedScript.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl.prototype = {
    _onPhotoNextClick: null,
    _onPhotoPrevClick: null,
    _onPhotoUpClick: null,
    _onPhotoDownClick: null,
    _onArrowKeyPress: null,
    keyDown: function SpottedScript_Controls_PhotoBrowser_PhotoBrowsingUsingKeysControl$keyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode === Sys.UI.Key.right) {
            if (this._onArrowKeyPress != null) {
                this._onArrowKeyPress(this, Sys.EventArgs.Empty);
            }
            if (this._onPhotoNextClick != null) {
                this._onPhotoNextClick(this, Sys.EventArgs.Empty);
            }
            e.preventDefault();
        }
        else if (e.keyCode === Sys.UI.Key.left) {
            if (this._onArrowKeyPress != null) {
                this._onArrowKeyPress(this, Sys.EventArgs.Empty);
            }
            if (this._onPhotoPrevClick != null) {
                this._onPhotoPrevClick(this, Sys.EventArgs.Empty);
            }
            e.preventDefault();
        }
        else {
            this.nonArrowKeyDown(e);
        }
    },
    nonArrowKeyDown: function SpottedScript_Controls_PhotoBrowser_PhotoBrowsingUsingKeysControl$nonArrowKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.PhotoProvider
SpottedScript.Controls.PhotoBrowser.PhotoProvider = function SpottedScript_Controls_PhotoBrowser_PhotoProvider() {
    /// <field name="_currentPhotoSet" type="SpottedScript.Controls.PhotoControl.PhotoResult">
    /// </field>
    /// <field name="doPostLoadPhotoSetActions" type="Sys.EventHandler">
    /// </field>
    /// <field name="photoSetIsLoadingFromServer" type="Sys.EventHandler">
    /// </field>
}
SpottedScript.Controls.PhotoBrowser.PhotoProvider.prototype = {
    _currentPhotoSet: null,
    get_currentPhotoSet: function SpottedScript_Controls_PhotoBrowser_PhotoProvider$get_currentPhotoSet() {
        /// <value type="SpottedScript.Controls.PhotoControl.PhotoResult"></value>
        return this._currentPhotoSet;
    },
    set_currentPhotoSet: function SpottedScript_Controls_PhotoBrowser_PhotoProvider$set_currentPhotoSet(value) {
        /// <value type="SpottedScript.Controls.PhotoControl.PhotoResult"></value>
        this._currentPhotoSet = value;
        return value;
    },
    doPostLoadPhotoSetActions: null,
    photoSetIsLoadingFromServer: null,
    successCallback: function SpottedScript_Controls_PhotoBrowser_PhotoProvider$successCallback(result, pageNumber, methodName) {
        /// <param name="result" type="SpottedScript.Controls.PhotoControl.PhotoResult">
        /// </param>
        /// <param name="pageNumber" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.set_currentPhotoSet(result);
        this.storePhotosInCache(this.get_currentPhotoSet(), pageNumber);
        if (this.doPostLoadPhotoSetActions != null) {
            this.doPostLoadPhotoSetActions(this, Sys.EventArgs.Empty);
        }
    },
    loadPhotos: function SpottedScript_Controls_PhotoBrowser_PhotoProvider$loadPhotos(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        if (this.loadPhotosFromCache(pageNumber)) {
            if (this.doPostLoadPhotoSetActions != null) {
                this.doPostLoadPhotoSetActions(this, Sys.EventArgs.Empty);
            }
        }
        else {
            if (this.photoSetIsLoadingFromServer != null) {
                this.photoSetIsLoadingFromServer(this, Sys.EventArgs.Empty);
            }
            this.loadPhotosViaWebRequest(pageNumber);
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.EventPhotoProvider
SpottedScript.Controls.PhotoBrowser.EventPhotoProvider = function SpottedScript_Controls_PhotoBrowser_EventPhotoProvider(galleryK, eventK) {
    /// <param name="galleryK" type="Number" integer="true">
    /// </param>
    /// <param name="eventK" type="Number" integer="true">
    /// </param>
    /// <field name="_photoSetsByGalleryK$1" type="Array">
    /// </field>
    /// <field name="_photoSetsByEventK$1" type="Array">
    /// </field>
    /// <field name="_galleryK$1" type="Number" integer="true">
    /// </field>
    /// <field name="_eventK$1" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.PhotoBrowser.EventPhotoProvider.initializeBase(this);
    this._galleryK$1 = galleryK;
    this._eventK$1 = eventK;
    this._photoSetsByGalleryK$1 = [];
    this._photoSetsByEventK$1 = [];
}
SpottedScript.Controls.PhotoBrowser.EventPhotoProvider.prototype = {
    _photoSetsByGalleryK$1: null,
    _photoSetsByEventK$1: null,
    _galleryK$1: 0,
    _eventK$1: 0,
    loadPhotosViaWebRequest: function SpottedScript_Controls_PhotoBrowser_EventPhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        if (this._galleryK$1 > 0) {
            Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByGalleryAndPage(this._galleryK$1, pageNumber, Function.createDelegate(this, this.successCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), pageNumber, -1);
        }
        else {
            Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByEventAndPage(this._eventK$1, pageNumber, Function.createDelegate(this, this.successCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), pageNumber, -1);
        }
    },
    loadPhotosFromCache: function SpottedScript_Controls_PhotoBrowser_EventPhotoProvider$loadPhotosFromCache(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this._galleryK$1 > 0) {
            if ((this._photoSetsByGalleryK$1[this._galleryK$1] != null) && ((this._photoSetsByGalleryK$1[this._galleryK$1])[pageNumber] != null)) {
                this.set_currentPhotoSet((this._photoSetsByGalleryK$1[this._galleryK$1])[pageNumber]);
                return true;
            }
        }
        else if (this._eventK$1 > 0 && (this._photoSetsByEventK$1[this._eventK$1] != null) && ((this._photoSetsByEventK$1[this._eventK$1])[pageNumber] != null)) {
            this.set_currentPhotoSet((this._photoSetsByEventK$1[this._eventK$1])[pageNumber]);
            return true;
        }
        return false;
    },
    storePhotosInCache: function SpottedScript_Controls_PhotoBrowser_EventPhotoProvider$storePhotosInCache(photos, pageNumber) {
        /// <param name="photos" type="SpottedScript.Controls.PhotoControl.PhotoResult">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this._galleryK$1 > 0) {
            if (this._photoSetsByGalleryK$1[this._galleryK$1] == null) {
                this._photoSetsByGalleryK$1[this._galleryK$1] = [];
            }
            (this._photoSetsByGalleryK$1[this._galleryK$1])[pageNumber] = photos;
            return true;
        }
        if (this._eventK$1 > 0) {
            if (this._photoSetsByEventK$1[this._eventK$1] == null) {
                this._photoSetsByEventK$1[this._eventK$1] = [];
            }
            (this._photoSetsByEventK$1[this._eventK$1])[pageNumber] = photos;
            return true;
        }
        return false;
    },
    _setGallery: function SpottedScript_Controls_PhotoBrowser_EventPhotoProvider$_setGallery(galleryK) {
        /// <param name="galleryK" type="Number" integer="true">
        /// </param>
        this._galleryK$1 = galleryK;
        this.loadPhotos(1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider
SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider = function SpottedScript_Controls_PhotoBrowser_SingleKeyPhotoProvider(key) {
    /// <param name="key" type="Number" integer="true">
    /// </param>
    /// <field name="_photoSets$1" type="Array">
    /// </field>
    /// <field name="key" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider.initializeBase(this);
    this.key = key;
    this._photoSets$1 = [];
}
SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider.prototype = {
    _photoSets$1: null,
    key: 0,
    storePhotosInCache: function SpottedScript_Controls_PhotoBrowser_SingleKeyPhotoProvider$storePhotosInCache(photoSet, pageNumber) {
        /// <param name="photoSet" type="SpottedScript.Controls.PhotoControl.PhotoResult">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this.key > 0) {
            if (this._photoSets$1[this.key] == null) {
                this._photoSets$1[this.key] = [];
            }
            (this._photoSets$1[this.key])[pageNumber] = photoSet;
            return true;
        }
        return false;
    },
    loadPhotosFromCache: function SpottedScript_Controls_PhotoBrowser_SingleKeyPhotoProvider$loadPhotosFromCache(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this.key > 0 && this._photoSets$1[this.key] != null && (this._photoSets$1[this.key])[pageNumber] != null) {
            this.set_currentPhotoSet((this._photoSets$1[this.key])[pageNumber]);
            return true;
        }
        return false;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.ArticlePhotoProvider
SpottedScript.Controls.PhotoBrowser.ArticlePhotoProvider = function SpottedScript_Controls_PhotoBrowser_ArticlePhotoProvider(k) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    SpottedScript.Controls.PhotoBrowser.ArticlePhotoProvider.initializeBase(this, [ k ]);
}
SpottedScript.Controls.PhotoBrowser.ArticlePhotoProvider.prototype = {
    loadPhotosViaWebRequest: function SpottedScript_Controls_PhotoBrowser_ArticlePhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByArticle(this.key, pageNumber, Function.createDelegate(this, this.successCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), pageNumber, -1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.TagPhotoProvider
SpottedScript.Controls.PhotoBrowser.TagPhotoProvider = function SpottedScript_Controls_PhotoBrowser_TagPhotoProvider(k) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    SpottedScript.Controls.PhotoBrowser.TagPhotoProvider.initializeBase(this, [ k ]);
}
SpottedScript.Controls.PhotoBrowser.TagPhotoProvider.prototype = {
    loadPhotosViaWebRequest: function SpottedScript_Controls_PhotoBrowser_TagPhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByTag(this.key, pageNumber, Function.createDelegate(this, this.successCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), pageNumber, -1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.GroupPhotoProvider
SpottedScript.Controls.PhotoBrowser.GroupPhotoProvider = function SpottedScript_Controls_PhotoBrowser_GroupPhotoProvider(k) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    SpottedScript.Controls.PhotoBrowser.GroupPhotoProvider.initializeBase(this, [ k ]);
}
SpottedScript.Controls.PhotoBrowser.GroupPhotoProvider.prototype = {
    loadPhotosViaWebRequest: function SpottedScript_Controls_PhotoBrowser_GroupPhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Spotted.WebServices.Controls.PhotoControl.Service.getPhotosByGroup(this.key, pageNumber, Function.createDelegate(this, this.successCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), pageNumber, -1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.PhotosOfUsrProvider
SpottedScript.Controls.PhotoBrowser.PhotosOfUsrProvider = function SpottedScript_Controls_PhotoBrowser_PhotosOfUsrProvider(k, spottedByUsrK) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    /// <param name="spottedByUsrK" type="Number" integer="true">
    /// </param>
    /// <field name="_spottedByUsrK$2" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.PhotoBrowser.PhotosOfUsrProvider.initializeBase(this, [ k ]);
    this._spottedByUsrK$2 = spottedByUsrK;
}
SpottedScript.Controls.PhotoBrowser.PhotosOfUsrProvider.prototype = {
    _spottedByUsrK$2: 0,
    loadPhotosViaWebRequest: function SpottedScript_Controls_PhotoBrowser_PhotosOfUsrProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Spotted.WebServices.Controls.PhotoControl.Service.getPhotosOfUsr(this.key, pageNumber, this._spottedByUsrK$2, Function.createDelegate(this, this.successCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), pageNumber, -1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider
SpottedScript.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider = function SpottedScript_Controls_PhotoBrowser_FavouritePhotosOfUsrProvider(k) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    SpottedScript.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider.initializeBase(this, [ k ]);
}
SpottedScript.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider.prototype = {
    loadPhotosViaWebRequest: function SpottedScript_Controls_PhotoBrowser_FavouritePhotosOfUsrProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Spotted.WebServices.Controls.PhotoControl.Service.getFavouritePhotosOfUsr(this.key, pageNumber, Function.createDelegate(this, this.successCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), pageNumber, -1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.VideoPhotoProvider
SpottedScript.Controls.PhotoBrowser.VideoPhotoProvider = function SpottedScript_Controls_PhotoBrowser_VideoPhotoProvider() {
    /// <field name="_videos$1" type="Array">
    /// </field>
    SpottedScript.Controls.PhotoBrowser.VideoPhotoProvider.initializeBase(this);
    this._videos$1 = [];
}
SpottedScript.Controls.PhotoBrowser.VideoPhotoProvider.prototype = {
    _videos$1: null,
    storePhotosInCache: function SpottedScript_Controls_PhotoBrowser_VideoPhotoProvider$storePhotosInCache(photoSet, pageNumber) {
        /// <param name="photoSet" type="SpottedScript.Controls.PhotoControl.PhotoResult">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        this._videos$1[pageNumber] = photoSet;
        return true;
    },
    loadPhotosFromCache: function SpottedScript_Controls_PhotoBrowser_VideoPhotoProvider$loadPhotosFromCache(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this._videos$1[pageNumber] != null) {
            this.set_currentPhotoSet(this._videos$1[pageNumber]);
            return true;
        }
        return false;
    },
    loadPhotosViaWebRequest: function SpottedScript_Controls_PhotoBrowser_VideoPhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Spotted.WebServices.Controls.PhotoControl.Service.getRecentVideos(pageNumber, Function.createDelegate(this, this.successCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), pageNumber, -1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.PhotosController
SpottedScript.Controls.PhotoBrowser.PhotosController = function SpottedScript_Controls_PhotoBrowser_PhotosController() {
}
SpottedScript.Controls.PhotoBrowser.PhotosController.prototype = {
    setupController: function SpottedScript_Controls_PhotoBrowser_PhotosController$setupController() {
        this.get_photoProvider().doPostLoadPhotoSetActions = Function.createDelegate(this.get_photoBrowser(), this.get_photoBrowser().doPostLoadPhotoSetActions);
        this.get_photoProvider().photoSetIsLoadingFromServer = Function.createDelegate(this.get_photoBrowser(), this.get_photoBrowser().photoSetIsLoadingFromServer);
        this.get_photoProvider().loadPhotos(this.get_photoBrowser().get__paginationControl().get__currentPage());
        this.get_photoBrowser().set__photoProvider(this.get_photoProvider());
        this.get_photoBrowser()._onChangePhotoSet = Function.createDelegate(this.get_photoControl(), this.get_photoControl()._photoSetChanged);
        this.get_photoBrowser()._onChangePhoto = Function.createDelegate(this.get_photoControl(), this.get_photoControl()._photoChanged);
        this.get_photoControl()._onPhotoNextClick = Function.createDelegate(this.get_photoBrowser(), this.get_photoBrowser()._moveToNextPhoto);
        this.get_photoControl()._onPhotoPrevClick = Function.createDelegate(this.get_photoBrowser(), this.get_photoBrowser()._moveToPreviousPhoto);
        this.get_photoControl()._onPhotoUpClick = Function.createDelegate(this.get_photoBrowser(), this.get_photoBrowser()._moveToPhotoAbove);
        this.get_photoControl()._onPhotoDownClick = Function.createDelegate(this.get_photoBrowser(), this.get_photoBrowser()._moveToPhotoBelow);
        this.get_photoControl()._onPhotoChanged = Function.createDelegate(this, this._photoChanged);
        this.get_photoControl()._onPhotoChangedAfterDelay = Function.createDelegate(this, this._photoChangedAfterDelay);
        this.get_photoControl()._onRolloverMouseOverTextChanged = Function.createDelegate(this.get_photoBrowser(), this.get_photoBrowser()._rolloverMouseOverTextChanged);
        this.get_threadControl().onThreadCreated = Function.createDelegate(this, this._threadCreated);
        this.get_threadControl().onCommentPosted = Function.createDelegate(this, this._commentPosted);
        this.get_threadControl().get_uiCommentsDisplay().onThreadDeleted = Function.createDelegate(this, this._threadDeleted);
    },
    _photoChanged: function SpottedScript_Controls_PhotoBrowser_PhotosController$_photoChanged(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var p = e;
        this.get_threadControl().get_uiCommentsDisplay().setCommentsCount(p.photo.commentsCount);
        this.get_latestChatController()._hide();
    },
    _photoChangedAfterDelay: function SpottedScript_Controls_PhotoBrowser_PhotosController$_photoChangedAfterDelay(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var p = e;
        this.get_threadControl().get_uiCommentsDisplay().showComments(p.photo.threadK, 1);
        this.get_threadControl().set_currentParentObjectK(p.photo.k);
        this.get_latestChatController()._show(p.photo.k);
    },
    _threadCreated: function SpottedScript_Controls_PhotoBrowser_PhotosController$_threadCreated(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this.get_photoControl().get_currentPhoto().threadK === 0) {
            this.get_photoControl().get_currentPhoto().threadK = (e).value;
        }
        this.get_photoControl().get_currentPhoto().commentsCount = 1;
        this.get_latestChatController()._update(o, e);
    },
    _commentPosted: function SpottedScript_Controls_PhotoBrowser_PhotosController$_commentPosted(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this.get_photoControl().get_currentPhoto().threadK === (e).value) {
            this.get_photoControl().get_currentPhoto().commentsCount++;
        }
    },
    _threadDeleted: function SpottedScript_Controls_PhotoBrowser_PhotosController$_threadDeleted(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this.get_photoControl().get_currentPhoto().threadK === (e).value) {
            this.get_photoControl().get_currentPhoto().threadK = 0;
            this.get_photoControl().get_currentPhoto().commentsCount = 0;
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PhotoBrowser.View
SpottedScript.Controls.PhotoBrowser.View = function SpottedScript_Controls_PhotoBrowser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.PhotoBrowser.View.prototype = {
    clientId: null,
    get_uiPhotoRepeaterContainer: function SpottedScript_Controls_PhotoBrowser_View$get_uiPhotoRepeaterContainer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoRepeaterContainer');
    },
    get_uiPaginationControl: function SpottedScript_Controls_PhotoBrowser_View$get_uiPaginationControl() {
        /// <value type="SpottedScript.Controls.PaginationControl2.Controller"></value>
        return eval(this.clientId + '_uiPaginationControlController');
    },
    get_uiPhotoRepeater: function SpottedScript_Controls_PhotoBrowser_View$get_uiPhotoRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoRepeater');
    },
    get_uiBlowUpIconSpan: function SpottedScript_Controls_PhotoBrowser_View$get_uiBlowUpIconSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBlowUpIconSpan');
    },
    get_uiIconsPerPage: function SpottedScript_Controls_PhotoBrowser_View$get_uiIconsPerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiIconsPerPage');
    },
    get_uiIconsPerRow: function SpottedScript_Controls_PhotoBrowser_View$get_uiIconsPerRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiIconsPerRow');
    },
    get_uiIconSize: function SpottedScript_Controls_PhotoBrowser_View$get_uiIconSize() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiIconSize');
    },
    get_uiTableCellsPrefix: function SpottedScript_Controls_PhotoBrowser_View$get_uiTableCellsPrefix() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTableCellsPrefix');
    }
}
SpottedScript.Controls.PhotoBrowser.PhotosController.registerClass('SpottedScript.Controls.PhotoBrowser.PhotosController');
SpottedScript.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl.registerClass('SpottedScript.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl');
SpottedScript.Controls.PhotoBrowser.Controller.registerClass('SpottedScript.Controls.PhotoBrowser.Controller', SpottedScript.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl);
SpottedScript.Controls.PhotoBrowser.PhotoEventArgs.registerClass('SpottedScript.Controls.PhotoBrowser.PhotoEventArgs', Sys.EventArgs);
SpottedScript.Controls.PhotoBrowser.PhotoSetEventArgs.registerClass('SpottedScript.Controls.PhotoBrowser.PhotoSetEventArgs', Sys.EventArgs);
SpottedScript.Controls.PhotoBrowser.PhotoProvider.registerClass('SpottedScript.Controls.PhotoBrowser.PhotoProvider');
SpottedScript.Controls.PhotoBrowser.EventPhotoProvider.registerClass('SpottedScript.Controls.PhotoBrowser.EventPhotoProvider', SpottedScript.Controls.PhotoBrowser.PhotoProvider);
SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider.registerClass('SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider', SpottedScript.Controls.PhotoBrowser.PhotoProvider);
SpottedScript.Controls.PhotoBrowser.ArticlePhotoProvider.registerClass('SpottedScript.Controls.PhotoBrowser.ArticlePhotoProvider', SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider);
SpottedScript.Controls.PhotoBrowser.TagPhotoProvider.registerClass('SpottedScript.Controls.PhotoBrowser.TagPhotoProvider', SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider);
SpottedScript.Controls.PhotoBrowser.GroupPhotoProvider.registerClass('SpottedScript.Controls.PhotoBrowser.GroupPhotoProvider', SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider);
SpottedScript.Controls.PhotoBrowser.PhotosOfUsrProvider.registerClass('SpottedScript.Controls.PhotoBrowser.PhotosOfUsrProvider', SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider);
SpottedScript.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider.registerClass('SpottedScript.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider', SpottedScript.Controls.PhotoBrowser.SingleKeyPhotoProvider);
SpottedScript.Controls.PhotoBrowser.VideoPhotoProvider.registerClass('SpottedScript.Controls.PhotoBrowser.VideoPhotoProvider', SpottedScript.Controls.PhotoBrowser.PhotoProvider);
SpottedScript.Controls.PhotoBrowser.View.registerClass('SpottedScript.Controls.PhotoBrowser.View');
