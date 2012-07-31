Type.registerNamespace('SpottedScript.Pages.MyDetails');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyDetails.View
SpottedScript.Pages.MyDetails.View = function SpottedScript_Pages_MyDetails_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyDetails.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyDetails.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_MyDetails_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyDetails.View.registerClass('SpottedScript.Pages.MyDetails.View', SpottedScript.DsiUserControl.View);
