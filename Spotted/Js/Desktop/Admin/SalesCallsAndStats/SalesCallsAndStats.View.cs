//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Image", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.SalesCallsAndStats
{
	public partial class View
		 : Js.AdminUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement HeaderPanel {get {if (_HeaderPanel == null) {_HeaderPanel = (DivElement)Document.GetElementById(clientId + "_HeaderPanel");}; return _HeaderPanel;}} private DivElement _HeaderPanel;
		public jQueryObject HeaderPanelJ {get {if (_HeaderPanelJ == null) {_HeaderPanelJ = jQuery.Select("#" + clientId + "_HeaderPanel");}; return _HeaderPanelJ;}} private jQueryObject _HeaderPanelJ;
		public Element MyTodayButton {get {if (_MyTodayButton == null) {_MyTodayButton = (Element)Document.GetElementById(clientId + "_MyTodayButton");}; return _MyTodayButton;}} private Element _MyTodayButton;
		public jQueryObject MyTodayButtonJ {get {if (_MyTodayButtonJ == null) {_MyTodayButtonJ = jQuery.Select("#" + clientId + "_MyTodayButton");}; return _MyTodayButtonJ;}} private jQueryObject _MyTodayButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element MyMonthButton {get {if (_MyMonthButton == null) {_MyMonthButton = (Element)Document.GetElementById(clientId + "_MyMonthButton");}; return _MyMonthButton;}} private Element _MyMonthButton;
		public jQueryObject MyMonthButtonJ {get {if (_MyMonthButtonJ == null) {_MyMonthButtonJ = jQuery.Select("#" + clientId + "_MyMonthButton");}; return _MyMonthButtonJ;}} private jQueryObject _MyMonthButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element TeamTodayButton {get {if (_TeamTodayButton == null) {_TeamTodayButton = (Element)Document.GetElementById(clientId + "_TeamTodayButton");}; return _TeamTodayButton;}} private Element _TeamTodayButton;
		public jQueryObject TeamTodayButtonJ {get {if (_TeamTodayButtonJ == null) {_TeamTodayButtonJ = jQuery.Select("#" + clientId + "_TeamTodayButton");}; return _TeamTodayButtonJ;}} private jQueryObject _TeamTodayButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element TeamMonthButton {get {if (_TeamMonthButton == null) {_TeamMonthButton = (Element)Document.GetElementById(clientId + "_TeamMonthButton");}; return _TeamMonthButton;}} private Element _TeamMonthButton;
		public jQueryObject TeamMonthButtonJ {get {if (_TeamMonthButtonJ == null) {_TeamMonthButtonJ = jQuery.Select("#" + clientId + "_TeamMonthButton");}; return _TeamMonthButtonJ;}} private jQueryObject _TeamMonthButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element UserLabel {get {if (_UserLabel == null) {_UserLabel = (Element)Document.GetElementById(clientId + "_UserLabel");}; return _UserLabel;}} private Element _UserLabel;
		public jQueryObject UserLabelJ {get {if (_UserLabelJ == null) {_UserLabelJ = jQuery.Select("#" + clientId + "_UserLabel");}; return _UserLabelJ;}} private jQueryObject _UserLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SpacerImage1 {get {if (_SpacerImage1 == null) {_SpacerImage1 = (Element)Document.GetElementById(clientId + "_SpacerImage1");}; return _SpacerImage1;}} private Element _SpacerImage1;
		public jQueryObject SpacerImage1J {get {if (_SpacerImage1J == null) {_SpacerImage1J = jQuery.Select("#" + clientId + "_SpacerImage1");}; return _SpacerImage1J;}} private jQueryObject _SpacerImage1J;//mappings.Add("System.Web.UI.WebControls.Image", ElementGetter("Element"));
		public SelectElement SalesPersonsDropDownList {get {if (_SalesPersonsDropDownList == null) {_SalesPersonsDropDownList = (SelectElement)Document.GetElementById(clientId + "_SalesPersonsDropDownList");}; return _SalesPersonsDropDownList;}} private SelectElement _SalesPersonsDropDownList;
		public jQueryObject SalesPersonsDropDownListJ {get {if (_SalesPersonsDropDownListJ == null) {_SalesPersonsDropDownListJ = jQuery.Select("#" + clientId + "_SalesPersonsDropDownList");}; return _SalesPersonsDropDownListJ;}} private jQueryObject _SalesPersonsDropDownListJ;
		public CheckBoxElement OverrideDateCheckBox {get {if (_OverrideDateCheckBox == null) {_OverrideDateCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_OverrideDateCheckBox");}; return _OverrideDateCheckBox;}} private CheckBoxElement _OverrideDateCheckBox;
		public jQueryObject OverrideDateCheckBoxJ {get {if (_OverrideDateCheckBoxJ == null) {_OverrideDateCheckBoxJ = jQuery.Select("#" + clientId + "_OverrideDateCheckBox");}; return _OverrideDateCheckBoxJ;}} private jQueryObject _OverrideDateCheckBoxJ;
		public Element OverrideFromDatesRow {get {if (_OverrideFromDatesRow == null) {_OverrideFromDatesRow = (Element)Document.GetElementById(clientId + "_OverrideFromDatesRow");}; return _OverrideFromDatesRow;}} private Element _OverrideFromDatesRow;
		public jQueryObject OverrideFromDatesRowJ {get {if (_OverrideFromDatesRowJ == null) {_OverrideFromDatesRowJ = jQuery.Select("#" + clientId + "_OverrideFromDatesRow");}; return _OverrideFromDatesRowJ;}} private jQueryObject _OverrideFromDatesRowJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element FromLabel {get {if (_FromLabel == null) {_FromLabel = (Element)Document.GetElementById(clientId + "_FromLabel");}; return _FromLabel;}} private Element _FromLabel;
		public jQueryObject FromLabelJ {get {if (_FromLabelJ == null) {_FromLabelJ = jQuery.Select("#" + clientId + "_FromLabel");}; return _FromLabelJ;}} private jQueryObject _FromLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Js.CustomControls.Cal.Controller FromDateCal {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_FromDateCalController");}}
		public Element OverrideToDatesRow {get {if (_OverrideToDatesRow == null) {_OverrideToDatesRow = (Element)Document.GetElementById(clientId + "_OverrideToDatesRow");}; return _OverrideToDatesRow;}} private Element _OverrideToDatesRow;
		public jQueryObject OverrideToDatesRowJ {get {if (_OverrideToDatesRowJ == null) {_OverrideToDatesRowJ = jQuery.Select("#" + clientId + "_OverrideToDatesRow");}; return _OverrideToDatesRowJ;}} private jQueryObject _OverrideToDatesRowJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element ToLabel {get {if (_ToLabel == null) {_ToLabel = (Element)Document.GetElementById(clientId + "_ToLabel");}; return _ToLabel;}} private Element _ToLabel;
		public jQueryObject ToLabelJ {get {if (_ToLabelJ == null) {_ToLabelJ = jQuery.Select("#" + clientId + "_ToLabel");}; return _ToLabelJ;}} private jQueryObject _ToLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Js.CustomControls.Cal.Controller ToDateCal {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_ToDateCalController");}}
		public Element DateRangeCustomValidator {get {if (_DateRangeCustomValidator == null) {_DateRangeCustomValidator = (Element)Document.GetElementById(clientId + "_DateRangeCustomValidator");}; return _DateRangeCustomValidator;}} private Element _DateRangeCustomValidator;
		public jQueryObject DateRangeCustomValidatorJ {get {if (_DateRangeCustomValidatorJ == null) {_DateRangeCustomValidatorJ = jQuery.Select("#" + clientId + "_DateRangeCustomValidator");}; return _DateRangeCustomValidatorJ;}} private jQueryObject _DateRangeCustomValidatorJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element SalesCallsDailyButton {get {if (_SalesCallsDailyButton == null) {_SalesCallsDailyButton = (Element)Document.GetElementById(clientId + "_SalesCallsDailyButton");}; return _SalesCallsDailyButton;}} private Element _SalesCallsDailyButton;
		public jQueryObject SalesCallsDailyButtonJ {get {if (_SalesCallsDailyButtonJ == null) {_SalesCallsDailyButtonJ = jQuery.Select("#" + clientId + "_SalesCallsDailyButton");}; return _SalesCallsDailyButtonJ;}} private jQueryObject _SalesCallsDailyButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SalesCallsWeeklyButton {get {if (_SalesCallsWeeklyButton == null) {_SalesCallsWeeklyButton = (Element)Document.GetElementById(clientId + "_SalesCallsWeeklyButton");}; return _SalesCallsWeeklyButton;}} private Element _SalesCallsWeeklyButton;
		public jQueryObject SalesCallsWeeklyButtonJ {get {if (_SalesCallsWeeklyButtonJ == null) {_SalesCallsWeeklyButtonJ = jQuery.Select("#" + clientId + "_SalesCallsWeeklyButton");}; return _SalesCallsWeeklyButtonJ;}} private jQueryObject _SalesCallsWeeklyButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SalesCallsMonthlyButton {get {if (_SalesCallsMonthlyButton == null) {_SalesCallsMonthlyButton = (Element)Document.GetElementById(clientId + "_SalesCallsMonthlyButton");}; return _SalesCallsMonthlyButton;}} private Element _SalesCallsMonthlyButton;
		public jQueryObject SalesCallsMonthlyButtonJ {get {if (_SalesCallsMonthlyButtonJ == null) {_SalesCallsMonthlyButtonJ = jQuery.Select("#" + clientId + "_SalesCallsMonthlyButton");}; return _SalesCallsMonthlyButtonJ;}} private jQueryObject _SalesCallsMonthlyButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SalesStatsButton {get {if (_SalesStatsButton == null) {_SalesStatsButton = (Element)Document.GetElementById(clientId + "_SalesStatsButton");}; return _SalesStatsButton;}} private Element _SalesStatsButton;
		public jQueryObject SalesStatsButtonJ {get {if (_SalesStatsButtonJ == null) {_SalesStatsButtonJ = jQuery.Select("#" + clientId + "_SalesStatsButton");}; return _SalesStatsButtonJ;}} private jQueryObject _SalesStatsButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element NotSalesPersonCustomValidator {get {if (_NotSalesPersonCustomValidator == null) {_NotSalesPersonCustomValidator = (Element)Document.GetElementById(clientId + "_NotSalesPersonCustomValidator");}; return _NotSalesPersonCustomValidator;}} private Element _NotSalesPersonCustomValidator;
		public jQueryObject NotSalesPersonCustomValidatorJ {get {if (_NotSalesPersonCustomValidatorJ == null) {_NotSalesPersonCustomValidatorJ = jQuery.Select("#" + clientId + "_NotSalesPersonCustomValidator");}; return _NotSalesPersonCustomValidatorJ;}} private jQueryObject _NotSalesPersonCustomValidatorJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element SalesValidationSummary {get {if (_SalesValidationSummary == null) {_SalesValidationSummary = (Element)Document.GetElementById(clientId + "_SalesValidationSummary");}; return _SalesValidationSummary;}} private Element _SalesValidationSummary;
		public jQueryObject SalesValidationSummaryJ {get {if (_SalesValidationSummaryJ == null) {_SalesValidationSummaryJ = jQuery.Select("#" + clientId + "_SalesValidationSummary");}; return _SalesValidationSummaryJ;}} private jQueryObject _SalesValidationSummaryJ;//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
		public DivElement ResultsPanel {get {if (_ResultsPanel == null) {_ResultsPanel = (DivElement)Document.GetElementById(clientId + "_ResultsPanel");}; return _ResultsPanel;}} private DivElement _ResultsPanel;
		public jQueryObject ResultsPanelJ {get {if (_ResultsPanelJ == null) {_ResultsPanelJ = jQuery.Select("#" + clientId + "_ResultsPanel");}; return _ResultsPanelJ;}} private jQueryObject _ResultsPanelJ;
		public Element DateRangeLabel {get {if (_DateRangeLabel == null) {_DateRangeLabel = (Element)Document.GetElementById(clientId + "_DateRangeLabel");}; return _DateRangeLabel;}} private Element _DateRangeLabel;
		public jQueryObject DateRangeLabelJ {get {if (_DateRangeLabelJ == null) {_DateRangeLabelJ = jQuery.Select("#" + clientId + "_DateRangeLabel");}; return _DateRangeLabelJ;}} private jQueryObject _DateRangeLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element DateRangeValueLabel {get {if (_DateRangeValueLabel == null) {_DateRangeValueLabel = (Element)Document.GetElementById(clientId + "_DateRangeValueLabel");}; return _DateRangeValueLabel;}} private Element _DateRangeValueLabel;
		public jQueryObject DateRangeValueLabelJ {get {if (_DateRangeValueLabelJ == null) {_DateRangeValueLabelJ = jQuery.Select("#" + clientId + "_DateRangeValueLabel");}; return _DateRangeValueLabelJ;}} private jQueryObject _DateRangeValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SalesCallsResultsTable {get {if (_SalesCallsResultsTable == null) {_SalesCallsResultsTable = (Element)Document.GetElementById(clientId + "_SalesCallsResultsTable");}; return _SalesCallsResultsTable;}} private Element _SalesCallsResultsTable;
		public jQueryObject SalesCallsResultsTableJ {get {if (_SalesCallsResultsTableJ == null) {_SalesCallsResultsTableJ = jQuery.Select("#" + clientId + "_SalesCallsResultsTable");}; return _SalesCallsResultsTableJ;}} private jQueryObject _SalesCallsResultsTableJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public Element SalesStatsResultsTable {get {if (_SalesStatsResultsTable == null) {_SalesStatsResultsTable = (Element)Document.GetElementById(clientId + "_SalesStatsResultsTable");}; return _SalesStatsResultsTable;}} private Element _SalesStatsResultsTable;
		public jQueryObject SalesStatsResultsTableJ {get {if (_SalesStatsResultsTableJ == null) {_SalesStatsResultsTableJ = jQuery.Select("#" + clientId + "_SalesStatsResultsTable");}; return _SalesStatsResultsTableJ;}} private jQueryObject _SalesStatsResultsTableJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
