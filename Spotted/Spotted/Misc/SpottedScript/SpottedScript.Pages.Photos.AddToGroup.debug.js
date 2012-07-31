Type.registerNamespace('SpottedScript.Pages.Photos.AddToGroup');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Photos.AddToGroup.View
SpottedScript.Pages.Photos.AddToGroup.View = function SpottedScript_Pages_Photos_AddToGroup_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Photos.AddToGroup.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Photos.AddToGroup.View.prototype = {
    clientId: null,
    get_photoImg: function SpottedScript_Pages_Photos_AddToGroup_View$get_photoImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoImg');
    },
    get_photoAnchor: function SpottedScript_Pages_Photos_AddToGroup_View$get_photoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoAnchor');
    },
    get_groupRepeater: function SpottedScript_Pages_Photos_AddToGroup_View$get_groupRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupRepeater');
    },
    get_groupPanel: function SpottedScript_Pages_Photos_AddToGroup_View$get_groupPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPanel');
    },
    get_repeaterPanel: function SpottedScript_Pages_Photos_AddToGroup_View$get_repeaterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RepeaterPanel');
    },
    get_groupLabel: function SpottedScript_Pages_Photos_AddToGroup_View$get_groupLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupLabel');
    },
    get_showCheckBox: function SpottedScript_Pages_Photos_AddToGroup_View$get_showCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ShowCheckBox');
    },
    get_captionTextBox: function SpottedScript_Pages_Photos_AddToGroup_View$get_captionTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionTextBox');
    },
    get_h11: function SpottedScript_Pages_Photos_AddToGroup_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_captionCompetitionCheckBox: function SpottedScript_Pages_Photos_AddToGroup_View$get_captionCompetitionCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionCompetitionCheckBox');
    },
    get_button1: function SpottedScript_Pages_Photos_AddToGroup_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_sentEmailsLabel: function SpottedScript_Pages_Photos_AddToGroup_View$get_sentEmailsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SentEmailsLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Photos_AddToGroup_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Photos.AddToGroup.View.registerClass('SpottedScript.Pages.Photos.AddToGroup.View', SpottedScript.DsiUserControl.View);
