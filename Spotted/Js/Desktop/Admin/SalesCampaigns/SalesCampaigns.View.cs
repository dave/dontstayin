//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.SalesCampaigns
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
		public Element CampaignsDataGrid {get {if (_CampaignsDataGrid == null) {_CampaignsDataGrid = (Element)Document.GetElementById(clientId + "_CampaignsDataGrid");}; return _CampaignsDataGrid;}} private Element _CampaignsDataGrid;
		public jQueryObject CampaignsDataGridJ {get {if (_CampaignsDataGridJ == null) {_CampaignsDataGridJ = jQuery.Select("#" + clientId + "_CampaignsDataGrid");}; return _CampaignsDataGridJ;}} private jQueryObject _CampaignsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public InputElement AddName {get {if (_AddName == null) {_AddName = (InputElement)Document.GetElementById(clientId + "_AddName");}; return _AddName;}} private InputElement _AddName;
		public jQueryObject AddNameJ {get {if (_AddNameJ == null) {_AddNameJ = jQuery.Select("#" + clientId + "_AddName");}; return _AddNameJ;}} private jQueryObject _AddNameJ;
		public InputElement AddDescription {get {if (_AddDescription == null) {_AddDescription = (InputElement)Document.GetElementById(clientId + "_AddDescription");}; return _AddDescription;}} private InputElement _AddDescription;
		public jQueryObject AddDescriptionJ {get {if (_AddDescriptionJ == null) {_AddDescriptionJ = jQuery.Select("#" + clientId + "_AddDescription");}; return _AddDescriptionJ;}} private jQueryObject _AddDescriptionJ;
		public Js.CustomControls.Cal.Controller AddStartDate {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_AddStartDateController");}}
		public Js.CustomControls.Cal.Controller AddEndDate {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_AddEndDateController");}}
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
