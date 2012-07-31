Type.registerNamespace('SpottedScript.Admin.EventsWithNoSpendPromoters');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.EventsWithNoSpendPromoters.View
SpottedScript.Admin.EventsWithNoSpendPromoters.View = function SpottedScript_Admin_EventsWithNoSpendPromoters_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.EventsWithNoSpendPromoters.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.EventsWithNoSpendPromoters.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Admin_EventsWithNoSpendPromoters_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.EventsWithNoSpendPromoters.View.registerClass('SpottedScript.Admin.EventsWithNoSpendPromoters.View', SpottedScript.AdminUserControl.View);
