Type.registerNamespace('SpottedScript.Admin.SalesLetterFollowUp');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesLetterFollowUp.View
SpottedScript.Admin.SalesLetterFollowUp.View = function SpottedScript_Admin_SalesLetterFollowUp_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesLetterFollowUp.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesLetterFollowUp.View.prototype = {
    clientId: null,
    get_promoterDataGrid: function SpottedScript_Admin_SalesLetterFollowUp_View$get_promoterDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesLetterFollowUp_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesLetterFollowUp.View.registerClass('SpottedScript.Admin.SalesLetterFollowUp.View', SpottedScript.AdminUserControl.View);
