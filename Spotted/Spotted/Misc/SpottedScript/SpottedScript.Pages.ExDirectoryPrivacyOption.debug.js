Type.registerNamespace('SpottedScript.Pages.ExDirectoryPrivacyOption');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.ExDirectoryPrivacyOption.View
SpottedScript.Pages.ExDirectoryPrivacyOption.View = function SpottedScript_Pages_ExDirectoryPrivacyOption_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.ExDirectoryPrivacyOption.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.ExDirectoryPrivacyOption.View.prototype = {
    clientId: null,
    get_uiExDirectory: function SpottedScript_Pages_ExDirectoryPrivacyOption_View$get_uiExDirectory() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiExDirectory');
    },
    get_uiSuccess: function SpottedScript_Pages_ExDirectoryPrivacyOption_View$get_uiSuccess() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSuccess');
    },
    get_genericContainerPage: function SpottedScript_Pages_ExDirectoryPrivacyOption_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.ExDirectoryPrivacyOption.View.registerClass('SpottedScript.Pages.ExDirectoryPrivacyOption.View', SpottedScript.DsiUserControl.View);
