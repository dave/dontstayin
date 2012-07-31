//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlContainerControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Latest", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Places.Home
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
		public DivElement TopPhotosNewsPanel {get {if (_TopPhotosNewsPanel == null) {_TopPhotosNewsPanel = (DivElement)Document.GetElementById(clientId + "_TopPhotosNewsPanel");}; return _TopPhotosNewsPanel;}} private DivElement _TopPhotosNewsPanel;
		public jQueryObject TopPhotosNewsPanelJ {get {if (_TopPhotosNewsPanelJ == null) {_TopPhotosNewsPanelJ = jQuery.Select("#" + clientId + "_TopPhotosNewsPanel");}; return _TopPhotosNewsPanelJ;}} private jQueryObject _TopPhotosNewsPanelJ;
		public AnchorElement QuickLinksHotTickets {get {if (_QuickLinksHotTickets == null) {_QuickLinksHotTickets = (AnchorElement)Document.GetElementById(clientId + "_QuickLinksHotTickets");}; return _QuickLinksHotTickets;}} private AnchorElement _QuickLinksHotTickets;
		public jQueryObject QuickLinksHotTicketsJ {get {if (_QuickLinksHotTicketsJ == null) {_QuickLinksHotTicketsJ = jQuery.Select("#" + clientId + "_QuickLinksHotTickets");}; return _QuickLinksHotTicketsJ;}} private jQueryObject _QuickLinksHotTicketsJ;
		public AnchorElement QuickLinksFreeGuestlist {get {if (_QuickLinksFreeGuestlist == null) {_QuickLinksFreeGuestlist = (AnchorElement)Document.GetElementById(clientId + "_QuickLinksFreeGuestlist");}; return _QuickLinksFreeGuestlist;}} private AnchorElement _QuickLinksFreeGuestlist;
		public jQueryObject QuickLinksFreeGuestlistJ {get {if (_QuickLinksFreeGuestlistJ == null) {_QuickLinksFreeGuestlistJ = jQuery.Select("#" + clientId + "_QuickLinksFreeGuestlist");}; return _QuickLinksFreeGuestlistJ;}} private jQueryObject _QuickLinksFreeGuestlistJ;
		public Element HotTicketsLinkPlaceLabel {get {if (_HotTicketsLinkPlaceLabel == null) {_HotTicketsLinkPlaceLabel = (Element)Document.GetElementById(clientId + "_HotTicketsLinkPlaceLabel");}; return _HotTicketsLinkPlaceLabel;}} private Element _HotTicketsLinkPlaceLabel;
		public jQueryObject HotTicketsLinkPlaceLabelJ {get {if (_HotTicketsLinkPlaceLabelJ == null) {_HotTicketsLinkPlaceLabelJ = jQuery.Select("#" + clientId + "_HotTicketsLinkPlaceLabel");}; return _HotTicketsLinkPlaceLabelJ;}} private jQueryObject _HotTicketsLinkPlaceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FreeGuestlistLinkPlaceLabel {get {if (_FreeGuestlistLinkPlaceLabel == null) {_FreeGuestlistLinkPlaceLabel = (Element)Document.GetElementById(clientId + "_FreeGuestlistLinkPlaceLabel");}; return _FreeGuestlistLinkPlaceLabel;}} private Element _FreeGuestlistLinkPlaceLabel;
		public jQueryObject FreeGuestlistLinkPlaceLabelJ {get {if (_FreeGuestlistLinkPlaceLabelJ == null) {_FreeGuestlistLinkPlaceLabelJ = jQuery.Select("#" + clientId + "_FreeGuestlistLinkPlaceLabel");}; return _FreeGuestlistLinkPlaceLabelJ;}} private jQueryObject _FreeGuestlistLinkPlaceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FeaturedVenuesDataList {get {if (_FeaturedVenuesDataList == null) {_FeaturedVenuesDataList = (Element)Document.GetElementById(clientId + "_FeaturedVenuesDataList");}; return _FeaturedVenuesDataList;}} private Element _FeaturedVenuesDataList;
		public jQueryObject FeaturedVenuesDataListJ {get {if (_FeaturedVenuesDataListJ == null) {_FeaturedVenuesDataListJ = jQuery.Select("#" + clientId + "_FeaturedVenuesDataList");}; return _FeaturedVenuesDataListJ;}} private jQueryObject _FeaturedVenuesDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element PageHeading {get {if (_PageHeading == null) {_PageHeading = (Element)Document.GetElementById(clientId + "_PageHeading");}; return _PageHeading;}} private Element _PageHeading;
		public jQueryObject PageHeadingJ {get {if (_PageHeadingJ == null) {_PageHeadingJ = jQuery.Select("#" + clientId + "_PageHeading");}; return _PageHeadingJ;}} private jQueryObject _PageHeadingJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlContainerControl", ElementGetter("Element"));
		public DivElement PlaceSelectedPanel {get {if (_PlaceSelectedPanel == null) {_PlaceSelectedPanel = (DivElement)Document.GetElementById(clientId + "_PlaceSelectedPanel");}; return _PlaceSelectedPanel;}} private DivElement _PlaceSelectedPanel;
		public jQueryObject PlaceSelectedPanelJ {get {if (_PlaceSelectedPanelJ == null) {_PlaceSelectedPanelJ = jQuery.Select("#" + clientId + "_PlaceSelectedPanel");}; return _PlaceSelectedPanelJ;}} private jQueryObject _PlaceSelectedPanelJ;
		public Element PlacePopulationLabel {get {if (_PlacePopulationLabel == null) {_PlacePopulationLabel = (Element)Document.GetElementById(clientId + "_PlacePopulationLabel");}; return _PlacePopulationLabel;}} private Element _PlacePopulationLabel;
		public jQueryObject PlacePopulationLabelJ {get {if (_PlacePopulationLabelJ == null) {_PlacePopulationLabelJ = jQuery.Select("#" + clientId + "_PlacePopulationLabel");}; return _PlacePopulationLabelJ;}} private jQueryObject _PlacePopulationLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public AnchorElement QuickLinksCalendar {get {if (_QuickLinksCalendar == null) {_QuickLinksCalendar = (AnchorElement)Document.GetElementById(clientId + "_QuickLinksCalendar");}; return _QuickLinksCalendar;}} private AnchorElement _QuickLinksCalendar;
		public jQueryObject QuickLinksCalendarJ {get {if (_QuickLinksCalendarJ == null) {_QuickLinksCalendarJ = jQuery.Select("#" + clientId + "_QuickLinksCalendar");}; return _QuickLinksCalendarJ;}} private jQueryObject _QuickLinksCalendarJ;
		public AnchorElement QuickLinksTickets {get {if (_QuickLinksTickets == null) {_QuickLinksTickets = (AnchorElement)Document.GetElementById(clientId + "_QuickLinksTickets");}; return _QuickLinksTickets;}} private AnchorElement _QuickLinksTickets;
		public jQueryObject QuickLinksTicketsJ {get {if (_QuickLinksTicketsJ == null) {_QuickLinksTicketsJ = jQuery.Select("#" + clientId + "_QuickLinksTickets");}; return _QuickLinksTicketsJ;}} private jQueryObject _QuickLinksTicketsJ;
		public ImageElement PlacePicImg {get {if (_PlacePicImg == null) {_PlacePicImg = (ImageElement)Document.GetElementById(clientId + "_PlacePicImg");}; return _PlacePicImg;}} private ImageElement _PlacePicImg;
		public jQueryObject PlacePicImgJ {get {if (_PlacePicImgJ == null) {_PlacePicImgJ = jQuery.Select("#" + clientId + "_PlacePicImg");}; return _PlacePicImgJ;}} private jQueryObject _PlacePicImgJ;
		public AnchorElement DiscussionLink {get {if (_DiscussionLink == null) {_DiscussionLink = (AnchorElement)Document.GetElementById(clientId + "_DiscussionLink");}; return _DiscussionLink;}} private AnchorElement _DiscussionLink;
		public jQueryObject DiscussionLinkJ {get {if (_DiscussionLinkJ == null) {_DiscussionLinkJ = jQuery.Select("#" + clientId + "_DiscussionLink");}; return _DiscussionLinkJ;}} private jQueryObject _DiscussionLinkJ;
		public Element PlaceImgCell {get {if (_PlaceImgCell == null) {_PlaceImgCell = (Element)Document.GetElementById(clientId + "_PlaceImgCell");}; return _PlaceImgCell;}} private Element _PlaceImgCell;
		public jQueryObject PlaceImgCellJ {get {if (_PlaceImgCellJ == null) {_PlaceImgCellJ = jQuery.Select("#" + clientId + "_PlaceImgCell");}; return _PlaceImgCellJ;}} private jQueryObject _PlaceImgCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public Element DiscussionLinkPlaceLabel {get {if (_DiscussionLinkPlaceLabel == null) {_DiscussionLinkPlaceLabel = (Element)Document.GetElementById(clientId + "_DiscussionLinkPlaceLabel");}; return _DiscussionLinkPlaceLabel;}} private Element _DiscussionLinkPlaceLabel;
		public jQueryObject DiscussionLinkPlaceLabelJ {get {if (_DiscussionLinkPlaceLabelJ == null) {_DiscussionLinkPlaceLabelJ = jQuery.Select("#" + clientId + "_DiscussionLinkPlaceLabel");}; return _DiscussionLinkPlaceLabelJ;}} private jQueryObject _DiscussionLinkPlaceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element CalendarLinkPlaceLabel {get {if (_CalendarLinkPlaceLabel == null) {_CalendarLinkPlaceLabel = (Element)Document.GetElementById(clientId + "_CalendarLinkPlaceLabel");}; return _CalendarLinkPlaceLabel;}} private Element _CalendarLinkPlaceLabel;
		public jQueryObject CalendarLinkPlaceLabelJ {get {if (_CalendarLinkPlaceLabelJ == null) {_CalendarLinkPlaceLabelJ = jQuery.Select("#" + clientId + "_CalendarLinkPlaceLabel");}; return _CalendarLinkPlaceLabelJ;}} private jQueryObject _CalendarLinkPlaceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element TicketsLinkPlaceLabel {get {if (_TicketsLinkPlaceLabel == null) {_TicketsLinkPlaceLabel = (Element)Document.GetElementById(clientId + "_TicketsLinkPlaceLabel");}; return _TicketsLinkPlaceLabel;}} private Element _TicketsLinkPlaceLabel;
		public jQueryObject TicketsLinkPlaceLabelJ {get {if (_TicketsLinkPlaceLabelJ == null) {_TicketsLinkPlaceLabelJ = jQuery.Select("#" + clientId + "_TicketsLinkPlaceLabel");}; return _TicketsLinkPlaceLabelJ;}} private jQueryObject _TicketsLinkPlaceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element Latest {get {if (_Latest == null) {_Latest = (Element)Document.GetElementById(clientId + "_Latest");}; return _Latest;}} private Element _Latest;
		public jQueryObject LatestJ {get {if (_LatestJ == null) {_LatestJ = jQuery.Select("#" + clientId + "_Latest");}; return _LatestJ;}} private jQueryObject _LatestJ;//mappings.Add("Spotted.Controls.Latest", ElementGetter("Element"));
		public CheckBoxElement VisitCheck {get {if (_VisitCheck == null) {_VisitCheck = (CheckBoxElement)Document.GetElementById(clientId + "_VisitCheck");}; return _VisitCheck;}} private CheckBoxElement _VisitCheck;
		public jQueryObject VisitCheckJ {get {if (_VisitCheckJ == null) {_VisitCheckJ = jQuery.Select("#" + clientId + "_VisitCheck");}; return _VisitCheckJ;}} private jQueryObject _VisitCheckJ;
		public Element DiscussionLinkCommentsLabel {get {if (_DiscussionLinkCommentsLabel == null) {_DiscussionLinkCommentsLabel = (Element)Document.GetElementById(clientId + "_DiscussionLinkCommentsLabel");}; return _DiscussionLinkCommentsLabel;}} private Element _DiscussionLinkCommentsLabel;
		public jQueryObject DiscussionLinkCommentsLabelJ {get {if (_DiscussionLinkCommentsLabelJ == null) {_DiscussionLinkCommentsLabelJ = jQuery.Select("#" + clientId + "_DiscussionLinkCommentsLabel");}; return _DiscussionLinkCommentsLabelJ;}} private jQueryObject _DiscussionLinkCommentsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PlaceNameLabel1 {get {if (_PlaceNameLabel1 == null) {_PlaceNameLabel1 = (Element)Document.GetElementById(clientId + "_PlaceNameLabel1");}; return _PlaceNameLabel1;}} private Element _PlaceNameLabel1;
		public jQueryObject PlaceNameLabel1J {get {if (_PlaceNameLabel1J == null) {_PlaceNameLabel1J = jQuery.Select("#" + clientId + "_PlaceNameLabel1");}; return _PlaceNameLabel1J;}} private jQueryObject _PlaceNameLabel1J;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PlaceNameLabel3 {get {if (_PlaceNameLabel3 == null) {_PlaceNameLabel3 = (Element)Document.GetElementById(clientId + "_PlaceNameLabel3");}; return _PlaceNameLabel3;}} private Element _PlaceNameLabel3;
		public jQueryObject PlaceNameLabel3J {get {if (_PlaceNameLabel3J == null) {_PlaceNameLabel3J = jQuery.Select("#" + clientId + "_PlaceNameLabel3");}; return _PlaceNameLabel3J;}} private jQueryObject _PlaceNameLabel3J;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PlaceNameLabel2 {get {if (_PlaceNameLabel2 == null) {_PlaceNameLabel2 = (Element)Document.GetElementById(clientId + "_PlaceNameLabel2");}; return _PlaceNameLabel2;}} private Element _PlaceNameLabel2;
		public jQueryObject PlaceNameLabel2J {get {if (_PlaceNameLabel2J == null) {_PlaceNameLabel2J = jQuery.Select("#" + clientId + "_PlaceNameLabel2");}; return _PlaceNameLabel2J;}} private jQueryObject _PlaceNameLabel2J;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element VenueDataListDiv {get {if (_VenueDataListDiv == null) {_VenueDataListDiv = (Element)Document.GetElementById(clientId + "_VenueDataListDiv");}; return _VenueDataListDiv;}} private Element _VenueDataListDiv;
		public jQueryObject VenueDataListDivJ {get {if (_VenueDataListDivJ == null) {_VenueDataListDivJ = jQuery.Select("#" + clientId + "_VenueDataListDiv");}; return _VenueDataListDivJ;}} private jQueryObject _VenueDataListDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element NoVenuesDiv {get {if (_NoVenuesDiv == null) {_NoVenuesDiv = (Element)Document.GetElementById(clientId + "_NoVenuesDiv");}; return _NoVenuesDiv;}} private Element _NoVenuesDiv;
		public jQueryObject NoVenuesDivJ {get {if (_NoVenuesDivJ == null) {_NoVenuesDivJ = jQuery.Select("#" + clientId + "_NoVenuesDiv");}; return _NoVenuesDivJ;}} private jQueryObject _NoVenuesDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement SuggestVenueLink {get {if (_SuggestVenueLink == null) {_SuggestVenueLink = (AnchorElement)Document.GetElementById(clientId + "_SuggestVenueLink");}; return _SuggestVenueLink;}} private AnchorElement _SuggestVenueLink;
		public jQueryObject SuggestVenueLinkJ {get {if (_SuggestVenueLinkJ == null) {_SuggestVenueLinkJ = jQuery.Select("#" + clientId + "_SuggestVenueLink");}; return _SuggestVenueLinkJ;}} private jQueryObject _SuggestVenueLinkJ;
		public AnchorElement SuggestVenueLink1 {get {if (_SuggestVenueLink1 == null) {_SuggestVenueLink1 = (AnchorElement)Document.GetElementById(clientId + "_SuggestVenueLink1");}; return _SuggestVenueLink1;}} private AnchorElement _SuggestVenueLink1;
		public jQueryObject SuggestVenueLink1J {get {if (_SuggestVenueLink1J == null) {_SuggestVenueLink1J = jQuery.Select("#" + clientId + "_SuggestVenueLink1");}; return _SuggestVenueLink1J;}} private jQueryObject _SuggestVenueLink1J;
		public AnchorElement VenuesMoreLink {get {if (_VenuesMoreLink == null) {_VenuesMoreLink = (AnchorElement)Document.GetElementById(clientId + "_VenuesMoreLink");}; return _VenuesMoreLink;}} private AnchorElement _VenuesMoreLink;
		public jQueryObject VenuesMoreLinkJ {get {if (_VenuesMoreLinkJ == null) {_VenuesMoreLinkJ = jQuery.Select("#" + clientId + "_VenuesMoreLink");}; return _VenuesMoreLinkJ;}} private jQueryObject _VenuesMoreLinkJ;
		public Element VenueDataList {get {if (_VenueDataList == null) {_VenueDataList = (Element)Document.GetElementById(clientId + "_VenueDataList");}; return _VenueDataList;}} private Element _VenueDataList;
		public jQueryObject VenueDataListJ {get {if (_VenueDataListJ == null) {_VenueDataListJ = jQuery.Select("#" + clientId + "_VenueDataList");}; return _VenueDataListJ;}} private jQueryObject _VenueDataListJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public DivElement VenuesMorePanel {get {if (_VenuesMorePanel == null) {_VenuesMorePanel = (DivElement)Document.GetElementById(clientId + "_VenuesMorePanel");}; return _VenuesMorePanel;}} private DivElement _VenuesMorePanel;
		public jQueryObject VenuesMorePanelJ {get {if (_VenuesMorePanelJ == null) {_VenuesMorePanelJ = jQuery.Select("#" + clientId + "_VenuesMorePanel");}; return _VenuesMorePanelJ;}} private jQueryObject _VenuesMorePanelJ;
		public DivElement VenuesPanel {get {if (_VenuesPanel == null) {_VenuesPanel = (DivElement)Document.GetElementById(clientId + "_VenuesPanel");}; return _VenuesPanel;}} private DivElement _VenuesPanel;
		public jQueryObject VenuesPanelJ {get {if (_VenuesPanelJ == null) {_VenuesPanelJ = jQuery.Select("#" + clientId + "_VenuesPanel");}; return _VenuesPanelJ;}} private jQueryObject _VenuesPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
