//! Generator.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.Banners.Generator');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Banners.Generator.BannerRenderInfoType

Js.Controls.Banners.Generator.BannerRenderInfoType = function() { 
    /// <field name="flash" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="html" type="Number" integer="true" static="true">
    /// </field>
};
Js.Controls.Banners.Generator.BannerRenderInfoType.prototype = {
    flash: 1, 
    html: 2
}
Js.Controls.Banners.Generator.BannerRenderInfoType.registerEnum('Js.Controls.Banners.Generator.BannerRenderInfoType', false);


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Banners.Generator.BannerRenderInfo

Js.Controls.Banners.Generator.BannerRenderInfo = function Js_Controls_Banners_Generator_BannerRenderInfo() {
    /// <field name="bannerRenderInfoType" type="Js.Controls.Banners.Generator.BannerRenderInfoType">
    /// </field>
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="flashUrl" type="String">
    /// </field>
    /// <field name="height" type="Number" integer="true">
    /// </field>
    /// <field name="width" type="Number" integer="true">
    /// </field>
    /// <field name="html" type="String">
    /// </field>
    /// <field name="duration" type="Number" integer="true">
    /// </field>
    /// <field name="miscNeedsClickHelper" type="Boolean">
    /// </field>
    /// <field name="flashVersion" type="String">
    /// </field>
    /// <field name="linkTag" type="String">
    /// </field>
    /// <field name="targetTag" type="String">
    /// </field>
}
Js.Controls.Banners.Generator.BannerRenderInfo.prototype = {
    bannerRenderInfoType: 0,
    k: 0,
    flashUrl: null,
    height: 0,
    width: 0,
    html: null,
    duration: 0,
    miscNeedsClickHelper: false,
    flashVersion: null,
    linkTag: null,
    targetTag: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Banners.Generator.Controller

Js.Controls.Banners.Generator.Controller = function Js_Controls_Banners_Generator_Controller(view) {
    /// <param name="view" type="Js.Controls.Banners.Generator.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.Banners.Generator.View">
    /// </field>
    /// <field name="_timeout" type="Number" integer="true">
    /// </field>
    /// <field name="_pos" type="Number" integer="true">
    /// </field>
    /// <field name="_timerId" type="Number" integer="true">
    /// </field>
    /// <field name="_inactivityTimerPeriod" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_initialisedInactivityTimer" type="Boolean" static="true">
    /// </field>
    /// <field name="_userInactivityPeriodDuration" type="Number" integer="true">
    /// </field>
    /// <field name="refreshAllBanners" type="Function" static="true">
    /// </field>
    /// <field name="_oldRefreshBanners" type="Function">
    /// </field>
    /// <field name="_inactiveTime" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_stopped" type="Boolean">
    /// </field>
    /// <field name="_lastK" type="Number" integer="true">
    /// </field>
    this._view = view;
    this._pos = parseInt(this._view.get_uiPosition().value);
    this._timeout = parseInt(this._view.get_uiDuration().value);
    if (this._view.get_uiK().value.length > 0) {
        this._lastK = parseInt(this._view.get_uiK().value);
        this._startTimer();
    }
    window.setInterval(ss.Delegate.create(this, this._checkInactivity), 15000);
    if (!Js.Controls.Banners.Generator.Controller._initialisedInactivityTimer) {
        $(window).keydown(ss.Delegate.create(this, this._onUserAction)).scroll(ss.Delegate.create(this, this._onUserAction));
        $(document).click(ss.Delegate.create(this, this._onUserAction));
        Js.Controls.Banners.Generator.Controller._initialisedInactivityTimer = true;
    }
    this._userInactivityPeriodDuration = parseInt(this._view.get_uiInactivityTimeoutDuration().value);
    this._oldRefreshBanners = Js.Controls.Banners.Generator.Controller.refreshAllBanners;
    Js.Controls.Banners.Generator.Controller.refreshAllBanners = ss.Delegate.create(this, function() {
        this._requestNewBanner();
        if (this._oldRefreshBanners != null) {
            this._oldRefreshBanners();
        }
    });
}
Js.Controls.Banners.Generator.Controller.prototype = {
    _view: null,
    _timeout: 0,
    _pos: 0,
    _timerId: 0,
    _userInactivityPeriodDuration: 0,
    _oldRefreshBanners: null,
    
    _onUserAction: function Js_Controls_Banners_Generator_Controller$_onUserAction(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        Js.Library.Trace.write('User action');
        this._restartActivityTimer();
    },
    
    _stopped: false,
    
    _checkInactivity: function Js_Controls_Banners_Generator_Controller$_checkInactivity() {
        Js.Controls.Banners.Generator.Controller._inactiveTime += 15000;
        if (!this._stopped && this._userInactivityPeriodDuration < Js.Controls.Banners.Generator.Controller._inactiveTime) {
            Js.Library.Trace.write('User inactive. Stopping banner rotation');
            window.clearTimeout(this._timerId);
            this._stopped = true;
        }
    },
    
    _restartActivityTimer: function Js_Controls_Banners_Generator_Controller$_restartActivityTimer() {
        Js.Controls.Banners.Generator.Controller._inactiveTime = 0;
        if (this._stopped) {
            Js.Library.Trace.write('User active. Active banner rotation');
            this._startTimer();
        }
    },
    
    _startTimer: function Js_Controls_Banners_Generator_Controller$_startTimer() {
        this._stopped = false;
        if (this._timeout > 0) {
            this._timerId = window.setTimeout(ss.Delegate.create(this, this._onTimerElapsed), this._timeout);
        }
    },
    
    _onTimerElapsed: function Js_Controls_Banners_Generator_Controller$_onTimerElapsed() {
        Js.Library.Trace.write('Requesting new banners');
        if (this._bannerIsVisible()) {
            this._requestNewBanner();
        }
        else {
            this._startTimer();
        }
    },
    
    _requestNewBanner: function Js_Controls_Banners_Generator_Controller$_requestNewBanner() {
        if (this._pos !== 1 && this._pos !== 2 && this._pos !== 5) {
            return;
        }
        try {
            window.clearTimeout(this._timerId);
            Js.Controls.Banners.Generator.Service.getBanner(this._pos, this._view.get_uiPlaceKs().value, this._view.get_uiMusicTypes().value, ss.Delegate.create(this, this._gotBanner), Js.Library.Trace.webServiceFailure, null, 5000);
        }
        catch (ex) {
            Js.Library.Trace.write(ex.toString());
            Js.Library.Trace.write(ex.message);
        }
    },
    
    _bannerIsVisible: function Js_Controls_Banners_Generator_Controller$_bannerIsVisible() {
        /// <returns type="Boolean"></returns>
        var scrollOffset = document.body.scrollTop;
        return scrollOffset < this._view.get_uiBannerJ().offset().top + this._view.get_uiBannerJ().height();
    },
    
    _lastK: 0,
    
    _gotBanner: function Js_Controls_Banners_Generator_Controller$_gotBanner(result, userContext, methodName) {
        /// <param name="result" type="Js.Controls.Banners.Generator.BannerRenderInfo">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (result != null) {
            if (this._lastK !== result.k) {
                this._timeout = result.duration;
                this._view.get_uiBannerJ().unbind('mouseover');
                if (result.bannerRenderInfoType === 1) {
                    var bannerHolderId = this._view.get_uiBanner().id;
                    var banner = new SWFObject(result.flashUrl, bannerHolderId + '_movie', result.width, result.height, result.flashVersion, '#ffffff');
                    banner.addParam('wmode', 'transparent');
                    banner.addParam('AllowScriptAccess', 'always');
                    banner.addVariable('targetTag', result.targetTag);
                    banner.addVariable('linkTag', result.linkTag);
                    banner.addParam('loop', 'true');
                    banner.addParam('menu', 'false');
                    banner.write(bannerHolderId);
                    if (result.miscNeedsClickHelper) {
                        Js.Library.Trace.write('linkTag' + result.linkTag);
                        var div = document.createElement('DIV');
                        div.style.left = (0 + parseInt(this._view.get_uiClickHelperLeft().value)) + 'px';
                        div.style.top = (0 + parseInt(this._view.get_uiClickHelperLeft().value)) + 'px';
                        div.style.position = 'absolute';
                        div.style.zIndex = 1;
                        var a = document.createElement('A');
                        a.href = result.linkTag;
                        a.target = result.targetTag;
                        a.style.border = 'solid 0px black';
                        a.className = 'NoStyle';
                        var gif = document.createElement('IMG');
                        gif.style.width = result.width + 'px';
                        gif.style.height = result.height + 'px';
                        gif.style.border = 'solid 0px black';
                        gif.style.display = 'block';
                        gif.src = '/gfx/1pix.gif';
                        a.appendChild(gif);
                        div.appendChild(a);
                        this._view.get_uiBanner().appendChild(div);
                    }
                    this._lastK = result.k;
                }
                else {
                    document.getElementById(this._view.get_uiBanner().id).innerHTML = result.html;
                }
                this._view.get_uiBannerJ().mouseover(ss.Delegate.create(this, this._stopTimer));
            }
        }
        else {
        }
        this._startTimer();
    },
    
    _stopTimer: function Js_Controls_Banners_Generator_Controller$_stopTimer(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        window.clearTimeout(this._timerId);
        this._view.get_uiBannerJ().unbind('mouseover');
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Banners.Generator.Service

Js.Controls.Banners.Generator.Service = function Js_Controls_Banners_Generator_Service() {
}
Js.Controls.Banners.Generator.Service.getBanner = function Js_Controls_Banners_Generator_Service$getBanner(positionAsInt, relevantPlacesCsv, relevantMusicTypesCsv, success, failure, userContext, timeout) {
    /// <param name="positionAsInt" type="Number" integer="true">
    /// </param>
    /// <param name="relevantPlacesCsv" type="String">
    /// </param>
    /// <param name="relevantMusicTypesCsv" type="String">
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
    p['positionAsInt'] = positionAsInt;
    p['relevantPlacesCsv'] = relevantPlacesCsv;
    p['relevantMusicTypesCsv'] = relevantMusicTypesCsv;
    var o = Js.Library.WebServiceHelper.options('GetBanner', '/WebServices/Controls/Banners/Generator/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetBanner');
    };
    $.ajax(o);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Banners.Generator.View

Js.Controls.Banners.Generator.View = function Js_Controls_Banners_Generator_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_Holder" type="Object" domElement="true">
    /// </field>
    /// <field name="_HolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBanner" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBannerJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPosition" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPositionJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiK" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiKJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiMusicTypes" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiMusicTypesJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPlaceKs" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPlaceKsJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiDuration" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiDurationJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiInactivityTimeoutDuration" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiInactivityTimeoutDurationJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiClickHelperLeft" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiClickHelperLeftJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiClickHelperTop" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiClickHelperTopJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.Banners.Generator.View.prototype = {
    clientId: null,
    
    get_holder: function Js_Controls_Banners_Generator_View$get_holder() {
        /// <value type="Object" domElement="true"></value>
        if (this._Holder == null) {
            this._Holder = document.getElementById(this.clientId + '_Holder');
        }
        return this._Holder;
    },
    
    _Holder: null,
    
    get_holderJ: function Js_Controls_Banners_Generator_View$get_holderJ() {
        /// <value type="jQueryObject"></value>
        if (this._HolderJ == null) {
            this._HolderJ = $('#' + this.clientId + '_Holder');
        }
        return this._HolderJ;
    },
    
    _HolderJ: null,
    
    get_uiBanner: function Js_Controls_Banners_Generator_View$get_uiBanner() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBanner == null) {
            this._uiBanner = document.getElementById(this.clientId + '_uiBanner');
        }
        return this._uiBanner;
    },
    
    _uiBanner: null,
    
    get_uiBannerJ: function Js_Controls_Banners_Generator_View$get_uiBannerJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBannerJ == null) {
            this._uiBannerJ = $('#' + this.clientId + '_uiBanner');
        }
        return this._uiBannerJ;
    },
    
    _uiBannerJ: null,
    
    get_uiPosition: function Js_Controls_Banners_Generator_View$get_uiPosition() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPosition == null) {
            this._uiPosition = document.getElementById(this.clientId + '_uiPosition');
        }
        return this._uiPosition;
    },
    
    _uiPosition: null,
    
    get_uiPositionJ: function Js_Controls_Banners_Generator_View$get_uiPositionJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPositionJ == null) {
            this._uiPositionJ = $('#' + this.clientId + '_uiPosition');
        }
        return this._uiPositionJ;
    },
    
    _uiPositionJ: null,
    
    get_uiK: function Js_Controls_Banners_Generator_View$get_uiK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiK == null) {
            this._uiK = document.getElementById(this.clientId + '_uiK');
        }
        return this._uiK;
    },
    
    _uiK: null,
    
    get_uiKJ: function Js_Controls_Banners_Generator_View$get_uiKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiKJ == null) {
            this._uiKJ = $('#' + this.clientId + '_uiK');
        }
        return this._uiKJ;
    },
    
    _uiKJ: null,
    
    get_uiMusicTypes: function Js_Controls_Banners_Generator_View$get_uiMusicTypes() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiMusicTypes == null) {
            this._uiMusicTypes = document.getElementById(this.clientId + '_uiMusicTypes');
        }
        return this._uiMusicTypes;
    },
    
    _uiMusicTypes: null,
    
    get_uiMusicTypesJ: function Js_Controls_Banners_Generator_View$get_uiMusicTypesJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiMusicTypesJ == null) {
            this._uiMusicTypesJ = $('#' + this.clientId + '_uiMusicTypes');
        }
        return this._uiMusicTypesJ;
    },
    
    _uiMusicTypesJ: null,
    
    get_uiPlaceKs: function Js_Controls_Banners_Generator_View$get_uiPlaceKs() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPlaceKs == null) {
            this._uiPlaceKs = document.getElementById(this.clientId + '_uiPlaceKs');
        }
        return this._uiPlaceKs;
    },
    
    _uiPlaceKs: null,
    
    get_uiPlaceKsJ: function Js_Controls_Banners_Generator_View$get_uiPlaceKsJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPlaceKsJ == null) {
            this._uiPlaceKsJ = $('#' + this.clientId + '_uiPlaceKs');
        }
        return this._uiPlaceKsJ;
    },
    
    _uiPlaceKsJ: null,
    
    get_uiDuration: function Js_Controls_Banners_Generator_View$get_uiDuration() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiDuration == null) {
            this._uiDuration = document.getElementById(this.clientId + '_uiDuration');
        }
        return this._uiDuration;
    },
    
    _uiDuration: null,
    
    get_uiDurationJ: function Js_Controls_Banners_Generator_View$get_uiDurationJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiDurationJ == null) {
            this._uiDurationJ = $('#' + this.clientId + '_uiDuration');
        }
        return this._uiDurationJ;
    },
    
    _uiDurationJ: null,
    
    get_uiInactivityTimeoutDuration: function Js_Controls_Banners_Generator_View$get_uiInactivityTimeoutDuration() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiInactivityTimeoutDuration == null) {
            this._uiInactivityTimeoutDuration = document.getElementById(this.clientId + '_uiInactivityTimeoutDuration');
        }
        return this._uiInactivityTimeoutDuration;
    },
    
    _uiInactivityTimeoutDuration: null,
    
    get_uiInactivityTimeoutDurationJ: function Js_Controls_Banners_Generator_View$get_uiInactivityTimeoutDurationJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiInactivityTimeoutDurationJ == null) {
            this._uiInactivityTimeoutDurationJ = $('#' + this.clientId + '_uiInactivityTimeoutDuration');
        }
        return this._uiInactivityTimeoutDurationJ;
    },
    
    _uiInactivityTimeoutDurationJ: null,
    
    get_uiClickHelperLeft: function Js_Controls_Banners_Generator_View$get_uiClickHelperLeft() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiClickHelperLeft == null) {
            this._uiClickHelperLeft = document.getElementById(this.clientId + '_uiClickHelperLeft');
        }
        return this._uiClickHelperLeft;
    },
    
    _uiClickHelperLeft: null,
    
    get_uiClickHelperLeftJ: function Js_Controls_Banners_Generator_View$get_uiClickHelperLeftJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiClickHelperLeftJ == null) {
            this._uiClickHelperLeftJ = $('#' + this.clientId + '_uiClickHelperLeft');
        }
        return this._uiClickHelperLeftJ;
    },
    
    _uiClickHelperLeftJ: null,
    
    get_uiClickHelperTop: function Js_Controls_Banners_Generator_View$get_uiClickHelperTop() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiClickHelperTop == null) {
            this._uiClickHelperTop = document.getElementById(this.clientId + '_uiClickHelperTop');
        }
        return this._uiClickHelperTop;
    },
    
    _uiClickHelperTop: null,
    
    get_uiClickHelperTopJ: function Js_Controls_Banners_Generator_View$get_uiClickHelperTopJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiClickHelperTopJ == null) {
            this._uiClickHelperTopJ = $('#' + this.clientId + '_uiClickHelperTop');
        }
        return this._uiClickHelperTopJ;
    },
    
    _uiClickHelperTopJ: null
}


Js.Controls.Banners.Generator.BannerRenderInfo.registerClass('Js.Controls.Banners.Generator.BannerRenderInfo');
Js.Controls.Banners.Generator.Controller.registerClass('Js.Controls.Banners.Generator.Controller');
Js.Controls.Banners.Generator.Service.registerClass('Js.Controls.Banners.Generator.Service');
Js.Controls.Banners.Generator.View.registerClass('Js.Controls.Banners.Generator.View');
Js.Controls.Banners.Generator.Controller._initialisedInactivityTimer = false;
Js.Controls.Banners.Generator.Controller.refreshAllBanners = null;
Js.Controls.Banners.Generator.Controller._inactiveTime = 0;
})(jQuery);

//! This script was generated using Script# v0.7.4.0
