Type.registerNamespace('SpottedScript.Pages.TalkToFrank');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.TalkToFrank.View
SpottedScript.Pages.TalkToFrank.View = function SpottedScript_Pages_TalkToFrank_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.TalkToFrank.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.TalkToFrank.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_TalkToFrank_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.TalkToFrank.View.registerClass('SpottedScript.Pages.TalkToFrank.View', SpottedScript.DsiUserControl.View);
