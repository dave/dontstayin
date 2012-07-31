//! FindYourPhoto.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.FindYourPhoto');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.FindYourPhoto.Controller

Js.Pages.FindYourPhoto.Controller = function Js_Pages_FindYourPhoto_Controller(v) {
    /// <param name="v" type="Js.Pages.FindYourPhoto.View">
    /// </param>
    /// <field name="view" type="Js.Pages.FindYourPhoto.View">
    /// </field>
    /// <field name="_requestId" type="Number" integer="true">
    /// </field>
    /// <field name="_loadId" type="Number" integer="true">
    /// </field>
    this.view = v;
    this.view.get_picker().selectedSpotterChanged = ss.Delegate.create(this, this._spotterChanged);
    this.view.get_picker().selectedEventChanged = ss.Delegate.create(this, this._eventChanged);
    if (Js.Library.Misc.get_browserIsIE()) {
        $(ss.Delegate.create(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
Js.Pages.FindYourPhoto.Controller.prototype = {
    view: null,
    
    _initialise: function Js_Pages_FindYourPhoto_Controller$_initialise() {
    },
    
    _spotterChanged: function Js_Pages_FindYourPhoto_Controller$_spotterChanged(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Js.Controls.Picker.StringArgs">
        /// </param>
        var usrK = 0;
        try {
            usrK = parseInt(e.val.replaceAll('-', ''));
        }
        catch ($e1) {
        }
        if (usrK > 0) {
            this._changeNow(usrK, 0);
        }
        else {
            this.view.get_result().innerHTML = "Can't find this spotter. The spotter code should be 8 digits, like 1234-5678.";
            this.view.get_resultOuter().style.display = '';
            this.view.get_loadingOverlay().style.display = 'none';
        }
    },
    
    _eventChanged: function Js_Pages_FindYourPhoto_Controller$_eventChanged(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Js.Controls.Picker.ObjectArgs">
        /// </param>
        this._changeNow(0, (e.object == null) ? 0 : e.object.k);
    },
    
    _changeNow: function Js_Pages_FindYourPhoto_Controller$_changeNow(usrK, eventK) {
        /// <param name="usrK" type="Number" integer="true">
        /// </param>
        /// <param name="eventK" type="Number" integer="true">
        /// </param>
        if (!usrK && !eventK) {
            this.view.get_resultOuter().style.display = 'none';
            return;
        }
        var url = '/pages/findyourphotocontent.aspx?' + ((usrK > 0) ? ('usrk=' + usrK.toString()) : ('eventk=' + eventK.toString()));
        this._requestId++;
        var currentRequestId = this._requestId;
        var currentLoadId = this._loadId;
        $.get(url, null, ss.Delegate.create(this, this._gotContent), null, currentRequestId.toString());
        window.setTimeout(ss.Delegate.create(this, function() {
            if (this._loadId === currentLoadId) {
                this.view.get_loadingOverlay().style.height = this.view.get_result().offsetHeight.toString() + 'px';
                this.view.get_loadingOverlay().style.display = '';
            }
        }), 100);
    },
    
    _requestId: 0,
    _loadId: 0,
    
    _gotContent: function Js_Pages_FindYourPhoto_Controller$_gotContent(data, textStatus, req, args) {
        /// <param name="data" type="String">
        /// </param>
        /// <param name="textStatus" type="String">
        /// </param>
        /// <param name="req" type="jQueryXmlHttpRequest">
        /// </param>
        /// <param name="args" type="String">
        /// </param>
        var requestIdFromArgs = parseInt(args);
        if (this._requestId === requestIdFromArgs) {
            this._loadId++;
            this.view.get_result().innerHTML = data;
            this.view.get_resultOuter().style.display = '';
            this.view.get_loadingOverlay().style.display = 'none';
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.FindYourPhoto.View

Js.Pages.FindYourPhoto.View = function Js_Pages_FindYourPhoto_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_TopIcon$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_TopIconJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_ResultOuter$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_ResultOuterJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LoadingOverlay$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LoadingOverlayJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_Result$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_ResultJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Pages.FindYourPhoto.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.FindYourPhoto.View.prototype = {
    clientId: null,
    
    get_topIcon: function Js_Pages_FindYourPhoto_View$get_topIcon() {
        /// <value type="Object" domElement="true"></value>
        if (this._TopIcon$2 == null) {
            this._TopIcon$2 = document.getElementById(this.clientId + '_TopIcon');
        }
        return this._TopIcon$2;
    },
    
    _TopIcon$2: null,
    
    get_topIconJ: function Js_Pages_FindYourPhoto_View$get_topIconJ() {
        /// <value type="jQueryObject"></value>
        if (this._TopIconJ$2 == null) {
            this._TopIconJ$2 = $('#' + this.clientId + '_TopIcon');
        }
        return this._TopIconJ$2;
    },
    
    _TopIconJ$2: null,
    
    get_picker: function Js_Pages_FindYourPhoto_View$get_picker() {
        /// <value type="Js.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_PickerController');
    },
    
    get_resultOuter: function Js_Pages_FindYourPhoto_View$get_resultOuter() {
        /// <value type="Object" domElement="true"></value>
        if (this._ResultOuter$2 == null) {
            this._ResultOuter$2 = document.getElementById(this.clientId + '_ResultOuter');
        }
        return this._ResultOuter$2;
    },
    
    _ResultOuter$2: null,
    
    get_resultOuterJ: function Js_Pages_FindYourPhoto_View$get_resultOuterJ() {
        /// <value type="jQueryObject"></value>
        if (this._ResultOuterJ$2 == null) {
            this._ResultOuterJ$2 = $('#' + this.clientId + '_ResultOuter');
        }
        return this._ResultOuterJ$2;
    },
    
    _ResultOuterJ$2: null,
    
    get_loadingOverlay: function Js_Pages_FindYourPhoto_View$get_loadingOverlay() {
        /// <value type="Object" domElement="true"></value>
        if (this._LoadingOverlay$2 == null) {
            this._LoadingOverlay$2 = document.getElementById(this.clientId + '_LoadingOverlay');
        }
        return this._LoadingOverlay$2;
    },
    
    _LoadingOverlay$2: null,
    
    get_loadingOverlayJ: function Js_Pages_FindYourPhoto_View$get_loadingOverlayJ() {
        /// <value type="jQueryObject"></value>
        if (this._LoadingOverlayJ$2 == null) {
            this._LoadingOverlayJ$2 = $('#' + this.clientId + '_LoadingOverlay');
        }
        return this._LoadingOverlayJ$2;
    },
    
    _LoadingOverlayJ$2: null,
    
    get_result: function Js_Pages_FindYourPhoto_View$get_result() {
        /// <value type="Object" domElement="true"></value>
        if (this._Result$2 == null) {
            this._Result$2 = document.getElementById(this.clientId + '_Result');
        }
        return this._Result$2;
    },
    
    _Result$2: null,
    
    get_resultJ: function Js_Pages_FindYourPhoto_View$get_resultJ() {
        /// <value type="jQueryObject"></value>
        if (this._ResultJ$2 == null) {
            this._ResultJ$2 = $('#' + this.clientId + '_Result');
        }
        return this._ResultJ$2;
    },
    
    _ResultJ$2: null,
    
    get_genericContainerPage: function Js_Pages_FindYourPhoto_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Pages_FindYourPhoto_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Pages.FindYourPhoto.Controller.registerClass('Js.Pages.FindYourPhoto.Controller');
Js.Pages.FindYourPhoto.View.registerClass('Js.Pages.FindYourPhoto.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
