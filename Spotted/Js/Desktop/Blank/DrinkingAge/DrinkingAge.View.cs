//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.DrinkingAge
{
	public partial class View
		 : Js.BlankUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public SelectElement Day {get {if (_Day == null) {_Day = (SelectElement)Document.GetElementById(clientId + "_Day");}; return _Day;}} private SelectElement _Day;
		public jQueryObject DayJ {get {if (_DayJ == null) {_DayJ = jQuery.Select("#" + clientId + "_Day");}; return _DayJ;}} private jQueryObject _DayJ;
		public SelectElement Month {get {if (_Month == null) {_Month = (SelectElement)Document.GetElementById(clientId + "_Month");}; return _Month;}} private SelectElement _Month;
		public jQueryObject MonthJ {get {if (_MonthJ == null) {_MonthJ = jQuery.Select("#" + clientId + "_Month");}; return _MonthJ;}} private jQueryObject _MonthJ;
		public SelectElement Year {get {if (_Year == null) {_Year = (SelectElement)Document.GetElementById(clientId + "_Year");}; return _Year;}} private SelectElement _Year;
		public jQueryObject YearJ {get {if (_YearJ == null) {_YearJ = jQuery.Select("#" + clientId + "_Year");}; return _YearJ;}} private jQueryObject _YearJ;
		public SelectElement CountryDrop {get {if (_CountryDrop == null) {_CountryDrop = (SelectElement)Document.GetElementById(clientId + "_CountryDrop");}; return _CountryDrop;}} private SelectElement _CountryDrop;
		public jQueryObject CountryDropJ {get {if (_CountryDropJ == null) {_CountryDropJ = jQuery.Select("#" + clientId + "_CountryDrop");}; return _CountryDropJ;}} private jQueryObject _CountryDropJ;
		public Element EntryVal {get {if (_EntryVal == null) {_EntryVal = (Element)Document.GetElementById(clientId + "_EntryVal");}; return _EntryVal;}} private Element _EntryVal;
		public jQueryObject EntryValJ {get {if (_EntryValJ == null) {_EntryValJ = jQuery.Select("#" + clientId + "_EntryVal");}; return _EntryValJ;}} private jQueryObject _EntryValJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
