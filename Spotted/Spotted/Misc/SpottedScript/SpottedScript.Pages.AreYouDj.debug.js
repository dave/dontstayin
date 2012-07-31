Type.registerNamespace('SpottedScript.Pages.AreYouDj');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.AreYouDj.View
SpottedScript.Pages.AreYouDj.View = function SpottedScript_Pages_AreYouDj_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.AreYouDj.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.AreYouDj.View.prototype = {
    clientId: null,
    get_isDjYes: function SpottedScript_Pages_AreYouDj_View$get_isDjYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IsDjYes');
    },
    get_isDjNo: function SpottedScript_Pages_AreYouDj_View$get_isDjNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IsDjNo');
    },
    get_customValidatorIsDj: function SpottedScript_Pages_AreYouDj_View$get_customValidatorIsDj() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidatorIsDj');
    },
    get_savedPanel: function SpottedScript_Pages_AreYouDj_View$get_savedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SavedPanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_AreYouDj_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.AreYouDj.View.registerClass('SpottedScript.Pages.AreYouDj.View', SpottedScript.DsiUserControl.View);
