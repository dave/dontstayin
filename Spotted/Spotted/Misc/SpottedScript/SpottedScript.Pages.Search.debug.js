Type.registerNamespace('SpottedScript.Pages.Search');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Search.View
SpottedScript.Pages.Search.View = function SpottedScript_Pages_Search_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Search.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Search.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Pages_Search_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_genericContainerPage: function SpottedScript_Pages_Search_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Search.View.registerClass('SpottedScript.Pages.Search.View', SpottedScript.DsiUserControl.View);
