Type.registerNamespace('SpottedScript.Controls.Banners.Generator');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Banners.Generator.BannerRenderInfoType
SpottedScript.Controls.Banners.Generator.BannerRenderInfoType = function() { 
    /// <field name="flash" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="html" type="Number" integer="true" static="true">
    /// </field>
};
SpottedScript.Controls.Banners.Generator.BannerRenderInfoType.prototype = {
    flash: 1, 
    html: 2
}
SpottedScript.Controls.Banners.Generator.BannerRenderInfoType.registerEnum('SpottedScript.Controls.Banners.Generator.BannerRenderInfoType', false);
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Banners.Generator.BannerRenderInfo
SpottedScript.Controls.Banners.Generator.BannerRenderInfo = function SpottedScript_Controls_Banners_Generator_BannerRenderInfo() {
    /// <field name="bannerRenderInfoType" type="SpottedScript.Controls.Banners.Generator.BannerRenderInfoType">
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
    /// Number of ms to wait before changing banner
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
SpottedScript.Controls.Banners.Generator.BannerRenderInfo.prototype = {
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
// SpottedScript.Controls.Banners.Generator.Controller
SpottedScript.Controls.Banners.Generator.Controller = function SpottedScript_Controls_Banners_Generator_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.Banners.Generator.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.Banners.Generator.View">
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
    /// <field name="refreshAllBanners" type="ScriptSharpLibrary.Action" static="true">
    /// </field>
    /// <field name="_oldRefreshBanners" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="_inactiveTime" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_stopped" type="Boolean">
    /// </field>
    /// <field name="_lastK" type="Number" integer="true">
    /// </field>
    this._view = view;
    this._pos = Number.parseInvariant(this._view.get_uiPosition().value);
    this._timeout = Number.parseInvariant(this._view.get_uiDuration().value);
    if (this._view.get_uiK().value.length > 0) {
        this._lastK = Number.parseInvariant(this._view.get_uiK().value);
        this._startTimer();
    }
    window.setInterval(Function.createDelegate(this, this._checkInactivity), SpottedScript.Controls.Banners.Generator.Controller._inactivityTimerPeriod);
    if (!SpottedScript.Controls.Banners.Generator.Controller._initialisedInactivityTimer) {
        $addHandler(window.self, 'keydown', Function.createDelegate(this, this._onUserAction));
        $addHandler(window.self, 'scroll', Function.createDelegate(this, this._onUserAction));
        $addHandler(document.body, 'click', Function.createDelegate(this, this._onUserAction));
        SpottedScript.Controls.Banners.Generator.Controller._initialisedInactivityTimer = true;
    }
    this._userInactivityPeriodDuration = Number.parseInvariant(this._view.get_uiInactivityTimeoutDuration().value);
    this._oldRefreshBanners = SpottedScript.Controls.Banners.Generator.Controller.refreshAllBanners;
    SpottedScript.Controls.Banners.Generator.Controller.refreshAllBanners = Function.createDelegate(this, function() {
        this._requestNewBanner();
        if (this._oldRefreshBanners != null) {
            this._oldRefreshBanners();
        }
    });
}
SpottedScript.Controls.Banners.Generator.Controller.prototype = {
    _view: null,
    _timeout: 0,
    _pos: 0,
    _timerId: 0,
    _userInactivityPeriodDuration: 0,
    _oldRefreshBanners: null,
    _onUserAction: function SpottedScript_Controls_Banners_Generator_Controller$_onUserAction(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        Utils.Trace.write('User action');
        this._restartActivityTimer();
    },
    _stopped: false,
    _checkInactivity: function SpottedScript_Controls_Banners_Generator_Controller$_checkInactivity() {
        SpottedScript.Controls.Banners.Generator.Controller._inactiveTime += SpottedScript.Controls.Banners.Generator.Controller._inactivityTimerPeriod;
        if (!this._stopped && this._userInactivityPeriodDuration < SpottedScript.Controls.Banners.Generator.Controller._inactiveTime) {
            Utils.Trace.write('User inactive. Stopping banner rotation');
            window.clearTimeout(this._timerId);
            this._stopped = true;
        }
        else {
            Utils.Trace.write('Timeout in ' + (this._userInactivityPeriodDuration - SpottedScript.Controls.Banners.Generator.Controller._inactiveTime) / 1000);
        }
    },
    _restartActivityTimer: function SpottedScript_Controls_Banners_Generator_Controller$_restartActivityTimer() {
        SpottedScript.Controls.Banners.Generator.Controller._inactiveTime = 0;
        if (this._stopped) {
            Utils.Trace.write('User active. Active banner rotation');
            this._startTimer();
        }
    },
    _startTimer: function SpottedScript_Controls_Banners_Generator_Controller$_startTimer() {
        this._stopped = false;
        if (this._timeout > 0) {
            this._timerId = window.setTimeout(Function.createDelegate(this, this._onTimerElapsed), this._timeout);
        }
    },
    _onTimerElapsed: function SpottedScript_Controls_Banners_Generator_Controller$_onTimerElapsed() {
        Utils.Trace.write('Requesting new banners');
        if (this._bannerIsVisible()) {
            this._requestNewBanner();
        }
        else {
            this._startTimer();
        }
    },
    _requestNewBanner: function SpottedScript_Controls_Banners_Generator_Controller$_requestNewBanner() {
        if (this._pos !== 1 && this._pos !== 2 && this._pos !== 5) {
            return;
        }
        try {
            window.clearTimeout(this._timerId);
            Spotted.WebServices.Controls.Banners.Generator.Service.getBanner(this._pos, this._view.get_uiPlaceKs().value, this._view.get_uiMusicTypes().value, Function.createDelegate(this, this._gotBanner), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, 5000);
        }
        catch (ex) {
            Utils.Trace.write(ex.toString());
            Utils.Trace.write(ex.message);
        }
    },
    _bannerIsVisible: function SpottedScript_Controls_Banners_Generator_Controller$_bannerIsVisible() {
        /// <returns type="Boolean"></returns>
        var scrollOffset = document.body.scrollTop;
        var j = jQuery(this._view.get_uiBanner());
        return scrollOffset < j.offset().top + j.height();
    },
    _lastK: 0,
    _gotBanner: function SpottedScript_Controls_Banners_Generator_Controller$_gotBanner(result, userContext, methodName) {
        /// <param name="result" type="SpottedScript.Controls.Banners.Generator.BannerRenderInfo">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (result != null) {
            if (this._lastK !== result.k) {
                this._timeout = result.duration;
                $clearHandlers(this._view.get_uiBanner());
                if (result.bannerRenderInfoType === SpottedScript.Controls.Banners.Generator.BannerRenderInfoType.flash) {
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
                        Utils.Trace.write('linkTag' + result.linkTag);
                        var div = document.createElement('DIV');
                        div.style.left = (0 + Number.parseInvariant(this._view.get_uiClickHelperLeft().value)) + 'px';
                        div.style.top = (0 + Number.parseInvariant(this._view.get_uiClickHelperLeft().value)) + 'px';
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
                $addHandler(this._view.get_uiBanner(), 'mouseover', Function.createDelegate(this, this._stopTimer));
            }
        }
        else {
        }
        this._startTimer();
    },
    _stopTimer: function SpottedScript_Controls_Banners_Generator_Controller$_stopTimer(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        window.clearTimeout(this._timerId);
        $clearHandlers(this._view.get_uiBanner());
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Banners.Generator.View
SpottedScript.Controls.Banners.Generator.View = function SpottedScript_Controls_Banners_Generator_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.Banners.Generator.View.prototype = {
    clientId: null,
    get_holder: function SpottedScript_Controls_Banners_Generator_View$get_holder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Holder');
    },
    get_uiBanner: function SpottedScript_Controls_Banners_Generator_View$get_uiBanner() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBanner');
    },
    get_uiPosition: function SpottedScript_Controls_Banners_Generator_View$get_uiPosition() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPosition');
    },
    get_uiK: function SpottedScript_Controls_Banners_Generator_View$get_uiK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiK');
    },
    get_uiMusicTypes: function SpottedScript_Controls_Banners_Generator_View$get_uiMusicTypes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMusicTypes');
    },
    get_uiPlaceKs: function SpottedScript_Controls_Banners_Generator_View$get_uiPlaceKs() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPlaceKs');
    },
    get_uiDuration: function SpottedScript_Controls_Banners_Generator_View$get_uiDuration() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDuration');
    },
    get_uiInactivityTimeoutDuration: function SpottedScript_Controls_Banners_Generator_View$get_uiInactivityTimeoutDuration() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInactivityTimeoutDuration');
    },
    get_uiClickHelperLeft: function SpottedScript_Controls_Banners_Generator_View$get_uiClickHelperLeft() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiClickHelperLeft');
    },
    get_uiClickHelperTop: function SpottedScript_Controls_Banners_Generator_View$get_uiClickHelperTop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiClickHelperTop');
    }
}
SpottedScript.Controls.Banners.Generator.BannerRenderInfo.registerClass('SpottedScript.Controls.Banners.Generator.BannerRenderInfo');
SpottedScript.Controls.Banners.Generator.Controller.registerClass('SpottedScript.Controls.Banners.Generator.Controller');
SpottedScript.Controls.Banners.Generator.View.registerClass('SpottedScript.Controls.Banners.Generator.View');
SpottedScript.Controls.Banners.Generator.Controller._inactivityTimerPeriod = 15000;
SpottedScript.Controls.Banners.Generator.Controller._initialisedInactivityTimer = false;
SpottedScript.Controls.Banners.Generator.Controller.refreshAllBanners = null;
SpottedScript.Controls.Banners.Generator.Controller._inactiveTime = 0;
