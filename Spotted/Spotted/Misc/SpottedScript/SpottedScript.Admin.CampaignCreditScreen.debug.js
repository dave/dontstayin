Type.registerNamespace('SpottedScript.Admin.CampaignCreditScreen');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.CampaignCreditScreen.View
SpottedScript.Admin.CampaignCreditScreen.View = function SpottedScript_Admin_CampaignCreditScreen_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.CampaignCreditScreen.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.CampaignCreditScreen.View.prototype = {
    clientId: null,
    get_h1_1: function SpottedScript_Admin_CampaignCreditScreen_View$get_h1_1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1_1');
    },
    get_mainPanel: function SpottedScript_Admin_CampaignCreditScreen_View$get_mainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MainPanel');
    },
    get_campaignCreditKLabel: function SpottedScript_Admin_CampaignCreditScreen_View$get_campaignCreditKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CampaignCreditKLabel');
    },
    get_actionDateTimeLabel: function SpottedScript_Admin_CampaignCreditScreen_View$get_actionDateTimeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionDateTimeLabel');
    },
    get_uiPromotersAutoComplete: function SpottedScript_Admin_CampaignCreditScreen_View$get_uiPromotersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromotersAutoCompleteBehaviour');
    },
    get_promoterValueLabel: function SpottedScript_Admin_CampaignCreditScreen_View$get_promoterValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterValueLabel');
    },
    get_promoterCampaignCreditsLabel: function SpottedScript_Admin_CampaignCreditScreen_View$get_promoterCampaignCreditsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterCampaignCreditsLabel');
    },
    get_promoterRequiredFieldValidator: function SpottedScript_Admin_CampaignCreditScreen_View$get_promoterRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterRequiredFieldValidator');
    },
    get_usrDropDownList: function SpottedScript_Admin_CampaignCreditScreen_View$get_usrDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrDropDownList');
    },
    get_usrValueLabel: function SpottedScript_Admin_CampaignCreditScreen_View$get_usrValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrValueLabel');
    },
    get_uiActionUserAutoComplete: function SpottedScript_Admin_CampaignCreditScreen_View$get_uiActionUserAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiActionUserAutoCompleteBehaviour');
    },
    get_actionUsrValueLabel: function SpottedScript_Admin_CampaignCreditScreen_View$get_actionUsrValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUsrValueLabel');
    },
    get_actionUsrRequiredFieldValidator: function SpottedScript_Admin_CampaignCreditScreen_View$get_actionUsrRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionUsrRequiredFieldValidator');
    },
    get_buyableObjectTypeDropDownList: function SpottedScript_Admin_CampaignCreditScreen_View$get_buyableObjectTypeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyableObjectTypeDropDownList');
    },
    get_buyableObjectTypeValueLabel: function SpottedScript_Admin_CampaignCreditScreen_View$get_buyableObjectTypeValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyableObjectTypeValueLabel');
    },
    get_buyableObjectKRow: function SpottedScript_Admin_CampaignCreditScreen_View$get_buyableObjectKRow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyableObjectKRow');
    },
    get_buyableObjectKTextBox: function SpottedScript_Admin_CampaignCreditScreen_View$get_buyableObjectKTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyableObjectKTextBox');
    },
    get_buyableObjectCustomValidator: function SpottedScript_Admin_CampaignCreditScreen_View$get_buyableObjectCustomValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyableObjectCustomValidator');
    },
    get_invoiceItemTypeDropDownList: function SpottedScript_Admin_CampaignCreditScreen_View$get_invoiceItemTypeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceItemTypeDropDownList');
    },
    get_invoiceItemTypeValueLabel: function SpottedScript_Admin_CampaignCreditScreen_View$get_invoiceItemTypeValueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvoiceItemTypeValueLabel');
    },
    get_creditsTextBox: function SpottedScript_Admin_CampaignCreditScreen_View$get_creditsTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditsTextBox');
    },
    get_creditsRequiredFieldValidator: function SpottedScript_Admin_CampaignCreditScreen_View$get_creditsRequiredFieldValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditsRequiredFieldValidator');
    },
    get_creditsRangeValidator: function SpottedScript_Admin_CampaignCreditScreen_View$get_creditsRangeValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditsRangeValidator');
    },
    get_descriptionTextBox: function SpottedScript_Admin_CampaignCreditScreen_View$get_descriptionTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DescriptionTextBox');
    },
    get_notesAddOnlyTextBox: function SpottedScript_Admin_CampaignCreditScreen_View$get_notesAddOnlyTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotesAddOnlyTextBox');
    },
    get_campaignCreditValidationSummary: function SpottedScript_Admin_CampaignCreditScreen_View$get_campaignCreditValidationSummary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CampaignCreditValidationSummary');
    },
    get_saveButton: function SpottedScript_Admin_CampaignCreditScreen_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveButton');
    },
    get_cancelButton: function SpottedScript_Admin_CampaignCreditScreen_View$get_cancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelButton');
    },
    get_genericContainerPage: function SpottedScript_Admin_CampaignCreditScreen_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.CampaignCreditScreen.View.registerClass('SpottedScript.Admin.CampaignCreditScreen.View', SpottedScript.AdminUserControl.View);
