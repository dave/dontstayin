//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.RecentlyEndedBanners
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
		public Js.CustomControls.Cal.Controller uiFirstDate {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_uiFirstDateController");}}
		public Js.CustomControls.Cal.Controller uiSecondDate {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_uiSecondDateController");}}
		public Element uiChangeDateRange {get {if (_uiChangeDateRange == null) {_uiChangeDateRange = (Element)Document.GetElementById(clientId + "_uiChangeDateRange");}; return _uiChangeDateRange;}} private Element _uiChangeDateRange;
		public jQueryObject uiChangeDateRangeJ {get {if (_uiChangeDateRangeJ == null) {_uiChangeDateRangeJ = jQuery.Select("#" + clientId + "_uiChangeDateRange");}; return _uiChangeDateRangeJ;}} private jQueryObject _uiChangeDateRangeJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiBanners {get {if (_uiBanners == null) {_uiBanners = (Element)Document.GetElementById(clientId + "_uiBanners");}; return _uiBanners;}} private Element _uiBanners;
		public jQueryObject uiBannersJ {get {if (_uiBannersJ == null) {_uiBannersJ = jQuery.Select("#" + clientId + "_uiBanners");}; return _uiBannersJ;}} private jQueryObject _uiBannersJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
