Type.registerNamespace('SpottedScript.Blank.MailingUpload');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.MailingUpload.View
SpottedScript.Blank.MailingUpload.View = function SpottedScript_Blank_MailingUpload_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.MailingUpload.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.MailingUpload.View.prototype = {
    clientId: null,
    get_button1: function SpottedScript_Blank_MailingUpload_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_oldPass: function SpottedScript_Blank_MailingUpload_View$get_oldPass() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OldPass');
    },
    get_pass: function SpottedScript_Blank_MailingUpload_View$get_pass() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Pass');
    },
    get_button2: function SpottedScript_Blank_MailingUpload_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_genericContainerPage: function SpottedScript_Blank_MailingUpload_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.MailingUpload.View.registerClass('SpottedScript.Blank.MailingUpload.View', SpottedScript.BlankUserControl.View);
