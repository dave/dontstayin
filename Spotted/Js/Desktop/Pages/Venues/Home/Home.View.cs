//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Latest", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Venues.Home
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
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Div1 {get {if (_Div1 == null) {_Div1 = (Element)Document.GetElementById(clientId + "_Div1");}; return _Div1;}} private Element _Div1;
		public jQueryObject Div1J {get {if (_Div1J == null) {_Div1J = jQuery.Select("#" + clientId + "_Div1");}; return _Div1J;}} private jQueryObject _Div1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement HotTicketsLink {get {if (_HotTicketsLink == null) {_HotTicketsLink = (AnchorElement)Document.GetElementById(clientId + "_HotTicketsLink");}; return _HotTicketsLink;}} private AnchorElement _HotTicketsLink;
		public jQueryObject HotTicketsLinkJ {get {if (_HotTicketsLinkJ == null) {_HotTicketsLinkJ = jQuery.Select("#" + clientId + "_HotTicketsLink");}; return _HotTicketsLinkJ;}} private jQueryObject _HotTicketsLinkJ;
		public Element HotTicketsLinkVenueLabel {get {if (_HotTicketsLinkVenueLabel == null) {_HotTicketsLinkVenueLabel = (Element)Document.GetElementById(clientId + "_HotTicketsLinkVenueLabel");}; return _HotTicketsLinkVenueLabel;}} private Element _HotTicketsLinkVenueLabel;
		public jQueryObject HotTicketsLinkVenueLabelJ {get {if (_HotTicketsLinkVenueLabelJ == null) {_HotTicketsLinkVenueLabelJ = jQuery.Select("#" + clientId + "_HotTicketsLinkVenueLabel");}; return _HotTicketsLinkVenueLabelJ;}} private jQueryObject _HotTicketsLinkVenueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public AnchorElement TicketsLink {get {if (_TicketsLink == null) {_TicketsLink = (AnchorElement)Document.GetElementById(clientId + "_TicketsLink");}; return _TicketsLink;}} private AnchorElement _TicketsLink;
		public jQueryObject TicketsLinkJ {get {if (_TicketsLinkJ == null) {_TicketsLinkJ = jQuery.Select("#" + clientId + "_TicketsLink");}; return _TicketsLinkJ;}} private jQueryObject _TicketsLinkJ;
		public Element TicketsLinkVenueLabel {get {if (_TicketsLinkVenueLabel == null) {_TicketsLinkVenueLabel = (Element)Document.GetElementById(clientId + "_TicketsLinkVenueLabel");}; return _TicketsLinkVenueLabel;}} private Element _TicketsLinkVenueLabel;
		public jQueryObject TicketsLinkVenueLabelJ {get {if (_TicketsLinkVenueLabelJ == null) {_TicketsLinkVenueLabelJ = jQuery.Select("#" + clientId + "_TicketsLinkVenueLabel");}; return _TicketsLinkVenueLabelJ;}} private jQueryObject _TicketsLinkVenueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public AnchorElement FreeGuestlistLink {get {if (_FreeGuestlistLink == null) {_FreeGuestlistLink = (AnchorElement)Document.GetElementById(clientId + "_FreeGuestlistLink");}; return _FreeGuestlistLink;}} private AnchorElement _FreeGuestlistLink;
		public jQueryObject FreeGuestlistLinkJ {get {if (_FreeGuestlistLinkJ == null) {_FreeGuestlistLinkJ = jQuery.Select("#" + clientId + "_FreeGuestlistLink");}; return _FreeGuestlistLinkJ;}} private jQueryObject _FreeGuestlistLinkJ;
		public Element FreeGuestlistLinkVenueLabel {get {if (_FreeGuestlistLinkVenueLabel == null) {_FreeGuestlistLinkVenueLabel = (Element)Document.GetElementById(clientId + "_FreeGuestlistLinkVenueLabel");}; return _FreeGuestlistLinkVenueLabel;}} private Element _FreeGuestlistLinkVenueLabel;
		public jQueryObject FreeGuestlistLinkVenueLabelJ {get {if (_FreeGuestlistLinkVenueLabelJ == null) {_FreeGuestlistLinkVenueLabelJ = jQuery.Select("#" + clientId + "_FreeGuestlistLinkVenueLabel");}; return _FreeGuestlistLinkVenueLabelJ;}} private jQueryObject _FreeGuestlistLinkVenueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement Panel1 {get {if (_Panel1 == null) {_Panel1 = (DivElement)Document.GetElementById(clientId + "_Panel1");}; return _Panel1;}} private DivElement _Panel1;
		public jQueryObject Panel1J {get {if (_Panel1J == null) {_Panel1J = jQuery.Select("#" + clientId + "_Panel1");}; return _Panel1J;}} private jQueryObject _Panel1J;
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element VenueHeader {get {if (_VenueHeader == null) {_VenueHeader = (Element)Document.GetElementById(clientId + "_VenueHeader");}; return _VenueHeader;}} private Element _VenueHeader;
		public jQueryObject VenueHeaderJ {get {if (_VenueHeaderJ == null) {_VenueHeaderJ = jQuery.Select("#" + clientId + "_VenueHeader");}; return _VenueHeaderJ;}} private jQueryObject _VenueHeaderJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element VenueHeader1 {get {if (_VenueHeader1 == null) {_VenueHeader1 = (Element)Document.GetElementById(clientId + "_VenueHeader1");}; return _VenueHeader1;}} private Element _VenueHeader1;
		public jQueryObject VenueHeader1J {get {if (_VenueHeader1J == null) {_VenueHeader1J = jQuery.Select("#" + clientId + "_VenueHeader1");}; return _VenueHeader1J;}} private jQueryObject _VenueHeader1J;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement VenueSelectedPanel {get {if (_VenueSelectedPanel == null) {_VenueSelectedPanel = (DivElement)Document.GetElementById(clientId + "_VenueSelectedPanel");}; return _VenueSelectedPanel;}} private DivElement _VenueSelectedPanel;
		public jQueryObject VenueSelectedPanelJ {get {if (_VenueSelectedPanelJ == null) {_VenueSelectedPanelJ = jQuery.Select("#" + clientId + "_VenueSelectedPanel");}; return _VenueSelectedPanelJ;}} private jQueryObject _VenueSelectedPanelJ;
		public ImageElement VenuePicImg {get {if (_VenuePicImg == null) {_VenuePicImg = (ImageElement)Document.GetElementById(clientId + "_VenuePicImg");}; return _VenuePicImg;}} private ImageElement _VenuePicImg;
		public jQueryObject VenuePicImgJ {get {if (_VenuePicImgJ == null) {_VenuePicImgJ = jQuery.Select("#" + clientId + "_VenuePicImg");}; return _VenuePicImgJ;}} private jQueryObject _VenuePicImgJ;
		public Element VenuePicCell {get {if (_VenuePicCell == null) {_VenuePicCell = (Element)Document.GetElementById(clientId + "_VenuePicCell");}; return _VenuePicCell;}} private Element _VenuePicCell;
		public jQueryObject VenuePicCellJ {get {if (_VenuePicCellJ == null) {_VenuePicCellJ = jQuery.Select("#" + clientId + "_VenuePicCell");}; return _VenuePicCellJ;}} private jQueryObject _VenuePicCellJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableCell", ElementGetter("Element"));
		public AnchorElement DiscussionLink {get {if (_DiscussionLink == null) {_DiscussionLink = (AnchorElement)Document.GetElementById(clientId + "_DiscussionLink");}; return _DiscussionLink;}} private AnchorElement _DiscussionLink;
		public jQueryObject DiscussionLinkJ {get {if (_DiscussionLinkJ == null) {_DiscussionLinkJ = jQuery.Select("#" + clientId + "_DiscussionLink");}; return _DiscussionLinkJ;}} private jQueryObject _DiscussionLinkJ;
		public Element MapSpan {get {if (_MapSpan == null) {_MapSpan = (Element)Document.GetElementById(clientId + "_MapSpan");}; return _MapSpan;}} private Element _MapSpan;
		public jQueryObject MapSpanJ {get {if (_MapSpanJ == null) {_MapSpanJ = jQuery.Select("#" + clientId + "_MapSpan");}; return _MapSpanJ;}} private jQueryObject _MapSpanJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement MapLink {get {if (_MapLink == null) {_MapLink = (AnchorElement)Document.GetElementById(clientId + "_MapLink");}; return _MapLink;}} private AnchorElement _MapLink;
		public jQueryObject MapLinkJ {get {if (_MapLinkJ == null) {_MapLinkJ = jQuery.Select("#" + clientId + "_MapLink");}; return _MapLinkJ;}} private jQueryObject _MapLinkJ;
		public AnchorElement DirectionsLink {get {if (_DirectionsLink == null) {_DirectionsLink = (AnchorElement)Document.GetElementById(clientId + "_DirectionsLink");}; return _DirectionsLink;}} private AnchorElement _DirectionsLink;
		public jQueryObject DirectionsLinkJ {get {if (_DirectionsLinkJ == null) {_DirectionsLinkJ = jQuery.Select("#" + clientId + "_DirectionsLink");}; return _DirectionsLinkJ;}} private jQueryObject _DirectionsLinkJ;
		public Element DiscussionLinkVenueLabel {get {if (_DiscussionLinkVenueLabel == null) {_DiscussionLinkVenueLabel = (Element)Document.GetElementById(clientId + "_DiscussionLinkVenueLabel");}; return _DiscussionLinkVenueLabel;}} private Element _DiscussionLinkVenueLabel;
		public jQueryObject DiscussionLinkVenueLabelJ {get {if (_DiscussionLinkVenueLabelJ == null) {_DiscussionLinkVenueLabelJ = jQuery.Select("#" + clientId + "_DiscussionLinkVenueLabel");}; return _DiscussionLinkVenueLabelJ;}} private jQueryObject _DiscussionLinkVenueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element CalendarLinkVenueLabel {get {if (_CalendarLinkVenueLabel == null) {_CalendarLinkVenueLabel = (Element)Document.GetElementById(clientId + "_CalendarLinkVenueLabel");}; return _CalendarLinkVenueLabel;}} private Element _CalendarLinkVenueLabel;
		public jQueryObject CalendarLinkVenueLabelJ {get {if (_CalendarLinkVenueLabelJ == null) {_CalendarLinkVenueLabelJ = jQuery.Select("#" + clientId + "_CalendarLinkVenueLabel");}; return _CalendarLinkVenueLabelJ;}} private jQueryObject _CalendarLinkVenueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement InfoPanel {get {if (_InfoPanel == null) {_InfoPanel = (DivElement)Document.GetElementById(clientId + "_InfoPanel");}; return _InfoPanel;}} private DivElement _InfoPanel;
		public jQueryObject InfoPanelJ {get {if (_InfoPanelJ == null) {_InfoPanelJ = jQuery.Select("#" + clientId + "_InfoPanel");}; return _InfoPanelJ;}} private jQueryObject _InfoPanelJ;
		public Element EventBodyTitle {get {if (_EventBodyTitle == null) {_EventBodyTitle = (Element)Document.GetElementById(clientId + "_EventBodyTitle");}; return _EventBodyTitle;}} private Element _EventBodyTitle;
		public jQueryObject EventBodyTitleJ {get {if (_EventBodyTitleJ == null) {_EventBodyTitleJ = jQuery.Select("#" + clientId + "_EventBodyTitle");}; return _EventBodyTitleJ;}} private jQueryObject _EventBodyTitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element MusicTypeLabel {get {if (_MusicTypeLabel == null) {_MusicTypeLabel = (Element)Document.GetElementById(clientId + "_MusicTypeLabel");}; return _MusicTypeLabel;}} private Element _MusicTypeLabel;
		public jQueryObject MusicTypeLabelJ {get {if (_MusicTypeLabelJ == null) {_MusicTypeLabelJ = jQuery.Select("#" + clientId + "_MusicTypeLabel");}; return _MusicTypeLabelJ;}} private jQueryObject _MusicTypeLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element Latest {get {if (_Latest == null) {_Latest = (Element)Document.GetElementById(clientId + "_Latest");}; return _Latest;}} private Element _Latest;
		public jQueryObject LatestJ {get {if (_LatestJ == null) {_LatestJ = jQuery.Select("#" + clientId + "_Latest");}; return _LatestJ;}} private jQueryObject _LatestJ;//mappings.Add("Spotted.Controls.Latest", ElementGetter("Element"));
		public Element DiscussionLinkCommentsLabel {get {if (_DiscussionLinkCommentsLabel == null) {_DiscussionLinkCommentsLabel = (Element)Document.GetElementById(clientId + "_DiscussionLinkCommentsLabel");}; return _DiscussionLinkCommentsLabel;}} private Element _DiscussionLinkCommentsLabel;
		public jQueryObject DiscussionLinkCommentsLabelJ {get {if (_DiscussionLinkCommentsLabelJ == null) {_DiscussionLinkCommentsLabelJ = jQuery.Select("#" + clientId + "_DiscussionLinkCommentsLabel");}; return _DiscussionLinkCommentsLabelJ;}} private jQueryObject _DiscussionLinkCommentsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement UpcomingAllEventsPanel {get {if (_UpcomingAllEventsPanel == null) {_UpcomingAllEventsPanel = (DivElement)Document.GetElementById(clientId + "_UpcomingAllEventsPanel");}; return _UpcomingAllEventsPanel;}} private DivElement _UpcomingAllEventsPanel;
		public jQueryObject UpcomingAllEventsPanelJ {get {if (_UpcomingAllEventsPanelJ == null) {_UpcomingAllEventsPanelJ = jQuery.Select("#" + clientId + "_UpcomingAllEventsPanel");}; return _UpcomingAllEventsPanelJ;}} private jQueryObject _UpcomingAllEventsPanelJ;
		public DivElement PreviousAllEventsPanel {get {if (_PreviousAllEventsPanel == null) {_PreviousAllEventsPanel = (DivElement)Document.GetElementById(clientId + "_PreviousAllEventsPanel");}; return _PreviousAllEventsPanel;}} private DivElement _PreviousAllEventsPanel;
		public jQueryObject PreviousAllEventsPanelJ {get {if (_PreviousAllEventsPanelJ == null) {_PreviousAllEventsPanelJ = jQuery.Select("#" + clientId + "_PreviousAllEventsPanel");}; return _PreviousAllEventsPanelJ;}} private jQueryObject _PreviousAllEventsPanelJ;
		public Element VenueNameLabel {get {if (_VenueNameLabel == null) {_VenueNameLabel = (Element)Document.GetElementById(clientId + "_VenueNameLabel");}; return _VenueNameLabel;}} private Element _VenueNameLabel;
		public jQueryObject VenueNameLabelJ {get {if (_VenueNameLabelJ == null) {_VenueNameLabelJ = jQuery.Select("#" + clientId + "_VenueNameLabel");}; return _VenueNameLabelJ;}} private jQueryObject _VenueNameLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public AnchorElement PlaceNameLink {get {if (_PlaceNameLink == null) {_PlaceNameLink = (AnchorElement)Document.GetElementById(clientId + "_PlaceNameLink");}; return _PlaceNameLink;}} private AnchorElement _PlaceNameLink;
		public jQueryObject PlaceNameLinkJ {get {if (_PlaceNameLinkJ == null) {_PlaceNameLinkJ = jQuery.Select("#" + clientId + "_PlaceNameLink");}; return _PlaceNameLinkJ;}} private jQueryObject _PlaceNameLinkJ;
		public AnchorElement CalendarLink {get {if (_CalendarLink == null) {_CalendarLink = (AnchorElement)Document.GetElementById(clientId + "_CalendarLink");}; return _CalendarLink;}} private AnchorElement _CalendarLink;
		public jQueryObject CalendarLinkJ {get {if (_CalendarLinkJ == null) {_CalendarLinkJ = jQuery.Select("#" + clientId + "_CalendarLink");}; return _CalendarLinkJ;}} private jQueryObject _CalendarLinkJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
