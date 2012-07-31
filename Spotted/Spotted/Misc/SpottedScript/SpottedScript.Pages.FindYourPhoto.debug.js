Type.registerNamespace('SpottedScript.Pages.FindYourPhoto');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.FindYourPhoto.Controller
SpottedScript.Pages.FindYourPhoto.Controller = function SpottedScript_Pages_FindYourPhoto_Controller(v) {
    /// <param name="v" type="SpottedScript.Pages.FindYourPhoto.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Pages.FindYourPhoto.View">
    /// </field>
    /// <field name="_requestId" type="Number" integer="true">
    /// </field>
    /// <field name="_loadId" type="Number" integer="true">
    /// </field>
    this.view = v;
    this.view.get_picker().selectedSpotterChanged = Function.createDelegate(this, this._spotterChanged);
    this.view.get_picker().selectedEventChanged = Function.createDelegate(this, this._eventChanged);
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.Pages.FindYourPhoto.Controller.prototype = {
    view: null,
    _initialise: function SpottedScript_Pages_FindYourPhoto_Controller$_initialise() {
    },
    _spotterChanged: function SpottedScript_Pages_FindYourPhoto_Controller$_spotterChanged(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="SpottedScript.Controls.Picker.StringArgs">
        /// </param>
        var usrK = 0;
        try {
            usrK = Number.parseInvariant(e.val.replace('-', ''));
        }
        catch ($e1) {
        }
        if (usrK > 0) {
            this._changeNow(usrK, 0);
        }
        else {
            this.view.get_result().innerHTML = 'Can\'t find this spotter. The spotter code should be 8 digits, like 1234-5678.';
            this.view.get_resultOuter().style.display = '';
            this.view.get_loadingOverlay().style.display = 'none';
        }
    },
    _eventChanged: function SpottedScript_Pages_FindYourPhoto_Controller$_eventChanged(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="SpottedScript.Controls.Picker.ObjectArgs">
        /// </param>
        this._changeNow(0, (e.object == null) ? 0 : e.object.k);
    },
    _changeNow: function SpottedScript_Pages_FindYourPhoto_Controller$_changeNow(usrK, eventK) {
        /// <param name="usrK" type="Number" integer="true">
        /// </param>
        /// <param name="eventK" type="Number" integer="true">
        /// </param>
        if (usrK === 0 && eventK === 0) {
            this.view.get_resultOuter().style.display = 'none';
            return;
        }
        var url = '/pages/findyourphotocontent.aspx?' + ((usrK > 0) ? ('usrk=' + usrK.toString()) : ('eventk=' + eventK.toString()));
        this._requestId++;
        var currentRequestId = this._requestId;
        var currentLoadId = this._loadId;
        jQuery.get(url, null, Function.createDelegate(this, this._gotContent), null, currentRequestId.toString());
        window.setTimeout(Function.createDelegate(this, function() {
            if (this._loadId === currentLoadId) {
                this.view.get_loadingOverlay().style.height = this.view.get_result().offsetHeight.toString() + 'px';
                this.view.get_loadingOverlay().style.display = '';
            }
        }), 100);
    },
    _requestId: 0,
    _loadId: 0,
    _gotContent: function SpottedScript_Pages_FindYourPhoto_Controller$_gotContent(data, textStatus, req, args) {
        /// <param name="data" type="String">
        /// </param>
        /// <param name="textStatus" type="String">
        /// </param>
        /// <param name="req" type="XMLHttpRequest">
        /// </param>
        /// <param name="args" type="String">
        /// </param>
        var requestIdFromArgs = Number.parseInvariant(args);
        if (this._requestId === requestIdFromArgs) {
            this._loadId++;
            this.view.get_result().innerHTML = data;
            this.view.get_resultOuter().style.display = '';
            this.view.get_loadingOverlay().style.display = 'none';
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.FindYourPhoto.View
SpottedScript.Pages.FindYourPhoto.View = function SpottedScript_Pages_FindYourPhoto_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.FindYourPhoto.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.FindYourPhoto.View.prototype = {
    clientId: null,
    get_topIcon: function SpottedScript_Pages_FindYourPhoto_View$get_topIcon() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopIcon');
    },
    get_picker: function SpottedScript_Pages_FindYourPhoto_View$get_picker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_PickerController');
    },
    get_resultOuter: function SpottedScript_Pages_FindYourPhoto_View$get_resultOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ResultOuter');
    },
    get_loadingOverlay: function SpottedScript_Pages_FindYourPhoto_View$get_loadingOverlay() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoadingOverlay');
    },
    get_result: function SpottedScript_Pages_FindYourPhoto_View$get_result() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Result');
    },
    get_genericContainerPage: function SpottedScript_Pages_FindYourPhoto_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.FindYourPhoto.Controller.registerClass('SpottedScript.Pages.FindYourPhoto.Controller');
SpottedScript.Pages.FindYourPhoto.View.registerClass('SpottedScript.Pages.FindYourPhoto.View', SpottedScript.DsiUserControl.View);
