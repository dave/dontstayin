//! PhotoBrowser.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.PhotoBrowser');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.Controller

Js.Controls.PhotoBrowser.Controller = function Js_Controls_PhotoBrowser_Controller(view) {
    /// <param name="view" type="Js.Controls.PhotoBrowser.View">
    /// </param>
    /// <field name="_view$1" type="Js.Controls.PhotoBrowser.View">
    /// </field>
    /// <field name="_cells$1" type="Array" elementType="Object" elementDomElement="true">
    /// </field>
    /// <field name="_iconsPerPage$1" type="Number" integer="true">
    /// </field>
    /// <field name="_iconsPerRow$1" type="Number" integer="true">
    /// </field>
    /// <field name="_iconSize$1" type="Number" integer="true">
    /// </field>
    /// <field name="_photoProvider$1" type="Js.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    /// <field name="_selectedIndex$1" type="Number" integer="true">
    /// </field>
    /// <field name="onChangePhoto" type="Function">
    /// </field>
    /// <field name="onChangePhotoSet" type="Function">
    /// </field>
    /// <field name="_photoSetIsLoadingFromServer$1" type="Boolean">
    /// </field>
    /// <field name="_firstLoad$1" type="Boolean">
    /// </field>
    Js.Controls.PhotoBrowser.Controller.initializeBase(this, [ [ view.get_uiPhotoRepeaterContainerJ() ] ]);
    this._view$1 = view;
    this._iconsPerRow$1 = parseInt(view.get_uiIconsPerRow().value);
    this._iconSize$1 = parseInt(view.get_uiIconSize().value);
    this._iconsPerPage$1 = parseInt(view.get_uiIconsPerPage().value);
    view.get_uiPaginationControl().onPageChanged = ss.Delegate.create(this, this._loadBrowserItems$1);
    this._cells$1 = new Array(this._iconsPerPage$1);
    for (var i = 0; i < this._cells$1.length; i++) {
        this._cells$1[i] = document.getElementById(view.get_uiTableCellsPrefix().value + i);
    }
    for (var i = 0; i < this._cells$1.length; i++) {
        $(this._cells$1[i].childNodes[0]).click(ss.Delegate.create(this, this._photoClick$1));
    }
    this.set_selectedIndex(this._highlightedCellIndex$1());
    this.onPhotoNextClick = ss.Delegate.create(this, this.moveToNextPhoto);
    this.onPhotoPrevClick = ss.Delegate.create(this, this.moveToPreviousPhoto);
    this.onPhotoUpClick = ss.Delegate.create(this, this.moveToPhotoAbove);
    this.onPhotoDownClick = ss.Delegate.create(this, this.moveToPhotoBelow);
    this.onArrowKeyPress = ss.Delegate.create(this, this._arrowKeyPress$1);
}
Js.Controls.PhotoBrowser.Controller.prototype = {
    _view$1: null,
    _cells$1: null,
    _iconsPerPage$1: 0,
    _iconsPerRow$1: 0,
    _iconSize$1: 0,
    _photoProvider$1: null,
    
    get_photoProvider: function Js_Controls_PhotoBrowser_Controller$get_photoProvider() {
        /// <value type="Js.Controls.PhotoBrowser.PhotoProvider"></value>
        return this._photoProvider$1;
    },
    set_photoProvider: function Js_Controls_PhotoBrowser_Controller$set_photoProvider(value) {
        /// <value type="Js.Controls.PhotoBrowser.PhotoProvider"></value>
        this._photoProvider$1 = value;
        return value;
    },
    
    get_paginationControl: function Js_Controls_PhotoBrowser_Controller$get_paginationControl() {
        /// <value type="Js.Controls.PaginationControl2.Controller"></value>
        return this._view$1.get_uiPaginationControl();
    },
    
    _selectedIndex$1: 0,
    
    get_selectedIndex: function Js_Controls_PhotoBrowser_Controller$get_selectedIndex() {
        /// <value type="Number" integer="true"></value>
        return this._selectedIndex$1;
    },
    set_selectedIndex: function Js_Controls_PhotoBrowser_Controller$set_selectedIndex(value) {
        /// <value type="Number" integer="true"></value>
        this._selectedIndex$1 = value;
        return value;
    },
    
    onChangePhoto: null,
    onChangePhotoSet: null,
    
    _arrowKeyPress$1: function Js_Controls_PhotoBrowser_Controller$_arrowKeyPress$1(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
    },
    
    _photoClick$1: function Js_Controls_PhotoBrowser_Controller$_photoClick$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        for (var i = 0; i < this._cells$1.length; i++) {
            if (this._cells$1[i] === e.target.parentNode.parentNode) {
                this.set_selectedIndex(i);
                break;
            }
        }
        this._highlightCell$1();
        if (this.onChangePhoto != null) {
            this.onChangePhoto(this, new Js.Library.IntEventArgs(this.get_selectedIndex()));
        }
    },
    
    _highlightedCellIndex$1: function Js_Controls_PhotoBrowser_Controller$_highlightedCellIndex$1() {
        /// <returns type="Number" integer="true"></returns>
        for (var i = 0; i < this._cells$1.length; i++) {
            if (this._cells$1[i].className === 'PhotoBrowserCellHighlight') {
                return i;
            }
        }
        return 0;
    },
    
    _highlightCell$1: function Js_Controls_PhotoBrowser_Controller$_highlightCell$1() {
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
        this._cells$1[this.get_selectedIndex()].className = 'PhotoBrowserCellHighlight';
        var imageHilight = this._cells$1[this.get_selectedIndex()].childNodes[0].childNodes[1];
        imageHilight.className = 'PhotoBrowserImageHighlight';
        imageHilight.style.width = (this._iconSize$1 - 2).toString() + 'px';
        imageHilight.style.height = (this._iconSize$1 - 2).toString() + 'px';
        var imageHilightBlowUp = this._cells$1[this.get_selectedIndex()].childNodes[0].childNodes[2];
        imageHilightBlowUp.className = 'PhotoBrowserImageHighlight';
        imageHilightBlowUp.style.marginTop = '-1px';
        imageHilightBlowUp.style.marginLeft = '-1px';
    },
    
    _photoSetIsLoadingFromServer$1: false,
    _firstLoad$1: true,
    
    doPostLoadPhotoSetActions: function Js_Controls_PhotoBrowser_Controller$doPostLoadPhotoSetActions(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._photoSetIsLoadingFromServer$1 = false;
        if (!this._firstLoad$1) {
            this._setBrowserPhotos$1();
            this._correctSelectedIndex$1();
            this._highlightCell$1();
        }
        this._firstLoad$1 = false;
        this._view$1.get_uiPaginationControl().set_lastPage(this.get_photoProvider().get_currentPhotoSet().lastPage);
        if (this.onChangePhotoSet != null) {
            this.onChangePhotoSet(this, new Js.Controls.PhotoBrowser.PhotoSetEventArgs(this.get_photoProvider().get_currentPhotoSet().photos, this.get_selectedIndex()));
        }
    },
    
    photoSetIsLoadingFromServer: function Js_Controls_PhotoBrowser_Controller$photoSetIsLoadingFromServer(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._photoSetIsLoadingFromServer$1 = true;
    },
    
    _setBrowserPhotos$1: function Js_Controls_PhotoBrowser_Controller$_setBrowserPhotos$1() {
        for (var i = 0; i < this._cells$1.length; i++) {
            if (i < this.get_photoProvider().get_currentPhotoSet().photos.length) {
                (this._cells$1[i].childNodes[0].childNodes[1]).src = '/gfx/1pix.gif';
            }
            else {
                break;
            }
        }
        for (var i = 0; i < this._cells$1.length; i++) {
            if (i < this.get_photoProvider().get_currentPhotoSet().photos.length) {
                this._cells$1[i].style.display = '';
                this._setRolloverMouseOverText$1(i);
                (this._cells$1[i].childNodes[0].childNodes[1]).src = this.get_photoProvider().get_currentPhotoSet().photos[i].iconPath;
                var blowUp = (this._cells$1[i].childNodes[0].childNodes[2]);
                blowUp.src = this.get_photoProvider().get_currentPhotoSet().photos[i].thumbPath;
                blowUp.style.width = this.get_photoProvider().get_currentPhotoSet().photos[i].thumbWidth.toString() + 'px';
                blowUp.style.height = this.get_photoProvider().get_currentPhotoSet().photos[i].thumbHeight.toString() + 'px';
                blowUp.style.top = (-(this.get_photoProvider().get_currentPhotoSet().photos[i].thumbHeight - 75) / 2).toString() + 'px';
                blowUp.style.left = (-(this.get_photoProvider().get_currentPhotoSet().photos[i].thumbWidth - 75) / 2).toString() + 'px';
            }
            else {
                this._cells$1[i].style.display = 'none';
            }
        }
    },
    
    _loadBrowserItems$1: function Js_Controls_PhotoBrowser_Controller$_loadBrowserItems$1(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.get_photoProvider().loadPhotos((e).value);
    },
    
    _moveToSelectedIndex$1: function Js_Controls_PhotoBrowser_Controller$_moveToSelectedIndex$1() {
        this._highlightCell$1();
        if (this.onChangePhoto != null) {
            this.onChangePhoto(this, new Js.Library.IntEventArgs(this.get_selectedIndex()));
        }
    },
    
    _selectedIndexValid$1: function Js_Controls_PhotoBrowser_Controller$_selectedIndexValid$1() {
        /// <returns type="Boolean"></returns>
        return this.get_selectedIndex() >= 0 && this.get_selectedIndex() < this.get_photoProvider().get_currentPhotoSet().photos.length;
    },
    
    _correctSelectedIndex$1: function Js_Controls_PhotoBrowser_Controller$_correctSelectedIndex$1() {
        if (this.get_selectedIndex() < 0) {
            this.set_selectedIndex(0);
        }
        else if (this.get_selectedIndex() >= this.get_photoProvider().get_currentPhotoSet().photos.length) {
            this.set_selectedIndex(this.get_photoProvider().get_currentPhotoSet().photos.length - 1);
        }
    },
    
    moveToNextPhoto: function Js_Controls_PhotoBrowser_Controller$moveToNextPhoto(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (!this._photoSetIsLoadingFromServer$1) {
            this.set_selectedIndex(this.get_selectedIndex() + 1) - 1;
            if (this.get_selectedIndex() >= this.get_photoProvider().get_currentPhotoSet().photos.length || this.get_photoProvider().get_currentPhotoSet().photos[this.get_selectedIndex()] == null) {
                this.set_selectedIndex(0);
                this._view$1.get_uiPaginationControl().moveToNextPage();
            }
            else {
                this._moveToSelectedIndex$1();
            }
        }
    },
    
    moveToPreviousPhoto: function Js_Controls_PhotoBrowser_Controller$moveToPreviousPhoto(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (!this._photoSetIsLoadingFromServer$1) {
            this.set_selectedIndex(this.get_selectedIndex() - 1) + 1;
            if (this.get_selectedIndex() < 0) {
                this.set_selectedIndex(this.get_selectedIndex() + this._cells$1.length);
                this._view$1.get_uiPaginationControl().moveToPreviousPage();
            }
            else {
                this._moveToSelectedIndex$1();
            }
        }
    },
    
    moveToPhotoAbove: function Js_Controls_PhotoBrowser_Controller$moveToPhotoAbove(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.set_selectedIndex(this.get_selectedIndex() - this._iconsPerRow$1);
        if (this.get_selectedIndex() < 0) {
            this.set_selectedIndex(this.get_selectedIndex() + this._cells$1.length);
            this._view$1.get_uiPaginationControl().moveToPreviousPage();
        }
        else {
            this._moveToSelectedIndex$1();
        }
    },
    
    moveToPhotoBelow: function Js_Controls_PhotoBrowser_Controller$moveToPhotoBelow(source, e) {
        /// <param name="source" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.get_selectedIndex() === this.get_photoProvider().get_currentPhotoSet().photos.length - 1 || this.get_selectedIndex() + this._iconsPerRow$1 >= this._cells$1.length) {
            this._setSelectedIndexToBeInFirstRow$1();
            this._view$1.get_uiPaginationControl().moveToNextPage();
        }
        else {
            this.set_selectedIndex(this.get_selectedIndex() + this._iconsPerRow$1);
            this._moveToSelectedIndex$1();
        }
    },
    
    _setSelectedIndexToBeInFirstRow$1: function Js_Controls_PhotoBrowser_Controller$_setSelectedIndexToBeInFirstRow$1() {
        while (this.get_selectedIndex() < 0) {
            this.set_selectedIndex(this.get_selectedIndex() + this._iconsPerRow$1);
        }
        while (this.get_selectedIndex() >= this._iconsPerRow$1) {
            this.set_selectedIndex(this.get_selectedIndex() - this._iconsPerRow$1);
        }
    },
    
    rolloverMouseOverTextChanged: function Js_Controls_PhotoBrowser_Controller$rolloverMouseOverTextChanged(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        var index = (e).value;
        this._setRolloverMouseOverText$1(index);
    },
    
    _setRolloverMouseOverText$1: function Js_Controls_PhotoBrowser_Controller$_setRolloverMouseOverText$1(index) {
        /// <param name="index" type="Number" integer="true">
        /// </param>
        this._cells$1[index].childNodes[0].childNodes[0].setAttribute('rolloverMouseOverText', this.get_photoProvider().get_currentPhotoSet().photos[index].rolloverMouseOverText);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.Service

Js.Controls.PhotoBrowser.Service = function Js_Controls_PhotoBrowser_Service() {
}
Js.Controls.PhotoBrowser.Service.getRecentVideos = function Js_Controls_PhotoBrowser_Service$getRecentVideos(pageNumber, success, failure, userContext, timeout) {
    /// <param name="pageNumber" type="Number" integer="true">
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
    p['pageNumber'] = pageNumber;
    var o = Js.Library.WebServiceHelper.options('GetRecentVideos', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetRecentVideos');
    };
    $.ajax(o);
}
Js.Controls.PhotoBrowser.Service.getPhotosByEventAndPage = function Js_Controls_PhotoBrowser_Service$getPhotosByEventAndPage(eventK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="eventK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
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
    p['eventK'] = eventK;
    p['pageNumber'] = pageNumber;
    var o = Js.Library.WebServiceHelper.options('GetPhotosByEventAndPage', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPhotosByEventAndPage');
    };
    $.ajax(o);
}
Js.Controls.PhotoBrowser.Service.getPhotosByGalleryAndPage = function Js_Controls_PhotoBrowser_Service$getPhotosByGalleryAndPage(galleryK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="galleryK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
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
    p['galleryK'] = galleryK;
    p['pageNumber'] = pageNumber;
    var o = Js.Library.WebServiceHelper.options('GetPhotosByGalleryAndPage', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPhotosByGalleryAndPage');
    };
    $.ajax(o);
}
Js.Controls.PhotoBrowser.Service.getPhotosByArticle = function Js_Controls_PhotoBrowser_Service$getPhotosByArticle(articleK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="articleK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
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
    p['articleK'] = articleK;
    p['pageNumber'] = pageNumber;
    var o = Js.Library.WebServiceHelper.options('GetPhotosByArticle', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPhotosByArticle');
    };
    $.ajax(o);
}
Js.Controls.PhotoBrowser.Service.getPhotosOfUsr = function Js_Controls_PhotoBrowser_Service$getPhotosOfUsr(usrK, pageNumber, spottedByUsrK, success, failure, userContext, timeout) {
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="spottedByUsrK" type="Number" integer="true">
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
    p['usrK'] = usrK;
    p['pageNumber'] = pageNumber;
    p['spottedByUsrK'] = spottedByUsrK;
    var o = Js.Library.WebServiceHelper.options('GetPhotosOfUsr', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPhotosOfUsr');
    };
    $.ajax(o);
}
Js.Controls.PhotoBrowser.Service.getFavouritePhotosOfUsr = function Js_Controls_PhotoBrowser_Service$getFavouritePhotosOfUsr(usrK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
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
    p['usrK'] = usrK;
    p['pageNumber'] = pageNumber;
    var o = Js.Library.WebServiceHelper.options('GetFavouritePhotosOfUsr', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetFavouritePhotosOfUsr');
    };
    $.ajax(o);
}
Js.Controls.PhotoBrowser.Service.getPhotosByGroup = function Js_Controls_PhotoBrowser_Service$getPhotosByGroup(groupK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="groupK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
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
    p['groupK'] = groupK;
    p['pageNumber'] = pageNumber;
    var o = Js.Library.WebServiceHelper.options('GetPhotosByGroup', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPhotosByGroup');
    };
    $.ajax(o);
}
Js.Controls.PhotoBrowser.Service.getPhotosByTag = function Js_Controls_PhotoBrowser_Service$getPhotosByTag(tagK, pageNumber, success, failure, userContext, timeout) {
    /// <param name="tagK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
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
    p['tagK'] = tagK;
    p['pageNumber'] = pageNumber;
    var o = Js.Library.WebServiceHelper.options('GetPhotosByTag', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPhotosByTag');
    };
    $.ajax(o);
}
Js.Controls.PhotoBrowser.Service.setAsPhotoOfWeek = function Js_Controls_PhotoBrowser_Service$setAsPhotoOfWeek(photoK, isPhotoOfWeek, photoOfWeekCaption, success, failure, userContext, timeout) {
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="isPhotoOfWeek" type="Boolean">
    /// </param>
    /// <param name="photoOfWeekCaption" type="String">
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
    p['isPhotoOfWeek'] = isPhotoOfWeek;
    p['photoOfWeekCaption'] = photoOfWeekCaption;
    var o = Js.Library.WebServiceHelper.options('SetAsPhotoOfWeek', '/WebServices/Controls/PhotoBrowser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'SetAsPhotoOfWeek');
    };
    $.ajax(o);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.View

Js.Controls.PhotoBrowser.View = function Js_Controls_PhotoBrowser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiPhotoRepeaterContainer" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoRepeaterContainerJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPhotoRepeater" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPhotoRepeaterJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBlowUpIconSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBlowUpIconSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiIconsPerPage" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiIconsPerPageJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiIconsPerRow" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiIconsPerRowJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiIconSize" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiIconSizeJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiTableCellsPrefix" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiTableCellsPrefixJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.PhotoBrowser.View.prototype = {
    clientId: null,
    
    get_uiPhotoRepeaterContainer: function Js_Controls_PhotoBrowser_View$get_uiPhotoRepeaterContainer() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoRepeaterContainer == null) {
            this._uiPhotoRepeaterContainer = document.getElementById(this.clientId + '_uiPhotoRepeaterContainer');
        }
        return this._uiPhotoRepeaterContainer;
    },
    
    _uiPhotoRepeaterContainer: null,
    
    get_uiPhotoRepeaterContainerJ: function Js_Controls_PhotoBrowser_View$get_uiPhotoRepeaterContainerJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoRepeaterContainerJ == null) {
            this._uiPhotoRepeaterContainerJ = $('#' + this.clientId + '_uiPhotoRepeaterContainer');
        }
        return this._uiPhotoRepeaterContainerJ;
    },
    
    _uiPhotoRepeaterContainerJ: null,
    
    get_uiPaginationControl: function Js_Controls_PhotoBrowser_View$get_uiPaginationControl() {
        /// <value type="Js.Controls.PaginationControl2.Controller"></value>
        return eval(this.clientId + '_uiPaginationControlController');
    },
    
    get_uiPhotoRepeater: function Js_Controls_PhotoBrowser_View$get_uiPhotoRepeater() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPhotoRepeater == null) {
            this._uiPhotoRepeater = document.getElementById(this.clientId + '_uiPhotoRepeater');
        }
        return this._uiPhotoRepeater;
    },
    
    _uiPhotoRepeater: null,
    
    get_uiPhotoRepeaterJ: function Js_Controls_PhotoBrowser_View$get_uiPhotoRepeaterJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPhotoRepeaterJ == null) {
            this._uiPhotoRepeaterJ = $('#' + this.clientId + '_uiPhotoRepeater');
        }
        return this._uiPhotoRepeaterJ;
    },
    
    _uiPhotoRepeaterJ: null,
    
    get_uiBlowUpIconSpan: function Js_Controls_PhotoBrowser_View$get_uiBlowUpIconSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBlowUpIconSpan == null) {
            this._uiBlowUpIconSpan = document.getElementById(this.clientId + '_uiBlowUpIconSpan');
        }
        return this._uiBlowUpIconSpan;
    },
    
    _uiBlowUpIconSpan: null,
    
    get_uiBlowUpIconSpanJ: function Js_Controls_PhotoBrowser_View$get_uiBlowUpIconSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBlowUpIconSpanJ == null) {
            this._uiBlowUpIconSpanJ = $('#' + this.clientId + '_uiBlowUpIconSpan');
        }
        return this._uiBlowUpIconSpanJ;
    },
    
    _uiBlowUpIconSpanJ: null,
    
    get_uiIconsPerPage: function Js_Controls_PhotoBrowser_View$get_uiIconsPerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiIconsPerPage == null) {
            this._uiIconsPerPage = document.getElementById(this.clientId + '_uiIconsPerPage');
        }
        return this._uiIconsPerPage;
    },
    
    _uiIconsPerPage: null,
    
    get_uiIconsPerPageJ: function Js_Controls_PhotoBrowser_View$get_uiIconsPerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiIconsPerPageJ == null) {
            this._uiIconsPerPageJ = $('#' + this.clientId + '_uiIconsPerPage');
        }
        return this._uiIconsPerPageJ;
    },
    
    _uiIconsPerPageJ: null,
    
    get_uiIconsPerRow: function Js_Controls_PhotoBrowser_View$get_uiIconsPerRow() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiIconsPerRow == null) {
            this._uiIconsPerRow = document.getElementById(this.clientId + '_uiIconsPerRow');
        }
        return this._uiIconsPerRow;
    },
    
    _uiIconsPerRow: null,
    
    get_uiIconsPerRowJ: function Js_Controls_PhotoBrowser_View$get_uiIconsPerRowJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiIconsPerRowJ == null) {
            this._uiIconsPerRowJ = $('#' + this.clientId + '_uiIconsPerRow');
        }
        return this._uiIconsPerRowJ;
    },
    
    _uiIconsPerRowJ: null,
    
    get_uiIconSize: function Js_Controls_PhotoBrowser_View$get_uiIconSize() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiIconSize == null) {
            this._uiIconSize = document.getElementById(this.clientId + '_uiIconSize');
        }
        return this._uiIconSize;
    },
    
    _uiIconSize: null,
    
    get_uiIconSizeJ: function Js_Controls_PhotoBrowser_View$get_uiIconSizeJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiIconSizeJ == null) {
            this._uiIconSizeJ = $('#' + this.clientId + '_uiIconSize');
        }
        return this._uiIconSizeJ;
    },
    
    _uiIconSizeJ: null,
    
    get_uiTableCellsPrefix: function Js_Controls_PhotoBrowser_View$get_uiTableCellsPrefix() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiTableCellsPrefix == null) {
            this._uiTableCellsPrefix = document.getElementById(this.clientId + '_uiTableCellsPrefix');
        }
        return this._uiTableCellsPrefix;
    },
    
    _uiTableCellsPrefix: null,
    
    get_uiTableCellsPrefixJ: function Js_Controls_PhotoBrowser_View$get_uiTableCellsPrefixJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiTableCellsPrefixJ == null) {
            this._uiTableCellsPrefixJ = $('#' + this.clientId + '_uiTableCellsPrefix');
        }
        return this._uiTableCellsPrefixJ;
    },
    
    _uiTableCellsPrefixJ: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.PhotoProvider

