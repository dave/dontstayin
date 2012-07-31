Type.registerNamespace('SpottedScript.Pages.Promoters.CampaignCredits');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.CampaignCredits.View
SpottedScript.Pages.Promoters.CampaignCredits.View = function SpottedScript_Pages_Promoters_CampaignCredits_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.CampaignCredits.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.CampaignCredits.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_h1Title: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_h1Title() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Title');
    },
    get_creditsPanel: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_creditsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditsPanel');
    },
    get_selectedCredits: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_selectedCredits() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedCredits');
    },
    get_creditsTable: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_creditsTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditsTable');
    },
    get_buyButton: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_buyButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyButton');
    },
    get_ensureCampaignCreditsSelectedValidator: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_ensureCampaignCreditsSelectedValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EnsureCampaignCreditsSelectedValidator');
    },
    get_customCampaignCreditsCustomValidator: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_customCampaignCreditsCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomCampaignCreditsCustomValidator');
    },
    get_paymentPanel: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_paymentPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaymentPanel');
    },
    get_payment: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_adminPriceEditP: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_adminPriceEditP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminPriceEditP');
    },
    get_fixPriceTextBox: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_fixPriceTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FixPriceTextBox');
    },
    get_fixPriceExVatButton: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_fixPriceExVatButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FixPriceExVatButton');
    },
    get_fixPriceIncVatButton: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_fixPriceIncVatButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FixPriceIncVatButton');
    },
    get_fixPriceDiscountButton: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_fixPriceDiscountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FixPriceDiscountButton');
    },
    get_clearFixDiscountButton: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_clearFixDiscountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClearFixDiscountButton');
    },
    get_backToCreditOptionsButton: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_backToCreditOptionsButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BackToCreditOptionsButton');
    },
    get_successPanel: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_successPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuccessPanel');
    },
    get_customRadioButton: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_customRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomRadioButton');
    },
    get_customCreditsTextBox: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_customCreditsTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomCreditsTextBox');
    },
    get_customPriceLabel: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_customPriceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomPriceLabel');
    },
    get_customDiscountLabel: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_customDiscountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomDiscountLabel');
    },
    get_customTotalTextBox: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_customTotalTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomTotalTextBox');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_CampaignCredits_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.CampaignCredits.View.registerClass('SpottedScript.Pages.Promoters.CampaignCredits.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
