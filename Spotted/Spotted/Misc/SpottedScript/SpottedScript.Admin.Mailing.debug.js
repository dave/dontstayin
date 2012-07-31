Type.registerNamespace('SpottedScript.Admin.Mailing');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.Mailing.View
SpottedScript.Admin.Mailing.View = function SpottedScript_Admin_Mailing_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.Mailing.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.Mailing.View.prototype = {
    clientId: null,
    get_titleLabel: function SpottedScript_Admin_Mailing_View$get_titleLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TitleLabel');
    },
    get_doneLabel: function SpottedScript_Admin_Mailing_View$get_doneLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoneLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_Mailing_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.Mailing.View.registerClass('SpottedScript.Admin.Mailing.View', SpottedScript.AdminUserControl.View);
