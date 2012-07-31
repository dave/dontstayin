//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.SalesNew
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
		public Element NewPromoterDataGrid {get {if (_NewPromoterDataGrid == null) {_NewPromoterDataGrid = (Element)Document.GetElementById(clientId + "_NewPromoterDataGrid");}; return _NewPromoterDataGrid;}} private Element _NewPromoterDataGrid;
		public jQueryObject NewPromoterDataGridJ {get {if (_NewPromoterDataGridJ == null) {_NewPromoterDataGridJ = jQuery.Select("#" + clientId + "_NewPromoterDataGrid");}; return _NewPromoterDataGridJ;}} private jQueryObject _NewPromoterDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element CallBacksDataGrid {get {if (_CallBacksDataGrid == null) {_CallBacksDataGrid = (Element)Document.GetElementById(clientId + "_CallBacksDataGrid");}; return _CallBacksDataGrid;}} private Element _CallBacksDataGrid;
		public jQueryObject CallBacksDataGridJ {get {if (_CallBacksDataGridJ == null) {_CallBacksDataGridJ = jQuery.Select("#" + clientId + "_CallBacksDataGrid");}; return _CallBacksDataGridJ;}} private jQueryObject _CallBacksDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
