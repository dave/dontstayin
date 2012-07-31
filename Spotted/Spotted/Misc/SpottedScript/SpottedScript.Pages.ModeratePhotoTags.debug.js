Type.registerNamespace('SpottedScript.Pages.ModeratePhotoTags');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.ModeratePhotoTags.View
SpottedScript.Pages.ModeratePhotoTags.View = function SpottedScript_Pages_ModeratePhotoTags_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.ModeratePhotoTags.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.ModeratePhotoTags.View.prototype = {
    clientId: null,
    get_h16: function SpottedScript_Pages_ModeratePhotoTags_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_uiPhotoImg: function SpottedScript_Pages_ModeratePhotoTags_View$get_uiPhotoImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoImg');
    },
    get_uiNoTags: function SpottedScript_Pages_ModeratePhotoTags_View$get_uiNoTags() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNoTags');
    },
    get_uiPhotoTags: function SpottedScript_Pages_ModeratePhotoTags_View$get_uiPhotoTags() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoTags');
    },
    get_genericContainerPage: function SpottedScript_Pages_ModeratePhotoTags_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.ModeratePhotoTags.View.registerClass('SpottedScript.Pages.ModeratePhotoTags.View', SpottedScript.DsiUserControl.View);
