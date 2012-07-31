Type.registerNamespace('SpottedScript.Admin.SalesCallsAndStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesCallsAndStats.View
SpottedScript.Admin.SalesCallsAndStats.View = function SpottedScript_Admin_SalesCallsAndStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesCallsAndStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesCallsAndStats.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Admin_SalesCallsAndStats_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_headerPanel: function SpottedScript_Admin_SalesCallsAndStats_View$get_headerPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HeaderPanel');
    },
    get_myTodayButton: function SpottedScript_Admin_SalesCallsAndStats_View$get_myTodayButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyTodayButton');
    },
    get_myMonthButton: function SpottedScript_Admin_SalesCallsAndStats_View$get_myMonthButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyMonthButton');
    },
    get_teamTodayButton: function SpottedScript_Admin_SalesCallsAndStats_View$get_teamTodayButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TeamTodayButton');
    },
    get_teamMonthButton: function SpottedScript_Admin_SalesCallsAndStats_View$get_teamMonthButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TeamMonthButton');
    },
    get_userLabel: function SpottedScript_Admin_SalesCallsAndStats_View$get_userLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserLabel');
    },
    get_spacerImage1: function SpottedScript_Admin_SalesCallsAndStats_View$get_spacerImage1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpacerImage1');
    },
    get_salesPersonsDropDownList: function SpottedScript_Admin_SalesCallsAndStats_View$get_salesPersonsDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesPersonsDropDownList');
    },
    get_overrideDateCheckBox: function SpottedScript_Admin_SalesCallsAndStats_View$get_overrideDateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideDateCheckBox');
    },
    get_overrideFromDatesRow: function SpottedScript_Admin_SalesCallsAndStats_View$get_overrideFromDatesRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideFromDatesRow');
    },
    get_fromLabel: function SpottedScript_Admin_SalesCallsAndStats_View$get_fromLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FromLabel');
    },
    get_fromDateCal: function SpottedScript_Admin_SalesCallsAndStats_View$get_fromDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_FromDateCalController');
    },
    get_overrideToDatesRow: function SpottedScript_Admin_SalesCallsAndStats_View$get_overrideToDatesRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverrideToDatesRow');
    },
    get_toLabel: function SpottedScript_Admin_SalesCallsAndStats_View$get_toLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ToLabel');
    },
    get_toDateCal: function SpottedScript_Admin_SalesCallsAndStats_View$get_toDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_ToDateCalController');
    },
    get_dateRangeCustomValidator: function SpottedScript_Admin_SalesCallsAndStats_View$get_dateRangeCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateRangeCustomValidator');
    },
    get_salesCallsDailyButton: function SpottedScript_Admin_SalesCallsAndStats_View$get_salesCallsDailyButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesCallsDailyButton');
    },
    get_salesCallsWeeklyButton: function SpottedScript_Admin_SalesCallsAndStats_View$get_salesCallsWeeklyButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesCallsWeeklyButton');
    },
    get_salesCallsMonthlyButton: function SpottedScript_Admin_SalesCallsAndStats_View$get_salesCallsMonthlyButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesCallsMonthlyButton');
    },
    get_salesStatsButton: function SpottedScript_Admin_SalesCallsAndStats_View$get_salesStatsButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesStatsButton');
    },
    get_notSalesPersonCustomValidator: function SpottedScript_Admin_SalesCallsAndStats_View$get_notSalesPersonCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotSalesPersonCustomValidator');
    },
    get_salesValidationSummary: function SpottedScript_Admin_SalesCallsAndStats_View$get_salesValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesValidationSummary');
    },
    get_resultsPanel: function SpottedScript_Admin_SalesCallsAndStats_View$get_resultsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ResultsPanel');
    },
    get_dateRangeLabel: function SpottedScript_Admin_SalesCallsAndStats_View$get_dateRangeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateRangeLabel');
    },
    get_dateRangeValueLabel: function SpottedScript_Admin_SalesCallsAndStats_View$get_dateRangeValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateRangeValueLabel');
    },
    get_salesCallsResultsTable: function SpottedScript_Admin_SalesCallsAndStats_View$get_salesCallsResultsTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesCallsResultsTable');
    },
    get_salesStatsResultsTable: function SpottedScript_Admin_SalesCallsAndStats_View$get_salesStatsResultsTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SalesStatsResultsTable');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesCallsAndStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesCallsAndStats.View.registerClass('SpottedScript.Admin.SalesCallsAndStats.View', SpottedScript.AdminUserControl.View);
