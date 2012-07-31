Type.registerNamespace('SpottedScript.Pages.Promoters.CampaignCreditHistory');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.CampaignCreditHistory.View
SpottedScript.Pages.Promoters.CampaignCreditHistory.View = function SpottedScript_Pages_Promoters_CampaignCreditHistory_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.CampaignCreditHistory.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.CampaignCreditHistory.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_CampaignCreditHistory_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_h1Title: function SpottedScript_Pages_Promoters_CampaignCreditHistory_View$get_h1Title() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Title');
    },
    get_campaignCreditsHistoryPanel: function SpottedScript_Pages_Promoters_CampaignCreditHistory_View$get_campaignCreditsHistoryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CampaignCreditsHistoryPanel');
    },
    get_campaignCreditHistoryGridView: function SpottedScript_Pages_Promoters_CampaignCreditHistory_View$get_campaignCreditHistoryGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CampaignCreditHistoryGridView');
    },
    get_uiPaginationControl: function SpottedScript_Pages_Promoters_CampaignCreditHistory_View$get_uiPaginationControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPaginationControl');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_CampaignCreditHistory_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.CampaignCreditHistory.View.registerClass('SpottedScript.Pages.Promoters.CampaignCreditHistory.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