Js.Controls.PhotoBrowser.PhotoProvider = function Js_Controls_PhotoBrowser_PhotoProvider() {
    /// <field name="_currentPhotoSet" type="Js.Controls.PhotoControl.PhotoResult">
    /// </field>
    /// <field name="doPostLoadPhotoSetActions" type="Function">
    /// </field>
    /// <field name="photoSetIsLoadingFromServer" type="Function">
    /// </field>
}
Js.Controls.PhotoBrowser.PhotoProvider.prototype = {
    _currentPhotoSet: null,
    
    get_currentPhotoSet: function Js_Controls_PhotoBrowser_PhotoProvider$get_currentPhotoSet() {
        /// <value type="Js.Controls.PhotoControl.PhotoResult"></value>
        return this._currentPhotoSet;
    },
    set_currentPhotoSet: function Js_Controls_PhotoBrowser_PhotoProvider$set_currentPhotoSet(value) {
        /// <value type="Js.Controls.PhotoControl.PhotoResult"></value>
        this._currentPhotoSet = value;
        return value;
    },
    
    doPostLoadPhotoSetActions: null,
    photoSetIsLoadingFromServer: null,
    
    successCallback: function Js_Controls_PhotoBrowser_PhotoProvider$successCallback(result, pageNumber, methodName) {
        /// <param name="result" type="Js.Controls.PhotoControl.PhotoResult">
        /// </param>
        /// <param name="pageNumber" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.set_currentPhotoSet(result);
        this.storePhotosInCache(this.get_currentPhotoSet(), pageNumber);
        if (this.doPostLoadPhotoSetActions != null) {
            this.doPostLoadPhotoSetActions(this, ss.EventArgs.Empty);
        }
    },
    
    loadPhotos: function Js_Controls_PhotoBrowser_PhotoProvider$loadPhotos(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        if (this.loadPhotosFromCache(pageNumber)) {
            if (this.doPostLoadPhotoSetActions != null) {
                this.doPostLoadPhotoSetActions(this, ss.EventArgs.Empty);
            }
        }
        else {
            if (this.photoSetIsLoadingFromServer != null) {
                this.photoSetIsLoadingFromServer(this, ss.EventArgs.Empty);
            }
            this.loadPhotosViaWebRequest(pageNumber);
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.EventPhotoProvider

Js.Controls.PhotoBrowser.EventPhotoProvider = function Js_Controls_PhotoBrowser_EventPhotoProvider(galleryK, eventK) {
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
    Js.Controls.PhotoBrowser.EventPhotoProvider.initializeBase(this);
    this._galleryK$1 = galleryK;
    this._eventK$1 = eventK;
    this._photoSetsByGalleryK$1 = [];
    this._photoSetsByEventK$1 = [];
}
Js.Controls.PhotoBrowser.EventPhotoProvider.prototype = {
    _photoSetsByGalleryK$1: null,
    _photoSetsByEventK$1: null,
    _galleryK$1: 0,
    _eventK$1: 0,
    
    loadPhotosViaWebRequest: function Js_Controls_PhotoBrowser_EventPhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        if (this._galleryK$1 > 0) {
            Js.Controls.PhotoBrowser.Service.getPhotosByGalleryAndPage(this._galleryK$1, pageNumber, ss.Delegate.create(this, this.successCallback), Js.Library.Trace.webServiceFailure, pageNumber, -1);
        }
        else {
            Js.Controls.PhotoBrowser.Service.getPhotosByEventAndPage(this._eventK$1, pageNumber, ss.Delegate.create(this, this.successCallback), Js.Library.Trace.webServiceFailure, pageNumber, -1);
        }
    },
    
    loadPhotosFromCache: function Js_Controls_PhotoBrowser_EventPhotoProvider$loadPhotosFromCache(pageNumber) {
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
    
    storePhotosInCache: function Js_Controls_PhotoBrowser_EventPhotoProvider$storePhotosInCache(photos, pageNumber) {
        /// <param name="photos" type="Js.Controls.PhotoControl.PhotoResult">
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
    
    setGallery: function Js_Controls_PhotoBrowser_EventPhotoProvider$setGallery(galleryK) {
        /// <param name="galleryK" type="Number" integer="true">
        /// </param>
        this._galleryK$1 = galleryK;
        this.loadPhotos(1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.SingleKeyPhotoProvider

Js.Controls.PhotoBrowser.SingleKeyPhotoProvider = function Js_Controls_PhotoBrowser_SingleKeyPhotoProvider(key) {
    /// <param name="key" type="Number" integer="true">
    /// </param>
    /// <field name="_photoSets$1" type="Array">
    /// </field>
    /// <field name="key" type="Number" integer="true">
    /// </field>
    Js.Controls.PhotoBrowser.SingleKeyPhotoProvider.initializeBase(this);
    this.key = key;
    this._photoSets$1 = [];
}
Js.Controls.PhotoBrowser.SingleKeyPhotoProvider.prototype = {
    _photoSets$1: null,
    key: 0,
    
    storePhotosInCache: function Js_Controls_PhotoBrowser_SingleKeyPhotoProvider$storePhotosInCache(photoSet, pageNumber) {
        /// <param name="photoSet" type="Js.Controls.PhotoControl.PhotoResult">
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
    
    loadPhotosFromCache: function Js_Controls_PhotoBrowser_SingleKeyPhotoProvider$loadPhotosFromCache(pageNumber) {
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
// Js.Controls.PhotoBrowser.ArticlePhotoProvider

Js.Controls.PhotoBrowser.ArticlePhotoProvider = function Js_Controls_PhotoBrowser_ArticlePhotoProvider(k) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    Js.Controls.PhotoBrowser.ArticlePhotoProvider.initializeBase(this, [ k ]);
}
Js.Controls.PhotoBrowser.ArticlePhotoProvider.prototype = {
    
    loadPhotosViaWebRequest: function Js_Controls_PhotoBrowser_ArticlePhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Js.Controls.PhotoBrowser.Service.getPhotosByArticle(this.key, pageNumber, ss.Delegate.create(this, this.successCallback), Js.Library.Trace.webServiceFailure, pageNumber, -1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.TagPhotoProvider

Js.Controls.PhotoBrowser.TagPhotoProvider = function Js_Controls_PhotoBrowser_TagPhotoProvider(k) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    Js.Controls.PhotoBrowser.TagPhotoProvider.initializeBase(this, [ k ]);
}
Js.Controls.PhotoBrowser.TagPhotoProvider.prototype = {
    
    loadPhotosViaWebRequest: function Js_Controls_PhotoBrowser_TagPhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Js.Controls.PhotoBrowser.Service.getPhotosByTag(this.key, pageNumber, ss.Delegate.create(this, this.successCallback), Js.Library.Trace.webServiceFailure, pageNumber, -1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.GroupPhotoProvider

Js.Controls.PhotoBrowser.GroupPhotoProvider = function Js_Controls_PhotoBrowser_GroupPhotoProvider(k) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    Js.Controls.PhotoBrowser.GroupPhotoProvider.initializeBase(this, [ k ]);
}
Js.Controls.PhotoBrowser.GroupPhotoProvider.prototype = {
    
    loadPhotosViaWebRequest: function Js_Controls_PhotoBrowser_GroupPhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Js.Controls.PhotoBrowser.Service.getPhotosByGroup(this.key, pageNumber, ss.Delegate.create(this, this.successCallback), Js.Library.Trace.webServiceFailure, pageNumber, -1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.PhotosOfUsrProvider

Js.Controls.PhotoBrowser.PhotosOfUsrProvider = function Js_Controls_PhotoBrowser_PhotosOfUsrProvider(k, spottedByUsrK) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    /// <param name="spottedByUsrK" type="Number" integer="true">
    /// </param>
    /// <field name="_spottedByUsrK$2" type="Number" integer="true">
    /// </field>
    Js.Controls.PhotoBrowser.PhotosOfUsrProvider.initializeBase(this, [ k ]);
    this._spottedByUsrK$2 = spottedByUsrK;
}
Js.Controls.PhotoBrowser.PhotosOfUsrProvider.prototype = {
    _spottedByUsrK$2: 0,
    
    loadPhotosViaWebRequest: function Js_Controls_PhotoBrowser_PhotosOfUsrProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Js.Controls.PhotoBrowser.Service.getPhotosOfUsr(this.key, pageNumber, this._spottedByUsrK$2, ss.Delegate.create(this, this.successCallback), Js.Library.Trace.webServiceFailure, pageNumber, -1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider

Js.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider = function Js_Controls_PhotoBrowser_FavouritePhotosOfUsrProvider(k) {
    /// <param name="k" type="Number" integer="true">
    /// </param>
    Js.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider.initializeBase(this, [ k ]);
}
Js.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider.prototype = {
    
    loadPhotosViaWebRequest: function Js_Controls_PhotoBrowser_FavouritePhotosOfUsrProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Js.Controls.PhotoBrowser.Service.getFavouritePhotosOfUsr(this.key, pageNumber, ss.Delegate.create(this, this.successCallback), Js.Library.Trace.webServiceFailure, pageNumber, -1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.VideoPhotoProvider

Js.Controls.PhotoBrowser.VideoPhotoProvider = function Js_Controls_PhotoBrowser_VideoPhotoProvider() {
    /// <field name="_videos$1" type="Array">
    /// </field>
    Js.Controls.PhotoBrowser.VideoPhotoProvider.initializeBase(this);
    this._videos$1 = [];
}
Js.Controls.PhotoBrowser.VideoPhotoProvider.prototype = {
    _videos$1: null,
    
    storePhotosInCache: function Js_Controls_PhotoBrowser_VideoPhotoProvider$storePhotosInCache(photoSet, pageNumber) {
        /// <param name="photoSet" type="Js.Controls.PhotoControl.PhotoResult">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        this._videos$1[pageNumber] = photoSet;
        return true;
    },
    
    loadPhotosFromCache: function Js_Controls_PhotoBrowser_VideoPhotoProvider$loadPhotosFromCache(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this._videos$1[pageNumber] != null) {
            this.set_currentPhotoSet(this._videos$1[pageNumber]);
            return true;
        }
        return false;
    },
    
    loadPhotosViaWebRequest: function Js_Controls_PhotoBrowser_VideoPhotoProvider$loadPhotosViaWebRequest(pageNumber) {
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        Js.Controls.PhotoBrowser.Service.getRecentVideos(pageNumber, ss.Delegate.create(this, this.successCallback), Js.Library.Trace.webServiceFailure, pageNumber, -1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PhotoBrowser.PhotosController

Js.Controls.PhotoBrowser.PhotosController = function Js_Controls_PhotoBrowser_PhotosController() {
}
Js.Controls.PhotoBrowser.PhotosController.prototype = {
    
    setupController: function Js_Controls_PhotoBrowser_PhotosController$setupController() {
        this.get_photoProvider().doPostLoadPhotoSetActions = ss.Delegate.create(this.get_photoBrowser(), this.get_photoBrowser().doPostLoadPhotoSetActions);
        this.get_photoProvider().photoSetIsLoadingFromServer = ss.Delegate.create(this.get_photoBrowser(), this.get_photoBrowser().photoSetIsLoadingFromServer);
        this.get_photoProvider().loadPhotos(this.get_photoBrowser().get_paginationControl().get_currentPage());
        this.get_photoBrowser().set_photoProvider(this.get_photoProvider());
        this.get_photoBrowser().onChangePhotoSet = ss.Delegate.create(this.get_photoControl(), this.get_photoControl().photoSetChanged);
        this.get_photoBrowser().onChangePhoto = ss.Delegate.create(this.get_photoControl(), this.get_photoControl().photoChanged);
        this.get_photoControl().onPhotoNextClick = ss.Delegate.create(this.get_photoBrowser(), this.get_photoBrowser().moveToNextPhoto);
        this.get_photoControl().onPhotoPrevClick = ss.Delegate.create(this.get_photoBrowser(), this.get_photoBrowser().moveToPreviousPhoto);
        this.get_photoControl().onPhotoUpClick = ss.Delegate.create(this.get_photoBrowser(), this.get_photoBrowser().moveToPhotoAbove);
        this.get_photoControl().onPhotoDownClick = ss.Delegate.create(this.get_photoBrowser(), this.get_photoBrowser().moveToPhotoBelow);
        this.get_photoControl().onPhotoChanged = ss.Delegate.create(this, this._photoChanged);
        this.get_photoControl().onPhotoChangedAfterDelay = ss.Delegate.create(this, this._photoChangedAfterDelay);
        this.get_photoControl().onRolloverMouseOverTextChanged = ss.Delegate.create(this.get_photoBrowser(), this.get_photoBrowser().rolloverMouseOverTextChanged);
        this.get_threadControl().onThreadCreated = ss.Delegate.create(this, this._threadCreated);
        this.get_threadControl().onCommentPosted = ss.Delegate.create(this, this._commentPosted);
        this.get_threadControl().get_uiCommentsDisplay().onThreadDeleted = ss.Delegate.create(this, this._threadDeleted);
    },
    
    _photoChanged: function Js_Controls_PhotoBrowser_PhotosController$_photoChanged(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        var p = e;
        this.get_threadControl().get_uiCommentsDisplay().setCommentsCount(p.photo.commentsCount);
        this.get_latestChatController().hide();
    },
    
    _photoChangedAfterDelay: function Js_Controls_PhotoBrowser_PhotosController$_photoChangedAfterDelay(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        var p = e;
        this.get_threadControl().get_uiCommentsDisplay().showComments(p.photo.threadK, 1);
        this.get_threadControl().set_currentParentObjectK(p.photo.k);
        this.get_latestChatController().show(p.photo.k);
    },
    
    _threadCreated: function Js_Controls_PhotoBrowser_PhotosController$_threadCreated(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (!this.get_photoControl().get_currentPhoto().threadK) {
            this.get_photoControl().get_currentPhoto().threadK = (e).value;
        }
        this.get_photoControl().get_currentPhoto().commentsCount = 1;
        this.get_latestChatController().update(o, e);
    },
    
    _commentPosted: function Js_Controls_PhotoBrowser_PhotosController$_commentPosted(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.get_photoControl().get_currentPhoto().threadK === (e).value) {
            this.get_photoControl().get_currentPhoto().commentsCount++;
        }
    },
    
    _threadDeleted: function Js_Controls_PhotoBrowser_PhotosController$_threadDeleted(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.get_photoControl().get_currentPhoto().threadK === (e).value) {
            this.get_photoControl().get_currentPhoto().threadK = 0;
            this.get_photoControl().get_currentPhoto().commentsCount = 0;
        }
    }
}


Js.Controls.PhotoBrowser.Controller.registerClass('Js.Controls.PhotoBrowser.Controller', Js.Controls.PhotoBrowser.PhotoBrowsingUsingKeysControl);
Js.Controls.PhotoBrowser.Service.registerClass('Js.Controls.PhotoBrowser.Service');
Js.Controls.PhotoBrowser.View.registerClass('Js.Controls.PhotoBrowser.View');
Js.Controls.PhotoBrowser.PhotoProvider.registerClass('Js.Controls.PhotoBrowser.PhotoProvider');
Js.Controls.PhotoBrowser.EventPhotoProvider.registerClass('Js.Controls.PhotoBrowser.EventPhotoProvider', Js.Controls.PhotoBrowser.PhotoProvider);
Js.Controls.PhotoBrowser.SingleKeyPhotoProvider.registerClass('Js.Controls.PhotoBrowser.SingleKeyPhotoProvider', Js.Controls.PhotoBrowser.PhotoProvider);
Js.Controls.PhotoBrowser.ArticlePhotoProvider.registerClass('Js.Controls.PhotoBrowser.ArticlePhotoProvider', Js.Controls.PhotoBrowser.SingleKeyPhotoProvider);
Js.Controls.PhotoBrowser.TagPhotoProvider.registerClass('Js.Controls.PhotoBrowser.TagPhotoProvider', Js.Controls.PhotoBrowser.SingleKeyPhotoProvider);
Js.Controls.PhotoBrowser.GroupPhotoProvider.registerClass('Js.Controls.PhotoBrowser.GroupPhotoProvider', Js.Controls.PhotoBrowser.SingleKeyPhotoProvider);
Js.Controls.PhotoBrowser.PhotosOfUsrProvider.registerClass('Js.Controls.PhotoBrowser.PhotosOfUsrProvider', Js.Controls.PhotoBrowser.SingleKeyPhotoProvider);
Js.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider.registerClass('Js.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider', Js.Controls.PhotoBrowser.SingleKeyPhotoProvider);
Js.Controls.PhotoBrowser.VideoPhotoProvider.registerClass('Js.Controls.PhotoBrowser.VideoPhotoProvider', Js.Controls.PhotoBrowser.PhotoProvider);
Js.Controls.PhotoBrowser.PhotosController.registerClass('Js.Controls.PhotoBrowser.PhotosController');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
