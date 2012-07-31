Type.registerNamespace('SpottedScript.Admin.InsertionOrderScreen');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.InsertionOrderScreen.View
SpottedScript.Admin.InsertionOrderScreen.View = function SpottedScript_Admin_InsertionOrderScreen_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.InsertionOrderScreen.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.InsertionOrderScreen.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Admin_InsertionOrderScreen_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_objectDataSource1: function SpottedScript_Admin_InsertionOrderScreen_View$get_objectDataSource1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ObjectDataSource1');
    },
    get_mainPanel: function SpottedScript_Admin_InsertionOrderScreen_View$get_mainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MainPanel');
    },
    get_uiInsertionOrderKLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiInsertionOrderKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInsertionOrderKLabel');
    },
    get_uiInsertionOrderK: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiInsertionOrderK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInsertionOrderK');
    },
    get_uiCreatedDateLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCreatedDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCreatedDateLabel');
    },
    get_uiCreatedDate: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCreatedDate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCreatedDate');
    },
    get_uiCampaignNameLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignNameLabel');
    },
    get_uiCampaignName: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignName');
    },
    get_promoterLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_promoterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterLabel');
    },
    get_uiPromoterAutoComplete: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiPromoterAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromoterAutoCompleteBehaviour');
    },
    get_uiCampaignStartLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignStartLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignStartLabel');
    },
    get_uiCampaignStartCal: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignStartCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiCampaignStartCalController');
    },
    get_userLabel0: function SpottedScript_Admin_InsertionOrderScreen_View$get_userLabel0() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UserLabel0');
    },
    get_uiUserDropDownList: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiUserDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUserDropDownList');
    },
    get_uiUsrNameOverride: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiUsrNameOverride() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUsrNameOverride');
    },
    get_uiUsrValidator: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiUsrValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUsrValidator');
    },
    get_uiCampaignEndLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignEndLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignEndLabel');
    },
    get_uiCampaignEndCal: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignEndCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiCampaignEndCalController');
    },
    get_uiCampaignEndDateAfterStartValidator: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignEndDateAfterStartValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignEndDateAfterStartValidator');
    },
    get_uiActionUsrLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiActionUsrLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiActionUsrLabel');
    },
    get_uiActionUserAutoComplete: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiActionUserAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiActionUserAutoCompleteBehaviour');
    },
    get_uiActionUsrValidator: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiActionUsrValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiActionUsrValidator');
    },
    get_uiNextInvoiceDueLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiNextInvoiceDueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNextInvoiceDueLabel');
    },
    get_uiNextInvoiceDue: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiNextInvoiceDue() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiNextInvoiceDueController');
    },
    get_uiAgencyDiscountLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiAgencyDiscountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAgencyDiscountLabel');
    },
    get_uiAgencyDiscount: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiAgencyDiscount() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAgencyDiscount');
    },
    get_uiAgencyDiscountRangeValidator0: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiAgencyDiscountRangeValidator0() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAgencyDiscountRangeValidator0');
    },
    get_uiAgencyDiscountRangeValidator1: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiAgencyDiscountRangeValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAgencyDiscountRangeValidator1');
    },
    get_uiAgencyDiscountRangeValidator2: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiAgencyDiscountRangeValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAgencyDiscountRangeValidator2');
    },
    get_uiCustomerRefLabel0: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCustomerRefLabel0() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCustomerRefLabel0');
    },
    get_uiCustomerRef: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCustomerRef() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCustomerRef');
    },
    get_uiPaymentTermsLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiPaymentTermsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPaymentTermsLabel');
    },
    get_uiPaymentTerms: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiPaymentTerms() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPaymentTerms');
    },
    get_uiInvoicePeriodLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiInvoicePeriodLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInvoicePeriodLabel');
    },
    get_uiInvoicePeriod: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiInvoicePeriod() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInvoicePeriod');
    },
    get_uiCampaignCreditsLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignCreditsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignCreditsLabel');
    },
    get_uiCampaignCredits: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignCredits() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignCredits');
    },
    get_uiCampaignCreditsOverrideCheckBox: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignCreditsOverrideCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignCreditsOverrideCheckBox');
    },
    get_uiCampaignCreditsOverride: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCampaignCreditsOverride() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCampaignCreditsOverride');
    },
    get_notesLabel: function SpottedScript_Admin_InsertionOrderScreen_View$get_notesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotesLabel');
    },
    get_uiNotesAddOnlyTextBox: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiNotesAddOnlyTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNotesAddOnlyTextBox');
    },
    get_uiInsertionOrderItemGridView: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiInsertionOrderItemGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInsertionOrderItemGridView');
    },
    get_uiSaveButton: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiSaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSaveButton');
    },
    get_uiCancelButton: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiCancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCancelButton');
    },
    get_uiRaiseButton: function SpottedScript_Admin_InsertionOrderScreen_View$get_uiRaiseButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiRaiseButton');
    },
    get_genericContainerPage: function SpottedScript_Admin_InsertionOrderScreen_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.InsertionOrderScreen.View.registerClass('SpottedScript.Admin.InsertionOrderScreen.View', SpottedScript.AdminUserControl.View);
