Type.registerNamespace('SpottedScript.Pages.Promoters.BannerOptions');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.BannerOptions.View
SpottedScript.Pages.Promoters.BannerOptions.View = function SpottedScript_Pages_Promoters_BannerOptions_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.BannerOptions.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.BannerOptions.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_BannerOptions_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_introBannerListLink: function SpottedScript_Pages_Promoters_BannerOptions_View$get_introBannerListLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroBannerListLink');
    },
    get_introEventOptionsSpan: function SpottedScript_Pages_Promoters_BannerOptions_View$get_introEventOptionsSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroEventOptionsSpan');
    },
    get_introEventOptionsAnchor: function SpottedScript_Pages_Promoters_BannerOptions_View$get_introEventOptionsAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroEventOptionsAnchor');
    },
    get_panelDelete: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelDelete() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDelete');
    },
    get_h119: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h119() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H119');
    },
    get_deleteLabel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_deleteLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteLabel');
    },
    get_panelEdit: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelEdit() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEdit');
    },
    get_adminPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_adminPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminPanel');
    },
    get_h1ssadsf3: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h1ssadsf3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1ssadsf3');
    },
    get_pausePanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_pausePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PausePanel');
    },
    get_pauseButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_pauseButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PauseButton');
    },
    get_resumePanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_resumePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ResumePanel');
    },
    get_resumeButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_resumeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ResumeButton');
    },
    get_h116: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h116() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H116');
    },
    get_editPreviewPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPreviewPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPreviewPanel');
    },
    get_editPreviewOuterDiv: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPreviewOuterDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPreviewOuterDiv');
    },
    get_editPreviewDiv: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPreviewDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPreviewDiv');
    },
    get_editNewPreviewPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editNewPreviewPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditNewPreviewPanel');
    },
    get_editNewPreviewOuterDiv: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editNewPreviewOuterDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditNewPreviewOuterDiv');
    },
    get_editNewPreviewDiv: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editNewPreviewDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditNewPreviewDiv');
    },
    get_bannerStatPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_bannerStatPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerStatPanel');
    },
    get_editTotalHitsLabel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editTotalHitsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditTotalHitsLabel');
    },
    get_editTotalClicksLabel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editTotalClicksLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditTotalClicksLabel');
    },
    get_editTotalClicksPercentageLabel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editTotalClicksPercentageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditTotalClicksPercentageLabel');
    },
    get_bannerStatDataGrid: function SpottedScript_Pages_Promoters_BannerOptions_View$get_bannerStatDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerStatDataGrid');
    },
    get_editNameCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editNameCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditNameCell');
    },
    get_button8: function SpottedScript_Pages_Promoters_BannerOptions_View$get_button8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button8');
    },
    get_editKCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editKCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditKCell');
    },
    get_editPositionCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPositionCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPositionCell');
    },
    get_editPositionLock: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPositionLock() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPositionLock');
    },
    get_editPositionChange: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPositionChange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPositionChange');
    },
    get_editArtworkCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editArtworkCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArtworkCell');
    },
    get_editArtworkLock: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editArtworkLock() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArtworkLock');
    },
    get_editArtworkChange: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editArtworkChange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArtworkChange');
    },
    get_editArtworkCustomise: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editArtworkCustomise() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditArtworkCustomise');
    },
    get_editLinkTargetCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editLinkTargetCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkTargetCell');
    },
    get_button1: function SpottedScript_Pages_Promoters_BannerOptions_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_editDatesCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editDatesCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditDatesCell');
    },
    get_editDatesTick: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editDatesTick() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditDatesTick');
    },
    get_editDatesLock: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editDatesLock() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditDatesLock');
    },
    get_editDatesChange: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editDatesChange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditDatesChange');
    },
    get_editExposureCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editExposureCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditExposureCell');
    },
    get_editExposureTick: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editExposureTick() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditExposureTick');
    },
    get_editExposureLock: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editExposureLock() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditExposureLock');
    },
    get_editExposureChange: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editExposureChange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditExposureChange');
    },
    get_editPlacesCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPlacesCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPlacesCell');
    },
    get_button2: function SpottedScript_Pages_Promoters_BannerOptions_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_editMusicCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editMusicCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditMusicCell');
    },
    get_button4: function SpottedScript_Pages_Promoters_BannerOptions_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_editPaymentCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPaymentCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPaymentCell');
    },
    get_editPaymentTick: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPaymentTick() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPaymentTick');
    },
    get_payment: function SpottedScript_Pages_Promoters_BannerOptions_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_editPaymentLabel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editPaymentLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPaymentLabel');
    },
    get_adminPriceEditP: function SpottedScript_Pages_Promoters_BannerOptions_View$get_adminPriceEditP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminPriceEditP');
    },
    get_fixPriceTextBox: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fixPriceTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FixPriceTextBox');
    },
    get_fixPriceExVatButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fixPriceExVatButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FixPriceExVatButton');
    },
    get_fixPriceIncVatButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fixPriceIncVatButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FixPriceIncVatButton');
    },
    get_fixPriceDiscountButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fixPriceDiscountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FixPriceDiscountButton');
    },
    get_clearFixDiscountButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_clearFixDiscountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClearFixDiscountButton');
    },
    get_editFileBody: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editFileBody() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditFileBody');
    },
    get_editAssignedCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editAssignedCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditAssignedCell');
    },
    get_editFileTick: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editFileTick() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditFileTick');
    },
    get_button6: function SpottedScript_Pages_Promoters_BannerOptions_View$get_button6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button6');
    },
    get_button7: function SpottedScript_Pages_Promoters_BannerOptions_View$get_button7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button7');
    },
    get_editWaitingRow: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editWaitingRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditWaitingRow');
    },
    get_editWaitingCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editWaitingCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditWaitingCell');
    },
    get_editFailedRow: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editFailedRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditFailedRow');
    },
    get_editFailedCell: function SpottedScript_Pages_Promoters_BannerOptions_View$get_editFailedCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditFailedCell');
    },
    get_refundedPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_refundedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefundedPanel');
    },
    get_h116sdfaw: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h116sdfaw() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H116sdfaw');
    },
    get_cancelledPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_cancelledPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelledPanel');
    },
    get_h116sdfa: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h116sdfa() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H116sdfa');
    },
    get_panelExtension: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelExtension() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelExtension');
    },
    get_panelExtensionTitle: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelExtensionTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelExtensionTitle');
    },
    get_panelExtensionOptions: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelExtensionOptions() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelExtensionOptions');
    },
    get_panelExtensionButtons: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelExtensionButtons() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelExtensionButtons');
    },
    get_increaseBannerButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_increaseBannerButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IncreaseBannerButton');
    },
    get_extendBannerButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_extendBannerButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExtendBannerButton');
    },
    get_extensionModeHidden: function SpottedScript_Pages_Promoters_BannerOptions_View$get_extensionModeHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExtensionModeHidden');
    },
    get_panelExtensionSettings: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelExtensionSettings() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelExtensionSettings');
    },
    get_extensionTitleP: function SpottedScript_Pages_Promoters_BannerOptions_View$get_extensionTitleP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExtensionTitleP');
    },
    get_datesCustomEndDateValidator: function SpottedScript_Pages_Promoters_BannerOptions_View$get_datesCustomEndDateValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesCustomEndDateValidator');
    },
    get_datesStartP: function SpottedScript_Pages_Promoters_BannerOptions_View$get_datesStartP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesStartP');
    },
    get_datesEndP: function SpottedScript_Pages_Promoters_BannerOptions_View$get_datesEndP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesEndP');
    },
    get_datesEndCalDiv: function SpottedScript_Pages_Promoters_BannerOptions_View$get_datesEndCalDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatesEndCalDiv');
    },
    get_datesEndCal: function SpottedScript_Pages_Promoters_BannerOptions_View$get_datesEndCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_DatesEndCalController');
    },
    get_exposureTitle: function SpottedScript_Pages_Promoters_BannerOptions_View$get_exposureTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureTitle');
    },
    get_exposureValidator: function SpottedScript_Pages_Promoters_BannerOptions_View$get_exposureValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureValidator');
    },
    get_exposureCustomValidator: function SpottedScript_Pages_Promoters_BannerOptions_View$get_exposureCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureCustomValidator');
    },
    get_exposureLightRadio: function SpottedScript_Pages_Promoters_BannerOptions_View$get_exposureLightRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureLightRadio');
    },
    get_exposureMediumRadio: function SpottedScript_Pages_Promoters_BannerOptions_View$get_exposureMediumRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureMediumRadio');
    },
    get_exposureHeavyRadio: function SpottedScript_Pages_Promoters_BannerOptions_View$get_exposureHeavyRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureHeavyRadio');
    },
    get_exposureCustomRadio: function SpottedScript_Pages_Promoters_BannerOptions_View$get_exposureCustomRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureCustomRadio');
    },
    get_impressionsRow: function SpottedScript_Pages_Promoters_BannerOptions_View$get_impressionsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImpressionsRow');
    },
    get_impressionsTextBox: function SpottedScript_Pages_Promoters_BannerOptions_View$get_impressionsTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImpressionsTextBox');
    },
    get_exposureDetailsRow: function SpottedScript_Pages_Promoters_BannerOptions_View$get_exposureDetailsRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExposureDetailsRow');
    },
    get_saveButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveButton');
    },
    get_copyBannerPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_copyBannerPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CopyBannerPanel');
    },
    get_cancelPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_cancelPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelPanel');
    },
    get_h1164321sdaf: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h1164321sdaf() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1164321sdaf');
    },
    get_cancelButton: function SpottedScript_Pages_Promoters_BannerOptions_View$get_cancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelButton');
    },
    get_optionsPanel: function SpottedScript_Pages_Promoters_BannerOptions_View$get_optionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsPanel');
    },
    get_h13: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_panelExtensionSuccessful: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelExtensionSuccessful() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelExtensionSuccessful');
    },
    get_h14: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_panelCancelSuccessful: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelCancelSuccessful() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelCancelSuccessful');
    },
    get_h1: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_panelFileAssign: function SpottedScript_Pages_Promoters_BannerOptions_View$get_panelFileAssign() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelFileAssign');
    },
    get_h118: function SpottedScript_Pages_Promoters_BannerOptions_View$get_h118() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H118');
    },
    get_fileAssignDropDownP: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fileAssignDropDownP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FileAssignDropDownP');
    },
    get_fileAssignDropDown: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fileAssignDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FileAssignDropDown');
    },
    get_fileAssignNoFiles: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fileAssignNoFiles() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FileAssignNoFiles');
    },
    get_fileAssignNoFilesUploadLink: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fileAssignNoFilesUploadLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FileAssignNoFilesUploadLink');
    },
    get_fileAssignPreviewDiv: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fileAssignPreviewDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FileAssignPreviewDiv');
    },
    get_fileAssignDiv: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fileAssignDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FileAssignDiv');
    },
    get_fileWaitingForCheckP: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fileWaitingForCheckP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FileWaitingForCheckP');
    },
    get_fileCheckedP: function SpottedScript_Pages_Promoters_BannerOptions_View$get_fileCheckedP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FileCheckedP');
    },
    get_button13: function SpottedScript_Pages_Promoters_BannerOptions_View$get_button13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button13');
    },
    get_button14: function SpottedScript_Pages_Promoters_BannerOptions_View$get_button14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button14');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_BannerOptions_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.BannerOptions.View.registerClass('SpottedScript.Pages.Promoters.BannerOptions.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
