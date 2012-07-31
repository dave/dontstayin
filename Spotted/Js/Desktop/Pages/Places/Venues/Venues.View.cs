//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlContainerControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Places.Venues
{
	public partial class View
		 : Js.DsiUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement AllVenuesPanel {get {if (_AllVenuesPanel == null) {_AllVenuesPanel = (DivElement)Document.GetElementById(clientId + "_AllVenuesPanel");}; return _AllVenuesPanel;}} private DivElement _AllVenuesPanel;
		public jQueryObject AllVenuesPanelJ {get {if (_AllVenuesPanelJ == null) {_AllVenuesPanelJ = jQuery.Select("#" + clientId + "_AllVenuesPanel");}; return _AllVenuesPanelJ;}} private jQueryObject _AllVenuesPanelJ;
		public DivElement LargeRegularVenuePanel {get {if (_LargeRegularVenuePanel == null) {_LargeRegularVenuePanel = (DivElement)Document.GetElementById(clientId + "_LargeRegularVenuePanel");}; return _LargeRegularVenuePanel;}} private DivElement _LargeRegularVenuePanel;
		public jQueryObject LargeRegularVenuePanelJ {get {if (_LargeRegularVenuePanelJ == null) {_LargeRegularVenuePanelJ = jQuery.Select("#" + clientId + "_LargeRegularVenuePanel");}; return _LargeRegularVenuePanelJ;}} private jQueryObject _LargeRegularVenuePanelJ;
		public DivElement MediumRegularVenuePanel {get {if (_MediumRegularVenuePanel == null) {_MediumRegularVenuePanel = (DivElement)Document.GetElementById(clientId + "_MediumRegularVenuePanel");}; return _MediumRegularVenuePanel;}} private DivElement _MediumRegularVenuePanel;
		public jQueryObject MediumRegularVenuePanelJ {get {if (_MediumRegularVenuePanelJ == null) {_MediumRegularVenuePanelJ = jQuery.Select("#" + clientId + "_MediumRegularVenuePanel");}; return _MediumRegularVenuePanelJ;}} private jQueryObject _MediumRegularVenuePanelJ;
		public DivElement SmallRegularVenuePanel {get {if (_SmallRegularVenuePanel == null) {_SmallRegularVenuePanel = (DivElement)Document.GetElementById(clientId + "_SmallRegularVenuePanel");}; return _SmallRegularVenuePanel;}} private DivElement _SmallRegularVenuePanel;
		public jQueryObject SmallRegularVenuePanelJ {get {if (_SmallRegularVenuePanelJ == null) {_SmallRegularVenuePanelJ = jQuery.Select("#" + clientId + "_SmallRegularVenuePanel");}; return _SmallRegularVenuePanelJ;}} private jQueryObject _SmallRegularVenuePanelJ;
		public DivElement NotRegularVenuePanel {get {if (_NotRegularVenuePanel == null) {_NotRegularVenuePanel = (DivElement)Document.GetElementById(clientId + "_NotRegularVenuePanel");}; return _NotRegularVenuePanel;}} private DivElement _NotRegularVenuePanel;
		public jQueryObject NotRegularVenuePanelJ {get {if (_NotRegularVenuePanelJ == null) {_NotRegularVenuePanelJ = jQuery.Select("#" + clientId + "_NotRegularVenuePanel");}; return _NotRegularVenuePanelJ;}} private jQueryObject _NotRegularVenuePanelJ;
		public Element LargeRegularVenueDataList {get {if (_LargeRegularVenueDataList == null) {_LargeRegularVenueDataList = (Element)Document.GetElementById(clientId + "_LargeRegularVenueDataList");}; return _LargeRegularVenueDataList;}} private Element _LargeRegularVenueDataList;
		public jQueryObject LargeRegularVenueDataListJ {get {if (_LargeRegularVenueDataListJ == null) {_LargeRegularVenueDataListJ = jQuery.Select("#" + clientId + "_LargeRegularVenueDataList");}; return _LargeRegularVenueDataListJ;}} private jQueryObject _LargeRegularVenueDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element MediumRegularVenueDataList {get {if (_MediumRegularVenueDataList == null) {_MediumRegularVenueDataList = (Element)Document.GetElementById(clientId + "_MediumRegularVenueDataList");}; return _MediumRegularVenueDataList;}} private Element _MediumRegularVenueDataList;
		public jQueryObject MediumRegularVenueDataListJ {get {if (_MediumRegularVenueDataListJ == null) {_MediumRegularVenueDataListJ = jQuery.Select("#" + clientId + "_MediumRegularVenueDataList");}; return _MediumRegularVenueDataListJ;}} private jQueryObject _MediumRegularVenueDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element SmallRegularVenueDataList {get {if (_SmallRegularVenueDataList == null) {_SmallRegularVenueDataList = (Element)Document.GetElementById(clientId + "_SmallRegularVenueDataList");}; return _SmallRegularVenueDataList;}} private Element _SmallRegularVenueDataList;
		public jQueryObject SmallRegularVenueDataListJ {get {if (_SmallRegularVenueDataListJ == null) {_SmallRegularVenueDataListJ = jQuery.Select("#" + clientId + "_SmallRegularVenueDataList");}; return _SmallRegularVenueDataListJ;}} private jQueryObject _SmallRegularVenueDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element NotRegularVenueDataList {get {if (_NotRegularVenueDataList == null) {_NotRegularVenueDataList = (Element)Document.GetElementById(clientId + "_NotRegularVenueDataList");}; return _NotRegularVenueDataList;}} private Element _NotRegularVenueDataList;
		public jQueryObject NotRegularVenueDataListJ {get {if (_NotRegularVenueDataListJ == null) {_NotRegularVenueDataListJ = jQuery.Select("#" + clientId + "_NotRegularVenueDataList");}; return _NotRegularVenueDataListJ;}} private jQueryObject _NotRegularVenueDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public AnchorElement AllVenuesPlaceLink {get {if (_AllVenuesPlaceLink == null) {_AllVenuesPlaceLink = (AnchorElement)Document.GetElementById(clientId + "_AllVenuesPlaceLink");}; return _AllVenuesPlaceLink;}} private AnchorElement _AllVenuesPlaceLink;
		public jQueryObject AllVenuesPlaceLinkJ {get {if (_AllVenuesPlaceLinkJ == null) {_AllVenuesPlaceLinkJ = jQuery.Select("#" + clientId + "_AllVenuesPlaceLink");}; return _AllVenuesPlaceLinkJ;}} private jQueryObject _AllVenuesPlaceLinkJ;
		public Element PageHeadingAllVenues {get {if (_PageHeadingAllVenues == null) {_PageHeadingAllVenues = (Element)Document.GetElementById(clientId + "_PageHeadingAllVenues");}; return _PageHeadingAllVenues;}} private Element _PageHeadingAllVenues;
		public jQueryObject PageHeadingAllVenuesJ {get {if (_PageHeadingAllVenuesJ == null) {_PageHeadingAllVenuesJ = jQuery.Select("#" + clientId + "_PageHeadingAllVenues");}; return _PageHeadingAllVenuesJ;}} private jQueryObject _PageHeadingAllVenuesJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlContainerControl", ElementGetter("Element"));
		public Element AllVenuesPanelNoVenues {get {if (_AllVenuesPanelNoVenues == null) {_AllVenuesPanelNoVenues = (Element)Document.GetElementById(clientId + "_AllVenuesPanelNoVenues");}; return _AllVenuesPanelNoVenues;}} private Element _AllVenuesPanelNoVenues;
		public jQueryObject AllVenuesPanelNoVenuesJ {get {if (_AllVenuesPanelNoVenuesJ == null) {_AllVenuesPanelNoVenuesJ = jQuery.Select("#" + clientId + "_AllVenuesPanelNoVenues");}; return _AllVenuesPanelNoVenuesJ;}} private jQueryObject _AllVenuesPanelNoVenuesJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
