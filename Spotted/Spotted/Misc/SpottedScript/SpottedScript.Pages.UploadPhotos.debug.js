Type.registerNamespace('SpottedScript.Pages.UploadPhotos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.UploadPhotos.Controller
SpottedScript.Pages.UploadPhotos.Controller = function SpottedScript_Pages_UploadPhotos_Controller(v) {
    /// <param name="v" type="SpottedScript.Pages.UploadPhotos.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Pages.UploadPhotos.View">
    /// </field>
    /// <field name="_previousSpecificationState" type="String">
    /// </field>
    /// <field name="_requestId" type="Number" integer="true">
    /// </field>
    /// <field name="_loadId" type="Number" integer="true">
    /// </field>
    /// <field name="_debugCount" type="Number" integer="true">
    /// </field>
    this.view = v;
    this.view.get_picker().eventSelectionSepcificationChanged = Function.createDelegate(this, this.eventSelectionChange);
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.Pages.UploadPhotos.Controller.prototype = {
    view: null,
    _initialise: function SpottedScript_Pages_UploadPhotos_Controller$_initialise() {
        $addHandler(this.view.get_backLink(), 'click', Function.createDelegate(this, this.backLinkClick));
        $addHandler(this.view.get_forwardLink(), 'click', Function.createDelegate(this, this.forwardLinkClick));
    },
    backLinkClick: function SpottedScript_Pages_UploadPhotos_Controller$backLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this.view.get_picker().dateModify(-7, 'd');
    },
    forwardLinkClick: function SpottedScript_Pages_UploadPhotos_Controller$forwardLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this.view.get_picker().dateModify(7, 'd');
    },
    _updateDate: function SpottedScript_Pages_UploadPhotos_Controller$_updateDate(newDate) {
        /// <param name="newDate" type="SpottedScript.Controls.Picker.DateStub">
        /// </param>
        var year = new Date().getFullYear();
        var previousMonday = newDate.previousMonday();
        var nextSunday = newDate.nextSunday();
        if (newDate.nextSunday().month === newDate.previousMonday().month) {
            this.view.get_monthLabel().innerHTML = previousMonday.day + ' - ' + nextSunday.day + ' ' + nextSunday.get_monthNameFull() + ((nextSunday.year !== year) ? (' ' + nextSunday.year.toString()) : '');
        }
        else {
            this.view.get_monthLabel().innerHTML = previousMonday.day + ' ' + previousMonday.get_monthNameFull() + ' - ' + nextSunday.day + ' ' + nextSunday.get_monthNameFull() + ((nextSunday.year !== year) ? (' ' + nextSunday.year.toString()) : '');
        }
    },
    _previousSpecificationState: '',
    eventSelectionChange: function SpottedScript_Pages_UploadPhotos_Controller$eventSelectionChange(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="SpottedScript.Controls.Picker.EventSelectionArgs">
        /// </param>
        var hasBrand = e.specification != null && e.specification.brand != null && e.specification.brand.k > 0;
        var hasPlace = e.specification != null && e.specification.place != null && e.specification.place.k > 0;
        var hasVenue = e.specification != null && e.specification.venue != null && e.specification.venue.k > 0;
        var hasMusic = e.specification != null && e.specification.music != null && e.specification.music.k > 0;
        var hasMe = e.specification != null && e.specification.me;
        if (!hasMe && !hasBrand && !hasVenue && !(hasMusic && hasPlace)) {
            this.view.get_calendarHolderOuter().style.display = 'none';
            return;
        }
        var specificationState = 'brand-' + ((e.specification.brand == null) ? '0' : e.specification.brand.k.toString()) + '|' + 'place-' + ((e.specification.place == null) ? '0' : e.specification.place.k.toString()) + '|' + 'venue-' + ((e.specification.venue == null) ? '0' : e.specification.venue.k.toString()) + '|' + 'music-' + ((e.specification.music == null) ? '0' : e.specification.music.k.toString()) + '|' + 'date-' + ((e.specification.date == null) ? '0' : e.specification.date.toString()) + '|' + 'me-' + ((e.specification.me) ? '1' : '0');
        if (specificationState !== this._previousSpecificationState) {
            this._previousSpecificationState = specificationState;
            var url = '/support/getuncached.aspx?type=calendar&addgallery=1' + '&brandk=' + ((e.specification.brand == null) ? '0' : e.specification.brand.k.toString()) + '&placek=' + ((e.specification.place == null) ? '0' : e.specification.place.k.toString()) + '&venuek=' + ((e.specification.venue == null) ? '0' : e.specification.venue.k.toString()) + '&musictypek=' + ((e.specification.music == null) ? '0' : e.specification.music.k.toString()) + '&date=' + ((e.specification.date == null) ? '0' : e.specification.date.toString()) + '&me=' + ((e.specification.me) ? '1' : '0');
            this._updateDate(e.specification.date);
            this._requestId++;
            var currentRequestId = this._requestId;
            var currentLoadId = this._loadId;
            jQuery.get(url, null, Function.createDelegate(this, this._gotCalendar), null, currentRequestId.toString());
            window.setTimeout(Function.createDelegate(this, function() {
                if (this._loadId === currentLoadId) {
                    this.view.get_calendarLoadingOverlay().style.height = this.view.get_calendarHolder().offsetHeight.toString() + 'px';
                    this.view.get_calendarLoadingOverlay().style.display = '';
                    this.view.get_loadingLabel().style.display = '';
                    this.view.get_monthLabel().style.display = 'none';
                }
            }), 100);
        }
        else {
            this.view.get_calendarHolderOuter().style.display = '';
            this.view.get_calendarLoadingOverlay().style.display = 'none';
            this.view.get_loadingLabel().style.display = 'none';
            this.view.get_monthLabel().style.display = '';
        }
    },
    _requestId: 0,
    _loadId: 0,
    _gotCalendar: function SpottedScript_Pages_UploadPhotos_Controller$_gotCalendar(data, textStatus, req, args) {
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
            this.view.get_calendarHolder().innerHTML = data;
            this.view.get_calendarHolderOuter().style.display = '';
            this.view.get_calendarLoadingOverlay().style.display = 'none';
            this.view.get_loadingLabel().style.display = 'none';
            this.view.get_monthLabel().style.display = '';
        }
    },
    _debugCount: 0,
    _debug: function SpottedScript_Pages_UploadPhotos_Controller$_debug(text) {
        /// <param name="text" type="String">
        /// </param>
        this.view.get_debug().style.display = '';
        this._debugCount++;
        this.view.get_debug().value = this._debugCount.toString() + ' ' + text + '\n' + this.view.get_debug().value;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.UploadPhotos.View
SpottedScript.Pages.UploadPhotos.View = function SpottedScript_Pages_UploadPhotos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.UploadPhotos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.UploadPhotos.View.prototype = {
    clientId: null,
    get_newUserWizardOptions: function SpottedScript_Pages_UploadPhotos_View$get_newUserWizardOptions() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewUserWizardOptions');
    },
    get_findEventsHeader: function SpottedScript_Pages_UploadPhotos_View$get_findEventsHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FindEventsHeader');
    },
    get_topIcon: function SpottedScript_Pages_UploadPhotos_View$get_topIcon() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopIcon');
    },
    get_debug: function SpottedScript_Pages_UploadPhotos_View$get_debug() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Debug');
    },
    get_picker: function SpottedScript_Pages_UploadPhotos_View$get_picker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_PickerController');
    },
    get_calendarHolderOuter: function SpottedScript_Pages_UploadPhotos_View$get_calendarHolderOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarHolderOuter');
    },
    get_backLink: function SpottedScript_Pages_UploadPhotos_View$get_backLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BackLink');
    },
    get_monthLabel: function SpottedScript_Pages_UploadPhotos_View$get_monthLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MonthLabel');
    },
    get_loadingLabel: function SpottedScript_Pages_UploadPhotos_View$get_loadingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoadingLabel');
    },
    get_forwardLink: function SpottedScript_Pages_UploadPhotos_View$get_forwardLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ForwardLink');
    },
    get_calendarLoadingOverlay: function SpottedScript_Pages_UploadPhotos_View$get_calendarLoadingOverlay() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarLoadingOverlay');
    },
    get_calendarHolder: function SpottedScript_Pages_UploadPhotos_View$get_calendarHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarHolder');
    },
    get_genericContainerPage: function SpottedScript_Pages_UploadPhotos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.UploadPhotos.Controller.registerClass('SpottedScript.Pages.UploadPhotos.Controller');
SpottedScript.Pages.UploadPhotos.View.registerClass('SpottedScript.Pages.UploadPhotos.View', SpottedScript.DsiUserControl.View);
