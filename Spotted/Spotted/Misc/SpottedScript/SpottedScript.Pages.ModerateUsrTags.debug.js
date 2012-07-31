Type.registerNamespace('SpottedScript.Pages.ModerateUsrTags');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.ModerateUsrTags.View
SpottedScript.Pages.ModerateUsrTags.View = function SpottedScript_Pages_ModerateUsrTags_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.ModerateUsrTags.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.ModerateUsrTags.View.prototype = {
    clientId: null,
    get_h16: function SpottedScript_Pages_ModerateUsrTags_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_uiTypeOfAction: function SpottedScript_Pages_ModerateUsrTags_View$get_uiTypeOfAction() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTypeOfAction');
    },
    get_uiNoTags: function SpottedScript_Pages_ModerateUsrTags_View$get_uiNoTags() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNoTags');
    },
    get_uiInfo: function SpottedScript_Pages_ModerateUsrTags_View$get_uiInfo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInfo');
    },
    get_genericContainerPage: function SpottedScript_Pages_ModerateUsrTags_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.ModerateUsrTags.View.registerClass('SpottedScript.Pages.ModerateUsrTags.View', SpottedScript.DsiUserControl.View);
