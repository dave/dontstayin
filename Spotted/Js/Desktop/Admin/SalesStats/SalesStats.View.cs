//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.SalesStats
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
		public Element CallsDataGrid {get {if (_CallsDataGrid == null) {_CallsDataGrid = (Element)Document.GetElementById(clientId + "_CallsDataGrid");}; return _CallsDataGrid;}} private Element _CallsDataGrid;
		public jQueryObject CallsDataGridJ {get {if (_CallsDataGridJ == null) {_CallsDataGridJ = jQuery.Select("#" + clientId + "_CallsDataGrid");}; return _CallsDataGridJ;}} private jQueryObject _CallsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element DailySalesDataGrid {get {if (_DailySalesDataGrid == null) {_DailySalesDataGrid = (Element)Document.GetElementById(clientId + "_DailySalesDataGrid");}; return _DailySalesDataGrid;}} private Element _DailySalesDataGrid;
		public jQueryObject DailySalesDataGridJ {get {if (_DailySalesDataGridJ == null) {_DailySalesDataGridJ = jQuery.Select("#" + clientId + "_DailySalesDataGrid");}; return _DailySalesDataGridJ;}} private jQueryObject _DailySalesDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element MonthlySalesDataGrid {get {if (_MonthlySalesDataGrid == null) {_MonthlySalesDataGrid = (Element)Document.GetElementById(clientId + "_MonthlySalesDataGrid");}; return _MonthlySalesDataGrid;}} private Element _MonthlySalesDataGrid;
		public jQueryObject MonthlySalesDataGridJ {get {if (_MonthlySalesDataGridJ == null) {_MonthlySalesDataGridJ = jQuery.Select("#" + clientId + "_MonthlySalesDataGrid");}; return _MonthlySalesDataGridJ;}} private jQueryObject _MonthlySalesDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
