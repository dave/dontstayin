//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.AddBulkSkeletonPromoters
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
		public SelectElement Sector {get {if (_Sector == null) {_Sector = (SelectElement)Document.GetElementById(clientId + "_Sector");}; return _Sector;}} private SelectElement _Sector;
		public jQueryObject SectorJ {get {if (_SectorJ == null) {_SectorJ = jQuery.Select("#" + clientId + "_Sector");}; return _SectorJ;}} private jQueryObject _SectorJ;
		public SelectElement SalesCampaignDropDown {get {if (_SalesCampaignDropDown == null) {_SalesCampaignDropDown = (SelectElement)Document.GetElementById(clientId + "_SalesCampaignDropDown");}; return _SalesCampaignDropDown;}} private SelectElement _SalesCampaignDropDown;
		public jQueryObject SalesCampaignDropDownJ {get {if (_SalesCampaignDropDownJ == null) {_SalesCampaignDropDownJ = jQuery.Select("#" + clientId + "_SalesCampaignDropDown");}; return _SalesCampaignDropDownJ;}} private jQueryObject _SalesCampaignDropDownJ;
		public InputElement Csv {get {if (_Csv == null) {_Csv = (InputElement)Document.GetElementById(clientId + "_Csv");}; return _Csv;}} private InputElement _Csv;
		public jQueryObject CsvJ {get {if (_CsvJ == null) {_CsvJ = jQuery.Select("#" + clientId + "_Csv");}; return _CsvJ;}} private jQueryObject _CsvJ;
		public Element Error {get {if (_Error == null) {_Error = (Element)Document.GetElementById(clientId + "_Error");}; return _Error;}} private Element _Error;
		public jQueryObject ErrorJ {get {if (_ErrorJ == null) {_ErrorJ = jQuery.Select("#" + clientId + "_Error");}; return _ErrorJ;}} private jQueryObject _ErrorJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
