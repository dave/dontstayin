Type.registerNamespace('SpottedScript.Pages.Go');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Go.View
SpottedScript.Pages.Go.View = function SpottedScript_Pages_Go_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Go.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Go.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Go_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Go.View.registerClass('SpottedScript.Pages.Go.View', SpottedScript.DsiUserControl.View);
