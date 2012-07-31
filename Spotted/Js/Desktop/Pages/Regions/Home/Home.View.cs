//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Regions.Home
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
		public DivElement Panel1 {get {if (_Panel1 == null) {_Panel1 = (DivElement)Document.GetElementById(clientId + "_Panel1");}; return _Panel1;}} private DivElement _Panel1;
		public jQueryObject Panel1J {get {if (_Panel1J == null) {_Panel1J = jQuery.Select("#" + clientId + "_Panel1");}; return _Panel1J;}} private jQueryObject _Panel1J;
		public Element PageHeadingNoPlace {get {if (_PageHeadingNoPlace == null) {_PageHeadingNoPlace = (Element)Document.GetElementById(clientId + "_PageHeadingNoPlace");}; return _PageHeadingNoPlace;}} private Element _PageHeadingNoPlace;
		public jQueryObject PageHeadingNoPlaceJ {get {if (_PageHeadingNoPlaceJ == null) {_PageHeadingNoPlaceJ = jQuery.Select("#" + clientId + "_PageHeadingNoPlace");}; return _PageHeadingNoPlaceJ;}} private jQueryObject _PageHeadingNoPlaceJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public AnchorElement NoPlaceSelectedFlagAnchor {get {if (_NoPlaceSelectedFlagAnchor == null) {_NoPlaceSelectedFlagAnchor = (AnchorElement)Document.GetElementById(clientId + "_NoPlaceSelectedFlagAnchor");}; return _NoPlaceSelectedFlagAnchor;}} private AnchorElement _NoPlaceSelectedFlagAnchor;
		public jQueryObject NoPlaceSelectedFlagAnchorJ {get {if (_NoPlaceSelectedFlagAnchorJ == null) {_NoPlaceSelectedFlagAnchorJ = jQuery.Select("#" + clientId + "_NoPlaceSelectedFlagAnchor");}; return _NoPlaceSelectedFlagAnchorJ;}} private jQueryObject _NoPlaceSelectedFlagAnchorJ;
		public ImageElement NoPlaceSelectedFlagImg {get {if (_NoPlaceSelectedFlagImg == null) {_NoPlaceSelectedFlagImg = (ImageElement)Document.GetElementById(clientId + "_NoPlaceSelectedFlagImg");}; return _NoPlaceSelectedFlagImg;}} private ImageElement _NoPlaceSelectedFlagImg;
		public jQueryObject NoPlaceSelectedFlagImgJ {get {if (_NoPlaceSelectedFlagImgJ == null) {_NoPlaceSelectedFlagImgJ = jQuery.Select("#" + clientId + "_NoPlaceSelectedFlagImg");}; return _NoPlaceSelectedFlagImgJ;}} private jQueryObject _NoPlaceSelectedFlagImgJ;
		public Element RegionNameLabel {get {if (_RegionNameLabel == null) {_RegionNameLabel = (Element)Document.GetElementById(clientId + "_RegionNameLabel");}; return _RegionNameLabel;}} private Element _RegionNameLabel;
		public jQueryObject RegionNameLabelJ {get {if (_RegionNameLabelJ == null) {_RegionNameLabelJ = jQuery.Select("#" + clientId + "_RegionNameLabel");}; return _RegionNameLabelJ;}} private jQueryObject _RegionNameLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public AnchorElement NoPlaceSelectedCountryLink {get {if (_NoPlaceSelectedCountryLink == null) {_NoPlaceSelectedCountryLink = (AnchorElement)Document.GetElementById(clientId + "_NoPlaceSelectedCountryLink");}; return _NoPlaceSelectedCountryLink;}} private AnchorElement _NoPlaceSelectedCountryLink;
		public jQueryObject NoPlaceSelectedCountryLinkJ {get {if (_NoPlaceSelectedCountryLinkJ == null) {_NoPlaceSelectedCountryLinkJ = jQuery.Select("#" + clientId + "_NoPlaceSelectedCountryLink");}; return _NoPlaceSelectedCountryLinkJ;}} private jQueryObject _NoPlaceSelectedCountryLinkJ;
		public Element SizeOrderLink {get {if (_SizeOrderLink == null) {_SizeOrderLink = (Element)Document.GetElementById(clientId + "_SizeOrderLink");}; return _SizeOrderLink;}} private Element _SizeOrderLink;
		public jQueryObject SizeOrderLinkJ {get {if (_SizeOrderLinkJ == null) {_SizeOrderLinkJ = jQuery.Select("#" + clientId + "_SizeOrderLink");}; return _SizeOrderLinkJ;}} private jQueryObject _SizeOrderLinkJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element NameOrderLink {get {if (_NameOrderLink == null) {_NameOrderLink = (Element)Document.GetElementById(clientId + "_NameOrderLink");}; return _NameOrderLink;}} private Element _NameOrderLink;
		public jQueryObject NameOrderLinkJ {get {if (_NameOrderLinkJ == null) {_NameOrderLinkJ = jQuery.Select("#" + clientId + "_NameOrderLink");}; return _NameOrderLinkJ;}} private jQueryObject _NameOrderLinkJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element LatitudeOrderLink {get {if (_LatitudeOrderLink == null) {_LatitudeOrderLink = (Element)Document.GetElementById(clientId + "_LatitudeOrderLink");}; return _LatitudeOrderLink;}} private Element _LatitudeOrderLink;
		public jQueryObject LatitudeOrderLinkJ {get {if (_LatitudeOrderLinkJ == null) {_LatitudeOrderLinkJ = jQuery.Select("#" + clientId + "_LatitudeOrderLink");}; return _LatitudeOrderLinkJ;}} private jQueryObject _LatitudeOrderLinkJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element LongitudeOrderLink {get {if (_LongitudeOrderLink == null) {_LongitudeOrderLink = (Element)Document.GetElementById(clientId + "_LongitudeOrderLink");}; return _LongitudeOrderLink;}} private Element _LongitudeOrderLink;
		public jQueryObject LongitudeOrderLinkJ {get {if (_LongitudeOrderLinkJ == null) {_LongitudeOrderLinkJ = jQuery.Select("#" + clientId + "_LongitudeOrderLink");}; return _LongitudeOrderLinkJ;}} private jQueryObject _LongitudeOrderLinkJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element EventOrderLink {get {if (_EventOrderLink == null) {_EventOrderLink = (Element)Document.GetElementById(clientId + "_EventOrderLink");}; return _EventOrderLink;}} private Element _EventOrderLink;
		public jQueryObject EventOrderLinkJ {get {if (_EventOrderLinkJ == null) {_EventOrderLinkJ = jQuery.Select("#" + clientId + "_EventOrderLink");}; return _EventOrderLinkJ;}} private jQueryObject _EventOrderLinkJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element PlacesDataList {get {if (_PlacesDataList == null) {_PlacesDataList = (Element)Document.GetElementById(clientId + "_PlacesDataList");}; return _PlacesDataList;}} private Element _PlacesDataList;
		public jQueryObject PlacesDataListJ {get {if (_PlacesDataListJ == null) {_PlacesDataListJ = jQuery.Select("#" + clientId + "_PlacesDataList");}; return _PlacesDataListJ;}} private jQueryObject _PlacesDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
