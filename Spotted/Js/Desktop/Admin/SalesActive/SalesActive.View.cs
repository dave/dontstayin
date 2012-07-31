//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.SalesActive
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
		public SelectElement SalesEstimateFilterDropDownList {get {if (_SalesEstimateFilterDropDownList == null) {_SalesEstimateFilterDropDownList = (SelectElement)Document.GetElementById(clientId + "_SalesEstimateFilterDropDownList");}; return _SalesEstimateFilterDropDownList;}} private SelectElement _SalesEstimateFilterDropDownList;
		public jQueryObject SalesEstimateFilterDropDownListJ {get {if (_SalesEstimateFilterDropDownListJ == null) {_SalesEstimateFilterDropDownListJ = jQuery.Select("#" + clientId + "_SalesEstimateFilterDropDownList");}; return _SalesEstimateFilterDropDownListJ;}} private jQueryObject _SalesEstimateFilterDropDownListJ;
		public SelectElement SectorFilterDropDownList {get {if (_SectorFilterDropDownList == null) {_SectorFilterDropDownList = (SelectElement)Document.GetElementById(clientId + "_SectorFilterDropDownList");}; return _SectorFilterDropDownList;}} private SelectElement _SectorFilterDropDownList;
		public jQueryObject SectorFilterDropDownListJ {get {if (_SectorFilterDropDownListJ == null) {_SectorFilterDropDownListJ = jQuery.Select("#" + clientId + "_SectorFilterDropDownList");}; return _SectorFilterDropDownListJ;}} private jQueryObject _SectorFilterDropDownListJ;
		public Element PromoterDataGrid {get {if (_PromoterDataGrid == null) {_PromoterDataGrid = (Element)Document.GetElementById(clientId + "_PromoterDataGrid");}; return _PromoterDataGrid;}} private Element _PromoterDataGrid;
		public jQueryObject PromoterDataGridJ {get {if (_PromoterDataGridJ == null) {_PromoterDataGridJ = jQuery.Select("#" + clientId + "_PromoterDataGrid");}; return _PromoterDataGridJ;}} private jQueryObject _PromoterDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element ExpiredDataGrid {get {if (_ExpiredDataGrid == null) {_ExpiredDataGrid = (Element)Document.GetElementById(clientId + "_ExpiredDataGrid");}; return _ExpiredDataGrid;}} private Element _ExpiredDataGrid;
		public jQueryObject ExpiredDataGridJ {get {if (_ExpiredDataGridJ == null) {_ExpiredDataGridJ = jQuery.Select("#" + clientId + "_ExpiredDataGrid");}; return _ExpiredDataGridJ;}} private jQueryObject _ExpiredDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
