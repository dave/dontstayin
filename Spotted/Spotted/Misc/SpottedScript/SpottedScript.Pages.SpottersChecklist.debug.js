Type.registerNamespace('SpottedScript.Pages.SpottersChecklist');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.SpottersChecklist.View
SpottedScript.Pages.SpottersChecklist.View = function SpottedScript_Pages_SpottersChecklist_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.SpottersChecklist.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.SpottersChecklist.View.prototype = {
    clientId: null,
    get_h19: function SpottedScript_Pages_SpottersChecklist_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_genericContainerPage: function SpottedScript_Pages_SpottersChecklist_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.SpottersChecklist.View.registerClass('SpottedScript.Pages.SpottersChecklist.View', SpottedScript.DsiUserControl.View);
