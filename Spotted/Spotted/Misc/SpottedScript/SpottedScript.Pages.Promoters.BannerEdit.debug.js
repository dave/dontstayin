Type.registerNamespace('SpottedScript.Pages.Promoters.BannerEdit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.BannerEdit.View
SpottedScript.Pages.Promoters.BannerEdit.View = function SpottedScript_Pages_Promoters_BannerEdit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.BannerEdit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.BannerEdit.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_BannerEdit_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_introBannerListLink: function SpottedScript_Pages_Promoters_BannerEdit_View$get_introBannerListLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroBannerListLink');
    },
    get_h12: function SpottedScript_Pages_Promoters_BannerEdit_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_panel2: function SpottedScript_Pages_Promoters_BannerEdit_View$get_panel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Panel2');
    },
    get_button2: function SpottedScript_Pages_Promoters_BannerEdit_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_button1: function SpottedScript_Pages_Promoters_BannerEdit_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_modePanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_modePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModePanel');
    },
    get_modeBeginnerRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_modeBeginnerRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeBeginnerRadio');
    },
    get_modeExpertRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_modeExpertRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeExpertRadio');
    },
    get_linkPanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkPanel');
    },
    get_linkValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkValidator');
    },
    get_linkCustomValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkCustomValidator');
    },
    get_linkEventSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkEventSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkEventSpan');
    },
    get_linkEventRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkEventRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkEventRadio');
    },
    get_linkEventLockedSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkEventLockedSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkEventLockedSpan');
    },
    get_linkEventDropDown: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkEventDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkEventDropDown');
    },
    get_linkBrandSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkBrandSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkBrandSpan');
    },
    get_linkBrandRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkBrandRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkBrandRadio');
    },
    get_linkBrandDropDown: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkBrandDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkBrandDropDown');
    },
    get_linkVenueSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkVenueSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkVenueSpan');
    },
    get_linkVenueRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkVenueRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkVenueRadio');
    },
    get_linkVenueDropDown: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkVenueDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkVenueDropDown');
    },
    get_linkTicketsSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkTicketsSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkTicketsSpan');
    },
    get_linkTicketsRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkTicketsRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkTicketsRadio');
    },
    get_linkTicketsDropDown: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkTicketsDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkTicketsDropDown');
    },
    get_linkCustomSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkCustomSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkCustomSpan');
    },
    get_linkCustomRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkCustomRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkCustomRadio');
    },
    get_linkCustomTextBox: function SpottedScript_Pages_Promoters_BannerEdit_View$get_linkCustomTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkCustomTextBox');
    },
    get_positionPanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionPanel');
    },
    get_positionValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionValidator');
    },
    get_positionLockedRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionLockedRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionLockedRow');
    },
    get_positionLockedLabel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionLockedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionLockedLabel');
    },
    get_positionRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionRow');
    },
    get_positionHotboxRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionHotboxRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionHotboxRadio');
    },
    get_positionLeaderboardRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionLeaderboardRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionLeaderboardRadio');
    },
    get_positionSkyscraperRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionSkyscraperRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionSkyscraperRadio');
    },
    get_positionPhotoBannerRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionPhotoBannerRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionPhotoBannerRadio');
    },
    get_positionEmailBannerRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_positionEmailBannerRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PositionEmailBannerRadio');
    },
    get_datesPanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesPanel');
    },
    get_datesHead: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesHead() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesHead');
    },
    get_datesValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesValidator');
    },
    get_datesCustomValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesCustomValidator');
    },
    get_datesCustomEndDateValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesCustomEndDateValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesCustomEndDateValidator');
    },
    get_datesLockedRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesLockedRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesLockedRow');
    },
    get_datesLockedLabel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesLockedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesLockedLabel');
    },
    get_datesAutoRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesAutoRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesAutoRow');
    },
    get_dates1WeekSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_dates1WeekSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Dates1WeekSpan');
    },
    get_dates1WeekRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_dates1WeekRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Dates1WeekRadio');
    },
    get_dates2WeekSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_dates2WeekSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Dates2WeekSpan');
    },
    get_dates2WeekRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_dates2WeekRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Dates2WeekRadio');
    },
    get_dates3WeekSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_dates3WeekSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Dates3WeekSpan');
    },
    get_dates3WeekRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_dates3WeekRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Dates3WeekRadio');
    },
    get_dates4WeekSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_dates4WeekSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Dates4WeekSpan');
    },
    get_dates4WeekRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_dates4WeekRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Dates4WeekRadio');
    },
    get_datesCustomRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesCustomRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesCustomRadio');
    },
    get_datesStartDateRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesStartDateRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesStartDateRow');
    },
    get_datesStartCal: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesStartCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_DatesStartCalController');
    },
    get_datesEndDateRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesEndDateRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesEndDateRow');
    },
    get_datesEndCal: function SpottedScript_Pages_Promoters_BannerEdit_View$get_datesEndCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_DatesEndCalController');
    },
    get_exposurePanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposurePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposurePanel');
    },
    get_exposureValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureValidator');
    },
    get_exposureCustomValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureCustomValidator');
    },
    get_exposureLockedRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureLockedRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureLockedRow');
    },
    get_exposureLockedLabel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureLockedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureLockedLabel');
    },
    get_exposureRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureRow');
    },
    get_exposureLightRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureLightRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureLightRadio');
    },
    get_exposureMediumRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureMediumRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureMediumRadio');
    },
    get_exposureHeavyRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureHeavyRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureHeavyRadio');
    },
    get_exposureCustomRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureCustomRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureCustomRadio');
    },
    get_impressionsRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_impressionsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImpressionsRow');
    },
    get_impressionsTextBox: function SpottedScript_Pages_Promoters_BannerEdit_View$get_impressionsTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImpressionsTextBox');
    },
    get_exposureDetailsRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureDetailsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureDetailsRow');
    },
    get_exposureLevelP: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureLevelP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureLevelP');
    },
    get_exposureCostCampaignCreditsP: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureCostCampaignCreditsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureCostCampaignCreditsP');
    },
    get_exposureCostCashP: function SpottedScript_Pages_Promoters_BannerEdit_View$get_exposureCostCashP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureCostCashP');
    },
    get_targettingPanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_targettingPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TargettingPanel');
    },
    get_targettingValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_targettingValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TargettingValidator');
    },
    get_targettingCustomValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_targettingCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TargettingCustomValidator');
    },
    get_targettingAutomaticSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_targettingAutomaticSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TargettingAutomaticSpan');
    },
    get_targettingAutomaticRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_targettingAutomaticRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TargettingAutomaticRadio');
    },
    get_targettingNoneRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_targettingNoneRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TargettingNoneRadio');
    },
    get_targettingCustomRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_targettingCustomRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TargettingCustomRadio');
    },
    get_locationTargettingRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_locationTargettingRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationTargettingRow');
    },
    get_locationTargettingHidden: function SpottedScript_Pages_Promoters_BannerEdit_View$get_locationTargettingHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationTargettingHidden');
    },
    get_locationTargettingTextBox: function SpottedScript_Pages_Promoters_BannerEdit_View$get_locationTargettingTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationTargettingTextBox');
    },
    get_locationTargettingButton: function SpottedScript_Pages_Promoters_BannerEdit_View$get_locationTargettingButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationTargettingButton');
    },
    get_musicTargettingRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_musicTargettingRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTargettingRow');
    },
    get_musicTargettingHidden: function SpottedScript_Pages_Promoters_BannerEdit_View$get_musicTargettingHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTargettingHidden');
    },
    get_musicTargettingTextBox: function SpottedScript_Pages_Promoters_BannerEdit_View$get_musicTargettingTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTargettingTextBox');
    },
    get_musicTargettingButton: function SpottedScript_Pages_Promoters_BannerEdit_View$get_musicTargettingButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTargettingButton');
    },
    get_artworkPanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkPanel');
    },
    get_artworkValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkValidator');
    },
    get_artworkLockedRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkLockedRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkLockedRow');
    },
    get_artworkLockedLabel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkLockedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkLockedLabel');
    },
    get_artworkRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkRow');
    },
    get_artworkUploadRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkUploadRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkUploadRadio');
    },
    get_artworkJpgRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkJpgRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkJpgRadio');
    },
    get_artworkGifRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkGifRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkGifRadio');
    },
    get_artworkFlashSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkFlashSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkFlashSpan');
    },
    get_artworkFlashRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkFlashRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkFlashRadio');
    },
    get_artworkAutomaticSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkAutomaticSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkAutomaticSpan');
    },
    get_artworkAutomaticRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_artworkAutomaticRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArtworkAutomaticRadio');
    },
    get_automaticArtworkRow: function SpottedScript_Pages_Promoters_BannerEdit_View$get_automaticArtworkRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutomaticArtworkRow');
    },
    get_automaticEventBannerHidden: function SpottedScript_Pages_Promoters_BannerEdit_View$get_automaticEventBannerHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutomaticEventBannerHidden');
    },
    get_automaticEventBannerTextBox: function SpottedScript_Pages_Promoters_BannerEdit_View$get_automaticEventBannerTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutomaticEventBannerTextBox');
    },
    get_automaticEventBannerButton: function SpottedScript_Pages_Promoters_BannerEdit_View$get_automaticEventBannerButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutomaticEventBannerButton');
    },
    get_namePanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_namePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NamePanel');
    },
    get_nameValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_nameValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NameValidator');
    },
    get_nameTextBox: function SpottedScript_Pages_Promoters_BannerEdit_View$get_nameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NameTextBox');
    },
    get_folderPanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_folderPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderPanel');
    },
    get_folderValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_folderValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderValidator');
    },
    get_folderActionEventSpan: function SpottedScript_Pages_Promoters_BannerEdit_View$get_folderActionEventSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderActionEventSpan');
    },
    get_folderActionEventRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_folderActionEventRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderActionEventRadio');
    },
    get_folderActionExistingRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_folderActionExistingRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderActionExistingRadio');
    },
    get_folderExistingDropDown: function SpottedScript_Pages_Promoters_BannerEdit_View$get_folderExistingDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderExistingDropDown');
    },
    get_folderActionNewRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_folderActionNewRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderActionNewRadio');
    },
    get_folderNewTextBox: function SpottedScript_Pages_Promoters_BannerEdit_View$get_folderNewTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderNewTextBox');
    },
    get_bookPanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_bookPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookPanel');
    },
    get_bookValidator: function SpottedScript_Pages_Promoters_BannerEdit_View$get_bookValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookValidator');
    },
    get_bookNowCreditsRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_bookNowCreditsRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookNowCreditsRadio');
    },
    get_bookNowCashRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_bookNowCashRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookNowCashRadio');
    },
    get_bookLaterRadio: function SpottedScript_Pages_Promoters_BannerEdit_View$get_bookLaterRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookLaterRadio');
    },
    get_backNextPanel: function SpottedScript_Pages_Promoters_BannerEdit_View$get_backNextPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BackNextPanel');
    },
    get_currentPanelIndexHidden: function SpottedScript_Pages_Promoters_BannerEdit_View$get_currentPanelIndexHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CurrentPanelIndexHidden');
    },
    get_panel1: function SpottedScript_Pages_Promoters_BannerEdit_View$get_panel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Panel1');
    },
    get_button3: function SpottedScript_Pages_Promoters_BannerEdit_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_button4: function SpottedScript_Pages_Promoters_BannerEdit_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_BannerEdit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.BannerEdit.View.registerClass('SpottedScript.Pages.Promoters.BannerEdit.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
