Type.registerNamespace('SpottedScript.Admin.PromoterWithTicketRunsVatStatus');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.PromoterWithTicketRunsVatStatus.View
SpottedScript.Admin.PromoterWithTicketRunsVatStatus.View = function SpottedScript_Admin_PromoterWithTicketRunsVatStatus_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.PromoterWithTicketRunsVatStatus.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.PromoterWithTicketRunsVatStatus.View.prototype = {
    clientId: null,
    get_promoterWithTicketRunsVatStatusPanel: function SpottedScript_Admin_PromoterWithTicketRunsVatStatus_View$get_promoterWithTicketRunsVatStatusPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterWithTicketRunsVatStatusPanel');
    },
    get_vatStatusDropDownList: function SpottedScript_Admin_PromoterWithTicketRunsVatStatus_View$get_vatStatusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VatStatusDropDownList');
    },
    get_sendReminderEmailForUnknownVatStatusPromotersButton: function SpottedScript_Admin_PromoterWithTicketRunsVatStatus_View$get_sendReminderEmailForUnknownVatStatusPromotersButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendReminderEmailForUnknownVatStatusPromotersButton');
    },
    get_unknownPromoterVatStatusGridView: function SpottedScript_Admin_PromoterWithTicketRunsVatStatus_View$get_unknownPromoterVatStatusGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UnknownPromoterVatStatusGridView');
    },
    get_genericContainerPage: function SpottedScript_Admin_PromoterWithTicketRunsVatStatus_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.PromoterWithTicketRunsVatStatus.View.registerClass('SpottedScript.Admin.PromoterWithTicketRunsVatStatus.View', SpottedScript.AdminUserControl.View);
