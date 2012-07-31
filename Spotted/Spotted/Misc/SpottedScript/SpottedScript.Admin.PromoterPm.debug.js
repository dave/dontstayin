Type.registerNamespace('SpottedScript.Admin.PromoterPm');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.PromoterPm.View
SpottedScript.Admin.PromoterPm.View = function SpottedScript_Admin_PromoterPm_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.PromoterPm.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.PromoterPm.View.prototype = {
    clientId: null,
    get_button1: function SpottedScript_Admin_PromoterPm_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button2: function SpottedScript_Admin_PromoterPm_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_button3: function SpottedScript_Admin_PromoterPm_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_sendCommentButton: function SpottedScript_Admin_PromoterPm_View$get_sendCommentButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendCommentButton');
    },
    get_messageId: function SpottedScript_Admin_PromoterPm_View$get_messageId() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MessageId');
    },
    get_commentTextBox: function SpottedScript_Admin_PromoterPm_View$get_commentTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentTextBox');
    },
    get_genericContainerPage: function SpottedScript_Admin_PromoterPm_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.PromoterPm.View.registerClass('SpottedScript.Admin.PromoterPm.View', SpottedScript.AdminUserControl.View);
