//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.BannerImpressionStats
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
		public Element GridView {get {if (_GridView == null) {_GridView = (Element)Document.GetElementById(clientId + "_GridView");}; return _GridView;}} private Element _GridView;
		public jQueryObject GridViewJ {get {if (_GridViewJ == null) {_GridViewJ = jQuery.Select("#" + clientId + "_GridView");}; return _GridViewJ;}} private jQueryObject _GridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
