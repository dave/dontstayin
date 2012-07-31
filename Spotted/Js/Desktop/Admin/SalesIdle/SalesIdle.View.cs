//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.SalesIdle
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
		public Element PageNumberP {get {if (_PageNumberP == null) {_PageNumberP = (Element)Document.GetElementById(clientId + "_PageNumberP");}; return _PageNumberP;}} private Element _PageNumberP;
		public jQueryObject PageNumberPJ {get {if (_PageNumberPJ == null) {_PageNumberPJ = jQuery.Select("#" + clientId + "_PageNumberP");}; return _PageNumberPJ;}} private jQueryObject _PageNumberPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element PromoterDataGrid {get {if (_PromoterDataGrid == null) {_PromoterDataGrid = (Element)Document.GetElementById(clientId + "_PromoterDataGrid");}; return _PromoterDataGrid;}} private Element _PromoterDataGrid;
		public jQueryObject PromoterDataGridJ {get {if (_PromoterDataGridJ == null) {_PromoterDataGridJ = jQuery.Select("#" + clientId + "_PromoterDataGrid");}; return _PromoterDataGridJ;}} private jQueryObject _PromoterDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
