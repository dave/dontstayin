Type.registerNamespace('SpottedScript.DsiUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.DsiUserControl.View
SpottedScript.DsiUserControl.View = function SpottedScript_DsiUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.DsiUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.DsiUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_DsiUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.DsiUserControl.View.registerClass('SpottedScript.DsiUserControl.View', SpottedScript.GenericUserControl.View);
