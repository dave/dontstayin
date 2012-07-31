Type.registerNamespace('SpottedScript.Pages.Events.Review');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Review.View
SpottedScript.Pages.Events.Review.View = function SpottedScript_Pages_Events_Review_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Review.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Review.View.prototype = {
    clientId: null,
    get_summaryTextBox: function SpottedScript_Pages_Events_Review_View$get_summaryTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SummaryTextBox');
    },
    get_reviewBody: function SpottedScript_Pages_Events_Review_View$get_reviewBody() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReviewBody');
    },
    get_statusLabel: function SpottedScript_Pages_Events_Review_View$get_statusLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusLabel');
    },
    get_deleteReviewPanel: function SpottedScript_Pages_Events_Review_View$get_deleteReviewPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteReviewPanel');
    },
    get_cantEditPanel: function SpottedScript_Pages_Events_Review_View$get_cantEditPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CantEditPanel');
    },
    get_infoPanel: function SpottedScript_Pages_Events_Review_View$get_infoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoPanel');
    },
    get_deleteButton: function SpottedScript_Pages_Events_Review_View$get_deleteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteButton');
    },
    get_h11: function SpottedScript_Pages_Events_Review_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Events_Review_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_requiredfieldvalidator2: function SpottedScript_Pages_Events_Review_View$get_requiredfieldvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator2');
    },
    get_reviewHtml: function SpottedScript_Pages_Events_Review_View$get_reviewHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_ReviewHtmlController');
    },
    get_h12: function SpottedScript_Pages_Events_Review_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Review_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Review.View.registerClass('SpottedScript.Pages.Events.Review.View', SpottedScript.Pages.Events.EventUserControl.View);
