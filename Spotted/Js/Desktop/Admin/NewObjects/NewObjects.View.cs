//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Admin.BannerDataGrid", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.NewObjects
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
		public Element NewSpotterNumberLabel {get {if (_NewSpotterNumberLabel == null) {_NewSpotterNumberLabel = (Element)Document.GetElementById(clientId + "_NewSpotterNumberLabel");}; return _NewSpotterNumberLabel;}} private Element _NewSpotterNumberLabel;
		public jQueryObject NewSpotterNumberLabelJ {get {if (_NewSpotterNumberLabelJ == null) {_NewSpotterNumberLabelJ = jQuery.Select("#" + clientId + "_NewSpotterNumberLabel");}; return _NewSpotterNumberLabelJ;}} private jQueryObject _NewSpotterNumberLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SpotterCardRequestLabel {get {if (_SpotterCardRequestLabel == null) {_SpotterCardRequestLabel = (Element)Document.GetElementById(clientId + "_SpotterCardRequestLabel");}; return _SpotterCardRequestLabel;}} private Element _SpotterCardRequestLabel;
		public jQueryObject SpotterCardRequestLabelJ {get {if (_SpotterCardRequestLabelJ == null) {_SpotterCardRequestLabelJ = jQuery.Select("#" + clientId + "_SpotterCardRequestLabel");}; return _SpotterCardRequestLabelJ;}} private jQueryObject _SpotterCardRequestLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element SpotterDl {get {if (_SpotterDl == null) {_SpotterDl = (Element)Document.GetElementById(clientId + "_SpotterDl");}; return _SpotterDl;}} private Element _SpotterDl;
		public jQueryObject SpotterDlJ {get {if (_SpotterDlJ == null) {_SpotterDlJ = jQuery.Select("#" + clientId + "_SpotterDl");}; return _SpotterDlJ;}} private jQueryObject _SpotterDlJ;//mappings.Add("System.Web.UI.WebControls.DataList", ElementGetter("Element"));
		public Element GalleriesDataGrid {get {if (_GalleriesDataGrid == null) {_GalleriesDataGrid = (Element)Document.GetElementById(clientId + "_GalleriesDataGrid");}; return _GalleriesDataGrid;}} private Element _GalleriesDataGrid;
		public jQueryObject GalleriesDataGridJ {get {if (_GalleriesDataGridJ == null) {_GalleriesDataGridJ = jQuery.Select("#" + clientId + "_GalleriesDataGrid");}; return _GalleriesDataGridJ;}} private jQueryObject _GalleriesDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element NewEvents {get {if (_NewEvents == null) {_NewEvents = (Element)Document.GetElementById(clientId + "_NewEvents");}; return _NewEvents;}} private Element _NewEvents;
		public jQueryObject NewEventsJ {get {if (_NewEventsJ == null) {_NewEventsJ = jQuery.Select("#" + clientId + "_NewEvents");}; return _NewEventsJ;}} private jQueryObject _NewEventsJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element LiveEmailBanners {get {if (_LiveEmailBanners == null) {_LiveEmailBanners = (Element)Document.GetElementById(clientId + "_LiveEmailBanners");}; return _LiveEmailBanners;}} private Element _LiveEmailBanners;
		public jQueryObject LiveEmailBannersJ {get {if (_LiveEmailBannersJ == null) {_LiveEmailBannersJ = jQuery.Select("#" + clientId + "_LiveEmailBanners");}; return _LiveEmailBannersJ;}} private jQueryObject _LiveEmailBannersJ;//mappings.Add("Spotted.Controls.Admin.BannerDataGrid", ElementGetter("Element"));
		public Element LivePhotoBanners {get {if (_LivePhotoBanners == null) {_LivePhotoBanners = (Element)Document.GetElementById(clientId + "_LivePhotoBanners");}; return _LivePhotoBanners;}} private Element _LivePhotoBanners;
		public jQueryObject LivePhotoBannersJ {get {if (_LivePhotoBannersJ == null) {_LivePhotoBannersJ = jQuery.Select("#" + clientId + "_LivePhotoBanners");}; return _LivePhotoBannersJ;}} private jQueryObject _LivePhotoBannersJ;//mappings.Add("Spotted.Controls.Admin.BannerDataGrid", ElementGetter("Element"));
		public Element LiveHotBoxes {get {if (_LiveHotBoxes == null) {_LiveHotBoxes = (Element)Document.GetElementById(clientId + "_LiveHotBoxes");}; return _LiveHotBoxes;}} private Element _LiveHotBoxes;
		public jQueryObject LiveHotBoxesJ {get {if (_LiveHotBoxesJ == null) {_LiveHotBoxesJ = jQuery.Select("#" + clientId + "_LiveHotBoxes");}; return _LiveHotBoxesJ;}} private jQueryObject _LiveHotBoxesJ;//mappings.Add("Spotted.Controls.Admin.BannerDataGrid", ElementGetter("Element"));
		public Element LiveLeaderboards {get {if (_LiveLeaderboards == null) {_LiveLeaderboards = (Element)Document.GetElementById(clientId + "_LiveLeaderboards");}; return _LiveLeaderboards;}} private Element _LiveLeaderboards;
		public jQueryObject LiveLeaderboardsJ {get {if (_LiveLeaderboardsJ == null) {_LiveLeaderboardsJ = jQuery.Select("#" + clientId + "_LiveLeaderboards");}; return _LiveLeaderboardsJ;}} private jQueryObject _LiveLeaderboardsJ;//mappings.Add("Spotted.Controls.Admin.BannerDataGrid", ElementGetter("Element"));
		public Element LiveSkyscrapers {get {if (_LiveSkyscrapers == null) {_LiveSkyscrapers = (Element)Document.GetElementById(clientId + "_LiveSkyscrapers");}; return _LiveSkyscrapers;}} private Element _LiveSkyscrapers;
		public jQueryObject LiveSkyscrapersJ {get {if (_LiveSkyscrapersJ == null) {_LiveSkyscrapersJ = jQuery.Select("#" + clientId + "_LiveSkyscrapers");}; return _LiveSkyscrapersJ;}} private jQueryObject _LiveSkyscrapersJ;//mappings.Add("Spotted.Controls.Admin.BannerDataGrid", ElementGetter("Element"));
		public Element PaidForBanners {get {if (_PaidForBanners == null) {_PaidForBanners = (Element)Document.GetElementById(clientId + "_PaidForBanners");}; return _PaidForBanners;}} private Element _PaidForBanners;
		public jQueryObject PaidForBannersJ {get {if (_PaidForBannersJ == null) {_PaidForBannersJ = jQuery.Select("#" + clientId + "_PaidForBanners");}; return _PaidForBannersJ;}} private jQueryObject _PaidForBannersJ;//mappings.Add("Spotted.Controls.Admin.BannerDataGrid", ElementGetter("Element"));
		public Element NotPaidForBanners {get {if (_NotPaidForBanners == null) {_NotPaidForBanners = (Element)Document.GetElementById(clientId + "_NotPaidForBanners");}; return _NotPaidForBanners;}} private Element _NotPaidForBanners;
		public jQueryObject NotPaidForBannersJ {get {if (_NotPaidForBannersJ == null) {_NotPaidForBannersJ = jQuery.Select("#" + clientId + "_NotPaidForBanners");}; return _NotPaidForBannersJ;}} private jQueryObject _NotPaidForBannersJ;//mappings.Add("Spotted.Controls.Admin.BannerDataGrid", ElementGetter("Element"));
		public Element BannedUsrDataGrid {get {if (_BannedUsrDataGrid == null) {_BannedUsrDataGrid = (Element)Document.GetElementById(clientId + "_BannedUsrDataGrid");}; return _BannedUsrDataGrid;}} private Element _BannedUsrDataGrid;
		public jQueryObject BannedUsrDataGridJ {get {if (_BannedUsrDataGridJ == null) {_BannedUsrDataGridJ = jQuery.Select("#" + clientId + "_BannedUsrDataGrid");}; return _BannedUsrDataGridJ;}} private jQueryObject _BannedUsrDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element GuestlistDataGrid {get {if (_GuestlistDataGrid == null) {_GuestlistDataGrid = (Element)Document.GetElementById(clientId + "_GuestlistDataGrid");}; return _GuestlistDataGrid;}} private Element _GuestlistDataGrid;
		public jQueryObject GuestlistDataGridJ {get {if (_GuestlistDataGridJ == null) {_GuestlistDataGridJ = jQuery.Select("#" + clientId + "_GuestlistDataGrid");}; return _GuestlistDataGridJ;}} private jQueryObject _GuestlistDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element BrandRepeater {get {if (_BrandRepeater == null) {_BrandRepeater = (Element)Document.GetElementById(clientId + "_BrandRepeater");}; return _BrandRepeater;}} private Element _BrandRepeater;
		public jQueryObject BrandRepeaterJ {get {if (_BrandRepeaterJ == null) {_BrandRepeaterJ = jQuery.Select("#" + clientId + "_BrandRepeater");}; return _BrandRepeaterJ;}} private jQueryObject _BrandRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public DivElement PanelUnconfirmedBrands {get {if (_PanelUnconfirmedBrands == null) {_PanelUnconfirmedBrands = (DivElement)Document.GetElementById(clientId + "_PanelUnconfirmedBrands");}; return _PanelUnconfirmedBrands;}} private DivElement _PanelUnconfirmedBrands;
		public jQueryObject PanelUnconfirmedBrandsJ {get {if (_PanelUnconfirmedBrandsJ == null) {_PanelUnconfirmedBrandsJ = jQuery.Select("#" + clientId + "_PanelUnconfirmedBrands");}; return _PanelUnconfirmedBrandsJ;}} private jQueryObject _PanelUnconfirmedBrandsJ;
		public DivElement UpdateDonePanel {get {if (_UpdateDonePanel == null) {_UpdateDonePanel = (DivElement)Document.GetElementById(clientId + "_UpdateDonePanel");}; return _UpdateDonePanel;}} private DivElement _UpdateDonePanel;
		public jQueryObject UpdateDonePanelJ {get {if (_UpdateDonePanelJ == null) {_UpdateDonePanelJ = jQuery.Select("#" + clientId + "_UpdateDonePanel");}; return _UpdateDonePanelJ;}} private jQueryObject _UpdateDonePanelJ;
		public DivElement PanelUnconfirmedVenues {get {if (_PanelUnconfirmedVenues == null) {_PanelUnconfirmedVenues = (DivElement)Document.GetElementById(clientId + "_PanelUnconfirmedVenues");}; return _PanelUnconfirmedVenues;}} private DivElement _PanelUnconfirmedVenues;
		public jQueryObject PanelUnconfirmedVenuesJ {get {if (_PanelUnconfirmedVenuesJ == null) {_PanelUnconfirmedVenuesJ = jQuery.Select("#" + clientId + "_PanelUnconfirmedVenues");}; return _PanelUnconfirmedVenuesJ;}} private jQueryObject _PanelUnconfirmedVenuesJ;
		public Element VenueRepeater {get {if (_VenueRepeater == null) {_VenueRepeater = (Element)Document.GetElementById(clientId + "_VenueRepeater");}; return _VenueRepeater;}} private Element _VenueRepeater;
		public jQueryObject VenueRepeaterJ {get {if (_VenueRepeaterJ == null) {_VenueRepeaterJ = jQuery.Select("#" + clientId + "_VenueRepeater");}; return _VenueRepeaterJ;}} private jQueryObject _VenueRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
