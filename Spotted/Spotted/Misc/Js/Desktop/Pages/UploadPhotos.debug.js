//! UploadPhotos.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.UploadPhotos');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.UploadPhotos.Controller

Js.Pages.UploadPhotos.Controller = function Js_Pages_UploadPhotos_Controller(v) {
    /// <param name="v" type="Js.Pages.UploadPhotos.View">
    /// </param>
    /// <field name="view" type="Js.Pages.UploadPhotos.View">
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
    this.view.get_picker().eventSelectionSepcificationChanged = ss.Delegate.create(this, this.eventSelectionChange);
    if (Js.Library.Misc.get_browserIsIE()) {
        $(ss.Delegate.create(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
Js.Pages.UploadPhotos.Controller.prototype = {
    view: null,
    
    _initialise: function Js_Pages_UploadPhotos_Controller$_initialise() {
        this.view.get_backLinkJ().click(ss.Delegate.create(this, this.backLinkClick));
        this.view.get_forwardLinkJ().click(ss.Delegate.create(this, this.forwardLinkClick));
    },
    
    backLinkClick: function Js_Pages_UploadPhotos_Controller$backLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this.view.get_picker().dateModify(-7, 'd');
    },
    
    forwardLinkClick: function Js_Pages_UploadPhotos_Controller$forwardLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this.view.get_picker().dateModify(7, 'd');
    },
    
    _updateDate: function Js_Pages_UploadPhotos_Controller$_updateDate(newDate) {
        /// <param name="newDate" type="Js.Controls.Picker.DateStub">
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
    
    eventSelectionChange: function Js_Pages_UploadPhotos_Controller$eventSelectionChange(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Js.Controls.Picker.EventSelectionArgs">
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
            $.get(url, null, ss.Delegate.create(this, this._gotCalendar), null, currentRequestId.toString());
            window.setTimeout(ss.Delegate.create(this, function() {
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
    
    _gotCalendar: function Js_Pages_UploadPhotos_Controller$_gotCalendar(data, textStatus, req, args) {
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
            this.view.get_calendarHolder().innerHTML = data;
            this.view.get_calendarHolderOuter().style.display = '';
            this.view.get_calendarLoadingOverlay().style.display = 'none';
            this.view.get_loadingLabel().style.display = 'none';
            this.view.get_monthLabel().style.display = '';
        }
    },
    
    _debugCount: 0,
    
    _debug: function Js_Pages_UploadPhotos_Controller$_debug(text) {
        /// <param name="text" type="String">
        /// </param>
        this.view.get_debug().style.display = '';
        this._debugCount++;
        this.view.get_debug().value = this._debugCount.toString() + ' ' + text + '\n' + this.view.get_debug().value;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.UploadPhotos.View

Js.Pages.UploadPhotos.View = function Js_Pages_UploadPhotos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_NewUserWizardOptions$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_NewUserWizardOptionsJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_FindEventsHeader$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_FindEventsHeaderJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_TopIcon$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_TopIconJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_Debug$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_DebugJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_CalendarHolderOuter$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_CalendarHolderOuterJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_BackLink$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_BackLinkJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_MonthLabel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_MonthLabelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_LoadingLabel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_LoadingLabelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_ForwardLink$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_ForwardLinkJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_CalendarLoadingOverlay$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_CalendarLoadingOverlayJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_CalendarHolder$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_CalendarHolderJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Pages.UploadPhotos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.UploadPhotos.View.prototype = {
    clientId: null,
    
    get_newUserWizardOptions: function Js_Pages_UploadPhotos_View$get_newUserWizardOptions() {
        /// <value type="Object" domElement="true"></value>
        if (this._NewUserWizardOptions$2 == null) {
            this._NewUserWizardOptions$2 = document.getElementById(this.clientId + '_NewUserWizardOptions');
        }
        return this._NewUserWizardOptions$2;
    },
    
    _NewUserWizardOptions$2: null,
    
    get_newUserWizardOptionsJ: function Js_Pages_UploadPhotos_View$get_newUserWizardOptionsJ() {
        /// <value type="jQueryObject"></value>
        if (this._NewUserWizardOptionsJ$2 == null) {
            this._NewUserWizardOptionsJ$2 = $('#' + this.clientId + '_NewUserWizardOptions');
        }
        return this._NewUserWizardOptionsJ$2;
    },
    
    _NewUserWizardOptionsJ$2: null,
    
    get_findEventsHeader: function Js_Pages_UploadPhotos_View$get_findEventsHeader() {
        /// <value type="Object" domElement="true"></value>
        if (this._FindEventsHeader$2 == null) {
            this._FindEventsHeader$2 = document.getElementById(this.clientId + '_FindEventsHeader');
        }
        return this._FindEventsHeader$2;
    },
    
    _FindEventsHeader$2: null,
    
    get_findEventsHeaderJ: function Js_Pages_UploadPhotos_View$get_findEventsHeaderJ() {
        /// <value type="jQueryObject"></value>
        if (this._FindEventsHeaderJ$2 == null) {
            this._FindEventsHeaderJ$2 = $('#' + this.clientId + '_FindEventsHeader');
        }
        return this._FindEventsHeaderJ$2;
    },
    
    _FindEventsHeaderJ$2: null,
    
    get_topIcon: function Js_Pages_UploadPhotos_View$get_topIcon() {
        /// <value type="Object" domElement="true"></value>
        if (this._TopIcon$2 == null) {
            this._TopIcon$2 = document.getElementById(this.clientId + '_TopIcon');
        }
        return this._TopIcon$2;
    },
    
    _TopIcon$2: null,
    
    get_topIconJ: function Js_Pages_UploadPhotos_View$get_topIconJ() {
        /// <value type="jQueryObject"></value>
        if (this._TopIconJ$2 == null) {
            this._TopIconJ$2 = $('#' + this.clientId + '_TopIcon');
        }
        return this._TopIconJ$2;
    },
    
    _TopIconJ$2: null,
    
    get_debug: function Js_Pages_UploadPhotos_View$get_debug() {
        /// <value type="Object" domElement="true"></value>
        if (this._Debug$2 == null) {
            this._Debug$2 = document.getElementById(this.clientId + '_Debug');
        }
        return this._Debug$2;
    },
    
    _Debug$2: null,
    
    get_debugJ: function Js_Pages_UploadPhotos_View$get_debugJ() {
        /// <value type="jQueryObject"></value>
        if (this._DebugJ$2 == null) {
            this._DebugJ$2 = $('#' + this.clientId + '_Debug');
        }
        return this._DebugJ$2;
    },
    
    _DebugJ$2: null,
    
    get_picker: function Js_Pages_UploadPhotos_View$get_picker() {
        /// <value type="Js.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_PickerController');
    },
    
    get_calendarHolderOuter: function Js_Pages_UploadPhotos_View$get_calendarHolderOuter() {
        /// <value type="Object" domElement="true"></value>
        if (this._CalendarHolderOuter$2 == null) {
            this._CalendarHolderOuter$2 = document.getElementById(this.clientId + '_CalendarHolderOuter');
        }
        return this._CalendarHolderOuter$2;
    },
    
    _CalendarHolderOuter$2: null,
    
    get_calendarHolderOuterJ: function Js_Pages_UploadPhotos_View$get_calendarHolderOuterJ() {
        /// <value type="jQueryObject"></value>
        if (this._CalendarHolderOuterJ$2 == null) {
            this._CalendarHolderOuterJ$2 = $('#' + this.clientId + '_CalendarHolderOuter');
        }
        return this._CalendarHolderOuterJ$2;
    },
    
    _CalendarHolderOuterJ$2: null,
    
    get_backLink: function Js_Pages_UploadPhotos_View$get_backLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._BackLink$2 == null) {
            this._BackLink$2 = document.getElementById(this.clientId + '_BackLink');
        }
        return this._BackLink$2;
    },
    
    _BackLink$2: null,
    
    get_backLinkJ: function Js_Pages_UploadPhotos_View$get_backLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._BackLinkJ$2 == null) {
            this._BackLinkJ$2 = $('#' + this.clientId + '_BackLink');
        }
        return this._BackLinkJ$2;
    },
    
    _BackLinkJ$2: null,
    
    get_monthLabel: function Js_Pages_UploadPhotos_View$get_monthLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._MonthLabel$2 == null) {
            this._MonthLabel$2 = document.getElementById(this.clientId + '_MonthLabel');
        }
        return this._MonthLabel$2;
    },
    
    _MonthLabel$2: null,
    
    get_monthLabelJ: function Js_Pages_UploadPhotos_View$get_monthLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._MonthLabelJ$2 == null) {
            this._MonthLabelJ$2 = $('#' + this.clientId + '_MonthLabel');
        }
        return this._MonthLabelJ$2;
    },
    
    _MonthLabelJ$2: null,
    
    get_loadingLabel: function Js_Pages_UploadPhotos_View$get_loadingLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._LoadingLabel$2 == null) {
            this._LoadingLabel$2 = document.getElementById(this.clientId + '_LoadingLabel');
        }
        return this._LoadingLabel$2;
    },
    
    _LoadingLabel$2: null,
    
    get_loadingLabelJ: function Js_Pages_UploadPhotos_View$get_loadingLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._LoadingLabelJ$2 == null) {
            this._LoadingLabelJ$2 = $('#' + this.clientId + '_LoadingLabel');
        }
        return this._LoadingLabelJ$2;
    },
    
    _LoadingLabelJ$2: null,
    
    get_forwardLink: function Js_Pages_UploadPhotos_View$get_forwardLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._ForwardLink$2 == null) {
            this._ForwardLink$2 = document.getElementById(this.clientId + '_ForwardLink');
        }
        return this._ForwardLink$2;
    },
    
    _ForwardLink$2: null,
    
    get_forwardLinkJ: function Js_Pages_UploadPhotos_View$get_forwardLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._ForwardLinkJ$2 == null) {
            this._ForwardLinkJ$2 = $('#' + this.clientId + '_ForwardLink');
        }
        return this._ForwardLinkJ$2;
    },
    
    _ForwardLinkJ$2: null,
    
    get_calendarLoadingOverlay: function Js_Pages_UploadPhotos_View$get_calendarLoadingOverlay() {
        /// <value type="Object" domElement="true"></value>
        if (this._CalendarLoadingOverlay$2 == null) {
            this._CalendarLoadingOverlay$2 = document.getElementById(this.clientId + '_CalendarLoadingOverlay');
        }
        return this._CalendarLoadingOverlay$2;
    },
    
    _CalendarLoadingOverlay$2: null,
    
    get_calendarLoadingOverlayJ: function Js_Pages_UploadPhotos_View$get_calendarLoadingOverlayJ() {
        /// <value type="jQueryObject"></value>
        if (this._CalendarLoadingOverlayJ$2 == null) {
            this._CalendarLoadingOverlayJ$2 = $('#' + this.clientId + '_CalendarLoadingOverlay');
        }
        return this._CalendarLoadingOverlayJ$2;
    },
    
    _CalendarLoadingOverlayJ$2: null,
    
    get_calendarHolder: function Js_Pages_UploadPhotos_View$get_calendarHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._CalendarHolder$2 == null) {
            this._CalendarHolder$2 = document.getElementById(this.clientId + '_CalendarHolder');
        }
        return this._CalendarHolder$2;
    },
    
    _CalendarHolder$2: null,
    
    get_calendarHolderJ: function Js_Pages_UploadPhotos_View$get_calendarHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._CalendarHolderJ$2 == null) {
            this._CalendarHolderJ$2 = $('#' + this.clientId + '_CalendarHolder');
        }
        return this._CalendarHolderJ$2;
    },
    
    _CalendarHolderJ$2: null,
    
    get_genericContainerPage: function Js_Pages_UploadPhotos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Pages_UploadPhotos_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Pages.UploadPhotos.Controller.registerClass('Js.Pages.UploadPhotos.Controller');
Js.Pages.UploadPhotos.View.registerClass('Js.Pages.UploadPhotos.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
