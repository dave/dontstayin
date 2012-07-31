Type.registerNamespace('SpottedScript.Pages.MyComments');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyComments.View
SpottedScript.Pages.MyComments.View = function SpottedScript_Pages_MyComments_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyComments.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyComments.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_MyComments_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyComments.View.registerClass('SpottedScript.Pages.MyComments.View', SpottedScript.DsiUserControl.View);
