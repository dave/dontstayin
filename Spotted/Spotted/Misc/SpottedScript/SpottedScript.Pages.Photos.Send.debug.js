Type.registerNamespace('SpottedScript.Pages.Photos.Send');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Photos.Send.View
SpottedScript.Pages.Photos.Send.View = function SpottedScript_Pages_Photos_Send_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Photos.Send.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Photos.Send.View.prototype = {
    clientId: null,
    get_h11: function SpottedScript_Pages_Photos_Send_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_photoAnchor: function SpottedScript_Pages_Photos_Send_View$get_photoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoAnchor');
    },
    get_photoImg: function SpottedScript_Pages_Photos_Send_View$get_photoImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoImg');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Photos_Send_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_messageHtml: function SpottedScript_Pages_Photos_Send_View$get_messageHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_MessageHtmlController');
    },
    get_buddyPanel: function SpottedScript_Pages_Photos_Send_View$get_buddyPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddyPanel');
    },
    get_multiBuddyChooser: function SpottedScript_Pages_Photos_Send_View$get_multiBuddyChooser() {
        /// <value type="SpottedScript.Controls.MultiBuddyChooser.Controller"></value>
        return eval(this.clientId + '_MultiBuddyChooserController');
    },
    get_button1: function SpottedScript_Pages_Photos_Send_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_sentEmailsLabel: function SpottedScript_Pages_Photos_Send_View$get_sentEmailsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SentEmailsLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Photos_Send_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Photos.Send.View.registerClass('SpottedScript.Pages.Photos.Send.View', SpottedScript.DsiUserControl.View);
