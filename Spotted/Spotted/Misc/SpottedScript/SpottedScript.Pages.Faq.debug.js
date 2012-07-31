Type.registerNamespace('SpottedScript.Pages.Faq');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Faq.View
SpottedScript.Pages.Faq.View = function SpottedScript_Pages_Faq_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Faq.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Faq.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Faq_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Faq.View.registerClass('SpottedScript.Pages.Faq.View', SpottedScript.DsiUserControl.View);
