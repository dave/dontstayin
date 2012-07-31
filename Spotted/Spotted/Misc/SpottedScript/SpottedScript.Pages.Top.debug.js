Type.registerNamespace('SpottedScript.Pages.Top');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Top.View
SpottedScript.Pages.Top.View = function SpottedScript_Pages_Top_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Top.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Top.View.prototype = {
    clientId: null,
    get_topContent: function SpottedScript_Pages_Top_View$get_topContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopContent');
    },
    get_genericContainerPage: function SpottedScript_Pages_Top_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Top.View.registerClass('SpottedScript.Pages.Top.View', SpottedScript.DsiUserControl.View);
